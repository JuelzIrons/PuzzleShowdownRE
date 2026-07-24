public class LobbyEventCallbacks
{
	public event global::System.Action<global::Unity.Services.Lobbies.ILobbyChanges> LobbyChanged;

	public event global::System.Action<global::System.Collections.Generic.List<global::Unity.Services.Lobbies.LobbyPlayerJoined>> PlayerJoined;

	public event global::System.Action<global::System.Collections.Generic.List<int>> PlayerLeft;

	public event global::System.Action<global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::Unity.Services.Lobbies.Models.DataObject>>> DataChanged;

	public event global::System.Action<global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::Unity.Services.Lobbies.Models.DataObject>>> DataRemoved;

	public event global::System.Action<global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::Unity.Services.Lobbies.Models.DataObject>>> DataAdded;

	public event global::System.Action<global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::Unity.Services.Lobbies.Models.PlayerDataObject>>>> PlayerDataChanged;

	public event global::System.Action<global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::Unity.Services.Lobbies.Models.PlayerDataObject>>>> PlayerDataRemoved;

	public event global::System.Action<global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::Unity.Services.Lobbies.Models.PlayerDataObject>>>> PlayerDataAdded;

	public event global::System.Action LobbyDeleted;

	public event global::System.Action KickedFromLobby;

	public event global::System.Action<global::Unity.Services.Lobbies.LobbyEventConnectionState> LobbyEventConnectionStateChanged;

	internal void InvokeLobbyChanged(global::Unity.Services.Lobbies.ILobbyChanges changes)
	{
		this.LobbyChanged?.Invoke(changes);
		if (changes.LobbyDeleted)
		{
			this.LobbyDeleted?.Invoke();
		}
		if (changes.PlayerJoined.Changed || changes.PlayerJoined.Added)
		{
			this.PlayerJoined?.Invoke(changes.PlayerJoined.Value);
		}
		if (changes.PlayerLeft.Changed || changes.PlayerLeft.Added)
		{
			this.PlayerLeft?.Invoke(changes.PlayerLeft.Value);
		}
		if (changes.Data.Added || changes.Data.Changed || changes.Data.Removed)
		{
			global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::Unity.Services.Lobbies.Models.DataObject>> dictionary = new global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::Unity.Services.Lobbies.Models.DataObject>>();
			global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::Unity.Services.Lobbies.Models.DataObject>> dictionary2 = new global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::Unity.Services.Lobbies.Models.DataObject>>();
			global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::Unity.Services.Lobbies.Models.DataObject>> dictionary3 = new global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::Unity.Services.Lobbies.Models.DataObject>>();
			if (changes.Data.Value == null)
			{
				this.DataRemoved?.Invoke(null);
				return;
			}
			foreach (string key in changes.Data.Value.Keys)
			{
				if (changes.Data.Added || changes.Data.Value[key].Added)
				{
					dictionary3.Add(key, changes.Data.Value[key]);
				}
				else if (changes.Data.Removed || changes.Data.Value[key].Removed)
				{
					dictionary2.Add(key, changes.Data.Value[key]);
				}
				else if (changes.Data.Changed && changes.Data.Value[key].Changed)
				{
					dictionary.Add(key, changes.Data.Value[key]);
				}
			}
			if (dictionary.Count > 0)
			{
				this.DataChanged?.Invoke(dictionary);
			}
			if (dictionary2.Count > 0)
			{
				this.DataRemoved?.Invoke(dictionary2);
			}
			if (dictionary3.Count > 0)
			{
				this.DataAdded?.Invoke(dictionary3);
			}
		}
		if (!changes.PlayerData.Added && !changes.PlayerData.Changed)
		{
			return;
		}
		global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::Unity.Services.Lobbies.Models.PlayerDataObject>>> dictionary4 = new global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::Unity.Services.Lobbies.Models.PlayerDataObject>>>();
		global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::Unity.Services.Lobbies.Models.PlayerDataObject>>> dictionary5 = new global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::Unity.Services.Lobbies.Models.PlayerDataObject>>>();
		global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::Unity.Services.Lobbies.Models.PlayerDataObject>>> dictionary6 = new global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::Unity.Services.Lobbies.Models.PlayerDataObject>>>();
		foreach (global::System.Collections.Generic.KeyValuePair<int, global::Unity.Services.Lobbies.LobbyPlayerChanges> item in changes.PlayerData.Value)
		{
			if (item.Value != null && item.Value.ChangedData.ChangeType == global::Unity.Services.Lobbies.LobbyValueChangeType.Unchanged)
			{
				continue;
			}
			if (item.Value == null || item.Value.ChangedData.Value == null)
			{
				dictionary5.Add(item.Key, null);
				continue;
			}
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::Unity.Services.Lobbies.Models.PlayerDataObject>> item2 in item.Value.ChangedData.Value)
			{
				if (item2.Value.Added)
				{
					if (!dictionary6.ContainsKey(item.Key))
					{
						dictionary6.Add(item.Key, new global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::Unity.Services.Lobbies.Models.PlayerDataObject>>());
					}
					dictionary6[item.Key].Add(item2.Key, item2.Value);
				}
				else if (item2.Value.Removed)
				{
					if (!dictionary5.ContainsKey(item.Key))
					{
						dictionary5.Add(item.Key, new global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::Unity.Services.Lobbies.Models.PlayerDataObject>>());
					}
					dictionary5[item.Key].Add(item2.Key, item2.Value);
				}
				else if (item2.Value.Changed)
				{
					if (!dictionary4.ContainsKey(item.Key))
					{
						dictionary4.Add(item.Key, new global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<global::Unity.Services.Lobbies.Models.PlayerDataObject>>());
					}
					dictionary4[item.Key].Add(item2.Key, item2.Value);
				}
			}
		}
		if (dictionary4.Count > 0)
		{
			this.PlayerDataChanged?.Invoke(dictionary4);
		}
		if (dictionary5.Count > 0)
		{
			this.PlayerDataRemoved?.Invoke(dictionary5);
		}
		if (dictionary6.Count > 0)
		{
			this.PlayerDataAdded?.Invoke(dictionary6);
		}
	}

	internal void InvokeKickedFromLobby()
	{
		this.KickedFromLobby?.Invoke();
	}

	internal void InvokeLobbyEventConnectionStateChanged(global::Unity.Services.Lobbies.LobbyEventConnectionState state)
	{
		this.LobbyEventConnectionStateChanged?.Invoke(state);
	}
}
