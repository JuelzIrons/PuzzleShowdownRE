namespace WebSocketSharp.Net
{
	internal sealed class EndPointListener
	{
		private global::System.Collections.Generic.List<global::WebSocketSharp.Net.HttpListenerPrefix> _all;

		private global::System.Collections.Generic.Dictionary<global::WebSocketSharp.Net.HttpConnection, global::WebSocketSharp.Net.HttpConnection> _connections;

		private object _connectionsSync;

		private static readonly string _defaultCertFolderPath;

		private global::System.Net.IPEndPoint _endpoint;

		private global::System.Collections.Generic.List<global::WebSocketSharp.Net.HttpListenerPrefix> _prefixes;

		private bool _secure;

		private global::System.Net.Sockets.Socket _socket;

		private global::WebSocketSharp.Net.ServerSslConfiguration _sslConfig;

		private global::System.Collections.Generic.List<global::WebSocketSharp.Net.HttpListenerPrefix> _unhandled;

		public global::System.Net.IPAddress Address => _endpoint.Address;

		public bool IsSecure => _secure;

		public int Port => _endpoint.Port;

		public global::WebSocketSharp.Net.ServerSslConfiguration SslConfiguration => _sslConfig;

		static EndPointListener()
		{
			_defaultCertFolderPath = global::System.Environment.GetFolderPath(global::System.Environment.SpecialFolder.ApplicationData);
		}

		internal EndPointListener(global::System.Net.IPEndPoint endpoint, bool secure, string certificateFolderPath, global::WebSocketSharp.Net.ServerSslConfiguration sslConfig, bool reuseAddress)
		{
			_endpoint = endpoint;
			if (secure)
			{
				global::System.Security.Cryptography.X509Certificates.X509Certificate2 certificate = getCertificate(endpoint.Port, certificateFolderPath, sslConfig.ServerCertificate);
				if (certificate == null)
				{
					string message = "No server certificate could be found.";
					throw new global::System.ArgumentException(message);
				}
				_secure = true;
				_sslConfig = new global::WebSocketSharp.Net.ServerSslConfiguration(sslConfig);
				_sslConfig.ServerCertificate = certificate;
			}
			_prefixes = new global::System.Collections.Generic.List<global::WebSocketSharp.Net.HttpListenerPrefix>();
			_connections = new global::System.Collections.Generic.Dictionary<global::WebSocketSharp.Net.HttpConnection, global::WebSocketSharp.Net.HttpConnection>();
			_connectionsSync = ((global::System.Collections.ICollection)_connections).SyncRoot;
			_socket = new global::System.Net.Sockets.Socket(endpoint.Address.AddressFamily, global::System.Net.Sockets.SocketType.Stream, global::System.Net.Sockets.ProtocolType.Tcp);
			if (reuseAddress)
			{
				_socket.SetSocketOption(global::System.Net.Sockets.SocketOptionLevel.Socket, global::System.Net.Sockets.SocketOptionName.ReuseAddress, optionValue: true);
			}
			_socket.Bind(endpoint);
			_socket.Listen(500);
			_socket.BeginAccept(onAccept, this);
		}

		private static void addSpecial(global::System.Collections.Generic.List<global::WebSocketSharp.Net.HttpListenerPrefix> prefixes, global::WebSocketSharp.Net.HttpListenerPrefix prefix)
		{
			string path = prefix.Path;
			foreach (global::WebSocketSharp.Net.HttpListenerPrefix prefix2 in prefixes)
			{
				if (prefix2.Path == path)
				{
					string message = "The prefix is already in use.";
					throw new global::WebSocketSharp.Net.HttpListenerException(87, message);
				}
			}
			prefixes.Add(prefix);
		}

		private void clearConnections()
		{
			global::WebSocketSharp.Net.HttpConnection[] array = null;
			lock (_connectionsSync)
			{
				int count = _connections.Count;
				if (count == 0)
				{
					return;
				}
				array = new global::WebSocketSharp.Net.HttpConnection[count];
				global::System.Collections.Generic.Dictionary<global::WebSocketSharp.Net.HttpConnection, global::WebSocketSharp.Net.HttpConnection>.ValueCollection values = _connections.Values;
				values.CopyTo(array, 0);
				_connections.Clear();
			}
			global::WebSocketSharp.Net.HttpConnection[] array2 = array;
			foreach (global::WebSocketSharp.Net.HttpConnection httpConnection in array2)
			{
				httpConnection.Close(force: true);
			}
		}

		private static global::System.Security.Cryptography.RSACryptoServiceProvider createRSAFromFile(string path)
		{
			global::System.Security.Cryptography.RSACryptoServiceProvider rSACryptoServiceProvider = new global::System.Security.Cryptography.RSACryptoServiceProvider();
			byte[] keyBlob = global::System.IO.File.ReadAllBytes(path);
			rSACryptoServiceProvider.ImportCspBlob(keyBlob);
			return rSACryptoServiceProvider;
		}

		private static global::System.Security.Cryptography.X509Certificates.X509Certificate2 getCertificate(int port, string folderPath, global::System.Security.Cryptography.X509Certificates.X509Certificate2 defaultCertificate)
		{
			if (folderPath == null || folderPath.Length == 0)
			{
				folderPath = _defaultCertFolderPath;
			}
			try
			{
				string text = global::System.IO.Path.Combine(folderPath, $"{port}.cer");
				string path = global::System.IO.Path.Combine(folderPath, $"{port}.key");
				if (global::System.IO.File.Exists(text) && global::System.IO.File.Exists(path))
				{
					global::System.Security.Cryptography.X509Certificates.X509Certificate2 x509Certificate = new global::System.Security.Cryptography.X509Certificates.X509Certificate2(text);
					x509Certificate.PrivateKey = createRSAFromFile(path);
					return x509Certificate;
				}
			}
			catch
			{
			}
			return defaultCertificate;
		}

		private void leaveIfNoPrefix()
		{
			if (_prefixes.Count > 0)
			{
				return;
			}
			global::System.Collections.Generic.List<global::WebSocketSharp.Net.HttpListenerPrefix> unhandled = _unhandled;
			if (unhandled == null || unhandled.Count <= 0)
			{
				unhandled = _all;
				if (unhandled == null || unhandled.Count <= 0)
				{
					Close();
				}
			}
		}

		private static void onAccept(global::System.IAsyncResult asyncResult)
		{
			global::WebSocketSharp.Net.EndPointListener endPointListener = (global::WebSocketSharp.Net.EndPointListener)asyncResult.AsyncState;
			global::System.Net.Sockets.Socket socket = null;
			try
			{
				socket = endPointListener._socket.EndAccept(asyncResult);
			}
			catch (global::System.ObjectDisposedException)
			{
				return;
			}
			catch (global::System.Exception)
			{
			}
			try
			{
				endPointListener._socket.BeginAccept(onAccept, endPointListener);
			}
			catch (global::System.Exception)
			{
				socket?.Close();
				return;
			}
			if (socket != null)
			{
				processAccepted(socket, endPointListener);
			}
		}

		private static void processAccepted(global::System.Net.Sockets.Socket socket, global::WebSocketSharp.Net.EndPointListener listener)
		{
			global::WebSocketSharp.Net.HttpConnection httpConnection = null;
			try
			{
				httpConnection = new global::WebSocketSharp.Net.HttpConnection(socket, listener);
			}
			catch (global::System.Exception)
			{
				socket.Close();
				return;
			}
			lock (listener._connectionsSync)
			{
				listener._connections.Add(httpConnection, httpConnection);
			}
			httpConnection.BeginReadRequest();
		}

		private static bool removeSpecial(global::System.Collections.Generic.List<global::WebSocketSharp.Net.HttpListenerPrefix> prefixes, global::WebSocketSharp.Net.HttpListenerPrefix prefix)
		{
			string path = prefix.Path;
			int count = prefixes.Count;
			for (int i = 0; i < count; i++)
			{
				if (prefixes[i].Path == path)
				{
					prefixes.RemoveAt(i);
					return true;
				}
			}
			return false;
		}

		private static global::WebSocketSharp.Net.HttpListener searchHttpListenerFromSpecial(string path, global::System.Collections.Generic.List<global::WebSocketSharp.Net.HttpListenerPrefix> prefixes)
		{
			if (prefixes == null)
			{
				return null;
			}
			global::WebSocketSharp.Net.HttpListener result = null;
			int num = -1;
			foreach (global::WebSocketSharp.Net.HttpListenerPrefix prefix in prefixes)
			{
				string path2 = prefix.Path;
				int length = path2.Length;
				if (length >= num && path.StartsWith(path2, global::System.StringComparison.Ordinal))
				{
					num = length;
					result = prefix.Listener;
				}
			}
			return result;
		}

		internal static bool CertificateExists(int port, string folderPath)
		{
			if (folderPath == null || folderPath.Length == 0)
			{
				folderPath = _defaultCertFolderPath;
			}
			string path = global::System.IO.Path.Combine(folderPath, $"{port}.cer");
			string path2 = global::System.IO.Path.Combine(folderPath, $"{port}.key");
			return global::System.IO.File.Exists(path) && global::System.IO.File.Exists(path2);
		}

		internal void RemoveConnection(global::WebSocketSharp.Net.HttpConnection connection)
		{
			lock (_connectionsSync)
			{
				_connections.Remove(connection);
			}
		}

		internal bool TrySearchHttpListener(global::System.Uri uri, out global::WebSocketSharp.Net.HttpListener listener)
		{
			listener = null;
			if (uri == null)
			{
				return false;
			}
			string host = uri.Host;
			bool flag = global::System.Uri.CheckHostName(host) == global::System.UriHostNameType.Dns;
			string text = uri.Port.ToString();
			string text2 = global::WebSocketSharp.Net.HttpUtility.UrlDecode(uri.AbsolutePath);
			if (text2[text2.Length - 1] != '/')
			{
				text2 += "/";
			}
			if (host != null && host.Length > 0)
			{
				global::System.Collections.Generic.List<global::WebSocketSharp.Net.HttpListenerPrefix> prefixes = _prefixes;
				int num = -1;
				foreach (global::WebSocketSharp.Net.HttpListenerPrefix item in prefixes)
				{
					if (flag)
					{
						string host2 = item.Host;
						if (global::System.Uri.CheckHostName(host2) == global::System.UriHostNameType.Dns && host2 != host)
						{
							continue;
						}
					}
					if (!(item.Port != text))
					{
						string path = item.Path;
						int length = path.Length;
						if (length >= num && text2.StartsWith(path, global::System.StringComparison.Ordinal))
						{
							num = length;
							listener = item.Listener;
						}
					}
				}
				if (num != -1)
				{
					return true;
				}
			}
			listener = searchHttpListenerFromSpecial(text2, _unhandled);
			if (listener != null)
			{
				return true;
			}
			listener = searchHttpListenerFromSpecial(text2, _all);
			return listener != null;
		}

		public void AddPrefix(global::WebSocketSharp.Net.HttpListenerPrefix prefix)
		{
			global::System.Collections.Generic.List<global::WebSocketSharp.Net.HttpListenerPrefix> unhandled;
			global::System.Collections.Generic.List<global::WebSocketSharp.Net.HttpListenerPrefix> list;
			if (prefix.Host == "*")
			{
				do
				{
					unhandled = _unhandled;
					list = ((unhandled != null) ? new global::System.Collections.Generic.List<global::WebSocketSharp.Net.HttpListenerPrefix>(unhandled) : new global::System.Collections.Generic.List<global::WebSocketSharp.Net.HttpListenerPrefix>());
					addSpecial(list, prefix);
				}
				while (global::System.Threading.Interlocked.CompareExchange(ref _unhandled, list, unhandled) != unhandled);
				return;
			}
			if (prefix.Host == "+")
			{
				do
				{
					unhandled = _all;
					list = ((unhandled != null) ? new global::System.Collections.Generic.List<global::WebSocketSharp.Net.HttpListenerPrefix>(unhandled) : new global::System.Collections.Generic.List<global::WebSocketSharp.Net.HttpListenerPrefix>());
					addSpecial(list, prefix);
				}
				while (global::System.Threading.Interlocked.CompareExchange(ref _all, list, unhandled) != unhandled);
				return;
			}
			do
			{
				unhandled = _prefixes;
				int num = unhandled.IndexOf(prefix);
				if (num > -1)
				{
					if (unhandled[num].Listener != prefix.Listener)
					{
						string message = $"There is another listener for {prefix}.";
						throw new global::WebSocketSharp.Net.HttpListenerException(87, message);
					}
					break;
				}
				list = new global::System.Collections.Generic.List<global::WebSocketSharp.Net.HttpListenerPrefix>(unhandled);
				list.Add(prefix);
			}
			while (global::System.Threading.Interlocked.CompareExchange(ref _prefixes, list, unhandled) != unhandled);
		}

		public void Close()
		{
			_socket.Close();
			clearConnections();
			global::WebSocketSharp.Net.EndPointManager.RemoveEndPoint(_endpoint);
		}

		public void RemovePrefix(global::WebSocketSharp.Net.HttpListenerPrefix prefix)
		{
			global::System.Collections.Generic.List<global::WebSocketSharp.Net.HttpListenerPrefix> unhandled;
			global::System.Collections.Generic.List<global::WebSocketSharp.Net.HttpListenerPrefix> list;
			if (prefix.Host == "*")
			{
				do
				{
					unhandled = _unhandled;
					if (unhandled == null)
					{
						break;
					}
					list = new global::System.Collections.Generic.List<global::WebSocketSharp.Net.HttpListenerPrefix>(unhandled);
				}
				while (removeSpecial(list, prefix) && global::System.Threading.Interlocked.CompareExchange(ref _unhandled, list, unhandled) != unhandled);
				leaveIfNoPrefix();
				return;
			}
			if (prefix.Host == "+")
			{
				do
				{
					unhandled = _all;
					if (unhandled == null)
					{
						break;
					}
					list = new global::System.Collections.Generic.List<global::WebSocketSharp.Net.HttpListenerPrefix>(unhandled);
				}
				while (removeSpecial(list, prefix) && global::System.Threading.Interlocked.CompareExchange(ref _all, list, unhandled) != unhandled);
				leaveIfNoPrefix();
				return;
			}
			do
			{
				unhandled = _prefixes;
				if (!unhandled.Contains(prefix))
				{
					break;
				}
				list = new global::System.Collections.Generic.List<global::WebSocketSharp.Net.HttpListenerPrefix>(unhandled);
				list.Remove(prefix);
			}
			while (global::System.Threading.Interlocked.CompareExchange(ref _prefixes, list, unhandled) != unhandled);
			leaveIfNoPrefix();
		}
	}
}
