namespace Unity.Services.Lobbies
{
	public class LobbyPlayerChanges
	{
		public int PlayerIndex { get; }

		public global::Unity.Services.Lobbies.ChangedLobbyValue<string> ConnectionInfoChanged { get; internal set; }

		public global::Unity.Services.Lobbies.ChangedLobbyValue<global::System.DateTime> LastUpdatedChanged { get; internal set; }

		public global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::Unity.Services.Lobbies.Models.PlayerDataObject>>> ChangedData { get; internal set; }

		public LobbyPlayerChanges(int index)
		{
			PlayerIndex = index;
			ConnectionInfoChanged = default(global::Unity.Services.Lobbies.ChangedLobbyValue<string>);
			LastUpdatedChanged = default(global::Unity.Services.Lobbies.ChangedLobbyValue<global::System.DateTime>);
			ChangedData = default(global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::Unity.Services.Lobbies.Models.PlayerDataObject>>>);
		}
	}
}
