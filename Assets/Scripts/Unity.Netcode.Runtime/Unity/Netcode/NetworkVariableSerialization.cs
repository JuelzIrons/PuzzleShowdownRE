namespace Unity.Netcode
{
	[global::System.Serializable]
	public static class NetworkVariableSerialization<T>
	{
		public delegate bool EqualsDelegate(ref T a, ref T b);

		internal static global::Unity.Netcode.INetworkVariableSerializer<T> Serializer = new global::Unity.Netcode.FallbackSerializer<T>();

		public static global::Unity.Netcode.NetworkVariableSerialization<T>.EqualsDelegate AreEqual { get; internal set; }

		public static void Write(global::Unity.Netcode.FastBufferWriter writer, ref T value)
		{
			Serializer.Write(writer, ref value);
		}

		public static void Read(global::Unity.Netcode.FastBufferReader reader, ref T value)
		{
			Serializer.Read(reader, ref value);
		}

		public static void WriteDelta(global::Unity.Netcode.FastBufferWriter writer, ref T value, ref T previousValue)
		{
			Serializer.WriteDelta(writer, ref value, ref previousValue);
		}

		public static void ReadDelta(global::Unity.Netcode.FastBufferReader reader, ref T value)
		{
			Serializer.ReadDelta(reader, ref value);
		}

		public static void Duplicate(in T value, ref T duplicatedValue)
		{
			Serializer.Duplicate(in value, ref duplicatedValue);
		}
	}
}
