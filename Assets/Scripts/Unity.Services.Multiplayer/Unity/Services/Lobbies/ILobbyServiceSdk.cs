namespace Unity.Services.Lobbies
{
	internal interface ILobbyServiceSdk
	{
		global::Unity.Services.Lobbies.Apis.Lobby.ILobbyApiClient LobbyApi { get; }

		global::Unity.Services.Lobbies.Configuration Configuration { get; }

		global::Unity.Services.Wire.Internal.IWire Wire { get; set; }

		global::Unity.Services.Core.Telemetry.Internal.IMetrics Metrics { get; }
	}
	public interface ILobbyServiceSDK : global::Unity.Services.Lobbies.ILobbyService
	{
	}
}
