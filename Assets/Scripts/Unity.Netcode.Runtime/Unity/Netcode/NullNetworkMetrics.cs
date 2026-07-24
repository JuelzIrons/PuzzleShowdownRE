namespace Unity.Netcode
{
	internal class NullNetworkMetrics : global::Unity.Netcode.INetworkMetrics
	{
		public void SetConnectionId(ulong connectionId)
		{
		}

		public void TrackTransportBytesSent(long bytesCount)
		{
		}

		public void TrackTransportBytesReceived(long bytesCount)
		{
		}

		public void TrackNetworkMessageSent(ulong receivedClientId, string messageType, long bytesCount)
		{
		}

		public void TrackNetworkMessageReceived(ulong senderClientId, string messageType, long bytesCount)
		{
		}

		public void TrackNamedMessageSent(ulong receiverClientId, string messageName, long bytesCount)
		{
		}

		public void TrackNamedMessageSent(global::System.Collections.Generic.IReadOnlyCollection<ulong> receiverClientIds, string messageName, long bytesCount)
		{
		}

		public void TrackNamedMessageReceived(ulong senderClientId, string messageName, long bytesCount)
		{
		}

		public void TrackUnnamedMessageSent(ulong receiverClientId, long bytesCount)
		{
		}

		public void TrackUnnamedMessageSent(global::System.Collections.Generic.IReadOnlyCollection<ulong> receiverClientIds, long bytesCount)
		{
		}

		public void TrackUnnamedMessageReceived(ulong senderClientId, long bytesCount)
		{
		}

		public void TrackNetworkVariableDeltaSent(ulong receiverClientId, global::Unity.Netcode.NetworkObject networkObject, string variableName, string networkBehaviourName, long bytesCount)
		{
		}

		public void TrackNetworkVariableDeltaReceived(ulong senderClientId, global::Unity.Netcode.NetworkObject networkObject, string variableName, string networkBehaviourName, long bytesCount)
		{
		}

		public void TrackOwnershipChangeSent(ulong receiverClientId, global::Unity.Netcode.NetworkObject networkObject, long bytesCount)
		{
		}

		public void TrackOwnershipChangeReceived(ulong senderClientId, global::Unity.Netcode.NetworkObject networkObject, long bytesCount)
		{
		}

		public void TrackObjectSpawnSent(ulong receiverClientId, global::Unity.Netcode.NetworkObject networkObject, long bytesCount)
		{
		}

		public void TrackObjectSpawnReceived(ulong senderClientId, global::Unity.Netcode.NetworkObject networkObject, long bytesCount)
		{
		}

		public void TrackObjectDestroySent(ulong senderClientId, global::Unity.Netcode.NetworkObject networkObject, long bytesCount)
		{
		}

		public void TrackObjectDestroyReceived(ulong senderClientId, global::Unity.Netcode.NetworkObject networkObject, long bytesCount)
		{
		}

		public void TrackRpcSent(ulong receiverClientId, global::Unity.Netcode.NetworkObject networkObject, string rpcName, string networkBehaviourName, long bytesCount)
		{
		}

		public void TrackRpcSent(ulong[] receiverClientIds, global::Unity.Netcode.NetworkObject networkObject, string rpcName, string networkBehaviourName, long bytesCount)
		{
		}

		public void TrackRpcReceived(ulong senderClientId, global::Unity.Netcode.NetworkObject networkObject, string rpcName, string networkBehaviourName, long bytesCount)
		{
		}

		public void TrackServerLogSent(ulong receiverClientId, uint logType, long bytesCount)
		{
		}

		public void TrackServerLogReceived(ulong senderClientId, uint logType, long bytesCount)
		{
		}

		public void TrackSceneEventSent(global::System.Collections.Generic.IReadOnlyList<ulong> receiverClientIds, uint sceneEventType, string sceneName, long bytesCount)
		{
		}

		public void TrackSceneEventSent(ulong receiverClientId, uint sceneEventType, string sceneName, long bytesCount)
		{
		}

		public void TrackSceneEventReceived(ulong senderClientId, uint sceneEventType, string sceneName, long bytesCount)
		{
		}

		public void TrackPacketSent(uint packetCount)
		{
		}

		public void TrackPacketReceived(uint packetCount)
		{
		}

		public void UpdateRttToServer(int rtt)
		{
		}

		public void UpdateNetworkObjectsCount(int count)
		{
		}

		public void UpdateConnectionsCount(int count)
		{
		}

		public void UpdatePacketLoss(float packetLoss)
		{
		}

		public void DispatchFrame()
		{
		}
	}
}
