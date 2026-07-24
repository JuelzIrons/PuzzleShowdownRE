namespace Unity.Services.Lobbies.Http
{
	internal abstract class BaseApiClient
	{
		protected readonly global::Unity.Services.Lobbies.Http.IHttpClient HttpClient;

		public BaseApiClient(global::Unity.Services.Lobbies.Http.IHttpClient httpClient)
		{
			HttpClient = httpClient ?? new global::Unity.Services.Lobbies.Http.HttpClient();
		}
	}
}
