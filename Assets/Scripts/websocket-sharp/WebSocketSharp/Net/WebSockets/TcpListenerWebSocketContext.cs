namespace WebSocketSharp.Net.WebSockets
{
	internal class TcpListenerWebSocketContext : global::WebSocketSharp.Net.WebSockets.WebSocketContext
	{
		private global::WebSocketSharp.Logger _log;

		private global::System.Collections.Specialized.NameValueCollection _queryString;

		private global::WebSocketSharp.HttpRequest _request;

		private global::System.Uri _requestUri;

		private bool _secure;

		private global::System.Net.EndPoint _serverEndPoint;

		private global::System.IO.Stream _stream;

		private global::System.Net.Sockets.TcpClient _tcpClient;

		private global::System.Security.Principal.IPrincipal _user;

		private global::System.Net.EndPoint _userEndPoint;

		private global::WebSocketSharp.WebSocket _websocket;

		internal global::WebSocketSharp.Logger Log => _log;

		internal global::System.IO.Stream Stream => _stream;

		public override global::WebSocketSharp.Net.CookieCollection CookieCollection => _request.Cookies;

		public override global::System.Collections.Specialized.NameValueCollection Headers => _request.Headers;

		public override string Host => _request.Headers["Host"];

		public override bool IsAuthenticated => _user != null;

		public override bool IsLocal => UserEndPoint.Address.IsLocal();

		public override bool IsSecureConnection => _secure;

		public override bool IsWebSocketRequest => _request.IsWebSocketRequest;

		public override string Origin => _request.Headers["Origin"];

		public override global::System.Collections.Specialized.NameValueCollection QueryString
		{
			get
			{
				if (_queryString == null)
				{
					global::System.Uri requestUri = RequestUri;
					_queryString = global::WebSocketSharp.Net.QueryStringCollection.Parse((requestUri != null) ? requestUri.Query : null, global::System.Text.Encoding.UTF8);
				}
				return _queryString;
			}
		}

		public override global::System.Uri RequestUri
		{
			get
			{
				if (_requestUri == null)
				{
					_requestUri = global::WebSocketSharp.Net.HttpUtility.CreateRequestUrl(_request.RequestUri, _request.Headers["Host"], _request.IsWebSocketRequest, _secure);
				}
				return _requestUri;
			}
		}

		public override string SecWebSocketKey => _request.Headers["Sec-WebSocket-Key"];

		public override global::System.Collections.Generic.IEnumerable<string> SecWebSocketProtocols
		{
			get
			{
				string val = _request.Headers["Sec-WebSocket-Protocol"];
				if (val == null || val.Length == 0)
				{
					yield break;
				}
				string[] array = val.Split(new char[1] { ',' });
				foreach (string elm in array)
				{
					string protocol = elm.Trim();
					if (protocol.Length != 0)
					{
						yield return protocol;
					}
				}
			}
		}

		public override string SecWebSocketVersion => _request.Headers["Sec-WebSocket-Version"];

		public override global::System.Net.IPEndPoint ServerEndPoint => (global::System.Net.IPEndPoint)_serverEndPoint;

		public override global::System.Security.Principal.IPrincipal User => _user;

		public override global::System.Net.IPEndPoint UserEndPoint => (global::System.Net.IPEndPoint)_userEndPoint;

		public override global::WebSocketSharp.WebSocket WebSocket => _websocket;

		internal TcpListenerWebSocketContext(global::System.Net.Sockets.TcpClient tcpClient, string protocol, bool secure, global::WebSocketSharp.Net.ServerSslConfiguration sslConfig, global::WebSocketSharp.Logger log)
		{
			_tcpClient = tcpClient;
			_secure = secure;
			_log = log;
			global::System.Net.Sockets.NetworkStream stream = tcpClient.GetStream();
			if (secure)
			{
				global::System.Net.Security.SslStream sslStream = new global::System.Net.Security.SslStream(stream, leaveInnerStreamOpen: false, sslConfig.ClientCertificateValidationCallback);
				sslStream.AuthenticateAsServer(sslConfig.ServerCertificate, sslConfig.ClientCertificateRequired, sslConfig.EnabledSslProtocols, sslConfig.CheckCertificateRevocation);
				_stream = sslStream;
			}
			else
			{
				_stream = stream;
			}
			global::System.Net.Sockets.Socket client = tcpClient.Client;
			_serverEndPoint = client.LocalEndPoint;
			_userEndPoint = client.RemoteEndPoint;
			_request = global::WebSocketSharp.HttpRequest.Read(_stream, 90000);
			_websocket = new global::WebSocketSharp.WebSocket(this, protocol);
		}

		private global::WebSocketSharp.HttpRequest sendAuthenticationChallenge(string challenge)
		{
			global::WebSocketSharp.HttpResponse httpResponse = global::WebSocketSharp.HttpResponse.CreateUnauthorizedResponse(challenge);
			byte[] array = httpResponse.ToByteArray();
			_stream.Write(array, 0, array.Length);
			return global::WebSocketSharp.HttpRequest.Read(_stream, 15000);
		}

		internal bool Authenticate(global::WebSocketSharp.Net.AuthenticationSchemes scheme, string realm, global::System.Func<global::System.Security.Principal.IIdentity, global::WebSocketSharp.Net.NetworkCredential> credentialsFinder)
		{
			string chal = new global::WebSocketSharp.Net.AuthenticationChallenge(scheme, realm).ToString();
			int retry = -1;
			global::System.Func<bool> auth = null;
			auth = delegate
			{
				retry++;
				if (retry > 99)
				{
					return false;
				}
				global::System.Security.Principal.IPrincipal principal = global::WebSocketSharp.Net.HttpUtility.CreateUser(_request.Headers["Authorization"], scheme, realm, _request.HttpMethod, credentialsFinder);
				if (principal != null && principal.Identity.IsAuthenticated)
				{
					_user = principal;
					return true;
				}
				_request = sendAuthenticationChallenge(chal);
				return auth();
			};
			return auth();
		}

		internal void Close()
		{
			_stream.Close();
			_tcpClient.Close();
		}

		internal void Close(global::WebSocketSharp.Net.HttpStatusCode code)
		{
			global::WebSocketSharp.HttpResponse httpResponse = global::WebSocketSharp.HttpResponse.CreateCloseResponse(code);
			byte[] array = httpResponse.ToByteArray();
			_stream.Write(array, 0, array.Length);
			_stream.Close();
			_tcpClient.Close();
		}

		public override string ToString()
		{
			return _request.ToString();
		}
	}
}
