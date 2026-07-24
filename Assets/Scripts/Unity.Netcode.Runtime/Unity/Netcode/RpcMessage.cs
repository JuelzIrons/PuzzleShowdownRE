namespace Unity.Netcode
{
	internal struct RpcMessage : global::Unity.Netcode.INetworkMessage
	{
		public global::Unity.Netcode.RpcMetadata Metadata;

		public ulong SenderClientId;

		public global::Unity.Netcode.FastBufferWriter WriteBuffer;

		public global::Unity.Netcode.FastBufferReader ReadBuffer;

		private const string k_Name = "RpcMessage";

		public int Version => 0;

		public void Serialize(global::Unity.Netcode.FastBufferWriter writer, int targetVersion)
		{
			global::Unity.Netcode.BytePacker.WriteValuePacked(writer, SenderClientId);
			global::Unity.Netcode.RpcMessageHelpers.Serialize(ref writer, ref Metadata, ref WriteBuffer);
		}

		public bool Deserialize(global::Unity.Netcode.FastBufferReader reader, ref global::Unity.Netcode.NetworkContext context, int receivedMessageVersion)
		{
			global::Unity.Netcode.ByteUnpacker.ReadValuePacked(reader, out SenderClientId);
			return global::Unity.Netcode.RpcMessageHelpers.Deserialize(ref reader, ref context, ref Metadata, ref ReadBuffer, "RpcMessage");
		}

		public void Handle(ref global::Unity.Netcode.NetworkContext context)
		{
			ulong num = (((global::Unity.Netcode.NetworkManager)context.SystemOwner).IsServer ? context.SenderId : SenderClientId);
			global::Unity.Netcode.__RpcParams rpcParams = new global::Unity.Netcode.__RpcParams
			{
				SenderId = num,
				Ext = new global::Unity.Netcode.RpcParams
				{
					Receive = new global::Unity.Netcode.RpcReceiveParams
					{
						SenderClientId = num
					}
				}
			};
			global::Unity.Netcode.RpcMessageHelpers.Handle(ref context, ref Metadata, ref ReadBuffer, ref rpcParams);
		}
	}
}
