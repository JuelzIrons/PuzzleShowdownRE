namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(typeof(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset))]
	[global::UnityEngine.Categorization.CategoryInfo(Name = "R: Runtime Shaders", Order = 1000)]
	[global::UnityEngine.HideInInspector]
	public class UniversalRenderPipelineRuntimeShaders : global::UnityEngine.Rendering.IRenderPipelineResources, global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings
	{
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private int m_Version;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Shaders/Utils/FallbackError.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.Shader m_FallbackErrorShader;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Shaders/Utils/BlitHDROverlay.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		internal global::UnityEngine.Shader m_BlitHDROverlay;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Shaders/Utils/CoreBlit.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		internal global::UnityEngine.Shader m_CoreBlitPS;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Shaders/Utils/CoreBlitColorAndDepth.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		internal global::UnityEngine.Shader m_CoreBlitColorAndDepthPS;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Shaders/Utils/Sampling.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.Shader m_SamplingPS;

		[global::UnityEngine.Header("Terrain")]
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.Shader m_TerrainDetailLit;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.Shader m_TerrainDetailGrassBillboard;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.Shader m_TerrainDetailGrass;

		public int version => m_Version;

		bool global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings.isAvailableInPlayerBuild => true;

		public global::UnityEngine.Shader fallbackErrorShader
		{
			get
			{
				return m_FallbackErrorShader;
			}
			set
			{
				this.SetValueAndNotify(ref m_FallbackErrorShader, value, "m_FallbackErrorShader");
			}
		}

		public global::UnityEngine.Shader blitHDROverlay
		{
			get
			{
				return m_BlitHDROverlay;
			}
			set
			{
				this.SetValueAndNotify(ref m_BlitHDROverlay, value, "m_BlitHDROverlay");
			}
		}

		public global::UnityEngine.Shader coreBlitPS
		{
			get
			{
				return m_CoreBlitPS;
			}
			set
			{
				this.SetValueAndNotify(ref m_CoreBlitPS, value, "m_CoreBlitPS");
			}
		}

		public global::UnityEngine.Shader coreBlitColorAndDepthPS
		{
			get
			{
				return m_CoreBlitColorAndDepthPS;
			}
			set
			{
				this.SetValueAndNotify(ref m_CoreBlitColorAndDepthPS, value, "m_CoreBlitColorAndDepthPS");
			}
		}

		public global::UnityEngine.Shader samplingPS
		{
			get
			{
				return m_SamplingPS;
			}
			set
			{
				this.SetValueAndNotify(ref m_SamplingPS, value, "m_SamplingPS");
			}
		}

		[global::System.Obsolete("terrainDetailLitShader is obsolete. Use UniversalRenderPipelineRuntimeTerrainShaders.terrainDetailLitShader instead.", false)]
		public global::UnityEngine.Shader terrainDetailLitShader
		{
			get
			{
				if (global::UnityEngine.Rendering.GraphicsSettings.TryGetRenderPipelineSettings<global::UnityEngine.Rendering.Universal.UniversalRenderPipelineRuntimeTerrainShaders>(out var settings))
				{
					return settings.terrainDetailLitShader;
				}
				return null;
			}
			set
			{
				if (global::UnityEngine.Rendering.GraphicsSettings.TryGetRenderPipelineSettings<global::UnityEngine.Rendering.Universal.UniversalRenderPipelineRuntimeTerrainShaders>(out var settings))
				{
					settings.terrainDetailLitShader = value;
				}
			}
		}

		[global::System.Obsolete("terrainDetailGrassBillboardShader is obsolete. Use UniversalRenderPipelineRuntimeTerrainShaders.terrainDetailGrassBillboardShader instead.", false)]
		public global::UnityEngine.Shader terrainDetailGrassBillboardShader
		{
			get
			{
				if (global::UnityEngine.Rendering.GraphicsSettings.TryGetRenderPipelineSettings<global::UnityEngine.Rendering.Universal.UniversalRenderPipelineRuntimeTerrainShaders>(out var settings))
				{
					return settings.terrainDetailGrassBillboardShader;
				}
				return null;
			}
			set
			{
				if (global::UnityEngine.Rendering.GraphicsSettings.TryGetRenderPipelineSettings<global::UnityEngine.Rendering.Universal.UniversalRenderPipelineRuntimeTerrainShaders>(out var settings))
				{
					settings.terrainDetailGrassBillboardShader = value;
				}
			}
		}

		[global::System.Obsolete("terrainDetailGrassShader is obsolete; Use UniversalRenderPipelineRuntimeTerrainShaders.terrainDetailGrassShader instead.)", false)]
		public global::UnityEngine.Shader terrainDetailGrassShader
		{
			get
			{
				if (global::UnityEngine.Rendering.GraphicsSettings.TryGetRenderPipelineSettings<global::UnityEngine.Rendering.Universal.UniversalRenderPipelineRuntimeTerrainShaders>(out var settings))
				{
					return settings.terrainDetailGrassShader;
				}
				return null;
			}
			set
			{
				if (global::UnityEngine.Rendering.GraphicsSettings.TryGetRenderPipelineSettings<global::UnityEngine.Rendering.Universal.UniversalRenderPipelineRuntimeTerrainShaders>(out var settings))
				{
					settings.terrainDetailGrassShader = value;
				}
			}
		}

		internal global::UnityEngine.Shader GetOriginalTerrainDetailLitShader()
		{
			return m_TerrainDetailLit;
		}

		internal global::UnityEngine.Shader GetOriginalTerrainDetailGrassBillboardShader()
		{
			return m_TerrainDetailGrassBillboard;
		}

		internal global::UnityEngine.Shader GetOriginalTerrainDetailGrassShader()
		{
			return m_TerrainDetailGrass;
		}

		internal void ClearOriginalTerrainDetailShaders()
		{
			m_TerrainDetailLit = null;
			m_TerrainDetailGrassBillboard = null;
			m_TerrainDetailGrass = null;
		}
	}
}
