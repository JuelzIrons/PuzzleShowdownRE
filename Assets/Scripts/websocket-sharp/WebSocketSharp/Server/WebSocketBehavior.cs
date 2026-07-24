namespace WebSocketSharp.Server
{
	public abstract class WebSocketBehavior : global::WebSocketSharp.Server.IWebSocketSession
	{
		private global::WebSocketSharp.Net.WebSockets.WebSocketContext _context;

		private global::System.Func<global::WebSocketSharp.Net.CookieCollection, global::WebSocketSharp.Net.CookieCollection, bool> _cookiesValidator;

		private bool _emitOnPing;

		private string _id;

		private bool _ignoreExtensions;

		private global::System.Func<string, bool> _originValidator;

		private string _protocol;

		private global::WebSocketSharp.Server.WebSocketSessionManager _sessions;

		private global::System.DateTime _startTime;

		private global::WebSocketSharp.WebSocket _websocket;

		protected global::System.Collections.Specialized.NameValueCollection Headers => (_context != null) ? _context.Headers : null;

		protected global::System.Collections.Specialized.NameValueCollection QueryString => (_context != null) ? _context.QueryString : null;

		protected global::WebSocketSharp.Server.WebSocketSessionManager Sessions => _sessions;

		public global::WebSocketSharp.WebSocketState ConnectionState => (_websocket != null) ? _websocket.ReadyState : global::WebSocketSharp.WebSocketState.Connecting;

		public global::WebSocketSharp.Net.WebSockets.WebSocketContext Context => _context;

		public global::System.Func<global::WebSocketSharp.Net.CookieCollection, global::WebSocketSharp.Net.CookieCollection, bool> CookiesValidator
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
				return (_websocket != null) ? _websocket.EmitOnPing : _emitOnPing;
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
				return (_websocket != null) ? _websocket.Protocol : (_protocol ?? string.Empty);
			}
			set
			{
				if (_websocket != null)
				{
					string message = "The session has already started.";
					throw new global::System.InvalidOperationException(message);
				}
				if (value == null || value.Length == 0)
				{
					_protocol = null;
					return;
				}
				if (!value.IsToken())
				{
					string message2 = "It is not a token.";
					throw new global::System.ArgumentException(message2, "value");
				}
				_protocol = value;
			}
		}

		public global::System.DateTime StartTime => _startTime;

		protected WebSocketBehavior()
		{
			_startTime = global::System.DateTime.MaxValue;
		}

		private string checkHandshakeRequest(global::WebSocketSharp.Net.WebSockets.WebSocketContext context)
		{
			if (_originValidator != null && !_originValidator(context.Origin))
			{
				return "It includes no Origin header or an invalid one.";
			}
			if (_cookiesValidator != null)
			{
				global::WebSocketSharp.Net.CookieCollection cookieCollection = context.CookieCollection;
				global::WebSocketSharp.Net.CookieCollection cookieCollection2 = context.WebSocket.CookieCollection;
				if (!_cookiesValidator(cookieCollection, cookieCollection2))
				{
					return "It includes no cookie or an invalid one.";
				}
			}
			return null;
		}

		private void onClose(object sender, global::WebSocketSharp.CloseEventArgs e)
		{
			if (_id != null)
			{
				_sessions.Remove(_id);
				OnClose(e);
			}
		}

		private void onError(object sender, global::WebSocketSharp.ErrorEventArgs e)
		{
			OnError(e);
		}

		private void onMessage(object sender, global::WebSocketSharp.MessageEventArgs e)
		{
			OnMessage(e);
		}

		private void onOpen(object sender, global::System.EventArgs e)
		{
			_id = _sessions.Add(this);
			if (_id == null)
			{
				_websocket.Close(global::WebSocketSharp.CloseStatusCode.Away);
				return;
			}
			_startTime = global::System.DateTime.Now;
			OnOpen();
		}

		internal void Start(global::WebSocketSharp.Net.WebSockets.WebSocketContext context, global::WebSocketSharp.Server.WebSocketSessionManager sessions)
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
			_websocket.InternalAccept();
		}

		protected void Close()
		{
			if (_websocket == null)
			{
				string message = "The session has not started yet.";
				throw new global::System.InvalidOperationException(message);
			}
			_websocket.Close();
		}

		protected void Close(ushort code, string reason)
		{
			if (_websocket == null)
			{
				string message = "The session has not started yet.";
				throw new global::System.InvalidOperationException(message);
			}
			_websocket.Close(code, reason);
		}

		protected void Close(global::WebSocketSharp.CloseStatusCode code, string reason)
		{
			if (_websocket == null)
			{
				string message = "The session has not started yet.";
				throw new global::System.InvalidOperationException(message);
			}
			_websocket.Close(code, reason);
		}

		protected void CloseAsync()
		{
			if (_websocket == null)
			{
				string message = "The session has not started yet.";
				throw new global::System.InvalidOperationException(message);
			}
			_websocket.CloseAsync();
		}

		protected void CloseAsync(ushort code, string reason)
		{
			if (_websocket == null)
			{
				string message = "The session has not started yet.";
				throw new global::System.InvalidOperationException(message);
			}
			_websocket.CloseAsync(code, reason);
		}

		protected void CloseAsync(global::WebSocketSharp.CloseStatusCode code, string reason)
		{
			if (_websocket == null)
			{
				string message = "The session has not started yet.";
				throw new global::System.InvalidOperationException(message);
			}
			_websocket.CloseAsync(code, reason);
		}

		protected virtual void OnClose(global::WebSocketSharp.CloseEventArgs e)
		{
		}

		protected virtual void OnError(global::WebSocketSharp.ErrorEventArgs e)
		{
		}

		protected virtual void OnMessage(global::WebSocketSharp.MessageEventArgs e)
		{
		}

		protected virtual void OnOpen()
		{
		}

		protected bool Ping()
		{
			if (_websocket == null)
			{
				string message = "The session has not started yet.";
				throw new global::System.InvalidOperationException(message);
			}
			return _websocket.Ping();
		}

		protected bool Ping(string message)
		{
			if (_websocket == null)
			{
				string message2 = "The session has not started yet.";
				throw new global::System.InvalidOperationException(message2);
			}
			return _websocket.Ping(message);
		}

		protected void Send(byte[] data)
		{
			if (_websocket == null)
			{
				string message = "The current state of the connection is not Open.";
				throw new global::System.InvalidOperationException(message);
			}
			_websocket.Send(data);
		}

		protected void Send(global::System.IO.FileInfo fileInfo)
		{
			if (_websocket == null)
			{
				string message = "The current state of the connection is not Open.";
				throw new global::System.InvalidOperationException(message);
			}
			_websocket.Send(fileInfo);
		}

		protected void Send(string data)
		{
			if (_websocket == null)
			{
				string message = "The current state of the connection is not Open.";
				throw new global::System.InvalidOperationException(message);
			}
			_websocket.Send(data);
		}

		protected void Send(global::System.IO.Stream stream, int length)
		{
			if (_websocket == null)
			{
				string message = "The current state of the connection is not Open.";
				throw new global::System.InvalidOperationException(message);
			}
			_websocket.Send(stream, length);
		}

		protected void SendAsync(byte[] data, global::System.Action<bool> completed)
		{
			if (_websocket == null)
			{
				string message = "The current state of the connection is not Open.";
				throw new global::System.InvalidOperationException(message);
			}
			_websocket.SendAsync(data, completed);
		}

		protected void SendAsync(global::System.IO.FileInfo fileInfo, global::System.Action<bool> completed)
		{
			if (_websocket == null)
			{
				string message = "The current state of the connection is not Open.";
				throw new global::System.InvalidOperationException(message);
			}
			_websocket.SendAsync(fileInfo, completed);
		}

		protected void SendAsync(string data, global::System.Action<bool> completed)
		{
			if (_websocket == null)
			{
				string message = "The current state of the connection is not Open.";
				throw new global::System.InvalidOperationException(message);
			}
			_websocket.SendAsync(data, completed);
		}

		protected void SendAsync(global::System.IO.Stream stream, int length, global::System.Action<bool> completed)
		{
			if (_websocket == null)
			{
				string message = "The current state of the connection is not Open.";
				throw new global::System.InvalidOperationException(message);
			}
			_websocket.SendAsync(stream, length, completed);
		}
	}
}
