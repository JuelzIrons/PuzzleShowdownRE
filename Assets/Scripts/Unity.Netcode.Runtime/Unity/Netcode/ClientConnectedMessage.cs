namespace Unity.Netcode
{
	internal struct ClientConnectedMessage : global::Unity.Netcode.INetworkMessage, global::Unity.Netcode.INetworkSerializeByMemcpy
	{
		public ulong ClientId;

		public bool ShouldSynchronize;

		public int Version => 0;

		public void Serialize(global::Unity.Netcode.FastBufferWriter writer, int targetVersion)
		{
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, ClientId);
			writer.WriteValueSafe(in ShouldSynchronize, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
		}

		public bool Deserialize(global::Unity.Netcode.FastBufferReader reader, ref global::Unity.Netcode.NetworkContext context, int receivedMessageVersion)
		{
			if (!((global::Unity.Netcode.NetworkManager)context.SystemOwner).IsClient)
			{
				return false;
			}
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out ClientId);
			reader.ReadValueSafe(out ShouldSynchronize, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			return true;
		}

		public void Handle(ref global::Unity.Netcode.NetworkContext context)
		{
			global::Unity.Netcode.NetworkManager networkManager = (global::Unity.Netcode.NetworkManager)context.SystemOwner;
			if (ShouldSynchronize && networkManager.NetworkConfig.EnableSceneManagement && networkManager.DistributedAuthorityMode && !networkManager.CMBServiceConnection && networkManager.LocalClient.IsSessionOwner)
			{
				networkManager.SceneManager.SynchronizeNetworkObjects(ClientId);
			}
			else
			{
				networkManager.ConnectionManager.AddClient(ClientId);
			}
			if (!networkManager.ConnectionManager.ConnectedClientIds.Contains(ClientId))
			{
				networkManager.ConnectionManager.ConnectedClientIds.Add(ClientId);
			}
			if (networkManager.IsConnectedClient)
			{
				networkManager.ConnectionManager.InvokeOnPeerConnectedCallback(ClientId);
			}
			if (networkManager.DistributedAuthorityMode && networkManager.CMBServiceConnection && !networkManager.NetworkConfig.EnableSceneManagement && ClientId != networkManager.LocalClientId)
			{
				networkManager.SpawnManager.SynchronizeObjectsToNewlyJoinedClient(ClientId);
				networkManager.RedistributeToClients = true;
				networkManager.ClientsToRedistribute.Add(ClientId);
			}
		}
	}
}
