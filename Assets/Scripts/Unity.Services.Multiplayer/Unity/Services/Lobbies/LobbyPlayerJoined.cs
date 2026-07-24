namespace Unity.Services.Lobbies
{
	public struct LobbyPlayerJoined
	{
		public int PlayerIndex { get; }

		public global::Unity.Services.Lobbies.Models.Player Player { get; }

		public LobbyPlayerJoined(int index, global::Unity.Services.Lobbies.Models.Player player)
		{
			PlayerIndex = index;
			Player = player;
		}
	}
}
