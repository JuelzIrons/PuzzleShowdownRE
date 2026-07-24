namespace Unity.Netcode
{
	internal static class RpcMessageHelpers
	{
		public unsafe static void Serialize(ref global::Unity.Netcode.FastBufferWriter writer, ref global::Unity.Netcode.RpcMetadata metadata, ref global::Unity.Netcode.FastBufferWriter payload)
		{
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, metadata.NetworkObjectId);
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, metadata.NetworkBehaviourId);
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, metadata.NetworkRpcMethodId);
			writer.WriteBytesSafe(payload.GetUnsafePtr(), payload.Length);
		}

		public unsafe static bool Deserialize(ref global::Unity.Netcode.FastBufferReader reader, ref global::Unity.Netcode.NetworkContext context, ref global::Unity.Netcode.RpcMetadata metadata, ref global::Unity.Netcode.FastBufferReader payload, string messageType)
		{
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out metadata.NetworkObjectId);
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out metadata.NetworkBehaviourId);
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out metadata.NetworkRpcMethodId);
			global::Unity.Netcode.NetworkManager networkManager = (global::Unity.Netcode.NetworkManager)context.SystemOwner;
			if (!networkManager.SpawnManager.SpawnedObjects.ContainsKey(metadata.NetworkObjectId))
			{
				networkManager.DeferredMessageManager.DeferMessage(global::Unity.Netcode.IDeferredNetworkMessageManager.TriggerType.OnSpawn, metadata.NetworkObjectId, reader, ref context, messageType);
				return false;
			}
			_ = networkManager.SpawnManager.SpawnedObjects[metadata.NetworkObjectId];
			global::Unity.Netcode.NetworkBehaviour networkBehaviourAtOrderIndex = networkManager.SpawnManager.SpawnedObjects[metadata.NetworkObjectId].GetNetworkBehaviourAtOrderIndex(metadata.NetworkBehaviourId);
			if (networkBehaviourAtOrderIndex == null)
			{
				return false;
			}
			if (!global::Unity.Netcode.NetworkBehaviour.__rpc_func_table[networkBehaviourAtOrderIndex.GetType()].ContainsKey(metadata.NetworkRpcMethodId))
			{
				return false;
			}
			payload = new global::Unity.Netcode.FastBufferReader(reader.GetUnsafePtrAtCurrentPosition(), global::Unity.Collections.Allocator.None, reader.Length - reader.Position);
			return true;
		}

		public static void Handle(ref global::Unity.Netcode.NetworkContext context, ref global::Unity.Netcode.RpcMetadata metadata, ref global::Unity.Netcode.FastBufferReader payload, ref global::Unity.Netcode.__RpcParams rpcParams)
		{
			global::Unity.Netcode.NetworkManager networkManager = (global::Unity.Netcode.NetworkManager)context.SystemOwner;
			if (!networkManager.SpawnManager.SpawnedObjects.TryGetValue(metadata.NetworkObjectId, out var value))
			{
				if (networkManager.LogLevel == global::Unity.Netcode.LogLevel.Developer)
				{
					global::Unity.Netcode.NetworkLog.LogWarning(string.Format("[{0}, {1}, {2}] An RPC called on a {3} that is not in the spawned objects list. Please make sure the {4} is spawned before calling RPCs.", metadata.NetworkObjectId, metadata.NetworkBehaviourId, metadata.NetworkRpcMethodId, "NetworkObject", "NetworkObject"));
				}
				return;
			}
			global::Unity.Netcode.NetworkBehaviour networkBehaviourAtOrderIndex = value.GetNetworkBehaviourAtOrderIndex(metadata.NetworkBehaviourId);
			try
			{
				global::Unity.Netcode.RpcInvokePermission rpcInvokePermission = global::Unity.Netcode.NetworkBehaviour.__rpc_permission_table[networkBehaviourAtOrderIndex.GetType()][metadata.NetworkRpcMethodId];
				if ((rpcInvokePermission == global::Unity.Netcode.RpcInvokePermission.Server && rpcParams.SenderId != 0L) || (rpcInvokePermission == global::Unity.Netcode.RpcInvokePermission.Owner && rpcParams.SenderId != value.OwnerClientId))
				{
					if (networkManager.LogLevel <= global::Unity.Netcode.LogLevel.Developer)
					{
						global::Unity.Netcode.NetworkLog.LogErrorServer($"Rpc message received from client-{rpcParams.SenderId} who does not have permission to perform this operation!");
					}
				}
				else
				{
					global::Unity.Netcode.NetworkBehaviour.__rpc_func_table[networkBehaviourAtOrderIndex.GetType()][metadata.NetworkRpcMethodId](networkBehaviourAtOrderIndex, payload, rpcParams);
				}
			}
			catch (global::System.Exception innerException)
			{
				global::UnityEngine.Debug.LogException(new global::System.Exception("Unhandled RPC exception!", innerException));
				if (networkManager.LogLevel > global::Unity.Netcode.LogLevel.Developer)
				{
					return;
				}
				global::UnityEngine.Debug.Log("RPC Table Contents");
				foreach (global::System.Collections.Generic.KeyValuePair<uint, global::Unity.Netcode.NetworkBehaviour.RpcReceiveHandler> item in global::Unity.Netcode.NetworkBehaviour.__rpc_func_table[networkBehaviourAtOrderIndex.GetType()])
				{
					global::Unity.Netcode.RpcInvokePermission rpcInvokePermission2 = global::Unity.Netcode.NetworkBehaviour.__rpc_permission_table[networkBehaviourAtOrderIndex.GetType()][metadata.NetworkRpcMethodId];
					global::UnityEngine.Debug.Log($"{item.Key} | {item.Value.Method.Name} | {rpcInvokePermission2}");
				}
			}
		}
	}
}
