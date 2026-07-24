namespace Unity.Netcode
{
	internal class ListSerializer<T> : global::Unity.Netcode.INetworkVariableSerializer<global::System.Collections.Generic.List<T>>
	{
		public void Write(global::Unity.Netcode.FastBufferWriter writer, ref global::System.Collections.Generic.List<T> value)
		{
			bool value2 = value == null;
			writer.WriteValueSafe(in value2, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			if (value2)
			{
				return;
			}
			global::Unity.Netcode.BytePacker.WriteValuePacked(writer, value.Count);
			foreach (T item in value)
			{
				T value3 = item;
				global::Unity.Netcode.NetworkVariableSerialization<T>.Write(writer, ref value3);
			}
		}

		public void Read(global::Unity.Netcode.FastBufferReader reader, ref global::System.Collections.Generic.List<T> value)
		{
			reader.ReadValueSafe(out bool value2, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			if (value2)
			{
				value = null;
				return;
			}
			if (value == null)
			{
				value = new global::System.Collections.Generic.List<T>();
			}
			global::Unity.Netcode.ByteUnpacker.ReadValuePacked(reader, out int value3);
			if (value3 < value.Count)
			{
				value.RemoveRange(value3, value.Count - value3);
			}
			for (int i = 0; i < value3; i++)
			{
				if (i < value.Count)
				{
					T value4 = value[i];
					global::Unity.Netcode.NetworkVariableSerialization<T>.Read(reader, ref value4);
					value[i] = value4;
				}
				else
				{
					T value5 = default(T);
					global::Unity.Netcode.NetworkVariableSerialization<T>.Read(reader, ref value5);
					value.Add(value5);
				}
			}
		}

		public void WriteDelta(global::Unity.Netcode.FastBufferWriter writer, ref global::System.Collections.Generic.List<T> value, ref global::System.Collections.Generic.List<T> previousValue)
		{
			global::Unity.Netcode.CollectionSerializationUtility.WriteListDelta(writer, ref value, ref previousValue);
		}

		public void ReadDelta(global::Unity.Netcode.FastBufferReader reader, ref global::System.Collections.Generic.List<T> value)
		{
			global::Unity.Netcode.CollectionSerializationUtility.ReadListDelta(reader, ref value);
		}

		void global::Unity.Netcode.INetworkVariableSerializer<global::System.Collections.Generic.List<T>>.ReadWithAllocator(global::Unity.Netcode.FastBufferReader reader, out global::System.Collections.Generic.List<T> value, global::Unity.Collections.Allocator allocator)
		{
			throw new global::System.NotImplementedException();
		}

		public void Duplicate(in global::System.Collections.Generic.List<T> value, ref global::System.Collections.Generic.List<T> duplicatedValue)
		{
			if (duplicatedValue == null)
			{
				duplicatedValue = new global::System.Collections.Generic.List<T>();
			}
			duplicatedValue.Clear();
			foreach (T item in value)
			{
				T value2 = item;
				T duplicatedValue2 = default(T);
				global::Unity.Netcode.NetworkVariableSerialization<T>.Duplicate(in value2, ref duplicatedValue2);
				duplicatedValue.Add(duplicatedValue2);
			}
		}

		void global::Unity.Netcode.INetworkVariableSerializer<global::System.Collections.Generic.List<T>>.Duplicate(in global::System.Collections.Generic.List<T> value, ref global::System.Collections.Generic.List<T> duplicatedValue)
		{
			Duplicate(in value, ref duplicatedValue);
		}
	}
}
