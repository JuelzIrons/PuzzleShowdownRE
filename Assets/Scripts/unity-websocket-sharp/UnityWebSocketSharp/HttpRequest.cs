namespace UnityWebSocketSharp
{
	internal class HttpRequest : global::UnityWebSocketSharp.HttpBase
	{
		private global::UnityWebSocketSharp.Net.CookieCollection _cookies;

		private string _method;

		private string _target;

		internal string RequestLine => $"{_method} {_target} HTTP/{base.ProtocolVersion}{(global::UnityWebSocketSharp.HttpBase.CrLf)}";

		public global::UnityWebSocketSharp.Net.AuthenticationResponse AuthenticationResponse
		{
			get
			{
				string text = base.Headers["Authorization"];
				if (text == null || text.Length <= 0)
				{
					return null;
				}
				return global::UnityWebSocketSharp.Net.AuthenticationResponse.Parse(text);
			}
		}

		public global::UnityWebSocketSharp.Net.CookieCollection Cookies
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

		public bool IsWebSocketRequest
		{
			get
			{
				if (_method == "GET" && base.ProtocolVersion > global::UnityWebSocketSharp.Net.HttpVersion.Version10)
				{
					return base.Headers.Upgrades("websocket");
				}
				return false;
			}
		}

		public override string MessageHeader => RequestLine + base.HeaderSection;

		public string RequestTarget => _target;

		private HttpRequest(string method, string target, global::System.Version version, global::System.Collections.Specialized.NameValueCollection headers)
			: base(version, headers)
		{
			_method = method;
			_target = target;
		}

		internal HttpRequest(string method, string target)
			: this(method, target, global::UnityWebSocketSharp.Net.HttpVersion.Version11, new global::System.Collections.Specialized.NameValueCollection())
		{
			base.Headers["User-Agent"] = "websocket-sharp/1.0";
		}

		internal static global::UnityWebSocketSharp.HttpRequest CreateConnectRequest(global::System.Uri targetUri)
		{
			string dnsSafeHost = targetUri.DnsSafeHost;
			int port = targetUri.Port;
			string text = $"{dnsSafeHost}:{port}";
			global::UnityWebSocketSharp.HttpRequest httpRequest = new global::UnityWebSocketSharp.HttpRequest("CONNECT", text);
			httpRequest.Headers["Host"] = ((port != 80) ? text : dnsSafeHost);
			return httpRequest;
		}

		internal static global::UnityWebSocketSharp.HttpRequest CreateWebSocketHandshakeRequest(global::System.Uri targetUri)
		{
			global::UnityWebSocketSharp.HttpRequest httpRequest = new global::UnityWebSocketSharp.HttpRequest("GET", targetUri.PathAndQuery);
			global::System.Collections.Specialized.NameValueCollection headers = httpRequest.Headers;
			int port = targetUri.Port;
			string scheme = targetUri.Scheme;
			bool flag = (port == 80 && scheme == "ws") || (port == 443 && scheme == "wss");
			headers["Host"] = ((!flag) ? targetUri.Authority : targetUri.DnsSafeHost);
			headers["Upgrade"] = "websocket";
			headers["Connection"] = "Upgrade";
			return httpRequest;
		}

		internal global::UnityWebSocketSharp.HttpResponse GetResponse(global::System.IO.Stream stream, int millisecondsTimeout)
		{
			WriteTo(stream);
			return global::UnityWebSocketSharp.HttpResponse.ReadResponse(stream, millisecondsTimeout);
		}

		internal static global::UnityWebSocketSharp.HttpRequest Parse(string[] messageHeader)
		{
			int num = messageHeader.Length;
			if (num == 0)
			{
				throw new global::System.ArgumentException("An empty request header.");
			}
			string[] array = messageHeader[0].Split(new char[1] { ' ' }, 3);
			if (array.Length != 3)
			{
				throw new global::System.ArgumentException("It includes an invalid request line.");
			}
			string method = array[0];
			string target = array[1];
			global::System.Version version = array[2].Substring(5).ToVersion();
			global::UnityWebSocketSharp.Net.WebHeaderCollection webHeaderCollection = new global::UnityWebSocketSharp.Net.WebHeaderCollection();
			for (int i = 1; i < num; i++)
			{
				webHeaderCollection.InternalSet(messageHeader[i], response: false);
			}
			return new global::UnityWebSocketSharp.HttpRequest(method, target, version, webHeaderCollection);
		}

		internal static global::UnityWebSocketSharp.HttpRequest ReadRequest(global::System.IO.Stream stream, int millisecondsTimeout)
		{
			return global::UnityWebSocketSharp.HttpBase.Read(stream, Parse, millisecondsTimeout);
		}

		public void SetCookies(global::UnityWebSocketSharp.Net.CookieCollection cookies)
		{
			if (cookies == null || cookies.Count == 0)
			{
				return;
			}
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder(64);
			foreach (global::UnityWebSocketSharp.Net.Cookie item in cookies.Sorted)
			{
				if (!item.Expired)
				{
					stringBuilder.AppendFormat("{0}; ", item);
				}
			}
			int length = stringBuilder.Length;
			if (length > 2)
			{
				stringBuilder.Length = length - 2;
				base.Headers["Cookie"] = stringBuilder.ToString();
			}
		}
	}
}
