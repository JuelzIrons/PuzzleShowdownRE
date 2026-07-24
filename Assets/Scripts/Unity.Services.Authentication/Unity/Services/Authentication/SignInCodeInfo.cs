namespace Unity.Services.Authentication
{
	public sealed class SignInCodeInfo
	{
		public string SignInCode { get; internal set; }

		public string Expiration { get; internal set; }

		public string Identifier { get; internal set; }
	}
}
