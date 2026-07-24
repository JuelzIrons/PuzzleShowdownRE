namespace Unity.Netcode
{
	internal interface INetworkVariableSerializer<T>
	{
		void Write(global::Unity.Netcode.FastBufferWriter writer, ref T value);

		void Read(global::Unity.Netcode.FastBufferReader reader, ref T value);

		void WriteDelta(global::Unity.Netcode.FastBufferWriter writer, ref T value, ref T previousValue);

		void ReadDelta(global::Unity.Netcode.FastBufferReader reader, ref T value);

		internal void ReadWithAllocator(global::Unity.Netcode.FastBufferReader reader, out T value, global::Unity.Collections.Allocator allocator);

		void Duplicate(in T value, ref T duplicatedValue);
	}
}
