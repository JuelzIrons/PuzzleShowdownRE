namespace UnityWebSocketSharp.Net
{
	internal class HttpStreamAsyncResult : global::System.IAsyncResult
	{
		private byte[] _buffer;

		private global::System.AsyncCallback _callback;

		private bool _completed;

		private int _count;

		private global::System.Exception _exception;

		private int _offset;

		private object _state;

		private object _sync;

		private int _syncRead;

		private global::System.Threading.ManualResetEvent _waitHandle;

		internal byte[] Buffer
		{
			get
			{
				return _buffer;
			}
			set
			{
				_buffer = value;
			}
		}

		internal int Count
		{
			get
			{
				return _count;
			}
			set
			{
				_count = value;
			}
		}

		internal global::System.Exception Exception => _exception;

		internal bool HasException => _exception != null;

		internal int Offset
		{
			get
			{
				return _offset;
			}
			set
			{
				_offset = value;
			}
		}

		internal int SyncRead
		{
			get
			{
				return _syncRead;
			}
			set
			{
				_syncRead = value;
			}
		}

		public object AsyncState => _state;

		public global::System.Threading.WaitHandle AsyncWaitHandle
		{
			get
			{
				lock (_sync)
				{
					if (_waitHandle == null)
					{
						_waitHandle = new global::System.Threading.ManualResetEvent(_completed);
					}
					return _waitHandle;
				}
			}
		}

		public bool CompletedSynchronously => _syncRead == _count;

		public bool IsCompleted
		{
			get
			{
				lock (_sync)
				{
					return _completed;
				}
			}
		}

		internal HttpStreamAsyncResult(global::System.AsyncCallback callback, object state)
		{
			_callback = callback;
			_state = state;
			_sync = new object();
		}

		internal void Complete()
		{
			lock (_sync)
			{
				if (_completed)
				{
					return;
				}
				_completed = true;
				if (_waitHandle != null)
				{
					_waitHandle.Set();
				}
				if (_callback != null)
				{
					_callback.BeginInvoke(this, delegate(global::System.IAsyncResult ar)
					{
						_callback.EndInvoke(ar);
					}, null);
				}
			}
		}

		internal void Complete(global::System.Exception exception)
		{
			lock (_sync)
			{
				if (_completed)
				{
					return;
				}
				_completed = true;
				_exception = exception;
				if (_waitHandle != null)
				{
					_waitHandle.Set();
				}
				if (_callback != null)
				{
					_callback.BeginInvoke(this, delegate(global::System.IAsyncResult ar)
					{
						_callback.EndInvoke(ar);
					}, null);
				}
			}
		}
	}
}
