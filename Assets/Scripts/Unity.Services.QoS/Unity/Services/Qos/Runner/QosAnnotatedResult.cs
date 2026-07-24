namespace Unity.Services.Qos.Runner
{
	public struct QosAnnotatedResult
	{
		public string Region;

		public int AverageLatencyMs;

		public float PacketLossPercent;

		public global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<string>> Annotations;
	}
}
