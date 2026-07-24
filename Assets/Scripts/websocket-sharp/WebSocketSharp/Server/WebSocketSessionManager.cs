namespace WebSocketSharp.Server
{
	public class WebSocketSessionManager
	{
		private object _forSweep;

		private volatile bool _keepClean;

		private global::WebSocketSharp.Logger _log;

		private global::System.Collections.Generic.Dictionary<string, global::WebSocketSharp.Server.IWebSocketSession> _sessions;

		private volatile global::WebSocketSharp.Server.ServerState _state;

		private volatile bool _sweeping;

		private global::System.Timers.Timer _sweepTimer;

		private object _sync;

		private global::System.TimeSpan _waitTime;

		internal global::WebSocketSharp.Server.ServerState State => _state;

		public global::System.Collections.Generic.IEnumerable<string> ActiveIDs
		{
			get
			{
				foreach (global::System.Collections.Generic.KeyValuePair<string, bool> res in broadping(global::WebSocketSharp.WebSocketFrame.EmptyPingBytes))
				{
					if (res.Value)
					{
						yield return res.Key;
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
				if (_state != global::WebSocketSharp.Server.ServerState.Start)
				{
					return global::System.Linq.Enumerable.Empty<string>();
				}
				lock (_sync)
				{
					if (_state != global::WebSocketSharp.Server.ServerState.Start)
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
				foreach (global::System.Collections.Generic.KeyValuePair<string, bool> res in broadping(global::WebSocketSharp.WebSocketFrame.EmptyPingBytes))
				{
					if (!res.Value)
					{
						yield return res.Key;
					}
				}
			}
		}

		public global::WebSocketSharp.Server.IWebSocketSession this[string id]
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

		public global::System.Collections.Generic.IEnumerable<global::WebSocketSharp.Server.IWebSocketSession> Sessions
		{
			get
			{
				if (_state != global::WebSocketSharp.Server.ServerState.Start)
				{
					return global::System.Linq.Enumerable.Empty<global::WebSocketSharp.Server.IWebSocketSession>();
				}
				lock (_sync)
				{
					if (_state != global::WebSocketSharp.Server.ServerState.Start)
					{
						return global::System.Linq.Enumerable.Empty<global::WebSocketSharp.Server.IWebSocketSession>();
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
					string message = "It is zero or less.";
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

		internal WebSocketSessionManager(global::WebSocketSharp.Logger log)
		{
			_log = log;
			_forSweep = new object();
			_keepClean = true;
			_sessions = new global::System.Collections.Generic.Dictionary<string, global::WebSocketSharp.Server.IWebSocketSession>();
			_state = global::WebSocketSharp.Server.ServerState.Ready;
			_sync = ((global::System.Collections.ICollection)_sessions).SyncRoot;
			_waitTime = global::System.TimeSpan.FromSeconds(1.0);
			setSweepTimer(60000.0);
		}

		private void broadcast(global::WebSocketSharp.Opcode opcode, byte[] data, global::System.Action completed)
		{
			global::System.Collections.Generic.Dictionary<global::WebSocketSharp.CompressionMethod, byte[]> dictionary = new global::System.Collections.Generic.Dictionary<global::WebSocketSharp.CompressionMethod, byte[]>();
			try
			{
				foreach (global::WebSocketSharp.Server.IWebSocketSession session in Sessions)
				{
					if (_state != global::WebSocketSharp.Server.ServerState.Start)
					{
						_log.Error("The service is shutting down.");
						break;
					}
					session.Context.WebSocket.Send(opcode, data, dictionary);
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

		private void broadcast(global::WebSocketSharp.Opcode opcode, global::System.IO.Stream stream, global::System.Action completed)
		{
			global::System.Collections.Generic.Dictionary<global::WebSocketSharp.CompressionMethod, global::System.IO.Stream> dictionary = new global::System.Collections.Generic.Dictionary<global::WebSocketSharp.CompressionMethod, global::System.IO.Stream>();
			try
			{
				foreach (global::WebSocketSharp.Server.IWebSocketSession session in Sessions)
				{
					if (_state != global::WebSocketSharp.Server.ServerState.Start)
					{
						_log.Error("The service is shutting down.");
						break;
					}
					session.Context.WebSocket.Send(opcode, stream, dictionary);
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

		private void broadcastAsync(global::WebSocketSharp.Opcode opcode, byte[] data, global::System.Action completed)
		{
			global::System.Threading.ThreadPool.QueueUserWorkItem(delegate
			{
				broadcast(opcode, data, completed);
			});
		}

		private void broadcastAsync(global::WebSocketSharp.Opcode opcode, global::System.IO.Stream stream, global::System.Action completed)
		{
			global::System.Threading.ThreadPool.QueueUserWorkItem(delegate
			{
				broadcast(opcode, stream, completed);
			});
		}

		private global::System.Collections.Generic.Dictionary<string, bool> broadping(byte[] frameAsBytes)
		{
			global::System.Collections.Generic.Dictionary<string, bool> dictionary = new global::System.Collections.Generic.Dictionary<string, bool>();
			foreach (global::WebSocketSharp.Server.IWebSocketSession session in Sessions)
			{
				if (_state != global::WebSocketSharp.Server.ServerState.Start)
				{
					_log.Error("The service is shutting down.");
					break;
				}
				bool value = session.Context.WebSocket.Ping(frameAsBytes, _waitTime);
				dictionary.Add(session.ID, value);
			}
			return dictionary;
		}

		private bool canSet()
		{
			return _state == global::WebSocketSharp.Server.ServerState.Ready || _state == global::WebSocketSharp.Server.ServerState.Stop;
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

		private void stop(global::WebSocketSharp.PayloadData payloadData, bool send)
		{
			byte[] frameAsBytes = (send ? global::WebSocketSharp.WebSocketFrame.CreateCloseFrame(payloadData, mask: false).ToArray() : null);
			lock (_sync)
			{
				_state = global::WebSocketSharp.Server.ServerState.ShuttingDown;
				_sweepTimer.Enabled = false;
				foreach (global::WebSocketSharp.Server.IWebSocketSession item in _sessions.Values.ToList())
				{
					item.Context.WebSocket.Close(payloadData, frameAsBytes);
				}
				_state = global::WebSocketSharp.Server.ServerState.Stop;
			}
		}

		private bool tryGetSession(string id, out global::WebSocketSharp.Server.IWebSocketSession session)
		{
			session = null;
			if (_state != global::WebSocketSharp.Server.ServerState.Start)
			{
				return false;
			}
			lock (_sync)
			{
				if (_state != global::WebSocketSharp.Server.ServerState.Start)
				{
					return false;
				}
				return _sessions.TryGetValue(id, out session);
			}
		}

		internal string Add(global::WebSocketSharp.Server.IWebSocketSession session)
		{
			lock (_sync)
			{
				if (_state != global::WebSocketSharp.Server.ServerState.Start)
				{
					return null;
				}
				string text = createID();
				_sessions.Add(text, session);
				return text;
			}
		}

		internal void Broadcast(global::WebSocketSharp.Opcode opcode, byte[] data, global::System.Collections.Generic.Dictionary<global::WebSocketSharp.CompressionMethod, byte[]> cache)
		{
			foreach (global::WebSocketSharp.Server.IWebSocketSession session in Sessions)
			{
				if (_state != global::WebSocketSharp.Server.ServerState.Start)
				{
					_log.Error("The service is shutting down.");
					break;
				}
				session.Context.WebSocket.Send(opcode, data, cache);
			}
		}

		internal void Broadcast(global::WebSocketSharp.Opcode opcode, global::System.IO.Stream stream, global::System.Collections.Generic.Dictionary<global::WebSocketSharp.CompressionMethod, global::System.IO.Stream> cache)
		{
			foreach (global::WebSocketSharp.Server.IWebSocketSession session in Sessions)
			{
				if (_state != global::WebSocketSharp.Server.ServerState.Start)
				{
					_log.Error("The service is shutting down.");
					break;
				}
				session.Context.WebSocket.Send(opcode, stream, cache);
			}
		}

		internal global::System.Collections.Generic.Dictionary<string, bool> Broadping(byte[] frameAsBytes, global::System.TimeSpan timeout)
		{
			global::System.Collections.Generic.Dictionary<string, bool> dictionary = new global::System.Collections.Generic.Dictionary<string, bool>();
			foreach (global::WebSocketSharp.Server.IWebSocketSession session in Sessions)
			{
				if (_state != global::WebSocketSharp.Server.ServerState.Start)
				{
					_log.Error("The service is shutting down.");
					break;
				}
				bool value = session.Context.WebSocket.Ping(frameAsBytes, timeout);
				dictionary.Add(session.ID, value);
			}
			return dictionary;
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
				_state = global::WebSocketSharp.Server.ServerState.Start;
			}
		}

		internal void Stop(ushort code, string reason)
		{
			if (code == 1005)
			{
				stop(global::WebSocketSharp.PayloadData.Empty, send: true);
			}
			else
			{
				stop(new global::WebSocketSharp.PayloadData(code, reason), !code.IsReserved());
			}
		}

		public void Broadcast(byte[] data)
		{
			if (_state != global::WebSocketSharp.Server.ServerState.Start)
			{
				string message = "The current state of the service is not Start.";
				throw new global::System.InvalidOperationException(message);
			}
			if (data == null)
			{
				throw new global::System.ArgumentNullException("data");
			}
			if (data.LongLength <= global::WebSocketSharp.WebSocket.FragmentLength)
			{
				broadcast(global::WebSocketSharp.Opcode.Binary, data, null);
			}
			else
			{
				broadcast(global::WebSocketSharp.Opcode.Binary, new global::System.IO.MemoryStream(data), null);
			}
		}

		public void Broadcast(string data)
		{
			if (_state != global::WebSocketSharp.Server.ServerState.Start)
			{
				string message = "The current state of the service is not Start.";
				throw new global::System.InvalidOperationException(message);
			}
			if (data == null)
			{
				throw new global::System.ArgumentNullException("data");
			}
			if (!data.TryGetUTF8EncodedBytes(out var bytes))
			{
				string message2 = "It could not be UTF-8-encoded.";
				throw new global::System.ArgumentException(message2, "data");
			}
			if (bytes.LongLength <= global::WebSocketSharp.WebSocket.FragmentLength)
			{
				broadcast(global::WebSocketSharp.Opcode.Text, bytes, null);
			}
			else
			{
				broadcast(global::WebSocketSharp.Opcode.Text, new global::System.IO.MemoryStream(bytes), null);
			}
		}

		public void Broadcast(global::System.IO.Stream stream, int length)
		{
			if (_state != global::WebSocketSharp.Server.ServerState.Start)
			{
				string message = "The current state of the service is not Start.";
				throw new global::System.InvalidOperationException(message);
			}
			if (stream == null)
			{
				throw new global::System.ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				string message2 = "It cannot be read.";
				throw new global::System.ArgumentException(message2, "stream");
			}
			if (length < 1)
			{
				string message3 = "It is less than 1.";
				throw new global::System.ArgumentException(message3, "length");
			}
			byte[] array = stream.ReadBytes(length);
			int num = array.Length;
			if (num == 0)
			{
				string message4 = "No data could be read from it.";
				throw new global::System.ArgumentException(message4, "stream");
			}
			if (num < length)
			{
				string format = "Only {0} byte(s) of data could be read from the stream.";
				string message5 = string.Format(format, num);
				_log.Warn(message5);
			}
			if (num <= global::WebSocketSharp.WebSocket.FragmentLength)
			{
				broadcast(global::WebSocketSharp.Opcode.Binary, array, null);
			}
			else
			{
				broadcast(global::WebSocketSharp.Opcode.Binary, new global::System.IO.MemoryStream(array), null);
			}
		}

		public void BroadcastAsync(byte[] data, global::System.Action completed)
		{
			if (_state != global::WebSocketSharp.Server.ServerState.Start)
			{
				string message = "The current state of the service is not Start.";
				throw new global::System.InvalidOperationException(message);
			}
			if (data == null)
			{
				throw new global::System.ArgumentNullException("data");
			}
			if (data.LongLength <= global::WebSocketSharp.WebSocket.FragmentLength)
			{
				broadcastAsync(global::WebSocketSharp.Opcode.Binary, data, completed);
			}
			else
			{
				broadcastAsync(global::WebSocketSharp.Opcode.Binary, new global::System.IO.MemoryStream(data), completed);
			}
		}

		public void BroadcastAsync(string data, global::System.Action completed)
		{
			if (_state != global::WebSocketSharp.Server.ServerState.Start)
			{
				string message = "The current state of the service is not Start.";
				throw new global::System.InvalidOperationException(message);
			}
			if (data == null)
			{
				throw new global::System.ArgumentNullException("data");
			}
			if (!data.TryGetUTF8EncodedBytes(out var bytes))
			{
				string message2 = "It could not be UTF-8-encoded.";
				throw new global::System.ArgumentException(message2, "data");
			}
			if (bytes.LongLength <= global::WebSocketSharp.WebSocket.FragmentLength)
			{
				broadcastAsync(global::WebSocketSharp.Opcode.Text, bytes, completed);
			}
			else
			{
				broadcastAsync(global::WebSocketSharp.Opcode.Text, new global::System.IO.MemoryStream(bytes), completed);
			}
		}

		public void BroadcastAsync(global::System.IO.Stream stream, int length, global::System.Action completed)
		{
			if (_state != global::WebSocketSharp.Server.ServerState.Start)
			{
				string message = "The current state of the service is not Start.";
				throw new global::System.InvalidOperationException(message);
			}
			if (stream == null)
			{
				throw new global::System.ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				string message2 = "It cannot be read.";
				throw new global::System.ArgumentException(message2, "stream");
			}
			if (length < 1)
			{
				string message3 = "It is less than 1.";
				throw new global::System.ArgumentException(message3, "length");
			}
			byte[] array = stream.ReadBytes(length);
			int num = array.Length;
			if (num == 0)
			{
				string message4 = "No data could be read from it.";
				throw new global::System.ArgumentException(message4, "stream");
			}
			if (num < length)
			{
				string format = "Only {0} byte(s) of data could be read from the stream.";
				string message5 = string.Format(format, num);
				_log.Warn(message5);
			}
			if (num <= global::WebSocketSharp.WebSocket.FragmentLength)
			{
				broadcastAsync(global::WebSocketSharp.Opcode.Binary, array, completed);
			}
			else
			{
				broadcastAsync(global::WebSocketSharp.Opcode.Binary, new global::System.IO.MemoryStream(array), completed);
			}
		}

		public void CloseSession(string id)
		{
			if (!TryGetSession(id, out var session))
			{
				string message = "The session could not be found.";
				throw new global::System.InvalidOperationException(message);
			}
			session.Context.WebSocket.Close();
		}

		public void CloseSession(string id, ushort code, string reason)
		{
			if (!TryGetSession(id, out var session))
			{
				string message = "The session could not be found.";
				throw new global::System.InvalidOperationException(message);
			}
			session.Context.WebSocket.Close(code, reason);
		}

		public void CloseSession(string id, global::WebSocketSharp.CloseStatusCode code, string reason)
		{
			if (!TryGetSession(id, out var session))
			{
				string message = "The session could not be found.";
				throw new global::System.InvalidOperationException(message);
			}
			session.Context.WebSocket.Close(code, reason);
		}

		public bool PingTo(string id)
		{
			if (!TryGetSession(id, out var session))
			{
				string message = "The session could not be found.";
				throw new global::System.InvalidOperationException(message);
			}
			return session.Context.WebSocket.Ping();
		}

		public bool PingTo(string message, string id)
		{
			if (!TryGetSession(id, out var session))
			{
				string message2 = "The session could not be found.";
				throw new global::System.InvalidOperationException(message2);
			}
			return session.Context.WebSocket.Ping(message);
		}

		public void SendTo(byte[] data, string id)
		{
			if (!TryGetSession(id, out var session))
			{
				string message = "The session could not be found.";
				throw new global::System.InvalidOperationException(message);
			}
			session.Context.WebSocket.Send(data);
		}

		public void SendTo(string data, string id)
		{
			if (!TryGetSession(id, out var session))
			{
				string message = "The session could not be found.";
				throw new global::System.InvalidOperationException(message);
			}
			session.Context.WebSocket.Send(data);
		}

		public void SendTo(global::System.IO.Stream stream, int length, string id)
		{
			if (!TryGetSession(id, out var session))
			{
				string message = "The session could not be found.";
				throw new global::System.InvalidOperationException(message);
			}
			session.Context.WebSocket.Send(stream, length);
		}

		public void SendToAsync(byte[] data, string id, global::System.Action<bool> completed)
		{
			if (!TryGetSession(id, out var session))
			{
				string message = "The session could not be found.";
				throw new global::System.InvalidOperationException(message);
			}
			session.Context.WebSocket.SendAsync(data, completed);
		}

		public void SendToAsync(string data, string id, global::System.Action<bool> completed)
		{
			if (!TryGetSession(id, out var session))
			{
				string message = "The session could not be found.";
				throw new global::System.InvalidOperationException(message);
			}
			session.Context.WebSocket.SendAsync(data, completed);
		}

		public void SendToAsync(global::System.IO.Stream stream, int length, string id, global::System.Action<bool> completed)
		{
			if (!TryGetSession(id, out var session))
			{
				string message = "The session could not be found.";
				throw new global::System.InvalidOperationException(message);
			}
			session.Context.WebSocket.SendAsync(stream, length, completed);
		}

		public void Sweep()
		{
			if (_sweeping)
			{
				_log.Info("The sweeping is already in progress.");
				return;
			}
			lock (_forSweep)
			{
				if (_sweeping)
				{
					_log.Info("The sweeping is already in progress.");
					return;
				}
				_sweeping = true;
			}
			foreach (string inactiveID in InactiveIDs)
			{
				if (_state != global::WebSocketSharp.Server.ServerState.Start)
				{
					break;
				}
				lock (_sync)
				{
					if (_state != global::WebSocketSharp.Server.ServerState.Start)
					{
						break;
					}
					if (_sessions.TryGetValue(inactiveID, out var value))
					{
						switch (value.ConnectionState)
						{
						case global::WebSocketSharp.WebSocketState.Open:
							value.Context.WebSocket.Close(global::WebSocketSharp.CloseStatusCode.Abnormal);
							break;
						default:
							_sessions.Remove(inactiveID);
							break;
						case global::WebSocketSharp.WebSocketState.Closing:
							break;
						}
					}
					continue;
				}
			}
			_sweeping = false;
		}

		public bool TryGetSession(string id, out global::WebSocketSharp.Server.IWebSocketSession session)
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
