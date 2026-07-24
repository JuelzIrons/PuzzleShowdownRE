namespace Unity.Networking.Transport
{
	internal struct NetworkDriverSender : global::System.IDisposable
	{
		[global::Unity.Burst.BurstCompile]
		private struct DequeuePacketsJob : global::Unity.Jobs.IJob
		{
			public global::Unity.Networking.Transport.PacketsQueue Queue;

			public global::Unity.Collections.NativeQueue<int> PendingSendQueue;

			public void Execute()
			{
				int count = PendingSendQueue.Count;
				for (int i = 0; i < count; i++)
				{
					Queue.EnqueuePacket(PendingSendQueue.Dequeue(), out var _);
				}
			}
		}

		internal struct Concurrent
		{
			[global::Unity.Collections.ReadOnly]
			internal global::Unity.Networking.Transport.PacketsQueue m_SendQueue;

			internal global::Unity.Collections.NativeQueue<int>.ParallelWriter m_PendingSendQueue;

			public int BeginSend(out global::Unity.Networking.Transport.NetworkInterfaceSendHandle sendHandle, uint packetSize = 0u)
			{
				sendHandle = default(global::Unity.Networking.Transport.NetworkInterfaceSendHandle);
				if (packetSize == 0)
				{
					packetSize = (uint)m_SendQueue.PayloadCapacity;
				}
				else if (packetSize > m_SendQueue.PayloadCapacity)
				{
					return -4;
				}
				if (m_SendQueue.TryAcquireBuffer(out var bufferIndex))
				{
					global::Unity.Networking.Transport.PacketBuffer packetBuffer = m_SendQueue.GetPacketBuffer(bufferIndex);
					sendHandle.id = bufferIndex;
					sendHandle.data = packetBuffer.Payload;
					sendHandle.capacity = (int)packetSize;
					return 0;
				}
				return -5;
			}

			public void AbortSend(ref global::Unity.Networking.Transport.NetworkInterfaceSendHandle sendHandle)
			{
				m_SendQueue.ReleaseBuffer(sendHandle.id);
			}

			public void EndSend(ref global::Unity.Networking.Transport.NetworkEndpoint destination, ref global::Unity.Networking.Transport.NetworkInterfaceSendHandle sendHandle, int padding = 0, global::Unity.Networking.Transport.ConnectionId connectionId = default(global::Unity.Networking.Transport.ConnectionId))
			{
				int id = sendHandle.id;
				m_SendQueue.GetMetadataRef(id) = new global::Unity.Networking.Transport.PacketMetadata
				{
					DataOffset = padding,
					DataLength = sendHandle.size,
					DataCapacity = sendHandle.capacity,
					Connection = connectionId
				};
				m_SendQueue.GetEndpointRef(id) = destination;
				m_PendingSendQueue.Enqueue(id);
			}
		}

		private global::Unity.Networking.Transport.PacketsQueue m_SendQueue;

		private global::Unity.Collections.NativeQueue<int> m_PendingSendQueue;

		internal global::Unity.Networking.Transport.PacketsQueue SendQueue => m_SendQueue;

		internal NetworkDriverSender(global::Unity.Networking.Transport.PacketsQueue sendQueue)
		{
			m_SendQueue = sendQueue;
			m_PendingSendQueue = new global::Unity.Collections.NativeQueue<int>(global::Unity.Collections.Allocator.Persistent);
		}

		public void Dispose()
		{
			if (m_PendingSendQueue.IsCreated)
			{
				m_PendingSendQueue.Dispose();
				m_SendQueue.Dispose();
			}
		}

		internal global::Unity.Jobs.JobHandle FlushPackets(global::Unity.Jobs.JobHandle dependency)
		{
			return global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Networking.Transport.NetworkDriverSender.DequeuePacketsJob
			{
				Queue = m_SendQueue,
				PendingSendQueue = m_PendingSendQueue
			}, dependency);
		}

		internal global::Unity.Networking.Transport.NetworkDriverSender.Concurrent ToConcurrent()
		{
			return new global::Unity.Networking.Transport.NetworkDriverSender.Concurrent
			{
				m_SendQueue = m_SendQueue,
				m_PendingSendQueue = m_PendingSendQueue.AsParallelWriter()
			};
		}
	}
}
