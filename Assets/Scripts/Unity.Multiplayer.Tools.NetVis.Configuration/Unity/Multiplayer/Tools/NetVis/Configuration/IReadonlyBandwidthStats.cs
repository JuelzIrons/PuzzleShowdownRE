namespace Unity.Multiplayer.Tools.NetVis.Configuration
{
	internal interface IReadonlyBandwidthStats
	{
		float MinBandwidth { get; }

		float MaxBandwidth { get; }

		event global::System.Action OnBandwidthStatsUpdated;
	}
}
