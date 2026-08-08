public class OperationModeManager : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI m_errorMsg;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_controllerbtn;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_kbBtn;

	private bool m_hasSelected;

	private void Start()
	{
		PersistentInputReader.Instance.m_keyboardInput.neverAutoSwitchControlSchemes = false;
		if (HasConnectedController(out var _))
		{
			global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(m_controllerbtn);
		}
		else
		{
			global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(m_kbBtn);
		}
	}

	public void TrySelectKbMouse()
	{
		if (!m_hasSelected)
		{
			if (HasConnectedKbandMouse())
			{
				m_hasSelected = true;
				global::UnityEngine.InputSystem.InputDevice[] devices = new global::UnityEngine.InputSystem.InputDevice[2]
				{
					global::UnityEngine.InputSystem.Keyboard.current,
					global::UnityEngine.InputSystem.Mouse.current
				};
				PersistentInputReader.Instance.m_keyboardInput.SwitchCurrentControlScheme("Keyboard&Mouse", devices);
				PersistentInputReader.Instance.m_keyboardInput.neverAutoSwitchControlSchemes = true;
				PersistentInputReader.Instance.CustomMenuCtrlSwapper.enabled = false;
				SceneLoader.Instance.LoadSceneByEnumRegularFade(AllGameScenes.SplashScreen);
			}
			else
			{
				DisplayError("No keyboard/mouse connected!");
			}
		}
	}

	public void TrySelectController()
	{
		if (!m_hasSelected)
		{
			if (HasConnectedController(out var firstController))
			{
				m_hasSelected = true;
				PersistentInputReader.Instance.m_keyboardInput.neverAutoSwitchControlSchemes = true;
				PersistentInputReader.Instance.m_keyboardInput.SwitchCurrentControlScheme(firstController);
				PersistentInputReader.Instance.CustomMenuCtrlSwapper.enabled = false;
			}
			else
			{
				DisplayError("No controller connected!");
			}
		}
	}

	private void DisplayError(string errorText)
	{
		m_errorMsg.GetComponent<global::UnityEngine.Animation>().Rewind();
		m_errorMsg.GetComponent<global::UnityEngine.Animation>().Play();
		m_errorMsg.text = errorText;
	}

	private bool HasConnectedController(out global::UnityEngine.InputSystem.InputDevice firstController)
	{
		firstController = null;
		global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Gamepad> all = global::UnityEngine.InputSystem.Gamepad.all;
		global::UnityEngine.Debug.Log($"{all.Count} gamepad(s) connected");
		if (all.Count != 0)
		{
			firstController = all[0];
			return true;
		}
		return false;
	}

	private bool HasConnectedKbandMouse()
	{
		if (global::UnityEngine.InputSystem.Keyboard.current != null && global::UnityEngine.InputSystem.Mouse.current != null)
		{
			return true;
		}
		return false;
	}
}
