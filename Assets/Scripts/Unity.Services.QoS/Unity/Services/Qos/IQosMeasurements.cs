namespace Unity.Services.Qos
{
	public interface IQosMeasurements
	{
		int AverageLatencyMs { get; }

		float PacketLossPercent { get; }
	}
}
