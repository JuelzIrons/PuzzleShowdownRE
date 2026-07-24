namespace WebSocketSharp.Net
{
	internal class ReadBufferState
	{
		private global::WebSocketSharp.Net.HttpStreamAsyncResult _asyncResult;

		private byte[] _buffer;

		private int _count;

		private int _initialCount;

		private int _offset;

		public global::WebSocketSharp.Net.HttpStreamAsyncResult AsyncResult
		{
			get
			{
				return _asyncResult;
			}
			set
			{
				_asyncResult = value;
			}
		}

		public byte[] Buffer
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

		public int Count
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

		public int InitialCount
		{
			get
			{
				return _initialCount;
			}
			set
			{
				_initialCount = value;
			}
		}

		public int Offset
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

		public ReadBufferState(byte[] buffer, int offset, int count, global::WebSocketSharp.Net.HttpStreamAsyncResult asyncResult)
		{
			_buffer = buffer;
			_offset = offset;
			_count = count;
			_asyncResult = asyncResult;
			_initialCount = count;
		}
	}
}
