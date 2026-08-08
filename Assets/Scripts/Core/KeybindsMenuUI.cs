public class KeybindsMenuUI : global::UnityEngine.MonoBehaviour
{
	[global::System.Serializable]
	public struct RebindRow
	{
		public string actionName;

		[global::UnityEngine.Header("Keyboard binding")]
		public int keyboardBindingIndex;

		public global::UnityEngine.UI.Button keyboardButton;

		public global::TMPro.TMP_Text keyboardLabel;

		[global::UnityEngine.Header("Controller binding")]
		public int controllerBindingIndex;

		public global::UnityEngine.UI.Button controllerButton;

		public global::TMPro.TMP_Text controllerLabel;
	}

	[global::UnityEngine.Header("References")]
	[global::UnityEngine.SerializeField]
	private global::System.Collections.Generic.List<KeybindsMenuUI.RebindRow> rows;

	[global::UnityEngine.Header("Reset")]
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Button resetAllButton;

	[global::UnityEngine.Header("Waiting-for-input overlay")]
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject listeningOverlay;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TMP_Text listeningLabel;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject warningOverlay;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TMP_Text warningLabel;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_warningCancelBtn;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_afterWarningButton;

	private global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation _activeOp;

	private InputRebindingManager rebindingManager => InputRebindingManager.Instance;

	private void OnEnable()
	{
		RefreshAllLabels();
		rebindingManager.OnBindingsChanged += RefreshAllLabels;
		foreach (KeybindsMenuUI.RebindRow row in rows)
		{
			KeybindsMenuUI.RebindRow r = row;
			r.keyboardButton.onClick.RemoveAllListeners();
			r.keyboardButton.onClick.AddListener(delegate
			{
				BeginRebind(r.actionName, r.keyboardBindingIndex, isController: false);
			});
			r.controllerButton.onClick.RemoveAllListeners();
			r.controllerButton.onClick.AddListener(delegate
			{
				BeginRebind(r.actionName, r.controllerBindingIndex, isController: true);
			});
		}
		if (resetAllButton != null)
		{
			resetAllButton.onClick.RemoveAllListeners();
			resetAllButton.onClick.AddListener(OnResetAll);
		}
		if (listeningOverlay != null)
		{
			listeningOverlay.SetActive(value: false);
		}
	}

	private void OnDisable()
	{
		rebindingManager.OnBindingsChanged -= RefreshAllLabels;
		CancelActiveRebind();
	}

	private void BeginRebind(string actionName, int bindingIndex, bool isController)
	{
		CancelActiveRebind();
		if (listeningOverlay != null)
		{
			listeningOverlay.SetActive(value: true);
			if (listeningLabel != null)
			{
				listeningLabel.text = "Rebinding  \"" + global::System.Text.RegularExpressions.Regex.Replace(actionName, "(?<!^)([A-Z])", " $1") + "\"\n" + (isController ? "[Press a gamepad button]" : "[Press a key]");
			}
		}
		_activeOp = rebindingManager.StartRebind(actionName, bindingIndex, excludeMouse: true, delegate
		{
			HideOverlay();
		}, delegate
		{
			HideOverlay();
		});
	}

	private void CancelActiveRebind()
	{
		if (_activeOp != null && _activeOp.started)
		{
			_activeOp.Cancel();
		}
		_activeOp = null;
	}

	private void HideOverlay()
	{
		if (listeningOverlay != null)
		{
			listeningOverlay.SetActive(value: false);
		}
	}

	private void OnResetAll()
	{
		CancelActiveRebind();
		if (warningOverlay != null)
		{
			warningOverlay.SetActive(value: true);
			global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(m_warningCancelBtn);
			if (warningLabel != null)
			{
				warningLabel.text = "Are you sure you want to reset all bindings to their default state?";
			}
		}
	}

	public void ResetConfirm()
	{
		rebindingManager.ResetAllToDefault();
		warningOverlay.SetActive(value: false);
		global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(m_afterWarningButton);
	}

	public void ResetCancel()
	{
		warningOverlay.SetActive(value: false);
		global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(m_afterWarningButton);
	}

	private void RefreshAllLabels()
	{
		foreach (KeybindsMenuUI.RebindRow row in rows)
		{
			if (row.keyboardLabel != null)
			{
				row.keyboardLabel.text = rebindingManager.GetBindingDisplayString(row.actionName, row.keyboardBindingIndex);
			}
			if (row.controllerLabel != null)
			{
				row.controllerLabel.text = rebindingManager.GetBindingDisplayString(row.actionName, row.controllerBindingIndex);
			}
		}
	}
}
