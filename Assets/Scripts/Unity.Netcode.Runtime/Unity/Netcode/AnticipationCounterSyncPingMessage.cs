namespace Unity.Netcode
{
	internal struct AnticipationCounterSyncPingMessage : global::Unity.Netcode.INetworkMessage
	{
		public ulong Counter;

		public double Time;

		public int Version => 0;

		public void Serialize(global::Unity.Netcode.FastBufferWriter writer, int targetVersion)
		{
			global::Unity.Netcode.BytePacker.WriteValuePacked(writer, Counter);
			writer.WriteValueSafe(in Time, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
		}

		public bool Deserialize(global::Unity.Netcode.FastBufferReader reader, ref global::Unity.Netcode.NetworkContext context, int receivedMessageVersion)
		{
			if (!((global::Unity.Netcode.NetworkManager)context.SystemOwner).IsServer)
			{
				return false;
			}
			global::Unity.Netcode.ByteUnpacker.ReadValuePacked(reader, out Counter);
			reader.ReadValueSafe(out Time, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			return true;
		}

		public void Handle(ref global::Unity.Netcode.NetworkContext context)
		{
			global::Unity.Netcode.NetworkManager networkManager = (global::Unity.Netcode.NetworkManager)context.SystemOwner;
			if (networkManager.IsListening && !networkManager.ShutdownInProgress && networkManager.ConnectedClients.ContainsKey(context.SenderId))
			{
				global::Unity.Netcode.AnticipationCounterSyncPongMessage message = new global::Unity.Netcode.AnticipationCounterSyncPongMessage
				{
					Counter = Counter,
					Time = Time
				};
				networkManager.MessageManager.SendMessage(ref message, global::Unity.Netcode.NetworkDelivery.Reliable, context.SenderId);
			}
		}
	}
}
