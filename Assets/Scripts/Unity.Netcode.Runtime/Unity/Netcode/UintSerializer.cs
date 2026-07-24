namespace Unity.Netcode
{
	internal class UintSerializer : global::Unity.Netcode.INetworkVariableSerializer<uint>
	{
		public void Write(global::Unity.Netcode.FastBufferWriter writer, ref uint value)
		{
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, value);
		}

		public void Read(global::Unity.Netcode.FastBufferReader reader, ref uint value)
		{
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out value);
		}

		public void WriteDelta(global::Unity.Netcode.FastBufferWriter writer, ref uint value, ref uint previousValue)
		{
			Write(writer, ref value);
		}

		public void ReadDelta(global::Unity.Netcode.FastBufferReader reader, ref uint value)
		{
			Read(reader, ref value);
		}

		void global::Unity.Netcode.INetworkVariableSerializer<uint>.ReadWithAllocator(global::Unity.Netcode.FastBufferReader reader, out uint value, global::Unity.Collections.Allocator allocator)
		{
			throw new global::System.NotImplementedException();
		}

		public void Duplicate(in uint value, ref uint duplicatedValue)
		{
			duplicatedValue = value;
		}

		void global::Unity.Netcode.INetworkVariableSerializer<uint>.Duplicate(in uint value, ref uint duplicatedValue)
		{
			Duplicate(in value, ref duplicatedValue);
		}
	}
}
