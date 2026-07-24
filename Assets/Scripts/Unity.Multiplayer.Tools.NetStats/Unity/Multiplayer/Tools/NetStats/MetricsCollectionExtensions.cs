namespace Unity.Multiplayer.Tools.NetStats
{
	internal static class MetricsCollectionExtensions
	{
		public static global::System.Collections.Generic.IReadOnlyList<TMetric> GetEventValues<TMetric>(this global::Unity.Multiplayer.Tools.NetStats.MetricCollection collection, global::Unity.Multiplayer.Tools.NetStats.MetricId metricId)
		{
			if (!collection.TryGetEvent(metricId, out global::Unity.Multiplayer.Tools.NetStats.IEventMetric<TMetric> metricEvent))
			{
				return global::System.Array.Empty<TMetric>();
			}
			return metricEvent.Values;
		}
	}
}
