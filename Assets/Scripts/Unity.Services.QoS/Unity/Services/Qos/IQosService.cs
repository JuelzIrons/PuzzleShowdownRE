namespace Unity.Services.Qos
{
	public interface IQosService
	{
		global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<global::Unity.Services.Qos.IQosResult>> GetSortedQosResultsAsync(string service, global::System.Collections.Generic.IList<string> regions);

		global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<global::Unity.Services.Qos.IQosAnnotatedResult>> GetSortedRelayQosResultsAsync(global::System.Collections.Generic.IList<string> regions);

		global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<global::Unity.Services.Qos.IQosAnnotatedResult>> GetSortedMultiplayQosResultsAsync(global::System.Collections.Generic.IList<string> fleet);

		global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<global::Unity.Services.Qos.V2.Models.QosServer>> GetAllServersAsync();

		global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<(global::Unity.Services.Qos.V2.Models.QosServer, global::Unity.Services.Qos.IQosMeasurements)>> GetQosResultsAsync(global::System.Collections.Generic.IList<global::Unity.Services.Qos.V2.Models.QosServer> servers);
	}
}
