namespace Unity.Netcode
{
	public class UserNetworkVariableSerialization<T>
	{
		public delegate void WriteValueDelegate(global::Unity.Netcode.FastBufferWriter writer, in T value);

		public delegate void WriteDeltaDelegate(global::Unity.Netcode.FastBufferWriter writer, in T value, in T previousValue);

		public delegate void ReadValueDelegate(global::Unity.Netcode.FastBufferReader reader, out T value);

		public delegate void ReadDeltaDelegate(global::Unity.Netcode.FastBufferReader reader, ref T value);

		public delegate void DuplicateValueDelegate(in T value, ref T duplicatedValue);

		public static global::Unity.Netcode.UserNetworkVariableSerialization<T>.WriteValueDelegate WriteValue;

		public static global::Unity.Netcode.UserNetworkVariableSerialization<T>.ReadValueDelegate ReadValue;

		public static global::Unity.Netcode.UserNetworkVariableSerialization<T>.WriteDeltaDelegate WriteDelta;

		public static global::Unity.Netcode.UserNetworkVariableSerialization<T>.ReadDeltaDelegate ReadDelta;

		public static global::Unity.Netcode.UserNetworkVariableSerialization<T>.DuplicateValueDelegate DuplicateValue;
	}
}
