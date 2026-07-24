namespace UnityWebSocketSharp
{
	internal class WebSocket : global::System.IDisposable
	{
		private global::UnityWebSocketSharp.Net.AuthenticationChallenge _authChallenge;

		private string _base64Key;

		private bool _client;

		private global::System.Action _closeContext;

		private global::UnityWebSocketSharp.CompressionMethod _compression;

		private global::UnityWebSocketSharp.Net.WebSockets.WebSocketContext _context;

		private global::UnityWebSocketSharp.Net.CookieCollection _cookies;

		private global::UnityWebSocketSharp.Net.NetworkCredential _credentials;

		private bool _emitOnPing;

		private bool _enableRedirection;

		private string _extensions;

		private bool _extensionsRequested;

		private object _forMessageEventQueue;

		private object _forPing;

		private object _forSend;

		private object _forState;

		private global::System.IO.MemoryStream _fragmentsBuffer;

		private bool _fragmentsCompressed;

		private global::UnityWebSocketSharp.Opcode _fragmentsOpcode;

		private const string _guid = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";

		private global::System.Func<global::UnityWebSocketSharp.Net.WebSockets.WebSocketContext, string> _handshakeRequestChecker;

		private bool _ignoreExtensions;

		private bool _inContinuation;

		private volatile bool _inMessage;

		private volatile global::UnityWebSocketSharp.Logger _log;

		private static readonly int _maxRetryCountForConnect;

		private global::System.Action<global::UnityWebSocketSharp.MessageEventArgs> _message;

		private global::System.Collections.Generic.Queue<global::UnityWebSocketSharp.MessageEventArgs> _messageEventQueue;

		private uint _nonceCount;

		private string _origin;

		private global::System.Threading.ManualResetEvent _pongReceived;

		private bool _preAuth;

		private string _protocol;

		private string[] _protocols;

		private bool _protocolsRequested;

		private global::UnityWebSocketSharp.Net.NetworkCredential _proxyCredentials;

		private global::System.Uri _proxyUri;

		private volatile global::UnityWebSocketSharp.WebSocketState _readyState;

		private global::System.Threading.ManualResetEvent _receivingExited;

		private int _retryCountForConnect;

		private bool _secure;

		private global::UnityWebSocketSharp.Net.ClientSslConfiguration _sslConfig;

		private global::System.IO.Stream _stream;

		private global::System.Uri _uri;

		private const string _version = "13";

		private global::System.TimeSpan _waitTime;

		internal static readonly byte[] EmptyBytes;

		internal static readonly int FragmentLength;

		internal static readonly global::System.Security.Cryptography.RandomNumberGenerator RandomNumber;

		internal global::UnityWebSocketSharp.Net.CookieCollection CookieCollection => _cookies;

		internal global::System.Func<global::UnityWebSocketSharp.Net.WebSockets.WebSocketContext, string> CustomHandshakeRequestChecker
		{
			get
			{
				return _handshakeRequestChecker;
			}
			set
			{
				_handshakeRequestChecker = value;
			}
		}

		internal global::System.Func<string, int, global::System.IO.Stream> GetNetworkStream { get; set; }

		internal bool IgnoreExtensions
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

		public global::UnityWebSocketSharp.CompressionMethod Compression
		{
			get
			{
				return _compression;
			}
			set
			{
				if (!_client)
				{
					throw new global::System.InvalidOperationException("The interface is not for the client.");
				}
				lock (_forState)
				{
					if (canSet())
					{
						_compression = value;
					}
				}
			}
		}

		public global::System.Collections.Generic.IEnumerable<global::UnityWebSocketSharp.Net.Cookie> Cookies
		{
			get
			{
				lock (_cookies.SyncRoot)
				{
					foreach (global::UnityWebSocketSharp.Net.Cookie cookie in _cookies)
					{
						yield return cookie;
					}
				}
			}
		}

		public global::UnityWebSocketSharp.Net.NetworkCredential Credentials => _credentials;

		public bool EmitOnPing
		{
			get
			{
				return _emitOnPing;
			}
			set
			{
				_emitOnPing = value;
			}
		}

		public bool EnableRedirection
		{
			get
			{
				return _enableRedirection;
			}
			set
			{
				if (!_client)
				{
					throw new global::System.InvalidOperationException("The interface is not for the client.");
				}
				lock (_forState)
				{
					if (canSet())
					{
						_enableRedirection = value;
					}
				}
			}
		}

		public string Extensions => _extensions ?? string.Empty;

		public bool IsAlive => ping(EmptyBytes);

		public bool IsSecure => _secure;

		public global::UnityWebSocketSharp.Logger Log
		{
			get
			{
				return _log;
			}
			internal set
			{
				_log = value;
			}
		}

		public string Origin
		{
			get
			{
				return _origin;
			}
			set
			{
				if (!_client)
				{
					throw new global::System.InvalidOperationException("The interface is not for the client.");
				}
				if (!value.IsNullOrEmpty())
				{
					if (!global::System.Uri.TryCreate(value, global::System.UriKind.Absolute, out var result))
					{
						throw new global::System.ArgumentException("Not an absolute URI string.", "value");
					}
					if (result.Segments.Length > 1)
					{
						throw new global::System.ArgumentException("It includes the path segments.", "value");
					}
				}
				lock (_forState)
				{
					if (canSet())
					{
						_origin = ((!value.IsNullOrEmpty()) ? value.TrimEnd('/') : value);
					}
				}
			}
		}

		public string Protocol
		{
			get
			{
				return _protocol ?? string.Empty;
			}
			internal set
			{
				_protocol = value;
			}
		}

		public global::UnityWebSocketSharp.WebSocketState ReadyState => _readyState;

		public global::UnityWebSocketSharp.Net.ClientSslConfiguration SslConfiguration
		{
			get
			{
				if (!_client)
				{
					throw new global::System.InvalidOperationException("The interface is not for the client.");
				}
				if (!_secure)
				{
					throw new global::System.InvalidOperationException("The interface does not use a secure connection.");
				}
				return getSslConfiguration();
			}
		}

		public global::System.Uri Url
		{
			get
			{
				if (!_client)
				{
					return _context.RequestUri;
				}
				return _uri;
			}
		}

		public global::System.TimeSpan WaitTime
		{
			get
			{
				return _waitTime;
			}
			set
			{
				if (value <= global::System.TimeSpan.Zero)
				{
					string text = "Zero or less.";
					throw new global::System.ArgumentOutOfRangeException("value", text);
				}
				lock (_forState)
				{
					if (canSet())
					{
						_waitTime = value;
					}
				}
			}
		}

		public event global::System.EventHandler<global::UnityWebSocketSharp.CloseEventArgs> OnClose;

		public event global::System.EventHandler<global::UnityWebSocketSharp.ErrorEventArgs> OnError;

		public event global::System.EventHandler<global::UnityWebSocketSharp.MessageEventArgs> OnMessage;

		public event global::System.EventHandler OnOpen;

		static WebSocket()
		{
			_maxRetryCountForConnect = 10;
			EmptyBytes = new byte[0];
			FragmentLength = 1016;
			RandomNumber = new global::System.Security.Cryptography.RNGCryptoServiceProvider();
		}

		internal WebSocket(global::UnityWebSocketSharp.Net.WebSockets.HttpListenerWebSocketContext context, string protocol)
		{
			_context = context;
			_protocol = protocol;
			_closeContext = context.Close;
			_log = context.Log;
			_message = messages;
			_secure = context.IsSecureConnection;
			_stream = context.Stream;
			_waitTime = global::System.TimeSpan.FromSeconds(1.0);
			init();
		}

		internal WebSocket(global::UnityWebSocketSharp.Net.WebSockets.TcpListenerWebSocketContext context, string protocol)
		{
			_context = context;
			_protocol = protocol;
			_closeContext = context.Close;
			_log = context.Log;
			_message = messages;
			_secure = context.IsSecureConnection;
			_stream = context.Stream;
			_waitTime = global::System.TimeSpan.FromSeconds(1.0);
			init();
		}

		public WebSocket(string url, params string[] protocols)
		{
			if (url == null)
			{
				throw new global::System.ArgumentNullException("url");
			}
			if (url.Length == 0)
			{
				throw new global::System.ArgumentException("An empty string.", "url");
			}
			if (!url.TryCreateWebSocketUri(out _uri, out var text))
			{
				throw new global::System.ArgumentException(text, "url");
			}
			if (protocols != null && protocols.Length != 0)
			{
				if (!checkProtocols(protocols, out text))
				{
					throw new global::System.ArgumentException(text, "protocols");
				}
				_protocols = protocols;
			}
			_base64Key = CreateBase64Key();
			_client = true;
			_log = new global::UnityWebSocketSharp.Logger();
			_message = messagec;
			_retryCountForConnect = -1;
			_secure = _uri.Scheme == "wss";
			_waitTime = global::System.TimeSpan.FromSeconds(5.0);
			init();
		}

		private void abort(string reason, global::System.Exception exception)
		{
			ushort code = (ushort)((exception is global::UnityWebSocketSharp.WebSocketException) ? ((global::UnityWebSocketSharp.WebSocketException)exception).Code : 1006);
			abort(code, reason);
		}

		private void abort(ushort code, string reason)
		{
			global::UnityWebSocketSharp.PayloadData payloadData = new global::UnityWebSocketSharp.PayloadData(code, reason);
			close(payloadData, send: false, received: false);
		}

		private bool accept()
		{
			lock (_forState)
			{
				if (_readyState == global::UnityWebSocketSharp.WebSocketState.Open)
				{
					_log.Trace("The connection has already been established.");
					return false;
				}
				if (_readyState == global::UnityWebSocketSharp.WebSocketState.Closing)
				{
					_log.Error("The close process is in progress.");
					error("An error has occurred before accepting.", null);
					return false;
				}
				if (_readyState == global::UnityWebSocketSharp.WebSocketState.Closed)
				{
					_log.Error("The connection has been closed.");
					error("An error has occurred before accepting.", null);
					return false;
				}
				_readyState = global::UnityWebSocketSharp.WebSocketState.Connecting;
				bool flag = false;
				try
				{
					flag = acceptHandshake();
				}
				catch (global::System.Exception ex)
				{
					_log.Fatal(ex.Message);
					_log.Debug(ex.ToString());
					abort(1011, "An exception has occurred while accepting.");
				}
				if (!flag)
				{
					return false;
				}
				_readyState = global::UnityWebSocketSharp.WebSocketState.Open;
				return true;
			}
		}

		private bool acceptHandshake()
		{
			if (!checkHandshakeRequest(_context, out var text))
			{
				_log.Error(text);
				_log.Debug(_context.ToString());
				refuseHandshake(1002, "A handshake error has occurred.");
				return false;
			}
			if (!customCheckHandshakeRequest(_context, out text))
			{
				_log.Error(text);
				_log.Debug(_context.ToString());
				refuseHandshake(1002, "A handshake error has occurred.");
				return false;
			}
			_base64Key = _context.Headers["Sec-WebSocket-Key"];
			if (_protocol != null && !_context.SecWebSocketProtocols.Contains((string p) => p == _protocol))
			{
				_protocol = null;
			}
			if (!_ignoreExtensions)
			{
				string value = _context.Headers["Sec-WebSocket-Extensions"];
				processSecWebSocketExtensionsClientHeader(value);
			}
			createHandshakeResponse().WriteTo(_stream);
			return true;
		}

		private bool canSet()
		{
			if (_readyState != global::UnityWebSocketSharp.WebSocketState.New)
			{
				return _readyState == global::UnityWebSocketSharp.WebSocketState.Closed;
			}
			return true;
		}

		private bool checkHandshakeRequest(global::UnityWebSocketSharp.Net.WebSockets.WebSocketContext context, out string message)
		{
			message = null;
			if (!context.IsWebSocketRequest)
			{
				message = "Not a WebSocket handshake request.";
				return false;
			}
			global::System.Collections.Specialized.NameValueCollection headers = context.Headers;
			string text = headers["Sec-WebSocket-Key"];
			if (text == null)
			{
				message = "The Sec-WebSocket-Key header is non-existent.";
				return false;
			}
			if (text.Length == 0)
			{
				message = "The Sec-WebSocket-Key header is invalid.";
				return false;
			}
			string text2 = headers["Sec-WebSocket-Version"];
			if (text2 == null)
			{
				message = "The Sec-WebSocket-Version header is non-existent.";
				return false;
			}
			if (text2 != "13")
			{
				message = "The Sec-WebSocket-Version header is invalid.";
				return false;
			}
			string text3 = headers["Sec-WebSocket-Protocol"];
			if (text3 != null && text3.Length == 0)
			{
				message = "The Sec-WebSocket-Protocol header is invalid.";
				return false;
			}
			if (!_ignoreExtensions)
			{
				string text4 = headers["Sec-WebSocket-Extensions"];
				if (text4 != null && text4.Length == 0)
				{
					message = "The Sec-WebSocket-Extensions header is invalid.";
					return false;
				}
			}
			return true;
		}

		private bool checkHandshakeResponse(global::UnityWebSocketSharp.HttpResponse response, out string message)
		{
			message = null;
			if (response.IsRedirect)
			{
				message = "The redirection is indicated.";
				return false;
			}
			if (response.IsUnauthorized)
			{
				message = "The authentication is required.";
				return false;
			}
			if (!response.IsWebSocketResponse)
			{
				message = "Not a WebSocket handshake response.";
				return false;
			}
			global::System.Collections.Specialized.NameValueCollection headers = response.Headers;
			string text = headers["Sec-WebSocket-Accept"];
			if (text == null)
			{
				message = "The Sec-WebSocket-Accept header is non-existent.";
				return false;
			}
			if (text != CreateResponseKey(_base64Key))
			{
				message = "The Sec-WebSocket-Accept header is invalid.";
				return false;
			}
			string text2 = headers["Sec-WebSocket-Version"];
			if (text2 != null && text2 != "13")
			{
				message = "The Sec-WebSocket-Version header is invalid.";
				return false;
			}
			string subp = headers["Sec-WebSocket-Protocol"];
			if (subp == null)
			{
				if (_protocolsRequested)
				{
					message = "The Sec-WebSocket-Protocol header is non-existent.";
					return false;
				}
			}
			else if (!_protocolsRequested || subp.Length <= 0 || !_protocols.Contains((string p) => p == subp))
			{
				message = "The Sec-WebSocket-Protocol header is invalid.";
				return false;
			}
			string text3 = headers["Sec-WebSocket-Extensions"];
			if (text3 != null && !validateSecWebSocketExtensionsServerHeader(text3))
			{
				message = "The Sec-WebSocket-Extensions header is invalid.";
				return false;
			}
			return true;
		}

		private static bool checkProtocols(string[] protocols, out string message)
		{
			message = null;
			global::System.Func<string, bool> condition = (string p) => p.IsNullOrEmpty() || !p.IsToken();
			if (protocols.Contains(condition))
			{
				message = "It contains a value that is not a token.";
				return false;
			}
			if (protocols.ContainsTwice())
			{
				message = "It contains a value twice.";
				return false;
			}
			return true;
		}

		private bool checkProxyConnectResponse(global::UnityWebSocketSharp.HttpResponse response, out string message)
		{
			message = null;
			if (response.IsProxyAuthenticationRequired)
			{
				message = "The proxy authentication is required.";
				return false;
			}
			if (!response.IsSuccess)
			{
				message = "The proxy has failed a connection to the requested URL.";
				return false;
			}
			return true;
		}

		private bool checkReceivedFrame(global::UnityWebSocketSharp.WebSocketFrame frame, out string message)
		{
			message = null;
			if (frame.IsMasked)
			{
				if (_client)
				{
					message = "A frame from the server is masked.";
					return false;
				}
			}
			else if (!_client)
			{
				message = "A frame from a client is not masked.";
				return false;
			}
			if (frame.IsCompressed)
			{
				if (_compression == global::UnityWebSocketSharp.CompressionMethod.None)
				{
					message = "A frame is compressed without any agreement for it.";
					return false;
				}
				if (!frame.IsData)
				{
					message = "A non data frame is compressed.";
					return false;
				}
			}
			if (frame.IsData && _inContinuation)
			{
				message = "A data frame was received while receiving continuation frames.";
				return false;
			}
			if (frame.IsControl)
			{
				if (frame.Fin == global::UnityWebSocketSharp.Fin.More)
				{
					message = "A control frame is fragmented.";
					return false;
				}
				if (frame.PayloadLength > 125)
				{
					message = "The payload length of a control frame is greater than 125.";
					return false;
				}
			}
			if (frame.Rsv2 == global::UnityWebSocketSharp.Rsv.On)
			{
				message = "The RSV2 of a frame is non-zero without any negotiation for it.";
				return false;
			}
			if (frame.Rsv3 == global::UnityWebSocketSharp.Rsv.On)
			{
				message = "The RSV3 of a frame is non-zero without any negotiation for it.";
				return false;
			}
			return true;
		}

		private void close(ushort code, string reason)
		{
			if (_readyState == global::UnityWebSocketSharp.WebSocketState.Closing)
			{
				_log.Trace("The close process is already in progress.");
				return;
			}
			if (_readyState == global::UnityWebSocketSharp.WebSocketState.Closed)
			{
				_log.Trace("The connection has already been closed.");
				return;
			}
			if (code == 1005)
			{
				close(global::UnityWebSocketSharp.PayloadData.Empty, send: true, received: false);
				return;
			}
			global::UnityWebSocketSharp.PayloadData payloadData = new global::UnityWebSocketSharp.PayloadData(code, reason);
			bool flag = !code.IsReservedStatusCode();
			close(payloadData, flag, received: false);
		}

		private void close(global::UnityWebSocketSharp.PayloadData payloadData, bool send, bool received)
		{
			lock (_forState)
			{
				if (_readyState == global::UnityWebSocketSharp.WebSocketState.Closing)
				{
					_log.Trace("The close process is already in progress.");
					return;
				}
				if (_readyState == global::UnityWebSocketSharp.WebSocketState.Closed)
				{
					_log.Trace("The connection has already been closed.");
					return;
				}
				send = send && _readyState == global::UnityWebSocketSharp.WebSocketState.Open;
				_readyState = global::UnityWebSocketSharp.WebSocketState.Closing;
			}
			_log.Trace("Begin closing the connection.");
			bool clean = closeHandshake(payloadData, send, received);
			releaseResources();
			_log.Trace("End closing the connection.");
			_readyState = global::UnityWebSocketSharp.WebSocketState.Closed;
			global::UnityWebSocketSharp.CloseEventArgs e = new global::UnityWebSocketSharp.CloseEventArgs(payloadData, clean);
			try
			{
				this.OnClose.Emit(this, e);
			}
			catch (global::System.Exception ex)
			{
				_log.Error(ex.Message);
				_log.Debug(ex.ToString());
			}
		}

		private void closeAsync(ushort code, string reason)
		{
			if (_readyState == global::UnityWebSocketSharp.WebSocketState.Closing)
			{
				_log.Trace("The close process is already in progress.");
				return;
			}
			if (_readyState == global::UnityWebSocketSharp.WebSocketState.Closed)
			{
				_log.Trace("The connection has already been closed.");
				return;
			}
			if (code == 1005)
			{
				closeAsync(global::UnityWebSocketSharp.PayloadData.Empty, send: true, received: false);
				return;
			}
			global::UnityWebSocketSharp.PayloadData payloadData = new global::UnityWebSocketSharp.PayloadData(code, reason);
			bool flag = !code.IsReservedStatusCode();
			closeAsync(payloadData, flag, received: false);
		}

		private void closeAsync(global::UnityWebSocketSharp.PayloadData payloadData, bool send, bool received)
		{
			global::System.Action<global::UnityWebSocketSharp.PayloadData, bool, bool> closer = close;
			closer.BeginInvoke(payloadData, send, received, delegate(global::System.IAsyncResult ar)
			{
				closer.EndInvoke(ar);
			}, null);
		}

		private bool closeHandshake(global::UnityWebSocketSharp.PayloadData payloadData, bool send, bool received)
		{
			bool flag = false;
			if (send)
			{
				global::UnityWebSocketSharp.WebSocketFrame webSocketFrame = global::UnityWebSocketSharp.WebSocketFrame.CreateCloseFrame(payloadData, _client);
				byte[] bytes = webSocketFrame.ToArray();
				flag = sendBytes(bytes);
				if (_client)
				{
					webSocketFrame.Unmask();
				}
			}
			if (!received && flag && _receivingExited != null)
			{
				received = _receivingExited.WaitOne(_waitTime);
			}
			bool flag2 = flag && received;
			string text = $"The closing was clean? {flag2} (sent: {flag} received: {received})";
			_log.Debug(text);
			return flag2;
		}

		private bool connect()
		{
			if (_readyState == global::UnityWebSocketSharp.WebSocketState.Connecting)
			{
				_log.Trace("The connect process is in progress.");
				return false;
			}
			lock (_forState)
			{
				if (_readyState == global::UnityWebSocketSharp.WebSocketState.Open)
				{
					_log.Trace("The connection has already been established.");
					return false;
				}
				if (_readyState == global::UnityWebSocketSharp.WebSocketState.Closing)
				{
					_log.Error("The close process is in progress.");
					error("An error has occurred before connecting.", null);
					return false;
				}
				if (_retryCountForConnect >= _maxRetryCountForConnect)
				{
					_log.Error("An opportunity for reconnecting has been lost.");
					error("An error has occurred before connecting.", null);
					return false;
				}
				_retryCountForConnect++;
				_readyState = global::UnityWebSocketSharp.WebSocketState.Connecting;
				bool flag = false;
				try
				{
					flag = doHandshake();
				}
				catch (global::System.Exception ex)
				{
					_log.Fatal(ex.Message);
					_log.Debug(ex.ToString());
					abort("An exception has occurred while connecting.", ex);
				}
				if (!flag)
				{
					return false;
				}
				_retryCountForConnect = -1;
				_readyState = global::UnityWebSocketSharp.WebSocketState.Open;
				return true;
			}
		}

		private global::UnityWebSocketSharp.Net.AuthenticationResponse createAuthenticationResponse()
		{
			if (_credentials == null)
			{
				return null;
			}
			if (_authChallenge != null)
			{
				global::UnityWebSocketSharp.Net.AuthenticationResponse authenticationResponse = new global::UnityWebSocketSharp.Net.AuthenticationResponse(_authChallenge, _credentials, _nonceCount);
				_nonceCount = authenticationResponse.NonceCount;
				return authenticationResponse;
			}
			if (!_preAuth)
			{
				return null;
			}
			return new global::UnityWebSocketSharp.Net.AuthenticationResponse(_credentials);
		}

		private string createExtensions()
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder(80);
			if (_compression != global::UnityWebSocketSharp.CompressionMethod.None)
			{
				string arg = _compression.ToExtensionString("server_no_context_takeover", "client_no_context_takeover");
				stringBuilder.AppendFormat("{0}, ", arg);
			}
			int length = stringBuilder.Length;
			if (length <= 2)
			{
				return null;
			}
			stringBuilder.Length = length - 2;
			return stringBuilder.ToString();
		}

		private global::UnityWebSocketSharp.HttpResponse createHandshakeFailureResponse()
		{
			global::UnityWebSocketSharp.HttpResponse httpResponse = global::UnityWebSocketSharp.HttpResponse.CreateCloseResponse(global::UnityWebSocketSharp.Net.HttpStatusCode.BadRequest);
			httpResponse.Headers["Sec-WebSocket-Version"] = "13";
			return httpResponse;
		}

		private global::UnityWebSocketSharp.HttpRequest createHandshakeRequest()
		{
			global::UnityWebSocketSharp.HttpRequest httpRequest = global::UnityWebSocketSharp.HttpRequest.CreateWebSocketHandshakeRequest(_uri);
			global::System.Collections.Specialized.NameValueCollection headers = httpRequest.Headers;
			headers["Sec-WebSocket-Key"] = _base64Key;
			headers["Sec-WebSocket-Version"] = "13";
			if (!_origin.IsNullOrEmpty())
			{
				headers["Origin"] = _origin;
			}
			if (_protocols != null)
			{
				headers["Sec-WebSocket-Protocol"] = _protocols.ToString(", ");
				_protocolsRequested = true;
			}
			string text = createExtensions();
			if (text != null)
			{
				headers["Sec-WebSocket-Extensions"] = text;
				_extensionsRequested = true;
			}
			global::UnityWebSocketSharp.Net.AuthenticationResponse authenticationResponse = createAuthenticationResponse();
			if (authenticationResponse != null)
			{
				headers["Authorization"] = authenticationResponse.ToString();
			}
			if (_cookies.Count > 0)
			{
				httpRequest.SetCookies(_cookies);
			}
			return httpRequest;
		}

		private global::UnityWebSocketSharp.HttpResponse createHandshakeResponse()
		{
			global::UnityWebSocketSharp.HttpResponse httpResponse = global::UnityWebSocketSharp.HttpResponse.CreateWebSocketHandshakeResponse();
			global::System.Collections.Specialized.NameValueCollection headers = httpResponse.Headers;
			headers["Sec-WebSocket-Accept"] = CreateResponseKey(_base64Key);
			if (_protocol != null)
			{
				headers["Sec-WebSocket-Protocol"] = _protocol;
			}
			if (_extensions != null)
			{
				headers["Sec-WebSocket-Extensions"] = _extensions;
			}
			if (_cookies.Count > 0)
			{
				httpResponse.SetCookies(_cookies);
			}
			return httpResponse;
		}

		private bool customCheckHandshakeRequest(global::UnityWebSocketSharp.Net.WebSockets.WebSocketContext context, out string message)
		{
			message = null;
			if (_handshakeRequestChecker == null)
			{
				return true;
			}
			message = _handshakeRequestChecker(context);
			return message == null;
		}

		private global::UnityWebSocketSharp.MessageEventArgs dequeueFromMessageEventQueue()
		{
			lock (_forMessageEventQueue)
			{
				return (_messageEventQueue.Count > 0) ? _messageEventQueue.Dequeue() : null;
			}
		}

		private bool doHandshake()
		{
			setClientStream();
			global::UnityWebSocketSharp.HttpResponse httpResponse = sendHandshakeRequest();
			if (!checkHandshakeResponse(httpResponse, out var text))
			{
				_log.Error(text);
				_log.Debug(httpResponse.ToString());
				abort(1002, "A handshake error has occurred.");
				return false;
			}
			if (_protocolsRequested)
			{
				_protocol = httpResponse.Headers["Sec-WebSocket-Protocol"];
			}
			if (_extensionsRequested)
			{
				string text2 = httpResponse.Headers["Sec-WebSocket-Extensions"];
				if (text2 == null)
				{
					_compression = global::UnityWebSocketSharp.CompressionMethod.None;
				}
				else
				{
					_extensions = text2;
				}
			}
			global::UnityWebSocketSharp.Net.CookieCollection cookies = httpResponse.Cookies;
			if (cookies.Count > 0)
			{
				_cookies.SetOrRemove(cookies);
			}
			return true;
		}

		private void enqueueToMessageEventQueue(global::UnityWebSocketSharp.MessageEventArgs e)
		{
			lock (_forMessageEventQueue)
			{
				_messageEventQueue.Enqueue(e);
			}
		}

		private void error(string message, global::System.Exception exception)
		{
			global::UnityWebSocketSharp.ErrorEventArgs e = new global::UnityWebSocketSharp.ErrorEventArgs(message, exception);
			try
			{
				this.OnError.Emit(this, e);
			}
			catch (global::System.Exception ex)
			{
				_log.Error(ex.Message);
				_log.Debug(ex.ToString());
			}
		}

		private global::UnityWebSocketSharp.Net.ClientSslConfiguration getSslConfiguration()
		{
			if (_sslConfig == null)
			{
				_sslConfig = new global::UnityWebSocketSharp.Net.ClientSslConfiguration(_uri.DnsSafeHost);
			}
			return _sslConfig;
		}

		private void init()
		{
			_compression = global::UnityWebSocketSharp.CompressionMethod.None;
			_cookies = new global::UnityWebSocketSharp.Net.CookieCollection();
			_forPing = new object();
			_forSend = new object();
			_forState = new object();
			_messageEventQueue = new global::System.Collections.Generic.Queue<global::UnityWebSocketSharp.MessageEventArgs>();
			_forMessageEventQueue = ((global::System.Collections.ICollection)_messageEventQueue).SyncRoot;
			_readyState = global::UnityWebSocketSharp.WebSocketState.New;
		}

		private void message()
		{
			global::UnityWebSocketSharp.MessageEventArgs obj = null;
			lock (_forMessageEventQueue)
			{
				if (_inMessage || _messageEventQueue.Count == 0 || _readyState != global::UnityWebSocketSharp.WebSocketState.Open)
				{
					return;
				}
				obj = _messageEventQueue.Dequeue();
				_inMessage = true;
			}
			_message(obj);
		}

		private void messagec(global::UnityWebSocketSharp.MessageEventArgs e)
		{
			while (true)
			{
				try
				{
					this.OnMessage.Emit(this, e);
				}
				catch (global::System.Exception ex)
				{
					_log.Error(ex.Message);
					_log.Debug(ex.ToString());
					error("An exception has occurred during an OnMessage event.", ex);
				}
				lock (_forMessageEventQueue)
				{
					if (_messageEventQueue.Count == 0)
					{
						_inMessage = false;
						break;
					}
					if (_readyState != global::UnityWebSocketSharp.WebSocketState.Open)
					{
						_inMessage = false;
						break;
					}
					e = _messageEventQueue.Dequeue();
				}
			}
		}

		private void messages(global::UnityWebSocketSharp.MessageEventArgs e)
		{
			try
			{
				this.OnMessage.Emit(this, e);
			}
			catch (global::System.Exception ex)
			{
				_log.Error(ex.Message);
				_log.Debug(ex.ToString());
				error("An exception has occurred during an OnMessage event.", ex);
			}
			lock (_forMessageEventQueue)
			{
				if (_messageEventQueue.Count == 0)
				{
					_inMessage = false;
					return;
				}
				if (_readyState != global::UnityWebSocketSharp.WebSocketState.Open)
				{
					_inMessage = false;
					return;
				}
				e = _messageEventQueue.Dequeue();
			}
			global::System.Threading.ThreadPool.QueueUserWorkItem(delegate
			{
				messages(e);
			});
		}

		private void open()
		{
			_inMessage = true;
			startReceiving();
			try
			{
				this.OnOpen.Emit(this, global::System.EventArgs.Empty);
			}
			catch (global::System.Exception ex)
			{
				_log.Error(ex.Message);
				_log.Debug(ex.ToString());
				error("An exception has occurred during the OnOpen event.", ex);
			}
			global::UnityWebSocketSharp.MessageEventArgs obj = null;
			lock (_forMessageEventQueue)
			{
				if (_messageEventQueue.Count == 0)
				{
					_inMessage = false;
					return;
				}
				if (_readyState != global::UnityWebSocketSharp.WebSocketState.Open)
				{
					_inMessage = false;
					return;
				}
				obj = _messageEventQueue.Dequeue();
			}
			_message.BeginInvoke(obj, delegate(global::System.IAsyncResult ar)
			{
				_message.EndInvoke(ar);
			}, null);
		}

		private bool ping(byte[] data)
		{
			if (_readyState != global::UnityWebSocketSharp.WebSocketState.Open)
			{
				return false;
			}
			global::System.Threading.ManualResetEvent pongReceived = _pongReceived;
			if (pongReceived == null)
			{
				return false;
			}
			lock (_forPing)
			{
				try
				{
					pongReceived.Reset();
					if (!send(global::UnityWebSocketSharp.Fin.Final, global::UnityWebSocketSharp.Opcode.Ping, data, compressed: false))
					{
						return false;
					}
					return pongReceived.WaitOne(_waitTime);
				}
				catch (global::System.ObjectDisposedException)
				{
					return false;
				}
			}
		}

		private bool processCloseFrame(global::UnityWebSocketSharp.WebSocketFrame frame)
		{
			global::UnityWebSocketSharp.PayloadData payloadData = frame.PayloadData;
			bool flag = !payloadData.HasReservedCode;
			close(payloadData, flag, received: true);
			return false;
		}

		private bool processDataFrame(global::UnityWebSocketSharp.WebSocketFrame frame)
		{
			global::UnityWebSocketSharp.MessageEventArgs e = (frame.IsCompressed ? new global::UnityWebSocketSharp.MessageEventArgs(frame.Opcode, frame.PayloadData.ApplicationData.Decompress(_compression)) : new global::UnityWebSocketSharp.MessageEventArgs(frame));
			enqueueToMessageEventQueue(e);
			return true;
		}

		private bool processFragmentFrame(global::UnityWebSocketSharp.WebSocketFrame frame)
		{
			if (!_inContinuation)
			{
				if (frame.IsContinuation)
				{
					return true;
				}
				_fragmentsOpcode = frame.Opcode;
				_fragmentsCompressed = frame.IsCompressed;
				_fragmentsBuffer = new global::System.IO.MemoryStream();
				_inContinuation = true;
			}
			_fragmentsBuffer.WriteBytes(frame.PayloadData.ApplicationData, 1024);
			if (frame.IsFinal)
			{
				using (_fragmentsBuffer)
				{
					byte[] rawData = (_fragmentsCompressed ? _fragmentsBuffer.DecompressToArray(_compression) : _fragmentsBuffer.ToArray());
					global::UnityWebSocketSharp.MessageEventArgs e = new global::UnityWebSocketSharp.MessageEventArgs(_fragmentsOpcode, rawData);
					enqueueToMessageEventQueue(e);
				}
				_fragmentsBuffer = null;
				_inContinuation = false;
			}
			return true;
		}

		private bool processPingFrame(global::UnityWebSocketSharp.WebSocketFrame frame)
		{
			_log.Trace("A ping was received.");
			global::UnityWebSocketSharp.WebSocketFrame webSocketFrame = global::UnityWebSocketSharp.WebSocketFrame.CreatePongFrame(frame.PayloadData, _client);
			lock (_forState)
			{
				if (_readyState != global::UnityWebSocketSharp.WebSocketState.Open)
				{
					_log.Trace("A pong to this ping cannot be sent.");
					return true;
				}
				byte[] bytes = webSocketFrame.ToArray();
				if (!sendBytes(bytes))
				{
					return false;
				}
			}
			_log.Trace("A pong to this ping has been sent.");
			if (_emitOnPing)
			{
				if (_client)
				{
					webSocketFrame.Unmask();
				}
				global::UnityWebSocketSharp.MessageEventArgs e = new global::UnityWebSocketSharp.MessageEventArgs(frame);
				enqueueToMessageEventQueue(e);
			}
			return true;
		}

		private bool processPongFrame(global::UnityWebSocketSharp.WebSocketFrame frame)
		{
			_log.Trace("A pong was received.");
			try
			{
				_pongReceived.Set();
			}
			catch (global::System.NullReferenceException)
			{
				return false;
			}
			catch (global::System.ObjectDisposedException)
			{
				return false;
			}
			_log.Trace("It has been signaled.");
			return true;
		}

		private bool processReceivedFrame(global::UnityWebSocketSharp.WebSocketFrame frame)
		{
			if (!checkReceivedFrame(frame, out var text))
			{
				_log.Error(text);
				_log.Debug(frame.ToString(dump: false));
				abort(1002, "An error has occurred while receiving.");
				return false;
			}
			frame.Unmask();
			if (!frame.IsFragment)
			{
				if (!frame.IsData)
				{
					if (!frame.IsPing)
					{
						if (!frame.IsPong)
						{
							if (!frame.IsClose)
							{
								return processUnsupportedFrame(frame);
							}
							return processCloseFrame(frame);
						}
						return processPongFrame(frame);
					}
					return processPingFrame(frame);
				}
				return processDataFrame(frame);
			}
			return processFragmentFrame(frame);
		}

		private void processSecWebSocketExtensionsClientHeader(string value)
		{
			if (value == null)
			{
				return;
			}
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder(80);
			bool flag = false;
			foreach (string item in value.SplitHeaderValue(','))
			{
				string text = item.Trim();
				if (text.Length != 0 && !flag && text.IsCompressionExtension(global::UnityWebSocketSharp.CompressionMethod.Deflate))
				{
					_compression = global::UnityWebSocketSharp.CompressionMethod.Deflate;
					string arg = _compression.ToExtensionString("client_no_context_takeover", "server_no_context_takeover");
					stringBuilder.AppendFormat("{0}, ", arg);
					flag = true;
				}
			}
			int length = stringBuilder.Length;
			if (length > 2)
			{
				stringBuilder.Length = length - 2;
				_extensions = stringBuilder.ToString();
			}
		}

		private bool processUnsupportedFrame(global::UnityWebSocketSharp.WebSocketFrame frame)
		{
			_log.Fatal("An unsupported frame was received.");
			_log.Debug(frame.ToString(dump: false));
			abort(1003, "There is no way to handle it.");
			return false;
		}

		private void refuseHandshake(ushort code, string reason)
		{
			createHandshakeFailureResponse().WriteTo(_stream);
			abort(code, reason);
		}

		private void releaseClientResources()
		{
			if (_stream != null)
			{
				_stream.Dispose();
				_stream = null;
			}
		}

		private void releaseCommonResources()
		{
			if (_fragmentsBuffer != null)
			{
				_fragmentsBuffer.Dispose();
				_fragmentsBuffer = null;
				_inContinuation = false;
			}
			if (_pongReceived != null)
			{
				_pongReceived.Close();
				_pongReceived = null;
			}
			if (_receivingExited != null)
			{
				_receivingExited.Close();
				_receivingExited = null;
			}
		}

		private void releaseResources()
		{
			if (_client)
			{
				releaseClientResources();
			}
			else
			{
				releaseServerResources();
			}
			releaseCommonResources();
		}

		private void releaseServerResources()
		{
			if (_closeContext != null)
			{
				_closeContext();
				_closeContext = null;
			}
			_stream = null;
			_context = null;
		}

		private bool send(byte[] rawFrame)
		{
			lock (_forState)
			{
				if (_readyState != global::UnityWebSocketSharp.WebSocketState.Open)
				{
					_log.Error("The current state of the interface is not Open.");
					return false;
				}
				return sendBytes(rawFrame);
			}
		}

		private bool send(global::UnityWebSocketSharp.Opcode opcode, global::System.IO.Stream sourceStream)
		{
			lock (_forSend)
			{
				global::System.IO.Stream stream = sourceStream;
				bool flag = false;
				bool flag2 = false;
				try
				{
					if (_compression != global::UnityWebSocketSharp.CompressionMethod.None)
					{
						stream = sourceStream.Compress(_compression);
						flag = true;
					}
					flag2 = send(opcode, stream, flag);
					if (!flag2)
					{
						error("A send has failed.", null);
					}
				}
				catch (global::System.Exception ex)
				{
					_log.Error(ex.Message);
					_log.Debug(ex.ToString());
					error("An exception has occurred during a send.", ex);
				}
				finally
				{
					if (flag)
					{
						stream.Dispose();
					}
					sourceStream.Dispose();
				}
				return flag2;
			}
		}

		private bool send(global::UnityWebSocketSharp.Opcode opcode, global::System.IO.Stream dataStream, bool compressed)
		{
			long length = dataStream.Length;
			if (length == 0L)
			{
				return send(global::UnityWebSocketSharp.Fin.Final, opcode, EmptyBytes, compressed: false);
			}
			long num = length / FragmentLength;
			int num2 = (int)(length % FragmentLength);
			byte[] array = null;
			switch (num)
			{
			case 0L:
				array = new byte[num2];
				if (dataStream.Read(array, 0, num2) == num2)
				{
					return send(global::UnityWebSocketSharp.Fin.Final, opcode, array, compressed);
				}
				return false;
			case 1L:
				if (num2 == 0)
				{
					array = new byte[FragmentLength];
					if (dataStream.Read(array, 0, FragmentLength) == FragmentLength)
					{
						return send(global::UnityWebSocketSharp.Fin.Final, opcode, array, compressed);
					}
					return false;
				}
				break;
			}
			array = new byte[FragmentLength];
			if (dataStream.Read(array, 0, FragmentLength) != FragmentLength || !send(global::UnityWebSocketSharp.Fin.More, opcode, array, compressed))
			{
				return false;
			}
			long num3 = ((num2 == 0) ? (num - 2) : (num - 1));
			for (long num4 = 0L; num4 < num3; num4++)
			{
				if (dataStream.Read(array, 0, FragmentLength) != FragmentLength || !send(global::UnityWebSocketSharp.Fin.More, global::UnityWebSocketSharp.Opcode.Cont, array, compressed: false))
				{
					return false;
				}
			}
			if (num2 == 0)
			{
				num2 = FragmentLength;
			}
			else
			{
				array = new byte[num2];
			}
			if (dataStream.Read(array, 0, num2) == num2)
			{
				return send(global::UnityWebSocketSharp.Fin.Final, global::UnityWebSocketSharp.Opcode.Cont, array, compressed: false);
			}
			return false;
		}

		private bool send(global::UnityWebSocketSharp.Fin fin, global::UnityWebSocketSharp.Opcode opcode, byte[] data, bool compressed)
		{
			byte[] rawFrame = new global::UnityWebSocketSharp.WebSocketFrame(fin, opcode, data, compressed, _client).ToArray();
			return send(rawFrame);
		}

		private void sendAsync(global::UnityWebSocketSharp.Opcode opcode, global::System.IO.Stream sourceStream, global::System.Action<bool> completed)
		{
			global::System.Func<global::UnityWebSocketSharp.Opcode, global::System.IO.Stream, bool> sender = send;
			sender.BeginInvoke(opcode, sourceStream, delegate(global::System.IAsyncResult ar)
			{
				try
				{
					bool obj = sender.EndInvoke(ar);
					if (completed != null)
					{
						completed(obj);
					}
				}
				catch (global::System.Exception ex)
				{
					_log.Error(ex.Message);
					_log.Debug(ex.ToString());
					error("An exception has occurred during the callback for an async send.", ex);
				}
			}, null);
		}

		private bool sendBytes(byte[] bytes)
		{
			try
			{
				_stream.Write(bytes, 0, bytes.Length);
			}
			catch (global::System.Exception ex)
			{
				_log.Error(ex.Message);
				_log.Debug(ex.ToString());
				return false;
			}
			return true;
		}

		private global::UnityWebSocketSharp.HttpResponse sendHandshakeRequest()
		{
			global::UnityWebSocketSharp.HttpRequest httpRequest = createHandshakeRequest();
			int millisecondsTimeout = 90000;
			global::UnityWebSocketSharp.HttpResponse response = httpRequest.GetResponse(_stream, millisecondsTimeout);
			if (response.IsUnauthorized)
			{
				string value = response.Headers["WWW-Authenticate"];
				if (value.IsNullOrEmpty())
				{
					_log.Debug("No authentication challenge is specified.");
					return response;
				}
				global::UnityWebSocketSharp.Net.AuthenticationChallenge authenticationChallenge = global::UnityWebSocketSharp.Net.AuthenticationChallenge.Parse(value);
				if (authenticationChallenge == null)
				{
					_log.Debug("An invalid authentication challenge is specified.");
					return response;
				}
				_authChallenge = authenticationChallenge;
				if (_credentials == null)
				{
					return response;
				}
				global::UnityWebSocketSharp.Net.AuthenticationResponse authenticationResponse = new global::UnityWebSocketSharp.Net.AuthenticationResponse(_authChallenge, _credentials, _nonceCount);
				_nonceCount = authenticationResponse.NonceCount;
				httpRequest.Headers["Authorization"] = authenticationResponse.ToString();
				if (response.CloseConnection)
				{
					releaseClientResources();
					setClientStream();
				}
				millisecondsTimeout = 15000;
				response = httpRequest.GetResponse(_stream, millisecondsTimeout);
			}
			if (response.IsRedirect)
			{
				if (!_enableRedirection)
				{
					return response;
				}
				string text = response.Headers["Location"];
				if (text.IsNullOrEmpty())
				{
					_log.Debug("No URL to redirect is located.");
					return response;
				}
				if (!text.TryCreateWebSocketUri(out var result, out var _))
				{
					_log.Debug("An invalid URL to redirect is located.");
					return response;
				}
				releaseClientResources();
				_uri = result;
				_secure = result.Scheme == "wss";
				setClientStream();
				return sendHandshakeRequest();
			}
			return response;
		}

		private global::UnityWebSocketSharp.HttpResponse sendProxyConnectRequest()
		{
			global::UnityWebSocketSharp.HttpRequest httpRequest = global::UnityWebSocketSharp.HttpRequest.CreateConnectRequest(_uri);
			int millisecondsTimeout = 90000;
			global::UnityWebSocketSharp.HttpResponse response = httpRequest.GetResponse(_stream, millisecondsTimeout);
			if (response.IsProxyAuthenticationRequired)
			{
				if (_proxyCredentials == null)
				{
					return response;
				}
				string value = response.Headers["Proxy-Authenticate"];
				if (value.IsNullOrEmpty())
				{
					_log.Debug("No proxy authentication challenge is specified.");
					return response;
				}
				global::UnityWebSocketSharp.Net.AuthenticationChallenge authenticationChallenge = global::UnityWebSocketSharp.Net.AuthenticationChallenge.Parse(value);
				if (authenticationChallenge == null)
				{
					_log.Debug("An invalid proxy authentication challenge is specified.");
					return response;
				}
				global::UnityWebSocketSharp.Net.AuthenticationResponse authenticationResponse = new global::UnityWebSocketSharp.Net.AuthenticationResponse(authenticationChallenge, _proxyCredentials, 0u);
				httpRequest.Headers["Proxy-Authorization"] = authenticationResponse.ToString();
				if (response.CloseConnection)
				{
					releaseClientResources();
					_stream = GetNetworkStream?.Invoke(_proxyUri.DnsSafeHost, _proxyUri.Port);
				}
				millisecondsTimeout = 15000;
				response = httpRequest.GetResponse(_stream, millisecondsTimeout);
			}
			return response;
		}

		private void setClientStream()
		{
			if (_proxyUri != null)
			{
				_stream = GetNetworkStream(_proxyUri.DnsSafeHost, _proxyUri.Port);
				global::UnityWebSocketSharp.HttpResponse response = sendProxyConnectRequest();
				if (!checkProxyConnectResponse(response, out var text))
				{
					throw new global::UnityWebSocketSharp.WebSocketException(text);
				}
			}
			else
			{
				try
				{
					_stream = GetNetworkStream(_uri.DnsSafeHost, _uri.Port);
				}
				catch (global::System.Exception value)
				{
					global::System.Console.WriteLine(value);
					throw;
				}
			}
			if (_secure)
			{
				global::UnityWebSocketSharp.Net.ClientSslConfiguration sslConfiguration = getSslConfiguration();
				string targetHost = sslConfiguration.TargetHost;
				if (targetHost != _uri.DnsSafeHost)
				{
					string text2 = "An invalid host name is specified.";
					throw new global::UnityWebSocketSharp.WebSocketException(global::UnityWebSocketSharp.CloseStatusCode.TlsHandshakeFailure, text2);
				}
				try
				{
					global::System.Net.Security.SslStream sslStream = new global::System.Net.Security.SslStream(_stream, leaveInnerStreamOpen: false, sslConfiguration.ServerCertificateValidationCallback, sslConfiguration.ClientCertificateSelectionCallback);
					sslStream.AuthenticateAsClient(targetHost, sslConfiguration.ClientCertificates, sslConfiguration.EnabledSslProtocols, sslConfiguration.CheckCertificateRevocation);
					_stream = sslStream;
				}
				catch (global::System.Exception innerException)
				{
					throw new global::UnityWebSocketSharp.WebSocketException(global::UnityWebSocketSharp.CloseStatusCode.TlsHandshakeFailure, innerException);
				}
			}
		}

		private void startReceiving()
		{
			if (_messageEventQueue.Count > 0)
			{
				_messageEventQueue.Clear();
			}
			_pongReceived = new global::System.Threading.ManualResetEvent(initialState: false);
			_receivingExited = new global::System.Threading.ManualResetEvent(initialState: false);
			global::System.Action receive = null;
			receive = delegate
			{
				global::UnityWebSocketSharp.WebSocketFrame.ReadFrameAsync(_stream, unmask: false, delegate(global::UnityWebSocketSharp.WebSocketFrame frame)
				{
					if (!processReceivedFrame(frame) || _readyState == global::UnityWebSocketSharp.WebSocketState.Closed)
					{
						_receivingExited?.Set();
					}
					else
					{
						receive();
						if (!_inMessage)
						{
							message();
						}
					}
				}, delegate(global::System.Exception ex)
				{
					_log.Fatal(ex.Message);
					_log.Debug(ex.ToString());
					abort("An exception has occurred while receiving.", ex);
				});
			};
			receive();
		}

		private bool validateSecWebSocketExtensionsServerHeader(string value)
		{
			if (!_extensionsRequested)
			{
				return false;
			}
			if (value.Length == 0)
			{
				return false;
			}
			bool flag = _compression != global::UnityWebSocketSharp.CompressionMethod.None;
			foreach (string item in value.SplitHeaderValue(','))
			{
				string text = item.Trim();
				if (flag && text.IsCompressionExtension(_compression))
				{
					string param1 = "server_no_context_takeover";
					string param2 = "client_no_context_takeover";
					if (!text.Contains(param1))
					{
						return false;
					}
					string name = _compression.ToExtensionString();
					if (text.SplitHeaderValue(';').Contains(delegate(string t)
					{
						t = t.Trim();
						return !(t == name) && !(t == param1) && !(t == param2);
					}))
					{
						return false;
					}
					flag = false;
					continue;
				}
				return false;
			}
			return true;
		}

		internal void Accept()
		{
			if (accept())
			{
				open();
			}
		}

		internal void AcceptAsync()
		{
			global::System.Func<bool> acceptor = accept;
			acceptor.BeginInvoke(delegate(global::System.IAsyncResult ar)
			{
				if (acceptor.EndInvoke(ar))
				{
					open();
				}
			}, null);
		}

		internal void Close(global::UnityWebSocketSharp.PayloadData payloadData, byte[] rawFrame)
		{
			lock (_forState)
			{
				if (_readyState == global::UnityWebSocketSharp.WebSocketState.Closing)
				{
					_log.Trace("The close process is already in progress.");
					return;
				}
				if (_readyState == global::UnityWebSocketSharp.WebSocketState.Closed)
				{
					_log.Trace("The connection has already been closed.");
					return;
				}
				_readyState = global::UnityWebSocketSharp.WebSocketState.Closing;
			}
			_log.Trace("Begin closing the connection.");
			bool flag = rawFrame != null && sendBytes(rawFrame);
			bool flag2 = flag && _receivingExited != null && _receivingExited.WaitOne(_waitTime);
			bool flag3 = flag && flag2;
			string text = $"The closing was clean? {flag3} (sent: {flag} received: {flag2})";
			_log.Debug(text);
			releaseServerResources();
			releaseCommonResources();
			_log.Trace("End closing the connection.");
			_readyState = global::UnityWebSocketSharp.WebSocketState.Closed;
			global::UnityWebSocketSharp.CloseEventArgs e = new global::UnityWebSocketSharp.CloseEventArgs(payloadData, flag3);
			try
			{
				this.OnClose.Emit(this, e);
			}
			catch (global::System.Exception ex)
			{
				_log.Error(ex.Message);
				_log.Debug(ex.ToString());
			}
		}

		internal static string CreateBase64Key()
		{
			byte[] array = new byte[16];
			RandomNumber.GetBytes(array);
			return global::System.Convert.ToBase64String(array);
		}

		internal static string CreateResponseKey(string base64Key)
		{
			global::System.Security.Cryptography.SHA1CryptoServiceProvider sHA1CryptoServiceProvider = new global::System.Security.Cryptography.SHA1CryptoServiceProvider();
			byte[] uTF8EncodedBytes = (base64Key + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11").GetUTF8EncodedBytes();
			return global::System.Convert.ToBase64String(sHA1CryptoServiceProvider.ComputeHash(uTF8EncodedBytes));
		}

		internal bool Ping(byte[] rawFrame)
		{
			if (_readyState != global::UnityWebSocketSharp.WebSocketState.Open)
			{
				return false;
			}
			global::System.Threading.ManualResetEvent pongReceived = _pongReceived;
			if (pongReceived == null)
			{
				return false;
			}
			lock (_forPing)
			{
				try
				{
					pongReceived.Reset();
					if (!send(rawFrame))
					{
						return false;
					}
					return pongReceived.WaitOne(_waitTime);
				}
				catch (global::System.ObjectDisposedException)
				{
					return false;
				}
			}
		}

		internal void Send(global::UnityWebSocketSharp.Opcode opcode, byte[] data, global::System.Collections.Generic.Dictionary<global::UnityWebSocketSharp.CompressionMethod, byte[]> cache)
		{
			lock (_forSend)
			{
				if (!cache.TryGetValue(_compression, out var value))
				{
					value = new global::UnityWebSocketSharp.WebSocketFrame(global::UnityWebSocketSharp.Fin.Final, opcode, data.Compress(_compression), _compression != global::UnityWebSocketSharp.CompressionMethod.None, mask: false).ToArray();
					cache.Add(_compression, value);
				}
				send(value);
			}
		}

		internal void Send(global::UnityWebSocketSharp.Opcode opcode, global::System.IO.Stream sourceStream, global::System.Collections.Generic.Dictionary<global::UnityWebSocketSharp.CompressionMethod, global::System.IO.Stream> cache)
		{
			lock (_forSend)
			{
				if (!cache.TryGetValue(_compression, out var value))
				{
					value = sourceStream.Compress(_compression);
					cache.Add(_compression, value);
				}
				else
				{
					value.Position = 0L;
				}
				send(opcode, value, _compression != global::UnityWebSocketSharp.CompressionMethod.None);
			}
		}

		public void Close()
		{
			close(1005, string.Empty);
		}

		public void Close(ushort code)
		{
			Close(code, string.Empty);
		}

		public void Close(global::UnityWebSocketSharp.CloseStatusCode code)
		{
			Close(code, string.Empty);
		}

		public void Close(ushort code, string reason)
		{
			if (!code.IsCloseStatusCode())
			{
				string text = "Less than 1000 or greater than 4999.";
				throw new global::System.ArgumentOutOfRangeException("code", text);
			}
			if (_client && code == 1011)
			{
				throw new global::System.ArgumentException("1011 cannot be used.", "code");
			}
			if (!_client && code == 1010)
			{
				throw new global::System.ArgumentException("1010 cannot be used.", "code");
			}
			if (reason.IsNullOrEmpty())
			{
				close(code, string.Empty);
				return;
			}
			if (code == 1005)
			{
				throw new global::System.ArgumentException("1005 cannot be used.", "code");
			}
			if (!reason.TryGetUTF8EncodedBytes(out var bytes))
			{
				throw new global::System.ArgumentException("It could not be UTF-8-encoded.", "reason");
			}
			if (bytes.Length > 123)
			{
				string text2 = "Its size is greater than 123 bytes.";
				throw new global::System.ArgumentOutOfRangeException("reason", text2);
			}
			close(code, reason);
		}

		public void Close(global::UnityWebSocketSharp.CloseStatusCode code, string reason)
		{
			if (_client && code == global::UnityWebSocketSharp.CloseStatusCode.ServerError)
			{
				throw new global::System.ArgumentException("ServerError cannot be used.", "code");
			}
			if (!_client && code == global::UnityWebSocketSharp.CloseStatusCode.MandatoryExtension)
			{
				throw new global::System.ArgumentException("MandatoryExtension cannot be used.", "code");
			}
			if (reason.IsNullOrEmpty())
			{
				close((ushort)code, string.Empty);
				return;
			}
			if (code == global::UnityWebSocketSharp.CloseStatusCode.NoStatus)
			{
				throw new global::System.ArgumentException("NoStatus cannot be used.", "code");
			}
			if (!reason.TryGetUTF8EncodedBytes(out var bytes))
			{
				throw new global::System.ArgumentException("It could not be UTF-8-encoded.", "reason");
			}
			if (bytes.Length > 123)
			{
				string text = "Its size is greater than 123 bytes.";
				throw new global::System.ArgumentOutOfRangeException("reason", text);
			}
			close((ushort)code, reason);
		}

		public void CloseAsync()
		{
			closeAsync(1005, string.Empty);
		}

		public void CloseAsync(ushort code)
		{
			CloseAsync(code, string.Empty);
		}

		public void CloseAsync(global::UnityWebSocketSharp.CloseStatusCode code)
		{
			CloseAsync(code, string.Empty);
		}

		public void CloseAsync(ushort code, string reason)
		{
			if (!code.IsCloseStatusCode())
			{
				string text = "Less than 1000 or greater than 4999.";
				throw new global::System.ArgumentOutOfRangeException("code", text);
			}
			if (_client && code == 1011)
			{
				throw new global::System.ArgumentException("1011 cannot be used.", "code");
			}
			if (!_client && code == 1010)
			{
				throw new global::System.ArgumentException("1010 cannot be used.", "code");
			}
			if (reason.IsNullOrEmpty())
			{
				closeAsync(code, string.Empty);
				return;
			}
			if (code == 1005)
			{
				throw new global::System.ArgumentException("1005 cannot be used.", "code");
			}
			if (!reason.TryGetUTF8EncodedBytes(out var bytes))
			{
				throw new global::System.ArgumentException("It could not be UTF-8-encoded.", "reason");
			}
			if (bytes.Length > 123)
			{
				string text2 = "Its size is greater than 123 bytes.";
				throw new global::System.ArgumentOutOfRangeException("reason", text2);
			}
			closeAsync(code, reason);
		}

		public void CloseAsync(global::UnityWebSocketSharp.CloseStatusCode code, string reason)
		{
			if (_client && code == global::UnityWebSocketSharp.CloseStatusCode.ServerError)
			{
				throw new global::System.ArgumentException("ServerError cannot be used.", "code");
			}
			if (!_client && code == global::UnityWebSocketSharp.CloseStatusCode.MandatoryExtension)
			{
				throw new global::System.ArgumentException("MandatoryExtension cannot be used.", "code");
			}
			if (reason.IsNullOrEmpty())
			{
				closeAsync((ushort)code, string.Empty);
				return;
			}
			if (code == global::UnityWebSocketSharp.CloseStatusCode.NoStatus)
			{
				throw new global::System.ArgumentException("NoStatus cannot be used.", "code");
			}
			if (!reason.TryGetUTF8EncodedBytes(out var bytes))
			{
				throw new global::System.ArgumentException("It could not be UTF-8-encoded.", "reason");
			}
			if (bytes.Length > 123)
			{
				string text = "Its size is greater than 123 bytes.";
				throw new global::System.ArgumentOutOfRangeException("reason", text);
			}
			closeAsync((ushort)code, reason);
		}

		public void Connect()
		{
			if (!_client)
			{
				throw new global::System.InvalidOperationException("The interface is not for the client.");
			}
			if (_retryCountForConnect >= _maxRetryCountForConnect)
			{
				throw new global::System.InvalidOperationException("A series of reconnecting has failed.");
			}
			if (connect())
			{
				open();
			}
		}

		public void ConnectAsync()
		{
			if (!_client)
			{
				throw new global::System.InvalidOperationException("The interface is not for the client.");
			}
			if (_retryCountForConnect >= _maxRetryCountForConnect)
			{
				throw new global::System.InvalidOperationException("A series of reconnecting has failed.");
			}
			global::System.Func<bool> connector = connect;
			connector.BeginInvoke(delegate(global::System.IAsyncResult ar)
			{
				if (connector.EndInvoke(ar))
				{
					open();
				}
			}, null);
		}

		public bool Ping()
		{
			return ping(EmptyBytes);
		}

		public bool Ping(string message)
		{
			if (message.IsNullOrEmpty())
			{
				return ping(EmptyBytes);
			}
			if (!message.TryGetUTF8EncodedBytes(out var bytes))
			{
				throw new global::System.ArgumentException("It could not be UTF-8-encoded.", "message");
			}
			if (bytes.Length > 125)
			{
				string text = "Its size is greater than 125 bytes.";
				throw new global::System.ArgumentOutOfRangeException("message", text);
			}
			return ping(bytes);
		}

		public void Send(byte[] data)
		{
			if (_readyState != global::UnityWebSocketSharp.WebSocketState.Open)
			{
				throw new global::System.InvalidOperationException("The current state of the interface is not Open.");
			}
			if (data == null)
			{
				throw new global::System.ArgumentNullException("data");
			}
			send(global::UnityWebSocketSharp.Opcode.Binary, new global::System.IO.MemoryStream(data));
		}

		public void Send(global::System.IO.FileInfo fileInfo)
		{
			if (_readyState != global::UnityWebSocketSharp.WebSocketState.Open)
			{
				throw new global::System.InvalidOperationException("The current state of the interface is not Open.");
			}
			if (fileInfo == null)
			{
				throw new global::System.ArgumentNullException("fileInfo");
			}
			if (!fileInfo.Exists)
			{
				throw new global::System.ArgumentException("The file does not exist.", "fileInfo");
			}
			if (!fileInfo.TryOpenRead(out var fileStream))
			{
				throw new global::System.ArgumentException("The file could not be opened.", "fileInfo");
			}
			send(global::UnityWebSocketSharp.Opcode.Binary, fileStream);
		}

		public void Send(string data)
		{
			if (_readyState != global::UnityWebSocketSharp.WebSocketState.Open)
			{
				throw new global::System.InvalidOperationException("The current state of the interface is not Open.");
			}
			if (data == null)
			{
				throw new global::System.ArgumentNullException("data");
			}
			if (!data.TryGetUTF8EncodedBytes(out var bytes))
			{
				throw new global::System.ArgumentException("It could not be UTF-8-encoded.", "data");
			}
			send(global::UnityWebSocketSharp.Opcode.Text, new global::System.IO.MemoryStream(bytes));
		}

		public void Send(global::System.IO.Stream stream, int length)
		{
			if (_readyState != global::UnityWebSocketSharp.WebSocketState.Open)
			{
				throw new global::System.InvalidOperationException("The current state of the interface is not Open.");
			}
			if (stream == null)
			{
				throw new global::System.ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				throw new global::System.ArgumentException("It cannot be read.", "stream");
			}
			if (length < 1)
			{
				throw new global::System.ArgumentException("Less than 1.", "length");
			}
			byte[] array = stream.ReadBytes(length);
			int num = array.Length;
			if (num == 0)
			{
				throw new global::System.ArgumentException("No data could be read from it.", "stream");
			}
			if (num < length)
			{
				string text = $"Only {num} byte(s) of data could be read from the stream.";
				_log.Warn(text);
			}
			send(global::UnityWebSocketSharp.Opcode.Binary, new global::System.IO.MemoryStream(array));
		}

		public void SendAsync(byte[] data, global::System.Action<bool> completed)
		{
			if (_readyState != global::UnityWebSocketSharp.WebSocketState.Open)
			{
				throw new global::System.InvalidOperationException("The current state of the interface is not Open.");
			}
			if (data == null)
			{
				throw new global::System.ArgumentNullException("data");
			}
			sendAsync(global::UnityWebSocketSharp.Opcode.Binary, new global::System.IO.MemoryStream(data), completed);
		}

		public void SendAsync(global::System.IO.FileInfo fileInfo, global::System.Action<bool> completed)
		{
			if (_readyState != global::UnityWebSocketSharp.WebSocketState.Open)
			{
				throw new global::System.InvalidOperationException("The current state of the interface is not Open.");
			}
			if (fileInfo == null)
			{
				throw new global::System.ArgumentNullException("fileInfo");
			}
			if (!fileInfo.Exists)
			{
				throw new global::System.ArgumentException("The file does not exist.", "fileInfo");
			}
			if (!fileInfo.TryOpenRead(out var fileStream))
			{
				throw new global::System.ArgumentException("The file could not be opened.", "fileInfo");
			}
			sendAsync(global::UnityWebSocketSharp.Opcode.Binary, fileStream, completed);
		}

		public void SendAsync(string data, global::System.Action<bool> completed)
		{
			if (_readyState != global::UnityWebSocketSharp.WebSocketState.Open)
			{
				throw new global::System.InvalidOperationException("The current state of the interface is not Open.");
			}
			if (data == null)
			{
				throw new global::System.ArgumentNullException("data");
			}
			if (!data.TryGetUTF8EncodedBytes(out var bytes))
			{
				throw new global::System.ArgumentException("It could not be UTF-8-encoded.", "data");
			}
			sendAsync(global::UnityWebSocketSharp.Opcode.Text, new global::System.IO.MemoryStream(bytes), completed);
		}

		public void SendAsync(global::System.IO.Stream stream, int length, global::System.Action<bool> completed)
		{
			if (_readyState != global::UnityWebSocketSharp.WebSocketState.Open)
			{
				throw new global::System.InvalidOperationException("The current state of the interface is not Open.");
			}
			if (stream == null)
			{
				throw new global::System.ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				throw new global::System.ArgumentException("It cannot be read.", "stream");
			}
			if (length < 1)
			{
				throw new global::System.ArgumentException("Less than 1.", "length");
			}
			byte[] array = stream.ReadBytes(length);
			int num = array.Length;
			if (num == 0)
			{
				throw new global::System.ArgumentException("No data could be read from it.", "stream");
			}
			if (num < length)
			{
				string text = $"Only {num} byte(s) of data could be read from the stream.";
				_log.Warn(text);
			}
			sendAsync(global::UnityWebSocketSharp.Opcode.Binary, new global::System.IO.MemoryStream(array), completed);
		}

		public void SetCookie(global::UnityWebSocketSharp.Net.Cookie cookie)
		{
			if (!_client)
			{
				throw new global::System.InvalidOperationException("The interface is not for the client.");
			}
			if (cookie == null)
			{
				throw new global::System.ArgumentNullException("cookie");
			}
			lock (_forState)
			{
				if (!canSet())
				{
					return;
				}
				lock (_cookies.SyncRoot)
				{
					_cookies.SetOrRemove(cookie);
				}
			}
		}

		public void SetCredentials(string username, string password, bool preAuth)
		{
			if (!_client)
			{
				throw new global::System.InvalidOperationException("The interface is not for the client.");
			}
			if (!username.IsNullOrEmpty() && (username.Contains(':') || !username.IsText()))
			{
				throw new global::System.ArgumentException("It contains an invalid character.", "username");
			}
			if (!password.IsNullOrEmpty() && !password.IsText())
			{
				throw new global::System.ArgumentException("It contains an invalid character.", "password");
			}
			lock (_forState)
			{
				if (canSet())
				{
					if (username.IsNullOrEmpty())
					{
						_credentials = null;
						_preAuth = false;
					}
					else
					{
						_credentials = new global::UnityWebSocketSharp.Net.NetworkCredential(username, password, _uri.PathAndQuery);
						_preAuth = preAuth;
					}
				}
			}
		}

		public void SetProxy(string url, string username, string password)
		{
			if (!_client)
			{
				throw new global::System.InvalidOperationException("The interface is not for the client.");
			}
			global::System.Uri result = null;
			if (!url.IsNullOrEmpty())
			{
				if (!global::System.Uri.TryCreate(url, global::System.UriKind.Absolute, out result))
				{
					throw new global::System.ArgumentException("Not an absolute URI string.", "url");
				}
				if (result.Scheme != "http")
				{
					throw new global::System.ArgumentException("The scheme part is not http.", "url");
				}
				if (result.Segments.Length > 1)
				{
					throw new global::System.ArgumentException("It includes the path segments.", "url");
				}
			}
			if (!username.IsNullOrEmpty() && (username.Contains(':') || !username.IsText()))
			{
				throw new global::System.ArgumentException("It contains an invalid character.", "username");
			}
			if (!password.IsNullOrEmpty() && !password.IsText())
			{
				throw new global::System.ArgumentException("It contains an invalid character.", "password");
			}
			lock (_forState)
			{
				if (canSet())
				{
					if (url.IsNullOrEmpty())
					{
						_proxyUri = null;
						_proxyCredentials = null;
					}
					else
					{
						_proxyUri = result;
						_proxyCredentials = ((!username.IsNullOrEmpty()) ? new global::UnityWebSocketSharp.Net.NetworkCredential(username, password, $"{_uri.DnsSafeHost}:{_uri.Port}") : null);
					}
				}
			}
		}

		void global::System.IDisposable.Dispose()
		{
			close(1001, string.Empty);
		}
	}
}
