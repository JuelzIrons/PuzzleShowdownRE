[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.InputSystem.UI.InputSystemUIInputModule))]
public class UiInputDeviceArbiter : global::UnityEngine.MonoBehaviour
{
	private enum InputMode
	{
		Pointer = 0,
		Navigation = 1
	}

	[global::UnityEngine.Tooltip("Minimum mouse delta (pixels) to count as 'mouse moved' and reclaim pointer control.")]
	[global::UnityEngine.SerializeField]
	private float mouseMoveThreshold = 0.5f;

	private global::UnityEngine.InputSystem.UI.InputSystemUIInputModule _module;

	private bool _pointerDisabled;

	private global::UnityEngine.Vector2 _lastMousePos;

	private UiInputDeviceArbiter.InputMode _mode;

	private global::UnityEngine.GameObject _lastSelected;

	private bool _mouseActivityThisFrame;

	private void Awake()
	{
		_module = GetComponent<global::UnityEngine.InputSystem.UI.InputSystemUIInputModule>();
	}

	private void OnEnable()
	{
		global::UnityEngine.InputSystem.InputSystem.onEvent += new global::System.Action<global::UnityEngine.InputSystem.LowLevel.InputEventPtr, global::UnityEngine.InputSystem.InputDevice>(OnAnyInputEvent);
		if (global::UnityEngine.InputSystem.Mouse.current != null)
		{
			_lastMousePos = global::UnityEngine.InputSystem.Mouse.current.position.ReadValue();
		}
		_lastSelected = ((global::UnityEngine.EventSystems.EventSystem.current != null) ? global::UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject : null);
	}

	private void OnDisable()
	{
		global::UnityEngine.InputSystem.InputSystem.onEvent -= new global::System.Action<global::UnityEngine.InputSystem.LowLevel.InputEventPtr, global::UnityEngine.InputSystem.InputDevice>(OnAnyInputEvent);
		RestorePointer();
	}

	private void Update()
	{
		if (global::UnityEngine.EventSystems.EventSystem.current == null)
		{
			return;
		}
		global::UnityEngine.GameObject currentSelectedGameObject = global::UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
		if (currentSelectedGameObject != _lastSelected)
		{
			_lastSelected = currentSelectedGameObject;
			if (currentSelectedGameObject != null && !_mouseActivityThisFrame)
			{
				SwitchToNavigationMode();
			}
		}
		_mouseActivityThisFrame = false;
	}

	private void OnAnyInputEvent(global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, global::UnityEngine.InputSystem.InputDevice device)
	{
		if (!eventPtr.valid || device == null)
		{
			return;
		}
		if (device is global::UnityEngine.InputSystem.Mouse mouse)
		{
			global::UnityEngine.Vector2 vector = mouse.position.ReadValue();
			if (global::UnityEngine.Vector2.Distance(vector, _lastMousePos) > mouseMoveThreshold)
			{
				_lastMousePos = vector;
				_mouseActivityThisFrame = true;
				SwitchToPointerMode();
			}
			else if (mouse.leftButton.wasPressedThisFrame || mouse.rightButton.wasPressedThisFrame || mouse.middleButton.wasPressedThisFrame || mouse.scroll.ReadValue() != global::UnityEngine.Vector2.zero)
			{
				_mouseActivityThisFrame = true;
				SwitchToPointerMode();
			}
		}
		else if (device is global::UnityEngine.InputSystem.Gamepad gamepad)
		{
			if (global::UnityEngine.InputSystem.InputControlExtensions.IsPressed(gamepad.dpad) || gamepad.leftStick.ReadValue().sqrMagnitude > 0.25f || gamepad.rightStick.ReadValue().sqrMagnitude > 0.25f || gamepad.buttonSouth.wasPressedThisFrame || gamepad.buttonNorth.wasPressedThisFrame || gamepad.buttonEast.wasPressedThisFrame || gamepad.buttonWest.wasPressedThisFrame)
			{
				SwitchToNavigationMode();
			}
		}
		else if (device is global::UnityEngine.InputSystem.Keyboard keyboard && (keyboard.upArrowKey.wasPressedThisFrame || keyboard.downArrowKey.wasPressedThisFrame || keyboard.leftArrowKey.wasPressedThisFrame || keyboard.rightArrowKey.wasPressedThisFrame || keyboard.tabKey.wasPressedThisFrame || keyboard.enterKey.wasPressedThisFrame))
		{
			SwitchToNavigationMode();
		}
	}

	private void SwitchToNavigationMode()
	{
		if (_mode == UiInputDeviceArbiter.InputMode.Navigation)
		{
			return;
		}
		_mode = UiInputDeviceArbiter.InputMode.Navigation;
		DisablePointer();
		if (global::UnityEngine.EventSystems.EventSystem.current != null && global::UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject == null)
		{
			global::UnityEngine.GameObject firstSelectedGameObject = global::UnityEngine.EventSystems.EventSystem.current.firstSelectedGameObject;
			if (firstSelectedGameObject != null)
			{
				global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(firstSelectedGameObject);
			}
		}
	}

	private void SwitchToPointerMode()
	{
		if (_mode != UiInputDeviceArbiter.InputMode.Pointer)
		{
			_mode = UiInputDeviceArbiter.InputMode.Pointer;
			RestorePointer();
		}
	}

	private void DisablePointer()
	{
		if (!_pointerDisabled && !(_module == null))
		{
			_pointerDisabled = true;
			_module.point?.action?.Disable();
			_module.leftClick?.action?.Disable();
			_module.middleClick?.action?.Disable();
			_module.rightClick?.action?.Disable();
			_module.scrollWheel?.action?.Disable();
			ClearPointerHoverState();
		}
	}

	private void RestorePointer()
	{
		if (_pointerDisabled && !(_module == null))
		{
			_pointerDisabled = false;
			_module.point?.action?.Enable();
			_module.leftClick?.action?.Enable();
			_module.middleClick?.action?.Enable();
			_module.rightClick?.action?.Enable();
			_module.scrollWheel?.action?.Enable();
		}
	}

	private void ClearPointerHoverState()
	{
		if (global::UnityEngine.EventSystems.EventSystem.current == null)
		{
			return;
		}
		global::UnityEngine.EventSystems.PointerEventData pointerEventData = new global::UnityEngine.EventSystems.PointerEventData(global::UnityEngine.EventSystems.EventSystem.current);
		global::UnityEngine.GameObject currentSelectedGameObject = global::UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
		global::UnityEngine.UI.Selectable[] allSelectablesArray = global::UnityEngine.UI.Selectable.allSelectablesArray;
		foreach (global::UnityEngine.UI.Selectable selectable in allSelectablesArray)
		{
			if (!(selectable == null) && !(selectable.gameObject == currentSelectedGameObject))
			{
				pointerEventData.pointerEnter = null;
				global::UnityEngine.EventSystems.ExecuteEvents.Execute(selectable.gameObject, pointerEventData, global::UnityEngine.EventSystems.ExecuteEvents.pointerExitHandler);
				selectable.OnPointerExit(pointerEventData);
			}
		}
	}
}
