namespace Unity.Services.Multiplayer
{
	internal interface IMatchmakerManager
	{
		global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.ISession> StartAsync(global::Unity.Services.Multiplayer.MatchmakerOptions matchOptions, global::Unity.Services.Multiplayer.SessionOptions sessionOptions, global::System.Threading.CancellationToken token = default(global::System.Threading.CancellationToken));
	}
}
