namespace Unity.Netcode
{
	public struct ConnectionEventData
	{
		public global::Unity.Netcode.ConnectionEvent EventType;

		public ulong ClientId;

		public global::Unity.Collections.NativeArray<ulong> PeerClientIds;
	}
}
