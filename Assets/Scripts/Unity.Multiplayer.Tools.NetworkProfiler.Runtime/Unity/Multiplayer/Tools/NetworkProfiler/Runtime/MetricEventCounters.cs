namespace Unity.Multiplayer.Tools.NetworkProfiler.Runtime
{
	internal class MetricEventCounters
	{
		private readonly global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.ICounter m_SentCounter;

		private readonly global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.ICounter m_ReceivedCounter;

		public string Sent { get; }

		public string Received { get; }

		public MetricEventCounters(string displayName, global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.ICounterFactory counterFactory)
		{
			Sent = displayName + " Sent";
			Received = displayName + " Received";
			m_SentCounter = counterFactory.Construct(Sent);
			m_ReceivedCounter = counterFactory.Construct(Received);
		}

		public void Sample<TEventData>(global::System.Collections.Generic.IReadOnlyCollection<TEventData> sent, global::System.Collections.Generic.IReadOnlyCollection<TEventData> received)
		{
			m_SentCounter.Sample(sent.Count);
			m_ReceivedCounter.Sample(received.Count);
		}
	}
}
