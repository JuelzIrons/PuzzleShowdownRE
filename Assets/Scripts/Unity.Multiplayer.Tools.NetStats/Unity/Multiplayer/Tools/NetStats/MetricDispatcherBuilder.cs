namespace Unity.Multiplayer.Tools.NetStats
{
	internal sealed class MetricDispatcherBuilder
	{
		private readonly global::System.Collections.Generic.IDictionary<global::Unity.Multiplayer.Tools.NetStats.MetricId, global::Unity.Multiplayer.Tools.NetStats.IMetric<long>> m_Counters = new global::System.Collections.Generic.Dictionary<global::Unity.Multiplayer.Tools.NetStats.MetricId, global::Unity.Multiplayer.Tools.NetStats.IMetric<long>>();

		private readonly global::System.Collections.Generic.IDictionary<global::Unity.Multiplayer.Tools.NetStats.MetricId, global::Unity.Multiplayer.Tools.NetStats.IMetric<double>> m_Gauges = new global::System.Collections.Generic.Dictionary<global::Unity.Multiplayer.Tools.NetStats.MetricId, global::Unity.Multiplayer.Tools.NetStats.IMetric<double>>();

		private readonly global::System.Collections.Generic.IDictionary<global::Unity.Multiplayer.Tools.NetStats.MetricId, global::Unity.Multiplayer.Tools.NetStats.IMetric<global::System.TimeSpan>> m_Timers = new global::System.Collections.Generic.Dictionary<global::Unity.Multiplayer.Tools.NetStats.MetricId, global::Unity.Multiplayer.Tools.NetStats.IMetric<global::System.TimeSpan>>();

		private readonly global::System.Collections.Generic.IDictionary<global::Unity.Multiplayer.Tools.NetStats.MetricId, global::Unity.Multiplayer.Tools.NetStats.IEventMetric> m_PayloadEvents = new global::System.Collections.Generic.Dictionary<global::Unity.Multiplayer.Tools.NetStats.MetricId, global::Unity.Multiplayer.Tools.NetStats.IEventMetric>();

		private readonly global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStats.IResettable> m_Resettables = new global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStats.IResettable>();

		public global::Unity.Multiplayer.Tools.NetStats.MetricDispatcherBuilder WithCounters(params global::Unity.Multiplayer.Tools.NetStats.Counter[] counters)
		{
			foreach (global::Unity.Multiplayer.Tools.NetStats.Counter counter in counters)
			{
				m_Counters[counter.Id] = counter;
				m_Resettables.Add(counter);
			}
			return this;
		}

		public global::Unity.Multiplayer.Tools.NetStats.MetricDispatcherBuilder WithGauges(params global::Unity.Multiplayer.Tools.NetStats.Gauge[] gauges)
		{
			foreach (global::Unity.Multiplayer.Tools.NetStats.Gauge gauge in gauges)
			{
				m_Gauges[gauge.Id] = gauge;
				m_Resettables.Add(gauge);
			}
			return this;
		}

		public global::Unity.Multiplayer.Tools.NetStats.MetricDispatcherBuilder WithTimers(params global::Unity.Multiplayer.Tools.NetStats.Timer[] timers)
		{
			foreach (global::Unity.Multiplayer.Tools.NetStats.Timer timer in timers)
			{
				m_Timers[timer.Id] = timer;
				m_Resettables.Add(timer);
			}
			return this;
		}

		public global::Unity.Multiplayer.Tools.NetStats.MetricDispatcherBuilder WithMetricEvents<TEvent>(params global::Unity.Multiplayer.Tools.NetStats.EventMetric<TEvent>[] metricEvents) where TEvent : unmanaged
		{
			foreach (global::Unity.Multiplayer.Tools.NetStats.EventMetric<TEvent> eventMetric in metricEvents)
			{
				m_PayloadEvents[eventMetric.Id] = eventMetric;
				m_Resettables.Add(eventMetric);
			}
			return this;
		}

		public global::Unity.Multiplayer.Tools.NetStats.IMetricDispatcher Build()
		{
			return new global::Unity.Multiplayer.Tools.NetStats.MetricDispatcher(new global::Unity.Multiplayer.Tools.NetStats.MetricCollection(new global::System.Collections.ObjectModel.ReadOnlyDictionary<global::Unity.Multiplayer.Tools.NetStats.MetricId, global::Unity.Multiplayer.Tools.NetStats.IMetric<long>>(m_Counters), new global::System.Collections.ObjectModel.ReadOnlyDictionary<global::Unity.Multiplayer.Tools.NetStats.MetricId, global::Unity.Multiplayer.Tools.NetStats.IMetric<double>>(m_Gauges), new global::System.Collections.ObjectModel.ReadOnlyDictionary<global::Unity.Multiplayer.Tools.NetStats.MetricId, global::Unity.Multiplayer.Tools.NetStats.IMetric<global::System.TimeSpan>>(m_Timers), new global::System.Collections.ObjectModel.ReadOnlyDictionary<global::Unity.Multiplayer.Tools.NetStats.MetricId, global::Unity.Multiplayer.Tools.NetStats.IEventMetric>(m_PayloadEvents)), m_Resettables, global::System.Linq.Enumerable.ToList(m_PayloadEvents.Values));
		}
	}
}
