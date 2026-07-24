namespace Unity.Multiplayer.Tools.NetStats
{
	[global::System.Serializable]
	internal sealed class MetricCollection
	{
		private global::System.Collections.Generic.IReadOnlyDictionary<global::Unity.Multiplayer.Tools.NetStats.MetricId, global::Unity.Multiplayer.Tools.NetStats.IMetric<long>> m_Counters;

		private global::System.Collections.Generic.IReadOnlyDictionary<global::Unity.Multiplayer.Tools.NetStats.MetricId, global::Unity.Multiplayer.Tools.NetStats.IMetric<double>> m_Gauges;

		private global::System.Collections.Generic.IReadOnlyDictionary<global::Unity.Multiplayer.Tools.NetStats.MetricId, global::Unity.Multiplayer.Tools.NetStats.IMetric<global::System.TimeSpan>> m_Timers;

		private global::System.Collections.Generic.IReadOnlyDictionary<global::Unity.Multiplayer.Tools.NetStats.MetricId, global::Unity.Multiplayer.Tools.NetStats.IEventMetric> m_PayloadEvents;

		public global::System.Collections.Generic.IReadOnlyList<global::Unity.Multiplayer.Tools.NetStats.IMetric> Metrics { get; }

		public ulong ConnectionId { get; set; } = ulong.MaxValue;

		internal MetricCollection(global::System.Collections.Generic.IReadOnlyDictionary<global::Unity.Multiplayer.Tools.NetStats.MetricId, global::Unity.Multiplayer.Tools.NetStats.IMetric<long>> counters, global::System.Collections.Generic.IReadOnlyDictionary<global::Unity.Multiplayer.Tools.NetStats.MetricId, global::Unity.Multiplayer.Tools.NetStats.IMetric<double>> gauges, global::System.Collections.Generic.IReadOnlyDictionary<global::Unity.Multiplayer.Tools.NetStats.MetricId, global::Unity.Multiplayer.Tools.NetStats.IMetric<global::System.TimeSpan>> timers, global::System.Collections.Generic.IReadOnlyDictionary<global::Unity.Multiplayer.Tools.NetStats.MetricId, global::Unity.Multiplayer.Tools.NetStats.IEventMetric> payloadEvents)
		{
			m_Counters = counters;
			m_Gauges = gauges;
			m_Timers = timers;
			m_PayloadEvents = payloadEvents;
			Metrics = global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Concat(global::System.Linq.Enumerable.Concat(global::System.Linq.Enumerable.Concat((global::System.Collections.Generic.IEnumerable<global::Unity.Multiplayer.Tools.NetStats.IMetric>)counters.Values, (global::System.Collections.Generic.IEnumerable<global::Unity.Multiplayer.Tools.NetStats.IMetric>)gauges.Values), timers.Values), m_PayloadEvents.Values));
		}

		internal MetricCollection(global::System.Collections.Generic.IReadOnlyCollection<global::Unity.Multiplayer.Tools.NetStats.IMetric> metrics, ulong localConnectionId)
		{
			m_Counters = global::System.Linq.Enumerable.ToDictionary(global::System.Linq.Enumerable.OfType<global::Unity.Multiplayer.Tools.NetStats.IMetric<long>>(metrics), ByMetricId);
			m_Gauges = global::System.Linq.Enumerable.ToDictionary(global::System.Linq.Enumerable.OfType<global::Unity.Multiplayer.Tools.NetStats.IMetric<double>>(metrics), ByMetricId);
			m_Timers = global::System.Linq.Enumerable.ToDictionary(global::System.Linq.Enumerable.OfType<global::Unity.Multiplayer.Tools.NetStats.IMetric<global::System.TimeSpan>>(metrics), ByMetricId);
			m_PayloadEvents = global::System.Linq.Enumerable.ToDictionary(global::System.Linq.Enumerable.OfType<global::Unity.Multiplayer.Tools.NetStats.IEventMetric>(metrics), ByMetricId);
			ConnectionId = localConnectionId;
			static global::Unity.Multiplayer.Tools.NetStats.MetricId ByMetricId(global::Unity.Multiplayer.Tools.NetStats.IMetric metric)
			{
				return metric.Id;
			}
		}

		public bool TryGetCounter(global::Unity.Multiplayer.Tools.NetStats.MetricId metricId, out global::Unity.Multiplayer.Tools.NetStats.IMetric<long> counter)
		{
			return m_Counters.TryGetValue(metricId, out counter);
		}

		public global::Unity.Multiplayer.Tools.NetStats.IMetric<long> GetCounterOrDefault(global::Unity.Multiplayer.Tools.NetStats.MetricId metricId)
		{
			if (TryGetCounter(metricId, out var counter))
			{
				return counter;
			}
			return null;
		}

		public bool TryGetGauge(global::Unity.Multiplayer.Tools.NetStats.MetricId metricId, out global::Unity.Multiplayer.Tools.NetStats.IMetric<double> gauge)
		{
			return m_Gauges.TryGetValue(metricId, out gauge);
		}

		public bool TryGetTimer(global::Unity.Multiplayer.Tools.NetStats.MetricId metricId, out global::Unity.Multiplayer.Tools.NetStats.IMetric<global::System.TimeSpan> timer)
		{
			return m_Timers.TryGetValue(metricId, out timer);
		}

		public bool TryGetEvent<TEvent>(global::Unity.Multiplayer.Tools.NetStats.MetricId metricId, out global::Unity.Multiplayer.Tools.NetStats.IEventMetric<TEvent> metricEvent)
		{
			if (m_PayloadEvents.TryGetValue(metricId, out var value) && value is global::Unity.Multiplayer.Tools.NetStats.IEventMetric<TEvent> eventMetric)
			{
				metricEvent = eventMetric;
				return true;
			}
			metricEvent = null;
			return false;
		}

		public global::Unity.Multiplayer.Tools.NetStats.IEventMetric<TEvent> GetPayloadEventOrDefault<TEvent>(global::Unity.Multiplayer.Tools.NetStats.MetricId metricId)
		{
			if (TryGetEvent(metricId, out global::Unity.Multiplayer.Tools.NetStats.IEventMetric<TEvent> metricEvent))
			{
				return metricEvent;
			}
			return null;
		}

		public int GetEventCount(global::Unity.Multiplayer.Tools.NetStats.MetricId metricId)
		{
			if (m_PayloadEvents.TryGetValue(metricId, out var value))
			{
				return value.Count;
			}
			return 0;
		}
	}
}
