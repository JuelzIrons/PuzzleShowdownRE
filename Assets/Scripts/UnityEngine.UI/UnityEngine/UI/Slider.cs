namespace UnityEngine.UI
{
	[global::UnityEngine.AddComponentMenu("UI (Canvas)/Slider", 34)]
	[global::UnityEngine.ExecuteAlways]
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.RectTransform))]
	public class Slider : global::UnityEngine.UI.Selectable, global::UnityEngine.EventSystems.IDragHandler, global::UnityEngine.EventSystems.IEventSystemHandler, global::UnityEngine.EventSystems.IInitializePotentialDragHandler, global::UnityEngine.UI.ICanvasElement
	{
		public enum Direction
		{
			LeftToRight = 0,
			RightToLeft = 1,
			BottomToTop = 2,
			TopToBottom = 3
		}

		[global::System.Serializable]
		public class SliderEvent : global::UnityEngine.Events.UnityEvent<float>
		{
		}

		private enum Axis
		{
			Horizontal = 0,
			Vertical = 1
		}

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.RectTransform m_FillRect;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.RectTransform m_HandleRect;

		[global::UnityEngine.Space]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.Slider.Direction m_Direction;

		[global::UnityEngine.SerializeField]
		private float m_MinValue;

		[global::UnityEngine.SerializeField]
		private float m_MaxValue = 1f;

		[global::UnityEngine.SerializeField]
		private bool m_WholeNumbers;

		[global::UnityEngine.SerializeField]
		protected float m_Value;

		[global::UnityEngine.Space]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.Slider.SliderEvent m_OnValueChanged = new global::UnityEngine.UI.Slider.SliderEvent();

		private global::UnityEngine.UI.Image m_FillImage;

		private global::UnityEngine.Transform m_FillTransform;

		private global::UnityEngine.RectTransform m_FillContainerRect;

		private global::UnityEngine.Transform m_HandleTransform;

		private global::UnityEngine.RectTransform m_HandleContainerRect;

		private global::UnityEngine.Vector2 m_Offset = global::UnityEngine.Vector2.zero;

		private global::UnityEngine.DrivenRectTransformTracker m_Tracker;

		private bool m_DelayedUpdateVisuals;

		public global::UnityEngine.RectTransform fillRect
		{
			get
			{
				return m_FillRect;
			}
			set
			{
				if (global::UnityEngine.UI.SetPropertyUtility.SetClass(ref m_FillRect, value))
				{
					UpdateCachedReferences();
					UpdateVisuals();
				}
			}
		}

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

		public global::UnityEngine.UI.Slider.Direction direction
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

		public float minValue
		{
			get
			{
				return m_MinValue;
			}
			set
			{
				if (global::UnityEngine.UI.SetPropertyUtility.SetStruct(ref m_MinValue, value))
				{
					Set(m_Value);
					UpdateVisuals();
				}
			}
		}

		public float maxValue
		{
			get
			{
				return m_MaxValue;
			}
			set
			{
				if (global::UnityEngine.UI.SetPropertyUtility.SetStruct(ref m_MaxValue, value))
				{
					Set(m_Value);
					UpdateVisuals();
				}
			}
		}

		public bool wholeNumbers
		{
			get
			{
				return m_WholeNumbers;
			}
			set
			{
				if (global::UnityEngine.UI.SetPropertyUtility.SetStruct(ref m_WholeNumbers, value))
				{
					Set(m_Value);
					UpdateVisuals();
				}
			}
		}

		public virtual float value
		{
			get
			{
				if (!wholeNumbers)
				{
					return m_Value;
				}
				return global::UnityEngine.Mathf.Round(m_Value);
			}
			set
			{
				Set(value);
			}
		}

		public float normalizedValue
		{
			get
			{
				if (global::UnityEngine.Mathf.Approximately(minValue, maxValue))
				{
					return 0f;
				}
				return global::UnityEngine.Mathf.InverseLerp(minValue, maxValue, value);
			}
			set
			{
				this.value = global::UnityEngine.Mathf.Lerp(minValue, maxValue, value);
			}
		}

		public global::UnityEngine.UI.Slider.SliderEvent onValueChanged
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
				if (!wholeNumbers)
				{
					return (maxValue - minValue) * 0.1f;
				}
				return 1f;
			}
		}

		private global::UnityEngine.UI.Slider.Axis axis
		{
			get
			{
				if (m_Direction != global::UnityEngine.UI.Slider.Direction.LeftToRight && m_Direction != global::UnityEngine.UI.Slider.Direction.RightToLeft)
				{
					return global::UnityEngine.UI.Slider.Axis.Vertical;
				}
				return global::UnityEngine.UI.Slider.Axis.Horizontal;
			}
		}

		private bool reverseValue
		{
			get
			{
				if (m_Direction != global::UnityEngine.UI.Slider.Direction.RightToLeft)
				{
					return m_Direction == global::UnityEngine.UI.Slider.Direction.TopToBottom;
				}
				return true;
			}
		}

		global::UnityEngine.Transform global::UnityEngine.UI.ICanvasElement.transform => base.transform;

		public virtual void SetValueWithoutNotify(float input)
		{
			Set(input, sendCallback: false);
		}

		protected Slider()
		{
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
				Set(m_Value, sendCallback: false);
				UpdateVisuals();
			}
		}

		protected override void OnDidApplyAnimationProperties()
		{
			m_Value = ClampValue(m_Value);
			float num = normalizedValue;
			if (m_FillContainerRect != null)
			{
				num = ((!(m_FillImage != null) || m_FillImage.type != global::UnityEngine.UI.Image.Type.Filled) ? (reverseValue ? (1f - m_FillRect.anchorMin[(int)axis]) : m_FillRect.anchorMax[(int)axis]) : m_FillImage.fillAmount);
			}
			else if (m_HandleContainerRect != null)
			{
				num = (reverseValue ? (1f - m_HandleRect.anchorMin[(int)axis]) : m_HandleRect.anchorMin[(int)axis]);
			}
			UpdateVisuals();
			if (num != normalizedValue)
			{
				global::UnityEngine.UISystemProfilerApi.AddMarker("Slider.value", this);
				onValueChanged.Invoke(m_Value);
			}
			base.OnDidApplyAnimationProperties();
		}

		private void UpdateCachedReferences()
		{
			if ((bool)m_FillRect && m_FillRect != (global::UnityEngine.RectTransform)base.transform)
			{
				m_FillTransform = m_FillRect.transform;
				m_FillImage = m_FillRect.GetComponent<global::UnityEngine.UI.Image>();
				if (m_FillTransform.parent != null)
				{
					m_FillContainerRect = m_FillTransform.parent.GetComponent<global::UnityEngine.RectTransform>();
				}
			}
			else
			{
				m_FillRect = null;
				m_FillContainerRect = null;
				m_FillImage = null;
			}
			if ((bool)m_HandleRect && m_HandleRect != (global::UnityEngine.RectTransform)base.transform)
			{
				m_HandleTransform = m_HandleRect.transform;
				if (m_HandleTransform.parent != null)
				{
					m_HandleContainerRect = m_HandleTransform.parent.GetComponent<global::UnityEngine.RectTransform>();
				}
			}
			else
			{
				m_HandleRect = null;
				m_HandleContainerRect = null;
			}
		}

		private float ClampValue(float input)
		{
			float num = global::UnityEngine.Mathf.Clamp(input, minValue, maxValue);
			if (wholeNumbers)
			{
				num = global::UnityEngine.Mathf.Round(num);
			}
			return num;
		}

		protected virtual void Set(float input, bool sendCallback = true)
		{
			float num = ClampValue(input);
			if (m_Value != num)
			{
				m_Value = num;
				MarkDirty();
				UpdateVisuals();
				if (sendCallback)
				{
					global::UnityEngine.UISystemProfilerApi.AddMarker("Slider.value", this);
					m_OnValueChanged.Invoke(num);
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
			if (m_FillContainerRect != null)
			{
				m_Tracker.Add(this, m_FillRect, global::UnityEngine.DrivenTransformProperties.Anchors);
				global::UnityEngine.Vector2 zero = global::UnityEngine.Vector2.zero;
				global::UnityEngine.Vector2 one = global::UnityEngine.Vector2.one;
				if (m_FillImage != null && m_FillImage.type == global::UnityEngine.UI.Image.Type.Filled)
				{
					m_FillImage.fillAmount = normalizedValue;
				}
				else if (reverseValue)
				{
					zero[(int)axis] = 1f - normalizedValue;
				}
				else
				{
					one[(int)axis] = normalizedValue;
				}
				m_FillRect.anchorMin = zero;
				m_FillRect.anchorMax = one;
			}
			if (m_HandleContainerRect != null)
			{
				m_Tracker.Add(this, m_HandleRect, global::UnityEngine.DrivenTransformProperties.Anchors);
				global::UnityEngine.Vector2 zero2 = global::UnityEngine.Vector2.zero;
				global::UnityEngine.Vector2 one2 = global::UnityEngine.Vector2.one;
				global::UnityEngine.UI.Slider.Axis index = axis;
				float num = (one2[(int)axis] = (reverseValue ? (1f - normalizedValue) : normalizedValue));
				zero2[(int)index] = num;
				m_HandleRect.anchorMin = zero2;
				m_HandleRect.anchorMax = one2;
			}
		}

		private void UpdateDrag(global::UnityEngine.EventSystems.PointerEventData eventData, global::UnityEngine.Camera cam)
		{
			global::UnityEngine.RectTransform rectTransform = m_HandleContainerRect ?? m_FillContainerRect;
			if (rectTransform != null && rectTransform.rect.size[(int)axis] > 0f)
			{
				global::UnityEngine.Vector2 position = global::UnityEngine.Vector2.zero;
				if (global::UnityEngine.UI.MultipleDisplayUtilities.GetRelativeMousePositionForDrag(eventData, ref position) && global::UnityEngine.RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, position, cam, out var localPoint))
				{
					localPoint -= rectTransform.rect.position;
					float num = global::UnityEngine.Mathf.Clamp01((localPoint - m_Offset)[(int)axis] / rectTransform.rect.size[(int)axis]);
					normalizedValue = (reverseValue ? (1f - num) : num);
				}
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

		public override void OnPointerDown(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (!MayDrag(eventData))
			{
				return;
			}
			base.OnPointerDown(eventData);
			m_Offset = global::UnityEngine.Vector2.zero;
			if (m_HandleContainerRect != null && global::UnityEngine.RectTransformUtility.RectangleContainsScreenPoint(m_HandleRect, eventData.pointerPressRaycast.screenPosition, eventData.enterEventCamera))
			{
				if (global::UnityEngine.RectTransformUtility.ScreenPointToLocalPointInRectangle(m_HandleRect, eventData.pointerPressRaycast.screenPosition, eventData.pressEventCamera, out var localPoint))
				{
					m_Offset = localPoint;
				}
			}
			else
			{
				UpdateDrag(eventData, eventData.pressEventCamera);
			}
		}

		public virtual void OnDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (MayDrag(eventData))
			{
				UpdateDrag(eventData, eventData.pressEventCamera);
			}
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
				if (axis == global::UnityEngine.UI.Slider.Axis.Horizontal && FindSelectableOnLeft() == null)
				{
					Set(reverseValue ? (value + stepSize) : (value - stepSize));
				}
				else
				{
					base.OnMove(eventData);
				}
				break;
			case global::UnityEngine.EventSystems.MoveDirection.Right:
				if (axis == global::UnityEngine.UI.Slider.Axis.Horizontal && FindSelectableOnRight() == null)
				{
					Set(reverseValue ? (value - stepSize) : (value + stepSize));
				}
				else
				{
					base.OnMove(eventData);
				}
				break;
			case global::UnityEngine.EventSystems.MoveDirection.Up:
				if (axis == global::UnityEngine.UI.Slider.Axis.Vertical && FindSelectableOnUp() == null)
				{
					Set(reverseValue ? (value - stepSize) : (value + stepSize));
				}
				else
				{
					base.OnMove(eventData);
				}
				break;
			case global::UnityEngine.EventSystems.MoveDirection.Down:
				if (axis == global::UnityEngine.UI.Slider.Axis.Vertical && FindSelectableOnDown() == null)
				{
					Set(reverseValue ? (value + stepSize) : (value - stepSize));
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
			if (base.navigation.mode == global::UnityEngine.UI.Navigation.Mode.Automatic && axis == global::UnityEngine.UI.Slider.Axis.Horizontal)
			{
				return null;
			}
			return base.FindSelectableOnLeft();
		}

		public override global::UnityEngine.UI.Selectable FindSelectableOnRight()
		{
			if (base.navigation.mode == global::UnityEngine.UI.Navigation.Mode.Automatic && axis == global::UnityEngine.UI.Slider.Axis.Horizontal)
			{
				return null;
			}
			return base.FindSelectableOnRight();
		}

		public override global::UnityEngine.UI.Selectable FindSelectableOnUp()
		{
			if (base.navigation.mode == global::UnityEngine.UI.Navigation.Mode.Automatic && axis == global::UnityEngine.UI.Slider.Axis.Vertical)
			{
				return null;
			}
			return base.FindSelectableOnUp();
		}

		public override global::UnityEngine.UI.Selectable FindSelectableOnDown()
		{
			if (base.navigation.mode == global::UnityEngine.UI.Navigation.Mode.Automatic && axis == global::UnityEngine.UI.Slider.Axis.Vertical)
			{
				return null;
			}
			return base.FindSelectableOnDown();
		}

		public virtual void OnInitializePotentialDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			eventData.useDragThreshold = false;
		}

		public void SetDirection(global::UnityEngine.UI.Slider.Direction direction, bool includeRectLayouts)
		{
			global::UnityEngine.UI.Slider.Axis axis = this.axis;
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
