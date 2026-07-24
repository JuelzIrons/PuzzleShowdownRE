namespace Unity.Multiplayer.Tools.NetStatsMonitor.Implementation
{
	internal class StatHistory
	{
		public global::Unity.Multiplayer.Tools.Common.ContinuousExponentialMovingAverage[] ContinuousExponentialMovingAverages { get; private set; }

		public global::Unity.Multiplayer.Tools.Common.EnumMap<global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate, global::Unity.Multiplayer.Tools.Common.RingBuffer<float>> SampleBuffers { get; } = new global::Unity.Multiplayer.Tools.Common.EnumMap<global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate, global::Unity.Multiplayer.Tools.Common.RingBuffer<float>>();

		public StatHistory(global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.StatHistoryRequirements requirements)
		{
			ContinuousExponentialMovingAverages = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(requirements.DecayConstants, (double decayConstant) => new global::Unity.Multiplayer.Tools.Common.ContinuousExponentialMovingAverage(decayConstant)));
			for (global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate sampleRate = global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate.PerFrame; sampleRate <= global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate.PerSecond; sampleRate = sampleRate.Next())
			{
				SampleBuffers[sampleRate] = new global::Unity.Multiplayer.Tools.Common.RingBuffer<float>(requirements.SampleCounts[sampleRate]);
			}
		}

		private static global::Unity.Multiplayer.Tools.Common.EnumMap<global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate, int> GetSampleBufferCapacities(global::Unity.Multiplayer.Tools.Common.EnumMap<global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate, global::Unity.Multiplayer.Tools.Common.RingBuffer<float>> sampleBuffers)
		{
			global::Unity.Multiplayer.Tools.Common.EnumMap<global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate, int> enumMap = new global::Unity.Multiplayer.Tools.Common.EnumMap<global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate, int>();
			for (global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate sampleRate = global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate.PerFrame; sampleRate <= global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate.PerSecond; sampleRate = sampleRate.Next())
			{
				enumMap[sampleRate] = sampleBuffers[sampleRate].Capacity;
			}
			return enumMap;
		}

		private global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.StatHistoryRequirements GetRequirements()
		{
			return new global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.StatHistoryRequirements(global::System.Linq.Enumerable.Select(ContinuousExponentialMovingAverages, (global::Unity.Multiplayer.Tools.Common.ContinuousExponentialMovingAverage cema) => cema.DecayConstant), GetSampleBufferCapacities(SampleBuffers));
		}

		internal void UpdateRequirements(global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.StatHistoryRequirements requirements)
		{
			for (global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate sampleRate = global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate.PerFrame; sampleRate <= global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate.PerSecond; sampleRate = sampleRate.Next())
			{
				SampleBuffers[sampleRate].Capacity = requirements.SampleCounts[sampleRate];
			}
			global::Unity.Multiplayer.Tools.Common.ContinuousExponentialMovingAverage[] existingAverages = ContinuousExponentialMovingAverages;
			ContinuousExponentialMovingAverages = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(requirements.DecayConstants, delegate(double decayConstant)
			{
				global::Unity.Multiplayer.Tools.Common.ContinuousExponentialMovingAverage continuousExponentialMovingAverage = global::System.Linq.Enumerable.FirstOrDefault(existingAverages, (global::Unity.Multiplayer.Tools.Common.ContinuousExponentialMovingAverage existingCema) => existingCema.DecayConstant == decayConstant);
				return (continuousExponentialMovingAverage == null) ? new global::Unity.Multiplayer.Tools.Common.ContinuousExponentialMovingAverage(decayConstant) : new global::Unity.Multiplayer.Tools.Common.ContinuousExponentialMovingAverage(decayConstant, continuousExponentialMovingAverage.LastValue, continuousExponentialMovingAverage.LastTime);
			}));
		}

		public void Update(global::Unity.Multiplayer.Tools.NetStats.MetricId metric, global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate rate, float value, double time)
		{
			SampleBuffers[rate].PushBack(value);
			if (rate != global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate.PerFrame)
			{
				return;
			}
			switch (metric.MetricKind)
			{
			case global::Unity.Multiplayer.Tools.NetStats.MetricKind.Counter:
			{
				global::Unity.Multiplayer.Tools.Common.ContinuousExponentialMovingAverage[] continuousExponentialMovingAverages = ContinuousExponentialMovingAverages;
				for (int i = 0; i < continuousExponentialMovingAverages.Length; i++)
				{
					continuousExponentialMovingAverages[i].AddSampleForCounter(value, time);
				}
				break;
			}
			case global::Unity.Multiplayer.Tools.NetStats.MetricKind.Gauge:
			{
				global::Unity.Multiplayer.Tools.Common.ContinuousExponentialMovingAverage[] continuousExponentialMovingAverages = ContinuousExponentialMovingAverages;
				for (int i = 0; i < continuousExponentialMovingAverages.Length; i++)
				{
					continuousExponentialMovingAverages[i].AddSampleForGauge(value, time);
				}
				break;
			}
			}
		}
	}
}
