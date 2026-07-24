public class PersistentInputReader : global::UnityEngine.MonoBehaviour
{
	public static PersistentInputReader Instance;

	public global::UnityEngine.InputSystem.PlayerInput m_keyboardInput;

	public global::UnityEngine.InputSystem.PlayerInput m_controllerInput1;

	public global::UnityEngine.InputSystem.PlayerInput m_controllerInput2;

	public CustomMenuControlSchemeBehaviour CustomMenuCtrlSwapper;

	public bool IsSelfUpdating;

	public bool IsSupressing;

	public bool AllowPausing;

	private bool m_flushKeyboardMove;

	private bool m_flushController1Move;

	private bool m_flushController2Move;

	public global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> KeyboardSwapAction;

	public global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> KeyboardAltSwapAction;

	public global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> KeyboardMoveAction;

	public global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> KeyboardStartAction;

	public global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> KeyboardForceUpAction;

	public global::System.Action KeyboardAnyAction;

	public global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> KeyboardSpecialEnterAction;

	public global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> Controller1SwapAction;

	public global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> Controller1AltSwapAction;

	public global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> Controller1MoveAction;

	public global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> Controller1StartAction;

	public global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> Controller1ForceUpAction;

	public global::System.Action Controller1AnyAction;

	public global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> Controller2SwapAction;

	public global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> Controller2AltSwapAction;

	public global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> Controller2MoveAction;

	public global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> Controller2StartAction;

	public global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> Controller2ForceUpAction;

	public global::System.Action Controller2AnyAction;

	public void SetSelfUpdate(bool selfUpdating)
	{
		IsSelfUpdating = selfUpdating;
	}

	public void SetSupressAllEvents(bool shouldSupressEvents)
	{
		IsSupressing = shouldSupressEvents;
		if (!shouldSupressEvents)
		{
			m_flushKeyboardMove = IsAnyMoveKeyHeld(m_keyboardInput);
			m_flushController1Move = IsAnyMoveKeyHeld(m_controllerInput1);
			m_flushController2Move = IsAnyMoveKeyHeld(m_controllerInput2);
		}
	}

	private bool IsAnyMoveKeyHeld(global::UnityEngine.InputSystem.PlayerInput input)
	{
		if (!(input.actions["Move Up"].ReadValue<global::UnityEngine.Vector2>() != global::UnityEngine.Vector2.zero) && !(input.actions["Move Down"].ReadValue<global::UnityEngine.Vector2>() != global::UnityEngine.Vector2.zero) && !(input.actions["Move Left"].ReadValue<global::UnityEngine.Vector2>() != global::UnityEngine.Vector2.zero))
		{
			return input.actions["Move Right"].ReadValue<global::UnityEngine.Vector2>() != global::UnityEngine.Vector2.zero;
		}
		return true;
	}

	public void ClearAllSubscriptions()
	{
		KeyboardSwapAction = null;
		KeyboardAltSwapAction = null;
		KeyboardMoveAction = null;
		KeyboardStartAction = null;
		KeyboardForceUpAction = null;
		KeyboardAnyAction = null;
		KeyboardSpecialEnterAction = null;
		Controller1SwapAction = null;
		Controller1AltSwapAction = null;
		Controller1MoveAction = null;
		Controller1StartAction = null;
		Controller1ForceUpAction = null;
		Controller1AnyAction = null;
		Controller2SwapAction = null;
		Controller2AltSwapAction = null;
		Controller2MoveAction = null;
		Controller2StartAction = null;
		Controller2ForceUpAction = null;
		Controller2AnyAction = null;
	}

	public void SetKeyboardEnabled(bool isEnabled)
	{
		m_keyboardInput.enabled = isEnabled;
	}

	public void SetController1Enabled(bool isEnabled)
	{
		m_controllerInput1.enabled = isEnabled;
	}

	public void SetController2Enabled(bool isEnabled)
	{
		m_controllerInput2.enabled = isEnabled;
	}

	public bool CanPlayLocalMp()
	{
		int count = global::UnityEngine.InputSystem.Gamepad.all.Count;
		bool flag = global::UnityEngine.InputSystem.Keyboard.current != null;
		if (flag && count == 0)
		{
			return false;
		}
		if (!flag && count == 1)
		{
			return false;
		}
		return true;
	}

	private void Start()
	{
		if (Instance != null)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		Instance = this;
		global::UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		m_keyboardInput.enabled = true;
		m_controllerInput1.enabled = true;
		m_controllerInput2.enabled = true;
		AssignDevices();
		if (InputRebindingManager.Instance != null)
		{
			InputRebindingManager.Instance.LoadOverrides();
		}
		global::UnityEngine.InputSystem.InputSystem.onDeviceChange += OnDeviceChange;
	}

	private void OnDestroy()
	{
		global::UnityEngine.InputSystem.InputSystem.onDeviceChange -= OnDeviceChange;
	}

	private void OnDeviceChange(global::UnityEngine.InputSystem.InputDevice device, global::UnityEngine.InputSystem.InputDeviceChange change)
	{
		if (device is global::UnityEngine.InputSystem.Gamepad && (change == global::UnityEngine.InputSystem.InputDeviceChange.Added || change == global::UnityEngine.InputSystem.InputDeviceChange.Removed))
		{
			AssignDevices();
		}
		if (LocalMpCtrlManager.Instance != null)
		{
			LocalMpCtrlManager.Instance.DevicesChanged();
		}
	}

	private void AssignDevices()
	{
		global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Gamepad> all = global::UnityEngine.InputSystem.Gamepad.all;
		if (global::UnityEngine.InputSystem.Keyboard.current != null)
		{
			global::UnityEngine.InputSystem.InputDevice[] devices = ((global::UnityEngine.InputSystem.Mouse.current == null) ? new global::UnityEngine.InputSystem.InputDevice[1] { global::UnityEngine.InputSystem.Keyboard.current } : new global::UnityEngine.InputSystem.InputDevice[2]
			{
				global::UnityEngine.InputSystem.Keyboard.current,
				global::UnityEngine.InputSystem.Mouse.current
			});
			m_keyboardInput.SwitchCurrentControlScheme("Keyboard&Mouse", devices);
		}
		if (all.Count >= 1)
		{
			m_controllerInput1.enabled = true;
			m_controllerInput1.SwitchCurrentControlScheme("Gamepad", all[0]);
		}
		else
		{
			m_controllerInput1.enabled = false;
		}
		if (all.Count >= 2)
		{
			m_controllerInput2.enabled = true;
			m_controllerInput2.SwitchCurrentControlScheme("Gamepad", all[1]);
		}
		else
		{
			m_controllerInput2.enabled = false;
		}
	}

	private void Update()
	{
		if (IsSelfUpdating)
		{
			global::UnityEngine.InputSystem.InputSystem.Update();
		}
	}

	public void OnNewSceneLoaded()
	{
		if (global::UnityEngine.EventSystems.EventSystem.current != null)
		{
			m_keyboardInput.uiInputModule = null;
			m_controllerInput1.uiInputModule = null;
			m_controllerInput2.uiInputModule = null;
		}
	}

	public void KeyboardSwap(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		if (!IsSupressing)
		{
			KeyboardSwapAction?.Invoke(context);
			KeyboardAnyAction?.Invoke();
		}
	}

	public void KeyboardAltSwap(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		if (!IsSupressing)
		{
			KeyboardAltSwapAction?.Invoke(context);
			KeyboardAnyAction?.Invoke();
		}
	}

	public void KeyboardMove(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		if (IsSupressing)
		{
			return;
		}
		if (m_flushKeyboardMove)
		{
			if (!IsAnyMoveKeyHeld(m_keyboardInput))
			{
				m_flushKeyboardMove = false;
			}
		}
		else
		{
			KeyboardMoveAction?.Invoke(context);
			KeyboardAnyAction?.Invoke();
		}
	}

	public void KeyboardStart(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		KeyboardStartAction?.Invoke(context);
		KeyboardAnyAction?.Invoke();
		if (context.performed && GameManager.Instance != null && AllowPausing && (!(DialogueManager.Instance != null) || !DialogueManager.Instance.m_currentyReading) && GameManager.Instance.DefinedGameMode != GameModeType.Online)
		{
			global::UnityEngine.EventSystems.EventSystem.current?.transform.GetComponent<PauseManager>().TogglePause(fromP1: true);
		}
	}

	public void KeyboardForceUp(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		if (!IsSupressing)
		{
			KeyboardForceUpAction?.Invoke(context);
			KeyboardAnyAction?.Invoke();
		}
	}

	public void KeyboardSpecialEnter(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		if (!IsSupressing)
		{
			KeyboardSpecialEnterAction?.Invoke(context);
			KeyboardAnyAction?.Invoke();
		}
	}

	public void Controller1Swap(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		if (!IsSupressing)
		{
			Controller1SwapAction?.Invoke(context);
			Controller1AnyAction?.Invoke();
		}
	}

	public void Controller1AltSwap(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		if (!IsSupressing)
		{
			Controller1AltSwapAction?.Invoke(context);
			Controller1AnyAction?.Invoke();
		}
	}

	public void Controller1Move(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		if (IsSupressing)
		{
			return;
		}
		if (m_flushController1Move)
		{
			if (!IsAnyMoveKeyHeld(m_controllerInput1))
			{
				m_flushController1Move = false;
			}
		}
		else
		{
			Controller1MoveAction?.Invoke(context);
			Controller1AnyAction?.Invoke();
		}
	}

	public void Controller1Start(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		Controller1StartAction?.Invoke(context);
		Controller1AnyAction?.Invoke();
		if (context.performed && GameManager.Instance != null && AllowPausing && (!(DialogueManager.Instance != null) || !DialogueManager.Instance.m_currentyReading) && GameManager.Instance.DefinedGameMode != GameModeType.Online)
		{
			global::UnityEngine.EventSystems.EventSystem.current?.transform.GetComponent<PauseManager>().TogglePause(fromP1: true);
		}
	}

	public void Controller1ForceUp(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		if (!IsSupressing)
		{
			Controller1ForceUpAction?.Invoke(context);
			Controller1AnyAction?.Invoke();
		}
	}

	public void Controller2Swap(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		if (!IsSupressing)
		{
			Controller2SwapAction?.Invoke(context);
			Controller2AnyAction?.Invoke();
		}
	}

	public void Controller2AltSwap(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		if (!IsSupressing)
		{
			Controller2AltSwapAction?.Invoke(context);
			Controller2AnyAction?.Invoke();
		}
	}

	public void Controller2Move(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		global::UnityEngine.Debug.Log("CONTROLLER 2 moveoeoeoe");
		if (IsSupressing)
		{
			return;
		}
		if (m_flushController2Move)
		{
			if (!IsAnyMoveKeyHeld(m_controllerInput2))
			{
				m_flushController2Move = false;
			}
		}
		else
		{
			Controller2MoveAction?.Invoke(context);
			Controller2AnyAction?.Invoke();
		}
	}

	public void Controller2Start(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		Controller2StartAction?.Invoke(context);
		Controller2AnyAction?.Invoke();
		if (context.performed && GameManager.Instance != null && AllowPausing && (!(DialogueManager.Instance != null) || !DialogueManager.Instance.m_currentyReading) && GameManager.Instance.DefinedGameMode != GameModeType.Online)
		{
			global::UnityEngine.EventSystems.EventSystem.current?.transform.GetComponent<PauseManager>().TogglePause(fromP1: true);
		}
	}

	public void Controller2ForceUp(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		if (!IsSupressing)
		{
			Controller2ForceUpAction?.Invoke(context);
			Controller2AnyAction?.Invoke();
		}
	}
}
