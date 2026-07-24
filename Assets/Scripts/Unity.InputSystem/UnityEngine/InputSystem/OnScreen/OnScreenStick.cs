namespace UnityEngine.InputSystem.OnScreen
{
	[global::UnityEngine.AddComponentMenu("Input/On-Screen Stick")]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.inputsystem@1.18/manual/OnScreen.html#on-screen-sticks")]
	public class OnScreenStick : global::UnityEngine.InputSystem.OnScreen.OnScreenControl, global::UnityEngine.EventSystems.IPointerDownHandler, global::UnityEngine.EventSystems.IEventSystemHandler, global::UnityEngine.EventSystems.IPointerUpHandler, global::UnityEngine.EventSystems.IDragHandler
	{
		public enum Behaviour
		{
			RelativePositionWithStaticOrigin = 0,
			ExactPositionWithStaticOrigin = 1,
			ExactPositionWithDynamicOrigin = 2
		}

		private const string kDynamicOriginClickable = "DynamicOriginClickable";

		[global::UnityEngine.Serialization.FormerlySerializedAs("movementRange")]
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Min(0f)]
		private float m_MovementRange = 50f;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Defines the circular region where the onscreen control may have it's origin placed.")]
		[global::UnityEngine.Min(0f)]
		private float m_DynamicOriginRange = 100f;

		[global::UnityEngine.InputSystem.Layouts.InputControl(layout = "Vector2")]
		[global::UnityEngine.SerializeField]
		private string m_ControlPath;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Choose how the onscreen stick will move relative to it's origin and the press position.\n\nRelativePositionWithStaticOrigin: The control's center of origin is fixed. The control will begin un-actuated at it's centered position and then move relative to the pointer or finger motion.\n\nExactPositionWithStaticOrigin: The control's center of origin is fixed. The stick will immediately jump to the exact position of the click or touch and begin tracking motion from there.\n\nExactPositionWithDynamicOrigin: The control's center of origin is determined by the initial press position. The stick will begin un-actuated at this center position and then track the current pointer or finger position.")]
		private global::UnityEngine.InputSystem.OnScreen.OnScreenStick.Behaviour m_Behaviour;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Set this to true to prevent cancellation of pointer events due to device switching. Cancellation will appear as the stick jumping back and forth between the pointer position and the stick center.")]
		private bool m_UseIsolatedInputActions;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("The action that will be used to detect pointer down events on the stick control. Note that if no bindings are set, default ones will be provided.")]
		private global::UnityEngine.InputSystem.InputAction m_PointerDownAction;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("The action that will be used to detect pointer movement on the stick control. Note that if no bindings are set, default ones will be provided.")]
		private global::UnityEngine.InputSystem.InputAction m_PointerMoveAction;

		private global::UnityEngine.Vector3 m_StartPos;

		private global::UnityEngine.Vector2 m_PointerDownPos;

		[global::System.NonSerialized]
		private global::System.Collections.Generic.List<global::UnityEngine.EventSystems.RaycastResult> m_RaycastResults;

		[global::System.NonSerialized]
		private global::UnityEngine.EventSystems.PointerEventData m_PointerEventData;

		[global::System.NonSerialized]
		private global::UnityEngine.InputSystem.Controls.TouchControl m_TouchControl;

		[global::System.NonSerialized]
		private bool m_IsIsolationActive;

		public float movementRange
		{
			get
			{
				return m_MovementRange;
			}
			set
			{
				m_MovementRange = value;
			}
		}

		public float dynamicOriginRange
		{
			get
			{
				return m_DynamicOriginRange;
			}
			set
			{
				if (m_DynamicOriginRange != value)
				{
					m_DynamicOriginRange = value;
					UpdateDynamicOriginClickableArea();
				}
			}
		}

		public bool useIsolatedInputActions
		{
			get
			{
				return m_UseIsolatedInputActions;
			}
			set
			{
				m_UseIsolatedInputActions = value;
			}
		}

		protected override string controlPathInternal
		{
			get
			{
				return m_ControlPath;
			}
			set
			{
				m_ControlPath = value;
			}
		}

		public global::UnityEngine.InputSystem.OnScreen.OnScreenStick.Behaviour behaviour
		{
			get
			{
				return m_Behaviour;
			}
			set
			{
				m_Behaviour = value;
			}
		}

		public void OnPointerDown(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (!m_UseIsolatedInputActions)
			{
				if (eventData == null)
				{
					throw new global::System.ArgumentNullException("eventData");
				}
				BeginInteraction(eventData.position, eventData.pressEventCamera);
			}
		}

		public void OnDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (!m_UseIsolatedInputActions)
			{
				if (eventData == null)
				{
					throw new global::System.ArgumentNullException("eventData");
				}
				MoveStick(eventData.position, eventData.pressEventCamera);
			}
		}

		public void OnPointerUp(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (!m_UseIsolatedInputActions)
			{
				EndInteraction();
			}
		}

		private void Start()
		{
			if (m_UseIsolatedInputActions)
			{
				m_RaycastResults = new global::System.Collections.Generic.List<global::UnityEngine.EventSystems.RaycastResult>();
				m_PointerEventData = new global::UnityEngine.EventSystems.PointerEventData(global::UnityEngine.EventSystems.EventSystem.current);
				if (m_PointerDownAction == null || m_PointerDownAction.bindings.Count == 0)
				{
					if (m_PointerDownAction == null)
					{
						m_PointerDownAction = new global::UnityEngine.InputSystem.InputAction(null, global::UnityEngine.InputSystem.InputActionType.PassThrough);
					}
					else if (m_PointerDownAction.m_Type != global::UnityEngine.InputSystem.InputActionType.PassThrough)
					{
						m_PointerDownAction.m_Type = global::UnityEngine.InputSystem.InputActionType.PassThrough;
					}
					m_PointerDownAction.AddBinding("<Mouse>/leftButton");
					m_PointerDownAction.AddBinding("<Pen>/tip");
					m_PointerDownAction.AddBinding("<Touchscreen>/touch*/press");
					m_PointerDownAction.AddBinding("<XRController>/trigger");
				}
				if (m_PointerMoveAction == null || m_PointerMoveAction.bindings.Count == 0)
				{
					if (m_PointerMoveAction == null)
					{
						m_PointerMoveAction = new global::UnityEngine.InputSystem.InputAction();
					}
					m_PointerMoveAction.AddBinding("<Mouse>/position");
					m_PointerMoveAction.AddBinding("<Pen>/position");
					m_PointerMoveAction.AddBinding("<Touchscreen>/touch*/position");
				}
				m_PointerDownAction.performed += OnPointerChanged;
				m_PointerDownAction.Enable();
				m_PointerMoveAction.Enable();
			}
			if (base.transform is global::UnityEngine.RectTransform)
			{
				m_StartPos = ((global::UnityEngine.RectTransform)base.transform).anchoredPosition;
				if (m_Behaviour == global::UnityEngine.InputSystem.OnScreen.OnScreenStick.Behaviour.ExactPositionWithDynamicOrigin)
				{
					m_PointerDownPos = m_StartPos;
					global::UnityEngine.GameObject obj = new global::UnityEngine.GameObject("DynamicOriginClickable", typeof(global::UnityEngine.UI.Image));
					obj.transform.SetParent(base.transform);
					global::UnityEngine.UI.Image component = obj.GetComponent<global::UnityEngine.UI.Image>();
					component.color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
					global::UnityEngine.RectTransform obj2 = (global::UnityEngine.RectTransform)obj.transform;
					obj2.sizeDelta = new global::UnityEngine.Vector2(m_DynamicOriginRange * 2f, m_DynamicOriginRange * 2f);
					obj2.localScale = new global::UnityEngine.Vector3(1f, 1f, 0f);
					obj2.anchoredPosition3D = global::UnityEngine.Vector3.zero;
					component.sprite = global::UnityEngine.InputSystem.Utilities.SpriteUtilities.CreateCircleSprite(16, new global::UnityEngine.Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue));
					component.alphaHitTestMinimumThreshold = 0.5f;
				}
			}
		}

		private void OnDestroy()
		{
			if (m_UseIsolatedInputActions)
			{
				m_PointerDownAction.performed -= OnPointerChanged;
			}
		}

		private void BeginInteraction(global::UnityEngine.Vector2 pointerPosition, global::UnityEngine.Camera uiCamera)
		{
			global::UnityEngine.RectTransform canvasRectTransform = global::UnityEngine.InputSystem.OnScreen.UGUIOnScreenControlUtils.GetCanvasRectTransform(base.transform);
			if (canvasRectTransform == null)
			{
				global::UnityEngine.Debug.LogError(GetWarningMessage());
				return;
			}
			switch (m_Behaviour)
			{
			case global::UnityEngine.InputSystem.OnScreen.OnScreenStick.Behaviour.RelativePositionWithStaticOrigin:
				global::UnityEngine.RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, pointerPosition, uiCamera, out m_PointerDownPos);
				break;
			case global::UnityEngine.InputSystem.OnScreen.OnScreenStick.Behaviour.ExactPositionWithStaticOrigin:
				global::UnityEngine.RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, pointerPosition, uiCamera, out m_PointerDownPos);
				MoveStick(pointerPosition, uiCamera);
				break;
			case global::UnityEngine.InputSystem.OnScreen.OnScreenStick.Behaviour.ExactPositionWithDynamicOrigin:
			{
				global::UnityEngine.RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, pointerPosition, uiCamera, out var localPoint);
				global::UnityEngine.Vector2 pointerDownPos = (((global::UnityEngine.RectTransform)base.transform).anchoredPosition = localPoint);
				m_PointerDownPos = pointerDownPos;
				break;
			}
			}
		}

		private void MoveStick(global::UnityEngine.Vector2 pointerPosition, global::UnityEngine.Camera uiCamera)
		{
			global::UnityEngine.RectTransform canvasRectTransform = global::UnityEngine.InputSystem.OnScreen.UGUIOnScreenControlUtils.GetCanvasRectTransform(base.transform);
			if (canvasRectTransform == null)
			{
				global::UnityEngine.Debug.LogError(GetWarningMessage());
				return;
			}
			global::UnityEngine.RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, pointerPosition, uiCamera, out var localPoint);
			global::UnityEngine.Vector2 vector = localPoint - m_PointerDownPos;
			switch (m_Behaviour)
			{
			case global::UnityEngine.InputSystem.OnScreen.OnScreenStick.Behaviour.RelativePositionWithStaticOrigin:
				vector = global::UnityEngine.Vector2.ClampMagnitude(vector, movementRange);
				((global::UnityEngine.RectTransform)base.transform).anchoredPosition = (global::UnityEngine.Vector2)m_StartPos + vector;
				break;
			case global::UnityEngine.InputSystem.OnScreen.OnScreenStick.Behaviour.ExactPositionWithStaticOrigin:
				vector = localPoint - (global::UnityEngine.Vector2)m_StartPos;
				vector = global::UnityEngine.Vector2.ClampMagnitude(vector, movementRange);
				((global::UnityEngine.RectTransform)base.transform).anchoredPosition = (global::UnityEngine.Vector2)m_StartPos + vector;
				break;
			case global::UnityEngine.InputSystem.OnScreen.OnScreenStick.Behaviour.ExactPositionWithDynamicOrigin:
				vector = global::UnityEngine.Vector2.ClampMagnitude(vector, movementRange);
				((global::UnityEngine.RectTransform)base.transform).anchoredPosition = m_PointerDownPos + vector;
				break;
			}
			global::UnityEngine.Vector2 value = new global::UnityEngine.Vector2(vector.x / movementRange, vector.y / movementRange);
			SendValueToControl(value);
		}

		private void EndInteraction()
		{
			((global::UnityEngine.RectTransform)base.transform).anchoredPosition = (m_PointerDownPos = m_StartPos);
			SendValueToControl(global::UnityEngine.Vector2.zero);
		}

		private void OnPointerDown(global::UnityEngine.InputSystem.InputAction.CallbackContext ctx)
		{
			if (m_IsIsolationActive)
			{
				return;
			}
			global::UnityEngine.Vector2 vector = global::UnityEngine.Vector2.zero;
			global::UnityEngine.InputSystem.Controls.TouchControl touchControl = null;
			if (ctx.control?.parent is global::UnityEngine.InputSystem.Controls.TouchControl touchControl2)
			{
				touchControl = touchControl2;
				vector = touchControl2.position.ReadValue();
			}
			else if (ctx.control?.device is global::UnityEngine.InputSystem.Pointer pointer)
			{
				vector = pointer.position.ReadValue();
			}
			m_PointerEventData.position = vector;
			global::UnityEngine.EventSystems.EventSystem.current.RaycastAll(m_PointerEventData, m_RaycastResults);
			if (m_RaycastResults.Count == 0)
			{
				return;
			}
			bool flag = false;
			foreach (global::UnityEngine.EventSystems.RaycastResult raycastResult in m_RaycastResults)
			{
				if (!(raycastResult.gameObject != base.gameObject))
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				BeginInteraction(vector, GetCameraFromCanvas());
				if (touchControl != null)
				{
					m_TouchControl = touchControl;
					m_PointerMoveAction.ApplyBindingOverride(touchControl.path + "/position", null, "<Touchscreen>/touch*/position");
				}
				m_PointerMoveAction.performed += OnPointerMove;
				m_IsIsolationActive = true;
			}
		}

		private void OnPointerChanged(global::UnityEngine.InputSystem.InputAction.CallbackContext ctx)
		{
			if (ctx.control.IsPressed())
			{
				OnPointerDown(ctx);
			}
			else
			{
				OnPointerUp(ctx);
			}
		}

		private void OnPointerMove(global::UnityEngine.InputSystem.InputAction.CallbackContext ctx)
		{
			global::UnityEngine.Vector2 pointerPosition;
			if (m_TouchControl != null)
			{
				if (!m_TouchControl.isInProgress)
				{
					return;
				}
				pointerPosition = m_TouchControl.position.ReadValue();
			}
			else
			{
				pointerPosition = ((global::UnityEngine.InputSystem.Pointer)ctx.control.device).position.ReadValue();
			}
			MoveStick(pointerPosition, GetCameraFromCanvas());
		}

		private void OnPointerUp(global::UnityEngine.InputSystem.InputAction.CallbackContext ctx)
		{
			if (!m_IsIsolationActive)
			{
				return;
			}
			if (m_TouchControl != null)
			{
				if (m_TouchControl.isInProgress)
				{
					return;
				}
				m_PointerMoveAction.ApplyBindingOverride(null, null, "<Touchscreen>/touch*/position");
				m_TouchControl = null;
			}
			EndInteraction();
			m_PointerMoveAction.performed -= OnPointerMove;
			m_IsIsolationActive = false;
		}

		private global::UnityEngine.Camera GetCameraFromCanvas()
		{
			global::UnityEngine.Canvas componentInParent = GetComponentInParent<global::UnityEngine.Canvas>();
			global::UnityEngine.RenderMode? renderMode = componentInParent?.renderMode;
			if (renderMode != global::UnityEngine.RenderMode.ScreenSpaceOverlay && (renderMode != global::UnityEngine.RenderMode.ScreenSpaceCamera || !(componentInParent?.worldCamera == null)))
			{
				return componentInParent?.worldCamera ?? global::UnityEngine.Camera.main;
			}
			return null;
		}

		private void OnDrawGizmosSelected()
		{
			global::UnityEngine.RectTransform rectTransform = base.transform.parent as global::UnityEngine.RectTransform;
			if (!(rectTransform == null))
			{
				global::UnityEngine.Gizmos.matrix = rectTransform.localToWorldMatrix;
				global::UnityEngine.Vector2 vector = rectTransform.anchoredPosition;
				if (global::UnityEngine.Application.isPlaying)
				{
					vector = m_StartPos;
				}
				global::UnityEngine.Gizmos.color = new global::UnityEngine.Color32(84, 173, 219, byte.MaxValue);
				global::UnityEngine.Vector2 center = vector;
				if (global::UnityEngine.Application.isPlaying && m_Behaviour == global::UnityEngine.InputSystem.OnScreen.OnScreenStick.Behaviour.ExactPositionWithDynamicOrigin)
				{
					center = m_PointerDownPos;
				}
				DrawGizmoCircle(center, m_MovementRange);
				if (m_Behaviour == global::UnityEngine.InputSystem.OnScreen.OnScreenStick.Behaviour.ExactPositionWithDynamicOrigin)
				{
					global::UnityEngine.Gizmos.color = new global::UnityEngine.Color32(158, 84, 219, byte.MaxValue);
					DrawGizmoCircle(vector, m_DynamicOriginRange);
				}
			}
		}

		private void DrawGizmoCircle(global::UnityEngine.Vector2 center, float radius)
		{
			for (int i = 0; i < 32; i++)
			{
				float f = (float)i / 32f * global::System.MathF.PI * 2f;
				float f2 = (float)(i + 1) / 32f * global::System.MathF.PI * 2f;
				global::UnityEngine.Gizmos.DrawLine(new global::UnityEngine.Vector3(center.x + global::UnityEngine.Mathf.Cos(f) * radius, center.y + global::UnityEngine.Mathf.Sin(f) * radius, 0f), new global::UnityEngine.Vector3(center.x + global::UnityEngine.Mathf.Cos(f2) * radius, center.y + global::UnityEngine.Mathf.Sin(f2) * radius, 0f));
			}
		}

		private void UpdateDynamicOriginClickableArea()
		{
			global::UnityEngine.Transform transform = base.transform.Find("DynamicOriginClickable");
			if ((bool)transform)
			{
				((global::UnityEngine.RectTransform)transform).sizeDelta = new global::UnityEngine.Vector2(m_DynamicOriginRange * 2f, m_DynamicOriginRange * 2f);
			}
		}
	}
}
