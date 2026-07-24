namespace Unity.Netcode
{
	internal struct ForwardServerRpcMessage : global::Unity.Netcode.INetworkMessage
	{
		public ulong OwnerId;

		public global::Unity.Netcode.NetworkDelivery NetworkDelivery;

		public global::Unity.Netcode.ServerRpcMessage ServerRpcMessage;

		public int Version => 0;

		public void Serialize(global::Unity.Netcode.FastBufferWriter writer, int targetVersion)
		{
			writer.WriteValueSafe(in OwnerId, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			writer.WriteValueSafe(in NetworkDelivery, default(global::Unity.Netcode.FastBufferWriter.ForEnums));
			ServerRpcMessage.Serialize(writer, targetVersion);
		}

		public unsafe bool Deserialize(global::Unity.Netcode.FastBufferReader reader, ref global::Unity.Netcode.NetworkContext context, int receivedMessageVersion)
		{
			reader.ReadValueSafe(out OwnerId, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			reader.ReadValueSafe(out NetworkDelivery, default(global::Unity.Netcode.FastBufferWriter.ForEnums));
			ServerRpcMessage.ReadBuffer = new global::Unity.Netcode.FastBufferReader(reader, global::Unity.Collections.Allocator.Persistent, reader.Length - reader.Position, sizeof(global::Unity.Netcode.RpcMetadata));
			if (!ServerRpcMessage.Deserialize(reader, ref context, receivedMessageVersion))
			{
				ServerRpcMessage.ReadBuffer.Dispose();
				return false;
			}
			return true;
		}

		public void Handle(ref global::Unity.Netcode.NetworkContext context)
		{
			global::Unity.Netcode.NetworkManager networkManager = (global::Unity.Netcode.NetworkManager)context.SystemOwner;
			if (networkManager.DAHost)
			{
				try
				{
					ServerRpcMessage.WriteBuffer = new global::Unity.Netcode.FastBufferWriter(ServerRpcMessage.ReadBuffer.Length, global::Unity.Collections.Allocator.TempJob);
					ServerRpcMessage.WriteBuffer.WriteBytesSafe(ServerRpcMessage.ReadBuffer.ToArray());
					networkManager.ConnectionManager.SendMessage(ref ServerRpcMessage, NetworkDelivery, OwnerId);
				}
				catch (global::System.Exception exception)
				{
					global::UnityEngine.Debug.LogException(exception);
				}
			}
			else
			{
				global::Unity.Netcode.NetworkLog.LogErrorServer(string.Format("Received {0} on client-{1}! Only DAHost may forward RPC messages!", "ForwardServerRpcMessage", networkManager.LocalClientId));
			}
			ServerRpcMessage.ReadBuffer.Dispose();
			ServerRpcMessage.WriteBuffer.Dispose();
		}
	}
}
