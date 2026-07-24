namespace Unity.Services.Authentication
{
	internal class AccessTokenComponent : global::Unity.Services.Authentication.Internal.IAccessToken, global::Unity.Services.Core.Internal.IServiceComponent, global::Unity.Services.Authentication.Internal.IAccessTokenObserver
	{
		private string m_AccessToken;

		public string AccessToken
		{
			get
			{
				return m_AccessToken;
			}
			internal set
			{
				SetAccessToken(value);
			}
		}

		public global::System.DateTime? RefreshTime { get; internal set; }

		public global::System.DateTime? ExpiryTime { get; internal set; }

		public event global::System.Action<string> AccessTokenChanged;

		internal AccessTokenComponent()
		{
		}

		internal void Clear()
		{
			AccessToken = null;
			ExpiryTime = null;
		}

		private void SetAccessToken(string accessToken)
		{
			if (m_AccessToken != accessToken)
			{
				m_AccessToken = accessToken;
				try
				{
					this.AccessTokenChanged?.Invoke(m_AccessToken);
				}
				catch (global::System.Exception exception)
				{
					global::Unity.Services.Authentication.Logger.LogException(exception);
				}
			}
		}
	}
}
