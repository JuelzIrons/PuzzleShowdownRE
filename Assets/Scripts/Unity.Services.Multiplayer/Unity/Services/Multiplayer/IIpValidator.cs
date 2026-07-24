namespace Unity.Services.Multiplayer
{
	public interface IIpValidator
	{
		bool IsValidIPAddress(string ip);

		bool TryParseIPAddress(string ip, out global::Unity.Services.Multiplayer.NetworkEndpointAddress endpoint);
	}
}
