namespace UnityEngine.InputSystem.UI
{
	[global::UnityEngine.AddComponentMenu("Input/Virtual Mouse")]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.inputsystem@1.18/manual/UISupport.html#virtual-mouse-cursor-control")]
	public class VirtualMouseInput : global::UnityEngine.MonoBehaviour
	{
		public enum CursorMode
		{
			SoftwareCursor = 0,
			HardwareCursorIfAvailable = 1
		}

		[global::UnityEngine.Header("Cursor")]
		[global::UnityEngine.Tooltip("Whether the component should set the cursor position of the hardware mouse cursor, if one is available. If so, the software cursor pointed (to by 'Cursor Graphic') will be hidden.")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.InputSystem.UI.VirtualMouseInput.CursorMode m_CursorMode;

		[global::UnityEngine.Tooltip("The graphic that represents the software cursor. This is hidden if a hardware cursor (see 'Cursor Mode') is used.")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.Graphic m_CursorGraphic;

		[global::UnityEngine.Tooltip("The transform for the software cursor. Will only be set if a software cursor is used (see 'Cursor Mode'). Moving the cursor updates the anchored position of the transform.")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.RectTransform m_CursorTransform;

		[global::UnityEngine.Header("Motion")]
		[global::UnityEngine.Tooltip("Speed in pixels per second with which to move the cursor. Scaled by the input from 'Stick Action'.")]
		[global::UnityEngine.SerializeField]
		private float m_CursorSpeed = 400f;

		[global::UnityEngine.Tooltip("Scale factor to apply to 'Scroll Wheel Action' when setting the mouse 'scrollWheel' control.")]
		[global::UnityEngine.SerializeField]
		private float m_ScrollSpeed = 45f;

		[global::UnityEngine.Space(10f)]
		[global::UnityEngine.Tooltip("Vector2 action that moves the cursor left/right (X) and up/down (Y) on screen.")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.InputSystem.InputActionProperty m_StickAction;

		[global::UnityEngine.Tooltip("Button action that triggers a left-click on the mouse.")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.InputSystem.InputActionProperty m_LeftButtonAction;

		[global::UnityEngine.Tooltip("Button action that triggers a middle-click on the mouse.")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.InputSystem.InputActionProperty m_MiddleButtonAction;

		[global::UnityEngine.Tooltip("Button action that triggers a right-click on the mouse.")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.InputSystem.InputActionProperty m_RightButtonAction;

		[global::UnityEngine.Tooltip("Button action that triggers a forward button (button #4) click on the mouse.")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.InputSystem.InputActionProperty m_ForwardButtonAction;

		[global::UnityEngine.Tooltip("Button action that triggers a back button (button #5) click on the mouse.")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.InputSystem.InputActionProperty m_BackButtonAction;

		[global::UnityEngine.Tooltip("Vector2 action that feeds into the mouse 'scrollWheel' action (scaled by 'Scroll Speed').")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.InputSystem.InputActionProperty m_ScrollWheelAction;

		private global::UnityEngine.Canvas m_Canvas;

		private global::UnityEngine.InputSystem.Mouse m_VirtualMouse;

		private global::UnityEngine.InputSystem.Mouse m_SystemMouse;

		private global::System.Action m_AfterInputUpdateDelegate;

		private global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> m_ButtonActionTriggeredDelegate;

		private double m_LastTime;

		private global::UnityEngine.Vector2 m_LastStickValue;

		public global::UnityEngine.RectTransform cursorTransform
		{
			get
			{
				return m_CursorTransform;
			}
			set
			{
				m_CursorTransform = value;
			}
		}

		public float cursorSpeed
		{
			get
			{
				return m_CursorSpeed;
			}
			set
			{
				m_CursorSpeed = value;
			}
		}

		public global::UnityEngine.InputSystem.UI.VirtualMouseInput.CursorMode cursorMode
		{
			get
			{
				return m_CursorMode;
			}
			set
			{
				if (m_CursorMode != value)
				{
					if (m_CursorMode == global::UnityEngine.InputSystem.UI.VirtualMouseInput.CursorMode.HardwareCursorIfAvailable && m_SystemMouse != null)
					{
						global::UnityEngine.InputSystem.InputSystem.EnableDevice(m_SystemMouse);
						m_SystemMouse = null;
					}
					m_CursorMode = value;
					if (m_CursorMode == global::UnityEngine.InputSystem.UI.VirtualMouseInput.CursorMode.HardwareCursorIfAvailable)
					{
						TryEnableHardwareCursor();
					}
					else if (m_CursorGraphic != null)
					{
						m_CursorGraphic.enabled = true;
					}
				}
			}
		}

		public global::UnityEngine.UI.Graphic cursorGraphic
		{
			get
			{
				return m_CursorGraphic;
			}
			set
			{
				m_CursorGraphic = value;
				TryFindCanvas();
			}
		}

		public float scrollSpeed
		{
			get
			{
				return m_ScrollSpeed;
			}
			set
			{
				m_ScrollSpeed = value;
			}
		}

		public global::UnityEngine.InputSystem.Mouse virtualMouse => m_VirtualMouse;

		public global::UnityEngine.InputSystem.InputActionProperty stickAction
		{
			get
			{
				return m_StickAction;
			}
			set
			{
				SetAction(ref m_StickAction, value);
			}
		}

		public global::UnityEngine.InputSystem.InputActionProperty leftButtonAction
		{
			get
			{
				return m_LeftButtonAction;
			}
			set
			{
				if (m_ButtonActionTriggeredDelegate != null)
				{
					SetActionCallback(m_LeftButtonAction, m_ButtonActionTriggeredDelegate, install: false);
				}
				SetAction(ref m_LeftButtonAction, value);
				if (m_ButtonActionTriggeredDelegate != null)
				{
					SetActionCallback(m_LeftButtonAction, m_ButtonActionTriggeredDelegate);
				}
			}
		}

		public global::UnityEngine.InputSystem.InputActionProperty rightButtonAction
		{
			get
			{
				return m_RightButtonAction;
			}
			set
			{
				if (m_ButtonActionTriggeredDelegate != null)
				{
					SetActionCallback(m_RightButtonAction, m_ButtonActionTriggeredDelegate, install: false);
				}
				SetAction(ref m_RightButtonAction, value);
				if (m_ButtonActionTriggeredDelegate != null)
				{
					SetActionCallback(m_RightButtonAction, m_ButtonActionTriggeredDelegate);
				}
			}
		}

		public global::UnityEngine.InputSystem.InputActionProperty middleButtonAction
		{
			get
			{
				return m_MiddleButtonAction;
			}
			set
			{
				if (m_ButtonActionTriggeredDelegate != null)
				{
					SetActionCallback(m_MiddleButtonAction, m_ButtonActionTriggeredDelegate, install: false);
				}
				SetAction(ref m_MiddleButtonAction, value);
				if (m_ButtonActionTriggeredDelegate != null)
				{
					SetActionCallback(m_MiddleButtonAction, m_ButtonActionTriggeredDelegate);
				}
			}
		}

		public global::UnityEngine.InputSystem.InputActionProperty forwardButtonAction
		{
			get
			{
				return m_ForwardButtonAction;
			}
			set
			{
				if (m_ButtonActionTriggeredDelegate != null)
				{
					SetActionCallback(m_ForwardButtonAction, m_ButtonActionTriggeredDelegate, install: false);
				}
				SetAction(ref m_ForwardButtonAction, value);
				if (m_ButtonActionTriggeredDelegate != null)
				{
					SetActionCallback(m_ForwardButtonAction, m_ButtonActionTriggeredDelegate);
				}
			}
		}

		public global::UnityEngine.InputSystem.InputActionProperty backButtonAction
		{
			get
			{
				return m_BackButtonAction;
			}
			set
			{
				if (m_ButtonActionTriggeredDelegate != null)
				{
					SetActionCallback(m_BackButtonAction, m_ButtonActionTriggeredDelegate, install: false);
				}
				SetAction(ref m_BackButtonAction, value);
				if (m_ButtonActionTriggeredDelegate != null)
				{
					SetActionCallback(m_BackButtonAction, m_ButtonActionTriggeredDelegate);
				}
			}
		}

		public global::UnityEngine.InputSystem.InputActionProperty scrollWheelAction
		{
			get
			{
				return m_ScrollWheelAction;
			}
			set
			{
				SetAction(ref m_ScrollWheelAction, value);
			}
		}

		protected void OnEnable()
		{
			if (m_CursorMode == global::UnityEngine.InputSystem.UI.VirtualMouseInput.CursorMode.HardwareCursorIfAvailable)
			{
				TryEnableHardwareCursor();
			}
			if (m_VirtualMouse == null)
			{
				m_VirtualMouse = (global::UnityEngine.InputSystem.Mouse)global::UnityEngine.InputSystem.InputSystem.AddDevice("VirtualMouse");
			}
			else if (!m_VirtualMouse.added)
			{
				global::UnityEngine.InputSystem.InputSystem.AddDevice(m_VirtualMouse);
			}
			if (m_CursorTransform != null)
			{
				global::UnityEngine.Vector2 anchoredPosition = m_CursorTransform.anchoredPosition;
				global::UnityEngine.InputSystem.LowLevel.InputState.Change(m_VirtualMouse.position, anchoredPosition);
				m_SystemMouse?.WarpCursorPosition(anchoredPosition);
			}
			if (m_AfterInputUpdateDelegate == null)
			{
				m_AfterInputUpdateDelegate = OnAfterInputUpdate;
			}
			global::UnityEngine.InputSystem.InputSystem.onAfterUpdate += m_AfterInputUpdateDelegate;
			if (m_ButtonActionTriggeredDelegate == null)
			{
				m_ButtonActionTriggeredDelegate = OnButtonActionTriggered;
			}
			SetActionCallback(m_LeftButtonAction, m_ButtonActionTriggeredDelegate);
			SetActionCallback(m_RightButtonAction, m_ButtonActionTriggeredDelegate);
			SetActionCallback(m_MiddleButtonAction, m_ButtonActionTriggeredDelegate);
			SetActionCallback(m_ForwardButtonAction, m_ButtonActionTriggeredDelegate);
			SetActionCallback(m_BackButtonAction, m_ButtonActionTriggeredDelegate);
			m_StickAction.action?.Enable();
			m_LeftButtonAction.action?.Enable();
			m_RightButtonAction.action?.Enable();
			m_MiddleButtonAction.action?.Enable();
			m_ForwardButtonAction.action?.Enable();
			m_BackButtonAction.action?.Enable();
			m_ScrollWheelAction.action?.Enable();
		}

		protected void OnDisable()
		{
			if (m_VirtualMouse != null && m_VirtualMouse.added)
			{
				global::UnityEngine.InputSystem.InputSystem.RemoveDevice(m_VirtualMouse);
			}
			if (m_SystemMouse != null)
			{
				global::UnityEngine.InputSystem.InputSystem.EnableDevice(m_SystemMouse);
				m_SystemMouse = null;
			}
			if (m_AfterInputUpdateDelegate != null)
			{
				global::UnityEngine.InputSystem.InputSystem.onAfterUpdate -= m_AfterInputUpdateDelegate;
			}
			m_StickAction.action?.Disable();
			m_LeftButtonAction.action?.Disable();
			m_RightButtonAction.action?.Disable();
			m_MiddleButtonAction.action?.Disable();
			m_ForwardButtonAction.action?.Disable();
			m_BackButtonAction.action?.Disable();
			m_ScrollWheelAction.action?.Disable();
			if (m_ButtonActionTriggeredDelegate != null)
			{
				SetActionCallback(m_LeftButtonAction, m_ButtonActionTriggeredDelegate, install: false);
				SetActionCallback(m_RightButtonAction, m_ButtonActionTriggeredDelegate, install: false);
				SetActionCallback(m_MiddleButtonAction, m_ButtonActionTriggeredDelegate, install: false);
				SetActionCallback(m_ForwardButtonAction, m_ButtonActionTriggeredDelegate, install: false);
				SetActionCallback(m_BackButtonAction, m_ButtonActionTriggeredDelegate, install: false);
			}
			m_LastTime = 0.0;
			m_LastStickValue = default(global::UnityEngine.Vector2);
		}

		private void TryFindCanvas()
		{
			m_Canvas = m_CursorGraphic?.GetComponentInParent<global::UnityEngine.Canvas>();
		}

		private void TryEnableHardwareCursor()
		{
			global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputDevice> devices = global::UnityEngine.InputSystem.InputSystem.devices;
			for (int i = 0; i < devices.Count; i++)
			{
				global::UnityEngine.InputSystem.InputDevice inputDevice = devices[i];
				if (inputDevice.native && inputDevice is global::UnityEngine.InputSystem.Mouse systemMouse)
				{
					m_SystemMouse = systemMouse;
					break;
				}
			}
			if (m_SystemMouse == null)
			{
				if (m_CursorGraphic != null)
				{
					m_CursorGraphic.enabled = true;
				}
				return;
			}
			global::UnityEngine.InputSystem.InputSystem.DisableDevice(m_SystemMouse);
			if (m_VirtualMouse != null)
			{
				m_SystemMouse.WarpCursorPosition(m_VirtualMouse.position.value);
			}
			if (m_CursorGraphic != null)
			{
				m_CursorGraphic.enabled = false;
			}
		}

		private void UpdateMotion()
		{
			if (m_VirtualMouse == null)
			{
				return;
			}
			global::UnityEngine.InputSystem.InputAction action = m_StickAction.action;
			if (action == null)
			{
				return;
			}
			global::UnityEngine.Vector2 lastStickValue = action.ReadValue<global::UnityEngine.Vector2>();
			if (global::UnityEngine.Mathf.Approximately(0f, lastStickValue.x) && global::UnityEngine.Mathf.Approximately(0f, lastStickValue.y))
			{
				m_LastTime = 0.0;
				m_LastStickValue = default(global::UnityEngine.Vector2);
			}
			else
			{
				double currentTime = global::UnityEngine.InputSystem.LowLevel.InputState.currentTime;
				if (global::UnityEngine.Mathf.Approximately(0f, m_LastStickValue.x) && global::UnityEngine.Mathf.Approximately(0f, m_LastStickValue.y))
				{
					m_LastTime = currentTime;
				}
				float num = (float)(currentTime - m_LastTime);
				global::UnityEngine.Vector2 vector = new global::UnityEngine.Vector2(m_CursorSpeed * lastStickValue.x * num, m_CursorSpeed * lastStickValue.y * num);
				global::UnityEngine.Vector2 vector2 = m_VirtualMouse.position.value + vector;
				if (m_Canvas != null)
				{
					global::UnityEngine.Rect pixelRect = m_Canvas.pixelRect;
					vector2.x = global::UnityEngine.Mathf.Clamp(vector2.x, pixelRect.xMin, pixelRect.xMax);
					vector2.y = global::UnityEngine.Mathf.Clamp(vector2.y, pixelRect.yMin, pixelRect.yMax);
				}
				global::UnityEngine.InputSystem.LowLevel.InputState.Change(m_VirtualMouse.position, vector2);
				global::UnityEngine.InputSystem.LowLevel.InputState.Change(m_VirtualMouse.delta, vector);
				if (m_CursorTransform != null && (m_CursorMode == global::UnityEngine.InputSystem.UI.VirtualMouseInput.CursorMode.SoftwareCursor || (m_CursorMode == global::UnityEngine.InputSystem.UI.VirtualMouseInput.CursorMode.HardwareCursorIfAvailable && m_SystemMouse == null)))
				{
					m_CursorTransform.anchoredPosition = vector2;
				}
				m_LastStickValue = lastStickValue;
				m_LastTime = currentTime;
				m_SystemMouse?.WarpCursorPosition(vector2);
			}
			global::UnityEngine.InputSystem.InputAction action2 = m_ScrollWheelAction.action;
			if (action2 != null)
			{
				global::UnityEngine.Vector2 state = action2.ReadValue<global::UnityEngine.Vector2>();
				state.x *= m_ScrollSpeed;
				state.y *= m_ScrollSpeed;
				global::UnityEngine.InputSystem.LowLevel.InputState.Change(m_VirtualMouse.scroll, state);
			}
		}

		private void OnButtonActionTriggered(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
		{
			if (m_VirtualMouse != null)
			{
				global::UnityEngine.InputSystem.InputAction action = context.action;
				global::UnityEngine.InputSystem.LowLevel.MouseButton? mouseButton = null;
				if (action == m_LeftButtonAction.action)
				{
					mouseButton = global::UnityEngine.InputSystem.LowLevel.MouseButton.Left;
				}
				else if (action == m_RightButtonAction.action)
				{
					mouseButton = global::UnityEngine.InputSystem.LowLevel.MouseButton.Right;
				}
				else if (action == m_MiddleButtonAction.action)
				{
					mouseButton = global::UnityEngine.InputSystem.LowLevel.MouseButton.Middle;
				}
				else if (action == m_ForwardButtonAction.action)
				{
					mouseButton = global::UnityEngine.InputSystem.LowLevel.MouseButton.Forward;
				}
				else if (action == m_BackButtonAction.action)
				{
					mouseButton = global::UnityEngine.InputSystem.LowLevel.MouseButton.Back;
				}
				if (mouseButton.HasValue)
				{
					bool state = context.control.IsPressed();
					m_VirtualMouse.CopyState<global::UnityEngine.InputSystem.LowLevel.MouseState>(out var state2);
					state2.WithButton(mouseButton.Value, state);
					global::UnityEngine.InputSystem.LowLevel.InputState.Change(m_VirtualMouse, state2);
				}
			}
		}

		private static void SetActionCallback(global::UnityEngine.InputSystem.InputActionProperty field, global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> callback, bool install = true)
		{
			global::UnityEngine.InputSystem.InputAction action = field.action;
			if (action != null)
			{
				if (install)
				{
					action.started += callback;
					action.canceled += callback;
				}
				else
				{
					action.started -= callback;
					action.canceled -= callback;
				}
			}
		}

		private static void SetAction(ref global::UnityEngine.InputSystem.InputActionProperty field, global::UnityEngine.InputSystem.InputActionProperty value)
		{
			global::UnityEngine.InputSystem.InputActionProperty inputActionProperty = field;
			field = value;
			if (!(inputActionProperty.reference == null))
			{
				return;
			}
			global::UnityEngine.InputSystem.InputAction action = inputActionProperty.action;
			if (action != null && action.enabled)
			{
				action.Disable();
				if (value.reference == null)
				{
					value.action?.Enable();
				}
			}
		}

		private void OnAfterInputUpdate()
		{
			UpdateMotion();
		}
	}
}
