namespace Unity.Multiplayer.Tools.MetricTypes
{
	[global::System.Serializable]
	internal struct SceneEventMetric : global::Unity.Multiplayer.Tools.MetricTypes.INetworkMetricEvent
	{
		public global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo Connection { get; }

		public global::Unity.Collections.FixedString64Bytes SceneEventType { get; }

		public global::Unity.Collections.FixedString64Bytes SceneName { get; }

		public long BytesCount { get; }

		public ulong TreeViewId => (ulong)((long)Connection.GetHashCode() + (long)SceneEventType.GetHashCode() + SceneName.GetHashCode() + BytesCount.GetHashCode());

		public SceneEventMetric(global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo connection, string sceneEventType, string sceneName, long bytesCount)
			: this(connection, global::Unity.Multiplayer.Tools.MetricTypes.StringConversionUtility.ConvertToFixedString(sceneEventType), global::Unity.Multiplayer.Tools.MetricTypes.StringConversionUtility.ConvertToFixedString(sceneName), bytesCount)
		{
		}

		public SceneEventMetric(global::Unity.Multiplayer.Tools.MetricTypes.ConnectionInfo connection, global::Unity.Collections.FixedString64Bytes sceneEventType, global::Unity.Collections.FixedString64Bytes sceneName, long bytesCount)
		{
			Connection = connection;
			SceneEventType = sceneEventType;
			SceneName = sceneName;
			BytesCount = bytesCount;
		}
	}
}
