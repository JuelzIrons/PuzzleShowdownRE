namespace UnityEngine.EventSystems
{
	public abstract class PointerInputModule : global::UnityEngine.EventSystems.BaseInputModule
	{
		protected class ButtonState
		{
			private global::UnityEngine.EventSystems.PointerEventData.InputButton m_Button;

			private global::UnityEngine.EventSystems.PointerInputModule.MouseButtonEventData m_EventData;

			public global::UnityEngine.EventSystems.PointerInputModule.MouseButtonEventData eventData
			{
				get
				{
					return m_EventData;
				}
				set
				{
					m_EventData = value;
				}
			}

			public global::UnityEngine.EventSystems.PointerEventData.InputButton button
			{
				get
				{
					return m_Button;
				}
				set
				{
					m_Button = value;
				}
			}
		}

		protected class MouseState
		{
			private global::System.Collections.Generic.List<global::UnityEngine.EventSystems.PointerInputModule.ButtonState> m_TrackedButtons = new global::System.Collections.Generic.List<global::UnityEngine.EventSystems.PointerInputModule.ButtonState>();

			public bool AnyPressesThisFrame()
			{
				int count = m_TrackedButtons.Count;
				for (int i = 0; i < count; i++)
				{
					if (m_TrackedButtons[i].eventData.PressedThisFrame())
					{
						return true;
					}
				}
				return false;
			}

			public bool AnyReleasesThisFrame()
			{
				int count = m_TrackedButtons.Count;
				for (int i = 0; i < count; i++)
				{
					if (m_TrackedButtons[i].eventData.ReleasedThisFrame())
					{
						return true;
					}
				}
				return false;
			}

			public global::UnityEngine.EventSystems.PointerInputModule.ButtonState GetButtonState(global::UnityEngine.EventSystems.PointerEventData.InputButton button)
			{
				global::UnityEngine.EventSystems.PointerInputModule.ButtonState buttonState = null;
				int count = m_TrackedButtons.Count;
				for (int i = 0; i < count; i++)
				{
					if (m_TrackedButtons[i].button == button)
					{
						buttonState = m_TrackedButtons[i];
						break;
					}
				}
				if (buttonState == null)
				{
					buttonState = new global::UnityEngine.EventSystems.PointerInputModule.ButtonState
					{
						button = button,
						eventData = new global::UnityEngine.EventSystems.PointerInputModule.MouseButtonEventData()
					};
					m_TrackedButtons.Add(buttonState);
				}
				return buttonState;
			}

			public void SetButtonState(global::UnityEngine.EventSystems.PointerEventData.InputButton button, global::UnityEngine.EventSystems.PointerEventData.FramePressState stateForMouseButton, global::UnityEngine.EventSystems.PointerEventData data)
			{
				global::UnityEngine.EventSystems.PointerInputModule.ButtonState buttonState = GetButtonState(button);
				buttonState.eventData.buttonState = stateForMouseButton;
				buttonState.eventData.buttonData = data;
			}
		}

		public class MouseButtonEventData
		{
			public global::UnityEngine.EventSystems.PointerEventData.FramePressState buttonState;

			public global::UnityEngine.EventSystems.PointerEventData buttonData;

			public bool PressedThisFrame()
			{
				if (buttonState != global::UnityEngine.EventSystems.PointerEventData.FramePressState.Pressed)
				{
					return buttonState == global::UnityEngine.EventSystems.PointerEventData.FramePressState.PressedAndReleased;
				}
				return true;
			}

			public bool ReleasedThisFrame()
			{
				if (buttonState != global::UnityEngine.EventSystems.PointerEventData.FramePressState.Released)
				{
					return buttonState == global::UnityEngine.EventSystems.PointerEventData.FramePressState.PressedAndReleased;
				}
				return true;
			}
		}

		public const int kMouseLeftId = -1;

		public const int kMouseRightId = -2;

		public const int kMouseMiddleId = -3;

		public const int kFakeTouchesId = -4;

		protected global::System.Collections.Generic.Dictionary<int, global::UnityEngine.EventSystems.PointerEventData> m_PointerData = new global::System.Collections.Generic.Dictionary<int, global::UnityEngine.EventSystems.PointerEventData>();

		private readonly global::UnityEngine.EventSystems.PointerInputModule.MouseState m_MouseState = new global::UnityEngine.EventSystems.PointerInputModule.MouseState();

		protected bool GetPointerData(int id, out global::UnityEngine.EventSystems.PointerEventData data, bool create)
		{
			if (!m_PointerData.TryGetValue(id, out data) && create)
			{
				data = new global::UnityEngine.EventSystems.PointerEventData(base.eventSystem)
				{
					pointerId = id
				};
				m_PointerData.Add(id, data);
				return true;
			}
			return false;
		}

		protected void RemovePointerData(global::UnityEngine.EventSystems.PointerEventData data)
		{
			m_PointerData.Remove(data.pointerId);
		}

		protected global::UnityEngine.EventSystems.PointerEventData GetTouchPointerEventData(global::UnityEngine.Touch input, out bool pressed, out bool released)
		{
			global::UnityEngine.EventSystems.PointerEventData data;
			bool pointerData = GetPointerData(input.fingerId, out data, create: true);
			data.Reset();
			pressed = pointerData || input.phase == global::UnityEngine.TouchPhase.Began;
			released = input.phase == global::UnityEngine.TouchPhase.Canceled || input.phase == global::UnityEngine.TouchPhase.Ended;
			if (pointerData)
			{
				data.position = input.position;
			}
			if (pressed)
			{
				data.delta = global::UnityEngine.Vector2.zero;
			}
			else
			{
				data.delta = input.position - data.position;
			}
			data.position = input.position;
			data.button = global::UnityEngine.EventSystems.PointerEventData.InputButton.Left;
			if (input.phase == global::UnityEngine.TouchPhase.Canceled)
			{
				data.pointerCurrentRaycast = default(global::UnityEngine.EventSystems.RaycastResult);
			}
			else
			{
				base.eventSystem.RaycastAll(data, m_RaycastResultCache);
				global::UnityEngine.EventSystems.RaycastResult pointerCurrentRaycast = global::UnityEngine.EventSystems.BaseInputModule.FindFirstRaycast(m_RaycastResultCache);
				data.pointerCurrentRaycast = pointerCurrentRaycast;
				m_RaycastResultCache.Clear();
			}
			data.pressure = input.pressure;
			data.altitudeAngle = input.altitudeAngle;
			data.azimuthAngle = input.azimuthAngle;
			data.radius = global::UnityEngine.Vector2.one * input.radius;
			data.radiusVariance = global::UnityEngine.Vector2.one * input.radiusVariance;
			return data;
		}

		protected void CopyFromTo(global::UnityEngine.EventSystems.PointerEventData from, global::UnityEngine.EventSystems.PointerEventData to)
		{
			to.position = from.position;
			to.delta = from.delta;
			to.scrollDelta = from.scrollDelta;
			to.pointerCurrentRaycast = from.pointerCurrentRaycast;
			to.pointerEnter = from.pointerEnter;
			to.pressure = from.pressure;
			to.tangentialPressure = from.tangentialPressure;
			to.altitudeAngle = from.altitudeAngle;
			to.azimuthAngle = from.azimuthAngle;
			to.twist = from.twist;
			to.radius = from.radius;
			to.radiusVariance = from.radiusVariance;
		}

		protected global::UnityEngine.EventSystems.PointerEventData.FramePressState StateForMouseButton(int buttonId)
		{
			bool mouseButtonDown = base.input.GetMouseButtonDown(buttonId);
			bool mouseButtonUp = base.input.GetMouseButtonUp(buttonId);
			if (mouseButtonDown && mouseButtonUp)
			{
				return global::UnityEngine.EventSystems.PointerEventData.FramePressState.PressedAndReleased;
			}
			if (mouseButtonDown)
			{
				return global::UnityEngine.EventSystems.PointerEventData.FramePressState.Pressed;
			}
			if (mouseButtonUp)
			{
				return global::UnityEngine.EventSystems.PointerEventData.FramePressState.Released;
			}
			return global::UnityEngine.EventSystems.PointerEventData.FramePressState.NotChanged;
		}

		protected virtual global::UnityEngine.EventSystems.PointerInputModule.MouseState GetMousePointerEventData()
		{
			return GetMousePointerEventData(0);
		}

		protected virtual global::UnityEngine.EventSystems.PointerInputModule.MouseState GetMousePointerEventData(int id)
		{
			global::UnityEngine.EventSystems.PointerEventData data;
			bool pointerData = GetPointerData(-1, out data, create: true);
			data.Reset();
			if (pointerData)
			{
				data.position = base.input.mousePosition;
			}
			global::UnityEngine.Vector2 mousePosition = base.input.mousePosition;
			if (global::UnityEngine.Cursor.lockState == global::UnityEngine.CursorLockMode.Locked)
			{
				data.position = new global::UnityEngine.Vector2(-1f, -1f);
				data.delta = global::UnityEngine.Vector2.zero;
			}
			else
			{
				data.delta = mousePosition - data.position;
				data.position = mousePosition;
			}
			data.scrollDelta = base.input.mouseScrollDelta;
			data.button = global::UnityEngine.EventSystems.PointerEventData.InputButton.Left;
			base.eventSystem.RaycastAll(data, m_RaycastResultCache);
			global::UnityEngine.EventSystems.RaycastResult pointerCurrentRaycast = global::UnityEngine.EventSystems.BaseInputModule.FindFirstRaycast(m_RaycastResultCache);
			data.pointerCurrentRaycast = pointerCurrentRaycast;
			m_RaycastResultCache.Clear();
			GetPointerData(-2, out var data2, create: true);
			data2.Reset();
			CopyFromTo(data, data2);
			data2.button = global::UnityEngine.EventSystems.PointerEventData.InputButton.Right;
			GetPointerData(-3, out var data3, create: true);
			data3.Reset();
			CopyFromTo(data, data3);
			data3.button = global::UnityEngine.EventSystems.PointerEventData.InputButton.Middle;
			m_MouseState.SetButtonState(global::UnityEngine.EventSystems.PointerEventData.InputButton.Left, StateForMouseButton(0), data);
			m_MouseState.SetButtonState(global::UnityEngine.EventSystems.PointerEventData.InputButton.Right, StateForMouseButton(1), data2);
			m_MouseState.SetButtonState(global::UnityEngine.EventSystems.PointerEventData.InputButton.Middle, StateForMouseButton(2), data3);
			return m_MouseState;
		}

		protected global::UnityEngine.EventSystems.PointerEventData GetLastPointerEventData(int id)
		{
			GetPointerData(id, out var data, create: false);
			return data;
		}

		private static bool ShouldStartDrag(global::UnityEngine.Vector2 pressPos, global::UnityEngine.Vector2 currentPos, float threshold, bool useDragThreshold)
		{
			if (!useDragThreshold)
			{
				return true;
			}
			return (pressPos - currentPos).sqrMagnitude >= threshold * threshold;
		}

		protected virtual void ProcessMove(global::UnityEngine.EventSystems.PointerEventData pointerEvent)
		{
			global::UnityEngine.GameObject newEnterTarget = ((global::UnityEngine.Cursor.lockState == global::UnityEngine.CursorLockMode.Locked) ? null : pointerEvent.pointerCurrentRaycast.gameObject);
			HandlePointerExitAndEnter(pointerEvent, newEnterTarget);
		}

		protected virtual void ProcessDrag(global::UnityEngine.EventSystems.PointerEventData pointerEvent)
		{
			if (!pointerEvent.IsPointerMoving() || global::UnityEngine.Cursor.lockState == global::UnityEngine.CursorLockMode.Locked || pointerEvent.pointerDrag == null)
			{
				return;
			}
			if (!pointerEvent.dragging && ShouldStartDrag(pointerEvent.pressPosition, pointerEvent.position, base.eventSystem.pixelDragThreshold, pointerEvent.useDragThreshold))
			{
				global::UnityEngine.EventSystems.ExecuteEvents.Execute(pointerEvent.pointerDrag, pointerEvent, global::UnityEngine.EventSystems.ExecuteEvents.beginDragHandler);
				pointerEvent.dragging = true;
			}
			if (pointerEvent.dragging)
			{
				if (pointerEvent.pointerPress != pointerEvent.pointerDrag)
				{
					global::UnityEngine.EventSystems.ExecuteEvents.Execute(pointerEvent.pointerPress, pointerEvent, global::UnityEngine.EventSystems.ExecuteEvents.pointerUpHandler);
					pointerEvent.eligibleForClick = false;
					pointerEvent.pointerPress = null;
					pointerEvent.rawPointerPress = null;
				}
				global::UnityEngine.EventSystems.ExecuteEvents.Execute(pointerEvent.pointerDrag, pointerEvent, global::UnityEngine.EventSystems.ExecuteEvents.dragHandler);
			}
		}

		public override bool IsPointerOverGameObject(int pointerId)
		{
			global::UnityEngine.EventSystems.PointerEventData lastPointerEventData = GetLastPointerEventData(pointerId);
			if (lastPointerEventData != null)
			{
				return lastPointerEventData.pointerEnter != null;
			}
			return false;
		}

		protected void ClearSelection()
		{
			global::UnityEngine.EventSystems.BaseEventData baseEventData = GetBaseEventData();
			foreach (global::UnityEngine.EventSystems.PointerEventData value in m_PointerData.Values)
			{
				HandlePointerExitAndEnter(value, null);
			}
			m_PointerData.Clear();
			base.eventSystem.SetSelectedGameObject(null, baseEventData);
		}

		public override string ToString()
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder("<b>Pointer Input Module of type: </b>" + GetType());
			stringBuilder.AppendLine();
			foreach (global::System.Collections.Generic.KeyValuePair<int, global::UnityEngine.EventSystems.PointerEventData> pointerDatum in m_PointerData)
			{
				if (pointerDatum.Value != null)
				{
					stringBuilder.AppendLine("<B>Pointer:</b> " + pointerDatum.Key);
					stringBuilder.AppendLine(pointerDatum.Value.ToString());
				}
			}
			return stringBuilder.ToString();
		}

		protected void DeselectIfSelectionChanged(global::UnityEngine.GameObject currentOverGo, global::UnityEngine.EventSystems.BaseEventData pointerEvent)
		{
			if (global::UnityEngine.EventSystems.ExecuteEvents.GetEventHandler<global::UnityEngine.EventSystems.ISelectHandler>(currentOverGo) != base.eventSystem.currentSelectedGameObject)
			{
				base.eventSystem.SetSelectedGameObject(null, pointerEvent);
			}
		}
	}
}
