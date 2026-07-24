namespace Unity.Multiplayer.Tools
{
	internal static class MetricObserverFactory
	{
		internal static global::Unity.Multiplayer.Tools.NetStats.IMetricObserver Construct()
		{
			return new global::Unity.Multiplayer.Tools.MetricObserver();
		}
	}
}
