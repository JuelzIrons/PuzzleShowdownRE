namespace Unity.Services.Wire.Internal
{
	internal interface IWebSocket
	{
		event global::Unity.Services.Wire.Internal.WebSocketOpenEventHandler OnOpen;

		event global::Unity.Services.Wire.Internal.WebSocketMessageEventHandler OnMessage;

		event global::Unity.Services.Wire.Internal.WebSocketErrorEventHandler OnError;

		event global::Unity.Services.Wire.Internal.WebSocketCloseEventHandler OnClose;

		void Connect();

		void Close(global::Unity.Services.Wire.Internal.WebSocketCloseCode code = global::Unity.Services.Wire.Internal.WebSocketCloseCode.Normal, string reason = null);

		void Send(byte[] data);

		global::Unity.Services.Wire.Internal.WebSocketState GetState();
	}
}
