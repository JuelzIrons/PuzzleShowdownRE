namespace Unity.Netcode
{
	internal class LongSerializer : global::Unity.Netcode.INetworkVariableSerializer<long>
	{
		public void Write(global::Unity.Netcode.FastBufferWriter writer, ref long value)
		{
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, value);
		}

		public void Read(global::Unity.Netcode.FastBufferReader reader, ref long value)
		{
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out value);
		}

		public void WriteDelta(global::Unity.Netcode.FastBufferWriter writer, ref long value, ref long previousValue)
		{
			Write(writer, ref value);
		}

		public void ReadDelta(global::Unity.Netcode.FastBufferReader reader, ref long value)
		{
			Read(reader, ref value);
		}

		void global::Unity.Netcode.INetworkVariableSerializer<long>.ReadWithAllocator(global::Unity.Netcode.FastBufferReader reader, out long value, global::Unity.Collections.Allocator allocator)
		{
			throw new global::System.NotImplementedException();
		}

		public void Duplicate(in long value, ref long duplicatedValue)
		{
			duplicatedValue = value;
		}

		void global::Unity.Netcode.INetworkVariableSerializer<long>.Duplicate(in long value, ref long duplicatedValue)
		{
			Duplicate(in value, ref duplicatedValue);
		}
	}
}
