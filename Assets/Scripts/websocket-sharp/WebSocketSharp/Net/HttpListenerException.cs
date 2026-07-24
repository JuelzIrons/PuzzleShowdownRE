namespace WebSocketSharp.Net
{
	[global::System.Serializable]
	public class HttpListenerException : global::System.ComponentModel.Win32Exception
	{
		public override int ErrorCode => base.NativeErrorCode;

		protected HttpListenerException(global::System.Runtime.Serialization.SerializationInfo serializationInfo, global::System.Runtime.Serialization.StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}

		public HttpListenerException()
		{
		}

		public HttpListenerException(int errorCode)
			: base(errorCode)
		{
		}

		public HttpListenerException(int errorCode, string message)
			: base(errorCode, message)
		{
		}
	}
}
