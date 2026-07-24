namespace UnityWebSocketSharp.Net
{
	internal class RequestStream : global::System.IO.Stream
	{
		private long _bodyLeft;

		private int _count;

		private bool _disposed;

		private byte[] _initialBuffer;

		private global::System.IO.Stream _innerStream;

		private int _offset;

		internal int Count => _count;

		internal byte[] InitialBuffer => _initialBuffer;

		internal int Offset => _offset;

		public override bool CanRead => true;

		public override bool CanSeek => false;

		public override bool CanWrite => false;

		public override long Length
		{
			get
			{
				throw new global::System.NotSupportedException();
			}
		}

		public override long Position
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

		internal RequestStream(global::System.IO.Stream innerStream, byte[] initialBuffer, int offset, int count, long contentLength)
		{
			_innerStream = innerStream;
			_initialBuffer = initialBuffer;
			_offset = offset;
			_count = count;
			_bodyLeft = contentLength;
		}

		private int fillFromInitialBuffer(byte[] buffer, int offset, int count)
		{
			if (_bodyLeft == 0L)
			{
				return -1;
			}
			if (_count == 0)
			{
				return 0;
			}
			if (count > _count)
			{
				count = _count;
			}
			if (_bodyLeft > 0 && _bodyLeft < count)
			{
				count = (int)_bodyLeft;
			}
			global::System.Buffer.BlockCopy(_initialBuffer, _offset, buffer, offset, count);
			_offset += count;
			_count -= count;
			if (_bodyLeft > 0)
			{
				_bodyLeft -= count;
			}
			return count;
		}

		public override global::System.IAsyncResult BeginRead(byte[] buffer, int offset, int count, global::System.AsyncCallback callback, object state)
		{
			if (_disposed)
			{
				throw new global::System.ObjectDisposedException(GetType().ToString());
			}
			if (buffer == null)
			{
				throw new global::System.ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				string message = "A negative value.";
				throw new global::System.ArgumentOutOfRangeException("offset", message);
			}
			if (count < 0)
			{
				string message2 = "A negative value.";
				throw new global::System.ArgumentOutOfRangeException("count", message2);
			}
			int num = buffer.Length;
			if (offset + count > num)
			{
				throw new global::System.ArgumentException("The sum of 'offset' and 'count' is greater than the length of 'buffer'.");
			}
			if (count == 0)
			{
				return _innerStream.BeginRead(buffer, offset, 0, callback, state);
			}
			int num2 = fillFromInitialBuffer(buffer, offset, count);
			if (num2 != 0)
			{
				global::UnityWebSocketSharp.Net.HttpStreamAsyncResult httpStreamAsyncResult = new global::UnityWebSocketSharp.Net.HttpStreamAsyncResult(callback, state);
				httpStreamAsyncResult.Buffer = buffer;
				httpStreamAsyncResult.Offset = offset;
				httpStreamAsyncResult.Count = count;
				httpStreamAsyncResult.SyncRead = ((num2 > 0) ? num2 : 0);
				httpStreamAsyncResult.Complete();
				return httpStreamAsyncResult;
			}
			if (_bodyLeft > 0 && _bodyLeft < count)
			{
				count = (int)_bodyLeft;
			}
			return _innerStream.BeginRead(buffer, offset, count, callback, state);
		}

		public override global::System.IAsyncResult BeginWrite(byte[] buffer, int offset, int count, global::System.AsyncCallback callback, object state)
		{
			throw new global::System.NotSupportedException();
		}

		public override void Close()
		{
			_disposed = true;
		}

		public override int EndRead(global::System.IAsyncResult asyncResult)
		{
			if (_disposed)
			{
				throw new global::System.ObjectDisposedException(GetType().ToString());
			}
			if (asyncResult == null)
			{
				throw new global::System.ArgumentNullException("asyncResult");
			}
			if (asyncResult is global::UnityWebSocketSharp.Net.HttpStreamAsyncResult)
			{
				global::UnityWebSocketSharp.Net.HttpStreamAsyncResult httpStreamAsyncResult = (global::UnityWebSocketSharp.Net.HttpStreamAsyncResult)asyncResult;
				if (!httpStreamAsyncResult.IsCompleted)
				{
					httpStreamAsyncResult.AsyncWaitHandle.WaitOne();
				}
				return httpStreamAsyncResult.SyncRead;
			}
			int num = _innerStream.EndRead(asyncResult);
			if (num > 0 && _bodyLeft > 0)
			{
				_bodyLeft -= num;
			}
			return num;
		}

		public override void EndWrite(global::System.IAsyncResult asyncResult)
		{
			throw new global::System.NotSupportedException();
		}

		public override void Flush()
		{
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			if (_disposed)
			{
				throw new global::System.ObjectDisposedException(GetType().ToString());
			}
			if (buffer == null)
			{
				throw new global::System.ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				string message = "A negative value.";
				throw new global::System.ArgumentOutOfRangeException("offset", message);
			}
			if (count < 0)
			{
				string message2 = "A negative value.";
				throw new global::System.ArgumentOutOfRangeException("count", message2);
			}
			int num = buffer.Length;
			if (offset + count > num)
			{
				throw new global::System.ArgumentException("The sum of 'offset' and 'count' is greater than the length of 'buffer'.");
			}
			if (count == 0)
			{
				return 0;
			}
			int num2 = fillFromInitialBuffer(buffer, offset, count);
			if (num2 == -1)
			{
				return 0;
			}
			if (num2 > 0)
			{
				return num2;
			}
			if (_bodyLeft > 0 && _bodyLeft < count)
			{
				count = (int)_bodyLeft;
			}
			num2 = _innerStream.Read(buffer, offset, count);
			if (num2 > 0 && _bodyLeft > 0)
			{
				_bodyLeft -= num2;
			}
			return num2;
		}

		public override long Seek(long offset, global::System.IO.SeekOrigin origin)
		{
			throw new global::System.NotSupportedException();
		}

		public override void SetLength(long value)
		{
			throw new global::System.NotSupportedException();
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new global::System.NotSupportedException();
		}
	}
}
