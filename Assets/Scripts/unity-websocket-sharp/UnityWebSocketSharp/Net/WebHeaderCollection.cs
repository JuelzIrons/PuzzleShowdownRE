namespace UnityWebSocketSharp.Net
{
	[global::System.Serializable]
	[global::System.Runtime.InteropServices.ComVisible(true)]
	internal class WebHeaderCollection : global::System.Collections.Specialized.NameValueCollection, global::System.Runtime.Serialization.ISerializable
	{
		private static readonly global::System.Collections.Generic.Dictionary<string, global::UnityWebSocketSharp.Net.HttpHeaderInfo> _headers;

		private bool _internallyUsed;

		private global::UnityWebSocketSharp.Net.HttpHeaderType _state;

		internal global::UnityWebSocketSharp.Net.HttpHeaderType State => _state;

		public override string[] AllKeys => base.AllKeys;

		public override int Count => base.Count;

		public string this[global::UnityWebSocketSharp.Net.HttpRequestHeader header]
		{
			get
			{
				string headerName = getHeaderName(header.ToString());
				return Get(headerName);
			}
			set
			{
				Add(header, value);
			}
		}

		public string this[global::UnityWebSocketSharp.Net.HttpResponseHeader header]
		{
			get
			{
				string headerName = getHeaderName(header.ToString());
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
			_headers = new global::System.Collections.Generic.Dictionary<string, global::UnityWebSocketSharp.Net.HttpHeaderInfo>(global::System.StringComparer.InvariantCultureIgnoreCase)
			{
				{
					"Accept",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Accept", global::UnityWebSocketSharp.Net.HttpHeaderType.Request | global::UnityWebSocketSharp.Net.HttpHeaderType.Restricted | global::UnityWebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"AcceptCharset",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Accept-Charset", global::UnityWebSocketSharp.Net.HttpHeaderType.Request | global::UnityWebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"AcceptEncoding",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Accept-Encoding", global::UnityWebSocketSharp.Net.HttpHeaderType.Request | global::UnityWebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"AcceptLanguage",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Accept-Language", global::UnityWebSocketSharp.Net.HttpHeaderType.Request | global::UnityWebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"AcceptRanges",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Accept-Ranges", global::UnityWebSocketSharp.Net.HttpHeaderType.Response | global::UnityWebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"Age",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Age", global::UnityWebSocketSharp.Net.HttpHeaderType.Response)
				},
				{
					"Allow",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Allow", global::UnityWebSocketSharp.Net.HttpHeaderType.Request | global::UnityWebSocketSharp.Net.HttpHeaderType.Response | global::UnityWebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"Authorization",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Authorization", global::UnityWebSocketSharp.Net.HttpHeaderType.Request | global::UnityWebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"CacheControl",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Cache-Control", global::UnityWebSocketSharp.Net.HttpHeaderType.Request | global::UnityWebSocketSharp.Net.HttpHeaderType.Response | global::UnityWebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"Connection",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Connection", global::UnityWebSocketSharp.Net.HttpHeaderType.Request | global::UnityWebSocketSharp.Net.HttpHeaderType.Response | global::UnityWebSocketSharp.Net.HttpHeaderType.Restricted | global::UnityWebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"ContentEncoding",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Content-Encoding", global::UnityWebSocketSharp.Net.HttpHeaderType.Request | global::UnityWebSocketSharp.Net.HttpHeaderType.Response | global::UnityWebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"ContentLanguage",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Content-Language", global::UnityWebSocketSharp.Net.HttpHeaderType.Request | global::UnityWebSocketSharp.Net.HttpHeaderType.Response | global::UnityWebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"ContentLength",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Content-Length", global::UnityWebSocketSharp.Net.HttpHeaderType.Request | global::UnityWebSocketSharp.Net.HttpHeaderType.Response | global::UnityWebSocketSharp.Net.HttpHeaderType.Restricted)
				},
				{
					"ContentLocation",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Content-Location", global::UnityWebSocketSharp.Net.HttpHeaderType.Request | global::UnityWebSocketSharp.Net.HttpHeaderType.Response)
				},
				{
					"ContentMd5",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Content-MD5", global::UnityWebSocketSharp.Net.HttpHeaderType.Request | global::UnityWebSocketSharp.Net.HttpHeaderType.Response)
				},
				{
					"ContentRange",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Content-Range", global::UnityWebSocketSharp.Net.HttpHeaderType.Request | global::UnityWebSocketSharp.Net.HttpHeaderType.Response)
				},
				{
					"ContentType",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Content-Type", global::UnityWebSocketSharp.Net.HttpHeaderType.Request | global::UnityWebSocketSharp.Net.HttpHeaderType.Response | global::UnityWebSocketSharp.Net.HttpHeaderType.Restricted)
				},
				{
					"Cookie",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Cookie", global::UnityWebSocketSharp.Net.HttpHeaderType.Request)
				},
				{
					"Cookie2",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Cookie2", global::UnityWebSocketSharp.Net.HttpHeaderType.Request)
				},
				{
					"Date",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Date", global::UnityWebSocketSharp.Net.HttpHeaderType.Request | global::UnityWebSocketSharp.Net.HttpHeaderType.Response | global::UnityWebSocketSharp.Net.HttpHeaderType.Restricted)
				},
				{
					"Expect",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Expect", global::UnityWebSocketSharp.Net.HttpHeaderType.Request | global::UnityWebSocketSharp.Net.HttpHeaderType.Restricted | global::UnityWebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"Expires",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Expires", global::UnityWebSocketSharp.Net.HttpHeaderType.Request | global::UnityWebSocketSharp.Net.HttpHeaderType.Response)
				},
				{
					"ETag",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("ETag", global::UnityWebSocketSharp.Net.HttpHeaderType.Response)
				},
				{
					"From",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("From", global::UnityWebSocketSharp.Net.HttpHeaderType.Request)
				},
				{
					"Host",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Host", global::UnityWebSocketSharp.Net.HttpHeaderType.Request | global::UnityWebSocketSharp.Net.HttpHeaderType.Restricted)
				},
				{
					"IfMatch",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("If-Match", global::UnityWebSocketSharp.Net.HttpHeaderType.Request | global::UnityWebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"IfModifiedSince",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("If-Modified-Since", global::UnityWebSocketSharp.Net.HttpHeaderType.Request | global::UnityWebSocketSharp.Net.HttpHeaderType.Restricted)
				},
				{
					"IfNoneMatch",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("If-None-Match", global::UnityWebSocketSharp.Net.HttpHeaderType.Request | global::UnityWebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"IfRange",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("If-Range", global::UnityWebSocketSharp.Net.HttpHeaderType.Request)
				},
				{
					"IfUnmodifiedSince",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("If-Unmodified-Since", global::UnityWebSocketSharp.Net.HttpHeaderType.Request)
				},
				{
					"KeepAlive",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Keep-Alive", global::UnityWebSocketSharp.Net.HttpHeaderType.Request | global::UnityWebSocketSharp.Net.HttpHeaderType.Response | global::UnityWebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"LastModified",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Last-Modified", global::UnityWebSocketSharp.Net.HttpHeaderType.Request | global::UnityWebSocketSharp.Net.HttpHeaderType.Response)
				},
				{
					"Location",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Location", global::UnityWebSocketSharp.Net.HttpHeaderType.Response)
				},
				{
					"MaxForwards",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Max-Forwards", global::UnityWebSocketSharp.Net.HttpHeaderType.Request)
				},
				{
					"Pragma",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Pragma", global::UnityWebSocketSharp.Net.HttpHeaderType.Request | global::UnityWebSocketSharp.Net.HttpHeaderType.Response)
				},
				{
					"ProxyAuthenticate",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Proxy-Authenticate", global::UnityWebSocketSharp.Net.HttpHeaderType.Response | global::UnityWebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"ProxyAuthorization",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Proxy-Authorization", global::UnityWebSocketSharp.Net.HttpHeaderType.Request)
				},
				{
					"ProxyConnection",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Proxy-Connection", global::UnityWebSocketSharp.Net.HttpHeaderType.Request | global::UnityWebSocketSharp.Net.HttpHeaderType.Response | global::UnityWebSocketSharp.Net.HttpHeaderType.Restricted)
				},
				{
					"Public",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Public", global::UnityWebSocketSharp.Net.HttpHeaderType.Response | global::UnityWebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"Range",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Range", global::UnityWebSocketSharp.Net.HttpHeaderType.Request | global::UnityWebSocketSharp.Net.HttpHeaderType.Restricted | global::UnityWebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"Referer",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Referer", global::UnityWebSocketSharp.Net.HttpHeaderType.Request | global::UnityWebSocketSharp.Net.HttpHeaderType.Restricted)
				},
				{
					"RetryAfter",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Retry-After", global::UnityWebSocketSharp.Net.HttpHeaderType.Response)
				},
				{
					"SecWebSocketAccept",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Sec-WebSocket-Accept", global::UnityWebSocketSharp.Net.HttpHeaderType.Response | global::UnityWebSocketSharp.Net.HttpHeaderType.Restricted)
				},
				{
					"SecWebSocketExtensions",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Sec-WebSocket-Extensions", global::UnityWebSocketSharp.Net.HttpHeaderType.Request | global::UnityWebSocketSharp.Net.HttpHeaderType.Response | global::UnityWebSocketSharp.Net.HttpHeaderType.Restricted | global::UnityWebSocketSharp.Net.HttpHeaderType.MultiValueInRequest)
				},
				{
					"SecWebSocketKey",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Sec-WebSocket-Key", global::UnityWebSocketSharp.Net.HttpHeaderType.Request | global::UnityWebSocketSharp.Net.HttpHeaderType.Restricted)
				},
				{
					"SecWebSocketProtocol",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Sec-WebSocket-Protocol", global::UnityWebSocketSharp.Net.HttpHeaderType.Request | global::UnityWebSocketSharp.Net.HttpHeaderType.Response | global::UnityWebSocketSharp.Net.HttpHeaderType.MultiValueInRequest)
				},
				{
					"SecWebSocketVersion",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Sec-WebSocket-Version", global::UnityWebSocketSharp.Net.HttpHeaderType.Request | global::UnityWebSocketSharp.Net.HttpHeaderType.Response | global::UnityWebSocketSharp.Net.HttpHeaderType.Restricted | global::UnityWebSocketSharp.Net.HttpHeaderType.MultiValueInResponse)
				},
				{
					"Server",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Server", global::UnityWebSocketSharp.Net.HttpHeaderType.Response)
				},
				{
					"SetCookie",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Set-Cookie", global::UnityWebSocketSharp.Net.HttpHeaderType.Response | global::UnityWebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"SetCookie2",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Set-Cookie2", global::UnityWebSocketSharp.Net.HttpHeaderType.Response | global::UnityWebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"Te",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("TE", global::UnityWebSocketSharp.Net.HttpHeaderType.Request)
				},
				{
					"Trailer",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Trailer", global::UnityWebSocketSharp.Net.HttpHeaderType.Request | global::UnityWebSocketSharp.Net.HttpHeaderType.Response)
				},
				{
					"TransferEncoding",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Transfer-Encoding", global::UnityWebSocketSharp.Net.HttpHeaderType.Request | global::UnityWebSocketSharp.Net.HttpHeaderType.Response | global::UnityWebSocketSharp.Net.HttpHeaderType.Restricted | global::UnityWebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"Translate",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Translate", global::UnityWebSocketSharp.Net.HttpHeaderType.Request)
				},
				{
					"Upgrade",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Upgrade", global::UnityWebSocketSharp.Net.HttpHeaderType.Request | global::UnityWebSocketSharp.Net.HttpHeaderType.Response | global::UnityWebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"UserAgent",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("User-Agent", global::UnityWebSocketSharp.Net.HttpHeaderType.Request | global::UnityWebSocketSharp.Net.HttpHeaderType.Restricted)
				},
				{
					"Vary",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Vary", global::UnityWebSocketSharp.Net.HttpHeaderType.Response | global::UnityWebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"Via",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Via", global::UnityWebSocketSharp.Net.HttpHeaderType.Request | global::UnityWebSocketSharp.Net.HttpHeaderType.Response | global::UnityWebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"Warning",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("Warning", global::UnityWebSocketSharp.Net.HttpHeaderType.Request | global::UnityWebSocketSharp.Net.HttpHeaderType.Response | global::UnityWebSocketSharp.Net.HttpHeaderType.MultiValue)
				},
				{
					"WwwAuthenticate",
					new global::UnityWebSocketSharp.Net.HttpHeaderInfo("WWW-Authenticate", global::UnityWebSocketSharp.Net.HttpHeaderType.Response | global::UnityWebSocketSharp.Net.HttpHeaderType.Restricted | global::UnityWebSocketSharp.Net.HttpHeaderType.MultiValue)
				}
			};
		}

		internal WebHeaderCollection(global::UnityWebSocketSharp.Net.HttpHeaderType state, bool internallyUsed)
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
				_state = (global::UnityWebSocketSharp.Net.HttpHeaderType)serializationInfo.GetInt32("State");
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

		private void add(string name, string value, global::UnityWebSocketSharp.Net.HttpHeaderType headerType)
		{
			base.Add(name, value);
			if (_state == global::UnityWebSocketSharp.Net.HttpHeaderType.Unspecified && headerType != global::UnityWebSocketSharp.Net.HttpHeaderType.Unspecified)
			{
				_state = headerType;
			}
		}

		private void checkAllowed(global::UnityWebSocketSharp.Net.HttpHeaderType headerType)
		{
			if (_state == global::UnityWebSocketSharp.Net.HttpHeaderType.Unspecified || headerType == global::UnityWebSocketSharp.Net.HttpHeaderType.Unspecified || headerType == _state)
			{
				return;
			}
			throw new global::System.InvalidOperationException("This instance does not allow the header.");
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
				throw new global::System.ArgumentException("The name is an empty string.", paramName);
			}
			name = name.Trim();
			if (name.Length == 0)
			{
				throw new global::System.ArgumentException("The name is a string of spaces.", paramName);
			}
			if (!name.IsToken())
			{
				throw new global::System.ArgumentException("The name contains an invalid character.", paramName);
			}
			return name;
		}

		private void checkRestricted(string name, global::UnityWebSocketSharp.Net.HttpHeaderType headerType)
		{
			if (!_internallyUsed)
			{
				bool response = headerType == global::UnityWebSocketSharp.Net.HttpHeaderType.Response;
				if (isRestricted(name, response))
				{
					throw new global::System.ArgumentException("The header is a restricted header.");
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
				throw new global::System.ArgumentException("The value contains an invalid character.", paramName);
			}
			return value;
		}

		private static global::UnityWebSocketSharp.Net.HttpHeaderInfo getHeaderInfo(string name)
		{
			global::System.StringComparison comparisonType = global::System.StringComparison.InvariantCultureIgnoreCase;
			foreach (global::UnityWebSocketSharp.Net.HttpHeaderInfo value in _headers.Values)
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
			if (!_headers.TryGetValue(key, out var value))
			{
				return null;
			}
			return value.HeaderName;
		}

		private static global::UnityWebSocketSharp.Net.HttpHeaderType getHeaderType(string name)
		{
			global::UnityWebSocketSharp.Net.HttpHeaderInfo headerInfo = getHeaderInfo(name);
			if (headerInfo == null)
			{
				return global::UnityWebSocketSharp.Net.HttpHeaderType.Unspecified;
			}
			if (headerInfo.IsRequest)
			{
				if (headerInfo.IsResponse)
				{
					return global::UnityWebSocketSharp.Net.HttpHeaderType.Unspecified;
				}
				return global::UnityWebSocketSharp.Net.HttpHeaderType.Request;
			}
			if (!headerInfo.IsResponse)
			{
				return global::UnityWebSocketSharp.Net.HttpHeaderType.Unspecified;
			}
			return global::UnityWebSocketSharp.Net.HttpHeaderType.Response;
		}

		private static bool isMultiValue(string name, bool response)
		{
			return getHeaderInfo(name)?.IsMultiValue(response) ?? false;
		}

		private static bool isRestricted(string name, bool response)
		{
			return getHeaderInfo(name)?.IsRestricted(response) ?? false;
		}

		private void set(string name, string value, global::UnityWebSocketSharp.Net.HttpHeaderType headerType)
		{
			base.Set(name, value);
			if (_state == global::UnityWebSocketSharp.Net.HttpHeaderType.Unspecified && headerType != global::UnityWebSocketSharp.Net.HttpHeaderType.Unspecified)
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
				throw new global::System.ArgumentException("It does not contain a colon character.", "header");
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
			global::UnityWebSocketSharp.Net.HttpHeaderType headerType = getHeaderType(headerName);
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
				throw new global::System.ArgumentException("An empty string.", "header");
			}
			int num = header.IndexOf(':');
			if (num == -1)
			{
				throw new global::System.ArgumentException("It does not contain a colon character.", "header");
			}
			string name = header.Substring(0, num);
			string value = ((num < length - 1) ? header.Substring(num + 1) : string.Empty);
			name = checkName(name, "header");
			value = checkValue(value, "header");
			global::UnityWebSocketSharp.Net.HttpHeaderType headerType = getHeaderType(name);
			checkRestricted(name, headerType);
			checkAllowed(headerType);
			add(name, value, headerType);
		}

		public void Add(global::UnityWebSocketSharp.Net.HttpRequestHeader header, string value)
		{
			value = checkValue(value, "value");
			string headerName = getHeaderName(header.ToString());
			checkRestricted(headerName, global::UnityWebSocketSharp.Net.HttpHeaderType.Request);
			checkAllowed(global::UnityWebSocketSharp.Net.HttpHeaderType.Request);
			add(headerName, value, global::UnityWebSocketSharp.Net.HttpHeaderType.Request);
		}

		public void Add(global::UnityWebSocketSharp.Net.HttpResponseHeader header, string value)
		{
			value = checkValue(value, "value");
			string headerName = getHeaderName(header.ToString());
			checkRestricted(headerName, global::UnityWebSocketSharp.Net.HttpHeaderType.Response);
			checkAllowed(global::UnityWebSocketSharp.Net.HttpHeaderType.Response);
			add(headerName, value, global::UnityWebSocketSharp.Net.HttpHeaderType.Response);
		}

		public override void Add(string name, string value)
		{
			name = checkName(name, "name");
			value = checkValue(value, "value");
			global::UnityWebSocketSharp.Net.HttpHeaderType headerType = getHeaderType(name);
			checkRestricted(name, headerType);
			checkAllowed(headerType);
			add(name, value, headerType);
		}

		public override void Clear()
		{
			base.Clear();
			_state = global::UnityWebSocketSharp.Net.HttpHeaderType.Unspecified;
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
			if (values == null || values.Length == 0)
			{
				return null;
			}
			return values;
		}

		public override string[] GetValues(string name)
		{
			string[] values = base.GetValues(name);
			if (values == null || values.Length == 0)
			{
				return null;
			}
			return values;
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

		public void Remove(global::UnityWebSocketSharp.Net.HttpRequestHeader header)
		{
			string headerName = getHeaderName(header.ToString());
			checkRestricted(headerName, global::UnityWebSocketSharp.Net.HttpHeaderType.Request);
			checkAllowed(global::UnityWebSocketSharp.Net.HttpHeaderType.Request);
			base.Remove(headerName);
		}

		public void Remove(global::UnityWebSocketSharp.Net.HttpResponseHeader header)
		{
			string headerName = getHeaderName(header.ToString());
			checkRestricted(headerName, global::UnityWebSocketSharp.Net.HttpHeaderType.Response);
			checkAllowed(global::UnityWebSocketSharp.Net.HttpHeaderType.Response);
			base.Remove(headerName);
		}

		public override void Remove(string name)
		{
			name = checkName(name, "name");
			global::UnityWebSocketSharp.Net.HttpHeaderType headerType = getHeaderType(name);
			checkRestricted(name, headerType);
			checkAllowed(headerType);
			base.Remove(name);
		}

		public void Set(global::UnityWebSocketSharp.Net.HttpRequestHeader header, string value)
		{
			value = checkValue(value, "value");
			string headerName = getHeaderName(header.ToString());
			checkRestricted(headerName, global::UnityWebSocketSharp.Net.HttpHeaderType.Request);
			checkAllowed(global::UnityWebSocketSharp.Net.HttpHeaderType.Request);
			set(headerName, value, global::UnityWebSocketSharp.Net.HttpHeaderType.Request);
		}

		public void Set(global::UnityWebSocketSharp.Net.HttpResponseHeader header, string value)
		{
			value = checkValue(value, "value");
			string headerName = getHeaderName(header.ToString());
			checkRestricted(headerName, global::UnityWebSocketSharp.Net.HttpHeaderType.Response);
			checkAllowed(global::UnityWebSocketSharp.Net.HttpHeaderType.Response);
			set(headerName, value, global::UnityWebSocketSharp.Net.HttpHeaderType.Response);
		}

		public override void Set(string name, string value)
		{
			name = checkName(name, "name");
			value = checkValue(value, "value");
			global::UnityWebSocketSharp.Net.HttpHeaderType headerType = getHeaderType(name);
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
