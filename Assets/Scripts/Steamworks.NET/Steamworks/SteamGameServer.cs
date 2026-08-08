namespace Steamworks
{
	public static class SteamGameServer
	{
		public static void SetProduct(string pszProduct)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pszProduct2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pszProduct);
			global::Steamworks.NativeMethods.ISteamGameServer_SetProduct(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer(), pszProduct2);
		}

		public static void SetGameDescription(string pszGameDescription)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pszGameDescription2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pszGameDescription);
			global::Steamworks.NativeMethods.ISteamGameServer_SetGameDescription(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer(), pszGameDescription2);
		}

		public static void SetModDir(string pszModDir)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pszModDir2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pszModDir);
			global::Steamworks.NativeMethods.ISteamGameServer_SetModDir(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer(), pszModDir2);
		}

		public static void SetDedicatedServer(bool bDedicated)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::Steamworks.NativeMethods.ISteamGameServer_SetDedicatedServer(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer(), bDedicated);
		}

		public static void LogOn(string pszToken)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pszToken2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pszToken);
			global::Steamworks.NativeMethods.ISteamGameServer_LogOn(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer(), pszToken2);
		}

		public static void LogOnAnonymous()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::Steamworks.NativeMethods.ISteamGameServer_LogOnAnonymous(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer());
		}

		public static void LogOff()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::Steamworks.NativeMethods.ISteamGameServer_LogOff(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer());
		}

		public static bool BLoggedOn()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamGameServer_BLoggedOn(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer());
		}

		public static bool BSecure()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamGameServer_BSecure(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer());
		}

		public static global::Steamworks.CSteamID GetSteamID()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.CSteamID)global::Steamworks.NativeMethods.ISteamGameServer_GetSteamID(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer());
		}

		public static bool WasRestartRequested()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamGameServer_WasRestartRequested(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer());
		}

		public static void SetMaxPlayerCount(int cPlayersMax)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::Steamworks.NativeMethods.ISteamGameServer_SetMaxPlayerCount(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer(), cPlayersMax);
		}

		public static void SetBotPlayerCount(int cBotplayers)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::Steamworks.NativeMethods.ISteamGameServer_SetBotPlayerCount(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer(), cBotplayers);
		}

		public static void SetServerName(string pszServerName)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pszServerName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pszServerName);
			global::Steamworks.NativeMethods.ISteamGameServer_SetServerName(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer(), pszServerName2);
		}

		public static void SetMapName(string pszMapName)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pszMapName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pszMapName);
			global::Steamworks.NativeMethods.ISteamGameServer_SetMapName(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer(), pszMapName2);
		}

		public static void SetPasswordProtected(bool bPasswordProtected)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::Steamworks.NativeMethods.ISteamGameServer_SetPasswordProtected(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer(), bPasswordProtected);
		}

		public static void SetSpectatorPort(ushort unSpectatorPort)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::Steamworks.NativeMethods.ISteamGameServer_SetSpectatorPort(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer(), unSpectatorPort);
		}

		public static void SetSpectatorServerName(string pszSpectatorServerName)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pszSpectatorServerName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pszSpectatorServerName);
			global::Steamworks.NativeMethods.ISteamGameServer_SetSpectatorServerName(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer(), pszSpectatorServerName2);
		}

		public static void ClearAllKeyValues()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::Steamworks.NativeMethods.ISteamGameServer_ClearAllKeyValues(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer());
		}

		public static void SetKeyValue(string pKey, string pValue)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pKey2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pKey);
			using global::Steamworks.InteropHelp.UTF8StringHandle pValue2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pValue);
			global::Steamworks.NativeMethods.ISteamGameServer_SetKeyValue(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer(), pKey2, pValue2);
		}

		public static void SetGameTags(string pchGameTags)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchGameTags2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchGameTags);
			global::Steamworks.NativeMethods.ISteamGameServer_SetGameTags(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer(), pchGameTags2);
		}

		public static void SetGameData(string pchGameData)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchGameData2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchGameData);
			global::Steamworks.NativeMethods.ISteamGameServer_SetGameData(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer(), pchGameData2);
		}

		public static void SetRegion(string pszRegion)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pszRegion2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pszRegion);
			global::Steamworks.NativeMethods.ISteamGameServer_SetRegion(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer(), pszRegion2);
		}

		public static void SetAdvertiseServerActive(bool bActive)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::Steamworks.NativeMethods.ISteamGameServer_SetAdvertiseServerActive(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer(), bActive);
		}

		public static global::Steamworks.HAuthTicket GetAuthSessionTicket(byte[] pTicket, int cbMaxTicket, out uint pcbTicket, ref global::Steamworks.SteamNetworkingIdentity pSnid)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.HAuthTicket)global::Steamworks.NativeMethods.ISteamGameServer_GetAuthSessionTicket(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer(), pTicket, cbMaxTicket, out pcbTicket, ref pSnid);
		}

		public static global::Steamworks.EBeginAuthSessionResult BeginAuthSession(byte[] pAuthTicket, int cbAuthTicket, global::Steamworks.CSteamID steamID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamGameServer_BeginAuthSession(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer(), pAuthTicket, cbAuthTicket, steamID);
		}

		public static void EndAuthSession(global::Steamworks.CSteamID steamID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::Steamworks.NativeMethods.ISteamGameServer_EndAuthSession(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer(), steamID);
		}

		public static void CancelAuthTicket(global::Steamworks.HAuthTicket hAuthTicket)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::Steamworks.NativeMethods.ISteamGameServer_CancelAuthTicket(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer(), hAuthTicket);
		}

		public static global::Steamworks.EUserHasLicenseForAppResult UserHasLicenseForApp(global::Steamworks.CSteamID steamID, global::Steamworks.AppId_t appID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamGameServer_UserHasLicenseForApp(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer(), steamID, appID);
		}

		public static bool RequestUserGroupStatus(global::Steamworks.CSteamID steamIDUser, global::Steamworks.CSteamID steamIDGroup)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamGameServer_RequestUserGroupStatus(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer(), steamIDUser, steamIDGroup);
		}

		public static void GetGameplayStats()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::Steamworks.NativeMethods.ISteamGameServer_GetGameplayStats(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer());
		}

		public static global::Steamworks.SteamAPICall_t GetServerReputation()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamGameServer_GetServerReputation(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer());
		}

		public static global::Steamworks.SteamIPAddress_t GetPublicIP()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamGameServer_GetPublicIP(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer());
		}

		public static bool HandleIncomingPacket(byte[] pData, int cbData, uint srcIP, ushort srcPort)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamGameServer_HandleIncomingPacket(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer(), pData, cbData, srcIP, srcPort);
		}

		public static int GetNextOutgoingPacket(byte[] pOut, int cbMaxOut, out uint pNetAdr, out ushort pPort)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamGameServer_GetNextOutgoingPacket(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer(), pOut, cbMaxOut, out pNetAdr, out pPort);
		}

		public static global::Steamworks.SteamAPICall_t AssociateWithClan(global::Steamworks.CSteamID steamIDClan)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamGameServer_AssociateWithClan(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer(), steamIDClan);
		}

		public static global::Steamworks.SteamAPICall_t ComputeNewPlayerCompatibility(global::Steamworks.CSteamID steamIDNewPlayer)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamGameServer_ComputeNewPlayerCompatibility(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer(), steamIDNewPlayer);
		}

		public static bool SendUserConnectAndAuthenticate_DEPRECATED(uint unIPClient, byte[] pvAuthBlob, uint cubAuthBlobSize, out global::Steamworks.CSteamID pSteamIDUser)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamGameServer_SendUserConnectAndAuthenticate_DEPRECATED(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer(), unIPClient, pvAuthBlob, cubAuthBlobSize, out pSteamIDUser);
		}

		public static global::Steamworks.CSteamID CreateUnauthenticatedUserConnection()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.CSteamID)global::Steamworks.NativeMethods.ISteamGameServer_CreateUnauthenticatedUserConnection(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer());
		}

		public static void SendUserDisconnect_DEPRECATED(global::Steamworks.CSteamID steamIDUser)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::Steamworks.NativeMethods.ISteamGameServer_SendUserDisconnect_DEPRECATED(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer(), steamIDUser);
		}

		public static bool BUpdateUserData(global::Steamworks.CSteamID steamIDUser, string pchPlayerName, uint uScore)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchPlayerName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchPlayerName);
			return global::Steamworks.NativeMethods.ISteamGameServer_BUpdateUserData(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServer(), steamIDUser, pchPlayerName2, uScore);
		}
	}
}
