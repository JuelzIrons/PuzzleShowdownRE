namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(typeof(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset))]
	[global::UnityEngine.Categorization.CategoryInfo(Name = "R: Runtime Shaders", Order = 1000)]
	[global::UnityEngine.HideInInspector]
	public class UniversalRenderPipelineRuntimeTerrainShaders : global::UnityEngine.Rendering.IRenderPipelineResources, global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings
	{
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private int m_Version;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Shaders/Terrain/TerrainDetailLit.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.Shader m_TerrainDetailLit;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Shaders/Terrain/WavingGrassBillboard.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.Shader m_TerrainDetailGrassBillboard;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Shaders/Terrain/WavingGrass.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.Shader m_TerrainDetailGrass;

		public int version => m_Version;

		bool global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings.isAvailableInPlayerBuild
		{
			get
			{
				if (global::UnityEngine.Rendering.GraphicsSettings.TryGetRenderPipelineSettings<global::UnityEngine.Rendering.Universal.URPTerrainShaderSetting>(out var settings))
				{
					return settings.includeTerrainShaders;
				}
				return false;
			}
		}

		public global::UnityEngine.Shader terrainDetailLitShader
		{
			get
			{
				return m_TerrainDetailLit;
			}
			set
			{
				this.SetValueAndNotify(ref m_TerrainDetailLit, value, "terrainDetailLitShader");
			}
		}

		public global::UnityEngine.Shader terrainDetailGrassBillboardShader
		{
			get
			{
				return m_TerrainDetailGrassBillboard;
			}
			set
			{
				this.SetValueAndNotify(ref m_TerrainDetailGrassBillboard, value, "terrainDetailGrassBillboardShader");
			}
		}

		public global::UnityEngine.Shader terrainDetailGrassShader
		{
			get
			{
				return m_TerrainDetailGrass;
			}
			set
			{
				this.SetValueAndNotify(ref m_TerrainDetailGrass, value, "terrainDetailGrassShader");
			}
		}
	}
}
