namespace Unity.Networking.Transport
{
	public struct PacketsQueue : global::System.IDisposable
	{
		[global::Unity.Burst.BurstCompile]
		private struct FillBufferPointers : global::Unity.Jobs.IJob
		{
			public global::Unity.Collections.NativeArray<global::Unity.Networking.Transport.PacketBuffer> Buffers;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			public global::System.IntPtr Payloads;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			public global::System.IntPtr Endpoints;

			public int PayloadAndMetadataSize;

			public int EndpointSize;

			public unsafe void Execute()
			{
				byte* ptr = (byte*)(void*)Payloads;
				byte* ptr2 = (byte*)(void*)Endpoints;
				int num = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Networking.Transport.PacketMetadata>();
				for (int i = 0; i < Buffers.Length; i++)
				{
					Buffers[i] = new global::Unity.Networking.Transport.PacketBuffer
					{
						Metadata = new global::System.IntPtr(ptr + i * PayloadAndMetadataSize),
						Payload = new global::System.IntPtr(ptr + i * PayloadAndMetadataSize + num),
						Endpoint = new global::System.IntPtr(ptr2 + i * EndpointSize)
					};
				}
			}
		}

		private global::Unity.Collections.NativeList<int> m_Queue;

		private global::Unity.Networking.Transport.Utilities.LowLevel.Unsafe.UnsafeAtomicFreeList m_FreeList;

		[global::Unity.Collections.NativeDisableParallelForRestriction]
		private global::Unity.Collections.NativeArray<global::Unity.Networking.Transport.PacketBuffer> m_Buffers;

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		private global::System.IntPtr m_PayloadsPtr;

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		private global::System.IntPtr m_EndpointsPtr;

		private int m_Capacity;

		private int m_MetadataSize;

		private int m_PayloadSize;

		private int m_EndpointSize;

		private int m_DefaultDataOffset;

		internal int BuffersInUse => m_FreeList.InUse;

		internal int BuffersAvailable => m_Capacity - m_FreeList.InUse;

		internal int PayloadCapacity => m_PayloadSize;

		internal int EndpointCapacity => m_EndpointSize;

		public int Capacity => m_Capacity;

		public int Count => m_Queue.Length;

		public bool IsCreated => m_Buffers.IsCreated;

		public global::Unity.Networking.Transport.PacketProcessor this[int packetIndex]
		{
			get
			{
				int bufferIndex = m_Queue[packetIndex];
				return GetPacketProcessor(bufferIndex);
			}
		}

		internal unsafe ref global::Unity.Networking.Transport.PacketMetadata GetMetadataRef(int bufferIndex)
		{
			return ref *(global::Unity.Networking.Transport.PacketMetadata*)(void*)m_Buffers[bufferIndex].Metadata;
		}

		internal unsafe ref global::Unity.Networking.Transport.NetworkEndpoint GetEndpointRef(int bufferIndex)
		{
			return ref *(global::Unity.Networking.Transport.NetworkEndpoint*)(void*)m_Buffers[bufferIndex].Endpoint;
		}

		internal global::Unity.Networking.Transport.PacketBuffer GetPacketBuffer(int bufferIndex)
		{
			return m_Buffers[bufferIndex];
		}

		private static void Initialize(out global::Unity.Networking.Transport.PacketsQueue packetsQueue, int metadataSize, int payloadSize, int endpointSize, int capacity)
		{
			packetsQueue.m_Capacity = capacity;
			packetsQueue.m_MetadataSize = metadataSize;
			packetsQueue.m_PayloadSize = payloadSize;
			packetsQueue.m_EndpointSize = endpointSize;
			packetsQueue.m_DefaultDataOffset = 0;
			packetsQueue.m_PayloadsPtr = global::System.IntPtr.Zero;
			packetsQueue.m_EndpointsPtr = global::System.IntPtr.Zero;
			packetsQueue.m_FreeList = new global::Unity.Networking.Transport.Utilities.LowLevel.Unsafe.UnsafeAtomicFreeList(capacity, global::Unity.Collections.Allocator.Persistent);
			packetsQueue.m_Buffers = new global::Unity.Collections.NativeArray<global::Unity.Networking.Transport.PacketBuffer>(capacity, global::Unity.Collections.Allocator.Persistent);
			packetsQueue.m_Queue = new global::Unity.Collections.NativeList<int>(capacity, global::Unity.Collections.Allocator.Persistent);
		}

		internal unsafe PacketsQueue(int capacity, int payloadSize = 1472)
		{
			int num = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Networking.Transport.PacketMetadata>();
			int num2 = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Networking.Transport.NetworkEndpoint>();
			int size = payloadSize + num;
			Initialize(out this, num, payloadSize, num2, capacity);
			size = AddPaddingToAlign(size, 64);
			num2 = AddPaddingToAlign(num2, 64);
			m_PayloadsPtr = new global::System.IntPtr(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(capacity * size, 64, global::Unity.Collections.Allocator.Persistent));
			m_EndpointsPtr = new global::System.IntPtr(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(capacity * num2, 64, global::Unity.Collections.Allocator.Persistent));
			global::Unity.Jobs.IJobExtensions.Run(new global::Unity.Networking.Transport.PacketsQueue.FillBufferPointers
			{
				Buffers = m_Buffers,
				Payloads = m_PayloadsPtr,
				Endpoints = m_EndpointsPtr,
				PayloadAndMetadataSize = size,
				EndpointSize = num2
			});
		}

		internal PacketsQueue(int metadataSize, int payloadSize, int endpointSize, int capacity, global::Unity.Collections.NativeArray<global::Unity.Networking.Transport.PacketBuffer> buffers)
		{
			Initialize(out this, metadataSize, payloadSize, endpointSize, capacity);
			buffers.CopyTo(m_Buffers);
		}

		private static int AddPaddingToAlign(int size, int alignment)
		{
			return alignment * (int)global::Unity.Mathematics.math.ceil((float)size / (float)alignment);
		}

		public unsafe void Dispose()
		{
			if (IsCreated)
			{
				if (m_PayloadsPtr != global::System.IntPtr.Zero)
				{
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Free((void*)m_PayloadsPtr, global::Unity.Collections.Allocator.Persistent);
					m_PayloadsPtr = global::System.IntPtr.Zero;
				}
				if (m_EndpointsPtr != global::System.IntPtr.Zero)
				{
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Free((void*)m_EndpointsPtr, global::Unity.Collections.Allocator.Persistent);
					m_EndpointsPtr = global::System.IntPtr.Zero;
				}
				m_FreeList.Dispose();
				m_Buffers.Dispose();
				m_Queue.Dispose();
			}
		}

		internal global::Unity.Networking.Transport.PacketProcessor GetPacketProcessor(int bufferIndex)
		{
			return new global::Unity.Networking.Transport.PacketProcessor(m_Buffers[bufferIndex]);
		}

		internal int GetPacketBufferIndex(int packetIndex)
		{
			return m_Queue[packetIndex];
		}

		public bool EnqueuePacket(out global::Unity.Networking.Transport.PacketProcessor packetProcessor)
		{
			if (TryAcquireBuffer(out var bufferIndex))
			{
				GetMetadataRef(bufferIndex) = new global::Unity.Networking.Transport.PacketMetadata
				{
					DataOffset = m_DefaultDataOffset
				};
				EnqueueAndGetProcessor(bufferIndex, out packetProcessor);
				return true;
			}
			packetProcessor = default(global::Unity.Networking.Transport.PacketProcessor);
			return false;
		}

		internal bool EnqueuePacket(int bufferIndex, out global::Unity.Networking.Transport.PacketProcessor packetProcessor)
		{
			EnqueueAndGetProcessor(bufferIndex, out packetProcessor);
			return true;
		}

		private void EnqueueAndGetProcessor(int bufferIndex, out global::Unity.Networking.Transport.PacketProcessor packetProcessor)
		{
			m_Queue.Add(in bufferIndex);
			packetProcessor = GetPacketProcessor(bufferIndex);
			GetMetadataRef(bufferIndex).DataCapacity = PayloadCapacity;
		}

		public unsafe void EnqueuePackets(ref global::Unity.Networking.Transport.PacketsQueue originQueue)
		{
			int count = originQueue.Count;
			for (int i = 0; i < count; i++)
			{
				global::Unity.Networking.Transport.PacketProcessor packetProcessor = originQueue[i];
				if (packetProcessor.Length != 0)
				{
					if (!EnqueuePacket(out var packetProcessor2))
					{
						break;
					}
					packetProcessor.CopyPayload((byte*)packetProcessor2.GetUnsafePayloadPtr() + packetProcessor2.Offset, packetProcessor.Length);
					packetProcessor2.SetUnsafeMetadata(packetProcessor.Length, packetProcessor2.Offset);
					packetProcessor2.ConnectionRef = packetProcessor.ConnectionRef;
					packetProcessor2.EndpointRef = packetProcessor.EndpointRef;
				}
			}
		}

		internal int DequeuePacketNoRelease(int packetIndex)
		{
			int result = m_Queue[packetIndex];
			m_Queue[packetIndex] = -1;
			return result;
		}

		private void DropPacket(int packetIndex)
		{
			int num = DequeuePacketNoRelease(packetIndex);
			if (num >= 0)
			{
				GetMetadataRef(num) = default(global::Unity.Networking.Transport.PacketMetadata);
				ReleaseBuffer(num);
			}
		}

		public void Clear()
		{
			for (int i = 0; i < m_Queue.Length; i++)
			{
				DropPacket(i);
			}
			m_Queue.Clear();
		}

		internal void UnsafeResetAcquisitionState()
		{
			m_FreeList.Reset();
		}

		internal bool TryAcquireBuffer(out int bufferIndex)
		{
			bufferIndex = m_FreeList.Pop();
			return bufferIndex >= 0;
		}

		internal void ReleaseBuffer(int bufferIndex)
		{
			m_FreeList.Push(bufferIndex);
		}

		internal void SetDefaultDataOffset(int offset)
		{
			m_DefaultDataOffset = offset;
		}
	}
}
