namespace WebSocketSharp
{
	public class WebSocket : global::System.IDisposable
	{
		private global::WebSocketSharp.Net.AuthenticationChallenge _authChallenge;

		private string _base64Key;

		private bool _client;

		private global::System.Action _closeContext;

		private global::WebSocketSharp.CompressionMethod _compression;

		private global::WebSocketSharp.Net.WebSockets.WebSocketContext _context;

		private global::WebSocketSharp.Net.CookieCollection _cookies;

		private global::WebSocketSharp.Net.NetworkCredential _credentials;

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

		private global::WebSocketSharp.Opcode _fragmentsOpcode;

		private const string _guid = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";

		private global::System.Func<global::WebSocketSharp.Net.WebSockets.WebSocketContext, string> _handshakeRequestChecker;

		private bool _ignoreExtensions;

		private bool _inContinuation;

		private volatile bool _inMessage;

		private volatile global::WebSocketSharp.Logger _logger;

		private static readonly int _maxRetryCountForConnect;

		private global::System.Action<global::WebSocketSharp.MessageEventArgs> _message;

		private global::System.Collections.Generic.Queue<global::WebSocketSharp.MessageEventArgs> _messageEventQueue;

		private uint _nonceCount;

		private string _origin;

		private global::System.Threading.ManualResetEvent _pongReceived;

		private bool _preAuth;

		private string _protocol;

		private string[] _protocols;

		private bool _protocolsRequested;

		private global::WebSocketSharp.Net.NetworkCredential _proxyCredentials;

		private global::System.Uri _proxyUri;

		private volatile global::WebSocketSharp.WebSocketState _readyState;

		private global::System.Threading.ManualResetEvent _receivingExited;

		private int _retryCountForConnect;

		private bool _secure;

		private global::WebSocketSharp.Net.ClientSslConfiguration _sslConfig;

		private global::System.IO.Stream _stream;

		private global::System.Net.Sockets.TcpClient _tcpClient;

		private global::System.Uri _uri;

		private const string _version = "13";

		private global::System.TimeSpan _waitTime;

		internal static readonly byte[] EmptyBytes;

		internal static readonly int FragmentLength;

		internal static readonly global::System.Security.Cryptography.RandomNumberGenerator RandomNumber;

		internal global::WebSocketSharp.Net.CookieCollection CookieCollection => _cookies;

		internal global::System.Func<global::WebSocketSharp.Net.WebSockets.WebSocketContext, string> CustomHandshakeRequestChecker
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

		internal bool HasMessage
		{
			get
			{
				lock (_forMessageEventQueue)
				{
					return _messageEventQueue.Count > 0;
				}
			}
		}

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

		internal bool IsConnected => _readyState == global::WebSocketSharp.WebSocketState.Open || _readyState == global::WebSocketSharp.WebSocketState.Closing;

		public global::WebSocketSharp.CompressionMethod Compression
		{
			get
			{
				return _compression;
			}
			set
			{
				string text = null;
				if (!_client)
				{
					text = "This instance is not a client.";
					throw new global::System.InvalidOperationException(text);
				}
				if (!canSet(out text))
				{
					_logger.Warn(text);
					return;
				}
				lock (_forState)
				{
					if (!canSet(out text))
					{
						_logger.Warn(text);
					}
					else
					{
						_compression = value;
					}
				}
			}
		}

		public global::System.Collections.Generic.IEnumerable<global::WebSocketSharp.Net.Cookie> Cookies
		{
			get
			{
				lock (_cookies.SyncRoot)
				{
					foreach (global::WebSocketSharp.Net.Cookie cookie in _cookies)
					{
						yield return cookie;
					}
				}
			}
		}

		public global::WebSocketSharp.Net.NetworkCredential Credentials => _credentials;

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
				string text = null;
				if (!_client)
				{
					text = "This instance is not a client.";
					throw new global::System.InvalidOperationException(text);
				}
				if (!canSet(out text))
				{
					_logger.Warn(text);
					return;
				}
				lock (_forState)
				{
					if (!canSet(out text))
					{
						_logger.Warn(text);
					}
					else
					{
						_enableRedirection = value;
					}
				}
			}
		}

		public string Extensions => _extensions ?? string.Empty;

		public bool IsAlive => ping(EmptyBytes);

		public bool IsSecure => _secure;

		public global::WebSocketSharp.Logger Log
		{
			get
			{
				return _logger;
			}
			internal set
			{
				_logger = value;
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
				string text = null;
				if (!_client)
				{
					text = "This instance is not a client.";
					throw new global::System.InvalidOperationException(text);
				}
				if (!value.IsNullOrEmpty())
				{
					if (!global::System.Uri.TryCreate(value, global::System.UriKind.Absolute, out var result))
					{
						text = "Not an absolute URI string.";
						throw new global::System.ArgumentException(text, "value");
					}
					if (result.Segments.Length > 1)
					{
						text = "It includes the path segments.";
						throw new global::System.ArgumentException(text, "value");
					}
				}
				if (!canSet(out text))
				{
					_logger.Warn(text);
					return;
				}
				lock (_forState)
				{
					if (!canSet(out text))
					{
						_logger.Warn(text);
						return;
					}
					_origin = ((!value.IsNullOrEmpty()) ? value.TrimEnd(new char[1] { '/' }) : value);
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

		public global::WebSocketSharp.WebSocketState ReadyState => _readyState;

		public global::WebSocketSharp.Net.ClientSslConfiguration SslConfiguration
		{
			get
			{
				if (!_client)
				{
					string text = "This instance is not a client.";
					throw new global::System.InvalidOperationException(text);
				}
				if (!_secure)
				{
					string text2 = "This instance does not use a secure connection.";
					throw new global::System.InvalidOperationException(text2);
				}
				return getSslConfiguration();
			}
		}

		public global::System.Uri Url => _client ? _uri : _context.RequestUri;

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
					throw new global::System.ArgumentOutOfRangeException("value", "Zero or less.");
				}
				if (!canSet(out var text))
				{
					_logger.Warn(text);
					return;
				}
				lock (_forState)
				{
					if (!canSet(out text))
					{
						_logger.Warn(text);
					}
					else
					{
						_waitTime = value;
					}
				}
			}
		}

		public event global::System.EventHandler<global::WebSocketSharp.CloseEventArgs> OnClose;

		public event global::System.EventHandler<global::WebSocketSharp.ErrorEventArgs> OnError;

		public event global::System.EventHandler<global::WebSocketSharp.MessageEventArgs> OnMessage;

		public event global::System.EventHandler OnOpen;

		static WebSocket()
		{
			_maxRetryCountForConnect = 10;
			EmptyBytes = new byte[0];
			FragmentLength = 1016;
			RandomNumber = new global::System.Security.Cryptography.RNGCryptoServiceProvider();
		}

		internal WebSocket(global::WebSocketSharp.Net.WebSockets.HttpListenerWebSocketContext context, string protocol)
		{
			_context = context;
			_protocol = protocol;
			_closeContext = context.Close;
			_logger = context.Log;
			_message = messages;
			_secure = context.IsSecureConnection;
			_stream = context.Stream;
			_waitTime = global::System.TimeSpan.FromSeconds(1.0);
			init();
		}

		internal WebSocket(global::WebSocketSharp.Net.WebSockets.TcpListenerWebSocketContext context, string protocol)
		{
			_context = context;
			_protocol = protocol;
			_closeContext = context.Close;
			_logger = context.Log;
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
			_logger = new global::WebSocketSharp.Logger();
			_message = messagec;
			_secure = _uri.Scheme == "wss";
			_waitTime = global::System.TimeSpan.FromSeconds(5.0);
			init();
		}

		private bool accept()
		{
			if (_readyState == global::WebSocketSharp.WebSocketState.Open)
			{
				string text = "The handshake request has already been accepted.";
				_logger.Warn(text);
				return false;
			}
			lock (_forState)
			{
				if (_readyState == global::WebSocketSharp.WebSocketState.Open)
				{
					string text2 = "The handshake request has already been accepted.";
					_logger.Warn(text2);
					return false;
				}
				if (_readyState == global::WebSocketSharp.WebSocketState.Closing)
				{
					string text3 = "The close process has set in.";
					_logger.Error(text3);
					text3 = "An interruption has occurred while attempting to accept.";
					error(text3, null);
					return false;
				}
				if (_readyState == global::WebSocketSharp.WebSocketState.Closed)
				{
					string text4 = "The connection has been closed.";
					_logger.Error(text4);
					text4 = "An interruption has occurred while attempting to accept.";
					error(text4, null);
					return false;
				}
				try
				{
					if (!acceptHandshake())
					{
						return false;
					}
				}
				catch (global::System.Exception ex)
				{
					_logger.Fatal(ex.Message);
					_logger.Debug(ex.ToString());
					string text5 = "An exception has occurred while attempting to accept.";
					fatal(text5, ex);
					return false;
				}
				_readyState = global::WebSocketSharp.WebSocketState.Open;
				return true;
			}
		}

		private bool acceptHandshake()
		{
			_logger.Debug($"A handshake request from {_context.UserEndPoint}:\n{_context}");
			if (!checkHandshakeRequest(_context, out var text))
			{
				_logger.Error(text);
				refuseHandshake(global::WebSocketSharp.CloseStatusCode.ProtocolError, "A handshake error has occurred while attempting to accept.");
				return false;
			}
			if (!customCheckHandshakeRequest(_context, out text))
			{
				_logger.Error(text);
				refuseHandshake(global::WebSocketSharp.CloseStatusCode.PolicyViolation, "A handshake error has occurred while attempting to accept.");
				return false;
			}
			_base64Key = _context.Headers["Sec-WebSocket-Key"];
			if (_protocol != null)
			{
				global::System.Collections.Generic.IEnumerable<string> secWebSocketProtocols = _context.SecWebSocketProtocols;
				processSecWebSocketProtocolClientHeader(secWebSocketProtocols);
			}
			if (!_ignoreExtensions)
			{
				string value = _context.Headers["Sec-WebSocket-Extensions"];
				processSecWebSocketExtensionsClientHeader(value);
			}
			return sendHttpResponse(createHandshakeResponse());
		}

		private bool canSet(out string message)
		{
			message = null;
			if (_readyState == global::WebSocketSharp.WebSocketState.Open)
			{
				message = "The connection has already been established.";
				return false;
			}
			if (_readyState == global::WebSocketSharp.WebSocketState.Closing)
			{
				message = "The connection is closing.";
				return false;
			}
			return true;
		}

		private bool checkHandshakeRequest(global::WebSocketSharp.Net.WebSockets.WebSocketContext context, out string message)
		{
			message = null;
			if (!context.IsWebSocketRequest)
			{
				message = "Not a handshake request.";
				return false;
			}
			if (context.RequestUri == null)
			{
				message = "It specifies an invalid Request-URI.";
				return false;
			}
			global::System.Collections.Specialized.NameValueCollection headers = context.Headers;
			string text = headers["Sec-WebSocket-Key"];
			if (text == null)
			{
				message = "It includes no Sec-WebSocket-Key header.";
				return false;
			}
			if (text.Length == 0)
			{
				message = "It includes an invalid Sec-WebSocket-Key header.";
				return false;
			}
			string text2 = headers["Sec-WebSocket-Version"];
			if (text2 == null)
			{
				message = "It includes no Sec-WebSocket-Version header.";
				return false;
			}
			if (text2 != "13")
			{
				message = "It includes an invalid Sec-WebSocket-Version header.";
				return false;
			}
			string text3 = headers["Sec-WebSocket-Protocol"];
			if (text3 != null && text3.Length == 0)
			{
				message = "It includes an invalid Sec-WebSocket-Protocol header.";
				return false;
			}
			if (!_ignoreExtensions)
			{
				string text4 = headers["Sec-WebSocket-Extensions"];
				if (text4 != null && text4.Length == 0)
				{
					message = "It includes an invalid Sec-WebSocket-Extensions header.";
					return false;
				}
			}
			return true;
		}

		private bool checkHandshakeResponse(global::WebSocketSharp.HttpResponse response, out string message)
		{
			message = null;
			if (response.IsRedirect)
			{
				message = "Indicates the redirection.";
				return false;
			}
			if (response.IsUnauthorized)
			{
				message = "Requires the authentication.";
				return false;
			}
			if (!response.IsWebSocketResponse)
			{
				message = "Not a WebSocket handshake response.";
				return false;
			}
			global::System.Collections.Specialized.NameValueCollection headers = response.Headers;
			if (!validateSecWebSocketAcceptHeader(headers["Sec-WebSocket-Accept"]))
			{
				message = "Includes no Sec-WebSocket-Accept header, or it has an invalid value.";
				return false;
			}
			if (!validateSecWebSocketProtocolServerHeader(headers["Sec-WebSocket-Protocol"]))
			{
				message = "Includes no Sec-WebSocket-Protocol header, or it has an invalid value.";
				return false;
			}
			if (!validateSecWebSocketExtensionsServerHeader(headers["Sec-WebSocket-Extensions"]))
			{
				message = "Includes an invalid Sec-WebSocket-Extensions header.";
				return false;
			}
			if (!validateSecWebSocketVersionServerHeader(headers["Sec-WebSocket-Version"]))
			{
				message = "Includes an invalid Sec-WebSocket-Version header.";
				return false;
			}
			return true;
		}

		private static bool checkProtocols(string[] protocols, out string message)
		{
			message = null;
			global::System.Func<string, bool> condition = (string protocol) => protocol.IsNullOrEmpty() || !protocol.IsToken();
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

		private bool checkReceivedFrame(global::WebSocketSharp.WebSocketFrame frame, out string message)
		{
			message = null;
			bool isMasked = frame.IsMasked;
			if (_client && isMasked)
			{
				message = "A frame from the server is masked.";
				return false;
			}
			if (!_client && !isMasked)
			{
				message = "A frame from a client is not masked.";
				return false;
			}
			if (_inContinuation && frame.IsData)
			{
				message = "A data frame has been received while receiving continuation frames.";
				return false;
			}
			if (frame.IsCompressed && _compression == global::WebSocketSharp.CompressionMethod.None)
			{
				message = "A compressed frame has been received without any agreement for it.";
				return false;
			}
			if (frame.Rsv2 == global::WebSocketSharp.Rsv.On)
			{
				message = "The RSV2 of a frame is non-zero without any negotiation for it.";
				return false;
			}
			if (frame.Rsv3 == global::WebSocketSharp.Rsv.On)
			{
				message = "The RSV3 of a frame is non-zero without any negotiation for it.";
				return false;
			}
			return true;
		}

		private void close(ushort code, string reason)
		{
			if (_readyState == global::WebSocketSharp.WebSocketState.Closing)
			{
				_logger.Info("The closing is already in progress.");
				return;
			}
			if (_readyState == global::WebSocketSharp.WebSocketState.Closed)
			{
				_logger.Info("The connection has already been closed.");
				return;
			}
			if (code == 1005)
			{
				close(global::WebSocketSharp.PayloadData.Empty, send: true, receive: true, received: false);
				return;
			}
			bool receive = !code.IsReserved();
			close(new global::WebSocketSharp.PayloadData(code, reason), receive, receive, received: false);
		}

		private void close(global::WebSocketSharp.PayloadData payloadData, bool send, bool receive, bool received)
		{
			lock (_forState)
			{
				if (_readyState == global::WebSocketSharp.WebSocketState.Closing)
				{
					_logger.Info("The closing is already in progress.");
					return;
				}
				if (_readyState == global::WebSocketSharp.WebSocketState.Closed)
				{
					_logger.Info("The connection has already been closed.");
					return;
				}
				send = send && _readyState == global::WebSocketSharp.WebSocketState.Open;
				receive = send && receive;
				_readyState = global::WebSocketSharp.WebSocketState.Closing;
			}
			_logger.Trace("Begin closing the connection.");
			bool clean = closeHandshake(payloadData, send, receive, received);
			releaseResources();
			_logger.Trace("End closing the connection.");
			_readyState = global::WebSocketSharp.WebSocketState.Closed;
			global::WebSocketSharp.CloseEventArgs e = new global::WebSocketSharp.CloseEventArgs(payloadData, clean);
			try
			{
				this.OnClose.Emit(this, e);
			}
			catch (global::System.Exception ex)
			{
				_logger.Error(ex.Message);
				_logger.Debug(ex.ToString());
			}
		}

		private void closeAsync(ushort code, string reason)
		{
			if (_readyState == global::WebSocketSharp.WebSocketState.Closing)
			{
				_logger.Info("The closing is already in progress.");
				return;
			}
			if (_readyState == global::WebSocketSharp.WebSocketState.Closed)
			{
				_logger.Info("The connection has already been closed.");
				return;
			}
			if (code == 1005)
			{
				closeAsync(global::WebSocketSharp.PayloadData.Empty, send: true, receive: true, received: false);
				return;
			}
			bool receive = !code.IsReserved();
			closeAsync(new global::WebSocketSharp.PayloadData(code, reason), receive, receive, received: false);
		}

		private void closeAsync(global::WebSocketSharp.PayloadData payloadData, bool send, bool receive, bool received)
		{
			global::System.Action<global::WebSocketSharp.PayloadData, bool, bool, bool> closer = close;
			closer.BeginInvoke(payloadData, send, receive, received, delegate(global::System.IAsyncResult ar)
			{
				closer.EndInvoke(ar);
			}, null);
		}

		private bool closeHandshake(byte[] frameAsBytes, bool receive, bool received)
		{
			bool flag = frameAsBytes != null && sendBytes(frameAsBytes);
			if (!received && flag && receive && _receivingExited != null)
			{
				received = _receivingExited.WaitOne(_waitTime);
			}
			bool flag2 = flag && received;
			_logger.Debug($"Was clean?: {flag2}\n  sent: {flag}\n  received: {received}");
			return flag2;
		}

		private bool closeHandshake(global::WebSocketSharp.PayloadData payloadData, bool send, bool receive, bool received)
		{
			bool flag = false;
			if (send)
			{
				global::WebSocketSharp.WebSocketFrame webSocketFrame = global::WebSocketSharp.WebSocketFrame.CreateCloseFrame(payloadData, _client);
				flag = sendBytes(webSocketFrame.ToArray());
				if (_client)
				{
					webSocketFrame.Unmask();
				}
			}
			if (!received && flag && receive && _receivingExited != null)
			{
				received = _receivingExited.WaitOne(_waitTime);
			}
			bool flag2 = flag && received;
			_logger.Debug($"Was clean?: {flag2}\n  sent: {flag}\n  received: {received}");
			return flag2;
		}

		private bool connect()
		{
			if (_readyState == global::WebSocketSharp.WebSocketState.Open)
			{
				string text = "The connection has already been established.";
				_logger.Warn(text);
				return false;
			}
			lock (_forState)
			{
				if (_readyState == global::WebSocketSharp.WebSocketState.Open)
				{
					string text2 = "The connection has already been established.";
					_logger.Warn(text2);
					return false;
				}
				if (_readyState == global::WebSocketSharp.WebSocketState.Closing)
				{
					string text3 = "The close process has set in.";
					_logger.Error(text3);
					text3 = "An interruption has occurred while attempting to connect.";
					error(text3, null);
					return false;
				}
				if (_retryCountForConnect > _maxRetryCountForConnect)
				{
					string text4 = "An opportunity for reconnecting has been lost.";
					_logger.Error(text4);
					text4 = "An interruption has occurred while attempting to connect.";
					error(text4, null);
					return false;
				}
				_readyState = global::WebSocketSharp.WebSocketState.Connecting;
				try
				{
					doHandshake();
				}
				catch (global::System.Exception ex)
				{
					_retryCountForConnect++;
					_logger.Fatal(ex.Message);
					_logger.Debug(ex.ToString());
					string text5 = "An exception has occurred while attempting to connect.";
					fatal(text5, ex);
					return false;
				}
				_retryCountForConnect = 1;
				_readyState = global::WebSocketSharp.WebSocketState.Open;
				return true;
			}
		}

		private string createExtensions()
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder(80);
			if (_compression != global::WebSocketSharp.CompressionMethod.None)
			{
				string arg = _compression.ToExtensionString("server_no_context_takeover", "client_no_context_takeover");
				stringBuilder.AppendFormat("{0}, ", arg);
			}
			int length = stringBuilder.Length;
			if (length > 2)
			{
				stringBuilder.Length = length - 2;
				return stringBuilder.ToString();
			}
			return null;
		}

		private global::WebSocketSharp.HttpResponse createHandshakeFailureResponse(global::WebSocketSharp.Net.HttpStatusCode code)
		{
			global::WebSocketSharp.HttpResponse httpResponse = global::WebSocketSharp.HttpResponse.CreateCloseResponse(code);
			httpResponse.Headers["Sec-WebSocket-Version"] = "13";
			return httpResponse;
		}

		private global::WebSocketSharp.HttpRequest createHandshakeRequest()
		{
			global::WebSocketSharp.HttpRequest httpRequest = global::WebSocketSharp.HttpRequest.CreateWebSocketRequest(_uri);
			global::System.Collections.Specialized.NameValueCollection headers = httpRequest.Headers;
			if (!_origin.IsNullOrEmpty())
			{
				headers["Origin"] = _origin;
			}
			headers["Sec-WebSocket-Key"] = _base64Key;
			_protocolsRequested = _protocols != null;
			if (_protocolsRequested)
			{
				headers["Sec-WebSocket-Protocol"] = _protocols.ToString(", ");
			}
			_extensionsRequested = _compression != global::WebSocketSharp.CompressionMethod.None;
			if (_extensionsRequested)
			{
				headers["Sec-WebSocket-Extensions"] = createExtensions();
			}
			headers["Sec-WebSocket-Version"] = "13";
			global::WebSocketSharp.Net.AuthenticationResponse authenticationResponse = null;
			if (_authChallenge != null && _credentials != null)
			{
				authenticationResponse = new global::WebSocketSharp.Net.AuthenticationResponse(_authChallenge, _credentials, _nonceCount);
				_nonceCount = authenticationResponse.NonceCount;
			}
			else if (_preAuth)
			{
				authenticationResponse = new global::WebSocketSharp.Net.AuthenticationResponse(_credentials);
			}
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

		private global::WebSocketSharp.HttpResponse createHandshakeResponse()
		{
			global::WebSocketSharp.HttpResponse httpResponse = global::WebSocketSharp.HttpResponse.CreateWebSocketResponse();
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

		private bool customCheckHandshakeRequest(global::WebSocketSharp.Net.WebSockets.WebSocketContext context, out string message)
		{
			message = null;
			if (_handshakeRequestChecker == null)
			{
				return true;
			}
			message = _handshakeRequestChecker(context);
			return message == null;
		}

		private global::WebSocketSharp.MessageEventArgs dequeueFromMessageEventQueue()
		{
			lock (_forMessageEventQueue)
			{
				return (_messageEventQueue.Count > 0) ? _messageEventQueue.Dequeue() : null;
			}
		}

		private void doHandshake()
		{
			setClientStream();
			global::WebSocketSharp.HttpResponse httpResponse = sendHandshakeRequest();
			if (!checkHandshakeResponse(httpResponse, out var text))
			{
				throw new global::WebSocketSharp.WebSocketException(global::WebSocketSharp.CloseStatusCode.ProtocolError, text);
			}
			if (_protocolsRequested)
			{
				_protocol = httpResponse.Headers["Sec-WebSocket-Protocol"];
			}
			if (_extensionsRequested)
			{
				processSecWebSocketExtensionsServerHeader(httpResponse.Headers["Sec-WebSocket-Extensions"]);
			}
			processCookies(httpResponse.Cookies);
		}

		private void enqueueToMessageEventQueue(global::WebSocketSharp.MessageEventArgs e)
		{
			lock (_forMessageEventQueue)
			{
				_messageEventQueue.Enqueue(e);
			}
		}

		private void error(string message, global::System.Exception exception)
		{
			try
			{
				this.OnError.Emit(this, new global::WebSocketSharp.ErrorEventArgs(message, exception));
			}
			catch (global::System.Exception ex)
			{
				_logger.Error(ex.Message);
				_logger.Debug(ex.ToString());
			}
		}

		private void fatal(string message, global::System.Exception exception)
		{
			global::WebSocketSharp.CloseStatusCode code = ((exception is global::WebSocketSharp.WebSocketException) ? ((global::WebSocketSharp.WebSocketException)exception).Code : global::WebSocketSharp.CloseStatusCode.Abnormal);
			fatal(message, (ushort)code);
		}

		private void fatal(string message, ushort code)
		{
			global::WebSocketSharp.PayloadData payloadData = new global::WebSocketSharp.PayloadData(code, message);
			close(payloadData, !code.IsReserved(), receive: false, received: false);
		}

		private void fatal(string message, global::WebSocketSharp.CloseStatusCode code)
		{
			fatal(message, (ushort)code);
		}

		private global::WebSocketSharp.Net.ClientSslConfiguration getSslConfiguration()
		{
			if (_sslConfig == null)
			{
				_sslConfig = new global::WebSocketSharp.Net.ClientSslConfiguration(_uri.DnsSafeHost);
			}
			return _sslConfig;
		}

		private void init()
		{
			_compression = global::WebSocketSharp.CompressionMethod.None;
			_cookies = new global::WebSocketSharp.Net.CookieCollection();
			_forPing = new object();
			_forSend = new object();
			_forState = new object();
			_messageEventQueue = new global::System.Collections.Generic.Queue<global::WebSocketSharp.MessageEventArgs>();
			_forMessageEventQueue = ((global::System.Collections.ICollection)_messageEventQueue).SyncRoot;
			_readyState = global::WebSocketSharp.WebSocketState.Connecting;
		}

		private void message()
		{
			global::WebSocketSharp.MessageEventArgs obj = null;
			lock (_forMessageEventQueue)
			{
				if (_inMessage || _messageEventQueue.Count == 0 || _readyState != global::WebSocketSharp.WebSocketState.Open)
				{
					return;
				}
				_inMessage = true;
				obj = _messageEventQueue.Dequeue();
			}
			_message(obj);
		}

		private void messagec(global::WebSocketSharp.MessageEventArgs e)
		{
			while (true)
			{
				try
				{
					this.OnMessage.Emit(this, e);
				}
				catch (global::System.Exception ex)
				{
					_logger.Error(ex.ToString());
					error("An error has occurred during an OnMessage event.", ex);
				}
				lock (_forMessageEventQueue)
				{
					if (_messageEventQueue.Count == 0 || _readyState != global::WebSocketSharp.WebSocketState.Open)
					{
						_inMessage = false;
						break;
					}
					e = _messageEventQueue.Dequeue();
				}
				bool flag = true;
			}
		}

		private void messages(global::WebSocketSharp.MessageEventArgs e)
		{
			try
			{
				this.OnMessage.Emit(this, e);
			}
			catch (global::System.Exception ex)
			{
				_logger.Error(ex.ToString());
				error("An error has occurred during an OnMessage event.", ex);
			}
			lock (_forMessageEventQueue)
			{
				if (_messageEventQueue.Count == 0 || _readyState != global::WebSocketSharp.WebSocketState.Open)
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
				_logger.Error(ex.ToString());
				error("An error has occurred during the OnOpen event.", ex);
			}
			global::WebSocketSharp.MessageEventArgs obj = null;
			lock (_forMessageEventQueue)
			{
				if (_messageEventQueue.Count == 0 || _readyState != global::WebSocketSharp.WebSocketState.Open)
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
			if (_readyState != global::WebSocketSharp.WebSocketState.Open)
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
					if (!send(global::WebSocketSharp.Fin.Final, global::WebSocketSharp.Opcode.Ping, data, compressed: false))
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

		private bool processCloseFrame(global::WebSocketSharp.WebSocketFrame frame)
		{
			global::WebSocketSharp.PayloadData payloadData = frame.PayloadData;
			close(payloadData, !payloadData.HasReservedCode, receive: false, received: true);
			return false;
		}

		private void processCookies(global::WebSocketSharp.Net.CookieCollection cookies)
		{
			if (cookies.Count != 0)
			{
				_cookies.SetOrRemove(cookies);
			}
		}

		private bool processDataFrame(global::WebSocketSharp.WebSocketFrame frame)
		{
			enqueueToMessageEventQueue(frame.IsCompressed ? new global::WebSocketSharp.MessageEventArgs(frame.Opcode, frame.PayloadData.ApplicationData.Decompress(_compression)) : new global::WebSocketSharp.MessageEventArgs(frame));
			return true;
		}

		private bool processFragmentFrame(global::WebSocketSharp.WebSocketFrame frame)
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
					enqueueToMessageEventQueue(new global::WebSocketSharp.MessageEventArgs(_fragmentsOpcode, rawData));
				}
				_fragmentsBuffer = null;
				_inContinuation = false;
			}
			return true;
		}

		private bool processPingFrame(global::WebSocketSharp.WebSocketFrame frame)
		{
			_logger.Trace("A ping was received.");
			global::WebSocketSharp.WebSocketFrame webSocketFrame = global::WebSocketSharp.WebSocketFrame.CreatePongFrame(frame.PayloadData, _client);
			lock (_forState)
			{
				if (_readyState != global::WebSocketSharp.WebSocketState.Open)
				{
					_logger.Error("The connection is closing.");
					return true;
				}
				if (!sendBytes(webSocketFrame.ToArray()))
				{
					return false;
				}
			}
			_logger.Trace("A pong to this ping has been sent.");
			if (_emitOnPing)
			{
				if (_client)
				{
					webSocketFrame.Unmask();
				}
				enqueueToMessageEventQueue(new global::WebSocketSharp.MessageEventArgs(frame));
			}
			return true;
		}

		private bool processPongFrame(global::WebSocketSharp.WebSocketFrame frame)
		{
			_logger.Trace("A pong was received.");
			try
			{
				_pongReceived.Set();
			}
			catch (global::System.NullReferenceException ex)
			{
				_logger.Error(ex.Message);
				_logger.Debug(ex.ToString());
				return false;
			}
			catch (global::System.ObjectDisposedException ex2)
			{
				_logger.Error(ex2.Message);
				_logger.Debug(ex2.ToString());
				return false;
			}
			_logger.Trace("It has been signaled.");
			return true;
		}

		private bool processReceivedFrame(global::WebSocketSharp.WebSocketFrame frame)
		{
			if (!checkReceivedFrame(frame, out var text))
			{
				throw new global::WebSocketSharp.WebSocketException(global::WebSocketSharp.CloseStatusCode.ProtocolError, text);
			}
			frame.Unmask();
			return frame.IsFragment ? processFragmentFrame(frame) : (frame.IsData ? processDataFrame(frame) : (frame.IsPing ? processPingFrame(frame) : (frame.IsPong ? processPongFrame(frame) : (frame.IsClose ? processCloseFrame(frame) : processUnsupportedFrame(frame)))));
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
				if (text.Length != 0 && !flag && text.IsCompressionExtension(global::WebSocketSharp.CompressionMethod.Deflate))
				{
					_compression = global::WebSocketSharp.CompressionMethod.Deflate;
					stringBuilder.AppendFormat("{0}, ", _compression.ToExtensionString("client_no_context_takeover", "server_no_context_takeover"));
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

		private void processSecWebSocketExtensionsServerHeader(string value)
		{
			if (value == null)
			{
				_compression = global::WebSocketSharp.CompressionMethod.None;
			}
			else
			{
				_extensions = value;
			}
		}

		private void processSecWebSocketProtocolClientHeader(global::System.Collections.Generic.IEnumerable<string> values)
		{
			if (!values.Contains((string val) => val == _protocol))
			{
				_protocol = null;
			}
		}

		private bool processUnsupportedFrame(global::WebSocketSharp.WebSocketFrame frame)
		{
			_logger.Fatal("An unsupported frame:" + frame.PrintToString(dumped: false));
			fatal("There is no way to handle it.", global::WebSocketSharp.CloseStatusCode.PolicyViolation);
			return false;
		}

		private void refuseHandshake(global::WebSocketSharp.CloseStatusCode code, string reason)
		{
			_readyState = global::WebSocketSharp.WebSocketState.Closing;
			global::WebSocketSharp.HttpResponse response = createHandshakeFailureResponse(global::WebSocketSharp.Net.HttpStatusCode.BadRequest);
			sendHttpResponse(response);
			releaseServerResources();
			_readyState = global::WebSocketSharp.WebSocketState.Closed;
			global::WebSocketSharp.CloseEventArgs e = new global::WebSocketSharp.CloseEventArgs((ushort)code, reason, clean: false);
			try
			{
				this.OnClose.Emit(this, e);
			}
			catch (global::System.Exception ex)
			{
				_logger.Error(ex.Message);
				_logger.Debug(ex.ToString());
			}
		}

		private void releaseClientResources()
		{
			if (_stream != null)
			{
				_stream.Dispose();
				_stream = null;
			}
			if (_tcpClient != null)
			{
				_tcpClient.Close();
				_tcpClient = null;
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
				_stream = null;
				_context = null;
			}
		}

		private bool send(global::WebSocketSharp.Opcode opcode, global::System.IO.Stream stream)
		{
			lock (_forSend)
			{
				global::System.IO.Stream stream2 = stream;
				bool flag = false;
				bool flag2 = false;
				try
				{
					if (_compression != global::WebSocketSharp.CompressionMethod.None)
					{
						stream = stream.Compress(_compression);
						flag = true;
					}
					flag2 = send(opcode, stream, flag);
					if (!flag2)
					{
						error("A send has been interrupted.", null);
					}
				}
				catch (global::System.Exception ex)
				{
					_logger.Error(ex.ToString());
					error("An error has occurred during a send.", ex);
				}
				finally
				{
					if (flag)
					{
						stream.Dispose();
					}
					stream2.Dispose();
				}
				return flag2;
			}
		}

		private bool send(global::WebSocketSharp.Opcode opcode, global::System.IO.Stream stream, bool compressed)
		{
			long length = stream.Length;
			if (length == 0)
			{
				return send(global::WebSocketSharp.Fin.Final, opcode, EmptyBytes, compressed: false);
			}
			long num = length / FragmentLength;
			int num2 = (int)(length % FragmentLength);
			byte[] array = null;
			switch (num)
			{
			case 0L:
				array = new byte[num2];
				return stream.Read(array, 0, num2) == num2 && send(global::WebSocketSharp.Fin.Final, opcode, array, compressed);
			case 1L:
				if (num2 == 0)
				{
					array = new byte[FragmentLength];
					return stream.Read(array, 0, FragmentLength) == FragmentLength && send(global::WebSocketSharp.Fin.Final, opcode, array, compressed);
				}
				break;
			}
			array = new byte[FragmentLength];
			if (stream.Read(array, 0, FragmentLength) != FragmentLength || !send(global::WebSocketSharp.Fin.More, opcode, array, compressed))
			{
				return false;
			}
			long num3 = ((num2 == 0) ? (num - 2) : (num - 1));
			for (long num4 = 0L; num4 < num3; num4++)
			{
				if (stream.Read(array, 0, FragmentLength) != FragmentLength || !send(global::WebSocketSharp.Fin.More, global::WebSocketSharp.Opcode.Cont, array, compressed: false))
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
			return stream.Read(array, 0, num2) == num2 && send(global::WebSocketSharp.Fin.Final, global::WebSocketSharp.Opcode.Cont, array, compressed: false);
		}

		private bool send(global::WebSocketSharp.Fin fin, global::WebSocketSharp.Opcode opcode, byte[] data, bool compressed)
		{
			lock (_forState)
			{
				if (_readyState != global::WebSocketSharp.WebSocketState.Open)
				{
					_logger.Error("The connection is closing.");
					return false;
				}
				global::WebSocketSharp.WebSocketFrame webSocketFrame = new global::WebSocketSharp.WebSocketFrame(fin, opcode, data, compressed, _client);
				return sendBytes(webSocketFrame.ToArray());
			}
		}

		private void sendAsync(global::WebSocketSharp.Opcode opcode, global::System.IO.Stream stream, global::System.Action<bool> completed)
		{
			global::System.Func<global::WebSocketSharp.Opcode, global::System.IO.Stream, bool> sender = send;
			sender.BeginInvoke(opcode, stream, delegate(global::System.IAsyncResult ar)
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
					_logger.Error(ex.ToString());
					error("An error has occurred during the callback for an async send.", ex);
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
				_logger.Error(ex.Message);
				_logger.Debug(ex.ToString());
				return false;
			}
			return true;
		}

		private global::WebSocketSharp.HttpResponse sendHandshakeRequest()
		{
			global::WebSocketSharp.HttpRequest httpRequest = createHandshakeRequest();
			global::WebSocketSharp.HttpResponse httpResponse = sendHttpRequest(httpRequest, 90000);
			if (httpResponse.IsUnauthorized)
			{
				string text = httpResponse.Headers["WWW-Authenticate"];
				_logger.Warn($"Received an authentication requirement for '{text}'.");
				if (text.IsNullOrEmpty())
				{
					_logger.Error("No authentication challenge is specified.");
					return httpResponse;
				}
				_authChallenge = global::WebSocketSharp.Net.AuthenticationChallenge.Parse(text);
				if (_authChallenge == null)
				{
					_logger.Error("An invalid authentication challenge is specified.");
					return httpResponse;
				}
				if (_credentials != null && (!_preAuth || _authChallenge.Scheme == global::WebSocketSharp.Net.AuthenticationSchemes.Digest))
				{
					if (httpResponse.HasConnectionClose)
					{
						releaseClientResources();
						setClientStream();
					}
					global::WebSocketSharp.Net.AuthenticationResponse authenticationResponse = new global::WebSocketSharp.Net.AuthenticationResponse(_authChallenge, _credentials, _nonceCount);
					_nonceCount = authenticationResponse.NonceCount;
					httpRequest.Headers["Authorization"] = authenticationResponse.ToString();
					httpResponse = sendHttpRequest(httpRequest, 15000);
				}
			}
			if (httpResponse.IsRedirect)
			{
				string text2 = httpResponse.Headers["Location"];
				_logger.Warn($"Received a redirection to '{text2}'.");
				if (_enableRedirection)
				{
					if (text2.IsNullOrEmpty())
					{
						_logger.Error("No url to redirect is located.");
						return httpResponse;
					}
					if (!text2.TryCreateWebSocketUri(out var result, out var text3))
					{
						_logger.Error("An invalid url to redirect is located: " + text3);
						return httpResponse;
					}
					releaseClientResources();
					_uri = result;
					_secure = result.Scheme == "wss";
					setClientStream();
					return sendHandshakeRequest();
				}
			}
			return httpResponse;
		}

		private global::WebSocketSharp.HttpResponse sendHttpRequest(global::WebSocketSharp.HttpRequest request, int millisecondsTimeout)
		{
			_logger.Debug("A request to the server:\n" + request.ToString());
			global::WebSocketSharp.HttpResponse response = request.GetResponse(_stream, millisecondsTimeout);
			_logger.Debug("A response to this request:\n" + response.ToString());
			return response;
		}

		private bool sendHttpResponse(global::WebSocketSharp.HttpResponse response)
		{
			_logger.Debug($"A response to {_context.UserEndPoint}:\n{response}");
			return sendBytes(response.ToByteArray());
		}

		private void sendProxyConnectRequest()
		{
			global::WebSocketSharp.HttpRequest httpRequest = global::WebSocketSharp.HttpRequest.CreateConnectRequest(_uri);
			global::WebSocketSharp.HttpResponse httpResponse = sendHttpRequest(httpRequest, 90000);
			if (httpResponse.IsProxyAuthenticationRequired)
			{
				string text = httpResponse.Headers["Proxy-Authenticate"];
				_logger.Warn($"Received a proxy authentication requirement for '{text}'.");
				if (text.IsNullOrEmpty())
				{
					throw new global::WebSocketSharp.WebSocketException("No proxy authentication challenge is specified.");
				}
				global::WebSocketSharp.Net.AuthenticationChallenge authenticationChallenge = global::WebSocketSharp.Net.AuthenticationChallenge.Parse(text);
				if (authenticationChallenge == null)
				{
					throw new global::WebSocketSharp.WebSocketException("An invalid proxy authentication challenge is specified.");
				}
				if (_proxyCredentials != null)
				{
					if (httpResponse.HasConnectionClose)
					{
						releaseClientResources();
						_tcpClient = new global::System.Net.Sockets.TcpClient(_proxyUri.DnsSafeHost, _proxyUri.Port);
						_stream = _tcpClient.GetStream();
					}
					global::WebSocketSharp.Net.AuthenticationResponse authenticationResponse = new global::WebSocketSharp.Net.AuthenticationResponse(authenticationChallenge, _proxyCredentials, 0u);
					httpRequest.Headers["Proxy-Authorization"] = authenticationResponse.ToString();
					httpResponse = sendHttpRequest(httpRequest, 15000);
				}
				if (httpResponse.IsProxyAuthenticationRequired)
				{
					throw new global::WebSocketSharp.WebSocketException("A proxy authentication is required.");
				}
			}
			if (httpResponse.StatusCode[0] != '2')
			{
				throw new global::WebSocketSharp.WebSocketException("The proxy has failed a connection to the requested host and port.");
			}
		}

		private void setClientStream()
		{
			if (_proxyUri != null)
			{
				_tcpClient = new global::System.Net.Sockets.TcpClient(_proxyUri.DnsSafeHost, _proxyUri.Port);
				_stream = _tcpClient.GetStream();
				sendProxyConnectRequest();
			}
			else
			{
				_tcpClient = new global::System.Net.Sockets.TcpClient(_uri.DnsSafeHost, _uri.Port);
				_stream = _tcpClient.GetStream();
			}
			if (_secure)
			{
				global::WebSocketSharp.Net.ClientSslConfiguration sslConfiguration = getSslConfiguration();
				string targetHost = sslConfiguration.TargetHost;
				if (targetHost != _uri.DnsSafeHost)
				{
					throw new global::WebSocketSharp.WebSocketException(global::WebSocketSharp.CloseStatusCode.TlsHandshakeFailure, "An invalid host name is specified.");
				}
				try
				{
					global::System.Net.Security.SslStream sslStream = new global::System.Net.Security.SslStream(_stream, leaveInnerStreamOpen: false, sslConfiguration.ServerCertificateValidationCallback, sslConfiguration.ClientCertificateSelectionCallback);
					sslStream.AuthenticateAsClient(targetHost, sslConfiguration.ClientCertificates, sslConfiguration.EnabledSslProtocols, sslConfiguration.CheckCertificateRevocation);
					_stream = sslStream;
				}
				catch (global::System.Exception innerException)
				{
					throw new global::WebSocketSharp.WebSocketException(global::WebSocketSharp.CloseStatusCode.TlsHandshakeFailure, innerException);
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
				global::WebSocketSharp.WebSocketFrame.ReadFrameAsync(_stream, unmask: false, delegate(global::WebSocketSharp.WebSocketFrame frame)
				{
					if (!processReceivedFrame(frame) || _readyState == global::WebSocketSharp.WebSocketState.Closed)
					{
						_receivingExited?.Set();
					}
					else
					{
						receive();
						if (!_inMessage && HasMessage && _readyState == global::WebSocketSharp.WebSocketState.Open)
						{
							message();
						}
					}
				}, delegate(global::System.Exception ex)
				{
					_logger.Fatal(ex.ToString());
					fatal("An exception has occurred while receiving.", ex);
				});
			};
			receive();
		}

		private bool validateSecWebSocketAcceptHeader(string value)
		{
			return value != null && value == CreateResponseKey(_base64Key);
		}

		private bool validateSecWebSocketExtensionsServerHeader(string value)
		{
			if (value == null)
			{
				return true;
			}
			if (value.Length == 0)
			{
				return false;
			}
			if (!_extensionsRequested)
			{
				return false;
			}
			bool flag = _compression != global::WebSocketSharp.CompressionMethod.None;
			foreach (string item in value.SplitHeaderValue(','))
			{
				string text = item.Trim();
				if (flag && text.IsCompressionExtension(_compression))
				{
					if (!text.Contains("server_no_context_takeover"))
					{
						_logger.Error("The server hasn't sent back 'server_no_context_takeover'.");
						return false;
					}
					if (!text.Contains("client_no_context_takeover"))
					{
						_logger.Warn("The server hasn't sent back 'client_no_context_takeover'.");
					}
					string method = _compression.ToExtensionString();
					if (text.SplitHeaderValue(';').Contains(delegate(string t)
					{
						t = t.Trim();
						return t != method && t != "server_no_context_takeover" && t != "client_no_context_takeover";
					}))
					{
						return false;
					}
					continue;
				}
				return false;
			}
			return true;
		}

		private bool validateSecWebSocketProtocolServerHeader(string value)
		{
			if (value == null)
			{
				return !_protocolsRequested;
			}
			if (value.Length == 0)
			{
				return false;
			}
			return _protocolsRequested && _protocols.Contains((string p) => p == value);
		}

		private bool validateSecWebSocketVersionServerHeader(string value)
		{
			return value == null || value == "13";
		}

		internal void Close(global::WebSocketSharp.HttpResponse response)
		{
			_readyState = global::WebSocketSharp.WebSocketState.Closing;
			sendHttpResponse(response);
			releaseServerResources();
			_readyState = global::WebSocketSharp.WebSocketState.Closed;
		}

		internal void Close(global::WebSocketSharp.Net.HttpStatusCode code)
		{
			Close(createHandshakeFailureResponse(code));
		}

		internal void Close(global::WebSocketSharp.PayloadData payloadData, byte[] frameAsBytes)
		{
			lock (_forState)
			{
				if (_readyState == global::WebSocketSharp.WebSocketState.Closing)
				{
					_logger.Info("The closing is already in progress.");
					return;
				}
				if (_readyState == global::WebSocketSharp.WebSocketState.Closed)
				{
					_logger.Info("The connection has already been closed.");
					return;
				}
				_readyState = global::WebSocketSharp.WebSocketState.Closing;
			}
			_logger.Trace("Begin closing the connection.");
			bool flag = frameAsBytes != null && sendBytes(frameAsBytes);
			bool flag2 = flag && _receivingExited != null && _receivingExited.WaitOne(_waitTime);
			bool flag3 = flag && flag2;
			_logger.Debug($"Was clean?: {flag3}\n  sent: {flag}\n  received: {flag2}");
			releaseServerResources();
			releaseCommonResources();
			_logger.Trace("End closing the connection.");
			_readyState = global::WebSocketSharp.WebSocketState.Closed;
			global::WebSocketSharp.CloseEventArgs e = new global::WebSocketSharp.CloseEventArgs(payloadData, flag3);
			try
			{
				this.OnClose.Emit(this, e);
			}
			catch (global::System.Exception ex)
			{
				_logger.Error(ex.Message);
				_logger.Debug(ex.ToString());
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
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder(base64Key, 64);
			stringBuilder.Append("258EAFA5-E914-47DA-95CA-C5AB0DC85B11");
			global::System.Security.Cryptography.SHA1 sHA = new global::System.Security.Cryptography.SHA1CryptoServiceProvider();
			byte[] inArray = sHA.ComputeHash(stringBuilder.ToString().GetUTF8EncodedBytes());
			return global::System.Convert.ToBase64String(inArray);
		}

		internal void InternalAccept()
		{
			try
			{
				if (!acceptHandshake())
				{
					return;
				}
			}
			catch (global::System.Exception ex)
			{
				_logger.Fatal(ex.Message);
				_logger.Debug(ex.ToString());
				string text = "An exception has occurred while attempting to accept.";
				fatal(text, ex);
				return;
			}
			_readyState = global::WebSocketSharp.WebSocketState.Open;
			open();
		}

		internal bool Ping(byte[] frameAsBytes, global::System.TimeSpan timeout)
		{
			if (_readyState != global::WebSocketSharp.WebSocketState.Open)
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
					lock (_forState)
					{
						if (_readyState != global::WebSocketSharp.WebSocketState.Open)
						{
							return false;
						}
						if (!sendBytes(frameAsBytes))
						{
							return false;
						}
					}
					return pongReceived.WaitOne(timeout);
				}
				catch (global::System.ObjectDisposedException)
				{
					return false;
				}
			}
		}

		internal void Send(global::WebSocketSharp.Opcode opcode, byte[] data, global::System.Collections.Generic.Dictionary<global::WebSocketSharp.CompressionMethod, byte[]> cache)
		{
			lock (_forSend)
			{
				lock (_forState)
				{
					if (_readyState != global::WebSocketSharp.WebSocketState.Open)
					{
						_logger.Error("The connection is closing.");
						return;
					}
					if (!cache.TryGetValue(_compression, out var value))
					{
						value = new global::WebSocketSharp.WebSocketFrame(global::WebSocketSharp.Fin.Final, opcode, data.Compress(_compression), _compression != global::WebSocketSharp.CompressionMethod.None, mask: false).ToArray();
						cache.Add(_compression, value);
					}
					sendBytes(value);
				}
			}
		}

		internal void Send(global::WebSocketSharp.Opcode opcode, global::System.IO.Stream stream, global::System.Collections.Generic.Dictionary<global::WebSocketSharp.CompressionMethod, global::System.IO.Stream> cache)
		{
			lock (_forSend)
			{
				if (!cache.TryGetValue(_compression, out var value))
				{
					value = stream.Compress(_compression);
					cache.Add(_compression, value);
				}
				else
				{
					value.Position = 0L;
				}
				send(opcode, value, _compression != global::WebSocketSharp.CompressionMethod.None);
			}
		}

		public void Accept()
		{
			if (_client)
			{
				string text = "This instance is a client.";
				throw new global::System.InvalidOperationException(text);
			}
			if (_readyState == global::WebSocketSharp.WebSocketState.Closing)
			{
				string text2 = "The close process is in progress.";
				throw new global::System.InvalidOperationException(text2);
			}
			if (_readyState == global::WebSocketSharp.WebSocketState.Closed)
			{
				string text3 = "The connection has already been closed.";
				throw new global::System.InvalidOperationException(text3);
			}
			if (accept())
			{
				open();
			}
		}

		public void AcceptAsync()
		{
			if (_client)
			{
				string text = "This instance is a client.";
				throw new global::System.InvalidOperationException(text);
			}
			if (_readyState == global::WebSocketSharp.WebSocketState.Closing)
			{
				string text2 = "The close process is in progress.";
				throw new global::System.InvalidOperationException(text2);
			}
			if (_readyState == global::WebSocketSharp.WebSocketState.Closed)
			{
				string text3 = "The connection has already been closed.";
				throw new global::System.InvalidOperationException(text3);
			}
			global::System.Func<bool> acceptor = accept;
			acceptor.BeginInvoke(delegate(global::System.IAsyncResult ar)
			{
				if (acceptor.EndInvoke(ar))
				{
					open();
				}
			}, null);
		}

		public void Close()
		{
			close(1005, string.Empty);
		}

		public void Close(ushort code)
		{
			if (!code.IsCloseStatusCode())
			{
				string text = "Less than 1000 or greater than 4999.";
				throw new global::System.ArgumentOutOfRangeException("code", text);
			}
			if (_client && code == 1011)
			{
				string text2 = "1011 cannot be used.";
				throw new global::System.ArgumentException(text2, "code");
			}
			if (!_client && code == 1010)
			{
				string text3 = "1010 cannot be used.";
				throw new global::System.ArgumentException(text3, "code");
			}
			close(code, string.Empty);
		}

		public void Close(global::WebSocketSharp.CloseStatusCode code)
		{
			if (_client && code == global::WebSocketSharp.CloseStatusCode.ServerError)
			{
				string text = "ServerError cannot be used.";
				throw new global::System.ArgumentException(text, "code");
			}
			if (!_client && code == global::WebSocketSharp.CloseStatusCode.MandatoryExtension)
			{
				string text2 = "MandatoryExtension cannot be used.";
				throw new global::System.ArgumentException(text2, "code");
			}
			close((ushort)code, string.Empty);
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
				string text2 = "1011 cannot be used.";
				throw new global::System.ArgumentException(text2, "code");
			}
			if (!_client && code == 1010)
			{
				string text3 = "1010 cannot be used.";
				throw new global::System.ArgumentException(text3, "code");
			}
			if (reason.IsNullOrEmpty())
			{
				close(code, string.Empty);
				return;
			}
			if (code == 1005)
			{
				string text4 = "1005 cannot be used.";
				throw new global::System.ArgumentException(text4, "code");
			}
			if (!reason.TryGetUTF8EncodedBytes(out var bytes))
			{
				string text5 = "It could not be UTF-8-encoded.";
				throw new global::System.ArgumentException(text5, "reason");
			}
			if (bytes.Length > 123)
			{
				string text6 = "Its size is greater than 123 bytes.";
				throw new global::System.ArgumentOutOfRangeException("reason", text6);
			}
			close(code, reason);
		}

		public void Close(global::WebSocketSharp.CloseStatusCode code, string reason)
		{
			if (_client && code == global::WebSocketSharp.CloseStatusCode.ServerError)
			{
				string text = "ServerError cannot be used.";
				throw new global::System.ArgumentException(text, "code");
			}
			if (!_client && code == global::WebSocketSharp.CloseStatusCode.MandatoryExtension)
			{
				string text2 = "MandatoryExtension cannot be used.";
				throw new global::System.ArgumentException(text2, "code");
			}
			if (reason.IsNullOrEmpty())
			{
				close((ushort)code, string.Empty);
				return;
			}
			if (code == global::WebSocketSharp.CloseStatusCode.NoStatus)
			{
				string text3 = "NoStatus cannot be used.";
				throw new global::System.ArgumentException(text3, "code");
			}
			if (!reason.TryGetUTF8EncodedBytes(out var bytes))
			{
				string text4 = "It could not be UTF-8-encoded.";
				throw new global::System.ArgumentException(text4, "reason");
			}
			if (bytes.Length > 123)
			{
				string text5 = "Its size is greater than 123 bytes.";
				throw new global::System.ArgumentOutOfRangeException("reason", text5);
			}
			close((ushort)code, reason);
		}

		public void CloseAsync()
		{
			closeAsync(1005, string.Empty);
		}

		public void CloseAsync(ushort code)
		{
			if (!code.IsCloseStatusCode())
			{
				string text = "Less than 1000 or greater than 4999.";
				throw new global::System.ArgumentOutOfRangeException("code", text);
			}
			if (_client && code == 1011)
			{
				string text2 = "1011 cannot be used.";
				throw new global::System.ArgumentException(text2, "code");
			}
			if (!_client && code == 1010)
			{
				string text3 = "1010 cannot be used.";
				throw new global::System.ArgumentException(text3, "code");
			}
			closeAsync(code, string.Empty);
		}

		public void CloseAsync(global::WebSocketSharp.CloseStatusCode code)
		{
			if (_client && code == global::WebSocketSharp.CloseStatusCode.ServerError)
			{
				string text = "ServerError cannot be used.";
				throw new global::System.ArgumentException(text, "code");
			}
			if (!_client && code == global::WebSocketSharp.CloseStatusCode.MandatoryExtension)
			{
				string text2 = "MandatoryExtension cannot be used.";
				throw new global::System.ArgumentException(text2, "code");
			}
			closeAsync((ushort)code, string.Empty);
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
				string text2 = "1011 cannot be used.";
				throw new global::System.ArgumentException(text2, "code");
			}
			if (!_client && code == 1010)
			{
				string text3 = "1010 cannot be used.";
				throw new global::System.ArgumentException(text3, "code");
			}
			if (reason.IsNullOrEmpty())
			{
				closeAsync(code, string.Empty);
				return;
			}
			if (code == 1005)
			{
				string text4 = "1005 cannot be used.";
				throw new global::System.ArgumentException(text4, "code");
			}
			if (!reason.TryGetUTF8EncodedBytes(out var bytes))
			{
				string text5 = "It could not be UTF-8-encoded.";
				throw new global::System.ArgumentException(text5, "reason");
			}
			if (bytes.Length > 123)
			{
				string text6 = "Its size is greater than 123 bytes.";
				throw new global::System.ArgumentOutOfRangeException("reason", text6);
			}
			closeAsync(code, reason);
		}

		public void CloseAsync(global::WebSocketSharp.CloseStatusCode code, string reason)
		{
			if (_client && code == global::WebSocketSharp.CloseStatusCode.ServerError)
			{
				string text = "ServerError cannot be used.";
				throw new global::System.ArgumentException(text, "code");
			}
			if (!_client && code == global::WebSocketSharp.CloseStatusCode.MandatoryExtension)
			{
				string text2 = "MandatoryExtension cannot be used.";
				throw new global::System.ArgumentException(text2, "code");
			}
			if (reason.IsNullOrEmpty())
			{
				closeAsync((ushort)code, string.Empty);
				return;
			}
			if (code == global::WebSocketSharp.CloseStatusCode.NoStatus)
			{
				string text3 = "NoStatus cannot be used.";
				throw new global::System.ArgumentException(text3, "code");
			}
			if (!reason.TryGetUTF8EncodedBytes(out var bytes))
			{
				string text4 = "It could not be UTF-8-encoded.";
				throw new global::System.ArgumentException(text4, "reason");
			}
			if (bytes.Length > 123)
			{
				string text5 = "Its size is greater than 123 bytes.";
				throw new global::System.ArgumentOutOfRangeException("reason", text5);
			}
			closeAsync((ushort)code, reason);
		}

		public void Connect()
		{
			if (!_client)
			{
				string text = "This instance is not a client.";
				throw new global::System.InvalidOperationException(text);
			}
			if (_readyState == global::WebSocketSharp.WebSocketState.Closing)
			{
				string text2 = "The close process is in progress.";
				throw new global::System.InvalidOperationException(text2);
			}
			if (_retryCountForConnect > _maxRetryCountForConnect)
			{
				string text3 = "A series of reconnecting has failed.";
				throw new global::System.InvalidOperationException(text3);
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
				string text = "This instance is not a client.";
				throw new global::System.InvalidOperationException(text);
			}
			if (_readyState == global::WebSocketSharp.WebSocketState.Closing)
			{
				string text2 = "The close process is in progress.";
				throw new global::System.InvalidOperationException(text2);
			}
			if (_retryCountForConnect > _maxRetryCountForConnect)
			{
				string text3 = "A series of reconnecting has failed.";
				throw new global::System.InvalidOperationException(text3);
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
				string text = "It could not be UTF-8-encoded.";
				throw new global::System.ArgumentException(text, "message");
			}
			if (bytes.Length > 125)
			{
				string text2 = "Its size is greater than 125 bytes.";
				throw new global::System.ArgumentOutOfRangeException("message", text2);
			}
			return ping(bytes);
		}

		public void Send(byte[] data)
		{
			if (_readyState != global::WebSocketSharp.WebSocketState.Open)
			{
				string text = "The current state of the connection is not Open.";
				throw new global::System.InvalidOperationException(text);
			}
			if (data == null)
			{
				throw new global::System.ArgumentNullException("data");
			}
			send(global::WebSocketSharp.Opcode.Binary, new global::System.IO.MemoryStream(data));
		}

		public void Send(global::System.IO.FileInfo fileInfo)
		{
			if (_readyState != global::WebSocketSharp.WebSocketState.Open)
			{
				string text = "The current state of the connection is not Open.";
				throw new global::System.InvalidOperationException(text);
			}
			if (fileInfo == null)
			{
				throw new global::System.ArgumentNullException("fileInfo");
			}
			if (!fileInfo.Exists)
			{
				string text2 = "The file does not exist.";
				throw new global::System.ArgumentException(text2, "fileInfo");
			}
			if (!fileInfo.TryOpenRead(out var fileStream))
			{
				string text3 = "The file could not be opened.";
				throw new global::System.ArgumentException(text3, "fileInfo");
			}
			send(global::WebSocketSharp.Opcode.Binary, fileStream);
		}

		public void Send(string data)
		{
			if (_readyState != global::WebSocketSharp.WebSocketState.Open)
			{
				string text = "The current state of the connection is not Open.";
				throw new global::System.InvalidOperationException(text);
			}
			if (data == null)
			{
				throw new global::System.ArgumentNullException("data");
			}
			if (!data.TryGetUTF8EncodedBytes(out var bytes))
			{
				string text2 = "It could not be UTF-8-encoded.";
				throw new global::System.ArgumentException(text2, "data");
			}
			send(global::WebSocketSharp.Opcode.Text, new global::System.IO.MemoryStream(bytes));
		}

		public void Send(global::System.IO.Stream stream, int length)
		{
			if (_readyState != global::WebSocketSharp.WebSocketState.Open)
			{
				string text = "The current state of the connection is not Open.";
				throw new global::System.InvalidOperationException(text);
			}
			if (stream == null)
			{
				throw new global::System.ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				string text2 = "It cannot be read.";
				throw new global::System.ArgumentException(text2, "stream");
			}
			if (length < 1)
			{
				string text3 = "Less than 1.";
				throw new global::System.ArgumentException(text3, "length");
			}
			byte[] array = stream.ReadBytes(length);
			int num = array.Length;
			if (num == 0)
			{
				string text4 = "No data could be read from it.";
				throw new global::System.ArgumentException(text4, "stream");
			}
			if (num < length)
			{
				_logger.Warn($"Only {num} byte(s) of data could be read from the stream.");
			}
			send(global::WebSocketSharp.Opcode.Binary, new global::System.IO.MemoryStream(array));
		}

		public void SendAsync(byte[] data, global::System.Action<bool> completed)
		{
			if (_readyState != global::WebSocketSharp.WebSocketState.Open)
			{
				string text = "The current state of the connection is not Open.";
				throw new global::System.InvalidOperationException(text);
			}
			if (data == null)
			{
				throw new global::System.ArgumentNullException("data");
			}
			sendAsync(global::WebSocketSharp.Opcode.Binary, new global::System.IO.MemoryStream(data), completed);
		}

		public void SendAsync(global::System.IO.FileInfo fileInfo, global::System.Action<bool> completed)
		{
			if (_readyState != global::WebSocketSharp.WebSocketState.Open)
			{
				string text = "The current state of the connection is not Open.";
				throw new global::System.InvalidOperationException(text);
			}
			if (fileInfo == null)
			{
				throw new global::System.ArgumentNullException("fileInfo");
			}
			if (!fileInfo.Exists)
			{
				string text2 = "The file does not exist.";
				throw new global::System.ArgumentException(text2, "fileInfo");
			}
			if (!fileInfo.TryOpenRead(out var fileStream))
			{
				string text3 = "The file could not be opened.";
				throw new global::System.ArgumentException(text3, "fileInfo");
			}
			sendAsync(global::WebSocketSharp.Opcode.Binary, fileStream, completed);
		}

		public void SendAsync(string data, global::System.Action<bool> completed)
		{
			if (_readyState != global::WebSocketSharp.WebSocketState.Open)
			{
				string text = "The current state of the connection is not Open.";
				throw new global::System.InvalidOperationException(text);
			}
			if (data == null)
			{
				throw new global::System.ArgumentNullException("data");
			}
			if (!data.TryGetUTF8EncodedBytes(out var bytes))
			{
				string text2 = "It could not be UTF-8-encoded.";
				throw new global::System.ArgumentException(text2, "data");
			}
			sendAsync(global::WebSocketSharp.Opcode.Text, new global::System.IO.MemoryStream(bytes), completed);
		}

		public void SendAsync(global::System.IO.Stream stream, int length, global::System.Action<bool> completed)
		{
			if (_readyState != global::WebSocketSharp.WebSocketState.Open)
			{
				string text = "The current state of the connection is not Open.";
				throw new global::System.InvalidOperationException(text);
			}
			if (stream == null)
			{
				throw new global::System.ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				string text2 = "It cannot be read.";
				throw new global::System.ArgumentException(text2, "stream");
			}
			if (length < 1)
			{
				string text3 = "Less than 1.";
				throw new global::System.ArgumentException(text3, "length");
			}
			byte[] array = stream.ReadBytes(length);
			int num = array.Length;
			if (num == 0)
			{
				string text4 = "No data could be read from it.";
				throw new global::System.ArgumentException(text4, "stream");
			}
			if (num < length)
			{
				_logger.Warn($"Only {num} byte(s) of data could be read from the stream.");
			}
			sendAsync(global::WebSocketSharp.Opcode.Binary, new global::System.IO.MemoryStream(array), completed);
		}

		public void SetCookie(global::WebSocketSharp.Net.Cookie cookie)
		{
			string text = null;
			if (!_client)
			{
				text = "This instance is not a client.";
				throw new global::System.InvalidOperationException(text);
			}
			if (cookie == null)
			{
				throw new global::System.ArgumentNullException("cookie");
			}
			if (!canSet(out text))
			{
				_logger.Warn(text);
				return;
			}
			lock (_forState)
			{
				if (!canSet(out text))
				{
					_logger.Warn(text);
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
			string text = null;
			if (!_client)
			{
				text = "This instance is not a client.";
				throw new global::System.InvalidOperationException(text);
			}
			if (!username.IsNullOrEmpty() && (global::WebSocketSharp.Ext.Contains(username, ':') || !username.IsText()))
			{
				text = "It contains an invalid character.";
				throw new global::System.ArgumentException(text, "username");
			}
			if (!password.IsNullOrEmpty() && !password.IsText())
			{
				text = "It contains an invalid character.";
				throw new global::System.ArgumentException(text, "password");
			}
			if (!canSet(out text))
			{
				_logger.Warn(text);
				return;
			}
			lock (_forState)
			{
				if (!canSet(out text))
				{
					_logger.Warn(text);
				}
				else if (username.IsNullOrEmpty())
				{
					_credentials = null;
					_preAuth = false;
				}
				else
				{
					_credentials = new global::WebSocketSharp.Net.NetworkCredential(username, password, _uri.PathAndQuery);
					_preAuth = preAuth;
				}
			}
		}

		public void SetProxy(string url, string username, string password)
		{
			string text = null;
			if (!_client)
			{
				text = "This instance is not a client.";
				throw new global::System.InvalidOperationException(text);
			}
			global::System.Uri result = null;
			if (!url.IsNullOrEmpty())
			{
				if (!global::System.Uri.TryCreate(url, global::System.UriKind.Absolute, out result))
				{
					text = "Not an absolute URI string.";
					throw new global::System.ArgumentException(text, "url");
				}
				if (result.Scheme != "http")
				{
					text = "The scheme part is not http.";
					throw new global::System.ArgumentException(text, "url");
				}
				if (result.Segments.Length > 1)
				{
					text = "It includes the path segments.";
					throw new global::System.ArgumentException(text, "url");
				}
			}
			if (!username.IsNullOrEmpty() && (global::WebSocketSharp.Ext.Contains(username, ':') || !username.IsText()))
			{
				text = "It contains an invalid character.";
				throw new global::System.ArgumentException(text, "username");
			}
			if (!password.IsNullOrEmpty() && !password.IsText())
			{
				text = "It contains an invalid character.";
				throw new global::System.ArgumentException(text, "password");
			}
			if (!canSet(out text))
			{
				_logger.Warn(text);
				return;
			}
			lock (_forState)
			{
				if (!canSet(out text))
				{
					_logger.Warn(text);
				}
				else if (url.IsNullOrEmpty())
				{
					_proxyUri = null;
					_proxyCredentials = null;
				}
				else
				{
					_proxyUri = result;
					_proxyCredentials = ((!username.IsNullOrEmpty()) ? new global::WebSocketSharp.Net.NetworkCredential(username, password, $"{_uri.DnsSafeHost}:{_uri.Port}") : null);
				}
			}
		}

		void global::System.IDisposable.Dispose()
		{
			close(1001, string.Empty);
		}
	}
}
