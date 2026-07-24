namespace UnityEngine.Rendering.UnifiedRayTracing
{
	internal sealed class BLASPositionsPool : global::System.IDisposable
	{
		public const int VertexSizeInDwords = 3;

		private const int intialVertexCount = 1000;

		private global::UnityEngine.GraphicsBuffer m_VerticesBuffer;

		private global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator m_VerticesAllocator;

		private readonly global::UnityEngine.ComputeShader m_CopyPositionsShader;

		private readonly int m_CopyVerticesKernel;

		private readonly global::UnityEngine.ComputeShader m_CopyShader;

		private const uint kItemsPerWorkgroup = 6144u;

		public global::UnityEngine.GraphicsBuffer VertexBuffer => m_VerticesBuffer;

		public BLASPositionsPool(global::UnityEngine.ComputeShader copyPositionsShader, global::UnityEngine.ComputeShader copyShader)
		{
			m_VerticesBuffer = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Structured, 3000, 4);
			m_VerticesAllocator = default(global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator);
			m_VerticesAllocator.Initialize(1000);
			m_CopyPositionsShader = copyPositionsShader;
			m_CopyVerticesKernel = m_CopyPositionsShader.FindKernel("CopyVertexBuffer");
			m_CopyShader = copyShader;
		}

		public void Dispose()
		{
			m_VerticesBuffer.Dispose();
			m_VerticesAllocator.Dispose();
		}

		public void Clear()
		{
			m_VerticesBuffer.Dispose();
			m_VerticesBuffer = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Structured, 3000, 4);
			m_VerticesAllocator.Dispose();
			m_VerticesAllocator = default(global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator);
			m_VerticesAllocator.Initialize(1000);
		}

		public void Add(global::UnityEngine.Rendering.UnifiedRayTracing.VertexBufferChunk info, out global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation verticesAllocation)
		{
			verticesAllocation = m_VerticesAllocator.Allocate((int)info.vertexCount);
			if (!verticesAllocation.valid)
			{
				int oldCapacity = m_VerticesAllocator.capacity;
				int num = (int)global::Unity.Mathematics.math.min(2147483647L, global::UnityEngine.Rendering.UnifiedRayTracing.GraphicsHelpers.MaxGraphicsBufferSizeInBytes / global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Mathematics.float3>());
				if (!m_VerticesAllocator.GetExpectedGrowthToFitAllocation((int)info.vertexCount, num, out var newCapacity))
				{
					throw new global::UnityEngine.Rendering.UnifiedRayTracing.UnifiedRayTracingException($"VerticesAllocator can't grow to {num} elements", global::UnityEngine.Rendering.UnifiedRayTracing.UnifiedRayTracingError.GraphicsBufferAllocationFailed);
				}
				if (!global::UnityEngine.Rendering.UnifiedRayTracing.GraphicsHelpers.ReallocateBuffer(m_CopyShader, oldCapacity, newCapacity, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Mathematics.float3>(), ref m_VerticesBuffer))
				{
					throw new global::UnityEngine.Rendering.UnifiedRayTracing.UnifiedRayTracingException($"Failed to allocate buffer of size: {(newCapacity * global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Mathematics.float3>())} bytes", global::UnityEngine.Rendering.UnifiedRayTracing.UnifiedRayTracingError.GraphicsBufferAllocationFailed);
				}
				verticesAllocation = m_VerticesAllocator.GrowAndAllocate((int)info.vertexCount, num, out oldCapacity, out newCapacity);
			}
			global::UnityEngine.Rendering.CommandBuffer commandBuffer = new global::UnityEngine.Rendering.CommandBuffer();
			commandBuffer.SetComputeIntParam(m_CopyPositionsShader, "_InputPosBufferCount", (int)info.vertexCount);
			commandBuffer.SetComputeIntParam(m_CopyPositionsShader, "_InputPosBufferOffset", info.verticesStartOffset);
			commandBuffer.SetComputeIntParam(m_CopyPositionsShader, "_InputBaseVertex", info.baseVertex);
			commandBuffer.SetComputeIntParam(m_CopyPositionsShader, "_InputPosBufferStride", (int)info.vertexStride);
			commandBuffer.SetComputeIntParam(m_CopyPositionsShader, "_OutputPosBufferOffset", verticesAllocation.block.offset * 3);
			commandBuffer.SetComputeBufferParam(m_CopyPositionsShader, m_CopyVerticesKernel, "_InputPosBuffer", info.vertices);
			commandBuffer.SetComputeBufferParam(m_CopyPositionsShader, m_CopyVerticesKernel, "_OutputPosBuffer", m_VerticesBuffer);
			commandBuffer.DispatchCompute(m_CopyPositionsShader, m_CopyVerticesKernel, (int)global::UnityEngine.Rendering.RadeonRays.Common.CeilDivide(info.vertexCount, 6144u), 1, 1);
			global::UnityEngine.Graphics.ExecuteCommandBuffer(commandBuffer);
		}

		public void Remove(ref global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation verticesAllocation)
		{
			m_VerticesAllocator.FreeAllocation(in verticesAllocation);
			verticesAllocation = global::UnityEngine.Rendering.UnifiedRayTracing.BlockAllocator.Allocation.Invalid;
		}
	}
}
