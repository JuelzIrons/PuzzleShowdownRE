namespace Unity.Multiplayer.Tools.NetStatsMonitor
{
	internal static class SampleRateExtensions
	{
		public static global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate Next(this global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate rate)
		{
			return rate + 1;
		}
	}
}
