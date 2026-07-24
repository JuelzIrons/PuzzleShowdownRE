namespace Unity.Multiplayer.Tools.NetStatsMonitor.Implementation
{
	internal class MultiStatHistory
	{
		[global::JetBrains.Annotations.NotNull]
		private readonly global::System.Collections.Generic.Dictionary<global::Unity.Multiplayer.Tools.NetStats.MetricId, global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.StatHistory> m_Data = new global::System.Collections.Generic.Dictionary<global::Unity.Multiplayer.Tools.NetStats.MetricId, global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.StatHistory>();

		[global::JetBrains.Annotations.NotNull]
		public global::System.Collections.Generic.IReadOnlyDictionary<global::Unity.Multiplayer.Tools.NetStats.MetricId, global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.StatHistory> Data => m_Data;

		[global::JetBrains.Annotations.NotNull]
		public global::Unity.Multiplayer.Tools.Common.EnumMap<global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate, global::Unity.Multiplayer.Tools.Common.RingBuffer<double>> TimeStamps { get; } = new global::Unity.Multiplayer.Tools.Common.EnumMap<global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate, global::Unity.Multiplayer.Tools.Common.RingBuffer<double>>
		{
			{
				global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate.PerFrame,
				new global::Unity.Multiplayer.Tools.Common.RingBuffer<double>(0)
			},
			{
				global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate.PerSecond,
				new global::Unity.Multiplayer.Tools.Common.RingBuffer<double>(0)
			}
		};

		public void Clear()
		{
			m_Data.Clear();
		}

		public void Collect(global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate rate, global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.StatsAccumulator statsAccumulator, double time)
		{
			TimeStamps[rate].PushBack(time);
			global::Unity.Multiplayer.Tools.NetStats.MetricId[] requiredMetrics = statsAccumulator.RequiredMetrics;
			foreach (global::Unity.Multiplayer.Tools.NetStats.MetricId metricId in requiredMetrics)
			{
				global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.StatHistory statHistory = m_Data[metricId];
				float value = statsAccumulator.Collect(metricId);
				statHistory.Update(metricId, rate, value, time);
			}
			statsAccumulator.LastCollectionTime = time;
		}

		internal void UpdateRequirements(global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MultiStatHistoryRequirements requirements)
		{
			global::System.Collections.Generic.Dictionary<global::Unity.Multiplayer.Tools.NetStats.MetricId, global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.StatHistoryRequirements> allStatRequirements = requirements.Data;
			foreach (global::Unity.Multiplayer.Tools.NetStats.MetricId item in global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Where(m_Data.Keys, (global::Unity.Multiplayer.Tools.NetStats.MetricId metricId) => !allStatRequirements.ContainsKey(metricId))))
			{
				m_Data.Remove(item);
			}
			global::Unity.Multiplayer.Tools.Common.EnumMap<global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate, int> enumMap = new global::Unity.Multiplayer.Tools.Common.EnumMap<global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate, int>(0);
			foreach (global::System.Collections.Generic.KeyValuePair<global::Unity.Multiplayer.Tools.NetStats.MetricId, global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.StatHistoryRequirements> item2 in allStatRequirements)
			{
				item2.Deconstruct(out var key, out var value);
				global::Unity.Multiplayer.Tools.NetStats.MetricId key2 = key;
				global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.StatHistoryRequirements statHistoryRequirements = value;
				for (global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate sampleRate = global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate.PerFrame; sampleRate <= global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate.PerSecond; sampleRate = sampleRate.Next())
				{
					enumMap[sampleRate] = global::System.Math.Max(enumMap[sampleRate], statHistoryRequirements.SampleCounts[sampleRate]);
				}
				if (m_Data.ContainsKey(key2))
				{
					m_Data[key2].UpdateRequirements(statHistoryRequirements);
				}
				else
				{
					m_Data[key2] = new global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.StatHistory(statHistoryRequirements);
				}
			}
			for (global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate sampleRate2 = global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate.PerFrame; sampleRate2 <= global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate.PerSecond; sampleRate2 = sampleRate2.Next())
			{
				TimeStamps[sampleRate2].Capacity = enumMap[sampleRate2] + 1;
				for (int num = 0; num < 1; num++)
				{
					TimeStamps[sampleRate2].PushBack(0.0);
				}
			}
		}

		private double? GetSimpleMovingAverageForCounter(global::Unity.Multiplayer.Tools.NetStats.MetricId metricId, global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate sampleRate, int maxSampleCount, double time)
		{
			if (!Data.TryGetValue(metricId, out var value))
			{
				return null;
			}
			global::Unity.Multiplayer.Tools.Common.RingBuffer<float> ringBuffer = value.SampleBuffers[sampleRate];
			int num = global::System.Math.Min(maxSampleCount, ringBuffer.Length);
			if (num <= 1)
			{
				return null;
			}
			float num2 = global::Unity.Multiplayer.Tools.Common.RingBufferExtensions.SumLastN(ringBuffer, num);
			double num3 = TimeSpanOfLastNSamples(sampleRate, num);
			return (double)num2 / num3;
		}

		private double? GetSimpleMovingAverageForGauge(global::Unity.Multiplayer.Tools.NetStats.MetricId metricId, global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate sampleRate, int maxSampleCount)
		{
			if (!Data.TryGetValue(metricId, out var value))
			{
				return null;
			}
			global::Unity.Multiplayer.Tools.Common.RingBuffer<float> ringBuffer = value.SampleBuffers[sampleRate];
			int num = global::System.Math.Min(maxSampleCount, ringBuffer.Length);
			if (num <= 0)
			{
				return null;
			}
			return global::Unity.Multiplayer.Tools.Common.RingBufferExtensions.SumLastN(ringBuffer, num) / (float)num;
		}

		public double? GetSimpleMovingAverage(global::Unity.Multiplayer.Tools.NetStats.MetricId metricId, global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate sampleRate, int maxSampleCount, double time)
		{
			global::Unity.Multiplayer.Tools.NetStats.MetricKind metricKind = metricId.MetricKind;
			return metricKind switch
			{
				global::Unity.Multiplayer.Tools.NetStats.MetricKind.Counter => GetSimpleMovingAverageForCounter(metricId, sampleRate, maxSampleCount, time), 
				global::Unity.Multiplayer.Tools.NetStats.MetricKind.Gauge => GetSimpleMovingAverageForGauge(metricId, sampleRate, maxSampleCount), 
				_ => throw new global::System.NotSupportedException(string.Format("Unhandled {0} {1}", "MetricKind", metricKind)), 
			};
		}

		internal double TimeSpanOfLastNSamples(global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate sampleRate, int sampleCount)
		{
			global::Unity.Multiplayer.Tools.Common.RingBuffer<double> ringBuffer = TimeStamps[sampleRate];
			int num = global::System.Math.Min(sampleCount + 1, ringBuffer.Length);
			if (num <= 1)
			{
				return 0.0;
			}
			double num2 = ringBuffer[^num];
			return ringBuffer.MostRecent - num2;
		}
	}
}
