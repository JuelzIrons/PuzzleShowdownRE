namespace Unity.Networking.Transport.Error
{
	public enum DisconnectReason : byte
	{
		Default = 0,
		Timeout = 1,
		MaxConnectionAttempts = 2,
		ClosedByRemote = 3,
		AuthenticationFailure = 6,
		ProtocolError = 7,
		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		[global::System.Obsolete("Value is not in use anymore and nothing will return it.")]
		Count = 8,
		HostNotFound = 9
	}
}
