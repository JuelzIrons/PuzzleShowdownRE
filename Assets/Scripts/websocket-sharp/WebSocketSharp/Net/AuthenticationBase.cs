namespace WebSocketSharp.Net
{
	internal abstract class AuthenticationBase
	{
		private global::WebSocketSharp.Net.AuthenticationSchemes _scheme;

		internal global::System.Collections.Specialized.NameValueCollection Parameters;

		public string Algorithm => Parameters["algorithm"];

		public string Nonce => Parameters["nonce"];

		public string Opaque => Parameters["opaque"];

		public string Qop => Parameters["qop"];

		public string Realm => Parameters["realm"];

		public global::WebSocketSharp.Net.AuthenticationSchemes Scheme => _scheme;

		protected AuthenticationBase(global::WebSocketSharp.Net.AuthenticationSchemes scheme, global::System.Collections.Specialized.NameValueCollection parameters)
		{
			_scheme = scheme;
			Parameters = parameters;
		}

		internal static string CreateNonceValue()
		{
			byte[] array = new byte[16];
			global::System.Random random = new global::System.Random();
			random.NextBytes(array);
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder(32);
			byte[] array2 = array;
			foreach (byte b in array2)
			{
				stringBuilder.Append(b.ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		internal static global::System.Collections.Specialized.NameValueCollection ParseParameters(string value)
		{
			global::System.Collections.Specialized.NameValueCollection nameValueCollection = new global::System.Collections.Specialized.NameValueCollection();
			foreach (string item in value.SplitHeaderValue(','))
			{
				int num = item.IndexOf('=');
				string name = ((num > 0) ? item.Substring(0, num).Trim() : null);
				string value2 = ((num < 0) ? item.Trim().Trim(new char[1] { '"' }) : ((num < item.Length - 1) ? item.Substring(num + 1).Trim().Trim(new char[1] { '"' }) : string.Empty));
				nameValueCollection.Add(name, value2);
			}
			return nameValueCollection;
		}

		internal abstract string ToBasicString();

		internal abstract string ToDigestString();

		public override string ToString()
		{
			return (_scheme == global::WebSocketSharp.Net.AuthenticationSchemes.Basic) ? ToBasicString() : ((_scheme == global::WebSocketSharp.Net.AuthenticationSchemes.Digest) ? ToDigestString() : string.Empty);
		}
	}
}
