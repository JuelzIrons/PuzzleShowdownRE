namespace Unity.Netcode
{
	internal ref struct NetworkContext
	{
		public object SystemOwner;

		public ulong SenderId;

		public float Timestamp;

		public global::Unity.Netcode.NetworkMessageHeader Header;

		public int SerializedHeaderSize;

		public uint MessageSize;
	}
}
