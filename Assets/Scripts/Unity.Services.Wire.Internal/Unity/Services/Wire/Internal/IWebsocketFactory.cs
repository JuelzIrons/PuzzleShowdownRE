namespace Unity.Services.Wire.Internal
{
	internal interface IWebsocketFactory
	{
		global::Unity.Services.Wire.Internal.IWebSocket CreateInstance(string url);
	}
}
