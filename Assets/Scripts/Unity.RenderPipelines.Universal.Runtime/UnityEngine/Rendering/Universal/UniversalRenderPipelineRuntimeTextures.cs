namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(typeof(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset))]
	[global::UnityEngine.Categorization.CategoryInfo(Name = "R: Runtime Textures", Order = 1000)]
	[global::UnityEngine.HideInInspector]
	public class UniversalRenderPipelineRuntimeTextures : global::UnityEngine.Rendering.IRenderPipelineResources, global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings
	{
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private int m_Version = 1;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Textures/BlueNoise64/L/LDR_LLL1_0.png", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.Texture2D m_BlueNoise64LTex;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Textures/BayerMatrix.png", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.Texture2D m_BayerMatrixTex;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Textures/DebugFont.tga", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.Texture2D m_DebugFontTex;

		private global::UnityEngine.Texture2D m_StencilDitherTex;

		public int version => m_Version;

		bool global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings.isAvailableInPlayerBuild => true;

		public global::UnityEngine.Texture2D blueNoise64LTex
		{
			get
			{
				return m_BlueNoise64LTex;
			}
			set
			{
				this.SetValueAndNotify(ref m_BlueNoise64LTex, value, "m_BlueNoise64LTex");
			}
		}

		public global::UnityEngine.Texture2D bayerMatrixTex
		{
			get
			{
				return m_BayerMatrixTex;
			}
			set
			{
				this.SetValueAndNotify(ref m_BayerMatrixTex, value, "m_BayerMatrixTex");
			}
		}

		public global::UnityEngine.Texture2D debugFontTexture
		{
			get
			{
				return m_DebugFontTex;
			}
			set
			{
				this.SetValueAndNotify(ref m_DebugFontTex, value, "m_DebugFontTex");
			}
		}

		public global::UnityEngine.Texture2D stencilDitherTex
		{
			get
			{
				if (!m_StencilDitherTex)
				{
					m_StencilDitherTex = new global::UnityEngine.Texture2D(2, 2, global::UnityEngine.TextureFormat.Alpha8, mipChain: false, linear: true);
					m_StencilDitherTex.SetPixel(0, 0, global::UnityEngine.Color.red * 0.25f);
					m_StencilDitherTex.SetPixel(1, 1, global::UnityEngine.Color.red * 0.5f);
					m_StencilDitherTex.SetPixel(0, 1, global::UnityEngine.Color.red * 0.75f);
					m_StencilDitherTex.SetPixel(1, 0, global::UnityEngine.Color.red * 1f);
					m_StencilDitherTex.Apply();
				}
				return m_StencilDitherTex;
			}
		}
	}
}
