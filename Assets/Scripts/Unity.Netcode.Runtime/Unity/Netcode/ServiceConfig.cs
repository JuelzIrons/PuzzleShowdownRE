namespace Unity.Netcode
{
	internal struct ServiceConfig : global::Unity.Netcode.INetworkSerializable
	{
		public uint SessionVersion;

		public bool IsRestoredSession;

		public ulong CurrentSessionOwner;

		public bool ServerRedistribution;

		public ulong SessionStateToken;

		public void NetworkSerialize<T>(global::Unity.Netcode.BufferSerializer<T> serializer) where T : global::Unity.Netcode.IReaderWriter
		{
			if (serializer.IsWriter)
			{
				global::Unity.Netcode.BytePacker.WriteValueBitPacked(serializer.GetFastBufferWriter(), SessionVersion);
				serializer.SerializeValue(ref IsRestoredSession, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				global::Unity.Netcode.BytePacker.WriteValueBitPacked(serializer.GetFastBufferWriter(), CurrentSessionOwner);
				if (SessionVersion >= 2)
				{
					serializer.SerializeValue(ref ServerRedistribution, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				}
				if (SessionVersion >= 3)
				{
					serializer.SerializeValue(ref SessionStateToken, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				}
			}
			else
			{
				global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(serializer.GetFastBufferReader(), out SessionVersion);
				serializer.SerializeValue(ref IsRestoredSession, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(serializer.GetFastBufferReader(), out CurrentSessionOwner);
				if (SessionVersion >= 2)
				{
					serializer.SerializeValue(ref ServerRedistribution, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				}
				else
				{
					ServerRedistribution = false;
				}
				if (SessionVersion >= 3)
				{
					serializer.SerializeValue(ref SessionStateToken, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				}
				else
				{
					SessionStateToken = 0uL;
				}
			}
		}
	}
}
