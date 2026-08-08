namespace Steamworks
{
	[global::System.Serializable]
	public struct ISteamNetworkingConnectionSignaling
	{
		public bool SendSignal(global::Steamworks.HSteamNetConnection hConn, ref global::Steamworks.SteamNetConnectionInfo_t info, global::System.IntPtr pMsg, int cbMsg)
		{
			return global::Steamworks.NativeMethods.SteamAPI_ISteamNetworkingConnectionSignaling_SendSignal(ref this, hConn, ref info, pMsg, cbMsg);
		}

		public void Release()
		{
			global::Steamworks.NativeMethods.SteamAPI_ISteamNetworkingConnectionSignaling_Release(ref this);
		}
	}
}
