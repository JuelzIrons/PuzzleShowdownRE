namespace Steamworks
{
	[global::System.Serializable]
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1)]
	public struct SteamIPAddress_t
	{
		private long m_ip0;

		private long m_ip1;

		private global::Steamworks.ESteamIPType m_eType;

		public SteamIPAddress_t(global::System.Net.IPAddress iPAddress)
		{
			byte[] addressBytes = iPAddress.GetAddressBytes();
			switch (iPAddress.AddressFamily)
			{
			case global::System.Net.Sockets.AddressFamily.InterNetwork:
				if (addressBytes.Length != 4)
				{
					throw new global::System.TypeInitializationException("SteamIPAddress_t: Unexpected byte length for Ipv4." + addressBytes.Length, null);
				}
				m_ip0 = (addressBytes[0] << 24) | (addressBytes[1] << 16) | (addressBytes[2] << 8) | addressBytes[3];
				m_ip1 = 0L;
				m_eType = global::Steamworks.ESteamIPType.k_ESteamIPTypeIPv4;
				break;
			case global::System.Net.Sockets.AddressFamily.InterNetworkV6:
				if (addressBytes.Length != 16)
				{
					throw new global::System.TypeInitializationException("SteamIPAddress_t: Unexpected byte length for Ipv6: " + addressBytes.Length, null);
				}
				m_ip0 = (addressBytes[1] << 24) | (addressBytes[0] << 16) | (addressBytes[3] << 8) | addressBytes[2] | (addressBytes[5] << 24) | (addressBytes[4] << 16) | (addressBytes[7] << 8) | addressBytes[6];
				m_ip1 = (addressBytes[9] << 24) | (addressBytes[8] << 16) | (addressBytes[11] << 8) | addressBytes[10] | (addressBytes[13] << 24) | (addressBytes[12] << 16) | (addressBytes[15] << 8) | addressBytes[14];
				m_eType = global::Steamworks.ESteamIPType.k_ESteamIPTypeIPv6;
				break;
			default:
				throw new global::System.TypeInitializationException("SteamIPAddress_t: Unexpected address family " + iPAddress.AddressFamily, null);
			}
		}

		public global::System.Net.IPAddress ToIPAddress()
		{
			if (m_eType == global::Steamworks.ESteamIPType.k_ESteamIPTypeIPv4)
			{
				byte[] bytes = global::System.BitConverter.GetBytes(m_ip0);
				return new global::System.Net.IPAddress(new byte[4]
				{
					bytes[3],
					bytes[2],
					bytes[1],
					bytes[0]
				});
			}
			byte[] array = new byte[16];
			global::System.BitConverter.GetBytes(m_ip0).CopyTo(array, 0);
			global::System.BitConverter.GetBytes(m_ip1).CopyTo(array, 8);
			return new global::System.Net.IPAddress(array);
		}

		public override string ToString()
		{
			return ToIPAddress().ToString();
		}

		public global::Steamworks.ESteamIPType GetIPType()
		{
			return m_eType;
		}

		public bool IsSet()
		{
			if (m_ip0 == 0L)
			{
				return m_ip1 != 0;
			}
			return true;
		}
	}
}
