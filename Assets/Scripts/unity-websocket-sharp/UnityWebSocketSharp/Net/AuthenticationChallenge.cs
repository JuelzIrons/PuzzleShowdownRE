namespace UnityWebSocketSharp.Net
{
	internal class AuthenticationChallenge
	{
		private global::System.Collections.Specialized.NameValueCollection _parameters;

		private global::UnityWebSocketSharp.Net.AuthenticationSchemes _scheme;

		internal global::System.Collections.Specialized.NameValueCollection Parameters => _parameters;

		public string Algorithm => _parameters["algorithm"];

		public string Domain => _parameters["domain"];

		public string Nonce => _parameters["nonce"];

		public string Opaque => _parameters["opaque"];

		public string Qop => _parameters["qop"];

		public string Realm => _parameters["realm"];

		public global::UnityWebSocketSharp.Net.AuthenticationSchemes Scheme => _scheme;

		public string Stale => _parameters["stale"];

		private AuthenticationChallenge(global::UnityWebSocketSharp.Net.AuthenticationSchemes scheme, global::System.Collections.Specialized.NameValueCollection parameters)
		{
			_scheme = scheme;
			_parameters = parameters;
		}

		internal AuthenticationChallenge(global::UnityWebSocketSharp.Net.AuthenticationSchemes scheme, string realm)
			: this(scheme, new global::System.Collections.Specialized.NameValueCollection())
		{
			_parameters["realm"] = realm;
			if (scheme == global::UnityWebSocketSharp.Net.AuthenticationSchemes.Digest)
			{
				_parameters["nonce"] = CreateNonceValue();
				_parameters["algorithm"] = "MD5";
				_parameters["qop"] = "auth";
			}
		}

		internal static global::UnityWebSocketSharp.Net.AuthenticationChallenge CreateBasicChallenge(string realm)
		{
			return new global::UnityWebSocketSharp.Net.AuthenticationChallenge(global::UnityWebSocketSharp.Net.AuthenticationSchemes.Basic, realm);
		}

		internal static global::UnityWebSocketSharp.Net.AuthenticationChallenge CreateDigestChallenge(string realm)
		{
			return new global::UnityWebSocketSharp.Net.AuthenticationChallenge(global::UnityWebSocketSharp.Net.AuthenticationSchemes.Digest, realm);
		}

		internal static string CreateNonceValue()
		{
			global::System.Security.Cryptography.RandomNumberGenerator randomNumberGenerator = global::System.Security.Cryptography.RandomNumberGenerator.Create();
			byte[] array = new byte[16];
			randomNumberGenerator.GetBytes(array);
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder(32);
			byte[] array2 = array;
			foreach (byte b in array2)
			{
				stringBuilder.Append(b.ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		internal static global::UnityWebSocketSharp.Net.AuthenticationChallenge Parse(string value)
		{
			string[] array = value.Split(new char[1] { ' ' }, 2);
			if (array.Length != 2)
			{
				return null;
			}
			string text = array[0].ToLower();
			if (text == "basic")
			{
				global::System.Collections.Specialized.NameValueCollection parameters = ParseParameters(array[1]);
				return new global::UnityWebSocketSharp.Net.AuthenticationChallenge(global::UnityWebSocketSharp.Net.AuthenticationSchemes.Basic, parameters);
			}
			if (text == "digest")
			{
				global::System.Collections.Specialized.NameValueCollection parameters2 = ParseParameters(array[1]);
				return new global::UnityWebSocketSharp.Net.AuthenticationChallenge(global::UnityWebSocketSharp.Net.AuthenticationSchemes.Digest, parameters2);
			}
			return null;
		}

		internal static global::System.Collections.Specialized.NameValueCollection ParseParameters(string value)
		{
			global::System.Collections.Specialized.NameValueCollection nameValueCollection = new global::System.Collections.Specialized.NameValueCollection();
			foreach (string item in value.SplitHeaderValue(','))
			{
				int num = item.IndexOf('=');
				string name = ((num > 0) ? item.Substring(0, num).Trim() : null);
				string value2 = ((num < 0) ? item.Trim().Trim('"') : ((num < item.Length - 1) ? item.Substring(num + 1).Trim().Trim('"') : string.Empty));
				nameValueCollection.Add(name, value2);
			}
			return nameValueCollection;
		}

		internal string ToBasicString()
		{
			return string.Format("Basic realm=\"{0}\"", _parameters["realm"]);
		}

		internal string ToDigestString()
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder(128);
			string text = _parameters["domain"];
			string arg = _parameters["realm"];
			string text2 = _parameters["nonce"];
			if (text != null)
			{
				stringBuilder.AppendFormat("Digest realm=\"{0}\", domain=\"{1}\", nonce=\"{2}\"", arg, text, text2);
			}
			else
			{
				stringBuilder.AppendFormat("Digest realm=\"{0}\", nonce=\"{1}\"", arg, text2);
			}
			string text3 = _parameters["opaque"];
			if (text3 != null)
			{
				stringBuilder.AppendFormat(", opaque=\"{0}\"", text3);
			}
			string text4 = _parameters["stale"];
			if (text4 != null)
			{
				stringBuilder.AppendFormat(", stale={0}", text4);
			}
			string text5 = _parameters["algorithm"];
			if (text5 != null)
			{
				stringBuilder.AppendFormat(", algorithm={0}", text5);
			}
			string text6 = _parameters["qop"];
			if (text6 != null)
			{
				stringBuilder.AppendFormat(", qop=\"{0}\"", text6);
			}
			return stringBuilder.ToString();
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
