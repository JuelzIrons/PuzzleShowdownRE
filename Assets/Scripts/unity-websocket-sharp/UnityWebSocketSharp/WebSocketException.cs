namespace UnityWebSocketSharp
{
	internal class WebSocketException : global::System.Exception
	{
		private ushort _code;

		public ushort Code => _code;

		private WebSocketException(ushort code, string message, global::System.Exception innerException)
			: base(message ?? code.GetErrorMessage(), innerException)
		{
			_code = code;
		}

		internal WebSocketException()
			: this(global::UnityWebSocketSharp.CloseStatusCode.Abnormal, null, null)
		{
		}

		internal WebSocketException(global::System.Exception innerException)
			: this(global::UnityWebSocketSharp.CloseStatusCode.Abnormal, null, innerException)
		{
		}

		internal WebSocketException(string message)
			: this(global::UnityWebSocketSharp.CloseStatusCode.Abnormal, message, null)
		{
		}

		internal WebSocketException(global::UnityWebSocketSharp.CloseStatusCode code)
			: this(code, null, null)
		{
		}

		internal WebSocketException(string message, global::System.Exception innerException)
			: this(global::UnityWebSocketSharp.CloseStatusCode.Abnormal, message, innerException)
		{
		}

		internal WebSocketException(global::UnityWebSocketSharp.CloseStatusCode code, global::System.Exception innerException)
			: this(code, null, innerException)
		{
		}

		internal WebSocketException(global::UnityWebSocketSharp.CloseStatusCode code, string message)
			: this(code, message, null)
		{
		}

		internal WebSocketException(global::UnityWebSocketSharp.CloseStatusCode code, string message, global::System.Exception innerException)
			: this((ushort)code, message, innerException)
		{
		}
	}
}
