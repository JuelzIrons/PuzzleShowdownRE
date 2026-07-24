namespace Unity.Netcode
{
	internal struct NamedMessage : global::Unity.Netcode.INetworkMessage
	{
		public ulong Hash;

		public global::Unity.Netcode.FastBufferWriter SendData;

		private global::Unity.Netcode.FastBufferReader m_ReceiveData;

		public int Version => 0;

		public unsafe void Serialize(global::Unity.Netcode.FastBufferWriter writer, int targetVersion)
		{
			writer.WriteValueSafe(in Hash, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			writer.WriteBytesSafe(SendData.GetUnsafePtr(), SendData.Length);
		}

		public bool Deserialize(global::Unity.Netcode.FastBufferReader reader, ref global::Unity.Netcode.NetworkContext context, int receivedMessageVersion)
		{
			reader.ReadValueSafe(out Hash, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			m_ReceiveData = reader;
			return true;
		}

		public void Handle(ref global::Unity.Netcode.NetworkContext context)
		{
			global::Unity.Netcode.NetworkManager networkManager = (global::Unity.Netcode.NetworkManager)context.SystemOwner;
			if (!networkManager.ShutdownInProgress && networkManager.CustomMessagingManager != null)
			{
				networkManager.CustomMessagingManager.InvokeNamedMessage(Hash, context.SenderId, m_ReceiveData, context.SerializedHeaderSize);
			}
		}
	}
}
