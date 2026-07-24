namespace Unity.Networking.Transport
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	internal struct StreamSegmentationLayer : global::Unity.Networking.Transport.INetworkLayer, global::System.IDisposable
	{
		[global::Unity.Burst.BurstCompile]
		private struct SendJob : global::Unity.Jobs.IJob
		{
			public global::Unity.Networking.Transport.PacketsQueue SendQueue;

			public unsafe void Execute()
			{
				int count = SendQueue.Count;
				for (int i = 0; i < count; i++)
				{
					global::Unity.Networking.Transport.PacketProcessor packetProcessor = SendQueue[i];
					global::Unity.Networking.Transport.PacketProcessor packetProcessor2;
					while (packetProcessor.Length > 0 && SendQueue.EnqueuePacket(out packetProcessor2))
					{
						packetProcessor2.ConnectionRef = packetProcessor.ConnectionRef;
						packetProcessor2.EndpointRef = packetProcessor.EndpointRef;
						packetProcessor2.SetUnsafeMetadata(0);
						int num = global::System.Math.Min(packetProcessor.Length, 294);
						packetProcessor2.AppendToPayload((byte*)packetProcessor.GetUnsafePayloadPtr() + packetProcessor.Offset, num);
						packetProcessor.SetUnsafeMetadata(packetProcessor.Length - num, packetProcessor.Offset + num);
					}
					packetProcessor.Drop();
				}
			}
		}

		private const int k_SegmentSize = 294;

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void Warn(string msg)
		{
			global::UnityEngine.Debug.LogWarning(msg);
		}

		public int Initialize(ref global::Unity.Networking.Transport.NetworkSettings settings, ref global::Unity.Networking.Transport.ConnectionList connectionList, ref int packetPadding)
		{
			return 0;
		}

		public void Dispose()
		{
		}

		public global::Unity.Jobs.JobHandle ScheduleSend(ref global::Unity.Networking.Transport.SendJobArguments arguments, global::Unity.Jobs.JobHandle dep)
		{
			return global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Networking.Transport.StreamSegmentationLayer.SendJob
			{
				SendQueue = arguments.SendQueue
			}, dep);
		}

		public global::Unity.Jobs.JobHandle ScheduleReceive(ref global::Unity.Networking.Transport.ReceiveJobArguments arguments, global::Unity.Jobs.JobHandle dep)
		{
			return dep;
		}
	}
}
