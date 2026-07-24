namespace Unity.Services.Authentication.PlayerAccounts
{
	internal class JwtDecoder : global::Unity.Services.Authentication.PlayerAccounts.IJwtDecoder
	{
		private static readonly global::System.DateTime k_UnixEpoch = new global::System.DateTime(1970, 1, 1, 0, 0, 0, 0, global::System.DateTimeKind.Utc);

		private static readonly char[] k_JwtSeparator = new char[1] { '.' };

		private readonly global::Newtonsoft.Json.JsonSerializerSettings m_JsonSerializerSettings = new global::Newtonsoft.Json.JsonSerializerSettings();

		private readonly global::Unity.Services.Authentication.PlayerAccounts.IDateTimeWrapper m_DateTime;

		internal JwtDecoder(global::Unity.Services.Authentication.PlayerAccounts.IDateTimeWrapper dateTime)
		{
			m_DateTime = dateTime;
		}

		public T Decode<T>(string token) where T : global::Unity.Services.Authentication.PlayerAccounts.BaseJwt
		{
			string[] array = token.Split(k_JwtSeparator);
			if (array.Length == 3)
			{
				string input = array[0];
				string input2 = array[1];
				Base64UrlDecode(array[2]);
				string value = global::System.Text.Encoding.UTF8.GetString(Base64UrlDecode(input));
				string value2 = global::System.Text.Encoding.UTF8.GetString(Base64UrlDecode(input2));
				global::Newtonsoft.Json.JsonConvert.DeserializeObject<global::System.Collections.Generic.Dictionary<string, string>>(value, m_JsonSerializerSettings);
				T val = global::Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value2, m_JsonSerializerSettings);
				if (m_DateTime.SecondsSinceUnixEpoch() >= (double)val.ExpirationTimeUnix)
				{
					global::UnityEngine.Debug.LogError("Token has expired.");
					return null;
				}
				return val;
			}
			global::UnityEngine.Debug.LogError($"That is not a valid token (expected 3 parts but has {array.Length}).");
			return null;
		}

		private static byte[] Base64UrlDecode(string input)
		{
			string text = input;
			text = text.Replace('-', '+');
			text = text.Replace('_', '/');
			int num = input.Length % 4;
			if (num > 0)
			{
				text += new string('=', 4 - num);
			}
			return global::System.Convert.FromBase64String(text);
		}
	}
}
