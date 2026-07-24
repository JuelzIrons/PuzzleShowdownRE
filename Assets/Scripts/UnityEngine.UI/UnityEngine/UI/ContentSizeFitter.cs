namespace UnityEngine.UI
{
	[global::UnityEngine.AddComponentMenu("Layout/Content Size Fitter", 141)]
	[global::UnityEngine.ExecuteAlways]
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.RectTransform))]
	public class ContentSizeFitter : global::UnityEngine.EventSystems.UIBehaviour, global::UnityEngine.UI.ILayoutSelfController, global::UnityEngine.UI.ILayoutController
	{
		public enum FitMode
		{
			Unconstrained = 0,
			MinSize = 1,
			PreferredSize = 2
		}

		[global::UnityEngine.SerializeField]
		protected global::UnityEngine.UI.ContentSizeFitter.FitMode m_HorizontalFit;

		[global::UnityEngine.SerializeField]
		protected global::UnityEngine.UI.ContentSizeFitter.FitMode m_VerticalFit;

		[global::System.NonSerialized]
		private global::UnityEngine.RectTransform m_Rect;

		private global::UnityEngine.DrivenRectTransformTracker m_Tracker;

		public global::UnityEngine.UI.ContentSizeFitter.FitMode horizontalFit
		{
			get
			{
				return m_HorizontalFit;
			}
			set
			{
				if (global::UnityEngine.UI.SetPropertyUtility.SetStruct(ref m_HorizontalFit, value))
				{
					SetDirty();
				}
			}
		}

		public global::UnityEngine.UI.ContentSizeFitter.FitMode verticalFit
		{
			get
			{
				return m_VerticalFit;
			}
			set
			{
				if (global::UnityEngine.UI.SetPropertyUtility.SetStruct(ref m_VerticalFit, value))
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

		protected ContentSizeFitter()
		{
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			SetDirty();
		}

		protected override void OnDisable()
		{
			m_Tracker.Clear();
			global::UnityEngine.UI.LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
			base.OnDisable();
		}

		protected override void OnRectTransformDimensionsChange()
		{
			SetDirty();
		}

		private void HandleSelfFittingAlongAxis(int axis)
		{
			global::UnityEngine.UI.ContentSizeFitter.FitMode fitMode = ((axis == 0) ? horizontalFit : verticalFit);
			if (fitMode == global::UnityEngine.UI.ContentSizeFitter.FitMode.Unconstrained)
			{
				m_Tracker.Add(this, rectTransform, global::UnityEngine.DrivenTransformProperties.None);
				return;
			}
			m_Tracker.Add(this, rectTransform, (axis == 0) ? global::UnityEngine.DrivenTransformProperties.SizeDeltaX : global::UnityEngine.DrivenTransformProperties.SizeDeltaY);
			if (fitMode == global::UnityEngine.UI.ContentSizeFitter.FitMode.MinSize)
			{
				rectTransform.SetSizeWithCurrentAnchors((global::UnityEngine.RectTransform.Axis)axis, global::UnityEngine.UI.LayoutUtility.GetMinSize(m_Rect, axis));
			}
			else
			{
				rectTransform.SetSizeWithCurrentAnchors((global::UnityEngine.RectTransform.Axis)axis, global::UnityEngine.UI.LayoutUtility.GetPreferredSize(m_Rect, axis));
			}
		}

		public virtual void SetLayoutHorizontal()
		{
			m_Tracker.Clear();
			HandleSelfFittingAlongAxis(0);
		}

		public virtual void SetLayoutVertical()
		{
			HandleSelfFittingAlongAxis(1);
		}

		protected void SetDirty()
		{
			if (IsActive())
			{
				global::UnityEngine.UI.LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
			}
		}
	}
}
