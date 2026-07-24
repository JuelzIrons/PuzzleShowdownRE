namespace Unity.Networking.Transport
{
	internal struct NetworkEventQueue : global::System.IDisposable
	{
		private struct SubQueueItem
		{
			public int connection;

			public int idx;
		}

		public struct Concurrent
		{
			[global::Unity.Collections.LowLevel.Unsafe.NativeContainer]
			[global::Unity.Collections.LowLevel.Unsafe.NativeContainerIsAtomicWriteOnly]
			internal struct ConcurrentConnectionQueue
			{
				[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
				private unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeList<int>* m_ConnectionEventHeadTail;

				public unsafe int Length => m_ConnectionEventHeadTail->Length;

				public unsafe ConcurrentConnectionQueue(global::Unity.Collections.NativeList<int> queue)
				{
					m_ConnectionEventHeadTail = (global::Unity.Collections.LowLevel.Unsafe.UnsafeList<int>*)global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetInternalListDataPtrUnchecked(ref queue);
				}

				public unsafe int Dequeue(int connectionId)
				{
					int num = -1;
					if (connectionId < 0 || connectionId >= m_ConnectionEventHeadTail->Length / 2)
					{
						return -1;
					}
					while (num < 0)
					{
						num = m_ConnectionEventHeadTail->Ptr[connectionId * 2];
						if (num >= m_ConnectionEventHeadTail->Ptr[connectionId * 2 + 1])
						{
							return -1;
						}
						if (global::System.Threading.Interlocked.CompareExchange(ref m_ConnectionEventHeadTail->Ptr[connectionId * 2], num + 1, num) != num)
						{
							num = -1;
						}
					}
					return num;
				}
			}

			[global::Unity.Collections.ReadOnly]
			internal global::Unity.Collections.NativeList<global::Unity.Networking.Transport.NetworkEvent> m_ConnectionEventQ;

			internal global::Unity.Networking.Transport.NetworkEventQueue.Concurrent.ConcurrentConnectionQueue m_ConnectionEventHeadTail;

			private int MaxEvents => m_ConnectionEventQ.Length / (m_ConnectionEventHeadTail.Length / 2);

			public global::Unity.Networking.Transport.NetworkEvent.Type PopEventForConnection(int connectionId, out int offset, out int size)
			{
				int pipelineId;
				return PopEventForConnection(connectionId, out offset, out size, out pipelineId);
			}

			public global::Unity.Networking.Transport.NetworkEvent.Type PopEventForConnection(int connectionId, out int offset, out int size, out int pipelineId)
			{
				offset = 0;
				size = 0;
				pipelineId = 0;
				int num = m_ConnectionEventHeadTail.Dequeue(connectionId);
				if (num < 0)
				{
					return global::Unity.Networking.Transport.NetworkEvent.Type.Empty;
				}
				global::Unity.Networking.Transport.NetworkEvent networkEvent = m_ConnectionEventQ[connectionId * MaxEvents + num];
				pipelineId = networkEvent.pipelineId;
				if (networkEvent.type == global::Unity.Networking.Transport.NetworkEvent.Type.Data || networkEvent.type == global::Unity.Networking.Transport.NetworkEvent.Type.Disconnect)
				{
					offset = networkEvent.offset;
					size = networkEvent.size;
				}
				return networkEvent.type;
			}
		}

		private global::Unity.Collections.NativeQueue<global::Unity.Networking.Transport.NetworkEventQueue.SubQueueItem> m_MasterEventQ;

		private global::Unity.Collections.NativeList<global::Unity.Networking.Transport.NetworkEvent> m_ConnectionEventQ;

		private global::Unity.Collections.NativeList<int> m_ConnectionEventHeadTail;

		private int MaxEvents => m_ConnectionEventQ.Length / (m_ConnectionEventHeadTail.Length / 2);

		public NetworkEventQueue(int queueSizePerConnection)
		{
			m_MasterEventQ = new global::Unity.Collections.NativeQueue<global::Unity.Networking.Transport.NetworkEventQueue.SubQueueItem>(global::Unity.Collections.Allocator.Persistent);
			m_ConnectionEventQ = new global::Unity.Collections.NativeList<global::Unity.Networking.Transport.NetworkEvent>(queueSizePerConnection, global::Unity.Collections.Allocator.Persistent);
			m_ConnectionEventHeadTail = new global::Unity.Collections.NativeList<int>(2, global::Unity.Collections.Allocator.Persistent);
			m_ConnectionEventQ.ResizeUninitialized(queueSizePerConnection);
			m_ConnectionEventHeadTail.Add(0);
			m_ConnectionEventHeadTail.Add(0);
		}

		public void Dispose()
		{
			m_MasterEventQ.Dispose();
			m_ConnectionEventQ.Dispose();
			m_ConnectionEventHeadTail.Dispose();
		}

		public global::Unity.Networking.Transport.NetworkEvent.Type PopEvent(out int id, out int offset, out int size)
		{
			int pipelineId;
			return PopEvent(out id, out offset, out size, out pipelineId);
		}

		public global::Unity.Networking.Transport.NetworkEvent.Type PopEvent(out int id, out int offset, out int size, out int pipelineId)
		{
			offset = 0;
			size = 0;
			id = -1;
			pipelineId = 0;
			global::Unity.Networking.Transport.NetworkEventQueue.SubQueueItem item;
			do
			{
				if (!m_MasterEventQ.TryDequeue(out item))
				{
					return global::Unity.Networking.Transport.NetworkEvent.Type.Empty;
				}
			}
			while (m_ConnectionEventHeadTail[item.connection * 2] != item.idx);
			id = item.connection;
			return PopEventForConnection(item.connection, out offset, out size, out pipelineId);
		}

		public global::Unity.Networking.Transport.NetworkEvent.Type PopEventForConnection(int connectionId, out int offset, out int size)
		{
			int pipelineId;
			return PopEventForConnection(connectionId, out offset, out size, out pipelineId);
		}

		public global::Unity.Networking.Transport.NetworkEvent.Type PopEventForConnection(int connectionId, out int offset, out int size, out int pipelineId)
		{
			offset = 0;
			size = 0;
			pipelineId = 0;
			if (connectionId < 0 || connectionId >= m_ConnectionEventHeadTail.Length / 2)
			{
				return global::Unity.Networking.Transport.NetworkEvent.Type.Empty;
			}
			int num = m_ConnectionEventHeadTail[connectionId * 2];
			if (num >= m_ConnectionEventHeadTail[connectionId * 2 + 1])
			{
				return global::Unity.Networking.Transport.NetworkEvent.Type.Empty;
			}
			m_ConnectionEventHeadTail[connectionId * 2] = num + 1;
			global::Unity.Networking.Transport.NetworkEvent networkEvent = m_ConnectionEventQ[connectionId * MaxEvents + num];
			pipelineId = networkEvent.pipelineId;
			if (networkEvent.type == global::Unity.Networking.Transport.NetworkEvent.Type.Data || networkEvent.type == global::Unity.Networking.Transport.NetworkEvent.Type.Disconnect)
			{
				offset = networkEvent.offset;
				size = networkEvent.size;
			}
			return networkEvent.type;
		}

		public int GetCountForConnection(int connectionId)
		{
			if (connectionId < 0 || connectionId >= m_ConnectionEventHeadTail.Length / 2)
			{
				return 0;
			}
			return m_ConnectionEventHeadTail[connectionId * 2 + 1] - m_ConnectionEventHeadTail[connectionId * 2];
		}

		public void PushEvent(global::Unity.Networking.Transport.NetworkEvent ev)
		{
			int num = MaxEvents;
			if (ev.connectionId >= m_ConnectionEventHeadTail.Length / 2)
			{
				int i = m_ConnectionEventHeadTail.Length;
				m_ConnectionEventHeadTail.ResizeUninitialized((ev.connectionId + 1) * 2);
				for (; i < m_ConnectionEventHeadTail.Length; i++)
				{
					m_ConnectionEventHeadTail[i] = 0;
				}
				m_ConnectionEventQ.ResizeUninitialized(m_ConnectionEventHeadTail.Length / 2 * num);
			}
			int num2 = m_ConnectionEventHeadTail[ev.connectionId * 2 + 1];
			if (num2 >= num)
			{
				int num3 = num;
				while (num2 >= num)
				{
					num *= 2;
				}
				int num4 = m_ConnectionEventHeadTail.Length / 2;
				m_ConnectionEventQ.ResizeUninitialized(num4 * num);
				for (int num5 = num4 - 1; num5 >= 0; num5--)
				{
					for (int num6 = m_ConnectionEventHeadTail[num5 * 2 + 1] - 1; num6 >= m_ConnectionEventHeadTail[num5 * 2]; num6--)
					{
						m_ConnectionEventQ[num5 * num + num6] = m_ConnectionEventQ[num5 * num3 + num6];
					}
				}
			}
			m_ConnectionEventQ[ev.connectionId * num + num2] = ev;
			m_ConnectionEventHeadTail[ev.connectionId * 2 + 1] = num2 + 1;
			m_MasterEventQ.Enqueue(new global::Unity.Networking.Transport.NetworkEventQueue.SubQueueItem
			{
				connection = ev.connectionId,
				idx = num2
			});
		}

		internal void Clear()
		{
			m_MasterEventQ.Clear();
			for (int i = 0; i < m_ConnectionEventHeadTail.Length; i++)
			{
				m_ConnectionEventHeadTail[i] = 0;
			}
		}

		public global::Unity.Networking.Transport.NetworkEventQueue.Concurrent ToConcurrent()
		{
			global::Unity.Networking.Transport.NetworkEventQueue.Concurrent result = default(global::Unity.Networking.Transport.NetworkEventQueue.Concurrent);
			result.m_ConnectionEventQ = m_ConnectionEventQ;
			result.m_ConnectionEventHeadTail = new global::Unity.Networking.Transport.NetworkEventQueue.Concurrent.ConcurrentConnectionQueue(m_ConnectionEventHeadTail);
			return result;
		}
	}
}
