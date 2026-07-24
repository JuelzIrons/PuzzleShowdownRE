namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(typeof(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset))]
	[global::UnityEngine.Categorization.CategoryInfo(Name = "Render Graph", Order = 50)]
	[global::UnityEngine.Categorization.ElementInfo(Order = -10)]
	public class RenderGraphSettings : global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings
	{
		internal enum Version
		{
			Initial = 0
		}

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.Rendering.Universal.RenderGraphSettings.Version m_Version;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("When enabled, URP does not use the Render Graph API to construct and execute the frame. Use this option only for compatibility purposes.")]
		[global::UnityEngine.Rendering.RecreatePipelineOnChange]
		private bool m_EnableRenderCompatibilityMode;

		public int version => (int)m_Version;

		bool global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings.isAvailableInPlayerBuild => true;

		public bool enableRenderCompatibilityMode
		{
			get
			{
				return false;
			}
			[global::System.Obsolete("Compatibility Mode is being removed. This setter is not accessible without the define URP_COMPATIBILITY_MODE. #from(6000.3) #breakingFrom(6000.3)", true)]
			set
			{
			}
		}

		internal void SetCompatibilityModeFromUpgrade(bool value)
		{
			m_EnableRenderCompatibilityMode = value;
		}
	}
}
