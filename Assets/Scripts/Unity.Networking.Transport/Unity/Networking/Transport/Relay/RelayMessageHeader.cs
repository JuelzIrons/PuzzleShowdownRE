namespace Unity.Networking.Transport.Relay
{
	internal struct RelayMessageHeader
	{
		public const int k_Length = 4;

		public const ushort k_Signature = 29402;

		public const byte k_Version = 0;

		public ushort Signature;

		public byte Version;

		public global::Unity.Networking.Transport.Relay.RelayMessageType Type;

		public bool IsValid()
		{
			if (Signature == 29402)
			{
				return Version == 0;
			}
			return false;
		}

		public static global::Unity.Networking.Transport.Relay.RelayMessageHeader Create(global::Unity.Networking.Transport.Relay.RelayMessageType type)
		{
			return new global::Unity.Networking.Transport.Relay.RelayMessageHeader
			{
				Signature = 29402,
				Version = 0,
				Type = type
			};
		}

		public static void Write(ref global::Unity.Networking.Transport.PacketProcessor packetProcessor, global::Unity.Networking.Transport.Relay.RelayMessageType type)
		{
			packetProcessor.AppendToPayload((ushort)29402);
			packetProcessor.AppendToPayload((byte)0);
			packetProcessor.AppendToPayload(type);
		}
	}
}
