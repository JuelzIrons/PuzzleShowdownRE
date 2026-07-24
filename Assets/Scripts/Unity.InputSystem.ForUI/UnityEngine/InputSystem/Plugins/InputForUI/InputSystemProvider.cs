namespace UnityEngine.InputSystem.Plugins.InputForUI
{
	internal class InputSystemProvider : global::UnityEngine.InputForUI.IEventProviderImpl
	{
		public static class Actions
		{
			public static readonly string PointAction = "UI/Point";

			public static readonly string MoveAction = "UI/Navigate";

			public static readonly string SubmitAction = "UI/Submit";

			public static readonly string CancelAction = "UI/Cancel";

			public static readonly string LeftClickAction = "UI/Click";

			public static readonly string MiddleClickAction = "UI/MiddleClick";

			public static readonly string RightClickAction = "UI/RightClick";

			public static readonly string ScrollWheelAction = "UI/ScrollWheel";
		}

		private global::UnityEngine.InputForUI.InputEventPartialProvider m_InputEventPartialProvider;

		private global::UnityEngine.InputSystem.DefaultInputActions m_DefaultInputActions;

		private global::UnityEngine.InputSystem.InputActionAsset m_InputActionAsset;

		private global::UnityEngine.InputSystem.InputAction m_PointAction;

		private global::UnityEngine.InputSystem.InputAction m_MoveAction;

		private global::UnityEngine.InputSystem.InputAction m_SubmitAction;

		private global::UnityEngine.InputSystem.InputAction m_CancelAction;

		private global::UnityEngine.InputSystem.InputAction m_LeftClickAction;

		private global::UnityEngine.InputSystem.InputAction m_MiddleClickAction;

		private global::UnityEngine.InputSystem.InputAction m_RightClickAction;

		private global::UnityEngine.InputSystem.InputAction m_ScrollWheelAction;

		private global::UnityEngine.InputSystem.InputAction m_NextPreviousAction;

		private global::System.Collections.Generic.List<global::UnityEngine.InputForUI.Event> m_Events = new global::System.Collections.Generic.List<global::UnityEngine.InputForUI.Event>();

		private global::UnityEngine.InputForUI.PointerState m_MouseState;

		private global::UnityEngine.InputForUI.PointerState m_PenState;

		private bool m_SeenPenEvents;

		private global::UnityEngine.InputForUI.PointerState m_TouchState;

		private bool m_SeenTouchEvents;

		private const float k_SmallestReportedMovementSqrDist = 0.01f;

		private global::UnityEngine.InputForUI.NavigationEventRepeatHelper m_RepeatHelper = new global::UnityEngine.InputForUI.NavigationEventRepeatHelper();

		private bool m_ResetSeenEventsOnUpdate;

		private const float kScrollUGUIScaleFactor = 3f;

		private static global::System.Action<global::UnityEngine.InputSystem.InputActionAsset> s_OnRegisterActions;

		private const uint k_DefaultPlayerId = 0u;

		private global::UnityEngine.InputForUI.EventModifiers m_EventModifiers => m_InputEventPartialProvider._eventModifiers;

		private global::Unity.IntegerTime.DiscreteTime m_CurrentTime => (global::Unity.IntegerTime.DiscreteTime)global::UnityEngine.Time.timeAsRational;

		public uint playerCount => 1u;

		static InputSystemProvider()
		{
			global::UnityEngine.InputForUI.EventProvider.SetInputSystemProvider(new global::UnityEngine.InputSystem.Plugins.InputForUI.InputSystemProvider());
		}

		[global::UnityEngine.RuntimeInitializeOnLoadMethod(global::UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void Bootstrap()
		{
		}

		public void Initialize()
		{
			if (m_InputEventPartialProvider == null)
			{
				m_InputEventPartialProvider = new global::UnityEngine.InputForUI.InputEventPartialProvider();
			}
			m_InputEventPartialProvider.Initialize();
			m_Events.Clear();
			m_MouseState.Reset();
			m_PenState.Reset();
			m_SeenPenEvents = false;
			m_TouchState.Reset();
			m_SeenTouchEvents = false;
			SelectInputActionAsset();
			RegisterActions();
			RegisterFixedActions();
			global::UnityEngine.InputSystem.InputSystem.onActionsChange += OnActionsChange;
		}

		public void Shutdown()
		{
			UnregisterActions();
			UnregisterFixedActions();
			m_InputEventPartialProvider.Shutdown();
			m_InputEventPartialProvider = null;
			if (m_DefaultInputActions != null)
			{
				m_DefaultInputActions.Dispose();
				m_DefaultInputActions = null;
			}
			global::UnityEngine.InputSystem.InputSystem.onActionsChange -= OnActionsChange;
		}

		public void OnActionsChange()
		{
			UnregisterActions();
			SelectInputActionAsset();
			RegisterActions();
		}

		public void Update()
		{
			m_InputEventPartialProvider.Update();
			m_Events.Sort((global::UnityEngine.InputForUI.Event a, global::UnityEngine.InputForUI.Event b) => SortEvents(a, b));
			global::Unity.IntegerTime.DiscreteTime currentTime = (global::Unity.IntegerTime.DiscreteTime)global::UnityEngine.Time.timeAsRational;
			DirectionNavigation(currentTime);
			foreach (global::UnityEngine.InputForUI.Event @event in m_Events)
			{
				global::UnityEngine.InputForUI.Event ev = @event;
				if (m_SeenTouchEvents && ev.type == global::UnityEngine.InputForUI.Event.Type.PointerEvent && ev.eventSource == global::UnityEngine.InputForUI.EventSource.Pen)
				{
					m_PenState.Reset();
				}
				else if ((m_SeenTouchEvents || m_SeenPenEvents) && ev.type == global::UnityEngine.InputForUI.Event.Type.PointerEvent && (ev.eventSource == global::UnityEngine.InputForUI.EventSource.Mouse || ev.eventSource == global::UnityEngine.InputForUI.EventSource.Unspecified))
				{
					m_MouseState.Reset();
				}
				else
				{
					global::UnityEngine.InputForUI.EventProvider.Dispatch(in ev);
				}
			}
			if (m_ResetSeenEventsOnUpdate)
			{
				ResetSeenEvents();
				m_ResetSeenEventsOnUpdate = false;
			}
			m_Events.Clear();
		}

		private void ResetSeenEvents()
		{
			m_SeenTouchEvents = false;
			m_SeenPenEvents = false;
		}

		public bool ActionAssetIsNotNull()
		{
			return m_InputActionAsset != null;
		}

		private void DirectionNavigation(global::Unity.IntegerTime.DiscreteTime currentTime)
		{
			(global::UnityEngine.Vector2, bool) tuple = ReadCurrentNavigationMoveVector();
			global::UnityEngine.Vector2 item = tuple.Item1;
			bool axisButtonsWherePressedThisFrame = tuple.Item2;
			global::UnityEngine.InputForUI.NavigationEvent.Direction direction = global::UnityEngine.InputForUI.NavigationEvent.DetermineMoveDirection(item);
			if (direction == global::UnityEngine.InputForUI.NavigationEvent.Direction.None)
			{
				direction = ReadNextPreviousDirection();
				axisButtonsWherePressedThisFrame = m_NextPreviousAction.WasPressedThisFrame();
			}
			if (direction == global::UnityEngine.InputForUI.NavigationEvent.Direction.None)
			{
				m_RepeatHelper.Reset();
			}
			else if (m_RepeatHelper.ShouldSendMoveEvent(currentTime, direction, axisButtonsWherePressedThisFrame))
			{
				global::UnityEngine.InputForUI.EventProvider.Dispatch(global::UnityEngine.InputForUI.Event.From(new global::UnityEngine.InputForUI.NavigationEvent
				{
					type = global::UnityEngine.InputForUI.NavigationEvent.Type.Move,
					direction = direction,
					timestamp = currentTime,
					eventSource = GetEventSource(GetActiveDeviceFromDirection(direction)),
					playerId = 0u,
					eventModifiers = m_EventModifiers
				}));
			}
		}

		private global::UnityEngine.InputSystem.InputDevice GetActiveDeviceFromDirection(global::UnityEngine.InputForUI.NavigationEvent.Direction direction)
		{
			switch (direction)
			{
			case global::UnityEngine.InputForUI.NavigationEvent.Direction.Left:
			case global::UnityEngine.InputForUI.NavigationEvent.Direction.Up:
			case global::UnityEngine.InputForUI.NavigationEvent.Direction.Right:
			case global::UnityEngine.InputForUI.NavigationEvent.Direction.Down:
				if (m_MoveAction != null)
				{
					return m_MoveAction.activeControl.device;
				}
				break;
			case global::UnityEngine.InputForUI.NavigationEvent.Direction.Next:
			case global::UnityEngine.InputForUI.NavigationEvent.Direction.Previous:
				if (m_NextPreviousAction != null)
				{
					return m_NextPreviousAction.activeControl.device;
				}
				break;
			}
			return global::UnityEngine.InputSystem.Keyboard.current;
		}

		private (global::UnityEngine.Vector2, bool) ReadCurrentNavigationMoveVector()
		{
			if (m_MoveAction == null)
			{
				return (default(global::UnityEngine.Vector2), false);
			}
			global::UnityEngine.Vector2 item = m_MoveAction.ReadValue<global::UnityEngine.Vector2>();
			bool item2 = m_MoveAction.WasPressedThisFrame();
			return (item, item2);
		}

		private global::UnityEngine.InputForUI.NavigationEvent.Direction ReadNextPreviousDirection()
		{
			if (m_NextPreviousAction.IsPressed() && m_NextPreviousAction.activeControl.device is global::UnityEngine.InputSystem.Keyboard)
			{
				if (!(m_NextPreviousAction.activeControl.device as global::UnityEngine.InputSystem.Keyboard).shiftKey.isPressed)
				{
					return global::UnityEngine.InputForUI.NavigationEvent.Direction.Next;
				}
				return global::UnityEngine.InputForUI.NavigationEvent.Direction.Previous;
			}
			return global::UnityEngine.InputForUI.NavigationEvent.Direction.None;
		}

		private static int SortEvents(global::UnityEngine.InputForUI.Event a, global::UnityEngine.InputForUI.Event b)
		{
			return global::UnityEngine.InputForUI.Event.CompareType(a, b);
		}

		public void OnFocusChanged(bool focus)
		{
			m_InputEventPartialProvider.OnFocusChanged(focus);
		}

		public bool RequestCurrentState(global::UnityEngine.InputForUI.Event.Type type)
		{
			if (m_InputEventPartialProvider.RequestCurrentState(type))
			{
				return true;
			}
			switch (type)
			{
			case global::UnityEngine.InputForUI.Event.Type.PointerEvent:
				if (m_TouchState.LastPositionValid)
				{
					global::UnityEngine.InputForUI.EventProvider.Dispatch(global::UnityEngine.InputForUI.Event.From(ToPointerStateEvent(m_CurrentTime, in m_TouchState, global::UnityEngine.InputForUI.EventSource.Touch)));
				}
				if (m_PenState.LastPositionValid)
				{
					global::UnityEngine.InputForUI.EventProvider.Dispatch(global::UnityEngine.InputForUI.Event.From(ToPointerStateEvent(m_CurrentTime, in m_PenState, global::UnityEngine.InputForUI.EventSource.Pen)));
				}
				if (m_MouseState.LastPositionValid)
				{
					global::UnityEngine.InputForUI.EventProvider.Dispatch(global::UnityEngine.InputForUI.Event.From(ToPointerStateEvent(m_CurrentTime, in m_MouseState, global::UnityEngine.InputForUI.EventSource.Mouse)));
				}
				if (!m_TouchState.LastPositionValid && !m_PenState.LastPositionValid)
				{
					return m_MouseState.LastPositionValid;
				}
				return true;
			default:
				return false;
			}
		}

		internal static global::UnityEngine.Vector2 ScreenBottomLeftToPanelPosition(global::UnityEngine.Vector2 position, int targetDisplay)
		{
			int num = global::UnityEngine.Screen.height;
			if (targetDisplay > 0 && targetDisplay < global::UnityEngine.Display.displays.Length)
			{
				num = global::UnityEngine.Display.displays[targetDisplay].systemHeight;
			}
			position.y = (float)num - position.y;
			return position;
		}

		private global::UnityEngine.InputForUI.PointerEvent ToPointerStateEvent(global::Unity.IntegerTime.DiscreteTime currentTime, in global::UnityEngine.InputForUI.PointerState state, global::UnityEngine.InputForUI.EventSource eventSource)
		{
			return new global::UnityEngine.InputForUI.PointerEvent
			{
				type = global::UnityEngine.InputForUI.PointerEvent.Type.State,
				pointerIndex = 0,
				position = state.LastPosition,
				deltaPosition = global::UnityEngine.Vector2.zero,
				scroll = global::UnityEngine.Vector2.zero,
				displayIndex = state.LastDisplayIndex,
				button = global::UnityEngine.InputForUI.PointerEvent.Button.None,
				buttonsState = state.ButtonsState,
				clickCount = 0,
				timestamp = currentTime,
				eventSource = eventSource,
				playerId = 0u,
				eventModifiers = m_EventModifiers
			};
		}

		private global::UnityEngine.InputForUI.EventSource GetEventSource(global::UnityEngine.InputSystem.InputAction.CallbackContext ctx)
		{
			global::UnityEngine.InputSystem.InputDevice device = ctx.control.device;
			return GetEventSource(device);
		}

		private global::UnityEngine.InputForUI.EventSource GetEventSource(global::UnityEngine.InputSystem.InputDevice device)
		{
			if (device is global::UnityEngine.InputSystem.Touchscreen)
			{
				return global::UnityEngine.InputForUI.EventSource.Touch;
			}
			if (device is global::UnityEngine.InputSystem.Pen)
			{
				return global::UnityEngine.InputForUI.EventSource.Pen;
			}
			if (device is global::UnityEngine.InputSystem.Mouse)
			{
				return global::UnityEngine.InputForUI.EventSource.Mouse;
			}
			if (device is global::UnityEngine.InputSystem.Keyboard)
			{
				return global::UnityEngine.InputForUI.EventSource.Keyboard;
			}
			if (device is global::UnityEngine.InputSystem.Gamepad)
			{
				return global::UnityEngine.InputForUI.EventSource.Gamepad;
			}
			return global::UnityEngine.InputForUI.EventSource.Unspecified;
		}

		private ref global::UnityEngine.InputForUI.PointerState GetPointerStateForSource(global::UnityEngine.InputForUI.EventSource eventSource)
		{
			return eventSource switch
			{
				global::UnityEngine.InputForUI.EventSource.Touch => ref m_TouchState, 
				global::UnityEngine.InputForUI.EventSource.Pen => ref m_PenState, 
				_ => ref m_MouseState, 
			};
		}

		private void DispatchFromCallback(in global::UnityEngine.InputForUI.Event ev)
		{
			m_Events.Add(ev);
		}

		private static int FindTouchFingerIndex(global::UnityEngine.InputSystem.Touchscreen touchscreen, global::UnityEngine.InputSystem.InputAction.CallbackContext ctx)
		{
			if (touchscreen == null)
			{
				return 0;
			}
			global::UnityEngine.InputSystem.Controls.Vector2Control vector2Control = ((ctx.control is global::UnityEngine.InputSystem.Controls.Vector2Control) ? ((global::UnityEngine.InputSystem.Controls.Vector2Control)ctx.control) : null);
			global::UnityEngine.InputSystem.Controls.TouchPressControl touchPressControl = ((ctx.control is global::UnityEngine.InputSystem.Controls.TouchPressControl) ? ((global::UnityEngine.InputSystem.Controls.TouchPressControl)ctx.control) : null);
			global::UnityEngine.InputSystem.Controls.TouchControl touchControl = ((ctx.control is global::UnityEngine.InputSystem.Controls.TouchControl) ? ((global::UnityEngine.InputSystem.Controls.TouchControl)ctx.control) : null);
			for (int i = 0; i < touchscreen.touches.Count; i++)
			{
				if (vector2Control != null && vector2Control == touchscreen.touches[i].position)
				{
					return i;
				}
				if (touchPressControl != null && touchPressControl == touchscreen.touches[i].press)
				{
					return i;
				}
				if (touchControl != null && touchControl == touchscreen.touches[i])
				{
					return i;
				}
			}
			return 0;
		}

		private void OnPointerPerformed(global::UnityEngine.InputSystem.InputAction.CallbackContext ctx)
		{
			global::UnityEngine.InputForUI.EventSource eventSource = GetEventSource(ctx);
			ref global::UnityEngine.InputForUI.PointerState pointerStateForSource = ref GetPointerStateForSource(eventSource);
			global::UnityEngine.InputSystem.Pointer pointer = ((ctx.control.device is global::UnityEngine.InputSystem.Pointer) ? ((global::UnityEngine.InputSystem.Pointer)ctx.control.device) : null);
			global::UnityEngine.InputSystem.Pen pen = ((ctx.control.device is global::UnityEngine.InputSystem.Pen) ? ((global::UnityEngine.InputSystem.Pen)ctx.control.device) : null);
			global::UnityEngine.InputSystem.Touchscreen touchscreen = ((ctx.control.device is global::UnityEngine.InputSystem.Touchscreen) ? ((global::UnityEngine.InputSystem.Touchscreen)ctx.control.device) : null);
			global::UnityEngine.InputSystem.Controls.TouchControl touchControl = ((ctx.control is global::UnityEngine.InputSystem.Controls.TouchControl) ? ((global::UnityEngine.InputSystem.Controls.TouchControl)ctx.control) : null);
			int pointerIndex = FindTouchFingerIndex(touchscreen, ctx);
			m_ResetSeenEventsOnUpdate = false;
			if (touchControl != null || touchscreen != null)
			{
				m_SeenTouchEvents = true;
			}
			else if (pen != null)
			{
				m_SeenPenEvents = true;
			}
			global::UnityEngine.Vector2 position = ctx.ReadValue<global::UnityEngine.Vector2>();
			int num = pointer?.displayIndex.ReadValue() ?? touchscreen?.displayIndex.ReadValue() ?? pen?.displayIndex.ReadValue() ?? 0;
			global::UnityEngine.Vector2 vector = ScreenBottomLeftToPanelPosition(position, num);
			global::UnityEngine.Vector2 deltaPosition = (pointerStateForSource.LastPositionValid ? (vector - pointerStateForSource.LastPosition) : global::UnityEngine.Vector2.zero);
			global::UnityEngine.Vector2 tilt = pen?.tilt.ReadValue() ?? global::UnityEngine.Vector2.zero;
			float twist = pen?.twist.ReadValue() ?? 0f;
			float pressure = pen?.pressure.ReadValue() ?? touchControl?.pressure.ReadValue() ?? 0f;
			bool isInverted = pen?.eraser.isPressed ?? false;
			if (deltaPosition.sqrMagnitude >= 0.01f)
			{
				DispatchFromCallback(global::UnityEngine.InputForUI.Event.From(new global::UnityEngine.InputForUI.PointerEvent
				{
					type = global::UnityEngine.InputForUI.PointerEvent.Type.PointerMoved,
					pointerIndex = pointerIndex,
					position = vector,
					deltaPosition = deltaPosition,
					scroll = global::UnityEngine.Vector2.zero,
					displayIndex = num,
					tilt = tilt,
					twist = twist,
					pressure = pressure,
					isInverted = isInverted,
					button = global::UnityEngine.InputForUI.PointerEvent.Button.None,
					buttonsState = pointerStateForSource.ButtonsState,
					clickCount = 0,
					timestamp = m_CurrentTime,
					eventSource = eventSource,
					playerId = 0u,
					eventModifiers = m_EventModifiers
				}));
				pointerStateForSource.OnMove(m_CurrentTime, vector, num);
			}
			else if (!pointerStateForSource.LastPositionValid)
			{
				pointerStateForSource.OnMove(m_CurrentTime, vector, num);
			}
		}

		private void OnSubmitPerformed(global::UnityEngine.InputSystem.InputAction.CallbackContext ctx)
		{
			DispatchFromCallback(global::UnityEngine.InputForUI.Event.From(new global::UnityEngine.InputForUI.NavigationEvent
			{
				type = global::UnityEngine.InputForUI.NavigationEvent.Type.Submit,
				direction = global::UnityEngine.InputForUI.NavigationEvent.Direction.None,
				timestamp = m_CurrentTime,
				eventSource = GetEventSource(ctx),
				playerId = 0u,
				eventModifiers = m_EventModifiers
			}));
		}

		private void OnCancelPerformed(global::UnityEngine.InputSystem.InputAction.CallbackContext ctx)
		{
			DispatchFromCallback(global::UnityEngine.InputForUI.Event.From(new global::UnityEngine.InputForUI.NavigationEvent
			{
				type = global::UnityEngine.InputForUI.NavigationEvent.Type.Cancel,
				direction = global::UnityEngine.InputForUI.NavigationEvent.Direction.None,
				timestamp = m_CurrentTime,
				eventSource = GetEventSource(ctx),
				playerId = 0u,
				eventModifiers = m_EventModifiers
			}));
		}

		private void OnClickPerformed(global::UnityEngine.InputSystem.InputAction.CallbackContext ctx, global::UnityEngine.InputForUI.EventSource eventSource, global::UnityEngine.InputForUI.PointerEvent.Button button)
		{
			ref global::UnityEngine.InputForUI.PointerState pointerStateForSource = ref GetPointerStateForSource(eventSource);
			global::UnityEngine.InputSystem.Touchscreen touchscreen = ((ctx.control.device is global::UnityEngine.InputSystem.Touchscreen) ? ((global::UnityEngine.InputSystem.Touchscreen)ctx.control.device) : null);
			global::UnityEngine.InputSystem.Controls.TouchControl obj = ((ctx.control is global::UnityEngine.InputSystem.Controls.TouchControl) ? ((global::UnityEngine.InputSystem.Controls.TouchControl)ctx.control) : null);
			int pointerIndex = FindTouchFingerIndex(touchscreen, ctx);
			m_ResetSeenEventsOnUpdate = true;
			if (obj != null || touchscreen != null)
			{
				m_SeenTouchEvents = true;
			}
			bool previousState = pointerStateForSource.ButtonsState.Get(button);
			bool flag = ctx.ReadValueAsButton();
			pointerStateForSource.OnButtonChange(m_CurrentTime, button, previousState, flag);
			DispatchFromCallback(global::UnityEngine.InputForUI.Event.From(new global::UnityEngine.InputForUI.PointerEvent
			{
				type = (flag ? global::UnityEngine.InputForUI.PointerEvent.Type.ButtonPressed : global::UnityEngine.InputForUI.PointerEvent.Type.ButtonReleased),
				pointerIndex = pointerIndex,
				position = pointerStateForSource.LastPosition,
				deltaPosition = global::UnityEngine.Vector2.zero,
				scroll = global::UnityEngine.Vector2.zero,
				displayIndex = pointerStateForSource.LastDisplayIndex,
				tilt = global::UnityEngine.Vector2.zero,
				twist = 0f,
				pressure = 0f,
				isInverted = false,
				button = button,
				buttonsState = pointerStateForSource.ButtonsState,
				clickCount = pointerStateForSource.ClickCount,
				timestamp = m_CurrentTime,
				eventSource = eventSource,
				playerId = 0u,
				eventModifiers = m_EventModifiers
			}));
		}

		private void OnLeftClickPerformed(global::UnityEngine.InputSystem.InputAction.CallbackContext ctx)
		{
			OnClickPerformed(ctx, GetEventSource(ctx), global::UnityEngine.InputForUI.PointerEvent.Button.Primary);
		}

		private void OnMiddleClickPerformed(global::UnityEngine.InputSystem.InputAction.CallbackContext ctx)
		{
			OnClickPerformed(ctx, GetEventSource(ctx), global::UnityEngine.InputForUI.PointerEvent.Button.PenBarrelButton);
		}

		private void OnRightClickPerformed(global::UnityEngine.InputSystem.InputAction.CallbackContext ctx)
		{
			OnClickPerformed(ctx, GetEventSource(ctx), global::UnityEngine.InputForUI.PointerEvent.Button.PenEraserInTouch);
		}

		private void OnScrollWheelPerformed(global::UnityEngine.InputSystem.InputAction.CallbackContext ctx)
		{
			global::UnityEngine.Vector2 vector = ctx.ReadValue<global::UnityEngine.Vector2>() / global::UnityEngine.InputSystem.InputSystem.scrollWheelDeltaPerTick;
			if (!(vector.sqrMagnitude < 0.01f))
			{
				global::UnityEngine.InputForUI.EventSource eventSource = GetEventSource(ctx);
				ref global::UnityEngine.InputForUI.PointerState pointerStateForSource = ref GetPointerStateForSource(eventSource);
				global::UnityEngine.Vector2 position = global::UnityEngine.Vector2.zero;
				int displayIndex = 0;
				if (pointerStateForSource.LastPositionValid)
				{
					position = pointerStateForSource.LastPosition;
					displayIndex = pointerStateForSource.LastDisplayIndex;
				}
				else if (eventSource == global::UnityEngine.InputForUI.EventSource.Mouse && global::UnityEngine.InputSystem.Mouse.current != null)
				{
					position = global::UnityEngine.InputSystem.Mouse.current.position.ReadValue();
					displayIndex = global::UnityEngine.InputSystem.Mouse.current.displayIndex.ReadValue();
				}
				global::UnityEngine.Vector2 scroll = new global::UnityEngine.Vector2
				{
					x = vector.x * 3f,
					y = (0f - vector.y) * 3f
				};
				DispatchFromCallback(global::UnityEngine.InputForUI.Event.From(new global::UnityEngine.InputForUI.PointerEvent
				{
					type = global::UnityEngine.InputForUI.PointerEvent.Type.Scroll,
					pointerIndex = 0,
					position = position,
					deltaPosition = global::UnityEngine.Vector2.zero,
					scroll = scroll,
					displayIndex = displayIndex,
					tilt = global::UnityEngine.Vector2.zero,
					twist = 0f,
					pressure = 0f,
					isInverted = false,
					button = global::UnityEngine.InputForUI.PointerEvent.Button.None,
					buttonsState = pointerStateForSource.ButtonsState,
					clickCount = 0,
					timestamp = m_CurrentTime,
					eventSource = global::UnityEngine.InputForUI.EventSource.Mouse,
					playerId = 0u,
					eventModifiers = m_EventModifiers
				}));
			}
		}

		private void RegisterFixedActions()
		{
			m_NextPreviousAction = new global::UnityEngine.InputSystem.InputAction("nextPreviousAction", global::UnityEngine.InputSystem.InputActionType.Button);
			m_NextPreviousAction.AddBinding("<Keyboard>/tab");
			m_NextPreviousAction.Enable();
		}

		private void UnregisterFixedActions()
		{
			if (m_NextPreviousAction != null)
			{
				m_NextPreviousAction.Disable();
				m_NextPreviousAction = null;
			}
		}

		private global::UnityEngine.InputSystem.InputAction FindActionAndRegisterCallback(string actionNameOrId, global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> callback = null)
		{
			global::UnityEngine.InputSystem.InputAction inputAction = m_InputActionAsset.FindAction(actionNameOrId);
			if (inputAction != null && callback != null)
			{
				inputAction.performed += callback;
			}
			return inputAction;
		}

		private void RegisterActions()
		{
			s_OnRegisterActions?.Invoke(m_InputActionAsset);
			m_PointAction = FindActionAndRegisterCallback(global::UnityEngine.InputSystem.Plugins.InputForUI.InputSystemProvider.Actions.PointAction, OnPointerPerformed);
			m_MoveAction = FindActionAndRegisterCallback(global::UnityEngine.InputSystem.Plugins.InputForUI.InputSystemProvider.Actions.MoveAction);
			m_SubmitAction = FindActionAndRegisterCallback(global::UnityEngine.InputSystem.Plugins.InputForUI.InputSystemProvider.Actions.SubmitAction, OnSubmitPerformed);
			m_CancelAction = FindActionAndRegisterCallback(global::UnityEngine.InputSystem.Plugins.InputForUI.InputSystemProvider.Actions.CancelAction, OnCancelPerformed);
			m_LeftClickAction = FindActionAndRegisterCallback(global::UnityEngine.InputSystem.Plugins.InputForUI.InputSystemProvider.Actions.LeftClickAction, OnLeftClickPerformed);
			m_MiddleClickAction = FindActionAndRegisterCallback(global::UnityEngine.InputSystem.Plugins.InputForUI.InputSystemProvider.Actions.MiddleClickAction, OnMiddleClickPerformed);
			m_RightClickAction = FindActionAndRegisterCallback(global::UnityEngine.InputSystem.Plugins.InputForUI.InputSystemProvider.Actions.RightClickAction, OnRightClickPerformed);
			m_ScrollWheelAction = FindActionAndRegisterCallback(global::UnityEngine.InputSystem.Plugins.InputForUI.InputSystemProvider.Actions.ScrollWheelAction, OnScrollWheelPerformed);
			if (global::UnityEngine.InputSystem.InputSystem.actions == null)
			{
				m_InputActionAsset.FindActionMap("UI", throwIfNotFound: true).Enable();
			}
			else
			{
				m_InputActionAsset.Enable();
			}
		}

		private void UnregisterAction(ref global::UnityEngine.InputSystem.InputAction action, global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> callback = null)
		{
			if (action != null && callback != null)
			{
				action.performed -= callback;
			}
			action = null;
		}

		private void UnregisterActions()
		{
			UnregisterAction(ref m_PointAction, OnPointerPerformed);
			UnregisterAction(ref m_MoveAction);
			UnregisterAction(ref m_SubmitAction, OnSubmitPerformed);
			UnregisterAction(ref m_CancelAction, OnCancelPerformed);
			UnregisterAction(ref m_LeftClickAction, OnLeftClickPerformed);
			UnregisterAction(ref m_MiddleClickAction, OnMiddleClickPerformed);
			UnregisterAction(ref m_RightClickAction, OnRightClickPerformed);
			UnregisterAction(ref m_ScrollWheelAction, OnScrollWheelPerformed);
			if (m_InputActionAsset != null)
			{
				m_InputActionAsset.Disable();
			}
		}

		private void SelectInputActionAsset()
		{
			global::UnityEngine.InputSystem.InputActionAsset actions = global::UnityEngine.InputSystem.InputSystem.actions;
			if (actions != null && actions.FindActionMap("UI") != null)
			{
				m_InputActionAsset = global::UnityEngine.InputSystem.InputSystem.actions;
				return;
			}
			if (m_DefaultInputActions == null)
			{
				m_DefaultInputActions = new global::UnityEngine.InputSystem.DefaultInputActions();
			}
			m_InputActionAsset = m_DefaultInputActions.asset;
		}

		internal static void SetOnRegisterActions(global::System.Action<global::UnityEngine.InputSystem.InputActionAsset> callback)
		{
			s_OnRegisterActions = callback;
		}
	}
}
