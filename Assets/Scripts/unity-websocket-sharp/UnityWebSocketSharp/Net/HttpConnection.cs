namespace UnityWebSocketSharp.Net
{
	internal sealed class HttpConnection
	{
		private int _attempts;

		private byte[] _buffer;

		private static readonly int _bufferLength;

		private global::UnityWebSocketSharp.Net.HttpListenerContext _context;

		private global::System.Text.StringBuilder _currentLine;

		private global::UnityWebSocketSharp.Net.EndPointListener _endPointListener;

		private global::UnityWebSocketSharp.Net.InputState _inputState;

		private global::UnityWebSocketSharp.Net.RequestStream _inputStream;

		private global::UnityWebSocketSharp.Net.LineState _lineState;

		private global::System.Net.EndPoint _localEndPoint;

		private static readonly int _maxInputLength;

		private global::UnityWebSocketSharp.Net.ResponseStream _outputStream;

		private int _position;

		private global::System.Net.EndPoint _remoteEndPoint;

		private global::System.IO.MemoryStream _requestBuffer;

		private int _reuses;

		private bool _secure;

		private global::System.Net.Sockets.Socket _socket;

		private global::System.IO.Stream _stream;

		private object _sync;

		private int _timeout;

		private global::System.Collections.Generic.Dictionary<int, bool> _timeoutCanceled;

		private global::System.Threading.Timer _timer;

		public bool IsClosed => _socket == null;

		public bool IsLocal => ((global::System.Net.IPEndPoint)_remoteEndPoint).Address.IsLocal();

		public bool IsSecure => _secure;

		public global::System.Net.IPEndPoint LocalEndPoint => (global::System.Net.IPEndPoint)_localEndPoint;

		public global::System.Net.IPEndPoint RemoteEndPoint => (global::System.Net.IPEndPoint)_remoteEndPoint;

		public int Reuses => _reuses;

		public global::System.IO.Stream Stream => _stream;

		static HttpConnection()
		{
			_bufferLength = 8192;
			_maxInputLength = 32768;
		}

		internal HttpConnection(global::System.Net.Sockets.Socket socket, global::UnityWebSocketSharp.Net.EndPointListener listener)
		{
			_socket = socket;
			_endPointListener = listener;
			global::System.Net.Sockets.NetworkStream networkStream = new global::System.Net.Sockets.NetworkStream(socket, ownsSocket: false);
			if (listener.IsSecure)
			{
				global::UnityWebSocketSharp.Net.ServerSslConfiguration sslConfiguration = listener.SslConfiguration;
				global::System.Net.Security.SslStream sslStream = new global::System.Net.Security.SslStream(networkStream, leaveInnerStreamOpen: false, sslConfiguration.ClientCertificateValidationCallback);
				sslStream.AuthenticateAsServer(sslConfiguration.ServerCertificate, sslConfiguration.ClientCertificateRequired, sslConfiguration.EnabledSslProtocols, sslConfiguration.CheckCertificateRevocation);
				_secure = true;
				_stream = sslStream;
			}
			else
			{
				_stream = networkStream;
			}
			_buffer = new byte[_bufferLength];
			_localEndPoint = socket.LocalEndPoint;
			_remoteEndPoint = socket.RemoteEndPoint;
			_sync = new object();
			_timeoutCanceled = new global::System.Collections.Generic.Dictionary<int, bool>();
			_timer = new global::System.Threading.Timer(onTimeout, this, -1, -1);
			init(new global::System.IO.MemoryStream(), 90000);
		}

		private void close()
		{
			lock (_sync)
			{
				if (_socket == null)
				{
					return;
				}
				disposeTimer();
				disposeRequestBuffer();
				disposeStream();
				closeSocket();
			}
			_context.Unregister();
			_endPointListener.RemoveConnection(this);
		}

		private void closeSocket()
		{
			try
			{
				_socket.Shutdown(global::System.Net.Sockets.SocketShutdown.Both);
			}
			catch
			{
			}
			_socket.Close();
			_socket = null;
		}

		private static global::System.IO.MemoryStream createRequestBuffer(global::UnityWebSocketSharp.Net.RequestStream inputStream)
		{
			global::System.IO.MemoryStream memoryStream = new global::System.IO.MemoryStream();
			if (inputStream is global::UnityWebSocketSharp.Net.ChunkedRequestStream)
			{
				global::UnityWebSocketSharp.Net.ChunkedRequestStream chunkedRequestStream = (global::UnityWebSocketSharp.Net.ChunkedRequestStream)inputStream;
				if (chunkedRequestStream.HasRemainingBuffer)
				{
					byte[] remainingBuffer = chunkedRequestStream.RemainingBuffer;
					memoryStream.Write(remainingBuffer, 0, remainingBuffer.Length);
				}
				return memoryStream;
			}
			int count = inputStream.Count;
			if (count > 0)
			{
				memoryStream.Write(inputStream.InitialBuffer, inputStream.Offset, count);
			}
			return memoryStream;
		}

		private void disposeRequestBuffer()
		{
			if (_requestBuffer != null)
			{
				_requestBuffer.Dispose();
				_requestBuffer = null;
			}
		}

		private void disposeStream()
		{
			if (_stream != null)
			{
				_stream.Dispose();
				_stream = null;
			}
		}

		private void disposeTimer()
		{
			if (_timer != null)
			{
				try
				{
					_timer.Change(-1, -1);
				}
				catch
				{
				}
				_timer.Dispose();
				_timer = null;
			}
		}

		private void init(global::System.IO.MemoryStream requestBuffer, int timeout)
		{
			_requestBuffer = requestBuffer;
			_timeout = timeout;
			_context = new global::UnityWebSocketSharp.Net.HttpListenerContext(this);
			_currentLine = new global::System.Text.StringBuilder(64);
			_inputState = global::UnityWebSocketSharp.Net.InputState.RequestLine;
			_inputStream = null;
			_lineState = global::UnityWebSocketSharp.Net.LineState.None;
			_outputStream = null;
			_position = 0;
		}

		private static void onRead(global::System.IAsyncResult asyncResult)
		{
			global::UnityWebSocketSharp.Net.HttpConnection httpConnection = (global::UnityWebSocketSharp.Net.HttpConnection)asyncResult.AsyncState;
			int attempts = httpConnection._attempts;
			if (httpConnection._socket == null)
			{
				return;
			}
			lock (httpConnection._sync)
			{
				if (httpConnection._socket == null)
				{
					return;
				}
				httpConnection._timer.Change(-1, -1);
				httpConnection._timeoutCanceled[attempts] = true;
				int num = 0;
				try
				{
					num = httpConnection._stream.EndRead(asyncResult);
				}
				catch (global::System.Exception)
				{
					httpConnection.close();
					return;
				}
				if (num <= 0)
				{
					httpConnection.close();
					return;
				}
				httpConnection._requestBuffer.Write(httpConnection._buffer, 0, num);
				if (!httpConnection.processRequestBuffer())
				{
					httpConnection.BeginReadRequest();
				}
			}
		}

		private static void onTimeout(object state)
		{
			global::UnityWebSocketSharp.Net.HttpConnection httpConnection = (global::UnityWebSocketSharp.Net.HttpConnection)state;
			int attempts = httpConnection._attempts;
			if (httpConnection._socket == null)
			{
				return;
			}
			lock (httpConnection._sync)
			{
				if (httpConnection._socket != null && !httpConnection._timeoutCanceled[attempts])
				{
					httpConnection._context.SendError(408);
				}
			}
		}

		private bool processInput(byte[] data, int length)
		{
			global::UnityWebSocketSharp.Net.HttpListenerRequest request = _context.Request;
			try
			{
				while (true)
				{
					int nread;
					string text = readLineFrom(data, _position, length, out nread);
					_position += nread;
					if (text == null)
					{
						break;
					}
					if (text.Length == 0)
					{
						if (_inputState != global::UnityWebSocketSharp.Net.InputState.RequestLine)
						{
							if (_position > _maxInputLength)
							{
								_context.ErrorMessage = "Headers too long";
							}
							return true;
						}
						continue;
					}
					if (_inputState == global::UnityWebSocketSharp.Net.InputState.RequestLine)
					{
						request.SetRequestLine(text);
						_inputState = global::UnityWebSocketSharp.Net.InputState.Headers;
					}
					else
					{
						request.AddHeader(text);
					}
					if (_context.HasErrorMessage)
					{
						return true;
					}
				}
			}
			catch (global::System.Exception)
			{
				_context.ErrorMessage = "Processing failure";
				return true;
			}
			if (_position >= _maxInputLength)
			{
				_context.ErrorMessage = "Headers too long";
				return true;
			}
			return false;
		}

		private bool processRequestBuffer()
		{
			byte[] buffer = _requestBuffer.GetBuffer();
			int length = (int)_requestBuffer.Length;
			if (!processInput(buffer, length))
			{
				return false;
			}
			global::UnityWebSocketSharp.Net.HttpListenerRequest request = _context.Request;
			if (!_context.HasErrorMessage)
			{
				request.FinishInitialization();
			}
			if (_context.HasErrorMessage)
			{
				_context.SendError();
				return true;
			}
			global::System.Uri url = request.Url;
			if (!_endPointListener.TrySearchHttpListener(url, out var listener))
			{
				_context.SendError(404);
				return true;
			}
			listener.RegisterContext(_context);
			return true;
		}

		private string readLineFrom(byte[] buffer, int offset, int length, out int nread)
		{
			nread = 0;
			for (int i = offset; i < length; i++)
			{
				nread++;
				byte b = buffer[i];
				switch (b)
				{
				case 13:
					_lineState = global::UnityWebSocketSharp.Net.LineState.Cr;
					continue;
				case 10:
					break;
				default:
					_currentLine.Append((char)b);
					continue;
				}
				_lineState = global::UnityWebSocketSharp.Net.LineState.Lf;
				break;
			}
			if (_lineState != global::UnityWebSocketSharp.Net.LineState.Lf)
			{
				return null;
			}
			string result = _currentLine.ToString();
			_currentLine.Length = 0;
			_lineState = global::UnityWebSocketSharp.Net.LineState.None;
			return result;
		}

		private global::System.IO.MemoryStream takeOverRequestBuffer()
		{
			if (_inputStream != null)
			{
				return createRequestBuffer(_inputStream);
			}
			global::System.IO.MemoryStream memoryStream = new global::System.IO.MemoryStream();
			byte[] buffer = _requestBuffer.GetBuffer();
			int num = (int)_requestBuffer.Length - _position;
			if (num > 0)
			{
				memoryStream.Write(buffer, _position, num);
			}
			disposeRequestBuffer();
			return memoryStream;
		}

		internal void BeginReadRequest()
		{
			_attempts++;
			_timeoutCanceled.Add(_attempts, value: false);
			_timer.Change(_timeout, -1);
			try
			{
				_stream.BeginRead(_buffer, 0, _bufferLength, onRead, this);
			}
			catch (global::System.Exception)
			{
				close();
			}
		}

		internal void Close(bool force)
		{
			if (_socket == null)
			{
				return;
			}
			lock (_sync)
			{
				if (_socket == null)
				{
					return;
				}
				if (force)
				{
					if (_outputStream != null)
					{
						_outputStream.Close(force: true);
					}
					close();
					return;
				}
				GetResponseStream().Close(force: false);
				if (_context.Response.CloseConnection)
				{
					close();
					return;
				}
				if (!_context.Request.FlushInput())
				{
					close();
					return;
				}
				_context.Unregister();
				_reuses++;
				global::System.IO.MemoryStream memoryStream = takeOverRequestBuffer();
				long length = memoryStream.Length;
				init(memoryStream, 15000);
				if (length <= 0 || !processRequestBuffer())
				{
					BeginReadRequest();
				}
			}
		}

		public void Close()
		{
			Close(force: false);
		}

		public global::UnityWebSocketSharp.Net.RequestStream GetRequestStream(long contentLength, bool chunked)
		{
			lock (_sync)
			{
				if (_socket == null)
				{
					return null;
				}
				if (_inputStream != null)
				{
					return _inputStream;
				}
				byte[] buffer = _requestBuffer.GetBuffer();
				int count = (int)_requestBuffer.Length - _position;
				_inputStream = (chunked ? new global::UnityWebSocketSharp.Net.ChunkedRequestStream(_stream, buffer, _position, count, _context) : new global::UnityWebSocketSharp.Net.RequestStream(_stream, buffer, _position, count, contentLength));
				disposeRequestBuffer();
				return _inputStream;
			}
		}

		public global::UnityWebSocketSharp.Net.ResponseStream GetResponseStream()
		{
			lock (_sync)
			{
				if (_socket == null)
				{
					return null;
				}
				if (_outputStream != null)
				{
					return _outputStream;
				}
				bool ignoreWriteExceptions = _context.Listener?.IgnoreWriteExceptions ?? true;
				_outputStream = new global::UnityWebSocketSharp.Net.ResponseStream(_stream, _context.Response, ignoreWriteExceptions);
				return _outputStream;
			}
		}
	}
}
