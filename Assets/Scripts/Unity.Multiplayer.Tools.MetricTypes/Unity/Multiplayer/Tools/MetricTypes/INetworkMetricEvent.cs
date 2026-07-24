namespace Unity.Multiplayer.Tools.MetricTypes
{
	internal interface INetworkMetricEvent
	{
		global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo Connection { get; }

		long BytesCount { get; }
	}
}
