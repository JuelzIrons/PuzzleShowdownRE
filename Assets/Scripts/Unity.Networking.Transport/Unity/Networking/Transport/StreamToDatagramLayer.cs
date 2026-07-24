namespace Unity.Networking.Transport
{
	internal struct StreamToDatagramLayer : global::Unity.Networking.Transport.INetworkLayer, global::System.IDisposable
	{
		public struct StreamToDatagramLayerPacketBuffer
		{
			public const int Capacity = 2944;

			public unsafe fixed byte Data[2944];

			public int Length;
		}

		private struct ConnectionData
		{
			public global::Unity.Networking.Transport.StreamToDatagramLayer.StreamToDatagramLayerPacketBuffer RecvBuffer;

			public int ReceiveIgnore;
		}

		[global::Unity.Burst.BurstCompile]
		private struct SendJob : global::Unity.Jobs.IJob
		{
			public global::Unity.Networking.Transport.PacketsQueue SendQueue;

			private static ushort HostToNetwork(ushort value)
			{
				return (ushort)(global::System.BitConverter.IsLittleEndian ? (((value & 0xFF) << 8) | ((value >> 8) & 0xFF)) : value);
			}

			public void Execute()
			{
				int count = SendQueue.Count;
				for (int i = 0; i < count; i++)
				{
					global::Unity.Networking.Transport.PacketProcessor packetProcessor = SendQueue[i];
					if (packetProcessor.Length == 0 || (ushort)packetProcessor.Length > SendQueue.PayloadCapacity - 2)
					{
						packetProcessor.Drop();
						continue;
					}
					ushort value = HostToNetwork((ushort)packetProcessor.Length);
					packetProcessor.PrependToPayload(value);
				}
			}
		}

		[global::Unity.Burst.BurstCompile]
		private struct ReceiveJob : global::Unity.Jobs.IJob
		{
			public global::Unity.Networking.Transport.PacketsQueue ReceiveQueue;

			public global::Unity.Networking.Transport.ConnectionDataMap<global::Unity.Networking.Transport.StreamToDatagramLayer.ConnectionData> ConnectionMap;

			public unsafe void Execute()
			{
				int count = ReceiveQueue.Count;
				for (int i = 0; i < count; i++)
				{
					global::Unity.Networking.Transport.PacketProcessor packetProcessor = ReceiveQueue[i];
					if (packetProcessor.Length == 0 || packetProcessor.Length > ReceiveQueue.PayloadCapacity)
					{
						packetProcessor.Drop();
						continue;
					}
					global::Unity.Networking.Transport.ConnectionId connectionRef = packetProcessor.ConnectionRef;
					global::Unity.Networking.Transport.StreamToDatagramLayer.ConnectionData value = ConnectionMap[connectionRef];
					int num = global::System.Math.Max(0, global::System.Math.Min(value.ReceiveIgnore, packetProcessor.Length));
					if (num < packetProcessor.Length)
					{
						int num2 = packetProcessor.Length - num;
						global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(value.RecvBuffer.Data + value.RecvBuffer.Length, (byte*)packetProcessor.GetUnsafePayloadPtr() + packetProcessor.Offset + num, num2);
						value.RecvBuffer.Length += num2;
					}
					value.ReceiveIgnore -= num;
					int num3 = value.RecvBuffer.Length;
					int num4 = 0;
					while (num3 >= 2)
					{
						ushort num5 = (ushort)(((value.RecvBuffer.Data[num4] & 0xFF) << 8) + (value.RecvBuffer.Data[num4 + 1] & 0xFF));
						num3 -= 2;
						if (num5 > ReceiveQueue.PayloadCapacity - 2)
						{
							value.ReceiveIgnore = global::System.Math.Max(0, num5 - num3);
							num3 = global::System.Math.Max(0, num3 - num5);
							num4 += 2 + num5;
						}
						else if (num5 == 0)
						{
							num4 += 2;
						}
						else if (num5 <= num3)
						{
							num4 += 2;
							if (ReceiveQueue.EnqueuePacket(out var packetProcessor2))
							{
								packetProcessor2.ConnectionRef = packetProcessor.ConnectionRef;
								packetProcessor2.EndpointRef = packetProcessor.EndpointRef;
								packetProcessor2.AppendToPayload(value.RecvBuffer.Data + num4, num5);
							}
							num3 -= num5;
							num4 += num5;
						}
					}
					if (num4 > 0 && num4 < value.RecvBuffer.Length)
					{
						global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemMove(value.RecvBuffer.Data, value.RecvBuffer.Data + num4, value.RecvBuffer.Length - num4);
					}
					value.RecvBuffer.Length -= num4;
					ConnectionMap[connectionRef] = value;
					packetProcessor.Drop();
				}
			}
		}

		private const int k_HeaderSize = 2;

		private global::Unity.Networking.Transport.ConnectionDataMap<global::Unity.Networking.Transport.StreamToDatagramLayer.ConnectionData> m_ConnectionMap;

		public int Initialize(ref global::Unity.Networking.Transport.NetworkSettings settings, ref global::Unity.Networking.Transport.ConnectionList connectionList, ref int packetPadding)
		{
			packetPadding += 2;
			m_ConnectionMap = new global::Unity.Networking.Transport.ConnectionDataMap<global::Unity.Networking.Transport.StreamToDatagramLayer.ConnectionData>(1, default(global::Unity.Networking.Transport.StreamToDatagramLayer.ConnectionData), global::Unity.Collections.Allocator.Persistent);
			return 0;
		}

		public void Dispose()
		{
			m_ConnectionMap.Dispose();
		}

		public global::Unity.Jobs.JobHandle ScheduleSend(ref global::Unity.Networking.Transport.SendJobArguments arguments, global::Unity.Jobs.JobHandle dep)
		{
			return global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Networking.Transport.StreamToDatagramLayer.SendJob
			{
				SendQueue = arguments.SendQueue
			}, dep);
		}

		public global::Unity.Jobs.JobHandle ScheduleReceive(ref global::Unity.Networking.Transport.ReceiveJobArguments arguments, global::Unity.Jobs.JobHandle dep)
		{
			return global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Networking.Transport.StreamToDatagramLayer.ReceiveJob
			{
				ReceiveQueue = arguments.ReceiveQueue,
				ConnectionMap = m_ConnectionMap
			}, dep);
		}
	}
}
