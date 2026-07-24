namespace Steamworks
{
	public static class SteamUser
	{
		public static global::Steamworks.HSteamUser GetHSteamUser()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.HSteamUser)global::Steamworks.NativeMethods.ISteamUser_GetHSteamUser(global::Steamworks.CSteamAPIContext.GetSteamUser());
		}

		public static bool BLoggedOn()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamUser_BLoggedOn(global::Steamworks.CSteamAPIContext.GetSteamUser());
		}

		public static global::Steamworks.CSteamID GetSteamID()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.CSteamID)global::Steamworks.NativeMethods.ISteamUser_GetSteamID(global::Steamworks.CSteamAPIContext.GetSteamUser());
		}

		public static int InitiateGameConnection_DEPRECATED(byte[] pAuthBlob, int cbMaxAuthBlob, global::Steamworks.CSteamID steamIDGameServer, uint unIPServer, ushort usPortServer, bool bSecure)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamUser_InitiateGameConnection_DEPRECATED(global::Steamworks.CSteamAPIContext.GetSteamUser(), pAuthBlob, cbMaxAuthBlob, steamIDGameServer, unIPServer, usPortServer, bSecure);
		}

		public static void TerminateGameConnection_DEPRECATED(uint unIPServer, ushort usPortServer)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamUser_TerminateGameConnection_DEPRECATED(global::Steamworks.CSteamAPIContext.GetSteamUser(), unIPServer, usPortServer);
		}

		public static void TrackAppUsageEvent(global::Steamworks.CGameID gameID, int eAppUsageEvent, string pchExtraInfo = "")
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchExtraInfo2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchExtraInfo);
			global::Steamworks.NativeMethods.ISteamUser_TrackAppUsageEvent(global::Steamworks.CSteamAPIContext.GetSteamUser(), gameID, eAppUsageEvent, pchExtraInfo2);
		}

		public static bool GetUserDataFolder(out string pchBuffer, int cubBuffer)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(cubBuffer);
			bool flag = global::Steamworks.NativeMethods.ISteamUser_GetUserDataFolder(global::Steamworks.CSteamAPIContext.GetSteamUser(), intPtr, cubBuffer);
			pchBuffer = (flag ? global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr) : null);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		public static void StartVoiceRecording()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamUser_StartVoiceRecording(global::Steamworks.CSteamAPIContext.GetSteamUser());
		}

		public static void StopVoiceRecording()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamUser_StopVoiceRecording(global::Steamworks.CSteamAPIContext.GetSteamUser());
		}

		public static global::Steamworks.EVoiceResult GetAvailableVoice(out uint pcbCompressed)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamUser_GetAvailableVoice(global::Steamworks.CSteamAPIContext.GetSteamUser(), out pcbCompressed, global::System.IntPtr.Zero, 0u);
		}

		public static global::Steamworks.EVoiceResult GetVoice(bool bWantCompressed, byte[] pDestBuffer, uint cbDestBufferSize, out uint nBytesWritten)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamUser_GetVoice(global::Steamworks.CSteamAPIContext.GetSteamUser(), bWantCompressed, pDestBuffer, cbDestBufferSize, out nBytesWritten, bWantUncompressed_Deprecated: false, global::System.IntPtr.Zero, 0u, global::System.IntPtr.Zero, 0u);
		}

		public static global::Steamworks.EVoiceResult DecompressVoice(byte[] pCompressed, uint cbCompressed, byte[] pDestBuffer, uint cbDestBufferSize, out uint nBytesWritten, uint nDesiredSampleRate)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamUser_DecompressVoice(global::Steamworks.CSteamAPIContext.GetSteamUser(), pCompressed, cbCompressed, pDestBuffer, cbDestBufferSize, out nBytesWritten, nDesiredSampleRate);
		}

		public static uint GetVoiceOptimalSampleRate()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamUser_GetVoiceOptimalSampleRate(global::Steamworks.CSteamAPIContext.GetSteamUser());
		}

		public static global::Steamworks.HAuthTicket GetAuthSessionTicket(byte[] pTicket, int cbMaxTicket, out uint pcbTicket, ref global::Steamworks.SteamNetworkingIdentity pSteamNetworkingIdentity)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.HAuthTicket)global::Steamworks.NativeMethods.ISteamUser_GetAuthSessionTicket(global::Steamworks.CSteamAPIContext.GetSteamUser(), pTicket, cbMaxTicket, out pcbTicket, ref pSteamNetworkingIdentity);
		}

		public static global::Steamworks.HAuthTicket GetAuthTicketForWebApi(string pchIdentity)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchIdentity2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchIdentity);
			return (global::Steamworks.HAuthTicket)global::Steamworks.NativeMethods.ISteamUser_GetAuthTicketForWebApi(global::Steamworks.CSteamAPIContext.GetSteamUser(), pchIdentity2);
		}

		public static global::Steamworks.EBeginAuthSessionResult BeginAuthSession(byte[] pAuthTicket, int cbAuthTicket, global::Steamworks.CSteamID steamID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamUser_BeginAuthSession(global::Steamworks.CSteamAPIContext.GetSteamUser(), pAuthTicket, cbAuthTicket, steamID);
		}

		public static void EndAuthSession(global::Steamworks.CSteamID steamID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamUser_EndAuthSession(global::Steamworks.CSteamAPIContext.GetSteamUser(), steamID);
		}

		public static void CancelAuthTicket(global::Steamworks.HAuthTicket hAuthTicket)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamUser_CancelAuthTicket(global::Steamworks.CSteamAPIContext.GetSteamUser(), hAuthTicket);
		}

		public static global::Steamworks.EUserHasLicenseForAppResult UserHasLicenseForApp(global::Steamworks.CSteamID steamID, global::Steamworks.AppId_t appID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamUser_UserHasLicenseForApp(global::Steamworks.CSteamAPIContext.GetSteamUser(), steamID, appID);
		}

		public static bool BIsBehindNAT()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamUser_BIsBehindNAT(global::Steamworks.CSteamAPIContext.GetSteamUser());
		}

		public static void AdvertiseGame(global::Steamworks.CSteamID steamIDGameServer, uint unIPServer, ushort usPortServer)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamUser_AdvertiseGame(global::Steamworks.CSteamAPIContext.GetSteamUser(), steamIDGameServer, unIPServer, usPortServer);
		}

		public static global::Steamworks.SteamAPICall_t RequestEncryptedAppTicket(byte[] pDataToInclude, int cbDataToInclude)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamUser_RequestEncryptedAppTicket(global::Steamworks.CSteamAPIContext.GetSteamUser(), pDataToInclude, cbDataToInclude);
		}

		public static bool GetEncryptedAppTicket(byte[] pTicket, int cbMaxTicket, out uint pcbTicket)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamUser_GetEncryptedAppTicket(global::Steamworks.CSteamAPIContext.GetSteamUser(), pTicket, cbMaxTicket, out pcbTicket);
		}

		public static int GetGameBadgeLevel(int nSeries, bool bFoil)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamUser_GetGameBadgeLevel(global::Steamworks.CSteamAPIContext.GetSteamUser(), nSeries, bFoil);
		}

		public static int GetPlayerSteamLevel()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamUser_GetPlayerSteamLevel(global::Steamworks.CSteamAPIContext.GetSteamUser());
		}

		public static global::Steamworks.SteamAPICall_t RequestStoreAuthURL(string pchRedirectURL)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchRedirectURL2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchRedirectURL);
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamUser_RequestStoreAuthURL(global::Steamworks.CSteamAPIContext.GetSteamUser(), pchRedirectURL2);
		}

		public static bool BIsPhoneVerified()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamUser_BIsPhoneVerified(global::Steamworks.CSteamAPIContext.GetSteamUser());
		}

		public static bool BIsTwoFactorEnabled()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamUser_BIsTwoFactorEnabled(global::Steamworks.CSteamAPIContext.GetSteamUser());
		}

		public static bool BIsPhoneIdentifying()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamUser_BIsPhoneIdentifying(global::Steamworks.CSteamAPIContext.GetSteamUser());
		}

		public static bool BIsPhoneRequiringVerification()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamUser_BIsPhoneRequiringVerification(global::Steamworks.CSteamAPIContext.GetSteamUser());
		}

		public static global::Steamworks.SteamAPICall_t GetMarketEligibility()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamUser_GetMarketEligibility(global::Steamworks.CSteamAPIContext.GetSteamUser());
		}

		public static global::Steamworks.SteamAPICall_t GetDurationControl()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamUser_GetDurationControl(global::Steamworks.CSteamAPIContext.GetSteamUser());
		}

		public static bool BSetDurationControlOnlineState(global::Steamworks.EDurationControlOnlineState eNewState)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamUser_BSetDurationControlOnlineState(global::Steamworks.CSteamAPIContext.GetSteamUser(), eNewState);
		}
	}
}
