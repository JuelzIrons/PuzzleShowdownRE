namespace Unity.Services.Qos
{
	internal class WrappedQosService : global::Unity.Services.Qos.IQosService
	{
		private const string ResultLatencyMetricName = "qos_result_latency_ms";

		private const string ResultPacketLossMetricName = "qos_result_packet_loss";

		private const string MetricServiceNameLabelName = "qos_service_name";

		private const string MetricServiceRegionLabelName = "qos_service_region";

		private const string MetricClientCountryLabelName = "qos_client_country";

		private const string MetricClientRegionLabelName = "qos_client_region";

		private const string MetricClientBestResultLabelName = "qos_best_result";

		private const string MetricClientBestResultLabelTrueValue = "true";

		private global::Unity.Services.Qos.Apis.QosDiscovery.IQosDiscoveryApiClient _qosDiscoveryApiClient;

		private global::Unity.Services.Qos.V2.Apis.QosDiscovery.IQosDiscoveryApiClient _qosDiscoveryApiClientV2;

		private global::Unity.Services.Qos.Runner.IQosRunner _qosRunner;

		private global::Unity.Services.Authentication.Internal.IAccessToken _accessToken;

		private global::Unity.Services.Core.Telemetry.Internal.IMetrics _metrics;

		private string _latestCountryForTelemetry;

		private string _latestRegionForTelemetry;

		private string _getAllServersEtag = "";

		private global::System.Collections.Generic.IList<global::Unity.Services.Qos.V2.Models.QosServer> _getAllServersCached;

		internal WrappedQosService(global::Unity.Services.Qos.Apis.QosDiscovery.IQosDiscoveryApiClient qosDiscoveryApiClient, global::Unity.Services.Qos.V2.Apis.QosDiscovery.IQosDiscoveryApiClient qosDiscoveryApiClientV2, global::Unity.Services.Qos.Runner.IQosRunner qosRunner, global::Unity.Services.Authentication.Internal.IAccessToken accessToken, global::Unity.Services.Core.Telemetry.Internal.IMetrics metrics)
		{
			_qosDiscoveryApiClient = qosDiscoveryApiClient;
			_qosDiscoveryApiClientV2 = qosDiscoveryApiClientV2;
			_qosRunner = qosRunner;
			_accessToken = accessToken;
			_metrics = metrics;
		}

		public async global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<global::Unity.Services.Qos.IQosResult>> GetSortedQosResultsAsync(string service, global::System.Collections.Generic.IList<string> regions)
		{
			return global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Select(await GetSortedInternalQosResultsAsync(service, regions), MapToPublicQosResult));
		}

		internal async global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<global::Unity.Services.Qos.Internal.QosResult>> GetSortedInternalQosResultsAsync(string service, global::System.Collections.Generic.IList<string> regions)
		{
			if (string.IsNullOrEmpty(_accessToken.AccessToken))
			{
				throw new global::System.Exception("Access token not available, please sign in with the Authentication Service.");
			}
			global::System.Collections.Generic.List<string> list = regions as global::System.Collections.Generic.List<string>;
			if (list == null && regions != null)
			{
				list = new global::System.Collections.Generic.List<string>(regions);
			}
			global::Unity.Services.Qos.Response<global::Unity.Services.Qos.Models.QosServersResponseBody> httpResp = await _qosDiscoveryApiClient.GetServersAsync(new global::Unity.Services.Qos.QosDiscovery.GetServersRequest(list, service));
			global::System.Collections.Generic.List<global::Unity.Services.Qos.Models.QosServer> servers = httpResp.Result.Data.Servers;
			if (!global::System.Linq.Enumerable.Any(servers))
			{
				return new global::System.Collections.Generic.List<global::Unity.Services.Qos.Internal.QosResult>();
			}
			global::System.Collections.Generic.List<global::Unity.Services.Qos.Internal.QosResult> list2 = SortResults(await _qosRunner.MeasureQosAsync(servers));
			SendResultsMetrics(list2, service, httpResp);
			return list2;
		}

		private global::System.Collections.Generic.List<global::Unity.Services.Qos.Internal.QosResult> SortResults(global::System.Collections.Generic.IList<global::Unity.Services.Qos.Internal.QosResult> results)
		{
			return global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.ThenBy(global::System.Linq.Enumerable.OrderBy(results, (global::Unity.Services.Qos.Internal.QosResult q) => q.AverageLatencyMs), (global::Unity.Services.Qos.Internal.QosResult q) => q.PacketLossPercent));
		}

		public async global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<global::Unity.Services.Qos.IQosAnnotatedResult>> GetSortedRelayQosResultsAsync(global::System.Collections.Generic.IList<string> regions)
		{
			return await GetSortedInternalServiceQosResultsAsync("relay", regions, null);
		}

		public async global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<global::Unity.Services.Qos.IQosAnnotatedResult>> GetSortedMultiplayQosResultsAsync(global::System.Collections.Generic.IList<string> fleet)
		{
			return await GetSortedInternalServiceQosResultsAsync("multiplay", null, fleet);
		}

		public async global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<global::Unity.Services.Qos.V2.Models.QosServer>> GetAllServersAsync()
		{
			global::Unity.Services.Qos.V2.QosDiscovery.GetAllServersRequest request = new global::Unity.Services.Qos.V2.QosDiscovery.GetAllServersRequest();
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (!string.IsNullOrEmpty(_getAllServersEtag))
			{
				dictionary.Add("If-None-Match", _getAllServersEtag);
			}
			global::Unity.Services.Qos.V2.Configuration operationConfiguration = new global::Unity.Services.Qos.V2.Configuration(null, null, null, dictionary);
			try
			{
				global::Unity.Services.Qos.V2.Response<global::Unity.Services.Qos.V2.Models.QosServersResponseBody> response = await _qosDiscoveryApiClientV2.GetAllServersAsync(request, operationConfiguration);
				response.Headers.TryGetValue("ETag", out _getAllServersEtag);
				_getAllServersCached = response.Result.Data.Servers;
				response.Headers.TryGetValue("X-Client-Country", out _latestCountryForTelemetry);
				response.Headers.TryGetValue("X-Client-Region", out _latestRegionForTelemetry);
			}
			catch (global::Unity.Services.Qos.V2.Http.HttpException ex)
			{
				if (304 == ex.Response.StatusCode)
				{
					return _getAllServersCached;
				}
				throw;
			}
			return _getAllServersCached;
		}

		public async global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<(global::Unity.Services.Qos.V2.Models.QosServer, global::Unity.Services.Qos.IQosMeasurements)>> GetQosResultsAsync(global::System.Collections.Generic.IList<global::Unity.Services.Qos.V2.Models.QosServer> servers)
		{
			global::System.Collections.Generic.List<(global::Unity.Services.Qos.V2.Models.QosServer, global::Unity.Services.Qos.IQosMeasurements)> list = await _qosRunner.MeasureQosV2Async(servers);
			SendResultsMetricsV2(list);
			return list;
		}

		private void SendResultsMetricsV2(global::System.Collections.Generic.IReadOnlyCollection<(global::Unity.Services.Qos.V2.Models.QosServer, global::Unity.Services.Qos.IQosMeasurements)> qosResults)
		{
			SendResultsMetricsV2ForService(qosResults, (global::Unity.Services.Qos.V2.Models.QosServer qs) => qs.Annotations.RelayRegionId, "relay");
			SendResultsMetricsV2ForService(qosResults, (global::Unity.Services.Qos.V2.Models.QosServer qs) => qs.Annotations.MultiplayRegionId, "multiplay");
		}

		private void SendResultsMetricsV2ForService(global::System.Collections.Generic.IReadOnlyCollection<(global::Unity.Services.Qos.V2.Models.QosServer, global::Unity.Services.Qos.IQosMeasurements)> allResults, global::System.Func<global::Unity.Services.Qos.V2.Models.QosServer, global::System.Collections.Generic.List<string>> regionGetter, string service)
		{
			global::System.Collections.Generic.List<(global::Unity.Services.Qos.V2.Models.QosServer, global::Unity.Services.Qos.IQosMeasurements)> list = global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.ThenBy(global::System.Linq.Enumerable.OrderBy(global::System.Linq.Enumerable.Where(allResults, ((global::Unity.Services.Qos.V2.Models.QosServer, global::Unity.Services.Qos.IQosMeasurements) t) => regionGetter(t.Item1) != null && regionGetter(t.Item1).Count > 0), ((global::Unity.Services.Qos.V2.Models.QosServer, global::Unity.Services.Qos.IQosMeasurements) t) => t.Item2.AverageLatencyMs), ((global::Unity.Services.Qos.V2.Models.QosServer, global::Unity.Services.Qos.IQosMeasurements) t) => t.Item2.PacketLossPercent));
			for (int num = 0; num < list.Count; num++)
			{
				(global::Unity.Services.Qos.V2.Models.QosServer, global::Unity.Services.Qos.IQosMeasurements) tuple = list[num];
				SendResultMetrics(service, _latestCountryForTelemetry, _latestRegionForTelemetry, regionGetter(tuple.Item1)[0], tuple.Item2.AverageLatencyMs, tuple.Item2.PacketLossPercent, num == 0);
			}
		}

		internal async global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<global::Unity.Services.Qos.IQosAnnotatedResult>> GetSortedInternalServiceQosResultsAsync(string service, global::System.Collections.Generic.IList<string> regions, global::System.Collections.Generic.IList<string> fleet)
		{
			if (string.IsNullOrEmpty(_accessToken.AccessToken))
			{
				throw new global::System.Exception("Access token not available, please sign in with the Authentication Service.");
			}
			global::System.Collections.Generic.List<string> list = regions as global::System.Collections.Generic.List<string>;
			if (list == null && regions != null)
			{
				list = new global::System.Collections.Generic.List<string>(regions);
			}
			global::System.Collections.Generic.List<string> list2 = fleet as global::System.Collections.Generic.List<string>;
			if (list2 == null && fleet != null)
			{
				list2 = new global::System.Collections.Generic.List<string>(fleet);
			}
			global::Unity.Services.Qos.Response<global::Unity.Services.Qos.Models.QosServiceServersResponseBody> httpResp = await _qosDiscoveryApiClient.GetServiceServersAsync(new global::Unity.Services.Qos.QosDiscovery.GetServiceServersRequest(service, list, list2));
			global::System.Collections.Generic.List<global::Unity.Services.Qos.Models.QosServiceServer> servers = httpResp.Result.Data.Servers;
			if (!global::System.Linq.Enumerable.Any(servers))
			{
				return new global::System.Collections.Generic.List<global::Unity.Services.Qos.IQosAnnotatedResult>();
			}
			global::System.Collections.Generic.List<global::Unity.Services.Qos.IQosAnnotatedResult> list3 = SortServiceResults(await _qosRunner.MeasureQosAsync(servers));
			SendResultsMetrics(global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Cast<global::Unity.Services.Qos.IQosResult>(list3)), service, httpResp);
			return list3;
		}

		private global::System.Collections.Generic.List<global::Unity.Services.Qos.IQosAnnotatedResult> SortServiceResults(global::System.Collections.Generic.IList<global::Unity.Services.Qos.Runner.QosAnnotatedResult> results)
		{
			return global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.ThenBy(global::System.Linq.Enumerable.OrderBy(global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Select(global::System.Linq.Enumerable.GroupBy(global::System.Linq.Enumerable.Where(results, (global::Unity.Services.Qos.Runner.QosAnnotatedResult q) => q.AverageLatencyMs != int.MaxValue && (double)q.PacketLossPercent >= 0.0 && (double)q.PacketLossPercent < 1.0), (global::Unity.Services.Qos.Runner.QosAnnotatedResult q) => q.Region), (global::System.Func<global::System.Linq.IGrouping<string, global::Unity.Services.Qos.Runner.QosAnnotatedResult>, global::Unity.Services.Qos.IQosAnnotatedResult>)((global::System.Linq.IGrouping<string, global::Unity.Services.Qos.Runner.QosAnnotatedResult> q) => new global::Unity.Services.Qos.QosResult(q.Key, (int)global::System.Math.Round(global::System.Linq.Enumerable.Average(global::System.Linq.Enumerable.Select(q, (global::Unity.Services.Qos.Runner.QosAnnotatedResult x) => x.AverageLatencyMs))), global::System.Linq.Enumerable.Average(global::System.Linq.Enumerable.Select(q, (global::Unity.Services.Qos.Runner.QosAnnotatedResult x) => x.PacketLossPercent)), global::System.Linq.Enumerable.First(global::System.Linq.Enumerable.Select(q, (global::Unity.Services.Qos.Runner.QosAnnotatedResult x) => x.Annotations)))))), (global::Unity.Services.Qos.IQosAnnotatedResult q) => q.AverageLatencyMs), (global::Unity.Services.Qos.IQosAnnotatedResult q) => q.PacketLossPercent));
		}

		private void SendResultsMetrics(global::System.Collections.Generic.IList<global::Unity.Services.Qos.Internal.QosResult> sortedResults, string service, global::Unity.Services.Qos.Response discoveryResponse)
		{
			discoveryResponse.Headers.TryGetValue("X-Client-Country", out var value);
			discoveryResponse.Headers.TryGetValue("X-Client-Region", out var value2);
			for (int i = 0; i < sortedResults.Count; i++)
			{
				global::Unity.Services.Qos.Internal.QosResult qosResult = sortedResults[i];
				SendResultMetrics(service, value, value2, qosResult.Region, qosResult.AverageLatencyMs, qosResult.PacketLossPercent, i == 0);
			}
		}

		private void SendResultsMetrics(global::System.Collections.Generic.IList<global::Unity.Services.Qos.IQosResult> sortedResults, string service, global::Unity.Services.Qos.Response discoveryResponse)
		{
			discoveryResponse.Headers.TryGetValue("X-Client-Country", out var value);
			discoveryResponse.Headers.TryGetValue("X-Client-Region", out var value2);
			for (int i = 0; i < sortedResults.Count; i++)
			{
				global::Unity.Services.Qos.IQosResult qosResult = sortedResults[i];
				SendResultMetrics(service, value, value2, qosResult.Region, qosResult.AverageLatencyMs, qosResult.PacketLossPercent, i == 0);
			}
		}

		private void SendResultMetrics(string service, string clientCountry, string clientRegion, string region, int averageLatencyMs, float packetLossPercent, bool isBest)
		{
			global::System.Collections.Generic.IDictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			dictionary.Add("qos_service_name", service);
			dictionary.Add("qos_service_region", region);
			if (!string.IsNullOrEmpty(clientCountry))
			{
				dictionary.Add("qos_client_country", clientCountry);
			}
			if (!string.IsNullOrEmpty(clientRegion))
			{
				dictionary.Add("qos_client_region", clientRegion);
			}
			if (isBest)
			{
				dictionary.Add("qos_best_result", "true");
			}
			_metrics.SendHistogramMetric("qos_result_latency_ms", averageLatencyMs, dictionary);
			_metrics.SendHistogramMetric("qos_result_packet_loss", packetLossPercent, dictionary);
		}

		private global::Unity.Services.Qos.IQosResult MapToPublicQosResult(global::Unity.Services.Qos.Internal.QosResult internalQosResult)
		{
			return new global::Unity.Services.Qos.QosResult(internalQosResult.Region, internalQosResult.AverageLatencyMs, internalQosResult.PacketLossPercent);
		}
	}
}
