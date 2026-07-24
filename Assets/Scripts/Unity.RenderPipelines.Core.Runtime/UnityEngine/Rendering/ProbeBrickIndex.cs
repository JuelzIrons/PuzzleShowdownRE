namespace UnityEngine.Rendering
{
	internal class ProbeBrickIndex
	{
		[global::System.Serializable]
		[global::System.Diagnostics.DebuggerDisplay("Brick [{position}, {subdivisionLevel}]")]
		public struct Brick : global::System.IEquatable<global::UnityEngine.Rendering.ProbeBrickIndex.Brick>
		{
			public global::UnityEngine.Vector3Int position;

			public int subdivisionLevel;

			internal Brick(global::UnityEngine.Vector3Int position, int subdivisionLevel)
			{
				this.position = position;
				this.subdivisionLevel = subdivisionLevel;
			}

			public bool Equals(global::UnityEngine.Rendering.ProbeBrickIndex.Brick other)
			{
				if (position == other.position)
				{
					return subdivisionLevel == other.subdivisionLevel;
				}
				return false;
			}

			public bool IntersectArea(global::UnityEngine.Bounds boundInBricksToCheck)
			{
				int num = global::UnityEngine.Rendering.ProbeReferenceVolume.CellSize(subdivisionLevel);
				global::UnityEngine.Bounds bounds = new global::UnityEngine.Bounds
				{
					min = position,
					max = position + new global::UnityEngine.Vector3Int(num, num, num)
				};
				bounds.extents *= 0.99f;
				return boundInBricksToCheck.Intersects(bounds);
			}
		}

		public struct IndirectionEntryUpdateInfo
		{
			public int firstChunkIndex;

			public int numberOfChunks;

			public int minSubdivInCell;

			public global::UnityEngine.Vector3Int minValidBrickIndexForCellAtMaxRes;

			public global::UnityEngine.Vector3Int maxValidBrickIndexForCellAtMaxResPlusOne;

			public global::UnityEngine.Vector3Int entryPositionInBricksAtMaxRes;

			public bool hasOnlyBiggerBricks;
		}

		public struct CellIndexUpdateInfo
		{
			public global::UnityEngine.Rendering.ProbeBrickIndex.IndirectionEntryUpdateInfo[] entriesInfo;

			public int GetNumberOfChunks()
			{
				int num = 0;
				global::UnityEngine.Rendering.ProbeBrickIndex.IndirectionEntryUpdateInfo[] array = entriesInfo;
				for (int i = 0; i < array.Length; i++)
				{
					global::UnityEngine.Rendering.ProbeBrickIndex.IndirectionEntryUpdateInfo indirectionEntryUpdateInfo = array[i];
					num += indirectionEntryUpdateInfo.numberOfChunks;
				}
				return num;
			}
		}

		internal const int kMaxSubdivisionLevels = 7;

		internal const int kIndexChunkSize = 243;

		internal const int kFailChunkIndex = -1;

		internal const int kEmptyIndex = -2;

		private global::System.Collections.BitArray m_IndexChunks;

		private global::System.Collections.BitArray m_IndexChunksCopyForChecks;

		private int m_ChunksCount;

		private int m_AvailableChunkCount;

		private global::UnityEngine.ComputeBuffer m_PhysicalIndexBuffer;

		private global::Unity.Collections.NativeArray<int> m_PhysicalIndexBufferData;

		private global::UnityEngine.ComputeBuffer m_DebugFragmentationBuffer;

		private int[] m_DebugFragmentationData;

		private bool m_NeedUpdateIndexComputeBuffer;

		private int m_UpdateMinIndex = int.MaxValue;

		private int m_UpdateMaxIndex = int.MinValue;

		private global::UnityEngine.Vector3Int m_CenterRS;

		internal int estimatedVMemCost { get; private set; }

		internal float fragmentationRate { get; private set; }

		internal global::UnityEngine.ComputeBuffer GetDebugFragmentationBuffer()
		{
			return m_DebugFragmentationBuffer;
		}

		private int SizeOfPhysicalIndexFromBudget(global::UnityEngine.Rendering.ProbeVolumeTextureMemoryBudget memoryBudget)
		{
			return memoryBudget switch
			{
				global::UnityEngine.Rendering.ProbeVolumeTextureMemoryBudget.MemoryBudgetLow => 4000000, 
				global::UnityEngine.Rendering.ProbeVolumeTextureMemoryBudget.MemoryBudgetMedium => 8000000, 
				global::UnityEngine.Rendering.ProbeVolumeTextureMemoryBudget.MemoryBudgetHigh => 16000000, 
				_ => 32000000, 
			};
		}

		internal ProbeBrickIndex(global::UnityEngine.Rendering.ProbeVolumeTextureMemoryBudget memoryBudget)
		{
			m_CenterRS = new global::UnityEngine.Vector3Int(0, 0, 0);
			m_NeedUpdateIndexComputeBuffer = false;
			m_ChunksCount = global::UnityEngine.Mathf.Max(1, global::UnityEngine.Mathf.CeilToInt((float)SizeOfPhysicalIndexFromBudget(memoryBudget) / 243f));
			m_AvailableChunkCount = m_ChunksCount;
			m_IndexChunks = new global::System.Collections.BitArray(m_ChunksCount);
			m_IndexChunksCopyForChecks = new global::System.Collections.BitArray(m_ChunksCount);
			int num = m_ChunksCount * 243;
			m_PhysicalIndexBufferData = new global::Unity.Collections.NativeArray<int>(num, global::Unity.Collections.Allocator.Persistent, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			m_PhysicalIndexBuffer = new global::UnityEngine.ComputeBuffer(num, 4, global::UnityEngine.ComputeBufferType.Structured);
			estimatedVMemCost = num * 4;
			Clear();
		}

		public int GetRemainingChunkCount()
		{
			return m_AvailableChunkCount;
		}

		internal void UploadIndexData()
		{
			int count = m_UpdateMaxIndex - m_UpdateMinIndex + 1;
			m_PhysicalIndexBuffer.SetData(m_PhysicalIndexBufferData, m_UpdateMinIndex, m_UpdateMinIndex, count);
			m_NeedUpdateIndexComputeBuffer = false;
			m_UpdateMaxIndex = int.MinValue;
			m_UpdateMinIndex = int.MaxValue;
		}

		private void UpdateDebugData()
		{
			if (m_DebugFragmentationData == null || m_DebugFragmentationData.Length != m_IndexChunks.Length)
			{
				m_DebugFragmentationData = new int[m_IndexChunks.Length];
				global::UnityEngine.Rendering.CoreUtils.SafeRelease(m_DebugFragmentationBuffer);
				m_DebugFragmentationBuffer = new global::UnityEngine.ComputeBuffer(m_IndexChunks.Length, 4);
			}
			for (int i = 0; i < m_IndexChunks.Length; i++)
			{
				m_DebugFragmentationData[i] = (m_IndexChunks[i] ? 1 : (-1));
			}
			m_DebugFragmentationBuffer.SetData(m_DebugFragmentationData);
		}

		internal unsafe void Clear()
		{
			m_IndexChunks.SetAll(value: false);
			m_AvailableChunkCount = m_ChunksCount;
			uint* unsafePtr = (uint*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(m_PhysicalIndexBufferData);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemSet(unsafePtr, byte.MaxValue, m_PhysicalIndexBufferData.Length * 4);
			m_NeedUpdateIndexComputeBuffer = true;
			m_UpdateMinIndex = 0;
			m_UpdateMaxIndex = m_PhysicalIndexBufferData.Length - 1;
		}

		internal void GetRuntimeResources(ref global::UnityEngine.Rendering.ProbeReferenceVolume.RuntimeResources rr)
		{
			bool displayIndexFragmentation = global::UnityEngine.Rendering.ProbeReferenceVolume.instance.probeVolumeDebug.displayIndexFragmentation;
			if (m_NeedUpdateIndexComputeBuffer)
			{
				UploadIndexData();
				if (displayIndexFragmentation)
				{
					UpdateDebugData();
				}
			}
			if (displayIndexFragmentation && m_DebugFragmentationBuffer == null)
			{
				UpdateDebugData();
			}
			rr.index = m_PhysicalIndexBuffer;
		}

		internal void Cleanup()
		{
			m_PhysicalIndexBufferData.Dispose();
			global::UnityEngine.Rendering.CoreUtils.SafeRelease(m_PhysicalIndexBuffer);
			m_PhysicalIndexBuffer = null;
			global::UnityEngine.Rendering.CoreUtils.SafeRelease(m_DebugFragmentationBuffer);
			m_DebugFragmentationBuffer = null;
		}

		internal void ComputeFragmentationRate()
		{
			int num = 0;
			for (int num2 = m_ChunksCount - 1; num2 >= 0; num2--)
			{
				if (m_IndexChunks[num2])
				{
					num = num2 + 1;
					break;
				}
			}
			int num3 = m_ChunksCount - num;
			int num4 = m_AvailableChunkCount - num3;
			fragmentationRate = (float)num4 / (float)num;
		}

		private int MergeIndex(int index, int size)
		{
			return (index & -1879048193) | ((size & 7) << 28);
		}

		internal int GetNumberOfChunks(int brickCount)
		{
			return global::UnityEngine.Mathf.CeilToInt((float)brickCount / 243f);
		}

		internal bool FindSlotsForEntries(ref global::UnityEngine.Rendering.ProbeBrickIndex.IndirectionEntryUpdateInfo[] entriesInfo)
		{
			using (new global::Unity.Profiling.ProfilerMarker("FindSlotsForEntries").Auto())
			{
				m_IndexChunksCopyForChecks.SetAll(value: false);
				m_IndexChunksCopyForChecks.Or(m_IndexChunks);
				int num = entriesInfo.Length;
				for (int i = 0; i < num; i++)
				{
					entriesInfo[i].firstChunkIndex = -2;
					int numberOfChunks = entriesInfo[i].numberOfChunks;
					if (numberOfChunks == 0)
					{
						continue;
					}
					for (int j = 0; j < m_ChunksCount - numberOfChunks; j++)
					{
						if (!m_IndexChunksCopyForChecks[j])
						{
							int firstChunkIndex = j;
							int num2 = j + numberOfChunks;
							while (j + 1 < num2 && !m_IndexChunksCopyForChecks[++j])
							{
							}
							if (!m_IndexChunksCopyForChecks[j])
							{
								entriesInfo[i].firstChunkIndex = firstChunkIndex;
								break;
							}
						}
					}
					if (entriesInfo[i].firstChunkIndex < 0)
					{
						for (int k = 0; k < num; k++)
						{
							entriesInfo[k].firstChunkIndex = -1;
						}
						return false;
					}
					for (int l = entriesInfo[i].firstChunkIndex; l < entriesInfo[i].firstChunkIndex + numberOfChunks; l++)
					{
						m_IndexChunksCopyForChecks[l] = true;
					}
				}
				return true;
			}
		}

		internal bool ReserveChunks(global::UnityEngine.Rendering.ProbeBrickIndex.IndirectionEntryUpdateInfo[] entriesInfo, bool ignoreErrorLog)
		{
			int num = entriesInfo.Length;
			for (int i = 0; i < num; i++)
			{
				int firstChunkIndex = entriesInfo[i].firstChunkIndex;
				int numberOfChunks = entriesInfo[i].numberOfChunks;
				if (numberOfChunks == 0)
				{
					continue;
				}
				if (firstChunkIndex < 0)
				{
					if (!ignoreErrorLog)
					{
						global::UnityEngine.Debug.LogError("APV Index Allocation failed.");
					}
					return false;
				}
				for (int j = firstChunkIndex; j < firstChunkIndex + numberOfChunks; j++)
				{
					m_IndexChunks[j] = true;
				}
				m_AvailableChunkCount -= numberOfChunks;
			}
			return true;
		}

		internal static bool BrickOverlapEntry(global::UnityEngine.Vector3Int brickMin, global::UnityEngine.Vector3Int brickMax, global::UnityEngine.Vector3Int entryMin, global::UnityEngine.Vector3Int entryMax)
		{
			if (brickMax.x > entryMin.x && entryMax.x > brickMin.x && brickMax.y > entryMin.y && entryMax.y > brickMin.y && brickMax.z > entryMin.z)
			{
				return entryMax.z > brickMin.z;
			}
			return false;
		}

		private static int LocationToIndex(int x, int y, int z, global::UnityEngine.Vector3Int sizeOfValid)
		{
			return z * (sizeOfValid.x * sizeOfValid.y) + x * sizeOfValid.y + y;
		}

		private void MarkBrickInPhysicalBuffer(in global::UnityEngine.Rendering.ProbeBrickIndex.IndirectionEntryUpdateInfo entry, global::UnityEngine.Vector3Int brickMin, global::UnityEngine.Vector3Int brickMax, int brickSubdivLevel, int entrySubdivLevel, int idx)
		{
			m_NeedUpdateIndexComputeBuffer = true;
			if (entry.hasOnlyBiggerBricks)
			{
				int num = entry.firstChunkIndex * 243;
				m_UpdateMinIndex = global::System.Math.Min(m_UpdateMinIndex, num);
				m_UpdateMaxIndex = global::System.Math.Max(m_UpdateMaxIndex, num);
				m_PhysicalIndexBufferData[num] = idx;
				return;
			}
			int num2 = global::UnityEngine.Rendering.ProbeReferenceVolume.CellSize(entry.minSubdivInCell);
			global::UnityEngine.Vector3Int vector3Int = entry.minValidBrickIndexForCellAtMaxRes / num2;
			global::UnityEngine.Vector3Int vector3Int2 = entry.maxValidBrickIndexForCellAtMaxResPlusOne / num2 - vector3Int;
			if (brickSubdivLevel >= entrySubdivLevel)
			{
				brickMin = global::UnityEngine.Vector3Int.zero;
				brickMax = vector3Int2;
			}
			else
			{
				brickMin -= entry.entryPositionInBricksAtMaxRes;
				brickMax -= entry.entryPositionInBricksAtMaxRes;
				brickMin /= num2;
				brickMax /= num2;
				global::UnityEngine.Rendering.ProbeReferenceVolume.CellSize(entrySubdivLevel - entry.minSubdivInCell);
				brickMin -= vector3Int;
				brickMax -= vector3Int;
			}
			int num3 = entry.firstChunkIndex * 243;
			int val = num3 + LocationToIndex(brickMin.x, brickMin.y, brickMin.z, vector3Int2);
			int val2 = num3 + LocationToIndex(brickMax.x - 1, brickMax.y - 1, brickMax.z - 1, vector3Int2);
			m_UpdateMinIndex = global::System.Math.Min(m_UpdateMinIndex, val);
			m_UpdateMaxIndex = global::System.Math.Max(m_UpdateMaxIndex, val2);
			for (int i = brickMin.x; i < brickMax.x; i++)
			{
				for (int j = brickMin.z; j < brickMax.z; j++)
				{
					for (int k = brickMin.y; k < brickMax.y; k++)
					{
						int num4 = LocationToIndex(i, k, j, vector3Int2);
						m_PhysicalIndexBufferData[num3 + num4] = idx;
					}
				}
			}
		}

		public void AddBricks(global::UnityEngine.Rendering.ProbeReferenceVolume.CellIndexInfo cellInfo, global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.ProbeBrickIndex.Brick> bricks, global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc> allocations, int allocationSize, int poolWidth, int poolHeight)
		{
			int entrySubdivLevel = global::UnityEngine.Rendering.ProbeReferenceVolume.instance.GetEntrySubdivLevel();
			int num = 0;
			for (int i = 0; i < allocations.Count; i++)
			{
				global::UnityEngine.Rendering.ProbeBrickPool.BrickChunkAlloc brickChunkAlloc = allocations[i];
				int num2 = num + global::UnityEngine.Mathf.Min(allocationSize, bricks.Length - num);
				while (num != num2)
				{
					global::UnityEngine.Rendering.ProbeBrickIndex.Brick brick = bricks[num++];
					int idx = MergeIndex(brickChunkAlloc.flattenIndex(poolWidth, poolHeight), brick.subdivisionLevel);
					brickChunkAlloc.x += 4;
					int num3 = global::UnityEngine.Rendering.ProbeReferenceVolume.CellSize(brick.subdivisionLevel);
					global::UnityEngine.Vector3Int position = brick.position;
					global::UnityEngine.Vector3Int brickMax = brick.position + new global::UnityEngine.Vector3Int(num3, num3, num3);
					global::UnityEngine.Rendering.ProbeBrickIndex.IndirectionEntryUpdateInfo[] entriesInfo = cellInfo.updateInfo.entriesInfo;
					for (int j = 0; j < entriesInfo.Length; j++)
					{
						global::UnityEngine.Rendering.ProbeBrickIndex.IndirectionEntryUpdateInfo entry = entriesInfo[j];
						global::UnityEngine.Vector3Int entryMin = entry.entryPositionInBricksAtMaxRes + entry.minValidBrickIndexForCellAtMaxRes;
						global::UnityEngine.Vector3Int entryMax = entry.entryPositionInBricksAtMaxRes + entry.maxValidBrickIndexForCellAtMaxResPlusOne - global::UnityEngine.Vector3Int.one;
						if (BrickOverlapEntry(position, brickMax, entryMin, entryMax))
						{
							MarkBrickInPhysicalBuffer(in entry, position, brickMax, brick.subdivisionLevel, entrySubdivLevel, idx);
						}
					}
				}
			}
		}

		public void RemoveBricks(global::UnityEngine.Rendering.ProbeReferenceVolume.CellIndexInfo cellInfo)
		{
			for (int i = 0; i < cellInfo.updateInfo.entriesInfo.Length; i++)
			{
				ref global::UnityEngine.Rendering.ProbeBrickIndex.IndirectionEntryUpdateInfo reference = ref cellInfo.updateInfo.entriesInfo[i];
				if (reference.firstChunkIndex >= 0)
				{
					for (int j = reference.firstChunkIndex; j < reference.firstChunkIndex + reference.numberOfChunks; j++)
					{
						m_IndexChunks[j] = false;
					}
					m_AvailableChunkCount += reference.numberOfChunks;
					reference.numberOfChunks = 0;
				}
			}
		}
	}
}
