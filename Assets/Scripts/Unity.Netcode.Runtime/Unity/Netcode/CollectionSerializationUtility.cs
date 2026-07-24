namespace Unity.Netcode
{
	internal static class CollectionSerializationUtility
	{
		private static class ListCache<T>
		{
			private static global::System.Collections.Generic.List<T> s_AddedList = new global::System.Collections.Generic.List<T>();

			private static global::System.Collections.Generic.List<T> s_RemovedList = new global::System.Collections.Generic.List<T>();

			private static global::System.Collections.Generic.List<T> s_ChangedList = new global::System.Collections.Generic.List<T>();

			public static global::System.Collections.Generic.List<T> GetAddedList()
			{
				s_AddedList.Clear();
				return s_AddedList;
			}

			public static global::System.Collections.Generic.List<T> GetRemovedList()
			{
				s_RemovedList.Clear();
				return s_RemovedList;
			}

			public static global::System.Collections.Generic.List<T> GetChangedList()
			{
				s_ChangedList.Clear();
				return s_ChangedList;
			}
		}

		public unsafe static void WriteNativeArrayDelta<T>(global::Unity.Netcode.FastBufferWriter writer, ref global::Unity.Collections.NativeArray<T> value, ref global::Unity.Collections.NativeArray<T> previousValue) where T : unmanaged
		{
			using global::Unity.Netcode.ResizableBitVector value2 = new global::Unity.Netcode.ResizableBitVector(global::Unity.Collections.Allocator.Temp);
			int num = global::Unity.Mathematics.math.min(value.Length, previousValue.Length);
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				T a = value[i];
				T b = previousValue[i];
				if (!global::Unity.Netcode.NetworkVariableSerialization<T>.AreEqual(ref a, ref b))
				{
					num2++;
					value2.Set(i);
				}
			}
			for (int j = previousValue.Length; j < value.Length; j++)
			{
				num2++;
				value2.Set(j);
			}
			if (value2.GetSerializedSize() + global::Unity.Netcode.FastBufferWriter.GetWriteSize<T>() * num2 > global::Unity.Netcode.FastBufferWriter.GetWriteSize<T>() * value.Length)
			{
				writer.WriteByteSafe(1);
				writer.WriteValueSafe(value);
				return;
			}
			writer.WriteByte(0);
			global::Unity.Netcode.BytePacker.WriteValuePacked(writer, value.Length);
			writer.WriteValueSafe(in value2, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
			T* unsafePtr = (T*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(value);
			T* unsafePtr2 = (T*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(previousValue);
			for (int k = 0; k < value.Length; k++)
			{
				if (value2.IsSet(k))
				{
					if (k < previousValue.Length)
					{
						global::Unity.Netcode.NetworkVariableSerialization<T>.WriteDelta(writer, ref unsafePtr[k], ref unsafePtr2[k]);
					}
					else
					{
						global::Unity.Netcode.NetworkVariableSerialization<T>.Write(writer, ref unsafePtr[k]);
					}
				}
			}
		}

		public unsafe static void ReadNativeArrayDelta<T>(global::Unity.Netcode.FastBufferReader reader, ref global::Unity.Collections.NativeArray<T> value) where T : unmanaged
		{
			reader.ReadByteSafe(out var value2);
			if (value2 == 1)
			{
				value.Dispose();
				reader.ReadValueSafe(out value, global::Unity.Collections.Allocator.Persistent, default(global::Unity.Netcode.FastBufferWriter.ForGeneric));
				return;
			}
			global::Unity.Netcode.ByteUnpacker.ReadValuePacked(reader, out int value3);
			global::Unity.Netcode.ResizableBitVector value4 = new global::Unity.Netcode.ResizableBitVector(global::Unity.Collections.Allocator.Temp);
			using (value4)
			{
				reader.ReadNetworkSerializableInPlace(ref value4);
				int length = value.Length;
				if (value3 != value.Length)
				{
					global::Unity.Collections.NativeArray<T> nativeArray = new global::Unity.Collections.NativeArray<T>(value3, global::Unity.Collections.Allocator.Persistent);
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(nativeArray), global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(value), global::Unity.Mathematics.math.min(nativeArray.Length * sizeof(T), value.Length * sizeof(T)));
					value.Dispose();
					value = nativeArray;
				}
				T* unsafePtr = (T*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(value);
				for (int i = 0; i < value.Length; i++)
				{
					if (value4.IsSet(i))
					{
						if (i < length)
						{
							global::Unity.Netcode.NetworkVariableSerialization<T>.ReadDelta(reader, ref unsafePtr[i]);
						}
						else
						{
							global::Unity.Netcode.NetworkVariableSerialization<T>.Read(reader, ref unsafePtr[i]);
						}
					}
				}
			}
		}

		public static void WriteListDelta<T>(global::Unity.Netcode.FastBufferWriter writer, ref global::System.Collections.Generic.List<T> value, ref global::System.Collections.Generic.List<T> previousValue)
		{
			if (value == null || previousValue == null)
			{
				writer.WriteByteSafe(1);
				global::Unity.Netcode.NetworkVariableSerialization<global::System.Collections.Generic.List<T>>.Write(writer, ref value);
				return;
			}
			using global::Unity.Netcode.ResizableBitVector value2 = new global::Unity.Netcode.ResizableBitVector(global::Unity.Collections.Allocator.Temp);
			int num = global::Unity.Mathematics.math.min(value.Count, previousValue.Count);
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				T a = value[i];
				T b = previousValue[i];
				if (!global::Unity.Netcode.NetworkVariableSerialization<T>.AreEqual(ref a, ref b))
				{
					num2++;
					value2.Set(i);
				}
			}
			for (int j = previousValue.Count; j < value.Count; j++)
			{
				num2++;
				value2.Set(j);
			}
			if ((double)num2 >= (double)value.Count * 0.9)
			{
				writer.WriteByteSafe(1);
				global::Unity.Netcode.NetworkVariableSerialization<global::System.Collections.Generic.List<T>>.Write(writer, ref value);
				return;
			}
			writer.WriteByteSafe(0);
			global::Unity.Netcode.BytePacker.WriteValuePacked(writer, value.Count);
			writer.WriteValueSafe(in value2, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
			for (int k = 0; k < value.Count; k++)
			{
				if (value2.IsSet(k))
				{
					T value3 = value[k];
					if (k < previousValue.Count)
					{
						T previousValue2 = previousValue[k];
						global::Unity.Netcode.NetworkVariableSerialization<T>.WriteDelta(writer, ref value3, ref previousValue2);
					}
					else
					{
						global::Unity.Netcode.NetworkVariableSerialization<T>.Write(writer, ref value3);
					}
				}
			}
		}

		public static void ReadListDelta<T>(global::Unity.Netcode.FastBufferReader reader, ref global::System.Collections.Generic.List<T> value)
		{
			reader.ReadByteSafe(out var value2);
			if (value2 == 1)
			{
				global::Unity.Netcode.NetworkVariableSerialization<global::System.Collections.Generic.List<T>>.Read(reader, ref value);
				return;
			}
			global::Unity.Netcode.ByteUnpacker.ReadValuePacked(reader, out int value3);
			global::Unity.Netcode.ResizableBitVector value4 = new global::Unity.Netcode.ResizableBitVector(global::Unity.Collections.Allocator.Temp);
			using (value4)
			{
				reader.ReadNetworkSerializableInPlace(ref value4);
				if (value3 < value.Count)
				{
					value.RemoveRange(value3, value.Count - value3);
				}
				for (int i = 0; i < value3; i++)
				{
					if (value4.IsSet(i))
					{
						if (i < value.Count)
						{
							T value5 = value[i];
							global::Unity.Netcode.NetworkVariableSerialization<T>.ReadDelta(reader, ref value5);
							value[i] = value5;
						}
						else
						{
							T value6 = default(T);
							global::Unity.Netcode.NetworkVariableSerialization<T>.Read(reader, ref value6);
							value.Add(value6);
						}
					}
				}
			}
		}

		public static void WriteHashSetDelta<T>(global::Unity.Netcode.FastBufferWriter writer, ref global::System.Collections.Generic.HashSet<T> value, ref global::System.Collections.Generic.HashSet<T> previousValue) where T : global::System.IEquatable<T>
		{
			if (value == null || previousValue == null)
			{
				writer.WriteByteSafe(1);
				global::Unity.Netcode.NetworkVariableSerialization<global::System.Collections.Generic.HashSet<T>>.Write(writer, ref value);
				return;
			}
			global::System.Collections.Generic.List<T> addedList = global::Unity.Netcode.CollectionSerializationUtility.ListCache<T>.GetAddedList();
			global::System.Collections.Generic.List<T> removedList = global::Unity.Netcode.CollectionSerializationUtility.ListCache<T>.GetRemovedList();
			foreach (T item in value)
			{
				if (!previousValue.Contains(item))
				{
					addedList.Add(item);
				}
			}
			foreach (T item2 in previousValue)
			{
				if (!value.Contains(item2))
				{
					removedList.Add(item2);
				}
			}
			if (addedList.Count + removedList.Count >= value.Count)
			{
				writer.WriteByteSafe(1);
				global::Unity.Netcode.NetworkVariableSerialization<global::System.Collections.Generic.HashSet<T>>.Write(writer, ref value);
				return;
			}
			writer.WriteByteSafe(0);
			writer.WriteValueSafe<int>(addedList.Count, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			for (int i = 0; i < addedList.Count; i++)
			{
				T value2 = addedList[i];
				global::Unity.Netcode.NetworkVariableSerialization<T>.Write(writer, ref value2);
			}
			writer.WriteValueSafe<int>(removedList.Count, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			for (int j = 0; j < removedList.Count; j++)
			{
				T value3 = removedList[j];
				global::Unity.Netcode.NetworkVariableSerialization<T>.Write(writer, ref value3);
			}
		}

		public static void ReadHashSetDelta<T>(global::Unity.Netcode.FastBufferReader reader, ref global::System.Collections.Generic.HashSet<T> value) where T : global::System.IEquatable<T>
		{
			reader.ReadByteSafe(out var value2);
			if (value2 != 0)
			{
				global::Unity.Netcode.NetworkVariableSerialization<global::System.Collections.Generic.HashSet<T>>.Read(reader, ref value);
				return;
			}
			reader.ReadValueSafe(out int value3, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			for (int i = 0; i < value3; i++)
			{
				T value4 = default(T);
				global::Unity.Netcode.NetworkVariableSerialization<T>.Read(reader, ref value4);
				value.Add(value4);
			}
			reader.ReadValueSafe(out int value5, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			for (int j = 0; j < value5; j++)
			{
				T value6 = default(T);
				global::Unity.Netcode.NetworkVariableSerialization<T>.Read(reader, ref value6);
				value.Remove(value6);
			}
		}

		public static void WriteDictionaryDelta<TKey, TVal>(global::Unity.Netcode.FastBufferWriter writer, ref global::System.Collections.Generic.Dictionary<TKey, TVal> value, ref global::System.Collections.Generic.Dictionary<TKey, TVal> previousValue) where TKey : global::System.IEquatable<TKey>
		{
			if (value == null || previousValue == null)
			{
				writer.WriteByteSafe(1);
				global::Unity.Netcode.NetworkVariableSerialization<global::System.Collections.Generic.Dictionary<TKey, TVal>>.Write(writer, ref value);
				return;
			}
			global::System.Collections.Generic.List<global::System.Collections.Generic.KeyValuePair<TKey, TVal>> addedList = global::Unity.Netcode.CollectionSerializationUtility.ListCache<global::System.Collections.Generic.KeyValuePair<TKey, TVal>>.GetAddedList();
			global::System.Collections.Generic.List<global::System.Collections.Generic.KeyValuePair<TKey, TVal>> removedList = global::Unity.Netcode.CollectionSerializationUtility.ListCache<global::System.Collections.Generic.KeyValuePair<TKey, TVal>>.GetRemovedList();
			global::System.Collections.Generic.List<global::System.Collections.Generic.KeyValuePair<TKey, TVal>> changedList = global::Unity.Netcode.CollectionSerializationUtility.ListCache<global::System.Collections.Generic.KeyValuePair<TKey, TVal>>.GetChangedList();
			foreach (global::System.Collections.Generic.KeyValuePair<TKey, TVal> item in value)
			{
				TVal a = item.Value;
				if (!previousValue.TryGetValue(item.Key, out var value2))
				{
					addedList.Add(item);
				}
				else if (!global::Unity.Netcode.NetworkVariableSerialization<TVal>.AreEqual(ref a, ref value2))
				{
					removedList.Add(item);
				}
			}
			foreach (global::System.Collections.Generic.KeyValuePair<TKey, TVal> item2 in previousValue)
			{
				if (!value.ContainsKey(item2.Key))
				{
					changedList.Add(item2);
				}
			}
			if (addedList.Count + changedList.Count + removedList.Count >= value.Count)
			{
				writer.WriteByteSafe(1);
				global::Unity.Netcode.NetworkVariableSerialization<global::System.Collections.Generic.Dictionary<TKey, TVal>>.Write(writer, ref value);
				return;
			}
			writer.WriteByteSafe(0);
			writer.WriteValueSafe<int>(addedList.Count, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			for (int i = 0; i < addedList.Count; i++)
			{
				TKey key = addedList[i].Key;
				TVal value3 = addedList[i].Value;
				TKey value4 = key;
				TVal value5 = value3;
				global::Unity.Netcode.NetworkVariableSerialization<TKey>.Write(writer, ref value4);
				global::Unity.Netcode.NetworkVariableSerialization<TVal>.Write(writer, ref value5);
			}
			writer.WriteValueSafe<int>(changedList.Count, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			for (int j = 0; j < changedList.Count; j++)
			{
				TKey value6 = changedList[j].Key;
				global::Unity.Netcode.NetworkVariableSerialization<TKey>.Write(writer, ref value6);
			}
			writer.WriteValueSafe<int>(removedList.Count, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			for (int k = 0; k < removedList.Count; k++)
			{
				TKey key = removedList[k].Key;
				TVal value7 = removedList[k].Value;
				TKey value8 = key;
				TVal value9 = value7;
				global::Unity.Netcode.NetworkVariableSerialization<TKey>.Write(writer, ref value8);
				global::Unity.Netcode.NetworkVariableSerialization<TVal>.Write(writer, ref value9);
			}
		}

		public static void ReadDictionaryDelta<TKey, TVal>(global::Unity.Netcode.FastBufferReader reader, ref global::System.Collections.Generic.Dictionary<TKey, TVal> value) where TKey : global::System.IEquatable<TKey>
		{
			reader.ReadByteSafe(out var value2);
			if (value2 != 0)
			{
				global::Unity.Netcode.NetworkVariableSerialization<global::System.Collections.Generic.Dictionary<TKey, TVal>>.Read(reader, ref value);
				return;
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
			reader.ReadValueSafe(out value3, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			for (int j = 0; j < value3; j++)
			{
				TKey value6 = default(TKey);
				global::Unity.Netcode.NetworkVariableSerialization<TKey>.Read(reader, ref value6);
				value.Remove(value6);
			}
			reader.ReadValueSafe(out value3, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			for (int k = 0; k < value3; k++)
			{
				TKey value7 = default(TKey);
				TVal value8 = default(TVal);
				global::Unity.Netcode.NetworkVariableSerialization<TKey>.Read(reader, ref value7);
				global::Unity.Netcode.NetworkVariableSerialization<TVal>.Read(reader, ref value8);
				value[value7] = value8;
			}
		}
	}
}
