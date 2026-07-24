namespace UnityEngine.Rendering
{
	internal class ProbeGlobalIndirection
	{
		internal struct IndexMetaData
		{
			private static uint[] s_PackedValues = new uint[3];

			internal global::UnityEngine.Vector3Int minLocalIdx;

			internal global::UnityEngine.Vector3Int maxLocalIdxPlusOne;

			internal int firstChunkIndex;

			internal int minSubdiv;

			internal void Pack(out uint[] vals)
			{
				vals = s_PackedValues;
				for (int i = 0; i < 3; i++)
				{
					vals[i] = 0u;
				}
				global::UnityEngine.Vector3Int vector3Int = maxLocalIdxPlusOne - minLocalIdx;
				vals[0] = (uint)(firstChunkIndex & 0x1FFFFFFF);
				vals[0] |= (uint)((minSubdiv & 7) << 29);
				vals[1] = (uint)(minLocalIdx.x & 0x3FF);
				vals[1] |= (uint)((minLocalIdx.y & 0x3FF) << 10);
				vals[1] |= (uint)((minLocalIdx.z & 0x3FF) << 20);
				vals[2] = (uint)(vector3Int.x & 0x3FF);
				vals[2] |= (uint)((vector3Int.y & 0x3FF) << 10);
				vals[2] |= (uint)((vector3Int.z & 0x3FF) << 20);
			}
		}

		private const int kUintPerEntry = 3;

		internal const int kEntryMaxSubdivLevel = 3;

		private global::UnityEngine.ComputeBuffer m_IndexOfIndicesBuffer;

		private uint[] m_IndexOfIndicesData;

		private int m_CellSizeInMinBricks;

		private global::UnityEngine.Vector3Int m_EntriesCount;

		private global::UnityEngine.Vector3Int m_EntryMin;

		private global::UnityEngine.Vector3Int m_EntryMax;

		private bool m_NeedUpdateComputeBuffer;

		internal int estimatedVMemCost { get; private set; }

		private int entrySizeInBricks => global::UnityEngine.Mathf.Min((int)global::UnityEngine.Mathf.Pow(3f, 3f), m_CellSizeInMinBricks);

		internal int entriesPerCellDimension => m_CellSizeInMinBricks / global::UnityEngine.Mathf.Max(1, entrySizeInBricks);

		internal void GetMinMaxEntry(out global::UnityEngine.Vector3Int minEntry, out global::UnityEngine.Vector3Int maxEntry)
		{
			minEntry = m_EntryMin;
			maxEntry = m_EntryMax;
		}

		internal global::UnityEngine.Vector3Int GetGlobalIndirectionDimension()
		{
			return m_EntriesCount;
		}

		internal global::UnityEngine.Vector3Int GetGlobalIndirectionMinEntry()
		{
			return m_EntryMin;
		}

		private int GetFlatIndex(global::UnityEngine.Vector3Int normalizedPos)
		{
			return normalizedPos.z * (m_EntriesCount.x * m_EntriesCount.y) + normalizedPos.y * m_EntriesCount.x + normalizedPos.x;
		}

		internal ProbeGlobalIndirection(global::UnityEngine.Vector3Int cellMin, global::UnityEngine.Vector3Int cellMax, int cellSizeInMinBricks)
		{
			m_CellSizeInMinBricks = cellSizeInMinBricks;
			global::UnityEngine.Vector3Int vector3Int = cellMax + global::UnityEngine.Vector3Int.one - cellMin;
			m_EntriesCount = vector3Int * entriesPerCellDimension;
			m_EntryMin = cellMin * entriesPerCellDimension;
			m_EntryMax = (cellMax + global::UnityEngine.Vector3Int.one) * entriesPerCellDimension - global::UnityEngine.Vector3Int.one;
			int num = m_EntriesCount.x * m_EntriesCount.y * m_EntriesCount.z;
			int num2 = 3 * num;
			m_IndexOfIndicesBuffer = new global::UnityEngine.ComputeBuffer(num, 12);
			m_IndexOfIndicesData = new uint[num2];
			m_NeedUpdateComputeBuffer = false;
			estimatedVMemCost = num * 3 * 4;
		}

		internal int GetFlatIdxForEntry(global::UnityEngine.Vector3Int entryPosition)
		{
			global::UnityEngine.Vector3Int normalizedPos = entryPosition - m_EntryMin;
			return GetFlatIndex(normalizedPos);
		}

		internal int[] GetFlatIndicesForCell(global::UnityEngine.Vector3Int cellPosition)
		{
			global::UnityEngine.Vector3Int vector3Int = cellPosition * entriesPerCellDimension;
			int num = m_CellSizeInMinBricks / entrySizeInBricks;
			int[] array = new int[entriesPerCellDimension * entriesPerCellDimension * entriesPerCellDimension];
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num; j++)
				{
					for (int k = 0; k < num; k++)
					{
						array[num2++] = GetFlatIdxForEntry(vector3Int + new global::UnityEngine.Vector3Int(i, j, k));
					}
				}
			}
			return array;
		}

		internal void UpdateCell(global::UnityEngine.Rendering.ProbeReferenceVolume.CellIndexInfo cellInfo)
		{
			for (int i = 0; i < cellInfo.flatIndicesInGlobalIndirection.Length; i++)
			{
				int num = cellInfo.flatIndicesInGlobalIndirection[i];
				global::UnityEngine.Rendering.ProbeBrickIndex.IndirectionEntryUpdateInfo indirectionEntryUpdateInfo = cellInfo.updateInfo.entriesInfo[i];
				int num2 = global::UnityEngine.Rendering.ProbeReferenceVolume.CellSize(indirectionEntryUpdateInfo.minSubdivInCell);
				global::UnityEngine.Rendering.ProbeGlobalIndirection.IndexMetaData indexMetaData = default(global::UnityEngine.Rendering.ProbeGlobalIndirection.IndexMetaData);
				indexMetaData.minSubdiv = indirectionEntryUpdateInfo.minSubdivInCell;
				indexMetaData.minLocalIdx = (indirectionEntryUpdateInfo.hasOnlyBiggerBricks ? global::UnityEngine.Vector3Int.zero : (indirectionEntryUpdateInfo.minValidBrickIndexForCellAtMaxRes / num2));
				indexMetaData.maxLocalIdxPlusOne = (indirectionEntryUpdateInfo.hasOnlyBiggerBricks ? global::UnityEngine.Vector3Int.one : (indirectionEntryUpdateInfo.maxValidBrickIndexForCellAtMaxResPlusOne / num2));
				indexMetaData.firstChunkIndex = indirectionEntryUpdateInfo.firstChunkIndex;
				indexMetaData.Pack(out var vals);
				for (int j = 0; j < 3; j++)
				{
					m_IndexOfIndicesData[num * 3 + j] = vals[j];
				}
			}
			m_NeedUpdateComputeBuffer = true;
		}

		internal void MarkEntriesAsUnloaded(int[] entriesFlatIndices)
		{
			for (int i = 0; i < entriesFlatIndices.Length; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					m_IndexOfIndicesData[entriesFlatIndices[i] * 3 + j] = uint.MaxValue;
				}
			}
			m_NeedUpdateComputeBuffer = true;
		}

		internal void PushComputeData()
		{
			m_IndexOfIndicesBuffer.SetData(m_IndexOfIndicesData);
			m_NeedUpdateComputeBuffer = false;
		}

		internal void GetRuntimeResources(ref global::UnityEngine.Rendering.ProbeReferenceVolume.RuntimeResources rr)
		{
			if (m_NeedUpdateComputeBuffer)
			{
				PushComputeData();
			}
			rr.cellIndices = m_IndexOfIndicesBuffer;
		}

		internal void Cleanup()
		{
			global::UnityEngine.Rendering.CoreUtils.SafeRelease(m_IndexOfIndicesBuffer);
			m_IndexOfIndicesBuffer = null;
		}
	}
}
