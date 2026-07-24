namespace UnityEngine.UI
{
	[global::UnityEngine.AddComponentMenu("UI (Canvas)/Rect Mask 2D", 14)]
	[global::UnityEngine.ExecuteAlways]
	[global::UnityEngine.DisallowMultipleComponent]
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.RectTransform))]
	public class RectMask2D : global::UnityEngine.EventSystems.UIBehaviour, global::UnityEngine.UI.IClipper, global::UnityEngine.ICanvasRaycastFilter
	{
		[global::System.NonSerialized]
		private readonly global::UnityEngine.UI.RectangularVertexClipper m_VertexClipper = new global::UnityEngine.UI.RectangularVertexClipper();

		[global::System.NonSerialized]
		private global::UnityEngine.RectTransform m_RectTransform;

		[global::System.NonSerialized]
		private global::System.Collections.Generic.HashSet<global::UnityEngine.UI.MaskableGraphic> m_MaskableTargets = new global::System.Collections.Generic.HashSet<global::UnityEngine.UI.MaskableGraphic>();

		[global::System.NonSerialized]
		private global::System.Collections.Generic.HashSet<global::UnityEngine.UI.IClippable> m_ClipTargets = new global::System.Collections.Generic.HashSet<global::UnityEngine.UI.IClippable>();

		[global::System.NonSerialized]
		private bool m_ShouldRecalculateClipRects;

		[global::System.NonSerialized]
		private global::System.Collections.Generic.List<global::UnityEngine.UI.RectMask2D> m_Clippers = new global::System.Collections.Generic.List<global::UnityEngine.UI.RectMask2D>();

		[global::System.NonSerialized]
		private global::UnityEngine.Rect m_LastClipRectCanvasSpace;

		[global::System.NonSerialized]
		private bool m_ForceClip;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Vector4 m_Padding;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Vector2Int m_Softness;

		[global::System.NonSerialized]
		private global::UnityEngine.Canvas m_Canvas;

		private global::UnityEngine.Vector3[] m_Corners = new global::UnityEngine.Vector3[4];

		public global::UnityEngine.Vector4 padding
		{
			get
			{
				return m_Padding;
			}
			set
			{
				m_Padding = value;
				global::UnityEngine.UI.MaskUtilities.Notify2DMaskStateChanged(this);
			}
		}

		public global::UnityEngine.Vector2Int softness
		{
			get
			{
				return m_Softness;
			}
			set
			{
				m_Softness.x = global::UnityEngine.Mathf.Max(0, value.x);
				m_Softness.y = global::UnityEngine.Mathf.Max(0, value.y);
				global::UnityEngine.UI.MaskUtilities.Notify2DMaskStateChanged(this);
			}
		}

		internal global::UnityEngine.Canvas Canvas
		{
			get
			{
				if (m_Canvas == null)
				{
					global::System.Collections.Generic.List<global::UnityEngine.Canvas> list = global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Canvas>, global::UnityEngine.Canvas>.Get();
					base.gameObject.GetComponentsInParent(includeInactive: false, list);
					if (list.Count > 0)
					{
						m_Canvas = list[list.Count - 1];
					}
					else
					{
						m_Canvas = null;
					}
					global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Canvas>, global::UnityEngine.Canvas>.Release(list);
				}
				return m_Canvas;
			}
		}

		public global::UnityEngine.Rect canvasRect => m_VertexClipper.GetCanvasRect(rectTransform, Canvas);

		public global::UnityEngine.RectTransform rectTransform => m_RectTransform ?? (m_RectTransform = GetComponent<global::UnityEngine.RectTransform>());

		private global::UnityEngine.Rect rootCanvasRect
		{
			get
			{
				rectTransform.GetWorldCorners(m_Corners);
				if ((object)Canvas != null)
				{
					global::UnityEngine.Canvas rootCanvas = Canvas.rootCanvas;
					for (int i = 0; i < 4; i++)
					{
						m_Corners[i] = rootCanvas.transform.InverseTransformPoint(m_Corners[i]);
					}
				}
				return new global::UnityEngine.Rect(m_Corners[0].x, m_Corners[0].y, m_Corners[2].x - m_Corners[0].x, m_Corners[2].y - m_Corners[0].y);
			}
		}

		protected RectMask2D()
		{
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			m_ShouldRecalculateClipRects = true;
			global::UnityEngine.UI.ClipperRegistry.Register(this);
			global::UnityEngine.UI.MaskUtilities.Notify2DMaskStateChanged(this);
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			m_ClipTargets.Clear();
			m_MaskableTargets.Clear();
			m_Clippers.Clear();
			global::UnityEngine.UI.ClipperRegistry.Disable(this);
			global::UnityEngine.UI.MaskUtilities.Notify2DMaskStateChanged(this);
		}

		protected override void OnDestroy()
		{
			global::UnityEngine.UI.ClipperRegistry.Unregister(this);
			base.OnDestroy();
		}

		public virtual bool IsRaycastLocationValid(global::UnityEngine.Vector2 sp, global::UnityEngine.Camera eventCamera)
		{
			if (!base.isActiveAndEnabled)
			{
				return true;
			}
			return global::UnityEngine.RectTransformUtility.RectangleContainsScreenPoint(rectTransform, sp, eventCamera, m_Padding);
		}

		public virtual void PerformClipping()
		{
			if ((object)Canvas == null)
			{
				return;
			}
			if (m_ShouldRecalculateClipRects)
			{
				global::UnityEngine.UI.MaskUtilities.GetRectMasksForClip(this, m_Clippers);
				m_ShouldRecalculateClipRects = false;
			}
			bool validRect = true;
			global::UnityEngine.Rect rect = global::UnityEngine.UI.Clipping.FindCullAndClipWorldRect(m_Clippers, out validRect);
			global::UnityEngine.RenderMode renderMode = Canvas.rootCanvas.renderMode;
			if ((renderMode == global::UnityEngine.RenderMode.ScreenSpaceCamera || renderMode == global::UnityEngine.RenderMode.ScreenSpaceOverlay) && !rect.Overlaps(rootCanvasRect, allowInverse: true))
			{
				rect = global::UnityEngine.Rect.zero;
				validRect = false;
			}
			if (rect != m_LastClipRectCanvasSpace)
			{
				foreach (global::UnityEngine.UI.IClippable clipTarget in m_ClipTargets)
				{
					clipTarget.SetClipRect(rect, validRect);
				}
				foreach (global::UnityEngine.UI.MaskableGraphic maskableTarget in m_MaskableTargets)
				{
					maskableTarget.SetClipRect(rect, validRect);
					maskableTarget.Cull(rect, validRect);
				}
			}
			else if (m_ForceClip)
			{
				foreach (global::UnityEngine.UI.IClippable clipTarget2 in m_ClipTargets)
				{
					clipTarget2.SetClipRect(rect, validRect);
				}
				foreach (global::UnityEngine.UI.MaskableGraphic maskableTarget2 in m_MaskableTargets)
				{
					maskableTarget2.SetClipRect(rect, validRect);
					if (maskableTarget2.canvasRenderer.hasMoved)
					{
						maskableTarget2.Cull(rect, validRect);
					}
				}
			}
			else
			{
				foreach (global::UnityEngine.UI.MaskableGraphic maskableTarget3 in m_MaskableTargets)
				{
					maskableTarget3.Cull(rect, validRect);
				}
			}
			m_LastClipRectCanvasSpace = rect;
			m_ForceClip = false;
			UpdateClipSoftness();
		}

		public virtual void UpdateClipSoftness()
		{
			if ((object)Canvas == null)
			{
				return;
			}
			foreach (global::UnityEngine.UI.IClippable clipTarget in m_ClipTargets)
			{
				clipTarget.SetClipSoftness(m_Softness);
			}
			foreach (global::UnityEngine.UI.MaskableGraphic maskableTarget in m_MaskableTargets)
			{
				maskableTarget.SetClipSoftness(m_Softness);
			}
		}

		public void AddClippable(global::UnityEngine.UI.IClippable clippable)
		{
			if (clippable != null)
			{
				m_ShouldRecalculateClipRects = true;
				global::UnityEngine.UI.MaskableGraphic maskableGraphic = clippable as global::UnityEngine.UI.MaskableGraphic;
				if (maskableGraphic == null)
				{
					m_ClipTargets.Add(clippable);
				}
				else
				{
					m_MaskableTargets.Add(maskableGraphic);
				}
				m_ForceClip = true;
			}
		}

		public void RemoveClippable(global::UnityEngine.UI.IClippable clippable)
		{
			if (clippable != null)
			{
				m_ShouldRecalculateClipRects = true;
				clippable.SetClipRect(default(global::UnityEngine.Rect), validRect: false);
				global::UnityEngine.UI.MaskableGraphic maskableGraphic = clippable as global::UnityEngine.UI.MaskableGraphic;
				if (maskableGraphic == null)
				{
					m_ClipTargets.Remove(clippable);
				}
				else
				{
					m_MaskableTargets.Remove(maskableGraphic);
				}
				m_ForceClip = true;
			}
		}

		protected override void OnTransformParentChanged()
		{
			m_Canvas = null;
			base.OnTransformParentChanged();
			m_ShouldRecalculateClipRects = true;
		}

		protected override void OnCanvasHierarchyChanged()
		{
			m_Canvas = null;
			base.OnCanvasHierarchyChanged();
			m_ShouldRecalculateClipRects = true;
		}
	}
}
