namespace Unity.Services.Multiplayer
{
	public struct NetworkEndpointAddress
	{
		private const string InvalidIpAddressErrorMessage = "The IP address is not valid.";

		public static readonly global::Unity.Services.Multiplayer.NetworkEndpointAddress LoopbackIpv4 = new global::Unity.Services.Multiplayer.NetworkEndpointAddress(global::Unity.Networking.Transport.NetworkEndpoint.LoopbackIpv4);

		public static readonly global::Unity.Services.Multiplayer.NetworkEndpointAddress LoopbackIpv6 = new global::Unity.Services.Multiplayer.NetworkEndpointAddress(global::Unity.Networking.Transport.NetworkEndpoint.LoopbackIpv6);

		public static readonly global::Unity.Services.Multiplayer.NetworkEndpointAddress AnyIpv4 = new global::Unity.Services.Multiplayer.NetworkEndpointAddress(global::Unity.Networking.Transport.NetworkEndpoint.AnyIpv4);

		public static readonly global::Unity.Services.Multiplayer.NetworkEndpointAddress AnyIpv6 = new global::Unity.Services.Multiplayer.NetworkEndpointAddress(global::Unity.Networking.Transport.NetworkEndpoint.AnyIpv6);

		private global::Unity.Networking.Transport.NetworkEndpoint m_UtpEndpoint;

		public ushort Port => m_UtpEndpoint.Port;

		public string Address => m_UtpEndpoint.Address;

		public string AddressNoPort
		{
			get
			{
				if (m_UtpEndpoint.Family == global::Unity.Networking.Transport.NetworkFamily.Ipv4)
				{
					return m_UtpEndpoint.ToFixedStringNoPort().ToString();
				}
				return m_UtpEndpoint.ToFixedStringNoPort().ToString().Trim('[', ']');
			}
		}

		public global::Unity.Networking.Transport.NetworkEndpoint UtpEndpoint => m_UtpEndpoint;

		public NetworkEndpointAddress(string ip, ushort port)
			: this(ip, port, global::Unity.Services.Multiplayer.IpValidatorUtils.GetPlatformDependentValidator())
		{
		}

		internal NetworkEndpointAddress(string ip, ushort port, global::Unity.Services.Multiplayer.IIpValidator ipValidator)
		{
			if (!ipValidator.TryParseIPAddress(ip, out var endpoint))
			{
				throw new global::System.ArgumentException("The IP address is not valid.");
			}
			m_UtpEndpoint = endpoint.UtpEndpoint.WithPort(port);
		}

		public NetworkEndpointAddress(global::Unity.Networking.Transport.NetworkEndpoint utpEndpoint)
		{
			m_UtpEndpoint = utpEndpoint;
		}

		public readonly global::Unity.Services.Multiplayer.NetworkEndpointAddress WithPort(ushort port)
		{
			global::Unity.Services.Multiplayer.NetworkEndpointAddress result = this;
			result.m_UtpEndpoint.Port = port;
			return result;
		}
	}
}
