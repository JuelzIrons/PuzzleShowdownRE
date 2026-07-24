namespace Unity.Services.Authentication
{
	internal class PlayerNameComponent : global::Unity.Services.Authentication.Internal.IPlayerName, global::Unity.Services.Authentication.Internal.IPlayerNameComponent, global::Unity.Services.Core.Internal.IServiceComponent
	{
		private const string k_CacheKey = "player_name";

		private string m_PlayerName;

		private readonly global::Unity.Services.Authentication.IAuthenticationCache m_Cache;

		public string PlayerName
		{
			get
			{
				return m_PlayerName;
			}
			internal set
			{
				SetPlayerName(value);
			}
		}

		public event global::System.Action<string> PlayerNameChanged;

		internal PlayerNameComponent(global::Unity.Services.Authentication.IAuthenticationCache cache)
		{
			m_Cache = cache;
			m_PlayerName = GetPlayerNameFromCache();
		}

		internal void Clear()
		{
			SetPlayerName(null);
		}

		internal void Refresh()
		{
			SetPlayerName(GetPlayerNameFromCache());
		}

		private string GetPlayerNameFromCache()
		{
			return m_Cache.GetString("player_name");
		}

		private void SetPlayerName(string playerName)
		{
			if (PlayerName != playerName)
			{
				m_PlayerName = playerName;
				if (m_PlayerName == null)
				{
					m_Cache.DeleteKey("player_name");
				}
				else
				{
					m_Cache.SetString("player_name", m_PlayerName);
				}
				try
				{
					this.PlayerNameChanged?.Invoke(playerName);
				}
				catch (global::System.Exception exception)
				{
					global::Unity.Services.Authentication.Logger.LogException(exception);
				}
			}
		}
	}
}
