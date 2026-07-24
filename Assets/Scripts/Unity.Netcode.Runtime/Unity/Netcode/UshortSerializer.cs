namespace Unity.Netcode
{
	internal class UshortSerializer : global::Unity.Netcode.INetworkVariableSerializer<ushort>
	{
		public void Write(global::Unity.Netcode.FastBufferWriter writer, ref ushort value)
		{
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, value);
		}

		public void Read(global::Unity.Netcode.FastBufferReader reader, ref ushort value)
		{
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out value);
		}

		public void WriteDelta(global::Unity.Netcode.FastBufferWriter writer, ref ushort value, ref ushort previousValue)
		{
			Write(writer, ref value);
		}

		public void ReadDelta(global::Unity.Netcode.FastBufferReader reader, ref ushort value)
		{
			Read(reader, ref value);
		}

		void global::Unity.Netcode.INetworkVariableSerializer<ushort>.ReadWithAllocator(global::Unity.Netcode.FastBufferReader reader, out ushort value, global::Unity.Collections.Allocator allocator)
		{
			throw new global::System.NotImplementedException();
		}

		public void Duplicate(in ushort value, ref ushort duplicatedValue)
		{
			duplicatedValue = value;
		}

		void global::Unity.Netcode.INetworkVariableSerializer<ushort>.Duplicate(in ushort value, ref ushort duplicatedValue)
		{
			Duplicate(in value, ref duplicatedValue);
		}
	}
}
