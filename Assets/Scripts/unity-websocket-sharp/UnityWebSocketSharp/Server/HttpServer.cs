namespace UnityWebSocketSharp.Server
{
	internal class HttpServer
	{
		private global::System.Net.IPAddress _address;

		private string _docRootPath;

		private string _hostname;

		private global::UnityWebSocketSharp.Net.HttpListener _listener;

		private global::UnityWebSocketSharp.Logger _log;

		private int _port;

		private global::System.Threading.Thread _receiveThread;

		private bool _secure;

		private global::UnityWebSocketSharp.Server.WebSocketServiceManager _services;

		private volatile global::UnityWebSocketSharp.Server.ServerState _state;

		private object _sync;

		public global::System.Net.IPAddress Address => _address;

		public global::UnityWebSocketSharp.Net.AuthenticationSchemes AuthenticationSchemes
		{
			get
			{
				return _listener.AuthenticationSchemes;
			}
			set
			{
				lock (_sync)
				{
					if (canSet())
					{
						_listener.AuthenticationSchemes = value;
					}
				}
			}
		}

		public string DocumentRootPath
		{
			get
			{
				return _docRootPath;
			}
			set
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				if (value.Length == 0)
				{
					throw new global::System.ArgumentException("An empty string.", "value");
				}
				value = value.TrimSlashOrBackslashFromEnd();
				if (value == "/")
				{
					throw new global::System.ArgumentException("An absolute root.", "value");
				}
				if (value == "\\")
				{
					throw new global::System.ArgumentException("An absolute root.", "value");
				}
				if (value.Length == 2 && value[1] == ':')
				{
					throw new global::System.ArgumentException("An absolute root.", "value");
				}
				string text = null;
				try
				{
					text = global::System.IO.Path.GetFullPath(value);
				}
				catch (global::System.Exception innerException)
				{
					throw new global::System.ArgumentException("An invalid path string.", "value", innerException);
				}
				if (text == "/")
				{
					throw new global::System.ArgumentException("An absolute root.", "value");
				}
				text = text.TrimSlashOrBackslashFromEnd();
				if (text.Length == 2 && text[1] == ':')
				{
					throw new global::System.ArgumentException("An absolute root.", "value");
				}
				lock (_sync)
				{
					if (canSet())
					{
						_docRootPath = value;
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
				return _listener.Realm;
			}
			set
			{
				lock (_sync)
				{
					if (canSet())
					{
						_listener.Realm = value;
					}
				}
			}
		}

		public bool ReuseAddress
		{
			get
			{
				return _listener.ReuseAddress;
			}
			set
			{
				lock (_sync)
				{
					if (canSet())
					{
						_listener.ReuseAddress = value;
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
				return _listener.SslConfiguration;
			}
		}

		public global::System.Func<global::System.Security.Principal.IIdentity, global::UnityWebSocketSharp.Net.NetworkCredential> UserCredentialsFinder
		{
			get
			{
				return _listener.UserCredentialsFinder;
			}
			set
			{
				lock (_sync)
				{
					if (canSet())
					{
						_listener.UserCredentialsFinder = value;
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

		public event global::System.EventHandler<global::UnityWebSocketSharp.Server.HttpRequestEventArgs> OnConnect;

		public event global::System.EventHandler<global::UnityWebSocketSharp.Server.HttpRequestEventArgs> OnDelete;

		public event global::System.EventHandler<global::UnityWebSocketSharp.Server.HttpRequestEventArgs> OnGet;

		public event global::System.EventHandler<global::UnityWebSocketSharp.Server.HttpRequestEventArgs> OnHead;

		public event global::System.EventHandler<global::UnityWebSocketSharp.Server.HttpRequestEventArgs> OnOptions;

		public event global::System.EventHandler<global::UnityWebSocketSharp.Server.HttpRequestEventArgs> OnPost;

		public event global::System.EventHandler<global::UnityWebSocketSharp.Server.HttpRequestEventArgs> OnPut;

		public event global::System.EventHandler<global::UnityWebSocketSharp.Server.HttpRequestEventArgs> OnTrace;

		public HttpServer()
		{
			init("*", global::System.Net.IPAddress.Any, 80, secure: false);
		}

		public HttpServer(int port)
			: this(port, port == 443)
		{
		}

		public HttpServer(string url)
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
			string dnsSafeHost = result.GetDnsSafeHost(bracketIPv6: true);
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
			init(dnsSafeHost, iPAddress, result.Port, result.Scheme == "https");
		}

		public HttpServer(int port, bool secure)
		{
			if (!port.IsPortNumber())
			{
				string message = "Less than 1 or greater than 65535.";
				throw new global::System.ArgumentOutOfRangeException("port", message);
			}
			init("*", global::System.Net.IPAddress.Any, port, secure);
		}

		public HttpServer(global::System.Net.IPAddress address, int port)
			: this(address, port, port == 443)
		{
		}

		public HttpServer(global::System.Net.IPAddress address, int port, bool secure)
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
			init(address.ToString(bracketIPv6: true), address, port, secure);
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
				_services.Stop(1006, string.Empty);
			}
			catch (global::System.Exception ex)
			{
				_log.Fatal(ex.Message);
				_log.Debug(ex.ToString());
			}
			try
			{
				_listener.Abort();
			}
			catch (global::System.Exception ex2)
			{
				_log.Fatal(ex2.Message);
				_log.Debug(ex2.ToString());
			}
			_state = global::UnityWebSocketSharp.Server.ServerState.Stop;
		}

		private bool canSet()
		{
			if (_state != global::UnityWebSocketSharp.Server.ServerState.Ready)
			{
				return _state == global::UnityWebSocketSharp.Server.ServerState.Stop;
			}
			return true;
		}

		private bool checkCertificate(out string message)
		{
			message = null;
			bool flag = _listener.SslConfiguration.ServerCertificate != null;
			string certificateFolderPath = _listener.CertificateFolderPath;
			bool flag2 = global::UnityWebSocketSharp.Net.EndPointListener.CertificateExists(_port, certificateFolderPath);
			if (!(flag || flag2))
			{
				message = "There is no server certificate for secure connection.";
				return false;
			}
			if (flag && flag2)
			{
				string message2 = "The server certificate associated with the port is used.";
				_log.Warn(message2);
			}
			return true;
		}

		private static global::UnityWebSocketSharp.Net.HttpListener createListener(string hostname, int port, bool secure)
		{
			global::UnityWebSocketSharp.Net.HttpListener httpListener = new global::UnityWebSocketSharp.Net.HttpListener();
			string arg = (secure ? "https" : "http");
			string uriPrefix = $"{arg}://{hostname}:{port}/";
			httpListener.Prefixes.Add(uriPrefix);
			return httpListener;
		}

		private void init(string hostname, global::System.Net.IPAddress address, int port, bool secure)
		{
			_hostname = hostname;
			_address = address;
			_port = port;
			_secure = secure;
			_docRootPath = "./Public";
			_listener = createListener(_hostname, _port, _secure);
			_log = _listener.Log;
			_services = new global::UnityWebSocketSharp.Server.WebSocketServiceManager(_log);
			_sync = new object();
		}

		private void processRequest(global::UnityWebSocketSharp.Net.HttpListenerContext context)
		{
			global::System.EventHandler<global::UnityWebSocketSharp.Server.HttpRequestEventArgs> eventHandler = context.Request.HttpMethod switch
			{
				"TRACE" => this.OnTrace, 
				"OPTIONS" => this.OnOptions, 
				"CONNECT" => this.OnConnect, 
				"DELETE" => this.OnDelete, 
				"PUT" => this.OnPut, 
				"POST" => this.OnPost, 
				"HEAD" => this.OnHead, 
				"GET" => this.OnGet, 
				_ => null, 
			};
			if (eventHandler == null)
			{
				context.ErrorStatusCode = 501;
				context.SendError();
			}
			else
			{
				global::UnityWebSocketSharp.Server.HttpRequestEventArgs e = new global::UnityWebSocketSharp.Server.HttpRequestEventArgs(context, _docRootPath);
				eventHandler(this, e);
				context.Response.Close();
			}
		}

		private void processRequest(global::UnityWebSocketSharp.Net.WebSockets.HttpListenerWebSocketContext context)
		{
			global::System.Uri requestUri = context.RequestUri;
			if (requestUri == null)
			{
				context.Close(global::UnityWebSocketSharp.Net.HttpStatusCode.BadRequest);
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
				global::UnityWebSocketSharp.Net.HttpListenerContext ctx = null;
				try
				{
					ctx = _listener.GetContext();
					global::System.Threading.ThreadPool.QueueUserWorkItem(delegate
					{
						try
						{
							if (ctx.Request.IsUpgradeRequest("websocket"))
							{
								processRequest(ctx.GetWebSocketContext(null));
							}
							else
							{
								processRequest(ctx);
							}
						}
						catch (global::System.Exception ex4)
						{
							_log.Error(ex4.Message);
							_log.Debug(ex4.ToString());
							ctx.Connection.Close(force: true);
						}
					});
				}
				catch (global::UnityWebSocketSharp.Net.HttpListenerException ex)
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
					if (ctx != null)
					{
						ctx.Connection.Close(force: true);
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
				if (_state != global::UnityWebSocketSharp.Server.ServerState.Start && _state != global::UnityWebSocketSharp.Server.ServerState.ShuttingDown)
				{
					if (_secure && !checkCertificate(out var message))
					{
						throw new global::System.InvalidOperationException(message);
					}
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
		}

		private void startReceiving()
		{
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
				_services.Stop(code, reason);
			}
			catch (global::System.Exception ex)
			{
				_log.Fatal(ex.Message);
				_log.Debug(ex.ToString());
			}
			try
			{
				int millisecondsTimeout = 5000;
				stopReceiving(millisecondsTimeout);
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
			result = null;
			message = null;
			global::System.Uri uri = uriString.ToUri();
			if (uri == null)
			{
				message = "An invalid URI string.";
				return false;
			}
			if (!uri.IsAbsoluteUri)
			{
				message = "A relative URI.";
				return false;
			}
			string scheme = uri.Scheme;
			if (!(scheme == "http") && !(scheme == "https"))
			{
				message = "The scheme part is not 'http' or 'https'.";
				return false;
			}
			if (uri.PathAndQuery != "/")
			{
				message = "It includes either or both path and query components.";
				return false;
			}
			if (uri.Fragment.Length > 0)
			{
				message = "It includes the fragment component.";
				return false;
			}
			if (uri.Port == 0)
			{
				message = "The port part is zero.";
				return false;
			}
			result = uri;
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
