namespace Unity.Services.Wire.Internal
{
	internal class WebSocketFactory : global::Unity.Services.Wire.Internal.IWebsocketFactory
	{
		public global::Unity.Services.Wire.Internal.IWebSocket CreateInstance(string url)
		{
			return new global::Unity.Services.Wire.Internal.WebSocket(url);
		}
	}
}
