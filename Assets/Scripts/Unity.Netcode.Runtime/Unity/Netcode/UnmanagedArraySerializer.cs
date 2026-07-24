namespace Unity.Netcode
{
	internal class UnmanagedArraySerializer<T> : global::Unity.Netcode.INetworkVariableSerializer<global::Unity.Collections.NativeArray<T>> where T : unmanaged
	{
		public void Write(global::Unity.Netcode.FastBufferWriter writer, ref global::Unity.Collections.NativeArray<T> value)
		{
			writer.WriteUnmanagedSafe(value);
		}

		public void Read(global::Unity.Netcode.FastBufferReader reader, ref global::Unity.Collections.NativeArray<T> value)
		{
			value.Dispose();
			reader.ReadUnmanagedSafe(out value, global::Unity.Collections.Allocator.Persistent);
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
			reader.ReadUnmanagedSafe(out value, allocator);
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
