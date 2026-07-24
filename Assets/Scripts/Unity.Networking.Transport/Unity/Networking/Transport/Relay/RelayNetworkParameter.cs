namespace Unity.Networking.Transport.Relay
{
	[global::System.Serializable]
	public struct RelayNetworkParameter : global::Unity.Networking.Transport.INetworkParameter
	{
		internal const int k_DefaultConnectionTimeMS = 3000;

		public global::Unity.Networking.Transport.Relay.RelayServerData ServerData;

		public int RelayConnectionTimeMS;

		public bool Validate()
		{
			bool result = true;
			if (ServerData.Endpoint == default(global::Unity.Networking.Transport.NetworkEndpoint))
			{
				result = false;
				global::UnityEngine.Debug.LogError($"ServerData.Endpoint value ({ServerData.Endpoint}) must be a valid value");
			}
			if (ServerData.AllocationId == default(global::Unity.Networking.Transport.Relay.RelayAllocationId))
			{
				result = false;
				global::UnityEngine.Debug.LogError($"ServerData.AllocationId value ({ServerData.AllocationId}) must be a valid value");
			}
			if (RelayConnectionTimeMS < 0)
			{
				result = false;
				global::UnityEngine.Debug.LogError($"RelayConnectionTimeMS value ({RelayConnectionTimeMS}) must be greater than or equal to 0");
			}
			return result;
		}
	}
}
