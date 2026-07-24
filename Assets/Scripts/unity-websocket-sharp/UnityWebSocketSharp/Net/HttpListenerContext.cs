namespace UnityWebSocketSharp.Net
{
	internal sealed class HttpListenerContext
	{
		private global::UnityWebSocketSharp.Net.HttpConnection _connection;

		private string _errorMessage;

		private int _errorStatusCode;

		private global::UnityWebSocketSharp.Net.HttpListener _listener;

		private global::UnityWebSocketSharp.Net.HttpListenerRequest _request;

		private global::UnityWebSocketSharp.Net.HttpListenerResponse _response;

		private global::System.Security.Principal.IPrincipal _user;

		private global::UnityWebSocketSharp.Net.WebSockets.HttpListenerWebSocketContext _websocketContext;

		internal global::UnityWebSocketSharp.Net.HttpConnection Connection => _connection;

		internal string ErrorMessage
		{
			get
			{
				return _errorMessage;
			}
			set
			{
				_errorMessage = value;
			}
		}

		internal int ErrorStatusCode
		{
			get
			{
				return _errorStatusCode;
			}
			set
			{
				_errorStatusCode = value;
			}
		}

		internal bool HasErrorMessage => _errorMessage != null;

		internal global::UnityWebSocketSharp.Net.HttpListener Listener
		{
			get
			{
				return _listener;
			}
			set
			{
				_listener = value;
			}
		}

		public global::UnityWebSocketSharp.Net.HttpListenerRequest Request => _request;

		public global::UnityWebSocketSharp.Net.HttpListenerResponse Response => _response;

		public global::System.Security.Principal.IPrincipal User => _user;

		internal HttpListenerContext(global::UnityWebSocketSharp.Net.HttpConnection connection)
		{
			_connection = connection;
			_errorStatusCode = 400;
			_request = new global::UnityWebSocketSharp.Net.HttpListenerRequest(this);
			_response = new global::UnityWebSocketSharp.Net.HttpListenerResponse(this);
		}

		private static string createErrorContent(int statusCode, string statusDescription, string message)
		{
			if (message == null || message.Length <= 0)
			{
				return $"<html><body><h1>{statusCode} {statusDescription}</h1></body></html>";
			}
			return $"<html><body><h1>{statusCode} {statusDescription} ({message})</h1></body></html>";
		}

		internal global::UnityWebSocketSharp.Net.WebSockets.HttpListenerWebSocketContext GetWebSocketContext(string protocol)
		{
			_websocketContext = new global::UnityWebSocketSharp.Net.WebSockets.HttpListenerWebSocketContext(this, protocol);
			return _websocketContext;
		}

		internal void SendAuthenticationChallenge(global::UnityWebSocketSharp.Net.AuthenticationSchemes scheme, string realm)
		{
			_response.StatusCode = 401;
			string value = new global::UnityWebSocketSharp.Net.AuthenticationChallenge(scheme, realm).ToString();
			_response.Headers.InternalSet("WWW-Authenticate", value, response: true);
			_response.Close();
		}

		internal void SendError()
		{
			try
			{
				_response.StatusCode = _errorStatusCode;
				_response.ContentType = "text/html";
				string s = createErrorContent(_errorStatusCode, _response.StatusDescription, _errorMessage);
				global::System.Text.Encoding uTF = global::System.Text.Encoding.UTF8;
				byte[] bytes = uTF.GetBytes(s);
				_response.ContentEncoding = uTF;
				_response.ContentLength64 = bytes.LongLength;
				_response.Close(bytes, willBlock: true);
			}
			catch
			{
				_connection.Close(force: true);
			}
		}

		internal void SendError(int statusCode)
		{
			_errorStatusCode = statusCode;
			SendError();
		}

		internal void SendError(int statusCode, string message)
		{
			_errorStatusCode = statusCode;
			_errorMessage = message;
			SendError();
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

		internal void Unregister()
		{
			if (_listener != null)
			{
				_listener.UnregisterContext(this);
			}
		}

		public global::UnityWebSocketSharp.Net.WebSockets.HttpListenerWebSocketContext AcceptWebSocket(string protocol)
		{
			return AcceptWebSocket(protocol, null);
		}

		public global::UnityWebSocketSharp.Net.WebSockets.HttpListenerWebSocketContext AcceptWebSocket(string protocol, global::System.Action<global::UnityWebSocketSharp.WebSocket> initializer)
		{
			if (_websocketContext != null)
			{
				throw new global::System.InvalidOperationException("The method has already been done.");
			}
			if (!_request.IsWebSocketRequest)
			{
				throw new global::System.InvalidOperationException("The request is not a WebSocket handshake request.");
			}
			if (protocol != null)
			{
				if (protocol.Length == 0)
				{
					throw new global::System.ArgumentException("An empty string.", "protocol");
				}
				if (!protocol.IsToken())
				{
					throw new global::System.ArgumentException("It contains an invalid character.", "protocol");
				}
			}
			global::UnityWebSocketSharp.Net.WebSockets.HttpListenerWebSocketContext webSocketContext = GetWebSocketContext(protocol);
			global::UnityWebSocketSharp.WebSocket webSocket = webSocketContext.WebSocket;
			if (initializer != null)
			{
				try
				{
					initializer(webSocket);
				}
				catch (global::System.Exception innerException)
				{
					if (webSocket.ReadyState == global::UnityWebSocketSharp.WebSocketState.New)
					{
						_websocketContext = null;
					}
					throw new global::System.ArgumentException("It caused an exception.", "initializer", innerException);
				}
			}
			webSocket.Accept();
			return webSocketContext;
		}
	}
}
