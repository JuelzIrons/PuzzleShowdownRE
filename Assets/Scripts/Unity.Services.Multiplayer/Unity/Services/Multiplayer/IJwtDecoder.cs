namespace Unity.Services.Multiplayer
{
	internal interface IJwtDecoder
	{
		T Decode<T>(string token) where T : global::Unity.Services.Multiplayer.BaseJwt;
	}
}
