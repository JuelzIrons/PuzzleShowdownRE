namespace Unity.Netcode
{
	internal struct BufferSerializerWriter : global::Unity.Netcode.IReaderWriter
	{
		private global::Unity.Netcode.FastBufferWriter m_Writer;

		public bool IsReader => false;

		public bool IsWriter => true;

		public BufferSerializerWriter(global::Unity.Netcode.FastBufferWriter writer)
		{
			m_Writer = writer;
		}

		public global::Unity.Netcode.FastBufferReader GetFastBufferReader()
		{
			throw new global::System.InvalidOperationException("Cannot retrieve a FastBufferReader from a serializer where IsReader = false");
		}

		public global::Unity.Netcode.FastBufferWriter GetFastBufferWriter()
		{
			return m_Writer;
		}

		public void SerializeValue(ref string s, bool oneByteChars = false)
		{
			m_Writer.WriteValueSafe(s, oneByteChars);
		}

		public void SerializeValue(ref byte value)
		{
			m_Writer.WriteByteSafe(value);
		}

		public void SerializeValue<T>(ref T value, global::Unity.Netcode.FastBufferWriter.ForPrimitives unused = default(global::Unity.Netcode.FastBufferWriter.ForPrimitives)) where T : unmanaged, global::System.IComparable, global::System.IConvertible, global::System.IComparable<T>, global::System.IEquatable<T>
		{
			m_Writer.WriteValueSafe(in value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
		}

		public void SerializeValue<T>(ref T[] value, global::Unity.Netcode.FastBufferWriter.ForPrimitives unused = default(global::Unity.Netcode.FastBufferWriter.ForPrimitives)) where T : unmanaged, global::System.IComparable, global::System.IConvertible, global::System.IComparable<T>, global::System.IEquatable<T>
		{
			m_Writer.WriteValueSafe(value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
		}

		public void SerializeValue<T>(ref T value, global::Unity.Netcode.FastBufferWriter.ForEnums unused = default(global::Unity.Netcode.FastBufferWriter.ForEnums)) where T : unmanaged, global::System.Enum
		{
			m_Writer.WriteValueSafe(in value, default(global::Unity.Netcode.FastBufferWriter.ForEnums));
		}

		public void SerializeValue<T>(ref T[] value, global::Unity.Netcode.FastBufferWriter.ForEnums unused = default(global::Unity.Netcode.FastBufferWriter.ForEnums)) where T : unmanaged, global::System.Enum
		{
			m_Writer.WriteValueSafe(value, default(global::Unity.Netcode.FastBufferWriter.ForEnums));
		}

		public void SerializeValue<T>(ref T value, global::Unity.Netcode.FastBufferWriter.ForStructs unused = default(global::Unity.Netcode.FastBufferWriter.ForStructs)) where T : unmanaged, global::Unity.Netcode.INetworkSerializeByMemcpy
		{
			m_Writer.WriteValueSafe(in value, default(global::Unity.Netcode.FastBufferWriter.ForStructs));
		}

		public void SerializeValue<T>(ref T[] value, global::Unity.Netcode.FastBufferWriter.ForStructs unused = default(global::Unity.Netcode.FastBufferWriter.ForStructs)) where T : unmanaged, global::Unity.Netcode.INetworkSerializeByMemcpy
		{
			m_Writer.WriteValueSafe(value, default(global::Unity.Netcode.FastBufferWriter.ForStructs));
		}

		public void SerializeValue<T>(ref global::Unity.Collections.NativeArray<T> value, global::Unity.Collections.Allocator allocator, global::Unity.Netcode.FastBufferWriter.ForGeneric unused = default(global::Unity.Netcode.FastBufferWriter.ForGeneric)) where T : unmanaged
		{
			m_Writer.WriteValueSafe(value);
		}

		public void SerializeValue<T>(ref T value, global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable unused = default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable)) where T : global::Unity.Netcode.INetworkSerializable, new()
		{
			m_Writer.WriteValue(in value, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
		}

		public void SerializeValue<T>(ref T[] value, global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable unused = default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable)) where T : global::Unity.Netcode.INetworkSerializable, new()
		{
			m_Writer.WriteValue(value, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
		}

		public void SerializeValue<T>(ref T value, global::Unity.Netcode.FastBufferWriter.ForFixedStrings unused = default(global::Unity.Netcode.FastBufferWriter.ForFixedStrings)) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			m_Writer.WriteValueSafe(in value, default(global::Unity.Netcode.FastBufferWriter.ForFixedStrings));
		}

		public void SerializeValue<T>(ref global::Unity.Collections.NativeArray<T> value, global::Unity.Collections.Allocator allocator) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			m_Writer.WriteValueSafe(in value);
		}

		public void SerializeValue(ref global::UnityEngine.Vector2 value)
		{
			m_Writer.WriteValueSafe(in value);
		}

		public void SerializeValue(ref global::UnityEngine.Vector2[] value)
		{
			m_Writer.WriteValueSafe(value);
		}

		public void SerializeValue(ref global::UnityEngine.Vector3 value)
		{
			m_Writer.WriteValueSafe(in value);
		}

		public void SerializeValue(ref global::UnityEngine.Vector3[] value)
		{
			m_Writer.WriteValueSafe(value);
		}

		public void SerializeValue(ref global::UnityEngine.Vector2Int value)
		{
			m_Writer.WriteValueSafe(in value);
		}

		public void SerializeValue(ref global::UnityEngine.Vector2Int[] value)
		{
			m_Writer.WriteValueSafe(value);
		}

		public void SerializeValue(ref global::UnityEngine.Vector3Int value)
		{
			m_Writer.WriteValueSafe(in value);
		}

		public void SerializeValue(ref global::UnityEngine.Vector3Int[] value)
		{
			m_Writer.WriteValueSafe(value);
		}

		public void SerializeValue(ref global::UnityEngine.Vector4 value)
		{
			m_Writer.WriteValueSafe(in value);
		}

		public void SerializeValue(ref global::UnityEngine.Vector4[] value)
		{
			m_Writer.WriteValueSafe(value);
		}

		public void SerializeValue(ref global::UnityEngine.Quaternion value)
		{
			m_Writer.WriteValueSafe(in value);
		}

		public void SerializeValue(ref global::UnityEngine.Quaternion[] value)
		{
			m_Writer.WriteValueSafe(value);
		}

		public void SerializeValue(ref global::UnityEngine.Pose value)
		{
			m_Writer.WriteValueSafe(in value);
		}

		public void SerializeValue(ref global::UnityEngine.Pose[] value)
		{
			m_Writer.WriteValueSafe(value);
		}

		public void SerializeValue(ref global::UnityEngine.Color value)
		{
			m_Writer.WriteValueSafe(in value);
		}

		public void SerializeValue(ref global::UnityEngine.Color[] value)
		{
			m_Writer.WriteValueSafe(value);
		}

		public void SerializeValue(ref global::UnityEngine.Color32 value)
		{
			m_Writer.WriteValueSafe(in value);
		}

		public void SerializeValue(ref global::UnityEngine.Color32[] value)
		{
			m_Writer.WriteValueSafe(value);
		}

		public void SerializeValue(ref global::UnityEngine.Ray value)
		{
			m_Writer.WriteValueSafe(in value);
		}

		public void SerializeValue(ref global::UnityEngine.Ray[] value)
		{
			m_Writer.WriteValueSafe(value);
		}

		public void SerializeValue(ref global::UnityEngine.Ray2D value)
		{
			m_Writer.WriteValueSafe(in value);
		}

		public void SerializeValue(ref global::UnityEngine.Ray2D[] value)
		{
			m_Writer.WriteValueSafe(value);
		}

		public void SerializeNetworkSerializable<T>(ref T value) where T : global::Unity.Netcode.INetworkSerializable, new()
		{
			m_Writer.WriteNetworkSerializable(in value);
		}

		public bool PreCheck(int amount)
		{
			return m_Writer.TryBeginWrite(amount);
		}

		public void SerializeValuePreChecked(ref string s, bool oneByteChars = false)
		{
			m_Writer.WriteValue(s, oneByteChars);
		}

		public void SerializeValuePreChecked(ref byte value)
		{
			m_Writer.WriteByte(value);
		}

		public void SerializeValuePreChecked<T>(ref T value, global::Unity.Netcode.FastBufferWriter.ForPrimitives unused = default(global::Unity.Netcode.FastBufferWriter.ForPrimitives)) where T : unmanaged, global::System.IComparable, global::System.IConvertible, global::System.IComparable<T>, global::System.IEquatable<T>
		{
			m_Writer.WriteValue(in value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
		}

		public void SerializeValuePreChecked<T>(ref T[] value, global::Unity.Netcode.FastBufferWriter.ForPrimitives unused = default(global::Unity.Netcode.FastBufferWriter.ForPrimitives)) where T : unmanaged, global::System.IComparable, global::System.IConvertible, global::System.IComparable<T>, global::System.IEquatable<T>
		{
			m_Writer.WriteValue(value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
		}

		public void SerializeValuePreChecked<T>(ref T value, global::Unity.Netcode.FastBufferWriter.ForEnums unused = default(global::Unity.Netcode.FastBufferWriter.ForEnums)) where T : unmanaged, global::System.Enum
		{
			m_Writer.WriteValue(in value, default(global::Unity.Netcode.FastBufferWriter.ForEnums));
		}

		public void SerializeValuePreChecked<T>(ref T[] value, global::Unity.Netcode.FastBufferWriter.ForEnums unused = default(global::Unity.Netcode.FastBufferWriter.ForEnums)) where T : unmanaged, global::System.Enum
		{
			m_Writer.WriteValue(value, default(global::Unity.Netcode.FastBufferWriter.ForEnums));
		}

		public void SerializeValuePreChecked<T>(ref T value, global::Unity.Netcode.FastBufferWriter.ForStructs unused = default(global::Unity.Netcode.FastBufferWriter.ForStructs)) where T : unmanaged, global::Unity.Netcode.INetworkSerializeByMemcpy
		{
			m_Writer.WriteValue(in value, default(global::Unity.Netcode.FastBufferWriter.ForStructs));
		}

		public void SerializeValuePreChecked<T>(ref T[] value, global::Unity.Netcode.FastBufferWriter.ForStructs unused = default(global::Unity.Netcode.FastBufferWriter.ForStructs)) where T : unmanaged, global::Unity.Netcode.INetworkSerializeByMemcpy
		{
			m_Writer.WriteValue(value, default(global::Unity.Netcode.FastBufferWriter.ForStructs));
		}

		public void SerializeValuePreChecked<T>(ref global::Unity.Collections.NativeArray<T> value, global::Unity.Collections.Allocator allocator, global::Unity.Netcode.FastBufferWriter.ForGeneric unused = default(global::Unity.Netcode.FastBufferWriter.ForGeneric)) where T : unmanaged
		{
			m_Writer.WriteValue(value);
		}

		public void SerializeValuePreChecked<T>(ref T value, global::Unity.Netcode.FastBufferWriter.ForFixedStrings unused = default(global::Unity.Netcode.FastBufferWriter.ForFixedStrings)) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			m_Writer.WriteValue(in value, default(global::Unity.Netcode.FastBufferWriter.ForFixedStrings));
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Vector2 value)
		{
			m_Writer.WriteValue(in value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Vector2[] value)
		{
			m_Writer.WriteValue(value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Vector3 value)
		{
			m_Writer.WriteValue(in value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Vector3[] value)
		{
			m_Writer.WriteValue(value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Vector2Int value)
		{
			m_Writer.WriteValue(in value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Vector2Int[] value)
		{
			m_Writer.WriteValue(value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Vector3Int value)
		{
			m_Writer.WriteValue(in value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Vector3Int[] value)
		{
			m_Writer.WriteValue(value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Vector4 value)
		{
			m_Writer.WriteValue(in value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Vector4[] value)
		{
			m_Writer.WriteValue(value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Quaternion value)
		{
			m_Writer.WriteValue(in value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Quaternion[] value)
		{
			m_Writer.WriteValue(value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Pose value)
		{
			m_Writer.WriteValue(in value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Pose[] value)
		{
			m_Writer.WriteValue(value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Color value)
		{
			m_Writer.WriteValue(in value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Color[] value)
		{
			m_Writer.WriteValue(value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Color32 value)
		{
			m_Writer.WriteValue(in value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Color32[] value)
		{
			m_Writer.WriteValue(value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Ray value)
		{
			m_Writer.WriteValue(in value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Ray[] value)
		{
			m_Writer.WriteValue(value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Ray2D value)
		{
			m_Writer.WriteValue(in value);
		}

		public void SerializeValuePreChecked(ref global::UnityEngine.Ray2D[] value)
		{
			m_Writer.WriteValue(value);
		}
	}
}
