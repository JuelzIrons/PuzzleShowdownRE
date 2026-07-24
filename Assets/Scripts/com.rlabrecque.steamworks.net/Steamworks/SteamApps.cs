namespace Steamworks
{
	public static class SteamApps
	{
		public static bool BIsSubscribed()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamApps_BIsSubscribed(global::Steamworks.CSteamAPIContext.GetSteamApps());
		}

		public static bool BIsLowViolence()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamApps_BIsLowViolence(global::Steamworks.CSteamAPIContext.GetSteamApps());
		}

		public static bool BIsCybercafe()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamApps_BIsCybercafe(global::Steamworks.CSteamAPIContext.GetSteamApps());
		}

		public static bool BIsVACBanned()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamApps_BIsVACBanned(global::Steamworks.CSteamAPIContext.GetSteamApps());
		}

		public static string GetCurrentGameLanguage()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.InteropHelp.PtrToStringUTF8(global::Steamworks.NativeMethods.ISteamApps_GetCurrentGameLanguage(global::Steamworks.CSteamAPIContext.GetSteamApps()));
		}

		public static string GetAvailableGameLanguages()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.InteropHelp.PtrToStringUTF8(global::Steamworks.NativeMethods.ISteamApps_GetAvailableGameLanguages(global::Steamworks.CSteamAPIContext.GetSteamApps()));
		}

		public static bool BIsSubscribedApp(global::Steamworks.AppId_t appID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamApps_BIsSubscribedApp(global::Steamworks.CSteamAPIContext.GetSteamApps(), appID);
		}

		public static bool BIsDlcInstalled(global::Steamworks.AppId_t appID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamApps_BIsDlcInstalled(global::Steamworks.CSteamAPIContext.GetSteamApps(), appID);
		}

		public static uint GetEarliestPurchaseUnixTime(global::Steamworks.AppId_t nAppID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamApps_GetEarliestPurchaseUnixTime(global::Steamworks.CSteamAPIContext.GetSteamApps(), nAppID);
		}

		public static bool BIsSubscribedFromFreeWeekend()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamApps_BIsSubscribedFromFreeWeekend(global::Steamworks.CSteamAPIContext.GetSteamApps());
		}

		public static int GetDLCCount()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamApps_GetDLCCount(global::Steamworks.CSteamAPIContext.GetSteamApps());
		}

		public static bool BGetDLCDataByIndex(int iDLC, out global::Steamworks.AppId_t pAppID, out bool pbAvailable, out string pchName, int cchNameBufferSize)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(cchNameBufferSize);
			bool flag = global::Steamworks.NativeMethods.ISteamApps_BGetDLCDataByIndex(global::Steamworks.CSteamAPIContext.GetSteamApps(), iDLC, out pAppID, out pbAvailable, intPtr, cchNameBufferSize);
			pchName = (flag ? global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr) : null);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		public static void InstallDLC(global::Steamworks.AppId_t nAppID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamApps_InstallDLC(global::Steamworks.CSteamAPIContext.GetSteamApps(), nAppID);
		}

		public static void UninstallDLC(global::Steamworks.AppId_t nAppID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamApps_UninstallDLC(global::Steamworks.CSteamAPIContext.GetSteamApps(), nAppID);
		}

		public static void RequestAppProofOfPurchaseKey(global::Steamworks.AppId_t nAppID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamApps_RequestAppProofOfPurchaseKey(global::Steamworks.CSteamAPIContext.GetSteamApps(), nAppID);
		}

		public static bool GetCurrentBetaName(out string pchName, int cchNameBufferSize)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(cchNameBufferSize);
			bool flag = global::Steamworks.NativeMethods.ISteamApps_GetCurrentBetaName(global::Steamworks.CSteamAPIContext.GetSteamApps(), intPtr, cchNameBufferSize);
			pchName = (flag ? global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr) : null);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		public static bool MarkContentCorrupt(bool bMissingFilesOnly)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamApps_MarkContentCorrupt(global::Steamworks.CSteamAPIContext.GetSteamApps(), bMissingFilesOnly);
		}

		public static uint GetInstalledDepots(global::Steamworks.AppId_t appID, global::Steamworks.DepotId_t[] pvecDepots, uint cMaxDepots)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamApps_GetInstalledDepots(global::Steamworks.CSteamAPIContext.GetSteamApps(), appID, pvecDepots, cMaxDepots);
		}

		public static uint GetAppInstallDir(global::Steamworks.AppId_t appID, out string pchFolder, uint cchFolderBufferSize)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal((int)cchFolderBufferSize);
			uint num = global::Steamworks.NativeMethods.ISteamApps_GetAppInstallDir(global::Steamworks.CSteamAPIContext.GetSteamApps(), appID, intPtr, cchFolderBufferSize);
			pchFolder = ((num != 0) ? global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr) : null);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
			return num;
		}

		public static bool BIsAppInstalled(global::Steamworks.AppId_t appID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamApps_BIsAppInstalled(global::Steamworks.CSteamAPIContext.GetSteamApps(), appID);
		}

		public static global::Steamworks.CSteamID GetAppOwner()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.CSteamID)global::Steamworks.NativeMethods.ISteamApps_GetAppOwner(global::Steamworks.CSteamAPIContext.GetSteamApps());
		}

		public static string GetLaunchQueryParam(string pchKey)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchKey2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchKey);
			return global::Steamworks.InteropHelp.PtrToStringUTF8(global::Steamworks.NativeMethods.ISteamApps_GetLaunchQueryParam(global::Steamworks.CSteamAPIContext.GetSteamApps(), pchKey2));
		}

		public static bool GetDlcDownloadProgress(global::Steamworks.AppId_t nAppID, out ulong punBytesDownloaded, out ulong punBytesTotal)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamApps_GetDlcDownloadProgress(global::Steamworks.CSteamAPIContext.GetSteamApps(), nAppID, out punBytesDownloaded, out punBytesTotal);
		}

		public static int GetAppBuildId()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamApps_GetAppBuildId(global::Steamworks.CSteamAPIContext.GetSteamApps());
		}

		public static void RequestAllProofOfPurchaseKeys()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamApps_RequestAllProofOfPurchaseKeys(global::Steamworks.CSteamAPIContext.GetSteamApps());
		}

		public static global::Steamworks.SteamAPICall_t GetFileDetails(string pszFileName)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pszFileName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pszFileName);
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamApps_GetFileDetails(global::Steamworks.CSteamAPIContext.GetSteamApps(), pszFileName2);
		}

		public static int GetLaunchCommandLine(out string pszCommandLine, int cubCommandLine)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(cubCommandLine);
			int num = global::Steamworks.NativeMethods.ISteamApps_GetLaunchCommandLine(global::Steamworks.CSteamAPIContext.GetSteamApps(), intPtr, cubCommandLine);
			pszCommandLine = ((num != -1) ? global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr) : null);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
			return num;
		}

		public static bool BIsSubscribedFromFamilySharing()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamApps_BIsSubscribedFromFamilySharing(global::Steamworks.CSteamAPIContext.GetSteamApps());
		}

		public static bool BIsTimedTrial(out uint punSecondsAllowed, out uint punSecondsPlayed)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamApps_BIsTimedTrial(global::Steamworks.CSteamAPIContext.GetSteamApps(), out punSecondsAllowed, out punSecondsPlayed);
		}

		public static bool SetDlcContext(global::Steamworks.AppId_t nAppID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamApps_SetDlcContext(global::Steamworks.CSteamAPIContext.GetSteamApps(), nAppID);
		}

		public static int GetNumBetas(out int pnAvailable, out int pnPrivate)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamApps_GetNumBetas(global::Steamworks.CSteamAPIContext.GetSteamApps(), out pnAvailable, out pnPrivate);
		}

		public static bool GetBetaInfo(int iBetaIndex, out uint punFlags, out uint punBuildID, out string pchBetaName, int cchBetaName, out string pchDescription, int cchDescription)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(cchBetaName);
			global::System.IntPtr intPtr2 = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(cchDescription);
			bool flag = global::Steamworks.NativeMethods.ISteamApps_GetBetaInfo(global::Steamworks.CSteamAPIContext.GetSteamApps(), iBetaIndex, out punFlags, out punBuildID, intPtr, cchBetaName, intPtr2, cchDescription);
			pchBetaName = (flag ? global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr) : null);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
			pchDescription = (flag ? global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr2) : null);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr2);
			return flag;
		}

		public static bool SetActiveBeta(string pchBetaName)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchBetaName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchBetaName);
			return global::Steamworks.NativeMethods.ISteamApps_SetActiveBeta(global::Steamworks.CSteamAPIContext.GetSteamApps(), pchBetaName2);
		}
	}
}
