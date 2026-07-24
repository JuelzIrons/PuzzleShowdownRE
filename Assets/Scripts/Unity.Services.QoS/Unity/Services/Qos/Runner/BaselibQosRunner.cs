namespace Unity.Services.Qos.Runner
{
	internal class BaselibQosRunner : global::Unity.Services.Qos.Runner.IQosRunner
	{
		internal struct QosMeasurementImpl : global::Unity.Services.Qos.IQosMeasurements
		{
			public int AverageLatencyMs { get; }

			public float PacketLossPercent { get; }

			public QosMeasurementImpl(int averageLatencyMs, float packetLossPercent)
			{
				AverageLatencyMs = averageLatencyMs;
				PacketLossPercent = packetLossPercent;
			}
		}

		private global::Unity.Services.Qos.Runner.QosJobProvider _qosJobProvider = (global::System.Collections.Generic.IList<global::Unity.Networking.QoS.UcgQosServer> servers, string title) => new global::Unity.Networking.QoS.QosJob(servers, title, 5u, 10000uL, 500uL);

		private global::Unity.Services.Qos.Runner.DnsResolver _dnsResolver = global::System.Net.Dns.GetHostAddressesAsync;

		public BaselibQosRunner(global::Unity.Services.Qos.Runner.QosJobProvider qosJobProvider = null, global::Unity.Services.Qos.Runner.DnsResolver dnsResolver = null)
		{
			if (qosJobProvider != null)
			{
				_qosJobProvider = qosJobProvider;
			}
			if (dnsResolver != null)
			{
				_dnsResolver = dnsResolver;
			}
		}

		public async global::System.Threading.Tasks.Task<global::System.Collections.Generic.List<global::Unity.Services.Qos.Internal.QosResult>> MeasureQosAsync(global::System.Collections.Generic.IList<global::Unity.Services.Qos.Models.QosServer> servers)
		{
			global::System.Collections.Generic.List<global::Unity.Networking.QoS.UcgQosServer> convertedServers = global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Select(global::System.Linq.Enumerable.Where(await global::System.Threading.Tasks.Task.WhenAll(global::System.Linq.Enumerable.Select(servers, ToUcgFormat)), (global::Unity.Networking.QoS.UcgQosServer? s) => s.HasValue), (global::Unity.Networking.QoS.UcgQosServer? s) => s.Value));
			global::System.Collections.Generic.List<global::Unity.Services.Qos.Internal.QosResult> results = new global::System.Collections.Generic.List<global::Unity.Services.Qos.Internal.QosResult>();
			global::Unity.Services.Qos.Runner.IQosJob qosJob = await RunQosJob(convertedServers);
			if (global::System.Linq.Enumerable.Count(servers) == global::System.Linq.Enumerable.Count(qosJob.QosResults))
			{
				results = ParseResults(qosJob.QosResults, servers);
			}
			qosJob.Dispose();
			qosJob.QosResults.Dispose();
			return results;
		}

		public async global::System.Threading.Tasks.Task<global::System.Collections.Generic.List<global::Unity.Services.Qos.Runner.QosAnnotatedResult>> MeasureQosAsync(global::System.Collections.Generic.IList<global::Unity.Services.Qos.Models.QosServiceServer> servers)
		{
			global::System.Collections.Generic.List<global::Unity.Networking.QoS.UcgQosServer> convertedServers = global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Select(global::System.Linq.Enumerable.Where(await global::System.Threading.Tasks.Task.WhenAll(global::System.Linq.Enumerable.Select(servers, ToUcgFormat)), (global::Unity.Networking.QoS.UcgQosServer? s) => s.HasValue), (global::Unity.Networking.QoS.UcgQosServer? s) => s.Value));
			global::System.Collections.Generic.List<global::Unity.Services.Qos.Runner.QosAnnotatedResult> results = new global::System.Collections.Generic.List<global::Unity.Services.Qos.Runner.QosAnnotatedResult>();
			global::Unity.Services.Qos.Runner.IQosJob qosJob = await RunQosJob(convertedServers);
			if (global::System.Linq.Enumerable.Count(servers) == global::System.Linq.Enumerable.Count(qosJob.QosResults))
			{
				results = ParseResults(qosJob.QosResults, servers);
			}
			qosJob.Dispose();
			qosJob.QosResults.Dispose();
			return results;
		}

		public async global::System.Threading.Tasks.Task<global::System.Collections.Generic.List<(global::Unity.Services.Qos.V2.Models.QosServer, global::Unity.Services.Qos.IQosMeasurements)>> MeasureQosV2Async(global::System.Collections.Generic.IList<global::Unity.Services.Qos.V2.Models.QosServer> servers)
		{
			global::System.Collections.Generic.List<global::Unity.Services.Qos.Models.QosServer> servers2 = global::System.Linq.Enumerable.ToList(servers).ConvertAll((global::Unity.Services.Qos.V2.Models.QosServer q) => new global::Unity.Services.Qos.Models.QosServer(q.Endpoints, "unused"));
			global::System.Collections.Generic.List<global::Unity.Services.Qos.Internal.QosResult> list = await MeasureQosAsync(servers2);
			global::System.Collections.Generic.List<(global::Unity.Services.Qos.V2.Models.QosServer, global::Unity.Services.Qos.IQosMeasurements)> list2 = new global::System.Collections.Generic.List<(global::Unity.Services.Qos.V2.Models.QosServer, global::Unity.Services.Qos.IQosMeasurements)>(list.Count);
			if (list.Count == 0 || list.Count != servers.Count)
			{
				return list2;
			}
			global::System.Collections.Generic.List<global::Unity.Services.Qos.Runner.BaselibQosRunner.QosMeasurementImpl> list3 = list.ConvertAll((global::Unity.Services.Qos.Internal.QosResult qr) => new global::Unity.Services.Qos.Runner.BaselibQosRunner.QosMeasurementImpl(qr.AverageLatencyMs, qr.PacketLossPercent));
			for (int num = 0; num < servers.Count; num++)
			{
				list2.Add((servers[num], list3[num]));
			}
			return list2;
		}

		private async global::System.Threading.Tasks.Task<global::Unity.Services.Qos.Runner.IQosJob> RunQosJob(global::System.Collections.Generic.List<global::Unity.Networking.QoS.UcgQosServer> convertedServers)
		{
			string title = "QoS request";
			global::Unity.Services.Qos.Runner.IQosJob job = _qosJobProvider(convertedServers, title);
			global::Unity.Jobs.JobHandle handle = job.Schedule<global::Unity.Networking.QoS.QosJob>();
			while (!handle.IsCompleted)
			{
				await global::System.Threading.Tasks.Task.Yield();
			}
			handle.Complete();
			return job;
		}

		private global::System.Threading.Tasks.Task<global::Unity.Networking.QoS.UcgQosServer?> ToUcgFormat(global::Unity.Services.Qos.Models.QosServer server)
		{
			string serverEndpoint = server.Endpoints[0];
			string region = server.Region;
			return ToUcgFormat(serverEndpoint, region);
		}

		private global::System.Threading.Tasks.Task<global::Unity.Networking.QoS.UcgQosServer?> ToUcgFormat(global::Unity.Services.Qos.Models.QosServiceServer server)
		{
			string serverEndpoint = server.Endpoints[0];
			string region = server.Region;
			return ToUcgFormat(serverEndpoint, region);
		}

		private global::System.Threading.Tasks.Task<global::Unity.Networking.QoS.UcgQosServer?> ToUcgFormat(string serverEndpoint, string serverRegion)
		{
			if (!global::System.Uri.TryCreate("udp://" + serverEndpoint, global::System.UriKind.Absolute, out var uri))
			{
				global::UnityEngine.Debug.LogError("Could not create address from endpoint: '" + serverEndpoint + "'.");
				return global::System.Threading.Tasks.Task.FromResult<global::Unity.Networking.QoS.UcgQosServer?>(null);
			}
			if (uri.Port == -1)
			{
				global::UnityEngine.Debug.LogError("Missing or invalid port in endpoint: '" + serverEndpoint + "'.");
				return global::System.Threading.Tasks.Task.FromResult<global::Unity.Networking.QoS.UcgQosServer?>(null);
			}
			return MakeUcgQosServer();
			static global::System.Net.IPAddress GetIpAddress(in global::System.ReadOnlySpan<global::System.Net.IPAddress> resolvedIps)
			{
				return resolvedIps[0];
			}
			async global::System.Threading.Tasks.Task<global::Unity.Networking.QoS.UcgQosServer?> MakeUcgQosServer()
			{
				global::System.Net.IPAddress[] array = await _dnsResolver(uri.Host);
				if (array.Length == 0)
				{
					global::UnityEngine.Debug.LogError("No addresses could be resolved for host " + uri.Host + ".");
					return null;
				}
				global::System.Net.IPAddress iPAddress = GetIpAddress((global::System.ReadOnlySpan<global::System.Net.IPAddress>)array);
				return new global::Unity.Networking.QoS.UcgQosServer
				{
					regionid = serverRegion,
					ipv4 = ((iPAddress.AddressFamily == global::System.Net.Sockets.AddressFamily.InterNetwork) ? iPAddress.ToString() : null),
					ipv6 = ((iPAddress.AddressFamily == global::System.Net.Sockets.AddressFamily.InterNetworkV6) ? iPAddress.ToString() : null),
					port = global::System.Convert.ToUInt16(uri.Port),
					BackoffUntilUtc = default(global::System.DateTime)
				};
			}
		}

		private static global::System.Collections.Generic.List<global::Unity.Services.Qos.Internal.QosResult> ParseResults(global::System.Collections.Generic.IEnumerable<global::Unity.Networking.QoS.InternalQosResult> ucgResults, global::System.Collections.Generic.IEnumerable<global::Unity.Services.Qos.Models.QosServer> servers)
		{
			global::System.Collections.Generic.List<global::Unity.Services.Qos.Internal.QosResult> list = new global::System.Collections.Generic.List<global::Unity.Services.Qos.Internal.QosResult>();
			using global::System.Collections.Generic.IEnumerator<global::Unity.Services.Qos.Models.QosServer> enumerator = servers.GetEnumerator();
			foreach (global::Unity.Networking.QoS.InternalQosResult ucgResult in ucgResults)
			{
				enumerator.MoveNext();
				if (enumerator.Current == null)
				{
					break;
				}
				int averageLatencyMs = (int)((ucgResult.AverageLatencyMs > int.MaxValue) ? int.MaxValue : ucgResult.AverageLatencyMs);
				list.Add(new global::Unity.Services.Qos.Internal.QosResult
				{
					Region = enumerator.Current.Region,
					AverageLatencyMs = averageLatencyMs,
					PacketLossPercent = ucgResult.PacketLoss
				});
			}
			return list;
		}

		private static global::System.Collections.Generic.List<global::Unity.Services.Qos.Runner.QosAnnotatedResult> ParseResults(global::System.Collections.Generic.IEnumerable<global::Unity.Networking.QoS.InternalQosResult> ucgResults, global::System.Collections.Generic.IEnumerable<global::Unity.Services.Qos.Models.QosServiceServer> servers)
		{
			global::System.Collections.Generic.List<global::Unity.Services.Qos.Runner.QosAnnotatedResult> list = new global::System.Collections.Generic.List<global::Unity.Services.Qos.Runner.QosAnnotatedResult>();
			using global::System.Collections.Generic.IEnumerator<global::Unity.Services.Qos.Models.QosServiceServer> enumerator = servers.GetEnumerator();
			foreach (global::Unity.Networking.QoS.InternalQosResult ucgResult in ucgResults)
			{
				enumerator.MoveNext();
				if (enumerator.Current == null)
				{
					break;
				}
				int averageLatencyMs = (int)((ucgResult.AverageLatencyMs > int.MaxValue) ? int.MaxValue : ucgResult.AverageLatencyMs);
				list.Add(new global::Unity.Services.Qos.Runner.QosAnnotatedResult
				{
					Region = enumerator.Current.Region,
					AverageLatencyMs = averageLatencyMs,
					PacketLossPercent = ucgResult.PacketLoss,
					Annotations = enumerator.Current.Annotations
				});
			}
			return list;
		}
	}
}
