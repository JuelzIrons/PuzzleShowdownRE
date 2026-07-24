namespace Unity.Netcode
{
	internal struct RpcMetadata : global::Unity.Netcode.INetworkSerializeByMemcpy
	{
		public ulong NetworkObjectId;

		public ushort NetworkBehaviourId;

		public uint NetworkRpcMethodId;
	}
}
