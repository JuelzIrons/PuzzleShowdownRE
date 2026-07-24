namespace Unity.Services.Qos
{
	internal class QosResults : global::Unity.Services.Qos.Internal.IQosResults, global::Unity.Services.Core.Internal.IServiceComponent
	{
		private global::Unity.Services.Qos.WrappedQosService _qosService;

		internal QosResults(global::Unity.Services.Qos.WrappedQosService qosService)
		{
			_qosService = qosService;
		}

		public global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<global::Unity.Services.Qos.Internal.QosResult>> GetSortedQosResultsAsync(string service, global::System.Collections.Generic.IList<string> regions)
		{
			return _qosService.GetSortedInternalQosResultsAsync(service, regions);
		}
	}
}
