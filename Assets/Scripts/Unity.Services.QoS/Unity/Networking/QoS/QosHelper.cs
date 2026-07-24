namespace Unity.Networking.QoS
{
	internal static class QosHelper
	{
		private const ulong WSAEWOULDBLOCK = 10035uL;

		private const ulong WSAETIMEDOUT = 10060uL;

		private const ulong EAGAIN_EWOULDBLOCK_1 = 11uL;

		private const ulong EAGAIN_EWOULDBLOCK_2 = 35uL;

		internal static bool WouldBlock(ulong errorcode)
		{
			if (errorcode != 10035 && errorcode != 10060 && errorcode != 11)
			{
				return errorcode == 35;
			}
			return true;
		}

		internal static bool ExpiredUtc(global::System.DateTime timeUtc)
		{
			return global::System.DateTime.UtcNow > timeUtc;
		}

		internal static string Since(global::System.DateTime dt)
		{
			return $"{(global::System.DateTime.UtcNow - dt).TotalMilliseconds:F0}ms";
		}
	}
}
