namespace WebSocketSharp.Net
{
	public sealed class HttpListener : global::System.IDisposable
	{
		private global::WebSocketSharp.Net.AuthenticationSchemes _authSchemes;

		private global::System.Func<global::WebSocketSharp.Net.HttpListenerRequest, global::WebSocketSharp.Net.AuthenticationSchemes> _authSchemeSelector;

		private string _certFolderPath;

		private global::System.Collections.Generic.Queue<global::WebSocketSharp.Net.HttpListenerContext> _contextQueue;

		private global::System.Collections.Generic.LinkedList<global::WebSocketSharp.Net.HttpListenerContext> _contextRegistry;

		private object _contextRegistrySync;

		private static readonly string _defaultRealm;

		private bool _disposed;

		private bool _ignoreWriteExceptions;

		private volatile bool _listening;

		private global::WebSocketSharp.Logger _log;

		private string _objectName;

		private global::WebSocketSharp.Net.HttpListenerPrefixCollection _prefixes;

		private string _realm;

		private bool _reuseAddress;

		private global::WebSocketSharp.Net.ServerSslConfiguration _sslConfig;

		private global::System.Func<global::System.Security.Principal.IIdentity, global::WebSocketSharp.Net.NetworkCredential> _userCredFinder;

		private global::System.Collections.Generic.Queue<global::WebSocketSharp.Net.HttpListenerAsyncResult> _waitQueue;

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

		public global::WebSocketSharp.Net.AuthenticationSchemes AuthenticationSchemes
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

		public global::System.Func<global::WebSocketSharp.Net.HttpListenerRequest, global::WebSocketSharp.Net.AuthenticationSchemes> AuthenticationSchemeSelector
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

		public global::WebSocketSharp.Logger Log => _log;

		public global::WebSocketSharp.Net.HttpListenerPrefixCollection Prefixes
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

		public global::WebSocketSharp.Net.ServerSslConfiguration SslConfiguration
		{
			get
			{
				if (_disposed)
				{
					throw new global::System.ObjectDisposedException(_objectName);
				}
				if (_sslConfig == null)
				{
					_sslConfig = new global::WebSocketSharp.Net.ServerSslConfiguration();
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

		public global::System.Func<global::System.Security.Principal.IIdentity, global::WebSocketSharp.Net.NetworkCredential> UserCredentialsFinder
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
			_authSchemes = global::WebSocketSharp.Net.AuthenticationSchemes.Anonymous;
			_contextQueue = new global::System.Collections.Generic.Queue<global::WebSocketSharp.Net.HttpListenerContext>();
			_contextRegistry = new global::System.Collections.Generic.LinkedList<global::WebSocketSharp.Net.HttpListenerContext>();
			_contextRegistrySync = ((global::System.Collections.ICollection)_contextRegistry).SyncRoot;
			_log = new global::WebSocketSharp.Logger();
			_objectName = GetType().ToString();
			_prefixes = new global::WebSocketSharp.Net.HttpListenerPrefixCollection(this);
			_waitQueue = new global::System.Collections.Generic.Queue<global::WebSocketSharp.Net.HttpListenerAsyncResult>();
		}

		private global::WebSocketSharp.Net.HttpListenerAsyncResult beginGetContext(global::System.AsyncCallback callback, object state)
		{
			lock (_contextRegistrySync)
			{
				if (!_listening)
				{
					string message = (_disposed ? "The listener is closed." : "The listener is stopped.");
					throw new global::WebSocketSharp.Net.HttpListenerException(995, message);
				}
				global::WebSocketSharp.Net.HttpListenerAsyncResult httpListenerAsyncResult = new global::WebSocketSharp.Net.HttpListenerAsyncResult(callback, state);
				if (_contextQueue.Count == 0)
				{
					_waitQueue.Enqueue(httpListenerAsyncResult);
				}
				else
				{
					global::WebSocketSharp.Net.HttpListenerContext context = _contextQueue.Dequeue();
					httpListenerAsyncResult.Complete(context, completedSynchronously: true);
				}
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
			global::WebSocketSharp.Net.HttpListenerContext[] array = _contextQueue.ToArray();
			_contextQueue.Clear();
			global::WebSocketSharp.Net.HttpListenerContext[] array2 = array;
			foreach (global::WebSocketSharp.Net.HttpListenerContext httpListenerContext in array2)
			{
				httpListenerContext.ErrorStatusCode = 503;
				httpListenerContext.SendError();
			}
		}

		private void cleanupContextRegistry()
		{
			int count = _contextRegistry.Count;
			if (count != 0)
			{
				global::WebSocketSharp.Net.HttpListenerContext[] array = new global::WebSocketSharp.Net.HttpListenerContext[count];
				_contextRegistry.CopyTo(array, 0);
				_contextRegistry.Clear();
				global::WebSocketSharp.Net.HttpListenerContext[] array2 = array;
				foreach (global::WebSocketSharp.Net.HttpListenerContext httpListenerContext in array2)
				{
					httpListenerContext.Connection.Close(force: true);
				}
			}
		}

		private void cleanupWaitQueue(string message)
		{
			if (_waitQueue.Count != 0)
			{
				global::WebSocketSharp.Net.HttpListenerAsyncResult[] array = _waitQueue.ToArray();
				_waitQueue.Clear();
				global::WebSocketSharp.Net.HttpListenerAsyncResult[] array2 = array;
				foreach (global::WebSocketSharp.Net.HttpListenerAsyncResult httpListenerAsyncResult in array2)
				{
					global::WebSocketSharp.Net.HttpListenerException exception = new global::WebSocketSharp.Net.HttpListenerException(995, message);
					httpListenerAsyncResult.Complete(exception);
				}
			}
		}

		private void close(bool force)
		{
			if (!_listening)
			{
				_disposed = true;
				return;
			}
			_listening = false;
			cleanupContextQueue(force);
			cleanupContextRegistry();
			string message = "The listener is closed.";
			cleanupWaitQueue(message);
			global::WebSocketSharp.Net.EndPointManager.RemoveListener(this);
			_disposed = true;
		}

		private string getRealm()
		{
			string realm = _realm;
			return (realm != null && realm.Length > 0) ? realm : _defaultRealm;
		}

		private global::WebSocketSharp.Net.AuthenticationSchemes selectAuthenticationScheme(global::WebSocketSharp.Net.HttpListenerRequest request)
		{
			global::System.Func<global::WebSocketSharp.Net.HttpListenerRequest, global::WebSocketSharp.Net.AuthenticationSchemes> authSchemeSelector = _authSchemeSelector;
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
				return global::WebSocketSharp.Net.AuthenticationSchemes.None;
			}
		}

		internal bool AuthenticateContext(global::WebSocketSharp.Net.HttpListenerContext context)
		{
			global::WebSocketSharp.Net.HttpListenerRequest request = context.Request;
			global::WebSocketSharp.Net.AuthenticationSchemes authenticationSchemes = selectAuthenticationScheme(request);
			switch (authenticationSchemes)
			{
			case global::WebSocketSharp.Net.AuthenticationSchemes.Anonymous:
				return true;
			case global::WebSocketSharp.Net.AuthenticationSchemes.None:
				context.ErrorStatusCode = 403;
				context.ErrorMessage = "Authentication not allowed";
				context.SendError();
				return false;
			default:
			{
				string realm = getRealm();
				global::System.Security.Principal.IPrincipal principal = global::WebSocketSharp.Net.HttpUtility.CreateUser(request.Headers["Authorization"], authenticationSchemes, realm, request.HttpMethod, _userCredFinder);
				if (principal == null || !principal.Identity.IsAuthenticated)
				{
					context.SendAuthenticationChallenge(authenticationSchemes, realm);
					return false;
				}
				context.User = principal;
				return true;
			}
			}
		}

		internal void CheckDisposed()
		{
			if (_disposed)
			{
				throw new global::System.ObjectDisposedException(_objectName);
			}
		}

		internal bool RegisterContext(global::WebSocketSharp.Net.HttpListenerContext context)
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
				}
				else
				{
					global::WebSocketSharp.Net.HttpListenerAsyncResult httpListenerAsyncResult = _waitQueue.Dequeue();
					httpListenerAsyncResult.Complete(context, completedSynchronously: false);
				}
				return true;
			}
		}

		internal void UnregisterContext(global::WebSocketSharp.Net.HttpListenerContext context)
		{
			lock (_contextRegistrySync)
			{
				_contextRegistry.Remove(context);
			}
		}

		public void Abort()
		{
			if (_disposed)
			{
				return;
			}
			lock (_contextRegistrySync)
			{
				if (!_disposed)
				{
					close(force: true);
				}
			}
		}

		public global::System.IAsyncResult BeginGetContext(global::System.AsyncCallback callback, object state)
		{
			if (_disposed)
			{
				throw new global::System.ObjectDisposedException(_objectName);
			}
			if (_prefixes.Count == 0)
			{
				string message = "The listener has no URI prefix on which listens.";
				throw new global::System.InvalidOperationException(message);
			}
			if (!_listening)
			{
				string message2 = "The listener has not been started.";
				throw new global::System.InvalidOperationException(message2);
			}
			return beginGetContext(callback, state);
		}

		public void Close()
		{
			if (_disposed)
			{
				return;
			}
			lock (_contextRegistrySync)
			{
				if (!_disposed)
				{
					close(force: false);
				}
			}
		}

		public global::WebSocketSharp.Net.HttpListenerContext EndGetContext(global::System.IAsyncResult asyncResult)
		{
			if (_disposed)
			{
				throw new global::System.ObjectDisposedException(_objectName);
			}
			if (asyncResult == null)
			{
				throw new global::System.ArgumentNullException("asyncResult");
			}
			if (!(asyncResult is global::WebSocketSharp.Net.HttpListenerAsyncResult { SyncRoot: var syncRoot } httpListenerAsyncResult))
			{
				string message = "A wrong IAsyncResult instance.";
				throw new global::System.ArgumentException(message, "asyncResult");
			}
			bool lockTaken = false;
			try
			{
				global::System.Threading.Monitor.Enter(syncRoot, ref lockTaken);
				if (httpListenerAsyncResult.EndCalled)
				{
					string message2 = "This IAsyncResult instance cannot be reused.";
					throw new global::System.InvalidOperationException(message2);
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

		public global::WebSocketSharp.Net.HttpListenerContext GetContext()
		{
			if (_disposed)
			{
				throw new global::System.ObjectDisposedException(_objectName);
			}
			if (_prefixes.Count == 0)
			{
				string message = "The listener has no URI prefix on which listens.";
				throw new global::System.InvalidOperationException(message);
			}
			if (!_listening)
			{
				string message2 = "The listener has not been started.";
				throw new global::System.InvalidOperationException(message2);
			}
			global::WebSocketSharp.Net.HttpListenerAsyncResult httpListenerAsyncResult = beginGetContext(null, null);
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
			lock (_contextRegistrySync)
			{
				if (_disposed)
				{
					throw new global::System.ObjectDisposedException(_objectName);
				}
				if (!_listening)
				{
					global::WebSocketSharp.Net.EndPointManager.AddListener(this);
					_listening = true;
				}
			}
		}

		public void Stop()
		{
			if (_disposed)
			{
				throw new global::System.ObjectDisposedException(_objectName);
			}
			lock (_contextRegistrySync)
			{
				if (_listening)
				{
					_listening = false;
					cleanupContextQueue(force: false);
					cleanupContextRegistry();
					string message = "The listener is stopped.";
					cleanupWaitQueue(message);
					global::WebSocketSharp.Net.EndPointManager.RemoveListener(this);
				}
			}
		}

		void global::System.IDisposable.Dispose()
		{
			if (_disposed)
			{
				return;
			}
			lock (_contextRegistrySync)
			{
				if (!_disposed)
				{
					close(force: true);
				}
			}
		}
	}
}
