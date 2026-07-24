namespace Unity.Multiplayer.Tools.NetStatsMonitor
{
	internal static class ConfigurationLimits
	{
		internal const int k_GraphSampleMin = 8;

		internal const int k_GraphSampleDefault = 256;

		internal const int k_GraphSampleMax = 512;

		internal const int k_CounterSampleMin = 8;

		internal const int k_CounterSampleDefault = 64;

		internal const int k_CounterSampleMax = 512;

		internal const int k_CounterSignificantDigitsMin = 1;

		internal const int k_CounterSignificantDigitsMax = 7;

		internal const double k_ExponentialMovingAverageHalfLifeMin = 0.0;

		internal const double k_RefreshRateMin = 1.0;

		internal const float k_PositionMin = 0f;

		internal const float k_PositionMax = 1f;

		internal const float k_GraphLineThicknessMin = 1f;

		internal const float k_GraphLineThicknessMax = 5f;
	}
}
