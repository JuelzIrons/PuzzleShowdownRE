namespace Unity.Services.Authentication
{
	internal class CodeChallengeGenerator
	{
		private const int k_CodeLength = 125;

		private const string k_CodeChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

		private readonly global::System.Text.StringBuilder m_CodeBuilder;

		internal CodeChallengeGenerator()
		{
			m_CodeBuilder = new global::System.Text.StringBuilder(125);
		}

		public string GenerateCode()
		{
			byte[] array = new byte[125];
			using (global::System.Security.Cryptography.RNGCryptoServiceProvider rNGCryptoServiceProvider = new global::System.Security.Cryptography.RNGCryptoServiceProvider())
			{
				rNGCryptoServiceProvider.GetBytes(array);
			}
			m_CodeBuilder.Clear();
			for (int i = 0; i < 125; i++)
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
			return global::System.BitConverter.ToString(sHA.ComputeHash(bytes)).Replace("-", "").ToLower();
		}
	}
}
