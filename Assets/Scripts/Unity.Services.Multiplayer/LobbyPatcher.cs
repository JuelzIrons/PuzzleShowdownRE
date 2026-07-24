internal static class LobbyPatcher
{
	internal class LobbyPatch
	{
		public string op;

		public string path;

		public object value;

		[global::UnityEngine.Scripting.Preserve]
		public LobbyPatch()
		{
		}
	}

	internal class LobbyPatches
	{
		public int Version;

		public global::System.Collections.Generic.List<LobbyPatcher.LobbyPatch> Patches;

		[global::UnityEngine.Scripting.Preserve]
		public LobbyPatches()
		{
		}
	}

	private const int MaxPlayerCount = 150;

	internal static void ApplyPatchesToLobby(global::Unity.Services.Lobbies.ILobbyChanges changes, global::Unity.Services.Lobbies.Models.Lobby lobbyToChange)
	{
		if (changes.Version.Value <= lobbyToChange.Version)
		{
			return;
		}
		if (changes.LobbyDeleted)
		{
			global::Unity.Services.Multiplayer.Logger.LogWarning("Attempting to apply changes to lobby, but the lobby has been deleted. Check if a lobby has been deleted by checking .LobbyDeleted");
			return;
		}
		if (changes.Name.Changed)
		{
			lobbyToChange.Name = changes.Name.Value;
		}
		if (changes.IsPrivate.Changed)
		{
			lobbyToChange.IsPrivate = changes.IsPrivate.Value;
		}
		if (changes.IsLocked.Changed)
		{
			lobbyToChange.IsLocked = changes.IsLocked.Value;
		}
		if (changes.HasPassword.Changed)
		{
			lobbyToChange.HasPassword = changes.HasPassword.Value;
		}
		if (changes.AvailableSlots.Changed)
		{
			lobbyToChange.AvailableSlots = changes.AvailableSlots.Value;
		}
		if (changes.MaxPlayers.Changed)
		{
			lobbyToChange.MaxPlayers = changes.MaxPlayers.Value;
		}
		if (changes.Data.Removed)
		{
			if (lobbyToChange.Data != null)
			{
				lobbyToChange.Data.Clear();
			}
		}
		else if (changes.Data.Changed)
		{
			if (lobbyToChange.Data == null)
			{
				lobbyToChange.Data = new global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.Models.DataObject>();
			}
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::Unity.Services.Lobbies.Models.DataObject>> item in changes.Data.Value)
			{
				if (item.Value.Removed)
				{
					lobbyToChange.Data.Remove(item.Key);
				}
				else
				{
					lobbyToChange.Data[item.Key] = item.Value.Value;
				}
			}
		}
		if (changes.PlayerLeft.Changed)
		{
			global::System.Collections.Generic.List<int> value = changes.PlayerLeft.Value;
			value.Sort((int first, int second) => second.CompareTo(first));
			foreach (int item2 in value)
			{
				lobbyToChange.Players.RemoveAt(item2);
			}
		}
		if (changes.PlayerJoined.Changed)
		{
			if (lobbyToChange.Players == null)
			{
				lobbyToChange.Players = new global::System.Collections.Generic.List<global::Unity.Services.Lobbies.Models.Player>(changes.PlayerJoined.Value.Count);
			}
			foreach (global::Unity.Services.Lobbies.LobbyPlayerJoined item3 in changes.PlayerJoined.Value)
			{
				lobbyToChange.Players.Insert(item3.PlayerIndex, item3.Player);
			}
		}
		if (changes.PlayerData.Changed)
		{
			foreach (global::System.Collections.Generic.KeyValuePair<int, global::Unity.Services.Lobbies.LobbyPlayerChanges> item4 in changes.PlayerData.Value)
			{
				global::Unity.Services.Lobbies.LobbyPlayerChanges value2 = item4.Value;
				int playerIndex = item4.Value.PlayerIndex;
				global::Unity.Services.Lobbies.Models.Player player = lobbyToChange.Players[playerIndex];
				if (value2.ConnectionInfoChanged.Changed)
				{
					player.ConnectionInfo = item4.Value.ConnectionInfoChanged.Value;
				}
				if (value2.LastUpdatedChanged.Changed)
				{
					player.LastUpdated = value2.LastUpdatedChanged.Value;
				}
				if (value2.ChangedData.Removed)
				{
					if (player.Data != null)
					{
						player.Data.Clear();
					}
				}
				else
				{
					if (!value2.ChangedData.Changed)
					{
						continue;
					}
					if (player.Data == null)
					{
						player.Data = new global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.Models.PlayerDataObject>();
					}
					foreach (global::System.Collections.Generic.KeyValuePair<string, global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::Unity.Services.Lobbies.Models.PlayerDataObject>> item5 in value2.ChangedData.Value)
					{
						if (item5.Value.Removed)
						{
							player.Data.Remove(item5.Key);
						}
						else
						{
							player.Data[item5.Key] = item5.Value.Value;
						}
					}
				}
			}
		}
		if (changes.Version.Changed)
		{
			lobbyToChange.Version = changes.Version.Value;
		}
		if (changes.HostId.Changed)
		{
			lobbyToChange.HostId = changes.HostId.Value;
		}
		if (changes.LastUpdated.Changed)
		{
			lobbyToChange.LastUpdated = changes.LastUpdated.Value;
		}
	}

	internal static global::Unity.Services.Lobbies.LobbyPatcherChanges GetLobbyDiff(global::Unity.Services.Lobbies.Models.Lobby lobby1, global::Unity.Services.Lobbies.Models.Lobby lobby2)
	{
		global::Unity.Services.Lobbies.LobbyPatcherChanges lobbyPatcherChanges = new global::Unity.Services.Lobbies.LobbyPatcherChanges(lobby1.Version);
		if (lobby2 == null)
		{
			lobbyPatcherChanges.LobbyDeletedChange();
			return lobbyPatcherChanges;
		}
		if (lobby1.Version == lobby2.Version)
		{
			return lobbyPatcherChanges;
		}
		global::Unity.Services.Lobbies.Models.Lobby lobby3 = ((lobby1.Version < lobby2.Version) ? lobby1 : lobby2);
		global::Unity.Services.Lobbies.Models.Lobby lobby4 = ((lobby1.Version > lobby2.Version) ? lobby1 : lobby2);
		lobbyPatcherChanges = new global::Unity.Services.Lobbies.LobbyPatcherChanges(lobby4.Version);
		if (lobby3.Name != null && !lobby3.Name.Equals(lobby4.Name))
		{
			lobbyPatcherChanges.NameChange(lobby4.Name);
		}
		if (lobby3.IsPrivate != lobby4.IsPrivate)
		{
			lobbyPatcherChanges.IsPrivateChange(lobby4.IsPrivate);
		}
		if (lobby3.IsLocked != lobby4.IsLocked)
		{
			lobbyPatcherChanges.IsLockedChange(lobby4.IsLocked);
		}
		if (lobby3.AvailableSlots != lobby4.AvailableSlots)
		{
			lobbyPatcherChanges.AvailableSlotsChange(lobby4.AvailableSlots);
		}
		if (lobby3.MaxPlayers != lobby4.MaxPlayers)
		{
			lobbyPatcherChanges.MaxPlayersChange(lobby4.MaxPlayers);
		}
		if (lobby3.HostId != null && !lobby3.HostId.Equals(lobby4.HostId))
		{
			lobbyPatcherChanges.HostChange(lobby4.HostId);
		}
		if (!lobby3.LastUpdated.Equals(lobby4.LastUpdated))
		{
			lobbyPatcherChanges.LastUpdatedChange(lobby4.LastUpdated);
		}
		if (lobby4.Data == null)
		{
			if (lobby3.Data != null)
			{
				lobbyPatcherChanges.DataRemoveChange();
			}
		}
		else
		{
			foreach (string key in lobby4.Data.Keys)
			{
				if (lobby3.Data == null || !lobby3.Data.ContainsKey(key) || lobby3.Data[key] == null)
				{
					lobbyPatcherChanges.DataAdded(key, lobby4.Data[key]);
				}
			}
			if (lobby3.Data != null)
			{
				foreach (string key2 in lobby3.Data.Keys)
				{
					if (!lobby4.Data.ContainsKey(key2) || lobby4.Data[key2] == null)
					{
						lobbyPatcherChanges.DataRemoveChange(key2);
					}
					else if (lobby3.Data[key2] == null)
					{
						lobbyPatcherChanges.DataAdded(key2, lobby4.Data[key2]);
					}
					else if (!IsLobbyDataEqual(lobby3.Data[key2], lobby4.Data[key2]))
					{
						lobbyPatcherChanges.DataChange(key2, lobby4.Data[key2]);
					}
				}
			}
		}
		if (lobby4.Players == null || lobby4.Players.Count == 0)
		{
			if (lobby3.Players != null)
			{
				for (int num = lobby3.Players.Count - 1; num >= 0; num--)
				{
					lobbyPatcherChanges.PlayerLeftChange(num);
				}
			}
			return lobbyPatcherChanges;
		}
		global::System.Collections.Generic.List<global::Unity.Services.Lobbies.Models.Player> list = null;
		global::Unity.Services.Lobbies.Models.Lobby lobby5 = lobby3;
		if (lobby5.Players == null)
		{
			global::System.Collections.Generic.List<global::Unity.Services.Lobbies.Models.Player> list2 = (lobby5.Players = new global::System.Collections.Generic.List<global::Unity.Services.Lobbies.Models.Player>());
		}
		lobby5 = lobby4;
		if (lobby5.Players == null)
		{
			global::System.Collections.Generic.List<global::Unity.Services.Lobbies.Models.Player> list2 = (lobby5.Players = new global::System.Collections.Generic.List<global::Unity.Services.Lobbies.Models.Player>());
		}
		global::System.Collections.Generic.Dictionary<string, int> dictionary = new global::System.Collections.Generic.Dictionary<string, int>(lobby3.Players.Count);
		for (int i = 0; i < lobby3.Players.Count; i++)
		{
			dictionary.Add(lobby3.Players[i].Id, i);
		}
		global::System.Collections.Generic.Dictionary<string, int> dictionary2 = new global::System.Collections.Generic.Dictionary<string, int>(lobby4.Players.Count);
		for (int j = 0; j < lobby4.Players.Count; j++)
		{
			dictionary2.Add(lobby4.Players[j].Id, j);
		}
		if (dictionary.Count != 0)
		{
			list = new global::System.Collections.Generic.List<global::Unity.Services.Lobbies.Models.Player>(lobby3.Players);
			for (int num2 = lobby3.Players.Count - 1; num2 >= 0; num2--)
			{
				global::Unity.Services.Lobbies.Models.Player player = lobby3.Players[num2];
				if (!dictionary2.ContainsKey(player.Id) || dictionary2[player.Id] > dictionary[player.Id])
				{
					lobbyPatcherChanges.PlayerLeftChange(num2);
					list.RemoveAt(num2);
				}
				else
				{
					int index = dictionary2[player.Id];
					global::Unity.Services.Lobbies.Models.Player player2 = lobby4.Players[index];
					if (player.LastUpdated != player2.LastUpdated)
					{
						lobbyPatcherChanges.PlayerLastUpdatedChange(index, player2.LastUpdated);
					}
					if (player.ConnectionInfo != player2.ConnectionInfo)
					{
						lobbyPatcherChanges.PlayerConnectionInfoChange(index, player2.ConnectionInfo);
					}
					if (player.Data != null && player2.Data == null)
					{
						lobbyPatcherChanges.PlayerDataRemoveChange(index);
					}
					else if (player.Data == null && player2.Data != null)
					{
						foreach (string key3 in player2.Data.Keys)
						{
							lobbyPatcherChanges.PlayerDataAdded(index, key3, player2.Data[key3]);
						}
					}
					else if (player.Data != null && player2.Data != null)
					{
						foreach (string key4 in player2.Data.Keys)
						{
							if (!player.Data.ContainsKey(key4) || player.Data[key4] == null)
							{
								lobbyPatcherChanges.PlayerDataAdded(index, key4, player2.Data[key4]);
							}
						}
						using (player.Data.Keys.GetEnumerator())
						{
							foreach (string key5 in player.Data.Keys)
							{
								if (!player2.Data.ContainsKey(key5) || player2.Data[key5] == null)
								{
									lobbyPatcherChanges.PlayerDataRemoveChange(index, key5);
								}
								else if (player.Data[key5] == null)
								{
									lobbyPatcherChanges.PlayerDataAdded(index, key5, player2.Data[key5]);
								}
								else if (!IsPlayerDataEqual(player.Data[key5], player2.Data[key5]))
								{
									lobbyPatcherChanges.PlayerDataChange(index, key5, player2.Data[key5]);
								}
							}
						}
					}
				}
			}
		}
		foreach (string key6 in dictionary2.Keys)
		{
			int num3 = dictionary2[key6];
			if (!dictionary.TryGetValue(key6, out var value))
			{
				lobbyPatcherChanges.PlayerJoinedChange(num3, lobby4.Players[num3]);
			}
			else if ((num3 >= lobby3.Players.Count || !(lobby3.Players[num3].Id == lobby4.Players[num3].Id)) && value <= num3)
			{
				lobbyPatcherChanges.PlayerJoinedChange(num3, lobby4.Players[num3]);
			}
		}
		return lobbyPatcherChanges;
	}

	private static bool IsLobbyDataEqual(global::Unity.Services.Lobbies.Models.DataObject d1, global::Unity.Services.Lobbies.Models.DataObject d2)
	{
		if (d1.Value == d2.Value && d1.Index == d2.Index)
		{
			return d1.Visibility == d2.Visibility;
		}
		return false;
	}

	private static bool IsPlayerDataEqual(global::Unity.Services.Lobbies.Models.PlayerDataObject d1, global::Unity.Services.Lobbies.Models.PlayerDataObject d2)
	{
		if (d1.Value == d2.Value)
		{
			return d1.Visibility == d2.Visibility;
		}
		return false;
	}

	internal static global::Unity.Services.Lobbies.LobbyPatcherChanges GetLobbyChanges(string json)
	{
		if (string.IsNullOrWhiteSpace(json))
		{
			global::Unity.Services.Multiplayer.Logger.LogError("Unable to apply patches to lobby as the provided JSON was null!");
		}
		LobbyPatcher.LobbyPatches? lobbyPatches = global::Newtonsoft.Json.JsonConvert.DeserializeObject<LobbyPatcher.LobbyPatches>(json);
		if (lobbyPatches == null)
		{
			global::Unity.Services.Multiplayer.Logger.LogError("Unable to deserialize JSON to LobbyPatches!");
		}
		return GetLobbyPatches(lobbyPatches);
	}

	internal static global::Unity.Services.Lobbies.LobbyPatcherChanges GetLobbyPatches(LobbyPatcher.LobbyPatches lobbyPatches)
	{
		if (lobbyPatches.Patches == null || lobbyPatches.Patches.Count < 1)
		{
			global::Unity.Services.Multiplayer.Logger.LogWarning("Attempting to apply patches to lobby, but there were no patches to apply.");
			return new global::Unity.Services.Lobbies.LobbyPatcherChanges(lobbyPatches.Version);
		}
		global::Unity.Services.Lobbies.LobbyPatcherChanges lobbyPatcherChanges = new global::Unity.Services.Lobbies.LobbyPatcherChanges(lobbyPatches.Version);
		foreach (LobbyPatcher.LobbyPatch patch in lobbyPatches.Patches)
		{
			ParseLobbyPatch(patch, lobbyPatcherChanges);
		}
		return lobbyPatcherChanges;
	}

	private static void ParseLobbyPatch(LobbyPatcher.LobbyPatch patch, global::Unity.Services.Lobbies.LobbyPatcherChanges changes)
	{
		switch (patch.op)
		{
		case "add":
			ParseAddPatch(patch, changes);
			break;
		case "replace":
			ParseReplacePatch(patch, changes);
			break;
		case "remove":
			ParseRemovePatch(patch, changes);
			break;
		default:
			global::Unity.Services.Multiplayer.Logger.LogError("patch.op(" + patch.op + ") is not implemented by the LobbyPatcher");
			break;
		}
	}

	private static void ParseAddPatch(LobbyPatcher.LobbyPatch patch, global::Unity.Services.Lobbies.LobbyPatcherChanges changes)
	{
		if (patch.path.StartsWith("/data/"))
		{
			ParseLobbyDataAddOrReplacePatch(patch, changes);
			return;
		}
		if (patch.path.StartsWith("/players/"))
		{
			ParsePlayerAddPatch(patch, changes);
			return;
		}
		switch (patch.path)
		{
		case "/name":
			changes.NameChange((string)patch.value);
			break;
		case "/isPrivate":
			changes.IsPrivateChange((bool)patch.value);
			break;
		case "/isLocked":
			changes.IsLockedChange((bool)patch.value);
			break;
		case "/hasPassword":
			changes.HasPasswordChange((bool)patch.value);
			break;
		case "/availableSlots":
			changes.AvailableSlotsChange((int)(long)patch.value);
			break;
		case "/maxPlayers":
			changes.MaxPlayersChange((int)(long)patch.value);
			break;
		case "/data":
			ParseAddLobbyData((global::Newtonsoft.Json.Linq.JObject)patch.value, changes);
			break;
		case "/hostId":
			changes.HostChange((string)patch.value);
			break;
		case "/lastUpdated":
			changes.LastUpdatedChange((global::System.DateTime)patch.value);
			break;
		}
	}

	private static void ParseReplacePatch(LobbyPatcher.LobbyPatch patch, global::Unity.Services.Lobbies.LobbyPatcherChanges changes)
	{
		if (patch.path.StartsWith("/data/"))
		{
			ParseLobbyDataAddOrReplacePatch(patch, changes);
			return;
		}
		if (patch.path.StartsWith("/players/"))
		{
			ParsePlayerReplacePatch(patch, changes);
			return;
		}
		switch (patch.path)
		{
		case "/name":
			changes.NameChange((string)patch.value);
			break;
		case "/isPrivate":
			changes.IsPrivateChange((bool)patch.value);
			break;
		case "/isLocked":
			changes.IsLockedChange((bool)patch.value);
			break;
		case "/hasPassword":
			changes.HasPasswordChange((bool)patch.value);
			break;
		case "/availableSlots":
			changes.AvailableSlotsChange((int)(long)patch.value);
			break;
		case "/maxPlayers":
			changes.MaxPlayersChange((int)(long)patch.value);
			break;
		case "/hostId":
			changes.HostChange((string)patch.value);
			break;
		case "/lastUpdated":
			changes.LastUpdatedChange((global::System.DateTime)patch.value);
			break;
		}
	}

	private static void ParseRemovePatch(LobbyPatcher.LobbyPatch patch, global::Unity.Services.Lobbies.LobbyPatcherChanges changes)
	{
		if (patch.path.StartsWith("/data/"))
		{
			ParseLobbyDataRemovePatch(patch, changes);
			return;
		}
		if (patch.path.StartsWith("/players/"))
		{
			ParsePlayerRemovePatch(patch, changes);
			return;
		}
		switch (patch.path)
		{
		case "/name":
			changes.NameChange(null);
			break;
		case "/isPrivate":
			changes.IsPrivateChange(isPrivate: false);
			break;
		case "/isLocked":
			changes.IsLockedChange(isLocked: false);
			break;
		case "/hasPassword":
			changes.HasPasswordChange(hasPassword: false);
			break;
		case "/availableSlots":
			changes.AvailableSlotsChange(0);
			break;
		case "/maxPlayers":
			changes.MaxPlayersChange(150);
			break;
		case "/data":
			ParseLobbyDataRemovePatch(patch, changes);
			break;
		case "/hostId":
			changes.HostChange(null);
			break;
		case "/":
			changes.LobbyDeletedChange();
			break;
		}
	}

	private static void ParseAddLobbyData(global::Newtonsoft.Json.Linq.JObject data, global::Unity.Services.Lobbies.LobbyPatcherChanges changes)
	{
		foreach (global::System.Collections.Generic.KeyValuePair<string, global::Newtonsoft.Json.Linq.JToken> datum in data)
		{
			changes.DataAdded(datum.Key, datum.Value.ToObject<global::Unity.Services.Lobbies.Models.DataObject>());
		}
	}

	private static void ParseLobbyDataAddOrReplacePatch(LobbyPatcher.LobbyPatch patch, global::Unity.Services.Lobbies.LobbyPatcherChanges changes)
	{
		int length = "/data/".Length;
		int num = patch.path.IndexOf('/', length);
		string key = ((num < 0) ? patch.path.Substring(length) : patch.path.Substring(num));
		global::Newtonsoft.Json.Linq.JObject jObject = (global::Newtonsoft.Json.Linq.JObject)patch.value;
		changes.DataChange(key, jObject.ToObject<global::Unity.Services.Lobbies.Models.DataObject>());
	}

	private static void ParseLobbyDataRemovePatch(LobbyPatcher.LobbyPatch patch, global::Unity.Services.Lobbies.LobbyPatcherChanges changes)
	{
		if (patch.path == "/data")
		{
			changes.DataRemoveChange();
			return;
		}
		string key = patch.path.Substring("/data/".Length);
		changes.DataRemoveChange(key);
	}

	private static string GetPlayerPathAndIndex(LobbyPatcher.LobbyPatch patch, out int playerIndex)
	{
		string[] array = patch.path.Split('/');
		if (!int.TryParse(array[2], out playerIndex))
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			string[] array2 = array;
			foreach (string text in array2)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("\"" + text + "\"");
			}
			throw new global::System.InvalidOperationException($"Unable to parse section[{array[2]}] from sections[{stringBuilder}]");
		}
		if (array.Length <= 3)
		{
			return "/" + array[1];
		}
		return patch.path.Substring(array[1].Length + array[2].Length + 2);
	}

	private static void ParsePlayerAddPatch(LobbyPatcher.LobbyPatch patch, global::Unity.Services.Lobbies.LobbyPatcherChanges changes)
	{
		int playerIndex;
		string playerPathAndIndex = GetPlayerPathAndIndex(patch, out playerIndex);
		if (playerPathAndIndex.StartsWith("/data"))
		{
			ParseAddOrReplacePlayerData(playerIndex, patch, playerPathAndIndex, changes, isAdding: true);
		}
		else if (!(playerPathAndIndex == "/players"))
		{
			if (playerPathAndIndex == "/connectionInfo")
			{
				changes.PlayerConnectionInfoChange(playerIndex, (string)patch.value);
				return;
			}
			global::Unity.Services.Multiplayer.Logger.LogError("Not implemented add player patch with path[" + playerPathAndIndex + "] from player patch[" + patch.path + "]");
		}
		else
		{
			ParseAddPlayer(patch, playerIndex, changes);
		}
	}

	private static void ParsePlayerReplacePatch(LobbyPatcher.LobbyPatch patch, global::Unity.Services.Lobbies.LobbyPatcherChanges changes)
	{
		int playerIndex;
		string playerPathAndIndex = GetPlayerPathAndIndex(patch, out playerIndex);
		if (playerPathAndIndex.StartsWith("/data"))
		{
			ParseAddOrReplacePlayerData(playerIndex, patch, playerPathAndIndex, changes);
		}
		else if (!(playerPathAndIndex == "/connectionInfo"))
		{
			if (playerPathAndIndex == "/lastUpdated")
			{
				changes.PlayerLastUpdatedChange(playerIndex, (global::System.DateTime)patch.value);
			}
			else
			{
				global::Unity.Services.Multiplayer.Logger.LogError("Not implemented replace player patch with path[" + playerPathAndIndex + "]");
			}
		}
		else
		{
			changes.PlayerConnectionInfoChange(playerIndex, (string)patch.value);
		}
	}

	private static void ParsePlayerRemovePatch(LobbyPatcher.LobbyPatch patch, global::Unity.Services.Lobbies.LobbyPatcherChanges changes)
	{
		int playerIndex;
		string playerPathAndIndex = GetPlayerPathAndIndex(patch, out playerIndex);
		if (playerPathAndIndex.StartsWith("/data"))
		{
			ParseRemovePlayerData(playerIndex, patch, playerPathAndIndex, changes);
		}
		else if (!(playerPathAndIndex == "/players"))
		{
			if (playerPathAndIndex == "/connectionInfo")
			{
				changes.PlayerConnectionInfoChange(playerIndex, null);
			}
			else
			{
				global::Unity.Services.Multiplayer.Logger.LogError("Not implemented remove player patch with path[" + playerPathAndIndex + "]");
			}
		}
		else
		{
			changes.PlayerLeftChange(playerIndex);
		}
	}

	private static void ParseAddPlayer(LobbyPatcher.LobbyPatch patch, int index, global::Unity.Services.Lobbies.LobbyPatcherChanges changes)
	{
		global::Unity.Services.Lobbies.Models.Player player = ((global::Newtonsoft.Json.Linq.JObject)patch.value).ToObject<global::Unity.Services.Lobbies.Models.Player>();
		changes.PlayerJoinedChange(index, player);
	}

	private static void ParseAddOrReplacePlayerData(int index, LobbyPatcher.LobbyPatch patch, string path, global::Unity.Services.Lobbies.LobbyPatcherChanges changes, bool isAdding = false)
	{
		if (path == "/data")
		{
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::Newtonsoft.Json.Linq.JToken> item in (global::Newtonsoft.Json.Linq.JObject)patch.value)
			{
				if (isAdding)
				{
					changes.PlayerDataAdded(index, item.Key, item.Value.ToObject<global::Unity.Services.Lobbies.Models.PlayerDataObject>());
				}
				else
				{
					changes.PlayerDataChange(index, item.Key, item.Value.ToObject<global::Unity.Services.Lobbies.Models.PlayerDataObject>());
				}
			}
			return;
		}
		string key = patch.path.Split('/')[4];
		global::Newtonsoft.Json.Linq.JObject jObject = (global::Newtonsoft.Json.Linq.JObject)patch.value;
		if (isAdding)
		{
			changes.PlayerDataAdded(index, key, jObject.ToObject<global::Unity.Services.Lobbies.Models.PlayerDataObject>());
		}
		else
		{
			changes.PlayerDataChange(index, key, jObject.ToObject<global::Unity.Services.Lobbies.Models.PlayerDataObject>());
		}
	}

	private static void ParseRemovePlayerData(int index, LobbyPatcher.LobbyPatch patch, string path, global::Unity.Services.Lobbies.LobbyPatcherChanges changes)
	{
		if (path == "/data")
		{
			changes.PlayerDataRemoveChange(index);
			return;
		}
		string key = path.Substring("/data/".Length);
		changes.PlayerDataRemoveChange(index, key);
	}
}
