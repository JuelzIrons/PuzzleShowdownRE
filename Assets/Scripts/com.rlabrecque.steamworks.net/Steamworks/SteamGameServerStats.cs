namespace Steamworks
{
	public static class SteamGameServerStats
	{
		public static global::Steamworks.SteamAPICall_t RequestUserStats(global::Steamworks.CSteamID steamIDUser)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamGameServerStats_RequestUserStats(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServerStats(), steamIDUser);
		}

		public static bool GetUserStat(global::Steamworks.CSteamID steamIDUser, string pchName, out int pData)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchName);
			return global::Steamworks.NativeMethods.ISteamGameServerStats_GetUserStatInt32(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServerStats(), steamIDUser, pchName2, out pData);
		}

		public static bool GetUserStat(global::Steamworks.CSteamID steamIDUser, string pchName, out float pData)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchName);
			return global::Steamworks.NativeMethods.ISteamGameServerStats_GetUserStatFloat(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServerStats(), steamIDUser, pchName2, out pData);
		}

		public static bool GetUserAchievement(global::Steamworks.CSteamID steamIDUser, string pchName, out bool pbAchieved)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchName);
			return global::Steamworks.NativeMethods.ISteamGameServerStats_GetUserAchievement(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServerStats(), steamIDUser, pchName2, out pbAchieved);
		}

		public static bool SetUserStat(global::Steamworks.CSteamID steamIDUser, string pchName, int nData)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchName);
			return global::Steamworks.NativeMethods.ISteamGameServerStats_SetUserStatInt32(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServerStats(), steamIDUser, pchName2, nData);
		}

		public static bool SetUserStat(global::Steamworks.CSteamID steamIDUser, string pchName, float fData)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchName);
			return global::Steamworks.NativeMethods.ISteamGameServerStats_SetUserStatFloat(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServerStats(), steamIDUser, pchName2, fData);
		}

		public static bool UpdateUserAvgRateStat(global::Steamworks.CSteamID steamIDUser, string pchName, float flCountThisSession, double dSessionLength)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchName);
			return global::Steamworks.NativeMethods.ISteamGameServerStats_UpdateUserAvgRateStat(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServerStats(), steamIDUser, pchName2, flCountThisSession, dSessionLength);
		}

		public static bool SetUserAchievement(global::Steamworks.CSteamID steamIDUser, string pchName)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchName);
			return global::Steamworks.NativeMethods.ISteamGameServerStats_SetUserAchievement(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServerStats(), steamIDUser, pchName2);
		}

		public static bool ClearUserAchievement(global::Steamworks.CSteamID steamIDUser, string pchName)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchName);
			return global::Steamworks.NativeMethods.ISteamGameServerStats_ClearUserAchievement(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServerStats(), steamIDUser, pchName2);
		}

		public static global::Steamworks.SteamAPICall_t StoreUserStats(global::Steamworks.CSteamID steamIDUser)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamGameServerStats_StoreUserStats(global::Steamworks.CSteamGameServerAPIContext.GetSteamGameServerStats(), steamIDUser);
		}
	}
}
