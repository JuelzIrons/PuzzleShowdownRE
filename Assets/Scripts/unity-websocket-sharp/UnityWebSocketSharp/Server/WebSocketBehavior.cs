namespace UnityWebSocketSharp.Server
{
	internal abstract class WebSocketBehavior : global::UnityWebSocketSharp.Server.IWebSocketSession
	{
		private global::UnityWebSocketSharp.Net.WebSockets.WebSocketContext _context;

		private global::System.Func<global::UnityWebSocketSharp.Net.CookieCollection, global::UnityWebSocketSharp.Net.CookieCollection, bool> _cookiesValidator;

		private bool _emitOnPing;

		private global::System.Func<string, bool> _hostValidator;

		private string _id;

		private bool _ignoreExtensions;

		private global::System.Func<string, bool> _originValidator;

		private string _protocol;

		private global::UnityWebSocketSharp.Server.WebSocketSessionManager _sessions;

		private global::System.DateTime _startTime;

		private global::UnityWebSocketSharp.WebSocket _websocket;

		protected global::System.Collections.Specialized.NameValueCollection Headers
		{
			get
			{
				if (_context == null)
				{
					throw new global::System.InvalidOperationException("The session has not started yet.");
				}
				return _context.Headers;
			}
		}

		protected bool IsAlive
		{
			get
			{
				if (_websocket == null)
				{
					throw new global::System.InvalidOperationException("The session has not started yet.");
				}
				return _websocket.IsAlive;
			}
		}

		protected global::System.Collections.Specialized.NameValueCollection QueryString
		{
			get
			{
				if (_context == null)
				{
					throw new global::System.InvalidOperationException("The session has not started yet.");
				}
				return _context.QueryString;
			}
		}

		protected global::UnityWebSocketSharp.WebSocketState ReadyState
		{
			get
			{
				if (_websocket == null)
				{
					throw new global::System.InvalidOperationException("The session has not started yet.");
				}
				return _websocket.ReadyState;
			}
		}

		protected global::UnityWebSocketSharp.Server.WebSocketSessionManager Sessions
		{
			get
			{
				if (_sessions == null)
				{
					throw new global::System.InvalidOperationException("The session has not started yet.");
				}
				return _sessions;
			}
		}

		protected global::System.Security.Principal.IPrincipal User
		{
			get
			{
				if (_context == null)
				{
					throw new global::System.InvalidOperationException("The session has not started yet.");
				}
				return _context.User;
			}
		}

		protected global::System.Net.IPEndPoint UserEndPoint
		{
			get
			{
				if (_context == null)
				{
					throw new global::System.InvalidOperationException("The session has not started yet.");
				}
				return _context.UserEndPoint;
			}
		}

		public global::System.Func<global::UnityWebSocketSharp.Net.CookieCollection, global::UnityWebSocketSharp.Net.CookieCollection, bool> CookiesValidator
		{
			get
			{
				return _cookiesValidator;
			}
			set
			{
				_cookiesValidator = value;
			}
		}

		public bool EmitOnPing
		{
			get
			{
				if (_websocket == null)
				{
					return _emitOnPing;
				}
				return _websocket.EmitOnPing;
			}
			set
			{
				if (_websocket != null)
				{
					_websocket.EmitOnPing = value;
				}
				else
				{
					_emitOnPing = value;
				}
			}
		}

		public global::System.Func<string, bool> HostValidator
		{
			get
			{
				return _hostValidator;
			}
			set
			{
				_hostValidator = value;
			}
		}

		public string ID => _id;

		public bool IgnoreExtensions
		{
			get
			{
				return _ignoreExtensions;
			}
			set
			{
				_ignoreExtensions = value;
			}
		}

		public global::System.Func<string, bool> OriginValidator
		{
			get
			{
				return _originValidator;
			}
			set
			{
				_originValidator = value;
			}
		}

		public string Protocol
		{
			get
			{
				string protocol;
				if (_websocket == null)
				{
					protocol = _protocol;
					if (protocol == null)
					{
						return string.Empty;
					}
				}
				else
				{
					protocol = _websocket.Protocol;
				}
				return protocol;
			}
			set
			{
				if (_websocket != null)
				{
					throw new global::System.InvalidOperationException("The session has already started.");
				}
				if (value == null || value.Length == 0)
				{
					_protocol = null;
					return;
				}
				if (!value.IsToken())
				{
					throw new global::System.ArgumentException("Not a token.", "value");
				}
				_protocol = value;
			}
		}

		public global::System.DateTime StartTime => _startTime;

		global::UnityWebSocketSharp.WebSocket global::UnityWebSocketSharp.Server.IWebSocketSession.WebSocket => _websocket;

		protected WebSocketBehavior()
		{
			_startTime = global::System.DateTime.MaxValue;
		}

		private string checkHandshakeRequest(global::UnityWebSocketSharp.Net.WebSockets.WebSocketContext context)
		{
			if (_hostValidator != null && !_hostValidator(context.Host))
			{
				return "The Host header is invalid.";
			}
			if (_originValidator != null && !_originValidator(context.Origin))
			{
				return "The Origin header is non-existent or invalid.";
			}
			if (_cookiesValidator != null)
			{
				global::UnityWebSocketSharp.Net.CookieCollection cookieCollection = context.CookieCollection;
				global::UnityWebSocketSharp.Net.CookieCollection cookieCollection2 = context.WebSocket.CookieCollection;
				if (!_cookiesValidator(cookieCollection, cookieCollection2))
				{
					return "The Cookie header is non-existent or invalid.";
				}
			}
			return null;
		}

		private void onClose(object sender, global::UnityWebSocketSharp.CloseEventArgs e)
		{
			if (_id != null)
			{
				_sessions.Remove(_id);
				OnClose(e);
			}
		}

		private void onError(object sender, global::UnityWebSocketSharp.ErrorEventArgs e)
		{
			OnError(e);
		}

		private void onMessage(object sender, global::UnityWebSocketSharp.MessageEventArgs e)
		{
			OnMessage(e);
		}

		private void onOpen(object sender, global::System.EventArgs e)
		{
			_id = _sessions.Add(this);
			if (_id == null)
			{
				_websocket.Close(global::UnityWebSocketSharp.CloseStatusCode.Away);
				return;
			}
			_startTime = global::System.DateTime.Now;
			OnOpen();
		}

		internal void Start(global::UnityWebSocketSharp.Net.WebSockets.WebSocketContext context, global::UnityWebSocketSharp.Server.WebSocketSessionManager sessions)
		{
			_context = context;
			_sessions = sessions;
			_websocket = context.WebSocket;
			_websocket.CustomHandshakeRequestChecker = checkHandshakeRequest;
			_websocket.EmitOnPing = _emitOnPing;
			_websocket.IgnoreExtensions = _ignoreExtensions;
			_websocket.Protocol = _protocol;
			global::System.TimeSpan waitTime = sessions.WaitTime;
			if (waitTime != _websocket.WaitTime)
			{
				_websocket.WaitTime = waitTime;
			}
			_websocket.OnOpen += onOpen;
			_websocket.OnMessage += onMessage;
			_websocket.OnError += onError;
			_websocket.OnClose += onClose;
			_websocket.Accept();
		}

		protected void Close()
		{
			if (_websocket == null)
			{
				throw new global::System.InvalidOperationException("The session has not started yet.");
			}
			_websocket.Close();
		}

		protected void Close(ushort code, string reason)
		{
			if (_websocket == null)
			{
				throw new global::System.InvalidOperationException("The session has not started yet.");
			}
			_websocket.Close(code, reason);
		}

		protected void Close(global::UnityWebSocketSharp.CloseStatusCode code, string reason)
		{
			if (_websocket == null)
			{
				throw new global::System.InvalidOperationException("The session has not started yet.");
			}
			_websocket.Close(code, reason);
		}

		protected void CloseAsync()
		{
			if (_websocket == null)
			{
				throw new global::System.InvalidOperationException("The session has not started yet.");
			}
			_websocket.CloseAsync();
		}

		protected void CloseAsync(ushort code, string reason)
		{
			if (_websocket == null)
			{
				throw new global::System.InvalidOperationException("The session has not started yet.");
			}
			_websocket.CloseAsync(code, reason);
		}

		protected void CloseAsync(global::UnityWebSocketSharp.CloseStatusCode code, string reason)
		{
			if (_websocket == null)
			{
				throw new global::System.InvalidOperationException("The session has not started yet.");
			}
			_websocket.CloseAsync(code, reason);
		}

		protected virtual void OnClose(global::UnityWebSocketSharp.CloseEventArgs e)
		{
		}

		protected virtual void OnError(global::UnityWebSocketSharp.ErrorEventArgs e)
		{
		}

		protected virtual void OnMessage(global::UnityWebSocketSharp.MessageEventArgs e)
		{
		}

		protected virtual void OnOpen()
		{
		}

		protected bool Ping()
		{
			if (_websocket == null)
			{
				throw new global::System.InvalidOperationException("The session has not started yet.");
			}
			return _websocket.Ping();
		}

		protected bool Ping(string message)
		{
			if (_websocket == null)
			{
				throw new global::System.InvalidOperationException("The session has not started yet.");
			}
			return _websocket.Ping(message);
		}

		protected void Send(byte[] data)
		{
			if (_websocket == null)
			{
				throw new global::System.InvalidOperationException("The session has not started yet.");
			}
			_websocket.Send(data);
		}

		protected void Send(global::System.IO.FileInfo fileInfo)
		{
			if (_websocket == null)
			{
				throw new global::System.InvalidOperationException("The session has not started yet.");
			}
			_websocket.Send(fileInfo);
		}

		protected void Send(string data)
		{
			if (_websocket == null)
			{
				throw new global::System.InvalidOperationException("The session has not started yet.");
			}
			_websocket.Send(data);
		}

		protected void Send(global::System.IO.Stream stream, int length)
		{
			if (_websocket == null)
			{
				throw new global::System.InvalidOperationException("The session has not started yet.");
			}
			_websocket.Send(stream, length);
		}

		protected void SendAsync(byte[] data, global::System.Action<bool> completed)
		{
			if (_websocket == null)
			{
				throw new global::System.InvalidOperationException("The session has not started yet.");
			}
			_websocket.SendAsync(data, completed);
		}

		protected void SendAsync(global::System.IO.FileInfo fileInfo, global::System.Action<bool> completed)
		{
			if (_websocket == null)
			{
				throw new global::System.InvalidOperationException("The session has not started yet.");
			}
			_websocket.SendAsync(fileInfo, completed);
		}

		protected void SendAsync(string data, global::System.Action<bool> completed)
		{
			if (_websocket == null)
			{
				throw new global::System.InvalidOperationException("The session has not started yet.");
			}
			_websocket.SendAsync(data, completed);
		}

		protected void SendAsync(global::System.IO.Stream stream, int length, global::System.Action<bool> completed)
		{
			if (_websocket == null)
			{
				throw new global::System.InvalidOperationException("The session has not started yet.");
			}
			_websocket.SendAsync(stream, length, completed);
		}
	}
}
