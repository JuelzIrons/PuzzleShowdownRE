namespace UnityEngine.UI
{
	[global::UnityEngine.AddComponentMenu("UI (Canvas)/Scrollbar", 36)]
	[global::UnityEngine.ExecuteAlways]
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.RectTransform))]
	public class Scrollbar : global::UnityEngine.UI.Selectable, global::UnityEngine.EventSystems.IBeginDragHandler, global::UnityEngine.EventSystems.IEventSystemHandler, global::UnityEngine.EventSystems.IDragHandler, global::UnityEngine.EventSystems.IInitializePotentialDragHandler, global::UnityEngine.UI.ICanvasElement
	{
		public enum Direction
		{
			LeftToRight = 0,
			RightToLeft = 1,
			BottomToTop = 2,
			TopToBottom = 3
		}

		[global::System.Serializable]
		public class ScrollEvent : global::UnityEngine.Events.UnityEvent<float>
		{
		}

		private enum Axis
		{
			Horizontal = 0,
			Vertical = 1
		}

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.RectTransform m_HandleRect;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.Scrollbar.Direction m_Direction;

		[global::UnityEngine.Range(0f, 1f)]
		[global::UnityEngine.SerializeField]
		private float m_Value;

		[global::UnityEngine.Range(0f, 1f)]
		[global::UnityEngine.SerializeField]
		private float m_Size = 0.2f;

		[global::UnityEngine.Range(0f, 11f)]
		[global::UnityEngine.SerializeField]
		private int m_NumberOfSteps;

		[global::UnityEngine.Space(6f)]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.Scrollbar.ScrollEvent m_OnValueChanged = new global::UnityEngine.UI.Scrollbar.ScrollEvent();

		private global::UnityEngine.RectTransform m_ContainerRect;

		private global::UnityEngine.Vector2 m_Offset = global::UnityEngine.Vector2.zero;

		private global::UnityEngine.DrivenRectTransformTracker m_Tracker;

		private global::UnityEngine.Coroutine m_PointerDownRepeat;

		private bool isPointerDownAndNotDragging;

		private bool m_DelayedUpdateVisuals;

		public global::UnityEngine.RectTransform handleRect
		{
			get
			{
				return m_HandleRect;
			}
			set
			{
				if (global::UnityEngine.UI.SetPropertyUtility.SetClass(ref m_HandleRect, value))
				{
					UpdateCachedReferences();
					UpdateVisuals();
				}
			}
		}

		public global::UnityEngine.UI.Scrollbar.Direction direction
		{
			get
			{
				return m_Direction;
			}
			set
			{
				if (global::UnityEngine.UI.SetPropertyUtility.SetStruct(ref m_Direction, value))
				{
					UpdateVisuals();
				}
			}
		}

		public float value
		{
			get
			{
				float num = m_Value;
				if (m_NumberOfSteps > 1)
				{
					num = global::UnityEngine.Mathf.Round(num * (float)(m_NumberOfSteps - 1)) / (float)(m_NumberOfSteps - 1);
				}
				return num;
			}
			set
			{
				Set(value);
			}
		}

		public float size
		{
			get
			{
				return m_Size;
			}
			set
			{
				if (global::UnityEngine.UI.SetPropertyUtility.SetStruct(ref m_Size, global::UnityEngine.Mathf.Clamp01(value)))
				{
					UpdateVisuals();
				}
			}
		}

		public int numberOfSteps
		{
			get
			{
				return m_NumberOfSteps;
			}
			set
			{
				if (global::UnityEngine.UI.SetPropertyUtility.SetStruct(ref m_NumberOfSteps, value))
				{
					Set(m_Value);
					UpdateVisuals();
				}
			}
		}

		public global::UnityEngine.UI.Scrollbar.ScrollEvent onValueChanged
		{
			get
			{
				return m_OnValueChanged;
			}
			set
			{
				m_OnValueChanged = value;
			}
		}

		private float stepSize
		{
			get
			{
				if (m_NumberOfSteps <= 1)
				{
					return 0.1f;
				}
				return 1f / (float)(m_NumberOfSteps - 1);
			}
		}

		private global::UnityEngine.UI.Scrollbar.Axis axis
		{
			get
			{
				if (m_Direction != global::UnityEngine.UI.Scrollbar.Direction.LeftToRight && m_Direction != global::UnityEngine.UI.Scrollbar.Direction.RightToLeft)
				{
					return global::UnityEngine.UI.Scrollbar.Axis.Vertical;
				}
				return global::UnityEngine.UI.Scrollbar.Axis.Horizontal;
			}
		}

		private bool reverseValue
		{
			get
			{
				if (m_Direction != global::UnityEngine.UI.Scrollbar.Direction.RightToLeft)
				{
					return m_Direction == global::UnityEngine.UI.Scrollbar.Direction.TopToBottom;
				}
				return true;
			}
		}

		global::UnityEngine.Transform global::UnityEngine.UI.ICanvasElement.transform => base.transform;

		protected Scrollbar()
		{
		}

		public virtual void SetValueWithoutNotify(float input)
		{
			Set(input, sendCallback: false);
		}

		public virtual void Rebuild(global::UnityEngine.UI.CanvasUpdate executing)
		{
		}

		public virtual void LayoutComplete()
		{
		}

		public virtual void GraphicUpdateComplete()
		{
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			UpdateCachedReferences();
			Set(m_Value, sendCallback: false);
			UpdateVisuals();
		}

		protected override void OnDisable()
		{
			m_Tracker.Clear();
			base.OnDisable();
		}

		protected virtual void Update()
		{
			if (m_DelayedUpdateVisuals)
			{
				m_DelayedUpdateVisuals = false;
				UpdateVisuals();
			}
		}

		private void UpdateCachedReferences()
		{
			if ((bool)m_HandleRect && m_HandleRect.parent != null)
			{
				m_ContainerRect = m_HandleRect.parent.GetComponent<global::UnityEngine.RectTransform>();
			}
			else
			{
				m_ContainerRect = null;
			}
		}

		private void Set(float input, bool sendCallback = true)
		{
			float num = m_Value;
			m_Value = input;
			if (num != value)
			{
				UpdateVisuals();
				if (sendCallback)
				{
					global::UnityEngine.UISystemProfilerApi.AddMarker("Scrollbar.value", this);
					m_OnValueChanged.Invoke(value);
				}
			}
		}

		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			if (IsActive())
			{
				UpdateVisuals();
			}
		}

		private void UpdateVisuals()
		{
			m_Tracker.Clear();
			if (m_ContainerRect != null)
			{
				m_Tracker.Add(this, m_HandleRect, global::UnityEngine.DrivenTransformProperties.Anchors);
				global::UnityEngine.Vector2 zero = global::UnityEngine.Vector2.zero;
				global::UnityEngine.Vector2 one = global::UnityEngine.Vector2.one;
				float num = global::UnityEngine.Mathf.Clamp01(value) * (1f - size);
				if (reverseValue)
				{
					zero[(int)axis] = 1f - num - size;
					one[(int)axis] = 1f - num;
				}
				else
				{
					zero[(int)axis] = num;
					one[(int)axis] = num + size;
				}
				m_HandleRect.anchorMin = zero;
				m_HandleRect.anchorMax = one;
			}
		}

		private void UpdateDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (eventData.button == global::UnityEngine.EventSystems.PointerEventData.InputButton.Left && !(m_ContainerRect == null))
			{
				global::UnityEngine.Vector2 position = global::UnityEngine.Vector2.zero;
				if (global::UnityEngine.UI.MultipleDisplayUtilities.GetRelativeMousePositionForDrag(eventData, ref position))
				{
					UpdateDrag(m_ContainerRect, position, eventData.pressEventCamera);
				}
			}
		}

		private void UpdateDrag(global::UnityEngine.RectTransform containerRect, global::UnityEngine.Vector2 position, global::UnityEngine.Camera camera)
		{
			if (global::UnityEngine.RectTransformUtility.ScreenPointToLocalPointInRectangle(containerRect, position, camera, out var localPoint))
			{
				global::UnityEngine.Vector2 handleCorner = localPoint - m_Offset - m_ContainerRect.rect.position - (m_HandleRect.rect.size - m_HandleRect.sizeDelta) * 0.5f;
				float num = ((axis == global::UnityEngine.UI.Scrollbar.Axis.Horizontal) ? m_ContainerRect.rect.width : m_ContainerRect.rect.height) * (1f - size);
				if (!(num <= 0f))
				{
					DoUpdateDrag(handleCorner, num);
				}
			}
		}

		private void DoUpdateDrag(global::UnityEngine.Vector2 handleCorner, float remainingSize)
		{
			switch (m_Direction)
			{
			case global::UnityEngine.UI.Scrollbar.Direction.LeftToRight:
				Set(global::UnityEngine.Mathf.Clamp01(handleCorner.x / remainingSize));
				break;
			case global::UnityEngine.UI.Scrollbar.Direction.RightToLeft:
				Set(global::UnityEngine.Mathf.Clamp01(1f - handleCorner.x / remainingSize));
				break;
			case global::UnityEngine.UI.Scrollbar.Direction.BottomToTop:
				Set(global::UnityEngine.Mathf.Clamp01(handleCorner.y / remainingSize));
				break;
			case global::UnityEngine.UI.Scrollbar.Direction.TopToBottom:
				Set(global::UnityEngine.Mathf.Clamp01(1f - handleCorner.y / remainingSize));
				break;
			}
		}

		private bool MayDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (IsActive() && IsInteractable())
			{
				return eventData.button == global::UnityEngine.EventSystems.PointerEventData.InputButton.Left;
			}
			return false;
		}

		public virtual void OnBeginDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			isPointerDownAndNotDragging = false;
			if (MayDrag(eventData) && !(m_ContainerRect == null))
			{
				m_Offset = global::UnityEngine.Vector2.zero;
				if (global::UnityEngine.RectTransformUtility.RectangleContainsScreenPoint(m_HandleRect, eventData.pointerPressRaycast.screenPosition, eventData.enterEventCamera) && global::UnityEngine.RectTransformUtility.ScreenPointToLocalPointInRectangle(m_HandleRect, eventData.pointerPressRaycast.screenPosition, eventData.pressEventCamera, out var localPoint))
				{
					m_Offset = localPoint - m_HandleRect.rect.center;
				}
			}
		}

		public virtual void OnDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (MayDrag(eventData) && m_ContainerRect != null)
			{
				UpdateDrag(eventData);
			}
		}

		public override void OnPointerDown(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (MayDrag(eventData))
			{
				base.OnPointerDown(eventData);
				isPointerDownAndNotDragging = true;
				m_PointerDownRepeat = StartCoroutine(ClickRepeat(eventData.pointerPressRaycast.screenPosition, eventData.enterEventCamera));
			}
		}

		protected global::System.Collections.IEnumerator ClickRepeat(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			return ClickRepeat(eventData.pointerPressRaycast.screenPosition, eventData.enterEventCamera);
		}

		protected global::System.Collections.IEnumerator ClickRepeat(global::UnityEngine.Vector2 screenPosition, global::UnityEngine.Camera camera)
		{
			while (isPointerDownAndNotDragging)
			{
				if (!global::UnityEngine.RectTransformUtility.RectangleContainsScreenPoint(m_HandleRect, screenPosition, camera))
				{
					UpdateDrag(m_ContainerRect, screenPosition, camera);
				}
				yield return new global::UnityEngine.WaitForEndOfFrame();
			}
			StopCoroutine(m_PointerDownRepeat);
		}

		public override void OnPointerUp(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			base.OnPointerUp(eventData);
			isPointerDownAndNotDragging = false;
		}

		public override void OnMove(global::UnityEngine.EventSystems.AxisEventData eventData)
		{
			if (!IsActive() || !IsInteractable())
			{
				base.OnMove(eventData);
				return;
			}
			switch (eventData.moveDir)
			{
			case global::UnityEngine.EventSystems.MoveDirection.Left:
				if (axis == global::UnityEngine.UI.Scrollbar.Axis.Horizontal && FindSelectableOnLeft() == null)
				{
					Set(global::UnityEngine.Mathf.Clamp01(reverseValue ? (value + stepSize) : (value - stepSize)));
				}
				else
				{
					base.OnMove(eventData);
				}
				break;
			case global::UnityEngine.EventSystems.MoveDirection.Right:
				if (axis == global::UnityEngine.UI.Scrollbar.Axis.Horizontal && FindSelectableOnRight() == null)
				{
					Set(global::UnityEngine.Mathf.Clamp01(reverseValue ? (value - stepSize) : (value + stepSize)));
				}
				else
				{
					base.OnMove(eventData);
				}
				break;
			case global::UnityEngine.EventSystems.MoveDirection.Up:
				if (axis == global::UnityEngine.UI.Scrollbar.Axis.Vertical && FindSelectableOnUp() == null)
				{
					Set(global::UnityEngine.Mathf.Clamp01(reverseValue ? (value - stepSize) : (value + stepSize)));
				}
				else
				{
					base.OnMove(eventData);
				}
				break;
			case global::UnityEngine.EventSystems.MoveDirection.Down:
				if (axis == global::UnityEngine.UI.Scrollbar.Axis.Vertical && FindSelectableOnDown() == null)
				{
					Set(global::UnityEngine.Mathf.Clamp01(reverseValue ? (value + stepSize) : (value - stepSize)));
				}
				else
				{
					base.OnMove(eventData);
				}
				break;
			}
		}

		public override global::UnityEngine.UI.Selectable FindSelectableOnLeft()
		{
			if (base.navigation.mode == global::UnityEngine.UI.Navigation.Mode.Automatic && axis == global::UnityEngine.UI.Scrollbar.Axis.Horizontal)
			{
				return null;
			}
			return base.FindSelectableOnLeft();
		}

		public override global::UnityEngine.UI.Selectable FindSelectableOnRight()
		{
			if (base.navigation.mode == global::UnityEngine.UI.Navigation.Mode.Automatic && axis == global::UnityEngine.UI.Scrollbar.Axis.Horizontal)
			{
				return null;
			}
			return base.FindSelectableOnRight();
		}

		public override global::UnityEngine.UI.Selectable FindSelectableOnUp()
		{
			if (base.navigation.mode == global::UnityEngine.UI.Navigation.Mode.Automatic && axis == global::UnityEngine.UI.Scrollbar.Axis.Vertical)
			{
				return null;
			}
			return base.FindSelectableOnUp();
		}

		public override global::UnityEngine.UI.Selectable FindSelectableOnDown()
		{
			if (base.navigation.mode == global::UnityEngine.UI.Navigation.Mode.Automatic && axis == global::UnityEngine.UI.Scrollbar.Axis.Vertical)
			{
				return null;
			}
			return base.FindSelectableOnDown();
		}

		public virtual void OnInitializePotentialDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			eventData.useDragThreshold = false;
		}

		public void SetDirection(global::UnityEngine.UI.Scrollbar.Direction direction, bool includeRectLayouts)
		{
			global::UnityEngine.UI.Scrollbar.Axis axis = this.axis;
			bool flag = reverseValue;
			this.direction = direction;
			if (includeRectLayouts)
			{
				if (this.axis != axis)
				{
					global::UnityEngine.RectTransformUtility.FlipLayoutAxes(base.transform as global::UnityEngine.RectTransform, keepPositioning: true, recursive: true);
				}
				if (reverseValue != flag)
				{
					global::UnityEngine.RectTransformUtility.FlipLayoutOnAxis(base.transform as global::UnityEngine.RectTransform, (int)this.axis, keepPositioning: true, recursive: true);
				}
			}
		}
	}
}
