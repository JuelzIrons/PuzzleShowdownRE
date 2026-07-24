namespace WebSocketSharp.Server
{
	public class WebSocketServiceManager
	{
		private global::System.Collections.Generic.Dictionary<string, global::WebSocketSharp.Server.WebSocketServiceHost> _hosts;

		private volatile bool _keepClean;

		private global::WebSocketSharp.Logger _log;

		private volatile global::WebSocketSharp.Server.ServerState _state;

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

		public global::System.Collections.Generic.IEnumerable<global::WebSocketSharp.Server.WebSocketServiceHost> Hosts
		{
			get
			{
				lock (_sync)
				{
					return _hosts.Values.ToList();
				}
			}
		}

		public global::WebSocketSharp.Server.WebSocketServiceHost this[string path]
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
					string message = "It is not an absolute path.";
					throw new global::System.ArgumentException(message, "path");
				}
				if (path.IndexOfAny(new char[2] { '?', '#' }) > -1)
				{
					string message2 = "It includes either or both query and fragment components.";
					throw new global::System.ArgumentException(message2, "path");
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
					if (!canSet(out var message))
					{
						_log.Warn(message);
						return;
					}
					foreach (global::WebSocketSharp.Server.WebSocketServiceHost value2 in _hosts.Values)
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
					string message = "It is zero or less.";
					throw new global::System.ArgumentOutOfRangeException("value", message);
				}
				lock (_sync)
				{
					if (!canSet(out var message2))
					{
						_log.Warn(message2);
						return;
					}
					foreach (global::WebSocketSharp.Server.WebSocketServiceHost value2 in _hosts.Values)
					{
						value2.WaitTime = value;
					}
					_waitTime = value;
				}
			}
		}

		internal WebSocketServiceManager(global::WebSocketSharp.Logger log)
		{
			_log = log;
			_hosts = new global::System.Collections.Generic.Dictionary<string, global::WebSocketSharp.Server.WebSocketServiceHost>();
			_keepClean = true;
			_state = global::WebSocketSharp.Server.ServerState.Ready;
			_sync = ((global::System.Collections.ICollection)_hosts).SyncRoot;
			_waitTime = global::System.TimeSpan.FromSeconds(1.0);
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

		internal bool InternalTryGetServiceHost(string path, out global::WebSocketSharp.Server.WebSocketServiceHost host)
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
				foreach (global::WebSocketSharp.Server.WebSocketServiceHost value in _hosts.Values)
				{
					value.Start();
				}
				_state = global::WebSocketSharp.Server.ServerState.Start;
			}
		}

		internal void Stop(ushort code, string reason)
		{
			lock (_sync)
			{
				_state = global::WebSocketSharp.Server.ServerState.ShuttingDown;
				foreach (global::WebSocketSharp.Server.WebSocketServiceHost value in _hosts.Values)
				{
					value.Stop(code, reason);
				}
				_state = global::WebSocketSharp.Server.ServerState.Stop;
			}
		}

		public void AddService<TBehavior>(string path, global::System.Action<TBehavior> initializer) where TBehavior : global::WebSocketSharp.Server.WebSocketBehavior, new()
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
				string message = "It is not an absolute path.";
				throw new global::System.ArgumentException(message, "path");
			}
			if (path.IndexOfAny(new char[2] { '?', '#' }) > -1)
			{
				string message2 = "It includes either or both query and fragment components.";
				throw new global::System.ArgumentException(message2, "path");
			}
			path = path.TrimSlashFromEnd();
			lock (_sync)
			{
				if (_hosts.TryGetValue(path, out var value))
				{
					string message3 = "It is already in use.";
					throw new global::System.ArgumentException(message3, "path");
				}
				value = new global::WebSocketSharp.Server.WebSocketServiceHost<TBehavior>(path, initializer, _log);
				if (!_keepClean)
				{
					value.KeepClean = false;
				}
				if (_waitTime != value.WaitTime)
				{
					value.WaitTime = _waitTime;
				}
				if (_state == global::WebSocketSharp.Server.ServerState.Start)
				{
					value.Start();
				}
				_hosts.Add(path, value);
			}
		}

		public void Clear()
		{
			global::System.Collections.Generic.List<global::WebSocketSharp.Server.WebSocketServiceHost> list = null;
			lock (_sync)
			{
				list = _hosts.Values.ToList();
				_hosts.Clear();
			}
			foreach (global::WebSocketSharp.Server.WebSocketServiceHost item in list)
			{
				if (item.State == global::WebSocketSharp.Server.ServerState.Start)
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
				string message = "It is not an absolute path.";
				throw new global::System.ArgumentException(message, "path");
			}
			if (path.IndexOfAny(new char[2] { '?', '#' }) > -1)
			{
				string message2 = "It includes either or both query and fragment components.";
				throw new global::System.ArgumentException(message2, "path");
			}
			path = path.TrimSlashFromEnd();
			global::WebSocketSharp.Server.WebSocketServiceHost value;
			lock (_sync)
			{
				if (!_hosts.TryGetValue(path, out value))
				{
					return false;
				}
				_hosts.Remove(path);
			}
			if (value.State == global::WebSocketSharp.Server.ServerState.Start)
			{
				value.Stop(1001, string.Empty);
			}
			return true;
		}

		public bool TryGetServiceHost(string path, out global::WebSocketSharp.Server.WebSocketServiceHost host)
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
				string message = "It is not an absolute path.";
				throw new global::System.ArgumentException(message, "path");
			}
			if (path.IndexOfAny(new char[2] { '?', '#' }) > -1)
			{
				string message2 = "It includes either or both query and fragment components.";
				throw new global::System.ArgumentException(message2, "path");
			}
			return InternalTryGetServiceHost(path, out host);
		}
	}
}
