namespace Unity.Netcode
{
	internal class ManagedNetworkSerializableSerializer<T> : global::Unity.Netcode.INetworkVariableSerializer<T> where T : class, global::Unity.Netcode.INetworkSerializable, new()
	{
		public void Write(global::Unity.Netcode.FastBufferWriter writer, ref T value)
		{
			global::Unity.Netcode.BufferSerializer<global::Unity.Netcode.BufferSerializerWriter> bufferSerializer = new global::Unity.Netcode.BufferSerializer<global::Unity.Netcode.BufferSerializerWriter>(new global::Unity.Netcode.BufferSerializerWriter(writer));
			bool value2 = value == null;
			bufferSerializer.SerializeValue(ref value2, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			if (!value2)
			{
				global::Unity.Netcode.BufferSerializer<global::Unity.Netcode.BufferSerializerWriter> serializer = bufferSerializer;
				value.NetworkSerialize(serializer);
			}
		}

		public void Read(global::Unity.Netcode.FastBufferReader reader, ref T value)
		{
			global::Unity.Netcode.BufferSerializer<global::Unity.Netcode.BufferSerializerReader> bufferSerializer = new global::Unity.Netcode.BufferSerializer<global::Unity.Netcode.BufferSerializerReader>(new global::Unity.Netcode.BufferSerializerReader(reader));
			bool value2 = false;
			bufferSerializer.SerializeValue(ref value2, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			if (value2)
			{
				value = null;
				return;
			}
			if (value == null)
			{
				value = new T();
			}
			global::Unity.Netcode.BufferSerializer<global::Unity.Netcode.BufferSerializerReader> serializer = bufferSerializer;
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
			using global::Unity.Netcode.FastBufferWriter writer = new global::Unity.Netcode.FastBufferWriter(256, global::Unity.Collections.Allocator.Temp, int.MaxValue);
			T value2 = value;
			Write(writer, ref value2);
			using global::Unity.Netcode.FastBufferReader reader = new global::Unity.Netcode.FastBufferReader(writer, global::Unity.Collections.Allocator.None);
			Read(reader, ref duplicatedValue);
		}

		void global::Unity.Netcode.INetworkVariableSerializer<T>.Duplicate(in T value, ref T duplicatedValue)
		{
			Duplicate(in value, ref duplicatedValue);
		}
	}
}
