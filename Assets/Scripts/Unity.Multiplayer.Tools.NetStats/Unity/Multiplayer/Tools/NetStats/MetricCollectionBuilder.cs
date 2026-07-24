namespace Unity.Multiplayer.Tools.NetStats
{
	internal class MetricCollectionBuilder
	{
		private readonly global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStats.IMetric<long>> m_Counters = new global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStats.IMetric<long>>();

		private readonly global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStats.IMetric<double>> m_Gauges = new global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStats.IMetric<double>>();

		private readonly global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStats.IMetric<global::System.TimeSpan>> m_Timers = new global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStats.IMetric<global::System.TimeSpan>>();

		private readonly global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStats.IEventMetric> m_PayloadEvents = new global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStats.IEventMetric>();

		public global::Unity.Multiplayer.Tools.NetStats.MetricCollectionBuilder WithCounters(params global::Unity.Multiplayer.Tools.NetStats.Counter[] counters)
		{
			m_Counters.AddRange(counters);
			return this;
		}

		public global::Unity.Multiplayer.Tools.NetStats.MetricCollectionBuilder WithGauges(params global::Unity.Multiplayer.Tools.NetStats.Gauge[] gauges)
		{
			m_Gauges.AddRange(gauges);
			return this;
		}

		public global::Unity.Multiplayer.Tools.NetStats.MetricCollectionBuilder WithTimers(params global::Unity.Multiplayer.Tools.NetStats.Timer[] timers)
		{
			m_Timers.AddRange(timers);
			return this;
		}

		public global::Unity.Multiplayer.Tools.NetStats.MetricCollectionBuilder WithMetricEvents<TEvent>(params global::Unity.Multiplayer.Tools.NetStats.IEventMetric<TEvent>[] metricEvents) where TEvent : struct
		{
			m_PayloadEvents.AddRange(metricEvents);
			return this;
		}

		public global::Unity.Multiplayer.Tools.NetStats.MetricCollection Build()
		{
			return new global::Unity.Multiplayer.Tools.NetStats.MetricCollection(new global::System.Collections.ObjectModel.ReadOnlyDictionary<global::Unity.Multiplayer.Tools.NetStats.MetricId, global::Unity.Multiplayer.Tools.NetStats.IMetric<long>>(global::System.Linq.Enumerable.ToDictionary(m_Counters, (global::Unity.Multiplayer.Tools.NetStats.IMetric<long> x) => x.Id, (global::Unity.Multiplayer.Tools.NetStats.IMetric<long> x) => x)), new global::System.Collections.ObjectModel.ReadOnlyDictionary<global::Unity.Multiplayer.Tools.NetStats.MetricId, global::Unity.Multiplayer.Tools.NetStats.IMetric<double>>(global::System.Linq.Enumerable.ToDictionary(m_Gauges, (global::Unity.Multiplayer.Tools.NetStats.IMetric<double> x) => x.Id, (global::Unity.Multiplayer.Tools.NetStats.IMetric<double> x) => x)), new global::System.Collections.ObjectModel.ReadOnlyDictionary<global::Unity.Multiplayer.Tools.NetStats.MetricId, global::Unity.Multiplayer.Tools.NetStats.IMetric<global::System.TimeSpan>>(global::System.Linq.Enumerable.ToDictionary(m_Timers, (global::Unity.Multiplayer.Tools.NetStats.IMetric<global::System.TimeSpan> x) => x.Id, (global::Unity.Multiplayer.Tools.NetStats.IMetric<global::System.TimeSpan> x) => x)), new global::System.Collections.ObjectModel.ReadOnlyDictionary<global::Unity.Multiplayer.Tools.NetStats.MetricId, global::Unity.Multiplayer.Tools.NetStats.IEventMetric>(global::System.Linq.Enumerable.ToDictionary(m_PayloadEvents, (global::Unity.Multiplayer.Tools.NetStats.IEventMetric x) => x.Id, (global::Unity.Multiplayer.Tools.NetStats.IEventMetric x) => x)));
		}
	}
}
