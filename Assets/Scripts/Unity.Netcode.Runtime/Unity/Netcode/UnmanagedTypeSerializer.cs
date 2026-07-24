namespace Unity.Netcode
{
	internal class UnmanagedTypeSerializer<T> : global::Unity.Netcode.INetworkVariableSerializer<T> where T : unmanaged
	{
		public void Write(global::Unity.Netcode.FastBufferWriter writer, ref T value)
		{
			writer.WriteUnmanagedSafe(in value);
		}

		public void Read(global::Unity.Netcode.FastBufferReader reader, ref T value)
		{
			reader.ReadUnmanagedSafe(out value);
		}

		public void WriteDelta(global::Unity.Netcode.FastBufferWriter writer, ref T value, ref T previousValue)
		{
			Write(writer, ref value);
		}

		public void ReadDelta(global::Unity.Netcode.FastBufferReader reader, ref T value)
		{
			Read(reader, ref value);
		}

		void global::Unity.Netcode.INetworkVariableSerializer<T>.ReadWithAllocator(global::Unity.Netcode.FastBufferReader reader, out T value, global::Unity.Collections.Allocator allocator)
		{
			throw new global::System.NotImplementedException();
		}

		public void Duplicate(in T value, ref T duplicatedValue)
		{
			duplicatedValue = value;
		}

		void global::Unity.Netcode.INetworkVariableSerializer<T>.Duplicate(in T value, ref T duplicatedValue)
		{
			Duplicate(in value, ref duplicatedValue);
		}
	}
}
