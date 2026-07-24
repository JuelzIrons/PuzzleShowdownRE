namespace Unity.Services.Multiplayer
{
	public interface IHostSessionNetwork
	{
		global::Unity.Services.Multiplayer.NetworkState State { get; }

		global::Unity.Services.Multiplayer.INetworkHandler NetworkHandler { get; set; }

		internal global::Unity.Services.Multiplayer.NetworkInfo NetworkInfo { get; }

		event global::System.Action<global::Unity.Services.Multiplayer.NetworkState> StateChanged;

		event global::System.Action<global::Unity.Services.Multiplayer.SessionError> StartFailed;

		event global::System.Action<global::Unity.Services.Multiplayer.SessionError> StopFailed;

		event global::System.Action<global::Unity.Services.Multiplayer.SessionError> MigrationFailed;

		global::System.Threading.Tasks.Task StartDirectNetworkAsync(global::Unity.Services.Multiplayer.DirectNetworkOptions networkOptions);

		global::System.Threading.Tasks.Task StartRelayNetworkAsync(global::Unity.Services.Multiplayer.RelayNetworkOptions networkOptions);

		global::System.Threading.Tasks.Task StartDistributedAuthorityNetworkAsync(global::Unity.Services.Multiplayer.RelayNetworkOptions networkOptions);

		global::System.Threading.Tasks.Task StopNetworkAsync();
	}
}
