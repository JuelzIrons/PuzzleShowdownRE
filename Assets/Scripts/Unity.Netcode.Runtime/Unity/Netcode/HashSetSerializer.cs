namespace Unity.Netcode
{
	internal class HashSetSerializer<T> : global::Unity.Netcode.INetworkVariableSerializer<global::System.Collections.Generic.HashSet<T>> where T : global::System.IEquatable<T>
	{
		public void Write(global::Unity.Netcode.FastBufferWriter writer, ref global::System.Collections.Generic.HashSet<T> value)
		{
			bool value2 = value == null;
			writer.WriteValueSafe(in value2, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			if (value2)
			{
				return;
			}
			writer.WriteValueSafe<int>(value.Count, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			foreach (T item in value)
			{
				T value3 = item;
				global::Unity.Netcode.NetworkVariableSerialization<T>.Write(writer, ref value3);
			}
		}

		public void Read(global::Unity.Netcode.FastBufferReader reader, ref global::System.Collections.Generic.HashSet<T> value)
		{
			reader.ReadValueSafe(out bool value2, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			if (value2)
			{
				value = null;
				return;
			}
			if (value == null)
			{
				value = new global::System.Collections.Generic.HashSet<T>();
			}
			else
			{
				value.Clear();
			}
			reader.ReadValueSafe(out int value3, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			for (int i = 0; i < value3; i++)
			{
				T value4 = default(T);
				global::Unity.Netcode.NetworkVariableSerialization<T>.Read(reader, ref value4);
				value.Add(value4);
			}
		}

		public void WriteDelta(global::Unity.Netcode.FastBufferWriter writer, ref global::System.Collections.Generic.HashSet<T> value, ref global::System.Collections.Generic.HashSet<T> previousValue)
		{
			global::Unity.Netcode.CollectionSerializationUtility.WriteHashSetDelta(writer, ref value, ref previousValue);
		}

		public void ReadDelta(global::Unity.Netcode.FastBufferReader reader, ref global::System.Collections.Generic.HashSet<T> value)
		{
			global::Unity.Netcode.CollectionSerializationUtility.ReadHashSetDelta(reader, ref value);
		}

		void global::Unity.Netcode.INetworkVariableSerializer<global::System.Collections.Generic.HashSet<T>>.ReadWithAllocator(global::Unity.Netcode.FastBufferReader reader, out global::System.Collections.Generic.HashSet<T> value, global::Unity.Collections.Allocator allocator)
		{
			throw new global::System.NotImplementedException();
		}

		public void Duplicate(in global::System.Collections.Generic.HashSet<T> value, ref global::System.Collections.Generic.HashSet<T> duplicatedValue)
		{
			if (duplicatedValue == null)
			{
				duplicatedValue = new global::System.Collections.Generic.HashSet<T>();
			}
			duplicatedValue.Clear();
			foreach (T item in value)
			{
				T value2 = item;
				T duplicatedValue2 = default(T);
				global::Unity.Netcode.NetworkVariableSerialization<T>.Duplicate(in value2, ref duplicatedValue2);
				duplicatedValue.Add(value2);
			}
		}

		void global::Unity.Netcode.INetworkVariableSerializer<global::System.Collections.Generic.HashSet<T>>.Duplicate(in global::System.Collections.Generic.HashSet<T> value, ref global::System.Collections.Generic.HashSet<T> duplicatedValue)
		{
			Duplicate(in value, ref duplicatedValue);
		}
	}
}
