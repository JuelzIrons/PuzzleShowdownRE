namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(typeof(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset))]
	[global::UnityEngine.Categorization.CategoryInfo(Name = "R: Universal Renderer Shaders", Order = 1000)]
	[global::UnityEngine.HideInInspector]
	public class UniversalRendererResources : global::UnityEngine.Rendering.IRenderPipelineResources, global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings
	{
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private int m_Version;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Shaders/Utils/CopyDepth.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.Shader m_CopyDepthPS;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Shaders/CameraMotionVectors.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.Shader m_CameraMotionVector;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Shaders/Utils/StencilDeferred.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.Shader m_StencilDeferredPS;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Shaders/Utils/ClusterDeferred.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.Shader m_ClusterDeferred;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Shaders/Utils/StencilDitherMaskSeed.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.Shader m_StencilDitherMaskSeedPS;

		[global::UnityEngine.Header("Decal Renderer Feature Specific")]
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Runtime/Decal/DBuffer/DBufferClear.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.Shader m_DBufferClear;

		public int version => m_Version;

		bool global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings.isAvailableInPlayerBuild => true;

		public global::UnityEngine.Shader copyDepthPS
		{
			get
			{
				return m_CopyDepthPS;
			}
			set
			{
				this.SetValueAndNotify(ref m_CopyDepthPS, value, "m_CopyDepthPS");
			}
		}

		public global::UnityEngine.Shader cameraMotionVector
		{
			get
			{
				return m_CameraMotionVector;
			}
			set
			{
				this.SetValueAndNotify(ref m_CameraMotionVector, value, "m_CameraMotionVector");
			}
		}

		public global::UnityEngine.Shader stencilDeferredPS
		{
			get
			{
				return m_StencilDeferredPS;
			}
			set
			{
				this.SetValueAndNotify(ref m_StencilDeferredPS, value, "m_StencilDeferredPS");
			}
		}

		public global::UnityEngine.Shader clusterDeferred
		{
			get
			{
				return m_ClusterDeferred;
			}
			set
			{
				this.SetValueAndNotify(ref m_ClusterDeferred, value, "m_ClusterDeferred");
			}
		}

		public global::UnityEngine.Shader stencilDitherMaskSeedPS
		{
			get
			{
				return m_StencilDitherMaskSeedPS;
			}
			set
			{
				this.SetValueAndNotify(ref m_StencilDitherMaskSeedPS, value, "m_StencilDitherMaskSeedPS");
			}
		}

		public global::UnityEngine.Shader decalDBufferClear
		{
			get
			{
				return m_DBufferClear;
			}
			set
			{
				this.SetValueAndNotify(ref m_DBufferClear, value, "m_DBufferClear");
			}
		}
	}
}
