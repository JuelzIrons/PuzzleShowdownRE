namespace Unity.Netcode
{
	internal class NetcodeObserver
	{
		public static global::Unity.Multiplayer.Tools.NetStats.IMetricObserver Observer { get; } = global::Unity.Multiplayer.Tools.MetricObserverFactory.Construct();
	}
}
