namespace UnityWebSocketSharp.Net
{
	internal class HttpListenerAsyncResult : global::System.IAsyncResult
	{
		private global::System.AsyncCallback _callback;

		private bool _completed;

		private bool _completedSynchronously;

		private global::UnityWebSocketSharp.Net.HttpListenerContext _context;

		private bool _endCalled;

		private global::System.Exception _exception;

		private object _state;

		private object _sync;

		private global::System.Threading.ManualResetEvent _waitHandle;

		internal global::UnityWebSocketSharp.Net.HttpListenerContext Context
		{
			get
			{
				if (_exception != null)
				{
					throw _exception;
				}
				return _context;
			}
		}

		internal bool EndCalled
		{
			get
			{
				return _endCalled;
			}
			set
			{
				_endCalled = value;
			}
		}

		internal object SyncRoot => _sync;

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

		public bool CompletedSynchronously => _completedSynchronously;

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

		internal HttpListenerAsyncResult(global::System.AsyncCallback callback, object state)
		{
			_callback = callback;
			_state = state;
			_sync = new object();
		}

		private void complete()
		{
			lock (_sync)
			{
				_completed = true;
				if (_waitHandle != null)
				{
					_waitHandle.Set();
				}
			}
			if (_callback == null)
			{
				return;
			}
			global::System.Threading.ThreadPool.QueueUserWorkItem(delegate
			{
				try
				{
					_callback(this);
				}
				catch
				{
				}
			}, null);
		}

		internal void Complete(global::System.Exception exception)
		{
			_exception = exception;
			complete();
		}

		internal void Complete(global::UnityWebSocketSharp.Net.HttpListenerContext context, bool completedSynchronously)
		{
			_context = context;
			_completedSynchronously = completedSynchronously;
			complete();
		}
	}
}
