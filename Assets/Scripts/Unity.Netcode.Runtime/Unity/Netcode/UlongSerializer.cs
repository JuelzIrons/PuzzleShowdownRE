namespace Unity.Netcode
{
	internal class UlongSerializer : global::Unity.Netcode.INetworkVariableSerializer<ulong>
	{
		public void Write(global::Unity.Netcode.FastBufferWriter writer, ref ulong value)
		{
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, value);
		}

		public void Read(global::Unity.Netcode.FastBufferReader reader, ref ulong value)
		{
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out value);
		}

		public void WriteDelta(global::Unity.Netcode.FastBufferWriter writer, ref ulong value, ref ulong previousValue)
		{
			Write(writer, ref value);
		}

		public void ReadDelta(global::Unity.Netcode.FastBufferReader reader, ref ulong value)
		{
			Read(reader, ref value);
		}

		void global::Unity.Netcode.INetworkVariableSerializer<ulong>.ReadWithAllocator(global::Unity.Netcode.FastBufferReader reader, out ulong value, global::Unity.Collections.Allocator allocator)
		{
			throw new global::System.NotImplementedException();
		}

		public void Duplicate(in ulong value, ref ulong duplicatedValue)
		{
			duplicatedValue = value;
		}

		void global::Unity.Netcode.INetworkVariableSerializer<ulong>.Duplicate(in ulong value, ref ulong duplicatedValue)
		{
			Duplicate(in value, ref duplicatedValue);
		}
	}
}
