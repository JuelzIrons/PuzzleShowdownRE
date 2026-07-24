namespace UnityEngine.Rendering.RenderGraphModule.Util
{
	[global::System.Serializable]
	[global::UnityEngine.HideInInspector]
	[global::System.ComponentModel.Category("Resources/Render Graph Helper Function Resources")]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(new global::System.Type[] { })]
	internal class RenderGraphUtilsResources : global::UnityEngine.Rendering.IRenderPipelineResources, global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings
	{
		public enum Version
		{
			Initial = 0,
			Count = 1,
			Latest = 0
		}

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtilsResources.Version m_Version;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Shaders/CoreCopy.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		internal global::UnityEngine.Shader m_CoreCopyPS;

		int global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings.version => (int)m_Version;

		public global::UnityEngine.Shader coreCopyPS
		{
			get
			{
				return m_CoreCopyPS;
			}
			set
			{
				this.SetValueAndNotify(ref m_CoreCopyPS, value, "m_CoreCopyPS");
			}
		}
	}
}
