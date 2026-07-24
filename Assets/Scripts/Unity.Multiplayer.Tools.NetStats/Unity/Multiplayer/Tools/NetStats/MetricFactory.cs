namespace Unity.Multiplayer.Tools.NetStats
{
	internal class MetricFactory
	{
		private readonly global::System.Collections.Generic.Dictionary<global::Unity.Multiplayer.Tools.NetStats.MetricContainerType, global::Unity.Multiplayer.Tools.NetStats.IMetricFactory> k_Factories = new global::System.Collections.Generic.Dictionary<global::Unity.Multiplayer.Tools.NetStats.MetricContainerType, global::Unity.Multiplayer.Tools.NetStats.IMetricFactory>
		{
			{
				global::Unity.Multiplayer.Tools.NetStats.MetricContainerType.Counter,
				new global::Unity.Multiplayer.Tools.NetStats.CounterFactory()
			},
			{
				global::Unity.Multiplayer.Tools.NetStats.MetricContainerType.Event,
				new global::Unity.Multiplayer.Tools.NetStats.EventMetricFactory()
			},
			{
				global::Unity.Multiplayer.Tools.NetStats.MetricContainerType.Gauge,
				new global::Unity.Multiplayer.Tools.NetStats.GaugeFactory()
			},
			{
				global::Unity.Multiplayer.Tools.NetStats.MetricContainerType.Timer,
				new global::Unity.Multiplayer.Tools.NetStats.TimerFactory()
			}
		};

		public bool TryConstruct(global::Unity.Multiplayer.Tools.NetStats.MetricHeader header, out global::Unity.Multiplayer.Tools.NetStats.IMetric metric)
		{
			if (!k_Factories.TryGetValue(header.MetricContainerType, out var value))
			{
				global::UnityEngine.Debug.LogError("Failed to find factory for type " + header.MetricContainerType);
				metric = null;
				return false;
			}
			return value.TryConstruct(header, out metric);
		}
	}
}
