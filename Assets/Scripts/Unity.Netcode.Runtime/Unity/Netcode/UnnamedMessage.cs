namespace Unity.Netcode
{
	internal struct UnnamedMessage : global::Unity.Netcode.INetworkMessage
	{
		public global::Unity.Netcode.FastBufferWriter SendData;

		private global::Unity.Netcode.FastBufferReader m_ReceivedData;

		public int Version => 0;

		public unsafe void Serialize(global::Unity.Netcode.FastBufferWriter writer, int targetVersion)
		{
			writer.WriteBytesSafe(SendData.GetUnsafePtr(), SendData.Length);
		}

		public bool Deserialize(global::Unity.Netcode.FastBufferReader reader, ref global::Unity.Netcode.NetworkContext context, int receivedMessageVersion)
		{
			m_ReceivedData = reader;
			return true;
		}

		public void Handle(ref global::Unity.Netcode.NetworkContext context)
		{
			((global::Unity.Netcode.NetworkManager)context.SystemOwner).CustomMessagingManager?.InvokeUnnamedMessage(context.SenderId, m_ReceivedData, context.SerializedHeaderSize);
		}
	}
}
