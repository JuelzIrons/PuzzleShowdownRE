namespace UnityWebSocketSharp
{
	internal class HttpResponse : global::UnityWebSocketSharp.HttpBase
	{
		private int _code;

		private string _reason;

		internal string StatusLine
		{
			get
			{
				if (_reason == null)
				{
					return $"HTTP/{base.ProtocolVersion} {_code}{(global::UnityWebSocketSharp.HttpBase.CrLf)}";
				}
				return $"HTTP/{base.ProtocolVersion} {_code} {_reason}{(global::UnityWebSocketSharp.HttpBase.CrLf)}";
			}
		}

		public bool CloseConnection
		{
			get
			{
				global::System.StringComparison comparisonTypeForValue = global::System.StringComparison.OrdinalIgnoreCase;
				return base.Headers.Contains("Connection", "close", comparisonTypeForValue);
			}
		}

		public global::UnityWebSocketSharp.Net.CookieCollection Cookies => base.Headers.GetCookies(response: true);

		public bool IsProxyAuthenticationRequired => _code == 407;

		public bool IsRedirect
		{
			get
			{
				if (_code != 301)
				{
					return _code == 302;
				}
				return true;
			}
		}

		public bool IsSuccess
		{
			get
			{
				if (_code >= 200)
				{
					return _code <= 299;
				}
				return false;
			}
		}

		public bool IsUnauthorized => _code == 401;

		public bool IsWebSocketResponse
		{
			get
			{
				if (base.ProtocolVersion > global::UnityWebSocketSharp.Net.HttpVersion.Version10 && _code == 101)
				{
					return base.Headers.Upgrades("websocket");
				}
				return false;
			}
		}

		public override string MessageHeader => StatusLine + base.HeaderSection;

		public string Reason => _reason;

		public int StatusCode => _code;

		private HttpResponse(int code, string reason, global::System.Version version, global::System.Collections.Specialized.NameValueCollection headers)
			: base(version, headers)
		{
			_code = code;
			_reason = reason;
		}

		internal HttpResponse(int code)
			: this(code, code.GetStatusDescription())
		{
		}

		internal HttpResponse(global::UnityWebSocketSharp.Net.HttpStatusCode code)
			: this((int)code)
		{
		}

		internal HttpResponse(int code, string reason)
			: this(code, reason, global::UnityWebSocketSharp.Net.HttpVersion.Version11, new global::System.Collections.Specialized.NameValueCollection())
		{
			base.Headers["Server"] = "websocket-sharp/1.0";
		}

		internal HttpResponse(global::UnityWebSocketSharp.Net.HttpStatusCode code, string reason)
			: this((int)code, reason)
		{
		}

		internal static global::UnityWebSocketSharp.HttpResponse CreateCloseResponse(global::UnityWebSocketSharp.Net.HttpStatusCode code)
		{
			global::UnityWebSocketSharp.HttpResponse httpResponse = new global::UnityWebSocketSharp.HttpResponse(code);
			httpResponse.Headers["Connection"] = "close";
			return httpResponse;
		}

		internal static global::UnityWebSocketSharp.HttpResponse CreateUnauthorizedResponse(string challenge)
		{
			global::UnityWebSocketSharp.HttpResponse httpResponse = new global::UnityWebSocketSharp.HttpResponse(global::UnityWebSocketSharp.Net.HttpStatusCode.Unauthorized);
			httpResponse.Headers["WWW-Authenticate"] = challenge;
			return httpResponse;
		}

		internal static global::UnityWebSocketSharp.HttpResponse CreateWebSocketHandshakeResponse()
		{
			global::UnityWebSocketSharp.HttpResponse httpResponse = new global::UnityWebSocketSharp.HttpResponse(global::UnityWebSocketSharp.Net.HttpStatusCode.SwitchingProtocols);
			global::System.Collections.Specialized.NameValueCollection headers = httpResponse.Headers;
			headers["Upgrade"] = "websocket";
			headers["Connection"] = "Upgrade";
			return httpResponse;
		}

		internal static global::UnityWebSocketSharp.HttpResponse Parse(string[] messageHeader)
		{
			int num = messageHeader.Length;
			if (num == 0)
			{
				throw new global::System.ArgumentException("An empty response header.");
			}
			string[] array = messageHeader[0].Split(new char[1] { ' ' }, 3);
			int num2 = array.Length;
			if (num2 < 2)
			{
				throw new global::System.ArgumentException("It includes an invalid status line.");
			}
			int code = array[1].ToInt32();
			string reason = ((num2 == 3) ? array[2] : null);
			global::System.Version version = array[0].Substring(5).ToVersion();
			global::UnityWebSocketSharp.Net.WebHeaderCollection webHeaderCollection = new global::UnityWebSocketSharp.Net.WebHeaderCollection();
			for (int i = 1; i < num; i++)
			{
				webHeaderCollection.InternalSet(messageHeader[i], response: true);
			}
			return new global::UnityWebSocketSharp.HttpResponse(code, reason, version, webHeaderCollection);
		}

		internal static global::UnityWebSocketSharp.HttpResponse ReadResponse(global::System.IO.Stream stream, int millisecondsTimeout)
		{
			return global::UnityWebSocketSharp.HttpBase.Read(stream, Parse, millisecondsTimeout);
		}

		public void SetCookies(global::UnityWebSocketSharp.Net.CookieCollection cookies)
		{
			if (cookies == null || cookies.Count == 0)
			{
				return;
			}
			global::System.Collections.Specialized.NameValueCollection headers = base.Headers;
			foreach (global::UnityWebSocketSharp.Net.Cookie item in cookies.Sorted)
			{
				string value = item.ToResponseString();
				headers.Add("Set-Cookie", value);
			}
		}
	}
}
