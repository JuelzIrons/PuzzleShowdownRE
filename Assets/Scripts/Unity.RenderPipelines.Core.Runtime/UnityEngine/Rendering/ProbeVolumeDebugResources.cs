namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(new global::System.Type[] { })]
	[global::UnityEngine.Categorization.CategoryInfo(Name = "R: Adaptive Probe Volumes", Order = 1000)]
	[global::UnityEngine.HideInInspector]
	internal class ProbeVolumeDebugResources : global::UnityEngine.Rendering.IRenderPipelineResources, global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings
	{
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private int m_Version = 1;

		[global::UnityEngine.Header("Debug")]
		[global::UnityEngine.Rendering.ResourcePath("Runtime/Debug/ProbeVolumeDebug.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		public global::UnityEngine.Shader probeVolumeDebugShader;

		[global::UnityEngine.Rendering.ResourcePath("Runtime/Debug/ProbeVolumeFragmentationDebug.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		public global::UnityEngine.Shader probeVolumeFragmentationDebugShader;

		[global::UnityEngine.Rendering.ResourcePath("Runtime/Debug/ProbeVolumeSamplingDebug.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		public global::UnityEngine.Shader probeVolumeSamplingDebugShader;

		[global::UnityEngine.Rendering.ResourcePath("Runtime/Debug/ProbeVolumeOffsetDebug.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		public global::UnityEngine.Shader probeVolumeOffsetDebugShader;

		[global::UnityEngine.Rendering.ResourcePath("Runtime/Debug/ProbeSamplingDebugMesh.fbx", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		public global::UnityEngine.Mesh probeSamplingDebugMesh;

		[global::UnityEngine.Rendering.ResourcePath("Runtime/Debug/ProbeVolumeNumbersDisplayTex.png", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		public global::UnityEngine.Texture2D numbersDisplayTex;

		public int version => m_Version;
	}
}
