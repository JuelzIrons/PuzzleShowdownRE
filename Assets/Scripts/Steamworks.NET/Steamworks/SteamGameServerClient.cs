namespace Steamworks
{
	public static class SteamGameServerClient
	{
		public static global::Steamworks.HSteamPipe CreateSteamPipe()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.HSteamPipe)global::Steamworks.NativeMethods.ISteamClient_CreateSteamPipe(global::Steamworks.CSteamGameServerAPIContext.GetSteamClient());
		}

		public static bool BReleaseSteamPipe(global::Steamworks.HSteamPipe hSteamPipe)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamClient_BReleaseSteamPipe(global::Steamworks.CSteamGameServerAPIContext.GetSteamClient(), hSteamPipe);
		}

		public static global::Steamworks.HSteamUser ConnectToGlobalUser(global::Steamworks.HSteamPipe hSteamPipe)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.HSteamUser)global::Steamworks.NativeMethods.ISteamClient_ConnectToGlobalUser(global::Steamworks.CSteamGameServerAPIContext.GetSteamClient(), hSteamPipe);
		}

		public static global::Steamworks.HSteamUser CreateLocalUser(out global::Steamworks.HSteamPipe phSteamPipe, global::Steamworks.EAccountType eAccountType)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.HSteamUser)global::Steamworks.NativeMethods.ISteamClient_CreateLocalUser(global::Steamworks.CSteamGameServerAPIContext.GetSteamClient(), out phSteamPipe, eAccountType);
		}

		public static void ReleaseUser(global::Steamworks.HSteamPipe hSteamPipe, global::Steamworks.HSteamUser hUser)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::Steamworks.NativeMethods.ISteamClient_ReleaseUser(global::Steamworks.CSteamGameServerAPIContext.GetSteamClient(), hSteamPipe, hUser);
		}

		public static global::System.IntPtr GetISteamUser(global::Steamworks.HSteamUser hSteamUser, global::Steamworks.HSteamPipe hSteamPipe, string pchVersion)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchVersion2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchVersion);
			return global::Steamworks.NativeMethods.ISteamClient_GetISteamUser(global::Steamworks.CSteamGameServerAPIContext.GetSteamClient(), hSteamUser, hSteamPipe, pchVersion2);
		}

		public static global::System.IntPtr GetISteamGameServer(global::Steamworks.HSteamUser hSteamUser, global::Steamworks.HSteamPipe hSteamPipe, string pchVersion)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchVersion2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchVersion);
			return global::Steamworks.NativeMethods.ISteamClient_GetISteamGameServer(global::Steamworks.CSteamGameServerAPIContext.GetSteamClient(), hSteamUser, hSteamPipe, pchVersion2);
		}

		public static void SetLocalIPBinding(ref global::Steamworks.SteamIPAddress_t unIP, ushort usPort)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::Steamworks.NativeMethods.ISteamClient_SetLocalIPBinding(global::Steamworks.CSteamGameServerAPIContext.GetSteamClient(), ref unIP, usPort);
		}

		public static global::System.IntPtr GetISteamFriends(global::Steamworks.HSteamUser hSteamUser, global::Steamworks.HSteamPipe hSteamPipe, string pchVersion)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchVersion2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchVersion);
			return global::Steamworks.NativeMethods.ISteamClient_GetISteamFriends(global::Steamworks.CSteamGameServerAPIContext.GetSteamClient(), hSteamUser, hSteamPipe, pchVersion2);
		}

		public static global::System.IntPtr GetISteamUtils(global::Steamworks.HSteamPipe hSteamPipe, string pchVersion)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchVersion2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchVersion);
			return global::Steamworks.NativeMethods.ISteamClient_GetISteamUtils(global::Steamworks.CSteamGameServerAPIContext.GetSteamClient(), hSteamPipe, pchVersion2);
		}

		public static global::System.IntPtr GetISteamMatchmaking(global::Steamworks.HSteamUser hSteamUser, global::Steamworks.HSteamPipe hSteamPipe, string pchVersion)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchVersion2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchVersion);
			return global::Steamworks.NativeMethods.ISteamClient_GetISteamMatchmaking(global::Steamworks.CSteamGameServerAPIContext.GetSteamClient(), hSteamUser, hSteamPipe, pchVersion2);
		}

		public static global::System.IntPtr GetISteamMatchmakingServers(global::Steamworks.HSteamUser hSteamUser, global::Steamworks.HSteamPipe hSteamPipe, string pchVersion)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchVersion2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchVersion);
			return global::Steamworks.NativeMethods.ISteamClient_GetISteamMatchmakingServers(global::Steamworks.CSteamGameServerAPIContext.GetSteamClient(), hSteamUser, hSteamPipe, pchVersion2);
		}

		public static global::System.IntPtr GetISteamGenericInterface(global::Steamworks.HSteamUser hSteamUser, global::Steamworks.HSteamPipe hSteamPipe, string pchVersion)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchVersion2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchVersion);
			return global::Steamworks.NativeMethods.ISteamClient_GetISteamGenericInterface(global::Steamworks.CSteamGameServerAPIContext.GetSteamClient(), hSteamUser, hSteamPipe, pchVersion2);
		}

		public static global::System.IntPtr GetISteamUserStats(global::Steamworks.HSteamUser hSteamUser, global::Steamworks.HSteamPipe hSteamPipe, string pchVersion)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchVersion2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchVersion);
			return global::Steamworks.NativeMethods.ISteamClient_GetISteamUserStats(global::Steamworks.CSteamGameServerAPIContext.GetSteamClient(), hSteamUser, hSteamPipe, pchVersion2);
		}

		public static global::System.IntPtr GetISteamGameServerStats(global::Steamworks.HSteamUser hSteamuser, global::Steamworks.HSteamPipe hSteamPipe, string pchVersion)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchVersion2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchVersion);
			return global::Steamworks.NativeMethods.ISteamClient_GetISteamGameServerStats(global::Steamworks.CSteamGameServerAPIContext.GetSteamClient(), hSteamuser, hSteamPipe, pchVersion2);
		}

		public static global::System.IntPtr GetISteamApps(global::Steamworks.HSteamUser hSteamUser, global::Steamworks.HSteamPipe hSteamPipe, string pchVersion)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchVersion2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchVersion);
			return global::Steamworks.NativeMethods.ISteamClient_GetISteamApps(global::Steamworks.CSteamGameServerAPIContext.GetSteamClient(), hSteamUser, hSteamPipe, pchVersion2);
		}

		public static global::System.IntPtr GetISteamNetworking(global::Steamworks.HSteamUser hSteamUser, global::Steamworks.HSteamPipe hSteamPipe, string pchVersion)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchVersion2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchVersion);
			return global::Steamworks.NativeMethods.ISteamClient_GetISteamNetworking(global::Steamworks.CSteamGameServerAPIContext.GetSteamClient(), hSteamUser, hSteamPipe, pchVersion2);
		}

		public static global::System.IntPtr GetISteamRemoteStorage(global::Steamworks.HSteamUser hSteamuser, global::Steamworks.HSteamPipe hSteamPipe, string pchVersion)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchVersion2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchVersion);
			return global::Steamworks.NativeMethods.ISteamClient_GetISteamRemoteStorage(global::Steamworks.CSteamGameServerAPIContext.GetSteamClient(), hSteamuser, hSteamPipe, pchVersion2);
		}

		public static global::System.IntPtr GetISteamScreenshots(global::Steamworks.HSteamUser hSteamuser, global::Steamworks.HSteamPipe hSteamPipe, string pchVersion)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchVersion2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchVersion);
			return global::Steamworks.NativeMethods.ISteamClient_GetISteamScreenshots(global::Steamworks.CSteamGameServerAPIContext.GetSteamClient(), hSteamuser, hSteamPipe, pchVersion2);
		}

		public static uint GetIPCCallCount()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamClient_GetIPCCallCount(global::Steamworks.CSteamGameServerAPIContext.GetSteamClient());
		}

		public static void SetWarningMessageHook(global::Steamworks.SteamAPIWarningMessageHook_t pFunction)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::Steamworks.NativeMethods.ISteamClient_SetWarningMessageHook(global::Steamworks.CSteamGameServerAPIContext.GetSteamClient(), pFunction);
		}

		public static bool BShutdownIfAllPipesClosed()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamClient_BShutdownIfAllPipesClosed(global::Steamworks.CSteamGameServerAPIContext.GetSteamClient());
		}

		public static global::System.IntPtr GetISteamHTTP(global::Steamworks.HSteamUser hSteamuser, global::Steamworks.HSteamPipe hSteamPipe, string pchVersion)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchVersion2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchVersion);
			return global::Steamworks.NativeMethods.ISteamClient_GetISteamHTTP(global::Steamworks.CSteamGameServerAPIContext.GetSteamClient(), hSteamuser, hSteamPipe, pchVersion2);
		}

		public static global::System.IntPtr GetISteamController(global::Steamworks.HSteamUser hSteamUser, global::Steamworks.HSteamPipe hSteamPipe, string pchVersion)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchVersion2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchVersion);
			return global::Steamworks.NativeMethods.ISteamClient_GetISteamController(global::Steamworks.CSteamGameServerAPIContext.GetSteamClient(), hSteamUser, hSteamPipe, pchVersion2);
		}

		public static global::System.IntPtr GetISteamUGC(global::Steamworks.HSteamUser hSteamUser, global::Steamworks.HSteamPipe hSteamPipe, string pchVersion)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchVersion2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchVersion);
			return global::Steamworks.NativeMethods.ISteamClient_GetISteamUGC(global::Steamworks.CSteamGameServerAPIContext.GetSteamClient(), hSteamUser, hSteamPipe, pchVersion2);
		}

		public static global::System.IntPtr GetISteamMusic(global::Steamworks.HSteamUser hSteamuser, global::Steamworks.HSteamPipe hSteamPipe, string pchVersion)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchVersion2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchVersion);
			return global::Steamworks.NativeMethods.ISteamClient_GetISteamMusic(global::Steamworks.CSteamGameServerAPIContext.GetSteamClient(), hSteamuser, hSteamPipe, pchVersion2);
		}

		public static global::System.IntPtr GetISteamHTMLSurface(global::Steamworks.HSteamUser hSteamuser, global::Steamworks.HSteamPipe hSteamPipe, string pchVersion)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchVersion2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchVersion);
			return global::Steamworks.NativeMethods.ISteamClient_GetISteamHTMLSurface(global::Steamworks.CSteamGameServerAPIContext.GetSteamClient(), hSteamuser, hSteamPipe, pchVersion2);
		}

		public static global::System.IntPtr GetISteamInventory(global::Steamworks.HSteamUser hSteamuser, global::Steamworks.HSteamPipe hSteamPipe, string pchVersion)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchVersion2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchVersion);
			return global::Steamworks.NativeMethods.ISteamClient_GetISteamInventory(global::Steamworks.CSteamGameServerAPIContext.GetSteamClient(), hSteamuser, hSteamPipe, pchVersion2);
		}

		public static global::System.IntPtr GetISteamVideo(global::Steamworks.HSteamUser hSteamuser, global::Steamworks.HSteamPipe hSteamPipe, string pchVersion)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchVersion2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchVersion);
			return global::Steamworks.NativeMethods.ISteamClient_GetISteamVideo(global::Steamworks.CSteamGameServerAPIContext.GetSteamClient(), hSteamuser, hSteamPipe, pchVersion2);
		}

		public static global::System.IntPtr GetISteamParentalSettings(global::Steamworks.HSteamUser hSteamuser, global::Steamworks.HSteamPipe hSteamPipe, string pchVersion)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchVersion2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchVersion);
			return global::Steamworks.NativeMethods.ISteamClient_GetISteamParentalSettings(global::Steamworks.CSteamGameServerAPIContext.GetSteamClient(), hSteamuser, hSteamPipe, pchVersion2);
		}

		public static global::System.IntPtr GetISteamInput(global::Steamworks.HSteamUser hSteamUser, global::Steamworks.HSteamPipe hSteamPipe, string pchVersion)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchVersion2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchVersion);
			return global::Steamworks.NativeMethods.ISteamClient_GetISteamInput(global::Steamworks.CSteamGameServerAPIContext.GetSteamClient(), hSteamUser, hSteamPipe, pchVersion2);
		}

		public static global::System.IntPtr GetISteamParties(global::Steamworks.HSteamUser hSteamUser, global::Steamworks.HSteamPipe hSteamPipe, string pchVersion)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchVersion2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchVersion);
			return global::Steamworks.NativeMethods.ISteamClient_GetISteamParties(global::Steamworks.CSteamGameServerAPIContext.GetSteamClient(), hSteamUser, hSteamPipe, pchVersion2);
		}

		public static global::System.IntPtr GetISteamRemotePlay(global::Steamworks.HSteamUser hSteamUser, global::Steamworks.HSteamPipe hSteamPipe, string pchVersion)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchVersion2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchVersion);
			return global::Steamworks.NativeMethods.ISteamClient_GetISteamRemotePlay(global::Steamworks.CSteamGameServerAPIContext.GetSteamClient(), hSteamUser, hSteamPipe, pchVersion2);
		}
	}
}
