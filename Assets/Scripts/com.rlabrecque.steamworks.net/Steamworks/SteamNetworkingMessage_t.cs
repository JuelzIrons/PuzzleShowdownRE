namespace Steamworks
{
	[global::System.Serializable]
	public struct SteamNetworkingMessage_t
	{
		public global::System.IntPtr m_pData;

		public int m_cbSize;

		public global::Steamworks.HSteamNetConnection m_conn;

		public global::Steamworks.SteamNetworkingIdentity m_identityPeer;

		public long m_nConnUserData;

		public global::Steamworks.SteamNetworkingMicroseconds m_usecTimeReceived;

		public long m_nMessageNumber;

		public global::System.IntPtr m_pfnFreeData;

		internal global::System.IntPtr m_pfnRelease;

		public int m_nChannel;

		public int m_nFlags;

		public long m_nUserData;

		public ushort m_idxLane;

		public ushort _pad1__;

		public void Release()
		{
			throw new global::System.NotImplementedException("Please use the static Release function instead which takes an IntPtr.");
		}

		public static void Release(global::System.IntPtr pointer)
		{
			global::Steamworks.NativeMethods.SteamAPI_SteamNetworkingMessage_t_Release(pointer);
		}

		public static global::Steamworks.SteamNetworkingMessage_t FromIntPtr(global::System.IntPtr pointer)
		{
			return (global::Steamworks.SteamNetworkingMessage_t)global::System.Runtime.InteropServices.Marshal.PtrToStructure(pointer, typeof(global::Steamworks.SteamNetworkingMessage_t));
		}
	}
}
