namespace WebSocketSharp.Net
{
	internal class RequestStream : global::System.IO.Stream
	{
		private long _bodyLeft;

		private byte[] _buffer;

		private int _count;

		private bool _disposed;

		private int _offset;

		private global::System.IO.Stream _stream;

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

		internal RequestStream(global::System.IO.Stream stream, byte[] buffer, int offset, int count, long contentLength)
		{
			_stream = stream;
			_buffer = buffer;
			_offset = offset;
			_count = count;
			_bodyLeft = contentLength;
		}

		private int fillFromBuffer(byte[] buffer, int offset, int count)
		{
			if (_bodyLeft == 0)
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
			global::System.Buffer.BlockCopy(_buffer, _offset, buffer, offset, count);
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
				string objectName = GetType().ToString();
				throw new global::System.ObjectDisposedException(objectName);
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
				string message3 = "The sum of 'offset' and 'count' is greater than the length of 'buffer'.";
				throw new global::System.ArgumentException(message3);
			}
			if (count == 0)
			{
				return _stream.BeginRead(buffer, offset, 0, callback, state);
			}
			int num2 = fillFromBuffer(buffer, offset, count);
			if (num2 != 0)
			{
				global::WebSocketSharp.Net.HttpStreamAsyncResult httpStreamAsyncResult = new global::WebSocketSharp.Net.HttpStreamAsyncResult(callback, state);
				httpStreamAsyncResult.Buffer = buffer;
				httpStreamAsyncResult.Offset = offset;
				httpStreamAsyncResult.Count = count;
				httpStreamAsyncResult.SyncRead = ((num2 > 0) ? num2 : 0);
				httpStreamAsyncResult.Complete();
				return httpStreamAsyncResult;
			}
			if (_bodyLeft >= 0 && _bodyLeft < count)
			{
				count = (int)_bodyLeft;
			}
			return _stream.BeginRead(buffer, offset, count, callback, state);
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
				string objectName = GetType().ToString();
				throw new global::System.ObjectDisposedException(objectName);
			}
			if (asyncResult == null)
			{
				throw new global::System.ArgumentNullException("asyncResult");
			}
			if (asyncResult is global::WebSocketSharp.Net.HttpStreamAsyncResult)
			{
				global::WebSocketSharp.Net.HttpStreamAsyncResult httpStreamAsyncResult = (global::WebSocketSharp.Net.HttpStreamAsyncResult)asyncResult;
				if (!httpStreamAsyncResult.IsCompleted)
				{
					httpStreamAsyncResult.AsyncWaitHandle.WaitOne();
				}
				return httpStreamAsyncResult.SyncRead;
			}
			int num = _stream.EndRead(asyncResult);
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
				string objectName = GetType().ToString();
				throw new global::System.ObjectDisposedException(objectName);
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
				string message3 = "The sum of 'offset' and 'count' is greater than the length of 'buffer'.";
				throw new global::System.ArgumentException(message3);
			}
			if (count == 0)
			{
				return 0;
			}
			int num2 = fillFromBuffer(buffer, offset, count);
			if (num2 == -1)
			{
				return 0;
			}
			if (num2 > 0)
			{
				return num2;
			}
			num2 = _stream.Read(buffer, offset, count);
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
