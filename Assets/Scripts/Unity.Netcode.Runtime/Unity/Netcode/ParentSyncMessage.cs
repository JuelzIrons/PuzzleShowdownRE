namespace Unity.Netcode
{
	internal struct ParentSyncMessage : global::Unity.Netcode.INetworkMessage
	{
		private const string k_Name = "ParentSyncMessage";

		public ulong NetworkObjectId;

		private const byte k_WorldPositionStays = 1;

		private const byte k_IsLatestParentSet = 2;

		private const byte k_RemoveParent = 4;

		private const byte k_AuthorityApplied = 8;

		public bool WorldPositionStays;

		public bool IsLatestParentSet;

		public ulong? LatestParent;

		public bool RemoveParent;

		public bool AuthorityApplied;

		public global::UnityEngine.Vector3 Position;

		public global::UnityEngine.Quaternion Rotation;

		public global::UnityEngine.Vector3 Scale;

		public int Version => 0;

		public void Serialize(global::Unity.Netcode.FastBufferWriter writer, int targetVersion)
		{
			byte b = 0;
			if (WorldPositionStays)
			{
				b |= 1;
			}
			if (IsLatestParentSet)
			{
				b |= 2;
			}
			if (RemoveParent)
			{
				b |= 4;
			}
			if (AuthorityApplied)
			{
				b |= 8;
			}
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, NetworkObjectId);
			writer.WriteByteSafe(b);
			if (!RemoveParent && IsLatestParentSet)
			{
				global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, LatestParent.Value);
			}
			writer.WriteValueSafe(in Position);
			writer.WriteValueSafe(in Rotation);
			writer.WriteValueSafe(in Scale);
		}

		public bool Deserialize(global::Unity.Netcode.FastBufferReader reader, ref global::Unity.Netcode.NetworkContext context, int receivedMessageVersion)
		{
			global::Unity.Netcode.NetworkManager networkManager = (global::Unity.Netcode.NetworkManager)context.SystemOwner;
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out NetworkObjectId);
			reader.ReadByteSafe(out var value);
			WorldPositionStays = (value & 1) != 0;
			IsLatestParentSet = (value & 2) != 0;
			RemoveParent = (value & 4) != 0;
			AuthorityApplied = (value & 8) != 0;
			if (!RemoveParent && IsLatestParentSet)
			{
				global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out ulong value2);
				LatestParent = value2;
			}
			reader.ReadValueSafe(out Position);
			reader.ReadValueSafe(out Rotation);
			reader.ReadValueSafe(out Scale);
			if (!networkManager.SpawnManager.SpawnedObjects.ContainsKey(NetworkObjectId))
			{
				networkManager.DeferredMessageManager.DeferMessage(global::Unity.Netcode.IDeferredNetworkMessageManager.TriggerType.OnSpawn, NetworkObjectId, reader, ref context, "ParentSyncMessage");
				return false;
			}
			if (LatestParent.HasValue && !networkManager.SpawnManager.SpawnedObjects.ContainsKey(LatestParent.Value))
			{
				networkManager.DeferredMessageManager.DeferMessage(global::Unity.Netcode.IDeferredNetworkMessageManager.TriggerType.OnSpawn, LatestParent.Value, reader, ref context, "ParentSyncMessage");
				return false;
			}
			return true;
		}

		public void Handle(ref global::Unity.Netcode.NetworkContext context)
		{
			global::Unity.Netcode.NetworkManager networkManager = (global::Unity.Netcode.NetworkManager)context.SystemOwner;
			global::Unity.Netcode.NetworkObject networkObject = networkManager.SpawnManager.SpawnedObjects[NetworkObjectId];
			networkObject.AuthorityAppliedParenting = AuthorityApplied || context.SenderId == networkObject.OwnerClientId || context.SenderId == 0;
			if (!networkObject.AuthorityAppliedParenting && networkManager.LogLevel <= global::Unity.Netcode.LogLevel.Normal)
			{
				global::Unity.Netcode.NetworkLog.LogWarningServer(string.Format("Client-{0} sent a ParentSyncMessage but is not the authority of {1}'s {2} component!", context.SenderId, networkObject.gameObject.name, "NetworkObject"));
			}
			networkObject.SetNetworkParenting(LatestParent, WorldPositionStays);
			networkObject.ApplyNetworkParenting(RemoveParent);
			if (networkObject.SyncOwnerTransformWhenParented || (!networkObject.SyncOwnerTransformWhenParented && !networkObject.IsOwner))
			{
				if (!WorldPositionStays)
				{
					networkObject.transform.SetLocalPositionAndRotation(Position, Rotation);
				}
				else
				{
					networkObject.transform.SetPositionAndRotation(Position, Rotation);
				}
				networkObject.transform.localScale = Scale;
			}
			if ((!networkManager.DistributedAuthorityMode || networkManager.CMBServiceConnection || !networkManager.DAHost) && (!networkObject.AllowOwnerToParent || context.SenderId != networkObject.OwnerClientId || !networkManager.IsServer))
			{
				return;
			}
			int num = 0;
			global::Unity.Netcode.ParentSyncMessage message = this;
			foreach (global::System.Collections.Generic.KeyValuePair<ulong, global::Unity.Netcode.NetworkClient> connectedClient in networkManager.ConnectedClients)
			{
				if (connectedClient.Value.ClientId != networkObject.OwnerClientId && connectedClient.Value.ClientId != networkManager.LocalClientId)
				{
					if (networkObject.IsNetworkVisibleTo(connectedClient.Value.ClientId))
					{
						num = networkManager.ConnectionManager.SendMessage(ref message, global::Unity.Netcode.NetworkDelivery.ReliableSequenced, connectedClient.Value.ClientId);
						networkManager.NetworkMetrics.TrackOwnershipChangeSent(connectedClient.Key, networkObject, num);
					}
					else
					{
						global::UnityEngine.Debug.Log($"[DAHost][ParentingProxy] Client-{connectedClient.Value.ClientId} has no visibility to {networkObject.name}!");
					}
				}
			}
		}
	}
}
