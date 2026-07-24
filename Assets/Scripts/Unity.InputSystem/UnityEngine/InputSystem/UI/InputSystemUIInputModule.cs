namespace UnityEngine.InputSystem.UI
{
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.inputsystem@1.18/manual/UISupport.html#setting-up-ui-input")]
	public class InputSystemUIInputModule : global::UnityEngine.EventSystems.BaseInputModule
	{
		private struct InputActionReferenceState
		{
			public int refCount;

			public bool enabledByInputModule;
		}

		public enum CursorLockBehavior
		{
			OutsideScreen = 0,
			ScreenCenter = 1
		}

		private const float kClickSpeed = 0.3f;

		[global::UnityEngine.Serialization.FormerlySerializedAs("m_RepeatDelay")]
		[global::UnityEngine.Tooltip("The Initial delay (in seconds) between an initial move action and a repeated move action.")]
		[global::UnityEngine.SerializeField]
		private float m_MoveRepeatDelay = 0.5f;

		[global::UnityEngine.Serialization.FormerlySerializedAs("m_RepeatRate")]
		[global::UnityEngine.Tooltip("The speed (in seconds) that the move action repeats itself once repeating (max 1 per frame).")]
		[global::UnityEngine.SerializeField]
		private float m_MoveRepeatRate = 0.1f;

		[global::UnityEngine.Tooltip("Scales the Eventsystem.DragThreshold, for tracked devices, to make selection easier.")]
		private float m_TrackedDeviceDragThresholdMultiplier = 2f;

		[global::UnityEngine.Tooltip("Transform representing the real world origin for tracking devices. When using the XR Interaction Toolkit, this should be pointing to the XR Rig's Transform.")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Transform m_XRTrackingOrigin;

		private static global::UnityEngine.InputSystem.DefaultInputActions defaultActions;

		private const float kSmallestScrollDeltaPerTick = 1E-05f;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.InputSystem.InputActionAsset m_ActionsAsset;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.InputSystem.InputActionReference m_PointAction;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.InputSystem.InputActionReference m_MoveAction;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.InputSystem.InputActionReference m_SubmitAction;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.InputSystem.InputActionReference m_CancelAction;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.InputSystem.InputActionReference m_LeftClickAction;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.InputSystem.InputActionReference m_MiddleClickAction;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.InputSystem.InputActionReference m_RightClickAction;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.InputSystem.InputActionReference m_ScrollWheelAction;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.InputSystem.InputActionReference m_TrackedDevicePositionAction;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.InputSystem.InputActionReference m_TrackedDeviceOrientationAction;

		[global::UnityEngine.SerializeField]
		private bool m_DeselectOnBackgroundClick = true;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.InputSystem.UI.UIPointerBehavior m_PointerBehavior;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		internal global::UnityEngine.InputSystem.UI.InputSystemUIInputModule.CursorLockBehavior m_CursorLockBehavior;

		[global::UnityEngine.SerializeField]
		private float m_ScrollDeltaPerTick = 6f;

		private static global::System.Collections.Generic.Dictionary<global::UnityEngine.InputSystem.InputAction, global::UnityEngine.InputSystem.UI.InputSystemUIInputModule.InputActionReferenceState> s_InputActionReferenceCounts = new global::System.Collections.Generic.Dictionary<global::UnityEngine.InputSystem.InputAction, global::UnityEngine.InputSystem.UI.InputSystemUIInputModule.InputActionReferenceState>();

		[global::System.NonSerialized]
		private bool m_ActionsHooked;

		[global::System.NonSerialized]
		private bool m_NeedToPurgeStalePointers;

		private global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> m_OnPointDelegate;

		private global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> m_OnMoveDelegate;

		private global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> m_OnSubmitCancelDelegate;

		private global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> m_OnLeftClickDelegate;

		private global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> m_OnRightClickDelegate;

		private global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> m_OnMiddleClickDelegate;

		private global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> m_OnScrollWheelDelegate;

		private global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> m_OnTrackedDevicePositionDelegate;

		private global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> m_OnTrackedDeviceOrientationDelegate;

		private global::System.Action<object> m_OnControlsChangedDelegate;

		[global::System.NonSerialized]
		private int m_CurrentPointerId = -1;

		[global::System.NonSerialized]
		private int m_CurrentPointerIndex = -1;

		[global::System.NonSerialized]
		internal global::UnityEngine.InputSystem.UI.UIPointerType m_CurrentPointerType;

		internal global::UnityEngine.InputSystem.Utilities.InlinedArray<int> m_PointerIds;

		internal global::UnityEngine.InputSystem.Utilities.InlinedArray<global::UnityEngine.InputSystem.UI.PointerModel> m_PointerStates;

		private global::UnityEngine.InputSystem.UI.NavigationModel m_NavigationState;

		private global::UnityEngine.InputSystem.UI.SubmitCancelModel m_SubmitCancelState;

		[global::System.NonSerialized]
		private global::UnityEngine.GameObject m_LocalMultiPlayerRoot;

		public bool deselectOnBackgroundClick
		{
			get
			{
				return m_DeselectOnBackgroundClick;
			}
			set
			{
				m_DeselectOnBackgroundClick = value;
			}
		}

		public global::UnityEngine.InputSystem.UI.UIPointerBehavior pointerBehavior
		{
			get
			{
				return m_PointerBehavior;
			}
			set
			{
				m_PointerBehavior = value;
			}
		}

		public global::UnityEngine.InputSystem.UI.InputSystemUIInputModule.CursorLockBehavior cursorLockBehavior
		{
			get
			{
				return m_CursorLockBehavior;
			}
			set
			{
				m_CursorLockBehavior = value;
			}
		}

		internal global::UnityEngine.GameObject localMultiPlayerRoot
		{
			get
			{
				return m_LocalMultiPlayerRoot;
			}
			set
			{
				m_LocalMultiPlayerRoot = value;
			}
		}

		public float scrollDeltaPerTick
		{
			get
			{
				return m_ScrollDeltaPerTick;
			}
			set
			{
				m_ScrollDeltaPerTick = value;
			}
		}

		public float moveRepeatDelay
		{
			get
			{
				return m_MoveRepeatDelay;
			}
			set
			{
				m_MoveRepeatDelay = value;
			}
		}

		public float moveRepeatRate
		{
			get
			{
				return m_MoveRepeatRate;
			}
			set
			{
				m_MoveRepeatRate = value;
			}
		}

		private bool explictlyIgnoreFocus => global::UnityEngine.InputSystem.InputSystem.settings.backgroundBehavior == global::UnityEngine.InputSystem.InputSettings.BackgroundBehavior.IgnoreFocus;

		private bool shouldIgnoreFocus
		{
			get
			{
				if (!explictlyIgnoreFocus)
				{
					return global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_Instance.runInBackground;
				}
				return true;
			}
		}

		[global::System.Obsolete("'repeatRate' has been obsoleted; use 'moveRepeatRate' instead. (UnityUpgradable) -> moveRepeatRate", false)]
		public float repeatRate
		{
			get
			{
				return moveRepeatRate;
			}
			set
			{
				moveRepeatRate = value;
			}
		}

		[global::System.Obsolete("'repeatDelay' has been obsoleted; use 'moveRepeatDelay' instead. (UnityUpgradable) -> moveRepeatDelay", false)]
		public float repeatDelay
		{
			get
			{
				return moveRepeatDelay;
			}
			set
			{
				moveRepeatDelay = value;
			}
		}

		public global::UnityEngine.Transform xrTrackingOrigin
		{
			get
			{
				return m_XRTrackingOrigin;
			}
			set
			{
				m_XRTrackingOrigin = value;
			}
		}

		public float trackedDeviceDragThresholdMultiplier
		{
			get
			{
				return m_TrackedDeviceDragThresholdMultiplier;
			}
			set
			{
				m_TrackedDeviceDragThresholdMultiplier = value;
			}
		}

		public global::UnityEngine.InputSystem.InputActionReference point
		{
			get
			{
				return m_PointAction;
			}
			set
			{
				SwapAction(ref m_PointAction, value, m_ActionsHooked, m_OnPointDelegate);
			}
		}

		public global::UnityEngine.InputSystem.InputActionReference scrollWheel
		{
			get
			{
				return m_ScrollWheelAction;
			}
			set
			{
				SwapAction(ref m_ScrollWheelAction, value, m_ActionsHooked, m_OnScrollWheelDelegate);
			}
		}

		public global::UnityEngine.InputSystem.InputActionReference leftClick
		{
			get
			{
				return m_LeftClickAction;
			}
			set
			{
				SwapAction(ref m_LeftClickAction, value, m_ActionsHooked, m_OnLeftClickDelegate);
			}
		}

		public global::UnityEngine.InputSystem.InputActionReference middleClick
		{
			get
			{
				return m_MiddleClickAction;
			}
			set
			{
				SwapAction(ref m_MiddleClickAction, value, m_ActionsHooked, m_OnMiddleClickDelegate);
			}
		}

		public global::UnityEngine.InputSystem.InputActionReference rightClick
		{
			get
			{
				return m_RightClickAction;
			}
			set
			{
				SwapAction(ref m_RightClickAction, value, m_ActionsHooked, m_OnRightClickDelegate);
			}
		}

		public global::UnityEngine.InputSystem.InputActionReference move
		{
			get
			{
				return m_MoveAction;
			}
			set
			{
				SwapAction(ref m_MoveAction, value, m_ActionsHooked, m_OnMoveDelegate);
			}
		}

		public global::UnityEngine.InputSystem.InputActionReference submit
		{
			get
			{
				return m_SubmitAction;
			}
			set
			{
				SwapAction(ref m_SubmitAction, value, m_ActionsHooked, m_OnSubmitCancelDelegate);
			}
		}

		public global::UnityEngine.InputSystem.InputActionReference cancel
		{
			get
			{
				return m_CancelAction;
			}
			set
			{
				SwapAction(ref m_CancelAction, value, m_ActionsHooked, m_OnSubmitCancelDelegate);
			}
		}

		public global::UnityEngine.InputSystem.InputActionReference trackedDeviceOrientation
		{
			get
			{
				return m_TrackedDeviceOrientationAction;
			}
			set
			{
				SwapAction(ref m_TrackedDeviceOrientationAction, value, m_ActionsHooked, m_OnTrackedDeviceOrientationDelegate);
			}
		}

		public global::UnityEngine.InputSystem.InputActionReference trackedDevicePosition
		{
			get
			{
				return m_TrackedDevicePositionAction;
			}
			set
			{
				SwapAction(ref m_TrackedDevicePositionAction, value, m_ActionsHooked, m_OnTrackedDevicePositionDelegate);
			}
		}

		[global::System.Obsolete("'trackedDeviceSelect' has been obsoleted; use 'leftClick' instead.", true)]
		public global::UnityEngine.InputSystem.InputActionReference trackedDeviceSelect
		{
			get
			{
				throw new global::System.InvalidOperationException();
			}
			set
			{
				throw new global::System.InvalidOperationException();
			}
		}

		public global::UnityEngine.InputSystem.InputActionAsset actionsAsset
		{
			get
			{
				return m_ActionsAsset;
			}
			set
			{
				if (value != m_ActionsAsset)
				{
					UnhookActions();
					m_ActionsAsset = value;
					point = UpdateReferenceForNewAsset(point);
					move = UpdateReferenceForNewAsset(move);
					leftClick = UpdateReferenceForNewAsset(leftClick);
					rightClick = UpdateReferenceForNewAsset(rightClick);
					middleClick = UpdateReferenceForNewAsset(middleClick);
					scrollWheel = UpdateReferenceForNewAsset(scrollWheel);
					submit = UpdateReferenceForNewAsset(submit);
					cancel = UpdateReferenceForNewAsset(cancel);
					trackedDeviceOrientation = UpdateReferenceForNewAsset(trackedDeviceOrientation);
					trackedDevicePosition = UpdateReferenceForNewAsset(trackedDevicePosition);
					HookActions();
				}
			}
		}

		internal new bool sendPointerHoverToParent
		{
			get
			{
				return base.sendPointerHoverToParent;
			}
			set
			{
				base.sendPointerHoverToParent = value;
			}
		}

		public override void ActivateModule()
		{
			base.ActivateModule();
			global::UnityEngine.GameObject gameObject = base.eventSystem.currentSelectedGameObject;
			if (gameObject == null)
			{
				gameObject = base.eventSystem.firstSelectedGameObject;
			}
			base.eventSystem.SetSelectedGameObject(gameObject, GetBaseEventData());
		}

		public override bool IsPointerOverGameObject(int pointerOrTouchId)
		{
			if (global::UnityEngine.InputSystem.InputSystem.isProcessingEvents)
			{
				global::UnityEngine.Debug.LogWarning("Calling IsPointerOverGameObject() from within event processing (such as from InputAction callbacks) will not work as expected; it will query UI state from the last frame");
			}
			int num = -1;
			if (pointerOrTouchId < 0)
			{
				if (m_CurrentPointerId != -1)
				{
					num = m_CurrentPointerIndex;
				}
				else if (m_PointerStates.length > 0)
				{
					num = 0;
				}
			}
			else
			{
				num = GetPointerStateIndexFor(pointerOrTouchId);
			}
			if (num == -1)
			{
				return false;
			}
			return m_PointerStates[num].eventData.pointerEnter != null;
		}

		public global::UnityEngine.EventSystems.RaycastResult GetLastRaycastResult(int pointerOrTouchId)
		{
			int pointerStateIndexFor = GetPointerStateIndexFor(pointerOrTouchId);
			if (pointerStateIndexFor == -1)
			{
				return default(global::UnityEngine.EventSystems.RaycastResult);
			}
			return m_PointerStates[pointerStateIndexFor].eventData.pointerCurrentRaycast;
		}

		private global::UnityEngine.EventSystems.RaycastResult PerformRaycast(global::UnityEngine.InputSystem.UI.ExtendedPointerEventData eventData)
		{
			if (eventData == null)
			{
				throw new global::System.ArgumentNullException("eventData");
			}
			if (eventData.pointerType == global::UnityEngine.InputSystem.UI.UIPointerType.Tracked && global::UnityEngine.InputSystem.UI.TrackedDeviceRaycaster.s_Instances.length > 0)
			{
				for (int i = 0; i < global::UnityEngine.InputSystem.UI.TrackedDeviceRaycaster.s_Instances.length; i++)
				{
					global::UnityEngine.InputSystem.UI.TrackedDeviceRaycaster trackedDeviceRaycaster = global::UnityEngine.InputSystem.UI.TrackedDeviceRaycaster.s_Instances[i];
					m_RaycastResultCache.Clear();
					trackedDeviceRaycaster.PerformRaycast(eventData, m_RaycastResultCache);
					if (m_RaycastResultCache.Count > 0)
					{
						global::UnityEngine.EventSystems.RaycastResult result = m_RaycastResultCache[0];
						m_RaycastResultCache.Clear();
						return result;
					}
				}
				return default(global::UnityEngine.EventSystems.RaycastResult);
			}
			base.eventSystem.RaycastAll(eventData, m_RaycastResultCache);
			global::UnityEngine.EventSystems.RaycastResult result2 = global::UnityEngine.EventSystems.BaseInputModule.FindFirstRaycast(m_RaycastResultCache);
			m_RaycastResultCache.Clear();
			return result2;
		}

		private void ProcessPointer(ref global::UnityEngine.InputSystem.UI.PointerModel state)
		{
			global::UnityEngine.InputSystem.UI.ExtendedPointerEventData eventData = state.eventData;
			global::UnityEngine.InputSystem.UI.UIPointerType pointerType = eventData.pointerType;
			if (pointerType == global::UnityEngine.InputSystem.UI.UIPointerType.MouseOrPen && global::UnityEngine.Cursor.lockState == global::UnityEngine.CursorLockMode.Locked)
			{
				eventData.position = ((m_CursorLockBehavior == global::UnityEngine.InputSystem.UI.InputSystemUIInputModule.CursorLockBehavior.OutsideScreen) ? new global::UnityEngine.Vector2(-1f, -1f) : new global::UnityEngine.Vector2((float)global::UnityEngine.Screen.width / 2f, (float)global::UnityEngine.Screen.height / 2f));
				eventData.delta = default(global::UnityEngine.Vector2);
			}
			else if (pointerType == global::UnityEngine.InputSystem.UI.UIPointerType.Tracked)
			{
				global::UnityEngine.Vector3 position = state.worldPosition;
				global::UnityEngine.Quaternion quaternion = state.worldOrientation;
				if (m_XRTrackingOrigin != null)
				{
					position = m_XRTrackingOrigin.TransformPoint(position);
					quaternion = m_XRTrackingOrigin.rotation * quaternion;
				}
				eventData.trackedDeviceOrientation = quaternion;
				eventData.trackedDevicePosition = position;
			}
			else
			{
				eventData.delta = state.screenPosition - eventData.position;
				eventData.position = state.screenPosition;
			}
			eventData.Reset();
			eventData.pointerCurrentRaycast = PerformRaycast(eventData);
			if (pointerType == global::UnityEngine.InputSystem.UI.UIPointerType.Tracked && eventData.pointerCurrentRaycast.isValid)
			{
				global::UnityEngine.Vector2 screenPosition = eventData.pointerCurrentRaycast.screenPosition;
				eventData.delta = screenPosition - eventData.position;
				eventData.position = eventData.pointerCurrentRaycast.screenPosition;
			}
			eventData.button = global::UnityEngine.EventSystems.PointerEventData.InputButton.Left;
			state.leftButton.CopyPressStateTo(eventData);
			ProcessPointerMovement(ref state, eventData);
			if (state.changedThisFrame || (!(xrTrackingOrigin == null) && state.pointerType == global::UnityEngine.InputSystem.UI.UIPointerType.Tracked))
			{
				ProcessPointerButton(ref state.leftButton, eventData);
				ProcessPointerButtonDrag(ref state.leftButton, eventData);
				ProcessPointerScroll(ref state, eventData);
				eventData.button = global::UnityEngine.EventSystems.PointerEventData.InputButton.Right;
				state.rightButton.CopyPressStateTo(eventData);
				ProcessPointerButton(ref state.rightButton, eventData);
				ProcessPointerButtonDrag(ref state.rightButton, eventData);
				eventData.button = global::UnityEngine.EventSystems.PointerEventData.InputButton.Middle;
				state.middleButton.CopyPressStateTo(eventData);
				ProcessPointerButton(ref state.middleButton, eventData);
				ProcessPointerButtonDrag(ref state.middleButton, eventData);
			}
		}

		private bool PointerShouldIgnoreTransform(global::UnityEngine.Transform t)
		{
			if (base.eventSystem is global::UnityEngine.InputSystem.UI.MultiplayerEventSystem multiplayerEventSystem && multiplayerEventSystem.playerRoot != null && !t.IsChildOf(multiplayerEventSystem.playerRoot.transform))
			{
				return true;
			}
			return false;
		}

		private void ProcessPointerMovement(ref global::UnityEngine.InputSystem.UI.PointerModel pointer, global::UnityEngine.InputSystem.UI.ExtendedPointerEventData eventData)
		{
			global::UnityEngine.GameObject currentPointerTarget = ((eventData.pointerType == global::UnityEngine.InputSystem.UI.UIPointerType.Touch && !pointer.leftButton.isPressed && !pointer.leftButton.wasReleasedThisFrame) ? null : eventData.pointerCurrentRaycast.gameObject);
			ProcessPointerMovement(eventData, currentPointerTarget);
		}

		private void ProcessPointerMovement(global::UnityEngine.InputSystem.UI.ExtendedPointerEventData eventData, global::UnityEngine.GameObject currentPointerTarget)
		{
			bool flag = eventData.IsPointerMoving();
			if (flag)
			{
				for (int i = 0; i < eventData.hovered.Count; i++)
				{
					global::UnityEngine.EventSystems.ExecuteEvents.Execute(eventData.hovered[i], eventData, global::UnityEngine.EventSystems.ExecuteEvents.pointerMoveHandler);
				}
			}
			if (currentPointerTarget == null || eventData.pointerEnter == null)
			{
				for (int j = 0; j < eventData.hovered.Count; j++)
				{
					global::UnityEngine.EventSystems.ExecuteEvents.Execute(eventData.hovered[j], eventData, global::UnityEngine.EventSystems.ExecuteEvents.pointerExitHandler);
				}
				eventData.hovered.Clear();
				if (currentPointerTarget == null)
				{
					eventData.pointerEnter = null;
					return;
				}
			}
			if (eventData.pointerEnter == currentPointerTarget && (bool)currentPointerTarget)
			{
				return;
			}
			global::UnityEngine.Transform transform = global::UnityEngine.EventSystems.BaseInputModule.FindCommonRoot(eventData.pointerEnter, currentPointerTarget)?.transform;
			global::UnityEngine.Transform transform2 = ((global::UnityEngine.Component)currentPointerTarget.GetComponentInParent<global::UnityEngine.EventSystems.IPointerExitHandler>())?.transform;
			if (eventData.pointerEnter != null)
			{
				global::UnityEngine.Transform parent = eventData.pointerEnter.transform;
				while (parent != null && (!sendPointerHoverToParent || !(parent == transform)) && (sendPointerHoverToParent || !(parent == transform2)))
				{
					eventData.fullyExited = parent != transform && eventData.pointerEnter != currentPointerTarget;
					global::UnityEngine.EventSystems.ExecuteEvents.Execute(parent.gameObject, eventData, global::UnityEngine.EventSystems.ExecuteEvents.pointerExitHandler);
					eventData.hovered.Remove(parent.gameObject);
					if (sendPointerHoverToParent)
					{
						parent = parent.parent;
					}
					if (parent == transform)
					{
						break;
					}
					if (!sendPointerHoverToParent)
					{
						parent = parent.parent;
					}
				}
			}
			global::UnityEngine.Transform transform3 = (eventData.pointerEnter ? eventData.pointerEnter.transform : null);
			eventData.pointerEnter = currentPointerTarget;
			if (!(currentPointerTarget != null))
			{
				return;
			}
			global::UnityEngine.Transform parent2 = currentPointerTarget.transform;
			while (parent2 != null && !PointerShouldIgnoreTransform(parent2))
			{
				eventData.reentered = parent2 == transform && parent2 != transform3;
				if (!sendPointerHoverToParent || !eventData.reentered)
				{
					global::UnityEngine.EventSystems.ExecuteEvents.Execute(parent2.gameObject, eventData, global::UnityEngine.EventSystems.ExecuteEvents.pointerEnterHandler);
					if (flag)
					{
						global::UnityEngine.EventSystems.ExecuteEvents.Execute(parent2.gameObject, eventData, global::UnityEngine.EventSystems.ExecuteEvents.pointerMoveHandler);
					}
					eventData.hovered.Add(parent2.gameObject);
					if (sendPointerHoverToParent || parent2.GetComponent<global::UnityEngine.EventSystems.IPointerEnterHandler>() == null)
					{
						if (sendPointerHoverToParent)
						{
							parent2 = parent2.parent;
						}
						if (!(parent2 == transform))
						{
							if (!sendPointerHoverToParent)
							{
								parent2 = parent2.parent;
							}
							continue;
						}
						break;
					}
					break;
				}
				break;
			}
		}

		private void ProcessPointerButton(ref global::UnityEngine.InputSystem.UI.PointerModel.ButtonState button, global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			global::UnityEngine.GameObject gameObject = eventData.pointerCurrentRaycast.gameObject;
			if (gameObject != null && PointerShouldIgnoreTransform(gameObject.transform))
			{
				return;
			}
			if (button.wasPressedThisFrame)
			{
				button.pressTime = global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_Instance.unscaledGameTime;
				eventData.delta = global::UnityEngine.Vector2.zero;
				eventData.dragging = false;
				eventData.pressPosition = eventData.position;
				eventData.pointerPressRaycast = eventData.pointerCurrentRaycast;
				eventData.eligibleForClick = true;
				eventData.useDragThreshold = true;
				global::UnityEngine.GameObject eventHandler = global::UnityEngine.EventSystems.ExecuteEvents.GetEventHandler<global::UnityEngine.EventSystems.ISelectHandler>(gameObject);
				if (eventHandler != base.eventSystem.currentSelectedGameObject && (eventHandler != null || m_DeselectOnBackgroundClick))
				{
					base.eventSystem.SetSelectedGameObject(null, eventData);
				}
				global::UnityEngine.GameObject gameObject2 = global::UnityEngine.EventSystems.ExecuteEvents.ExecuteHierarchy(gameObject, eventData, global::UnityEngine.EventSystems.ExecuteEvents.pointerDownHandler);
				global::UnityEngine.GameObject eventHandler2 = global::UnityEngine.EventSystems.ExecuteEvents.GetEventHandler<global::UnityEngine.EventSystems.IPointerClickHandler>(gameObject);
				if (gameObject2 == null)
				{
					gameObject2 = eventHandler2;
				}
				button.clickedOnSameGameObject = gameObject2 == eventData.lastPress && button.pressTime - eventData.clickTime <= 0.3f;
				if (eventData.clickCount > 0 && !button.clickedOnSameGameObject)
				{
					eventData.clickCount = 0;
					eventData.clickTime = 0f;
				}
				eventData.pointerPress = gameObject2;
				eventData.pointerClick = eventHandler2;
				eventData.rawPointerPress = gameObject;
				eventData.pointerDrag = global::UnityEngine.EventSystems.ExecuteEvents.GetEventHandler<global::UnityEngine.EventSystems.IDragHandler>(gameObject);
				if (eventData.pointerDrag != null)
				{
					global::UnityEngine.EventSystems.ExecuteEvents.Execute(eventData.pointerDrag, eventData, global::UnityEngine.EventSystems.ExecuteEvents.initializePotentialDrag);
				}
			}
			if (button.wasReleasedThisFrame)
			{
				global::UnityEngine.GameObject eventHandler3 = global::UnityEngine.EventSystems.ExecuteEvents.GetEventHandler<global::UnityEngine.EventSystems.IPointerClickHandler>(gameObject);
				int num;
				if (eventData.pointerClick != null && eventData.pointerClick == eventHandler3)
				{
					num = (eventData.eligibleForClick ? 1 : 0);
					if (num != 0)
					{
						if (button.clickedOnSameGameObject)
						{
							int clickCount = eventData.clickCount + 1;
							eventData.clickCount = clickCount;
						}
						else
						{
							eventData.clickCount = 1;
						}
						eventData.clickTime = global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_Instance.unscaledGameTime;
					}
				}
				else
				{
					num = 0;
				}
				global::UnityEngine.EventSystems.ExecuteEvents.Execute(eventData.pointerPress, eventData, global::UnityEngine.EventSystems.ExecuteEvents.pointerUpHandler);
				if (num != 0)
				{
					global::UnityEngine.EventSystems.ExecuteEvents.Execute(eventData.pointerClick, eventData, global::UnityEngine.EventSystems.ExecuteEvents.pointerClickHandler);
				}
				else if (eventData.dragging && eventData.pointerDrag != null)
				{
					global::UnityEngine.EventSystems.ExecuteEvents.ExecuteHierarchy(gameObject, eventData, global::UnityEngine.EventSystems.ExecuteEvents.dropHandler);
				}
				eventData.eligibleForClick = false;
				eventData.pointerPress = null;
				eventData.rawPointerPress = null;
				if (eventData.dragging && eventData.pointerDrag != null)
				{
					global::UnityEngine.EventSystems.ExecuteEvents.Execute(eventData.pointerDrag, eventData, global::UnityEngine.EventSystems.ExecuteEvents.endDragHandler);
				}
				eventData.dragging = false;
				eventData.pointerDrag = null;
				button.ignoreNextClick = false;
			}
			button.CopyPressStateFrom(eventData);
		}

		private void ProcessPointerButtonDrag(ref global::UnityEngine.InputSystem.UI.PointerModel.ButtonState button, global::UnityEngine.InputSystem.UI.ExtendedPointerEventData eventData)
		{
			if (!eventData.IsPointerMoving() || (eventData.pointerType == global::UnityEngine.InputSystem.UI.UIPointerType.MouseOrPen && global::UnityEngine.Cursor.lockState == global::UnityEngine.CursorLockMode.Locked) || eventData.pointerDrag == null)
			{
				return;
			}
			if (!eventData.dragging && (!eventData.useDragThreshold || (double)(eventData.pressPosition - eventData.position).sqrMagnitude >= (double)base.eventSystem.pixelDragThreshold * (double)base.eventSystem.pixelDragThreshold * (double)((eventData.pointerType == global::UnityEngine.InputSystem.UI.UIPointerType.Tracked) ? m_TrackedDeviceDragThresholdMultiplier : 1f)))
			{
				global::UnityEngine.EventSystems.ExecuteEvents.Execute(eventData.pointerDrag, eventData, global::UnityEngine.EventSystems.ExecuteEvents.beginDragHandler);
				eventData.dragging = true;
			}
			if (eventData.dragging)
			{
				if (eventData.pointerPress != eventData.pointerDrag)
				{
					global::UnityEngine.EventSystems.ExecuteEvents.Execute(eventData.pointerPress, eventData, global::UnityEngine.EventSystems.ExecuteEvents.pointerUpHandler);
					eventData.eligibleForClick = false;
					eventData.pointerPress = null;
					eventData.rawPointerPress = null;
				}
				global::UnityEngine.EventSystems.ExecuteEvents.Execute(eventData.pointerDrag, eventData, global::UnityEngine.EventSystems.ExecuteEvents.dragHandler);
				button.CopyPressStateFrom(eventData);
			}
		}

		private static void ProcessPointerScroll(ref global::UnityEngine.InputSystem.UI.PointerModel pointer, global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			global::UnityEngine.Vector2 scrollDelta = pointer.scrollDelta;
			if (!global::UnityEngine.Mathf.Approximately(scrollDelta.sqrMagnitude, 0f))
			{
				eventData.scrollDelta = scrollDelta;
				global::UnityEngine.EventSystems.ExecuteEvents.ExecuteHierarchy(global::UnityEngine.EventSystems.ExecuteEvents.GetEventHandler<global::UnityEngine.EventSystems.IScrollHandler>(eventData.pointerEnter), eventData, global::UnityEngine.EventSystems.ExecuteEvents.scrollHandler);
			}
		}

		internal void ProcessNavigation(ref global::UnityEngine.InputSystem.UI.NavigationModel navigationState)
		{
			bool flag = false;
			if (base.eventSystem.currentSelectedGameObject != null)
			{
				global::UnityEngine.EventSystems.BaseEventData baseEventData = GetBaseEventData();
				global::UnityEngine.EventSystems.ExecuteEvents.Execute(base.eventSystem.currentSelectedGameObject, baseEventData, global::UnityEngine.EventSystems.ExecuteEvents.updateSelectedHandler);
				flag = baseEventData.used;
			}
			if (!base.eventSystem.sendNavigationEvents)
			{
				return;
			}
			global::UnityEngine.Vector2 vector = navigationState.move;
			if (!flag && (!global::UnityEngine.Mathf.Approximately(vector.x, 0f) || !global::UnityEngine.Mathf.Approximately(vector.y, 0f)))
			{
				float unscaledGameTime = global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_Instance.unscaledGameTime;
				global::UnityEngine.Vector2 moveVector = navigationState.move;
				global::UnityEngine.EventSystems.MoveDirection moveDirection = global::UnityEngine.EventSystems.MoveDirection.None;
				if (moveVector.sqrMagnitude > 0f)
				{
					moveDirection = ((!(global::UnityEngine.Mathf.Abs(moveVector.x) > global::UnityEngine.Mathf.Abs(moveVector.y))) ? ((moveVector.y > 0f) ? global::UnityEngine.EventSystems.MoveDirection.Up : global::UnityEngine.EventSystems.MoveDirection.Down) : ((moveVector.x > 0f) ? global::UnityEngine.EventSystems.MoveDirection.Right : global::UnityEngine.EventSystems.MoveDirection.Left));
				}
				if (moveDirection != m_NavigationState.lastMoveDirection)
				{
					m_NavigationState.consecutiveMoveCount = 0;
				}
				if (moveDirection != global::UnityEngine.EventSystems.MoveDirection.None)
				{
					bool flag2 = true;
					if (m_NavigationState.consecutiveMoveCount != 0)
					{
						flag2 = ((m_NavigationState.consecutiveMoveCount <= 1) ? (unscaledGameTime > m_NavigationState.lastMoveTime + moveRepeatDelay) : (unscaledGameTime > m_NavigationState.lastMoveTime + moveRepeatRate));
					}
					if (flag2)
					{
						global::UnityEngine.InputSystem.UI.ExtendedAxisEventData extendedAxisEventData = m_NavigationState.eventData as global::UnityEngine.InputSystem.UI.ExtendedAxisEventData;
						if (extendedAxisEventData == null)
						{
							extendedAxisEventData = new global::UnityEngine.InputSystem.UI.ExtendedAxisEventData(base.eventSystem);
							m_NavigationState.eventData = extendedAxisEventData;
						}
						extendedAxisEventData.Reset();
						extendedAxisEventData.moveVector = moveVector;
						extendedAxisEventData.moveDir = moveDirection;
						extendedAxisEventData.device = navigationState.device;
						if (IsMoveAllowed(extendedAxisEventData))
						{
							global::UnityEngine.EventSystems.ExecuteEvents.Execute(base.eventSystem.currentSelectedGameObject, extendedAxisEventData, global::UnityEngine.EventSystems.ExecuteEvents.moveHandler);
							flag = extendedAxisEventData.used;
							m_NavigationState.consecutiveMoveCount++;
							m_NavigationState.lastMoveTime = unscaledGameTime;
							m_NavigationState.lastMoveDirection = moveDirection;
						}
					}
				}
				else
				{
					m_NavigationState.consecutiveMoveCount = 0;
				}
			}
			else
			{
				m_NavigationState.consecutiveMoveCount = 0;
			}
			if (!flag && base.eventSystem.currentSelectedGameObject != null)
			{
				global::UnityEngine.InputSystem.InputAction inputAction = m_SubmitAction?.action;
				global::UnityEngine.InputSystem.InputAction inputAction2 = m_CancelAction?.action;
				global::UnityEngine.InputSystem.UI.ExtendedSubmitCancelEventData extendedSubmitCancelEventData = m_SubmitCancelState.eventData as global::UnityEngine.InputSystem.UI.ExtendedSubmitCancelEventData;
				if (extendedSubmitCancelEventData == null)
				{
					extendedSubmitCancelEventData = new global::UnityEngine.InputSystem.UI.ExtendedSubmitCancelEventData(base.eventSystem);
					m_SubmitCancelState.eventData = extendedSubmitCancelEventData;
				}
				extendedSubmitCancelEventData.Reset();
				extendedSubmitCancelEventData.device = m_SubmitCancelState.device;
				if (inputAction2 != null && inputAction2.WasPerformedThisDynamicUpdate())
				{
					global::UnityEngine.EventSystems.ExecuteEvents.Execute(base.eventSystem.currentSelectedGameObject, extendedSubmitCancelEventData, global::UnityEngine.EventSystems.ExecuteEvents.cancelHandler);
				}
				if (!extendedSubmitCancelEventData.used && inputAction != null && inputAction.WasPerformedThisDynamicUpdate())
				{
					global::UnityEngine.EventSystems.ExecuteEvents.Execute(base.eventSystem.currentSelectedGameObject, extendedSubmitCancelEventData, global::UnityEngine.EventSystems.ExecuteEvents.submitHandler);
				}
			}
		}

		private bool IsMoveAllowed(global::UnityEngine.EventSystems.AxisEventData eventData)
		{
			if (m_LocalMultiPlayerRoot == null)
			{
				return true;
			}
			if (base.eventSystem.currentSelectedGameObject == null)
			{
				return true;
			}
			global::UnityEngine.UI.Selectable component = base.eventSystem.currentSelectedGameObject.GetComponent<global::UnityEngine.UI.Selectable>();
			if (component == null)
			{
				return true;
			}
			global::UnityEngine.UI.Selectable selectable = null;
			switch (eventData.moveDir)
			{
			case global::UnityEngine.EventSystems.MoveDirection.Right:
				selectable = component.FindSelectableOnRight();
				break;
			case global::UnityEngine.EventSystems.MoveDirection.Up:
				selectable = component.FindSelectableOnUp();
				break;
			case global::UnityEngine.EventSystems.MoveDirection.Left:
				selectable = component.FindSelectableOnLeft();
				break;
			case global::UnityEngine.EventSystems.MoveDirection.Down:
				selectable = component.FindSelectableOnDown();
				break;
			}
			if (selectable == null)
			{
				return true;
			}
			return selectable.transform.IsChildOf(m_LocalMultiPlayerRoot.transform);
		}

		private void SwapAction(ref global::UnityEngine.InputSystem.InputActionReference property, global::UnityEngine.InputSystem.InputActionReference newValue, bool actionsHooked, global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> actionCallback)
		{
			if (!(property == newValue) && (!(property != null) || !(newValue != null) || property.action != newValue.action))
			{
				if (property != null && actionCallback != null && actionsHooked)
				{
					property.action.performed -= actionCallback;
					property.action.canceled -= actionCallback;
				}
				bool flag = property?.action == null;
				bool flag2 = property?.action != null && property.action.enabled;
				TryDisableInputAction(property);
				property = newValue;
				if (newValue?.action != null && actionCallback != null && actionsHooked)
				{
					property.action.performed += actionCallback;
					property.action.canceled += actionCallback;
				}
				if (base.isActiveAndEnabled && newValue?.action != null && (flag2 || flag))
				{
					EnableInputAction(property);
				}
			}
		}

		[global::UnityEngine.RuntimeInitializeOnLoadMethod(global::UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void ResetDefaultActions()
		{
			if (defaultActions != null)
			{
				defaultActions.Dispose();
				defaultActions = null;
			}
		}

		public void AssignDefaultActions()
		{
			if (defaultActions == null)
			{
				defaultActions = new global::UnityEngine.InputSystem.DefaultInputActions();
			}
			actionsAsset = defaultActions.asset;
			cancel = global::UnityEngine.InputSystem.InputActionReference.Create(defaultActions.UI.Cancel);
			submit = global::UnityEngine.InputSystem.InputActionReference.Create(defaultActions.UI.Submit);
			move = global::UnityEngine.InputSystem.InputActionReference.Create(defaultActions.UI.Navigate);
			leftClick = global::UnityEngine.InputSystem.InputActionReference.Create(defaultActions.UI.Click);
			rightClick = global::UnityEngine.InputSystem.InputActionReference.Create(defaultActions.UI.RightClick);
			middleClick = global::UnityEngine.InputSystem.InputActionReference.Create(defaultActions.UI.MiddleClick);
			point = global::UnityEngine.InputSystem.InputActionReference.Create(defaultActions.UI.Point);
			scrollWheel = global::UnityEngine.InputSystem.InputActionReference.Create(defaultActions.UI.ScrollWheel);
			trackedDeviceOrientation = global::UnityEngine.InputSystem.InputActionReference.Create(defaultActions.UI.TrackedDeviceOrientation);
			trackedDevicePosition = global::UnityEngine.InputSystem.InputActionReference.Create(defaultActions.UI.TrackedDevicePosition);
		}

		public void UnassignActions()
		{
			defaultActions?.Dispose();
			defaultActions = null;
			actionsAsset = null;
			cancel = null;
			submit = null;
			move = null;
			leftClick = null;
			rightClick = null;
			middleClick = null;
			point = null;
			scrollWheel = null;
			trackedDeviceOrientation = null;
			trackedDevicePosition = null;
		}

		protected override void Awake()
		{
			base.Awake();
			m_NavigationState.Reset();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			UnhookActions();
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			if (m_OnControlsChangedDelegate == null)
			{
				m_OnControlsChangedDelegate = OnControlsChanged;
			}
			global::UnityEngine.InputSystem.InputActionState.s_GlobalState.onActionControlsChanged.AddCallback(m_OnControlsChangedDelegate);
			if (HasNoActions())
			{
				AssignDefaultActions();
			}
			ResetPointers();
			HookActions();
			EnableAllActions();
		}

		protected override void OnDisable()
		{
			ResetPointers();
			global::UnityEngine.InputSystem.InputActionState.s_GlobalState.onActionControlsChanged.RemoveCallback(m_OnControlsChangedDelegate);
			DisableAllActions();
			UnhookActions();
			if (defaultActions != null && defaultActions.asset == actionsAsset)
			{
				UnassignActions();
			}
			base.OnDisable();
		}

		private void ResetPointers()
		{
			for (int i = 0; i < m_PointerStates.length; i++)
			{
				if (SendPointerExitEventsAndRemovePointer(i))
				{
					i--;
				}
			}
			m_CurrentPointerId = -1;
			m_CurrentPointerIndex = -1;
			m_CurrentPointerType = global::UnityEngine.InputSystem.UI.UIPointerType.None;
		}

		private bool HasNoActions()
		{
			if (m_ActionsAsset != null)
			{
				return false;
			}
			if (m_PointAction?.action == null && m_LeftClickAction?.action == null && m_RightClickAction?.action == null && m_MiddleClickAction?.action == null && m_SubmitAction?.action == null && m_CancelAction?.action == null && m_ScrollWheelAction?.action == null && m_TrackedDeviceOrientationAction?.action == null)
			{
				return m_TrackedDevicePositionAction?.action == null;
			}
			return false;
		}

		private void EnableAllActions()
		{
			EnableInputAction(m_PointAction);
			EnableInputAction(m_LeftClickAction);
			EnableInputAction(m_RightClickAction);
			EnableInputAction(m_MiddleClickAction);
			EnableInputAction(m_MoveAction);
			EnableInputAction(m_SubmitAction);
			EnableInputAction(m_CancelAction);
			EnableInputAction(m_ScrollWheelAction);
			EnableInputAction(m_TrackedDeviceOrientationAction);
			EnableInputAction(m_TrackedDevicePositionAction);
		}

		private void DisableAllActions()
		{
			TryDisableInputAction(m_PointAction, isComponentDisabling: true);
			TryDisableInputAction(m_LeftClickAction, isComponentDisabling: true);
			TryDisableInputAction(m_RightClickAction, isComponentDisabling: true);
			TryDisableInputAction(m_MiddleClickAction, isComponentDisabling: true);
			TryDisableInputAction(m_MoveAction, isComponentDisabling: true);
			TryDisableInputAction(m_SubmitAction, isComponentDisabling: true);
			TryDisableInputAction(m_CancelAction, isComponentDisabling: true);
			TryDisableInputAction(m_ScrollWheelAction, isComponentDisabling: true);
			TryDisableInputAction(m_TrackedDeviceOrientationAction, isComponentDisabling: true);
			TryDisableInputAction(m_TrackedDevicePositionAction, isComponentDisabling: true);
		}

		private void EnableInputAction(global::UnityEngine.InputSystem.InputActionReference inputActionReference)
		{
			global::UnityEngine.InputSystem.InputAction inputAction = inputActionReference?.action;
			if (inputAction != null)
			{
				if (s_InputActionReferenceCounts.TryGetValue(inputAction, out var value))
				{
					value.refCount++;
					s_InputActionReferenceCounts[inputAction] = value;
				}
				else
				{
					value = new global::UnityEngine.InputSystem.UI.InputSystemUIInputModule.InputActionReferenceState
					{
						refCount = 1,
						enabledByInputModule = !inputAction.enabled
					};
					s_InputActionReferenceCounts.Add(inputAction, value);
				}
				inputAction.Enable();
			}
		}

		private void TryDisableInputAction(global::UnityEngine.InputSystem.InputActionReference inputActionReference, bool isComponentDisabling = false)
		{
			global::UnityEngine.InputSystem.InputAction inputAction = inputActionReference?.action;
			if (inputAction != null && (base.isActiveAndEnabled || isComponentDisabling) && s_InputActionReferenceCounts.TryGetValue(inputAction, out var value))
			{
				if (value.refCount - 1 == 0 && value.enabledByInputModule)
				{
					inputAction.Disable();
					s_InputActionReferenceCounts.Remove(inputAction);
				}
				else
				{
					value.refCount--;
					s_InputActionReferenceCounts[inputAction] = value;
				}
			}
		}

		private int GetPointerStateIndexFor(int pointerOrTouchId)
		{
			if (pointerOrTouchId == m_CurrentPointerId)
			{
				return m_CurrentPointerIndex;
			}
			for (int i = 0; i < m_PointerIds.length; i++)
			{
				if (m_PointerIds[i] == pointerOrTouchId)
				{
					return i;
				}
			}
			for (int j = 0; j < m_PointerStates.length; j++)
			{
				global::UnityEngine.InputSystem.UI.ExtendedPointerEventData eventData = m_PointerStates[j].eventData;
				if (eventData.touchId == pointerOrTouchId || (eventData.touchId != 0 && eventData.device.deviceId == pointerOrTouchId))
				{
					return j;
				}
			}
			return -1;
		}

		private ref global::UnityEngine.InputSystem.UI.PointerModel GetPointerStateForIndex(int index)
		{
			if (index == 0)
			{
				return ref m_PointerStates.firstValue;
			}
			return ref m_PointerStates.additionalValues[index - 1];
		}

		private int GetDisplayIndexFor(global::UnityEngine.InputSystem.InputControl control)
		{
			int result = 0;
			if (control.device is global::UnityEngine.InputSystem.Pointer pointer)
			{
				result = pointer.displayIndex.ReadValue();
			}
			return result;
		}

		private int GetPointerStateIndexFor(ref global::UnityEngine.InputSystem.InputAction.CallbackContext context)
		{
			if (CheckForRemovedDevice(ref context))
			{
				return -1;
			}
			global::UnityEngine.InputSystem.InputActionPhase phase = context.phase;
			return GetPointerStateIndexFor(context.control, phase != global::UnityEngine.InputSystem.InputActionPhase.Canceled);
		}

		private int GetPointerStateIndexFor(global::UnityEngine.InputSystem.InputControl control, bool createIfNotExists = true)
		{
			global::UnityEngine.InputSystem.InputDevice device = control.device;
			global::UnityEngine.InputSystem.InputControl parent = control.parent;
			int num = device.deviceId;
			int num2 = 0;
			global::UnityEngine.Vector2 screenPosition = global::UnityEngine.Vector2.zero;
			if (parent is global::UnityEngine.InputSystem.Controls.TouchControl touchControl)
			{
				num2 = touchControl.touchId.value;
				screenPosition = touchControl.position.value;
			}
			else if (parent is global::UnityEngine.InputSystem.Touchscreen touchscreen)
			{
				num2 = touchscreen.primaryTouch.touchId.value;
				screenPosition = touchscreen.primaryTouch.position.value;
			}
			int displayIndexFor = GetDisplayIndexFor(control);
			if (num2 != 0)
			{
				num = global::UnityEngine.InputSystem.UI.ExtendedPointerEventData.MakePointerIdForTouch(num, num2);
			}
			if (m_CurrentPointerId == num)
			{
				return m_CurrentPointerIndex;
			}
			for (int i = 0; i < m_PointerIds.length; i++)
			{
				if (m_PointerIds[i] == num)
				{
					m_CurrentPointerId = num;
					m_CurrentPointerIndex = i;
					m_CurrentPointerType = m_PointerStates[i].pointerType;
					return i;
				}
			}
			if (!createIfNotExists)
			{
				return -1;
			}
			global::UnityEngine.InputSystem.UI.UIPointerType uIPointerType = global::UnityEngine.InputSystem.UI.UIPointerType.None;
			if (num2 != 0)
			{
				uIPointerType = global::UnityEngine.InputSystem.UI.UIPointerType.Touch;
			}
			else if (HaveControlForDevice(device, point))
			{
				uIPointerType = global::UnityEngine.InputSystem.UI.UIPointerType.MouseOrPen;
			}
			else if (HaveControlForDevice(device, trackedDevicePosition))
			{
				uIPointerType = global::UnityEngine.InputSystem.UI.UIPointerType.Tracked;
			}
			if ((m_PointerBehavior == global::UnityEngine.InputSystem.UI.UIPointerBehavior.SingleUnifiedPointer && uIPointerType != global::UnityEngine.InputSystem.UI.UIPointerType.None) || (m_PointerBehavior == global::UnityEngine.InputSystem.UI.UIPointerBehavior.SingleMouseOrPenButMultiTouchAndTrack && uIPointerType == global::UnityEngine.InputSystem.UI.UIPointerType.MouseOrPen))
			{
				if (m_CurrentPointerIndex == -1)
				{
					m_CurrentPointerIndex = AllocatePointer(num, displayIndexFor, num2, uIPointerType, control, device, (num2 != 0) ? parent : null);
				}
				else
				{
					ref global::UnityEngine.InputSystem.UI.PointerModel pointerStateForIndex = ref GetPointerStateForIndex(m_CurrentPointerIndex);
					global::UnityEngine.InputSystem.UI.ExtendedPointerEventData eventData = pointerStateForIndex.eventData;
					eventData.control = control;
					eventData.device = device;
					eventData.pointerType = uIPointerType;
					eventData.pointerId = num;
					eventData.touchId = num2;
					eventData.displayIndex = displayIndexFor;
					eventData.trackedDeviceOrientation = default(global::UnityEngine.Quaternion);
					eventData.trackedDevicePosition = default(global::UnityEngine.Vector3);
					if (m_PointerBehavior == global::UnityEngine.InputSystem.UI.UIPointerBehavior.SingleUnifiedPointer)
					{
						pointerStateForIndex.leftButton.OnEndFrame();
						pointerStateForIndex.rightButton.OnEndFrame();
						pointerStateForIndex.middleButton.OnEndFrame();
					}
				}
				if (uIPointerType == global::UnityEngine.InputSystem.UI.UIPointerType.Touch)
				{
					GetPointerStateForIndex(m_CurrentPointerIndex).screenPosition = screenPosition;
				}
				m_CurrentPointerId = num;
				m_CurrentPointerType = uIPointerType;
				return m_CurrentPointerIndex;
			}
			int num3 = -1;
			if (uIPointerType != global::UnityEngine.InputSystem.UI.UIPointerType.None)
			{
				num3 = AllocatePointer(num, displayIndexFor, num2, uIPointerType, control, device, (num2 != 0) ? parent : null);
			}
			else
			{
				if (m_CurrentPointerId != -1)
				{
					return m_CurrentPointerIndex;
				}
				global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputControl>? readOnlyArray = point?.action?.controls;
				global::UnityEngine.InputSystem.InputDevice inputDevice = ((readOnlyArray.HasValue && readOnlyArray.Value.Count > 0) ? readOnlyArray.Value[0].device : null);
				if (inputDevice != null && !(inputDevice is global::UnityEngine.InputSystem.Touchscreen))
				{
					num3 = AllocatePointer(inputDevice.deviceId, displayIndexFor, 0, global::UnityEngine.InputSystem.UI.UIPointerType.MouseOrPen, readOnlyArray.Value[0], inputDevice);
				}
				else
				{
					global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputControl>? readOnlyArray2 = trackedDevicePosition?.action?.controls;
					global::UnityEngine.InputSystem.InputDevice inputDevice2 = ((readOnlyArray2.HasValue && readOnlyArray2.Value.Count > 0) ? readOnlyArray2.Value[0].device : null);
					num3 = ((inputDevice2 == null) ? AllocatePointer(num, displayIndexFor, 0, global::UnityEngine.InputSystem.UI.UIPointerType.None, control, device) : AllocatePointer(inputDevice2.deviceId, displayIndexFor, 0, global::UnityEngine.InputSystem.UI.UIPointerType.Tracked, readOnlyArray2.Value[0], inputDevice2));
				}
			}
			if (uIPointerType == global::UnityEngine.InputSystem.UI.UIPointerType.Touch)
			{
				GetPointerStateForIndex(num3).screenPosition = screenPosition;
			}
			m_CurrentPointerId = num;
			m_CurrentPointerIndex = num3;
			m_CurrentPointerType = uIPointerType;
			return num3;
		}

		private int AllocatePointer(int pointerId, int displayIndex, int touchId, global::UnityEngine.InputSystem.UI.UIPointerType pointerType, global::UnityEngine.InputSystem.InputControl control, global::UnityEngine.InputSystem.InputDevice device, global::UnityEngine.InputSystem.InputControl touchControl = null)
		{
			global::UnityEngine.InputSystem.UI.ExtendedPointerEventData extendedPointerEventData = null;
			if (m_PointerStates.Capacity > m_PointerStates.length)
			{
				extendedPointerEventData = ((m_PointerStates.length != 0) ? m_PointerStates.additionalValues[m_PointerStates.length - 1].eventData : m_PointerStates.firstValue.eventData);
			}
			if (extendedPointerEventData == null)
			{
				extendedPointerEventData = new global::UnityEngine.InputSystem.UI.ExtendedPointerEventData(base.eventSystem);
			}
			extendedPointerEventData.pointerId = pointerId;
			extendedPointerEventData.displayIndex = displayIndex;
			extendedPointerEventData.touchId = touchId;
			extendedPointerEventData.pointerType = pointerType;
			extendedPointerEventData.control = control;
			extendedPointerEventData.device = device;
			m_PointerIds.AppendWithCapacity(pointerId);
			return m_PointerStates.AppendWithCapacity(new global::UnityEngine.InputSystem.UI.PointerModel(extendedPointerEventData));
		}

		private bool SendPointerExitEventsAndRemovePointer(int index)
		{
			global::UnityEngine.InputSystem.UI.ExtendedPointerEventData eventData = m_PointerStates[index].eventData;
			if (eventData.pointerEnter != null)
			{
				ProcessPointerMovement(eventData, null);
			}
			return RemovePointerAtIndex(index);
		}

		private bool RemovePointerAtIndex(int index)
		{
			if (m_PointerStates.length == 0)
			{
				return false;
			}
			global::UnityEngine.InputSystem.UI.ExtendedPointerEventData eventData = m_PointerStates[index].eventData;
			if (index == m_CurrentPointerIndex)
			{
				m_CurrentPointerId = -1;
				m_CurrentPointerIndex = -1;
				m_CurrentPointerType = global::UnityEngine.InputSystem.UI.UIPointerType.None;
			}
			else if (m_CurrentPointerIndex == m_PointerIds.length - 1)
			{
				m_CurrentPointerIndex = index;
			}
			m_PointerIds.RemoveAtByMovingTailWithCapacity(index);
			m_PointerStates.RemoveAtByMovingTailWithCapacity(index);
			eventData.hovered.Clear();
			eventData.device = null;
			eventData.pointerCurrentRaycast = default(global::UnityEngine.EventSystems.RaycastResult);
			eventData.pointerPressRaycast = default(global::UnityEngine.EventSystems.RaycastResult);
			eventData.pointerPress = null;
			eventData.pointerPress = null;
			eventData.pointerDrag = null;
			eventData.pointerEnter = null;
			eventData.rawPointerPress = null;
			if (m_PointerStates.length == 0)
			{
				m_PointerStates.firstValue.eventData = eventData;
			}
			else
			{
				m_PointerStates.additionalValues[m_PointerStates.length - 1].eventData = eventData;
			}
			return true;
		}

		private void PurgeStalePointers()
		{
			for (int i = 0; i < m_PointerStates.length; i++)
			{
				global::UnityEngine.InputSystem.InputDevice device = GetPointerStateForIndex(i).eventData.device;
				if ((!device.added || (!HaveControlForDevice(device, point) && !HaveControlForDevice(device, trackedDevicePosition) && !HaveControlForDevice(device, trackedDeviceOrientation))) && SendPointerExitEventsAndRemovePointer(i))
				{
					i--;
				}
			}
			m_NeedToPurgeStalePointers = false;
		}

		private static bool HaveControlForDevice(global::UnityEngine.InputSystem.InputDevice device, global::UnityEngine.InputSystem.InputActionReference actionReference)
		{
			global::UnityEngine.InputSystem.InputAction inputAction = actionReference?.action;
			if (inputAction == null)
			{
				return false;
			}
			global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputControl> controls = inputAction.controls;
			for (int i = 0; i < controls.Count; i++)
			{
				if (controls[i].device == device)
				{
					return true;
				}
			}
			return false;
		}

		private void OnPointCallback(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
		{
			if (!CheckForRemovedDevice(ref context) && !context.canceled)
			{
				int pointerStateIndexFor = GetPointerStateIndexFor(context.control);
				if (pointerStateIndexFor != -1)
				{
					ref global::UnityEngine.InputSystem.UI.PointerModel pointerStateForIndex = ref GetPointerStateForIndex(pointerStateIndexFor);
					pointerStateForIndex.screenPosition = context.ReadValue<global::UnityEngine.Vector2>();
					pointerStateForIndex.eventData.displayIndex = GetDisplayIndexFor(context.control);
				}
			}
		}

		private bool IgnoreNextClick(ref global::UnityEngine.InputSystem.InputAction.CallbackContext context, bool wasPressed)
		{
			if (explictlyIgnoreFocus)
			{
				return false;
			}
			return context.canceled && !global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_Instance.isPlayerFocused && !context.control.device.canRunInBackground && wasPressed;
		}

		private void OnLeftClickCallback(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
		{
			int pointerStateIndexFor = GetPointerStateIndexFor(ref context);
			if (pointerStateIndexFor != -1)
			{
				ref global::UnityEngine.InputSystem.UI.PointerModel pointerStateForIndex = ref GetPointerStateForIndex(pointerStateIndexFor);
				bool isPressed = pointerStateForIndex.leftButton.isPressed;
				pointerStateForIndex.leftButton.isPressed = context.ReadValueAsButton();
				pointerStateForIndex.changedThisFrame = true;
				if (IgnoreNextClick(ref context, isPressed))
				{
					pointerStateForIndex.leftButton.ignoreNextClick = true;
				}
				pointerStateForIndex.eventData.displayIndex = GetDisplayIndexFor(context.control);
			}
		}

		private void OnRightClickCallback(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
		{
			int pointerStateIndexFor = GetPointerStateIndexFor(ref context);
			if (pointerStateIndexFor != -1)
			{
				ref global::UnityEngine.InputSystem.UI.PointerModel pointerStateForIndex = ref GetPointerStateForIndex(pointerStateIndexFor);
				bool isPressed = pointerStateForIndex.rightButton.isPressed;
				pointerStateForIndex.rightButton.isPressed = context.ReadValueAsButton();
				pointerStateForIndex.changedThisFrame = true;
				if (IgnoreNextClick(ref context, isPressed))
				{
					pointerStateForIndex.rightButton.ignoreNextClick = true;
				}
				pointerStateForIndex.eventData.displayIndex = GetDisplayIndexFor(context.control);
			}
		}

		private void OnMiddleClickCallback(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
		{
			int pointerStateIndexFor = GetPointerStateIndexFor(ref context);
			if (pointerStateIndexFor != -1)
			{
				ref global::UnityEngine.InputSystem.UI.PointerModel pointerStateForIndex = ref GetPointerStateForIndex(pointerStateIndexFor);
				bool isPressed = pointerStateForIndex.middleButton.isPressed;
				pointerStateForIndex.middleButton.isPressed = context.ReadValueAsButton();
				pointerStateForIndex.changedThisFrame = true;
				if (IgnoreNextClick(ref context, isPressed))
				{
					pointerStateForIndex.middleButton.ignoreNextClick = true;
				}
				pointerStateForIndex.eventData.displayIndex = GetDisplayIndexFor(context.control);
			}
		}

		private bool CheckForRemovedDevice(ref global::UnityEngine.InputSystem.InputAction.CallbackContext context)
		{
			if (context.canceled && !context.control.device.added)
			{
				m_NeedToPurgeStalePointers = true;
				return true;
			}
			return false;
		}

		private void OnScrollCallback(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
		{
			int pointerStateIndexFor = GetPointerStateIndexFor(ref context);
			if (pointerStateIndexFor != -1)
			{
				ref global::UnityEngine.InputSystem.UI.PointerModel pointerStateForIndex = ref GetPointerStateForIndex(pointerStateIndexFor);
				global::UnityEngine.Vector2 vector = context.ReadValue<global::UnityEngine.Vector2>();
				pointerStateForIndex.scrollDelta = vector / global::UnityEngine.InputSystem.InputSystem.scrollWheelDeltaPerTick * scrollDeltaPerTick;
				pointerStateForIndex.eventData.displayIndex = GetDisplayIndexFor(context.control);
			}
		}

		private void OnMoveCallback(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
		{
			m_NavigationState.move = context.ReadValue<global::UnityEngine.Vector2>();
			m_NavigationState.device = context.control.device;
		}

		private void OnSubmitCancelCallback(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
		{
			m_SubmitCancelState.device = context.control.device;
		}

		private void OnTrackedDeviceOrientationCallback(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
		{
			int pointerStateIndexFor = GetPointerStateIndexFor(ref context);
			if (pointerStateIndexFor != -1)
			{
				ref global::UnityEngine.InputSystem.UI.PointerModel pointerStateForIndex = ref GetPointerStateForIndex(pointerStateIndexFor);
				pointerStateForIndex.worldOrientation = context.ReadValue<global::UnityEngine.Quaternion>();
				pointerStateForIndex.eventData.displayIndex = GetDisplayIndexFor(context.control);
			}
		}

		private void OnTrackedDevicePositionCallback(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
		{
			int pointerStateIndexFor = GetPointerStateIndexFor(ref context);
			if (pointerStateIndexFor != -1)
			{
				ref global::UnityEngine.InputSystem.UI.PointerModel pointerStateForIndex = ref GetPointerStateForIndex(pointerStateIndexFor);
				pointerStateForIndex.worldPosition = context.ReadValue<global::UnityEngine.Vector3>();
				pointerStateForIndex.eventData.displayIndex = GetDisplayIndexFor(context.control);
			}
		}

		private void OnControlsChanged(object obj)
		{
			m_NeedToPurgeStalePointers = true;
		}

		private void FilterPointerStatesByType()
		{
			global::UnityEngine.InputSystem.UI.UIPointerType uIPointerType = global::UnityEngine.InputSystem.UI.UIPointerType.None;
			for (int i = 0; i < m_PointerStates.length; i++)
			{
				ref global::UnityEngine.InputSystem.UI.PointerModel pointerStateForIndex = ref GetPointerStateForIndex(i);
				pointerStateForIndex.eventData.ReadDeviceState();
				pointerStateForIndex.CopyTouchOrPenStateFrom(pointerStateForIndex.eventData);
				if (pointerStateForIndex.changedThisFrame && uIPointerType == global::UnityEngine.InputSystem.UI.UIPointerType.None)
				{
					uIPointerType = pointerStateForIndex.pointerType;
				}
			}
			if (m_PointerBehavior != global::UnityEngine.InputSystem.UI.UIPointerBehavior.SingleMouseOrPenButMultiTouchAndTrack)
			{
				return;
			}
			switch (uIPointerType)
			{
			case global::UnityEngine.InputSystem.UI.UIPointerType.MouseOrPen:
			{
				for (int j = 0; j < m_PointerStates.length; j++)
				{
					if (m_PointerStates[j].pointerType != global::UnityEngine.InputSystem.UI.UIPointerType.MouseOrPen && SendPointerExitEventsAndRemovePointer(j))
					{
						j--;
					}
				}
				return;
			}
			case global::UnityEngine.InputSystem.UI.UIPointerType.None:
				return;
			}
			for (int k = 0; k < m_PointerStates.length; k++)
			{
				if (m_PointerStates[k].pointerType == global::UnityEngine.InputSystem.UI.UIPointerType.MouseOrPen && SendPointerExitEventsAndRemovePointer(k))
				{
					k--;
				}
			}
		}

		public override void Process()
		{
			if (m_NeedToPurgeStalePointers)
			{
				PurgeStalePointers();
			}
			if (!base.eventSystem.isFocused && !shouldIgnoreFocus)
			{
				for (int i = 0; i < m_PointerStates.length; i++)
				{
					m_PointerStates[i].OnFrameFinished();
				}
				return;
			}
			ProcessNavigation(ref m_NavigationState);
			FilterPointerStatesByType();
			for (int j = 0; j < m_PointerStates.length; j++)
			{
				ref global::UnityEngine.InputSystem.UI.PointerModel pointerStateForIndex = ref GetPointerStateForIndex(j);
				ProcessPointer(ref pointerStateForIndex);
				if (pointerStateForIndex.pointerType == global::UnityEngine.InputSystem.UI.UIPointerType.Touch && !pointerStateForIndex.leftButton.isPressed && !pointerStateForIndex.leftButton.wasReleasedThisFrame)
				{
					if (RemovePointerAtIndex(j))
					{
						j--;
					}
				}
				else
				{
					pointerStateForIndex.OnFrameFinished();
				}
			}
		}

		public override int ConvertUIToolkitPointerId(global::UnityEngine.EventSystems.PointerEventData sourcePointerData)
		{
			if (m_PointerBehavior == global::UnityEngine.InputSystem.UI.UIPointerBehavior.SingleUnifiedPointer)
			{
				return global::UnityEngine.UIElements.PointerId.mousePointerId;
			}
			if (!(sourcePointerData is global::UnityEngine.InputSystem.UI.ExtendedPointerEventData extendedPointerEventData))
			{
				return base.ConvertUIToolkitPointerId(sourcePointerData);
			}
			return extendedPointerEventData.uiToolkitPointerId;
		}

		public override global::UnityEngine.Vector2 ConvertPointerEventScrollDeltaToTicks(global::UnityEngine.Vector2 scrollDelta)
		{
			if (global::UnityEngine.Mathf.Abs(scrollDeltaPerTick) < 1E-05f)
			{
				return global::UnityEngine.Vector2.zero;
			}
			return scrollDelta / scrollDeltaPerTick;
		}

		public override global::UnityEngine.EventSystems.NavigationDeviceType GetNavigationEventDeviceType(global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			if (!(eventData is global::UnityEngine.InputSystem.UI.INavigationEventData navigationEventData))
			{
				return global::UnityEngine.EventSystems.NavigationDeviceType.Unknown;
			}
			if (navigationEventData.device is global::UnityEngine.InputSystem.Keyboard)
			{
				return global::UnityEngine.EventSystems.NavigationDeviceType.Keyboard;
			}
			return global::UnityEngine.EventSystems.NavigationDeviceType.NonKeyboard;
		}

		private void HookActions()
		{
			if (!m_ActionsHooked)
			{
				if (m_OnPointDelegate == null)
				{
					m_OnPointDelegate = OnPointCallback;
				}
				if (m_OnLeftClickDelegate == null)
				{
					m_OnLeftClickDelegate = OnLeftClickCallback;
				}
				if (m_OnRightClickDelegate == null)
				{
					m_OnRightClickDelegate = OnRightClickCallback;
				}
				if (m_OnMiddleClickDelegate == null)
				{
					m_OnMiddleClickDelegate = OnMiddleClickCallback;
				}
				if (m_OnScrollWheelDelegate == null)
				{
					m_OnScrollWheelDelegate = OnScrollCallback;
				}
				if (m_OnMoveDelegate == null)
				{
					m_OnMoveDelegate = OnMoveCallback;
				}
				if (m_OnSubmitCancelDelegate == null)
				{
					m_OnSubmitCancelDelegate = OnSubmitCancelCallback;
				}
				if (m_OnTrackedDeviceOrientationDelegate == null)
				{
					m_OnTrackedDeviceOrientationDelegate = OnTrackedDeviceOrientationCallback;
				}
				if (m_OnTrackedDevicePositionDelegate == null)
				{
					m_OnTrackedDevicePositionDelegate = OnTrackedDevicePositionCallback;
				}
				SetActionCallbacks(install: true);
			}
		}

		private void UnhookActions()
		{
			if (m_ActionsHooked)
			{
				SetActionCallbacks(install: false);
			}
		}

		private void SetActionCallbacks(bool install)
		{
			m_ActionsHooked = install;
			SetActionCallback(m_PointAction, m_OnPointDelegate, install);
			SetActionCallback(m_MoveAction, m_OnMoveDelegate, install);
			SetActionCallback(m_SubmitAction, m_OnSubmitCancelDelegate, install);
			SetActionCallback(m_CancelAction, m_OnSubmitCancelDelegate, install);
			SetActionCallback(m_LeftClickAction, m_OnLeftClickDelegate, install);
			SetActionCallback(m_RightClickAction, m_OnRightClickDelegate, install);
			SetActionCallback(m_MiddleClickAction, m_OnMiddleClickDelegate, install);
			SetActionCallback(m_ScrollWheelAction, m_OnScrollWheelDelegate, install);
			SetActionCallback(m_TrackedDeviceOrientationAction, m_OnTrackedDeviceOrientationDelegate, install);
			SetActionCallback(m_TrackedDevicePositionAction, m_OnTrackedDevicePositionDelegate, install);
		}

		private static void SetActionCallback(global::UnityEngine.InputSystem.InputActionReference actionReference, global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> callback, bool install)
		{
			if ((!install && callback == null) || actionReference == null)
			{
				return;
			}
			global::UnityEngine.InputSystem.InputAction action = actionReference.action;
			if (action != null)
			{
				if (install)
				{
					action.performed += callback;
					action.canceled += callback;
				}
				else
				{
					action.performed -= callback;
					action.canceled -= callback;
				}
			}
		}

		private global::UnityEngine.InputSystem.InputActionReference UpdateReferenceForNewAsset(global::UnityEngine.InputSystem.InputActionReference actionReference)
		{
			global::UnityEngine.InputSystem.InputAction inputAction = actionReference?.action;
			if (inputAction == null)
			{
				return null;
			}
			global::UnityEngine.InputSystem.InputActionMap actionMap = inputAction.actionMap;
			global::UnityEngine.InputSystem.InputActionMap inputActionMap = m_ActionsAsset?.FindActionMap(actionMap.name);
			if (inputActionMap == null)
			{
				return null;
			}
			global::UnityEngine.InputSystem.InputAction inputAction2 = inputActionMap.FindAction(inputAction.name);
			if (inputAction2 == null)
			{
				return null;
			}
			return global::UnityEngine.InputSystem.InputActionReference.Create(inputAction2);
		}
	}
}
