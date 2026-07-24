namespace UnityEngine.UIElements
{
	[global::UnityEngine.AddComponentMenu("UI Toolkit/Panel Event Handler (UI Toolkit)")]
	public class PanelEventHandler : global::UnityEngine.EventSystems.UIBehaviour, global::UnityEngine.EventSystems.IPointerMoveHandler, global::UnityEngine.EventSystems.IEventSystemHandler, global::UnityEngine.EventSystems.IPointerUpHandler, global::UnityEngine.EventSystems.IPointerDownHandler, global::UnityEngine.EventSystems.ISubmitHandler, global::UnityEngine.EventSystems.ICancelHandler, global::UnityEngine.EventSystems.IMoveHandler, global::UnityEngine.EventSystems.IScrollHandler, global::UnityEngine.EventSystems.ISelectHandler, global::UnityEngine.EventSystems.IDeselectHandler, global::UnityEngine.EventSystems.IPointerExitHandler, global::UnityEngine.EventSystems.IPointerEnterHandler, global::UnityEngine.UIElements.IRuntimePanelComponent, global::UnityEngine.EventSystems.IPointerClickHandler
	{
		private enum PointerEventType
		{
			Default = 0,
			Down = 1,
			Up = 2
		}

		private class PointerEvent : global::UnityEngine.UIElements.IPointerEvent
		{
			public int pointerId { get; private set; }

			public string pointerType { get; private set; }

			public bool isPrimary { get; private set; }

			public int button { get; private set; }

			public int pressedButtons { get; private set; }

			public global::UnityEngine.Vector3 position { get; private set; }

			public global::UnityEngine.Vector3 localPosition { get; private set; }

			public global::UnityEngine.Vector3 deltaPosition { get; private set; }

			public float deltaTime { get; private set; }

			public int clickCount { get; private set; }

			public float pressure { get; private set; }

			public float tangentialPressure { get; private set; }

			public float altitudeAngle { get; private set; }

			public float azimuthAngle { get; private set; }

			public float twist { get; private set; }

			public global::UnityEngine.Vector2 tilt { get; private set; }

			public global::UnityEngine.PenStatus penStatus { get; private set; }

			public global::UnityEngine.Vector2 radius { get; private set; }

			public global::UnityEngine.Vector2 radiusVariance { get; private set; }

			public global::UnityEngine.EventModifiers modifiers { get; private set; }

			public bool shiftKey => (modifiers & global::UnityEngine.EventModifiers.Shift) != 0;

			public bool ctrlKey => (modifiers & global::UnityEngine.EventModifiers.Control) != 0;

			public bool commandKey => (modifiers & global::UnityEngine.EventModifiers.Command) != 0;

			public bool altKey => (modifiers & global::UnityEngine.EventModifiers.Alt) != 0;

			public bool actionKey
			{
				get
				{
					if (global::UnityEngine.Application.platform != global::UnityEngine.RuntimePlatform.OSXEditor && global::UnityEngine.Application.platform != global::UnityEngine.RuntimePlatform.OSXPlayer)
					{
						return ctrlKey;
					}
					return commandKey;
				}
			}

			public global::UnityEngine.Vector3 screenPosition { get; private set; }

			public global::UnityEngine.Vector3 screenDelta { get; private set; }

			public global::UnityEngine.Ray worldRay { get; private set; }

			public global::UnityEngine.UIElements.UIDocument document { get; private set; }

			public global::UnityEngine.UIElements.VisualElement elementTarget { get; private set; }

			public global::UnityEngine.UIElements.VisualElement elementUnderPointer { get; private set; }

			public void Read(global::UnityEngine.UIElements.PanelEventHandler self, global::UnityEngine.EventSystems.PointerEventData eventData, global::UnityEngine.UIElements.PanelEventHandler.PointerEventType eventType)
			{
				pointerId = self.eventSystem.currentInputModule.ConvertUIToolkitPointerId(eventData);
				pointerType = (InRange(pointerId, global::UnityEngine.UIElements.PointerId.touchPointerIdBase, global::UnityEngine.UIElements.PointerId.touchPointerCount) ? global::UnityEngine.UIElements.PointerType.touch : (InRange(pointerId, global::UnityEngine.UIElements.PointerId.penPointerIdBase, global::UnityEngine.UIElements.PointerId.penPointerCount) ? global::UnityEngine.UIElements.PointerType.pen : global::UnityEngine.UIElements.PointerType.mouse));
				isPrimary = pointerId == global::UnityEngine.UIElements.PointerId.mousePointerId || pointerId == global::UnityEngine.UIElements.PointerId.touchPointerIdBase || pointerId == global::UnityEngine.UIElements.PointerId.penPointerIdBase;
				int num = global::UnityEngine.Screen.height;
				global::UnityEngine.Vector3 relativeMousePositionForRaycast = global::UnityEngine.UI.MultipleDisplayUtilities.GetRelativeMousePositionForRaycast(eventData);
				int num2 = (int)relativeMousePositionForRaycast.z;
				if (global::UnityEngineInternal.DisplayInternal.IsASecondaryDisplayIndex(num2))
				{
					num = global::UnityEngine.Display.displays[num2].systemHeight;
				}
				global::UnityEngine.Vector2 delta = eventData.delta;
				relativeMousePositionForRaycast.y = (float)num - relativeMousePositionForRaycast.y;
				delta.y = 0f - delta.y;
				screenPosition = relativeMousePositionForRaycast;
				screenDelta = delta;
				deltaTime = 0f;
				pressure = eventData.pressure;
				tangentialPressure = eventData.tangentialPressure;
				altitudeAngle = eventData.altitudeAngle;
				azimuthAngle = eventData.azimuthAngle;
				twist = eventData.twist;
				tilt = eventData.tilt;
				penStatus = eventData.penStatus;
				radius = eventData.radius;
				radiusVariance = eventData.radiusVariance;
				modifiers = s_Modifiers;
				if (eventType == global::UnityEngine.UIElements.PanelEventHandler.PointerEventType.Default)
				{
					button = -1;
					clickCount = 0;
				}
				else
				{
					button = global::UnityEngine.Mathf.Max(0, (int)eventData.button);
					clickCount = eventData.clickCount;
					switch (eventType)
					{
					case global::UnityEngine.UIElements.PanelEventHandler.PointerEventType.Down:
						if (global::UnityEngine.Time.unscaledTime > self.m_LastClickTime + (float)global::UnityEngine.UIElements.ClickDetector.s_DoubleClickTime * 0.001f)
						{
							clickCount = 0;
						}
						clickCount++;
						global::UnityEngine.UIElements.PointerDeviceState.PressButton(pointerId, button);
						break;
					case global::UnityEngine.UIElements.PanelEventHandler.PointerEventType.Up:
						global::UnityEngine.UIElements.PointerDeviceState.ReleaseButton(pointerId, button);
						break;
					}
					clickCount = global::UnityEngine.Mathf.Max(1, clickCount);
				}
				pressedButtons = global::UnityEngine.UIElements.PointerDeviceState.GetPressedButtons(pointerId);
				global::UnityEngine.Vector3 origin = eventData.pointerCurrentRaycast.origin;
				worldRay = new global::UnityEngine.Ray(origin, eventData.pointerCurrentRaycast.worldPosition - origin);
				document = eventData.pointerCurrentRaycast.document;
				elementUnderPointer = eventData.pointerCurrentRaycast.element;
				static bool InRange(int i, int start, int count)
				{
					if (i >= start)
					{
						return i < start + count;
					}
					return false;
				}
			}

			public bool ComputeTarget(global::UnityEngine.UIElements.BaseRuntimePanel panel)
			{
				global::UnityEngine.Vector3 panelPosition;
				if (panel.isFlat)
				{
					panel.ScreenToPanel(screenPosition, screenDelta, out panelPosition, allowOutside: true);
					elementTarget = null;
				}
				else
				{
					if (document == null)
					{
						return false;
					}
					global::UnityEngine.UIElements.VisualElement visualElement = global::UnityEngine.UIElements.RuntimePanel.s_EventDispatcher.pointerState.GetCapturingElement(pointerId) as global::UnityEngine.UIElements.VisualElement;
					if (visualElement != null && visualElement.panel != panel)
					{
						return false;
					}
					elementTarget = visualElement ?? elementUnderPointer ?? document.rootVisualElement;
					panelPosition = GetPanelPosition(elementTarget, document, worldRay);
				}
				global::UnityEngine.Vector3 vector = (position = panelPosition);
				localPosition = vector;
				deltaPosition = global::UnityEngine.UIElements.PointerDeviceState.GetPointerDeltaPosition(pointerId, global::UnityEngine.UIElements.ContextType.Player, position);
				return true;
			}

			private global::UnityEngine.Vector3 GetPanelPosition(global::UnityEngine.UIElements.VisualElement pickedElement, global::UnityEngine.UIElements.UIDocument document, global::UnityEngine.Ray worldRay)
			{
				global::UnityEngine.Ray ray = document.transform.worldToLocalMatrix.TransformRay(worldRay);
				pickedElement.IntersectWorldRay(ray, out var distance, out var _);
				return ray.origin + ray.direction * distance;
			}
		}

		private global::UnityEngine.UIElements.BaseRuntimePanel m_Panel;

		private readonly global::UnityEngine.UIElements.PanelEventHandler.PointerEvent m_PointerEvent = new global::UnityEngine.UIElements.PanelEventHandler.PointerEvent();

		private readonly global::System.Collections.Generic.List<global::UnityEngine.EventSystems.PointerEventData> m_ContainedPointers = new global::System.Collections.Generic.List<global::UnityEngine.EventSystems.PointerEventData>();

		private float m_LastClickTime;

		private bool m_Selecting;

		private global::UnityEngine.Event m_Event = new global::UnityEngine.Event();

		private static global::UnityEngine.EventModifiers s_Modifiers;

		public global::UnityEngine.UIElements.IPanel panel
		{
			get
			{
				return m_Panel;
			}
			set
			{
				global::UnityEngine.UIElements.BaseRuntimePanel baseRuntimePanel = (global::UnityEngine.UIElements.BaseRuntimePanel)value;
				if (m_Panel != baseRuntimePanel)
				{
					UnregisterCallbacks();
					m_Panel = baseRuntimePanel;
					RegisterCallbacks();
				}
			}
		}

		private global::UnityEngine.GameObject selectableGameObject => m_Panel?.selectableGameObject;

		private global::UnityEngine.EventSystems.EventSystem eventSystem => global::UnityEngine.UIElements.UIElementsRuntimeUtility.activeEventSystem as global::UnityEngine.EventSystems.EventSystem;

		private bool isCurrentFocusedPanel
		{
			get
			{
				if (m_Panel != null && eventSystem != null)
				{
					return eventSystem.currentSelectedGameObject == selectableGameObject;
				}
				return false;
			}
		}

		private global::UnityEngine.UIElements.Focusable currentFocusedElement => m_Panel?.focusController.GetLeafFocusedElement();

		protected override void OnEnable()
		{
			base.OnEnable();
			RegisterCallbacks();
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			UnregisterCallbacks();
		}

		private void RegisterCallbacks()
		{
			if (m_Panel != null)
			{
				m_Panel.destroyed += OnPanelDestroyed;
				m_Panel.visualTree.RegisterCallback<global::UnityEngine.UIElements.FocusEvent>(OnElementFocus, global::UnityEngine.UIElements.TrickleDown.TrickleDown);
				m_Panel.visualTree.RegisterCallback<global::UnityEngine.UIElements.BlurEvent>(OnElementBlur, global::UnityEngine.UIElements.TrickleDown.TrickleDown);
			}
		}

		private void UnregisterCallbacks()
		{
			if (m_Panel != null)
			{
				m_Panel.destroyed -= OnPanelDestroyed;
				m_Panel.visualTree.UnregisterCallback<global::UnityEngine.UIElements.FocusEvent>(OnElementFocus, global::UnityEngine.UIElements.TrickleDown.TrickleDown);
				m_Panel.visualTree.UnregisterCallback<global::UnityEngine.UIElements.BlurEvent>(OnElementBlur, global::UnityEngine.UIElements.TrickleDown.TrickleDown);
			}
		}

		private void OnPanelDestroyed()
		{
			panel = null;
		}

		private void OnElementFocus(global::UnityEngine.UIElements.FocusEvent e)
		{
			if (!m_Selecting && eventSystem != null)
			{
				eventSystem.SetSelectedGameObject(selectableGameObject);
			}
		}

		private void OnElementBlur(global::UnityEngine.UIElements.BlurEvent e)
		{
		}

		public void OnSelect(global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			m_Selecting = true;
			try
			{
				m_Panel?.Focus();
			}
			finally
			{
				m_Selecting = false;
			}
		}

		public void OnDeselect(global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			m_Panel?.Blur();
		}

		public void OnPointerMove(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (!ReadPointerData(m_PointerEvent, eventData))
			{
				return;
			}
			using global::UnityEngine.UIElements.PointerMoveEvent e = global::UnityEngine.UIElements.PointerEventBase<global::UnityEngine.UIElements.PointerMoveEvent>.GetPooled(m_PointerEvent);
			UpdatePointerEventTarget(e, m_PointerEvent);
			SendEvent(e, eventData);
		}

		public void OnPointerUp(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (!ReadPointerData(m_PointerEvent, eventData, global::UnityEngine.UIElements.PanelEventHandler.PointerEventType.Up))
			{
				return;
			}
			using global::UnityEngine.UIElements.PointerUpEvent pointerUpEvent = global::UnityEngine.UIElements.PointerEventBase<global::UnityEngine.UIElements.PointerUpEvent>.GetPooled(m_PointerEvent);
			UpdatePointerEventTarget(pointerUpEvent, m_PointerEvent);
			SendEvent(pointerUpEvent, eventData);
			if (pointerUpEvent.pressedButtons == 0)
			{
				global::UnityEngine.UIElements.PointerDeviceState.SetElementWithSoftPointerCapture(pointerUpEvent.pointerId, null, null);
			}
		}

		public void OnPointerDown(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (!ReadPointerData(m_PointerEvent, eventData, global::UnityEngine.UIElements.PanelEventHandler.PointerEventType.Down))
			{
				return;
			}
			global::UnityEngine.UIElements.Focusable target = currentFocusedElement ?? m_Panel.visualTree;
			ProcessImguiEvents(target);
			if (eventSystem != null)
			{
				eventSystem.SetSelectedGameObject(selectableGameObject);
			}
			using global::UnityEngine.UIElements.PointerDownEvent pointerDownEvent = global::UnityEngine.UIElements.PointerEventBase<global::UnityEngine.UIElements.PointerDownEvent>.GetPooled(m_PointerEvent);
			UpdatePointerEventTarget(pointerDownEvent, m_PointerEvent);
			SendEvent(pointerDownEvent, eventData);
			global::UnityEngine.UIElements.PointerDeviceState.SetElementWithSoftPointerCapture(pointerDownEvent.pointerId, pointerDownEvent.elementTarget, eventData.pressEventCamera);
		}

		public void OnPointerExit(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			m_ContainedPointers.Remove(eventData);
			if (!ReadPointerData(m_PointerEvent, eventData))
			{
				if (m_Panel != null && !m_Panel.isFlat)
				{
					m_Panel.PointerLeavesPanel(m_PointerEvent.pointerId);
				}
				return;
			}
			if (eventData.pointerCurrentRaycast.gameObject == base.gameObject && eventData.pointerPressRaycast.gameObject != base.gameObject && m_PointerEvent.pointerId != global::UnityEngine.UIElements.PointerId.mousePointerId)
			{
				using global::UnityEngine.UIElements.PointerCancelEvent pointerCancelEvent = global::UnityEngine.UIElements.PointerEventBase<global::UnityEngine.UIElements.PointerCancelEvent>.GetPooled(m_PointerEvent);
				UpdatePointerEventTarget(pointerCancelEvent, m_PointerEvent);
				SendEvent(pointerCancelEvent, eventData);
				if (pointerCancelEvent.pressedButtons == 0)
				{
					global::UnityEngine.UIElements.PointerDeviceState.SetElementWithSoftPointerCapture(pointerCancelEvent.pointerId, null, null);
				}
			}
			m_Panel.PointerLeavesPanel(m_PointerEvent.pointerId);
		}

		public void OnPointerEnter(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (ReadPointerData(m_PointerEvent, eventData))
			{
				m_ContainedPointers.Add(eventData);
				m_Panel.PointerEntersPanel(m_PointerEvent.pointerId, m_PointerEvent.position);
			}
		}

		public void OnPointerClick(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			m_LastClickTime = global::UnityEngine.Time.unscaledTime;
		}

		public void OnSubmit(global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			if (m_Panel == null)
			{
				return;
			}
			global::UnityEngine.UIElements.Focusable target = currentFocusedElement ?? m_Panel.visualTree;
			ProcessImguiEvents(target);
			using global::UnityEngine.UIElements.NavigationSubmitEvent navigationSubmitEvent = global::UnityEngine.UIElements.NavigationEventBase<global::UnityEngine.UIElements.NavigationSubmitEvent>.GetPooled(GetDeviceType(eventData), s_Modifiers);
			navigationSubmitEvent.target = target;
			SendEvent(navigationSubmitEvent, eventData);
		}

		public void OnCancel(global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			if (m_Panel == null)
			{
				return;
			}
			global::UnityEngine.UIElements.Focusable target = currentFocusedElement ?? m_Panel.visualTree;
			ProcessImguiEvents(target);
			using global::UnityEngine.UIElements.NavigationCancelEvent navigationCancelEvent = global::UnityEngine.UIElements.NavigationEventBase<global::UnityEngine.UIElements.NavigationCancelEvent>.GetPooled(GetDeviceType(eventData), s_Modifiers);
			navigationCancelEvent.target = target;
			SendEvent(navigationCancelEvent, eventData);
		}

		public void OnMove(global::UnityEngine.EventSystems.AxisEventData eventData)
		{
			if (m_Panel == null)
			{
				return;
			}
			global::UnityEngine.UIElements.Focusable target = currentFocusedElement ?? m_Panel.visualTree;
			ProcessImguiEvents(target);
			using global::UnityEngine.UIElements.NavigationMoveEvent navigationMoveEvent = global::UnityEngine.UIElements.NavigationMoveEvent.GetPooled(eventData.moveVector, GetDeviceType(eventData), s_Modifiers);
			navigationMoveEvent.target = target;
			SendEvent(navigationMoveEvent, eventData);
		}

		public void OnScroll(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (!ReadPointerData(m_PointerEvent, eventData))
			{
				return;
			}
			global::UnityEngine.Vector2 scrollDelta = eventData.scrollDelta;
			global::UnityEngine.Vector2 vector = eventSystem.currentInputModule.ConvertPointerEventScrollDeltaToTicks(scrollDelta) * 3f;
			vector.y = 0f - vector.y;
			using global::UnityEngine.UIElements.WheelEvent e = global::UnityEngine.UIElements.WheelEvent.GetPooled(vector, m_PointerEvent);
			SendEvent(e, eventData);
		}

		private void SendEvent(global::UnityEngine.UIElements.EventBase e, global::UnityEngine.EventSystems.BaseEventData sourceEventData)
		{
			m_Panel.SendEvent(e);
			if (e.isPropagationStopped)
			{
				sourceEventData.Use();
			}
		}

		private void SendEvent(global::UnityEngine.UIElements.EventBase e, global::UnityEngine.Event sourceEvent)
		{
			m_Panel.SendEvent(e);
		}

		public void Update()
		{
			if (isCurrentFocusedPanel)
			{
				ProcessImguiEvents(currentFocusedElement ?? m_Panel.visualTree);
			}
			UpdateWorldSpacePointers();
		}

		private void LateUpdate()
		{
			ProcessImguiEvents(null);
		}

		private void ProcessImguiEvents(global::UnityEngine.UIElements.Focusable target)
		{
			bool flag = true;
			while (global::UnityEngine.Event.PopEvent(m_Event))
			{
				if (m_Event.type == global::UnityEngine.EventType.Ignore || m_Event.type == global::UnityEngine.EventType.Repaint || m_Event.type == global::UnityEngine.EventType.Layout)
				{
					continue;
				}
				s_Modifiers = (flag ? m_Event.modifiers : (s_Modifiers | m_Event.modifiers));
				flag = false;
				if (target != null)
				{
					ProcessKeyboardEvent(m_Event, target);
					if (eventSystem.sendNavigationEvents)
					{
						ProcessTabEvent(m_Event, target);
					}
				}
			}
		}

		private void ProcessKeyboardEvent(global::UnityEngine.Event e, global::UnityEngine.UIElements.Focusable target)
		{
			if (e.type == global::UnityEngine.EventType.KeyUp)
			{
				SendKeyUpEvent(e, target);
			}
			else if (e.type == global::UnityEngine.EventType.KeyDown)
			{
				SendKeyDownEvent(e, target);
			}
		}

		private void ProcessTabEvent(global::UnityEngine.Event e, global::UnityEngine.UIElements.Focusable target)
		{
			if (e.ShouldSendNavigationMoveEventRuntime())
			{
				SendTabEvent(e, e.shift ? global::UnityEngine.UIElements.NavigationMoveEvent.Direction.Previous : global::UnityEngine.UIElements.NavigationMoveEvent.Direction.Next, target);
			}
		}

		private void SendTabEvent(global::UnityEngine.Event e, global::UnityEngine.UIElements.NavigationMoveEvent.Direction direction, global::UnityEngine.UIElements.Focusable target)
		{
			using global::UnityEngine.UIElements.NavigationMoveEvent navigationMoveEvent = global::UnityEngine.UIElements.NavigationMoveEvent.GetPooled(direction, s_Modifiers);
			navigationMoveEvent.target = target;
			SendEvent(navigationMoveEvent, e);
		}

		private void SendKeyUpEvent(global::UnityEngine.Event e, global::UnityEngine.UIElements.Focusable target)
		{
			using global::UnityEngine.UIElements.KeyUpEvent keyUpEvent = (global::UnityEngine.UIElements.KeyUpEvent)global::UnityEngine.UIElements.UIElementsRuntimeUtility.CreateEvent(e);
			keyUpEvent.target = target;
			SendEvent(keyUpEvent, e);
		}

		private void SendKeyDownEvent(global::UnityEngine.Event e, global::UnityEngine.UIElements.Focusable target)
		{
			using global::UnityEngine.UIElements.KeyDownEvent keyDownEvent = (global::UnityEngine.UIElements.KeyDownEvent)global::UnityEngine.UIElements.UIElementsRuntimeUtility.CreateEvent(e);
			keyDownEvent.target = target;
			SendEvent(keyDownEvent, e);
		}

		private bool ReadPointerData(global::UnityEngine.UIElements.PanelEventHandler.PointerEvent pe, global::UnityEngine.EventSystems.PointerEventData eventData, global::UnityEngine.UIElements.PanelEventHandler.PointerEventType eventType = global::UnityEngine.UIElements.PanelEventHandler.PointerEventType.Default)
		{
			if (m_Panel == null || eventSystem == null || eventSystem.currentInputModule == null)
			{
				return false;
			}
			pe.Read(this, eventData, eventType);
			if (!pe.ComputeTarget(m_Panel))
			{
				return false;
			}
			return true;
		}

		private void UpdatePointerEventTarget<TPointerEvent>(TPointerEvent e, global::UnityEngine.UIElements.PanelEventHandler.PointerEvent eventData) where TPointerEvent : global::UnityEngine.UIElements.PointerEventBase<TPointerEvent>, new()
		{
			e.target = eventData.elementTarget;
			if (!m_Panel.isFlat)
			{
				m_Panel.SetTopElementUnderPointer(eventData.pointerId, eventData.elementUnderPointer, e);
			}
		}

		private global::UnityEngine.UIElements.NavigationDeviceType GetDeviceType(global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			if (eventSystem == null || eventSystem.currentInputModule == null)
			{
				return global::UnityEngine.UIElements.NavigationDeviceType.Unknown;
			}
			return (global::UnityEngine.UIElements.NavigationDeviceType)eventSystem.currentInputModule.GetNavigationEventDeviceType(eventData);
		}

		private void UpdateWorldSpacePointers()
		{
			if (m_Panel == null || m_Panel.isFlat || eventSystem == null || eventSystem.currentInputModule == null)
			{
				return;
			}
			foreach (global::UnityEngine.EventSystems.PointerEventData containedPointer in m_ContainedPointers)
			{
				if (ReadPointerData(m_PointerEvent, containedPointer))
				{
					m_Panel.SetTopElementUnderPointer(m_PointerEvent.pointerId, m_PointerEvent.elementUnderPointer, m_PointerEvent.position);
					m_Panel.CommitElementUnderPointers();
				}
			}
		}
	}
}
