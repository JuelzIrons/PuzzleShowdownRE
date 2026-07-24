namespace WebSocketSharp.Server
{
	public interface IWebSocketSession
	{
		global::WebSocketSharp.WebSocketState ConnectionState { get; }

		global::WebSocketSharp.Net.WebSockets.WebSocketContext Context { get; }

		string ID { get; }

		string Protocol { get; }

		global::System.DateTime StartTime { get; }
	}
}
