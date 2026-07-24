namespace Unity.Services.Authentication
{
	internal class PlayerIdComponent : global::Unity.Services.Authentication.Internal.IPlayerId, global::Unity.Services.Core.Internal.IServiceComponent
	{
		private const string k_CacheKey = "player_id";

		private string m_PlayerId;

		private readonly global::Unity.Services.Authentication.IAuthenticationCache m_Cache;

		public string PlayerId
		{
			get
			{
				return m_PlayerId;
			}
			internal set
			{
				SetPlayerId(value);
			}
		}

		public event global::System.Action<string> PlayerIdChanged;

		internal PlayerIdComponent(global::Unity.Services.Authentication.IAuthenticationCache cache)
		{
			m_Cache = cache;
			m_PlayerId = GetPlayerIdFromCache();
		}

		internal void Clear()
		{
			SetPlayerId(null);
		}

		internal void Refresh()
		{
			SetPlayerId(GetPlayerIdFromCache());
		}

		private string GetPlayerIdFromCache()
		{
			return m_Cache.GetString("player_id");
		}

		private void SetPlayerId(string playerId)
		{
			if (PlayerId != playerId)
			{
				m_PlayerId = playerId;
				if (m_PlayerId == null)
				{
					m_Cache.DeleteKey("player_id");
				}
				else
				{
					m_Cache.SetString("player_id", m_PlayerId);
				}
				try
				{
					this.PlayerIdChanged?.Invoke(m_PlayerId);
				}
				catch (global::System.Exception exception)
				{
					global::Unity.Services.Authentication.Logger.LogException(exception);
				}
			}
		}
	}
}
