namespace Unity.Services.Qos.Runner
{
	internal interface IQosRunner
	{
		global::System.Threading.Tasks.Task<global::System.Collections.Generic.List<global::Unity.Services.Qos.Internal.QosResult>> MeasureQosAsync(global::System.Collections.Generic.IList<global::Unity.Services.Qos.Models.QosServer> servers);

		global::System.Threading.Tasks.Task<global::System.Collections.Generic.List<global::Unity.Services.Qos.Runner.QosAnnotatedResult>> MeasureQosAsync(global::System.Collections.Generic.IList<global::Unity.Services.Qos.Models.QosServiceServer> servers);

		global::System.Threading.Tasks.Task<global::System.Collections.Generic.List<(global::Unity.Services.Qos.V2.Models.QosServer, global::Unity.Services.Qos.IQosMeasurements)>> MeasureQosV2Async(global::System.Collections.Generic.IList<global::Unity.Services.Qos.V2.Models.QosServer> servers);
	}
}
