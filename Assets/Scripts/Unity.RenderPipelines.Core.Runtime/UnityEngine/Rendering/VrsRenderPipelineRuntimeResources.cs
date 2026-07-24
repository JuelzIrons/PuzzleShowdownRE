namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(new global::System.Type[] { })]
	[global::UnityEngine.Categorization.CategoryInfo(Name = "VRS - Runtime Resources", Order = 1000)]
	public sealed class VrsRenderPipelineRuntimeResources : global::UnityEngine.Rendering.IRenderPipelineResources, global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings
	{
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Compute shader used for converting textures to shading rate values")]
		[global::UnityEngine.Rendering.ResourcePath("Runtime/Vrs/Shaders/VrsTexture.compute", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.ComputeShader m_TextureComputeShader;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Shader used when visualizing shading rate values as a color image")]
		[global::UnityEngine.Rendering.ResourcePath("Runtime/Vrs/Shaders/VrsVisualization.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.Shader m_VisualizationShader;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Colors to visualize the shading rates")]
		private global::UnityEngine.Rendering.VrsLut m_VisualizationLookupTable = global::UnityEngine.Rendering.VrsLut.CreateDefault();

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Colors to convert between shading rates and textures")]
		private global::UnityEngine.Rendering.VrsLut m_ConversionLookupTable = global::UnityEngine.Rendering.VrsLut.CreateDefault();

		public int version => 0;

		bool global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings.isAvailableInPlayerBuild => true;

		public global::UnityEngine.ComputeShader textureComputeShader
		{
			get
			{
				return m_TextureComputeShader;
			}
			set
			{
				this.SetValueAndNotify(ref m_TextureComputeShader, value, "m_TextureComputeShader");
			}
		}

		public global::UnityEngine.Shader visualizationShader
		{
			get
			{
				return m_VisualizationShader;
			}
			set
			{
				this.SetValueAndNotify(ref m_VisualizationShader, value, "m_VisualizationShader");
			}
		}

		public global::UnityEngine.Rendering.VrsLut visualizationLookupTable
		{
			get
			{
				return m_VisualizationLookupTable;
			}
			set
			{
				this.SetValueAndNotify(ref m_VisualizationLookupTable, value, "m_VisualizationLookupTable");
			}
		}

		public global::UnityEngine.Rendering.VrsLut conversionLookupTable
		{
			get
			{
				return m_ConversionLookupTable;
			}
			set
			{
				this.SetValueAndNotify(ref m_ConversionLookupTable, value, "m_ConversionLookupTable");
			}
		}
	}
}
