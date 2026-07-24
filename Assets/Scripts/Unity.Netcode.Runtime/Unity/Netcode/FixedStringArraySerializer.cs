namespace Unity.Netcode
{
	internal class FixedStringArraySerializer<T> : global::Unity.Netcode.INetworkVariableSerializer<global::Unity.Collections.NativeArray<T>> where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
	{
		public void Write(global::Unity.Netcode.FastBufferWriter writer, ref global::Unity.Collections.NativeArray<T> value)
		{
			writer.WriteValueSafe(in value);
		}

		public void Read(global::Unity.Netcode.FastBufferReader reader, ref global::Unity.Collections.NativeArray<T> value)
		{
			value.Dispose();
			reader.ReadValueSafe(out value, global::Unity.Collections.Allocator.Persistent);
		}

		public void WriteDelta(global::Unity.Netcode.FastBufferWriter writer, ref global::Unity.Collections.NativeArray<T> value, ref global::Unity.Collections.NativeArray<T> previousValue)
		{
			global::Unity.Netcode.CollectionSerializationUtility.WriteNativeArrayDelta(writer, ref value, ref previousValue);
		}

		public void ReadDelta(global::Unity.Netcode.FastBufferReader reader, ref global::Unity.Collections.NativeArray<T> value)
		{
			global::Unity.Netcode.CollectionSerializationUtility.ReadNativeArrayDelta(reader, ref value);
		}

		void global::Unity.Netcode.INetworkVariableSerializer<global::Unity.Collections.NativeArray<T>>.ReadWithAllocator(global::Unity.Netcode.FastBufferReader reader, out global::Unity.Collections.NativeArray<T> value, global::Unity.Collections.Allocator allocator)
		{
			reader.ReadValueSafe(out value, allocator);
		}

		public void Duplicate(in global::Unity.Collections.NativeArray<T> value, ref global::Unity.Collections.NativeArray<T> duplicatedValue)
		{
			if (!duplicatedValue.IsCreated || duplicatedValue.Length != value.Length)
			{
				if (duplicatedValue.IsCreated)
				{
					duplicatedValue.Dispose();
				}
				duplicatedValue = new global::Unity.Collections.NativeArray<T>(value.Length, global::Unity.Collections.Allocator.Persistent, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			}
			duplicatedValue.CopyFrom(value);
		}

		void global::Unity.Netcode.INetworkVariableSerializer<global::Unity.Collections.NativeArray<T>>.Duplicate(in global::Unity.Collections.NativeArray<T> value, ref global::Unity.Collections.NativeArray<T> duplicatedValue)
		{
			Duplicate(in value, ref duplicatedValue);
		}
	}
}
