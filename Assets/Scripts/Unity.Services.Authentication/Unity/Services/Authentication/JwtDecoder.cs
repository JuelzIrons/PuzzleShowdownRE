namespace Unity.Services.Authentication
{
	internal class JwtDecoder : global::Unity.Services.Authentication.IJwtDecoder
	{
		private static readonly char[] k_JwtSeparator = new char[1] { '.' };

		public T Decode<T>(string token) where T : global::Unity.Services.Authentication.BaseJwt
		{
			string[] array = token.Split(k_JwtSeparator);
			if (array.Length == 3)
			{
				string input = array[1];
				return global::Unity.Services.Authentication.IsolatedJsonConvert.DeserializeObject<T>(global::System.Text.Encoding.UTF8.GetString(Base64UrlDecode(input)), global::Unity.Services.Authentication.SerializerSettings.DefaultSerializerSettings);
			}
			global::Unity.Services.Authentication.Logger.LogError($"That is not a valid token (expected 3 parts but has {array.Length}).");
			return null;
		}

		private byte[] Base64UrlDecode(string input)
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
