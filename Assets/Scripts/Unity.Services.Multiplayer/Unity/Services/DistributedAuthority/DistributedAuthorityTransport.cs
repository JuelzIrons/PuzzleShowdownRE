namespace Unity.Services.DistributedAuthority
{
	internal class DistributedAuthorityTransport : global::Unity.Netcode.Transports.UTP.UnityTransport
	{
		internal const string DAWarning = "Detected DAHost mode. This is an unsupported configuration. Falling back to ClientServer. Please start Distributed Authority mode as client.";

		public global::Unity.Collections.NativeArray<byte> ConnectPayload = new global::Unity.Collections.NativeArray<byte>(0, global::Unity.Collections.Allocator.Temp);

		protected override global::Unity.Networking.Transport.NetworkConnection Connect(global::Unity.Networking.Transport.NetworkEndpoint serverEndpoint)
		{
			return m_Driver.Connect(serverEndpoint, ConnectPayload);
		}

		protected override global::Unity.Netcode.NetworkTopologyTypes OnCurrentTopology()
		{
			if (m_NetworkManager.DAHost || (m_NetworkManager.DistributedAuthorityMode && !m_NetworkManager.CMBServiceConnection))
			{
				global::Unity.Services.Multiplayer.Logger.LogWarning("Detected DAHost mode. This is an unsupported configuration. Falling back to ClientServer. Please start Distributed Authority mode as client.");
				return global::Unity.Netcode.NetworkTopologyTypes.ClientServer;
			}
			return base.OnCurrentTopology();
		}
	}
}
