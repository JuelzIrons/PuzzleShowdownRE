namespace Unity.Services.Multiplayer
{
	internal interface ISessionQuerier
	{
		global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.QuerySessionsResults> QueryAsync(global::Unity.Services.Multiplayer.QuerySessionsOptions options);
	}
}
