namespace Unity.Multiplayer.Tools.NetStats
{
	internal struct MetricHeader
	{
		public global::Unity.Collections.FixedString128Bytes EventFactoryTypeName { get; set; }

		public global::Unity.Multiplayer.Tools.NetStats.MetricContainerType MetricContainerType { get; set; }

		public global::Unity.Multiplayer.Tools.NetStats.MetricId MetricId { get; set; }

		public MetricHeader(global::Unity.Collections.FixedString128Bytes eventFactoryTypeName, global::Unity.Multiplayer.Tools.NetStats.MetricContainerType metricContainerType, global::Unity.Multiplayer.Tools.NetStats.MetricId metricId)
		{
			EventFactoryTypeName = eventFactoryTypeName;
			MetricContainerType = metricContainerType;
			MetricId = metricId;
		}
	}
}
