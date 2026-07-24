namespace Unity.Networking.Transport.Relay
{
	internal struct RelayMessageConnectRequest
	{
		public const int k_Length = 276;

		public global::Unity.Networking.Transport.Relay.RelayMessageHeader Header;

		public global::Unity.Networking.Transport.Relay.RelayAllocationId AllocationId;

		public byte ToConnectionDataLength;

		public global::Unity.Networking.Transport.Relay.RelayConnectionData ToConnectionData;

		public static global::Unity.Networking.Transport.Relay.RelayMessageConnectRequest Create(global::Unity.Networking.Transport.Relay.RelayAllocationId allocationId, global::Unity.Networking.Transport.Relay.RelayConnectionData toConnectionData)
		{
			return new global::Unity.Networking.Transport.Relay.RelayMessageConnectRequest
			{
				Header = global::Unity.Networking.Transport.Relay.RelayMessageHeader.Create(global::Unity.Networking.Transport.Relay.RelayMessageType.ConnectRequest),
				AllocationId = allocationId,
				ToConnectionDataLength = byte.MaxValue,
				ToConnectionData = toConnectionData
			};
		}

		public static void Write(ref global::Unity.Networking.Transport.PacketProcessor packetProcessor, ref global::Unity.Networking.Transport.Relay.RelayAllocationId allocationId, ref global::Unity.Networking.Transport.Relay.RelayConnectionData toConnectionData)
		{
			global::Unity.Networking.Transport.Relay.RelayMessageHeader.Write(ref packetProcessor, global::Unity.Networking.Transport.Relay.RelayMessageType.ConnectRequest);
			packetProcessor.AppendToPayload(allocationId);
			packetProcessor.AppendToPayload(byte.MaxValue);
			packetProcessor.AppendToPayload(toConnectionData);
		}
	}
}
