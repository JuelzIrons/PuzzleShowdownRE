namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(new global::System.Type[] { })]
	[global::UnityEngine.Categorization.CategoryInfo(Name = "Lighting", Order = 20)]
	internal class ProbeVolumeGlobalSettings : global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings
	{
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private int m_Version = 1;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Enabling this will make APV baked data assets compatible with Addressables and Asset Bundles. This will also make Disk Streaming unavailable. After changing this setting, a clean rebuild may be required for data assets to be included in Adressables and Asset Bundles.")]
		private bool m_ProbeVolumeDisableStreamingAssets;

		public int version => m_Version;

		public bool probeVolumeDisableStreamingAssets
		{
			get
			{
				return m_ProbeVolumeDisableStreamingAssets;
			}
			set
			{
				this.SetValueAndNotify(ref m_ProbeVolumeDisableStreamingAssets, value, "m_ProbeVolumeDisableStreamingAssets");
			}
		}
	}
}
