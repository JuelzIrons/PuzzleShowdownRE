namespace Unity.Services.Core.Networking.Internal
{
	internal class HttpResponse
	{
		public global::Unity.Services.Core.Networking.Internal.ReadOnlyHttpRequest Request;

		public global::System.Collections.Generic.Dictionary<string, string> Headers;

		public byte[] Data;

		public long StatusCode;

		public string ErrorMessage;

		public bool IsHttpError;

		public bool IsNetworkError;

		public global::Unity.Services.Core.Networking.Internal.HttpResponse SetRequest(global::Unity.Services.Core.Networking.Internal.HttpRequest request)
		{
			Request = new global::Unity.Services.Core.Networking.Internal.ReadOnlyHttpRequest(request);
			return this;
		}

		public global::Unity.Services.Core.Networking.Internal.HttpResponse SetRequest(global::Unity.Services.Core.Networking.Internal.ReadOnlyHttpRequest request)
		{
			Request = request;
			return this;
		}

		public global::Unity.Services.Core.Networking.Internal.HttpResponse SetHeader(string key, string value)
		{
			Headers[key] = value;
			return this;
		}

		public global::Unity.Services.Core.Networking.Internal.HttpResponse SetHeaders(global::System.Collections.Generic.Dictionary<string, string> headers)
		{
			Headers = headers;
			return this;
		}

		public global::Unity.Services.Core.Networking.Internal.HttpResponse SetData(byte[] data)
		{
			Data = data;
			return this;
		}

		public global::Unity.Services.Core.Networking.Internal.HttpResponse SetStatusCode(long statusCode)
		{
			StatusCode = statusCode;
			return this;
		}

		public global::Unity.Services.Core.Networking.Internal.HttpResponse SetErrorMessage(string errorMessage)
		{
			ErrorMessage = errorMessage;
			return this;
		}

		public global::Unity.Services.Core.Networking.Internal.HttpResponse SetIsHttpError(bool isHttpError)
		{
			IsHttpError = isHttpError;
			return this;
		}

		public global::Unity.Services.Core.Networking.Internal.HttpResponse SetIsNetworkError(bool isNetworkError)
		{
			IsNetworkError = isNetworkError;
			return this;
		}
	}
}
