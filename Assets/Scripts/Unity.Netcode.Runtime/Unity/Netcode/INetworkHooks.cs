namespace Unity.Netcode
{
	internal interface INetworkHooks
	{
		void OnBeforeSendMessage<T>(ulong clientId, ref T message, global::Unity.Netcode.NetworkDelivery delivery) where T : global::Unity.Netcode.INetworkMessage;

		void OnAfterSendMessage<T>(ulong clientId, ref T message, global::Unity.Netcode.NetworkDelivery delivery, int messageSizeBytes) where T : global::Unity.Netcode.INetworkMessage;

		void OnBeforeReceiveMessage(ulong senderId, global::System.Type messageType, int messageSizeBytes);

		void OnAfterReceiveMessage(ulong senderId, global::System.Type messageType, int messageSizeBytes);

		void OnBeforeSendBatch(ulong clientId, int messageCount, int batchSizeInBytes, global::Unity.Netcode.NetworkDelivery delivery);

		void OnAfterSendBatch(ulong clientId, int messageCount, int batchSizeInBytes, global::Unity.Netcode.NetworkDelivery delivery);

		void OnBeforeReceiveBatch(ulong senderId, int messageCount, int batchSizeInBytes);

		void OnAfterReceiveBatch(ulong senderId, int messageCount, int batchSizeInBytes);

		bool OnVerifyCanSend(ulong destinationId, global::System.Type messageType, global::Unity.Netcode.NetworkDelivery delivery);

		bool OnVerifyCanReceive(ulong senderId, global::System.Type messageType, global::Unity.Netcode.FastBufferReader messageContent, ref global::Unity.Netcode.NetworkContext context);

		void OnBeforeHandleMessage<T>(ref T message, ref global::Unity.Netcode.NetworkContext context) where T : global::Unity.Netcode.INetworkMessage;

		void OnAfterHandleMessage<T>(ref T message, ref global::Unity.Netcode.NetworkContext context) where T : global::Unity.Netcode.INetworkMessage;
	}
}
