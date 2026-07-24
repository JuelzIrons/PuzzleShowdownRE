namespace Unity.Netcode
{
	internal struct ConnectionRequestMessage : global::Unity.Netcode.INetworkMessage
	{
		internal const string InvalidSessionVersionMessage = "The client version is not compatible with the session version.";

		private const int k_SendClientConfigToService = 1;

		public ulong ConfigHash;

		public bool DistributedAuthority;

		public global::Unity.Netcode.ClientConfig ClientConfig;

		public byte[] ConnectionData;

		public bool ShouldSendConnectionData;

		public global::Unity.Collections.NativeArray<global::Unity.Netcode.MessageVersionData> MessageVersions;

		public int Version => 1;

		public void Serialize(global::Unity.Netcode.FastBufferWriter writer, int targetVersion)
		{
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, MessageVersions.Length);
			foreach (global::Unity.Netcode.MessageVersionData messageVersion in MessageVersions)
			{
				messageVersion.Serialize(writer);
			}
			if (DistributedAuthority)
			{
				writer.WriteNetworkSerializable(in ClientConfig);
			}
			if (ShouldSendConnectionData)
			{
				writer.WriteValueSafe(in ConfigHash, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				writer.WriteValueSafe(ConnectionData, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			}
			else
			{
				writer.WriteValueSafe(in ConfigHash, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			}
		}

		public bool Deserialize(global::Unity.Netcode.FastBufferReader reader, ref global::Unity.Netcode.NetworkContext context, int receivedMessageVersion)
		{
			global::Unity.Netcode.NetworkManager networkManager = (global::Unity.Netcode.NetworkManager)context.SystemOwner;
			if (!networkManager.IsServer)
			{
				return false;
			}
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out int value);
			for (int i = 0; i < value; i++)
			{
				global::Unity.Netcode.MessageVersionData messageVersionData = default(global::Unity.Netcode.MessageVersionData);
				messageVersionData.Deserialize(reader);
				networkManager.ConnectionManager.MessageManager.SetVersion(context.SenderId, messageVersionData.Hash, messageVersionData.Version);
				if (networkManager.ConnectionManager.MessageManager.GetMessageForHash(messageVersionData.Hash) == typeof(global::Unity.Netcode.ConnectionRequestMessage))
				{
					receivedMessageVersion = messageVersionData.Version;
				}
			}
			if (networkManager.DAHost)
			{
				reader.ReadNetworkSerializable(out ClientConfig);
			}
			if (networkManager.NetworkConfig.ConnectionApproval)
			{
				if (!reader.TryBeginRead(global::Unity.Netcode.FastBufferWriter.GetWriteSize(in ConfigHash, default(global::Unity.Netcode.FastBufferWriter.ForStructs)) + global::Unity.Netcode.FastBufferWriter.GetWriteSize<int>()))
				{
					if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Normal)
					{
						global::Unity.Netcode.NetworkLog.LogWarning("Incomplete connection request message given config - possible NetworkConfig mismatch.");
					}
					networkManager.DisconnectClient(context.SenderId);
					return false;
				}
				reader.ReadValue(out ConfigHash, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				if (!networkManager.NetworkConfig.CompareConfig(ConfigHash))
				{
					if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Normal)
					{
						global::Unity.Netcode.NetworkLog.LogWarning("NetworkConfig mismatch. The configuration between the server and client does not match");
					}
					networkManager.DisconnectClient(context.SenderId);
					return false;
				}
				reader.ReadValueSafe(out ConnectionData, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			}
			else
			{
				if (!reader.TryBeginRead(global::Unity.Netcode.FastBufferWriter.GetWriteSize(in ConfigHash, default(global::Unity.Netcode.FastBufferWriter.ForStructs))))
				{
					if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Normal)
					{
						global::Unity.Netcode.NetworkLog.LogWarning("Incomplete connection request message.");
					}
					networkManager.DisconnectClient(context.SenderId);
					return false;
				}
				reader.ReadValue(out ConfigHash, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				if (!networkManager.NetworkConfig.CompareConfig(ConfigHash))
				{
					if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Normal)
					{
						global::Unity.Netcode.NetworkLog.LogWarning("NetworkConfig mismatch. The configuration between the server and client does not match");
					}
					networkManager.DisconnectClient(context.SenderId);
					return false;
				}
			}
			return true;
		}

		public void Handle(ref global::Unity.Netcode.NetworkContext context)
		{
			global::Unity.Netcode.NetworkManager networkManager = (global::Unity.Netcode.NetworkManager)context.SystemOwner;
			ulong senderId = context.SenderId;
			if (networkManager.DAHost && ClientConfig.RemoteClientSessionVersion < networkManager.SessionConfig.SessionVersion)
			{
				networkManager.ConnectionManager.DisconnectClient(senderId, "The client version is not compatible with the session version.");
				return;
			}
			if (networkManager.ConnectionManager.PendingClients.TryGetValue(senderId, out var value))
			{
				value.ConnectionState = global::Unity.Netcode.PendingClient.State.PendingApproval;
			}
			if (networkManager.NetworkConfig.ConnectionApproval)
			{
				global::Unity.Netcode.ConnectionRequestMessage connectionRequestMessage = this;
				networkManager.ConnectionManager.ApproveConnection(ref connectionRequestMessage, ref context);
				return;
			}
			bool createPlayerObject = networkManager.NetworkConfig.PlayerPrefab != null;
			if (networkManager.DistributedAuthorityMode && networkManager.AutoSpawnPlayerPrefabClientSide)
			{
				createPlayerObject = false;
			}
			networkManager.ConnectionManager.HandleConnectionApproval(senderId, createPlayerObject);
		}
	}
}
