namespace Unity.Services.Wire.Internal
{
	internal class WebSocketInvalidArgumentException : global::Unity.Services.Wire.Internal.WebSocketException
	{
		public WebSocketInvalidArgumentException()
		{
		}

		public WebSocketInvalidArgumentException(string message)
			: base(message)
		{
		}

		public WebSocketInvalidArgumentException(string message, global::System.Exception inner)
			: base(message, inner)
		{
		}
	}
}
