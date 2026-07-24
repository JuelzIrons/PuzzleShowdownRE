namespace Unity.Services.Multiplayer
{
	internal interface ISessionMatchmaking
	{
		global::Unity.Services.Multiplayer.MatchmakerState State { get; }

		string TicketId { get; }

		event global::System.Action<global::Unity.Services.Multiplayer.MatchmakerState> StateChanged;

		event global::System.Action MatchFound;

		event global::System.Action MatchFailed;

		event global::System.Action<global::Unity.Services.Multiplayer.ISession> MatchJoined;

		event global::System.Action MatchJoinFailed;

		global::System.Threading.Tasks.Task CancelAsync();

		global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.ISession> JoinAsync();
	}
}
