namespace Unity.Netcode.Transports.UTP
{
	public static class ErrorUtilities
	{
		public static string ErrorToString(global::Unity.Networking.Transport.Error.StatusCode error, ulong connectionId)
		{
			return ErrorToFixedString((int)error).ToString();
		}

		internal static global::Unity.Collections.FixedString128Bytes ErrorToFixedString(int error)
		{
			switch ((global::Unity.Networking.Transport.Error.StatusCode)error)
			{
			case global::Unity.Networking.Transport.Error.StatusCode.NetworkStateMismatch:
			case global::Unity.Networking.Transport.Error.StatusCode.NetworkVersionMismatch:
				return "invalid connection state (likely stale/closed connection)";
			case global::Unity.Networking.Transport.Error.StatusCode.NetworkPacketOverflow:
				return "packet is too large for the transport (likely need to increase MTU)";
			case global::Unity.Networking.Transport.Error.StatusCode.NetworkSendQueueFull:
				return "send queue full (need to increase 'Max Packet Queue Size' parameter)";
			default:
				return global::Unity.Collections.FixedString.Format("unexpected error code {0}", error);
			}
		}
	}
}
