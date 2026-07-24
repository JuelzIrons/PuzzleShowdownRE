namespace Unity.Services.Wire.Internal
{
	internal class WebSocketUnexpectedException : global::Unity.Services.Wire.Internal.WebSocketException
	{
		public WebSocketUnexpectedException()
		{
		}

		public WebSocketUnexpectedException(string message)
			: base(message)
		{
		}

		public WebSocketUnexpectedException(string message, global::System.Exception inner)
			: base(message, inner)
		{
		}
	}
}
