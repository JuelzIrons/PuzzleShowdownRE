namespace UnityWebSocketSharp.Net
{
	internal class AuthenticationResponse
	{
		private uint _nonceCount;

		private global::System.Collections.Specialized.NameValueCollection _parameters;

		private global::UnityWebSocketSharp.Net.AuthenticationSchemes _scheme;

		internal uint NonceCount
		{
			get
			{
				if (_nonceCount >= uint.MaxValue)
				{
					return 0u;
				}
				return _nonceCount;
			}
		}

		internal global::System.Collections.Specialized.NameValueCollection Parameters => _parameters;

		public string Algorithm => _parameters["algorithm"];

		public string Cnonce => _parameters["cnonce"];

		public string Nc => _parameters["nc"];

		public string Nonce => _parameters["nonce"];

		public string Opaque => _parameters["opaque"];

		public string Password => _parameters["password"];

		public string Qop => _parameters["qop"];

		public string Realm => _parameters["realm"];

		public string Response => _parameters["response"];

		public global::UnityWebSocketSharp.Net.AuthenticationSchemes Scheme => _scheme;

		public string Uri => _parameters["uri"];

		public string UserName => _parameters["username"];

		private AuthenticationResponse(global::UnityWebSocketSharp.Net.AuthenticationSchemes scheme, global::System.Collections.Specialized.NameValueCollection parameters)
		{
			_scheme = scheme;
			_parameters = parameters;
		}

		internal AuthenticationResponse(global::UnityWebSocketSharp.Net.NetworkCredential credentials)
			: this(global::UnityWebSocketSharp.Net.AuthenticationSchemes.Basic, new global::System.Collections.Specialized.NameValueCollection(), credentials, 0u)
		{
		}

		internal AuthenticationResponse(global::UnityWebSocketSharp.Net.AuthenticationChallenge challenge, global::UnityWebSocketSharp.Net.NetworkCredential credentials, uint nonceCount)
			: this(challenge.Scheme, challenge.Parameters, credentials, nonceCount)
		{
		}

		internal AuthenticationResponse(global::UnityWebSocketSharp.Net.AuthenticationSchemes scheme, global::System.Collections.Specialized.NameValueCollection parameters, global::UnityWebSocketSharp.Net.NetworkCredential credentials, uint nonceCount)
			: this(scheme, parameters)
		{
			_parameters["username"] = credentials.Username;
			_parameters["password"] = credentials.Password;
			_parameters["uri"] = credentials.Domain;
			_nonceCount = nonceCount;
			if (scheme == global::UnityWebSocketSharp.Net.AuthenticationSchemes.Digest)
			{
				initAsDigest();
			}
		}

		private static string createA1(string username, string password, string realm)
		{
			return $"{username}:{realm}:{password}";
		}

		private static string createA1(string username, string password, string realm, string nonce, string cnonce)
		{
			string value = createA1(username, password, realm);
			return $"{hash(value)}:{nonce}:{cnonce}";
		}

		private static string createA2(string method, string uri)
		{
			return $"{method}:{uri}";
		}

		private static string createA2(string method, string uri, string entity)
		{
			return $"{method}:{uri}:{hash(entity)}";
		}

		private static string hash(string value)
		{
			global::System.Security.Cryptography.MD5 mD = global::System.Security.Cryptography.MD5.Create();
			byte[] bytes = global::System.Text.Encoding.UTF8.GetBytes(value);
			byte[] array = mD.ComputeHash(bytes);
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder(64);
			byte[] array2 = array;
			foreach (byte b in array2)
			{
				stringBuilder.Append(b.ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		private void initAsDigest()
		{
			string text = _parameters["qop"];
			if (text != null)
			{
				if (text.Split(',').Contains((string qop) => qop.Trim().ToLower() == "auth"))
				{
					_parameters["qop"] = "auth";
					_parameters["cnonce"] = global::UnityWebSocketSharp.Net.AuthenticationChallenge.CreateNonceValue();
					_parameters["nc"] = $"{++_nonceCount:x8}";
				}
				else
				{
					_parameters["qop"] = null;
				}
			}
			_parameters["method"] = "GET";
			_parameters["response"] = CreateRequestDigest(_parameters);
		}

		internal static string CreateRequestDigest(global::System.Collections.Specialized.NameValueCollection parameters)
		{
			string username = parameters["username"];
			string password = parameters["password"];
			string realm = parameters["realm"];
			string text = parameters["nonce"];
			string uri = parameters["uri"];
			string text2 = parameters["algorithm"];
			string text3 = parameters["qop"];
			string text4 = parameters["cnonce"];
			string text5 = parameters["nc"];
			string method = parameters["method"];
			string value = ((text2 != null && text2.ToLower() == "md5-sess") ? createA1(username, password, realm, text, text4) : createA1(username, password, realm));
			string value2 = ((text3 != null && text3.ToLower() == "auth-int") ? createA2(method, uri, parameters["entity"]) : createA2(method, uri));
			string arg = hash(value);
			string arg2 = ((text3 != null) ? $"{text}:{text5}:{text4}:{text3}:{hash(value2)}" : $"{text}:{hash(value2)}");
			return hash($"{arg}:{arg2}");
		}

		internal static global::UnityWebSocketSharp.Net.AuthenticationResponse Parse(string value)
		{
			try
			{
				string[] array = value.Split(new char[1] { ' ' }, 2);
				if (array.Length != 2)
				{
					return null;
				}
				string text = array[0].ToLower();
				if (text == "basic")
				{
					global::System.Collections.Specialized.NameValueCollection parameters = ParseBasicCredentials(array[1]);
					return new global::UnityWebSocketSharp.Net.AuthenticationResponse(global::UnityWebSocketSharp.Net.AuthenticationSchemes.Basic, parameters);
				}
				if (text == "digest")
				{
					global::System.Collections.Specialized.NameValueCollection parameters2 = global::UnityWebSocketSharp.Net.AuthenticationChallenge.ParseParameters(array[1]);
					return new global::UnityWebSocketSharp.Net.AuthenticationResponse(global::UnityWebSocketSharp.Net.AuthenticationSchemes.Digest, parameters2);
				}
				return null;
			}
			catch
			{
				return null;
			}
		}

		internal static global::System.Collections.Specialized.NameValueCollection ParseBasicCredentials(string value)
		{
			global::System.Collections.Specialized.NameValueCollection nameValueCollection = new global::System.Collections.Specialized.NameValueCollection();
			byte[] bytes = global::System.Convert.FromBase64String(value);
			string text = global::System.Text.Encoding.Default.GetString(bytes);
			int num = text.IndexOf(':');
			string text2 = text.Substring(0, num);
			string value2 = ((num < text.Length - 1) ? text.Substring(num + 1) : string.Empty);
			num = text2.IndexOf('\\');
			if (num > -1)
			{
				text2 = text2.Substring(num + 1);
			}
			nameValueCollection["username"] = text2;
			nameValueCollection["password"] = value2;
			return nameValueCollection;
		}

		internal string ToBasicString()
		{
			string arg = _parameters["username"];
			string arg2 = _parameters["password"];
			string s = $"{arg}:{arg2}";
			string text = global::System.Convert.ToBase64String(global::System.Text.Encoding.UTF8.GetBytes(s));
			return "Basic " + text;
		}

		internal string ToDigestString()
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder(256);
			string text = _parameters["username"];
			string text2 = _parameters["realm"];
			string text3 = _parameters["nonce"];
			string text4 = _parameters["uri"];
			string text5 = _parameters["response"];
			stringBuilder.AppendFormat("Digest username=\"{0}\", realm=\"{1}\", nonce=\"{2}\", uri=\"{3}\", response=\"{4}\"", text, text2, text3, text4, text5);
			string text6 = _parameters["opaque"];
			if (text6 != null)
			{
				stringBuilder.AppendFormat(", opaque=\"{0}\"", text6);
			}
			string text7 = _parameters["algorithm"];
			if (text7 != null)
			{
				stringBuilder.AppendFormat(", algorithm={0}", text7);
			}
			string text8 = _parameters["qop"];
			if (text8 != null)
			{
				string arg = _parameters["cnonce"];
				string arg2 = _parameters["nc"];
				stringBuilder.AppendFormat(", qop={0}, cnonce=\"{1}\", nc={2}", text8, arg, arg2);
			}
			return stringBuilder.ToString();
		}

		public global::System.Security.Principal.IIdentity ToIdentity()
		{
			if (_scheme == global::UnityWebSocketSharp.Net.AuthenticationSchemes.Basic)
			{
				string username = _parameters["username"];
				string password = _parameters["password"];
				return new global::UnityWebSocketSharp.Net.HttpBasicIdentity(username, password);
			}
			if (_scheme == global::UnityWebSocketSharp.Net.AuthenticationSchemes.Digest)
			{
				return new global::UnityWebSocketSharp.Net.HttpDigestIdentity(_parameters);
			}
			return null;
		}

		public override string ToString()
		{
			if (_scheme == global::UnityWebSocketSharp.Net.AuthenticationSchemes.Basic)
			{
				return ToBasicString();
			}
			if (_scheme == global::UnityWebSocketSharp.Net.AuthenticationSchemes.Digest)
			{
				return ToDigestString();
			}
			return string.Empty;
		}
	}
}
