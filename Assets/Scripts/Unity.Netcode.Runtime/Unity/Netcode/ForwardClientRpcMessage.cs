namespace Unity.Netcode
{
	internal struct ForwardClientRpcMessage : global::Unity.Netcode.INetworkMessage
	{
		public bool BroadCast;

		public ulong[] TargetClientIds;

		public global::Unity.Netcode.NetworkDelivery NetworkDelivery;

		public global::Unity.Netcode.ClientRpcMessage ClientRpcMessage;

		public int Version => 0;

		public void Serialize(global::Unity.Netcode.FastBufferWriter writer, int targetVersion)
		{
			if (TargetClientIds == null)
			{
				BroadCast = true;
				writer.WriteValueSafe(in BroadCast, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			}
			else
			{
				BroadCast = false;
				writer.WriteValueSafe(in BroadCast, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				writer.WriteValueSafe(TargetClientIds, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			}
			writer.WriteValueSafe(in NetworkDelivery, default(global::Unity.Netcode.FastBufferWriter.ForEnums));
			ClientRpcMessage.Serialize(writer, targetVersion);
		}

		public unsafe bool Deserialize(global::Unity.Netcode.FastBufferReader reader, ref global::Unity.Netcode.NetworkContext context, int receivedMessageVersion)
		{
			reader.ReadValueSafe(out BroadCast, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			if (!BroadCast)
			{
				reader.ReadValueSafe(out TargetClientIds, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			}
			reader.ReadValueSafe(out NetworkDelivery, default(global::Unity.Netcode.FastBufferWriter.ForEnums));
			ClientRpcMessage.ReadBuffer = new global::Unity.Netcode.FastBufferReader(reader, global::Unity.Collections.Allocator.Persistent, reader.Length - reader.Position, sizeof(global::Unity.Netcode.RpcMetadata));
			if (!ClientRpcMessage.Deserialize(reader, ref context, receivedMessageVersion))
			{
				ClientRpcMessage.ReadBuffer.Dispose();
				return false;
			}
			return true;
		}

		public void Handle(ref global::Unity.Netcode.NetworkContext context)
		{
			global::Unity.Netcode.NetworkManager networkManager = (global::Unity.Netcode.NetworkManager)context.SystemOwner;
			if (networkManager.DAHost)
			{
				ClientRpcMessage.WriteBuffer = new global::Unity.Netcode.FastBufferWriter(ClientRpcMessage.ReadBuffer.Length, global::Unity.Collections.Allocator.TempJob);
				ClientRpcMessage.WriteBuffer.WriteBytesSafe(ClientRpcMessage.ReadBuffer.ToArray());
				if (BroadCast)
				{
					networkManager.ConnectionManager.SendMessage<global::Unity.Netcode.ClientRpcMessage, global::System.Collections.Generic.IReadOnlyList<ulong>>(ref ClientRpcMessage, NetworkDelivery, networkManager.ConnectedClientsIds);
				}
				else
				{
					networkManager.ConnectionManager.SendMessage(ref ClientRpcMessage, NetworkDelivery, in TargetClientIds);
				}
			}
			else
			{
				global::Unity.Netcode.NetworkLog.LogErrorServer(string.Format("Received {0} on client-{1}! Only DAHost may forward RPC messages!", "ForwardClientRpcMessage", networkManager.LocalClientId));
			}
			ClientRpcMessage.WriteBuffer.Dispose();
			ClientRpcMessage.ReadBuffer.Dispose();
		}
	}
}
