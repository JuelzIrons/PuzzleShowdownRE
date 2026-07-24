namespace Unity.Services.Relay
{
	public interface IRelayService
	{
		global::System.Threading.Tasks.Task<global::Unity.Services.Relay.Models.Allocation> CreateAllocationAsync(int maxConnections, string region = null);

		global::System.Threading.Tasks.Task<string> GetJoinCodeAsync(global::System.Guid allocationId);

		global::System.Threading.Tasks.Task<global::Unity.Services.Relay.Models.JoinAllocation> JoinAllocationAsync(string joinCode);

		global::System.Threading.Tasks.Task<global::System.Collections.Generic.List<global::Unity.Services.Relay.Models.Region>> ListRegionsAsync();
	}
}
