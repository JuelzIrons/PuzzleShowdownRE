namespace Unity.Multiplayer.Tools.NetStats
{
	internal interface IMetricFactory
	{
		bool TryConstruct(global::Unity.Multiplayer.Tools.NetStats.MetricHeader header, out global::Unity.Multiplayer.Tools.NetStats.IMetric metric);
	}
}
