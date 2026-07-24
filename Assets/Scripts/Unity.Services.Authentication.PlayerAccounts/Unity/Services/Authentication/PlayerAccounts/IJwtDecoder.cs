namespace Unity.Services.Authentication.PlayerAccounts
{
	internal interface IJwtDecoder
	{
		T Decode<T>(string token) where T : global::Unity.Services.Authentication.PlayerAccounts.BaseJwt;
	}
}
