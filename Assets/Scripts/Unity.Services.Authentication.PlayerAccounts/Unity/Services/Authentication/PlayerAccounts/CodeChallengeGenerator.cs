namespace Unity.Services.Authentication.PlayerAccounts
{
	internal class CodeChallengeGenerator
	{
		private const int k_CodeLength = 128;

		private const string k_CodeChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

		private readonly global::System.Text.StringBuilder m_CodeBuilder;

		internal CodeChallengeGenerator()
		{
			m_CodeBuilder = new global::System.Text.StringBuilder(128);
		}

		public string GenerateCode()
		{
			byte[] array = new byte[128];
			using (global::System.Security.Cryptography.RNGCryptoServiceProvider rNGCryptoServiceProvider = new global::System.Security.Cryptography.RNGCryptoServiceProvider())
			{
				rNGCryptoServiceProvider.GetBytes(array);
			}
			m_CodeBuilder.Clear();
			for (int i = 0; i < 128; i++)
			{
				m_CodeBuilder.Append("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"[array[i] % "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".Length]);
			}
			return m_CodeBuilder.ToString();
		}

		public string GenerateStateString()
		{
			return global::System.Guid.NewGuid().ToString();
		}

		public static string S256EncodeChallenge(string code)
		{
			global::System.Security.Cryptography.SHA256 sHA = global::System.Security.Cryptography.SHA256.Create();
			byte[] bytes = global::System.Text.Encoding.UTF8.GetBytes(code);
			return UrlSafeBase64Encode(sHA.ComputeHash(bytes));
		}

		private static string UrlSafeBase64Encode(byte[] input)
		{
			return global::System.Convert.ToBase64String(input).Replace('+', '-').Replace('/', '_')
				.Replace("=", "");
		}
	}
}
