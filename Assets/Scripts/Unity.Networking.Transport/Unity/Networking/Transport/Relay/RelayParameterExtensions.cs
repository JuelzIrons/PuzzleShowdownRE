namespace Unity.Networking.Transport.Relay
{
	public static class RelayParameterExtensions
	{
		public static ref global::Unity.Networking.Transport.NetworkSettings WithRelayParameters(this ref global::Unity.Networking.Transport.NetworkSettings settings, ref global::Unity.Networking.Transport.Relay.RelayServerData serverData, int relayConnectionTimeMS = 3000)
		{
			global::Unity.Networking.Transport.Relay.RelayNetworkParameter parameter = new global::Unity.Networking.Transport.Relay.RelayNetworkParameter
			{
				ServerData = serverData,
				RelayConnectionTimeMS = relayConnectionTimeMS
			};
			settings.AddRawParameterStruct(ref parameter);
			return ref settings;
		}

		public static global::Unity.Networking.Transport.Relay.RelayNetworkParameter GetRelayParameters(this ref global::Unity.Networking.Transport.NetworkSettings settings)
		{
			if (!settings.TryGet<global::Unity.Networking.Transport.Relay.RelayNetworkParameter>(out var parameter))
			{
				throw new global::System.InvalidOperationException("Can't extract Relay parameters: RelayNetworkParameter must be provided to the NetworkSettings");
			}
			return parameter;
		}
	}
}
