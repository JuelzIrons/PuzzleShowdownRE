namespace UnityEngine.UI
{
	[global::UnityEngine.AddComponentMenu("UI (Canvas)/Scroll Rect", 37)]
	[global::UnityEngine.SelectionBase]
	[global::UnityEngine.ExecuteAlways]
	[global::UnityEngine.DisallowMultipleComponent]
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.RectTransform))]
	public class ScrollRect : global::UnityEngine.EventSystems.UIBehaviour, global::UnityEngine.EventSystems.IInitializePotentialDragHandler, global::UnityEngine.EventSystems.IEventSystemHandler, global::UnityEngine.EventSystems.IBeginDragHandler, global::UnityEngine.EventSystems.IEndDragHandler, global::UnityEngine.EventSystems.IDragHandler, global::UnityEngine.EventSystems.IScrollHandler, global::UnityEngine.UI.ICanvasElement, global::UnityEngine.UI.ILayoutElement, global::UnityEngine.UI.ILayoutGroup, global::UnityEngine.UI.ILayoutController
	{
		public enum MovementType
		{
			Unrestricted = 0,
			Elastic = 1,
			Clamped = 2
		}

		public enum ScrollbarVisibility
		{
			Permanent = 0,
			AutoHide = 1,
			AutoHideAndExpandViewport = 2
		}

		[global::System.Serializable]
		public class ScrollRectEvent : global::UnityEngine.Events.UnityEvent<global::UnityEngine.Vector2>
		{
		}

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.RectTransform m_Content;

		[global::UnityEngine.SerializeField]
		private bool m_Horizontal = true;

		[global::UnityEngine.SerializeField]
		private bool m_Vertical = true;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.ScrollRect.MovementType m_MovementType = global::UnityEngine.UI.ScrollRect.MovementType.Elastic;

		[global::UnityEngine.SerializeField]
		private float m_Elasticity = 0.1f;

		[global::UnityEngine.SerializeField]
		private bool m_Inertia = true;

		[global::UnityEngine.SerializeField]
		private float m_DecelerationRate = 0.135f;

		[global::UnityEngine.SerializeField]
		private float m_ScrollSensitivity = 1f;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.RectTransform m_Viewport;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.Scrollbar m_HorizontalScrollbar;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.Scrollbar m_VerticalScrollbar;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.ScrollRect.ScrollbarVisibility m_HorizontalScrollbarVisibility;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.ScrollRect.ScrollbarVisibility m_VerticalScrollbarVisibility;

		[global::UnityEngine.SerializeField]
		private float m_HorizontalScrollbarSpacing;

		[global::UnityEngine.SerializeField]
		private float m_VerticalScrollbarSpacing;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.ScrollRect.ScrollRectEvent m_OnValueChanged = new global::UnityEngine.UI.ScrollRect.ScrollRectEvent();

		private global::UnityEngine.Vector2 m_PointerStartLocalCursor = global::UnityEngine.Vector2.zero;

		protected global::UnityEngine.Vector2 m_ContentStartPosition = global::UnityEngine.Vector2.zero;

		private global::UnityEngine.RectTransform m_ViewRect;

		protected global::UnityEngine.Bounds m_ContentBounds;

		private global::UnityEngine.Bounds m_ViewBounds;

		private global::UnityEngine.Vector2 m_Velocity;

		private bool m_Dragging;

		private bool m_Scrolling;

		private global::UnityEngine.Vector2 m_PrevPosition = global::UnityEngine.Vector2.zero;

		private global::UnityEngine.Bounds m_PrevContentBounds;

		private global::UnityEngine.Bounds m_PrevViewBounds;

		[global::System.NonSerialized]
		private bool m_HasRebuiltLayout;

		private bool m_HSliderExpand;

		private bool m_VSliderExpand;

		private float m_HSliderHeight;

		private float m_VSliderWidth;

		[global::System.NonSerialized]
		private global::UnityEngine.RectTransform m_Rect;

		private global::UnityEngine.RectTransform m_HorizontalScrollbarRect;

		private global::UnityEngine.RectTransform m_VerticalScrollbarRect;

		private global::UnityEngine.DrivenRectTransformTracker m_Tracker;

		private readonly global::UnityEngine.Vector3[] m_Corners = new global::UnityEngine.Vector3[4];

		public global::UnityEngine.RectTransform content
		{
			get
			{
				return m_Content;
			}
			set
			{
				m_Content = value;
			}
		}

		public bool horizontal
		{
			get
			{
				return m_Horizontal;
			}
			set
			{
				m_Horizontal = value;
			}
		}

		public bool vertical
		{
			get
			{
				return m_Vertical;
			}
			set
			{
				m_Vertical = value;
			}
		}

		public global::UnityEngine.UI.ScrollRect.MovementType movementType
		{
			get
			{
				return m_MovementType;
			}
			set
			{
				m_MovementType = value;
			}
		}

		public float elasticity
		{
			get
			{
				return m_Elasticity;
			}
			set
			{
				m_Elasticity = value;
			}
		}

		public bool inertia
		{
			get
			{
				return m_Inertia;
			}
			set
			{
				m_Inertia = value;
			}
		}

		public float decelerationRate
		{
			get
			{
				return m_DecelerationRate;
			}
			set
			{
				m_DecelerationRate = value;
			}
		}

		public float scrollSensitivity
		{
			get
			{
				return m_ScrollSensitivity;
			}
			set
			{
				m_ScrollSensitivity = value;
			}
		}

		public global::UnityEngine.RectTransform viewport
		{
			get
			{
				return m_Viewport;
			}
			set
			{
				m_Viewport = value;
				SetDirtyCaching();
			}
		}

		public global::UnityEngine.UI.Scrollbar horizontalScrollbar
		{
			get
			{
				return m_HorizontalScrollbar;
			}
			set
			{
				if ((bool)m_HorizontalScrollbar)
				{
					m_HorizontalScrollbar.onValueChanged.RemoveListener(SetHorizontalNormalizedPosition);
				}
				m_HorizontalScrollbar = value;
				if (m_Horizontal && (bool)m_HorizontalScrollbar)
				{
					m_HorizontalScrollbar.onValueChanged.AddListener(SetHorizontalNormalizedPosition);
				}
				SetDirtyCaching();
			}
		}

		public global::UnityEngine.UI.Scrollbar verticalScrollbar
		{
			get
			{
				return m_VerticalScrollbar;
			}
			set
			{
				if ((bool)m_VerticalScrollbar)
				{
					m_VerticalScrollbar.onValueChanged.RemoveListener(SetVerticalNormalizedPosition);
				}
				m_VerticalScrollbar = value;
				if (m_Vertical && (bool)m_VerticalScrollbar)
				{
					m_VerticalScrollbar.onValueChanged.AddListener(SetVerticalNormalizedPosition);
				}
				SetDirtyCaching();
			}
		}

		public global::UnityEngine.UI.ScrollRect.ScrollbarVisibility horizontalScrollbarVisibility
		{
			get
			{
				return m_HorizontalScrollbarVisibility;
			}
			set
			{
				m_HorizontalScrollbarVisibility = value;
				SetDirtyCaching();
			}
		}

		public global::UnityEngine.UI.ScrollRect.ScrollbarVisibility verticalScrollbarVisibility
		{
			get
			{
				return m_VerticalScrollbarVisibility;
			}
			set
			{
				m_VerticalScrollbarVisibility = value;
				SetDirtyCaching();
			}
		}

		public float horizontalScrollbarSpacing
		{
			get
			{
				return m_HorizontalScrollbarSpacing;
			}
			set
			{
				m_HorizontalScrollbarSpacing = value;
				SetDirty();
			}
		}

		public float verticalScrollbarSpacing
		{
			get
			{
				return m_VerticalScrollbarSpacing;
			}
			set
			{
				m_VerticalScrollbarSpacing = value;
				SetDirty();
			}
		}

		public global::UnityEngine.UI.ScrollRect.ScrollRectEvent onValueChanged
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

		protected global::UnityEngine.RectTransform viewRect
		{
			get
			{
				if (m_ViewRect == null)
				{
					m_ViewRect = m_Viewport;
				}
				if (m_ViewRect == null)
				{
					m_ViewRect = (global::UnityEngine.RectTransform)base.transform;
				}
				return m_ViewRect;
			}
		}

		public global::UnityEngine.Vector2 velocity
		{
			get
			{
				return m_Velocity;
			}
			set
			{
				m_Velocity = value;
			}
		}

		private global::UnityEngine.RectTransform rectTransform
		{
			get
			{
				if (m_Rect == null)
				{
					m_Rect = GetComponent<global::UnityEngine.RectTransform>();
				}
				return m_Rect;
			}
		}

		public global::UnityEngine.Vector2 normalizedPosition
		{
			get
			{
				return new global::UnityEngine.Vector2(horizontalNormalizedPosition, verticalNormalizedPosition);
			}
			set
			{
				SetNormalizedPosition(value.x, 0);
				SetNormalizedPosition(value.y, 1);
			}
		}

		public float horizontalNormalizedPosition
		{
			get
			{
				UpdateBounds();
				if (m_ContentBounds.size.x <= m_ViewBounds.size.x || global::UnityEngine.Mathf.Approximately(m_ContentBounds.size.x, m_ViewBounds.size.x))
				{
					return (m_ViewBounds.min.x > m_ContentBounds.min.x) ? 1 : 0;
				}
				return (m_ViewBounds.min.x - m_ContentBounds.min.x) / (m_ContentBounds.size.x - m_ViewBounds.size.x);
			}
			set
			{
				SetNormalizedPosition(value, 0);
			}
		}

		public float verticalNormalizedPosition
		{
			get
			{
				UpdateBounds();
				if (m_ContentBounds.size.y <= m_ViewBounds.size.y || global::UnityEngine.Mathf.Approximately(m_ContentBounds.size.y, m_ViewBounds.size.y))
				{
					return (m_ViewBounds.min.y > m_ContentBounds.min.y) ? 1 : 0;
				}
				return (m_ViewBounds.min.y - m_ContentBounds.min.y) / (m_ContentBounds.size.y - m_ViewBounds.size.y);
			}
			set
			{
				SetNormalizedPosition(value, 1);
			}
		}

		private bool hScrollingNeeded
		{
			get
			{
				if (global::UnityEngine.Application.isPlaying)
				{
					return m_ContentBounds.size.x > m_ViewBounds.size.x + 0.01f;
				}
				return true;
			}
		}

		private bool vScrollingNeeded
		{
			get
			{
				if (global::UnityEngine.Application.isPlaying)
				{
					return m_ContentBounds.size.y > m_ViewBounds.size.y + 0.01f;
				}
				return true;
			}
		}

		public virtual float minWidth => -1f;

		public virtual float preferredWidth => -1f;

		public virtual float flexibleWidth => -1f;

		public virtual float minHeight => -1f;

		public virtual float preferredHeight => -1f;

		public virtual float flexibleHeight => -1f;

		public virtual int layoutPriority => -1;

		global::UnityEngine.Transform global::UnityEngine.UI.ICanvasElement.transform => base.transform;

		protected ScrollRect()
		{
		}

		public virtual void Rebuild(global::UnityEngine.UI.CanvasUpdate executing)
		{
			if (executing == global::UnityEngine.UI.CanvasUpdate.Prelayout)
			{
				UpdateCachedData();
			}
			if (executing == global::UnityEngine.UI.CanvasUpdate.PostLayout)
			{
				UpdateBounds();
				UpdateScrollbars(global::UnityEngine.Vector2.zero);
				UpdatePrevData();
				m_HasRebuiltLayout = true;
			}
		}

		public virtual void LayoutComplete()
		{
		}

		public virtual void GraphicUpdateComplete()
		{
		}

		private void UpdateCachedData()
		{
			global::UnityEngine.Transform transform = base.transform;
			m_HorizontalScrollbarRect = ((m_HorizontalScrollbar == null) ? null : (m_HorizontalScrollbar.transform as global::UnityEngine.RectTransform));
			m_VerticalScrollbarRect = ((m_VerticalScrollbar == null) ? null : (m_VerticalScrollbar.transform as global::UnityEngine.RectTransform));
			bool num = viewRect.parent == transform;
			bool flag = !m_HorizontalScrollbarRect || m_HorizontalScrollbarRect.parent == transform;
			bool flag2 = !m_VerticalScrollbarRect || m_VerticalScrollbarRect.parent == transform;
			bool flag3 = num && flag && flag2;
			m_HSliderExpand = flag3 && (bool)m_HorizontalScrollbarRect && horizontalScrollbarVisibility == global::UnityEngine.UI.ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
			m_VSliderExpand = flag3 && (bool)m_VerticalScrollbarRect && verticalScrollbarVisibility == global::UnityEngine.UI.ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
			m_HSliderHeight = ((m_HorizontalScrollbarRect == null) ? 0f : m_HorizontalScrollbarRect.rect.height);
			m_VSliderWidth = ((m_VerticalScrollbarRect == null) ? 0f : m_VerticalScrollbarRect.rect.width);
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			if (m_Horizontal && (bool)m_HorizontalScrollbar)
			{
				m_HorizontalScrollbar.onValueChanged.AddListener(SetHorizontalNormalizedPosition);
			}
			if (m_Vertical && (bool)m_VerticalScrollbar)
			{
				m_VerticalScrollbar.onValueChanged.AddListener(SetVerticalNormalizedPosition);
			}
			global::UnityEngine.UI.CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(this);
			SetDirty();
		}

		protected override void OnDisable()
		{
			global::UnityEngine.UI.CanvasUpdateRegistry.UnRegisterCanvasElementForRebuild(this);
			if ((bool)m_HorizontalScrollbar)
			{
				m_HorizontalScrollbar.onValueChanged.RemoveListener(SetHorizontalNormalizedPosition);
			}
			if ((bool)m_VerticalScrollbar)
			{
				m_VerticalScrollbar.onValueChanged.RemoveListener(SetVerticalNormalizedPosition);
			}
			m_Dragging = false;
			m_Scrolling = false;
			m_HasRebuiltLayout = false;
			m_Tracker.Clear();
			m_Velocity = global::UnityEngine.Vector2.zero;
			global::UnityEngine.UI.LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
			base.OnDisable();
		}

		public override bool IsActive()
		{
			if (base.IsActive())
			{
				return m_Content != null;
			}
			return false;
		}

		private void EnsureLayoutHasRebuilt()
		{
			if (!m_HasRebuiltLayout && !global::UnityEngine.UI.CanvasUpdateRegistry.IsRebuildingLayout())
			{
				global::UnityEngine.Canvas.ForceUpdateCanvases();
			}
		}

		public virtual void StopMovement()
		{
			m_Velocity = global::UnityEngine.Vector2.zero;
		}

		public virtual void OnScroll(global::UnityEngine.EventSystems.PointerEventData data)
		{
			if (!IsActive())
			{
				return;
			}
			EnsureLayoutHasRebuilt();
			UpdateBounds();
			global::UnityEngine.Vector2 scrollDelta = data.scrollDelta;
			scrollDelta.y *= -1f;
			if (vertical && !horizontal)
			{
				if (global::UnityEngine.Mathf.Abs(scrollDelta.x) > global::UnityEngine.Mathf.Abs(scrollDelta.y))
				{
					scrollDelta.y = scrollDelta.x;
				}
				scrollDelta.x = 0f;
			}
			if (horizontal && !vertical)
			{
				if (global::UnityEngine.Mathf.Abs(scrollDelta.y) > global::UnityEngine.Mathf.Abs(scrollDelta.x))
				{
					scrollDelta.x = scrollDelta.y;
				}
				scrollDelta.y = 0f;
			}
			if (data.IsScrolling())
			{
				m_Scrolling = true;
			}
			global::UnityEngine.Vector2 anchoredPosition = m_Content.anchoredPosition;
			anchoredPosition += scrollDelta * m_ScrollSensitivity;
			if (m_MovementType == global::UnityEngine.UI.ScrollRect.MovementType.Clamped)
			{
				anchoredPosition += CalculateOffset(anchoredPosition - m_Content.anchoredPosition);
			}
			SetContentAnchoredPosition(anchoredPosition);
			UpdateBounds();
		}

		public virtual void OnInitializePotentialDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (eventData.button == global::UnityEngine.EventSystems.PointerEventData.InputButton.Left)
			{
				m_Velocity = global::UnityEngine.Vector2.zero;
			}
		}

		public virtual void OnBeginDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (eventData.button == global::UnityEngine.EventSystems.PointerEventData.InputButton.Left && IsActive())
			{
				UpdateBounds();
				m_PointerStartLocalCursor = global::UnityEngine.Vector2.zero;
				global::UnityEngine.RectTransformUtility.ScreenPointToLocalPointInRectangle(viewRect, eventData.position, eventData.pressEventCamera, out m_PointerStartLocalCursor);
				m_ContentStartPosition = m_Content.anchoredPosition;
				m_Dragging = true;
			}
		}

		public virtual void OnEndDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (eventData.button == global::UnityEngine.EventSystems.PointerEventData.InputButton.Left)
			{
				m_Dragging = false;
			}
		}

		public virtual void OnDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (!m_Dragging || eventData.button != global::UnityEngine.EventSystems.PointerEventData.InputButton.Left || !IsActive() || !global::UnityEngine.RectTransformUtility.ScreenPointToLocalPointInRectangle(viewRect, eventData.position, eventData.pressEventCamera, out var localPoint))
			{
				return;
			}
			UpdateBounds();
			global::UnityEngine.Vector2 vector = localPoint - m_PointerStartLocalCursor;
			global::UnityEngine.Vector2 vector2 = m_ContentStartPosition + vector;
			global::UnityEngine.Vector2 vector3 = CalculateOffset(vector2 - m_Content.anchoredPosition);
			vector2 += vector3;
			if (m_MovementType == global::UnityEngine.UI.ScrollRect.MovementType.Elastic)
			{
				if (vector3.x != 0f)
				{
					vector2.x -= RubberDelta(vector3.x, m_ViewBounds.size.x);
				}
				if (vector3.y != 0f)
				{
					vector2.y -= RubberDelta(vector3.y, m_ViewBounds.size.y);
				}
			}
			SetContentAnchoredPosition(vector2);
		}

		protected virtual void SetContentAnchoredPosition(global::UnityEngine.Vector2 position)
		{
			if (!m_Horizontal)
			{
				position.x = m_Content.anchoredPosition.x;
			}
			if (!m_Vertical)
			{
				position.y = m_Content.anchoredPosition.y;
			}
			if (position != m_Content.anchoredPosition)
			{
				m_Content.anchoredPosition = position;
				UpdateBounds();
			}
		}

		protected virtual void LateUpdate()
		{
			if (!m_Content)
			{
				return;
			}
			EnsureLayoutHasRebuilt();
			UpdateBounds();
			float unscaledDeltaTime = global::UnityEngine.Time.unscaledDeltaTime;
			global::UnityEngine.Vector2 vector = CalculateOffset(global::UnityEngine.Vector2.zero);
			if (unscaledDeltaTime > 0f)
			{
				if (!m_Dragging && (vector != global::UnityEngine.Vector2.zero || m_Velocity != global::UnityEngine.Vector2.zero))
				{
					global::UnityEngine.Vector2 anchoredPosition = m_Content.anchoredPosition;
					for (int i = 0; i < 2; i++)
					{
						if (m_MovementType == global::UnityEngine.UI.ScrollRect.MovementType.Elastic && vector[i] != 0f)
						{
							float currentVelocity = m_Velocity[i];
							float num = m_Elasticity;
							if (m_Scrolling)
							{
								num *= 3f;
							}
							anchoredPosition[i] = global::UnityEngine.Mathf.SmoothDamp(m_Content.anchoredPosition[i], m_Content.anchoredPosition[i] + vector[i], ref currentVelocity, num, float.PositiveInfinity, unscaledDeltaTime);
							if (global::UnityEngine.Mathf.Abs(currentVelocity) < 1f)
							{
								currentVelocity = 0f;
							}
							m_Velocity[i] = currentVelocity;
						}
						else if (m_Inertia)
						{
							m_Velocity[i] *= global::UnityEngine.Mathf.Pow(m_DecelerationRate, unscaledDeltaTime);
							if (global::UnityEngine.Mathf.Abs(m_Velocity[i]) < 1f)
							{
								m_Velocity[i] = 0f;
							}
							anchoredPosition[i] += m_Velocity[i] * unscaledDeltaTime;
						}
						else
						{
							m_Velocity[i] = 0f;
						}
					}
					if (m_MovementType == global::UnityEngine.UI.ScrollRect.MovementType.Clamped)
					{
						vector = CalculateOffset(anchoredPosition - m_Content.anchoredPosition);
						anchoredPosition += vector;
					}
					SetContentAnchoredPosition(anchoredPosition);
				}
				if (m_Dragging && m_Inertia)
				{
					global::UnityEngine.Vector3 b = (m_Content.anchoredPosition - m_PrevPosition) / unscaledDeltaTime;
					m_Velocity = global::UnityEngine.Vector3.Lerp(m_Velocity, b, unscaledDeltaTime * 10f);
				}
			}
			if (m_ViewBounds != m_PrevViewBounds || m_ContentBounds != m_PrevContentBounds || m_Content.anchoredPosition != m_PrevPosition)
			{
				UpdateScrollbars(vector);
				global::UnityEngine.UISystemProfilerApi.AddMarker("ScrollRect.value", this);
				m_OnValueChanged.Invoke(normalizedPosition);
				UpdatePrevData();
			}
			UpdateScrollbarVisibility();
			m_Scrolling = false;
		}

		protected void UpdatePrevData()
		{
			if (m_Content == null)
			{
				m_PrevPosition = global::UnityEngine.Vector2.zero;
			}
			else
			{
				m_PrevPosition = m_Content.anchoredPosition;
			}
			m_PrevViewBounds = m_ViewBounds;
			m_PrevContentBounds = m_ContentBounds;
		}

		private void UpdateScrollbars(global::UnityEngine.Vector2 offset)
		{
			if ((bool)m_HorizontalScrollbar)
			{
				if (m_ContentBounds.size.x > 0f)
				{
					m_HorizontalScrollbar.size = global::UnityEngine.Mathf.Clamp01((m_ViewBounds.size.x - global::UnityEngine.Mathf.Abs(offset.x)) / m_ContentBounds.size.x);
				}
				else
				{
					m_HorizontalScrollbar.size = 1f;
				}
				m_HorizontalScrollbar.value = horizontalNormalizedPosition;
			}
			if ((bool)m_VerticalScrollbar)
			{
				if (m_ContentBounds.size.y > 0f)
				{
					m_VerticalScrollbar.size = global::UnityEngine.Mathf.Clamp01((m_ViewBounds.size.y - global::UnityEngine.Mathf.Abs(offset.y)) / m_ContentBounds.size.y);
				}
				else
				{
					m_VerticalScrollbar.size = 1f;
				}
				m_VerticalScrollbar.value = verticalNormalizedPosition;
			}
		}

		private void SetHorizontalNormalizedPosition(float value)
		{
			if (horizontalNormalizedPosition != value)
			{
				SetNormalizedPosition(value, 0);
			}
		}

		private void SetVerticalNormalizedPosition(float value)
		{
			if (verticalNormalizedPosition != value)
			{
				SetNormalizedPosition(value, 1);
			}
		}

		protected virtual void SetNormalizedPosition(float value, int axis)
		{
			EnsureLayoutHasRebuilt();
			UpdateBounds();
			float num = m_ContentBounds.size[axis] - m_ViewBounds.size[axis];
			float num2 = m_ViewBounds.min[axis] - value * num;
			float num3 = m_Content.anchoredPosition[axis] + num2 - m_ContentBounds.min[axis];
			global::UnityEngine.Vector3 vector = m_Content.anchoredPosition;
			if (global::UnityEngine.Mathf.Abs(vector[axis] - num3) > 0.01f)
			{
				vector[axis] = num3;
				m_Content.anchoredPosition = vector;
				m_Velocity[axis] = 0f;
				UpdateBounds();
			}
		}

		private static float RubberDelta(float overStretching, float viewSize)
		{
			return (1f - 1f / (global::UnityEngine.Mathf.Abs(overStretching) * 0.55f / viewSize + 1f)) * viewSize * global::UnityEngine.Mathf.Sign(overStretching);
		}

		protected override void OnRectTransformDimensionsChange()
		{
			SetDirty();
		}

		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		public virtual void CalculateLayoutInputVertical()
		{
		}

		public virtual void SetLayoutHorizontal()
		{
			m_Tracker.Clear();
			UpdateCachedData();
			if (m_HSliderExpand || m_VSliderExpand)
			{
				m_Tracker.Add(this, viewRect, global::UnityEngine.DrivenTransformProperties.Anchors | global::UnityEngine.DrivenTransformProperties.AnchoredPosition | global::UnityEngine.DrivenTransformProperties.SizeDelta);
				viewRect.anchorMin = global::UnityEngine.Vector2.zero;
				viewRect.anchorMax = global::UnityEngine.Vector2.one;
				viewRect.sizeDelta = global::UnityEngine.Vector2.zero;
				viewRect.anchoredPosition = global::UnityEngine.Vector2.zero;
				global::UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(content);
				m_ViewBounds = new global::UnityEngine.Bounds(viewRect.rect.center, viewRect.rect.size);
				m_ContentBounds = GetBounds();
			}
			if (m_VSliderExpand && vScrollingNeeded)
			{
				viewRect.sizeDelta = new global::UnityEngine.Vector2(0f - (m_VSliderWidth + m_VerticalScrollbarSpacing), viewRect.sizeDelta.y);
				global::UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(content);
				m_ViewBounds = new global::UnityEngine.Bounds(viewRect.rect.center, viewRect.rect.size);
				m_ContentBounds = GetBounds();
			}
			if (m_HSliderExpand && hScrollingNeeded)
			{
				viewRect.sizeDelta = new global::UnityEngine.Vector2(viewRect.sizeDelta.x, 0f - (m_HSliderHeight + m_HorizontalScrollbarSpacing));
				m_ViewBounds = new global::UnityEngine.Bounds(viewRect.rect.center, viewRect.rect.size);
				m_ContentBounds = GetBounds();
			}
			if (m_VSliderExpand && vScrollingNeeded && viewRect.sizeDelta.x == 0f && viewRect.sizeDelta.y < 0f)
			{
				viewRect.sizeDelta = new global::UnityEngine.Vector2(0f - (m_VSliderWidth + m_VerticalScrollbarSpacing), viewRect.sizeDelta.y);
			}
		}

		public virtual void SetLayoutVertical()
		{
			UpdateScrollbarLayout();
			m_ViewBounds = new global::UnityEngine.Bounds(viewRect.rect.center, viewRect.rect.size);
			m_ContentBounds = GetBounds();
		}

		private void UpdateScrollbarVisibility()
		{
			UpdateOneScrollbarVisibility(vScrollingNeeded, m_Vertical, m_VerticalScrollbarVisibility, m_VerticalScrollbar);
			UpdateOneScrollbarVisibility(hScrollingNeeded, m_Horizontal, m_HorizontalScrollbarVisibility, m_HorizontalScrollbar);
		}

		private static void UpdateOneScrollbarVisibility(bool xScrollingNeeded, bool xAxisEnabled, global::UnityEngine.UI.ScrollRect.ScrollbarVisibility scrollbarVisibility, global::UnityEngine.UI.Scrollbar scrollbar)
		{
			if (!scrollbar)
			{
				return;
			}
			if (scrollbarVisibility == global::UnityEngine.UI.ScrollRect.ScrollbarVisibility.Permanent)
			{
				if (scrollbar.gameObject.activeSelf != xAxisEnabled)
				{
					scrollbar.gameObject.SetActive(xAxisEnabled);
				}
			}
			else if (scrollbar.gameObject.activeSelf != xScrollingNeeded)
			{
				scrollbar.gameObject.SetActive(xScrollingNeeded);
			}
		}

		private void UpdateScrollbarLayout()
		{
			if (m_VSliderExpand && (bool)m_HorizontalScrollbar)
			{
				m_Tracker.Add(this, m_HorizontalScrollbarRect, global::UnityEngine.DrivenTransformProperties.AnchoredPositionX | global::UnityEngine.DrivenTransformProperties.AnchorMinX | global::UnityEngine.DrivenTransformProperties.AnchorMaxX | global::UnityEngine.DrivenTransformProperties.SizeDeltaX);
				m_HorizontalScrollbarRect.anchorMin = new global::UnityEngine.Vector2(0f, m_HorizontalScrollbarRect.anchorMin.y);
				m_HorizontalScrollbarRect.anchorMax = new global::UnityEngine.Vector2(1f, m_HorizontalScrollbarRect.anchorMax.y);
				m_HorizontalScrollbarRect.anchoredPosition = new global::UnityEngine.Vector2(0f, m_HorizontalScrollbarRect.anchoredPosition.y);
				if (vScrollingNeeded)
				{
					m_HorizontalScrollbarRect.sizeDelta = new global::UnityEngine.Vector2(0f - (m_VSliderWidth + m_VerticalScrollbarSpacing), m_HorizontalScrollbarRect.sizeDelta.y);
				}
				else
				{
					m_HorizontalScrollbarRect.sizeDelta = new global::UnityEngine.Vector2(0f, m_HorizontalScrollbarRect.sizeDelta.y);
				}
			}
			if (m_HSliderExpand && (bool)m_VerticalScrollbar)
			{
				m_Tracker.Add(this, m_VerticalScrollbarRect, global::UnityEngine.DrivenTransformProperties.AnchoredPositionY | global::UnityEngine.DrivenTransformProperties.AnchorMinY | global::UnityEngine.DrivenTransformProperties.AnchorMaxY | global::UnityEngine.DrivenTransformProperties.SizeDeltaY);
				m_VerticalScrollbarRect.anchorMin = new global::UnityEngine.Vector2(m_VerticalScrollbarRect.anchorMin.x, 0f);
				m_VerticalScrollbarRect.anchorMax = new global::UnityEngine.Vector2(m_VerticalScrollbarRect.anchorMax.x, 1f);
				m_VerticalScrollbarRect.anchoredPosition = new global::UnityEngine.Vector2(m_VerticalScrollbarRect.anchoredPosition.x, 0f);
				if (hScrollingNeeded)
				{
					m_VerticalScrollbarRect.sizeDelta = new global::UnityEngine.Vector2(m_VerticalScrollbarRect.sizeDelta.x, 0f - (m_HSliderHeight + m_HorizontalScrollbarSpacing));
				}
				else
				{
					m_VerticalScrollbarRect.sizeDelta = new global::UnityEngine.Vector2(m_VerticalScrollbarRect.sizeDelta.x, 0f);
				}
			}
		}

		protected void UpdateBounds()
		{
			m_ViewBounds = new global::UnityEngine.Bounds(viewRect.rect.center, viewRect.rect.size);
			m_ContentBounds = GetBounds();
			if (m_Content == null)
			{
				return;
			}
			global::UnityEngine.Vector3 contentSize = m_ContentBounds.size;
			global::UnityEngine.Vector3 contentPos = m_ContentBounds.center;
			global::UnityEngine.Vector2 contentPivot = m_Content.pivot;
			AdjustBounds(ref m_ViewBounds, ref contentPivot, ref contentSize, ref contentPos);
			m_ContentBounds.size = contentSize;
			m_ContentBounds.center = contentPos;
			if (movementType != global::UnityEngine.UI.ScrollRect.MovementType.Clamped)
			{
				return;
			}
			global::UnityEngine.Vector2 zero = global::UnityEngine.Vector2.zero;
			if (m_ViewBounds.max.x > m_ContentBounds.max.x)
			{
				zero.x = global::System.Math.Min(m_ViewBounds.min.x - m_ContentBounds.min.x, m_ViewBounds.max.x - m_ContentBounds.max.x);
			}
			else if (m_ViewBounds.min.x < m_ContentBounds.min.x)
			{
				zero.x = global::System.Math.Max(m_ViewBounds.min.x - m_ContentBounds.min.x, m_ViewBounds.max.x - m_ContentBounds.max.x);
			}
			if (m_ViewBounds.min.y < m_ContentBounds.min.y)
			{
				zero.y = global::System.Math.Max(m_ViewBounds.min.y - m_ContentBounds.min.y, m_ViewBounds.max.y - m_ContentBounds.max.y);
			}
			else if (m_ViewBounds.max.y > m_ContentBounds.max.y)
			{
				zero.y = global::System.Math.Min(m_ViewBounds.min.y - m_ContentBounds.min.y, m_ViewBounds.max.y - m_ContentBounds.max.y);
			}
			if (zero.sqrMagnitude > float.Epsilon)
			{
				contentPos = m_Content.anchoredPosition + zero;
				if (!m_Horizontal)
				{
					contentPos.x = m_Content.anchoredPosition.x;
				}
				if (!m_Vertical)
				{
					contentPos.y = m_Content.anchoredPosition.y;
				}
				AdjustBounds(ref m_ViewBounds, ref contentPivot, ref contentSize, ref contentPos);
			}
		}

		internal static void AdjustBounds(ref global::UnityEngine.Bounds viewBounds, ref global::UnityEngine.Vector2 contentPivot, ref global::UnityEngine.Vector3 contentSize, ref global::UnityEngine.Vector3 contentPos)
		{
			global::UnityEngine.Vector3 vector = viewBounds.size - contentSize;
			if (vector.x > 0f)
			{
				contentPos.x -= vector.x * (contentPivot.x - 0.5f);
				contentSize.x = viewBounds.size.x;
			}
			if (vector.y > 0f)
			{
				contentPos.y -= vector.y * (contentPivot.y - 0.5f);
				contentSize.y = viewBounds.size.y;
			}
		}

		private global::UnityEngine.Bounds GetBounds()
		{
			if (m_Content == null)
			{
				return default(global::UnityEngine.Bounds);
			}
			m_Content.GetWorldCorners(m_Corners);
			global::UnityEngine.Matrix4x4 viewWorldToLocalMatrix = viewRect.worldToLocalMatrix;
			return InternalGetBounds(m_Corners, ref viewWorldToLocalMatrix);
		}

		internal static global::UnityEngine.Bounds InternalGetBounds(global::UnityEngine.Vector3[] corners, ref global::UnityEngine.Matrix4x4 viewWorldToLocalMatrix)
		{
			global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
			global::UnityEngine.Vector3 vector2 = new global::UnityEngine.Vector3(float.MinValue, float.MinValue, float.MinValue);
			for (int i = 0; i < 4; i++)
			{
				global::UnityEngine.Vector3 lhs = viewWorldToLocalMatrix.MultiplyPoint3x4(corners[i]);
				vector = global::UnityEngine.Vector3.Min(lhs, vector);
				vector2 = global::UnityEngine.Vector3.Max(lhs, vector2);
			}
			global::UnityEngine.Bounds result = new global::UnityEngine.Bounds(vector, global::UnityEngine.Vector3.zero);
			result.Encapsulate(vector2);
			return result;
		}

		private global::UnityEngine.Vector2 CalculateOffset(global::UnityEngine.Vector2 delta)
		{
			return InternalCalculateOffset(ref m_ViewBounds, ref m_ContentBounds, m_Horizontal, m_Vertical, m_MovementType, ref delta);
		}

		internal static global::UnityEngine.Vector2 InternalCalculateOffset(ref global::UnityEngine.Bounds viewBounds, ref global::UnityEngine.Bounds contentBounds, bool horizontal, bool vertical, global::UnityEngine.UI.ScrollRect.MovementType movementType, ref global::UnityEngine.Vector2 delta)
		{
			global::UnityEngine.Vector2 zero = global::UnityEngine.Vector2.zero;
			if (movementType == global::UnityEngine.UI.ScrollRect.MovementType.Unrestricted)
			{
				return zero;
			}
			global::UnityEngine.Vector2 vector = contentBounds.min;
			global::UnityEngine.Vector2 vector2 = contentBounds.max;
			if (horizontal)
			{
				vector.x += delta.x;
				vector2.x += delta.x;
				float num = viewBounds.max.x - vector2.x;
				float num2 = viewBounds.min.x - vector.x;
				if (num2 < -0.001f)
				{
					zero.x = num2;
				}
				else if (num > 0.001f)
				{
					zero.x = num;
				}
			}
			if (vertical)
			{
				vector.y += delta.y;
				vector2.y += delta.y;
				float num3 = viewBounds.max.y - vector2.y;
				float num4 = viewBounds.min.y - vector.y;
				if (num3 > 0.001f)
				{
					zero.y = num3;
				}
				else if (num4 < -0.001f)
				{
					zero.y = num4;
				}
			}
			return zero;
		}

		protected void SetDirty()
		{
			if (IsActive())
			{
				global::UnityEngine.UI.LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
			}
		}

		protected void SetDirtyCaching()
		{
			if (IsActive())
			{
				global::UnityEngine.UI.CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(this);
				global::UnityEngine.UI.LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
				m_ViewRect = null;
			}
		}
	}
}
