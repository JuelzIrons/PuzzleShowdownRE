namespace Unity.Netcode
{
	internal struct ClientConfig : global::Unity.Netcode.INetworkSerializable
	{
		public global::Unity.Netcode.SessionConfig SessionConfig;

		public uint TickRate;

		public bool EnableSceneManagement;

		public int RemoteClientSessionVersion;

		public int SessionVersion => (int)SessionConfig.SessionVersion;

		public void NetworkSerialize<T>(global::Unity.Netcode.BufferSerializer<T> serializer) where T : global::Unity.Netcode.IReaderWriter
		{
			if (serializer.IsWriter)
			{
				global::Unity.Netcode.FastBufferWriter fastBufferWriter = serializer.GetFastBufferWriter();
				global::Unity.Netcode.BytePacker.WriteValueBitPacked(fastBufferWriter, SessionVersion);
				global::Unity.Netcode.BytePacker.WriteValueBitPacked(fastBufferWriter, TickRate);
				fastBufferWriter.WriteValueSafe(in EnableSceneManagement, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			}
			else
			{
				global::Unity.Netcode.FastBufferReader fastBufferReader = serializer.GetFastBufferReader();
				global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(fastBufferReader, out RemoteClientSessionVersion);
				global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(fastBufferReader, out TickRate);
				fastBufferReader.ReadValueSafe(out EnableSceneManagement, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			}
		}
	}
}
