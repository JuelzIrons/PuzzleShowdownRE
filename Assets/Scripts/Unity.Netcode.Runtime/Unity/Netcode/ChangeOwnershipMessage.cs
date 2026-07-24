namespace Unity.Netcode
{
	internal struct ChangeOwnershipMessage : global::Unity.Netcode.INetworkMessage, global::Unity.Netcode.INetworkSerializeByMemcpy
	{
		internal enum ChangeType : byte
		{
			OwnershipChanging = 1,
			OwnershipFlagsUpdate = 2,
			RequestOwnership = 4,
			RequestApproved = 8,
			RequestDenied = 0x10
		}

		private const string k_Name = "ChangeOwnershipMessage";

		public ulong NetworkObjectId;

		public ulong OwnerClientId;

		internal ulong RequestClientId;

		internal int ClientIdCount;

		internal ulong[] ClientIds;

		internal bool DistributedAuthorityMode;

		internal ushort OwnershipFlags;

		internal byte OwnershipRequestResponseStatus;

		internal global::Unity.Netcode.ChangeOwnershipMessage.ChangeType ChangeMessageType;

		public int Version => 0;

		public void Serialize(global::Unity.Netcode.FastBufferWriter writer, int targetVersion)
		{
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, NetworkObjectId);
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, OwnerClientId);
			if (!DistributedAuthorityMode)
			{
				return;
			}
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, ClientIdCount);
			if (ClientIdCount > 0)
			{
				if (ClientIdCount != ClientIds.Length)
				{
					throw new global::System.Exception(string.Format("[{0}] ClientIdCount is {1} but the ClientIds length is {2}!", "ChangeOwnershipMessage", ClientIdCount, ClientIds.Length));
				}
				ulong[] clientIds = ClientIds;
				foreach (ulong value in clientIds)
				{
					global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, value);
				}
			}
			writer.WriteValueSafe(in ChangeMessageType, default(global::Unity.Netcode.FastBufferWriter.ForEnums));
			if (ChangeMessageType == global::Unity.Netcode.ChangeOwnershipMessage.ChangeType.OwnershipFlagsUpdate || ChangeMessageType == global::Unity.Netcode.ChangeOwnershipMessage.ChangeType.OwnershipChanging || ChangeMessageType == global::Unity.Netcode.ChangeOwnershipMessage.ChangeType.RequestApproved)
			{
				writer.WriteValueSafe(in OwnershipFlags, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			}
			if (ChangeMessageType == global::Unity.Netcode.ChangeOwnershipMessage.ChangeType.RequestOwnership || ChangeMessageType == global::Unity.Netcode.ChangeOwnershipMessage.ChangeType.RequestApproved || ChangeMessageType == global::Unity.Netcode.ChangeOwnershipMessage.ChangeType.RequestDenied)
			{
				writer.WriteValueSafe(in RequestClientId, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				if (ChangeMessageType == global::Unity.Netcode.ChangeOwnershipMessage.ChangeType.RequestDenied)
				{
					writer.WriteValueSafe(in OwnershipRequestResponseStatus, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				}
			}
		}

		public bool Deserialize(global::Unity.Netcode.FastBufferReader reader, ref global::Unity.Netcode.NetworkContext context, int receivedMessageVersion)
		{
			global::Unity.Netcode.NetworkManager networkManager = (global::Unity.Netcode.NetworkManager)context.SystemOwner;
			if (!networkManager.IsClient)
			{
				return false;
			}
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out NetworkObjectId);
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out OwnerClientId);
			if (networkManager.DistributedAuthorityMode)
			{
				global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out ClientIdCount);
				if (ClientIdCount > 0)
				{
					ClientIds = new ulong[ClientIdCount];
					for (int i = 0; i < ClientIdCount; i++)
					{
						global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out ulong value);
						ClientIds[i] = value;
					}
				}
				reader.ReadValueSafe(out ChangeMessageType, default(global::Unity.Netcode.FastBufferWriter.ForEnums));
				if (ChangeMessageType == global::Unity.Netcode.ChangeOwnershipMessage.ChangeType.OwnershipFlagsUpdate || ChangeMessageType == global::Unity.Netcode.ChangeOwnershipMessage.ChangeType.OwnershipChanging || ChangeMessageType == global::Unity.Netcode.ChangeOwnershipMessage.ChangeType.RequestApproved)
				{
					reader.ReadValueSafe(out OwnershipFlags, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				}
				if (ChangeMessageType == global::Unity.Netcode.ChangeOwnershipMessage.ChangeType.RequestOwnership || ChangeMessageType == global::Unity.Netcode.ChangeOwnershipMessage.ChangeType.RequestApproved || ChangeMessageType == global::Unity.Netcode.ChangeOwnershipMessage.ChangeType.RequestDenied)
				{
					reader.ReadValueSafe(out RequestClientId, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
					if (ChangeMessageType == global::Unity.Netcode.ChangeOwnershipMessage.ChangeType.RequestDenied)
					{
						reader.ReadValueSafe(out OwnershipRequestResponseStatus, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
					}
				}
			}
			else
			{
				ChangeMessageType = global::Unity.Netcode.ChangeOwnershipMessage.ChangeType.OwnershipChanging;
			}
			if (!networkManager.DAHost && !networkManager.SpawnManager.SpawnedObjects.ContainsKey(NetworkObjectId))
			{
				networkManager.DeferredMessageManager.DeferMessage(global::Unity.Netcode.IDeferredNetworkMessageManager.TriggerType.OnSpawn, NetworkObjectId, reader, ref context, "ChangeOwnershipMessage");
				return false;
			}
			return true;
		}

		public void Handle(ref global::Unity.Netcode.NetworkContext context)
		{
			global::Unity.Netcode.NetworkManager networkManager = (global::Unity.Netcode.NetworkManager)context.SystemOwner;
			global::Unity.Netcode.NetworkObject value;
			bool flag = networkManager.SpawnManager.SpawnedObjects.TryGetValue(NetworkObjectId, out value);
			if (networkManager.DAHost && !HandleDAHostMessageForwarding(ref networkManager, context.SenderId, flag, ref value))
			{
				return;
			}
			if (!flag)
			{
				if (networkManager.LogLevel <= global::Unity.Netcode.LogLevel.Normal)
				{
					global::Unity.Netcode.NetworkLog.LogError("Ownership change received for an unknown network object. This should not happen.");
				}
			}
			else if (!value.IsSpawned)
			{
				if (networkManager.LogLevel <= global::Unity.Netcode.LogLevel.Normal)
				{
					global::Unity.Netcode.NetworkLog.LogError("[" + value.name + "] Ownership change received for network object that is not yet spawned.");
				}
			}
			else if (ChangeMessageType == global::Unity.Netcode.ChangeOwnershipMessage.ChangeType.OwnershipChanging || ChangeMessageType == global::Unity.Netcode.ChangeOwnershipMessage.ChangeType.RequestApproved || !networkManager.DistributedAuthorityMode)
			{
				HandleOwnershipChange(ref context, ref networkManager, ref value);
			}
			else if (networkManager.DistributedAuthorityMode)
			{
				HandleExtendedOwnershipUpdate(ref context, ref value);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void HandleExtendedOwnershipUpdate(ref global::Unity.Netcode.NetworkContext context, ref global::Unity.Netcode.NetworkObject networkObject)
		{
			if (ChangeMessageType == global::Unity.Netcode.ChangeOwnershipMessage.ChangeType.OwnershipFlagsUpdate)
			{
				networkObject.Ownership = (global::Unity.Netcode.NetworkObject.OwnershipStatus)OwnershipFlags;
			}
			else if (ChangeMessageType == global::Unity.Netcode.ChangeOwnershipMessage.ChangeType.RequestOwnership)
			{
				networkObject.OwnershipRequest(RequestClientId);
			}
			else if (ChangeMessageType == global::Unity.Netcode.ChangeOwnershipMessage.ChangeType.RequestDenied)
			{
				networkObject.OwnershipRequestResponse((global::Unity.Netcode.NetworkObject.OwnershipRequestResponseStatus)OwnershipRequestResponseStatus);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void HandleOwnershipChange(ref global::Unity.Netcode.NetworkContext context, ref global::Unity.Netcode.NetworkManager networkManager, ref global::Unity.Netcode.NetworkObject networkObject)
		{
			bool distributedAuthorityMode = networkManager.DistributedAuthorityMode;
			if (networkObject.OwnerClientId == OwnerClientId)
			{
				global::Unity.Netcode.NetworkLog.LogError($"[Receiver: Client-{networkManager.LocalClientId}][Sender: Client-{context.SenderId}][RID: {RequestClientId}] Detected unnecessary ownership changed message for {networkObject.name} (NID:{NetworkObjectId}).");
				return;
			}
			ulong ownerClientId = networkObject.OwnerClientId;
			networkObject.OwnerClientId = OwnerClientId;
			if (distributedAuthorityMode)
			{
				networkObject.Ownership = (global::Unity.Netcode.NetworkObject.OwnershipStatus)OwnershipFlags;
			}
			networkObject.InvokeBehaviourOnOwnershipChanged(ownerClientId, OwnerClientId);
			if (!distributedAuthorityMode && ownerClientId == networkManager.LocalClientId)
			{
				networkObject.SynchronizeOwnerNetworkVariables(ownerClientId, networkObject.PreviousOwnerId);
			}
			networkObject.InvokeOwnershipChanged(ownerClientId, OwnerClientId);
			if (distributedAuthorityMode && networkManager.LocalClientId == OwnerClientId)
			{
				if (ChangeMessageType == global::Unity.Netcode.ChangeOwnershipMessage.ChangeType.RequestApproved)
				{
					networkObject.OwnershipRequestResponse(global::Unity.Netcode.NetworkObject.OwnershipRequestResponseStatus.Approved);
				}
				if (networkObject.HasExtendedOwnershipStatus(global::Unity.Netcode.NetworkObject.OwnershipStatusExtended.Requested))
				{
					networkObject.RemoveOwnershipExtended(global::Unity.Netcode.NetworkObject.OwnershipStatusExtended.Requested);
					networkObject.SendOwnershipStatusUpdate();
				}
			}
			networkManager.NetworkMetrics.TrackOwnershipChangeReceived(context.SenderId, networkObject, context.MessageSize);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private bool HandleDAHostMessageForwarding(ref global::Unity.Netcode.NetworkManager networkManager, ulong senderId, bool hasObject, ref global::Unity.Netcode.NetworkObject networkObject)
		{
			global::Unity.Netcode.ChangeOwnershipMessage message = new global::Unity.Netcode.ChangeOwnershipMessage
			{
				NetworkObjectId = NetworkObjectId,
				OwnerClientId = OwnerClientId,
				DistributedAuthorityMode = true,
				OwnershipFlags = OwnershipFlags,
				RequestClientId = RequestClientId,
				ClientIdCount = 0,
				ChangeMessageType = ChangeMessageType
			};
			global::Unity.Netcode.NetworkDelivery defaultDelivery = global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.ChangeOwnershipMessage>.DefaultDelivery;
			if (ChangeMessageType == global::Unity.Netcode.ChangeOwnershipMessage.ChangeType.RequestDenied)
			{
				if (RequestClientId != networkManager.LocalClientId)
				{
					message.OwnershipRequestResponseStatus = OwnershipRequestResponseStatus;
					networkManager.ConnectionManager.SendMessage(ref message, defaultDelivery, RequestClientId);
					return false;
				}
			}
			else if (ChangeMessageType == global::Unity.Netcode.ChangeOwnershipMessage.ChangeType.RequestOwnership)
			{
				if (OwnerClientId != networkManager.LocalClientId)
				{
					networkManager.ConnectionManager.SendMessage(ref message, defaultDelivery, OwnerClientId);
					return false;
				}
			}
			else
			{
				ulong[] array = ClientIds;
				bool flag = true;
				if (ChangeMessageType == global::Unity.Netcode.ChangeOwnershipMessage.ChangeType.OwnershipFlagsUpdate)
				{
					array = (hasObject ? global::System.Linq.Enumerable.ToArray(networkObject.Observers) : global::System.Linq.Enumerable.ToArray(networkManager.ConnectedClientsIds));
					flag = false;
				}
				ulong[] array2 = array;
				foreach (ulong num in array2)
				{
					if (num == networkManager.LocalClientId)
					{
						continue;
					}
					if (num == senderId)
					{
						if (flag)
						{
							global::UnityEngine.Debug.LogError($"client-{senderId} sent a ChangeOwnershipMessage with themself inside the ClientIds list.");
						}
					}
					else
					{
						networkManager.ConnectionManager.SendMessage(ref message, defaultDelivery, num);
					}
				}
			}
			return hasObject;
		}
	}
}
