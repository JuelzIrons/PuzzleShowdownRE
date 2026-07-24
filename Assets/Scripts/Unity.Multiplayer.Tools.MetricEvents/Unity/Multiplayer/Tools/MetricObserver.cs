namespace Unity.Multiplayer.Tools
{
	internal class MetricObserver : global::Unity.Multiplayer.Tools.NetStats.IMetricObserver
	{
		public void Observe(global::Unity.Multiplayer.Tools.NetStats.MetricCollection collection)
		{
			global::Unity.Multiplayer.Tools.MetricEvents.MetricEventPublisher.RaiseOnMetricsReceived(collection);
		}
	}
}
