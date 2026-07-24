namespace UnityEngine.Rendering
{
	internal struct InstanceCullerSplitDebugArray : global::System.IDisposable
	{
		internal struct Info
		{
			public global::UnityEngine.Rendering.BatchCullingViewType viewType;

			public int viewInstanceID;

			public int splitIndex;
		}

		private const int MaxSplitCount = 64;

		private global::Unity.Collections.NativeList<global::UnityEngine.Rendering.InstanceCullerSplitDebugArray.Info> m_Info;

		private global::Unity.Collections.NativeArray<int> m_Counters;

		private global::Unity.Collections.NativeQueue<global::Unity.Jobs.JobHandle> m_CounterSync;

		public global::Unity.Collections.NativeArray<int> Counters => m_Counters;

		public void Init()
		{
			m_Info = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.InstanceCullerSplitDebugArray.Info>(global::Unity.Collections.Allocator.Persistent);
			m_Counters = new global::Unity.Collections.NativeArray<int>(192, global::Unity.Collections.Allocator.Persistent);
			m_CounterSync = new global::Unity.Collections.NativeQueue<global::Unity.Jobs.JobHandle>(global::Unity.Collections.Allocator.Persistent);
		}

		public void Dispose()
		{
			m_Info.Dispose();
			m_Counters.Dispose();
			m_CounterSync.Dispose();
		}

		public int TryAddSplits(global::UnityEngine.Rendering.BatchCullingViewType viewType, int viewInstanceID, int splitCount)
		{
			int length = m_Info.Length;
			if (length + splitCount > 64)
			{
				return -1;
			}
			for (int i = 0; i < splitCount; i++)
			{
				m_Info.Add(new global::UnityEngine.Rendering.InstanceCullerSplitDebugArray.Info
				{
					viewType = viewType,
					viewInstanceID = viewInstanceID,
					splitIndex = i
				});
			}
			return length;
		}

		public void AddSync(int baseIndex, global::Unity.Jobs.JobHandle jobHandle)
		{
			if (baseIndex != -1)
			{
				m_CounterSync.Enqueue(jobHandle);
			}
		}

		public void MoveToDebugStatsAndClear(global::UnityEngine.Rendering.DebugRendererBatcherStats debugStats)
		{
			global::Unity.Jobs.JobHandle item;
			while (m_CounterSync.TryDequeue(out item))
			{
				item.Complete();
			}
			debugStats.instanceCullerStats.Clear();
			for (int i = 0; i < m_Info.Length; i++)
			{
				global::UnityEngine.Rendering.InstanceCullerSplitDebugArray.Info info = m_Info[i];
				int num = i * 3;
				debugStats.instanceCullerStats.Add(new global::UnityEngine.Rendering.InstanceCullerViewStats
				{
					viewType = info.viewType,
					viewInstanceID = info.viewInstanceID,
					splitIndex = info.splitIndex,
					visibleInstancesOnCPU = m_Counters[num],
					visibleInstancesOnGPU = 0,
					visiblePrimitivesOnCPU = m_Counters[num + 1],
					visiblePrimitivesOnGPU = 0,
					drawCommands = m_Counters[num + 2]
				});
			}
			m_Info.Clear();
			global::UnityEngine.Rendering.ArrayExtensions.FillArray(ref m_Counters, 0);
		}
	}
}
