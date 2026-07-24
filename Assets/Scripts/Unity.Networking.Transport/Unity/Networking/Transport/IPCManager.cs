namespace Unity.Networking.Transport
{
	internal struct IPCManager
	{
		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit)]
		internal struct IPCData
		{
			[global::System.Runtime.InteropServices.FieldOffset(0)]
			public ushort fromPort;

			[global::System.Runtime.InteropServices.FieldOffset(2)]
			public int length;

			[global::System.Runtime.InteropServices.FieldOffset(6)]
			public unsafe fixed byte data[1472];
		}

		public static global::Unity.Networking.Transport.IPCManager Instance;

		private global::Unity.Networking.Transport.Utilities.NativeMultiQueue<global::Unity.Networking.Transport.IPCManager.IPCData> m_IPCQueue;

		private global::Unity.Collections.NativeParallelHashMap<ushort, int> m_IPCChannels;

		internal static global::Unity.Jobs.JobHandle ManagerAccessHandle;

		private int m_RefCount;

		public bool IsCreated => m_IPCQueue.IsCreated;

		public void AddRef()
		{
			if (m_RefCount == 0)
			{
				m_IPCQueue = new global::Unity.Networking.Transport.Utilities.NativeMultiQueue<global::Unity.Networking.Transport.IPCManager.IPCData>(128);
				m_IPCChannels = new global::Unity.Collections.NativeParallelHashMap<ushort, int>(64, global::Unity.Collections.Allocator.Persistent);
			}
			m_RefCount++;
		}

		public void Release()
		{
			m_RefCount--;
			if (m_RefCount == 0)
			{
				CompleteManagerAccess();
				m_IPCQueue.Dispose();
				m_IPCChannels.Dispose();
			}
		}

		internal unsafe void Update(global::Unity.Networking.Transport.NetworkEndpoint local, ref global::Unity.Networking.Transport.PacketsQueue sendQueue)
		{
			for (int i = 0; i < sendQueue.Count; i++)
			{
				global::Unity.Networking.Transport.PacketProcessor packetProcessor = sendQueue[i];
				if (packetProcessor.Length == 0)
				{
					continue;
				}
				if (!GetChannelByEndpoint(ref packetProcessor.EndpointRef, out var channel))
				{
					if (packetProcessor.EndpointRef.Port == 0)
					{
						continue;
					}
					global::Unity.Networking.Transport.NetworkEndpoint endpoint = CreateEndpoint(packetProcessor.EndpointRef.Port);
					GetChannelByEndpoint(ref endpoint, out channel);
				}
				global::Unity.Networking.Transport.IPCManager.IPCData value = default(global::Unity.Networking.Transport.IPCManager.IPCData);
				packetProcessor.CopyPayload(value.data, 1472);
				value.length = packetProcessor.Length;
				value.fromPort = local.Port;
				m_IPCQueue.Enqueue(channel, value);
			}
		}

		[global::Unity.Burst.BurstDiscard]
		private void CompleteManagerAccess()
		{
			if (!global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.IsExecutingJob)
			{
				ManagerAccessHandle.Complete();
			}
		}

		public global::Unity.Networking.Transport.NetworkEndpoint CreateEndpoint(ushort port)
		{
			CompleteManagerAccess();
			int item = 0;
			if (port == 0)
			{
				while (item == 0)
				{
					port = global::Unity.Networking.Transport.Utilities.RandomHelpers.GetRandomUShort();
					if (!m_IPCChannels.TryGetValue(port, out var _))
					{
						item = m_IPCChannels.Count() + 1;
						m_IPCChannels.TryAdd(port, item);
					}
				}
			}
			else if (!m_IPCChannels.TryGetValue(port, out item))
			{
				item = m_IPCChannels.Count() + 1;
				m_IPCChannels.TryAdd(port, item);
			}
			return global::Unity.Networking.Transport.NetworkEndpoint.LoopbackIpv4.WithPort(port);
		}

		public bool GetChannelByEndpoint(ref global::Unity.Networking.Transport.NetworkEndpoint endpoint, out int channel)
		{
			if (!endpoint.IsLoopback)
			{
				channel = -1;
				return false;
			}
			return m_IPCChannels.TryGetValue(endpoint.Port, out channel);
		}

		public unsafe int PeekNext(global::Unity.Networking.Transport.NetworkEndpoint local, void* slice, out int length, out global::Unity.Networking.Transport.NetworkEndpoint from)
		{
			CompleteManagerAccess();
			from = default(global::Unity.Networking.Transport.NetworkEndpoint);
			length = 0;
			if (!GetChannelByEndpoint(ref local, out var channel))
			{
				return 0;
			}
			if (m_IPCQueue.Peek(channel, out var value))
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(slice, value.data, value.length);
				length = value.length;
			}
			from = global::Unity.Networking.Transport.NetworkEndpoint.LoopbackIpv4.WithPort(value.fromPort);
			return length;
		}

		public bool HasDataAvailable(global::Unity.Networking.Transport.NetworkEndpoint localEndpoint)
		{
			CompleteManagerAccess();
			if (!GetChannelByEndpoint(ref localEndpoint, out var channel))
			{
				return false;
			}
			global::Unity.Networking.Transport.IPCManager.IPCData value;
			return m_IPCQueue.Peek(channel, out value);
		}

		public unsafe int ReceiveMessageEx(global::Unity.Networking.Transport.NetworkEndpoint local, void* payloadData, int payloadLen, ref global::Unity.Networking.Transport.NetworkEndpoint remote)
		{
			if (!GetChannelByEndpoint(ref local, out var channel))
			{
				return 0;
			}
			if (!m_IPCQueue.Dequeue(channel, out var value))
			{
				return 0;
			}
			remote = global::Unity.Networking.Transport.NetworkEndpoint.LoopbackIpv4.WithPort(value.fromPort);
			if (value.length > payloadLen)
			{
				return 0;
			}
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(payloadData, value.data, value.length);
			return value.length;
		}
	}
}
