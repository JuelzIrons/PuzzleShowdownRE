namespace Unity.Services.Qos
{
	internal class InternalQosDiscoveryService
	{
		private const int RequestTimeout = 10;

		private const int NumRetries = 4;

		public global::Unity.Services.Qos.Apis.QosDiscovery.IQosDiscoveryApiClient QosDiscoveryApi { get; set; }

		public global::Unity.Services.Qos.Configuration Configuration { get; set; }

		internal InternalQosDiscoveryService(string host, global::Unity.Services.Qos.Http.HttpClient httpClient, global::Unity.Services.Authentication.Internal.IAccessToken accessToken = null)
		{
			Configuration = new global::Unity.Services.Qos.Configuration(host, 10, 4, null);
			QosDiscoveryApi = new global::Unity.Services.Qos.Apis.QosDiscovery.QosDiscoveryApiClient(httpClient, accessToken, Configuration);
		}
	}
}
