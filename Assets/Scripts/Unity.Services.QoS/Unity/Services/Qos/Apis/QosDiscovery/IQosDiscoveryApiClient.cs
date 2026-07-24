namespace Unity.Services.Qos.Apis.QosDiscovery
{
	internal interface IQosDiscoveryApiClient
	{
		global::System.Threading.Tasks.Task<global::Unity.Services.Qos.Response<global::Unity.Services.Qos.Models.QosServersResponseBody>> GetServersAsync(global::Unity.Services.Qos.QosDiscovery.GetServersRequest request, global::Unity.Services.Qos.Configuration operationConfiguration = null);

		global::System.Threading.Tasks.Task<global::Unity.Services.Qos.Response<global::Unity.Services.Qos.Models.QosServiceServersResponseBody>> GetServiceServersAsync(global::Unity.Services.Qos.QosDiscovery.GetServiceServersRequest request, global::Unity.Services.Qos.Configuration operationConfiguration = null);
	}
}
