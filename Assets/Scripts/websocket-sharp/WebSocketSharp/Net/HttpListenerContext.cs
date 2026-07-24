namespace WebSocketSharp.Net
{
	public sealed class HttpListenerContext
	{
		private global::WebSocketSharp.Net.HttpConnection _connection;

		private string _errorMessage;

		private int _errorStatusCode;

		private global::WebSocketSharp.Net.HttpListener _listener;

		private global::WebSocketSharp.Net.HttpListenerRequest _request;

		private global::WebSocketSharp.Net.HttpListenerResponse _response;

		private global::System.Security.Principal.IPrincipal _user;

		private global::WebSocketSharp.Net.WebSockets.HttpListenerWebSocketContext _websocketContext;

		internal global::WebSocketSharp.Net.HttpConnection Connection => _connection;

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

		internal global::WebSocketSharp.Net.HttpListener Listener
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

		public global::WebSocketSharp.Net.HttpListenerRequest Request => _request;

		public global::WebSocketSharp.Net.HttpListenerResponse Response => _response;

		public global::System.Security.Principal.IPrincipal User
		{
			get
			{
				return _user;
			}
			internal set
			{
				_user = value;
			}
		}

		internal HttpListenerContext(global::WebSocketSharp.Net.HttpConnection connection)
		{
			_connection = connection;
			_errorStatusCode = 400;
			_request = new global::WebSocketSharp.Net.HttpListenerRequest(this);
			_response = new global::WebSocketSharp.Net.HttpListenerResponse(this);
		}

		private static string createErrorContent(int statusCode, string statusDescription, string message)
		{
			return (message != null && message.Length > 0) ? $"<html><body><h1>{statusCode} {statusDescription} ({message})</h1></body></html>" : $"<html><body><h1>{statusCode} {statusDescription}</h1></body></html>";
		}

		internal global::WebSocketSharp.Net.WebSockets.HttpListenerWebSocketContext GetWebSocketContext(string protocol)
		{
			_websocketContext = new global::WebSocketSharp.Net.WebSockets.HttpListenerWebSocketContext(this, protocol);
			return _websocketContext;
		}

		internal void SendAuthenticationChallenge(global::WebSocketSharp.Net.AuthenticationSchemes scheme, string realm)
		{
			string value = new global::WebSocketSharp.Net.AuthenticationChallenge(scheme, realm).ToString();
			_response.StatusCode = 401;
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

		internal void Unregister()
		{
			if (_listener != null)
			{
				_listener.UnregisterContext(this);
			}
		}

		public global::WebSocketSharp.Net.WebSockets.HttpListenerWebSocketContext AcceptWebSocket(string protocol)
		{
			if (_websocketContext != null)
			{
				string message = "The accepting is already in progress.";
				throw new global::System.InvalidOperationException(message);
			}
			if (protocol != null)
			{
				if (protocol.Length == 0)
				{
					string message2 = "An empty string.";
					throw new global::System.ArgumentException(message2, "protocol");
				}
				if (!protocol.IsToken())
				{
					string message3 = "It contains an invalid character.";
					throw new global::System.ArgumentException(message3, "protocol");
				}
			}
			return GetWebSocketContext(protocol);
		}
	}
}
