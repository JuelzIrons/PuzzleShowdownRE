namespace Unity.Services.Relay
{
	internal interface IRelayServiceSdk
	{
		global::Unity.Services.Relay.Apis.RelayAllocations.IRelayAllocationsApiClient AllocationsApi { get; set; }

		global::Unity.Services.Relay.Configuration Configuration { get; set; }

		global::Unity.Services.Authentication.Internal.IAccessToken AccessToken { get; set; }

		global::Unity.Services.Qos.Internal.IQosResults QosResults { get; set; }
	}
	public interface IRelayServiceSDK : global::Unity.Services.Relay.IRelayService
	{
	}
}
