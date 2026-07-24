namespace Unity.Networking.Transport.Relay
{
	public static class NetworkDriverRelayExtensions
	{
		public static global::Unity.Networking.Transport.Relay.RelayConnectionStatus GetRelayConnectionStatus(this global::Unity.Networking.Transport.NetworkDriver driver)
		{
			if (driver.m_NetworkStack.TryGetLayer<global::Unity.Networking.Transport.RelayLayer>(out var layer))
			{
				return layer.ConnectionStatus;
			}
			return global::Unity.Networking.Transport.Relay.RelayConnectionStatus.NotUsingRelay;
		}

		public static global::Unity.Networking.Transport.NetworkConnection Connect(this global::Unity.Networking.Transport.NetworkDriver driver)
		{
			if (driver.CurrentSettings.TryGet<global::Unity.Networking.Transport.Relay.RelayNetworkParameter>(out var parameter))
			{
				return driver.Connect(parameter.ServerData.Endpoint);
			}
			global::UnityEngine.Debug.LogError("Can't call Connect without an endpoint when not using the Relay.");
			return default(global::Unity.Networking.Transport.NetworkConnection);
		}
	}
}
