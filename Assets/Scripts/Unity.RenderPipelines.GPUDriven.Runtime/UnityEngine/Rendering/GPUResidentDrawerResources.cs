namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(new global::System.Type[] { })]
	[global::UnityEngine.Categorization.CategoryInfo(Name = "R: GPU Resident Drawers", Order = 1000)]
	[global::UnityEngine.HideInInspector]
	internal class GPUResidentDrawerResources : global::UnityEngine.Rendering.IRenderPipelineResources, global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings
	{
		public enum Version
		{
			Initial = 0,
			Count = 1,
			Latest = 0
		}

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.Rendering.GPUResidentDrawerResources.Version m_Version;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Runtime/RenderPipelineResources/GPUDriven/InstanceDataBufferCopyKernels.compute", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.ComputeShader m_InstanceDataBufferCopyKernels;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Runtime/RenderPipelineResources/GPUDriven/InstanceDataBufferUploadKernels.compute", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.ComputeShader m_InstanceDataBufferUploadKernels;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Runtime/RenderPipelineResources/GPUDriven/InstanceTransformUpdateKernels.compute", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.ComputeShader m_TransformUpdaterKernels;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Runtime/RenderPipelineResources/GPUDriven/InstanceWindDataUpdateKernels.compute", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		public global::UnityEngine.ComputeShader m_WindDataUpdaterKernels;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Runtime/RenderPipelineResources/GPUDriven/OccluderDepthPyramidKernels.compute", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.ComputeShader m_OccluderDepthPyramidKernels;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Runtime/RenderPipelineResources/GPUDriven/InstanceOcclusionCullingKernels.compute", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.ComputeShader m_InstanceOcclusionCullingKernels;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Runtime/RenderPipelineResources/GPUDriven/OcclusionCullingDebug.compute", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.ComputeShader m_OcclusionCullingDebugKernels;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Runtime/RenderPipelineResources/GPUDriven/DebugOcclusionTest.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.Shader m_DebugOcclusionTestPS;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Runtime/RenderPipelineResources/GPUDriven/DebugOccluder.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.Shader m_DebugOccluderPS;

		int global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings.version => (int)m_Version;

		public global::UnityEngine.ComputeShader instanceDataBufferCopyKernels
		{
			get
			{
				return m_InstanceDataBufferCopyKernels;
			}
			set
			{
				this.SetValueAndNotify(ref m_InstanceDataBufferCopyKernels, value, "m_InstanceDataBufferCopyKernels");
			}
		}

		public global::UnityEngine.ComputeShader instanceDataBufferUploadKernels
		{
			get
			{
				return m_InstanceDataBufferUploadKernels;
			}
			set
			{
				this.SetValueAndNotify(ref m_InstanceDataBufferUploadKernels, value, "m_InstanceDataBufferUploadKernels");
			}
		}

		public global::UnityEngine.ComputeShader transformUpdaterKernels
		{
			get
			{
				return m_TransformUpdaterKernels;
			}
			set
			{
				this.SetValueAndNotify(ref m_TransformUpdaterKernels, value, "m_TransformUpdaterKernels");
			}
		}

		public global::UnityEngine.ComputeShader windDataUpdaterKernels
		{
			get
			{
				return m_WindDataUpdaterKernels;
			}
			set
			{
				this.SetValueAndNotify(ref m_WindDataUpdaterKernels, value, "m_WindDataUpdaterKernels");
			}
		}

		public global::UnityEngine.ComputeShader occluderDepthPyramidKernels
		{
			get
			{
				return m_OccluderDepthPyramidKernels;
			}
			set
			{
				this.SetValueAndNotify(ref m_OccluderDepthPyramidKernels, value, "m_OccluderDepthPyramidKernels");
			}
		}

		public global::UnityEngine.ComputeShader instanceOcclusionCullingKernels
		{
			get
			{
				return m_InstanceOcclusionCullingKernels;
			}
			set
			{
				this.SetValueAndNotify(ref m_InstanceOcclusionCullingKernels, value, "m_InstanceOcclusionCullingKernels");
			}
		}

		public global::UnityEngine.ComputeShader occlusionCullingDebugKernels
		{
			get
			{
				return m_OcclusionCullingDebugKernels;
			}
			set
			{
				this.SetValueAndNotify(ref m_OcclusionCullingDebugKernels, value, "m_OcclusionCullingDebugKernels");
			}
		}

		public global::UnityEngine.Shader debugOcclusionTestPS
		{
			get
			{
				return m_DebugOcclusionTestPS;
			}
			set
			{
				this.SetValueAndNotify(ref m_DebugOcclusionTestPS, value, "m_DebugOcclusionTestPS");
			}
		}

		public global::UnityEngine.Shader debugOccluderPS
		{
			get
			{
				return m_DebugOccluderPS;
			}
			set
			{
				this.SetValueAndNotify(ref m_DebugOccluderPS, value, "m_DebugOccluderPS");
			}
		}
	}
}
