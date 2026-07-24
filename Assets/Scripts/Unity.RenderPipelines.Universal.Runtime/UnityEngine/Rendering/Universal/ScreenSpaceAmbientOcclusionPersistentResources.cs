namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(typeof(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset))]
	[global::UnityEngine.Categorization.CategoryInfo(Name = "R: SSAO Shader", Order = 1000)]
	[global::UnityEngine.Categorization.ElementInfo(Order = 0)]
	[global::UnityEngine.HideInInspector]
	internal class ScreenSpaceAmbientOcclusionPersistentResources : global::UnityEngine.Rendering.IRenderPipelineResources, global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings
	{
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Shaders/Utils/ScreenSpaceAmbientOcclusion.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.Shader m_Shader;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private int m_Version;

		public global::UnityEngine.Shader Shader
		{
			get
			{
				return m_Shader;
			}
			set
			{
				this.SetValueAndNotify(ref m_Shader, value, "Shader");
			}
		}

		public bool isAvailableInPlayerBuild => true;

		public int version => m_Version;
	}
}
