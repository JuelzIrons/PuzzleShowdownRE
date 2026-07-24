namespace UnityWebSocketSharp.Server
{
	internal class WebSocketServer
	{
		private global::System.Net.IPAddress _address;

		private global::UnityWebSocketSharp.Net.AuthenticationSchemes _authSchemes;

		private static readonly string _defaultRealm;

		private bool _dnsStyle;

		private string _hostname;

		private global::System.Net.Sockets.TcpListener _listener;

		private global::UnityWebSocketSharp.Logger _log;

		private int _port;

		private string _realm;

		private string _realmInUse;

		private global::System.Threading.Thread _receiveThread;

		private bool _reuseAddress;

		private bool _secure;

		private global::UnityWebSocketSharp.Server.WebSocketServiceManager _services;

		private global::UnityWebSocketSharp.Net.ServerSslConfiguration _sslConfig;

		private global::UnityWebSocketSharp.Net.ServerSslConfiguration _sslConfigInUse;

		private volatile global::UnityWebSocketSharp.Server.ServerState _state;

		private object _sync;

		private global::System.Func<global::System.Security.Principal.IIdentity, global::UnityWebSocketSharp.Net.NetworkCredential> _userCredFinder;

		public global::System.Net.IPAddress Address => _address;

		public global::UnityWebSocketSharp.Net.AuthenticationSchemes AuthenticationSchemes
		{
			get
			{
				return _authSchemes;
			}
			set
			{
				lock (_sync)
				{
					if (canSet())
					{
						_authSchemes = value;
					}
				}
			}
		}

		public bool IsListening => _state == global::UnityWebSocketSharp.Server.ServerState.Start;

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

		public global::UnityWebSocketSharp.Logger Log => _log;

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
					if (canSet())
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
					if (canSet())
					{
						_reuseAddress = value;
					}
				}
			}
		}

		public global::UnityWebSocketSharp.Net.ServerSslConfiguration SslConfiguration
		{
			get
			{
				if (!_secure)
				{
					throw new global::System.InvalidOperationException("The server does not provide secure connections.");
				}
				return getSslConfiguration();
			}
		}

		public global::System.Func<global::System.Security.Principal.IIdentity, global::UnityWebSocketSharp.Net.NetworkCredential> UserCredentialsFinder
		{
			get
			{
				return _userCredFinder;
			}
			set
			{
				lock (_sync)
				{
					if (canSet())
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

		public global::UnityWebSocketSharp.Server.WebSocketServiceManager WebSocketServices => _services;

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
				string message = "Less than 1 or greater than 65535.";
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
				throw new global::System.ArgumentException("Not a local IP address.", "address");
			}
			if (!port.IsPortNumber())
			{
				string message = "Less than 1 or greater than 65535.";
				throw new global::System.ArgumentOutOfRangeException("port", message);
			}
			init(address.ToString(), address, port, secure);
		}

		private void abort()
		{
			lock (_sync)
			{
				if (_state != global::UnityWebSocketSharp.Server.ServerState.Start)
				{
					return;
				}
				_state = global::UnityWebSocketSharp.Server.ServerState.ShuttingDown;
			}
			try
			{
				_listener.Stop();
			}
			catch (global::System.Exception ex)
			{
				_log.Fatal(ex.Message);
				_log.Debug(ex.ToString());
			}
			try
			{
				_services.Stop(1006, string.Empty);
			}
			catch (global::System.Exception ex2)
			{
				_log.Fatal(ex2.Message);
				_log.Debug(ex2.ToString());
			}
			_state = global::UnityWebSocketSharp.Server.ServerState.Stop;
		}

		private bool authenticateClient(global::UnityWebSocketSharp.Net.WebSockets.TcpListenerWebSocketContext context)
		{
			if (_authSchemes == global::UnityWebSocketSharp.Net.AuthenticationSchemes.Anonymous)
			{
				return true;
			}
			if (_authSchemes == global::UnityWebSocketSharp.Net.AuthenticationSchemes.None)
			{
				return false;
			}
			string chal = new global::UnityWebSocketSharp.Net.AuthenticationChallenge(_authSchemes, _realmInUse).ToString();
			int retry = -1;
			global::System.Func<bool> auth = null;
			auth = delegate
			{
				retry++;
				if (retry > 99)
				{
					return false;
				}
				if (context.SetUser(_authSchemes, _realmInUse, _userCredFinder))
				{
					return true;
				}
				context.SendAuthenticationChallenge(chal);
				return auth();
			};
			return auth();
		}

		private bool canSet()
		{
			if (_state != global::UnityWebSocketSharp.Server.ServerState.Ready)
			{
				return _state == global::UnityWebSocketSharp.Server.ServerState.Stop;
			}
			return true;
		}

		private bool checkHostNameForRequest(string name)
		{
			if (_dnsStyle && global::System.Uri.CheckHostName(name) == global::System.UriHostNameType.Dns)
			{
				return name == _hostname;
			}
			return true;
		}

		private string getRealm()
		{
			string realm = _realm;
			if (realm == null || realm.Length <= 0)
			{
				return _defaultRealm;
			}
			return realm;
		}

		private global::UnityWebSocketSharp.Net.ServerSslConfiguration getSslConfiguration()
		{
			if (_sslConfig == null)
			{
				_sslConfig = new global::UnityWebSocketSharp.Net.ServerSslConfiguration();
			}
			return _sslConfig;
		}

		private void init(string hostname, global::System.Net.IPAddress address, int port, bool secure)
		{
			_hostname = hostname;
			_address = address;
			_port = port;
			_secure = secure;
			_authSchemes = global::UnityWebSocketSharp.Net.AuthenticationSchemes.Anonymous;
			_dnsStyle = global::System.Uri.CheckHostName(hostname) == global::System.UriHostNameType.Dns;
			_listener = new global::System.Net.Sockets.TcpListener(address, port);
			_log = new global::UnityWebSocketSharp.Logger();
			_services = new global::UnityWebSocketSharp.Server.WebSocketServiceManager(_log);
			_sync = new object();
		}

		private void processRequest(global::UnityWebSocketSharp.Net.WebSockets.TcpListenerWebSocketContext context)
		{
			if (!authenticateClient(context))
			{
				context.Close(global::UnityWebSocketSharp.Net.HttpStatusCode.Forbidden);
				return;
			}
			global::System.Uri requestUri = context.RequestUri;
			if (requestUri == null)
			{
				context.Close(global::UnityWebSocketSharp.Net.HttpStatusCode.BadRequest);
				return;
			}
			string dnsSafeHost = requestUri.DnsSafeHost;
			if (!checkHostNameForRequest(dnsSafeHost))
			{
				context.Close(global::UnityWebSocketSharp.Net.HttpStatusCode.NotFound);
				return;
			}
			string text = requestUri.AbsolutePath;
			if (text.IndexOfAny(new char[2] { '%', '+' }) > -1)
			{
				text = global::UnityWebSocketSharp.Net.HttpUtility.UrlDecode(text, global::System.Text.Encoding.UTF8);
			}
			if (!_services.InternalTryGetServiceHost(text, out var host))
			{
				context.Close(global::UnityWebSocketSharp.Net.HttpStatusCode.NotImplemented);
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
							global::UnityWebSocketSharp.Net.WebSockets.TcpListenerWebSocketContext context = new global::UnityWebSocketSharp.Net.WebSockets.TcpListenerWebSocketContext(cl, null, _secure, _sslConfigInUse, _log);
							processRequest(context);
						}
						catch (global::System.Exception ex4)
						{
							_log.Error(ex4.Message);
							_log.Debug(ex4.ToString());
							cl.Close();
						}
					});
				}
				catch (global::System.Net.Sockets.SocketException ex)
				{
					if (_state == global::UnityWebSocketSharp.Server.ServerState.ShuttingDown)
					{
						return;
					}
					_log.Fatal(ex.Message);
					_log.Debug(ex.ToString());
					break;
				}
				catch (global::System.InvalidOperationException ex2)
				{
					if (_state == global::UnityWebSocketSharp.Server.ServerState.ShuttingDown)
					{
						return;
					}
					_log.Fatal(ex2.Message);
					_log.Debug(ex2.ToString());
					break;
				}
				catch (global::System.Exception ex3)
				{
					_log.Fatal(ex3.Message);
					_log.Debug(ex3.ToString());
					if (cl != null)
					{
						cl.Close();
					}
					if (_state == global::UnityWebSocketSharp.Server.ServerState.ShuttingDown)
					{
						return;
					}
					break;
				}
			}
			abort();
		}

		private void start()
		{
			lock (_sync)
			{
				if (_state == global::UnityWebSocketSharp.Server.ServerState.Start || _state == global::UnityWebSocketSharp.Server.ServerState.ShuttingDown)
				{
					return;
				}
				if (_secure)
				{
					global::UnityWebSocketSharp.Net.ServerSslConfiguration serverSslConfiguration = new global::UnityWebSocketSharp.Net.ServerSslConfiguration(getSslConfiguration());
					if (serverSslConfiguration.ServerCertificate == null)
					{
						throw new global::System.InvalidOperationException("There is no server certificate for secure connection.");
					}
					_sslConfigInUse = serverSslConfiguration;
				}
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
				_state = global::UnityWebSocketSharp.Server.ServerState.Start;
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
				throw new global::System.InvalidOperationException("The underlying listener has failed to start.", innerException);
			}
			global::System.Threading.ThreadStart threadStart = receiveRequest;
			_receiveThread = new global::System.Threading.Thread(threadStart);
			_receiveThread.IsBackground = true;
			_receiveThread.Start();
		}

		private void stop(ushort code, string reason)
		{
			lock (_sync)
			{
				if (_state != global::UnityWebSocketSharp.Server.ServerState.Start)
				{
					return;
				}
				_state = global::UnityWebSocketSharp.Server.ServerState.ShuttingDown;
			}
			try
			{
				int millisecondsTimeout = 5000;
				stopReceiving(millisecondsTimeout);
			}
			catch (global::System.Exception ex)
			{
				_log.Fatal(ex.Message);
				_log.Debug(ex.ToString());
			}
			try
			{
				_services.Stop(code, reason);
			}
			catch (global::System.Exception ex2)
			{
				_log.Fatal(ex2.Message);
				_log.Debug(ex2.ToString());
			}
			_state = global::UnityWebSocketSharp.Server.ServerState.Stop;
		}

		private void stopReceiving(int millisecondsTimeout)
		{
			_listener.Stop();
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

		public void AddWebSocketService<TBehavior>(string path) where TBehavior : global::UnityWebSocketSharp.Server.WebSocketBehavior, new()
		{
			_services.AddService<TBehavior>(path, null);
		}

		public void AddWebSocketService<TBehavior>(string path, global::System.Action<TBehavior> initializer) where TBehavior : global::UnityWebSocketSharp.Server.WebSocketBehavior, new()
		{
			_services.AddService(path, initializer);
		}

		public bool RemoveWebSocketService(string path)
		{
			return _services.RemoveService(path);
		}

		public void Start()
		{
			if (_state != global::UnityWebSocketSharp.Server.ServerState.Start && _state != global::UnityWebSocketSharp.Server.ServerState.ShuttingDown)
			{
				start();
			}
		}

		public void Stop()
		{
			if (_state == global::UnityWebSocketSharp.Server.ServerState.Start)
			{
				stop(1001, string.Empty);
			}
		}
	}
}
