namespace Unity.Multiplayer.Tools.MetricTestData
{
	internal interface ITestDataTracker
	{
		global::Unity.Multiplayer.Tools.NetStats.IMetricDispatcher Dispatcher { get; }

		void SetConnectionId(ulong connectionId);

		void TrackTransportBytesSent(long bytesCount);

		void TrackTransportBytesReceived(long bytesCount);

		void TrackNetworkMessageSent(global::Unity.Multiplayer.Tools.MetricTypes.NetworkMessageEvent networkMessageEvent);

		void TrackNetworkMessageReceived(global::Unity.Multiplayer.Tools.MetricTypes.NetworkMessageEvent networkMessageEvent);

		void TrackNamedMessageSent(global::Unity.Multiplayer.Tools.MetricTypes.NamedMessageEvent namedMessageEvent);

		void TrackNamedMessageReceived(global::Unity.Multiplayer.Tools.MetricTypes.NamedMessageEvent namedMessageEvent);

		void TrackUnnamedMessageSent(global::Unity.Multiplayer.Tools.MetricTypes.UnnamedMessageEvent unnamedMessageEvent);

		void TrackUnnamedMessageReceived(global::Unity.Multiplayer.Tools.MetricTypes.UnnamedMessageEvent unnamedMessageEvent);

		void TrackNetworkVariableDeltaSent(global::Unity.Multiplayer.Tools.MetricTypes.NetworkVariableEvent networkVariableEvent);

		void TrackNetworkVariableDeltaReceived(global::Unity.Multiplayer.Tools.MetricTypes.NetworkVariableEvent networkVariableEvent);

		void TrackOwnershipChangeSent(global::Unity.Multiplayer.Tools.MetricTypes.OwnershipChangeEvent ownershipChangeEvent);

		void TrackOwnershipChangeReceived(global::Unity.Multiplayer.Tools.MetricTypes.OwnershipChangeEvent ownershipChangeEvent);

		void TrackObjectSpawnSent(global::Unity.Multiplayer.Tools.MetricTypes.ObjectSpawnedEvent objectSpawnedEvent);

		void TrackObjectSpawnReceived(global::Unity.Multiplayer.Tools.MetricTypes.ObjectSpawnedEvent objectSpawnedEvent);

		void TrackObjectDestroySent(global::Unity.Multiplayer.Tools.MetricTypes.ObjectDestroyedEvent objectDestroyedEvent);

		void TrackObjectDestroyReceived(global::Unity.Multiplayer.Tools.MetricTypes.ObjectDestroyedEvent objectDestroyedEvent);

		void TrackRpcSent(global::Unity.Multiplayer.Tools.MetricTypes.RpcEvent rpcEvent);

		void TrackRpcReceived(global::Unity.Multiplayer.Tools.MetricTypes.RpcEvent rpcEvent);

		void TrackServerLogSent(global::Unity.Multiplayer.Tools.MetricTypes.ServerLogEvent serverLogEvent);

		void TrackServerLogReceived(global::Unity.Multiplayer.Tools.MetricTypes.ServerLogEvent serverLogEvent);

		void TrackSceneEventSent(global::Unity.Multiplayer.Tools.MetricTypes.SceneEventMetric sceneEvent);

		void TrackSceneEventReceived(global::Unity.Multiplayer.Tools.MetricTypes.SceneEventMetric sceneEvent);

		void TrackPacketSent(int packetCount);

		void TrackPacketReceived(int packetCount);

		void TrackRttToServer(int rtt);

		void UpdateNetworkObjectsCount(int count);

		void UpdateConnectionsCount(int count);

		void UpdatePacketLoss(float count);
	}
}
