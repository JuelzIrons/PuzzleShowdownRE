namespace Unity.Multiplayer.Tools.NetStatsMonitor.Implementation
{
	internal class GraphDataSampler
	{
		private readonly global::System.Collections.Generic.Dictionary<global::Unity.Multiplayer.Tools.NetStats.MetricId, global::Unity.Multiplayer.Tools.Common.RingBuffer<float>> m_PointValues = new global::System.Collections.Generic.Dictionary<global::Unity.Multiplayer.Tools.NetStats.MetricId, global::Unity.Multiplayer.Tools.Common.RingBuffer<float>>();

		public global::System.Collections.Generic.IReadOnlyDictionary<global::Unity.Multiplayer.Tools.NetStats.MetricId, global::Unity.Multiplayer.Tools.Common.RingBuffer<float>> PointValues => m_PointValues;

		public void UpdateConfiguration(global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStats.MetricId> stats)
		{
			foreach (global::Unity.Multiplayer.Tools.NetStats.MetricId item in global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Where(m_PointValues.Keys, (global::Unity.Multiplayer.Tools.NetStats.MetricId key) => !stats.Contains(key))))
			{
				m_PointValues.Remove(item);
			}
			foreach (global::Unity.Multiplayer.Tools.NetStats.MetricId stat in stats)
			{
				if (!m_PointValues.ContainsKey(stat))
				{
					m_PointValues.Add(stat, new global::Unity.Multiplayer.Tools.Common.RingBuffer<float>(0));
				}
			}
		}

		public void ResizeBuffersIfNeeded(in global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphBufferParameters bufferParams)
		{
			int graphWidthPoints = bufferParams.GraphWidthPoints;
			foreach (global::Unity.Multiplayer.Tools.Common.RingBuffer<float> value in m_PointValues.Values)
			{
				value.Capacity = graphWidthPoints;
				value.Length = graphWidthPoints;
			}
		}

		public void SampleNewPoints(global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MultiStatHistory history, global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStats.MetricId> stats, global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate rate, int graphWidthPoints, int graphWidthSamples, float graphSamplesPerPoint, int pointsToAdvance)
		{
			if (pointsToAdvance <= 0)
			{
				return;
			}
			global::Unity.Multiplayer.Tools.Common.RingBuffer<double> ringBuffer = history.TimeStamps[rate];
			foreach (global::Unity.Multiplayer.Tools.NetStats.MetricId stat in stats)
			{
				global::Unity.Multiplayer.Tools.Common.RingBuffer<float> ringBuffer2 = history.Data[stat].SampleBuffers[rate];
				int length = ringBuffer2.Length;
				global::Unity.Multiplayer.Tools.Common.RingBuffer<float> ringBuffer3 = m_PointValues[stat];
				int num = length - graphWidthSamples;
				int num2 = global::System.Math.Max(graphWidthPoints - pointsToAdvance, 0);
				global::Unity.Multiplayer.Tools.NetStats.MetricKind metricKind = stat.MetricKind;
				float sampleIndex = (float)num + (float)num2 * graphSamplesPerPoint;
				switch (metricKind)
				{
				case global::Unity.Multiplayer.Tools.NetStats.MetricKind.Counter:
				{
					int timeStampOffset = ringBuffer.Length - length;
					for (int j = num2; j < graphWidthPoints; j++)
					{
						float value2 = SampleCounter(ringBuffer, timeStampOffset, ringBuffer2, length, graphSamplesPerPoint, ref sampleIndex);
						ringBuffer3.PushBack(value2);
					}
					break;
				}
				case global::Unity.Multiplayer.Tools.NetStats.MetricKind.Gauge:
				{
					for (int i = num2; i < graphWidthPoints; i++)
					{
						float value = SampleGauge(ringBuffer2, length, graphSamplesPerPoint, ref sampleIndex);
						ringBuffer3.PushBack(value);
					}
					break;
				}
				}
			}
		}

		public static float SampleCounter(global::Unity.Multiplayer.Tools.Common.RingBuffer<double> timeStamps, int timeStampOffset, global::Unity.Multiplayer.Tools.Common.RingBuffer<float> statData, int sampleCount, float graphSamplesPerPoint, ref float sampleIndex)
		{
			float num = global::System.Math.Clamp(sampleIndex, 0f, sampleCount);
			float num2 = global::System.Math.Clamp(sampleIndex + graphSamplesPerPoint, 0f, sampleCount);
			if (num2 <= num)
			{
				return 0f;
			}
			int num3 = (int)global::System.Math.Floor(num);
			int num4 = (int)global::System.Math.Ceiling(num2) - 1;
			float num5 = num - (float)num3;
			float num6 = num2 - (float)num4;
			double num7 = (double)(1f - num5) * timeStamps[timeStampOffset + num3 - 1] + (double)num5 * timeStamps[timeStampOffset + num3];
			double num8 = (double)(1f - num6) * timeStamps[timeStampOffset + num4 - 1] + (double)num6 * timeStamps[timeStampOffset + num4];
			float num9 = (global::System.Math.Min(num3 + 1, num2) - num) * statData[num3];
			for (int i = num3 + 1; i < num4; i++)
			{
				num9 += statData[i];
			}
			if (num4 > num3)
			{
				num9 += num6 * statData[num4];
			}
			double num10 = num8 - num7;
			float result = num9 / (float)num10;
			sampleIndex = num2;
			return result;
		}

		public static float SampleGauge(global::Unity.Multiplayer.Tools.Common.RingBuffer<float> statData, int sampleCount, float graphSamplesPerPoint, ref float sampleIndex)
		{
			float num = global::System.Math.Clamp(sampleIndex, 0f, sampleCount);
			float num2 = global::System.Math.Clamp(sampleIndex + graphSamplesPerPoint, 0f, sampleCount);
			if (num2 <= num)
			{
				return 0f;
			}
			int num3 = (int)global::System.Math.Floor(num);
			int num4 = (int)global::System.Math.Ceiling(num2) - 1;
			float num5 = (global::System.Math.Min(num3 + 1, num2) - num) * statData[num3];
			for (int i = num3 + 1; i < num4; i++)
			{
				num5 += statData[i];
			}
			if (num4 > num3)
			{
				float num6 = num2 - (float)num4;
				num5 += num6 * statData[num4];
			}
			float result = num5 / (num2 - num);
			sampleIndex = num2;
			return result;
		}
	}
}
