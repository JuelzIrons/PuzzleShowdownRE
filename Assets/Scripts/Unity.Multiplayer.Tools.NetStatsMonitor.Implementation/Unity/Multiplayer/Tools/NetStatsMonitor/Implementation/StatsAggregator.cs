namespace Unity.Multiplayer.Tools.NetStatsMonitor.Implementation
{
	internal static class StatsAggregator
	{
		internal static void UpdateAccumulatorWithStatsFromMetrics(global::Unity.Multiplayer.Tools.NetStats.MetricCollection metrics, global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.StatsAccumulator statsAccumulator, double time)
		{
			global::Unity.Multiplayer.Tools.NetStats.MetricId[] requiredMetrics = statsAccumulator.RequiredMetrics;
			for (int i = 0; i < requiredMetrics.Length; i++)
			{
				global::Unity.Multiplayer.Tools.NetStats.MetricId metricId = requiredMetrics[i];
				global::Unity.Multiplayer.Tools.NetStats.MetricKind metricKind = metricId.MetricKind;
				switch (metricKind)
				{
				case global::Unity.Multiplayer.Tools.NetStats.MetricKind.Counter:
				{
					if (metrics.TryGetCounter(metricId, out var counter))
					{
						statsAccumulator.Accumulate(metricId, counter.Value);
						break;
					}
					int eventCount = metrics.GetEventCount(metricId);
					statsAccumulator.Accumulate(metricId, eventCount);
					break;
				}
				case global::Unity.Multiplayer.Tools.NetStats.MetricKind.Gauge:
				{
					if (metrics.TryGetGauge(metricId, out var gauge))
					{
						statsAccumulator.Accumulate(metricId, (float)gauge.Value);
					}
					break;
				}
				default:
					throw new global::System.NotSupportedException(string.Format("Unhandled {0} {1}", "MetricKind", metricKind));
				}
			}
			statsAccumulator.LastAccumulationTime = time;
		}
	}
}
