namespace Unity.Services.Multiplayer
{
	internal class NetworkInfo
	{
		internal global::Unity.Services.Multiplayer.NetworkType Network { get; private set; }

		internal global::Unity.Services.Multiplayer.PublishIPAddress PublishAddress { get; private set; }

		internal global::Unity.Services.Multiplayer.ListenIPAddress ListenAddress { get; private set; }

		internal global::Unity.Services.Multiplayer.RelayNetworkOptions RelayOptions { get; private set; }

		private NetworkInfo()
		{
		}

		public static global::Unity.Services.Multiplayer.NetworkInfo BuildDirect(global::Unity.Services.Multiplayer.DirectNetworkOptions networkOptions)
		{
			return new global::Unity.Services.Multiplayer.NetworkInfo
			{
				Network = global::Unity.Services.Multiplayer.NetworkType.Direct,
				PublishAddress = networkOptions.PublishIp.WithPort(networkOptions.Port),
				ListenAddress = networkOptions.ListenIp.WithPort(networkOptions.Port)
			};
		}

		public static global::Unity.Services.Multiplayer.NetworkInfo BuildRelay(global::Unity.Services.Multiplayer.RelayNetworkOptions relayOptions)
		{
			return new global::Unity.Services.Multiplayer.NetworkInfo
			{
				Network = global::Unity.Services.Multiplayer.NetworkType.Relay,
				RelayOptions = relayOptions
			};
		}

		public static global::Unity.Services.Multiplayer.NetworkInfo BuildDistributed(global::Unity.Services.Multiplayer.RelayNetworkOptions relayOptions)
		{
			return new global::Unity.Services.Multiplayer.NetworkInfo
			{
				Network = global::Unity.Services.Multiplayer.NetworkType.DistributedAuthority,
				RelayOptions = relayOptions
			};
		}

		public void OverrideDirectNetworkInfo(global::Unity.Services.Multiplayer.PublishIPAddress publishAddress, ushort port, global::Unity.Services.Multiplayer.ListenIPAddress listenAddress)
		{
			PublishAddress = publishAddress.WithPort(port);
			ListenAddress = listenAddress.WithPort(port);
		}
	}
}
