namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(new global::System.Type[] { })]
	[global::UnityEngine.Categorization.CategoryInfo(Name = "R: Adaptive Probe Volumes", Order = 1000)]
	[global::UnityEngine.HideInInspector]
	internal class ProbeVolumeRuntimeResources : global::UnityEngine.Rendering.IRenderPipelineResources, global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings
	{
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private int m_Version = 1;

		[global::UnityEngine.Header("Runtime")]
		[global::UnityEngine.Rendering.ResourcePath("Runtime/Lighting/ProbeVolume/ProbeVolumeBlendStates.compute", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		public global::UnityEngine.ComputeShader probeVolumeBlendStatesCS;

		[global::UnityEngine.Rendering.ResourcePath("Runtime/Lighting/ProbeVolume/ProbeVolumeUploadData.compute", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		public global::UnityEngine.ComputeShader probeVolumeUploadDataCS;

		[global::UnityEngine.Rendering.ResourcePath("Runtime/Lighting/ProbeVolume/ProbeVolumeUploadDataL2.compute", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		public global::UnityEngine.ComputeShader probeVolumeUploadDataL2CS;

		public int version => m_Version;
	}
}
