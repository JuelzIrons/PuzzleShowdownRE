namespace WebSocketSharp
{
	internal class HttpRequest : global::WebSocketSharp.HttpBase
	{
		private global::WebSocketSharp.Net.CookieCollection _cookies;

		private string _method;

		private string _uri;

		public global::WebSocketSharp.Net.AuthenticationResponse AuthenticationResponse
		{
			get
			{
				string text = base.Headers["Authorization"];
				return (text != null && text.Length > 0) ? global::WebSocketSharp.Net.AuthenticationResponse.Parse(text) : null;
			}
		}

		public global::WebSocketSharp.Net.CookieCollection Cookies
		{
			get
			{
				if (_cookies == null)
				{
					_cookies = base.Headers.GetCookies(response: false);
				}
				return _cookies;
			}
		}

		public string HttpMethod => _method;

		public bool IsWebSocketRequest => _method == "GET" && base.ProtocolVersion > global::WebSocketSharp.Net.HttpVersion.Version10 && base.Headers.Upgrades("websocket");

		public string RequestUri => _uri;

		private HttpRequest(string method, string uri, global::System.Version version, global::System.Collections.Specialized.NameValueCollection headers)
			: base(version, headers)
		{
			_method = method;
			_uri = uri;
		}

		internal HttpRequest(string method, string uri)
			: this(method, uri, global::WebSocketSharp.Net.HttpVersion.Version11, new global::System.Collections.Specialized.NameValueCollection())
		{
			base.Headers["User-Agent"] = "websocket-sharp/1.0";
		}

		internal static global::WebSocketSharp.HttpRequest CreateConnectRequest(global::System.Uri uri)
		{
			string dnsSafeHost = uri.DnsSafeHost;
			int port = uri.Port;
			string text = $"{dnsSafeHost}:{port}";
			global::WebSocketSharp.HttpRequest httpRequest = new global::WebSocketSharp.HttpRequest("CONNECT", text);
			httpRequest.Headers["Host"] = ((port == 80) ? dnsSafeHost : text);
			return httpRequest;
		}

		internal static global::WebSocketSharp.HttpRequest CreateWebSocketRequest(global::System.Uri uri)
		{
			global::WebSocketSharp.HttpRequest httpRequest = new global::WebSocketSharp.HttpRequest("GET", uri.PathAndQuery);
			global::System.Collections.Specialized.NameValueCollection headers = httpRequest.Headers;
			int port = uri.Port;
			string scheme = uri.Scheme;
			headers["Host"] = (((port == 80 && scheme == "ws") || (port == 443 && scheme == "wss")) ? uri.DnsSafeHost : uri.Authority);
			headers["Upgrade"] = "websocket";
			headers["Connection"] = "Upgrade";
			return httpRequest;
		}

		internal global::WebSocketSharp.HttpResponse GetResponse(global::System.IO.Stream stream, int millisecondsTimeout)
		{
			byte[] array = ToByteArray();
			stream.Write(array, 0, array.Length);
			return global::WebSocketSharp.HttpBase.Read(stream, global::WebSocketSharp.HttpResponse.Parse, millisecondsTimeout);
		}

		internal static global::WebSocketSharp.HttpRequest Parse(string[] headerParts)
		{
			string[] array = headerParts[0].Split(new char[1] { ' ' }, 3);
			if (array.Length != 3)
			{
				throw new global::System.ArgumentException("Invalid request line: " + headerParts[0]);
			}
			global::WebSocketSharp.Net.WebHeaderCollection webHeaderCollection = new global::WebSocketSharp.Net.WebHeaderCollection();
			for (int i = 1; i < headerParts.Length; i++)
			{
				webHeaderCollection.InternalSet(headerParts[i], response: false);
			}
			return new global::WebSocketSharp.HttpRequest(array[0], array[1], new global::System.Version(array[2].Substring(5)), webHeaderCollection);
		}

		internal static global::WebSocketSharp.HttpRequest Read(global::System.IO.Stream stream, int millisecondsTimeout)
		{
			return global::WebSocketSharp.HttpBase.Read(stream, Parse, millisecondsTimeout);
		}

		public void SetCookies(global::WebSocketSharp.Net.CookieCollection cookies)
		{
			if (cookies == null || cookies.Count == 0)
			{
				return;
			}
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder(64);
			foreach (global::WebSocketSharp.Net.Cookie item in cookies.Sorted)
			{
				if (!item.Expired)
				{
					stringBuilder.AppendFormat("{0}; ", item.ToString());
				}
			}
			int length = stringBuilder.Length;
			if (length > 2)
			{
				stringBuilder.Length = length - 2;
				base.Headers["Cookie"] = stringBuilder.ToString();
			}
		}

		public override string ToString()
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder(64);
			stringBuilder.AppendFormat("{0} {1} HTTP/{2}{3}", _method, _uri, base.ProtocolVersion, "\r\n");
			global::System.Collections.Specialized.NameValueCollection headers = base.Headers;
			string[] allKeys = headers.AllKeys;
			foreach (string text in allKeys)
			{
				stringBuilder.AppendFormat("{0}: {1}{2}", text, headers[text], "\r\n");
			}
			stringBuilder.Append("\r\n");
			string entityBody = base.EntityBody;
			if (entityBody.Length > 0)
			{
				stringBuilder.Append(entityBody);
			}
			return stringBuilder.ToString();
		}
	}
}
