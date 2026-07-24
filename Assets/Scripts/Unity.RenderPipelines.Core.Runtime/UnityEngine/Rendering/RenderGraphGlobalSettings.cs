namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(new global::System.Type[] { })]
	[global::UnityEngine.Categorization.CategoryInfo(Name = "Render Graph", Order = 50)]
	[global::UnityEngine.Categorization.ElementInfo(Order = 0)]
	public class RenderGraphGlobalSettings : global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings
	{
		private enum Version
		{
			Initial = 0,
			Count = 1,
			Last = 0
		}

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.Rendering.RenderGraphGlobalSettings.Version m_version;

		[global::UnityEngine.Rendering.RecreatePipelineOnChange]
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Enable caching of render graph compilation from one frame to another.")]
		private bool m_EnableCompilationCaching = true;

		[global::UnityEngine.Rendering.RecreatePipelineOnChange]
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Enable validity checks of render graph in Editor and Development mode. Always disabled in Release build.")]
		private bool m_EnableValidityChecks = true;

		bool global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings.isAvailableInPlayerBuild => true;

		int global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings.version => (int)m_version;

		public bool enableCompilationCaching
		{
			get
			{
				return m_EnableCompilationCaching;
			}
			set
			{
				this.SetValueAndNotify(ref m_EnableCompilationCaching, value, "enableCompilationCaching");
			}
		}

		public bool enableValidityChecks
		{
			get
			{
				return m_EnableValidityChecks;
			}
			set
			{
				this.SetValueAndNotify(ref m_EnableValidityChecks, value, "enableValidityChecks");
			}
		}
	}
}
