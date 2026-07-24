namespace Steamworks
{
	public static class SteamNetworkingSockets
	{
		public static global::Steamworks.HSteamListenSocket CreateListenSocketIP(ref global::Steamworks.SteamNetworkingIPAddr localAddress, int nOptions, global::Steamworks.SteamNetworkingConfigValue_t[] pOptions)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.HSteamListenSocket)global::Steamworks.NativeMethods.ISteamNetworkingSockets_CreateListenSocketIP(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), ref localAddress, nOptions, pOptions);
		}

		public static global::Steamworks.HSteamNetConnection ConnectByIPAddress(ref global::Steamworks.SteamNetworkingIPAddr address, int nOptions, global::Steamworks.SteamNetworkingConfigValue_t[] pOptions)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.HSteamNetConnection)global::Steamworks.NativeMethods.ISteamNetworkingSockets_ConnectByIPAddress(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), ref address, nOptions, pOptions);
		}

		public static global::Steamworks.HSteamListenSocket CreateListenSocketP2P(int nLocalVirtualPort, int nOptions, global::Steamworks.SteamNetworkingConfigValue_t[] pOptions)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.HSteamListenSocket)global::Steamworks.NativeMethods.ISteamNetworkingSockets_CreateListenSocketP2P(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), nLocalVirtualPort, nOptions, pOptions);
		}

		public static global::Steamworks.HSteamNetConnection ConnectP2P(ref global::Steamworks.SteamNetworkingIdentity identityRemote, int nRemoteVirtualPort, int nOptions, global::Steamworks.SteamNetworkingConfigValue_t[] pOptions)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.HSteamNetConnection)global::Steamworks.NativeMethods.ISteamNetworkingSockets_ConnectP2P(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), ref identityRemote, nRemoteVirtualPort, nOptions, pOptions);
		}

		public static global::Steamworks.EResult AcceptConnection(global::Steamworks.HSteamNetConnection hConn)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamNetworkingSockets_AcceptConnection(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), hConn);
		}

		public static bool CloseConnection(global::Steamworks.HSteamNetConnection hPeer, int nReason, string pszDebug, bool bEnableLinger)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pszDebug2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pszDebug);
			return global::Steamworks.NativeMethods.ISteamNetworkingSockets_CloseConnection(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), hPeer, nReason, pszDebug2, bEnableLinger);
		}

		public static bool CloseListenSocket(global::Steamworks.HSteamListenSocket hSocket)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamNetworkingSockets_CloseListenSocket(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), hSocket);
		}

		public static bool SetConnectionUserData(global::Steamworks.HSteamNetConnection hPeer, long nUserData)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamNetworkingSockets_SetConnectionUserData(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), hPeer, nUserData);
		}

		public static long GetConnectionUserData(global::Steamworks.HSteamNetConnection hPeer)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamNetworkingSockets_GetConnectionUserData(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), hPeer);
		}

		public static void SetConnectionName(global::Steamworks.HSteamNetConnection hPeer, string pszName)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pszName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pszName);
			global::Steamworks.NativeMethods.ISteamNetworkingSockets_SetConnectionName(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), hPeer, pszName2);
		}

		public static bool GetConnectionName(global::Steamworks.HSteamNetConnection hPeer, out string pszName, int nMaxLen)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(nMaxLen);
			bool flag = global::Steamworks.NativeMethods.ISteamNetworkingSockets_GetConnectionName(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), hPeer, intPtr, nMaxLen);
			pszName = (flag ? global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr) : null);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		public static global::Steamworks.EResult SendMessageToConnection(global::Steamworks.HSteamNetConnection hConn, global::System.IntPtr pData, uint cbData, int nSendFlags, out long pOutMessageNumber)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamNetworkingSockets_SendMessageToConnection(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), hConn, pData, cbData, nSendFlags, out pOutMessageNumber);
		}

		public static void SendMessages(int nMessages, global::System.IntPtr[] pMessages, long[] pOutMessageNumberOrResult)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamNetworkingSockets_SendMessages(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), nMessages, pMessages, pOutMessageNumberOrResult);
		}

		public static global::Steamworks.EResult FlushMessagesOnConnection(global::Steamworks.HSteamNetConnection hConn)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamNetworkingSockets_FlushMessagesOnConnection(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), hConn);
		}

		public static int ReceiveMessagesOnConnection(global::Steamworks.HSteamNetConnection hConn, global::System.IntPtr[] ppOutMessages, int nMaxMessages)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			if (ppOutMessages != null && ppOutMessages.Length != nMaxMessages)
			{
				throw new global::System.ArgumentException("ppOutMessages must be the same size as nMaxMessages!");
			}
			return global::Steamworks.NativeMethods.ISteamNetworkingSockets_ReceiveMessagesOnConnection(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), hConn, ppOutMessages, nMaxMessages);
		}

		public static bool GetConnectionInfo(global::Steamworks.HSteamNetConnection hConn, out global::Steamworks.SteamNetConnectionInfo_t pInfo)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamNetworkingSockets_GetConnectionInfo(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), hConn, out pInfo);
		}

		public static global::Steamworks.EResult GetConnectionRealTimeStatus(global::Steamworks.HSteamNetConnection hConn, ref global::Steamworks.SteamNetConnectionRealTimeStatus_t pStatus, int nLanes, ref global::Steamworks.SteamNetConnectionRealTimeLaneStatus_t pLanes)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamNetworkingSockets_GetConnectionRealTimeStatus(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), hConn, ref pStatus, nLanes, ref pLanes);
		}

		public static int GetDetailedConnectionStatus(global::Steamworks.HSteamNetConnection hConn, out string pszBuf, int cbBuf)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(cbBuf);
			int num = global::Steamworks.NativeMethods.ISteamNetworkingSockets_GetDetailedConnectionStatus(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), hConn, intPtr, cbBuf);
			pszBuf = ((num != -1) ? global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr) : null);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
			return num;
		}

		public static bool GetListenSocketAddress(global::Steamworks.HSteamListenSocket hSocket, out global::Steamworks.SteamNetworkingIPAddr address)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamNetworkingSockets_GetListenSocketAddress(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), hSocket, out address);
		}

		public static bool CreateSocketPair(out global::Steamworks.HSteamNetConnection pOutConnection1, out global::Steamworks.HSteamNetConnection pOutConnection2, bool bUseNetworkLoopback, ref global::Steamworks.SteamNetworkingIdentity pIdentity1, ref global::Steamworks.SteamNetworkingIdentity pIdentity2)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamNetworkingSockets_CreateSocketPair(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), out pOutConnection1, out pOutConnection2, bUseNetworkLoopback, ref pIdentity1, ref pIdentity2);
		}

		public static global::Steamworks.EResult ConfigureConnectionLanes(global::Steamworks.HSteamNetConnection hConn, int nNumLanes, int[] pLanePriorities, ushort[] pLaneWeights)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamNetworkingSockets_ConfigureConnectionLanes(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), hConn, nNumLanes, pLanePriorities, pLaneWeights);
		}

		public static bool GetIdentity(out global::Steamworks.SteamNetworkingIdentity pIdentity)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamNetworkingSockets_GetIdentity(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), out pIdentity);
		}

		public static global::Steamworks.ESteamNetworkingAvailability InitAuthentication()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamNetworkingSockets_InitAuthentication(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets());
		}

		public static global::Steamworks.ESteamNetworkingAvailability GetAuthenticationStatus(out global::Steamworks.SteamNetAuthenticationStatus_t pDetails)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamNetworkingSockets_GetAuthenticationStatus(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), out pDetails);
		}

		public static global::Steamworks.HSteamNetPollGroup CreatePollGroup()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.HSteamNetPollGroup)global::Steamworks.NativeMethods.ISteamNetworkingSockets_CreatePollGroup(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets());
		}

		public static bool DestroyPollGroup(global::Steamworks.HSteamNetPollGroup hPollGroup)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamNetworkingSockets_DestroyPollGroup(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), hPollGroup);
		}

		public static bool SetConnectionPollGroup(global::Steamworks.HSteamNetConnection hConn, global::Steamworks.HSteamNetPollGroup hPollGroup)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamNetworkingSockets_SetConnectionPollGroup(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), hConn, hPollGroup);
		}

		public static int ReceiveMessagesOnPollGroup(global::Steamworks.HSteamNetPollGroup hPollGroup, global::System.IntPtr[] ppOutMessages, int nMaxMessages)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			if (ppOutMessages != null && ppOutMessages.Length != nMaxMessages)
			{
				throw new global::System.ArgumentException("ppOutMessages must be the same size as nMaxMessages!");
			}
			return global::Steamworks.NativeMethods.ISteamNetworkingSockets_ReceiveMessagesOnPollGroup(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), hPollGroup, ppOutMessages, nMaxMessages);
		}

		public static bool ReceivedRelayAuthTicket(global::System.IntPtr pvTicket, int cbTicket, out global::Steamworks.SteamDatagramRelayAuthTicket pOutParsedTicket)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamNetworkingSockets_ReceivedRelayAuthTicket(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), pvTicket, cbTicket, out pOutParsedTicket);
		}

		public static int FindRelayAuthTicketForServer(ref global::Steamworks.SteamNetworkingIdentity identityGameServer, int nRemoteVirtualPort, out global::Steamworks.SteamDatagramRelayAuthTicket pOutParsedTicket)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamNetworkingSockets_FindRelayAuthTicketForServer(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), ref identityGameServer, nRemoteVirtualPort, out pOutParsedTicket);
		}

		public static global::Steamworks.HSteamNetConnection ConnectToHostedDedicatedServer(ref global::Steamworks.SteamNetworkingIdentity identityTarget, int nRemoteVirtualPort, int nOptions, global::Steamworks.SteamNetworkingConfigValue_t[] pOptions)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.HSteamNetConnection)global::Steamworks.NativeMethods.ISteamNetworkingSockets_ConnectToHostedDedicatedServer(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), ref identityTarget, nRemoteVirtualPort, nOptions, pOptions);
		}

		public static ushort GetHostedDedicatedServerPort()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamNetworkingSockets_GetHostedDedicatedServerPort(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets());
		}

		public static global::Steamworks.SteamNetworkingPOPID GetHostedDedicatedServerPOPID()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.SteamNetworkingPOPID)global::Steamworks.NativeMethods.ISteamNetworkingSockets_GetHostedDedicatedServerPOPID(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets());
		}

		public static global::Steamworks.EResult GetHostedDedicatedServerAddress(out global::Steamworks.SteamDatagramHostedAddress pRouting)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamNetworkingSockets_GetHostedDedicatedServerAddress(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), out pRouting);
		}

		public static global::Steamworks.HSteamListenSocket CreateHostedDedicatedServerListenSocket(int nLocalVirtualPort, int nOptions, global::Steamworks.SteamNetworkingConfigValue_t[] pOptions)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.HSteamListenSocket)global::Steamworks.NativeMethods.ISteamNetworkingSockets_CreateHostedDedicatedServerListenSocket(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), nLocalVirtualPort, nOptions, pOptions);
		}

		public static global::Steamworks.EResult GetGameCoordinatorServerLogin(global::System.IntPtr pLoginInfo, out int pcbSignedBlob, global::System.IntPtr pBlob)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamNetworkingSockets_GetGameCoordinatorServerLogin(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), pLoginInfo, out pcbSignedBlob, pBlob);
		}

		public static global::Steamworks.HSteamNetConnection ConnectP2PCustomSignaling(out global::Steamworks.ISteamNetworkingConnectionSignaling pSignaling, ref global::Steamworks.SteamNetworkingIdentity pPeerIdentity, int nRemoteVirtualPort, int nOptions, global::Steamworks.SteamNetworkingConfigValue_t[] pOptions)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.HSteamNetConnection)global::Steamworks.NativeMethods.ISteamNetworkingSockets_ConnectP2PCustomSignaling(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), out pSignaling, ref pPeerIdentity, nRemoteVirtualPort, nOptions, pOptions);
		}

		public static bool ReceivedP2PCustomSignal(global::System.IntPtr pMsg, int cbMsg, out global::Steamworks.ISteamNetworkingSignalingRecvContext pContext)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamNetworkingSockets_ReceivedP2PCustomSignal(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), pMsg, cbMsg, out pContext);
		}

		public static bool GetCertificateRequest(out int pcbBlob, global::System.IntPtr pBlob, out global::Steamworks.SteamNetworkingErrMsg errMsg)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamNetworkingSockets_GetCertificateRequest(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), out pcbBlob, pBlob, out errMsg);
		}

		public static bool SetCertificate(global::System.IntPtr pCertificate, int cbCertificate, out global::Steamworks.SteamNetworkingErrMsg errMsg)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamNetworkingSockets_SetCertificate(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), pCertificate, cbCertificate, out errMsg);
		}

		public static void ResetIdentity(ref global::Steamworks.SteamNetworkingIdentity pIdentity)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamNetworkingSockets_ResetIdentity(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), ref pIdentity);
		}

		public static void RunCallbacks()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamNetworkingSockets_RunCallbacks(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets());
		}

		public static bool BeginAsyncRequestFakeIP(int nNumPorts)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamNetworkingSockets_BeginAsyncRequestFakeIP(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), nNumPorts);
		}

		public static void GetFakeIP(int idxFirstPort, out global::Steamworks.SteamNetworkingFakeIPResult_t pInfo)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamNetworkingSockets_GetFakeIP(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), idxFirstPort, out pInfo);
		}

		public static global::Steamworks.HSteamListenSocket CreateListenSocketP2PFakeIP(int idxFakePort, int nOptions, global::Steamworks.SteamNetworkingConfigValue_t[] pOptions)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.HSteamListenSocket)global::Steamworks.NativeMethods.ISteamNetworkingSockets_CreateListenSocketP2PFakeIP(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), idxFakePort, nOptions, pOptions);
		}

		public static global::Steamworks.EResult GetRemoteFakeIPForConnection(global::Steamworks.HSteamNetConnection hConn, out global::Steamworks.SteamNetworkingIPAddr pOutAddr)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamNetworkingSockets_GetRemoteFakeIPForConnection(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), hConn, out pOutAddr);
		}

		public static global::System.IntPtr CreateFakeUDPPort(int idxFakeServerPort)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamNetworkingSockets_CreateFakeUDPPort(global::Steamworks.CSteamAPIContext.GetSteamNetworkingSockets(), idxFakeServerPort);
		}
	}
}
