namespace Unity.Netcode
{
	internal class NetworkObjectProvider : global::Unity.Multiplayer.Tools.INetworkObjectProvider
	{
		private readonly global::Unity.Netcode.NetworkManager m_NetworkManager;

		public NetworkObjectProvider(global::Unity.Netcode.NetworkManager networkManager)
		{
			m_NetworkManager = networkManager;
		}

		public global::UnityEngine.Object GetNetworkObject(ulong networkObjectId)
		{
			if (m_NetworkManager.SpawnManager.SpawnedObjects.TryGetValue(networkObjectId, out var value))
			{
				return value;
			}
			return null;
		}
	}
}
