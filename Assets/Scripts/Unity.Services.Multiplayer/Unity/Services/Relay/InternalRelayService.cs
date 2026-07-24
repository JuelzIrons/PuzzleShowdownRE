namespace Unity.Services.Relay
{
	internal class InternalRelayService : global::Unity.Services.Relay.IRelayServiceSdk
	{
		private const string k_CloudEnvironmentKey = "com.unity.services.core.cloud-environment";

		private const string k_StagingEnvironment = "staging";

		public global::Unity.Services.Relay.Apis.RelayAllocations.IRelayAllocationsApiClient AllocationsApi { get; set; }

		public global::Unity.Services.Relay.Configuration Configuration { get; set; }

		public global::Unity.Services.Authentication.Internal.IAccessToken AccessToken { get; set; }

		public global::Unity.Services.Qos.Internal.IQosResults QosResults { get; set; }

		public InternalRelayService(global::Unity.Services.Relay.Http.HttpClient httpClient, global::Unity.Services.Core.Configuration.Internal.IProjectConfiguration projectConfiguration = null, global::Unity.Services.Authentication.Internal.IAccessToken accessToken = null, global::Unity.Services.Qos.Internal.IQosResults qosResults = null)
		{
			AllocationsApi = new global::Unity.Services.Relay.Apis.RelayAllocations.RelayAllocationsApiClient(httpClient, accessToken);
			Configuration = new global::Unity.Services.Relay.Configuration(GetHost(projectConfiguration), 10, 4, null);
			AccessToken = accessToken;
			QosResults = qosResults;
		}

		private string GetHost(global::Unity.Services.Core.Configuration.Internal.IProjectConfiguration projectConfiguration)
		{
			if (projectConfiguration?.GetString("com.unity.services.core.cloud-environment") == "staging")
			{
				return "https://relay-allocations-stg.services.api.unity.com";
			}
			return "https://relay-allocations.services.api.unity.com";
		}
	}
}
