namespace WebSocketSharp.Net
{
	[global::System.Serializable]
	[global::System.Runtime.InteropServices.ComVisible(true)]
	public class WebHeaderCollection : global::System.Collections.Specialized.NameValueCollection, global::System.Runtime.Serialization.ISerializable
	{
		private static readonly global::System.Collections.Generic.Dictionary<string, global::WebSocketSharp.Net.HttpHeaderInfo> _headers;

		private bool _internallyUsed;

		private global::WebSocketSharp.Net.HttpHeaderType _state;

		internal global::WebSocketSharp.Net.HttpHeaderType State => _state;

		public override string[] AllKeys => base.AllKeys;

		public override int Count => base.Count;

		public string this[global::WebSocketSharp.Net.HttpRequestHeader header]
		{
			get
			{
				string key = header.ToString();
				string headerName = getHeaderName(key);
				return Get(headerName);
			}
			set
			{
				Add(header, value);
			}
		}

		public string this[global::WebSocketSharp.Net.HttpResponseHeader header]
		{
			get
			{
				string key = header.ToString();
				string headerName = getHeaderName(key);
				return Get(headerName);
			}
			set
			{
				Add(header, value);
			}
		}

		public override global::System.Collections.Specialized.NameObjectCollectionBase.KeysCollection Keys => base.Keys;

		static WebHeaderCollection()
		{
			_headers = new global::System.Collections.Generic.Dictionary<string, global::WebSocketSharp.Net.HttpHeaderInfo>(global::System.StringComparer.InvariantCultureIgnoreCase)
			{
				{
					"Accept",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Accept", global::WebSocketSharp.Net.HttpHeaderType.Request | global::WebSocketSharp.Net.HttpHeaderType.Restricted | global::WebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"AcceptCharset",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Accept-Charset", global::WebSocketSharp.Net.HttpHeaderType.Request | global::WebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"AcceptEncoding",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Accept-Encoding", global::WebSocketSharp.Net.HttpHeaderType.Request | global::WebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"AcceptLanguage",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Accept-Language", global::WebSocketSharp.Net.HttpHeaderType.Request | global::WebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"AcceptRanges",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Accept-Ranges", global::WebSocketSharp.Net.HttpHeaderType.Response | global::WebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"Age",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Age", global::WebSocketSharp.Net.HttpHeaderType.Response)
				},
				{
					"Allow",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Allow", global::WebSocketSharp.Net.HttpHeaderType.Request | global::WebSocketSharp.Net.HttpHeaderType.Response | global::WebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"Authorization",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Authorization", global::WebSocketSharp.Net.HttpHeaderType.Request | global::WebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"CacheControl",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Cache-Control", global::WebSocketSharp.Net.HttpHeaderType.Request | global::WebSocketSharp.Net.HttpHeaderType.Response | global::WebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"Connection",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Connection", global::WebSocketSharp.Net.HttpHeaderType.Request | global::WebSocketSharp.Net.HttpHeaderType.Response | global::WebSocketSharp.Net.HttpHeaderType.Restricted | global::WebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"ContentEncoding",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Content-Encoding", global::WebSocketSharp.Net.HttpHeaderType.Request | global::WebSocketSharp.Net.HttpHeaderType.Response | global::WebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"ContentLanguage",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Content-Language", global::WebSocketSharp.Net.HttpHeaderType.Request | global::WebSocketSharp.Net.HttpHeaderType.Response | global::WebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"ContentLength",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Content-Length", global::WebSocketSharp.Net.HttpHeaderType.Request | global::WebSocketSharp.Net.HttpHeaderType.Response | global::WebSocketSharp.Net.HttpHeaderType.Restricted)
				},
				{
					"ContentLocation",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Content-Location", global::WebSocketSharp.Net.HttpHeaderType.Request | global::WebSocketSharp.Net.HttpHeaderType.Response)
				},
				{
					"ContentMd5",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Content-MD5", global::WebSocketSharp.Net.HttpHeaderType.Request | global::WebSocketSharp.Net.HttpHeaderType.Response)
				},
				{
					"ContentRange",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Content-Range", global::WebSocketSharp.Net.HttpHeaderType.Request | global::WebSocketSharp.Net.HttpHeaderType.Response)
				},
				{
					"ContentType",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Content-Type", global::WebSocketSharp.Net.HttpHeaderType.Request | global::WebSocketSharp.Net.HttpHeaderType.Response | global::WebSocketSharp.Net.HttpHeaderType.Restricted)
				},
				{
					"Cookie",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Cookie", global::WebSocketSharp.Net.HttpHeaderType.Request)
				},
				{
					"Cookie2",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Cookie2", global::WebSocketSharp.Net.HttpHeaderType.Request)
				},
				{
					"Date",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Date", global::WebSocketSharp.Net.HttpHeaderType.Request | global::WebSocketSharp.Net.HttpHeaderType.Response | global::WebSocketSharp.Net.HttpHeaderType.Restricted)
				},
				{
					"Expect",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Expect", global::WebSocketSharp.Net.HttpHeaderType.Request | global::WebSocketSharp.Net.HttpHeaderType.Restricted | global::WebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"Expires",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Expires", global::WebSocketSharp.Net.HttpHeaderType.Request | global::WebSocketSharp.Net.HttpHeaderType.Response)
				},
				{
					"ETag",
					new global::WebSocketSharp.Net.HttpHeaderInfo("ETag", global::WebSocketSharp.Net.HttpHeaderType.Response)
				},
				{
					"From",
					new global::WebSocketSharp.Net.HttpHeaderInfo("From", global::WebSocketSharp.Net.HttpHeaderType.Request)
				},
				{
					"Host",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Host", global::WebSocketSharp.Net.HttpHeaderType.Request | global::WebSocketSharp.Net.HttpHeaderType.Restricted)
				},
				{
					"IfMatch",
					new global::WebSocketSharp.Net.HttpHeaderInfo("If-Match", global::WebSocketSharp.Net.HttpHeaderType.Request | global::WebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"IfModifiedSince",
					new global::WebSocketSharp.Net.HttpHeaderInfo("If-Modified-Since", global::WebSocketSharp.Net.HttpHeaderType.Request | global::WebSocketSharp.Net.HttpHeaderType.Restricted)
				},
				{
					"IfNoneMatch",
					new global::WebSocketSharp.Net.HttpHeaderInfo("If-None-Match", global::WebSocketSharp.Net.HttpHeaderType.Request | global::WebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"IfRange",
					new global::WebSocketSharp.Net.HttpHeaderInfo("If-Range", global::WebSocketSharp.Net.HttpHeaderType.Request)
				},
				{
					"IfUnmodifiedSince",
					new global::WebSocketSharp.Net.HttpHeaderInfo("If-Unmodified-Since", global::WebSocketSharp.Net.HttpHeaderType.Request)
				},
				{
					"KeepAlive",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Keep-Alive", global::WebSocketSharp.Net.HttpHeaderType.Request | global::WebSocketSharp.Net.HttpHeaderType.Response | global::WebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"LastModified",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Last-Modified", global::WebSocketSharp.Net.HttpHeaderType.Request | global::WebSocketSharp.Net.HttpHeaderType.Response)
				},
				{
					"Location",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Location", global::WebSocketSharp.Net.HttpHeaderType.Response)
				},
				{
					"MaxForwards",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Max-Forwards", global::WebSocketSharp.Net.HttpHeaderType.Request)
				},
				{
					"Pragma",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Pragma", global::WebSocketSharp.Net.HttpHeaderType.Request | global::WebSocketSharp.Net.HttpHeaderType.Response)
				},
				{
					"ProxyAuthenticate",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Proxy-Authenticate", global::WebSocketSharp.Net.HttpHeaderType.Response | global::WebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"ProxyAuthorization",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Proxy-Authorization", global::WebSocketSharp.Net.HttpHeaderType.Request)
				},
				{
					"ProxyConnection",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Proxy-Connection", global::WebSocketSharp.Net.HttpHeaderType.Request | global::WebSocketSharp.Net.HttpHeaderType.Response | global::WebSocketSharp.Net.HttpHeaderType.Restricted)
				},
				{
					"Public",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Public", global::WebSocketSharp.Net.HttpHeaderType.Response | global::WebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"Range",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Range", global::WebSocketSharp.Net.HttpHeaderType.Request | global::WebSocketSharp.Net.HttpHeaderType.Restricted | global::WebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"Referer",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Referer", global::WebSocketSharp.Net.HttpHeaderType.Request | global::WebSocketSharp.Net.HttpHeaderType.Restricted)
				},
				{
					"RetryAfter",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Retry-After", global::WebSocketSharp.Net.HttpHeaderType.Response)
				},
				{
					"SecWebSocketAccept",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Sec-WebSocket-Accept", global::WebSocketSharp.Net.HttpHeaderType.Response | global::WebSocketSharp.Net.HttpHeaderType.Restricted)
				},
				{
					"SecWebSocketExtensions",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Sec-WebSocket-Extensions", global::WebSocketSharp.Net.HttpHeaderType.Request | global::WebSocketSharp.Net.HttpHeaderType.Response | global::WebSocketSharp.Net.HttpHeaderType.Restricted | global::WebSocketSharp.Net.HttpHeaderType.MultiValueInRequest)
				},
				{
					"SecWebSocketKey",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Sec-WebSocket-Key", global::WebSocketSharp.Net.HttpHeaderType.Request | global::WebSocketSharp.Net.HttpHeaderType.Restricted)
				},
				{
					"SecWebSocketProtocol",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Sec-WebSocket-Protocol", global::WebSocketSharp.Net.HttpHeaderType.Request | global::WebSocketSharp.Net.HttpHeaderType.Response | global::WebSocketSharp.Net.HttpHeaderType.MultiValueInRequest)
				},
				{
					"SecWebSocketVersion",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Sec-WebSocket-Version", global::WebSocketSharp.Net.HttpHeaderType.Request | global::WebSocketSharp.Net.HttpHeaderType.Response | global::WebSocketSharp.Net.HttpHeaderType.Restricted | global::WebSocketSharp.Net.HttpHeaderType.MultiValueInResponse)
				},
				{
					"Server",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Server", global::WebSocketSharp.Net.HttpHeaderType.Response)
				},
				{
					"SetCookie",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Set-Cookie", global::WebSocketSharp.Net.HttpHeaderType.Response | global::WebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"SetCookie2",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Set-Cookie2", global::WebSocketSharp.Net.HttpHeaderType.Response | global::WebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"Te",
					new global::WebSocketSharp.Net.HttpHeaderInfo("TE", global::WebSocketSharp.Net.HttpHeaderType.Request)
				},
				{
					"Trailer",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Trailer", global::WebSocketSharp.Net.HttpHeaderType.Request | global::WebSocketSharp.Net.HttpHeaderType.Response)
				},
				{
					"TransferEncoding",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Transfer-Encoding", global::WebSocketSharp.Net.HttpHeaderType.Request | global::WebSocketSharp.Net.HttpHeaderType.Response | global::WebSocketSharp.Net.HttpHeaderType.Restricted | global::WebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"Translate",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Translate", global::WebSocketSharp.Net.HttpHeaderType.Request)
				},
				{
					"Upgrade",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Upgrade", global::WebSocketSharp.Net.HttpHeaderType.Request | global::WebSocketSharp.Net.HttpHeaderType.Response | global::WebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"UserAgent",
					new global::WebSocketSharp.Net.HttpHeaderInfo("User-Agent", global::WebSocketSharp.Net.HttpHeaderType.Request | global::WebSocketSharp.Net.HttpHeaderType.Restricted)
				},
				{
					"Vary",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Vary", global::WebSocketSharp.Net.HttpHeaderType.Response | global::WebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"Via",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Via", global::WebSocketSharp.Net.HttpHeaderType.Request | global::WebSocketSharp.Net.HttpHeaderType.Response | global::WebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"Warning",
					new global::WebSocketSharp.Net.HttpHeaderInfo("Warning", global::WebSocketSharp.Net.HttpHeaderType.Request | global::WebSocketSharp.Net.HttpHeaderType.Response | global::WebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"WwwAuthenticate",
					new global::WebSocketSharp.Net.HttpHeaderInfo("WWW-Authenticate", global::WebSocketSharp.Net.HttpHeaderType.Response | global::WebSocketSharp.Net.HttpHeaderType.Restricted | global::WebSocketSharp.Net.HttpHeaderType.MultiValue)
				}
			};
		}

		internal WebHeaderCollection(global::WebSocketSharp.Net.HttpHeaderType state, bool internallyUsed)
		{
			_state = state;
			_internallyUsed = internallyUsed;
		}

		protected WebHeaderCollection(global::System.Runtime.Serialization.SerializationInfo serializationInfo, global::System.Runtime.Serialization.StreamingContext streamingContext)
		{
			if (serializationInfo == null)
			{
				throw new global::System.ArgumentNullException("serializationInfo");
			}
			try
			{
				_internallyUsed = serializationInfo.GetBoolean("InternallyUsed");
				_state = (global::WebSocketSharp.Net.HttpHeaderType)serializationInfo.GetInt32("State");
				int @int = serializationInfo.GetInt32("Count");
				for (int i = 0; i < @int; i++)
				{
					base.Add(serializationInfo.GetString(i.ToString()), serializationInfo.GetString((@int + i).ToString()));
				}
			}
			catch (global::System.Runtime.Serialization.SerializationException ex)
			{
				throw new global::System.ArgumentException(ex.Message, "serializationInfo", ex);
			}
		}

		public WebHeaderCollection()
		{
		}

		private void add(string name, string value, global::WebSocketSharp.Net.HttpHeaderType headerType)
		{
			base.Add(name, value);
			if (_state == global::WebSocketSharp.Net.HttpHeaderType.Unspecified && headerType != global::WebSocketSharp.Net.HttpHeaderType.Unspecified)
			{
				_state = headerType;
			}
		}

		private void checkAllowed(global::WebSocketSharp.Net.HttpHeaderType headerType)
		{
			if (_state == global::WebSocketSharp.Net.HttpHeaderType.Unspecified || headerType == global::WebSocketSharp.Net.HttpHeaderType.Unspecified || headerType == _state)
			{
				return;
			}
			string message = "This instance does not allow the header.";
			throw new global::System.InvalidOperationException(message);
		}

		private static string checkName(string name, string paramName)
		{
			if (name == null)
			{
				string message = "The name is null.";
				throw new global::System.ArgumentNullException(paramName, message);
			}
			if (name.Length == 0)
			{
				string message2 = "The name is an empty string.";
				throw new global::System.ArgumentException(message2, paramName);
			}
			name = name.Trim();
			if (name.Length == 0)
			{
				string message3 = "The name is a string of spaces.";
				throw new global::System.ArgumentException(message3, paramName);
			}
			if (!name.IsToken())
			{
				string message4 = "The name contains an invalid character.";
				throw new global::System.ArgumentException(message4, paramName);
			}
			return name;
		}

		private void checkRestricted(string name, global::WebSocketSharp.Net.HttpHeaderType headerType)
		{
			if (!_internallyUsed)
			{
				bool response = headerType == global::WebSocketSharp.Net.HttpHeaderType.Response;
				if (isRestricted(name, response))
				{
					string message = "The header is a restricted header.";
					throw new global::System.ArgumentException(message);
				}
			}
		}

		private static string checkValue(string value, string paramName)
		{
			if (value == null)
			{
				return string.Empty;
			}
			value = value.Trim();
			int length = value.Length;
			if (length == 0)
			{
				return value;
			}
			if (length > 65535)
			{
				string message = "The length of the value is greater than 65,535 characters.";
				throw new global::System.ArgumentOutOfRangeException(paramName, message);
			}
			if (!value.IsText())
			{
				string message2 = "The value contains an invalid character.";
				throw new global::System.ArgumentException(message2, paramName);
			}
			return value;
		}

		private static global::WebSocketSharp.Net.HttpHeaderInfo getHeaderInfo(string name)
		{
			global::System.StringComparison comparisonType = global::System.StringComparison.InvariantCultureIgnoreCase;
			foreach (global::WebSocketSharp.Net.HttpHeaderInfo value in _headers.Values)
			{
				if (value.HeaderName.Equals(name, comparisonType))
				{
					return value;
				}
			}
			return null;
		}

		private static string getHeaderName(string key)
		{
			global::WebSocketSharp.Net.HttpHeaderInfo value;
			return _headers.TryGetValue(key, out value) ? value.HeaderName : null;
		}

		private static global::WebSocketSharp.Net.HttpHeaderType getHeaderType(string name)
		{
			global::WebSocketSharp.Net.HttpHeaderInfo headerInfo = getHeaderInfo(name);
			if (headerInfo == null)
			{
				return global::WebSocketSharp.Net.HttpHeaderType.Unspecified;
			}
			if (headerInfo.IsRequest)
			{
				return (!headerInfo.IsResponse) ? global::WebSocketSharp.Net.HttpHeaderType.Request : global::WebSocketSharp.Net.HttpHeaderType.Unspecified;
			}
			return headerInfo.IsResponse ? global::WebSocketSharp.Net.HttpHeaderType.Response : global::WebSocketSharp.Net.HttpHeaderType.Unspecified;
		}

		private static bool isMultiValue(string name, bool response)
		{
			return getHeaderInfo(name)?.IsMultiValue(response) ?? false;
		}

		private static bool isRestricted(string name, bool response)
		{
			return getHeaderInfo(name)?.IsRestricted(response) ?? false;
		}

		private void set(string name, string value, global::WebSocketSharp.Net.HttpHeaderType headerType)
		{
			base.Set(name, value);
			if (_state == global::WebSocketSharp.Net.HttpHeaderType.Unspecified && headerType != global::WebSocketSharp.Net.HttpHeaderType.Unspecified)
			{
				_state = headerType;
			}
		}

		internal void InternalRemove(string name)
		{
			base.Remove(name);
		}

		internal void InternalSet(string header, bool response)
		{
			int num = header.IndexOf(':');
			if (num == -1)
			{
				string message = "It does not contain a colon character.";
				throw new global::System.ArgumentException(message, "header");
			}
			string name = header.Substring(0, num);
			string value = ((num < header.Length - 1) ? header.Substring(num + 1) : string.Empty);
			name = checkName(name, "header");
			value = checkValue(value, "header");
			if (isMultiValue(name, response))
			{
				base.Add(name, value);
			}
			else
			{
				base.Set(name, value);
			}
		}

		internal void InternalSet(string name, string value, bool response)
		{
			value = checkValue(value, "value");
			if (isMultiValue(name, response))
			{
				base.Add(name, value);
			}
			else
			{
				base.Set(name, value);
			}
		}

		internal string ToStringMultiValue(bool response)
		{
			int count = Count;
			if (count == 0)
			{
				return "\r\n";
			}
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			for (int i = 0; i < count; i++)
			{
				string key = GetKey(i);
				if (isMultiValue(key, response))
				{
					string[] values = GetValues(i);
					foreach (string arg in values)
					{
						stringBuilder.AppendFormat("{0}: {1}\r\n", key, arg);
					}
				}
				else
				{
					stringBuilder.AppendFormat("{0}: {1}\r\n", key, Get(i));
				}
			}
			stringBuilder.Append("\r\n");
			return stringBuilder.ToString();
		}

		protected void AddWithoutValidate(string headerName, string headerValue)
		{
			headerName = checkName(headerName, "headerName");
			headerValue = checkValue(headerValue, "headerValue");
			global::WebSocketSharp.Net.HttpHeaderType headerType = getHeaderType(headerName);
			checkAllowed(headerType);
			add(headerName, headerValue, headerType);
		}

		public void Add(string header)
		{
			if (header == null)
			{
				throw new global::System.ArgumentNullException("header");
			}
			int length = header.Length;
			if (length == 0)
			{
				string message = "An empty string.";
				throw new global::System.ArgumentException(message, "header");
			}
			int num = header.IndexOf(':');
			if (num == -1)
			{
				string message2 = "It does not contain a colon character.";
				throw new global::System.ArgumentException(message2, "header");
			}
			string name = header.Substring(0, num);
			string value = ((num < length - 1) ? header.Substring(num + 1) : string.Empty);
			name = checkName(name, "header");
			value = checkValue(value, "header");
			global::WebSocketSharp.Net.HttpHeaderType headerType = getHeaderType(name);
			checkRestricted(name, headerType);
			checkAllowed(headerType);
			add(name, value, headerType);
		}

		public void Add(global::WebSocketSharp.Net.HttpRequestHeader header, string value)
		{
			value = checkValue(value, "value");
			string key = header.ToString();
			string headerName = getHeaderName(key);
			checkRestricted(headerName, global::WebSocketSharp.Net.HttpHeaderType.Request);
			checkAllowed(global::WebSocketSharp.Net.HttpHeaderType.Request);
			add(headerName, value, global::WebSocketSharp.Net.HttpHeaderType.Request);
		}

		public void Add(global::WebSocketSharp.Net.HttpResponseHeader header, string value)
		{
			value = checkValue(value, "value");
			string key = header.ToString();
			string headerName = getHeaderName(key);
			checkRestricted(headerName, global::WebSocketSharp.Net.HttpHeaderType.Response);
			checkAllowed(global::WebSocketSharp.Net.HttpHeaderType.Response);
			add(headerName, value, global::WebSocketSharp.Net.HttpHeaderType.Response);
		}

		public override void Add(string name, string value)
		{
			name = checkName(name, "name");
			value = checkValue(value, "value");
			global::WebSocketSharp.Net.HttpHeaderType headerType = getHeaderType(name);
			checkRestricted(name, headerType);
			checkAllowed(headerType);
			add(name, value, headerType);
		}

		public override void Clear()
		{
			base.Clear();
			_state = global::WebSocketSharp.Net.HttpHeaderType.Unspecified;
		}

		public override string Get(int index)
		{
			return base.Get(index);
		}

		public override string Get(string name)
		{
			return base.Get(name);
		}

		public override global::System.Collections.IEnumerator GetEnumerator()
		{
			return base.GetEnumerator();
		}

		public override string GetKey(int index)
		{
			return base.GetKey(index);
		}

		public override string[] GetValues(int index)
		{
			string[] values = base.GetValues(index);
			return (values != null && values.Length != 0) ? values : null;
		}

		public override string[] GetValues(string name)
		{
			string[] values = base.GetValues(name);
			return (values != null && values.Length != 0) ? values : null;
		}

		[global::System.Security.Permissions.SecurityPermission(global::System.Security.Permissions.SecurityAction.LinkDemand, Flags = global::System.Security.Permissions.SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(global::System.Runtime.Serialization.SerializationInfo serializationInfo, global::System.Runtime.Serialization.StreamingContext streamingContext)
		{
			if (serializationInfo == null)
			{
				throw new global::System.ArgumentNullException("serializationInfo");
			}
			serializationInfo.AddValue("InternallyUsed", _internallyUsed);
			serializationInfo.AddValue("State", (int)_state);
			int count = Count;
			serializationInfo.AddValue("Count", count);
			for (int i = 0; i < count; i++)
			{
				serializationInfo.AddValue(i.ToString(), GetKey(i));
				serializationInfo.AddValue((count + i).ToString(), Get(i));
			}
		}

		public static bool IsRestricted(string headerName)
		{
			return IsRestricted(headerName, response: false);
		}

		public static bool IsRestricted(string headerName, bool response)
		{
			headerName = checkName(headerName, "headerName");
			return isRestricted(headerName, response);
		}

		public override void OnDeserialization(object sender)
		{
		}

		public void Remove(global::WebSocketSharp.Net.HttpRequestHeader header)
		{
			string key = header.ToString();
			string headerName = getHeaderName(key);
			checkRestricted(headerName, global::WebSocketSharp.Net.HttpHeaderType.Request);
			checkAllowed(global::WebSocketSharp.Net.HttpHeaderType.Request);
			base.Remove(headerName);
		}

		public void Remove(global::WebSocketSharp.Net.HttpResponseHeader header)
		{
			string key = header.ToString();
			string headerName = getHeaderName(key);
			checkRestricted(headerName, global::WebSocketSharp.Net.HttpHeaderType.Response);
			checkAllowed(global::WebSocketSharp.Net.HttpHeaderType.Response);
			base.Remove(headerName);
		}

		public override void Remove(string name)
		{
			name = checkName(name, "name");
			global::WebSocketSharp.Net.HttpHeaderType headerType = getHeaderType(name);
			checkRestricted(name, headerType);
			checkAllowed(headerType);
			base.Remove(name);
		}

		public void Set(global::WebSocketSharp.Net.HttpRequestHeader header, string value)
		{
			value = checkValue(value, "value");
			string key = header.ToString();
			string headerName = getHeaderName(key);
			checkRestricted(headerName, global::WebSocketSharp.Net.HttpHeaderType.Request);
			checkAllowed(global::WebSocketSharp.Net.HttpHeaderType.Request);
			set(headerName, value, global::WebSocketSharp.Net.HttpHeaderType.Request);
		}

		public void Set(global::WebSocketSharp.Net.HttpResponseHeader header, string value)
		{
			value = checkValue(value, "value");
			string key = header.ToString();
			string headerName = getHeaderName(key);
			checkRestricted(headerName, global::WebSocketSharp.Net.HttpHeaderType.Response);
			checkAllowed(global::WebSocketSharp.Net.HttpHeaderType.Response);
			set(headerName, value, global::WebSocketSharp.Net.HttpHeaderType.Response);
		}

		public override void Set(string name, string value)
		{
			name = checkName(name, "name");
			value = checkValue(value, "value");
			global::WebSocketSharp.Net.HttpHeaderType headerType = getHeaderType(name);
			checkRestricted(name, headerType);
			checkAllowed(headerType);
			set(name, value, headerType);
		}

		public byte[] ToByteArray()
		{
			return global::System.Text.Encoding.UTF8.GetBytes(ToString());
		}

		public override string ToString()
		{
			int count = Count;
			if (count == 0)
			{
				return "\r\n";
			}
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			for (int i = 0; i < count; i++)
			{
				stringBuilder.AppendFormat("{0}: {1}\r\n", GetKey(i), Get(i));
			}
			stringBuilder.Append("\r\n");
			return stringBuilder.ToString();
		}

		[global::System.Security.Permissions.SecurityPermission(global::System.Security.Permissions.SecurityAction.LinkDemand, Flags = global::System.Security.Permissions.SecurityPermissionFlag.SerializationFormatter, SerializationFormatter = true)]
		void global::System.Runtime.Serialization.ISerializable.GetObjectData(global::System.Runtime.Serialization.SerializationInfo serializationInfo, global::System.Runtime.Serialization.StreamingContext streamingContext)
		{
			GetObjectData(serializationInfo, streamingContext);
		}
	}
}
