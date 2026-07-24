namespace Lobbies.SDK.LobbyCacher
{
	internal class LobbyCacher : global::Unity.Services.Lobbies.ILobbyEvents
	{
		private bool m_IsSubscribed;

		private readonly global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.Models.Lobby> m_LobbyCacher;

		private readonly string m_PlayerID;

		public LobbyEventCallbacks Callbacks { get; private set; }

		public LobbyCacher(string playerID = "")
		{
			m_LobbyCacher = new global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.Models.Lobby>();
			m_PlayerID = playerID;
		}

		public global::Lobbies.SDK.LobbyCacher.LobbyCacher WithEventSubscription(LobbyEventCallbacks lobbyCallbacks)
		{
			Callbacks = lobbyCallbacks;
			m_IsSubscribed = true;
			return this;
		}

		public global::System.Threading.Tasks.Task SubscribeAsync()
		{
			if (Callbacks == null)
			{
				throw new global::System.InvalidOperationException("No callbacks set for lobby events. Set callbacks using WithEventSubscription before subscribing.");
			}
			m_IsSubscribed = true;
			return global::System.Threading.Tasks.Task.CompletedTask;
		}

		public global::System.Threading.Tasks.Task UnsubscribeAsync()
		{
			m_IsSubscribed = false;
			return global::System.Threading.Tasks.Task.CompletedTask;
		}

		public bool TryGetLobbyCache(string lobbyId, out global::Unity.Services.Lobbies.Models.Lobby cachedLobby)
		{
			return m_LobbyCacher.TryGetValue(lobbyId, out cachedLobby);
		}

		public string GetIfMatchTag(string lobbyId, bool applyIfMatch = true)
		{
			if (!applyIfMatch)
			{
				return null;
			}
			return GetLobbyCacheVersion(lobbyId);
		}

		public string GetLobbyCacheVersion(string lobbyId)
		{
			if (!TryGetLobbyCache(lobbyId, out var cachedLobby))
			{
				return null;
			}
			return cachedLobby.Version.ToString();
		}

		public bool RemoveLobbyCache(string lobbyId)
		{
			return m_LobbyCacher.Remove(lobbyId);
		}

		public bool UpdateLobbyCache(string lobbyId, global::Unity.Services.Lobbies.Models.Lobby newLobby)
		{
			if (!TryGetLobbyCache(lobbyId, out var cachedLobby))
			{
				return false;
			}
			return UpdateLobbyCache(lobbyId, LobbyPatcher.GetLobbyDiff(cachedLobby, newLobby));
		}

		public bool UpdateLobbyCache(string lobbyId, global::Unity.Services.Lobbies.ILobbyChanges changes)
		{
			if (!TryGetLobbyCache(lobbyId, out var cachedLobby))
			{
				return false;
			}
			if (changes.LobbyDeleted)
			{
				bool num = RemoveLobbyCache(lobbyId);
				if (num && m_IsSubscribed)
				{
					LobbyEventCallbacks callbacks = Callbacks;
					if (callbacks == null)
					{
						return num;
					}
					callbacks.InvokeLobbyChanged(changes);
				}
				return num;
			}
			if (cachedLobby.Version < changes.Version.Value)
			{
				if (WasRemovedFromLobby(changes, cachedLobby))
				{
					Callbacks?.InvokeKickedFromLobby();
					return true;
				}
				changes.ApplyToLobby(cachedLobby);
				Callbacks?.InvokeLobbyChanged(changes);
			}
			return true;
		}

		public bool AddLobbyCache(string lobbyId, global::Unity.Services.Lobbies.Models.Lobby lobby)
		{
			if (m_LobbyCacher.ContainsKey(lobbyId))
			{
				return false;
			}
			m_LobbyCacher[lobbyId] = lobby;
			return true;
		}

		private bool WasRemovedFromLobby(global::Unity.Services.Lobbies.ILobbyChanges changes, global::Unity.Services.Lobbies.Models.Lobby cachedLobby)
		{
			if (cachedLobby == null || !changes.PlayerLeft.Changed)
			{
				return false;
			}
			if (changes.PlayerLeft.Value.Exists((int playerIdx) => cachedLobby.Players[playerIdx].Id == m_PlayerID) && (changes.PlayerJoined.Value == null || !changes.PlayerJoined.Value.Exists((global::Unity.Services.Lobbies.LobbyPlayerJoined joinEvt) => joinEvt.Player.Id == m_PlayerID)))
			{
				RemoveLobbyCache(cachedLobby.Id);
				return true;
			}
			return false;
		}

		public void ResetLobby(string lobbyId)
		{
			m_LobbyCacher.Remove(lobbyId);
		}
	}
}
