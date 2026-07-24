namespace Unity.Netcode
{
	internal struct AnticipationCounterSyncPongMessage : global::Unity.Netcode.INetworkMessage
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
			if (!((global::Unity.Netcode.NetworkManager)context.SystemOwner).IsClient)
			{
				return false;
			}
			global::Unity.Netcode.ByteUnpacker.ReadValuePacked(reader, out Counter);
			reader.ReadValueSafe(out Time, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			return true;
		}

		public void Handle(ref global::Unity.Netcode.NetworkContext context)
		{
			global::Unity.Netcode.NetworkManager obj = (global::Unity.Netcode.NetworkManager)context.SystemOwner;
			obj.AnticipationSystem.LastAnticipationAck = Counter;
			obj.AnticipationSystem.LastAnticipationAckTime = Time;
		}
	}
}
