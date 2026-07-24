namespace Unity.Services.Multiplayer
{
	public enum MatchmakerState
	{
		None = 0,
		InProgress = 1,
		Canceled = 2,
		MatchFailed = 3,
		MatchFound = 4,
		JoinFailed = 5,
		Joined = 6
	}
}
