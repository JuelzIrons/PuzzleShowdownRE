namespace Unity.Services.Lobbies
{
	internal class InternalLobbyService : global::Unity.Services.Lobbies.ILobbyServiceSdk
	{
		public global::Unity.Services.Lobbies.Apis.Lobby.ILobbyApiClient LobbyApi { get; set; }

		public global::Unity.Services.Lobbies.Configuration Configuration { get; set; }

		public global::Unity.Services.Wire.Internal.IWire Wire { get; set; }

		public global::Unity.Services.Core.Telemetry.Internal.IMetrics Metrics { get; }

		public InternalLobbyService(global::Unity.Services.Lobbies.Http.HttpClient httpClient, global::Unity.Services.Authentication.Internal.IAccessToken accessToken = null, global::Unity.Services.Wire.Internal.IWire subscriptionFactory = null, global::Unity.Services.Core.Telemetry.Internal.IMetrics metrics = null, string cloudEnvironment = "production")
		{
			LobbyApi = new global::Unity.Services.Lobbies.Apis.Lobby.LobbyApiClient(httpClient, accessToken);
			string basePath = GetBasePath(cloudEnvironment);
			Configuration = new global::Unity.Services.Lobbies.Configuration(basePath, 10, 4, null);
			Wire = subscriptionFactory;
			Metrics = metrics;
		}

		private string GetBasePath(string cloudEnvironment)
		{
			if (cloudEnvironment == "staging")
			{
				return "https://lobby-stg.services.api.unity.com/v1";
			}
			return "https://lobby.services.api.unity.com/v1";
		}
	}
}
