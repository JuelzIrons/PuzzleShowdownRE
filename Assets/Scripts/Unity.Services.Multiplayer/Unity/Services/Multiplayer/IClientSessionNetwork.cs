namespace Unity.Services.Multiplayer
{
	public interface IClientSessionNetwork
	{
		global::Unity.Services.Multiplayer.NetworkState State { get; }

		global::Unity.Services.Multiplayer.INetworkHandler NetworkHandler { get; set; }

		event global::System.Action<global::Unity.Services.Multiplayer.NetworkState> StateChanged;

		event global::System.Action<global::Unity.Services.Multiplayer.SessionError> StartFailed;

		event global::System.Action<global::Unity.Services.Multiplayer.SessionError> StopFailed;

		event global::System.Action<global::Unity.Services.Multiplayer.SessionError> MigrationFailed;

		internal global::System.Threading.Tasks.Task StartNetworkAsync();

		internal global::System.Threading.Tasks.Task StopNetworkAsync();
	}
}
