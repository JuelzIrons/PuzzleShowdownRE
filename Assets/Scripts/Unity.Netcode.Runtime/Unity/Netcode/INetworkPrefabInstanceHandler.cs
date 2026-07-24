namespace Unity.Netcode
{
	public interface INetworkPrefabInstanceHandler
	{
		global::Unity.Netcode.NetworkObject Instantiate(ulong ownerClientId, global::UnityEngine.Vector3 position, global::UnityEngine.Quaternion rotation);

		void Destroy(global::Unity.Netcode.NetworkObject networkObject);
	}
}
