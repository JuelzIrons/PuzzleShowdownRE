namespace UnityWebSocketSharp.Server
{
	internal class WebSocketServiceManager
	{
		private global::System.Collections.Generic.Dictionary<string, global::UnityWebSocketSharp.Server.WebSocketServiceHost> _hosts;

		private volatile bool _keepClean;

		private global::UnityWebSocketSharp.Logger _log;

		private volatile global::UnityWebSocketSharp.Server.ServerState _state;

		private object _sync;

		private global::System.TimeSpan _waitTime;

		public int Count
		{
			get
			{
				lock (_sync)
				{
					return _hosts.Count;
				}
			}
		}

		public global::System.Collections.Generic.IEnumerable<global::UnityWebSocketSharp.Server.WebSocketServiceHost> Hosts
		{
			get
			{
				lock (_sync)
				{
					return _hosts.Values.ToList();
				}
			}
		}

		public global::UnityWebSocketSharp.Server.WebSocketServiceHost this[string path]
		{
			get
			{
				if (path == null)
				{
					throw new global::System.ArgumentNullException("path");
				}
				if (path.Length == 0)
				{
					throw new global::System.ArgumentException("An empty string.", "path");
				}
				if (path[0] != '/')
				{
					throw new global::System.ArgumentException("Not an absolute path.", "path");
				}
				if (path.IndexOfAny(new char[2] { '?', '#' }) > -1)
				{
					throw new global::System.ArgumentException("It includes either or both query and fragment components.", "path");
				}
				InternalTryGetServiceHost(path, out var host);
				return host;
			}
		}

		public bool KeepClean
		{
			get
			{
				return _keepClean;
			}
			set
			{
				lock (_sync)
				{
					if (!canSet())
					{
						return;
					}
					foreach (global::UnityWebSocketSharp.Server.WebSocketServiceHost value2 in _hosts.Values)
					{
						value2.KeepClean = value;
					}
					_keepClean = value;
				}
			}
		}

		public global::System.Collections.Generic.IEnumerable<string> Paths
		{
			get
			{
				lock (_sync)
				{
					return _hosts.Keys.ToList();
				}
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
					string message = "Zero or less.";
					throw new global::System.ArgumentOutOfRangeException("value", message);
				}
				lock (_sync)
				{
					if (!canSet())
					{
						return;
					}
					foreach (global::UnityWebSocketSharp.Server.WebSocketServiceHost value2 in _hosts.Values)
					{
						value2.WaitTime = value;
					}
					_waitTime = value;
				}
			}
		}

		internal WebSocketServiceManager(global::UnityWebSocketSharp.Logger log)
		{
			_log = log;
			_hosts = new global::System.Collections.Generic.Dictionary<string, global::UnityWebSocketSharp.Server.WebSocketServiceHost>();
			_keepClean = true;
			_state = global::UnityWebSocketSharp.Server.ServerState.Ready;
			_sync = ((global::System.Collections.ICollection)_hosts).SyncRoot;
			_waitTime = global::System.TimeSpan.FromSeconds(1.0);
		}

		private bool canSet()
		{
			if (_state != global::UnityWebSocketSharp.Server.ServerState.Ready)
			{
				return _state == global::UnityWebSocketSharp.Server.ServerState.Stop;
			}
			return true;
		}

		internal bool InternalTryGetServiceHost(string path, out global::UnityWebSocketSharp.Server.WebSocketServiceHost host)
		{
			path = path.TrimSlashFromEnd();
			lock (_sync)
			{
				return _hosts.TryGetValue(path, out host);
			}
		}

		internal void Start()
		{
			lock (_sync)
			{
				foreach (global::UnityWebSocketSharp.Server.WebSocketServiceHost value in _hosts.Values)
				{
					value.Start();
				}
				_state = global::UnityWebSocketSharp.Server.ServerState.Start;
			}
		}

		internal void Stop(ushort code, string reason)
		{
			lock (_sync)
			{
				_state = global::UnityWebSocketSharp.Server.ServerState.ShuttingDown;
				foreach (global::UnityWebSocketSharp.Server.WebSocketServiceHost value in _hosts.Values)
				{
					value.Stop(code, reason);
				}
				_state = global::UnityWebSocketSharp.Server.ServerState.Stop;
			}
		}

		public void AddService<TBehavior>(string path, global::System.Action<TBehavior> initializer) where TBehavior : global::UnityWebSocketSharp.Server.WebSocketBehavior, new()
		{
			if (path == null)
			{
				throw new global::System.ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new global::System.ArgumentException("An empty string.", "path");
			}
			if (path[0] != '/')
			{
				throw new global::System.ArgumentException("Not an absolute path.", "path");
			}
			if (path.IndexOfAny(new char[2] { '?', '#' }) > -1)
			{
				throw new global::System.ArgumentException("It includes either or both query and fragment components.", "path");
			}
			path = path.TrimSlashFromEnd();
			lock (_sync)
			{
				if (_hosts.TryGetValue(path, out var value))
				{
					throw new global::System.ArgumentException("It is already in use.", "path");
				}
				value = new global::UnityWebSocketSharp.Server.WebSocketServiceHost<TBehavior>(path, initializer, _log);
				if (!_keepClean)
				{
					value.KeepClean = false;
				}
				if (_waitTime != value.WaitTime)
				{
					value.WaitTime = _waitTime;
				}
				if (_state == global::UnityWebSocketSharp.Server.ServerState.Start)
				{
					value.Start();
				}
				_hosts.Add(path, value);
			}
		}

		public void Clear()
		{
			global::System.Collections.Generic.List<global::UnityWebSocketSharp.Server.WebSocketServiceHost> list = null;
			lock (_sync)
			{
				list = _hosts.Values.ToList();
				_hosts.Clear();
			}
			foreach (global::UnityWebSocketSharp.Server.WebSocketServiceHost item in list)
			{
				if (item.State == global::UnityWebSocketSharp.Server.ServerState.Start)
				{
					item.Stop(1001, string.Empty);
				}
			}
		}

		public bool RemoveService(string path)
		{
			if (path == null)
			{
				throw new global::System.ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new global::System.ArgumentException("An empty string.", "path");
			}
			if (path[0] != '/')
			{
				throw new global::System.ArgumentException("Not an absolute path.", "path");
			}
			if (path.IndexOfAny(new char[2] { '?', '#' }) > -1)
			{
				throw new global::System.ArgumentException("It includes either or both query and fragment components.", "path");
			}
			path = path.TrimSlashFromEnd();
			global::UnityWebSocketSharp.Server.WebSocketServiceHost value;
			lock (_sync)
			{
				if (!_hosts.TryGetValue(path, out value))
				{
					return false;
				}
				_hosts.Remove(path);
			}
			if (value.State == global::UnityWebSocketSharp.Server.ServerState.Start)
			{
				value.Stop(1001, string.Empty);
			}
			return true;
		}

		public bool TryGetServiceHost(string path, out global::UnityWebSocketSharp.Server.WebSocketServiceHost host)
		{
			if (path == null)
			{
				throw new global::System.ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new global::System.ArgumentException("An empty string.", "path");
			}
			if (path[0] != '/')
			{
				throw new global::System.ArgumentException("Not an absolute path.", "path");
			}
			if (path.IndexOfAny(new char[2] { '?', '#' }) > -1)
			{
				throw new global::System.ArgumentException("It includes either or both query and fragment components.", "path");
			}
			return InternalTryGetServiceHost(path, out host);
		}
	}
}
