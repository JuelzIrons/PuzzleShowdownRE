namespace Unity.Networking.Transport.Error
{
	public enum StatusCode
	{
		Success = 0,
		NetworkIdMismatch = -1,
		NetworkVersionMismatch = -2,
		NetworkStateMismatch = -3,
		NetworkPacketOverflow = -4,
		NetworkSendQueueFull = -5,
		[global::System.Obsolete("Return code is not in use anymore and nothing will return it.")]
		NetworkHeaderInvalid = -6,
		NetworkDriverParallelForErr = -7,
		NetworkSendHandleInvalid = -8,
		[global::System.Obsolete("Return code is not in use anymore and nothing will return it.")]
		NetworkArgumentMismatch = -9,
		NetworkReceiveQueueFull = -10,
		NetworkSocketError = -11
	}
}
