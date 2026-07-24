namespace Unity.Services.Wire.Internal
{
	public enum WireErrorCode
	{
		Unknown = 23000,
		CommandFailed = 23002,
		ConnectionFailed = 23003,
		InvalidToken = 23004,
		InvalidChannelName = 23005,
		TokenRetrieverFailed = 23006,
		Unauthorized = 23007,
		AlreadySubscribed = 23008,
		AlreadyUnsubscribed = 23009
	}
}
