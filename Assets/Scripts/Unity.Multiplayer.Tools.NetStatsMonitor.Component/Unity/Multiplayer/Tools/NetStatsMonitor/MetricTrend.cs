namespace Unity.Multiplayer.Tools.NetStatsMonitor
{
	[global::System.Serializable]
	internal class MetricTrend
	{
		[field: global::UnityEngine.SerializeField]
		public global::Unity.Multiplayer.Tools.NetStats.MetricId Metric { get; set; }

		[field: global::UnityEngine.SerializeField]
		public global::Unity.Multiplayer.Tools.Common.LogNormalRandomWalk Trend { get; set; }
	}
}
