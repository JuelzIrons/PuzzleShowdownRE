namespace UnityEngine.UI
{
	[global::UnityEngine.AddComponentMenu("Layout/Aspect Ratio Fitter", 142)]
	[global::UnityEngine.ExecuteAlways]
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.RectTransform))]
	[global::UnityEngine.DisallowMultipleComponent]
	public class AspectRatioFitter : global::UnityEngine.EventSystems.UIBehaviour, global::UnityEngine.UI.ILayoutSelfController, global::UnityEngine.UI.ILayoutController
	{
		public enum AspectMode
		{
			None = 0,
			WidthControlsHeight = 1,
			HeightControlsWidth = 2,
			FitInParent = 3,
			EnvelopeParent = 4
		}

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.AspectRatioFitter.AspectMode m_AspectMode;

		[global::UnityEngine.SerializeField]
		private float m_AspectRatio = 1f;

		[global::System.NonSerialized]
		private global::UnityEngine.RectTransform m_Rect;

		private bool m_DelayedSetDirty;

		private bool m_DoesParentExist;

		private global::UnityEngine.DrivenRectTransformTracker m_Tracker;

		public global::UnityEngine.UI.AspectRatioFitter.AspectMode aspectMode
		{
			get
			{
				return m_AspectMode;
			}
			set
			{
				if (global::UnityEngine.UI.SetPropertyUtility.SetStruct(ref m_AspectMode, value))
				{
					SetDirty();
				}
			}
		}

		public float aspectRatio
		{
			get
			{
				return m_AspectRatio;
			}
			set
			{
				if (global::UnityEngine.UI.SetPropertyUtility.SetStruct(ref m_AspectRatio, value))
				{
					SetDirty();
				}
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

		protected AspectRatioFitter()
		{
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			m_DoesParentExist = (rectTransform.parent ? true : false);
			SetDirty();
		}

		protected override void Start()
		{
			base.Start();
			if (!IsComponentValidOnObject() || !IsAspectModeValid())
			{
				base.enabled = false;
			}
		}

		protected override void OnDisable()
		{
			m_Tracker.Clear();
			global::UnityEngine.UI.LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
			base.OnDisable();
		}

		protected override void OnTransformParentChanged()
		{
			base.OnTransformParentChanged();
			m_DoesParentExist = (rectTransform.parent ? true : false);
			SetDirty();
		}

		protected virtual void Update()
		{
			if (m_DelayedSetDirty)
			{
				m_DelayedSetDirty = false;
				SetDirty();
			}
		}

		protected override void OnRectTransformDimensionsChange()
		{
			UpdateRect();
		}

		private void UpdateRect()
		{
			if (!IsActive() || !IsComponentValidOnObject())
			{
				return;
			}
			m_Tracker.Clear();
			switch (m_AspectMode)
			{
			case global::UnityEngine.UI.AspectRatioFitter.AspectMode.HeightControlsWidth:
				m_Tracker.Add(this, rectTransform, global::UnityEngine.DrivenTransformProperties.SizeDeltaX);
				rectTransform.SetSizeWithCurrentAnchors(global::UnityEngine.RectTransform.Axis.Horizontal, rectTransform.rect.height * m_AspectRatio);
				break;
			case global::UnityEngine.UI.AspectRatioFitter.AspectMode.WidthControlsHeight:
				m_Tracker.Add(this, rectTransform, global::UnityEngine.DrivenTransformProperties.SizeDeltaY);
				rectTransform.SetSizeWithCurrentAnchors(global::UnityEngine.RectTransform.Axis.Vertical, rectTransform.rect.width / m_AspectRatio);
				break;
			case global::UnityEngine.UI.AspectRatioFitter.AspectMode.FitInParent:
			case global::UnityEngine.UI.AspectRatioFitter.AspectMode.EnvelopeParent:
				if (DoesParentExists())
				{
					m_Tracker.Add(this, rectTransform, global::UnityEngine.DrivenTransformProperties.Anchors | global::UnityEngine.DrivenTransformProperties.AnchoredPosition | global::UnityEngine.DrivenTransformProperties.SizeDelta);
					rectTransform.anchorMin = global::UnityEngine.Vector2.zero;
					rectTransform.anchorMax = global::UnityEngine.Vector2.one;
					rectTransform.anchoredPosition = global::UnityEngine.Vector2.zero;
					global::UnityEngine.Vector2 zero = global::UnityEngine.Vector2.zero;
					global::UnityEngine.Vector2 parentSize = GetParentSize();
					if ((parentSize.y * aspectRatio < parentSize.x) ^ (m_AspectMode == global::UnityEngine.UI.AspectRatioFitter.AspectMode.FitInParent))
					{
						zero.y = GetSizeDeltaToProduceSize(parentSize.x / aspectRatio, 1);
					}
					else
					{
						zero.x = GetSizeDeltaToProduceSize(parentSize.y * aspectRatio, 0);
					}
					rectTransform.sizeDelta = zero;
				}
				break;
			}
		}

		private float GetSizeDeltaToProduceSize(float size, int axis)
		{
			return size - GetParentSize()[axis] * (rectTransform.anchorMax[axis] - rectTransform.anchorMin[axis]);
		}

		private global::UnityEngine.Vector2 GetParentSize()
		{
			global::UnityEngine.RectTransform rectTransform = this.rectTransform.parent as global::UnityEngine.RectTransform;
			if ((bool)rectTransform)
			{
				return rectTransform.rect.size;
			}
			return global::UnityEngine.Vector2.zero;
		}

		public virtual void SetLayoutHorizontal()
		{
		}

		public virtual void SetLayoutVertical()
		{
		}

		protected void SetDirty()
		{
			UpdateRect();
		}

		public bool IsComponentValidOnObject()
		{
			global::UnityEngine.Canvas component = base.gameObject.GetComponent<global::UnityEngine.Canvas>();
			if ((bool)component && component.isRootCanvas && component.renderMode != global::UnityEngine.RenderMode.WorldSpace)
			{
				return false;
			}
			return true;
		}

		public bool IsAspectModeValid()
		{
			if (!DoesParentExists() && (aspectMode == global::UnityEngine.UI.AspectRatioFitter.AspectMode.EnvelopeParent || aspectMode == global::UnityEngine.UI.AspectRatioFitter.AspectMode.FitInParent))
			{
				return false;
			}
			return true;
		}

		private bool DoesParentExists()
		{
			return m_DoesParentExist;
		}
	}
}
