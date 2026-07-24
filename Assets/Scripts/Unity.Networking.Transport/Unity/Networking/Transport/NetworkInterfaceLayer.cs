namespace Unity.Networking.Transport
{
	internal struct NetworkInterfaceLayer<N> : global::Unity.Networking.Transport.INetworkLayer, global::System.IDisposable where N : unmanaged, global::Unity.Networking.Transport.INetworkInterface
	{
		internal N m_NetworkInterface;

		public NetworkInterfaceLayer(N networkInterface)
		{
			m_NetworkInterface = networkInterface;
		}

		public unsafe int Initialize(ref global::Unity.Networking.Transport.NetworkSettings settings, ref global::Unity.Networking.Transport.ConnectionList connectionList, ref int packetPadding)
		{
			int num = m_NetworkInterface.Initialize(ref settings, ref packetPadding);
			if (num != 0)
			{
				return num;
			}
			if (global::Unity.Burst.BurstRuntime.GetHashCode64<N>() == global::Unity.Burst.BurstRuntime.GetHashCode64<global::Unity.Networking.Transport.WebSocketNetworkInterface>())
			{
				fixed (N* networkInterface = &m_NetworkInterface)
				{
					void* ptr = networkInterface;
					connectionList = ((global::Unity.Networking.Transport.WebSocketNetworkInterface*)ptr)->CreateConnectionList();
				}
			}
			else if (global::Unity.Burst.BurstRuntime.GetHashCode64<N>() == global::Unity.Burst.BurstRuntime.GetHashCode64<global::Unity.Networking.Transport.TCPNetworkInterface>())
			{
				fixed (N* networkInterface = &m_NetworkInterface)
				{
					void* ptr2 = networkInterface;
					connectionList = ((global::Unity.Networking.Transport.TCPNetworkInterface*)ptr2)->CreateConnectionList();
				}
			}
			return 0;
		}

		public int Bind(ref global::Unity.Networking.Transport.NetworkEndpoint endpoint)
		{
			return m_NetworkInterface.Bind(endpoint);
		}

		public int Listen()
		{
			return m_NetworkInterface.Listen();
		}

		public global::Unity.Networking.Transport.NetworkEndpoint GetLocalEndpoint()
		{
			return m_NetworkInterface.LocalEndpoint;
		}

		public void Dispose()
		{
			m_NetworkInterface.Dispose();
		}

		public global::Unity.Jobs.JobHandle ScheduleReceive(ref global::Unity.Networking.Transport.ReceiveJobArguments arguments, global::Unity.Jobs.JobHandle dependency)
		{
			return m_NetworkInterface.ScheduleReceive(ref arguments, dependency);
		}

		public global::Unity.Jobs.JobHandle ScheduleSend(ref global::Unity.Networking.Transport.SendJobArguments arguments, global::Unity.Jobs.JobHandle dependency)
		{
			return m_NetworkInterface.ScheduleSend(ref arguments, dependency);
		}
	}
}
