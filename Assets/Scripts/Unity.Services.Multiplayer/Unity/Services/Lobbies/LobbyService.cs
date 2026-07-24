namespace Unity.Services.Lobbies
{
	public static class LobbyService
	{
		private static readonly global::Unity.Services.Lobbies.Configuration configuration;

		private static global::Unity.Services.Lobbies.ILobbyService service { get; set; }

		public static global::Unity.Services.Lobbies.ILobbyService Instance
		{
			get
			{
				if (service == null)
				{
					throw new global::System.InvalidOperationException("Unable to get ILobbyServiceSdk because Lobby API is not initialized. Make sure you call UnityServices.InitializeAsync().");
				}
				return service;
			}
			internal set
			{
				service = value;
			}
		}

		static LobbyService()
		{
			configuration = new global::Unity.Services.Lobbies.Configuration("https://lobby.services.api.unity.com/v1", 10, 4, null);
		}
	}
}
