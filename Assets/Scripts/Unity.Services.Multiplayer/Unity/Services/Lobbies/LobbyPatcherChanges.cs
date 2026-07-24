namespace Unity.Services.Lobbies
{
	internal class LobbyPatcherChanges : global::Unity.Services.Lobbies.ILobbyChanges
	{
		public bool LobbyDeleted { get; private set; }

		public global::Unity.Services.Lobbies.ChangedLobbyValue<string> Name { get; private set; }

		public global::Unity.Services.Lobbies.ChangedLobbyValue<bool> IsPrivate { get; private set; }

		public global::Unity.Services.Lobbies.ChangedLobbyValue<bool> IsLocked { get; private set; }

		public global::Unity.Services.Lobbies.ChangedLobbyValue<bool> HasPassword { get; private set; }

		public global::Unity.Services.Lobbies.ChangedLobbyValue<int> AvailableSlots { get; private set; }

		public global::Unity.Services.Lobbies.ChangedLobbyValue<int> MaxPlayers { get; private set; }

		public global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::Unity.Services.Lobbies.Models.DataObject>>> Data { get; private set; }

		public global::Unity.Services.Lobbies.ChangedLobbyValue<global::System.Collections.Generic.List<int>> PlayerLeft { get; private set; }

		public global::Unity.Services.Lobbies.ChangedLobbyValue<global::System.Collections.Generic.List<global::Unity.Services.Lobbies.LobbyPlayerJoined>> PlayerJoined { get; private set; }

		public global::Unity.Services.Lobbies.ChangedLobbyValue<global::System.Collections.Generic.Dictionary<int, global::Unity.Services.Lobbies.LobbyPlayerChanges>> PlayerData { get; private set; }

		public global::Unity.Services.Lobbies.ChangedLobbyValue<string> HostId { get; private set; }

		public global::Unity.Services.Lobbies.ChangedLobbyValue<int> Version { get; private set; }

		public global::Unity.Services.Lobbies.ChangedLobbyValue<global::System.DateTime> LastUpdated { get; private set; }

		public LobbyPatcherChanges(int version)
		{
			Version = global::Unity.Services.Lobbies.LobbyValue.Changed(version);
		}

		public void LobbyDeletedChange()
		{
			LobbyDeleted = true;
		}

		public void NameChange(string name)
		{
			Name = global::Unity.Services.Lobbies.LobbyValue.Changed(name);
		}

		public void IsPrivateChange(bool isPrivate)
		{
			IsPrivate = global::Unity.Services.Lobbies.LobbyValue.Changed(isPrivate);
		}

		public void IsLockedChange(bool isLocked)
		{
			IsLocked = global::Unity.Services.Lobbies.LobbyValue.Changed(isLocked);
		}

		public void HasPasswordChange(bool hasPassword)
		{
			HasPassword = global::Unity.Services.Lobbies.LobbyValue.Changed(hasPassword);
		}

		public void AvailableSlotsChange(int availableSlots)
		{
			AvailableSlots = global::Unity.Services.Lobbies.LobbyValue.Changed(availableSlots);
		}

		public void MaxPlayersChange(int maxPlayers)
		{
			MaxPlayers = global::Unity.Services.Lobbies.LobbyValue.Changed(maxPlayers);
		}

		public void DataChange(string key, global::Unity.Services.Lobbies.Models.DataObject dataObject)
		{
			if (!Data.Changed)
			{
				Data = global::Unity.Services.Lobbies.LobbyValue.ChangedNotRemoved(new global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::Unity.Services.Lobbies.Models.DataObject>>());
			}
			Data.Value[key] = global::Unity.Services.Lobbies.LobbyValue.ChangedNotRemoved(dataObject);
		}

		public void DataAdded(string key, global::Unity.Services.Lobbies.Models.DataObject dataObject)
		{
			if (!Data.Added)
			{
				Data = global::Unity.Services.Lobbies.LobbyValue.ChangeAdded(new global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::Unity.Services.Lobbies.Models.DataObject>>());
			}
			Data.Value[key] = global::Unity.Services.Lobbies.LobbyValue.ChangeAdded(dataObject);
		}

		public void DataRemoveChange()
		{
			Data = global::Unity.Services.Lobbies.LobbyValue.Removed<global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::Unity.Services.Lobbies.Models.DataObject>>>();
		}

		public void DataRemoveChange(string key)
		{
			if (!Data.Changed)
			{
				Data = global::Unity.Services.Lobbies.LobbyValue.ChangedNotRemoved(new global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::Unity.Services.Lobbies.Models.DataObject>>());
			}
			Data.Value[key] = global::Unity.Services.Lobbies.LobbyValue.Removed<global::Unity.Services.Lobbies.Models.DataObject>();
		}

		public void HostChange(string newHostId)
		{
			HostId = global::Unity.Services.Lobbies.LobbyValue.Changed(newHostId);
		}

		public void LastUpdatedChange(global::System.DateTime lastUpdated)
		{
			LastUpdated = global::Unity.Services.Lobbies.LobbyValue.Changed(lastUpdated);
		}

		public void PlayerLeftChange(int index)
		{
			if (!PlayerLeft.Changed)
			{
				PlayerLeft = global::Unity.Services.Lobbies.LobbyValue.Changed(new global::System.Collections.Generic.List<int>());
			}
			PlayerLeft.Value.Add(index);
		}

		public void PlayerJoinedChange(int index, global::Unity.Services.Lobbies.Models.Player player)
		{
			if (!PlayerJoined.Changed)
			{
				PlayerJoined = global::Unity.Services.Lobbies.LobbyValue.Added(new global::System.Collections.Generic.List<global::Unity.Services.Lobbies.LobbyPlayerJoined>());
			}
			PlayerJoined.Value.Add(new global::Unity.Services.Lobbies.LobbyPlayerJoined(index, player));
		}

		public void PlayerDataChange(int index, string key, global::Unity.Services.Lobbies.Models.PlayerDataObject playerDataObject)
		{
			global::Unity.Services.Lobbies.LobbyPlayerChanges lobbyPlayerChanges = PreparePlayerDataChange(index);
			if (!lobbyPlayerChanges.ChangedData.Changed)
			{
				lobbyPlayerChanges.ChangedData = global::Unity.Services.Lobbies.LobbyValue.ChangedNotRemoved(new global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::Unity.Services.Lobbies.Models.PlayerDataObject>>());
			}
			lobbyPlayerChanges.ChangedData.Value[key] = global::Unity.Services.Lobbies.LobbyValue.ChangedNotRemoved(playerDataObject);
		}

		public void PlayerDataAdded(int index, string key, global::Unity.Services.Lobbies.Models.PlayerDataObject playerDataObject)
		{
			global::Unity.Services.Lobbies.LobbyPlayerChanges lobbyPlayerChanges = PreparePlayerDataAddition(index);
			if (!lobbyPlayerChanges.ChangedData.Added)
			{
				lobbyPlayerChanges.ChangedData = global::Unity.Services.Lobbies.LobbyValue.ChangeAdded(new global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::Unity.Services.Lobbies.Models.PlayerDataObject>>());
			}
			lobbyPlayerChanges.ChangedData.Value[key] = global::Unity.Services.Lobbies.LobbyValue.ChangeAdded(playerDataObject);
		}

		public void PlayerDataRemoveChange(int index)
		{
			global::Unity.Services.Lobbies.LobbyPlayerChanges lobbyPlayerChanges = PreparePlayerDataChange(index);
			if (!lobbyPlayerChanges.ChangedData.Changed)
			{
				lobbyPlayerChanges.ChangedData = global::Unity.Services.Lobbies.LobbyValue.Removed<global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::Unity.Services.Lobbies.Models.PlayerDataObject>>>();
			}
		}

		public void PlayerDataRemoveChange(int index, string key)
		{
			global::Unity.Services.Lobbies.LobbyPlayerChanges lobbyPlayerChanges = PreparePlayerDataChange(index);
			if (!lobbyPlayerChanges.ChangedData.Changed)
			{
				lobbyPlayerChanges.ChangedData = global::Unity.Services.Lobbies.LobbyValue.ChangedNotRemoved(new global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::Unity.Services.Lobbies.Models.PlayerDataObject>>());
			}
			lobbyPlayerChanges.ChangedData.Value[key] = global::Unity.Services.Lobbies.LobbyValue.Removed<global::Unity.Services.Lobbies.Models.PlayerDataObject>();
		}

		public void PlayerConnectionInfoChange(int index, string connectionInfo)
		{
			PreparePlayerDataChange(index).ConnectionInfoChanged = global::Unity.Services.Lobbies.LobbyValue.Changed(connectionInfo);
		}

		public void PlayerLastUpdatedChange(int index, global::System.DateTime lastUpdated)
		{
			PreparePlayerDataChange(index).LastUpdatedChanged = global::Unity.Services.Lobbies.LobbyValue.Changed(lastUpdated);
		}

		public void ApplyToLobby(global::Unity.Services.Lobbies.Models.Lobby lobby)
		{
			LobbyPatcher.ApplyPatchesToLobby(this, lobby);
		}

		private global::Unity.Services.Lobbies.LobbyPlayerChanges PreparePlayerDataChange(int index)
		{
			if (!PlayerData.Changed)
			{
				PlayerData = global::Unity.Services.Lobbies.LobbyValue.Changed(new global::System.Collections.Generic.Dictionary<int, global::Unity.Services.Lobbies.LobbyPlayerChanges>());
			}
			if (!PlayerData.Value.TryGetValue(index, out var value))
			{
				value = new global::Unity.Services.Lobbies.LobbyPlayerChanges(index);
				PlayerData.Value[index] = value;
			}
			return value;
		}

		private global::Unity.Services.Lobbies.LobbyPlayerChanges PreparePlayerDataAddition(int index)
		{
			if (!PlayerData.Changed)
			{
				PlayerData = global::Unity.Services.Lobbies.LobbyValue.Added(new global::System.Collections.Generic.Dictionary<int, global::Unity.Services.Lobbies.LobbyPlayerChanges>());
			}
			if (!PlayerData.Value.TryGetValue(index, out var value))
			{
				value = new global::Unity.Services.Lobbies.LobbyPlayerChanges(index);
				PlayerData.Value[index] = value;
			}
			return value;
		}
	}
}
