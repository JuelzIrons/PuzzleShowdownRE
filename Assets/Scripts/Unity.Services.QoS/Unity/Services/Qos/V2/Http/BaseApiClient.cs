namespace Unity.Services.Qos.V2.Http
{
	internal abstract class BaseApiClient
	{
		protected readonly global::Unity.Services.Qos.V2.Http.IHttpClient HttpClient;

		public BaseApiClient(global::Unity.Services.Qos.V2.Http.IHttpClient httpClient)
		{
			HttpClient = httpClient ?? new global::Unity.Services.Qos.V2.Http.HttpClient();
		}
	}
}
