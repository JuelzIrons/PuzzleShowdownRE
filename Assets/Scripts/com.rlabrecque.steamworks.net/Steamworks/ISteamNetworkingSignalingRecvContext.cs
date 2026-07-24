namespace Steamworks
{
	[global::System.Serializable]
	public struct ISteamNetworkingSignalingRecvContext
	{
		public global::System.IntPtr OnConnectRequest(global::Steamworks.HSteamNetConnection hConn, ref global::Steamworks.SteamNetworkingIdentity identityPeer, int nLocalVirtualPort)
		{
			return global::Steamworks.NativeMethods.SteamAPI_ISteamNetworkingSignalingRecvContext_OnConnectRequest(ref this, hConn, ref identityPeer, nLocalVirtualPort);
		}

		public void SendRejectionSignal(ref global::Steamworks.SteamNetworkingIdentity identityPeer, global::System.IntPtr pMsg, int cbMsg)
		{
			global::Steamworks.NativeMethods.SteamAPI_ISteamNetworkingSignalingRecvContext_SendRejectionSignal(ref this, ref identityPeer, pMsg, cbMsg);
		}
	}
}
