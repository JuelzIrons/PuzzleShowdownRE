namespace Unity.Networking.Transport.Relay
{
	internal static class RelayAllocationIdExtensions
	{
		public unsafe static ref global::Unity.Networking.Transport.Relay.RelayAllocationId AsRelayAllocationId(this ref global::Unity.Networking.Transport.NetworkEndpoint address)
		{
			fixed (global::Unity.Networking.Transport.NetworkEndpoint* ptr = &address)
			{
				return ref *(global::Unity.Networking.Transport.Relay.RelayAllocationId*)ptr;
			}
		}
	}
}
