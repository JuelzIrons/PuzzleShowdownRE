namespace Unity.Multiplayer.Tools.NetworkProfiler.Runtime
{
	internal class MetricCounters
	{
		public readonly global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.MetricByteCounters Bytes;

		public readonly global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.MetricEventCounters Events;

		public MetricCounters(string displayName, global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.ICounterFactory byteCounterFactory, global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.ICounterFactory eventCounterFactory)
		{
			Bytes = new global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.MetricByteCounters(displayName, byteCounterFactory);
			Events = new global::Unity.Multiplayer.Tools.NetworkProfiler.Runtime.MetricEventCounters(displayName, eventCounterFactory);
		}

		public void Sample<TEventData>(global::System.Collections.Generic.IReadOnlyList<TEventData> sent, global::System.Collections.Generic.IReadOnlyList<TEventData> received) where TEventData : struct, global::Unity.Multiplayer.Tools.MetricTypes.INetworkMetricEvent
		{
			Bytes.Sample(sent, received);
			Events.Sample(sent, received);
		}
	}
}
