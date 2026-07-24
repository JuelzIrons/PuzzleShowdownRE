namespace UnityEngine.EventSystems
{
	[global::UnityEngine.AddComponentMenu("Event/Standalone Input Module")]
	public class StandaloneInputModule : global::UnityEngine.EventSystems.PointerInputModule
	{
		[global::System.Obsolete("Mode is no longer needed on input module as it handles both mouse and keyboard simultaneously.", false)]
		public enum InputMode
		{
			Mouse = 0,
			Buttons = 1
		}

		private float m_PrevActionTime;

		private global::UnityEngine.Vector2 m_LastMoveVector;

		private int m_ConsecutiveMoveCount;

		private global::UnityEngine.Vector2 m_LastMousePosition;

		private global::UnityEngine.Vector2 m_MousePosition;

		private global::UnityEngine.GameObject m_CurrentFocusedGameObject;

		private readonly global::System.Collections.Generic.Dictionary<int, global::UnityEngine.EventSystems.PointerEventData> m_InputPointerEvents = new global::System.Collections.Generic.Dictionary<int, global::UnityEngine.EventSystems.PointerEventData>();

		private const float doubleClickTime = 0.3f;

		[global::UnityEngine.SerializeField]
		private string m_HorizontalAxis = "Horizontal";

		[global::UnityEngine.SerializeField]
		private string m_VerticalAxis = "Vertical";

		[global::UnityEngine.SerializeField]
		private string m_SubmitButton = "Submit";

		[global::UnityEngine.SerializeField]
		private string m_CancelButton = "Cancel";

		[global::UnityEngine.SerializeField]
		private float m_InputActionsPerSecond = 10f;

		[global::UnityEngine.SerializeField]
		private float m_RepeatDelay = 0.5f;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("m_AllowActivationOnMobileDevice")]
		[global::UnityEngine.HideInInspector]
		private bool m_ForceModuleActive;

		[global::System.Obsolete("Mode is no longer needed on input module as it handles both mouse and keyboard simultaneously.", false)]
		public global::UnityEngine.EventSystems.StandaloneInputModule.InputMode inputMode => global::UnityEngine.EventSystems.StandaloneInputModule.InputMode.Mouse;

		[global::System.Obsolete("allowActivationOnMobileDevice has been deprecated. Use forceModuleActive instead (UnityUpgradable) -> forceModuleActive")]
		public bool allowActivationOnMobileDevice
		{
			get
			{
				return m_ForceModuleActive;
			}
			set
			{
				m_ForceModuleActive = value;
			}
		}

		[global::System.Obsolete("forceModuleActive has been deprecated. There is no need to force the module awake as StandaloneInputModule works for all platforms")]
		public bool forceModuleActive
		{
			get
			{
				return m_ForceModuleActive;
			}
			set
			{
				m_ForceModuleActive = value;
			}
		}

		public float inputActionsPerSecond
		{
			get
			{
				return m_InputActionsPerSecond;
			}
			set
			{
				m_InputActionsPerSecond = value;
			}
		}

		public float repeatDelay
		{
			get
			{
				return m_RepeatDelay;
			}
			set
			{
				m_RepeatDelay = value;
			}
		}

		public string horizontalAxis
		{
			get
			{
				return m_HorizontalAxis;
			}
			set
			{
				m_HorizontalAxis = value;
			}
		}

		public string verticalAxis
		{
			get
			{
				return m_VerticalAxis;
			}
			set
			{
				m_VerticalAxis = value;
			}
		}

		public string submitButton
		{
			get
			{
				return m_SubmitButton;
			}
			set
			{
				m_SubmitButton = value;
			}
		}

		public string cancelButton
		{
			get
			{
				return m_CancelButton;
			}
			set
			{
				m_CancelButton = value;
			}
		}

		protected StandaloneInputModule()
		{
		}

		private bool ShouldIgnoreEventsOnNoFocus()
		{
			return true;
		}

		public override void UpdateModule()
		{
			if (!base.eventSystem.isFocused && ShouldIgnoreEventsOnNoFocus())
			{
				ReleasePointerDrags();
				return;
			}
			m_LastMousePosition = m_MousePosition;
			m_MousePosition = base.input.mousePosition;
		}

		private void ReleasePointerDrags()
		{
			global::System.Collections.Generic.List<int> value;
			using (global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<int>, int>.Get(out value))
			{
				foreach (int key in m_InputPointerEvents.Keys)
				{
					value.Add(key);
				}
				foreach (int item in value)
				{
					if (m_InputPointerEvents.TryGetValue(item, out var value2) && value2 != null && value2.pointerDrag != null && value2.dragging)
					{
						ReleaseMouse(value2, value2.pointerCurrentRaycast.gameObject);
					}
				}
			}
			m_InputPointerEvents.Clear();
		}

		private void ReleaseMouse(global::UnityEngine.EventSystems.PointerEventData pointerEvent, global::UnityEngine.GameObject currentOverGo)
		{
			global::UnityEngine.EventSystems.ExecuteEvents.Execute(pointerEvent.pointerPress, pointerEvent, global::UnityEngine.EventSystems.ExecuteEvents.pointerUpHandler);
			global::UnityEngine.GameObject eventHandler = global::UnityEngine.EventSystems.ExecuteEvents.GetEventHandler<global::UnityEngine.EventSystems.IPointerClickHandler>(currentOverGo);
			if (pointerEvent.pointerClick == eventHandler && pointerEvent.eligibleForClick)
			{
				global::UnityEngine.EventSystems.ExecuteEvents.Execute(pointerEvent.pointerClick, pointerEvent, global::UnityEngine.EventSystems.ExecuteEvents.pointerClickHandler);
			}
			if (pointerEvent.pointerDrag != null && pointerEvent.dragging)
			{
				global::UnityEngine.EventSystems.ExecuteEvents.ExecuteHierarchy(currentOverGo, pointerEvent, global::UnityEngine.EventSystems.ExecuteEvents.dropHandler);
			}
			pointerEvent.eligibleForClick = false;
			pointerEvent.pointerPress = null;
			pointerEvent.rawPointerPress = null;
			pointerEvent.pointerClick = null;
			if (pointerEvent.pointerDrag != null && pointerEvent.dragging)
			{
				global::UnityEngine.EventSystems.ExecuteEvents.Execute(pointerEvent.pointerDrag, pointerEvent, global::UnityEngine.EventSystems.ExecuteEvents.endDragHandler);
			}
			pointerEvent.dragging = false;
			pointerEvent.pointerDrag = null;
			if (currentOverGo != pointerEvent.pointerEnter)
			{
				HandlePointerExitAndEnter(pointerEvent, null);
				HandlePointerExitAndEnter(pointerEvent, currentOverGo);
			}
			m_InputPointerEvents[pointerEvent.pointerId] = pointerEvent;
		}

		public override bool ShouldActivateModule()
		{
			if (!base.ShouldActivateModule())
			{
				return false;
			}
			bool flag = m_ForceModuleActive;
			flag |= base.input.GetButtonDown(m_SubmitButton);
			flag |= base.input.GetButtonDown(m_CancelButton);
			flag |= !global::UnityEngine.Mathf.Approximately(base.input.GetAxisRaw(m_HorizontalAxis), 0f);
			flag |= !global::UnityEngine.Mathf.Approximately(base.input.GetAxisRaw(m_VerticalAxis), 0f);
			flag |= (m_MousePosition - m_LastMousePosition).sqrMagnitude > 0f;
			flag |= base.input.GetMouseButtonDown(0);
			if (base.input.touchCount > 0)
			{
				flag = true;
			}
			return flag;
		}

		public override void ActivateModule()
		{
			if (base.eventSystem.isFocused || !ShouldIgnoreEventsOnNoFocus())
			{
				base.ActivateModule();
				m_MousePosition = base.input.mousePosition;
				m_LastMousePosition = base.input.mousePosition;
				global::UnityEngine.GameObject gameObject = base.eventSystem.currentSelectedGameObject;
				if (gameObject == null)
				{
					gameObject = base.eventSystem.firstSelectedGameObject;
				}
				base.eventSystem.SetSelectedGameObject(gameObject, GetBaseEventData());
			}
		}

		public override void DeactivateModule()
		{
			base.DeactivateModule();
			ClearSelection();
		}

		public override void Process()
		{
			if (!base.eventSystem.isFocused && ShouldIgnoreEventsOnNoFocus())
			{
				return;
			}
			bool flag = SendUpdateEventToSelectedObject();
			if (!ProcessTouchEvents() && base.input.mousePresent)
			{
				ProcessMouseEvent();
			}
			if (base.eventSystem.sendNavigationEvents)
			{
				if (!flag)
				{
					flag |= SendMoveEventToSelectedObject();
				}
				if (!flag)
				{
					SendSubmitEventToSelectedObject();
				}
			}
		}

		private bool ProcessTouchEvents()
		{
			for (int i = 0; i < base.input.touchCount; i++)
			{
				global::UnityEngine.Touch touch = base.input.GetTouch(i);
				if (touch.type != global::UnityEngine.TouchType.Indirect)
				{
					bool pressed;
					bool released;
					global::UnityEngine.EventSystems.PointerEventData touchPointerEventData = GetTouchPointerEventData(touch, out pressed, out released);
					ProcessTouchPress(touchPointerEventData, pressed, released);
					if (!released)
					{
						ProcessMove(touchPointerEventData);
						ProcessDrag(touchPointerEventData);
					}
					else
					{
						RemovePointerData(touchPointerEventData);
					}
				}
			}
			return base.input.touchCount > 0;
		}

		protected void ProcessTouchPress(global::UnityEngine.EventSystems.PointerEventData pointerEvent, bool pressed, bool released)
		{
			global::UnityEngine.GameObject gameObject = pointerEvent.pointerCurrentRaycast.gameObject;
			if (pressed)
			{
				pointerEvent.eligibleForClick = true;
				pointerEvent.delta = global::UnityEngine.Vector2.zero;
				pointerEvent.dragging = false;
				pointerEvent.useDragThreshold = true;
				pointerEvent.pressPosition = pointerEvent.position;
				pointerEvent.pointerPressRaycast = pointerEvent.pointerCurrentRaycast;
				DeselectIfSelectionChanged(gameObject, pointerEvent);
				if (pointerEvent.pointerEnter != gameObject)
				{
					HandlePointerExitAndEnter(pointerEvent, gameObject);
					pointerEvent.pointerEnter = gameObject;
				}
				if (global::UnityEngine.Time.unscaledTime - pointerEvent.clickTime >= 0.3f)
				{
					pointerEvent.clickCount = 0;
				}
				global::UnityEngine.GameObject gameObject2 = global::UnityEngine.EventSystems.ExecuteEvents.ExecuteHierarchy(gameObject, pointerEvent, global::UnityEngine.EventSystems.ExecuteEvents.pointerDownHandler);
				global::UnityEngine.GameObject eventHandler = global::UnityEngine.EventSystems.ExecuteEvents.GetEventHandler<global::UnityEngine.EventSystems.IPointerClickHandler>(gameObject);
				if (gameObject2 == null)
				{
					gameObject2 = eventHandler;
				}
				float unscaledTime = global::UnityEngine.Time.unscaledTime;
				if (gameObject2 == pointerEvent.lastPress)
				{
					if (unscaledTime - pointerEvent.clickTime < 0.3f)
					{
						int clickCount = pointerEvent.clickCount + 1;
						pointerEvent.clickCount = clickCount;
					}
					else
					{
						pointerEvent.clickCount = 1;
					}
					pointerEvent.clickTime = unscaledTime;
				}
				else
				{
					pointerEvent.clickCount = 1;
				}
				pointerEvent.pointerPress = gameObject2;
				pointerEvent.rawPointerPress = gameObject;
				pointerEvent.pointerClick = eventHandler;
				pointerEvent.clickTime = unscaledTime;
				pointerEvent.pointerDrag = global::UnityEngine.EventSystems.ExecuteEvents.GetEventHandler<global::UnityEngine.EventSystems.IDragHandler>(gameObject);
				if (pointerEvent.pointerDrag != null)
				{
					global::UnityEngine.EventSystems.ExecuteEvents.Execute(pointerEvent.pointerDrag, pointerEvent, global::UnityEngine.EventSystems.ExecuteEvents.initializePotentialDrag);
				}
			}
			if (released)
			{
				global::UnityEngine.EventSystems.ExecuteEvents.Execute(pointerEvent.pointerPress, pointerEvent, global::UnityEngine.EventSystems.ExecuteEvents.pointerUpHandler);
				global::UnityEngine.GameObject eventHandler2 = global::UnityEngine.EventSystems.ExecuteEvents.GetEventHandler<global::UnityEngine.EventSystems.IPointerClickHandler>(gameObject);
				if (pointerEvent.pointerClick == eventHandler2 && pointerEvent.eligibleForClick)
				{
					global::UnityEngine.EventSystems.ExecuteEvents.Execute(pointerEvent.pointerClick, pointerEvent, global::UnityEngine.EventSystems.ExecuteEvents.pointerClickHandler);
				}
				if (pointerEvent.pointerDrag != null && pointerEvent.dragging)
				{
					global::UnityEngine.EventSystems.ExecuteEvents.ExecuteHierarchy(gameObject, pointerEvent, global::UnityEngine.EventSystems.ExecuteEvents.dropHandler);
				}
				pointerEvent.eligibleForClick = false;
				pointerEvent.pointerPress = null;
				pointerEvent.rawPointerPress = null;
				pointerEvent.pointerClick = null;
				if (pointerEvent.pointerDrag != null && pointerEvent.dragging)
				{
					global::UnityEngine.EventSystems.ExecuteEvents.Execute(pointerEvent.pointerDrag, pointerEvent, global::UnityEngine.EventSystems.ExecuteEvents.endDragHandler);
				}
				pointerEvent.dragging = false;
				pointerEvent.pointerDrag = null;
				global::UnityEngine.EventSystems.ExecuteEvents.ExecuteHierarchy(pointerEvent.pointerEnter, pointerEvent, global::UnityEngine.EventSystems.ExecuteEvents.pointerExitHandler);
				pointerEvent.pointerEnter = null;
			}
			m_InputPointerEvents[pointerEvent.pointerId] = pointerEvent;
		}

		protected bool SendSubmitEventToSelectedObject()
		{
			if (base.eventSystem.currentSelectedGameObject == null)
			{
				return false;
			}
			global::UnityEngine.EventSystems.BaseEventData baseEventData = GetBaseEventData();
			if (base.input.GetButtonDown(m_SubmitButton))
			{
				global::UnityEngine.EventSystems.ExecuteEvents.Execute(base.eventSystem.currentSelectedGameObject, baseEventData, global::UnityEngine.EventSystems.ExecuteEvents.submitHandler);
			}
			if (base.input.GetButtonDown(m_CancelButton))
			{
				global::UnityEngine.EventSystems.ExecuteEvents.Execute(base.eventSystem.currentSelectedGameObject, baseEventData, global::UnityEngine.EventSystems.ExecuteEvents.cancelHandler);
			}
			return baseEventData.used;
		}

		private global::UnityEngine.Vector2 GetRawMoveVector()
		{
			global::UnityEngine.Vector2 zero = global::UnityEngine.Vector2.zero;
			zero.x = base.input.GetAxisRaw(m_HorizontalAxis);
			zero.y = base.input.GetAxisRaw(m_VerticalAxis);
			if (base.input.GetButtonDown(m_HorizontalAxis))
			{
				if (zero.x < 0f)
				{
					zero.x = -1f;
				}
				if (zero.x > 0f)
				{
					zero.x = 1f;
				}
			}
			if (base.input.GetButtonDown(m_VerticalAxis))
			{
				if (zero.y < 0f)
				{
					zero.y = -1f;
				}
				if (zero.y > 0f)
				{
					zero.y = 1f;
				}
			}
			return zero;
		}

		protected bool SendMoveEventToSelectedObject()
		{
			float unscaledTime = global::UnityEngine.Time.unscaledTime;
			global::UnityEngine.Vector2 rawMoveVector = GetRawMoveVector();
			if (global::UnityEngine.Mathf.Approximately(rawMoveVector.x, 0f) && global::UnityEngine.Mathf.Approximately(rawMoveVector.y, 0f))
			{
				m_ConsecutiveMoveCount = 0;
				return false;
			}
			bool flag = global::UnityEngine.Vector2.Dot(rawMoveVector, m_LastMoveVector) > 0f;
			if (flag && m_ConsecutiveMoveCount == 1)
			{
				if (unscaledTime <= m_PrevActionTime + m_RepeatDelay)
				{
					return false;
				}
			}
			else if (unscaledTime <= m_PrevActionTime + 1f / m_InputActionsPerSecond)
			{
				return false;
			}
			global::UnityEngine.EventSystems.AxisEventData axisEventData = GetAxisEventData(rawMoveVector.x, rawMoveVector.y, 0.6f);
			if (axisEventData.moveDir != global::UnityEngine.EventSystems.MoveDirection.None)
			{
				global::UnityEngine.EventSystems.ExecuteEvents.Execute(base.eventSystem.currentSelectedGameObject, axisEventData, global::UnityEngine.EventSystems.ExecuteEvents.moveHandler);
				if (!flag)
				{
					m_ConsecutiveMoveCount = 0;
				}
				m_ConsecutiveMoveCount++;
				m_PrevActionTime = unscaledTime;
				m_LastMoveVector = rawMoveVector;
			}
			else
			{
				m_ConsecutiveMoveCount = 0;
			}
			return axisEventData.used;
		}

		protected void ProcessMouseEvent()
		{
			ProcessMouseEvent(0);
		}

		[global::System.Obsolete("This method is no longer checked, overriding it with return true does nothing!")]
		protected virtual bool ForceAutoSelect()
		{
			return false;
		}

		protected void ProcessMouseEvent(int id)
		{
			global::UnityEngine.EventSystems.PointerInputModule.MouseState mousePointerEventData = GetMousePointerEventData(id);
			global::UnityEngine.EventSystems.PointerInputModule.MouseButtonEventData eventData = mousePointerEventData.GetButtonState(global::UnityEngine.EventSystems.PointerEventData.InputButton.Left).eventData;
			m_CurrentFocusedGameObject = eventData.buttonData.pointerCurrentRaycast.gameObject;
			ProcessMousePress(eventData);
			ProcessMove(eventData.buttonData);
			ProcessDrag(eventData.buttonData);
			ProcessMousePress(mousePointerEventData.GetButtonState(global::UnityEngine.EventSystems.PointerEventData.InputButton.Right).eventData);
			ProcessDrag(mousePointerEventData.GetButtonState(global::UnityEngine.EventSystems.PointerEventData.InputButton.Right).eventData.buttonData);
			ProcessMousePress(mousePointerEventData.GetButtonState(global::UnityEngine.EventSystems.PointerEventData.InputButton.Middle).eventData);
			ProcessDrag(mousePointerEventData.GetButtonState(global::UnityEngine.EventSystems.PointerEventData.InputButton.Middle).eventData.buttonData);
			if (!global::UnityEngine.Mathf.Approximately(eventData.buttonData.scrollDelta.sqrMagnitude, 0f))
			{
				global::UnityEngine.EventSystems.ExecuteEvents.ExecuteHierarchy(global::UnityEngine.EventSystems.ExecuteEvents.GetEventHandler<global::UnityEngine.EventSystems.IScrollHandler>(eventData.buttonData.pointerCurrentRaycast.gameObject), eventData.buttonData, global::UnityEngine.EventSystems.ExecuteEvents.scrollHandler);
			}
		}

		protected bool SendUpdateEventToSelectedObject()
		{
			if (base.eventSystem.currentSelectedGameObject == null)
			{
				return false;
			}
			global::UnityEngine.EventSystems.BaseEventData baseEventData = GetBaseEventData();
			global::UnityEngine.EventSystems.ExecuteEvents.Execute(base.eventSystem.currentSelectedGameObject, baseEventData, global::UnityEngine.EventSystems.ExecuteEvents.updateSelectedHandler);
			return baseEventData.used;
		}

		protected void ProcessMousePress(global::UnityEngine.EventSystems.PointerInputModule.MouseButtonEventData data)
		{
			global::UnityEngine.EventSystems.PointerEventData buttonData = data.buttonData;
			global::UnityEngine.GameObject gameObject = buttonData.pointerCurrentRaycast.gameObject;
			if (data.PressedThisFrame())
			{
				buttonData.eligibleForClick = true;
				buttonData.delta = global::UnityEngine.Vector2.zero;
				buttonData.dragging = false;
				buttonData.useDragThreshold = true;
				buttonData.pressPosition = buttonData.position;
				buttonData.pointerPressRaycast = buttonData.pointerCurrentRaycast;
				DeselectIfSelectionChanged(gameObject, buttonData);
				if (global::UnityEngine.Time.unscaledTime - buttonData.clickTime >= 0.3f)
				{
					buttonData.clickCount = 0;
				}
				global::UnityEngine.GameObject gameObject2 = global::UnityEngine.EventSystems.ExecuteEvents.ExecuteHierarchy(gameObject, buttonData, global::UnityEngine.EventSystems.ExecuteEvents.pointerDownHandler);
				global::UnityEngine.GameObject eventHandler = global::UnityEngine.EventSystems.ExecuteEvents.GetEventHandler<global::UnityEngine.EventSystems.IPointerClickHandler>(gameObject);
				if (gameObject2 == null)
				{
					gameObject2 = eventHandler;
				}
				float unscaledTime = global::UnityEngine.Time.unscaledTime;
				if (gameObject2 == buttonData.lastPress)
				{
					if (unscaledTime - buttonData.clickTime < 0.3f)
					{
						int clickCount = buttonData.clickCount + 1;
						buttonData.clickCount = clickCount;
					}
					else
					{
						buttonData.clickCount = 1;
					}
					buttonData.clickTime = unscaledTime;
				}
				else
				{
					buttonData.clickCount = 1;
				}
				buttonData.pointerPress = gameObject2;
				buttonData.rawPointerPress = gameObject;
				buttonData.pointerClick = eventHandler;
				buttonData.clickTime = unscaledTime;
				buttonData.pointerDrag = global::UnityEngine.EventSystems.ExecuteEvents.GetEventHandler<global::UnityEngine.EventSystems.IDragHandler>(gameObject);
				if (buttonData.pointerDrag != null)
				{
					global::UnityEngine.EventSystems.ExecuteEvents.Execute(buttonData.pointerDrag, buttonData, global::UnityEngine.EventSystems.ExecuteEvents.initializePotentialDrag);
				}
				m_InputPointerEvents[buttonData.pointerId] = buttonData;
			}
			if (data.ReleasedThisFrame())
			{
				ReleaseMouse(buttonData, gameObject);
			}
		}

		protected global::UnityEngine.GameObject GetCurrentFocusedGameObject()
		{
			return m_CurrentFocusedGameObject;
		}
	}
}
