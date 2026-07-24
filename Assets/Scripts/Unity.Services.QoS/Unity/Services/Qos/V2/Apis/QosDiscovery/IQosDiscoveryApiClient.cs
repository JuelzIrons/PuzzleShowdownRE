namespace Unity.Services.Qos.V2.Apis.QosDiscovery
{
	internal interface IQosDiscoveryApiClient
	{
		global::System.Threading.Tasks.Task<global::Unity.Services.Qos.V2.Response<global::Unity.Services.Qos.V2.Models.QosServersResponseBody>> GetAllServersAsync(global::Unity.Services.Qos.V2.QosDiscovery.GetAllServersRequest request, global::Unity.Services.Qos.V2.Configuration operationConfiguration = null);
	}
}
