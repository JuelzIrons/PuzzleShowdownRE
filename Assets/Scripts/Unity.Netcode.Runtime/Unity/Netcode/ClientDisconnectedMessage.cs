namespace Unity.Netcode
{
	internal struct ClientDisconnectedMessage : global::Unity.Netcode.INetworkMessage, global::Unity.Netcode.INetworkSerializeByMemcpy
	{
		public ulong ClientId;

		public int Version => 0;

		public void Serialize(global::Unity.Netcode.FastBufferWriter writer, int targetVersion)
		{
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, ClientId);
		}

		public bool Deserialize(global::Unity.Netcode.FastBufferReader reader, ref global::Unity.Netcode.NetworkContext context, int receivedMessageVersion)
		{
			if (!((global::Unity.Netcode.NetworkManager)context.SystemOwner).IsClient)
			{
				return false;
			}
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out ClientId);
			return true;
		}

		public void Handle(ref global::Unity.Netcode.NetworkContext context)
		{
			global::Unity.Netcode.NetworkManager networkManager = (global::Unity.Netcode.NetworkManager)context.SystemOwner;
			if (networkManager.DistributedAuthorityMode && networkManager.CMBServiceConnection && networkManager.LocalClient.IsSessionOwner && networkManager.NetworkConfig.EnableSceneManagement)
			{
				networkManager.SceneManager.ClientConnectionQueue.Remove(ClientId);
			}
			networkManager.ConnectionManager.RemoveClient(ClientId);
			networkManager.ConnectionManager.ConnectedClientIds.Remove(ClientId);
			if (networkManager.IsConnectedClient)
			{
				networkManager.ConnectionManager.InvokeOnPeerDisconnectedCallback(ClientId);
			}
		}
	}
}
