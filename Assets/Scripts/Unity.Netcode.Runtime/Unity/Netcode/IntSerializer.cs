namespace Unity.Netcode
{
	internal class IntSerializer : global::Unity.Netcode.INetworkVariableSerializer<int>
	{
		public void Write(global::Unity.Netcode.FastBufferWriter writer, ref int value)
		{
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, value);
		}

		public void Read(global::Unity.Netcode.FastBufferReader reader, ref int value)
		{
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out value);
		}

		public void WriteDelta(global::Unity.Netcode.FastBufferWriter writer, ref int value, ref int previousValue)
		{
			Write(writer, ref value);
		}

		public void ReadDelta(global::Unity.Netcode.FastBufferReader reader, ref int value)
		{
			Read(reader, ref value);
		}

		void global::Unity.Netcode.INetworkVariableSerializer<int>.ReadWithAllocator(global::Unity.Netcode.FastBufferReader reader, out int value, global::Unity.Collections.Allocator allocator)
		{
			throw new global::System.NotImplementedException();
		}

		public void Duplicate(in int value, ref int duplicatedValue)
		{
			duplicatedValue = value;
		}

		void global::Unity.Netcode.INetworkVariableSerializer<int>.Duplicate(in int value, ref int duplicatedValue)
		{
			Duplicate(in value, ref duplicatedValue);
		}
	}
}
