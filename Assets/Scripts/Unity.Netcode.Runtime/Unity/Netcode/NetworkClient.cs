namespace Unity.Netcode
{
	public class NetworkClient
	{
		public ulong ClientId;

		public global::Unity.Netcode.NetworkObject PlayerObject;

		internal bool IsServer { get; set; }

		internal bool IsClient { get; set; }

		internal bool IsHost
		{
			get
			{
				if (IsClient)
				{
					return IsServer;
				}
				return false;
			}
		}

		internal bool IsConnected { get; set; }

		internal bool IsApproved { get; set; }

		public global::Unity.Netcode.NetworkTopologyTypes NetworkTopologyType { get; internal set; }

		public bool DAHost { get; internal set; }

		public bool IsSessionOwner { get; internal set; }

		public global::Unity.Netcode.NetworkObject[] OwnedObjects
		{
			get
			{
				if (!IsConnected)
				{
					return new global::Unity.Netcode.NetworkObject[0];
				}
				return SpawnManager.GetClientOwnedObjects(ClientId);
			}
		}

		internal global::Unity.Netcode.NetworkSpawnManager SpawnManager { get; private set; }

		internal bool SetRole(bool isServer, bool isClient, global::Unity.Netcode.NetworkManager networkManager = null)
		{
			ResetClient(isServer, isClient);
			IsServer = isServer;
			IsClient = isClient;
			if (networkManager != null)
			{
				SpawnManager = networkManager.SpawnManager;
				NetworkTopologyType = networkManager.NetworkConfig.NetworkTopology;
				if (NetworkTopologyType == global::Unity.Netcode.NetworkTopologyTypes.DistributedAuthority)
				{
					DAHost = IsClient && IsServer;
					if (!IsClient && IsServer)
					{
						global::UnityEngine.Debug.LogError("You cannot start NetworkManager as a server when operating in distributed authority mode!");
						return false;
					}
					if (DAHost && networkManager.CMBServiceConnection)
					{
						global::UnityEngine.Debug.LogError("You cannot start a host when connecting to a distributed authority CMB Service!");
						return false;
					}
				}
			}
			return true;
		}

		private void ResetClient(bool isServer, bool isClient)
		{
			if (!IsServer && !IsClient)
			{
				PlayerObject = null;
				ClientId = 0uL;
				IsConnected = false;
				IsApproved = false;
				SpawnManager = null;
				DAHost = false;
			}
		}

		internal void AssignPlayerObject(ref global::Unity.Netcode.NetworkObject networkObject)
		{
			PlayerObject = networkObject;
		}
	}
}
