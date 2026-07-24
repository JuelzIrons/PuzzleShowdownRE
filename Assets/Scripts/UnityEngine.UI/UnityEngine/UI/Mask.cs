namespace UnityEngine.UI
{
	[global::UnityEngine.AddComponentMenu("UI (Canvas)/Mask", 13)]
	[global::UnityEngine.ExecuteAlways]
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.RectTransform))]
	[global::UnityEngine.DisallowMultipleComponent]
	public class Mask : global::UnityEngine.EventSystems.UIBehaviour, global::UnityEngine.ICanvasRaycastFilter, global::UnityEngine.UI.IMaterialModifier
	{
		[global::System.NonSerialized]
		private global::UnityEngine.RectTransform m_RectTransform;

		[global::UnityEngine.SerializeField]
		private bool m_ShowMaskGraphic = true;

		[global::System.NonSerialized]
		private global::UnityEngine.UI.Graphic m_Graphic;

		[global::System.NonSerialized]
		private global::UnityEngine.Material m_MaskMaterial;

		[global::System.NonSerialized]
		private global::UnityEngine.Material m_UnmaskMaterial;

		public global::UnityEngine.RectTransform rectTransform => m_RectTransform ?? (m_RectTransform = GetComponent<global::UnityEngine.RectTransform>());

		public bool showMaskGraphic
		{
			get
			{
				return m_ShowMaskGraphic;
			}
			set
			{
				if (m_ShowMaskGraphic != value)
				{
					m_ShowMaskGraphic = value;
					if (graphic != null)
					{
						graphic.SetMaterialDirty();
					}
				}
			}
		}

		public global::UnityEngine.UI.Graphic graphic => m_Graphic ?? (m_Graphic = GetComponent<global::UnityEngine.UI.Graphic>());

		protected Mask()
		{
		}

		public virtual bool MaskEnabled()
		{
			if (IsActive())
			{
				return graphic != null;
			}
			return false;
		}

		[global::System.Obsolete("Not used anymore.")]
		public virtual void OnSiblingGraphicEnabledDisabled()
		{
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			if (graphic != null)
			{
				graphic.canvasRenderer.hasPopInstruction = true;
				graphic.SetMaterialDirty();
				if (graphic is global::UnityEngine.UI.MaskableGraphic)
				{
					(graphic as global::UnityEngine.UI.MaskableGraphic).isMaskingGraphic = true;
				}
			}
			global::UnityEngine.UI.MaskUtilities.NotifyStencilStateChanged(this);
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			if (graphic != null)
			{
				graphic.SetMaterialDirty();
				graphic.canvasRenderer.hasPopInstruction = false;
				graphic.canvasRenderer.popMaterialCount = 0;
				if (graphic is global::UnityEngine.UI.MaskableGraphic)
				{
					(graphic as global::UnityEngine.UI.MaskableGraphic).isMaskingGraphic = false;
				}
			}
			global::UnityEngine.UI.StencilMaterial.Remove(m_MaskMaterial);
			m_MaskMaterial = null;
			global::UnityEngine.UI.StencilMaterial.Remove(m_UnmaskMaterial);
			m_UnmaskMaterial = null;
			global::UnityEngine.UI.MaskUtilities.NotifyStencilStateChanged(this);
		}

		public virtual bool IsRaycastLocationValid(global::UnityEngine.Vector2 sp, global::UnityEngine.Camera eventCamera)
		{
			if (!base.isActiveAndEnabled)
			{
				return true;
			}
			return global::UnityEngine.RectTransformUtility.RectangleContainsScreenPoint(rectTransform, sp, eventCamera);
		}

		public virtual global::UnityEngine.Material GetModifiedMaterial(global::UnityEngine.Material baseMaterial)
		{
			if (!MaskEnabled())
			{
				return baseMaterial;
			}
			global::UnityEngine.Transform stopAfter = global::UnityEngine.UI.MaskUtilities.FindRootSortOverrideCanvas(base.transform);
			int stencilDepth = global::UnityEngine.UI.MaskUtilities.GetStencilDepth(base.transform, stopAfter);
			if (stencilDepth >= 8)
			{
				global::UnityEngine.Debug.LogWarning("Attempting to use a stencil mask with depth > 8", base.gameObject);
				return baseMaterial;
			}
			int num = 1 << stencilDepth;
			if (num == 1)
			{
				global::UnityEngine.Material maskMaterial = global::UnityEngine.UI.StencilMaterial.Add(baseMaterial, 1, global::UnityEngine.Rendering.StencilOp.Replace, global::UnityEngine.Rendering.CompareFunction.Always, m_ShowMaskGraphic ? global::UnityEngine.Rendering.ColorWriteMask.All : ((global::UnityEngine.Rendering.ColorWriteMask)0));
				global::UnityEngine.UI.StencilMaterial.Remove(m_MaskMaterial);
				m_MaskMaterial = maskMaterial;
				global::UnityEngine.Material unmaskMaterial = global::UnityEngine.UI.StencilMaterial.Add(baseMaterial, 1, global::UnityEngine.Rendering.StencilOp.Zero, global::UnityEngine.Rendering.CompareFunction.Always, (global::UnityEngine.Rendering.ColorWriteMask)0);
				global::UnityEngine.UI.StencilMaterial.Remove(m_UnmaskMaterial);
				m_UnmaskMaterial = unmaskMaterial;
				graphic.canvasRenderer.popMaterialCount = 1;
				graphic.canvasRenderer.SetPopMaterial(m_UnmaskMaterial, 0);
				return m_MaskMaterial;
			}
			global::UnityEngine.Material maskMaterial2 = global::UnityEngine.UI.StencilMaterial.Add(baseMaterial, num | (num - 1), global::UnityEngine.Rendering.StencilOp.Replace, global::UnityEngine.Rendering.CompareFunction.Equal, m_ShowMaskGraphic ? global::UnityEngine.Rendering.ColorWriteMask.All : ((global::UnityEngine.Rendering.ColorWriteMask)0), num - 1, num | (num - 1));
			global::UnityEngine.UI.StencilMaterial.Remove(m_MaskMaterial);
			m_MaskMaterial = maskMaterial2;
			graphic.canvasRenderer.hasPopInstruction = true;
			global::UnityEngine.Material unmaskMaterial2 = global::UnityEngine.UI.StencilMaterial.Add(baseMaterial, num - 1, global::UnityEngine.Rendering.StencilOp.Replace, global::UnityEngine.Rendering.CompareFunction.Equal, (global::UnityEngine.Rendering.ColorWriteMask)0, num - 1, num | (num - 1));
			global::UnityEngine.UI.StencilMaterial.Remove(m_UnmaskMaterial);
			m_UnmaskMaterial = unmaskMaterial2;
			graphic.canvasRenderer.popMaterialCount = 1;
			graphic.canvasRenderer.SetPopMaterial(m_UnmaskMaterial, 0);
			return m_MaskMaterial;
		}
	}
}
