namespace Steamworks
{
	public static class SteamMatchmakingServers
	{
		public static global::Steamworks.HServerListRequest RequestInternetServerList(global::Steamworks.AppId_t iApp, global::Steamworks.MatchMakingKeyValuePair_t[] ppchFilters, uint nFilters, global::Steamworks.ISteamMatchmakingServerListResponse pRequestServersResponse)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.HServerListRequest)global::Steamworks.NativeMethods.ISteamMatchmakingServers_RequestInternetServerList(global::Steamworks.CSteamAPIContext.GetSteamMatchmakingServers(), iApp, new global::Steamworks.MMKVPMarshaller(ppchFilters), nFilters, (global::System.IntPtr)pRequestServersResponse);
		}

		public static global::Steamworks.HServerListRequest RequestLANServerList(global::Steamworks.AppId_t iApp, global::Steamworks.ISteamMatchmakingServerListResponse pRequestServersResponse)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.HServerListRequest)global::Steamworks.NativeMethods.ISteamMatchmakingServers_RequestLANServerList(global::Steamworks.CSteamAPIContext.GetSteamMatchmakingServers(), iApp, (global::System.IntPtr)pRequestServersResponse);
		}

		public static global::Steamworks.HServerListRequest RequestFriendsServerList(global::Steamworks.AppId_t iApp, global::Steamworks.MatchMakingKeyValuePair_t[] ppchFilters, uint nFilters, global::Steamworks.ISteamMatchmakingServerListResponse pRequestServersResponse)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.HServerListRequest)global::Steamworks.NativeMethods.ISteamMatchmakingServers_RequestFriendsServerList(global::Steamworks.CSteamAPIContext.GetSteamMatchmakingServers(), iApp, new global::Steamworks.MMKVPMarshaller(ppchFilters), nFilters, (global::System.IntPtr)pRequestServersResponse);
		}

		public static global::Steamworks.HServerListRequest RequestFavoritesServerList(global::Steamworks.AppId_t iApp, global::Steamworks.MatchMakingKeyValuePair_t[] ppchFilters, uint nFilters, global::Steamworks.ISteamMatchmakingServerListResponse pRequestServersResponse)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.HServerListRequest)global::Steamworks.NativeMethods.ISteamMatchmakingServers_RequestFavoritesServerList(global::Steamworks.CSteamAPIContext.GetSteamMatchmakingServers(), iApp, new global::Steamworks.MMKVPMarshaller(ppchFilters), nFilters, (global::System.IntPtr)pRequestServersResponse);
		}

		public static global::Steamworks.HServerListRequest RequestHistoryServerList(global::Steamworks.AppId_t iApp, global::Steamworks.MatchMakingKeyValuePair_t[] ppchFilters, uint nFilters, global::Steamworks.ISteamMatchmakingServerListResponse pRequestServersResponse)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.HServerListRequest)global::Steamworks.NativeMethods.ISteamMatchmakingServers_RequestHistoryServerList(global::Steamworks.CSteamAPIContext.GetSteamMatchmakingServers(), iApp, new global::Steamworks.MMKVPMarshaller(ppchFilters), nFilters, (global::System.IntPtr)pRequestServersResponse);
		}

		public static global::Steamworks.HServerListRequest RequestSpectatorServerList(global::Steamworks.AppId_t iApp, global::Steamworks.MatchMakingKeyValuePair_t[] ppchFilters, uint nFilters, global::Steamworks.ISteamMatchmakingServerListResponse pRequestServersResponse)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.HServerListRequest)global::Steamworks.NativeMethods.ISteamMatchmakingServers_RequestSpectatorServerList(global::Steamworks.CSteamAPIContext.GetSteamMatchmakingServers(), iApp, new global::Steamworks.MMKVPMarshaller(ppchFilters), nFilters, (global::System.IntPtr)pRequestServersResponse);
		}

		public static void ReleaseRequest(global::Steamworks.HServerListRequest hServerListRequest)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamMatchmakingServers_ReleaseRequest(global::Steamworks.CSteamAPIContext.GetSteamMatchmakingServers(), hServerListRequest);
		}

		public static global::Steamworks.gameserveritem_t GetServerDetails(global::Steamworks.HServerListRequest hRequest, int iServer)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.gameserveritem_t)global::System.Runtime.InteropServices.Marshal.PtrToStructure(global::Steamworks.NativeMethods.ISteamMatchmakingServers_GetServerDetails(global::Steamworks.CSteamAPIContext.GetSteamMatchmakingServers(), hRequest, iServer), typeof(global::Steamworks.gameserveritem_t));
		}

		public static void CancelQuery(global::Steamworks.HServerListRequest hRequest)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamMatchmakingServers_CancelQuery(global::Steamworks.CSteamAPIContext.GetSteamMatchmakingServers(), hRequest);
		}

		public static void RefreshQuery(global::Steamworks.HServerListRequest hRequest)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamMatchmakingServers_RefreshQuery(global::Steamworks.CSteamAPIContext.GetSteamMatchmakingServers(), hRequest);
		}

		public static bool IsRefreshing(global::Steamworks.HServerListRequest hRequest)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamMatchmakingServers_IsRefreshing(global::Steamworks.CSteamAPIContext.GetSteamMatchmakingServers(), hRequest);
		}

		public static int GetServerCount(global::Steamworks.HServerListRequest hRequest)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamMatchmakingServers_GetServerCount(global::Steamworks.CSteamAPIContext.GetSteamMatchmakingServers(), hRequest);
		}

		public static void RefreshServer(global::Steamworks.HServerListRequest hRequest, int iServer)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamMatchmakingServers_RefreshServer(global::Steamworks.CSteamAPIContext.GetSteamMatchmakingServers(), hRequest, iServer);
		}

		public static global::Steamworks.HServerQuery PingServer(uint unIP, ushort usPort, global::Steamworks.ISteamMatchmakingPingResponse pRequestServersResponse)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.HServerQuery)global::Steamworks.NativeMethods.ISteamMatchmakingServers_PingServer(global::Steamworks.CSteamAPIContext.GetSteamMatchmakingServers(), unIP, usPort, (global::System.IntPtr)pRequestServersResponse);
		}

		public static global::Steamworks.HServerQuery PlayerDetails(uint unIP, ushort usPort, global::Steamworks.ISteamMatchmakingPlayersResponse pRequestServersResponse)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.HServerQuery)global::Steamworks.NativeMethods.ISteamMatchmakingServers_PlayerDetails(global::Steamworks.CSteamAPIContext.GetSteamMatchmakingServers(), unIP, usPort, (global::System.IntPtr)pRequestServersResponse);
		}

		public static global::Steamworks.HServerQuery ServerRules(uint unIP, ushort usPort, global::Steamworks.ISteamMatchmakingRulesResponse pRequestServersResponse)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.HServerQuery)global::Steamworks.NativeMethods.ISteamMatchmakingServers_ServerRules(global::Steamworks.CSteamAPIContext.GetSteamMatchmakingServers(), unIP, usPort, (global::System.IntPtr)pRequestServersResponse);
		}

		public static void CancelServerQuery(global::Steamworks.HServerQuery hServerQuery)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamMatchmakingServers_CancelServerQuery(global::Steamworks.CSteamAPIContext.GetSteamMatchmakingServers(), hServerQuery);
		}
	}
}
