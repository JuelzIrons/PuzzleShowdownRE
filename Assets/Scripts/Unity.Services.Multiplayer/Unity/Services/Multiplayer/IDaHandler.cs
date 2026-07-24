namespace Unity.Services.Multiplayer
{
	internal interface IDaHandler
	{
		string RelayJoinCode { get; }

		string Region { get; }

		global::System.Guid AllocationId { get; }

		global::System.Threading.Tasks.Task CreateAndJoinSessionAsync(string lobbyId, string region);

		global::System.Threading.Tasks.Task JoinSessionAsync(string relayJoinCode);

		global::Unity.Networking.Transport.Relay.RelayServerData GetRelayServerData(global::Unity.Services.Multiplayer.RelayProtocol connectionType);

		global::Unity.Services.DistributedAuthority.ConnectPayload GetConnectPayload();
	}
}
