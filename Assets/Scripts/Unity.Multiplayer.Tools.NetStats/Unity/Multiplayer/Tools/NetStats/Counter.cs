namespace Unity.Multiplayer.Tools.NetStats
{
	[global::System.Serializable]
	internal class Counter : global::Unity.Multiplayer.Tools.NetStats.Metric<long>
	{
		public override global::Unity.Multiplayer.Tools.NetStats.MetricContainerType MetricContainerType => global::Unity.Multiplayer.Tools.NetStats.MetricContainerType.Counter;

		public Counter(global::Unity.Multiplayer.Tools.NetStats.MetricId metricId, long defaultValue = 0L)
			: base(metricId, defaultValue)
		{
		}

		public void Increment(long increment = 1L)
		{
			base.Value += increment;
		}
	}
}
