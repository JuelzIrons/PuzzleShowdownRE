namespace Unity.Services.Multiplayer
{
	internal static class IpValidatorUtils
	{
		internal static global::Unity.Services.Multiplayer.IIpValidator GetPlatformDependentValidator()
		{
			return new global::Unity.Services.Multiplayer.IpValidator(global::Unity.Networking.Transport.NetworkFamily.Invalid);
		}

		internal static bool IsValidIPAddress(this string ip, global::Unity.Networking.Transport.NetworkFamily family)
		{
			global::Unity.Services.Multiplayer.NetworkEndpointAddress endpoint;
			return ip.TryParseIPAddress(out endpoint, family);
		}

		internal static bool TryParseIPAddress(this string ip, out global::Unity.Services.Multiplayer.NetworkEndpointAddress endpoint, global::Unity.Networking.Transport.NetworkFamily family)
		{
			global::Unity.Networking.Transport.NetworkEndpoint endpoint2;
			bool result = global::Unity.Networking.Transport.NetworkEndpoint.TryParse(ip, 0, out endpoint2, family);
			endpoint = new global::Unity.Services.Multiplayer.NetworkEndpointAddress(endpoint2);
			return result;
		}
	}
}
