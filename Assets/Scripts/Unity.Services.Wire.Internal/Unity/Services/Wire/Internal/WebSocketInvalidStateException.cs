namespace Unity.Services.Wire.Internal
{
	internal class WebSocketInvalidStateException : global::Unity.Services.Wire.Internal.WebSocketException
	{
		public WebSocketInvalidStateException()
		{
		}

		public WebSocketInvalidStateException(string message)
			: base(message)
		{
		}

		public WebSocketInvalidStateException(string message, global::System.Exception inner)
			: base(message, inner)
		{
		}
	}
}
