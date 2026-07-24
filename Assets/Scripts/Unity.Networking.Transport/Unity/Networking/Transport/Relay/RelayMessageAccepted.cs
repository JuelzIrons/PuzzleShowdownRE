namespace Unity.Networking.Transport.Relay
{
	internal struct RelayMessageAccepted
	{
		public const int k_Length = 36;

		public global::Unity.Networking.Transport.Relay.RelayMessageHeader Header;

		public global::Unity.Networking.Transport.Relay.RelayAllocationId FromAllocationId;

		public global::Unity.Networking.Transport.Relay.RelayAllocationId ToAllocationId;
	}
}
