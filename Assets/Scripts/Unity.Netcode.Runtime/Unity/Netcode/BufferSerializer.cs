namespace Unity.Netcode
{
	public ref struct BufferSerializer<TReaderWriter> where TReaderWriter : global::Unity.Netcode.IReaderWriter
	{
		private TReaderWriter m_Implementation;

		public bool IsReader => m_Implementation.IsReader;

		public bool IsWriter => m_Implementation.IsWriter;

		internal BufferSerializer(TReaderWriter implementation)
		{
			m_Implementation = implementation;
		}

		public global::Unity.Netcode.FastBufferReader GetFastBufferReader()
		{
			return m_Implementation.GetFastBufferReader();
		}

		public global::Unity.Netcode.FastBufferWriter GetFastBufferWriter()
		{
			return m_Implementation.GetFastBufferWriter();
		}

		public void SerializeValue(ref string s, bool oneByteChars = false)
		{
			m_Implementation.SerializeValue(ref s, oneByteChars);
		}

		public void SerializeValue(ref byte value)
		{
			m_Implementation.SerializeValue(ref value);
		}

		public void SerializeValue<T>(ref T value, global::Unity.Netcode.FastBufferWriter.ForPrimitives unused = default(global::Unity.Netcode.FastBufferWriter.ForPrimitives)) where T : unmanaged, global::System.IComparable, global::System.IConvertible, global::System.IComparable<T>, global::System.IEquatable<T>
		{
			m_Implementation.SerializeValue(ref value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
		}

		public void SerializeValue<T>(ref T[] value, global::Unity.Netcode.FastBufferWriter.ForPrimitives unused = default(global::Unity.Netcode.FastBufferWriter.ForPrimitives)) where T : unmanaged, global::System.IComparable, global::System.IConvertible, global::System.IComparable<T>, global::System.IEquatable<T>
		{
			m_Implementation.SerializeValue(ref value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
		}

		public void SerializeValue<T>(ref T value, global::Unity.Netcode.FastBufferWriter.ForEnums unused = default(global::Unity.Netcode.FastBufferWriter.ForEnums)) where T : unmanaged, global::System.Enum
		{
			m_Implementation.SerializeValue(ref value, default(global::Unity.Netcode.FastBufferWriter.ForEnums));
		}

		public void SerializeValue<T>(ref T[] value, global::Unity.Netcode.FastBufferWriter.ForEnums unused = default(global::Unity.Netcode.FastBufferWriter.ForEnums)) where T : unmanaged, global::System.Enum
		{
			m_Implementation.SerializeValue(ref value, default(global::Unity.Netcode.FastBufferWriter.ForEnums));
		}

		public void SerializeValue<T>(ref T value, global::Unity.Netcode.FastBufferWriter.ForStructs unused = default(global::Unity.Netcode.FastBufferWriter.ForStructs)) where T : unmanaged, global::Unity.Netcode.INetworkSerializeByMemcpy
		{
			m_Implementation.SerializeValue(ref value, default(global::Unity.Netcode.FastBufferWriter.ForStructs));
		}

		public void SerializeValue<T>(ref T[] value, global::Unity.Netcode.FastBufferWriter.ForStructs unused = default(global::Unity.Netcode.FastBufferWriter.ForStructs)) where T : unmanaged, global::Unity.Netcode.INetworkSerializeByMemcpy
		{
			m_Implementation.SerializeValue(ref value, default(global::Unity.Netcode.FastBufferWriter.ForStructs));
		}

		public void SerializeValue<T>(ref global::Unity.Collections.NativeArray<T> value, global::Unity.Collections.Allocator allocator, global::Unity.Netcode.FastBufferWriter.ForGeneric unused = default(global::Unity.Netcode.FastBufferWriter.ForGeneric)) where T : unmanaged
		{
			m_Implementation.SerializeValue(ref value, allocator, default(global::Unity.Netcode.FastBufferWriter.ForGeneric));
		}

		public void SerializeValue<T>(ref T value, global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable unused = default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable)) where T : global::Unity.Netcode.INetworkSerializable, new()
		{
			m_Implementation.SerializeValue(ref value, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
		}

		public void SerializeValue<T>(ref T[] value, global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable unused = default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable)) where T : global::Unity.Netcode.INetworkSerializable, new()
		{
			m_Implementation.SerializeValue(ref value, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
		}

		public void SerializeValue(ref global::UnityEngine.Vector2 value)
		{
			m_Implementation.SerializeValue(ref value);
		}

		public void SerializeValue(ref global::UnityEngine.Vector2[] value)
		{
			m_Implementation.SerializeValue(ref value);
		}

		public void SerializeValue(ref global::UnityEngine.Vector3 value)
		{
			m_Implementation.SerializeValue(ref value);
		}

		public void SerializeValue(ref global::UnityEngine.Vector3[] value)
		{
			m_Implementation.SerializeValue(ref value);
		}

		public void SerializeValue(ref global::UnityEngine.Vector2Int value)
		{
			m_Implementation.SerializeValue(ref value);
		}

		public void SerializeValue(ref global::UnityEngine.Vector2Int[] value)
		{
			m_Implementation.SerializeValue(ref value);
		}

		public void SerializeValue(ref global::UnityEngine.Vector3Int value)
		{
			m_Implementation.SerializeValue(ref value);
		}

		public void SerializeValue(ref global::UnityEngine.Vector3Int[] value)
		{
			m_Implementation.SerializeValue(ref value);
		}

		public void SerializeValue(ref global::UnityEngine.Vector4 value)
		{
			m_Implementation.SerializeValue(ref value);
		}

		public void SerializeValue(ref global::UnityEngine.Vector4[] value)
		{
			m_Implementation.SerializeValue(ref value);
		}

		public void SerializeValue(ref global::UnityEngine.Quaternion value)
		{
			m_Implementation.SerializeValue(ref value);
		}

		public void SerializeValue(ref global::UnityEngine.Quaternion[] value)
		{
			m_Implementation.SerializeValue(ref value);
		}

		public void SerializeValue(ref global::UnityEngine.Pose value)
		{
			m_Implementation.SerializeValue(ref value);
		}

		public void SerializeValue(ref global::UnityEngine.Pose[] value)
		{
			m_Implementation.SerializeValue(ref value);
		}

		public void SerializeValue(ref global::UnityEngine.Color value)
		{
			m_Implementation.SerializeValue(ref value);
		}

		public void SerializeValue(ref global::UnityEngine.Color[] value)
		{
			m_Implementation.SerializeValue(ref value);
		}

		public void SerializeValue(ref global::UnityEngine.Color32 value)
		{
			m_Implementation.SerializeValue(ref value);
		}

		public void SerializeValue(ref global::UnityEngine.Color32[] value)
		{
			m_Implementation.SerializeValue(ref value);
		}

		public void SerializeValue(ref global::UnityEngine.Ray value)
		{
			m_Implementation.SerializeValue(ref value);
		}

		public void SerializeValue(ref global::UnityEngine.Ray[] value)
		{
			m_Implementation.SerializeValue(ref value);
		}

		public void SerializeValue(ref global::UnityEngine.Ray2D value)
		{
			m_Implementation.SerializeValue(ref value);
		}

		public void SerializeValue(ref global::UnityEngine.Ray2D[] value)
		{
			m_Implementation.SerializeValue(ref value);
		}

		public void SerializeValue<T>(ref T value, global::Unity.Netcode.FastBufferWriter.ForFixedStrings unused = default(global::Unity.Netcode.FastBufferWriter.ForFixedStrings)) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			m_Implementation.SerializeValue(ref value, default(global::Unity.Netcode.FastBufferWriter.ForFixedStrings));
		}

		public void SerializeValue<T>(ref global::Unity.Collections.NativeArray<T> value, global::Unity.Collections.Allocator allocator) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			m_Implementation.SerializeValue(ref value, allocator);
		}

		public void SerializeNetworkSerializable<T>(ref T value) where T : global::Unity.Netcode.INetworkSerializable, new()
		{
			m_Implementation.SerializeNetworkSerializable(ref value);
		}

		public bool PreCheck(int amount)
		{
			return m_Implementation.PreCheck(amount);
		}

		public void SerializeValuePreChecked(ref string s, bool oneByteChars = false)
		{
			m_Implementation.SerializeValuePreChecked(ref s, oneByteChars);
		}

		public void SerializeValuePreChecked(ref byte value)
		{
			m_Implementation.SerializeValuePreChecked(ref value);
		}

		public void SerializeValuePreChecked<T>(ref T value, global::Unity.Netcode.FastBufferWriter.ForPrimitives unused = default(global::Unity.Netcode.FastBufferWriter.ForPrimitives)) where T : unmanaged, global::System.IComparable, global::System.IConvertible, global::System.IComparable<T>, global::System.IEquatable<T>
		{
			m_Implementation.SerializeValuePreChecked(ref value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
		}

		public void SerializeValuePreChecked<T>(ref T[] value, global::Unity.Netcode.FastBufferWriter.ForPrimitives unused = default(global::Unity.Netcode.FastBufferWriter.ForPrimitives)) where T : unmanaged, global::System.IComparable, global::System.IConvertible, global::System.IComparable<T>, global::System.IEquatable<T>
		{
			m_Implementation.SerializeValuePreChecked(ref value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
		}

		public void SerializeValuePreChecked<T>(ref T value, global::Unity.Netcode.FastBufferWriter.ForEnums unused = default(global::Unity.Netcode.FastBufferWriter.ForEnums)) where T : unmanaged, global::System.Enum
		{
			m_Implementation.SerializeValuePreChecked(ref value, default(global::Unity.Netcode.FastBufferWriter.ForEnums));
		}

		public void SerializeValuePreChecked<T>(ref T[] value, global::Unity.Netcode.FastBufferWriter.ForEnums unused = default(global::Unity.Netcode.FastBufferWriter.ForEnums)) where T : unmanaged, global::System.Enum
		{
			m_Implementation.SerializeValuePreChecked(ref value, default(global::Unity.Netcode.FastBufferWriter.ForEnums));
		}

		public void SerializeValuePreChecked<T>(ref T value, global::Unity.Netcode.FastBufferWriter.ForStructs unused = default(global::Unity.Netcode.FastBufferWriter.ForStructs)) where T : unmanaged, global::Unity.Netcode.INetworkSerializeByMemcpy
		{
			m_Implementation.SerializeValuePreChecked(ref value, default(global::Unity.Netcode.FastBufferWriter.ForStructs));
		}

		public void SerializeValuePreChecked<T>(ref T[] value, global::Unity.Netcode.FastBufferWriter.ForStructs unused = default(global::Unity.Netcode.FastBufferWriter.ForStructs)) where T : unmanaged, global::Unity.Netcode.INetworkSerializeByMemcpy
		{
			m_Implementation.SerializeValuePreChecked(ref value, default(global::Unity.Netcode.FastBufferWriter.ForStructs));
		}

		public void SerializeValuePreChecked<T>(ref global::Unity.Collections.NativeArray<T> value, global::Unity.Collections.Allocator allocator, global::Unity.Netcode.FastBufferWriter.ForGeneric unused = default(global::Unity.Netcode.FastBufferWriter.ForGeneric)) where T : unmanaged
		{
			m_Implementation.SerializeValuePreChecked(ref value, allocator);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Vector2 value)
		{
			m_Implementation.SerializeValuePreChecked(ref value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Vector2[] value)
		{
			m_Implementation.SerializeValuePreChecked(ref value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Vector3 value)
		{
			m_Implementation.SerializeValuePreChecked(ref value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Vector3[] value)
		{
			m_Implementation.SerializeValuePreChecked(ref value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Vector2Int value)
		{
			m_Implementation.SerializeValuePreChecked(ref value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Vector2Int[] value)
		{
			m_Implementation.SerializeValuePreChecked(ref value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Vector3Int value)
		{
			m_Implementation.SerializeValuePreChecked(ref value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Vector3Int[] value)
		{
			m_Implementation.SerializeValuePreChecked(ref value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Vector4 value)
		{
			m_Implementation.SerializeValuePreChecked(ref value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Vector4[] value)
		{
			m_Implementation.SerializeValuePreChecked(ref value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Quaternion value)
		{
			m_Implementation.SerializeValuePreChecked(ref value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Quaternion[] value)
		{
			m_Implementation.SerializeValuePreChecked(ref value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Pose value)
		{
			m_Implementation.SerializeValuePreChecked(ref value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Pose[] value)
		{
			m_Implementation.SerializeValuePreChecked(ref value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Color value)
		{
			m_Implementation.SerializeValuePreChecked(ref value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Color[] value)
		{
			m_Implementation.SerializeValuePreChecked(ref value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Color32 value)
		{
			m_Implementation.SerializeValuePreChecked(ref value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Color32[] value)
		{
			m_Implementation.SerializeValuePreChecked(ref value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Ray value)
		{
			m_Implementation.SerializeValuePreChecked(ref value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Ray[] value)
		{
			m_Implementation.SerializeValuePreChecked(ref value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Ray2D value)
		{
			m_Implementation.SerializeValuePreChecked(ref value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Ray2D[] value)
		{
			m_Implementation.SerializeValuePreChecked(ref value);
		}

		public void SerializeValuePreChecked<T>(ref T value, global::Unity.Netcode.FastBufferWriter.ForFixedStrings unused = default(global::Unity.Netcode.FastBufferWriter.ForFixedStrings)) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			m_Implementation.SerializeValuePreChecked(ref value, default(global::Unity.Netcode.FastBufferWriter.ForFixedStrings));
		}
	}
}
