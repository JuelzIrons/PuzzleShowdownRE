namespace Unity.Multiplayer.Tools.NetStats
{
	[global::System.Serializable]
	internal class Gauge : global::Unity.Multiplayer.Tools.NetStats.Metric<double>
	{
		public override global::Unity.Multiplayer.Tools.NetStats.MetricContainerType MetricContainerType => global::Unity.Multiplayer.Tools.NetStats.MetricContainerType.Gauge;

		public Gauge(global::Unity.Multiplayer.Tools.NetStats.MetricId metricId, double defaultValue = 0.0)
			: base(metricId, defaultValue)
		{
		}

		public void Set(double value)
		{
			base.Value = value;
		}
	}
}
