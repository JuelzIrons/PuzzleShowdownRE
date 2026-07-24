namespace UnityEngine.UI
{
	[global::UnityEngine.DisallowMultipleComponent]
	[global::UnityEngine.ExecuteAlways]
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.RectTransform))]
	public abstract class LayoutGroup : global::UnityEngine.EventSystems.UIBehaviour, global::UnityEngine.UI.ILayoutElement, global::UnityEngine.UI.ILayoutGroup, global::UnityEngine.UI.ILayoutController
	{
		[global::UnityEngine.SerializeField]
		protected global::UnityEngine.RectOffset m_Padding = new global::UnityEngine.RectOffset();

		[global::UnityEngine.SerializeField]
		protected global::UnityEngine.TextAnchor m_ChildAlignment;

		[global::System.NonSerialized]
		private global::UnityEngine.RectTransform m_Rect;

		protected global::UnityEngine.DrivenRectTransformTracker m_Tracker;

		private global::UnityEngine.Vector2 m_TotalMinSize = global::UnityEngine.Vector2.zero;

		private global::UnityEngine.Vector2 m_TotalPreferredSize = global::UnityEngine.Vector2.zero;

		private global::UnityEngine.Vector2 m_TotalFlexibleSize = global::UnityEngine.Vector2.zero;

		[global::System.NonSerialized]
		private global::System.Collections.Generic.List<global::UnityEngine.RectTransform> m_RectChildren = new global::System.Collections.Generic.List<global::UnityEngine.RectTransform>();

		public global::UnityEngine.RectOffset padding
		{
			get
			{
				return m_Padding;
			}
			set
			{
				SetProperty(ref m_Padding, value);
			}
		}

		public global::UnityEngine.TextAnchor childAlignment
		{
			get
			{
				return m_ChildAlignment;
			}
			set
			{
				SetProperty(ref m_ChildAlignment, value);
			}
		}

		protected global::UnityEngine.RectTransform rectTransform
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

		protected global::System.Collections.Generic.List<global::UnityEngine.RectTransform> rectChildren => m_RectChildren;

		public virtual float minWidth => GetTotalMinSize(0);

		public virtual float preferredWidth => GetTotalPreferredSize(0);

		public virtual float flexibleWidth => GetTotalFlexibleSize(0);

		public virtual float minHeight => GetTotalMinSize(1);

		public virtual float preferredHeight => GetTotalPreferredSize(1);

		public virtual float flexibleHeight => GetTotalFlexibleSize(1);

		public virtual int layoutPriority => 0;

		private bool isRootLayoutGroup
		{
			get
			{
				if (base.transform.parent == null)
				{
					return true;
				}
				return base.transform.parent.GetComponent(typeof(global::UnityEngine.UI.ILayoutGroup)) == null;
			}
		}

		public virtual void CalculateLayoutInputHorizontal()
		{
			m_RectChildren.Clear();
			global::System.Collections.Generic.List<global::UnityEngine.Component> list = global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Component>, global::UnityEngine.Component>.Get();
			for (int i = 0; i < this.rectTransform.childCount; i++)
			{
				global::UnityEngine.RectTransform rectTransform = this.rectTransform.GetChild(i) as global::UnityEngine.RectTransform;
				if (rectTransform == null || !rectTransform.gameObject.activeInHierarchy)
				{
					continue;
				}
				rectTransform.GetComponents(typeof(global::UnityEngine.UI.ILayoutIgnorer), list);
				if (list.Count == 0)
				{
					m_RectChildren.Add(rectTransform);
					continue;
				}
				for (int j = 0; j < list.Count; j++)
				{
					if (!((global::UnityEngine.UI.ILayoutIgnorer)list[j]).ignoreLayout)
					{
						m_RectChildren.Add(rectTransform);
						break;
					}
				}
			}
			global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Component>, global::UnityEngine.Component>.Release(list);
			m_Tracker.Clear();
		}

		public abstract void CalculateLayoutInputVertical();

		public abstract void SetLayoutHorizontal();

		public abstract void SetLayoutVertical();

		protected LayoutGroup()
		{
			if (m_Padding == null)
			{
				m_Padding = new global::UnityEngine.RectOffset();
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			rectTransform.sendChildDimensionsChange = true;
			SetDirty();
		}

		protected override void OnDisable()
		{
			m_Tracker.Clear();
			global::UnityEngine.UI.LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
			rectTransform.sendChildDimensionsChange = false;
			base.OnDisable();
		}

		protected override void OnDidApplyAnimationProperties()
		{
			SetDirty();
		}

		protected float GetTotalMinSize(int axis)
		{
			return m_TotalMinSize[axis];
		}

		protected float GetTotalPreferredSize(int axis)
		{
			return m_TotalPreferredSize[axis];
		}

		protected float GetTotalFlexibleSize(int axis)
		{
			return m_TotalFlexibleSize[axis];
		}

		protected float GetStartOffset(int axis, float requiredSpaceWithoutPadding)
		{
			float num = requiredSpaceWithoutPadding + (float)((axis == 0) ? padding.horizontal : padding.vertical);
			float num2 = rectTransform.rect.size[axis] - num;
			float alignmentOnAxis = GetAlignmentOnAxis(axis);
			return (float)((axis == 0) ? padding.left : padding.top) + num2 * alignmentOnAxis;
		}

		protected float GetAlignmentOnAxis(int axis)
		{
			if (axis == 0)
			{
				return (float)((int)childAlignment % 3) * 0.5f;
			}
			return (float)((int)childAlignment / 3) * 0.5f;
		}

		protected void SetLayoutInputForAxis(float totalMin, float totalPreferred, float totalFlexible, int axis)
		{
			m_TotalMinSize[axis] = totalMin;
			m_TotalPreferredSize[axis] = totalPreferred;
			m_TotalFlexibleSize[axis] = totalFlexible;
		}

		protected void SetChildAlongAxis(global::UnityEngine.RectTransform rect, int axis, float pos)
		{
			if (!(rect == null))
			{
				SetChildAlongAxisWithScale(rect, axis, pos, 1f);
			}
		}

		protected void SetChildAlongAxisWithScale(global::UnityEngine.RectTransform rect, int axis, float pos, float scaleFactor)
		{
			if (!(rect == null))
			{
				m_Tracker.Add(this, rect, (global::UnityEngine.DrivenTransformProperties)(0xF00 | ((axis == 0) ? 2 : 4)));
				rect.anchorMin = global::UnityEngine.Vector2.up;
				rect.anchorMax = global::UnityEngine.Vector2.up;
				global::UnityEngine.Vector2 anchoredPosition = rect.anchoredPosition;
				anchoredPosition[axis] = ((axis == 0) ? (pos + rect.sizeDelta[axis] * rect.pivot[axis] * scaleFactor) : (0f - pos - rect.sizeDelta[axis] * (1f - rect.pivot[axis]) * scaleFactor));
				rect.anchoredPosition = anchoredPosition;
			}
		}

		protected void SetChildAlongAxis(global::UnityEngine.RectTransform rect, int axis, float pos, float size)
		{
			if (!(rect == null))
			{
				SetChildAlongAxisWithScale(rect, axis, pos, size, 1f);
			}
		}

		protected void SetChildAlongAxisWithScale(global::UnityEngine.RectTransform rect, int axis, float pos, float size, float scaleFactor)
		{
			if (!(rect == null))
			{
				m_Tracker.Add(this, rect, (global::UnityEngine.DrivenTransformProperties)(0xF00 | ((axis == 0) ? 4098 : 8196)));
				rect.anchorMin = global::UnityEngine.Vector2.up;
				rect.anchorMax = global::UnityEngine.Vector2.up;
				global::UnityEngine.Vector2 sizeDelta = rect.sizeDelta;
				sizeDelta[axis] = size;
				rect.sizeDelta = sizeDelta;
				global::UnityEngine.Vector2 anchoredPosition = rect.anchoredPosition;
				anchoredPosition[axis] = ((axis == 0) ? (pos + size * rect.pivot[axis] * scaleFactor) : (0f - pos - size * (1f - rect.pivot[axis]) * scaleFactor));
				rect.anchoredPosition = anchoredPosition;
			}
		}

		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			if (isRootLayoutGroup)
			{
				SetDirty();
			}
		}

		protected virtual void OnTransformChildrenChanged()
		{
			SetDirty();
		}

		protected virtual void OnChildRectTransformDimensionsChange()
		{
			if (!global::UnityEngine.UI.CanvasUpdateRegistry.IsRebuildingLayout())
			{
				SetDirty();
			}
		}

		protected void SetProperty<T>(ref T currentValue, T newValue)
		{
			if ((currentValue != null || newValue != null) && (currentValue == null || !currentValue.Equals(newValue)))
			{
				currentValue = newValue;
				SetDirty();
			}
		}

		protected void SetDirty()
		{
			if (IsActive())
			{
				if (!global::UnityEngine.UI.CanvasUpdateRegistry.IsRebuildingLayout())
				{
					global::UnityEngine.UI.LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
				}
				else
				{
					StartCoroutine(DelayedSetDirty(rectTransform));
				}
			}
		}

		private global::System.Collections.IEnumerator DelayedSetDirty(global::UnityEngine.RectTransform rectTransform)
		{
			yield return null;
			global::UnityEngine.UI.LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
		}
	}
}
