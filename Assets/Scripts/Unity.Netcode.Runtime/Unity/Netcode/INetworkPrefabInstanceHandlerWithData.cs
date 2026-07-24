namespace Unity.Netcode
{
	internal interface INetworkPrefabInstanceHandlerWithData : global::Unity.Netcode.INetworkPrefabInstanceHandler
	{
		bool HandlesDataType<T>();

		global::Unity.Netcode.NetworkObject Instantiate(ulong ownerClientId, global::UnityEngine.Vector3 position, global::UnityEngine.Quaternion rotation, byte[] instantiationData);
	}
}
