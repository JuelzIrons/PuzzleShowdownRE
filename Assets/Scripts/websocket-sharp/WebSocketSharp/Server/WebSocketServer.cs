namespace WebSocketSharp.Server
{
	public class WebSocketServer
	{
		private global::System.Net.IPAddress _address;

		private bool _allowForwardedRequest;

		private global::WebSocketSharp.Net.AuthenticationSchemes _authSchemes;

		private static readonly string _defaultRealm;

		private bool _dnsStyle;

		private string _hostname;

		private global::System.Net.Sockets.TcpListener _listener;

		private global::WebSocketSharp.Logger _log;

		private int _port;

		private string _realm;

		private string _realmInUse;

		private global::System.Threading.Thread _receiveThread;

		private bool _reuseAddress;

		private bool _secure;

		private global::WebSocketSharp.Server.WebSocketServiceManager _services;

		private global::WebSocketSharp.Net.ServerSslConfiguration _sslConfig;

		private global::WebSocketSharp.Net.ServerSslConfiguration _sslConfigInUse;

		private volatile global::WebSocketSharp.Server.ServerState _state;

		private object _sync;

		private global::System.Func<global::System.Security.Principal.IIdentity, global::WebSocketSharp.Net.NetworkCredential> _userCredFinder;

		public global::System.Net.IPAddress Address => _address;

		public bool AllowForwardedRequest
		{
			get
			{
				return _allowForwardedRequest;
			}
			set
			{
				lock (_sync)
				{
					if (!canSet(out var message))
					{
						_log.Warn(message);
					}
					else
					{
						_allowForwardedRequest = value;
					}
				}
			}
		}

		public global::WebSocketSharp.Net.AuthenticationSchemes AuthenticationSchemes
		{
			get
			{
				return _authSchemes;
			}
			set
			{
				lock (_sync)
				{
					if (!canSet(out var message))
					{
						_log.Warn(message);
					}
					else
					{
						_authSchemes = value;
					}
				}
			}
		}

		public bool IsListening => _state == global::WebSocketSharp.Server.ServerState.Start;

		public bool IsSecure => _secure;

		public bool KeepClean
		{
			get
			{
				return _services.KeepClean;
			}
			set
			{
				_services.KeepClean = value;
			}
		}

		public global::WebSocketSharp.Logger Log => _log;

		public int Port => _port;

		public string Realm
		{
			get
			{
				return _realm;
			}
			set
			{
				lock (_sync)
				{
					if (!canSet(out var message))
					{
						_log.Warn(message);
					}
					else
					{
						_realm = value;
					}
				}
			}
		}

		public bool ReuseAddress
		{
			get
			{
				return _reuseAddress;
			}
			set
			{
				lock (_sync)
				{
					if (!canSet(out var message))
					{
						_log.Warn(message);
					}
					else
					{
						_reuseAddress = value;
					}
				}
			}
		}

		public global::WebSocketSharp.Net.ServerSslConfiguration SslConfiguration
		{
			get
			{
				if (!_secure)
				{
					string message = "The server does not provide secure connections.";
					throw new global::System.InvalidOperationException(message);
				}
				return getSslConfiguration();
			}
		}

		public global::System.Func<global::System.Security.Principal.IIdentity, global::WebSocketSharp.Net.NetworkCredential> UserCredentialsFinder
		{
			get
			{
				return _userCredFinder;
			}
			set
			{
				lock (_sync)
				{
					if (!canSet(out var message))
					{
						_log.Warn(message);
					}
					else
					{
						_userCredFinder = value;
					}
				}
			}
		}

		public global::System.TimeSpan WaitTime
		{
			get
			{
				return _services.WaitTime;
			}
			set
			{
				_services.WaitTime = value;
			}
		}

		public global::WebSocketSharp.Server.WebSocketServiceManager WebSocketServices => _services;

		static WebSocketServer()
		{
			_defaultRealm = "SECRET AREA";
		}

		public WebSocketServer()
		{
			global::System.Net.IPAddress any = global::System.Net.IPAddress.Any;
			init(any.ToString(), any, 80, secure: false);
		}

		public WebSocketServer(int port)
			: this(port, port == 443)
		{
		}

		public WebSocketServer(string url)
		{
			if (url == null)
			{
				throw new global::System.ArgumentNullException("url");
			}
			if (url.Length == 0)
			{
				throw new global::System.ArgumentException("An empty string.", "url");
			}
			if (!tryCreateUri(url, out var result, out var message))
			{
				throw new global::System.ArgumentException(message, "url");
			}
			string dnsSafeHost = result.DnsSafeHost;
			global::System.Net.IPAddress iPAddress = dnsSafeHost.ToIPAddress();
			if (iPAddress == null)
			{
				message = "The host part could not be converted to an IP address.";
				throw new global::System.ArgumentException(message, "url");
			}
			if (!iPAddress.IsLocal())
			{
				message = "The IP address of the host is not a local IP address.";
				throw new global::System.ArgumentException(message, "url");
			}
			init(dnsSafeHost, iPAddress, result.Port, result.Scheme == "wss");
		}

		public WebSocketServer(int port, bool secure)
		{
			if (!port.IsPortNumber())
			{
				string message = "It is less than 1 or greater than 65535.";
				throw new global::System.ArgumentOutOfRangeException("port", message);
			}
			global::System.Net.IPAddress any = global::System.Net.IPAddress.Any;
			init(any.ToString(), any, port, secure);
		}

		public WebSocketServer(global::System.Net.IPAddress address, int port)
			: this(address, port, port == 443)
		{
		}

		public WebSocketServer(global::System.Net.IPAddress address, int port, bool secure)
		{
			if (address == null)
			{
				throw new global::System.ArgumentNullException("address");
			}
			if (!address.IsLocal())
			{
				string message = "It is not a local IP address.";
				throw new global::System.ArgumentException(message, "address");
			}
			if (!port.IsPortNumber())
			{
				string message2 = "It is less than 1 or greater than 65535.";
				throw new global::System.ArgumentOutOfRangeException("port", message2);
			}
			init(address.ToString(), address, port, secure);
		}

		private void abort()
		{
			lock (_sync)
			{
				if (_state != global::WebSocketSharp.Server.ServerState.Start)
				{
					return;
				}
				_state = global::WebSocketSharp.Server.ServerState.ShuttingDown;
			}
			try
			{
				try
				{
					_listener.Stop();
				}
				finally
				{
					_services.Stop(1006, string.Empty);
				}
			}
			catch
			{
			}
			_state = global::WebSocketSharp.Server.ServerState.Stop;
		}

		private bool authenticateClient(global::WebSocketSharp.Net.WebSockets.TcpListenerWebSocketContext context)
		{
			if (_authSchemes == global::WebSocketSharp.Net.AuthenticationSchemes.Anonymous)
			{
				return true;
			}
			if (_authSchemes == global::WebSocketSharp.Net.AuthenticationSchemes.None)
			{
				return false;
			}
			return context.Authenticate(_authSchemes, _realmInUse, _userCredFinder);
		}

		private bool canSet(out string message)
		{
			message = null;
			if (_state == global::WebSocketSharp.Server.ServerState.Start)
			{
				message = "The server has already started.";
				return false;
			}
			if (_state == global::WebSocketSharp.Server.ServerState.ShuttingDown)
			{
				message = "The server is shutting down.";
				return false;
			}
			return true;
		}

		private bool checkHostNameForRequest(string name)
		{
			return !_dnsStyle || global::System.Uri.CheckHostName(name) != global::System.UriHostNameType.Dns || name == _hostname;
		}

		private string getRealm()
		{
			string realm = _realm;
			return (realm != null && realm.Length > 0) ? realm : _defaultRealm;
		}

		private global::WebSocketSharp.Net.ServerSslConfiguration getSslConfiguration()
		{
			if (_sslConfig == null)
			{
				_sslConfig = new global::WebSocketSharp.Net.ServerSslConfiguration();
			}
			return _sslConfig;
		}

		private void init(string hostname, global::System.Net.IPAddress address, int port, bool secure)
		{
			_hostname = hostname;
			_address = address;
			_port = port;
			_secure = secure;
			_authSchemes = global::WebSocketSharp.Net.AuthenticationSchemes.Anonymous;
			_dnsStyle = global::System.Uri.CheckHostName(hostname) == global::System.UriHostNameType.Dns;
			_listener = new global::System.Net.Sockets.TcpListener(address, port);
			_log = new global::WebSocketSharp.Logger();
			_services = new global::WebSocketSharp.Server.WebSocketServiceManager(_log);
			_sync = new object();
		}

		private void processRequest(global::WebSocketSharp.Net.WebSockets.TcpListenerWebSocketContext context)
		{
			if (!authenticateClient(context))
			{
				context.Close(global::WebSocketSharp.Net.HttpStatusCode.Forbidden);
				return;
			}
			global::System.Uri requestUri = context.RequestUri;
			if (requestUri == null)
			{
				context.Close(global::WebSocketSharp.Net.HttpStatusCode.BadRequest);
				return;
			}
			if (!_allowForwardedRequest)
			{
				if (requestUri.Port != _port)
				{
					context.Close(global::WebSocketSharp.Net.HttpStatusCode.BadRequest);
					return;
				}
				if (!checkHostNameForRequest(requestUri.DnsSafeHost))
				{
					context.Close(global::WebSocketSharp.Net.HttpStatusCode.NotFound);
					return;
				}
			}
			string text = requestUri.AbsolutePath;
			if (text.IndexOfAny(new char[2] { '%', '+' }) > -1)
			{
				text = global::WebSocketSharp.Net.HttpUtility.UrlDecode(text, global::System.Text.Encoding.UTF8);
			}
			if (!_services.InternalTryGetServiceHost(text, out var host))
			{
				context.Close(global::WebSocketSharp.Net.HttpStatusCode.NotImplemented);
			}
			else
			{
				host.StartSession(context);
			}
		}

		private void receiveRequest()
		{
			while (true)
			{
				global::System.Net.Sockets.TcpClient cl = null;
				try
				{
					cl = _listener.AcceptTcpClient();
					global::System.Threading.ThreadPool.QueueUserWorkItem(delegate
					{
						try
						{
							global::WebSocketSharp.Net.WebSockets.TcpListenerWebSocketContext context = new global::WebSocketSharp.Net.WebSockets.TcpListenerWebSocketContext(cl, null, _secure, _sslConfigInUse, _log);
							processRequest(context);
						}
						catch (global::System.Exception ex3)
						{
							_log.Error(ex3.Message);
							_log.Debug(ex3.ToString());
							cl.Close();
						}
					});
				}
				catch (global::System.Net.Sockets.SocketException ex)
				{
					if (_state == global::WebSocketSharp.Server.ServerState.ShuttingDown)
					{
						_log.Info("The underlying listener is stopped.");
						break;
					}
					_log.Fatal(ex.Message);
					_log.Debug(ex.ToString());
					break;
				}
				catch (global::System.Exception ex2)
				{
					_log.Fatal(ex2.Message);
					_log.Debug(ex2.ToString());
					if (cl != null)
					{
						cl.Close();
					}
					break;
				}
			}
			if (_state != global::WebSocketSharp.Server.ServerState.ShuttingDown)
			{
				abort();
			}
		}

		private void start(global::WebSocketSharp.Net.ServerSslConfiguration sslConfig)
		{
			lock (_sync)
			{
				if (_state == global::WebSocketSharp.Server.ServerState.Start)
				{
					_log.Info("The server has already started.");
					return;
				}
				if (_state == global::WebSocketSharp.Server.ServerState.ShuttingDown)
				{
					_log.Warn("The server is shutting down.");
					return;
				}
				_sslConfigInUse = sslConfig;
				_realmInUse = getRealm();
				_services.Start();
				try
				{
					startReceiving();
				}
				catch
				{
					_services.Stop(1011, string.Empty);
					throw;
				}
				_state = global::WebSocketSharp.Server.ServerState.Start;
			}
		}

		private void startReceiving()
		{
			if (_reuseAddress)
			{
				_listener.Server.SetSocketOption(global::System.Net.Sockets.SocketOptionLevel.Socket, global::System.Net.Sockets.SocketOptionName.ReuseAddress, optionValue: true);
			}
			try
			{
				_listener.Start();
			}
			catch (global::System.Exception innerException)
			{
				string message = "The underlying listener has failed to start.";
				throw new global::System.InvalidOperationException(message, innerException);
			}
			_receiveThread = new global::System.Threading.Thread(receiveRequest);
			_receiveThread.IsBackground = true;
			_receiveThread.Start();
		}

		private void stop(ushort code, string reason)
		{
			lock (_sync)
			{
				if (_state == global::WebSocketSharp.Server.ServerState.ShuttingDown)
				{
					_log.Info("The server is shutting down.");
					return;
				}
				if (_state == global::WebSocketSharp.Server.ServerState.Stop)
				{
					_log.Info("The server has already stopped.");
					return;
				}
				_state = global::WebSocketSharp.Server.ServerState.ShuttingDown;
			}
			try
			{
				bool flag = false;
				try
				{
					stopReceiving(5000);
				}
				catch
				{
					flag = true;
					throw;
				}
				finally
				{
					try
					{
						_services.Stop(code, reason);
					}
					catch
					{
						if (!flag)
						{
							throw;
						}
					}
				}
			}
			finally
			{
				_state = global::WebSocketSharp.Server.ServerState.Stop;
			}
		}

		private void stopReceiving(int millisecondsTimeout)
		{
			try
			{
				_listener.Stop();
			}
			catch (global::System.Exception innerException)
			{
				string message = "The underlying listener has failed to stop.";
				throw new global::System.InvalidOperationException(message, innerException);
			}
			_receiveThread.Join(millisecondsTimeout);
		}

		private static bool tryCreateUri(string uriString, out global::System.Uri result, out string message)
		{
			if (!uriString.TryCreateWebSocketUri(out result, out message))
			{
				return false;
			}
			if (result.PathAndQuery != "/")
			{
				result = null;
				message = "It includes either or both path and query components.";
				return false;
			}
			return true;
		}

		public void AddWebSocketService<TBehavior>(string path) where TBehavior : global::WebSocketSharp.Server.WebSocketBehavior, new()
		{
			_services.AddService<TBehavior>(path, null);
		}

		public void AddWebSocketService<TBehavior>(string path, global::System.Action<TBehavior> initializer) where TBehavior : global::WebSocketSharp.Server.WebSocketBehavior, new()
		{
			_services.AddService(path, initializer);
		}

		public bool RemoveWebSocketService(string path)
		{
			return _services.RemoveService(path);
		}

		public void Start()
		{
			global::WebSocketSharp.Net.ServerSslConfiguration serverSslConfiguration = null;
			if (_secure)
			{
				global::WebSocketSharp.Net.ServerSslConfiguration sslConfiguration = getSslConfiguration();
				serverSslConfiguration = new global::WebSocketSharp.Net.ServerSslConfiguration(sslConfiguration);
				if (serverSslConfiguration.ServerCertificate == null)
				{
					string message = "There is no server certificate for secure connection.";
					throw new global::System.InvalidOperationException(message);
				}
			}
			if (_state == global::WebSocketSharp.Server.ServerState.Start)
			{
				_log.Info("The server has already started.");
			}
			else if (_state == global::WebSocketSharp.Server.ServerState.ShuttingDown)
			{
				_log.Warn("The server is shutting down.");
			}
			else
			{
				start(serverSslConfiguration);
			}
		}

		public void Stop()
		{
			if (_state == global::WebSocketSharp.Server.ServerState.Ready)
			{
				_log.Info("The server is not started.");
			}
			else if (_state == global::WebSocketSharp.Server.ServerState.ShuttingDown)
			{
				_log.Info("The server is shutting down.");
			}
			else if (_state == global::WebSocketSharp.Server.ServerState.Stop)
			{
				_log.Info("The server has already stopped.");
			}
			else
			{
				stop(1001, string.Empty);
			}
		}
	}
}
