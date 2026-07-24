namespace Unity.Multiplayer.Tools.NetStatsMonitor.Implementation
{
	internal class GraphInputSynchronizer
	{
		private double m_LastReadSampleTimeStamp = double.NegativeInfinity;

		private float m_FractionRemainingOfLastReadSample;

		private (int lastReadSampleIndex, int unreadTimeStampCount) ComputeLastReadSampleIndexAndUnreadSampleCount(global::Unity.Multiplayer.Tools.Common.RingBuffer<double> timeStamps)
		{
			int num = timeStamps.Length - 1;
			int num2 = num;
			while (num2 >= 0 && timeStamps[num2] > m_LastReadSampleTimeStamp)
			{
				num2--;
			}
			int item = num - num2;
			return (lastReadSampleIndex: num2, unreadTimeStampCount: item);
		}

		public int ComputeNumberOfPointsToAdvance(global::Unity.Multiplayer.Tools.Common.RingBuffer<double> timeStamps, float samplesPerPoint)
		{
			(int lastReadSampleIndex, int unreadTimeStampCount) tuple = ComputeLastReadSampleIndexAndUnreadSampleCount(timeStamps);
			int item = tuple.lastReadSampleIndex;
			float num = (float)tuple.unreadTimeStampCount + m_FractionRemainingOfLastReadSample;
			int num2 = (int)(num / samplesPerPoint);
			if (num2 <= 0)
			{
				return 0;
			}
			float num3 = (float)num2 * samplesPerPoint;
			float x = (float)item + num3 - m_FractionRemainingOfLastReadSample;
			m_FractionRemainingOfLastReadSample = (num - num3) % 1f;
			m_LastReadSampleTimeStamp = timeStamps[(int)global::System.MathF.Ceiling(x)];
			return num2;
		}
	}
}
