namespace Steamworks
{
	public static class SteamMatchmaking
	{
		public static int GetFavoriteGameCount()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamMatchmaking_GetFavoriteGameCount(global::Steamworks.CSteamAPIContext.GetSteamMatchmaking());
		}

		public static bool GetFavoriteGame(int iGame, out global::Steamworks.AppId_t pnAppID, out uint pnIP, out ushort pnConnPort, out ushort pnQueryPort, out uint punFlags, out uint pRTime32LastPlayedOnServer)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamMatchmaking_GetFavoriteGame(global::Steamworks.CSteamAPIContext.GetSteamMatchmaking(), iGame, out pnAppID, out pnIP, out pnConnPort, out pnQueryPort, out punFlags, out pRTime32LastPlayedOnServer);
		}

		public static int AddFavoriteGame(global::Steamworks.AppId_t nAppID, uint nIP, ushort nConnPort, ushort nQueryPort, uint unFlags, uint rTime32LastPlayedOnServer)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamMatchmaking_AddFavoriteGame(global::Steamworks.CSteamAPIContext.GetSteamMatchmaking(), nAppID, nIP, nConnPort, nQueryPort, unFlags, rTime32LastPlayedOnServer);
		}

		public static bool RemoveFavoriteGame(global::Steamworks.AppId_t nAppID, uint nIP, ushort nConnPort, ushort nQueryPort, uint unFlags)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamMatchmaking_RemoveFavoriteGame(global::Steamworks.CSteamAPIContext.GetSteamMatchmaking(), nAppID, nIP, nConnPort, nQueryPort, unFlags);
		}

		public static global::Steamworks.SteamAPICall_t RequestLobbyList()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamMatchmaking_RequestLobbyList(global::Steamworks.CSteamAPIContext.GetSteamMatchmaking());
		}

		public static void AddRequestLobbyListStringFilter(string pchKeyToMatch, string pchValueToMatch, global::Steamworks.ELobbyComparison eComparisonType)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchKeyToMatch2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchKeyToMatch);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchValueToMatch2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchValueToMatch);
			global::Steamworks.NativeMethods.ISteamMatchmaking_AddRequestLobbyListStringFilter(global::Steamworks.CSteamAPIContext.GetSteamMatchmaking(), pchKeyToMatch2, pchValueToMatch2, eComparisonType);
		}

		public static void AddRequestLobbyListNumericalFilter(string pchKeyToMatch, int nValueToMatch, global::Steamworks.ELobbyComparison eComparisonType)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchKeyToMatch2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchKeyToMatch);
			global::Steamworks.NativeMethods.ISteamMatchmaking_AddRequestLobbyListNumericalFilter(global::Steamworks.CSteamAPIContext.GetSteamMatchmaking(), pchKeyToMatch2, nValueToMatch, eComparisonType);
		}

		public static void AddRequestLobbyListNearValueFilter(string pchKeyToMatch, int nValueToBeCloseTo)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchKeyToMatch2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchKeyToMatch);
			global::Steamworks.NativeMethods.ISteamMatchmaking_AddRequestLobbyListNearValueFilter(global::Steamworks.CSteamAPIContext.GetSteamMatchmaking(), pchKeyToMatch2, nValueToBeCloseTo);
		}

		public static void AddRequestLobbyListFilterSlotsAvailable(int nSlotsAvailable)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamMatchmaking_AddRequestLobbyListFilterSlotsAvailable(global::Steamworks.CSteamAPIContext.GetSteamMatchmaking(), nSlotsAvailable);
		}

		public static void AddRequestLobbyListDistanceFilter(global::Steamworks.ELobbyDistanceFilter eLobbyDistanceFilter)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamMatchmaking_AddRequestLobbyListDistanceFilter(global::Steamworks.CSteamAPIContext.GetSteamMatchmaking(), eLobbyDistanceFilter);
		}

		public static void AddRequestLobbyListResultCountFilter(int cMaxResults)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamMatchmaking_AddRequestLobbyListResultCountFilter(global::Steamworks.CSteamAPIContext.GetSteamMatchmaking(), cMaxResults);
		}

		public static void AddRequestLobbyListCompatibleMembersFilter(global::Steamworks.CSteamID steamIDLobby)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamMatchmaking_AddRequestLobbyListCompatibleMembersFilter(global::Steamworks.CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby);
		}

		public static global::Steamworks.CSteamID GetLobbyByIndex(int iLobby)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.CSteamID)global::Steamworks.NativeMethods.ISteamMatchmaking_GetLobbyByIndex(global::Steamworks.CSteamAPIContext.GetSteamMatchmaking(), iLobby);
		}

		public static global::Steamworks.SteamAPICall_t CreateLobby(global::Steamworks.ELobbyType eLobbyType, int cMaxMembers)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamMatchmaking_CreateLobby(global::Steamworks.CSteamAPIContext.GetSteamMatchmaking(), eLobbyType, cMaxMembers);
		}

		public static global::Steamworks.SteamAPICall_t JoinLobby(global::Steamworks.CSteamID steamIDLobby)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamMatchmaking_JoinLobby(global::Steamworks.CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby);
		}

		public static void LeaveLobby(global::Steamworks.CSteamID steamIDLobby)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamMatchmaking_LeaveLobby(global::Steamworks.CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby);
		}

		public static bool InviteUserToLobby(global::Steamworks.CSteamID steamIDLobby, global::Steamworks.CSteamID steamIDInvitee)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamMatchmaking_InviteUserToLobby(global::Steamworks.CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby, steamIDInvitee);
		}

		public static int GetNumLobbyMembers(global::Steamworks.CSteamID steamIDLobby)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamMatchmaking_GetNumLobbyMembers(global::Steamworks.CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby);
		}

		public static global::Steamworks.CSteamID GetLobbyMemberByIndex(global::Steamworks.CSteamID steamIDLobby, int iMember)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.CSteamID)global::Steamworks.NativeMethods.ISteamMatchmaking_GetLobbyMemberByIndex(global::Steamworks.CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby, iMember);
		}

		public static string GetLobbyData(global::Steamworks.CSteamID steamIDLobby, string pchKey)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchKey2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchKey);
			return global::Steamworks.InteropHelp.PtrToStringUTF8(global::Steamworks.NativeMethods.ISteamMatchmaking_GetLobbyData(global::Steamworks.CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby, pchKey2));
		}

		public static bool SetLobbyData(global::Steamworks.CSteamID steamIDLobby, string pchKey, string pchValue)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchKey2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchKey);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchValue2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchValue);
			return global::Steamworks.NativeMethods.ISteamMatchmaking_SetLobbyData(global::Steamworks.CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby, pchKey2, pchValue2);
		}

		public static int GetLobbyDataCount(global::Steamworks.CSteamID steamIDLobby)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamMatchmaking_GetLobbyDataCount(global::Steamworks.CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby);
		}

		public static bool GetLobbyDataByIndex(global::Steamworks.CSteamID steamIDLobby, int iLobbyData, out string pchKey, int cchKeyBufferSize, out string pchValue, int cchValueBufferSize)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(cchKeyBufferSize);
			global::System.IntPtr intPtr2 = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(cchValueBufferSize);
			bool flag = global::Steamworks.NativeMethods.ISteamMatchmaking_GetLobbyDataByIndex(global::Steamworks.CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby, iLobbyData, intPtr, cchKeyBufferSize, intPtr2, cchValueBufferSize);
			pchKey = (flag ? global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr) : null);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
			pchValue = (flag ? global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr2) : null);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr2);
			return flag;
		}

		public static bool DeleteLobbyData(global::Steamworks.CSteamID steamIDLobby, string pchKey)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchKey2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchKey);
			return global::Steamworks.NativeMethods.ISteamMatchmaking_DeleteLobbyData(global::Steamworks.CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby, pchKey2);
		}

		public static string GetLobbyMemberData(global::Steamworks.CSteamID steamIDLobby, global::Steamworks.CSteamID steamIDUser, string pchKey)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchKey2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchKey);
			return global::Steamworks.InteropHelp.PtrToStringUTF8(global::Steamworks.NativeMethods.ISteamMatchmaking_GetLobbyMemberData(global::Steamworks.CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby, steamIDUser, pchKey2));
		}

		public static void SetLobbyMemberData(global::Steamworks.CSteamID steamIDLobby, string pchKey, string pchValue)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchKey2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchKey);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchValue2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchValue);
			global::Steamworks.NativeMethods.ISteamMatchmaking_SetLobbyMemberData(global::Steamworks.CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby, pchKey2, pchValue2);
		}

		public static bool SendLobbyChatMsg(global::Steamworks.CSteamID steamIDLobby, byte[] pvMsgBody, int cubMsgBody)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamMatchmaking_SendLobbyChatMsg(global::Steamworks.CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby, pvMsgBody, cubMsgBody);
		}

		public static int GetLobbyChatEntry(global::Steamworks.CSteamID steamIDLobby, int iChatID, out global::Steamworks.CSteamID pSteamIDUser, byte[] pvData, int cubData, out global::Steamworks.EChatEntryType peChatEntryType)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamMatchmaking_GetLobbyChatEntry(global::Steamworks.CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby, iChatID, out pSteamIDUser, pvData, cubData, out peChatEntryType);
		}

		public static bool RequestLobbyData(global::Steamworks.CSteamID steamIDLobby)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamMatchmaking_RequestLobbyData(global::Steamworks.CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby);
		}

		public static void SetLobbyGameServer(global::Steamworks.CSteamID steamIDLobby, uint unGameServerIP, ushort unGameServerPort, global::Steamworks.CSteamID steamIDGameServer)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamMatchmaking_SetLobbyGameServer(global::Steamworks.CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby, unGameServerIP, unGameServerPort, steamIDGameServer);
		}

		public static bool GetLobbyGameServer(global::Steamworks.CSteamID steamIDLobby, out uint punGameServerIP, out ushort punGameServerPort, out global::Steamworks.CSteamID psteamIDGameServer)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamMatchmaking_GetLobbyGameServer(global::Steamworks.CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby, out punGameServerIP, out punGameServerPort, out psteamIDGameServer);
		}

		public static bool SetLobbyMemberLimit(global::Steamworks.CSteamID steamIDLobby, int cMaxMembers)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamMatchmaking_SetLobbyMemberLimit(global::Steamworks.CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby, cMaxMembers);
		}

		public static int GetLobbyMemberLimit(global::Steamworks.CSteamID steamIDLobby)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamMatchmaking_GetLobbyMemberLimit(global::Steamworks.CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby);
		}

		public static bool SetLobbyType(global::Steamworks.CSteamID steamIDLobby, global::Steamworks.ELobbyType eLobbyType)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamMatchmaking_SetLobbyType(global::Steamworks.CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby, eLobbyType);
		}

		public static bool SetLobbyJoinable(global::Steamworks.CSteamID steamIDLobby, bool bLobbyJoinable)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamMatchmaking_SetLobbyJoinable(global::Steamworks.CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby, bLobbyJoinable);
		}

		public static global::Steamworks.CSteamID GetLobbyOwner(global::Steamworks.CSteamID steamIDLobby)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.CSteamID)global::Steamworks.NativeMethods.ISteamMatchmaking_GetLobbyOwner(global::Steamworks.CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby);
		}

		public static bool SetLobbyOwner(global::Steamworks.CSteamID steamIDLobby, global::Steamworks.CSteamID steamIDNewOwner)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamMatchmaking_SetLobbyOwner(global::Steamworks.CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby, steamIDNewOwner);
		}

		public static bool SetLinkedLobby(global::Steamworks.CSteamID steamIDLobby, global::Steamworks.CSteamID steamIDLobbyDependent)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamMatchmaking_SetLinkedLobby(global::Steamworks.CSteamAPIContext.GetSteamMatchmaking(), steamIDLobby, steamIDLobbyDependent);
		}
	}
}
