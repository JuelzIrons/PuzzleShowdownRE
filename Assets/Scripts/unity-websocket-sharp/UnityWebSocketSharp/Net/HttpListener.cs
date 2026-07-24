namespace UnityWebSocketSharp.Net
{
	internal sealed class HttpListener : global::System.IDisposable
	{
		private global::UnityWebSocketSharp.Net.AuthenticationSchemes _authSchemes;

		private global::System.Func<global::UnityWebSocketSharp.Net.HttpListenerRequest, global::UnityWebSocketSharp.Net.AuthenticationSchemes> _authSchemeSelector;

		private string _certFolderPath;

		private global::System.Collections.Generic.Queue<global::UnityWebSocketSharp.Net.HttpListenerContext> _contextQueue;

		private global::System.Collections.Generic.LinkedList<global::UnityWebSocketSharp.Net.HttpListenerContext> _contextRegistry;

		private object _contextRegistrySync;

		private static readonly string _defaultRealm;

		private bool _disposed;

		private bool _ignoreWriteExceptions;

		private volatile bool _listening;

		private global::UnityWebSocketSharp.Logger _log;

		private string _objectName;

		private global::UnityWebSocketSharp.Net.HttpListenerPrefixCollection _prefixes;

		private string _realm;

		private bool _reuseAddress;

		private global::UnityWebSocketSharp.Net.ServerSslConfiguration _sslConfig;

		private object _sync;

		private global::System.Func<global::System.Security.Principal.IIdentity, global::UnityWebSocketSharp.Net.NetworkCredential> _userCredFinder;

		private global::System.Collections.Generic.Queue<global::UnityWebSocketSharp.Net.HttpListenerAsyncResult> _waitQueue;

		internal bool ReuseAddress
		{
			get
			{
				return _reuseAddress;
			}
			set
			{
				_reuseAddress = value;
			}
		}

		public global::UnityWebSocketSharp.Net.AuthenticationSchemes AuthenticationSchemes
		{
			get
			{
				if (_disposed)
				{
					throw new global::System.ObjectDisposedException(_objectName);
				}
				return _authSchemes;
			}
			set
			{
				if (_disposed)
				{
					throw new global::System.ObjectDisposedException(_objectName);
				}
				_authSchemes = value;
			}
		}

		public global::System.Func<global::UnityWebSocketSharp.Net.HttpListenerRequest, global::UnityWebSocketSharp.Net.AuthenticationSchemes> AuthenticationSchemeSelector
		{
			get
			{
				if (_disposed)
				{
					throw new global::System.ObjectDisposedException(_objectName);
				}
				return _authSchemeSelector;
			}
			set
			{
				if (_disposed)
				{
					throw new global::System.ObjectDisposedException(_objectName);
				}
				_authSchemeSelector = value;
			}
		}

		public string CertificateFolderPath
		{
			get
			{
				if (_disposed)
				{
					throw new global::System.ObjectDisposedException(_objectName);
				}
				return _certFolderPath;
			}
			set
			{
				if (_disposed)
				{
					throw new global::System.ObjectDisposedException(_objectName);
				}
				_certFolderPath = value;
			}
		}

		public bool IgnoreWriteExceptions
		{
			get
			{
				if (_disposed)
				{
					throw new global::System.ObjectDisposedException(_objectName);
				}
				return _ignoreWriteExceptions;
			}
			set
			{
				if (_disposed)
				{
					throw new global::System.ObjectDisposedException(_objectName);
				}
				_ignoreWriteExceptions = value;
			}
		}

		public bool IsListening => _listening;

		public static bool IsSupported => true;

		public global::UnityWebSocketSharp.Logger Log => _log;

		public global::UnityWebSocketSharp.Net.HttpListenerPrefixCollection Prefixes
		{
			get
			{
				if (_disposed)
				{
					throw new global::System.ObjectDisposedException(_objectName);
				}
				return _prefixes;
			}
		}

		public string Realm
		{
			get
			{
				if (_disposed)
				{
					throw new global::System.ObjectDisposedException(_objectName);
				}
				return _realm;
			}
			set
			{
				if (_disposed)
				{
					throw new global::System.ObjectDisposedException(_objectName);
				}
				_realm = value;
			}
		}

		public global::UnityWebSocketSharp.Net.ServerSslConfiguration SslConfiguration
		{
			get
			{
				if (_disposed)
				{
					throw new global::System.ObjectDisposedException(_objectName);
				}
				if (_sslConfig == null)
				{
					_sslConfig = new global::UnityWebSocketSharp.Net.ServerSslConfiguration();
				}
				return _sslConfig;
			}
		}

		public bool UnsafeConnectionNtlmAuthentication
		{
			get
			{
				throw new global::System.NotSupportedException();
			}
			set
			{
				throw new global::System.NotSupportedException();
			}
		}

		public global::System.Func<global::System.Security.Principal.IIdentity, global::UnityWebSocketSharp.Net.NetworkCredential> UserCredentialsFinder
		{
			get
			{
				if (_disposed)
				{
					throw new global::System.ObjectDisposedException(_objectName);
				}
				return _userCredFinder;
			}
			set
			{
				if (_disposed)
				{
					throw new global::System.ObjectDisposedException(_objectName);
				}
				_userCredFinder = value;
			}
		}

		static HttpListener()
		{
			_defaultRealm = "SECRET AREA";
		}

		public HttpListener()
		{
			_authSchemes = global::UnityWebSocketSharp.Net.AuthenticationSchemes.Anonymous;
			_contextQueue = new global::System.Collections.Generic.Queue<global::UnityWebSocketSharp.Net.HttpListenerContext>();
			_contextRegistry = new global::System.Collections.Generic.LinkedList<global::UnityWebSocketSharp.Net.HttpListenerContext>();
			_contextRegistrySync = ((global::System.Collections.ICollection)_contextRegistry).SyncRoot;
			_log = new global::UnityWebSocketSharp.Logger();
			_objectName = GetType().ToString();
			_prefixes = new global::UnityWebSocketSharp.Net.HttpListenerPrefixCollection(this);
			_sync = new object();
			_waitQueue = new global::System.Collections.Generic.Queue<global::UnityWebSocketSharp.Net.HttpListenerAsyncResult>();
		}

		private bool authenticateClient(global::UnityWebSocketSharp.Net.HttpListenerContext context)
		{
			global::UnityWebSocketSharp.Net.AuthenticationSchemes authenticationSchemes = selectAuthenticationScheme(context.Request);
			switch (authenticationSchemes)
			{
			case global::UnityWebSocketSharp.Net.AuthenticationSchemes.Anonymous:
				return true;
			case global::UnityWebSocketSharp.Net.AuthenticationSchemes.None:
			{
				string message = "Authentication not allowed";
				context.SendError(403, message);
				return false;
			}
			default:
			{
				string realm = getRealm();
				if (!context.SetUser(authenticationSchemes, realm, _userCredFinder))
				{
					context.SendAuthenticationChallenge(authenticationSchemes, realm);
					return false;
				}
				return true;
			}
			}
		}

		private global::UnityWebSocketSharp.Net.HttpListenerAsyncResult beginGetContext(global::System.AsyncCallback callback, object state)
		{
			lock (_contextRegistrySync)
			{
				if (!_listening)
				{
					string message = "The method is canceled.";
					throw new global::UnityWebSocketSharp.Net.HttpListenerException(995, message);
				}
				global::UnityWebSocketSharp.Net.HttpListenerAsyncResult httpListenerAsyncResult = new global::UnityWebSocketSharp.Net.HttpListenerAsyncResult(callback, state);
				if (_contextQueue.Count == 0)
				{
					_waitQueue.Enqueue(httpListenerAsyncResult);
					return httpListenerAsyncResult;
				}
				global::UnityWebSocketSharp.Net.HttpListenerContext context = _contextQueue.Dequeue();
				httpListenerAsyncResult.Complete(context, completedSynchronously: true);
				return httpListenerAsyncResult;
			}
		}

		private void cleanupContextQueue(bool force)
		{
			if (_contextQueue.Count == 0)
			{
				return;
			}
			if (force)
			{
				_contextQueue.Clear();
				return;
			}
			global::UnityWebSocketSharp.Net.HttpListenerContext[] array = _contextQueue.ToArray();
			_contextQueue.Clear();
			global::UnityWebSocketSharp.Net.HttpListenerContext[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].SendError(503);
			}
		}

		private void cleanupContextRegistry()
		{
			int count = _contextRegistry.Count;
			if (count != 0)
			{
				global::UnityWebSocketSharp.Net.HttpListenerContext[] array = new global::UnityWebSocketSharp.Net.HttpListenerContext[count];
				lock (_contextRegistrySync)
				{
					_contextRegistry.CopyTo(array, 0);
					_contextRegistry.Clear();
				}
				global::UnityWebSocketSharp.Net.HttpListenerContext[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i].Connection.Close(force: true);
				}
			}
		}

		private void cleanupWaitQueue(string message)
		{
			if (_waitQueue.Count != 0)
			{
				global::UnityWebSocketSharp.Net.HttpListenerAsyncResult[] array = _waitQueue.ToArray();
				_waitQueue.Clear();
				global::UnityWebSocketSharp.Net.HttpListenerAsyncResult[] array2 = array;
				foreach (global::UnityWebSocketSharp.Net.HttpListenerAsyncResult obj in array2)
				{
					global::UnityWebSocketSharp.Net.HttpListenerException exception = new global::UnityWebSocketSharp.Net.HttpListenerException(995, message);
					obj.Complete(exception);
				}
			}
		}

		private void close(bool force)
		{
			lock (_sync)
			{
				if (_disposed)
				{
					return;
				}
				lock (_contextRegistrySync)
				{
					if (!_listening)
					{
						_disposed = true;
						return;
					}
					_listening = false;
				}
				cleanupContextQueue(force);
				cleanupContextRegistry();
				string message = "The listener is closed.";
				cleanupWaitQueue(message);
				global::UnityWebSocketSharp.Net.EndPointManager.RemoveListener(this);
				_disposed = true;
			}
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

		private bool registerContext(global::UnityWebSocketSharp.Net.HttpListenerContext context)
		{
			if (!_listening)
			{
				return false;
			}
			lock (_contextRegistrySync)
			{
				if (!_listening)
				{
					return false;
				}
				context.Listener = this;
				_contextRegistry.AddLast(context);
				if (_waitQueue.Count == 0)
				{
					_contextQueue.Enqueue(context);
					return true;
				}
				_waitQueue.Dequeue().Complete(context, completedSynchronously: false);
				return true;
			}
		}

		private global::UnityWebSocketSharp.Net.AuthenticationSchemes selectAuthenticationScheme(global::UnityWebSocketSharp.Net.HttpListenerRequest request)
		{
			global::System.Func<global::UnityWebSocketSharp.Net.HttpListenerRequest, global::UnityWebSocketSharp.Net.AuthenticationSchemes> authSchemeSelector = _authSchemeSelector;
			if (authSchemeSelector == null)
			{
				return _authSchemes;
			}
			try
			{
				return authSchemeSelector(request);
			}
			catch
			{
				return global::UnityWebSocketSharp.Net.AuthenticationSchemes.None;
			}
		}

		internal void CheckDisposed()
		{
			if (_disposed)
			{
				throw new global::System.ObjectDisposedException(_objectName);
			}
		}

		internal bool RegisterContext(global::UnityWebSocketSharp.Net.HttpListenerContext context)
		{
			if (!authenticateClient(context))
			{
				return false;
			}
			if (!registerContext(context))
			{
				context.SendError(503);
				return false;
			}
			return true;
		}

		internal void UnregisterContext(global::UnityWebSocketSharp.Net.HttpListenerContext context)
		{
			lock (_contextRegistrySync)
			{
				_contextRegistry.Remove(context);
			}
		}

		public void Abort()
		{
			if (!_disposed)
			{
				close(force: true);
			}
		}

		public global::System.IAsyncResult BeginGetContext(global::System.AsyncCallback callback, object state)
		{
			if (_disposed)
			{
				throw new global::System.ObjectDisposedException(_objectName);
			}
			if (!_listening)
			{
				throw new global::System.InvalidOperationException("The listener has not been started.");
			}
			if (_prefixes.Count == 0)
			{
				throw new global::System.InvalidOperationException("The listener has no URI prefix on which listens.");
			}
			return beginGetContext(callback, state);
		}

		public void Close()
		{
			if (!_disposed)
			{
				close(force: false);
			}
		}

		public global::UnityWebSocketSharp.Net.HttpListenerContext EndGetContext(global::System.IAsyncResult asyncResult)
		{
			if (_disposed)
			{
				throw new global::System.ObjectDisposedException(_objectName);
			}
			if (!_listening)
			{
				throw new global::System.InvalidOperationException("The listener has not been started.");
			}
			if (asyncResult == null)
			{
				throw new global::System.ArgumentNullException("asyncResult");
			}
			if (!(asyncResult is global::UnityWebSocketSharp.Net.HttpListenerAsyncResult { SyncRoot: var syncRoot } httpListenerAsyncResult))
			{
				throw new global::System.ArgumentException("A wrong IAsyncResult instance.", "asyncResult");
			}
			bool lockTaken = false;
			try
			{
				global::System.Threading.Monitor.Enter(syncRoot, ref lockTaken);
				if (httpListenerAsyncResult.EndCalled)
				{
					throw new global::System.InvalidOperationException("This IAsyncResult instance cannot be reused.");
				}
				httpListenerAsyncResult.EndCalled = true;
			}
			finally
			{
				if (lockTaken)
				{
					global::System.Threading.Monitor.Exit(syncRoot);
				}
			}
			if (!httpListenerAsyncResult.IsCompleted)
			{
				httpListenerAsyncResult.AsyncWaitHandle.WaitOne();
			}
			return httpListenerAsyncResult.Context;
		}

		public global::UnityWebSocketSharp.Net.HttpListenerContext GetContext()
		{
			if (_disposed)
			{
				throw new global::System.ObjectDisposedException(_objectName);
			}
			if (!_listening)
			{
				throw new global::System.InvalidOperationException("The listener has not been started.");
			}
			if (_prefixes.Count == 0)
			{
				throw new global::System.InvalidOperationException("The listener has no URI prefix on which listens.");
			}
			global::UnityWebSocketSharp.Net.HttpListenerAsyncResult httpListenerAsyncResult = beginGetContext(null, null);
			httpListenerAsyncResult.EndCalled = true;
			if (!httpListenerAsyncResult.IsCompleted)
			{
				httpListenerAsyncResult.AsyncWaitHandle.WaitOne();
			}
			return httpListenerAsyncResult.Context;
		}

		public void Start()
		{
			if (_disposed)
			{
				throw new global::System.ObjectDisposedException(_objectName);
			}
			lock (_sync)
			{
				if (_disposed)
				{
					throw new global::System.ObjectDisposedException(_objectName);
				}
				lock (_contextRegistrySync)
				{
					if (!_listening)
					{
						global::UnityWebSocketSharp.Net.EndPointManager.AddListener(this);
						_listening = true;
					}
				}
			}
		}

		public void Stop()
		{
			if (_disposed)
			{
				throw new global::System.ObjectDisposedException(_objectName);
			}
			lock (_sync)
			{
				if (_disposed)
				{
					throw new global::System.ObjectDisposedException(_objectName);
				}
				lock (_contextRegistrySync)
				{
					if (!_listening)
					{
						return;
					}
					_listening = false;
				}
				cleanupContextQueue(force: false);
				cleanupContextRegistry();
				string message = "The listener is stopped.";
				cleanupWaitQueue(message);
				global::UnityWebSocketSharp.Net.EndPointManager.RemoveListener(this);
			}
		}

		void global::System.IDisposable.Dispose()
		{
			if (!_disposed)
			{
				close(force: true);
			}
		}
	}
}
