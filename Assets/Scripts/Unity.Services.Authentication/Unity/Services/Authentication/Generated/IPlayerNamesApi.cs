namespace Unity.Services.Authentication.Generated
{
	internal interface IPlayerNamesApi : global::Unity.Services.Authentication.Shared.IApiAccessor
	{
		global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.Shared.ApiResponse<global::Unity.Services.Authentication.Generated.Player>> GetNameAsync(string playerId, bool? autoGenerate = null, bool? showMetadata = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));

		global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.Shared.ApiResponse<global::Unity.Services.Authentication.Generated.Player>> UpdateNameAsync(string playerId, global::Unity.Services.Authentication.Generated.UpdateNameRequest updateNameRequest, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));
	}
}
