namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(new global::System.Type[] { })]
	[global::UnityEngine.Categorization.CategoryInfo(Name = "R: Adaptive Probe Volumes", Order = 1000)]
	[global::UnityEngine.HideInInspector]
	internal class ProbeVolumeBakingResources : global::UnityEngine.Rendering.IRenderPipelineResources, global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings
	{
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private int m_Version = 1;

		[global::UnityEngine.Header("Baking")]
		[global::UnityEngine.Rendering.ResourcePath("Editor/Lighting/ProbeVolume/ProbeVolumeCellDilation.compute", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		public global::UnityEngine.ComputeShader dilationShader;

		[global::UnityEngine.Rendering.ResourcePath("Editor/Lighting/ProbeVolume/ProbeVolumeSubdivide.compute", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		public global::UnityEngine.ComputeShader subdivideSceneCS;

		[global::UnityEngine.Rendering.ResourcePath("Editor/Lighting/ProbeVolume/VoxelizeScene.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		public global::UnityEngine.Shader voxelizeSceneShader;

		[global::UnityEngine.Rendering.ResourcePath("Editor/Lighting/ProbeVolume/VirtualOffset/TraceVirtualOffset.urtshader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		public global::UnityEngine.ComputeShader traceVirtualOffsetCS;

		[global::UnityEngine.Rendering.ResourcePath("Editor/Lighting/ProbeVolume/VirtualOffset/TraceVirtualOffset.urtshader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		public global::UnityEngine.Rendering.RayTracingShader traceVirtualOffsetRT;

		[global::UnityEngine.Rendering.ResourcePath("Editor/Lighting/ProbeVolume/DynamicGI/DynamicGISkyOcclusion.urtshader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		public global::UnityEngine.ComputeShader skyOcclusionCS;

		[global::UnityEngine.Rendering.ResourcePath("Editor/Lighting/ProbeVolume/DynamicGI/DynamicGISkyOcclusion.urtshader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		public global::UnityEngine.Rendering.RayTracingShader skyOcclusionRT;

		[global::UnityEngine.Rendering.ResourcePath("Editor/Lighting/ProbeVolume/RenderingLayerMask/TraceRenderingLayerMask.urtshader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		public global::UnityEngine.ComputeShader renderingLayerCS;

		[global::UnityEngine.Rendering.ResourcePath("Editor/Lighting/ProbeVolume/RenderingLayerMask/TraceRenderingLayerMask.urtshader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		public global::UnityEngine.Rendering.RayTracingShader renderingLayerRT;

		public int version => m_Version;
	}
}
