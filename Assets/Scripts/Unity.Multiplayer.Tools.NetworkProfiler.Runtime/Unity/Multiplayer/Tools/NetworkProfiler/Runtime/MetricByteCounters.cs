namespace Unity.Multiplayer.Tools.NetworkProfiler.Runtime
{
	internal class MetricByteCounters
	{
		private readonly global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.ICounter m_SentCounter;

		private readonly global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.ICounter m_ReceivedCounter;

		public string Sent { get; }

		public string Received { get; }

		public MetricByteCounters(string displayName, global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.ICounterFactory counterFactory)
		{
			Sent = displayName + " Bytes Sent";
			Received = displayName + " Bytes Received";
			m_SentCounter = counterFactory.Construct(Sent);
			m_ReceivedCounter = counterFactory.Construct(Received);
		}

		public void Sample<TEventData>(global::System.Collections.Generic.IReadOnlyList<TEventData> sentMetrics, global::System.Collections.Generic.IReadOnlyList<TEventData> receivedMetrics) where TEventData : struct, global::Unity.Multiplayer.Tools.MetricTypes.INetworkMetricEvent
		{
			long num = 0L;
			for (int i = 0; i < sentMetrics.Count; i++)
			{
				num += sentMetrics[i].BytesCount;
			}
			long num2 = 0L;
			for (int j = 0; j < receivedMetrics.Count; j++)
			{
				num2 += receivedMetrics[j].BytesCount;
			}
			Sample(num, num2);
		}

		public void Sample(long sent, long received)
		{
			m_SentCounter.Sample(sent);
			m_ReceivedCounter.Sample(received);
		}
	}
}
