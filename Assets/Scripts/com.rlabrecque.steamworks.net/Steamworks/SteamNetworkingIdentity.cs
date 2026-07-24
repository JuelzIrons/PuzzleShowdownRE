namespace Steamworks
{
	[global::System.Serializable]
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1)]
	public struct SteamNetworkingIdentity : global::System.IEquatable<global::Steamworks.SteamNetworkingIdentity>
	{
		public global::Steamworks.ESteamNetworkingIdentityType m_eType;

		private int m_cbSize;

		private uint m_reserved0;

		private uint m_reserved1;

		private uint m_reserved2;

		private uint m_reserved3;

		private uint m_reserved4;

		private uint m_reserved5;

		private uint m_reserved6;

		private uint m_reserved7;

		private uint m_reserved8;

		private uint m_reserved9;

		private uint m_reserved10;

		private uint m_reserved11;

		private uint m_reserved12;

		private uint m_reserved13;

		private uint m_reserved14;

		private uint m_reserved15;

		private uint m_reserved16;

		private uint m_reserved17;

		private uint m_reserved18;

		private uint m_reserved19;

		private uint m_reserved20;

		private uint m_reserved21;

		private uint m_reserved22;

		private uint m_reserved23;

		private uint m_reserved24;

		private uint m_reserved25;

		private uint m_reserved26;

		private uint m_reserved27;

		private uint m_reserved28;

		private uint m_reserved29;

		private uint m_reserved30;

		private uint m_reserved31;

		public const int k_cchMaxString = 128;

		public const int k_cchMaxGenericString = 32;

		public const int k_cchMaxXboxPairwiseID = 33;

		public const int k_cbMaxGenericBytes = 32;

		public void Clear()
		{
			global::Steamworks.NativeMethods.SteamAPI_SteamNetworkingIdentity_Clear(ref this);
		}

		public bool IsInvalid()
		{
			return global::Steamworks.NativeMethods.SteamAPI_SteamNetworkingIdentity_IsInvalid(ref this);
		}

		public void SetSteamID(global::Steamworks.CSteamID steamID)
		{
			global::Steamworks.NativeMethods.SteamAPI_SteamNetworkingIdentity_SetSteamID(ref this, (ulong)steamID);
		}

		public global::Steamworks.CSteamID GetSteamID()
		{
			return (global::Steamworks.CSteamID)global::Steamworks.NativeMethods.SteamAPI_SteamNetworkingIdentity_GetSteamID(ref this);
		}

		public void SetSteamID64(ulong steamID)
		{
			global::Steamworks.NativeMethods.SteamAPI_SteamNetworkingIdentity_SetSteamID64(ref this, steamID);
		}

		public ulong GetSteamID64()
		{
			return global::Steamworks.NativeMethods.SteamAPI_SteamNetworkingIdentity_GetSteamID64(ref this);
		}

		public bool SetXboxPairwiseID(string pszString)
		{
			using global::Steamworks.InteropHelp.UTF8StringHandle pszString2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pszString);
			return global::Steamworks.NativeMethods.SteamAPI_SteamNetworkingIdentity_SetXboxPairwiseID(ref this, pszString2);
		}

		public string GetXboxPairwiseID()
		{
			return global::Steamworks.InteropHelp.PtrToStringUTF8(global::Steamworks.NativeMethods.SteamAPI_SteamNetworkingIdentity_GetXboxPairwiseID(ref this));
		}

		public void SetPSNID(ulong id)
		{
			global::Steamworks.NativeMethods.SteamAPI_SteamNetworkingIdentity_SetPSNID(ref this, id);
		}

		public ulong GetPSNID()
		{
			return global::Steamworks.NativeMethods.SteamAPI_SteamNetworkingIdentity_GetPSNID(ref this);
		}

		public void SetIPAddr(global::Steamworks.SteamNetworkingIPAddr addr)
		{
			global::Steamworks.NativeMethods.SteamAPI_SteamNetworkingIdentity_SetIPAddr(ref this, ref addr);
		}

		public global::Steamworks.SteamNetworkingIPAddr GetIPAddr()
		{
			throw new global::System.NotImplementedException();
		}

		public void SetIPv4Addr(uint nIPv4, ushort nPort)
		{
			global::Steamworks.NativeMethods.SteamAPI_SteamNetworkingIdentity_SetIPv4Addr(ref this, nIPv4, nPort);
		}

		public uint GetIPv4()
		{
			return global::Steamworks.NativeMethods.SteamAPI_SteamNetworkingIdentity_GetIPv4(ref this);
		}

		public global::Steamworks.ESteamNetworkingFakeIPType GetFakeIPType()
		{
			return global::Steamworks.NativeMethods.SteamAPI_SteamNetworkingIdentity_GetFakeIPType(ref this);
		}

		public bool IsFakeIP()
		{
			return GetFakeIPType() > global::Steamworks.ESteamNetworkingFakeIPType.k_ESteamNetworkingFakeIPType_NotFake;
		}

		public void SetLocalHost()
		{
			global::Steamworks.NativeMethods.SteamAPI_SteamNetworkingIdentity_SetLocalHost(ref this);
		}

		public bool IsLocalHost()
		{
			return global::Steamworks.NativeMethods.SteamAPI_SteamNetworkingIdentity_IsLocalHost(ref this);
		}

		public bool SetGenericString(string pszString)
		{
			using global::Steamworks.InteropHelp.UTF8StringHandle pszString2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pszString);
			return global::Steamworks.NativeMethods.SteamAPI_SteamNetworkingIdentity_SetGenericString(ref this, pszString2);
		}

		public string GetGenericString()
		{
			return global::Steamworks.InteropHelp.PtrToStringUTF8(global::Steamworks.NativeMethods.SteamAPI_SteamNetworkingIdentity_GetGenericString(ref this));
		}

		public bool SetGenericBytes(byte[] data, uint cbLen)
		{
			return global::Steamworks.NativeMethods.SteamAPI_SteamNetworkingIdentity_SetGenericBytes(ref this, data, cbLen);
		}

		public byte[] GetGenericBytes(out int cbLen)
		{
			throw new global::System.NotImplementedException();
		}

		public bool Equals(global::Steamworks.SteamNetworkingIdentity x)
		{
			return global::Steamworks.NativeMethods.SteamAPI_SteamNetworkingIdentity_IsEqualTo(ref this, ref x);
		}

		public void ToString(out string buf)
		{
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(128);
			global::Steamworks.NativeMethods.SteamAPI_SteamNetworkingIdentity_ToString(ref this, intPtr, 128u);
			buf = global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
		}

		public bool ParseString(string pszStr)
		{
			using global::Steamworks.InteropHelp.UTF8StringHandle pszStr2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pszStr);
			return global::Steamworks.NativeMethods.SteamAPI_SteamNetworkingIdentity_ParseString(ref this, pszStr2);
		}
	}
}
