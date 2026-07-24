namespace Unity.Multiplayer.Tools.MetricTypes
{
	internal struct DirectionalMetricInfo
	{
		internal global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType DirectedMetricType { get; }

		internal global::Unity.Multiplayer.Tools.MetricTypes.MetricType Type => DirectedMetricType.GetMetric();

		internal global::Unity.Multiplayer.Tools.Common.NetworkDirection Direction => DirectedMetricType.GetDirection();

		internal global::Unity.Multiplayer.Tools.NetStats.MetricId Id => DirectedMetricType.GetId();

		internal string DisplayName => DirectedMetricType.GetDisplayName();

		public DirectionalMetricInfo(global::Unity.Multiplayer.Tools.MetricTypes.DirectedMetricType directedMetricType)
		{
			DirectedMetricType = directedMetricType;
		}

		public DirectionalMetricInfo(global::Unity.Multiplayer.Tools.MetricTypes.MetricType metricType, global::Unity.Multiplayer.Tools.Common.NetworkDirection networkDirection)
		{
			DirectedMetricType = metricType.GetDirectedMetric(networkDirection);
		}
	}
}
