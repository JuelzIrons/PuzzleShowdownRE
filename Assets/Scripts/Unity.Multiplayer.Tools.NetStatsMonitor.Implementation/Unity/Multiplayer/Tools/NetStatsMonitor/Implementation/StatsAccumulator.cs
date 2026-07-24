namespace Unity.Multiplayer.Tools.NetStatsMonitor.Implementation
{
	internal class StatsAccumulator
	{
		[global::JetBrains.Annotations.NotNull]
		private readonly global::System.Collections.Generic.Dictionary<global::Unity.Multiplayer.Tools.NetStats.MetricId, float> m_Sums = new global::System.Collections.Generic.Dictionary<global::Unity.Multiplayer.Tools.NetStats.MetricId, float>();

		[global::JetBrains.Annotations.NotNull]
		private readonly global::System.Collections.Generic.Dictionary<global::Unity.Multiplayer.Tools.NetStats.MetricId, int> m_GaugeCounts = new global::System.Collections.Generic.Dictionary<global::Unity.Multiplayer.Tools.NetStats.MetricId, int>();

		internal bool HasAccumulatedStats => LastAccumulationTime > LastCollectionTime;

		internal double LastAccumulationTime { get; set; } = double.MinValue;

		internal double LastCollectionTime { get; set; } = double.MinValue;

		internal global::Unity.Multiplayer.Tools.NetStats.MetricId[] RequiredMetrics { get; private set; } = global::System.Array.Empty<global::Unity.Multiplayer.Tools.NetStats.MetricId>();

		internal bool Contains(global::Unity.Multiplayer.Tools.NetStats.MetricId metricId)
		{
			return m_Sums.ContainsKey(metricId);
		}

		internal void Accumulate(global::Unity.Multiplayer.Tools.NetStats.MetricId metricId, float value)
		{
			m_Sums[metricId] += value;
			if (m_GaugeCounts.TryGetValue(metricId, out var value2))
			{
				m_GaugeCounts[metricId] = value2 + 1;
			}
		}

		internal float Collect(global::Unity.Multiplayer.Tools.NetStats.MetricId metric)
		{
			if (m_Sums.TryGetValue(metric, out var value))
			{
				m_Sums[metric] = 0f;
				if (m_GaugeCounts.TryGetValue(metric, out var value2))
				{
					m_GaugeCounts[metric] = 0;
					return value / (float)global::System.Math.Max(value2, 1);
				}
				return value;
			}
			return 0f;
		}

		internal void UpdateRequirements(global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MultiStatHistoryRequirements requirements, global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate sampleRate)
		{
			foreach (global::Unity.Multiplayer.Tools.NetStats.MetricId item in global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Where(m_Sums.Keys, (global::Unity.Multiplayer.Tools.NetStats.MetricId key) => !Required(key))))
			{
				m_Sums.Remove(item);
			}
			foreach (global::Unity.Multiplayer.Tools.NetStats.MetricId item2 in global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Where(m_GaugeCounts.Keys, (global::Unity.Multiplayer.Tools.NetStats.MetricId key) => !Required(key))))
			{
				m_GaugeCounts.Remove(item2);
			}
			foreach (global::Unity.Multiplayer.Tools.NetStats.MetricId key in requirements.Data.Keys)
			{
				if (Required(key))
				{
					if (!m_Sums.ContainsKey(key))
					{
						m_Sums.Add(key, 0f);
					}
					if (key.MetricKind == global::Unity.Multiplayer.Tools.NetStats.MetricKind.Gauge && !m_GaugeCounts.ContainsKey(key))
					{
						m_GaugeCounts.Add(key, 0);
					}
				}
			}
			RequiredMetrics = global::System.Linq.Enumerable.ToArray(m_Sums.Keys);
			bool Required(global::Unity.Multiplayer.Tools.NetStats.MetricId metric)
			{
				if (!requirements.Data.TryGetValue(metric, out var value))
				{
					return false;
				}
				if (value.SampleCounts[sampleRate] <= 0)
				{
					if (value.DecayConstants.Count > 0)
					{
						return sampleRate == global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate.PerFrame;
					}
					return false;
				}
				return true;
			}
		}
	}
}
