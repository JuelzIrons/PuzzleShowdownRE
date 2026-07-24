namespace UnityEngine.Rendering
{
	internal struct InstanceOcclusionEventDebugArray : global::System.IDisposable
	{
		internal struct Info
		{
			public int viewInstanceID;

			public global::UnityEngine.Rendering.InstanceOcclusionEventType eventType;

			public int occluderVersion;

			public int subviewMask;

			public global::UnityEngine.Rendering.OcclusionTest occlusionTest;

			public bool HasVersion()
			{
				if (eventType != global::UnityEngine.Rendering.InstanceOcclusionEventType.OccluderUpdate)
				{
					return occlusionTest != global::UnityEngine.Rendering.OcclusionTest.None;
				}
				return true;
			}
		}

		internal struct Request
		{
			public global::Unity.Collections.LowLevel.Unsafe.UnsafeList<global::UnityEngine.Rendering.InstanceOcclusionEventDebugArray.Info> info;

			public global::UnityEngine.Rendering.AsyncGPUReadbackRequest readback;
		}

		private const int InitialPassCount = 4;

		private const int MaxPassCount = 64;

		private global::UnityEngine.GraphicsBuffer m_CounterBuffer;

		private global::Unity.Collections.LowLevel.Unsafe.UnsafeList<global::UnityEngine.Rendering.InstanceOcclusionEventDebugArray.Info> m_PendingInfo;

		private global::Unity.Collections.NativeQueue<global::UnityEngine.Rendering.InstanceOcclusionEventDebugArray.Request> m_Requests;

		private global::Unity.Collections.LowLevel.Unsafe.UnsafeList<global::UnityEngine.Rendering.InstanceOcclusionEventDebugArray.Info> m_LatestInfo;

		private global::Unity.Collections.NativeArray<int> m_LatestCounters;

		private bool m_HasLatest;

		public global::UnityEngine.GraphicsBuffer CounterBuffer => m_CounterBuffer;

		public void Init()
		{
			m_CounterBuffer = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Structured, 256, 4);
			m_PendingInfo = new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<global::UnityEngine.Rendering.InstanceOcclusionEventDebugArray.Info>(4, global::Unity.Collections.Allocator.Persistent);
			m_Requests = new global::Unity.Collections.NativeQueue<global::UnityEngine.Rendering.InstanceOcclusionEventDebugArray.Request>(global::Unity.Collections.Allocator.Persistent);
		}

		public void Dispose()
		{
			if (m_HasLatest)
			{
				m_LatestInfo.Dispose();
				m_LatestCounters.Dispose();
				m_HasLatest = false;
			}
			global::UnityEngine.Rendering.InstanceOcclusionEventDebugArray.Request item;
			while (m_Requests.TryDequeue(out item))
			{
				item.readback.WaitForCompletion();
				item.info.Dispose();
			}
			m_Requests.Dispose();
			m_PendingInfo.Dispose();
			m_CounterBuffer.Dispose();
		}

		public int TryAdd(int viewInstanceID, global::UnityEngine.Rendering.InstanceOcclusionEventType eventType, int occluderVersion, int subviewMask, global::UnityEngine.Rendering.OcclusionTest occlusionTest)
		{
			int length = m_PendingInfo.Length;
			if (length + 1 > 64)
			{
				return -1;
			}
			m_PendingInfo.Add(new global::UnityEngine.Rendering.InstanceOcclusionEventDebugArray.Info
			{
				viewInstanceID = viewInstanceID,
				eventType = eventType,
				occluderVersion = occluderVersion,
				subviewMask = subviewMask,
				occlusionTest = occlusionTest
			});
			return length;
		}

		public void MoveToDebugStatsAndClear(global::UnityEngine.Rendering.DebugRendererBatcherStats debugStats)
		{
			if (m_PendingInfo.Length > 0)
			{
				m_Requests.Enqueue(new global::UnityEngine.Rendering.InstanceOcclusionEventDebugArray.Request
				{
					info = m_PendingInfo,
					readback = global::UnityEngine.Rendering.AsyncGPUReadback.Request(m_CounterBuffer, m_PendingInfo.Length * 4 * 4, 0)
				});
				m_PendingInfo = new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<global::UnityEngine.Rendering.InstanceOcclusionEventDebugArray.Info>(4, global::Unity.Collections.Allocator.Persistent);
			}
			while (!m_Requests.IsEmpty() && m_Requests.Peek().readback.done)
			{
				global::UnityEngine.Rendering.InstanceOcclusionEventDebugArray.Request request = m_Requests.Dequeue();
				if (request.readback.hasError)
				{
					continue;
				}
				global::Unity.Collections.NativeArray<int> data = request.readback.GetData<int>();
				if (data.Length == request.info.Length * 4)
				{
					if (m_HasLatest)
					{
						m_LatestInfo.Dispose();
						m_LatestCounters.Dispose();
						m_HasLatest = false;
					}
					m_LatestInfo = request.info;
					m_LatestCounters = new global::Unity.Collections.NativeArray<int>(data, global::Unity.Collections.Allocator.Persistent);
					m_HasLatest = true;
				}
			}
			debugStats.instanceOcclusionEventStats.Clear();
			if (m_HasLatest)
			{
				for (int i = 0; i < m_LatestInfo.Length; i++)
				{
					global::UnityEngine.Rendering.InstanceOcclusionEventDebugArray.Info info = m_LatestInfo[i];
					int occluderVersion = -1;
					if (info.HasVersion())
					{
						occluderVersion = 0;
						for (int j = 0; j < i; j++)
						{
							global::UnityEngine.Rendering.InstanceOcclusionEventDebugArray.Info info2 = m_LatestInfo[j];
							if (info2.HasVersion() && info2.viewInstanceID == info.viewInstanceID)
							{
								occluderVersion = info.occluderVersion - info2.occluderVersion;
								break;
							}
						}
					}
					int num = i * 4;
					int culledInstances = m_LatestCounters[num];
					int visibleInstances = m_LatestCounters[num + 1];
					int culledPrimitives = m_LatestCounters[num + 2];
					int visiblePrimitives = m_LatestCounters[num + 3];
					debugStats.instanceOcclusionEventStats.Add(new global::UnityEngine.Rendering.InstanceOcclusionEventStats
					{
						viewInstanceID = info.viewInstanceID,
						eventType = info.eventType,
						occluderVersion = occluderVersion,
						subviewMask = info.subviewMask,
						occlusionTest = info.occlusionTest,
						visibleInstances = visibleInstances,
						culledInstances = culledInstances,
						visiblePrimitives = visiblePrimitives,
						culledPrimitives = culledPrimitives
					});
				}
			}
			global::Unity.Collections.NativeArray<int> data2 = new global::Unity.Collections.NativeArray<int>(256, global::Unity.Collections.Allocator.Temp);
			m_CounterBuffer.SetData(data2);
			data2.Dispose();
		}
	}
}
