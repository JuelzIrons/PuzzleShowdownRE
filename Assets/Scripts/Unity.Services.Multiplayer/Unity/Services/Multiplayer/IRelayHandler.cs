namespace Unity.Services.Multiplayer
{
	internal interface IRelayHandler
	{
		global::Unity.Services.Multiplayer.RelayState State { get; }

		global::System.Guid AllocationId { get; }

		string RelayJoinCode { get; }

		string Region { get; }

		global::System.Threading.Tasks.Task CreateAllocationAsync(int maxPlayers, string region = null);

		global::System.Threading.Tasks.Task JoinAllocationAsync(string joinCode);

		global::System.Threading.Tasks.Task FetchJoinCodeAsync();

		void Disconnect();

		global::Unity.Networking.Transport.Relay.RelayServerData GetRelayServerData(global::Unity.Services.Multiplayer.RelayProtocol relayProtocol);
	}
}
