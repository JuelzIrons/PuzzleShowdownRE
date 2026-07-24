namespace Unity.Services.Authentication.PlayerAccounts
{
	public sealed class PlayerAccountsException : global::Unity.Services.Core.RequestFailedException
	{
		private PlayerAccountsException(int errorCode, string message, global::System.Exception innerException = null)
			: base(errorCode, message, innerException)
		{
		}

		internal static global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountsException Create(int errorCode, string message, global::System.Exception innerException = null)
		{
			return new global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountsException(errorCode, message, innerException);
		}
	}
}
