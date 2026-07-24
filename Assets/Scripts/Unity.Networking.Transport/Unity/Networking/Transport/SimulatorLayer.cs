namespace Unity.Networking.Transport
{
	internal struct SimulatorLayer : global::Unity.Networking.Transport.INetworkLayer, global::System.IDisposable
	{
		private struct PendingPacket
		{
			public unsafe fixed byte Packet[1472];

			public global::Unity.Networking.Transport.ConnectionId ConnectionId;

			public global::Unity.Networking.Transport.NetworkEndpoint Endpoint;

			public long PendingUntil;

			public int Length;

			public int Offset;
		}

		[global::Unity.Burst.BurstCompile]
		private struct SimulatorReceiveJob : global::Unity.Jobs.IJob
		{
			public global::Unity.Networking.Transport.PacketsQueue ReceiveQueue;

			public global::Unity.Collections.NativeReference<global::Unity.Mathematics.Random> RNG;

			public float PacketLoss;

			public int LimitMTU;

			public int DownStreamPadding;

			public void Execute()
			{
				global::Unity.Mathematics.Random value = RNG.Value;
				int count = ReceiveQueue.Count;
				for (int i = 0; i < count; i++)
				{
					global::Unity.Networking.Transport.PacketProcessor packetProcessor = ReceiveQueue[i];
					bool num = PacketLoss > 0f && value.NextFloat(100f) < PacketLoss;
					bool flag = LimitMTU > 0 && packetProcessor.Length + DownStreamPadding > LimitMTU;
					if (num || flag)
					{
						packetProcessor.Drop();
					}
				}
				RNG.Value = value;
			}
		}

		[global::Unity.Burst.BurstCompile]
		private struct SimulatorSendJob : global::Unity.Jobs.IJob
		{
			public global::Unity.Networking.Transport.PacketsQueue SendQueue;

			public global::Unity.Collections.NativeList<global::Unity.Networking.Transport.SimulatorLayer.PendingPacket> PendingPackets;

			public global::Unity.Collections.NativeReference<global::Unity.Mathematics.Random> RNG;

			public long Time;

			public float PacketLoss;

			public uint DelayMS;

			public uint JitterMS;

			public float DuplicatePercent;

			public void Execute()
			{
				global::Unity.Mathematics.Random random = RNG.Value;
				ProcessPackets(ref random);
				EnqueuePendingPackets();
				RNG.Value = random;
			}

			private unsafe void ProcessPackets(ref global::Unity.Mathematics.Random random)
			{
				int count = SendQueue.Count;
				for (int i = 0; i < count; i++)
				{
					global::Unity.Networking.Transport.PacketProcessor packetProcessor = SendQueue[i];
					if (PacketLoss > 0f && random.NextFloat(100f) < PacketLoss)
					{
						packetProcessor.Drop();
					}
					else if (DelayMS != 0 || JitterMS != 0 || DuplicatePercent != 0f)
					{
						long num = DelayMS + global::Unity.Mathematics.math.max(0, random.NextInt((int)(2 * JitterMS)) - (int)JitterMS);
						global::Unity.Networking.Transport.SimulatorLayer.PendingPacket value = new global::Unity.Networking.Transport.SimulatorLayer.PendingPacket
						{
							PendingUntil = Time + num,
							Length = packetProcessor.Length,
							Offset = packetProcessor.Offset,
							ConnectionId = packetProcessor.ConnectionRef,
							Endpoint = packetProcessor.EndpointRef
						};
						packetProcessor.CopyPayload(value.Packet, packetProcessor.Length);
						PendingPackets.Add(in value);
						if (random.NextFloat(100f) < DuplicatePercent)
						{
							value.PendingUntil = Time + DelayMS + random.NextUInt(2 * JitterMS) - JitterMS;
							PendingPackets.Add(in value);
						}
						packetProcessor.Drop();
					}
				}
			}

			private unsafe void EnqueuePendingPackets()
			{
				int num = 0;
				int count = SendQueue.Count;
				for (int i = 0; i < PendingPackets.Length; i++)
				{
					global::Unity.Networking.Transport.PacketProcessor packetProcessor = default(global::Unity.Networking.Transport.PacketProcessor);
					global::Unity.Networking.Transport.SimulatorLayer.PendingPacket pendingPacket = PendingPackets[i];
					if (Time >= pendingPacket.PendingUntil)
					{
						if (num < count && SendQueue[num].Length == 0)
						{
							packetProcessor = SendQueue[num];
							num++;
						}
						else if (!SendQueue.EnqueuePacket(out packetProcessor))
						{
							break;
						}
						packetProcessor.EndpointRef = pendingPacket.Endpoint;
						packetProcessor.ConnectionRef = pendingPacket.ConnectionId;
						packetProcessor.SetUnsafeMetadata(0, pendingPacket.Offset);
						packetProcessor.AppendToPayload(pendingPacket.Packet, pendingPacket.Length);
						PendingPackets.RemoveAtSwapBack(i--);
					}
				}
			}
		}

		private global::Unity.Collections.NativeReference<global::Unity.Networking.Transport.NetworkSimulatorParameter> m_Parameters;

		private global::Unity.Collections.NativeReference<global::Unity.Mathematics.Random> m_RNG;

		private global::Unity.Collections.NativeList<global::Unity.Networking.Transport.SimulatorLayer.PendingPacket> m_SendDelayedPackets;

		private int m_DownStreamPadding;

		public global::Unity.Networking.Transport.NetworkSimulatorParameter Parameters
		{
			private get
			{
				return m_Parameters.Value;
			}
			set
			{
				m_Parameters.Value = value;
			}
		}

		public int Initialize(ref global::Unity.Networking.Transport.NetworkSettings settings, ref global::Unity.Networking.Transport.ConnectionList connectionList, ref int packetPadding)
		{
			settings.TryGet<global::Unity.Networking.Transport.NetworkSimulatorParameter>(out var parameter);
			m_Parameters = new global::Unity.Collections.NativeReference<global::Unity.Networking.Transport.NetworkSimulatorParameter>(parameter, global::Unity.Collections.Allocator.Persistent);
			m_SendDelayedPackets = new global::Unity.Collections.NativeList<global::Unity.Networking.Transport.SimulatorLayer.PendingPacket>(global::Unity.Collections.Allocator.Persistent);
			uint seed = (uint)((parameter.RandomSeed != 0) ? parameter.RandomSeed : global::Unity.Networking.Transport.Utilities.TimerHelpers.GetTicks());
			m_RNG = new global::Unity.Collections.NativeReference<global::Unity.Mathematics.Random>(new global::Unity.Mathematics.Random(seed), global::Unity.Collections.Allocator.Persistent);
			m_DownStreamPadding = packetPadding;
			return 0;
		}

		public void Dispose()
		{
			m_Parameters.Dispose();
			m_SendDelayedPackets.Dispose();
			m_RNG.Dispose();
		}

		public global::Unity.Jobs.JobHandle ScheduleReceive(ref global::Unity.Networking.Transport.ReceiveJobArguments arguments, global::Unity.Jobs.JobHandle dependency)
		{
			if (ShouldSkipSimulatorInReceive())
			{
				return dependency;
			}
			return global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Networking.Transport.SimulatorLayer.SimulatorReceiveJob
			{
				ReceiveQueue = arguments.ReceiveQueue,
				RNG = m_RNG,
				PacketLoss = Parameters.ReceivePacketLossPercent,
				LimitMTU = (int)Parameters.ReceiveMtu,
				DownStreamPadding = m_DownStreamPadding
			}, dependency);
		}

		public global::Unity.Jobs.JobHandle ScheduleSend(ref global::Unity.Networking.Transport.SendJobArguments arguments, global::Unity.Jobs.JobHandle dependency)
		{
			if (ShouldSkipSimulatorInSend())
			{
				return dependency;
			}
			return global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Networking.Transport.SimulatorLayer.SimulatorSendJob
			{
				SendQueue = arguments.SendQueue,
				PendingPackets = m_SendDelayedPackets,
				RNG = m_RNG,
				Time = arguments.Time,
				PacketLoss = Parameters.SendPacketLossPercent,
				DelayMS = Parameters.SendDelayMS,
				JitterMS = Parameters.SendJitterMS,
				DuplicatePercent = Parameters.SendDuplicatePercent
			}, dependency);
		}

		private bool ShouldSkipSimulatorInReceive()
		{
			if (Parameters.ReceivePacketLossPercent == 0f)
			{
				return Parameters.ReceiveMtu == 0f;
			}
			return false;
		}

		private bool ShouldSkipSimulatorInSend()
		{
			if (Parameters.SendPacketLossPercent == 0f && Parameters.SendDelayMS == 0 && Parameters.SendJitterMS == 0)
			{
				return Parameters.SendDuplicatePercent == 0f;
			}
			return false;
		}
	}
}
