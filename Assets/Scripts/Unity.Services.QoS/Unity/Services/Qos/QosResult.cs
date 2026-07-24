namespace Unity.Services.Qos
{
	internal class QosResult : global::Unity.Services.Qos.IQosAnnotatedResult, global::Unity.Services.Qos.IQosResult
	{
		public string Region { get; }

		public int AverageLatencyMs { get; }

		public float PacketLossPercent { get; }

		public global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<string>> Annotations { get; }

		public QosResult(string region, int averageLatencyMs, float packetLossPercent, global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<string>> annotations = null)
		{
			Region = region;
			AverageLatencyMs = averageLatencyMs;
			PacketLossPercent = packetLossPercent;
			Annotations = annotations ?? new global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<string>>();
		}
	}
}
