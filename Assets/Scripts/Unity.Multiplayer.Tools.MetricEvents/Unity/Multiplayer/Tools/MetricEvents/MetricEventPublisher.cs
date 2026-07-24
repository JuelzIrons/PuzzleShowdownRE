namespace Unity.Multiplayer.Tools.MetricEvents
{
	internal static class MetricEventPublisher
	{
		public static event global::System.Action<global::Unity.Multiplayer.Tools.NetStats.MetricCollection> OnMetricsReceived;

		public static void RaiseOnMetricsReceived(global::Unity.Multiplayer.Tools.NetStats.MetricCollection metricCollection)
		{
			global::Unity.Multiplayer.Tools.MetricEvents.MetricEventPublisher.OnMetricsReceived?.Invoke(metricCollection);
		}
	}
}
