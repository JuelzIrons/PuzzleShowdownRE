namespace WebSocketSharp.Net
{
	internal class AuthenticationChallenge : global::WebSocketSharp.Net.AuthenticationBase
	{
		public string Domain => Parameters["domain"];

		public string Stale => Parameters["stale"];

		private AuthenticationChallenge(global::WebSocketSharp.Net.AuthenticationSchemes scheme, global::System.Collections.Specialized.NameValueCollection parameters)
			: base(scheme, parameters)
		{
		}

		internal AuthenticationChallenge(global::WebSocketSharp.Net.AuthenticationSchemes scheme, string realm)
			: base(scheme, new global::System.Collections.Specialized.NameValueCollection())
		{
			Parameters["realm"] = realm;
			if (scheme == global::WebSocketSharp.Net.AuthenticationSchemes.Digest)
			{
				Parameters["nonce"] = global::WebSocketSharp.Net.AuthenticationBase.CreateNonceValue();
				Parameters["algorithm"] = "MD5";
				Parameters["qop"] = "auth";
			}
		}

		internal static global::WebSocketSharp.Net.AuthenticationChallenge CreateBasicChallenge(string realm)
		{
			return new global::WebSocketSharp.Net.AuthenticationChallenge(global::WebSocketSharp.Net.AuthenticationSchemes.Basic, realm);
		}

		internal static global::WebSocketSharp.Net.AuthenticationChallenge CreateDigestChallenge(string realm)
		{
			return new global::WebSocketSharp.Net.AuthenticationChallenge(global::WebSocketSharp.Net.AuthenticationSchemes.Digest, realm);
		}

		internal static global::WebSocketSharp.Net.AuthenticationChallenge Parse(string value)
		{
			string[] array = value.Split(new char[1] { ' ' }, 2);
			if (array.Length != 2)
			{
				return null;
			}
			string text = array[0].ToLower();
			return (text == "basic") ? new global::WebSocketSharp.Net.AuthenticationChallenge(global::WebSocketSharp.Net.AuthenticationSchemes.Basic, global::WebSocketSharp.Net.AuthenticationBase.ParseParameters(array[1])) : ((text == "digest") ? new global::WebSocketSharp.Net.AuthenticationChallenge(global::WebSocketSharp.Net.AuthenticationSchemes.Digest, global::WebSocketSharp.Net.AuthenticationBase.ParseParameters(array[1])) : null);
		}

		internal override string ToBasicString()
		{
			return string.Format("Basic realm=\"{0}\"", Parameters["realm"]);
		}

		internal override string ToDigestString()
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder(128);
			string text = Parameters["domain"];
			if (text != null)
			{
				stringBuilder.AppendFormat("Digest realm=\"{0}\", domain=\"{1}\", nonce=\"{2}\"", Parameters["realm"], text, Parameters["nonce"]);
			}
			else
			{
				stringBuilder.AppendFormat("Digest realm=\"{0}\", nonce=\"{1}\"", Parameters["realm"], Parameters["nonce"]);
			}
			string text2 = Parameters["opaque"];
			if (text2 != null)
			{
				stringBuilder.AppendFormat(", opaque=\"{0}\"", text2);
			}
			string text3 = Parameters["stale"];
			if (text3 != null)
			{
				stringBuilder.AppendFormat(", stale={0}", text3);
			}
			string text4 = Parameters["algorithm"];
			if (text4 != null)
			{
				stringBuilder.AppendFormat(", algorithm={0}", text4);
			}
			string text5 = Parameters["qop"];
			if (text5 != null)
			{
				stringBuilder.AppendFormat(", qop=\"{0}\"", text5);
			}
			return stringBuilder.ToString();
		}
	}
}
