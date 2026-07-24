namespace Unity.Networking.Transport
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	[global::System.Obsolete("BaselibNetworkInterface has been deprecated. Use UDPNetworkInterface instead (UnityUpgradable) -> UDPNetworkInterface")]
	public struct BaselibNetworkInterface : global::Unity.Networking.Transport.INetworkInterface, global::System.IDisposable
	{
		public global::Unity.Networking.Transport.NetworkEndpoint LocalEndpoint
		{
			get
			{
				throw new global::System.NotImplementedException();
			}
		}

		public int Initialize(ref global::Unity.Networking.Transport.NetworkSettings settings, ref int packetPadding)
		{
			throw new global::System.NotImplementedException();
		}

		public global::Unity.Jobs.JobHandle ScheduleReceive(ref global::Unity.Networking.Transport.ReceiveJobArguments arguments, global::Unity.Jobs.JobHandle dep)
		{
			throw new global::System.NotImplementedException();
		}

		public global::Unity.Jobs.JobHandle ScheduleSend(ref global::Unity.Networking.Transport.SendJobArguments arguments, global::Unity.Jobs.JobHandle dep)
		{
			throw new global::System.NotImplementedException();
		}

		public int Bind(global::Unity.Networking.Transport.NetworkEndpoint endpoint)
		{
			throw new global::System.NotImplementedException();
		}

		public int Listen()
		{
			throw new global::System.NotImplementedException();
		}

		public void Dispose()
		{
			throw new global::System.NotImplementedException();
		}
	}
}
