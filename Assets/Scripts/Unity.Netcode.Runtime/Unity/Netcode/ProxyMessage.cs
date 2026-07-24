namespace Unity.Netcode
{
	internal struct ProxyMessage : global::Unity.Netcode.INetworkMessage
	{
		public global::Unity.Collections.NativeArray<ulong> TargetClientIds;

		public global::Unity.Netcode.NetworkDelivery Delivery;

		public global::Unity.Netcode.RpcMessage WrappedMessage;

		public int Version => default(global::Unity.Netcode.RpcMessage).Version;

		public void Serialize(global::Unity.Netcode.FastBufferWriter writer, int targetVersion)
		{
			writer.WriteValueSafe(TargetClientIds);
			global::Unity.Netcode.BytePacker.WriteValuePacked(writer, Delivery);
			WrappedMessage.Serialize(writer, targetVersion);
		}

		public bool Deserialize(global::Unity.Netcode.FastBufferReader reader, ref global::Unity.Netcode.NetworkContext context, int receivedMessageVersion)
		{
			reader.ReadValueSafe(out TargetClientIds, global::Unity.Collections.Allocator.Temp, default(global::Unity.Netcode.FastBufferWriter.ForGeneric));
			global::Unity.Netcode.ByteUnpacker.ReadValuePacked(reader, out Delivery);
			WrappedMessage = default(global::Unity.Netcode.RpcMessage);
			WrappedMessage.Deserialize(reader, ref context, receivedMessageVersion);
			return true;
		}

		public unsafe void Handle(ref global::Unity.Netcode.NetworkContext context)
		{
			global::Unity.Netcode.NetworkManager networkManager = (global::Unity.Netcode.NetworkManager)context.SystemOwner;
			if (!networkManager.SpawnManager.SpawnedObjects.TryGetValue(WrappedMessage.Metadata.NetworkObjectId, out var value))
			{
				if (networkManager.LogLevel == global::Unity.Netcode.LogLevel.Developer)
				{
					global::Unity.Netcode.NetworkLog.LogWarning(string.Format("[{0}, {1}, {2}] An RPC called on a {3} that is not in the spawned objects list. Please make sure the {4} is spawned before calling RPCs.", WrappedMessage.Metadata.NetworkObjectId, WrappedMessage.Metadata.NetworkBehaviourId, WrappedMessage.Metadata.NetworkRpcMethodId, "NetworkObject", "NetworkObject"));
				}
				return;
			}
			global::System.Collections.Generic.HashSet<ulong> observers = value.Observers;
			if (networkManager.IsServer)
			{
				global::Unity.Netcode.NetworkBehaviour networkBehaviourAtOrderIndex = value.GetNetworkBehaviourAtOrderIndex(WrappedMessage.Metadata.NetworkBehaviourId);
				if (global::Unity.Netcode.NetworkBehaviour.__rpc_permission_table[networkBehaviourAtOrderIndex.GetType()][WrappedMessage.Metadata.NetworkRpcMethodId] switch
				{
					global::Unity.Netcode.RpcInvokePermission.Everyone => 1, 
					global::Unity.Netcode.RpcInvokePermission.Server => (context.SenderId == networkManager.LocalClientId) ? 1 : 0, 
					global::Unity.Netcode.RpcInvokePermission.Owner => (context.SenderId == networkBehaviourAtOrderIndex.OwnerClientId) ? 1 : 0, 
					_ => 0, 
				} == 0)
				{
					if (networkManager.LogLevel <= global::Unity.Netcode.LogLevel.Developer)
					{
						global::Unity.Netcode.NetworkLog.LogErrorServer($"Rpc message received from client-{context.SenderId} who does not have permission to perform this operation!");
					}
					return;
				}
				WrappedMessage.SenderClientId = context.SenderId;
			}
			global::Unity.Collections.NativeList<ulong> clientIds = new global::Unity.Collections.NativeList<ulong>(global::Unity.Collections.Allocator.Temp);
			foreach (ulong targetClientId in TargetClientIds)
			{
				ulong value2 = targetClientId;
				if (observers.Contains(value2))
				{
					if (value2 == 0L)
					{
						WrappedMessage.Handle(ref context);
					}
					else
					{
						clientIds.Add(in value2);
					}
				}
			}
			WrappedMessage.WriteBuffer = new global::Unity.Netcode.FastBufferWriter(WrappedMessage.ReadBuffer.Length, global::Unity.Collections.Allocator.Temp);
			using (WrappedMessage.WriteBuffer)
			{
				WrappedMessage.WriteBuffer.WriteBytesSafe(WrappedMessage.ReadBuffer.GetUnsafePtr(), WrappedMessage.ReadBuffer.Length);
				networkManager.MessageManager.SendMessage(ref WrappedMessage, Delivery, in clientIds);
			}
		}
	}
}
