namespace Unity.Networking.Transport.Utilities
{
	public enum ApplyMode : byte
	{
		ReceivedPacketsOnly = 0,
		SentPacketsOnly = 1,
		AllPackets = 2,
		Off = 3
	}
}
