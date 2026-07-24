namespace UnityEngine.Rendering
{
	internal struct IndirectBufferContextStorage : global::System.IDisposable
	{
		private const int kAllocatorCount = 2;

		internal const int kInstanceInfoGpuOffsetMultiplier = 2;

		private global::UnityEngine.Rendering.IndirectBufferLimits m_BufferLimits;

		private global::UnityEngine.GraphicsBuffer m_InstanceBuffer;

		private global::UnityEngine.GraphicsBuffer m_InstanceInfoBuffer;

		private global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.IndirectInstanceInfo> m_InstanceInfoStaging;

		private global::UnityEngine.GraphicsBuffer m_DispatchArgsBuffer;

		private global::UnityEngine.GraphicsBuffer m_DrawArgsBuffer;

		private global::UnityEngine.GraphicsBuffer m_DrawInfoBuffer;

		private global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.IndirectDrawInfo> m_DrawInfoStaging;

		private int m_ContextAllocCounter;

		private global::Unity.Collections.NativeHashMap<int, int> m_ContextIndexFromViewID;

		private global::Unity.Collections.NativeList<global::UnityEngine.Rendering.IndirectBufferContext> m_Contexts;

		private global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.IndirectBufferAllocInfo> m_ContextAllocInfo;

		private global::Unity.Collections.NativeArray<int> m_AllocationCounters;

		public global::UnityEngine.GraphicsBuffer instanceBuffer => m_InstanceBuffer;

		public global::UnityEngine.GraphicsBuffer instanceInfoBuffer => m_InstanceInfoBuffer;

		public global::UnityEngine.GraphicsBuffer dispatchArgsBuffer => m_DispatchArgsBuffer;

		public global::UnityEngine.GraphicsBuffer drawArgsBuffer => m_DrawArgsBuffer;

		public global::UnityEngine.GraphicsBuffer drawInfoBuffer => m_DrawInfoBuffer;

		public global::UnityEngine.GraphicsBufferHandle visibleInstanceBufferHandle => m_InstanceBuffer.bufferHandle;

		public global::UnityEngine.GraphicsBufferHandle indirectDrawArgsBufferHandle => m_DrawArgsBuffer.bufferHandle;

		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.IndirectInstanceInfo> instanceInfoGlobalArray => m_InstanceInfoStaging;

		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.IndirectDrawInfo> drawInfoGlobalArray => m_DrawInfoStaging;

		public global::Unity.Collections.NativeArray<int> allocationCounters => m_AllocationCounters;

		public global::UnityEngine.Rendering.IndirectBufferContextHandles ImportBuffers(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph)
		{
			return new global::UnityEngine.Rendering.IndirectBufferContextHandles
			{
				instanceBuffer = renderGraph.ImportBuffer(m_InstanceBuffer),
				instanceInfoBuffer = renderGraph.ImportBuffer(m_InstanceInfoBuffer),
				dispatchArgsBuffer = renderGraph.ImportBuffer(m_DispatchArgsBuffer),
				drawArgsBuffer = renderGraph.ImportBuffer(m_DrawArgsBuffer),
				drawInfoBuffer = renderGraph.ImportBuffer(m_DrawInfoBuffer)
			};
		}

		public void Init()
		{
			int num = 256;
			int maxInstanceCount = 64 * num;
			int num2 = 8;
			AllocateInstanceBuffers(maxInstanceCount);
			AllocateDrawBuffers(num);
			m_ContextIndexFromViewID = new global::Unity.Collections.NativeHashMap<int, int>(num2, global::Unity.Collections.Allocator.Persistent);
			m_Contexts = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.IndirectBufferContext>(num2, global::Unity.Collections.Allocator.Persistent);
			m_ContextAllocInfo = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.IndirectBufferAllocInfo>(num2, global::Unity.Collections.Allocator.Persistent, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			m_AllocationCounters = new global::Unity.Collections.NativeArray<int>(2, global::Unity.Collections.Allocator.Persistent, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			ResetAllocators();
		}

		private void AllocateInstanceBuffers(int maxInstanceCount)
		{
			m_InstanceBuffer = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Raw, maxInstanceCount, 4);
			m_InstanceInfoBuffer = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Structured, 2 * maxInstanceCount, global::System.Runtime.InteropServices.Marshal.SizeOf<global::UnityEngine.Rendering.IndirectInstanceInfo>());
			m_InstanceInfoStaging = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.IndirectInstanceInfo>(maxInstanceCount, global::Unity.Collections.Allocator.Persistent, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			m_BufferLimits.maxInstanceCount = maxInstanceCount;
		}

		private void FreeInstanceBuffers()
		{
			m_InstanceBuffer.Release();
			m_InstanceInfoBuffer.Release();
			m_InstanceInfoStaging.Dispose();
			m_BufferLimits.maxInstanceCount = 0;
		}

		private void AllocateDrawBuffers(int maxDrawCount)
		{
			m_DispatchArgsBuffer = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Structured | global::UnityEngine.GraphicsBuffer.Target.IndirectArguments, 3, 4);
			m_DrawArgsBuffer = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Structured | global::UnityEngine.GraphicsBuffer.Target.IndirectArguments, maxDrawCount * 5, 4);
			m_DrawInfoBuffer = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Structured, maxDrawCount, global::System.Runtime.InteropServices.Marshal.SizeOf<global::UnityEngine.Rendering.IndirectDrawInfo>());
			m_DrawInfoStaging = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.IndirectDrawInfo>(maxDrawCount, global::Unity.Collections.Allocator.Persistent, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			m_BufferLimits.maxDrawCount = maxDrawCount;
		}

		private void FreeDrawBuffers()
		{
			m_DispatchArgsBuffer.Release();
			m_DrawArgsBuffer.Release();
			m_DrawInfoBuffer.Release();
			m_DrawInfoStaging.Dispose();
			m_BufferLimits.maxDrawCount = 0;
		}

		public void Dispose()
		{
			SyncContexts();
			FreeInstanceBuffers();
			FreeDrawBuffers();
			m_ContextIndexFromViewID.Dispose();
			m_Contexts.Dispose();
			m_ContextAllocInfo.Dispose();
			m_AllocationCounters.Dispose();
		}

		private void SyncContexts()
		{
			for (int i = 0; i < m_Contexts.Length; i++)
			{
				m_Contexts[i].cullingJobHandle.Complete();
			}
		}

		private void ResetAllocators()
		{
			m_ContextAllocCounter = 0;
			m_ContextIndexFromViewID.Clear();
			m_Contexts.Clear();
			global::UnityEngine.Rendering.ArrayExtensions.FillArray(ref m_AllocationCounters, 0);
		}

		private void GrowBuffers()
		{
			if (m_ContextAllocCounter > m_ContextAllocInfo.Length)
			{
				int num = m_ContextAllocCounter * 6 / 5;
				m_Contexts.Clear();
				m_Contexts.SetCapacity(num);
				m_ContextAllocInfo.Dispose();
				m_ContextAllocInfo = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.IndirectBufferAllocInfo>(num, global::Unity.Collections.Allocator.Persistent, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			}
			int num2 = m_AllocationCounters[0];
			if (num2 > m_BufferLimits.maxInstanceCount)
			{
				int maxInstanceCount = num2 * 6 / 5;
				FreeInstanceBuffers();
				AllocateInstanceBuffers(maxInstanceCount);
			}
			int num3 = m_AllocationCounters[1];
			if (num3 > m_BufferLimits.maxDrawCount)
			{
				int maxDrawCount = num3 * 6 / 5;
				FreeDrawBuffers();
				AllocateDrawBuffers(maxDrawCount);
			}
		}

		public void ClearContextsAndGrowBuffers()
		{
			SyncContexts();
			GrowBuffers();
			ResetAllocators();
		}

		public int TryAllocateContext(int viewID)
		{
			if (m_ContextIndexFromViewID.ContainsKey(viewID))
			{
				return -1;
			}
			int num = -1;
			m_ContextAllocCounter++;
			if (m_Contexts.Length < m_ContextAllocInfo.Length)
			{
				num = m_Contexts.Length;
				m_Contexts.Add(default(global::UnityEngine.Rendering.IndirectBufferContext));
				m_ContextIndexFromViewID.Add(viewID, num);
			}
			return num;
		}

		public int TryGetContextIndex(int viewID)
		{
			if (!m_ContextIndexFromViewID.TryGetValue(viewID, out var item))
			{
				return -1;
			}
			return item;
		}

		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.IndirectBufferAllocInfo> GetAllocInfoSubArray(int contextIndex)
		{
			int start = global::UnityEngine.Mathf.Max(contextIndex, 0);
			return m_ContextAllocInfo.GetSubArray(start, 1);
		}

		public global::UnityEngine.Rendering.IndirectBufferAllocInfo GetAllocInfo(int contextIndex)
		{
			global::UnityEngine.Rendering.IndirectBufferAllocInfo result = default(global::UnityEngine.Rendering.IndirectBufferAllocInfo);
			if (0 <= contextIndex && contextIndex < m_Contexts.Length)
			{
				return m_ContextAllocInfo[contextIndex];
			}
			return result;
		}

		public void CopyFromStaging(global::UnityEngine.Rendering.CommandBuffer cmd, in global::UnityEngine.Rendering.IndirectBufferAllocInfo allocInfo)
		{
			if (!allocInfo.IsEmpty())
			{
				cmd.SetBufferData(m_DrawInfoBuffer, m_DrawInfoStaging, allocInfo.drawAllocIndex, allocInfo.drawAllocIndex, allocInfo.drawCount);
				cmd.SetBufferData(m_InstanceInfoBuffer, m_InstanceInfoStaging, allocInfo.instanceAllocIndex, 2 * allocInfo.instanceAllocIndex, allocInfo.instanceCount);
			}
		}

		public global::UnityEngine.Rendering.IndirectBufferLimits GetLimits(int contextIndex)
		{
			global::UnityEngine.Rendering.IndirectBufferLimits result = default(global::UnityEngine.Rendering.IndirectBufferLimits);
			if (contextIndex >= 0)
			{
				return m_BufferLimits;
			}
			return result;
		}

		public global::UnityEngine.Rendering.IndirectBufferContext GetBufferContext(int contextIndex)
		{
			global::UnityEngine.Rendering.IndirectBufferContext result = default(global::UnityEngine.Rendering.IndirectBufferContext);
			if (0 <= contextIndex && contextIndex < m_Contexts.Length)
			{
				return m_Contexts[contextIndex];
			}
			return result;
		}

		public void SetBufferContext(int contextIndex, global::UnityEngine.Rendering.IndirectBufferContext ctx)
		{
			if (0 <= contextIndex && contextIndex < m_Contexts.Length)
			{
				m_Contexts[contextIndex] = ctx;
			}
		}
	}
}
