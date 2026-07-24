namespace Unity.Services.Authentication.PlayerAccounts
{
	internal class WebRequestException : global::System.Exception
	{
		public bool NetworkError { get; private set; }

		public bool DeserializationError { get; private set; }

		public bool ServerError { get; private set; }

		public long ResponseCode { get; private set; }

		public global::System.Collections.Generic.IDictionary<string, string> ResponseHeaders { get; private set; }

		internal WebRequestException(bool isNetworkError, bool isServerError, bool isDeserializationError, long responseCode, string errorMessage, global::System.Collections.Generic.IDictionary<string, string> responseHeaders = null)
			: base(errorMessage)
		{
			NetworkError = isNetworkError;
			ServerError = isServerError;
			DeserializationError = isDeserializationError;
			ResponseCode = responseCode;
			ResponseHeaders = responseHeaders;
		}
	}
}
