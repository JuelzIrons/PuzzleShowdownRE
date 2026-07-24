namespace Unity.Services.Relay.Apis.RelayAllocations
{
	internal interface IRelayAllocationsApiClient
	{
		global::System.Threading.Tasks.Task<global::Unity.Services.Relay.Response<global::Unity.Services.Relay.Models.AllocateResponseBody>> CreateAllocationAsync(global::Unity.Services.Relay.RelayAllocations.CreateAllocationRequest request, global::Unity.Services.Relay.Configuration operationConfiguration = null);

		global::System.Threading.Tasks.Task<global::Unity.Services.Relay.Response<global::Unity.Services.Relay.Models.JoinCodeResponseBody>> CreateJoincodeAsync(global::Unity.Services.Relay.RelayAllocations.CreateJoincodeRequest request, global::Unity.Services.Relay.Configuration operationConfiguration = null);

		global::System.Threading.Tasks.Task<global::Unity.Services.Relay.Response<global::Unity.Services.Relay.Models.JoinResponseBody>> JoinRelayAsync(global::Unity.Services.Relay.RelayAllocations.JoinRelayRequest request, global::Unity.Services.Relay.Configuration operationConfiguration = null);

		global::System.Threading.Tasks.Task<global::Unity.Services.Relay.Response<global::Unity.Services.Relay.Models.RegionsResponseBody>> ListRegionsAsync(global::Unity.Services.Relay.RelayAllocations.ListRegionsRequest request, global::Unity.Services.Relay.Configuration operationConfiguration = null);
	}
}
