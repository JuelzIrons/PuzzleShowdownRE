namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.ReloadGroup]
	[global::UnityEngine.ExcludeFromPreset]
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom(true, "UnityEngine.Experimental.Rendering.Universal", "Unity.RenderPipelines.Universal.Runtime", null)]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@latest/index.html?subfolder=/manual/2DRendererData-overview.html")]
	public class Renderer2DData : global::UnityEngine.Rendering.Universal.ScriptableRendererData
	{
		internal enum Renderer2DDefaultMaterialType
		{
			Lit = 0,
			Unlit = 1,
			Custom = 2
		}

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.LayerMask m_LayerMask = -1;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.TransparencySortMode m_TransparencySortMode;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Vector3 m_TransparencySortAxis = global::UnityEngine.Vector3.up;

		[global::UnityEngine.SerializeField]
		private float m_HDREmulationScale = 1f;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Range(0.01f, 1f)]
		private float m_LightRenderTextureScale = 0.5f;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("m_LightOperations")]
		private global::UnityEngine.Rendering.Universal.Light2DBlendStyle[] m_LightBlendStyles;

		[global::UnityEngine.SerializeField]
		private bool m_UseDepthStencilBuffer = true;

		[global::UnityEngine.SerializeField]
		private bool m_UseCameraSortingLayersTexture;

		[global::UnityEngine.SerializeField]
		private int m_CameraSortingLayersTextureBound;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.Downsampling m_CameraSortingLayerDownsamplingMethod;

		[global::UnityEngine.SerializeField]
		private uint m_MaxLightRenderTextureCount = 16u;

		[global::UnityEngine.SerializeField]
		private uint m_MaxShadowRenderTextureCount = 1u;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.PostProcessData m_PostProcessData;

		internal global::UnityEngine.Rendering.RTHandle normalsRenderTarget;

		internal global::UnityEngine.Rendering.RTHandle cameraSortingLayerRenderTarget;

		public float hdrEmulationScale => m_HDREmulationScale;

		internal float lightRenderTextureScale => m_LightRenderTextureScale;

		public global::UnityEngine.Rendering.Universal.Light2DBlendStyle[] lightBlendStyles => m_LightBlendStyles;

		internal bool useDepthStencilBuffer => m_UseDepthStencilBuffer;

		internal global::UnityEngine.Rendering.Universal.PostProcessData postProcessData
		{
			get
			{
				return m_PostProcessData;
			}
			set
			{
				m_PostProcessData = value;
			}
		}

		internal global::UnityEngine.TransparencySortMode transparencySortMode => m_TransparencySortMode;

		internal global::UnityEngine.Vector3 transparencySortAxis => m_TransparencySortAxis;

		internal uint lightRenderTextureMemoryBudget => m_MaxLightRenderTextureCount;

		internal uint shadowRenderTextureMemoryBudget => m_MaxShadowRenderTextureCount;

		internal bool useCameraSortingLayerTexture => m_UseCameraSortingLayersTexture;

		internal int cameraSortingLayerTextureBound => m_CameraSortingLayersTextureBound;

		internal global::UnityEngine.Rendering.Universal.Downsampling cameraSortingLayerDownsamplingMethod => m_CameraSortingLayerDownsamplingMethod;

		internal global::UnityEngine.LayerMask layerMask => m_LayerMask;

		internal global::System.Collections.Generic.Dictionary<uint, global::UnityEngine.Material> lightMaterials { get; } = new global::System.Collections.Generic.Dictionary<uint, global::UnityEngine.Material>();

		internal global::UnityEngine.Material spriteSelfShadowMaterial { get; set; }

		internal global::UnityEngine.Material spriteUnshadowMaterial { get; set; }

		internal global::UnityEngine.Material geometrySelfShadowMaterial { get; set; }

		internal global::UnityEngine.Material geometryUnshadowMaterial { get; set; }

		internal global::UnityEngine.Material projectedShadowMaterial { get; set; }

		internal global::UnityEngine.Material projectedUnshadowMaterial { get; set; }

		internal global::UnityEngine.Rendering.Universal.ILight2DCullResult lightCullResult { get; set; }

		protected override global::UnityEngine.Rendering.Universal.ScriptableRenderer Create()
		{
			global::UnityEngine.RenderAs2DUtil.InitializeCanRenderAs2D();
			return new global::UnityEngine.Rendering.Universal.Renderer2D(this);
		}

		internal void Dispose()
		{
			global::UnityEngine.RenderAs2DUtil.DisposeCanRenderAs2D();
			foreach (global::System.Collections.Generic.KeyValuePair<uint, global::UnityEngine.Material> lightMaterial in lightMaterials)
			{
				global::UnityEngine.Rendering.CoreUtils.Destroy(lightMaterial.Value);
			}
			lightMaterials.Clear();
			global::UnityEngine.Rendering.CoreUtils.Destroy(spriteSelfShadowMaterial);
			global::UnityEngine.Rendering.CoreUtils.Destroy(spriteUnshadowMaterial);
			global::UnityEngine.Rendering.CoreUtils.Destroy(geometrySelfShadowMaterial);
			global::UnityEngine.Rendering.CoreUtils.Destroy(geometryUnshadowMaterial);
			global::UnityEngine.Rendering.CoreUtils.Destroy(projectedShadowMaterial);
			global::UnityEngine.Rendering.CoreUtils.Destroy(projectedUnshadowMaterial);
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			geometrySelfShadowMaterial = null;
			geometryUnshadowMaterial = null;
			spriteSelfShadowMaterial = null;
			spriteUnshadowMaterial = null;
			projectedShadowMaterial = null;
			projectedUnshadowMaterial = null;
		}
	}
}
