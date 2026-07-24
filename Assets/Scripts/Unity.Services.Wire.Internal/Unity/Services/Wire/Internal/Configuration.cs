namespace Unity.Services.Wire.Internal
{
	internal class Configuration
	{
		public global::Unity.Services.Authentication.Internal.IAccessToken token;

		public string address;

		public double CommandTimeoutInSeconds = 5.0;

		public double RetrieveTokenTimeoutInSeconds = 5.0;

		public global::Unity.Services.Wire.Internal.IWebSocket WebSocket;

		public global::Unity.Services.Wire.Internal.INetworkUtil NetworkUtil;

		public double MaxServerPingDelay = 10.0;
	}
}
