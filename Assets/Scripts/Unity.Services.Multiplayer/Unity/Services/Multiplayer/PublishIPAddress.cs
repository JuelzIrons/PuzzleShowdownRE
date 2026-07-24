namespace Unity.Services.Multiplayer
{
	public readonly struct PublishIPAddress
	{
		private const string InvalidAddressErrorMessage = "The publish address is not valid.";

		private readonly global::Unity.Services.Multiplayer.NetworkEndpointAddress m_Endpoint;

		public global::Unity.Services.Multiplayer.NetworkEndpointAddress NetworkEndpoint => m_Endpoint;

		public global::Unity.Networking.Transport.NetworkEndpoint UtpNetworkEndpoint => m_Endpoint.UtpEndpoint;

		public static global::Unity.Services.Multiplayer.PublishIPAddress LoopbackIpv4 => new global::Unity.Services.Multiplayer.PublishIPAddress(global::Unity.Services.Multiplayer.NetworkEndpointAddress.LoopbackIpv4);

		public static global::Unity.Services.Multiplayer.PublishIPAddress LoopbackIpv6 => new global::Unity.Services.Multiplayer.PublishIPAddress(global::Unity.Services.Multiplayer.NetworkEndpointAddress.LoopbackIpv6);

		public PublishIPAddress(string address)
			: this(address, global::Unity.Services.Multiplayer.IpValidatorUtils.GetPlatformDependentValidator())
		{
		}

		internal PublishIPAddress(string address, global::Unity.Services.Multiplayer.IIpValidator ipValidator)
		{
			if (!ipValidator.TryParseIPAddress(address, out var endpoint))
			{
				throw new global::System.ArgumentException("The publish address is not valid.");
			}
			m_Endpoint = endpoint;
		}

		public PublishIPAddress(global::Unity.Services.Multiplayer.NetworkEndpointAddress endpoint)
		{
			m_Endpoint = endpoint;
		}

		public global::Unity.Services.Multiplayer.PublishIPAddress WithPort(ushort port)
		{
			return new global::Unity.Services.Multiplayer.PublishIPAddress(m_Endpoint.WithPort(port));
		}
	}
}
