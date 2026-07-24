namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(typeof(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset))]
	[global::UnityEngine.Categorization.CategoryInfo(Name = "R: SSAO Noise Textures", Order = 1000)]
	[global::UnityEngine.Categorization.ElementInfo(Order = 0)]
	[global::UnityEngine.HideInInspector]
	internal class ScreenSpaceAmbientOcclusionDynamicResources : global::UnityEngine.Rendering.IRenderPipelineResources, global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings
	{
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourceFormattedPaths("Textures/BlueNoise256/LDR_LLL1_{0}.png", 0, 7, global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.Texture2D[] m_BlueNoise256Textures;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private int m_Version;

		public global::UnityEngine.Texture2D[] BlueNoise256Textures
		{
			get
			{
				return m_BlueNoise256Textures;
			}
			set
			{
				this.SetValueAndNotify(ref m_BlueNoise256Textures, value, "BlueNoise256Textures");
			}
		}

		public bool isAvailableInPlayerBuild => true;

		public int version => m_Version;
	}
}
