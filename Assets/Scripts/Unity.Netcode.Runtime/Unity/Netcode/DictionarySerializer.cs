namespace Unity.Netcode
{
	internal class DictionarySerializer<TKey, TVal> : global::Unity.Netcode.INetworkVariableSerializer<global::System.Collections.Generic.Dictionary<TKey, TVal>> where TKey : global::System.IEquatable<TKey>
	{
		public void Write(global::Unity.Netcode.FastBufferWriter writer, ref global::System.Collections.Generic.Dictionary<TKey, TVal> value)
		{
			bool value2 = value == null;
			writer.WriteValueSafe(in value2, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			if (value2)
			{
				return;
			}
			writer.WriteValueSafe<int>(value.Count, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			foreach (global::System.Collections.Generic.KeyValuePair<TKey, TVal> item in value)
			{
				TKey key = item.Key;
				TVal value3 = item.Value;
				TKey value4 = key;
				TVal value5 = value3;
				global::Unity.Netcode.NetworkVariableSerialization<TKey>.Write(writer, ref value4);
				global::Unity.Netcode.NetworkVariableSerialization<TVal>.Write(writer, ref value5);
			}
		}

		public void Read(global::Unity.Netcode.FastBufferReader reader, ref global::System.Collections.Generic.Dictionary<TKey, TVal> value)
		{
			reader.ReadValueSafe(out bool value2, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			if (value2)
			{
				value = null;
				return;
			}
			if (value == null)
			{
				value = new global::System.Collections.Generic.Dictionary<TKey, TVal>();
			}
			else
			{
				value.Clear();
			}
			reader.ReadValueSafe(out int value3, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			for (int i = 0; i < value3; i++)
			{
				TKey value4 = default(TKey);
				TVal value5 = default(TVal);
				global::Unity.Netcode.NetworkVariableSerialization<TKey>.Read(reader, ref value4);
				global::Unity.Netcode.NetworkVariableSerialization<TVal>.Read(reader, ref value5);
				value.Add(value4, value5);
			}
		}

		public void WriteDelta(global::Unity.Netcode.FastBufferWriter writer, ref global::System.Collections.Generic.Dictionary<TKey, TVal> value, ref global::System.Collections.Generic.Dictionary<TKey, TVal> previousValue)
		{
			global::Unity.Netcode.CollectionSerializationUtility.WriteDictionaryDelta(writer, ref value, ref previousValue);
		}

		public void ReadDelta(global::Unity.Netcode.FastBufferReader reader, ref global::System.Collections.Generic.Dictionary<TKey, TVal> value)
		{
			global::Unity.Netcode.CollectionSerializationUtility.ReadDictionaryDelta(reader, ref value);
		}

		void global::Unity.Netcode.INetworkVariableSerializer<global::System.Collections.Generic.Dictionary<TKey, TVal>>.ReadWithAllocator(global::Unity.Netcode.FastBufferReader reader, out global::System.Collections.Generic.Dictionary<TKey, TVal> value, global::Unity.Collections.Allocator allocator)
		{
			throw new global::System.NotImplementedException();
		}

		public void Duplicate(in global::System.Collections.Generic.Dictionary<TKey, TVal> value, ref global::System.Collections.Generic.Dictionary<TKey, TVal> duplicatedValue)
		{
			if (duplicatedValue == null)
			{
				duplicatedValue = new global::System.Collections.Generic.Dictionary<TKey, TVal>();
			}
			duplicatedValue.Clear();
			foreach (global::System.Collections.Generic.KeyValuePair<TKey, TVal> item in value)
			{
				TKey duplicatedValue2 = default(TKey);
				TVal duplicatedValue3 = default(TVal);
				global::Unity.Netcode.NetworkVariableSerialization<TKey>.Duplicate(item.Key, ref duplicatedValue2);
				global::Unity.Netcode.NetworkVariableSerialization<TVal>.Duplicate(item.Value, ref duplicatedValue3);
				duplicatedValue.Add(duplicatedValue2, duplicatedValue3);
			}
		}

		void global::Unity.Netcode.INetworkVariableSerializer<global::System.Collections.Generic.Dictionary<TKey, TVal>>.Duplicate(in global::System.Collections.Generic.Dictionary<TKey, TVal> value, ref global::System.Collections.Generic.Dictionary<TKey, TVal> duplicatedValue)
		{
			Duplicate(in value, ref duplicatedValue);
		}
	}
}
