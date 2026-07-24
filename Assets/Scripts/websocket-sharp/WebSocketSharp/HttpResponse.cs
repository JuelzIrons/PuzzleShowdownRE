namespace WebSocketSharp
{
	internal class HttpResponse : global::WebSocketSharp.HttpBase
	{
		private string _code;

		private string _reason;

		public global::WebSocketSharp.Net.CookieCollection Cookies => base.Headers.GetCookies(response: true);

		public bool HasConnectionClose
		{
			get
			{
				global::System.StringComparison comparisonTypeForValue = global::System.StringComparison.OrdinalIgnoreCase;
				return base.Headers.Contains("Connection", "close", comparisonTypeForValue);
			}
		}

		public bool IsProxyAuthenticationRequired => _code == "407";

		public bool IsRedirect => _code == "301" || _code == "302";

		public bool IsUnauthorized => _code == "401";

		public bool IsWebSocketResponse => base.ProtocolVersion > global::WebSocketSharp.Net.HttpVersion.Version10 && _code == "101" && base.Headers.Upgrades("websocket");

		public string Reason => _reason;

		public string StatusCode => _code;

		private HttpResponse(string code, string reason, global::System.Version version, global::System.Collections.Specialized.NameValueCollection headers)
			: base(version, headers)
		{
			_code = code;
			_reason = reason;
		}

		internal HttpResponse(global::WebSocketSharp.Net.HttpStatusCode code)
			: this(code, code.GetDescription())
		{
		}

		internal HttpResponse(global::WebSocketSharp.Net.HttpStatusCode code, string reason)
			: this(((int)code).ToString(), reason, global::WebSocketSharp.Net.HttpVersion.Version11, new global::System.Collections.Specialized.NameValueCollection())
		{
			base.Headers["Server"] = "websocket-sharp/1.0";
		}

		internal static global::WebSocketSharp.HttpResponse CreateCloseResponse(global::WebSocketSharp.Net.HttpStatusCode code)
		{
			global::WebSocketSharp.HttpResponse httpResponse = new global::WebSocketSharp.HttpResponse(code);
			httpResponse.Headers["Connection"] = "close";
			return httpResponse;
		}

		internal static global::WebSocketSharp.HttpResponse CreateUnauthorizedResponse(string challenge)
		{
			global::WebSocketSharp.HttpResponse httpResponse = new global::WebSocketSharp.HttpResponse(global::WebSocketSharp.Net.HttpStatusCode.Unauthorized);
			httpResponse.Headers["WWW-Authenticate"] = challenge;
			return httpResponse;
		}

		internal static global::WebSocketSharp.HttpResponse CreateWebSocketResponse()
		{
			global::WebSocketSharp.HttpResponse httpResponse = new global::WebSocketSharp.HttpResponse(global::WebSocketSharp.Net.HttpStatusCode.SwitchingProtocols);
			global::System.Collections.Specialized.NameValueCollection headers = httpResponse.Headers;
			headers["Upgrade"] = "websocket";
			headers["Connection"] = "Upgrade";
			return httpResponse;
		}

		internal static global::WebSocketSharp.HttpResponse Parse(string[] headerParts)
		{
			string[] array = headerParts[0].Split(new char[1] { ' ' }, 3);
			if (array.Length != 3)
			{
				throw new global::System.ArgumentException("Invalid status line: " + headerParts[0]);
			}
			global::WebSocketSharp.Net.WebHeaderCollection webHeaderCollection = new global::WebSocketSharp.Net.WebHeaderCollection();
			for (int i = 1; i < headerParts.Length; i++)
			{
				webHeaderCollection.InternalSet(headerParts[i], response: true);
			}
			return new global::WebSocketSharp.HttpResponse(array[1], array[2], new global::System.Version(array[0].Substring(5)), webHeaderCollection);
		}

		internal static global::WebSocketSharp.HttpResponse Read(global::System.IO.Stream stream, int millisecondsTimeout)
		{
			return global::WebSocketSharp.HttpBase.Read(stream, Parse, millisecondsTimeout);
		}

		public void SetCookies(global::WebSocketSharp.Net.CookieCollection cookies)
		{
			if (cookies == null || cookies.Count == 0)
			{
				return;
			}
			global::System.Collections.Specialized.NameValueCollection headers = base.Headers;
			foreach (global::WebSocketSharp.Net.Cookie item in cookies.Sorted)
			{
				headers.Add("Set-Cookie", item.ToResponseString());
			}
		}

		public override string ToString()
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder(64);
			stringBuilder.AppendFormat("HTTP/{0} {1} {2}{3}", base.ProtocolVersion, _code, _reason, "\r\n");
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
