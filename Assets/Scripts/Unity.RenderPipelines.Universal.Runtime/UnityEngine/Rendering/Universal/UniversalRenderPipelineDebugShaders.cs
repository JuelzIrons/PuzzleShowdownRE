namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(typeof(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset))]
	[global::UnityEngine.Categorization.CategoryInfo(Name = "R: Debug Shaders", Order = 1000)]
	[global::UnityEngine.HideInInspector]
	public class UniversalRenderPipelineDebugShaders : global::UnityEngine.Rendering.IRenderPipelineResources, global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings
	{
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Shaders/Debug/DebugReplacement.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.Shader m_DebugReplacementPS;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Shaders/Debug/HDRDebugView.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.Shader m_HdrDebugViewPS;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Shaders/Debug/ProbeVolumeSamplingDebugPositionNormal.compute", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.ComputeShader m_ProbeVolumeSamplingDebugComputeShader;

		public int version => 0;

		bool global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings.isAvailableInPlayerBuild => false;

		public global::UnityEngine.Shader debugReplacementPS
		{
			get
			{
				return m_DebugReplacementPS;
			}
			set
			{
				this.SetValueAndNotify(ref m_DebugReplacementPS, value, "m_DebugReplacementPS");
			}
		}

		public global::UnityEngine.Shader hdrDebugViewPS
		{
			get
			{
				return m_HdrDebugViewPS;
			}
			set
			{
				this.SetValueAndNotify(ref m_HdrDebugViewPS, value, "m_HdrDebugViewPS");
			}
		}

		public global::UnityEngine.ComputeShader probeVolumeSamplingDebugComputeShader
		{
			get
			{
				return m_ProbeVolumeSamplingDebugComputeShader;
			}
			set
			{
				this.SetValueAndNotify(ref m_ProbeVolumeSamplingDebugComputeShader, value, "m_ProbeVolumeSamplingDebugComputeShader");
			}
		}
	}
}
