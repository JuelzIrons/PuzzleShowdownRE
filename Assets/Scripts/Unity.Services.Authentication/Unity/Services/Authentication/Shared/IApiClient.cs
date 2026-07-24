namespace Unity.Services.Authentication.Shared
{
	internal interface IApiClient
	{
		global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.Shared.ApiResponse<T>> GetAsync<T>(string path, global::Unity.Services.Authentication.Shared.ApiRequestOptions options, global::Unity.Services.Authentication.Shared.IApiConfiguration configuration = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));

		global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.Shared.ApiResponse> GetAsync(string path, global::Unity.Services.Authentication.Shared.ApiRequestOptions options, global::Unity.Services.Authentication.Shared.IApiConfiguration configuration = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));

		global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.Shared.ApiResponse<T>> PostAsync<T>(string path, global::Unity.Services.Authentication.Shared.ApiRequestOptions options, global::Unity.Services.Authentication.Shared.IApiConfiguration configuration = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));

		global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.Shared.ApiResponse> PostAsync(string path, global::Unity.Services.Authentication.Shared.ApiRequestOptions options, global::Unity.Services.Authentication.Shared.IApiConfiguration configuration = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));

		global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.Shared.ApiResponse<T>> PutAsync<T>(string path, global::Unity.Services.Authentication.Shared.ApiRequestOptions options, global::Unity.Services.Authentication.Shared.IApiConfiguration configuration = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));

		global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.Shared.ApiResponse> PutAsync(string path, global::Unity.Services.Authentication.Shared.ApiRequestOptions options, global::Unity.Services.Authentication.Shared.IApiConfiguration configuration = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));

		global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.Shared.ApiResponse<T>> DeleteAsync<T>(string path, global::Unity.Services.Authentication.Shared.ApiRequestOptions options, global::Unity.Services.Authentication.Shared.IApiConfiguration configuration = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));

		global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.Shared.ApiResponse> DeleteAsync(string path, global::Unity.Services.Authentication.Shared.ApiRequestOptions options, global::Unity.Services.Authentication.Shared.IApiConfiguration configuration = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));

		global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.Shared.ApiResponse<T>> HeadAsync<T>(string path, global::Unity.Services.Authentication.Shared.ApiRequestOptions options, global::Unity.Services.Authentication.Shared.IApiConfiguration configuration = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));

		global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.Shared.ApiResponse> HeadAsync(string path, global::Unity.Services.Authentication.Shared.ApiRequestOptions options, global::Unity.Services.Authentication.Shared.IApiConfiguration configuration = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));

		global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.Shared.ApiResponse<T>> OptionsAsync<T>(string path, global::Unity.Services.Authentication.Shared.ApiRequestOptions options, global::Unity.Services.Authentication.Shared.IApiConfiguration configuration = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));

		global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.Shared.ApiResponse> OptionsAsync(string path, global::Unity.Services.Authentication.Shared.ApiRequestOptions options, global::Unity.Services.Authentication.Shared.IApiConfiguration configuration = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));

		global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.Shared.ApiResponse<T>> PatchAsync<T>(string path, global::Unity.Services.Authentication.Shared.ApiRequestOptions options, global::Unity.Services.Authentication.Shared.IApiConfiguration configuration = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));

		global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.Shared.ApiResponse> PatchAsync(string path, global::Unity.Services.Authentication.Shared.ApiRequestOptions options, global::Unity.Services.Authentication.Shared.IApiConfiguration configuration = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));
	}
}
