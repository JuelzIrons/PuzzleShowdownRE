namespace Unity.Services.DistributedAuthority.Http
{
	internal abstract class BaseApiClient
	{
		protected readonly global::Unity.Services.DistributedAuthority.Http.IHttpClient HttpClient;

		public BaseApiClient(global::Unity.Services.DistributedAuthority.Http.IHttpClient httpClient)
		{
			HttpClient = httpClient ?? new global::Unity.Services.DistributedAuthority.Http.HttpClient();
		}
	}
}
