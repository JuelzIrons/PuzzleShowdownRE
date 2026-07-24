namespace Unity.Services.Qos.Http
{
	internal abstract class BaseApiClient
	{
		protected readonly global::Unity.Services.Qos.Http.IHttpClient HttpClient;

		public BaseApiClient(global::Unity.Services.Qos.Http.IHttpClient httpClient)
		{
			HttpClient = httpClient ?? new global::Unity.Services.Qos.Http.HttpClient();
		}
	}
}
