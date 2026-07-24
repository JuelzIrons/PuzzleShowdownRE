namespace Unity.Services.Core
{
	public static class UnityServicesExtensions
	{
		public static global::Unity.Services.Multiplayer.IMultiplayerService GetMultiplayerService(this global::Unity.Services.Core.IUnityServices unityServices)
		{
			return unityServices.GetService<global::Unity.Services.Multiplayer.IMultiplayerService>();
		}

		public static global::Unity.Services.Lobbies.ILobbyService GetLobbyService(this global::Unity.Services.Core.IUnityServices unityServices)
		{
			return unityServices.GetService<global::Unity.Services.Lobbies.ILobbyService>();
		}

		public static global::Unity.Services.Relay.IRelayService GetRelayService(this global::Unity.Services.Core.IUnityServices unityServices)
		{
			return unityServices.GetService<global::Unity.Services.Relay.IRelayService>();
		}

		public static global::Unity.Services.Matchmaker.IMatchmakerService GetMatchmakerService(this global::Unity.Services.Core.IUnityServices unityServices)
		{
			return unityServices.GetService<global::Unity.Services.Matchmaker.IMatchmakerService>();
		}
	}
}
