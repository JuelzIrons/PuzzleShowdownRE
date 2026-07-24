namespace Unity.Services.Authentication
{
	internal interface IJwtDecoder
	{
		T Decode<T>(string token) where T : global::Unity.Services.Authentication.BaseJwt;
	}
}
