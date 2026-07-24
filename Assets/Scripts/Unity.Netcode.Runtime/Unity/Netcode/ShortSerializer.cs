namespace Unity.Netcode
{
	internal class ShortSerializer : global::Unity.Netcode.INetworkVariableSerializer<short>
	{
		public void Write(global::Unity.Netcode.FastBufferWriter writer, ref short value)
		{
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, value);
		}

		public void Read(global::Unity.Netcode.FastBufferReader reader, ref short value)
		{
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out value);
		}

		public void WriteDelta(global::Unity.Netcode.FastBufferWriter writer, ref short value, ref short previousValue)
		{
			Write(writer, ref value);
		}

		public void ReadDelta(global::Unity.Netcode.FastBufferReader reader, ref short value)
		{
			Read(reader, ref value);
		}

		void global::Unity.Netcode.INetworkVariableSerializer<short>.ReadWithAllocator(global::Unity.Netcode.FastBufferReader reader, out short value, global::Unity.Collections.Allocator allocator)
		{
			throw new global::System.NotImplementedException();
		}

		public void Duplicate(in short value, ref short duplicatedValue)
		{
			duplicatedValue = value;
		}

		void global::Unity.Netcode.INetworkVariableSerializer<short>.Duplicate(in short value, ref short duplicatedValue)
		{
			Duplicate(in value, ref duplicatedValue);
		}
	}
}
