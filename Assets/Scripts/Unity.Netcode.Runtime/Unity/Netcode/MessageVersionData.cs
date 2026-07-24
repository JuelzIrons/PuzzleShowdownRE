namespace Unity.Netcode
{
	internal struct MessageVersionData
	{
		public uint Hash;

		public int Version;

		public void Serialize(global::Unity.Netcode.FastBufferWriter writer)
		{
			writer.WriteValueSafe(in Hash, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, Version);
		}

		public void Deserialize(global::Unity.Netcode.FastBufferReader reader)
		{
			reader.ReadValueSafe(out Hash, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out Version);
		}
	}
}
