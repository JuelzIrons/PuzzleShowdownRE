namespace Unity.Netcode
{
	public class RpcFallbackSerialization
	{
		public static void Write<T>(global::Unity.Netcode.FastBufferWriter writer, ref T value)
		{
			global::Unity.Netcode.NetworkVariableSerialization<T>.Write(writer, ref value);
		}

		public static void Read<T>(global::Unity.Netcode.FastBufferReader reader, ref T value)
		{
			global::Unity.Netcode.NetworkVariableSerialization<T>.Read(reader, ref value);
		}
	}
}
