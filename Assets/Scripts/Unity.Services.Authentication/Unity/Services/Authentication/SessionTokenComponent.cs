namespace Unity.Services.Authentication
{
	internal class SessionTokenComponent
	{
		private const string k_CacheKey = "session_token";

		private string m_SessionToken;

		private readonly global::Unity.Services.Authentication.IAuthenticationCache m_Cache;

		internal string SessionToken
		{
			get
			{
				return m_SessionToken;
			}
			set
			{
				SetSessionToken(value);
			}
		}

		internal SessionTokenComponent(global::Unity.Services.Authentication.IAuthenticationCache cache)
		{
			m_Cache = cache;
			m_SessionToken = GetSessionTokenFromCache();
		}

		internal void Clear()
		{
			SetSessionToken(null);
		}

		internal void Migrate()
		{
			m_Cache.Migrate("session_token");
		}

		internal void Refresh()
		{
			SetSessionToken(GetSessionTokenFromCache());
		}

		private string GetSessionTokenFromCache()
		{
			return m_Cache.GetString("session_token");
		}

		private void SetSessionToken(string sessionToken)
		{
			if (m_SessionToken != sessionToken)
			{
				m_SessionToken = sessionToken;
				if (m_SessionToken == null)
				{
					m_Cache.DeleteKey("session_token");
				}
				else
				{
					m_Cache.SetString("session_token", m_SessionToken);
				}
			}
		}
	}
}
