namespace Steamworks
{
	public static class SteamUserStats
	{
		public static bool GetStat(string pchName, out int pData)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchName);
			return global::Steamworks.NativeMethods.ISteamUserStats_GetStatInt32(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), pchName2, out pData);
		}

		public static bool GetStat(string pchName, out float pData)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchName);
			return global::Steamworks.NativeMethods.ISteamUserStats_GetStatFloat(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), pchName2, out pData);
		}

		public static bool SetStat(string pchName, int nData)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchName);
			return global::Steamworks.NativeMethods.ISteamUserStats_SetStatInt32(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), pchName2, nData);
		}

		public static bool SetStat(string pchName, float fData)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchName);
			return global::Steamworks.NativeMethods.ISteamUserStats_SetStatFloat(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), pchName2, fData);
		}

		public static bool UpdateAvgRateStat(string pchName, float flCountThisSession, double dSessionLength)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchName);
			return global::Steamworks.NativeMethods.ISteamUserStats_UpdateAvgRateStat(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), pchName2, flCountThisSession, dSessionLength);
		}

		public static bool GetAchievement(string pchName, out bool pbAchieved)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchName);
			return global::Steamworks.NativeMethods.ISteamUserStats_GetAchievement(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), pchName2, out pbAchieved);
		}

		public static bool SetAchievement(string pchName)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchName);
			return global::Steamworks.NativeMethods.ISteamUserStats_SetAchievement(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), pchName2);
		}

		public static bool ClearAchievement(string pchName)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchName);
			return global::Steamworks.NativeMethods.ISteamUserStats_ClearAchievement(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), pchName2);
		}

		public static bool GetAchievementAndUnlockTime(string pchName, out bool pbAchieved, out uint punUnlockTime)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchName);
			return global::Steamworks.NativeMethods.ISteamUserStats_GetAchievementAndUnlockTime(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), pchName2, out pbAchieved, out punUnlockTime);
		}

		public static bool StoreStats()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamUserStats_StoreStats(global::Steamworks.CSteamAPIContext.GetSteamUserStats());
		}

		public static int GetAchievementIcon(string pchName)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchName);
			return global::Steamworks.NativeMethods.ISteamUserStats_GetAchievementIcon(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), pchName2);
		}

		public static string GetAchievementDisplayAttribute(string pchName, string pchKey)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchName);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchKey2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchKey);
			return global::Steamworks.InteropHelp.PtrToStringUTF8(global::Steamworks.NativeMethods.ISteamUserStats_GetAchievementDisplayAttribute(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), pchName2, pchKey2));
		}

		public static bool IndicateAchievementProgress(string pchName, uint nCurProgress, uint nMaxProgress)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchName);
			return global::Steamworks.NativeMethods.ISteamUserStats_IndicateAchievementProgress(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), pchName2, nCurProgress, nMaxProgress);
		}

		public static uint GetNumAchievements()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamUserStats_GetNumAchievements(global::Steamworks.CSteamAPIContext.GetSteamUserStats());
		}

		public static string GetAchievementName(uint iAchievement)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.InteropHelp.PtrToStringUTF8(global::Steamworks.NativeMethods.ISteamUserStats_GetAchievementName(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), iAchievement));
		}

		public static global::Steamworks.SteamAPICall_t RequestUserStats(global::Steamworks.CSteamID steamIDUser)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamUserStats_RequestUserStats(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), steamIDUser);
		}

		public static bool GetUserStat(global::Steamworks.CSteamID steamIDUser, string pchName, out int pData)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchName);
			return global::Steamworks.NativeMethods.ISteamUserStats_GetUserStatInt32(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), steamIDUser, pchName2, out pData);
		}

		public static bool GetUserStat(global::Steamworks.CSteamID steamIDUser, string pchName, out float pData)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchName);
			return global::Steamworks.NativeMethods.ISteamUserStats_GetUserStatFloat(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), steamIDUser, pchName2, out pData);
		}

		public static bool GetUserAchievement(global::Steamworks.CSteamID steamIDUser, string pchName, out bool pbAchieved)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchName);
			return global::Steamworks.NativeMethods.ISteamUserStats_GetUserAchievement(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), steamIDUser, pchName2, out pbAchieved);
		}

		public static bool GetUserAchievementAndUnlockTime(global::Steamworks.CSteamID steamIDUser, string pchName, out bool pbAchieved, out uint punUnlockTime)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchName);
			return global::Steamworks.NativeMethods.ISteamUserStats_GetUserAchievementAndUnlockTime(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), steamIDUser, pchName2, out pbAchieved, out punUnlockTime);
		}

		public static bool ResetAllStats(bool bAchievementsToo)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamUserStats_ResetAllStats(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), bAchievementsToo);
		}

		public static global::Steamworks.SteamAPICall_t FindOrCreateLeaderboard(string pchLeaderboardName, global::Steamworks.ELeaderboardSortMethod eLeaderboardSortMethod, global::Steamworks.ELeaderboardDisplayType eLeaderboardDisplayType)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchLeaderboardName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchLeaderboardName);
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamUserStats_FindOrCreateLeaderboard(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), pchLeaderboardName2, eLeaderboardSortMethod, eLeaderboardDisplayType);
		}

		public static global::Steamworks.SteamAPICall_t FindLeaderboard(string pchLeaderboardName)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchLeaderboardName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchLeaderboardName);
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamUserStats_FindLeaderboard(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), pchLeaderboardName2);
		}

		public static string GetLeaderboardName(global::Steamworks.SteamLeaderboard_t hSteamLeaderboard)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.InteropHelp.PtrToStringUTF8(global::Steamworks.NativeMethods.ISteamUserStats_GetLeaderboardName(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), hSteamLeaderboard));
		}

		public static int GetLeaderboardEntryCount(global::Steamworks.SteamLeaderboard_t hSteamLeaderboard)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamUserStats_GetLeaderboardEntryCount(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), hSteamLeaderboard);
		}

		public static global::Steamworks.ELeaderboardSortMethod GetLeaderboardSortMethod(global::Steamworks.SteamLeaderboard_t hSteamLeaderboard)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamUserStats_GetLeaderboardSortMethod(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), hSteamLeaderboard);
		}

		public static global::Steamworks.ELeaderboardDisplayType GetLeaderboardDisplayType(global::Steamworks.SteamLeaderboard_t hSteamLeaderboard)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamUserStats_GetLeaderboardDisplayType(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), hSteamLeaderboard);
		}

		public static global::Steamworks.SteamAPICall_t DownloadLeaderboardEntries(global::Steamworks.SteamLeaderboard_t hSteamLeaderboard, global::Steamworks.ELeaderboardDataRequest eLeaderboardDataRequest, int nRangeStart, int nRangeEnd)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamUserStats_DownloadLeaderboardEntries(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), hSteamLeaderboard, eLeaderboardDataRequest, nRangeStart, nRangeEnd);
		}

		public static global::Steamworks.SteamAPICall_t DownloadLeaderboardEntriesForUsers(global::Steamworks.SteamLeaderboard_t hSteamLeaderboard, global::Steamworks.CSteamID[] prgUsers, int cUsers)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamUserStats_DownloadLeaderboardEntriesForUsers(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), hSteamLeaderboard, prgUsers, cUsers);
		}

		public static bool GetDownloadedLeaderboardEntry(global::Steamworks.SteamLeaderboardEntries_t hSteamLeaderboardEntries, int index, out global::Steamworks.LeaderboardEntry_t pLeaderboardEntry, int[] pDetails, int cDetailsMax)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamUserStats_GetDownloadedLeaderboardEntry(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), hSteamLeaderboardEntries, index, out pLeaderboardEntry, pDetails, cDetailsMax);
		}

		public static global::Steamworks.SteamAPICall_t UploadLeaderboardScore(global::Steamworks.SteamLeaderboard_t hSteamLeaderboard, global::Steamworks.ELeaderboardUploadScoreMethod eLeaderboardUploadScoreMethod, int nScore, int[] pScoreDetails, int cScoreDetailsCount)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamUserStats_UploadLeaderboardScore(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), hSteamLeaderboard, eLeaderboardUploadScoreMethod, nScore, pScoreDetails, cScoreDetailsCount);
		}

		public static global::Steamworks.SteamAPICall_t AttachLeaderboardUGC(global::Steamworks.SteamLeaderboard_t hSteamLeaderboard, global::Steamworks.UGCHandle_t hUGC)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamUserStats_AttachLeaderboardUGC(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), hSteamLeaderboard, hUGC);
		}

		public static global::Steamworks.SteamAPICall_t GetNumberOfCurrentPlayers()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamUserStats_GetNumberOfCurrentPlayers(global::Steamworks.CSteamAPIContext.GetSteamUserStats());
		}

		public static global::Steamworks.SteamAPICall_t RequestGlobalAchievementPercentages()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamUserStats_RequestGlobalAchievementPercentages(global::Steamworks.CSteamAPIContext.GetSteamUserStats());
		}

		public static int GetMostAchievedAchievementInfo(out string pchName, uint unNameBufLen, out float pflPercent, out bool pbAchieved)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal((int)unNameBufLen);
			int num = global::Steamworks.NativeMethods.ISteamUserStats_GetMostAchievedAchievementInfo(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), intPtr, unNameBufLen, out pflPercent, out pbAchieved);
			pchName = ((num != -1) ? global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr) : null);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
			return num;
		}

		public static int GetNextMostAchievedAchievementInfo(int iIteratorPrevious, out string pchName, uint unNameBufLen, out float pflPercent, out bool pbAchieved)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal((int)unNameBufLen);
			int num = global::Steamworks.NativeMethods.ISteamUserStats_GetNextMostAchievedAchievementInfo(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), iIteratorPrevious, intPtr, unNameBufLen, out pflPercent, out pbAchieved);
			pchName = ((num != -1) ? global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr) : null);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
			return num;
		}

		public static bool GetAchievementAchievedPercent(string pchName, out float pflPercent)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchName);
			return global::Steamworks.NativeMethods.ISteamUserStats_GetAchievementAchievedPercent(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), pchName2, out pflPercent);
		}

		public static global::Steamworks.SteamAPICall_t RequestGlobalStats(int nHistoryDays)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamUserStats_RequestGlobalStats(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), nHistoryDays);
		}

		public static bool GetGlobalStat(string pchStatName, out long pData)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchStatName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchStatName);
			return global::Steamworks.NativeMethods.ISteamUserStats_GetGlobalStatInt64(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), pchStatName2, out pData);
		}

		public static bool GetGlobalStat(string pchStatName, out double pData)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchStatName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchStatName);
			return global::Steamworks.NativeMethods.ISteamUserStats_GetGlobalStatDouble(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), pchStatName2, out pData);
		}

		public static int GetGlobalStatHistory(string pchStatName, long[] pData, uint cubData)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchStatName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchStatName);
			return global::Steamworks.NativeMethods.ISteamUserStats_GetGlobalStatHistoryInt64(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), pchStatName2, pData, cubData);
		}

		public static int GetGlobalStatHistory(string pchStatName, double[] pData, uint cubData)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchStatName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchStatName);
			return global::Steamworks.NativeMethods.ISteamUserStats_GetGlobalStatHistoryDouble(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), pchStatName2, pData, cubData);
		}

		public static bool GetAchievementProgressLimits(string pchName, out int pnMinProgress, out int pnMaxProgress)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchName);
			return global::Steamworks.NativeMethods.ISteamUserStats_GetAchievementProgressLimitsInt32(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), pchName2, out pnMinProgress, out pnMaxProgress);
		}

		public static bool GetAchievementProgressLimits(string pchName, out float pfMinProgress, out float pfMaxProgress)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchName);
			return global::Steamworks.NativeMethods.ISteamUserStats_GetAchievementProgressLimitsFloat(global::Steamworks.CSteamAPIContext.GetSteamUserStats(), pchName2, out pfMinProgress, out pfMaxProgress);
		}
	}
}
