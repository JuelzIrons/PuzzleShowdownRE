namespace Steamworks
{
	public static class SteamGameServerNetworking
	{
		public static bool SendP2PPacket(global::Steamworks.CSteamID steamIDRemote, byte[] pubData, uint cubData, global::Steamworks.EP2PSend eP2PSendType, int nChannel = 0)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworking_SendP2PPacket(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworking(), steamIDRemote, pubData, cubData, eP2PSendType, nChannel);
		}

		public static bool IsP2PPacketAvailable(out uint pcubMsgSize, int nChannel = 0)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworking_IsP2PPacketAvailable(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworking(), out pcubMsgSize, nChannel);
		}

		public static bool ReadP2PPacket(byte[] pubDest, uint cubDest, out uint pcubMsgSize, out global::Steamworks.CSteamID psteamIDRemote, int nChannel = 0)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworking_ReadP2PPacket(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworking(), pubDest, cubDest, out pcubMsgSize, out psteamIDRemote, nChannel);
		}

		public static bool AcceptP2PSessionWithUser(global::Steamworks.CSteamID steamIDRemote)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworking_AcceptP2PSessionWithUser(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworking(), steamIDRemote);
		}

		public static bool CloseP2PSessionWithUser(global::Steamworks.CSteamID steamIDRemote)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworking_CloseP2PSessionWithUser(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworking(), steamIDRemote);
		}

		public static bool CloseP2PChannelWithUser(global::Steamworks.CSteamID steamIDRemote, int nChannel)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworking_CloseP2PChannelWithUser(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworking(), steamIDRemote, nChannel);
		}

		public static bool GetP2PSessionState(global::Steamworks.CSteamID steamIDRemote, out global::Steamworks.P2PSessionState_t pConnectionState)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworking_GetP2PSessionState(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworking(), steamIDRemote, out pConnectionState);
		}

		public static bool AllowP2PPacketRelay(bool bAllow)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworking_AllowP2PPacketRelay(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworking(), bAllow);
		}

		public static global::Steamworks.SNetListenSocket_t CreateListenSocket(int nVirtualP2PPort, global::Steamworks.SteamIPAddress_t nIP, ushort nPort, bool bAllowUseOfPacketRelay)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.SNetListenSocket_t)global::Steamworks.NativeMethods.ISteamNetworking_CreateListenSocket(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworking(), nVirtualP2PPort, nIP, nPort, bAllowUseOfPacketRelay);
		}

		public static global::Steamworks.SNetSocket_t CreateP2PConnectionSocket(global::Steamworks.CSteamID steamIDTarget, int nVirtualPort, int nTimeoutSec, bool bAllowUseOfPacketRelay)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.SNetSocket_t)global::Steamworks.NativeMethods.ISteamNetworking_CreateP2PConnectionSocket(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworking(), steamIDTarget, nVirtualPort, nTimeoutSec, bAllowUseOfPacketRelay);
		}

		public static global::Steamworks.SNetSocket_t CreateConnectionSocket(global::Steamworks.SteamIPAddress_t nIP, ushort nPort, int nTimeoutSec)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.SNetSocket_t)global::Steamworks.NativeMethods.ISteamNetworking_CreateConnectionSocket(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworking(), nIP, nPort, nTimeoutSec);
		}

		public static bool DestroySocket(global::Steamworks.SNetSocket_t hSocket, bool bNotifyRemoteEnd)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworking_DestroySocket(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworking(), hSocket, bNotifyRemoteEnd);
		}

		public static bool DestroyListenSocket(global::Steamworks.SNetListenSocket_t hSocket, bool bNotifyRemoteEnd)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworking_DestroyListenSocket(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworking(), hSocket, bNotifyRemoteEnd);
		}

		public static bool SendDataOnSocket(global::Steamworks.SNetSocket_t hSocket, byte[] pubData, uint cubData, bool bReliable)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworking_SendDataOnSocket(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworking(), hSocket, pubData, cubData, bReliable);
		}

		public static bool IsDataAvailableOnSocket(global::Steamworks.SNetSocket_t hSocket, out uint pcubMsgSize)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworking_IsDataAvailableOnSocket(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworking(), hSocket, out pcubMsgSize);
		}

		public static bool RetrieveDataFromSocket(global::Steamworks.SNetSocket_t hSocket, byte[] pubDest, uint cubDest, out uint pcubMsgSize)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworking_RetrieveDataFromSocket(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworking(), hSocket, pubDest, cubDest, out pcubMsgSize);
		}

		public static bool IsDataAvailable(global::Steamworks.SNetListenSocket_t hListenSocket, out uint pcubMsgSize, out global::Steamworks.SNetSocket_t phSocket)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworking_IsDataAvailable(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworking(), hListenSocket, out pcubMsgSize, out phSocket);
		}

		public static bool RetrieveData(global::Steamworks.SNetListenSocket_t hListenSocket, byte[] pubDest, uint cubDest, out uint pcubMsgSize, out global::Steamworks.SNetSocket_t phSocket)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworking_RetrieveData(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworking(), hListenSocket, pubDest, cubDest, out pcubMsgSize, out phSocket);
		}

		public static bool GetSocketInfo(global::Steamworks.SNetSocket_t hSocket, out global::Steamworks.CSteamID pSteamIDRemote, out int peSocketStatus, out global::Steamworks.SteamIPAddress_t punIPRemote, out ushort punPortRemote)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworking_GetSocketInfo(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworking(), hSocket, out pSteamIDRemote, out peSocketStatus, out punIPRemote, out punPortRemote);
		}

		public static bool GetListenSocketInfo(global::Steamworks.SNetListenSocket_t hListenSocket, out global::Steamworks.SteamIPAddress_t pnIP, out ushort pnPort)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworking_GetListenSocketInfo(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworking(), hListenSocket, out pnIP, out pnPort);
		}

		public static global::Steamworks.ESNetSocketConnectionType GetSocketConnectionType(global::Steamworks.SNetSocket_t hSocket)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworking_GetSocketConnectionType(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworking(), hSocket);
		}

		public static int GetMaxPacketSize(global::Steamworks.SNetSocket_t hSocket)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworking_GetMaxPacketSize(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworking(), hSocket);
		}
	}
}
