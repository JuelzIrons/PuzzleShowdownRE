namespace Unity.Networking.Transport.Relay
{
	internal struct RelayMessagePing
	{
		public const int k_Length = 22;

		public global::Unity.Networking.Transport.Relay.RelayMessageHeader Header;

		public global::Unity.Networking.Transport.Relay.RelayAllocationId FromAllocationId;

		public ushort SequenceNumber;

		public static global::Unity.Networking.Transport.Relay.RelayMessagePing Create(global::Unity.Networking.Transport.Relay.RelayAllocationId fromAllocationId)
		{
			return new global::Unity.Networking.Transport.Relay.RelayMessagePing
			{
				Header = global::Unity.Networking.Transport.Relay.RelayMessageHeader.Create(global::Unity.Networking.Transport.Relay.RelayMessageType.Ping),
				FromAllocationId = fromAllocationId,
				SequenceNumber = 1
			};
		}

		public static void Write(ref global::Unity.Networking.Transport.PacketProcessor packetProcessor, ref global::Unity.Networking.Transport.Relay.RelayAllocationId fromAllocationId)
		{
			global::Unity.Networking.Transport.Relay.RelayMessageHeader.Write(ref packetProcessor, global::Unity.Networking.Transport.Relay.RelayMessageType.Ping);
			packetProcessor.AppendToPayload(fromAllocationId);
			packetProcessor.AppendToPayload((ushort)1);
		}
	}
}
