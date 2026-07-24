namespace Unity.Services.Multiplayer
{
	public enum RelayProtocol : byte
	{
		Unknown = 0,
		UDP = 1,
		DTLS = 2,
		WSS = 3,
		Default = 2
	}
}
