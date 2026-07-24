namespace Unity.Netcode
{
	internal struct NetworkBatchHeader : global::Unity.Netcode.INetworkSerializeByMemcpy
	{
		internal const ushort MagicValue = 4448;

		public ushort Magic;

		public ushort BatchCount;

		public int BatchSize;

		public ulong BatchHash;
	}
}
