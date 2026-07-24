namespace Unity.Services.Multiplayer
{
	internal class JwtDecoder : global::Unity.Services.Multiplayer.IJwtDecoder
	{
		private static readonly global::System.DateTime k_UnixEpoch = new global::System.DateTime(1970, 1, 1, 0, 0, 0, 0, global::System.DateTimeKind.Utc);

		private static readonly char[] k_JwtSeparator = new char[1] { '.' };

		private readonly global::Unity.Services.Multiplayer.IDateTimeWrapper m_DateTime;

		internal JwtDecoder(global::Unity.Services.Multiplayer.IDateTimeWrapper dateTime)
		{
			m_DateTime = dateTime;
		}

		public T Decode<T>(string token) where T : global::Unity.Services.Multiplayer.BaseJwt
		{
			string[] array = token.Split(k_JwtSeparator);
			if (array.Length == 3)
			{
				string input = array[1];
				return global::Newtonsoft.Json.JsonConvert.DeserializeObject<T>(global::System.Text.Encoding.UTF8.GetString(Base64UrlDecode(input)));
			}
			global::Unity.Services.Multiplayer.Logger.LogError($"That is not a valid token (expected 3 parts but has {array.Length}).");
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
