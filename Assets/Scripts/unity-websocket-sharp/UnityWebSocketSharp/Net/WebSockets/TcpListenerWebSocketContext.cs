namespace UnityWebSocketSharp.Net.WebSockets
{
	internal class TcpListenerWebSocketContext : global::UnityWebSocketSharp.Net.WebSockets.WebSocketContext
	{
		private global::UnityWebSocketSharp.Logger _log;

		private global::System.Collections.Specialized.NameValueCollection _queryString;

		private global::UnityWebSocketSharp.HttpRequest _request;

		private global::System.Uri _requestUri;

		private bool _secure;

		private global::System.Net.EndPoint _serverEndPoint;

		private global::System.IO.Stream _stream;

		private global::System.Net.Sockets.TcpClient _tcpClient;

		private global::System.Security.Principal.IPrincipal _user;

		private global::System.Net.EndPoint _userEndPoint;

		private global::UnityWebSocketSharp.WebSocket _websocket;

		internal global::UnityWebSocketSharp.Logger Log => _log;

		internal global::System.IO.Stream Stream => _stream;

		public override global::UnityWebSocketSharp.Net.CookieCollection CookieCollection => _request.Cookies;

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
					string query = ((requestUri != null) ? requestUri.Query : null);
					_queryString = global::UnityWebSocketSharp.Net.QueryStringCollection.Parse(query, global::System.Text.Encoding.UTF8);
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
					_requestUri = global::UnityWebSocketSharp.Net.HttpUtility.CreateRequestUrl(_request.RequestTarget, _request.Headers["Host"], _request.IsWebSocketRequest, _secure);
				}
				return _requestUri;
			}
		}

		public override string SecWebSocketKey => _request.Headers["Sec-WebSocket-Key"];

		public override global::System.Collections.Generic.IEnumerable<string> SecWebSocketProtocols
		{
			get
			{
				string text = _request.Headers["Sec-WebSocket-Protocol"];
				if (text == null || text.Length == 0)
				{
					yield break;
				}
				string[] array = text.Split(',');
				for (int i = 0; i < array.Length; i++)
				{
					string text2 = array[i].Trim();
					if (text2.Length != 0)
					{
						yield return text2;
					}
				}
			}
		}

		public override string SecWebSocketVersion => _request.Headers["Sec-WebSocket-Version"];

		public override global::System.Net.IPEndPoint ServerEndPoint => (global::System.Net.IPEndPoint)_serverEndPoint;

		public override global::System.Security.Principal.IPrincipal User => _user;

		public override global::System.Net.IPEndPoint UserEndPoint => (global::System.Net.IPEndPoint)_userEndPoint;

		public override global::UnityWebSocketSharp.WebSocket WebSocket => _websocket;

		internal TcpListenerWebSocketContext(global::System.Net.Sockets.TcpClient tcpClient, string protocol, bool secure, global::UnityWebSocketSharp.Net.ServerSslConfiguration sslConfig, global::UnityWebSocketSharp.Logger log)
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
			_request = global::UnityWebSocketSharp.HttpRequest.ReadRequest(_stream, 90000);
			_websocket = new global::UnityWebSocketSharp.WebSocket(this, protocol);
		}

		internal void Close()
		{
			_stream.Close();
			_tcpClient.Close();
		}

		internal void Close(global::UnityWebSocketSharp.Net.HttpStatusCode code)
		{
			global::UnityWebSocketSharp.HttpResponse.CreateCloseResponse(code).WriteTo(_stream);
			_stream.Close();
			_tcpClient.Close();
		}

		internal void SendAuthenticationChallenge(string challenge)
		{
			global::UnityWebSocketSharp.HttpResponse.CreateUnauthorizedResponse(challenge).WriteTo(_stream);
			_request = global::UnityWebSocketSharp.HttpRequest.ReadRequest(_stream, 15000);
		}

		internal bool SetUser(global::UnityWebSocketSharp.Net.AuthenticationSchemes scheme, string realm, global::System.Func<global::System.Security.Principal.IIdentity, global::UnityWebSocketSharp.Net.NetworkCredential> credentialsFinder)
		{
			global::System.Security.Principal.IPrincipal principal = global::UnityWebSocketSharp.Net.HttpUtility.CreateUser(_request.Headers["Authorization"], scheme, realm, _request.HttpMethod, credentialsFinder);
			if (principal == null)
			{
				return false;
			}
			if (!principal.Identity.IsAuthenticated)
			{
				return false;
			}
			_user = principal;
			return true;
		}

		public override string ToString()
		{
			return _request.ToString();
		}
	}
}
