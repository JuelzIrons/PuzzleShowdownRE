namespace Unity.Netcode
{
	internal struct ServerRpcMessage : global::Unity.Netcode.INetworkMessage
	{
		public global::Unity.Netcode.RpcMetadata Metadata;

		public global::Unity.Netcode.FastBufferWriter WriteBuffer;

		public global::Unity.Netcode.FastBufferReader ReadBuffer;

		private const string k_Name = "ServerRpcMessage";

		public int Version => 0;

		public void Serialize(global::Unity.Netcode.FastBufferWriter writer, int targetVersion)
		{
			global::Unity.Netcode.RpcMessageHelpers.Serialize(ref writer, ref Metadata, ref WriteBuffer);
		}

		public bool Deserialize(global::Unity.Netcode.FastBufferReader reader, ref global::Unity.Netcode.NetworkContext context, int receivedMessageVersion)
		{
			return global::Unity.Netcode.RpcMessageHelpers.Deserialize(ref reader, ref context, ref Metadata, ref ReadBuffer, "ServerRpcMessage");
		}

		public void Handle(ref global::Unity.Netcode.NetworkContext context)
		{
			global::Unity.Netcode.__RpcParams rpcParams = new global::Unity.Netcode.__RpcParams
			{
				SenderId = context.SenderId,
				Server = new global::Unity.Netcode.ServerRpcParams
				{
					Receive = new global::Unity.Netcode.ServerRpcReceiveParams
					{
						SenderClientId = context.SenderId
					}
				}
			};
			global::Unity.Netcode.RpcMessageHelpers.Handle(ref context, ref Metadata, ref ReadBuffer, ref rpcParams);
		}
	}
}
