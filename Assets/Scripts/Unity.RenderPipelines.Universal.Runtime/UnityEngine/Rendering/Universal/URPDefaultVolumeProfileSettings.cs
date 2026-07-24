namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(typeof(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset))]
	[global::UnityEngine.Categorization.CategoryInfo(Name = "Volume", Order = 0)]
	public class URPDefaultVolumeProfileSettings : global::UnityEngine.Rendering.IDefaultVolumeProfileSettings, global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings
	{
		internal enum Version
		{
			Initial = 0
		}

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.Rendering.Universal.URPDefaultVolumeProfileSettings.Version m_Version;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.VolumeProfile m_VolumeProfile;

		public int version => (int)m_Version;

		public global::UnityEngine.Rendering.VolumeProfile volumeProfile
		{
			get
			{
				return m_VolumeProfile;
			}
			set
			{
				this.SetValueAndNotify(ref m_VolumeProfile, value, "volumeProfile");
			}
		}
	}
}
