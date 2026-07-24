namespace Unity.Netcode
{
	internal struct SessionOwnerMessage : global::Unity.Netcode.INetworkMessage
	{
		public ulong SessionOwner;

		public int Version => 0;

		public void Serialize(global::Unity.Netcode.FastBufferWriter writer, int targetVersion)
		{
			global::Unity.Netcode.BytePacker.WriteValuePacked(writer, SessionOwner);
		}

		public bool Deserialize(global::Unity.Netcode.FastBufferReader reader, ref global::Unity.Netcode.NetworkContext context, int receivedMessageVersion)
		{
			global::Unity.Netcode.ByteUnpacker.ReadValuePacked(reader, out SessionOwner);
			return true;
		}

		public void Handle(ref global::Unity.Netcode.NetworkContext context)
		{
			((global::Unity.Netcode.NetworkManager)context.SystemOwner).SetSessionOwner(SessionOwner);
		}
	}
}
