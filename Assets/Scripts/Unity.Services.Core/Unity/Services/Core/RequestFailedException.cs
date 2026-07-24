namespace Unity.Services.Core
{
	public class RequestFailedException : global::System.Exception
	{
		public int ErrorCode { get; }

		public RequestFailedException(int errorCode, string message)
			: this(errorCode, message, null)
		{
		}

		public RequestFailedException(int errorCode, string message, global::System.Exception innerException)
			: base(message, innerException)
		{
			ErrorCode = errorCode;
		}
	}
}
