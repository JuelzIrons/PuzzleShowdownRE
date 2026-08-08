public class InputRebindingManager : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.Header("Player Input Components (from PersistentInputReader)")]
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.InputSystem.PlayerInput keyboardInput;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.InputSystem.PlayerInput controllerInput1;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.InputSystem.PlayerInput controllerInput2;

	private const string OverridesKey = "InputBindingOverrides";

	public static InputRebindingManager Instance;

	public event global::System.Action OnBindingsChanged;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
	}

	public global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation StartRebind(string actionName, int bindingIndex = -1, bool excludeMouse = true, global::System.Action<string> onComplete = null, global::System.Action onCancel = null)
	{
		global::UnityEngine.InputSystem.InputAction action = keyboardInput.actions.FindAction(actionName, throwIfNotFound: true);
		int idx = ((bindingIndex >= 0) ? bindingIndex : GetFirstNonCompositeBinding(action));
		action.Disable();
		global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation rebindingOperation = global::UnityEngine.InputSystem.InputActionRebindingExtensions.PerformInteractiveRebinding(action, idx).WithCancelingThrough("<Keyboard>/escape").WithExpectedControlType("Button")
			.OnMatchWaitForAnother(0.1f);
		if (excludeMouse)
		{
			rebindingOperation.WithControlsExcluding("Mouse");
		}
		rebindingOperation.OnComplete(delegate(global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation op)
		{
			op.Dispose();
			action.Enable();
			SaveAndMirrorOverrides();
			this.OnBindingsChanged?.Invoke();
			onComplete?.Invoke(global::UnityEngine.InputSystem.InputActionRebindingExtensions.GetBindingDisplayString(action, idx));
		}).OnCancel(delegate(global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation op)
		{
			op.Dispose();
			action.Enable();
			onCancel?.Invoke();
		}).Start();
		return rebindingOperation;
	}

	public void BindStickToMovement(string stickPath, string upAction = "Move Up", string downAction = "Move Down", string leftAction = "Move Left", string rightAction = "Move Right", int upIdx = -1, int downIdx = -1, int leftIdx = -1, int rightIdx = -1)
	{
		ApplyStickPart(upAction, upIdx, stickPath + "/up");
		ApplyStickPart(downAction, downIdx, stickPath + "/down");
		ApplyStickPart(leftAction, leftIdx, stickPath + "/left");
		ApplyStickPart(rightAction, rightIdx, stickPath + "/right");
		SaveAndMirrorOverrides();
		this.OnBindingsChanged?.Invoke();
	}

	private void ApplyStickPart(string actionName, int bindingIndex, string fullPath)
	{
		global::UnityEngine.InputSystem.InputAction action = keyboardInput.actions.FindAction(actionName, throwIfNotFound: true);
		int bindingIndex2 = ((bindingIndex >= 0) ? bindingIndex : GetFirstNonCompositeBinding(action));
		global::UnityEngine.InputSystem.InputActionRebindingExtensions.ApplyBindingOverride(action, bindingIndex2, fullPath);
	}

	public void ResetActionToDefault(string actionName)
	{
		global::UnityEngine.InputSystem.InputActionRebindingExtensions.RemoveAllBindingOverrides(keyboardInput.actions.FindAction(actionName, throwIfNotFound: true));
		SaveAndMirrorOverrides();
		this.OnBindingsChanged?.Invoke();
	}

	public void ResetAllToDefault()
	{
		global::UnityEngine.InputSystem.InputActionRebindingExtensions.RemoveAllBindingOverrides(keyboardInput.actions);
		ApplyJsonToAll("");
		global::UnityEngine.PlayerPrefs.DeleteKey("InputBindingOverrides");
		global::UnityEngine.PlayerPrefs.Save();
		this.OnBindingsChanged?.Invoke();
	}

	public string GetBindingDisplayString(string actionName, int bindingIndex = -1)
	{
		global::UnityEngine.InputSystem.InputAction inputAction = keyboardInput.actions.FindAction(actionName, throwIfNotFound: true);
		int index = ((bindingIndex >= 0) ? bindingIndex : GetFirstNonCompositeBinding(inputAction));
		global::UnityEngine.InputSystem.InputBinding inputBinding = inputAction.bindings[index];
		string text = (string.IsNullOrEmpty(inputBinding.overridePath) ? inputBinding.path : inputBinding.overridePath);
		int num = text.LastIndexOf('/');
		return global::System.Text.RegularExpressions.Regex.Replace((num >= 0) ? text.Substring(num + 1) : text, "(\\B[A-Z])", " $1");
	}

	private void SaveAndMirrorOverrides()
	{
		string text = global::UnityEngine.InputSystem.InputActionRebindingExtensions.SaveBindingOverridesAsJson(keyboardInput.actions);
		ApplyJsonToAll(text);
		global::UnityEngine.PlayerPrefs.SetString("InputBindingOverrides", text);
		global::UnityEngine.PlayerPrefs.Save();
	}

	public void LoadOverrides()
	{
		if (global::UnityEngine.PlayerPrefs.HasKey("InputBindingOverrides"))
		{
			string json = global::UnityEngine.PlayerPrefs.GetString("InputBindingOverrides");
			ApplyJsonToAll(json);
		}
	}

	private void ApplyJsonToAll(string json)
	{
		ApplyToPlayerInput(keyboardInput, json);
		ApplyToPlayerInput(controllerInput1, json);
		ApplyToPlayerInput(controllerInput2, json);
	}

	private static void ApplyToPlayerInput(global::UnityEngine.InputSystem.PlayerInput pi, string json)
	{
		if (!(pi == null) && !(pi.actions == null))
		{
			if (string.IsNullOrEmpty(json))
			{
				global::UnityEngine.InputSystem.InputActionRebindingExtensions.RemoveAllBindingOverrides(pi.actions);
			}
			else
			{
				global::UnityEngine.InputSystem.InputActionRebindingExtensions.LoadBindingOverridesFromJson(pi.actions, json);
			}
			pi.actions.Disable();
			pi.actions.Enable();
		}
	}

	private static int GetFirstNonCompositeBinding(global::UnityEngine.InputSystem.InputAction action)
	{
		for (int i = 0; i < action.bindings.Count; i++)
		{
			if (!action.bindings[i].isComposite && !action.bindings[i].isPartOfComposite)
			{
				return i;
			}
		}
		return 0;
	}
}
