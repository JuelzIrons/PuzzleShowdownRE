namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(new global::System.Type[] { })]
	[global::UnityEngine.Categorization.CategoryInfo(Name = "Lighting", Order = 20)]
	public class LightmapSamplingSettings : global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings
	{
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private int m_Version = 1;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Use Bicubic Lightmap Sampling. Enabling this will improve the appearance of lightmaps, but may worsen performance on lower end platforms.")]
		private bool m_UseBicubicLightmapSampling;

		int global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings.version => m_Version;

		bool global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings.isAvailableInPlayerBuild => true;

		public bool useBicubicLightmapSampling
		{
			get
			{
				return m_UseBicubicLightmapSampling;
			}
			set
			{
				this.SetValueAndNotify(ref m_UseBicubicLightmapSampling, value, "m_UseBicubicLightmapSampling");
			}
		}
	}
}
