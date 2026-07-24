namespace Unity.Services.Lobbies
{
	public interface ILobbyEvents
	{
		LobbyEventCallbacks Callbacks { get; }

		global::System.Threading.Tasks.Task SubscribeAsync();

		global::System.Threading.Tasks.Task UnsubscribeAsync();
	}
}
