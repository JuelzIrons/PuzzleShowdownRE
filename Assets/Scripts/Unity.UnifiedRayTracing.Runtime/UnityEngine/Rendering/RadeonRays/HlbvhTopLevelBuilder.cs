namespace UnityEngine.Rendering.RadeonRays
{
	internal class HlbvhTopLevelBuilder
	{
		private struct ScratchBufferLayout
		{
			public uint Aabb;

			public uint MortonCodes;

			public uint PrimitiveRefs;

			public uint SortedMortonCodes;

			public uint SortedPrimitiveRefs;

			public uint SortMemory;

			public uint InternalNodeRange;

			public uint TotalSize;

			public static global::UnityEngine.Rendering.RadeonRays.HlbvhTopLevelBuilder.ScratchBufferLayout Create(uint instanceCount)
			{
				global::UnityEngine.Rendering.RadeonRays.HlbvhTopLevelBuilder.ScratchBufferLayout result = default(global::UnityEngine.Rendering.RadeonRays.HlbvhTopLevelBuilder.ScratchBufferLayout);
				result.Aabb = result.Reserve(6u);
				result.MortonCodes = result.Reserve(instanceCount);
				result.PrimitiveRefs = result.Reserve(instanceCount);
				result.SortedMortonCodes = result.Reserve(instanceCount);
				result.SortedPrimitiveRefs = result.Reserve(instanceCount);
				result.SortMemory = result.Reserve((uint)global::UnityEngine.Rendering.RadeonRays.RadixSort.GetScratchDataSizeInDwords(instanceCount));
				result.InternalNodeRange = result.MortonCodes;
				return result;
			}

			private uint Reserve(uint size)
			{
				uint totalSize = TotalSize;
				TotalSize += size;
				return totalSize;
			}
		}

		private readonly global::UnityEngine.ComputeShader shaderBuildHlbvh;

		private readonly int kernelInit;

		private readonly int kernelCalculateAabb;

		private readonly int kernelCalculateMortonCodes;

		private readonly int kernelBuildTreeBottomUp;

		private readonly global::UnityEngine.Rendering.RadeonRays.RadixSort radixSort;

		private const uint kTrianglesPerThread = 8u;

		private const uint kGroupSize = 256u;

		private const uint kTrianglesPerGroup = 2048u;

		public HlbvhTopLevelBuilder(global::UnityEngine.Rendering.RadeonRays.RadeonRaysShaders shaders)
		{
			shaderBuildHlbvh = shaders.buildHlbvh;
			kernelInit = shaderBuildHlbvh.FindKernel("Init");
			kernelCalculateAabb = shaderBuildHlbvh.FindKernel("CalculateAabb");
			kernelCalculateMortonCodes = shaderBuildHlbvh.FindKernel("CalculateMortonCodes");
			kernelBuildTreeBottomUp = shaderBuildHlbvh.FindKernel("BuildTreeBottomUp");
			radixSort = new global::UnityEngine.Rendering.RadeonRays.RadixSort(shaders);
		}

		public ulong GetScratchDataSizeInDwords(uint instanceCount)
		{
			return global::UnityEngine.Rendering.RadeonRays.HlbvhTopLevelBuilder.ScratchBufferLayout.Create(instanceCount).TotalSize;
		}

		public static uint GetBvhNodeCount(uint leafCount)
		{
			return leafCount - 1;
		}

		public void AllocateResultBuffers(uint instanceCount, ref global::UnityEngine.Rendering.RadeonRays.TopLevelAccelStruct accelStruct)
		{
			uint bvhNodeCount = GetBvhNodeCount(instanceCount);
			accelStruct.Dispose();
			accelStruct.instanceInfos = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Structured, (int)instanceCount, global::System.Runtime.InteropServices.Marshal.SizeOf<global::UnityEngine.Rendering.RadeonRays.InstanceInfo>());
			accelStruct.topLevelBvh = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Structured, (int)(bvhNodeCount + 1), global::System.Runtime.InteropServices.Marshal.SizeOf<global::UnityEngine.Rendering.RadeonRays.BvhNode>());
		}

		public void CreateEmpty(ref global::UnityEngine.Rendering.RadeonRays.TopLevelAccelStruct accelStruct)
		{
			accelStruct.Dispose();
			accelStruct.topLevelBvh = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Structured, 2, global::System.Runtime.InteropServices.Marshal.SizeOf<global::UnityEngine.Rendering.RadeonRays.BvhNode>());
			accelStruct.instanceInfos = accelStruct.topLevelBvh;
			accelStruct.bottomLevelBvhs = accelStruct.topLevelBvh;
			accelStruct.instanceCount = 0u;
			global::UnityEngine.Rendering.RadeonRays.BvhNode[] array = new global::UnityEngine.Rendering.RadeonRays.BvhNode[2];
			array[0].child0 = 0u;
			array[0].child1 = 0u;
			array[0].parent = uint.MaxValue;
			array[1].child0 = 0u;
			array[1].child1 = 0u;
			array[1].parent = uint.MaxValue;
			array[1].update = 0u;
			array[1].aabb0_min = new global::Unity.Mathematics.float3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
			array[1].aabb0_max = new global::Unity.Mathematics.float3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
			array[1].aabb1_min = new global::Unity.Mathematics.float3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
			array[1].aabb1_max = new global::Unity.Mathematics.float3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
			accelStruct.topLevelBvh.SetData(array);
		}

		public void Execute(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.GraphicsBuffer scratch, ref global::UnityEngine.Rendering.RadeonRays.TopLevelAccelStruct accelStruct)
		{
			global::UnityEngine.Rendering.RadeonRays.Common.EnableKeyword(cmd, shaderBuildHlbvh, "TOP_LEVEL", enable: true);
			global::UnityEngine.Rendering.RadeonRays.Common.EnableKeyword(cmd, shaderBuildHlbvh, "UINT16_INDICES", enable: false);
			uint instanceCount = accelStruct.instanceCount;
			global::UnityEngine.Rendering.RadeonRays.HlbvhTopLevelBuilder.ScratchBufferLayout scratchLayout = global::UnityEngine.Rendering.RadeonRays.HlbvhTopLevelBuilder.ScratchBufferLayout.Create(instanceCount);
			cmd.SetComputeIntParam(shaderBuildHlbvh, global::UnityEngine.Rendering.RadeonRays.SID.g_constants_vertex_stride, 0);
			cmd.SetComputeIntParam(shaderBuildHlbvh, global::UnityEngine.Rendering.RadeonRays.SID.g_constants_triangle_count, (int)instanceCount);
			cmd.SetComputeIntParam(shaderBuildHlbvh, global::UnityEngine.Rendering.RadeonRays.SID.g_bvh_offset, 0);
			cmd.SetComputeIntParam(shaderBuildHlbvh, global::UnityEngine.Rendering.RadeonRays.SID.g_internal_node_range_offset, (int)scratchLayout.InternalNodeRange);
			cmd.SetComputeIntParam(shaderBuildHlbvh, global::UnityEngine.Rendering.RadeonRays.SID.g_aabb_offset, (int)scratchLayout.Aabb);
			BindKernelArguments(cmd, kernelInit, scratch, scratchLayout, accelStruct, setSortedCodes: false);
			cmd.DispatchCompute(shaderBuildHlbvh, kernelInit, 1, 1, 1);
			BindKernelArguments(cmd, kernelCalculateAabb, scratch, scratchLayout, accelStruct, setSortedCodes: false);
			cmd.DispatchCompute(shaderBuildHlbvh, kernelCalculateAabb, (int)global::UnityEngine.Rendering.RadeonRays.Common.CeilDivide(instanceCount, 2048u), 1, 1);
			BindKernelArguments(cmd, kernelCalculateMortonCodes, scratch, scratchLayout, accelStruct, setSortedCodes: false);
			cmd.DispatchCompute(shaderBuildHlbvh, kernelCalculateMortonCodes, (int)global::UnityEngine.Rendering.RadeonRays.Common.CeilDivide(instanceCount, 2048u), 1, 1);
			radixSort.Execute(cmd, scratch, scratchLayout.MortonCodes, scratchLayout.SortedMortonCodes, scratchLayout.PrimitiveRefs, scratchLayout.SortedPrimitiveRefs, scratchLayout.SortMemory, instanceCount);
			BindKernelArguments(cmd, kernelBuildTreeBottomUp, scratch, scratchLayout, accelStruct, setSortedCodes: true);
			cmd.DispatchCompute(shaderBuildHlbvh, kernelBuildTreeBottomUp, (int)global::UnityEngine.Rendering.RadeonRays.Common.CeilDivide(instanceCount, 2048u), 1, 1);
		}

		private void BindKernelArguments(global::UnityEngine.Rendering.CommandBuffer cmd, int kernel, global::UnityEngine.GraphicsBuffer scratch, global::UnityEngine.Rendering.RadeonRays.HlbvhTopLevelBuilder.ScratchBufferLayout scratchLayout, global::UnityEngine.Rendering.RadeonRays.TopLevelAccelStruct accelStruct, bool setSortedCodes)
		{
			cmd.SetComputeBufferParam(shaderBuildHlbvh, kernel, global::UnityEngine.Rendering.RadeonRays.SID.g_scratch_buffer, scratch);
			cmd.SetComputeBufferParam(shaderBuildHlbvh, kernel, global::UnityEngine.Rendering.RadeonRays.SID.g_bvh, accelStruct.topLevelBvh);
			cmd.SetComputeBufferParam(shaderBuildHlbvh, kernel, global::UnityEngine.Rendering.RadeonRays.SID.g_bottom_bvhs, accelStruct.bottomLevelBvhs);
			cmd.SetComputeBufferParam(shaderBuildHlbvh, kernel, global::UnityEngine.Rendering.RadeonRays.SID.g_instance_infos, accelStruct.instanceInfos);
			cmd.SetComputeIntParam(shaderBuildHlbvh, global::UnityEngine.Rendering.RadeonRays.SID.g_aabb_offset, (int)scratchLayout.Aabb);
			if (setSortedCodes)
			{
				cmd.SetComputeIntParam(shaderBuildHlbvh, global::UnityEngine.Rendering.RadeonRays.SID.g_morton_codes_offset, (int)scratchLayout.SortedMortonCodes);
				cmd.SetComputeIntParam(shaderBuildHlbvh, global::UnityEngine.Rendering.RadeonRays.SID.g_primitive_refs_offset, (int)scratchLayout.SortedPrimitiveRefs);
			}
			else
			{
				cmd.SetComputeIntParam(shaderBuildHlbvh, global::UnityEngine.Rendering.RadeonRays.SID.g_morton_codes_offset, (int)scratchLayout.MortonCodes);
				cmd.SetComputeIntParam(shaderBuildHlbvh, global::UnityEngine.Rendering.RadeonRays.SID.g_primitive_refs_offset, (int)scratchLayout.PrimitiveRefs);
			}
		}
	}
}
