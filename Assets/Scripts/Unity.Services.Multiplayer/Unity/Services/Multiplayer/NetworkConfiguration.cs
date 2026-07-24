namespace Unity.Services.Multiplayer
{
	public class NetworkConfiguration
	{
		public global::Unity.Services.Multiplayer.NetworkRole Role { get; }

		public global::Unity.Services.Multiplayer.NetworkType Type { get; }

		public global::Unity.Networking.Transport.NetworkEndpoint DirectNetworkPublishAddress { get; private set; }

		public global::Unity.Networking.Transport.NetworkEndpoint DirectNetworkListenAddress { get; private set; }

		public global::Unity.Networking.Transport.Relay.RelayServerData RelayServerData { get; }

		public global::Unity.Networking.Transport.Relay.RelayServerData RelayClientData { get; }

		internal global::Unity.Collections.NativeArray<byte> DistributedAuthorityConnectionPayload { get; } = new global::Unity.Collections.NativeArray<byte>(0, global::Unity.Collections.Allocator.Temp);

		internal NetworkConfiguration(global::Unity.Services.Multiplayer.NetworkRole networkRole, global::Unity.Services.Multiplayer.PublishIPAddress directNetworkPublishIp, global::Unity.Services.Multiplayer.ListenIPAddress directNetworkListenIp)
		{
			Role = networkRole;
			Type = global::Unity.Services.Multiplayer.NetworkType.Direct;
			DirectNetworkPublishAddress = directNetworkPublishIp.UtpNetworkEndpoint;
			DirectNetworkListenAddress = directNetworkListenIp.UtpNetworkEndpoint;
		}

		internal NetworkConfiguration(global::Unity.Services.Multiplayer.NetworkRole networkRole, global::Unity.Services.Multiplayer.NetworkType typeType, global::Unity.Networking.Transport.Relay.RelayServerData relayServerData, global::Unity.Networking.Transport.Relay.RelayServerData? relayClientData)
		{
			Role = networkRole;
			Type = typeType;
			RelayServerData = relayServerData;
			if (relayClientData.HasValue)
			{
				RelayClientData = relayClientData.Value;
			}
		}

		internal NetworkConfiguration(global::Unity.Services.Multiplayer.NetworkRole networkRole, global::Unity.Services.Multiplayer.NetworkType typeType, global::Unity.Networking.Transport.Relay.RelayServerData relayServerData, global::Unity.Collections.NativeArray<byte> distributedAuthorityConnectionPayload, global::Unity.Networking.Transport.Relay.RelayServerData? relayClientData = null)
		{
			Role = networkRole;
			Type = typeType;
			RelayServerData = relayServerData;
			DistributedAuthorityConnectionPayload = distributedAuthorityConnectionPayload;
			if (relayClientData.HasValue)
			{
				RelayClientData = relayClientData.Value;
			}
		}

		internal NetworkConfiguration(global::Unity.Services.Multiplayer.NetworkMetadata metadata)
		{
			Type = metadata.Network;
			DirectNetworkPublishAddress = metadata.Endpoint.UtpEndpoint;
		}

		public void UpdatePublishPort(ushort port)
		{
			DirectNetworkListenAddress = DirectNetworkListenAddress.WithPort(port);
			DirectNetworkPublishAddress = DirectNetworkPublishAddress.WithPort(port);
		}
	}
}
