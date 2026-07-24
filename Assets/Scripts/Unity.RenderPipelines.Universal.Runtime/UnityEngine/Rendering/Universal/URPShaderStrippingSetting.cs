namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(typeof(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset))]
	[global::UnityEngine.Categorization.CategoryInfo(Name = "Additional Shader Stripping Settings", Order = 40)]
	[global::UnityEngine.Categorization.ElementInfo(Order = 10)]
	public class URPShaderStrippingSetting : global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings
	{
		internal enum Version
		{
			Initial = 0
		}

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.Rendering.Universal.URPShaderStrippingSetting.Version m_Version;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Controls whether to automatically strip post processing shader variants based on VolumeProfile components. Stripping is done based on VolumeProfiles in project, their usage in scenes is not considered.")]
		private bool m_StripUnusedPostProcessingVariants;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Controls whether to strip variants if the feature is disabled.")]
		private bool m_StripUnusedVariants = true;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Controls whether Screen Coordinates Override shader variants are automatically stripped.")]
		private bool m_StripScreenCoordOverrideVariants = true;

		public int version => (int)m_Version;

		public bool stripUnusedPostProcessingVariants
		{
			get
			{
				return m_StripUnusedPostProcessingVariants;
			}
			set
			{
				this.SetValueAndNotify(ref m_StripUnusedPostProcessingVariants, value, "stripUnusedPostProcessingVariants");
			}
		}

		public bool stripUnusedVariants
		{
			get
			{
				return m_StripUnusedVariants;
			}
			set
			{
				this.SetValueAndNotify(ref m_StripUnusedVariants, value, "stripUnusedVariants");
			}
		}

		public bool stripScreenCoordOverrideVariants
		{
			get
			{
				return m_StripScreenCoordOverrideVariants;
			}
			set
			{
				this.SetValueAndNotify(ref m_StripScreenCoordOverrideVariants, value, "stripScreenCoordOverrideVariants");
			}
		}
	}
}
