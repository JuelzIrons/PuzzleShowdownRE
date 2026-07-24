namespace TMPro
{
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.RectTransform))]
	public class TextContainer : global::UnityEngine.EventSystems.UIBehaviour
	{
		private bool m_hasChanged;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Vector2 m_pivot;

		[global::UnityEngine.SerializeField]
		private global::TMPro.TextContainerAnchors m_anchorPosition = global::TMPro.TextContainerAnchors.Middle;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rect m_rect;

		private bool m_isDefaultWidth;

		private bool m_isDefaultHeight;

		private bool m_isAutoFitting;

		private global::UnityEngine.Vector3[] m_corners = new global::UnityEngine.Vector3[4];

		private global::UnityEngine.Vector3[] m_worldCorners = new global::UnityEngine.Vector3[4];

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Vector4 m_margins;

		private global::UnityEngine.RectTransform m_rectTransform;

		private static global::UnityEngine.Vector2 k_defaultSize = new global::UnityEngine.Vector2(100f, 100f);

		private global::TMPro.TextMeshPro m_textMeshPro;

		public bool hasChanged
		{
			get
			{
				return m_hasChanged;
			}
			set
			{
				m_hasChanged = value;
			}
		}

		public global::UnityEngine.Vector2 pivot
		{
			get
			{
				return m_pivot;
			}
			set
			{
				if (m_pivot != value)
				{
					m_pivot = value;
					m_anchorPosition = GetAnchorPosition(m_pivot);
					m_hasChanged = true;
					OnContainerChanged();
				}
			}
		}

		public global::TMPro.TextContainerAnchors anchorPosition
		{
			get
			{
				return m_anchorPosition;
			}
			set
			{
				if (m_anchorPosition != value)
				{
					m_anchorPosition = value;
					m_pivot = GetPivot(m_anchorPosition);
					m_hasChanged = true;
					OnContainerChanged();
				}
			}
		}

		public global::UnityEngine.Rect rect
		{
			get
			{
				return m_rect;
			}
			set
			{
				if (m_rect != value)
				{
					m_rect = value;
					m_hasChanged = true;
					OnContainerChanged();
				}
			}
		}

		public global::UnityEngine.Vector2 size
		{
			get
			{
				return new global::UnityEngine.Vector2(m_rect.width, m_rect.height);
			}
			set
			{
				if (new global::UnityEngine.Vector2(m_rect.width, m_rect.height) != value)
				{
					SetRect(value);
					m_hasChanged = true;
					m_isDefaultWidth = false;
					m_isDefaultHeight = false;
					OnContainerChanged();
				}
			}
		}

		public float width
		{
			get
			{
				return m_rect.width;
			}
			set
			{
				SetRect(new global::UnityEngine.Vector2(value, m_rect.height));
				m_hasChanged = true;
				m_isDefaultWidth = false;
				OnContainerChanged();
			}
		}

		public float height
		{
			get
			{
				return m_rect.height;
			}
			set
			{
				SetRect(new global::UnityEngine.Vector2(m_rect.width, value));
				m_hasChanged = true;
				m_isDefaultHeight = false;
				OnContainerChanged();
			}
		}

		public bool isDefaultWidth => m_isDefaultWidth;

		public bool isDefaultHeight => m_isDefaultHeight;

		public bool isAutoFitting
		{
			get
			{
				return m_isAutoFitting;
			}
			set
			{
				m_isAutoFitting = value;
			}
		}

		public global::UnityEngine.Vector3[] corners => m_corners;

		public global::UnityEngine.Vector3[] worldCorners => m_worldCorners;

		public global::UnityEngine.Vector4 margins
		{
			get
			{
				return m_margins;
			}
			set
			{
				if (m_margins != value)
				{
					m_margins = value;
					m_hasChanged = true;
					OnContainerChanged();
				}
			}
		}

		public global::UnityEngine.RectTransform rectTransform
		{
			get
			{
				if (m_rectTransform == null)
				{
					m_rectTransform = GetComponent<global::UnityEngine.RectTransform>();
				}
				return m_rectTransform;
			}
		}

		public global::TMPro.TextMeshPro textMeshPro
		{
			get
			{
				if (m_textMeshPro == null)
				{
					m_textMeshPro = GetComponent<global::TMPro.TextMeshPro>();
				}
				return m_textMeshPro;
			}
		}

		protected override void Awake()
		{
			global::UnityEngine.Debug.LogWarning("The Text Container component is now Obsolete and can safely be removed from [" + base.gameObject.name + "].", this);
		}

		protected override void OnEnable()
		{
			OnContainerChanged();
		}

		protected override void OnDisable()
		{
		}

		private void OnContainerChanged()
		{
			UpdateCorners();
			if (m_rectTransform != null)
			{
				m_rectTransform.sizeDelta = size;
				m_rectTransform.hasChanged = true;
			}
			if (textMeshPro != null)
			{
				m_textMeshPro.SetVerticesDirty();
				m_textMeshPro.margin = m_margins;
			}
		}

		protected override void OnRectTransformDimensionsChange()
		{
			if (rectTransform == null)
			{
				m_rectTransform = base.gameObject.AddComponent<global::UnityEngine.RectTransform>();
			}
			if (m_rectTransform.sizeDelta != k_defaultSize)
			{
				size = m_rectTransform.sizeDelta;
			}
			pivot = m_rectTransform.pivot;
			m_hasChanged = true;
			OnContainerChanged();
		}

		private void SetRect(global::UnityEngine.Vector2 size)
		{
			m_rect = new global::UnityEngine.Rect(m_rect.x, m_rect.y, size.x, size.y);
		}

		private void UpdateCorners()
		{
			m_corners[0] = new global::UnityEngine.Vector3((0f - m_pivot.x) * m_rect.width, (0f - m_pivot.y) * m_rect.height);
			m_corners[1] = new global::UnityEngine.Vector3((0f - m_pivot.x) * m_rect.width, (1f - m_pivot.y) * m_rect.height);
			m_corners[2] = new global::UnityEngine.Vector3((1f - m_pivot.x) * m_rect.width, (1f - m_pivot.y) * m_rect.height);
			m_corners[3] = new global::UnityEngine.Vector3((1f - m_pivot.x) * m_rect.width, (0f - m_pivot.y) * m_rect.height);
			if (m_rectTransform != null)
			{
				m_rectTransform.pivot = m_pivot;
			}
		}

		private global::UnityEngine.Vector2 GetPivot(global::TMPro.TextContainerAnchors anchor)
		{
			global::UnityEngine.Vector2 result = global::UnityEngine.Vector2.zero;
			switch (anchor)
			{
			case global::TMPro.TextContainerAnchors.TopLeft:
				result = new global::UnityEngine.Vector2(0f, 1f);
				break;
			case global::TMPro.TextContainerAnchors.Top:
				result = new global::UnityEngine.Vector2(0.5f, 1f);
				break;
			case global::TMPro.TextContainerAnchors.TopRight:
				result = new global::UnityEngine.Vector2(1f, 1f);
				break;
			case global::TMPro.TextContainerAnchors.Left:
				result = new global::UnityEngine.Vector2(0f, 0.5f);
				break;
			case global::TMPro.TextContainerAnchors.Middle:
				result = new global::UnityEngine.Vector2(0.5f, 0.5f);
				break;
			case global::TMPro.TextContainerAnchors.Right:
				result = new global::UnityEngine.Vector2(1f, 0.5f);
				break;
			case global::TMPro.TextContainerAnchors.BottomLeft:
				result = new global::UnityEngine.Vector2(0f, 0f);
				break;
			case global::TMPro.TextContainerAnchors.Bottom:
				result = new global::UnityEngine.Vector2(0.5f, 0f);
				break;
			case global::TMPro.TextContainerAnchors.BottomRight:
				result = new global::UnityEngine.Vector2(1f, 0f);
				break;
			}
			return result;
		}

		private global::TMPro.TextContainerAnchors GetAnchorPosition(global::UnityEngine.Vector2 pivot)
		{
			if (pivot == new global::UnityEngine.Vector2(0f, 1f))
			{
				return global::TMPro.TextContainerAnchors.TopLeft;
			}
			if (pivot == new global::UnityEngine.Vector2(0.5f, 1f))
			{
				return global::TMPro.TextContainerAnchors.Top;
			}
			if (pivot == new global::UnityEngine.Vector2(1f, 1f))
			{
				return global::TMPro.TextContainerAnchors.TopRight;
			}
			if (pivot == new global::UnityEngine.Vector2(0f, 0.5f))
			{
				return global::TMPro.TextContainerAnchors.Left;
			}
			if (pivot == new global::UnityEngine.Vector2(0.5f, 0.5f))
			{
				return global::TMPro.TextContainerAnchors.Middle;
			}
			if (pivot == new global::UnityEngine.Vector2(1f, 0.5f))
			{
				return global::TMPro.TextContainerAnchors.Right;
			}
			if (pivot == new global::UnityEngine.Vector2(0f, 0f))
			{
				return global::TMPro.TextContainerAnchors.BottomLeft;
			}
			if (pivot == new global::UnityEngine.Vector2(0.5f, 0f))
			{
				return global::TMPro.TextContainerAnchors.Bottom;
			}
			if (pivot == new global::UnityEngine.Vector2(1f, 0f))
			{
				return global::TMPro.TextContainerAnchors.BottomRight;
			}
			return global::TMPro.TextContainerAnchors.Custom;
		}
	}
}
