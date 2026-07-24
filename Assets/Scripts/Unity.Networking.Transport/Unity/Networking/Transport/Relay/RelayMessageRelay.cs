namespace Unity.Networking.Transport.Relay
{
	internal struct RelayMessageRelay
	{
		public const int k_Length = 38;

		public global::Unity.Networking.Transport.Relay.RelayMessageHeader Header;

		public global::Unity.Networking.Transport.Relay.RelayAllocationId FromAllocationId;

		public global::Unity.Networking.Transport.Relay.RelayAllocationId ToAllocationId;

		private ushort m_DataLength;

		public ushort DataLength
		{
			get
			{
				return SwitchEndianness(m_DataLength);
			}
			set
			{
				m_DataLength = SwitchEndianness(value);
			}
		}

		internal static ushort SwitchEndianness(ushort value)
		{
			if (global::Unity.Collections.DataStreamWriter.IsLittleEndian)
			{
				return (ushort)((value << 8) | (value >> 8));
			}
			return value;
		}

		public static global::Unity.Networking.Transport.Relay.RelayMessageRelay Create(global::Unity.Networking.Transport.Relay.RelayAllocationId fromAllocationId, global::Unity.Networking.Transport.Relay.RelayAllocationId toAllocationId, ushort dataLength)
		{
			return new global::Unity.Networking.Transport.Relay.RelayMessageRelay
			{
				Header = global::Unity.Networking.Transport.Relay.RelayMessageHeader.Create(global::Unity.Networking.Transport.Relay.RelayMessageType.Relay),
				FromAllocationId = fromAllocationId,
				ToAllocationId = toAllocationId,
				DataLength = dataLength
			};
		}

		public static void Write(ref global::Unity.Networking.Transport.PacketProcessor packetProcessor, ref global::Unity.Networking.Transport.Relay.RelayAllocationId fromAllocationId, ref global::Unity.Networking.Transport.Relay.RelayAllocationId toAllocationId, ushort dataLength)
		{
			packetProcessor.PrependToPayload(SwitchEndianness(dataLength));
			packetProcessor.PrependToPayload(toAllocationId);
			packetProcessor.PrependToPayload(fromAllocationId);
			packetProcessor.PrependToPayload(global::Unity.Networking.Transport.Relay.RelayMessageHeader.Create(global::Unity.Networking.Transport.Relay.RelayMessageType.Relay));
		}
	}
}
