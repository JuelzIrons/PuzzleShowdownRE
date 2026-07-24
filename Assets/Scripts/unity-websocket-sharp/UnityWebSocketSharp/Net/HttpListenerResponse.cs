namespace UnityWebSocketSharp.Net
{
	internal sealed class HttpListenerResponse : global::System.IDisposable
	{
		private bool _closeConnection;

		private global::System.Text.Encoding _contentEncoding;

		private long _contentLength;

		private string _contentType;

		private global::UnityWebSocketSharp.Net.HttpListenerContext _context;

		private global::UnityWebSocketSharp.Net.CookieCollection _cookies;

		private bool _disposed;

		private global::UnityWebSocketSharp.Net.WebHeaderCollection _headers;

		private bool _headersSent;

		private bool _keepAlive;

		private global::UnityWebSocketSharp.Net.ResponseStream _outputStream;

		private global::System.Uri _redirectLocation;

		private bool _sendChunked;

		private int _statusCode;

		private string _statusDescription;

		private global::System.Version _version;

		internal bool CloseConnection
		{
			get
			{
				return _closeConnection;
			}
			set
			{
				_closeConnection = value;
			}
		}

		internal global::UnityWebSocketSharp.Net.WebHeaderCollection FullHeaders
		{
			get
			{
				global::UnityWebSocketSharp.Net.WebHeaderCollection webHeaderCollection = new global::UnityWebSocketSharp.Net.WebHeaderCollection(global::UnityWebSocketSharp.Net.HttpHeaderType.Response, internallyUsed: true);
				if (_headers != null)
				{
					webHeaderCollection.Add(_headers);
				}
				if (_contentType != null)
				{
					webHeaderCollection.InternalSet("Content-Type", createContentTypeHeaderText(_contentType, _contentEncoding), response: true);
				}
				if (webHeaderCollection["Server"] == null)
				{
					webHeaderCollection.InternalSet("Server", "websocket-sharp/1.0", response: true);
				}
				if (webHeaderCollection["Date"] == null)
				{
					webHeaderCollection.InternalSet("Date", global::System.DateTime.UtcNow.ToString("r", global::System.Globalization.CultureInfo.InvariantCulture), response: true);
				}
				if (_sendChunked)
				{
					webHeaderCollection.InternalSet("Transfer-Encoding", "chunked", response: true);
				}
				else
				{
					webHeaderCollection.InternalSet("Content-Length", _contentLength.ToString(global::System.Globalization.CultureInfo.InvariantCulture), response: true);
				}
				bool num = !_context.Request.KeepAlive || !_keepAlive || _statusCode == 400 || _statusCode == 408 || _statusCode == 411 || _statusCode == 413 || _statusCode == 414 || _statusCode == 500 || _statusCode == 503;
				int reuses = _context.Connection.Reuses;
				if (num || reuses >= 100)
				{
					webHeaderCollection.InternalSet("Connection", "close", response: true);
				}
				else
				{
					webHeaderCollection.InternalSet("Keep-Alive", $"timeout=15,max={100 - reuses}", response: true);
					if (_context.Request.ProtocolVersion < global::UnityWebSocketSharp.Net.HttpVersion.Version11)
					{
						webHeaderCollection.InternalSet("Connection", "keep-alive", response: true);
					}
				}
				if (_redirectLocation != null)
				{
					webHeaderCollection.InternalSet("Location", _redirectLocation.AbsoluteUri, response: true);
				}
				if (_cookies != null)
				{
					foreach (global::UnityWebSocketSharp.Net.Cookie cookie in _cookies)
					{
						webHeaderCollection.InternalSet("Set-Cookie", cookie.ToResponseString(), response: true);
					}
				}
				return webHeaderCollection;
			}
		}

		internal bool HeadersSent
		{
			get
			{
				return _headersSent;
			}
			set
			{
				_headersSent = value;
			}
		}

		internal string StatusLine => $"HTTP/{_version} {_statusCode} {_statusDescription}\r\n";

		public global::System.Text.Encoding ContentEncoding
		{
			get
			{
				return _contentEncoding;
			}
			set
			{
				if (_disposed)
				{
					throw new global::System.ObjectDisposedException(GetType().ToString());
				}
				if (_headersSent)
				{
					throw new global::System.InvalidOperationException("The response is already being sent.");
				}
				_contentEncoding = value;
			}
		}

		public long ContentLength64
		{
			get
			{
				return _contentLength;
			}
			set
			{
				if (_disposed)
				{
					throw new global::System.ObjectDisposedException(GetType().ToString());
				}
				if (_headersSent)
				{
					throw new global::System.InvalidOperationException("The response is already being sent.");
				}
				if (value < 0)
				{
					throw new global::System.ArgumentOutOfRangeException("Less than zero.", "value");
				}
				_contentLength = value;
			}
		}

		public string ContentType
		{
			get
			{
				return _contentType;
			}
			set
			{
				if (_disposed)
				{
					throw new global::System.ObjectDisposedException(GetType().ToString());
				}
				if (_headersSent)
				{
					throw new global::System.InvalidOperationException("The response is already being sent.");
				}
				if (value == null)
				{
					_contentType = null;
					return;
				}
				if (value.Length == 0)
				{
					throw new global::System.ArgumentException("An empty string.", "value");
				}
				if (!isValidForContentType(value))
				{
					throw new global::System.ArgumentException("It contains an invalid character.", "value");
				}
				_contentType = value;
			}
		}

		public global::UnityWebSocketSharp.Net.CookieCollection Cookies
		{
			get
			{
				if (_cookies == null)
				{
					_cookies = new global::UnityWebSocketSharp.Net.CookieCollection();
				}
				return _cookies;
			}
			set
			{
				_cookies = value;
			}
		}

		public global::UnityWebSocketSharp.Net.WebHeaderCollection Headers
		{
			get
			{
				if (_headers == null)
				{
					_headers = new global::UnityWebSocketSharp.Net.WebHeaderCollection(global::UnityWebSocketSharp.Net.HttpHeaderType.Response, internallyUsed: false);
				}
				return _headers;
			}
			set
			{
				if (value == null)
				{
					_headers = null;
					return;
				}
				if (value.State != global::UnityWebSocketSharp.Net.HttpHeaderType.Response)
				{
					throw new global::System.InvalidOperationException("The value is not valid for a response.");
				}
				_headers = value;
			}
		}

		public bool KeepAlive
		{
			get
			{
				return _keepAlive;
			}
			set
			{
				if (_disposed)
				{
					throw new global::System.ObjectDisposedException(GetType().ToString());
				}
				if (_headersSent)
				{
					throw new global::System.InvalidOperationException("The response is already being sent.");
				}
				_keepAlive = value;
			}
		}

		public global::System.IO.Stream OutputStream
		{
			get
			{
				if (_disposed)
				{
					throw new global::System.ObjectDisposedException(GetType().ToString());
				}
				if (_outputStream == null)
				{
					_outputStream = _context.Connection.GetResponseStream();
				}
				return _outputStream;
			}
		}

		public global::System.Version ProtocolVersion => _version;

		public string RedirectLocation
		{
			get
			{
				if (!(_redirectLocation != null))
				{
					return null;
				}
				return _redirectLocation.OriginalString;
			}
			set
			{
				if (_disposed)
				{
					throw new global::System.ObjectDisposedException(GetType().ToString());
				}
				if (_headersSent)
				{
					throw new global::System.InvalidOperationException("The response is already being sent.");
				}
				if (value == null)
				{
					_redirectLocation = null;
					return;
				}
				if (value.Length == 0)
				{
					throw new global::System.ArgumentException("An empty string.", "value");
				}
				if (!global::System.Uri.TryCreate(value, global::System.UriKind.Absolute, out var result))
				{
					throw new global::System.ArgumentException("Not an absolute URL.", "value");
				}
				_redirectLocation = result;
			}
		}

		public bool SendChunked
		{
			get
			{
				return _sendChunked;
			}
			set
			{
				if (_disposed)
				{
					throw new global::System.ObjectDisposedException(GetType().ToString());
				}
				if (_headersSent)
				{
					throw new global::System.InvalidOperationException("The response is already being sent.");
				}
				_sendChunked = value;
			}
		}

		public int StatusCode
		{
			get
			{
				return _statusCode;
			}
			set
			{
				if (_disposed)
				{
					throw new global::System.ObjectDisposedException(GetType().ToString());
				}
				if (_headersSent)
				{
					throw new global::System.InvalidOperationException("The response is already being sent.");
				}
				if (value < 100 || value > 999)
				{
					throw new global::System.Net.ProtocolViolationException("A value is not between 100 and 999 inclusive.");
				}
				_statusCode = value;
				_statusDescription = value.GetStatusDescription();
			}
		}

		public string StatusDescription
		{
			get
			{
				return _statusDescription;
			}
			set
			{
				if (_disposed)
				{
					throw new global::System.ObjectDisposedException(GetType().ToString());
				}
				if (_headersSent)
				{
					throw new global::System.InvalidOperationException("The response is already being sent.");
				}
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				if (value.Length == 0)
				{
					_statusDescription = _statusCode.GetStatusDescription();
					return;
				}
				if (!isValidForStatusDescription(value))
				{
					throw new global::System.ArgumentException("It contains an invalid character.", "value");
				}
				_statusDescription = value;
			}
		}

		internal HttpListenerResponse(global::UnityWebSocketSharp.Net.HttpListenerContext context)
		{
			_context = context;
			_keepAlive = true;
			_statusCode = 200;
			_statusDescription = "OK";
			_version = global::UnityWebSocketSharp.Net.HttpVersion.Version11;
		}

		private bool canSetCookie(global::UnityWebSocketSharp.Net.Cookie cookie)
		{
			global::System.Collections.Generic.List<global::UnityWebSocketSharp.Net.Cookie> list = findCookie(cookie).ToList();
			if (list.Count == 0)
			{
				return true;
			}
			int version = cookie.Version;
			foreach (global::UnityWebSocketSharp.Net.Cookie item in list)
			{
				if (item.Version == version)
				{
					return true;
				}
			}
			return false;
		}

		private void close(bool force)
		{
			_disposed = true;
			_context.Connection.Close(force);
		}

		private void close(byte[] responseEntity, int bufferLength, bool willBlock)
		{
			global::System.IO.Stream outputStream = OutputStream;
			if (willBlock)
			{
				outputStream.WriteBytes(responseEntity, bufferLength);
				close(force: false);
			}
			else
			{
				outputStream.WriteBytesAsync(responseEntity, bufferLength, delegate
				{
					close(force: false);
				}, null);
			}
		}

		private static string createContentTypeHeaderText(string value, global::System.Text.Encoding encoding)
		{
			if (value.IndexOf("charset=", global::System.StringComparison.Ordinal) > -1)
			{
				return value;
			}
			if (encoding == null)
			{
				return value;
			}
			return $"{value}; charset={encoding.WebName}";
		}

		private global::System.Collections.Generic.IEnumerable<global::UnityWebSocketSharp.Net.Cookie> findCookie(global::UnityWebSocketSharp.Net.Cookie cookie)
		{
			if (_cookies == null || _cookies.Count == 0)
			{
				yield break;
			}
			foreach (global::UnityWebSocketSharp.Net.Cookie cookie2 in _cookies)
			{
				if (cookie2.EqualsWithoutValueAndVersion(cookie))
				{
					yield return cookie2;
				}
			}
		}

		private static bool isValidForContentType(string value)
		{
			foreach (char c in value)
			{
				if (c < ' ')
				{
					return false;
				}
				if (c > '~')
				{
					return false;
				}
				if ("()<>@:\\[]?{}".IndexOf(c) > -1)
				{
					return false;
				}
			}
			return true;
		}

		private static bool isValidForStatusDescription(string value)
		{
			foreach (char c in value)
			{
				if (c < ' ')
				{
					return false;
				}
				if (c > '~')
				{
					return false;
				}
			}
			return true;
		}

		public void Abort()
		{
			if (!_disposed)
			{
				close(force: true);
			}
		}

		public void AppendCookie(global::UnityWebSocketSharp.Net.Cookie cookie)
		{
			Cookies.Add(cookie);
		}

		public void AppendHeader(string name, string value)
		{
			Headers.Add(name, value);
		}

		public void Close()
		{
			if (!_disposed)
			{
				close(force: false);
			}
		}

		public void Close(byte[] responseEntity, bool willBlock)
		{
			if (_disposed)
			{
				throw new global::System.ObjectDisposedException(GetType().ToString());
			}
			if (responseEntity == null)
			{
				throw new global::System.ArgumentNullException("responseEntity");
			}
			long num = responseEntity.LongLength;
			if (num > int.MaxValue)
			{
				close(responseEntity, 1024, willBlock);
				return;
			}
			global::System.IO.Stream stream = OutputStream;
			if (willBlock)
			{
				stream.Write(responseEntity, 0, (int)num);
				close(force: false);
				return;
			}
			stream.BeginWrite(responseEntity, 0, (int)num, delegate(global::System.IAsyncResult ar)
			{
				stream.EndWrite(ar);
				close(force: false);
			}, null);
		}

		public void CopyFrom(global::UnityWebSocketSharp.Net.HttpListenerResponse templateResponse)
		{
			if (templateResponse == null)
			{
				throw new global::System.ArgumentNullException("templateResponse");
			}
			global::UnityWebSocketSharp.Net.WebHeaderCollection headers = templateResponse._headers;
			if (headers != null)
			{
				if (_headers != null)
				{
					_headers.Clear();
				}
				Headers.Add(headers);
			}
			else
			{
				_headers = null;
			}
			_contentLength = templateResponse._contentLength;
			_statusCode = templateResponse._statusCode;
			_statusDescription = templateResponse._statusDescription;
			_keepAlive = templateResponse._keepAlive;
			_version = templateResponse._version;
		}

		public void Redirect(string url)
		{
			if (_disposed)
			{
				throw new global::System.ObjectDisposedException(GetType().ToString());
			}
			if (_headersSent)
			{
				throw new global::System.InvalidOperationException("The response is already being sent.");
			}
			if (url == null)
			{
				throw new global::System.ArgumentNullException("url");
			}
			if (url.Length == 0)
			{
				throw new global::System.ArgumentException("An empty string.", "url");
			}
			if (!global::System.Uri.TryCreate(url, global::System.UriKind.Absolute, out var result))
			{
				throw new global::System.ArgumentException("Not an absolute URL.", "url");
			}
			_redirectLocation = result;
			_statusCode = 302;
			_statusDescription = "Found";
		}

		public void SetCookie(global::UnityWebSocketSharp.Net.Cookie cookie)
		{
			if (cookie == null)
			{
				throw new global::System.ArgumentNullException("cookie");
			}
			if (!canSetCookie(cookie))
			{
				throw new global::System.ArgumentException("It cannot be updated.", "cookie");
			}
			Cookies.Add(cookie);
		}

		public void SetHeader(string name, string value)
		{
			Headers.Set(name, value);
		}

		void global::System.IDisposable.Dispose()
		{
			if (!_disposed)
			{
				close(force: true);
			}
		}
	}
}
