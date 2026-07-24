namespace Unity.Netcode
{
	public abstract class NetworkPrefabInstanceHandlerWithData<T> : global::Unity.Netcode.INetworkPrefabInstanceHandlerWithData, global::Unity.Netcode.INetworkPrefabInstanceHandler where T : struct, global::Unity.Netcode.INetworkSerializable
	{
		public abstract global::Unity.Netcode.NetworkObject Instantiate(ulong ownerClientId, global::UnityEngine.Vector3 position, global::UnityEngine.Quaternion rotation, T instantiationData);

		public abstract void Destroy(global::Unity.Netcode.NetworkObject networkObject);

		bool global::Unity.Netcode.INetworkPrefabInstanceHandlerWithData.HandlesDataType<TK>()
		{
			return typeof(T) == typeof(TK);
		}

		global::Unity.Netcode.NetworkObject global::Unity.Netcode.INetworkPrefabInstanceHandlerWithData.Instantiate(ulong ownerClientId, global::UnityEngine.Vector3 position, global::UnityEngine.Quaternion rotation, byte[] instantiationData)
		{
			using global::Unity.Netcode.FastBufferReader fastBufferReader = new global::Unity.Netcode.FastBufferReader(instantiationData, global::Unity.Collections.Allocator.Temp);
			fastBufferReader.ReadValueSafe(out T value, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
			global::Unity.Netcode.NetworkObject networkObject = Instantiate(ownerClientId, position, rotation, value);
			if (networkObject != null)
			{
				networkObject.InstantiationData = instantiationData;
			}
			return networkObject;
		}

		global::Unity.Netcode.NetworkObject global::Unity.Netcode.INetworkPrefabInstanceHandler.Instantiate(ulong ownerClientId, global::UnityEngine.Vector3 position, global::UnityEngine.Quaternion rotation)
		{
			return Instantiate(ownerClientId, position, rotation, default(T));
		}
	}
}
