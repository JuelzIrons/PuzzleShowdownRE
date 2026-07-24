namespace Unity.Netcode
{
	internal class UnmanagedNetworkSerializableSerializer<T> : global::Unity.Netcode.INetworkVariableSerializer<T> where T : unmanaged, global::Unity.Netcode.INetworkSerializable
	{
		public void Write(global::Unity.Netcode.FastBufferWriter writer, ref T value)
		{
			global::Unity.Netcode.BufferSerializer<global::Unity.Netcode.BufferSerializerWriter> serializer = new global::Unity.Netcode.BufferSerializer<global::Unity.Netcode.BufferSerializerWriter>(new global::Unity.Netcode.BufferSerializerWriter(writer));
			value.NetworkSerialize(serializer);
		}

		public void Read(global::Unity.Netcode.FastBufferReader reader, ref T value)
		{
			global::Unity.Netcode.BufferSerializer<global::Unity.Netcode.BufferSerializerReader> serializer = new global::Unity.Netcode.BufferSerializer<global::Unity.Netcode.BufferSerializerReader>(new global::Unity.Netcode.BufferSerializerReader(reader));
			value.NetworkSerialize(serializer);
		}

		public void WriteDelta(global::Unity.Netcode.FastBufferWriter writer, ref T value, ref T previousValue)
		{
			if (global::Unity.Netcode.UserNetworkVariableSerialization<T>.WriteDelta != null && global::Unity.Netcode.UserNetworkVariableSerialization<T>.ReadDelta != null)
			{
				global::Unity.Netcode.UserNetworkVariableSerialization<T>.WriteDelta(writer, in value, in previousValue);
			}
			else
			{
				Write(writer, ref value);
			}
		}

		public void ReadDelta(global::Unity.Netcode.FastBufferReader reader, ref T value)
		{
			if (global::Unity.Netcode.UserNetworkVariableSerialization<T>.WriteDelta != null && global::Unity.Netcode.UserNetworkVariableSerialization<T>.ReadDelta != null)
			{
				global::Unity.Netcode.UserNetworkVariableSerialization<T>.ReadDelta(reader, ref value);
			}
			else
			{
				Read(reader, ref value);
			}
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
