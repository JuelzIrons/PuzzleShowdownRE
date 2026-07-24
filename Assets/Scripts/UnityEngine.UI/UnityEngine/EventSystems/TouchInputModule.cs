namespace UnityEngine.EventSystems
{
	[global::System.Obsolete("TouchInputModule is no longer required as Touch input is now handled in StandaloneInputModule.")]
	[global::UnityEngine.AddComponentMenu("Event/Touch Input Module")]
	public class TouchInputModule : global::UnityEngine.EventSystems.PointerInputModule
	{
		private global::UnityEngine.Vector2 m_LastMousePosition;

		private global::UnityEngine.Vector2 m_MousePosition;

		private global::UnityEngine.EventSystems.PointerEventData m_InputPointerEvent;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("m_AllowActivationOnStandalone")]
		private bool m_ForceModuleActive;

		[global::System.Obsolete("allowActivationOnStandalone has been deprecated. Use forceModuleActive instead (UnityUpgradable) -> forceModuleActive")]
		public bool allowActivationOnStandalone
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

		protected TouchInputModule()
		{
		}

		public override void UpdateModule()
		{
			if (!base.eventSystem.isFocused)
			{
				if (m_InputPointerEvent != null && m_InputPointerEvent.pointerDrag != null && m_InputPointerEvent.dragging)
				{
					global::UnityEngine.EventSystems.ExecuteEvents.Execute(m_InputPointerEvent.pointerDrag, m_InputPointerEvent, global::UnityEngine.EventSystems.ExecuteEvents.endDragHandler);
				}
				m_InputPointerEvent = null;
			}
			m_LastMousePosition = m_MousePosition;
			m_MousePosition = base.input.mousePosition;
		}

		public override bool IsModuleSupported()
		{
			if (!forceModuleActive)
			{
				return base.input.touchSupported;
			}
			return true;
		}

		public override bool ShouldActivateModule()
		{
			if (!base.ShouldActivateModule())
			{
				return false;
			}
			if (m_ForceModuleActive)
			{
				return true;
			}
			if (UseFakeInput())
			{
				return base.input.GetMouseButtonDown(0) | ((m_MousePosition - m_LastMousePosition).sqrMagnitude > 0f);
			}
			return base.input.touchCount > 0;
		}

		private bool UseFakeInput()
		{
			return !base.input.touchSupported;
		}

		public override void Process()
		{
			if (UseFakeInput())
			{
				FakeTouches();
			}
			else
			{
				ProcessTouchEvents();
			}
		}

		private void FakeTouches()
		{
			global::UnityEngine.EventSystems.PointerInputModule.MouseButtonEventData eventData = GetMousePointerEventData(0).GetButtonState(global::UnityEngine.EventSystems.PointerEventData.InputButton.Left).eventData;
			if (eventData.PressedThisFrame())
			{
				eventData.buttonData.delta = global::UnityEngine.Vector2.zero;
			}
			ProcessTouchPress(eventData.buttonData, eventData.PressedThisFrame(), eventData.ReleasedThisFrame());
			if (base.input.GetMouseButton(0))
			{
				ProcessMove(eventData.buttonData);
				ProcessDrag(eventData.buttonData);
			}
		}

		private void ProcessTouchEvents()
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
				global::UnityEngine.GameObject gameObject2 = global::UnityEngine.EventSystems.ExecuteEvents.ExecuteHierarchy(gameObject, pointerEvent, global::UnityEngine.EventSystems.ExecuteEvents.pointerDownHandler);
				if (gameObject2 == null)
				{
					gameObject2 = global::UnityEngine.EventSystems.ExecuteEvents.GetEventHandler<global::UnityEngine.EventSystems.IPointerClickHandler>(gameObject);
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
				pointerEvent.clickTime = unscaledTime;
				pointerEvent.pointerDrag = global::UnityEngine.EventSystems.ExecuteEvents.GetEventHandler<global::UnityEngine.EventSystems.IDragHandler>(gameObject);
				if (pointerEvent.pointerDrag != null)
				{
					global::UnityEngine.EventSystems.ExecuteEvents.Execute(pointerEvent.pointerDrag, pointerEvent, global::UnityEngine.EventSystems.ExecuteEvents.initializePotentialDrag);
				}
				m_InputPointerEvent = pointerEvent;
			}
			if (released)
			{
				global::UnityEngine.EventSystems.ExecuteEvents.Execute(pointerEvent.pointerPress, pointerEvent, global::UnityEngine.EventSystems.ExecuteEvents.pointerUpHandler);
				global::UnityEngine.GameObject eventHandler = global::UnityEngine.EventSystems.ExecuteEvents.GetEventHandler<global::UnityEngine.EventSystems.IPointerClickHandler>(gameObject);
				if (pointerEvent.pointerPress == eventHandler && pointerEvent.eligibleForClick)
				{
					global::UnityEngine.EventSystems.ExecuteEvents.Execute(pointerEvent.pointerPress, pointerEvent, global::UnityEngine.EventSystems.ExecuteEvents.pointerClickHandler);
				}
				else if (pointerEvent.pointerDrag != null && pointerEvent.dragging)
				{
					global::UnityEngine.EventSystems.ExecuteEvents.ExecuteHierarchy(gameObject, pointerEvent, global::UnityEngine.EventSystems.ExecuteEvents.dropHandler);
				}
				pointerEvent.eligibleForClick = false;
				pointerEvent.pointerPress = null;
				pointerEvent.rawPointerPress = null;
				if (pointerEvent.pointerDrag != null && pointerEvent.dragging)
				{
					global::UnityEngine.EventSystems.ExecuteEvents.Execute(pointerEvent.pointerDrag, pointerEvent, global::UnityEngine.EventSystems.ExecuteEvents.endDragHandler);
				}
				pointerEvent.dragging = false;
				pointerEvent.pointerDrag = null;
				global::UnityEngine.EventSystems.ExecuteEvents.ExecuteHierarchy(pointerEvent.pointerEnter, pointerEvent, global::UnityEngine.EventSystems.ExecuteEvents.pointerExitHandler);
				pointerEvent.pointerEnter = null;
				m_InputPointerEvent = pointerEvent;
			}
		}

		public override void DeactivateModule()
		{
			base.DeactivateModule();
			ClearSelection();
		}

		public override string ToString()
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			stringBuilder.AppendLine(UseFakeInput() ? "Input: Faked" : "Input: Touch");
			if (UseFakeInput())
			{
				global::UnityEngine.EventSystems.PointerEventData lastPointerEventData = GetLastPointerEventData(-1);
				if (lastPointerEventData != null)
				{
					stringBuilder.AppendLine(lastPointerEventData.ToString());
				}
			}
			else
			{
				foreach (global::System.Collections.Generic.KeyValuePair<int, global::UnityEngine.EventSystems.PointerEventData> pointerDatum in m_PointerData)
				{
					stringBuilder.AppendLine(pointerDatum.ToString());
				}
			}
			return stringBuilder.ToString();
		}
	}
}
