namespace Unity.Netcode
{
	public interface IReaderWriter
	{
		bool IsReader { get; }

		bool IsWriter { get; }

		global::Unity.Netcode.FastBufferReader GetFastBufferReader();

		global::Unity.Netcode.FastBufferWriter GetFastBufferWriter();

		void SerializeValue(ref string s, bool oneByteChars = false);

		void SerializeValue(ref byte value);

		void SerializeValue<T>(ref T value, global::Unity.Netcode.FastBufferWriter.ForPrimitives unused = default(global::Unity.Netcode.FastBufferWriter.ForPrimitives)) where T : unmanaged, global::System.IComparable, global::System.IConvertible, global::System.IComparable<T>, global::System.IEquatable<T>;

		void SerializeValue<T>(ref T[] value, global::Unity.Netcode.FastBufferWriter.ForPrimitives unused = default(global::Unity.Netcode.FastBufferWriter.ForPrimitives)) where T : unmanaged, global::System.IComparable, global::System.IConvertible, global::System.IComparable<T>, global::System.IEquatable<T>;

		void SerializeValue<T>(ref T value, global::Unity.Netcode.FastBufferWriter.ForEnums unused = default(global::Unity.Netcode.FastBufferWriter.ForEnums)) where T : unmanaged, global::System.Enum;

		void SerializeValue<T>(ref T[] value, global::Unity.Netcode.FastBufferWriter.ForEnums unused = default(global::Unity.Netcode.FastBufferWriter.ForEnums)) where T : unmanaged, global::System.Enum;

		void SerializeValue<T>(ref T value, global::Unity.Netcode.FastBufferWriter.ForStructs unused = default(global::Unity.Netcode.FastBufferWriter.ForStructs)) where T : unmanaged, global::Unity.Netcode.INetworkSerializeByMemcpy;

		void SerializeValue<T>(ref T[] value, global::Unity.Netcode.FastBufferWriter.ForStructs unused = default(global::Unity.Netcode.FastBufferWriter.ForStructs)) where T : unmanaged, global::Unity.Netcode.INetworkSerializeByMemcpy;

		void SerializeValue<T>(ref global::Unity.Collections.NativeArray<T> value, global::Unity.Collections.Allocator allocator, global::Unity.Netcode.FastBufferWriter.ForGeneric unused = default(global::Unity.Netcode.FastBufferWriter.ForGeneric)) where T : unmanaged;

		void SerializeValue<T>(ref T value, global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable unused = default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable)) where T : global::Unity.Netcode.INetworkSerializable, new();

		void SerializeValue<T>(ref T[] value, global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable unused = default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable)) where T : global::Unity.Netcode.INetworkSerializable, new();

		void SerializeValue<T>(ref T value, global::Unity.Netcode.FastBufferWriter.ForFixedStrings unused = default(global::Unity.Netcode.FastBufferWriter.ForFixedStrings)) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes;

		void SerializeValue<T>(ref global::Unity.Collections.NativeArray<T> value, global::Unity.Collections.Allocator allocator) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes;

		void SerializeValue(ref global::UnityEngine.Vector2 value);

		void SerializeValue(ref global::UnityEngine.Vector2[] value);

		void SerializeValue(ref global::UnityEngine.Vector3 value);

		void SerializeValue(ref global::UnityEngine.Vector3[] value);

		void SerializeValue(ref global::UnityEngine.Vector2Int value);

		void SerializeValue(ref global::UnityEngine.Vector2Int[] value);

		void SerializeValue(ref global::UnityEngine.Vector3Int value);

		void SerializeValue(ref global::UnityEngine.Vector3Int[] value);

		void SerializeValue(ref global::UnityEngine.Vector4 value);

		void SerializeValue(ref global::UnityEngine.Vector4[] value);

		void SerializeValue(ref global::UnityEngine.Quaternion value);

		void SerializeValue(ref global::UnityEngine.Quaternion[] value);

		void SerializeValue(ref global::UnityEngine.Pose value);

		void SerializeValue(ref global::UnityEngine.Pose[] value);

		void SerializeValue(ref global::UnityEngine.Color value);

		void SerializeValue(ref global::UnityEngine.Color[] value);

		void SerializeValue(ref global::UnityEngine.Color32 value);

		void SerializeValue(ref global::UnityEngine.Color32[] value);

		void SerializeValue(ref global::UnityEngine.Ray value);

		void SerializeValue(ref global::UnityEngine.Ray[] value);

		void SerializeValue(ref global::UnityEngine.Ray2D value);

		void SerializeValue(ref global::UnityEngine.Ray2D[] value);

		void SerializeNetworkSerializable<T>(ref T value) where T : global::Unity.Netcode.INetworkSerializable, new();

		bool PreCheck(int amount);

		void SerializeValuePreChecked(ref string s, bool oneByteChars = false);

		void SerializeValuePreChecked(ref byte value);

		void SerializeValuePreChecked<T>(ref T value, global::Unity.Netcode.FastBufferWriter.ForPrimitives unused = default(global::Unity.Netcode.FastBufferWriter.ForPrimitives)) where T : unmanaged, global::System.IComparable, global::System.IConvertible, global::System.IComparable<T>, global::System.IEquatable<T>;

		void SerializeValuePreChecked<T>(ref T[] value, global::Unity.Netcode.FastBufferWriter.ForPrimitives unused = default(global::Unity.Netcode.FastBufferWriter.ForPrimitives)) where T : unmanaged, global::System.IComparable, global::System.IConvertible, global::System.IComparable<T>, global::System.IEquatable<T>;

		void SerializeValuePreChecked<T>(ref T value, global::Unity.Netcode.FastBufferWriter.ForEnums unused = default(global::Unity.Netcode.FastBufferWriter.ForEnums)) where T : unmanaged, global::System.Enum;

		void SerializeValuePreChecked<T>(ref T[] value, global::Unity.Netcode.FastBufferWriter.ForEnums unused = default(global::Unity.Netcode.FastBufferWriter.ForEnums)) where T : unmanaged, global::System.Enum;

		void SerializeValuePreChecked<T>(ref T value, global::Unity.Netcode.FastBufferWriter.ForStructs unused = default(global::Unity.Netcode.FastBufferWriter.ForStructs)) where T : unmanaged, global::Unity.Netcode.INetworkSerializeByMemcpy;

		void SerializeValuePreChecked<T>(ref T[] value, global::Unity.Netcode.FastBufferWriter.ForStructs unused = default(global::Unity.Netcode.FastBufferWriter.ForStructs)) where T : unmanaged, global::Unity.Netcode.INetworkSerializeByMemcpy;

		void SerializeValuePreChecked<T>(ref global::Unity.Collections.NativeArray<T> value, global::Unity.Collections.Allocator allocator, global::Unity.Netcode.FastBufferWriter.ForGeneric unused = default(global::Unity.Netcode.FastBufferWriter.ForGeneric)) where T : unmanaged;

		void SerializeValuePreChecked<T>(ref T value, global::Unity.Netcode.FastBufferWriter.ForFixedStrings unused = default(global::Unity.Netcode.FastBufferWriter.ForFixedStrings)) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes;

		void SerializeValuePreChecked(ref global::UnityEngine.Vector2 value);

		void SerializeValuePreChecked(ref global::UnityEngine.Vector2[] value);

		void SerializeValuePreChecked(ref global::UnityEngine.Vector3 value);

		void SerializeValuePreChecked(ref global::UnityEngine.Vector3[] value);

		void SerializeValuePreChecked(ref global::UnityEngine.Vector2Int value);

		void SerializeValuePreChecked(ref global::UnityEngine.Vector2Int[] value);

		void SerializeValuePreChecked(ref global::UnityEngine.Vector3Int value);

		void SerializeValuePreChecked(ref global::UnityEngine.Vector3Int[] value);

		void SerializeValuePreChecked(ref global::UnityEngine.Vector4 value);

		void SerializeValuePreChecked(ref global::UnityEngine.Vector4[] value);

		void SerializeValuePreChecked(ref global::UnityEngine.Quaternion value);

		void SerializeValuePreChecked(ref global::UnityEngine.Quaternion[] value);

		void SerializeValuePreChecked(ref global::UnityEngine.Pose value);

		void SerializeValuePreChecked(ref global::UnityEngine.Pose[] value);

		void SerializeValuePreChecked(ref global::UnityEngine.Color value);

		void SerializeValuePreChecked(ref global::UnityEngine.Color[] value);

		void SerializeValuePreChecked(ref global::UnityEngine.Color32 value);

		void SerializeValuePreChecked(ref global::UnityEngine.Color32[] value);

		void SerializeValuePreChecked(ref global::UnityEngine.Ray value);

		void SerializeValuePreChecked(ref global::UnityEngine.Ray[] value);

		void SerializeValuePreChecked(ref global::UnityEngine.Ray2D value);

		void SerializeValuePreChecked(ref global::UnityEngine.Ray2D[] value);
	}
}
