namespace Unity.Services.DistributedAuthority
{
	internal interface IDistributedAuthorityService
	{
		global::System.Threading.Tasks.Task<global::Unity.Services.DistributedAuthority.Models.Session> CreateSessionForLobbyIdAsync(string lobbyId, string region = null);

		global::System.Threading.Tasks.Task<string> JoinSessionForLobbyIdAsync(string lobbyId, int timeoutSeconds = 120);
	}
}
