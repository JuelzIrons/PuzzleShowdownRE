namespace Unity.Services.Multiplayer
{
	internal class InternalServiceID : global::Unity.Services.Multiplayer.IServiceID
	{
		private readonly global::Unity.Services.Authentication.Internal.IAccessToken m_AccessToken;

		private string m_ServiceID;

		public string ServiceID => GetServiceID();

		public InternalServiceID(global::Unity.Services.Authentication.Internal.IAccessToken accessToken)
		{
			m_AccessToken = accessToken;
		}

		private string GetServiceID()
		{
			if (m_AccessToken?.AccessToken == null)
			{
				global::Unity.Services.Multiplayer.Logger.LogError("error: cannot get ServiceID until the server is authenticated");
				return null;
			}
			if (string.IsNullOrEmpty(m_ServiceID))
			{
				m_ServiceID = new global::Unity.Services.Multiplayer.JwtDecoder(new global::Unity.Services.Multiplayer.DateTimeWrapper()).Decode<global::Unity.Services.Multiplayer.ServerAccessToken>(m_AccessToken.AccessToken).Subject;
			}
			return m_ServiceID;
		}
	}
}
