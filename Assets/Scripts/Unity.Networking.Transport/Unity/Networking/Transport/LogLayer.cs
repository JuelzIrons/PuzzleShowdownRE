namespace Unity.Networking.Transport
{
	internal struct LogLayer : global::Unity.Networking.Transport.INetworkLayer, global::System.IDisposable
	{
		[global::Unity.Burst.BurstCompile]
		private struct LogJob : global::Unity.Jobs.IJob
		{
			public global::Unity.Collections.FixedString64Bytes Label;

			public global::Unity.Networking.Transport.PacketsQueue Queue;

			public void Execute()
			{
				int count = Queue.Count;
				if (count == 0)
				{
					return;
				}
				new global::Unity.Collections.LowLevel.Unsafe.UnsafeText(4096, global::Unity.Collections.Allocator.Temp);
				for (int i = 0; i < count; i++)
				{
					global::Unity.Networking.Transport.PacketProcessor packetProcessor = Queue[i];
					if (packetProcessor.Length > 0)
					{
						global::Unity.Collections.FixedString4096Bytes fs = new global::Unity.Collections.FixedString4096Bytes(in Label);
						global::Unity.Collections.FixedStringMethods.Append<global::Unity.Collections.FixedString4096Bytes, global::Unity.Collections.FixedString128Bytes>(ref fs, global::Unity.Collections.FixedString.Format(" {0} bytes [Endpoint: {1}]: ", packetProcessor.Length, packetProcessor.EndpointRef.ToFixedString512Bytes()));
						if (AppendPayload(ref fs, ref packetProcessor))
						{
							global::UnityEngine.Debug.Log(fs);
							continue;
						}
						global::UnityEngine.Debug.Log(fs);
						global::UnityEngine.Debug.Log("Message truncated");
					}
				}
				static bool AppendPayload(ref global::Unity.Collections.FixedString4096Bytes str, ref global::Unity.Networking.Transport.PacketProcessor reference)
				{
					int length = reference.Length;
					for (int j = 0; j < length; j++)
					{
						byte payloadDataRef = reference.GetPayloadDataRef<byte>(j);
						if (global::Unity.Collections.FixedStringMethods.Append<global::Unity.Collections.FixedString4096Bytes, global::Unity.Collections.FixedString32Bytes>(ref str, (global::Unity.Collections.FixedString32Bytes)$"{payloadDataRef:x2}") == global::Unity.Collections.FormatError.Overflow)
						{
							return false;
						}
						if (global::Unity.Collections.FixedStringMethods.Append(ref str, ' ') == global::Unity.Collections.FormatError.Overflow)
						{
							return false;
						}
					}
					return true;
				}
			}
		}

		private global::Unity.Collections.FixedString32Bytes m_DriverIdentifier;

		public void Dispose()
		{
		}

		public int Initialize(ref global::Unity.Networking.Transport.NetworkSettings settings, ref global::Unity.Networking.Transport.ConnectionList connectionList, ref int packetPadding)
		{
			if (settings.TryGet<global::Unity.Networking.Transport.Logging.LoggingParameter>(out var parameter))
			{
				m_DriverIdentifier = parameter.DriverName;
			}
			else
			{
				m_DriverIdentifier = "unidentified";
			}
			return 0;
		}

		public global::Unity.Jobs.JobHandle ScheduleReceive(ref global::Unity.Networking.Transport.ReceiveJobArguments arguments, global::Unity.Jobs.JobHandle dependency)
		{
			return global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Networking.Transport.LogLayer.LogJob
			{
				Label = $"[{m_DriverIdentifier}] Received",
				Queue = arguments.ReceiveQueue
			}, dependency);
		}

		public global::Unity.Jobs.JobHandle ScheduleSend(ref global::Unity.Networking.Transport.SendJobArguments arguments, global::Unity.Jobs.JobHandle dependency)
		{
			return global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Networking.Transport.LogLayer.LogJob
			{
				Label = $"[{m_DriverIdentifier}] Sent",
				Queue = arguments.SendQueue
			}, dependency);
		}
	}
}
