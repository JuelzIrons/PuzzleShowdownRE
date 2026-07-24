namespace Unity.Services.Multiplayer
{
	internal class IpValidator : global::Unity.Services.Multiplayer.IIpValidator
	{
		private readonly global::Unity.Networking.Transport.NetworkFamily m_Family;

		public static readonly global::Unity.Services.Multiplayer.IpValidator BothIpv4AndIpv6 = new global::Unity.Services.Multiplayer.IpValidator(global::Unity.Networking.Transport.NetworkFamily.Invalid);

		public static readonly global::Unity.Services.Multiplayer.IpValidator Ipv4Only = new global::Unity.Services.Multiplayer.IpValidator(global::Unity.Networking.Transport.NetworkFamily.Ipv4);

		public IpValidator(global::Unity.Networking.Transport.NetworkFamily family)
		{
			m_Family = family;
		}

		public bool IsValidIPAddress(string ip)
		{
			global::Unity.Services.Multiplayer.NetworkEndpointAddress endpoint;
			return TryParseIPAddress(ip, out endpoint);
		}

		public bool TryParseIPAddress(string ip, out global::Unity.Services.Multiplayer.NetworkEndpointAddress endpoint)
		{
			return ip.TryParseIPAddress(out endpoint, m_Family);
		}
	}
}
