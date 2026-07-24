namespace Steamworks
{
	[global::System.Serializable]
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1)]
	public struct SteamNetworkingIPAddr : global::System.IEquatable<global::Steamworks.SteamNetworkingIPAddr>
	{
		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 16)]
		public byte[] m_ipv6;

		public ushort m_port;

		public const int k_cchMaxString = 48;

		public void Clear()
		{
			global::Steamworks.NativeMethods.SteamAPI_SteamNetworkingIPAddr_Clear(ref this);
		}

		public bool IsIPv6AllZeros()
		{
			return global::Steamworks.NativeMethods.SteamAPI_SteamNetworkingIPAddr_IsIPv6AllZeros(ref this);
		}

		public void SetIPv6(byte[] ipv6, ushort nPort)
		{
			global::Steamworks.NativeMethods.SteamAPI_SteamNetworkingIPAddr_SetIPv6(ref this, ipv6, nPort);
		}

		public void SetIPv4(uint nIP, ushort nPort)
		{
			global::Steamworks.NativeMethods.SteamAPI_SteamNetworkingIPAddr_SetIPv4(ref this, nIP, nPort);
		}

		public bool IsIPv4()
		{
			return global::Steamworks.NativeMethods.SteamAPI_SteamNetworkingIPAddr_IsIPv4(ref this);
		}

		public uint GetIPv4()
		{
			return global::Steamworks.NativeMethods.SteamAPI_SteamNetworkingIPAddr_GetIPv4(ref this);
		}

		public void SetIPv6LocalHost(ushort nPort = 0)
		{
			global::Steamworks.NativeMethods.SteamAPI_SteamNetworkingIPAddr_SetIPv6LocalHost(ref this, nPort);
		}

		public bool IsLocalHost()
		{
			return global::Steamworks.NativeMethods.SteamAPI_SteamNetworkingIPAddr_IsLocalHost(ref this);
		}

		public void ToString(out string buf, bool bWithPort)
		{
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(48);
			global::Steamworks.NativeMethods.SteamAPI_SteamNetworkingIPAddr_ToString(ref this, intPtr, 48u, bWithPort);
			buf = global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
		}

		public bool ParseString(string pszStr)
		{
			using global::Steamworks.InteropHelp.UTF8StringHandle pszStr2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pszStr);
			return global::Steamworks.NativeMethods.SteamAPI_SteamNetworkingIPAddr_ParseString(ref this, pszStr2);
		}

		public bool Equals(global::Steamworks.SteamNetworkingIPAddr x)
		{
			return global::Steamworks.NativeMethods.SteamAPI_SteamNetworkingIPAddr_IsEqualTo(ref this, ref x);
		}

		public global::Steamworks.ESteamNetworkingFakeIPType GetFakeIPType()
		{
			return global::Steamworks.NativeMethods.SteamAPI_SteamNetworkingIPAddr_GetFakeIPType(ref this);
		}

		public bool IsFakeIP()
		{
			return GetFakeIPType() > global::Steamworks.ESteamNetworkingFakeIPType.k_ESteamNetworkingFakeIPType_NotFake;
		}
	}
}
