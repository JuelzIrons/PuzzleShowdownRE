namespace UnityWebSocketSharp.Net
{
	internal class ChunkedRequestStream : global::UnityWebSocketSharp.Net.RequestStream
	{
		private static readonly int _bufferLength;

		private global::UnityWebSocketSharp.Net.HttpListenerContext _context;

		private global::UnityWebSocketSharp.Net.ChunkStream _decoder;

		private bool _disposed;

		private bool _noMoreData;

		internal bool HasRemainingBuffer => _decoder.Count + base.Count > 0;

		internal byte[] RemainingBuffer
		{
			get
			{
				using global::System.IO.MemoryStream memoryStream = new global::System.IO.MemoryStream();
				int count = _decoder.Count;
				if (count > 0)
				{
					memoryStream.Write(_decoder.EndBuffer, _decoder.Offset, count);
				}
				count = base.Count;
				if (count > 0)
				{
					memoryStream.Write(base.InitialBuffer, base.Offset, count);
				}
				memoryStream.Close();
				return memoryStream.ToArray();
			}
		}

		static ChunkedRequestStream()
		{
			_bufferLength = 8192;
		}

		internal ChunkedRequestStream(global::System.IO.Stream innerStream, byte[] initialBuffer, int offset, int count, global::UnityWebSocketSharp.Net.HttpListenerContext context)
			: base(innerStream, initialBuffer, offset, count, -1L)
		{
			_context = context;
			_decoder = new global::UnityWebSocketSharp.Net.ChunkStream((global::UnityWebSocketSharp.Net.WebHeaderCollection)context.Request.Headers);
		}

		private void onRead(global::System.IAsyncResult asyncResult)
		{
			global::UnityWebSocketSharp.Net.ReadBufferState readBufferState = (global::UnityWebSocketSharp.Net.ReadBufferState)asyncResult.AsyncState;
			global::UnityWebSocketSharp.Net.HttpStreamAsyncResult asyncResult2 = readBufferState.AsyncResult;
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
			global::UnityWebSocketSharp.Net.HttpStreamAsyncResult httpStreamAsyncResult = new global::UnityWebSocketSharp.Net.HttpStreamAsyncResult(callback, state);
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
			global::UnityWebSocketSharp.Net.ReadBufferState readBufferState = new global::UnityWebSocketSharp.Net.ReadBufferState(buffer, offset, count, httpStreamAsyncResult);
			readBufferState.InitialCount += num2;
			base.BeginRead(httpStreamAsyncResult.Buffer, httpStreamAsyncResult.Offset, httpStreamAsyncResult.Count, (global::System.AsyncCallback)onRead, (object)readBufferState);
			return httpStreamAsyncResult;
		}

		public override void Close()
		{
			if (!_disposed)
			{
				base.Close();
				_disposed = true;
			}
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
			if (!(asyncResult is global::UnityWebSocketSharp.Net.HttpStreamAsyncResult httpStreamAsyncResult))
			{
				throw new global::System.ArgumentException("A wrong IAsyncResult instance.", "asyncResult");
			}
			if (!httpStreamAsyncResult.IsCompleted)
			{
				httpStreamAsyncResult.AsyncWaitHandle.WaitOne();
			}
			if (httpStreamAsyncResult.HasException)
			{
				string message = "The I/O operation has been aborted.";
				throw new global::UnityWebSocketSharp.Net.HttpListenerException(995, message);
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
