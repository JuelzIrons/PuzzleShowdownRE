namespace Unity.Netcode
{
	internal interface INetworkMessageSender
	{
		void Send(ulong clientId, global::Unity.Netcode.NetworkDelivery delivery, global::Unity.Netcode.FastBufferWriter batchData);
	}
}
