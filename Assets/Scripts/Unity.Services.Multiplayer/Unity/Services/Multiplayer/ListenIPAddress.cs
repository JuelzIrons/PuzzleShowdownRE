namespace Unity.Services.Multiplayer
{
	public readonly struct ListenIPAddress
	{
		private const string InvalidAddressErrorMessage = "The listen address is not valid.";

		private readonly global::Unity.Services.Multiplayer.NetworkEndpointAddress m_Endpoint;

		public global::Unity.Services.Multiplayer.NetworkEndpointAddress NetworkEndpoint => m_Endpoint;

		public global::Unity.Networking.Transport.NetworkEndpoint UtpNetworkEndpoint => m_Endpoint.UtpEndpoint;

		public static global::Unity.Services.Multiplayer.ListenIPAddress LoopbackIpv4 => new global::Unity.Services.Multiplayer.ListenIPAddress(global::Unity.Services.Multiplayer.NetworkEndpointAddress.LoopbackIpv4);

		public static global::Unity.Services.Multiplayer.ListenIPAddress LoopbackIpv6 => new global::Unity.Services.Multiplayer.ListenIPAddress(global::Unity.Services.Multiplayer.NetworkEndpointAddress.LoopbackIpv6);

		public static global::Unity.Services.Multiplayer.ListenIPAddress AnyIpv4 => new global::Unity.Services.Multiplayer.ListenIPAddress(global::Unity.Services.Multiplayer.NetworkEndpointAddress.AnyIpv4);

		public static global::Unity.Services.Multiplayer.ListenIPAddress AnyIpv6 => new global::Unity.Services.Multiplayer.ListenIPAddress(global::Unity.Services.Multiplayer.NetworkEndpointAddress.AnyIpv6);

		public ListenIPAddress(string address)
			: this(address, global::Unity.Services.Multiplayer.IpValidatorUtils.GetPlatformDependentValidator())
		{
		}

		internal ListenIPAddress(string address, global::Unity.Services.Multiplayer.IIpValidator ipValidator)
		{
			if (!ipValidator.TryParseIPAddress(address, out var endpoint))
			{
				global::Unity.Services.Multiplayer.Logger.LogError("Could not parse given ip address: " + address);
				throw new global::System.ArgumentException("The listen address is not valid.");
			}
			m_Endpoint = endpoint;
		}

		public ListenIPAddress(global::Unity.Services.Multiplayer.NetworkEndpointAddress endpoint)
		{
			m_Endpoint = endpoint;
		}

		public global::Unity.Services.Multiplayer.ListenIPAddress WithPort(ushort port)
		{
			return new global::Unity.Services.Multiplayer.ListenIPAddress(m_Endpoint.WithPort(port));
		}
	}
}
