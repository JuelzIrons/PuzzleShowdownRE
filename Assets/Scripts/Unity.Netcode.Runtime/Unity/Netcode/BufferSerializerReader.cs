namespace Unity.Netcode
{
	internal struct BufferSerializerReader : global::Unity.Netcode.IReaderWriter
	{
		private global::Unity.Netcode.FastBufferReader m_Reader;

		public bool IsReader => true;

		public bool IsWriter => false;

		public BufferSerializerReader(global::Unity.Netcode.FastBufferReader reader)
		{
			m_Reader = reader;
		}

		public global::Unity.Netcode.FastBufferReader GetFastBufferReader()
		{
			return m_Reader;
		}

		public global::Unity.Netcode.FastBufferWriter GetFastBufferWriter()
		{
			throw new global::System.InvalidOperationException("Cannot retrieve a FastBufferWriter from a serializer where IsWriter = false");
		}

		public void SerializeValue(ref string s, bool oneByteChars = false)
		{
			m_Reader.ReadValueSafe(out s, oneByteChars);
		}

		public void SerializeValue(ref byte value)
		{
			m_Reader.ReadByteSafe(out value);
		}

		public void SerializeValue<T>(ref T value, global::Unity.Netcode.FastBufferWriter.ForPrimitives unused = default(global::Unity.Netcode.FastBufferWriter.ForPrimitives)) where T : unmanaged, global::System.IComparable, global::System.IConvertible, global::System.IComparable<T>, global::System.IEquatable<T>
		{
			m_Reader.ReadValueSafe(out value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
		}

		public void SerializeValue<T>(ref T[] value, global::Unity.Netcode.FastBufferWriter.ForPrimitives unused = default(global::Unity.Netcode.FastBufferWriter.ForPrimitives)) where T : unmanaged, global::System.IComparable, global::System.IConvertible, global::System.IComparable<T>, global::System.IEquatable<T>
		{
			m_Reader.ReadValueSafe(out value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
		}

		public void SerializeValue<T>(ref T value, global::Unity.Netcode.FastBufferWriter.ForEnums unused = default(global::Unity.Netcode.FastBufferWriter.ForEnums)) where T : unmanaged, global::System.Enum
		{
			m_Reader.ReadValueSafe(out value, default(global::Unity.Netcode.FastBufferWriter.ForEnums));
		}

		public void SerializeValue<T>(ref T[] value, global::Unity.Netcode.FastBufferWriter.ForEnums unused = default(global::Unity.Netcode.FastBufferWriter.ForEnums)) where T : unmanaged, global::System.Enum
		{
			m_Reader.ReadValueSafe(out value, default(global::Unity.Netcode.FastBufferWriter.ForEnums));
		}

		public void SerializeValue<T>(ref T value, global::Unity.Netcode.FastBufferWriter.ForStructs unused = default(global::Unity.Netcode.FastBufferWriter.ForStructs)) where T : unmanaged, global::Unity.Netcode.INetworkSerializeByMemcpy
		{
			m_Reader.ReadValueSafe(out value, default(global::Unity.Netcode.FastBufferWriter.ForStructs));
		}

		public void SerializeValue<T>(ref T[] value, global::Unity.Netcode.FastBufferWriter.ForStructs unused = default(global::Unity.Netcode.FastBufferWriter.ForStructs)) where T : unmanaged, global::Unity.Netcode.INetworkSerializeByMemcpy
		{
			m_Reader.ReadValueSafe(out value, default(global::Unity.Netcode.FastBufferWriter.ForStructs));
		}

		public void SerializeValue<T>(ref global::Unity.Collections.NativeArray<T> value, global::Unity.Collections.Allocator allocator, global::Unity.Netcode.FastBufferWriter.ForGeneric unused = default(global::Unity.Netcode.FastBufferWriter.ForGeneric)) where T : unmanaged
		{
			m_Reader.ReadValueSafe(out value, allocator, default(global::Unity.Netcode.FastBufferWriter.ForGeneric));
		}

		public void SerializeValue<T>(ref T value, global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable unused = default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable)) where T : global::Unity.Netcode.INetworkSerializable, new()
		{
			m_Reader.ReadNetworkSerializableInPlace(ref value);
		}

		public void SerializeValue<T>(ref T[] value, global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable unused = default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable)) where T : global::Unity.Netcode.INetworkSerializable, new()
		{
			m_Reader.ReadValue(out value, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
		}

		public void SerializeValue<T>(ref T value, global::Unity.Netcode.FastBufferWriter.ForFixedStrings unused = default(global::Unity.Netcode.FastBufferWriter.ForFixedStrings)) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			m_Reader.ReadValueSafe(out value, default(global::Unity.Netcode.FastBufferWriter.ForFixedStrings));
		}

		public void SerializeValue<T>(ref global::Unity.Collections.NativeArray<T> value, global::Unity.Collections.Allocator allocator) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			m_Reader.ReadValueSafe(out value, allocator);
		}

		public void SerializeValue(ref global::UnityEngine.Vector2 value)
		{
			m_Reader.ReadValueSafe(out value);
		}

		public void SerializeValue(ref global::UnityEngine.Vector2[] value)
		{
			m_Reader.ReadValueSafe(out value);
		}

		public void SerializeValue(ref global::UnityEngine.Vector3 value)
		{
			m_Reader.ReadValueSafe(out value);
		}

		public void SerializeValue(ref global::UnityEngine.Vector3[] value)
		{
			m_Reader.ReadValueSafe(out value);
		}

		public void SerializeValue(ref global::UnityEngine.Vector2Int value)
		{
			m_Reader.ReadValueSafe(out value);
		}

		public void SerializeValue(ref global::UnityEngine.Vector2Int[] value)
		{
			m_Reader.ReadValueSafe(out value);
		}

		public void SerializeValue(ref global::UnityEngine.Vector3Int value)
		{
			m_Reader.ReadValueSafe(out value);
		}

		public void SerializeValue(ref global::UnityEngine.Vector3Int[] value)
		{
			m_Reader.ReadValueSafe(out value);
		}

		public void SerializeValue(ref global::UnityEngine.Vector4 value)
		{
			m_Reader.ReadValueSafe(out value);
		}

		public void SerializeValue(ref global::UnityEngine.Vector4[] value)
		{
			m_Reader.ReadValueSafe(out value);
		}

		public void SerializeValue(ref global::UnityEngine.Quaternion value)
		{
			m_Reader.ReadValueSafe(out value);
		}

		public void SerializeValue(ref global::UnityEngine.Quaternion[] value)
		{
			m_Reader.ReadValueSafe(out value);
		}

		public void SerializeValue(ref global::UnityEngine.Pose value)
		{
			m_Reader.ReadValueSafe(out value);
		}

		public void SerializeValue(ref global::UnityEngine.Pose[] value)
		{
			m_Reader.ReadValueSafe(out value);
		}

		public void SerializeValue(ref global::UnityEngine.Color value)
		{
			m_Reader.ReadValueSafe(out value);
		}

		public void SerializeValue(ref global::UnityEngine.Color[] value)
		{
			m_Reader.ReadValueSafe(out value);
		}

		public void SerializeValue(ref global::UnityEngine.Color32 value)
		{
			m_Reader.ReadValueSafe(out value);
		}

		public void SerializeValue(ref global::UnityEngine.Color32[] value)
		{
			m_Reader.ReadValueSafe(out value);
		}

		public void SerializeValue(ref global::UnityEngine.Ray value)
		{
			m_Reader.ReadValueSafe(out value);
		}

		public void SerializeValue(ref global::UnityEngine.Ray[] value)
		{
			m_Reader.ReadValueSafe(out value);
		}

		public void SerializeValue(ref global::UnityEngine.Ray2D value)
		{
			m_Reader.ReadValueSafe(out value);
		}

		public void SerializeValue(ref global::UnityEngine.Ray2D[] value)
		{
			m_Reader.ReadValueSafe(out value);
		}

		public void SerializeNetworkSerializable<T>(ref T value) where T : global::Unity.Netcode.INetworkSerializable, new()
		{
			m_Reader.ReadNetworkSerializable(out value);
		}

		public bool PreCheck(int amount)
		{
			return m_Reader.TryBeginRead(amount);
		}

		public void SerializeValuePreChecked(ref string s, bool oneByteChars = false)
		{
			m_Reader.ReadValue(out s, oneByteChars);
		}

		public void SerializeValuePreChecked(ref byte value)
		{
			m_Reader.ReadByte(out value);
		}

		public void SerializeValuePreChecked<T>(ref T value, global::Unity.Netcode.FastBufferWriter.ForPrimitives unused = default(global::Unity.Netcode.FastBufferWriter.ForPrimitives)) where T : unmanaged, global::System.IComparable, global::System.IConvertible, global::System.IComparable<T>, global::System.IEquatable<T>
		{
			m_Reader.ReadValue(out value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
		}

		public void SerializeValuePreChecked<T>(ref T[] value, global::Unity.Netcode.FastBufferWriter.ForPrimitives unused = default(global::Unity.Netcode.FastBufferWriter.ForPrimitives)) where T : unmanaged, global::System.IComparable, global::System.IConvertible, global::System.IComparable<T>, global::System.IEquatable<T>
		{
			m_Reader.ReadValue(out value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
		}

		public void SerializeValuePreChecked<T>(ref T value, global::Unity.Netcode.FastBufferWriter.ForEnums unused = default(global::Unity.Netcode.FastBufferWriter.ForEnums)) where T : unmanaged, global::System.Enum
		{
			m_Reader.ReadValue(out value, default(global::Unity.Netcode.FastBufferWriter.ForEnums));
		}

		public void SerializeValuePreChecked<T>(ref T[] value, global::Unity.Netcode.FastBufferWriter.ForEnums unused = default(global::Unity.Netcode.FastBufferWriter.ForEnums)) where T : unmanaged, global::System.Enum
		{
			m_Reader.ReadValue(out value, default(global::Unity.Netcode.FastBufferWriter.ForEnums));
		}

		public void SerializeValuePreChecked<T>(ref T value, global::Unity.Netcode.FastBufferWriter.ForStructs unused = default(global::Unity.Netcode.FastBufferWriter.ForStructs)) where T : unmanaged, global::Unity.Netcode.INetworkSerializeByMemcpy
		{
			m_Reader.ReadValue(out value, default(global::Unity.Netcode.FastBufferWriter.ForStructs));
		}

		public void SerializeValuePreChecked<T>(ref T[] value, global::Unity.Netcode.FastBufferWriter.ForStructs unused = default(global::Unity.Netcode.FastBufferWriter.ForStructs)) where T : unmanaged, global::Unity.Netcode.INetworkSerializeByMemcpy
		{
			m_Reader.ReadValue(out value, default(global::Unity.Netcode.FastBufferWriter.ForStructs));
		}

		public void SerializeValuePreChecked<T>(ref global::Unity.Collections.NativeArray<T> value, global::Unity.Collections.Allocator allocator, global::Unity.Netcode.FastBufferWriter.ForGeneric unused = default(global::Unity.Netcode.FastBufferWriter.ForGeneric)) where T : unmanaged
		{
			m_Reader.ReadValue(out value, allocator, default(global::Unity.Netcode.FastBufferWriter.ForGeneric));
		}

		public void SerializeValuePreChecked<T>(ref T value, global::Unity.Netcode.FastBufferWriter.ForFixedStrings unused = default(global::Unity.Netcode.FastBufferWriter.ForFixedStrings)) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			m_Reader.ReadValue<T>(out value, default(global::Unity.Netcode.FastBufferWriter.ForFixedStrings));
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Vector2 value)
		{
			m_Reader.ReadValue(out value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Vector2[] value)
		{
			m_Reader.ReadValue(out value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Vector3 value)
		{
			m_Reader.ReadValue(out value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Vector3[] value)
		{
			m_Reader.ReadValue(out value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Vector2Int value)
		{
			m_Reader.ReadValue(out value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Vector2Int[] value)
		{
			m_Reader.ReadValue(out value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Vector3Int value)
		{
			m_Reader.ReadValue(out value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Vector3Int[] value)
		{
			m_Reader.ReadValue(out value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Vector4 value)
		{
			m_Reader.ReadValue(out value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Vector4[] value)
		{
			m_Reader.ReadValue(out value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Quaternion value)
		{
			m_Reader.ReadValue(out value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Quaternion[] value)
		{
			m_Reader.ReadValue(out value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Pose value)
		{
			m_Reader.ReadValue(out value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Pose[] value)
		{
			m_Reader.ReadValue(out value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Color value)
		{
			m_Reader.ReadValue(out value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Color[] value)
		{
			m_Reader.ReadValue(out value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Color32 value)
		{
			m_Reader.ReadValue(out value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Color32[] value)
		{
			m_Reader.ReadValue(out value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Ray value)
		{
			m_Reader.ReadValue(out value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Ray[] value)
		{
			m_Reader.ReadValue(out value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Ray2D value)
		{
			m_Reader.ReadValue(out value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Ray2D[] value)
		{
			m_Reader.ReadValue(out value);
		}
	}
}
