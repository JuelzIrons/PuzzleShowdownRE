namespace Unity.Services.Authentication.PlayerAccounts
{
	internal static class PlayerAccountsExceptionHandler
	{
		public static global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountsException HandleError(string error, string description = null, global::System.Exception innerException = null)
		{
			return error switch
			{
				"invalid_scope" => global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountsException.Create(10104, error), 
				"invalid_state" => global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountsException.Create(10101, error), 
				"invalid_request" => global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountsException.Create(10105, error), 
				"unauthorized_client" => global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountsException.Create(10108, error), 
				"unsupported_response_type" => global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountsException.Create(10110, error), 
				"invalid_client" => global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountsException.Create(10103, description, innerException), 
				"invalid_grant" => global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountsException.Create(10106, description, innerException), 
				"unsupported_grant_type" => global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountsException.Create(10109, description, innerException), 
				_ => global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountsException.Create(10100, error), 
			};
		}
	}
}
