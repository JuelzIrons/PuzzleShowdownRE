namespace Unity.Multiplayer.Tools.NetStatsMonitor.Implementation
{
	internal static class NetStatsMonitorConfigurationExtensions
	{
		internal static int GetHistoryRequirementsHash(this global::Unity.Multiplayer.Tools.NetStatsMonitor.NetStatsMonitorConfiguration config)
		{
			int num = 0;
			foreach (global::Unity.Multiplayer.Tools.NetStatsMonitor.DisplayElementConfiguration displayElement in config.DisplayElements)
			{
				num = global::System.HashCode.Combine(num, displayElement.GetHistoryRequirementsHash());
			}
			return num;
		}
	}
}
