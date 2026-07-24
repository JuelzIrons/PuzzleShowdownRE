namespace UnityWebSocketSharp.Server
{
	internal interface IWebSocketSession
	{
		string ID { get; }

		global::System.DateTime StartTime { get; }

		global::UnityWebSocketSharp.WebSocket WebSocket { get; }
	}
}
