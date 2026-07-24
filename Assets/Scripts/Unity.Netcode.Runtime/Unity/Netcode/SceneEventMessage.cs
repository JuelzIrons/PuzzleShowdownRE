namespace Unity.Netcode
{
	internal struct SceneEventMessage : global::Unity.Netcode.INetworkMessage
	{
		public global::Unity.Netcode.SceneEventData EventData;

		private global::Unity.Netcode.FastBufferReader m_ReceivedData;

		public int Version => 0;

		public void Serialize(global::Unity.Netcode.FastBufferWriter writer, int targetVersion)
		{
			EventData.Serialize(writer);
		}

		public bool Deserialize(global::Unity.Netcode.FastBufferReader reader, ref global::Unity.Netcode.NetworkContext context, int receivedMessageVersion)
		{
			m_ReceivedData = reader;
			return true;
		}

		public void Handle(ref global::Unity.Netcode.NetworkContext context)
		{
			((global::Unity.Netcode.NetworkManager)context.SystemOwner).SceneManager.HandleSceneEvent(context.SenderId, m_ReceivedData);
		}
	}
}
