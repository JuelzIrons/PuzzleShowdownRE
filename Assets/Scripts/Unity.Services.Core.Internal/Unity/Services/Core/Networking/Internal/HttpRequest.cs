namespace Unity.Services.Core.Networking.Internal
{
	internal class HttpRequest
	{
		public string Method;

		public string Url;

		public global::System.Collections.Generic.Dictionary<string, string> Headers;

		public byte[] Body;

		public global::Unity.Services.Core.Networking.Internal.HttpOptions Options;

		public HttpRequest()
		{
		}

		public HttpRequest(string method, string url, global::System.Collections.Generic.Dictionary<string, string> headers, byte[] body)
		{
			Method = method;
			Url = url;
			Headers = headers;
			Body = body;
		}

		public global::Unity.Services.Core.Networking.Internal.HttpRequest SetMethod(string method)
		{
			Method = method;
			return this;
		}

		public global::Unity.Services.Core.Networking.Internal.HttpRequest SetUrl(string url)
		{
			Url = url;
			return this;
		}

		public global::Unity.Services.Core.Networking.Internal.HttpRequest SetHeader(string key, string value)
		{
			if (Headers == null)
			{
				Headers = new global::System.Collections.Generic.Dictionary<string, string>(1);
			}
			Headers[key] = value;
			return this;
		}

		public global::Unity.Services.Core.Networking.Internal.HttpRequest SetHeaders(global::System.Collections.Generic.Dictionary<string, string> headers)
		{
			Headers = headers;
			return this;
		}

		public global::Unity.Services.Core.Networking.Internal.HttpRequest SetBody(byte[] body)
		{
			Body = body;
			return this;
		}

		public global::Unity.Services.Core.Networking.Internal.HttpRequest SetOptions(global::Unity.Services.Core.Networking.Internal.HttpOptions options)
		{
			Options = options;
			return this;
		}

		public global::Unity.Services.Core.Networking.Internal.HttpRequest SetRedirectLimit(int redirectLimit)
		{
			Options.RedirectLimit = redirectLimit;
			return this;
		}

		public global::Unity.Services.Core.Networking.Internal.HttpRequest SetTimeOutInSeconds(int timeout)
		{
			Options.RequestTimeoutInSeconds = timeout;
			return this;
		}
	}
}
