namespace Unity.Services.Matchmaker.Http
{
	internal abstract class BaseApiClient
	{
		protected readonly global::Unity.Services.Matchmaker.Http.IHttpClient HttpClient;

		public BaseApiClient(global::Unity.Services.Matchmaker.Http.IHttpClient httpClient)
		{
			HttpClient = httpClient ?? new global::Unity.Services.Matchmaker.Http.HttpClient();
		}
	}
}
