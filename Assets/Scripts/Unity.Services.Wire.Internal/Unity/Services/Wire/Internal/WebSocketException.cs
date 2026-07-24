namespace Unity.Services.Wire.Internal
{
	public class WebSocketException : global::System.Exception
	{
		public WebSocketException()
		{
		}

		public WebSocketException(string message)
			: base(message)
		{
		}

		public WebSocketException(string message, global::System.Exception inner)
			: base(message, inner)
		{
		}
	}
}
