namespace Unity.Netcode
{
	public static class NetworkVariableSerializationTypedInitializers
	{
		[global::UnityEngine.RuntimeInitializeOnLoadMethod(global::UnityEngine.RuntimeInitializeLoadType.AfterAssembliesLoaded)]
		internal static void InitializeIntegerSerialization()
		{
			global::Unity.Netcode.NetworkVariableSerialization<short>.Serializer = new global::Unity.Netcode.ShortSerializer();
			global::Unity.Netcode.NetworkVariableSerialization<short>.AreEqual = global::Unity.Netcode.NetworkVariableEquality<short>.ValueEquals;
			global::Unity.Netcode.NetworkVariableSerialization<ushort>.Serializer = new global::Unity.Netcode.UshortSerializer();
			global::Unity.Netcode.NetworkVariableSerialization<ushort>.AreEqual = global::Unity.Netcode.NetworkVariableEquality<ushort>.ValueEquals;
			global::Unity.Netcode.NetworkVariableSerialization<int>.Serializer = new global::Unity.Netcode.IntSerializer();
			global::Unity.Netcode.NetworkVariableSerialization<int>.AreEqual = global::Unity.Netcode.NetworkVariableEquality<int>.ValueEquals;
			global::Unity.Netcode.NetworkVariableSerialization<uint>.Serializer = new global::Unity.Netcode.UintSerializer();
			global::Unity.Netcode.NetworkVariableSerialization<uint>.AreEqual = global::Unity.Netcode.NetworkVariableEquality<uint>.ValueEquals;
			global::Unity.Netcode.NetworkVariableSerialization<long>.Serializer = new global::Unity.Netcode.LongSerializer();
			global::Unity.Netcode.NetworkVariableSerialization<long>.AreEqual = global::Unity.Netcode.NetworkVariableEquality<long>.ValueEquals;
			global::Unity.Netcode.NetworkVariableSerialization<ulong>.Serializer = new global::Unity.Netcode.UlongSerializer();
			global::Unity.Netcode.NetworkVariableSerialization<ulong>.AreEqual = global::Unity.Netcode.NetworkVariableEquality<ulong>.ValueEquals;
		}

		public static void InitializeSerializer_UnmanagedByMemcpy<T>() where T : unmanaged
		{
			global::Unity.Netcode.NetworkVariableSerialization<T>.Serializer = new global::Unity.Netcode.UnmanagedTypeSerializer<T>();
		}

		public static void InitializeSerializer_UnmanagedByMemcpyArray<T>() where T : unmanaged
		{
			global::Unity.Netcode.NetworkVariableSerialization<global::Unity.Collections.NativeArray<T>>.Serializer = new global::Unity.Netcode.UnmanagedArraySerializer<T>();
		}

		public static void InitializeSerializer_List<T>()
		{
			global::Unity.Netcode.NetworkVariableSerialization<global::System.Collections.Generic.List<T>>.Serializer = new global::Unity.Netcode.ListSerializer<T>();
		}

		public static void InitializeSerializer_HashSet<T>() where T : global::System.IEquatable<T>
		{
			global::Unity.Netcode.NetworkVariableSerialization<global::System.Collections.Generic.HashSet<T>>.Serializer = new global::Unity.Netcode.HashSetSerializer<T>();
		}

		public static void InitializeSerializer_Dictionary<TKey, TVal>() where TKey : global::System.IEquatable<TKey>
		{
			global::Unity.Netcode.NetworkVariableSerialization<global::System.Collections.Generic.Dictionary<TKey, TVal>>.Serializer = new global::Unity.Netcode.DictionarySerializer<TKey, TVal>();
		}

		public static void InitializeSerializer_UnmanagedINetworkSerializable<T>() where T : unmanaged, global::Unity.Netcode.INetworkSerializable
		{
			global::Unity.Netcode.NetworkVariableSerialization<T>.Serializer = new global::Unity.Netcode.UnmanagedNetworkSerializableSerializer<T>();
		}

		public static void InitializeSerializer_UnmanagedINetworkSerializableArray<T>() where T : unmanaged, global::Unity.Netcode.INetworkSerializable
		{
			global::Unity.Netcode.NetworkVariableSerialization<global::Unity.Collections.NativeArray<T>>.Serializer = new global::Unity.Netcode.UnmanagedNetworkSerializableArraySerializer<T>();
		}

		public static void InitializeSerializer_ManagedINetworkSerializable<T>() where T : class, global::Unity.Netcode.INetworkSerializable, new()
		{
			global::Unity.Netcode.NetworkVariableSerialization<T>.Serializer = new global::Unity.Netcode.ManagedNetworkSerializableSerializer<T>();
		}

		public static void InitializeSerializer_FixedString<T>() where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Netcode.NetworkVariableSerialization<T>.Serializer = new global::Unity.Netcode.FixedStringSerializer<T>();
		}

		public static void InitializeSerializer_FixedStringArray<T>() where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Netcode.NetworkVariableSerialization<global::Unity.Collections.NativeArray<T>>.Serializer = new global::Unity.Netcode.FixedStringArraySerializer<T>();
		}

		public static void InitializeEqualityChecker_ManagedIEquatable<T>() where T : class, global::System.IEquatable<T>
		{
			global::Unity.Netcode.NetworkVariableSerialization<T>.AreEqual = global::Unity.Netcode.NetworkVariableEquality<T>.EqualityEqualsObject;
		}

		public static void InitializeEqualityChecker_UnmanagedIEquatable<T>() where T : unmanaged, global::System.IEquatable<T>
		{
			global::Unity.Netcode.NetworkVariableSerialization<T>.AreEqual = global::Unity.Netcode.NetworkVariableEquality<T>.EqualityEquals;
		}

		public static void InitializeEqualityChecker_UnmanagedIEquatableArray<T>() where T : unmanaged, global::System.IEquatable<T>
		{
			global::Unity.Netcode.NetworkVariableSerialization<global::Unity.Collections.NativeArray<T>>.AreEqual = global::Unity.Netcode.NetworkVariableEquality<T>.EqualityEqualsArray;
		}

		public static void InitializeEqualityChecker_List<T>()
		{
			global::Unity.Netcode.NetworkVariableSerialization<global::System.Collections.Generic.List<T>>.AreEqual = global::Unity.Netcode.NetworkVariableEquality<T>.EqualityEqualsList;
		}

		public static void InitializeEqualityChecker_HashSet<T>() where T : global::System.IEquatable<T>
		{
			global::Unity.Netcode.NetworkVariableSerialization<global::System.Collections.Generic.HashSet<T>>.AreEqual = global::Unity.Netcode.NetworkVariableEquality<T>.EqualityEqualsHashSet;
		}

		public static void InitializeEqualityChecker_Dictionary<TKey, TVal>() where TKey : global::System.IEquatable<TKey>
		{
			global::Unity.Netcode.NetworkVariableSerialization<global::System.Collections.Generic.Dictionary<TKey, TVal>>.AreEqual = NetworkVariableDictionarySerialization<TKey, TVal>.GenericEqualsDictionary;
		}

		public static void InitializeEqualityChecker_UnmanagedValueEquals<T>() where T : unmanaged
		{
			global::Unity.Netcode.NetworkVariableSerialization<T>.AreEqual = global::Unity.Netcode.NetworkVariableEquality<T>.ValueEquals;
		}

		public static void InitializeEqualityChecker_UnmanagedValueEqualsArray<T>() where T : unmanaged
		{
			global::Unity.Netcode.NetworkVariableSerialization<global::Unity.Collections.NativeArray<T>>.AreEqual = global::Unity.Netcode.NetworkVariableEquality<T>.ValueEqualsArray;
		}

		public static void InitializeEqualityChecker_ManagedClassEquals<T>() where T : class
		{
			global::Unity.Netcode.NetworkVariableSerialization<T>.AreEqual = global::Unity.Netcode.NetworkVariableEquality<T>.ClassEquals;
		}
	}
}
