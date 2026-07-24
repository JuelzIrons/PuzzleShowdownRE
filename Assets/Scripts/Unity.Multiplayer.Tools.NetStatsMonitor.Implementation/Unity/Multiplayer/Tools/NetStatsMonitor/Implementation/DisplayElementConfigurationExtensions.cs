namespace Unity.Multiplayer.Tools.NetStatsMonitor.Implementation
{
	internal static class DisplayElementConfigurationExtensions
	{
		internal static int GetHistoryRequirementsHash(this global::Unity.Multiplayer.Tools.NetStatsMonitor.DisplayElementConfiguration config)
		{
			int value = 0;
			foreach (global::Unity.Multiplayer.Tools.NetStats.MetricId stat in config.Stats)
			{
				value = global::System.HashCode.Combine(value, stat.GetHashCode());
			}
			return global::System.HashCode.Combine(value, config.SampleCount, config.SampleRate, config.HalfLife);
		}
	}
}
