namespace Unity.Netcode
{
	internal struct NetworkMessageHeader : global::Unity.Netcode.INetworkSerializeByMemcpy
	{
		public uint MessageType;

		public uint MessageSize;
	}
}
