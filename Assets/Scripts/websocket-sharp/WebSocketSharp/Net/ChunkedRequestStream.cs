namespace WebSocketSharp.Net
{
	internal class ChunkedRequestStream : global::WebSocketSharp.Net.RequestStream
	{
		private static readonly int _bufferLength;

		private global::WebSocketSharp.Net.HttpListenerContext _context;

		private global::WebSocketSharp.Net.ChunkStream _decoder;

		private bool _disposed;

		private bool _noMoreData;

		static ChunkedRequestStream()
		{
			_bufferLength = 8192;
		}

		internal ChunkedRequestStream(global::System.IO.Stream stream, byte[] buffer, int offset, int count, global::WebSocketSharp.Net.HttpListenerContext context)
			: base(stream, buffer, offset, count, -1L)
		{
			_context = context;
			_decoder = new global::WebSocketSharp.Net.ChunkStream((global::WebSocketSharp.Net.WebHeaderCollection)context.Request.Headers);
		}

		private void onRead(global::System.IAsyncResult asyncResult)
		{
			global::WebSocketSharp.Net.ReadBufferState readBufferState = (global::WebSocketSharp.Net.ReadBufferState)asyncResult.AsyncState;
			global::WebSocketSharp.Net.HttpStreamAsyncResult asyncResult2 = readBufferState.AsyncResult;
			try
			{
				int count = base.EndRead(asyncResult);
				_decoder.Write(asyncResult2.Buffer, asyncResult2.Offset, count);
				count = _decoder.Read(readBufferState.Buffer, readBufferState.Offset, readBufferState.Count);
				readBufferState.Offset += count;
				readBufferState.Count -= count;
				if (readBufferState.Count == 0 || !_decoder.WantsMore || count == 0)
				{
					_noMoreData = !_decoder.WantsMore && count == 0;
					asyncResult2.Count = readBufferState.InitialCount - readBufferState.Count;
					asyncResult2.Complete();
				}
				else
				{
					base.BeginRead(asyncResult2.Buffer, asyncResult2.Offset, asyncResult2.Count, (global::System.AsyncCallback)onRead, (object)readBufferState);
				}
			}
			catch (global::System.Exception exception)
			{
				_context.ErrorMessage = "I/O operation aborted";
				_context.SendError();
				asyncResult2.Complete(exception);
			}
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
			global::WebSocketSharp.Net.HttpStreamAsyncResult httpStreamAsyncResult = new global::WebSocketSharp.Net.HttpStreamAsyncResult(callback, state);
			if (_noMoreData)
			{
				httpStreamAsyncResult.Complete();
				return httpStreamAsyncResult;
			}
			int num2 = _decoder.Read(buffer, offset, count);
			offset += num2;
			count -= num2;
			if (count == 0)
			{
				httpStreamAsyncResult.Count = num2;
				httpStreamAsyncResult.Complete();
				return httpStreamAsyncResult;
			}
			if (!_decoder.WantsMore)
			{
				_noMoreData = num2 == 0;
				httpStreamAsyncResult.Count = num2;
				httpStreamAsyncResult.Complete();
				return httpStreamAsyncResult;
			}
			httpStreamAsyncResult.Buffer = new byte[_bufferLength];
			httpStreamAsyncResult.Offset = 0;
			httpStreamAsyncResult.Count = _bufferLength;
			global::WebSocketSharp.Net.ReadBufferState readBufferState = new global::WebSocketSharp.Net.ReadBufferState(buffer, offset, count, httpStreamAsyncResult);
			readBufferState.InitialCount += num2;
			base.BeginRead(httpStreamAsyncResult.Buffer, httpStreamAsyncResult.Offset, httpStreamAsyncResult.Count, (global::System.AsyncCallback)onRead, (object)readBufferState);
			return httpStreamAsyncResult;
		}

		public override void Close()
		{
			if (!_disposed)
			{
				_disposed = true;
				base.Close();
			}
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
			if (!(asyncResult is global::WebSocketSharp.Net.HttpStreamAsyncResult httpStreamAsyncResult))
			{
				string message = "A wrong IAsyncResult instance.";
				throw new global::System.ArgumentException(message, "asyncResult");
			}
			if (!httpStreamAsyncResult.IsCompleted)
			{
				httpStreamAsyncResult.AsyncWaitHandle.WaitOne();
			}
			if (httpStreamAsyncResult.HasException)
			{
				string message2 = "The I/O operation has been aborted.";
				throw new global::WebSocketSharp.Net.HttpListenerException(995, message2);
			}
			return httpStreamAsyncResult.Count;
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			global::System.IAsyncResult asyncResult = BeginRead(buffer, offset, count, null, null);
			return EndRead(asyncResult);
		}
	}
}
