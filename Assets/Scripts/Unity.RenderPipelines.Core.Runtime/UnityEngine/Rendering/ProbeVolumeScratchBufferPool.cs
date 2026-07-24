namespace UnityEngine.Rendering
{
	internal class ProbeVolumeScratchBufferPool
	{
		[global::System.Diagnostics.DebuggerDisplay("ChunkCount = {chunkCount} ElementCount = {pool.Count}")]
		private class ScratchBufferPool : global::System.IComparable<global::UnityEngine.Rendering.ProbeVolumeScratchBufferPool.ScratchBufferPool>
		{
			public int chunkCount = -1;

			public global::System.Collections.Generic.Stack<global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingScratchBuffer> pool = new global::System.Collections.Generic.Stack<global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingScratchBuffer>();

			public ScratchBufferPool(int chunkCount)
			{
				this.chunkCount = chunkCount;
			}

			private ScratchBufferPool()
			{
			}

			public int CompareTo(global::UnityEngine.Rendering.ProbeVolumeScratchBufferPool.ScratchBufferPool other)
			{
				if (chunkCount < other.chunkCount)
				{
					return -1;
				}
				if (chunkCount > other.chunkCount)
				{
					return 1;
				}
				return 0;
			}
		}

		private int m_L0Size;

		private int m_L1Size;

		private int m_ValiditySize;

		private int m_ValidityLayerCount;

		private int m_L2Size;

		private int m_ProbeOcclusionSize;

		private int m_SkyOcclusionSize;

		private int m_SkyShadingDirectionSize;

		private int m_CurrentlyAllocatedChunkCount;

		private global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeVolumeScratchBufferPool.ScratchBufferPool> m_Pools = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeVolumeScratchBufferPool.ScratchBufferPool>();

		private global::System.Collections.Generic.Dictionary<int, global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingScratchBufferLayout> m_Layouts = new global::System.Collections.Generic.Dictionary<int, global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingScratchBufferLayout>();

		private static int s_ChunkCount;

		public int chunkSize { get; private set; }

		public int maxChunkCount { get; private set; }

		public int allocatedMemory => chunkSize * m_CurrentlyAllocatedChunkCount;

		public ProbeVolumeScratchBufferPool(global::UnityEngine.Rendering.ProbeVolumeBakingSet bakingSet, global::UnityEngine.Rendering.ProbeVolumeSHBands shBands)
		{
			chunkSize = bakingSet.GetChunkGPUMemory(shBands);
			maxChunkCount = bakingSet.maxSHChunkCount;
			m_L0Size = bakingSet.L0ChunkSize;
			m_L1Size = bakingSet.L1ChunkSize;
			m_ValiditySize = bakingSet.sharedValidityMaskChunkSize;
			m_ValidityLayerCount = bakingSet.bakedMaskCount;
			m_SkyOcclusionSize = bakingSet.sharedSkyOcclusionL0L1ChunkSize;
			m_SkyShadingDirectionSize = bakingSet.sharedSkyShadingDirectionIndicesChunkSize;
			m_L2Size = bakingSet.L2TextureChunkSize;
			m_ProbeOcclusionSize = bakingSet.ProbeOcclusionChunkSize;
		}

		private global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingScratchBufferLayout GetOrCreateScratchBufferLayout(int chunkCount)
		{
			if (m_Layouts.TryGetValue(chunkCount, out var value))
			{
				return value;
			}
			global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingScratchBufferLayout cellStreamingScratchBufferLayout = new global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingScratchBufferLayout
			{
				_L0Size = m_L0Size,
				_L1Size = m_L1Size,
				_ValiditySize = m_ValiditySize,
				_ValidityProbeSize = m_ValidityLayerCount
			};
			if (m_SkyOcclusionSize != 0)
			{
				cellStreamingScratchBufferLayout._SkyOcclusionSize = m_SkyOcclusionSize;
				cellStreamingScratchBufferLayout._SkyOcclusionProbeSize = 8;
				if (m_SkyShadingDirectionSize != 0)
				{
					cellStreamingScratchBufferLayout._SkyShadingDirectionSize = m_SkyShadingDirectionSize;
					cellStreamingScratchBufferLayout._SkyShadingDirectionProbeSize = 1;
				}
				else
				{
					cellStreamingScratchBufferLayout._SkyShadingDirectionSize = 0;
					cellStreamingScratchBufferLayout._SkyShadingDirectionProbeSize = 0;
				}
			}
			else
			{
				cellStreamingScratchBufferLayout._SkyOcclusionSize = 0;
				cellStreamingScratchBufferLayout._SkyOcclusionProbeSize = 0;
				cellStreamingScratchBufferLayout._SkyShadingDirectionSize = 0;
				cellStreamingScratchBufferLayout._SkyShadingDirectionProbeSize = 0;
			}
			cellStreamingScratchBufferLayout._L2Size = m_L2Size;
			if (m_ProbeOcclusionSize != 0)
			{
				cellStreamingScratchBufferLayout._ProbeOcclusionSize = m_ProbeOcclusionSize;
				cellStreamingScratchBufferLayout._ProbeOcclusionProbeSize = 4;
			}
			else
			{
				cellStreamingScratchBufferLayout._ProbeOcclusionSize = 0;
				cellStreamingScratchBufferLayout._ProbeOcclusionProbeSize = 0;
			}
			cellStreamingScratchBufferLayout._L0ProbeSize = 8;
			cellStreamingScratchBufferLayout._L1ProbeSize = 4;
			cellStreamingScratchBufferLayout._L2ProbeSize = 4;
			int num = (cellStreamingScratchBufferLayout._SharedDestChunksOffset = chunkCount * 4 * 4);
			cellStreamingScratchBufferLayout._L0L1rxOffset = cellStreamingScratchBufferLayout._SharedDestChunksOffset + num;
			cellStreamingScratchBufferLayout._L1GryOffset = cellStreamingScratchBufferLayout._L0L1rxOffset + m_L0Size * chunkCount;
			cellStreamingScratchBufferLayout._L1BrzOffset = cellStreamingScratchBufferLayout._L1GryOffset + m_L1Size * chunkCount;
			cellStreamingScratchBufferLayout._ValidityOffset = cellStreamingScratchBufferLayout._L1BrzOffset + m_L1Size * chunkCount;
			cellStreamingScratchBufferLayout._ProbeOcclusionOffset = cellStreamingScratchBufferLayout._ValidityOffset + m_ValiditySize * chunkCount;
			cellStreamingScratchBufferLayout._SkyOcclusionOffset = cellStreamingScratchBufferLayout._ProbeOcclusionOffset + m_ProbeOcclusionSize * chunkCount;
			cellStreamingScratchBufferLayout._SkyShadingDirectionOffset = cellStreamingScratchBufferLayout._SkyOcclusionOffset + m_SkyOcclusionSize * chunkCount;
			cellStreamingScratchBufferLayout._L2_0Offset = cellStreamingScratchBufferLayout._SkyShadingDirectionOffset + m_SkyShadingDirectionSize * chunkCount;
			cellStreamingScratchBufferLayout._L2_1Offset = cellStreamingScratchBufferLayout._L2_0Offset + m_L2Size * chunkCount;
			cellStreamingScratchBufferLayout._L2_2Offset = cellStreamingScratchBufferLayout._L2_1Offset + m_L2Size * chunkCount;
			cellStreamingScratchBufferLayout._L2_3Offset = cellStreamingScratchBufferLayout._L2_2Offset + m_L2Size * chunkCount;
			cellStreamingScratchBufferLayout._ProbeCountInChunkLine = 512;
			cellStreamingScratchBufferLayout._ProbeCountInChunkSlice = 2048;
			m_Layouts.Add(chunkCount, cellStreamingScratchBufferLayout);
			return cellStreamingScratchBufferLayout;
		}

		private global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingScratchBuffer CreateScratchBuffer(int chunkCount, bool allocateGraphicsBuffers)
		{
			global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingScratchBuffer result = new global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingScratchBuffer(chunkCount, chunkSize, allocateGraphicsBuffers);
			m_CurrentlyAllocatedChunkCount += chunkCount;
			return result;
		}

		public bool AllocateScratchBuffer(int chunkCount, out global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingScratchBuffer scratchBuffer, out global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingScratchBufferLayout layout, bool allocateGraphicsBuffers)
		{
			s_ChunkCount = chunkCount;
			int num = m_Pools.FindIndex(0, (global::UnityEngine.Rendering.ProbeVolumeScratchBufferPool.ScratchBufferPool o) => o.chunkCount == s_ChunkCount);
			layout = GetOrCreateScratchBufferLayout(chunkCount);
			if (num != -1)
			{
				global::System.Collections.Generic.Stack<global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingScratchBuffer> pool = m_Pools[num].pool;
				if (pool.Count > 0)
				{
					scratchBuffer = pool.Pop();
					scratchBuffer.Swap();
					return true;
				}
				for (int num2 = num; num2 < m_Pools.Count; num2++)
				{
					global::UnityEngine.Rendering.ProbeVolumeScratchBufferPool.ScratchBufferPool scratchBufferPool = m_Pools[num2];
					if (scratchBufferPool.chunkCount >= chunkCount * 2)
					{
						break;
					}
					if (scratchBufferPool.pool.Count > 0)
					{
						scratchBuffer = scratchBufferPool.pool.Pop();
						scratchBuffer.Swap();
						return true;
					}
				}
				if (m_CurrentlyAllocatedChunkCount + chunkCount < maxChunkCount)
				{
					scratchBuffer = CreateScratchBuffer(chunkCount, allocateGraphicsBuffers);
					return true;
				}
				scratchBuffer = null;
				return false;
			}
			global::UnityEngine.Rendering.ProbeVolumeScratchBufferPool.ScratchBufferPool item = new global::UnityEngine.Rendering.ProbeVolumeScratchBufferPool.ScratchBufferPool(chunkCount);
			m_Pools.Add(item);
			m_Pools.Sort();
			scratchBuffer = CreateScratchBuffer(chunkCount, allocateGraphicsBuffers);
			return true;
		}

		public void ReleaseScratchBuffer(global::UnityEngine.Rendering.ProbeReferenceVolume.CellStreamingScratchBuffer scratchBuffer)
		{
			if (scratchBuffer.chunkSize != chunkSize)
			{
				scratchBuffer.Dispose();
				return;
			}
			s_ChunkCount = scratchBuffer.chunkCount;
			m_Pools.Find((global::UnityEngine.Rendering.ProbeVolumeScratchBufferPool.ScratchBufferPool o) => o.chunkCount == s_ChunkCount).pool.Push(scratchBuffer);
		}

		public void Cleanup()
		{
			foreach (global::UnityEngine.Rendering.ProbeVolumeScratchBufferPool.ScratchBufferPool pool in m_Pools)
			{
				while (pool.pool.Count > 0)
				{
					pool.pool.Pop().Dispose();
				}
			}
			m_Pools.Clear();
			m_CurrentlyAllocatedChunkCount = 0;
			chunkSize = 0;
			maxChunkCount = 0;
		}
	}
}
