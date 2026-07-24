namespace Unity.Services.Qos.V2.Http
{
	internal class HttpClientResponse
	{
		public global::System.Collections.Generic.Dictionary<string, string> Headers { get; }

		public long StatusCode { get; }

		public bool IsHttpError { get; }

		public bool IsNetworkError { get; }

		public byte[] Data { get; }

		public string ErrorMessage { get; }

		public HttpClientResponse(global::System.Collections.Generic.Dictionary<string, string> headers, long statusCode, bool isHttpError, bool isNetworkError, byte[] data, string errorMessage)
		{
			Headers = headers;
			StatusCode = statusCode;
			IsHttpError = isHttpError;
			IsNetworkError = isNetworkError;
			Data = data;
			ErrorMessage = errorMessage;
		}
	}
}
