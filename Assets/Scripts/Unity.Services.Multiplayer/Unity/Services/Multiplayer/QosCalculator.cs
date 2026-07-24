namespace Unity.Services.Multiplayer
{
	internal class QosCalculator
	{
		private readonly global::Unity.Services.Qos.IQosService m_QosService;

		private const string k_UnknownRegion = "unknown-region";

		public QosCalculator(global::Unity.Services.Qos.IQosService qosService)
		{
			m_QosService = qosService;
		}

		public async global::System.Threading.Tasks.Task<global::System.Collections.Generic.List<global::Unity.Services.Matchmaker.Models.QosResult>> GetQosResultsAsync(string queueName)
		{
			global::System.Collections.Generic.List<global::Unity.Services.Qos.V2.Models.QosServer> list = global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Where(global::System.Linq.Enumerable.Where(global::System.Linq.Enumerable.Where((await m_QosService.GetAllServersAsync()) ?? throw new global::Unity.Services.Multiplayer.SessionException("Could not find QoS servers for queue", global::Unity.Services.Multiplayer.SessionError.QoSMeasurementFailed), (global::Unity.Services.Qos.V2.Models.QosServer q) => q.Annotations.MatchmakerQueueName != null), (global::Unity.Services.Qos.V2.Models.QosServer q) => q.Annotations.MatchmakerQueueName.Contains(queueName)), (global::Unity.Services.Qos.V2.Models.QosServer q) => ExtractRegion(q.Annotations) != "unknown-region"));
			if (list.Count == 0)
			{
				return new global::System.Collections.Generic.List<global::Unity.Services.Matchmaker.Models.QosResult>();
			}
			global::System.Collections.Generic.IList<(global::Unity.Services.Qos.V2.Models.QosServer, global::Unity.Services.Qos.IQosMeasurements)> list2 = await m_QosService.GetQosResultsAsync(list);
			if (list2 == null)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("Could not measure QoS", global::Unity.Services.Multiplayer.SessionError.QoSMeasurementFailed);
			}
			try
			{
				return global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.ThenBy(global::System.Linq.Enumerable.OrderBy(global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Select(global::System.Linq.Enumerable.GroupBy(global::System.Linq.Enumerable.Where(list2, ((global::Unity.Services.Qos.V2.Models.QosServer, global::Unity.Services.Qos.IQosMeasurements) q) => q.Item2.AverageLatencyMs != int.MaxValue && q.Item2.AverageLatencyMs >= 0 && (double)q.Item2.PacketLossPercent >= 0.0 && (double)q.Item2.PacketLossPercent < 1.0), ((global::Unity.Services.Qos.V2.Models.QosServer, global::Unity.Services.Qos.IQosMeasurements) q) => ExtractRegion(q.Item1.Annotations)), (global::System.Linq.IGrouping<string, (global::Unity.Services.Qos.V2.Models.QosServer, global::Unity.Services.Qos.IQosMeasurements)> q) => new global::Unity.Services.Matchmaker.Models.QosResult(q.Key, global::System.Linq.Enumerable.Average(q, ((global::Unity.Services.Qos.V2.Models.QosServer, global::Unity.Services.Qos.IQosMeasurements) t) => t.Item2.PacketLossPercent), global::System.Linq.Enumerable.Average(q, ((global::Unity.Services.Qos.V2.Models.QosServer, global::Unity.Services.Qos.IQosMeasurements) t) => t.Item2.AverageLatencyMs), ExtractPoolId(global::System.Linq.Enumerable.First(q).Item1.Annotations)))), (global::Unity.Services.Matchmaker.Models.QosResult q) => q.Latency), (global::Unity.Services.Matchmaker.Models.QosResult q) => q.PacketLoss));
			}
			catch (global::System.Exception ex)
			{
				throw new global::Unity.Services.Multiplayer.SessionException(ex.Message, global::Unity.Services.Multiplayer.SessionError.QoSMeasurementFailed);
			}
		}

		private static string ExtractRegion(global::Unity.Services.Qos.V2.Models.QosServerAnnotations annotations)
		{
			if (annotations.RelayRegionId != null && annotations.RelayRegionId.Count > 0)
			{
				return annotations.RelayRegionId[0];
			}
			if (annotations.MultiplayRegionId != null && annotations.MultiplayRegionId.Count > 0)
			{
				return annotations.MultiplayRegionId[0];
			}
			return "unknown-region";
		}

		private static global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<string>> ExtractPoolId(global::Unity.Services.Qos.V2.Models.QosServerAnnotations annotations)
		{
			if (annotations.MatchmakerPoolId != null && annotations.MatchmakerPoolId.Count > 0)
			{
				return new global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<string>> { { "MatchmakerPoolId", annotations.MatchmakerPoolId } };
			}
			return null;
		}
	}
}
