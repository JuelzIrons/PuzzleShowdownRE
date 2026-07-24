namespace Unity.Networking.Transport
{
	[global::Unity.Burst.BurstCompile]
	public struct WebSocketNetworkInterface : global::Unity.Networking.Transport.INetworkInterface, global::System.IDisposable
	{
		private global::Unity.Networking.Transport.TCPNetworkInterface tcp;

		public global::Unity.Networking.Transport.NetworkEndpoint LocalEndpoint => tcp.LocalEndpoint;

		public void Dispose()
		{
			tcp.Dispose();
		}

		internal global::Unity.Networking.Transport.ConnectionList CreateConnectionList()
		{
			return tcp.CreateConnectionList();
		}

		public int Initialize(ref global::Unity.Networking.Transport.NetworkSettings settings, ref int packetPadding)
		{
			return tcp.Initialize(ref settings, ref packetPadding);
		}

		public int Bind(global::Unity.Networking.Transport.NetworkEndpoint endpoint)
		{
			return tcp.Bind(endpoint);
		}

		public int Listen()
		{
			return tcp.Listen();
		}

		public global::Unity.Jobs.JobHandle ScheduleReceive(ref global::Unity.Networking.Transport.ReceiveJobArguments arguments, global::Unity.Jobs.JobHandle dep)
		{
			return tcp.ScheduleReceive(ref arguments, dep);
		}

		public global::Unity.Jobs.JobHandle ScheduleSend(ref global::Unity.Networking.Transport.SendJobArguments arguments, global::Unity.Jobs.JobHandle dep)
		{
			return tcp.ScheduleSend(ref arguments, dep);
		}
	}
}
