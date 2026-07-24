namespace Unity.Services.DistributedAuthority.Apis.DistributedAuthority
{
	internal interface IDistributedAuthorityApiClient
	{
		global::System.Threading.Tasks.Task<global::Unity.Services.DistributedAuthority.Response<global::Unity.Services.DistributedAuthority.Models.Session>> CreateSessionAsync(global::Unity.Services.DistributedAuthority.DistributedAuthority.CreateSessionRequest request, global::Unity.Services.DistributedAuthority.Configuration operationConfiguration = null);
	}
}
