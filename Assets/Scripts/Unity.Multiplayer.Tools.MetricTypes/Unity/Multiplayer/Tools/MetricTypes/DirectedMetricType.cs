namespace Unity.Multiplayer.Tools.MetricTypes
{
	[global::Unity.Multiplayer.Tools.NetStats.MetricTypeEnum(DisplayName = "Built-In Metrics")]
	[global::Unity.Multiplayer.Tools.NetStats.MetricTypeSortPriority(SortPriority = global::Unity.Multiplayer.Tools.NetStats.SortPriority.VeryHigh)]
	public enum DirectedMetricType
	{
		[global::Unity.Multiplayer.Tools.NetStats.MetricMetadata(Units = global::Unity.Multiplayer.Tools.NetStats.Units.Bytes)]
		TotalBytesSent = 6,
		[global::Unity.Multiplayer.Tools.NetStats.MetricMetadata(Units = global::Unity.Multiplayer.Tools.NetStats.Units.Bytes)]
		TotalBytesReceived = 5,
		[global::Unity.Multiplayer.Tools.NetStats.MetricMetadata(DisplayName = "RPCs Sent")]
		RpcSent = 10,
		[global::Unity.Multiplayer.Tools.NetStats.MetricMetadata(DisplayName = "RPCs Received")]
		RpcReceived = 9,
		[global::Unity.Multiplayer.Tools.NetStats.MetricMetadata(DisplayName = "Named Messages Sent")]
		NamedMessageSent = 14,
		[global::Unity.Multiplayer.Tools.NetStats.MetricMetadata(DisplayName = "Named Messages Received")]
		NamedMessageReceived = 13,
		[global::Unity.Multiplayer.Tools.NetStats.MetricMetadata(DisplayName = "Unnamed Messages Sent")]
		UnnamedMessageSent = 18,
		[global::Unity.Multiplayer.Tools.NetStats.MetricMetadata(DisplayName = "Unnamed Messages Received")]
		UnnamedMessageReceived = 17,
		[global::Unity.Multiplayer.Tools.NetStats.MetricMetadata(DisplayName = "Network Variable Deltas Sent")]
		NetworkVariableDeltaSent = 22,
		[global::Unity.Multiplayer.Tools.NetStats.MetricMetadata(DisplayName = "Network Variable Deltas Received")]
		NetworkVariableDeltaReceived = 21,
		[global::Unity.Multiplayer.Tools.NetStats.MetricMetadata(DisplayName = "Objects Spawned Sent")]
		ObjectSpawnedSent = 26,
		[global::Unity.Multiplayer.Tools.NetStats.MetricMetadata(DisplayName = "Objects Spawned Received")]
		ObjectSpawnedReceived = 25,
		[global::Unity.Multiplayer.Tools.NetStats.MetricMetadata(DisplayName = "Objects Destroyed Sent")]
		ObjectDestroyedSent = 30,
		[global::Unity.Multiplayer.Tools.NetStats.MetricMetadata(DisplayName = "Objects Destroyed Received")]
		ObjectDestroyedReceived = 29,
		[global::Unity.Multiplayer.Tools.NetStats.MetricMetadata(DisplayName = "Ownership Changes Sent")]
		OwnershipChangeSent = 34,
		[global::Unity.Multiplayer.Tools.NetStats.MetricMetadata(DisplayName = "Ownership Changes Received")]
		OwnershipChangeReceived = 33,
		[global::Unity.Multiplayer.Tools.NetStats.MetricMetadata(DisplayName = "Server Logs Sent")]
		ServerLogSent = 38,
		[global::Unity.Multiplayer.Tools.NetStats.MetricMetadata(DisplayName = "Server Logs Received")]
		ServerLogReceived = 37,
		[global::Unity.Multiplayer.Tools.NetStats.MetricMetadata(DisplayName = "Scene Events Sent")]
		SceneEventSent = 42,
		[global::Unity.Multiplayer.Tools.NetStats.MetricMetadata(DisplayName = "Scene Events Received")]
		SceneEventReceived = 41,
		[global::Unity.Multiplayer.Tools.NetStats.MetricMetadata(DisplayName = "Network Messages Sent")]
		NetworkMessageSent = 46,
		[global::Unity.Multiplayer.Tools.NetStats.MetricMetadata(DisplayName = "Network Messages Received")]
		NetworkMessageReceived = 45,
		PacketsSent = 50,
		PacketsReceived = 49,
		[global::Unity.Multiplayer.Tools.NetStats.MetricMetadata(DisplayName = "RTT To Server", MetricKind = global::Unity.Multiplayer.Tools.NetStats.MetricKind.Gauge, Units = global::Unity.Multiplayer.Tools.NetStats.Units.Seconds)]
		RttToServer = 55,
		[global::Unity.Multiplayer.Tools.NetStats.MetricMetadata(MetricKind = global::Unity.Multiplayer.Tools.NetStats.MetricKind.Gauge)]
		NetworkObjects = 59,
		[global::Unity.Multiplayer.Tools.NetStats.MetricMetadata(MetricKind = global::Unity.Multiplayer.Tools.NetStats.MetricKind.Gauge)]
		Connections = 63,
		[global::Unity.Multiplayer.Tools.NetStats.MetricMetadata(MetricKind = global::Unity.Multiplayer.Tools.NetStats.MetricKind.Gauge, DisplayAsPercentage = true)]
		PacketLoss = 65
	}
}
