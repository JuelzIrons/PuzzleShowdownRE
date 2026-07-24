namespace Unity.Netcode
{
	internal struct ConnectionApprovedMessage : global::Unity.Netcode.INetworkMessage
	{
		private const int k_AddCMBServiceConfig = 2;

		private const int k_VersionAddClientIds = 1;

		public ulong OwnerClientId;

		public int NetworkTick;

		public global::Unity.Netcode.ServiceConfig ServiceConfig;

		public bool IsRestoredSession;

		public ulong CurrentSessionOwner;

		public bool IsDistributedAuthority;

		public global::System.Collections.Generic.HashSet<global::Unity.Netcode.NetworkObject> SpawnedObjectsList;

		private global::Unity.Netcode.FastBufferReader m_ReceivedSceneObjectData;

		public global::Unity.Collections.NativeArray<global::Unity.Netcode.MessageVersionData> MessageVersions;

		public global::Unity.Collections.NativeArray<ulong> ConnectedClientIds;

		private int m_ReceiveMessageVersion;

		public int Version => 2;

		private ulong GetSessionOwner()
		{
			if (m_ReceiveMessageVersion >= 2)
			{
				return ServiceConfig.CurrentSessionOwner;
			}
			return CurrentSessionOwner;
		}

		private bool GetIsSessionRestor()
		{
			if (m_ReceiveMessageVersion >= 2)
			{
				return ServiceConfig.IsRestoredSession;
			}
			return IsRestoredSession;
		}

		public void Serialize(global::Unity.Netcode.FastBufferWriter writer, int targetVersion)
		{
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, MessageVersions.Length);
			foreach (global::Unity.Netcode.MessageVersionData messageVersion in MessageVersions)
			{
				messageVersion.Serialize(writer);
			}
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, OwnerClientId);
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, NetworkTick);
			if (IsDistributedAuthority)
			{
				if (targetVersion >= 2)
				{
					ServiceConfig.IsRestoredSession = false;
					ServiceConfig.CurrentSessionOwner = CurrentSessionOwner;
					writer.WriteNetworkSerializable(in ServiceConfig);
				}
				else
				{
					writer.WriteValueSafe(in IsRestoredSession, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
					global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, CurrentSessionOwner);
				}
			}
			if (targetVersion >= 1)
			{
				writer.WriteValueSafe(ConnectedClientIds);
			}
			uint value = 0u;
			if (SpawnedObjectsList != null)
			{
				int position = writer.Position;
				writer.Seek(writer.Position + global::Unity.Netcode.FastBufferWriter.GetWriteSize(in value, default(global::Unity.Netcode.FastBufferWriter.ForStructs)));
				foreach (global::Unity.Netcode.NetworkObject spawnedObjects in SpawnedObjectsList)
				{
					if (spawnedObjects.SpawnWithObservers && (spawnedObjects.CheckObjectVisibility == null || spawnedObjects.CheckObjectVisibility(OwnerClientId)))
					{
						spawnedObjects.AddObserver(OwnerClientId);
						spawnedObjects.Serialize(OwnerClientId, IsDistributedAuthority).Serialize(writer);
						value++;
					}
				}
				writer.Seek(position);
				writer.WriteValueSafe(in value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				writer.Seek(writer.Length);
			}
			else
			{
				writer.WriteValueSafe(in value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			}
		}

		public bool Deserialize(global::Unity.Netcode.FastBufferReader reader, ref global::Unity.Netcode.NetworkContext context, int receivedMessageVersion)
		{
			global::Unity.Netcode.NetworkManager networkManager = (global::Unity.Netcode.NetworkManager)context.SystemOwner;
			if (!networkManager.IsClient)
			{
				return false;
			}
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out int value);
			global::Unity.Collections.NativeArray<uint> serverMessageOrder = new global::Unity.Collections.NativeArray<uint>(value, global::Unity.Collections.Allocator.Temp);
			for (int i = 0; i < value; i++)
			{
				global::Unity.Netcode.MessageVersionData messageVersionData = default(global::Unity.Netcode.MessageVersionData);
				messageVersionData.Deserialize(reader);
				networkManager.ConnectionManager.MessageManager.SetVersion(context.SenderId, messageVersionData.Hash, messageVersionData.Version);
				serverMessageOrder[i] = messageVersionData.Hash;
				if (networkManager.ConnectionManager.MessageManager.GetMessageForHash(messageVersionData.Hash) == typeof(global::Unity.Netcode.ConnectionApprovedMessage))
				{
					receivedMessageVersion = messageVersionData.Version;
				}
			}
			networkManager.ConnectionManager.MessageManager.SetServerMessageOrder(serverMessageOrder);
			serverMessageOrder.Dispose();
			m_ReceiveMessageVersion = receivedMessageVersion;
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out OwnerClientId);
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out NetworkTick);
			if (networkManager.DistributedAuthorityMode)
			{
				if (receivedMessageVersion >= 2)
				{
					reader.ReadNetworkSerializable(out ServiceConfig);
					networkManager.SessionConfig = new global::Unity.Netcode.SessionConfig(ServiceConfig);
				}
				else
				{
					reader.ReadValueSafe(out IsRestoredSession, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
					global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out CurrentSessionOwner);
					networkManager.SessionConfig = new global::Unity.Netcode.SessionConfig(0u);
				}
			}
			if (receivedMessageVersion >= 1)
			{
				reader.ReadValueSafe(out ConnectedClientIds, global::Unity.Collections.Allocator.TempJob, default(global::Unity.Netcode.FastBufferWriter.ForGeneric));
			}
			else
			{
				ConnectedClientIds = new global::Unity.Collections.NativeArray<ulong>(0, global::Unity.Collections.Allocator.TempJob);
			}
			m_ReceivedSceneObjectData = reader;
			return true;
		}

		public void Handle(ref global::Unity.Netcode.NetworkContext context)
		{
			global::Unity.Netcode.NetworkManager networkManager = (global::Unity.Netcode.NetworkManager)context.SystemOwner;
			if (networkManager.CMBServiceConnection && networkManager.LocalClient.IsSessionOwner && networkManager.NetworkConfig.EnableSceneManagement)
			{
				if (networkManager.LocalClientId != OwnerClientId)
				{
					if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Developer)
					{
						global::Unity.Netcode.NetworkLog.LogInfo($"[Session Owner] Received connection approved for Client-{OwnerClientId}! Synchronizing...");
					}
					networkManager.SceneManager.SynchronizeNetworkObjects(OwnerClientId);
				}
				else
				{
					global::Unity.Netcode.NetworkLog.LogWarning($"[Client-{OwnerClientId}] Receiving duplicate connection approved. Client is already connected!");
				}
				ConnectedClientIds.Dispose();
				return;
			}
			if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Developer)
			{
				global::Unity.Netcode.NetworkLog.LogInfo($"[Client-{OwnerClientId}] Connection approved! Synchronizing...");
			}
			networkManager.LocalClientId = OwnerClientId;
			networkManager.MessageManager.SetLocalClientId(networkManager.LocalClientId);
			networkManager.NetworkMetrics.SetConnectionId(networkManager.LocalClientId);
			if (networkManager.DistributedAuthorityMode)
			{
				networkManager.SetSessionOwner(GetSessionOwner());
				if (networkManager.LocalClient.IsSessionOwner)
				{
					if (networkManager.NetworkConfig.EnableSceneManagement)
					{
						networkManager.SceneManager.InitializeScenesLoaded();
					}
					if (networkManager.NetworkConfig.ConnectionApproval && networkManager.LogLevel <= global::Unity.Netcode.LogLevel.Developer)
					{
						global::Unity.Netcode.NetworkLog.LogWarning("ConnectionApproval is enabled but is not supported when using a distributed authority topology. The ConnectionApprovalCallback will not be invoked.");
					}
				}
			}
			global::Unity.Netcode.NetworkTime networkTime = new global::Unity.Netcode.NetworkTime(networkManager.NetworkTickSystem.TickRate, NetworkTick);
			networkManager.NetworkTimeSystem.Reset(networkTime.Time, 0.15000000596046448);
			networkManager.NetworkTickSystem.Reset(networkManager.NetworkTimeSystem.LocalTime, networkManager.NetworkTimeSystem.ServerTime);
			networkManager.ConnectionManager.LocalClient.SetRole(isServer: false, isClient: true, networkManager);
			networkManager.ConnectionManager.LocalClient.IsApproved = true;
			networkManager.ConnectionManager.LocalClient.ClientId = OwnerClientId;
			networkManager.ConnectionManager.StopClientApprovalCoroutine();
			foreach (ulong connectedClientId in ConnectedClientIds)
			{
				if (!networkManager.ConnectionManager.ConnectedClientIds.Contains(connectedClientId) || !networkManager.ConnectionManager.ConnectedClients.ContainsKey(connectedClientId))
				{
					networkManager.ConnectionManager.AddClient(connectedClientId);
				}
			}
			ConnectedClientIds.Dispose();
			if (!networkManager.NetworkConfig.EnableSceneManagement)
			{
				networkManager.IsConnectedClient = true;
				if (networkManager.DistributedAuthorityMode && networkManager.LocalClient.IsSessionOwner)
				{
					networkManager.SpawnManager.ServerSpawnSceneObjectsOnStartSweep();
				}
				else
				{
					networkManager.SpawnManager.DestroySceneObjects();
				}
				m_ReceivedSceneObjectData.ReadValueSafe(out uint value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				for (ushort num = 0; num < value; num++)
				{
					global::Unity.Netcode.NetworkObject.SerializedObject serializedObject = default(global::Unity.Netcode.NetworkObject.SerializedObject);
					serializedObject.Deserialize(m_ReceivedSceneObjectData);
					global::Unity.Netcode.NetworkObject.Deserialize(in serializedObject, m_ReceivedSceneObjectData, networkManager);
				}
				if (networkManager.AutoSpawnPlayerPrefabClientSide)
				{
					networkManager.ConnectionManager.CreateAndSpawnPlayer(OwnerClientId);
				}
				if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Developer)
				{
					global::Unity.Netcode.NetworkLog.LogInfo($"[Client-{OwnerClientId}][Scene Management Disabled] Synchronization complete!");
				}
				networkManager.ConnectionManager.InvokeOnClientConnectedCallback(OwnerClientId);
				networkManager.SpawnManager.NotifyNetworkObjectsSynchronized();
			}
			else
			{
				if (!networkManager.DistributedAuthorityMode || !networkManager.CMBServiceConnection || !networkManager.LocalClient.IsSessionOwner || !networkManager.NetworkConfig.EnableSceneManagement)
				{
					return;
				}
				networkManager.IsConnectedClient = networkManager.ConnectionManager.LocalClient.IsApproved;
				networkManager.SceneManager.IsRestoringSession = GetIsSessionRestor();
				if (!networkManager.SceneManager.IsRestoringSession)
				{
					networkManager.SpawnManager.ServerSpawnSceneObjectsOnStartSweep();
					networkManager.SceneManager.SynchronizeNetworkObjects(0uL, synchronizingService: true);
					if (networkManager.AutoSpawnPlayerPrefabClientSide)
					{
						networkManager.ConnectionManager.CreateAndSpawnPlayer(OwnerClientId);
					}
					networkManager.SpawnManager.NotifyNetworkObjectsSynchronized();
					networkManager.ConnectionManager.InvokeOnClientConnectedCallback(OwnerClientId);
				}
			}
		}
	}
}
