namespace UnityWebSocketSharp.Server
{
	internal class WebSocketSessionManager
	{
		private object _forSweep;

		private volatile bool _keepClean;

		private global::UnityWebSocketSharp.Logger _log;

		private static readonly byte[] _rawEmptyPingFrame;

		private global::System.Collections.Generic.Dictionary<string, global::UnityWebSocketSharp.Server.IWebSocketSession> _sessions;

		private volatile global::UnityWebSocketSharp.Server.ServerState _state;

		private volatile bool _sweeping;

		private global::System.Timers.Timer _sweepTimer;

		private object _sync;

		private global::System.TimeSpan _waitTime;

		internal global::UnityWebSocketSharp.Server.ServerState State => _state;

		public global::System.Collections.Generic.IEnumerable<string> ActiveIDs
		{
			get
			{
				foreach (global::System.Collections.Generic.KeyValuePair<string, bool> item in broadping(_rawEmptyPingFrame))
				{
					if (item.Value)
					{
						yield return item.Key;
					}
				}
			}
		}

		public int Count
		{
			get
			{
				lock (_sync)
				{
					return _sessions.Count;
				}
			}
		}

		public global::System.Collections.Generic.IEnumerable<string> IDs
		{
			get
			{
				if (_state != global::UnityWebSocketSharp.Server.ServerState.Start)
				{
					return global::System.Linq.Enumerable.Empty<string>();
				}
				lock (_sync)
				{
					if (_state != global::UnityWebSocketSharp.Server.ServerState.Start)
					{
						return global::System.Linq.Enumerable.Empty<string>();
					}
					return _sessions.Keys.ToList();
				}
			}
		}

		public global::System.Collections.Generic.IEnumerable<string> InactiveIDs
		{
			get
			{
				foreach (global::System.Collections.Generic.KeyValuePair<string, bool> item in broadping(_rawEmptyPingFrame))
				{
					if (!item.Value)
					{
						yield return item.Key;
					}
				}
			}
		}

		public global::UnityWebSocketSharp.Server.IWebSocketSession this[string id]
		{
			get
			{
				if (id == null)
				{
					throw new global::System.ArgumentNullException("id");
				}
				if (id.Length == 0)
				{
					throw new global::System.ArgumentException("An empty string.", "id");
				}
				tryGetSession(id, out var session);
				return session;
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
					if (canSet())
					{
						_keepClean = value;
					}
				}
			}
		}

		public global::System.Collections.Generic.IEnumerable<global::UnityWebSocketSharp.Server.IWebSocketSession> Sessions
		{
			get
			{
				if (_state != global::UnityWebSocketSharp.Server.ServerState.Start)
				{
					return global::System.Linq.Enumerable.Empty<global::UnityWebSocketSharp.Server.IWebSocketSession>();
				}
				lock (_sync)
				{
					if (_state != global::UnityWebSocketSharp.Server.ServerState.Start)
					{
						return global::System.Linq.Enumerable.Empty<global::UnityWebSocketSharp.Server.IWebSocketSession>();
					}
					return _sessions.Values.ToList();
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
					if (canSet())
					{
						_waitTime = value;
					}
				}
			}
		}

		static WebSocketSessionManager()
		{
			_rawEmptyPingFrame = global::UnityWebSocketSharp.WebSocketFrame.CreatePingFrame(mask: false).ToArray();
		}

		internal WebSocketSessionManager(global::UnityWebSocketSharp.Logger log)
		{
			_log = log;
			_forSweep = new object();
			_keepClean = true;
			_sessions = new global::System.Collections.Generic.Dictionary<string, global::UnityWebSocketSharp.Server.IWebSocketSession>();
			_state = global::UnityWebSocketSharp.Server.ServerState.Ready;
			_sync = ((global::System.Collections.ICollection)_sessions).SyncRoot;
			_waitTime = global::System.TimeSpan.FromSeconds(1.0);
			setSweepTimer(60000.0);
		}

		private void broadcast(global::UnityWebSocketSharp.Opcode opcode, byte[] data, global::System.Action completed)
		{
			global::System.Collections.Generic.Dictionary<global::UnityWebSocketSharp.CompressionMethod, byte[]> dictionary = new global::System.Collections.Generic.Dictionary<global::UnityWebSocketSharp.CompressionMethod, byte[]>();
			try
			{
				foreach (global::UnityWebSocketSharp.Server.IWebSocketSession session in Sessions)
				{
					if (_state != global::UnityWebSocketSharp.Server.ServerState.Start)
					{
						_log.Error("The send is cancelled.");
						break;
					}
					session.WebSocket.Send(opcode, data, dictionary);
				}
				completed?.Invoke();
			}
			catch (global::System.Exception ex)
			{
				_log.Error(ex.Message);
				_log.Debug(ex.ToString());
			}
			finally
			{
				dictionary.Clear();
			}
		}

		private void broadcast(global::UnityWebSocketSharp.Opcode opcode, global::System.IO.Stream sourceStream, global::System.Action completed)
		{
			global::System.Collections.Generic.Dictionary<global::UnityWebSocketSharp.CompressionMethod, global::System.IO.Stream> dictionary = new global::System.Collections.Generic.Dictionary<global::UnityWebSocketSharp.CompressionMethod, global::System.IO.Stream>();
			try
			{
				foreach (global::UnityWebSocketSharp.Server.IWebSocketSession session in Sessions)
				{
					if (_state != global::UnityWebSocketSharp.Server.ServerState.Start)
					{
						_log.Error("The send is cancelled.");
						break;
					}
					session.WebSocket.Send(opcode, sourceStream, dictionary);
				}
				completed?.Invoke();
			}
			catch (global::System.Exception ex)
			{
				_log.Error(ex.Message);
				_log.Debug(ex.ToString());
			}
			finally
			{
				foreach (global::System.IO.Stream value in dictionary.Values)
				{
					value.Dispose();
				}
				dictionary.Clear();
			}
		}

		private void broadcastAsync(global::UnityWebSocketSharp.Opcode opcode, byte[] data, global::System.Action completed)
		{
			global::System.Threading.ThreadPool.QueueUserWorkItem(delegate
			{
				broadcast(opcode, data, completed);
			});
		}

		private void broadcastAsync(global::UnityWebSocketSharp.Opcode opcode, global::System.IO.Stream sourceStream, global::System.Action completed)
		{
			global::System.Threading.ThreadPool.QueueUserWorkItem(delegate
			{
				broadcast(opcode, sourceStream, completed);
			});
		}

		private global::System.Collections.Generic.Dictionary<string, bool> broadping(byte[] rawFrame)
		{
			global::System.Collections.Generic.Dictionary<string, bool> dictionary = new global::System.Collections.Generic.Dictionary<string, bool>();
			foreach (global::UnityWebSocketSharp.Server.IWebSocketSession session in Sessions)
			{
				if (_state != global::UnityWebSocketSharp.Server.ServerState.Start)
				{
					dictionary.Clear();
					break;
				}
				bool value = session.WebSocket.Ping(rawFrame);
				dictionary.Add(session.ID, value);
			}
			return dictionary;
		}

		private bool canSet()
		{
			if (_state != global::UnityWebSocketSharp.Server.ServerState.Ready)
			{
				return _state == global::UnityWebSocketSharp.Server.ServerState.Stop;
			}
			return true;
		}

		private static string createID()
		{
			return global::System.Guid.NewGuid().ToString("N");
		}

		private void setSweepTimer(double interval)
		{
			_sweepTimer = new global::System.Timers.Timer(interval);
			_sweepTimer.Elapsed += delegate
			{
				Sweep();
			};
		}

		private void stop(global::UnityWebSocketSharp.PayloadData payloadData, bool send)
		{
			byte[] rawFrame = (send ? global::UnityWebSocketSharp.WebSocketFrame.CreateCloseFrame(payloadData, mask: false).ToArray() : null);
			lock (_sync)
			{
				_state = global::UnityWebSocketSharp.Server.ServerState.ShuttingDown;
				_sweepTimer.Enabled = false;
				foreach (global::UnityWebSocketSharp.Server.IWebSocketSession item in _sessions.Values.ToList())
				{
					item.WebSocket.Close(payloadData, rawFrame);
				}
				_state = global::UnityWebSocketSharp.Server.ServerState.Stop;
			}
		}

		private bool tryGetSession(string id, out global::UnityWebSocketSharp.Server.IWebSocketSession session)
		{
			session = null;
			if (_state != global::UnityWebSocketSharp.Server.ServerState.Start)
			{
				return false;
			}
			lock (_sync)
			{
				if (_state != global::UnityWebSocketSharp.Server.ServerState.Start)
				{
					return false;
				}
				return _sessions.TryGetValue(id, out session);
			}
		}

		internal string Add(global::UnityWebSocketSharp.Server.IWebSocketSession session)
		{
			lock (_sync)
			{
				if (_state != global::UnityWebSocketSharp.Server.ServerState.Start)
				{
					return null;
				}
				string text = createID();
				_sessions.Add(text, session);
				return text;
			}
		}

		internal bool Remove(string id)
		{
			lock (_sync)
			{
				return _sessions.Remove(id);
			}
		}

		internal void Start()
		{
			lock (_sync)
			{
				_sweepTimer.Enabled = _keepClean;
				_state = global::UnityWebSocketSharp.Server.ServerState.Start;
			}
		}

		internal void Stop(ushort code, string reason)
		{
			if (code == 1005)
			{
				stop(global::UnityWebSocketSharp.PayloadData.Empty, send: true);
				return;
			}
			global::UnityWebSocketSharp.PayloadData payloadData = new global::UnityWebSocketSharp.PayloadData(code, reason);
			bool send = !code.IsReservedStatusCode();
			stop(payloadData, send);
		}

		public void Broadcast(byte[] data)
		{
			if (_state != global::UnityWebSocketSharp.Server.ServerState.Start)
			{
				throw new global::System.InvalidOperationException("The current state of the service is not Start.");
			}
			if (data == null)
			{
				throw new global::System.ArgumentNullException("data");
			}
			if (data.LongLength <= global::UnityWebSocketSharp.WebSocket.FragmentLength)
			{
				broadcast(global::UnityWebSocketSharp.Opcode.Binary, data, null);
			}
			else
			{
				broadcast(global::UnityWebSocketSharp.Opcode.Binary, new global::System.IO.MemoryStream(data), null);
			}
		}

		public void Broadcast(string data)
		{
			if (_state != global::UnityWebSocketSharp.Server.ServerState.Start)
			{
				throw new global::System.InvalidOperationException("The current state of the service is not Start.");
			}
			if (data == null)
			{
				throw new global::System.ArgumentNullException("data");
			}
			if (!data.TryGetUTF8EncodedBytes(out var bytes))
			{
				throw new global::System.ArgumentException("It could not be UTF-8-encoded.", "data");
			}
			if (bytes.LongLength <= global::UnityWebSocketSharp.WebSocket.FragmentLength)
			{
				broadcast(global::UnityWebSocketSharp.Opcode.Text, bytes, null);
			}
			else
			{
				broadcast(global::UnityWebSocketSharp.Opcode.Text, new global::System.IO.MemoryStream(bytes), null);
			}
		}

		public void Broadcast(global::System.IO.Stream stream, int length)
		{
			if (_state != global::UnityWebSocketSharp.Server.ServerState.Start)
			{
				throw new global::System.InvalidOperationException("The current state of the service is not Start.");
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
				string message = $"Only {num} byte(s) of data could be read from the stream.";
				_log.Warn(message);
			}
			if (num <= global::UnityWebSocketSharp.WebSocket.FragmentLength)
			{
				broadcast(global::UnityWebSocketSharp.Opcode.Binary, array, null);
			}
			else
			{
				broadcast(global::UnityWebSocketSharp.Opcode.Binary, new global::System.IO.MemoryStream(array), null);
			}
		}

		public void BroadcastAsync(byte[] data, global::System.Action completed)
		{
			if (_state != global::UnityWebSocketSharp.Server.ServerState.Start)
			{
				throw new global::System.InvalidOperationException("The current state of the service is not Start.");
			}
			if (data == null)
			{
				throw new global::System.ArgumentNullException("data");
			}
			if (data.LongLength <= global::UnityWebSocketSharp.WebSocket.FragmentLength)
			{
				broadcastAsync(global::UnityWebSocketSharp.Opcode.Binary, data, completed);
			}
			else
			{
				broadcastAsync(global::UnityWebSocketSharp.Opcode.Binary, new global::System.IO.MemoryStream(data), completed);
			}
		}

		public void BroadcastAsync(string data, global::System.Action completed)
		{
			if (_state != global::UnityWebSocketSharp.Server.ServerState.Start)
			{
				throw new global::System.InvalidOperationException("The current state of the service is not Start.");
			}
			if (data == null)
			{
				throw new global::System.ArgumentNullException("data");
			}
			if (!data.TryGetUTF8EncodedBytes(out var bytes))
			{
				throw new global::System.ArgumentException("It could not be UTF-8-encoded.", "data");
			}
			if (bytes.LongLength <= global::UnityWebSocketSharp.WebSocket.FragmentLength)
			{
				broadcastAsync(global::UnityWebSocketSharp.Opcode.Text, bytes, completed);
			}
			else
			{
				broadcastAsync(global::UnityWebSocketSharp.Opcode.Text, new global::System.IO.MemoryStream(bytes), completed);
			}
		}

		public void BroadcastAsync(global::System.IO.Stream stream, int length, global::System.Action completed)
		{
			if (_state != global::UnityWebSocketSharp.Server.ServerState.Start)
			{
				throw new global::System.InvalidOperationException("The current state of the service is not Start.");
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
				string message = $"Only {num} byte(s) of data could be read from the stream.";
				_log.Warn(message);
			}
			if (num <= global::UnityWebSocketSharp.WebSocket.FragmentLength)
			{
				broadcastAsync(global::UnityWebSocketSharp.Opcode.Binary, array, completed);
			}
			else
			{
				broadcastAsync(global::UnityWebSocketSharp.Opcode.Binary, new global::System.IO.MemoryStream(array), completed);
			}
		}

		public void CloseSession(string id)
		{
			if (!TryGetSession(id, out var session))
			{
				throw new global::System.InvalidOperationException("The session could not be found.");
			}
			session.WebSocket.Close();
		}

		public void CloseSession(string id, ushort code, string reason)
		{
			if (!TryGetSession(id, out var session))
			{
				throw new global::System.InvalidOperationException("The session could not be found.");
			}
			session.WebSocket.Close(code, reason);
		}

		public void CloseSession(string id, global::UnityWebSocketSharp.CloseStatusCode code, string reason)
		{
			if (!TryGetSession(id, out var session))
			{
				throw new global::System.InvalidOperationException("The session could not be found.");
			}
			session.WebSocket.Close(code, reason);
		}

		public bool PingTo(string id)
		{
			if (!TryGetSession(id, out var session))
			{
				throw new global::System.InvalidOperationException("The session could not be found.");
			}
			return session.WebSocket.Ping();
		}

		public bool PingTo(string message, string id)
		{
			if (!TryGetSession(id, out var session))
			{
				throw new global::System.InvalidOperationException("The session could not be found.");
			}
			return session.WebSocket.Ping(message);
		}

		public void SendTo(byte[] data, string id)
		{
			if (!TryGetSession(id, out var session))
			{
				throw new global::System.InvalidOperationException("The session could not be found.");
			}
			session.WebSocket.Send(data);
		}

		public void SendTo(string data, string id)
		{
			if (!TryGetSession(id, out var session))
			{
				throw new global::System.InvalidOperationException("The session could not be found.");
			}
			session.WebSocket.Send(data);
		}

		public void SendTo(global::System.IO.Stream stream, int length, string id)
		{
			if (!TryGetSession(id, out var session))
			{
				throw new global::System.InvalidOperationException("The session could not be found.");
			}
			session.WebSocket.Send(stream, length);
		}

		public void SendToAsync(byte[] data, string id, global::System.Action<bool> completed)
		{
			if (!TryGetSession(id, out var session))
			{
				throw new global::System.InvalidOperationException("The session could not be found.");
			}
			session.WebSocket.SendAsync(data, completed);
		}

		public void SendToAsync(string data, string id, global::System.Action<bool> completed)
		{
			if (!TryGetSession(id, out var session))
			{
				throw new global::System.InvalidOperationException("The session could not be found.");
			}
			session.WebSocket.SendAsync(data, completed);
		}

		public void SendToAsync(global::System.IO.Stream stream, int length, string id, global::System.Action<bool> completed)
		{
			if (!TryGetSession(id, out var session))
			{
				throw new global::System.InvalidOperationException("The session could not be found.");
			}
			session.WebSocket.SendAsync(stream, length, completed);
		}

		public void Sweep()
		{
			if (_sweeping)
			{
				_log.Trace("The sweep process is already in progress.");
				return;
			}
			lock (_forSweep)
			{
				if (_sweeping)
				{
					_log.Trace("The sweep process is already in progress.");
					return;
				}
				_sweeping = true;
			}
			foreach (string inactiveID in InactiveIDs)
			{
				if (_state != global::UnityWebSocketSharp.Server.ServerState.Start)
				{
					break;
				}
				lock (_sync)
				{
					if (_state != global::UnityWebSocketSharp.Server.ServerState.Start)
					{
						break;
					}
					if (_sessions.TryGetValue(inactiveID, out var value))
					{
						switch (value.WebSocket.ReadyState)
						{
						case global::UnityWebSocketSharp.WebSocketState.Open:
							value.WebSocket.Close(global::UnityWebSocketSharp.CloseStatusCode.Abnormal);
							break;
						default:
							_sessions.Remove(inactiveID);
							break;
						case global::UnityWebSocketSharp.WebSocketState.Closing:
							break;
						}
					}
					continue;
				}
			}
			_sweeping = false;
		}

		public bool TryGetSession(string id, out global::UnityWebSocketSharp.Server.IWebSocketSession session)
		{
			if (id == null)
			{
				throw new global::System.ArgumentNullException("id");
			}
			if (id.Length == 0)
			{
				throw new global::System.ArgumentException("An empty string.", "id");
			}
			return tryGetSession(id, out session);
		}
	}
}
