namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(typeof(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset))]
	[global::UnityEngine.Categorization.CategoryInfo(Name = "Terrain Shader Inclusion Settings", Order = 50)]
	[global::UnityEngine.Categorization.ElementInfo(Order = 10)]
	public class URPTerrainShaderSetting : global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings
	{
		internal enum Version
		{
			Initial = 0
		}

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.Rendering.Universal.URPTerrainShaderSetting.Version m_Version;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Include terrain shaders in build even if not referenced.")]
		private bool m_IncludeTerrainShaders = true;

		public int version => (int)m_Version;

		public bool includeTerrainShaders
		{
			get
			{
				return m_IncludeTerrainShaders;
			}
			set
			{
				this.SetValueAndNotify(ref m_IncludeTerrainShaders, value, "includeTerrainShaders");
			}
		}
	}
}
