namespace Unity.Services.Lobbies
{
	public enum LobbyEventConnectionState
	{
		Unknown = 0,
		Unsubscribed = 1,
		Subscribing = 2,
		Subscribed = 3,
		Unsynced = 4,
		Error = 5
	}
}
