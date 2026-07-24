namespace WebSocketSharp
{
	public class WebSocketException : global::System.Exception
	{
		private global::WebSocketSharp.CloseStatusCode _code;

		public global::WebSocketSharp.CloseStatusCode Code => _code;

		internal WebSocketException()
			: this(global::WebSocketSharp.CloseStatusCode.Abnormal, null, null)
		{
		}

		internal WebSocketException(global::System.Exception innerException)
			: this(global::WebSocketSharp.CloseStatusCode.Abnormal, null, innerException)
		{
		}

		internal WebSocketException(string message)
			: this(global::WebSocketSharp.CloseStatusCode.Abnormal, message, null)
		{
		}

		internal WebSocketException(global::WebSocketSharp.CloseStatusCode code)
			: this(code, null, null)
		{
		}

		internal WebSocketException(string message, global::System.Exception innerException)
			: this(global::WebSocketSharp.CloseStatusCode.Abnormal, message, innerException)
		{
		}

		internal WebSocketException(global::WebSocketSharp.CloseStatusCode code, global::System.Exception innerException)
			: this(code, null, innerException)
		{
		}

		internal WebSocketException(global::WebSocketSharp.CloseStatusCode code, string message)
			: this(code, message, null)
		{
		}

		internal WebSocketException(global::WebSocketSharp.CloseStatusCode code, string message, global::System.Exception innerException)
			: base(message ?? code.GetMessage(), innerException)
		{
			_code = code;
		}
	}
}
