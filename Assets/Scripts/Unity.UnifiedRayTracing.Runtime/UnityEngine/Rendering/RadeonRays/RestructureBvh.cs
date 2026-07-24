namespace UnityEngine.Rendering.RadeonRays
{
	internal sealed class RestructureBvh : global::System.IDisposable
	{
		private struct ScratchBufferLayout
		{
			public uint LeafParents;

			public uint TreeletCount;

			public uint TreeletRoots;

			public uint PrimitiveCounts;

			public uint TotalSize;

			public static global::UnityEngine.Rendering.RadeonRays.RestructureBvh.ScratchBufferLayout Create(uint triangleCount)
			{
				global::UnityEngine.Rendering.RadeonRays.RestructureBvh.ScratchBufferLayout result = default(global::UnityEngine.Rendering.RadeonRays.RestructureBvh.ScratchBufferLayout);
				result.LeafParents = result.Reserve(triangleCount);
				result.TreeletCount = result.Reserve(1u);
				result.TreeletRoots = result.Reserve(triangleCount);
				result.PrimitiveCounts = result.Reserve(GetBvhNodeCount(triangleCount));
				return result;
			}

			private uint Reserve(uint size)
			{
				uint totalSize = TotalSize;
				TotalSize += size;
				return totalSize;
			}
		}

		private readonly global::UnityEngine.ComputeShader shader;

		private readonly int kernelInitPrimitiveCounts;

		private readonly int kernelFindTreeletRoots;

		private readonly int kernelRestructure;

		private readonly int kernelPrepareTreeletsDispatchSize;

		private const int numIterations = 3;

		private readonly global::UnityEngine.GraphicsBuffer treeletDispatchIndirectBuffer;

		private const uint kGroupSize = 256u;

		private const uint kTrianglesPerThread = 8u;

		private const uint kTrianglesPerGroup = 2048u;

		private const uint kMinPrimitivesPerTreelet = 64u;

		private const int kMaxThreadGroupsPerDispatch = 65535;

		public RestructureBvh(global::UnityEngine.Rendering.RadeonRays.RadeonRaysShaders shaders)
		{
			shader = shaders.restructureBvh;
			kernelInitPrimitiveCounts = shader.FindKernel("InitPrimitiveCounts");
			kernelFindTreeletRoots = shader.FindKernel("FindTreeletRoots");
			kernelRestructure = shader.FindKernel("Restructure");
			kernelPrepareTreeletsDispatchSize = shader.FindKernel("PrepareTreeletsDispatchSize");
			treeletDispatchIndirectBuffer = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.IndirectArguments, 6, 4);
		}

		public void Dispose()
		{
			treeletDispatchIndirectBuffer.Dispose();
		}

		public ulong GetScratchDataSizeInDwords(uint triangleCount)
		{
			return global::UnityEngine.Rendering.RadeonRays.RestructureBvh.ScratchBufferLayout.Create(triangleCount).TotalSize;
		}

		public static uint GetBvhNodeCount(uint leafCount)
		{
			return leafCount - 1;
		}

		public void Execute(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.GraphicsBuffer vertices, int verticesOffset, uint vertexStride, uint triangleCount, global::UnityEngine.GraphicsBuffer scratch, in global::UnityEngine.Rendering.RadeonRays.BottomLevelLevelAccelStruct result)
		{
			global::UnityEngine.Rendering.RadeonRays.RestructureBvh.ScratchBufferLayout scratchBufferLayout = global::UnityEngine.Rendering.RadeonRays.RestructureBvh.ScratchBufferLayout.Create(triangleCount);
			cmd.SetComputeIntParam(shader, global::UnityEngine.Rendering.RadeonRays.SID.g_vertices_offset, verticesOffset);
			cmd.SetComputeIntParam(shader, global::UnityEngine.Rendering.RadeonRays.SID.g_constants_vertex_stride, (int)vertexStride);
			cmd.SetComputeIntParam(shader, global::UnityEngine.Rendering.RadeonRays.SID.g_constants_triangle_count, (int)triangleCount);
			cmd.SetComputeIntParam(shader, global::UnityEngine.Rendering.RadeonRays.SID.g_treelet_count_offset, (int)scratchBufferLayout.TreeletCount);
			cmd.SetComputeIntParam(shader, global::UnityEngine.Rendering.RadeonRays.SID.g_treelet_roots_offset, (int)scratchBufferLayout.TreeletRoots);
			cmd.SetComputeIntParam(shader, global::UnityEngine.Rendering.RadeonRays.SID.g_primitive_counts_offset, (int)scratchBufferLayout.PrimitiveCounts);
			cmd.SetComputeIntParam(shader, global::UnityEngine.Rendering.RadeonRays.SID.g_leaf_parents_offset, (int)scratchBufferLayout.LeafParents);
			cmd.SetComputeIntParam(shader, global::UnityEngine.Rendering.RadeonRays.SID.g_bvh_offset, (int)result.bvhOffset);
			cmd.SetComputeIntParam(shader, global::UnityEngine.Rendering.RadeonRays.SID.g_bvh_leaves_offset, (int)result.bvhLeavesOffset);
			uint num = 64u;
			for (int i = 0; i < 3; i++)
			{
				cmd.SetComputeIntParam(shader, global::UnityEngine.Rendering.RadeonRays.SID.g_constants_min_prims_per_treelet, (int)num);
				BindKernelArguments(cmd, kernelInitPrimitiveCounts, vertices, scratch, result);
				cmd.DispatchCompute(shader, kernelInitPrimitiveCounts, (int)global::UnityEngine.Rendering.RadeonRays.Common.CeilDivide(2048u, 256u), 1, 1);
				BindKernelArguments(cmd, kernelFindTreeletRoots, vertices, scratch, result);
				cmd.DispatchCompute(shader, kernelFindTreeletRoots, (int)global::UnityEngine.Rendering.RadeonRays.Common.CeilDivide(2048u, 256u), 1, 1);
				BindKernelArguments(cmd, kernelPrepareTreeletsDispatchSize, vertices, scratch, result);
				cmd.DispatchCompute(shader, kernelPrepareTreeletsDispatchSize, 1, 1, 1);
				BindKernelArguments(cmd, kernelRestructure, vertices, scratch, result);
				cmd.SetComputeIntParam(shader, global::UnityEngine.Rendering.RadeonRays.SID.g_remainder_treelets, 0);
				cmd.DispatchCompute(shader, kernelRestructure, treeletDispatchIndirectBuffer, 0u);
				if (global::UnityEngine.Rendering.RadeonRays.Common.CeilDivide(triangleCount, num) > 65535)
				{
					cmd.SetComputeIntParam(shader, global::UnityEngine.Rendering.RadeonRays.SID.g_remainder_treelets, 1);
					cmd.DispatchCompute(shader, kernelRestructure, treeletDispatchIndirectBuffer, 12u);
				}
				num *= 2;
			}
		}

		private void BindKernelArguments(global::UnityEngine.Rendering.CommandBuffer cmd, int kernel, global::UnityEngine.GraphicsBuffer vertices, global::UnityEngine.GraphicsBuffer scratch, global::UnityEngine.Rendering.RadeonRays.BottomLevelLevelAccelStruct result)
		{
			cmd.SetComputeBufferParam(shader, kernel, global::UnityEngine.Rendering.RadeonRays.SID.g_vertices, vertices);
			cmd.SetComputeBufferParam(shader, kernel, global::UnityEngine.Rendering.RadeonRays.SID.g_scratch_buffer, scratch);
			cmd.SetComputeBufferParam(shader, kernel, global::UnityEngine.Rendering.RadeonRays.SID.g_bvh, result.bvh);
			cmd.SetComputeBufferParam(shader, kernel, global::UnityEngine.Rendering.RadeonRays.SID.g_bvh_leaves, result.bvhLeaves);
			cmd.SetComputeBufferParam(shader, kernel, global::UnityEngine.Rendering.RadeonRays.SID.g_treelet_dispatch_buffer, treeletDispatchIndirectBuffer);
		}
	}
}
