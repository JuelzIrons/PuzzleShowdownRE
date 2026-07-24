namespace UnityEngine.Rendering.UnifiedRayTracing
{
	[global::System.Serializable]
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom(true, "UnityEngine.Rendering.UnifiedRayTracing", "Unity.Rendering.LightTransport.Runtime", null)]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(new global::System.Type[] { })]
	[global::UnityEngine.Categorization.CategoryInfo(Name = "R: Unified Ray Tracing", Order = 1000)]
	[global::UnityEngine.HideInInspector]
	internal class RayTracingRenderPipelineResources : global::UnityEngine.Rendering.IRenderPipelineResources, global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings
	{
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private int m_Version = 1;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Runtime/UnifiedRayTracing/Common/GeometryPool/GeometryPoolKernels.compute", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.ComputeShader m_GeometryPoolKernels;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Runtime/UnifiedRayTracing/Common/Utilities/CopyBuffer.compute", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.ComputeShader m_CopyBuffer;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Runtime/UnifiedRayTracing/Compute/RadeonRays/kernels/copyPositions.compute", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.ComputeShader m_CopyPositions;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Runtime/UnifiedRayTracing/Compute/RadeonRays/kernels/bit_histogram.compute", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.ComputeShader m_BitHistogram;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Runtime/UnifiedRayTracing/Compute/RadeonRays/kernels/block_reduce_part.compute", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.ComputeShader m_BlockReducePart;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Runtime/UnifiedRayTracing/Compute/RadeonRays/kernels/block_scan.compute", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.ComputeShader m_BlockScan;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Runtime/UnifiedRayTracing/Compute/RadeonRays/kernels/build_hlbvh.compute", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.ComputeShader m_BuildHlbvh;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Runtime/UnifiedRayTracing/Compute/RadeonRays/kernels/restructure_bvh.compute", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.ComputeShader m_RestructureBvh;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Runtime/UnifiedRayTracing/Compute/RadeonRays/kernels/scatter.compute", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.ComputeShader m_Scatter;

		public int version => m_Version;

		public global::UnityEngine.ComputeShader GeometryPoolKernels
		{
			get
			{
				return m_GeometryPoolKernels;
			}
			set
			{
				this.SetValueAndNotify(ref m_GeometryPoolKernels, value, "m_GeometryPoolKernels");
			}
		}

		public global::UnityEngine.ComputeShader CopyBuffer
		{
			get
			{
				return m_CopyBuffer;
			}
			set
			{
				this.SetValueAndNotify(ref m_CopyBuffer, value, "m_CopyBuffer");
			}
		}

		public global::UnityEngine.ComputeShader CopyPositions
		{
			get
			{
				return m_CopyPositions;
			}
			set
			{
				this.SetValueAndNotify(ref m_CopyPositions, value, "m_CopyPositions");
			}
		}

		public global::UnityEngine.ComputeShader BitHistogram
		{
			get
			{
				return m_BitHistogram;
			}
			set
			{
				this.SetValueAndNotify(ref m_BitHistogram, value, "m_BitHistogram");
			}
		}

		public global::UnityEngine.ComputeShader BlockReducePart
		{
			get
			{
				return m_BlockReducePart;
			}
			set
			{
				this.SetValueAndNotify(ref m_BlockReducePart, value, "m_BlockReducePart");
			}
		}

		public global::UnityEngine.ComputeShader BlockScan
		{
			get
			{
				return m_BlockScan;
			}
			set
			{
				this.SetValueAndNotify(ref m_BlockScan, value, "m_BlockScan");
			}
		}

		public global::UnityEngine.ComputeShader BuildHlbvh
		{
			get
			{
				return m_BuildHlbvh;
			}
			set
			{
				this.SetValueAndNotify(ref m_BuildHlbvh, value, "m_BuildHlbvh");
			}
		}

		public global::UnityEngine.ComputeShader RestructureBvh
		{
			get
			{
				return m_RestructureBvh;
			}
			set
			{
				this.SetValueAndNotify(ref m_RestructureBvh, value, "m_RestructureBvh");
			}
		}

		public global::UnityEngine.ComputeShader Scatter
		{
			get
			{
				return m_Scatter;
			}
			set
			{
				this.SetValueAndNotify(ref m_Scatter, value, "m_Scatter");
			}
		}
	}
}
