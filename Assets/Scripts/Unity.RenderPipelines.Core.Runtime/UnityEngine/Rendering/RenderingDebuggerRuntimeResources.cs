namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::UnityEngine.HideInInspector]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(new global::System.Type[] { })]
	[global::UnityEngine.Categorization.CategoryInfo(Name = "R : Rendering Debugger Resources", Order = 100)]
	[global::UnityEngine.Categorization.ElementInfo(Order = 0)]
	internal class RenderingDebuggerRuntimeResources : global::UnityEngine.Rendering.IRenderPipelineResources, global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings
	{
		private enum Version
		{
			Initial = 0,
			Count = 1,
			Last = 0
		}

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.Rendering.RenderingDebuggerRuntimeResources.Version m_version;

		int global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings.version => (int)m_version;
	}
}
