namespace Unity.Networking.Transport.Relay
{
	internal struct RelayMessageDisconnect
	{
		public const int k_Length = 36;

		public global::Unity.Networking.Transport.Relay.RelayMessageHeader Header;

		public global::Unity.Networking.Transport.Relay.RelayAllocationId FromAllocationId;

		public global::Unity.Networking.Transport.Relay.RelayAllocationId ToAllocationId;

		public static global::Unity.Networking.Transport.Relay.RelayMessageDisconnect Create(global::Unity.Networking.Transport.Relay.RelayAllocationId fromAllocationId, global::Unity.Networking.Transport.Relay.RelayAllocationId toAllocationId)
		{
			return new global::Unity.Networking.Transport.Relay.RelayMessageDisconnect
			{
				Header = global::Unity.Networking.Transport.Relay.RelayMessageHeader.Create(global::Unity.Networking.Transport.Relay.RelayMessageType.Disconnect),
				FromAllocationId = fromAllocationId,
				ToAllocationId = toAllocationId
			};
		}

		public static void Write(ref global::Unity.Networking.Transport.PacketProcessor packetProcessor, ref global::Unity.Networking.Transport.Relay.RelayAllocationId fromAllocationId, ref global::Unity.Networking.Transport.Relay.RelayAllocationId toAllocationId)
		{
			global::Unity.Networking.Transport.Relay.RelayMessageHeader.Write(ref packetProcessor, global::Unity.Networking.Transport.Relay.RelayMessageType.Disconnect);
			packetProcessor.AppendToPayload(fromAllocationId);
			packetProcessor.AppendToPayload(toAllocationId);
		}
	}
}
