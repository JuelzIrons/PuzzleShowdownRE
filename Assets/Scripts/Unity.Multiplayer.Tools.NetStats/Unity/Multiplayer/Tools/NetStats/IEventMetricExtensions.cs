namespace Unity.Multiplayer.Tools.NetStats
{
	internal static class IEventMetricExtensions
	{
		public static bool WentOverLimit(this global::Unity.Multiplayer.Tools.NetStats.IEventMetric metric)
		{
			return metric.NumberOfValuesReceived > metric.MaxNumberOfValues;
		}

		public static int NumberOfValuesIgnored(this global::Unity.Multiplayer.Tools.NetStats.IEventMetric metric)
		{
			return metric.NumberOfValuesReceived - metric.Count;
		}

		public static string WentOverLimitMessage(this global::Unity.Multiplayer.Tools.NetStats.IEventMetric metric)
		{
			return $"Multiplayer Tools: Metric {metric.Name} received {metric.NumberOfValuesReceived} values, " + $"which exceeds the limit of {metric.MaxNumberOfValues}. " + $"{metric.NumberOfValuesIgnored()} values were ignored.";
		}
	}
}
