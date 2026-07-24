namespace UnityEngine.UI
{
	public abstract class MaskableGraphic : global::UnityEngine.UI.Graphic, global::UnityEngine.UI.IClippable, global::UnityEngine.UI.IMaskable, global::UnityEngine.UI.IMaterialModifier
	{
		[global::System.Serializable]
		public class CullStateChangedEvent : global::UnityEngine.Events.UnityEvent<bool>
		{
		}

		[global::System.NonSerialized]
		protected bool m_ShouldRecalculateStencil = true;

		[global::System.NonSerialized]
		protected global::UnityEngine.Material m_MaskMaterial;

		[global::System.NonSerialized]
		private global::UnityEngine.UI.RectMask2D m_ParentMask;

		[global::UnityEngine.SerializeField]
		private bool m_Maskable = true;

		private bool m_IsMaskingGraphic;

		[global::System.NonSerialized]
		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		[global::System.Obsolete("Not used anymore.", true)]
		protected bool m_IncludeForMasking;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.MaskableGraphic.CullStateChangedEvent m_OnCullStateChanged = new global::UnityEngine.UI.MaskableGraphic.CullStateChangedEvent();

		[global::System.NonSerialized]
		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		[global::System.Obsolete("Not used anymore", true)]
		protected bool m_ShouldRecalculate = true;

		[global::System.NonSerialized]
		protected int m_StencilValue;

		private readonly global::UnityEngine.Vector3[] m_Corners = new global::UnityEngine.Vector3[4];

		public global::UnityEngine.UI.MaskableGraphic.CullStateChangedEvent onCullStateChanged
		{
			get
			{
				return m_OnCullStateChanged;
			}
			set
			{
				m_OnCullStateChanged = value;
			}
		}

		public bool maskable
		{
			get
			{
				return m_Maskable;
			}
			set
			{
				if (value != m_Maskable)
				{
					m_Maskable = value;
					m_ShouldRecalculateStencil = true;
					SetMaterialDirty();
				}
			}
		}

		public bool isMaskingGraphic
		{
			get
			{
				return m_IsMaskingGraphic;
			}
			set
			{
				if (value != m_IsMaskingGraphic)
				{
					m_IsMaskingGraphic = value;
				}
			}
		}

		private global::UnityEngine.Rect rootCanvasRect
		{
			get
			{
				base.rectTransform.GetWorldCorners(m_Corners);
				if ((bool)base.canvas)
				{
					global::UnityEngine.Matrix4x4 worldToLocalMatrix = base.canvas.rootCanvas.transform.worldToLocalMatrix;
					for (int i = 0; i < 4; i++)
					{
						m_Corners[i] = worldToLocalMatrix.MultiplyPoint(m_Corners[i]);
					}
				}
				global::UnityEngine.Vector2 vector = m_Corners[0];
				global::UnityEngine.Vector2 vector2 = m_Corners[0];
				for (int j = 1; j < 4; j++)
				{
					vector.x = global::UnityEngine.Mathf.Min(m_Corners[j].x, vector.x);
					vector.y = global::UnityEngine.Mathf.Min(m_Corners[j].y, vector.y);
					vector2.x = global::UnityEngine.Mathf.Max(m_Corners[j].x, vector2.x);
					vector2.y = global::UnityEngine.Mathf.Max(m_Corners[j].y, vector2.y);
				}
				return new global::UnityEngine.Rect(vector, vector2 - vector);
			}
		}

		global::UnityEngine.GameObject global::UnityEngine.UI.IClippable.gameObject => base.gameObject;

		public virtual global::UnityEngine.Material GetModifiedMaterial(global::UnityEngine.Material baseMaterial)
		{
			global::UnityEngine.Material material = baseMaterial;
			if (m_ShouldRecalculateStencil)
			{
				if (maskable)
				{
					global::UnityEngine.Transform stopAfter = global::UnityEngine.UI.MaskUtilities.FindRootSortOverrideCanvas(base.transform);
					m_StencilValue = global::UnityEngine.UI.MaskUtilities.GetStencilDepth(base.transform, stopAfter);
				}
				else
				{
					m_StencilValue = 0;
				}
				m_ShouldRecalculateStencil = false;
			}
			if (m_StencilValue > 0 && !isMaskingGraphic)
			{
				global::UnityEngine.Material maskMaterial = global::UnityEngine.UI.StencilMaterial.Add(material, (1 << m_StencilValue) - 1, global::UnityEngine.Rendering.StencilOp.Keep, global::UnityEngine.Rendering.CompareFunction.Equal, global::UnityEngine.Rendering.ColorWriteMask.All, (1 << m_StencilValue) - 1, 0);
				global::UnityEngine.UI.StencilMaterial.Remove(m_MaskMaterial);
				m_MaskMaterial = maskMaterial;
				material = m_MaskMaterial;
			}
			return material;
		}

		public virtual void Cull(global::UnityEngine.Rect clipRect, bool validRect)
		{
			bool cull = !validRect || !clipRect.Overlaps(rootCanvasRect, allowInverse: true);
			UpdateCull(cull);
		}

		private void UpdateCull(bool cull)
		{
			if (base.canvasRenderer.cull != cull)
			{
				base.canvasRenderer.cull = cull;
				global::UnityEngine.UISystemProfilerApi.AddMarker("MaskableGraphic.cullingChanged", this);
				m_OnCullStateChanged.Invoke(cull);
				OnCullingChanged();
			}
		}

		public virtual void SetClipRect(global::UnityEngine.Rect clipRect, bool validRect)
		{
			if (validRect)
			{
				base.canvasRenderer.EnableRectClipping(clipRect);
			}
			else
			{
				base.canvasRenderer.DisableRectClipping();
			}
		}

		public virtual void SetClipSoftness(global::UnityEngine.Vector2 clipSoftness)
		{
			base.canvasRenderer.clippingSoftness = clipSoftness;
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			m_ShouldRecalculateStencil = true;
			UpdateClipParent();
			SetMaterialDirty();
			if (isMaskingGraphic)
			{
				global::UnityEngine.UI.MaskUtilities.NotifyStencilStateChanged(this);
			}
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			m_ShouldRecalculateStencil = true;
			SetMaterialDirty();
			UpdateClipParent();
			global::UnityEngine.UI.StencilMaterial.Remove(m_MaskMaterial);
			m_MaskMaterial = null;
			if (isMaskingGraphic)
			{
				global::UnityEngine.UI.MaskUtilities.NotifyStencilStateChanged(this);
			}
		}

		protected override void OnTransformParentChanged()
		{
			base.OnTransformParentChanged();
			if (base.isActiveAndEnabled)
			{
				m_ShouldRecalculateStencil = true;
				UpdateClipParent();
				SetMaterialDirty();
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		[global::System.Obsolete("Not used anymore.", true)]
		public virtual void ParentMaskStateChanged()
		{
		}

		protected override void OnCanvasHierarchyChanged()
		{
			base.OnCanvasHierarchyChanged();
			if (base.isActiveAndEnabled)
			{
				m_ShouldRecalculateStencil = true;
				UpdateClipParent();
				SetMaterialDirty();
			}
		}

		private void UpdateClipParent()
		{
			global::UnityEngine.UI.RectMask2D rectMask2D = ((maskable && IsActive()) ? global::UnityEngine.UI.MaskUtilities.GetRectMaskForClippable(this) : null);
			if (m_ParentMask != null && (rectMask2D != m_ParentMask || !rectMask2D.IsActive()))
			{
				m_ParentMask.RemoveClippable(this);
				UpdateCull(cull: false);
			}
			if (rectMask2D != null && rectMask2D.IsActive())
			{
				rectMask2D.AddClippable(this);
			}
			m_ParentMask = rectMask2D;
		}

		public virtual void RecalculateClipping()
		{
			UpdateClipParent();
		}

		public virtual void RecalculateMasking()
		{
			global::UnityEngine.UI.StencilMaterial.Remove(m_MaskMaterial);
			m_MaskMaterial = null;
			m_ShouldRecalculateStencil = true;
			SetMaterialDirty();
		}

		public override bool Raycast(global::UnityEngine.Vector2 sp, global::UnityEngine.Camera eventCamera)
		{
			return Raycast(sp, eventCamera, !maskable);
		}
	}
}
