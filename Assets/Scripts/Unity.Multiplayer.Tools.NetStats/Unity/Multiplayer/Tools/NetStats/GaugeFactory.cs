namespace Unity.Multiplayer.Tools.NetStats
{
	internal class GaugeFactory : global::Unity.Multiplayer.Tools.NetStats.IMetricFactory
	{
		public bool TryConstruct(global::Unity.Multiplayer.Tools.NetStats.MetricHeader header, out global::Unity.Multiplayer.Tools.NetStats.IMetric metric)
		{
			metric = new global::Unity.Multiplayer.Tools.NetStats.Gauge(header.MetricId);
			return true;
		}
	}
}
