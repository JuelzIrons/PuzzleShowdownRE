namespace WebSocketSharp.Net
{
	internal class ResponseStream : global::System.IO.Stream
	{
		private global::System.IO.MemoryStream _bodyBuffer;

		private static readonly byte[] _crlf;

		private bool _disposed;

		private global::System.IO.Stream _innerStream;

		private static readonly byte[] _lastChunk;

		private static readonly int _maxHeadersLength;

		private global::WebSocketSharp.Net.HttpListenerResponse _response;

		private bool _sendChunked;

		private global::System.Action<byte[], int, int> _write;

		private global::System.Action<byte[], int, int> _writeBody;

		private global::System.Action<byte[], int, int> _writeChunked;

		public override bool CanRead => false;

		public override bool CanSeek => false;

		public override bool CanWrite => !_disposed;

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

		static ResponseStream()
		{
			_crlf = new byte[2] { 13, 10 };
			_lastChunk = new byte[5] { 48, 13, 10, 13, 10 };
			_maxHeadersLength = 32768;
		}

		internal ResponseStream(global::System.IO.Stream innerStream, global::WebSocketSharp.Net.HttpListenerResponse response, bool ignoreWriteExceptions)
		{
			_innerStream = innerStream;
			_response = response;
			if (ignoreWriteExceptions)
			{
				_write = writeWithoutThrowingException;
				_writeChunked = writeChunkedWithoutThrowingException;
			}
			else
			{
				_write = innerStream.Write;
				_writeChunked = writeChunked;
			}
			_bodyBuffer = new global::System.IO.MemoryStream();
		}

		private bool flush(bool closing)
		{
			if (!_response.HeadersSent)
			{
				if (!flushHeaders())
				{
					return false;
				}
				_response.HeadersSent = true;
				_sendChunked = _response.SendChunked;
				_writeBody = (_sendChunked ? _writeChunked : _write);
			}
			flushBody(closing);
			return true;
		}

		private void flushBody(bool closing)
		{
			using (_bodyBuffer)
			{
				long length = _bodyBuffer.Length;
				if (length > int.MaxValue)
				{
					_bodyBuffer.Position = 0L;
					int num = 1024;
					byte[] array = new byte[num];
					int num2 = 0;
					while (true)
					{
						num2 = _bodyBuffer.Read(array, 0, num);
						if (num2 <= 0)
						{
							break;
						}
						_writeBody(array, 0, num2);
					}
				}
				else if (length > 0)
				{
					_writeBody(_bodyBuffer.GetBuffer(), 0, (int)length);
				}
			}
			if (!closing)
			{
				_bodyBuffer = new global::System.IO.MemoryStream();
				return;
			}
			if (_sendChunked)
			{
				_write(_lastChunk, 0, 5);
			}
			_bodyBuffer = null;
		}

		private bool flushHeaders()
		{
			if (!_response.SendChunked && _response.ContentLength64 != _bodyBuffer.Length)
			{
				return false;
			}
			string statusLine = _response.StatusLine;
			global::WebSocketSharp.Net.WebHeaderCollection fullHeaders = _response.FullHeaders;
			global::System.IO.MemoryStream memoryStream = new global::System.IO.MemoryStream();
			global::System.Text.Encoding uTF = global::System.Text.Encoding.UTF8;
			using (global::System.IO.StreamWriter streamWriter = new global::System.IO.StreamWriter(memoryStream, uTF, 256))
			{
				streamWriter.Write(statusLine);
				streamWriter.Write(fullHeaders.ToStringMultiValue(response: true));
				streamWriter.Flush();
				int num = uTF.GetPreamble().Length;
				long num2 = memoryStream.Length - num;
				if (num2 > _maxHeadersLength)
				{
					return false;
				}
				_write(memoryStream.GetBuffer(), num, (int)num2);
			}
			_response.CloseConnection = fullHeaders["Connection"] == "close";
			return true;
		}

		private static byte[] getChunkSizeBytes(int size)
		{
			string s = $"{size:x}\r\n";
			return global::System.Text.Encoding.ASCII.GetBytes(s);
		}

		private void writeChunked(byte[] buffer, int offset, int count)
		{
			byte[] chunkSizeBytes = getChunkSizeBytes(count);
			_innerStream.Write(chunkSizeBytes, 0, chunkSizeBytes.Length);
			_innerStream.Write(buffer, offset, count);
			_innerStream.Write(_crlf, 0, 2);
		}

		private void writeChunkedWithoutThrowingException(byte[] buffer, int offset, int count)
		{
			try
			{
				writeChunked(buffer, offset, count);
			}
			catch
			{
			}
		}

		private void writeWithoutThrowingException(byte[] buffer, int offset, int count)
		{
			try
			{
				_innerStream.Write(buffer, offset, count);
			}
			catch
			{
			}
		}

		internal void Close(bool force)
		{
			if (_disposed)
			{
				return;
			}
			_disposed = true;
			if (!force)
			{
				if (flush(closing: true))
				{
					_response.Close();
					_response = null;
					_innerStream = null;
					return;
				}
				_response.CloseConnection = true;
			}
			if (_sendChunked)
			{
				_write(_lastChunk, 0, 5);
			}
			_bodyBuffer.Dispose();
			_response.Abort();
			_bodyBuffer = null;
			_response = null;
			_innerStream = null;
		}

		internal void InternalWrite(byte[] buffer, int offset, int count)
		{
			_write(buffer, offset, count);
		}

		public override global::System.IAsyncResult BeginRead(byte[] buffer, int offset, int count, global::System.AsyncCallback callback, object state)
		{
			throw new global::System.NotSupportedException();
		}

		public override global::System.IAsyncResult BeginWrite(byte[] buffer, int offset, int count, global::System.AsyncCallback callback, object state)
		{
			if (_disposed)
			{
				string objectName = GetType().ToString();
				throw new global::System.ObjectDisposedException(objectName);
			}
			return _bodyBuffer.BeginWrite(buffer, offset, count, callback, state);
		}

		public override void Close()
		{
			Close(force: false);
		}

		protected override void Dispose(bool disposing)
		{
			Close(!disposing);
		}

		public override int EndRead(global::System.IAsyncResult asyncResult)
		{
			throw new global::System.NotSupportedException();
		}

		public override void EndWrite(global::System.IAsyncResult asyncResult)
		{
			if (_disposed)
			{
				string objectName = GetType().ToString();
				throw new global::System.ObjectDisposedException(objectName);
			}
			_bodyBuffer.EndWrite(asyncResult);
		}

		public override void Flush()
		{
			if (!_disposed && (_sendChunked || _response.SendChunked))
			{
				flush(closing: false);
			}
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new global::System.NotSupportedException();
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
			if (_disposed)
			{
				string objectName = GetType().ToString();
				throw new global::System.ObjectDisposedException(objectName);
			}
			_bodyBuffer.Write(buffer, offset, count);
		}
	}
}
