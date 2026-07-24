namespace Unity.Services.Relay.Http
{
	internal abstract class BaseApiClient
	{
		protected readonly global::Unity.Services.Relay.Http.IHttpClient HttpClient;

		public BaseApiClient(global::Unity.Services.Relay.Http.IHttpClient httpClient)
		{
			HttpClient = httpClient ?? new global::Unity.Services.Relay.Http.HttpClient();
		}
	}
}
