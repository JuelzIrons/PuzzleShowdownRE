namespace Unity.Multiplayer.Tools.NetStats
{
	internal class CounterFactory : global::Unity.Multiplayer.Tools.NetStats.IMetricFactory
	{
		public bool TryConstruct(global::Unity.Multiplayer.Tools.NetStats.MetricHeader header, out global::Unity.Multiplayer.Tools.NetStats.IMetric metric)
		{
			metric = new global::Unity.Multiplayer.Tools.NetStats.Counter(header.MetricId, 0L);
			return true;
		}
	}
}
