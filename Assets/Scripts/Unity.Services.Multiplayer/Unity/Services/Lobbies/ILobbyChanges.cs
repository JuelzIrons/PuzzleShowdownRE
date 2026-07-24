namespace Unity.Services.Lobbies
{
	public interface ILobbyChanges
	{
		bool LobbyDeleted { get; }

		global::Unity.Services.Lobbies.ChangedLobbyValue<string> Name { get; }

		global::Unity.Services.Lobbies.ChangedLobbyValue<bool> IsPrivate { get; }

		global::Unity.Services.Lobbies.ChangedLobbyValue<bool> IsLocked { get; }

		global::Unity.Services.Lobbies.ChangedLobbyValue<bool> HasPassword { get; }

		global::Unity.Services.Lobbies.ChangedLobbyValue<int> AvailableSlots { get; }

		global::Unity.Services.Lobbies.ChangedLobbyValue<int> MaxPlayers { get; }

		global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::Unity.Services.Lobbies.Models.DataObject>>> Data { get; }

		global::Unity.Services.Lobbies.ChangedLobbyValue<global::System.Collections.Generic.List<int>> PlayerLeft { get; }

		global::Unity.Services.Lobbies.ChangedLobbyValue<global::System.Collections.Generic.List<global::Unity.Services.Lobbies.LobbyPlayerJoined>> PlayerJoined { get; }

		global::Unity.Services.Lobbies.ChangedLobbyValue<global::System.Collections.Generic.Dictionary<int, global::Unity.Services.Lobbies.LobbyPlayerChanges>> PlayerData { get; }

		global::Unity.Services.Lobbies.ChangedLobbyValue<string> HostId { get; }

		global::Unity.Services.Lobbies.ChangedLobbyValue<int> Version { get; }

		global::Unity.Services.Lobbies.ChangedLobbyValue<global::System.DateTime> LastUpdated { get; }

		void ApplyToLobby(global::Unity.Services.Lobbies.Models.Lobby lobby);
	}
}
