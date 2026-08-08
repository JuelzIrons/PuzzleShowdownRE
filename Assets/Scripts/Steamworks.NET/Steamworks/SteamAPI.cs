namespace Steamworks
{
	public static class SteamAPI
	{
		public static global::Steamworks.ESteamAPIInitResult InitEx(out string OutSteamErrMsg)
		{
			global::Steamworks.InteropHelp.TestIfPlatformSupported();
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			stringBuilder.Append("SteamUtils010").Append("\0");
			stringBuilder.Append("SteamNetworkingUtils004").Append("\0");
			stringBuilder.Append("STEAMAPPS_INTERFACE_VERSION008").Append("\0");
			stringBuilder.Append("SteamFriends018").Append("\0");
			stringBuilder.Append("STEAMHTMLSURFACE_INTERFACE_VERSION_005").Append("\0");
			stringBuilder.Append("STEAMHTTP_INTERFACE_VERSION003").Append("\0");
			stringBuilder.Append("SteamInput006").Append("\0");
			stringBuilder.Append("STEAMINVENTORY_INTERFACE_V003").Append("\0");
			stringBuilder.Append("SteamMatchMakingServers002").Append("\0");
			stringBuilder.Append("SteamMatchMaking009").Append("\0");
			stringBuilder.Append("STEAMMUSIC_INTERFACE_VERSION001").Append("\0");
			stringBuilder.Append("SteamNetworkingMessages002").Append("\0");
			stringBuilder.Append("SteamNetworkingSockets012").Append("\0");
			stringBuilder.Append("SteamNetworking006").Append("\0");
			stringBuilder.Append("STEAMPARENTALSETTINGS_INTERFACE_VERSION001").Append("\0");
			stringBuilder.Append("SteamParties002").Append("\0");
			stringBuilder.Append("STEAMREMOTEPLAY_INTERFACE_VERSION003").Append("\0");
			stringBuilder.Append("STEAMREMOTESTORAGE_INTERFACE_VERSION016").Append("\0");
			stringBuilder.Append("STEAMSCREENSHOTS_INTERFACE_VERSION003").Append("\0");
			stringBuilder.Append("STEAMUGC_INTERFACE_VERSION021").Append("\0");
			stringBuilder.Append("STEAMUSERSTATS_INTERFACE_VERSION013").Append("\0");
			stringBuilder.Append("SteamUser023").Append("\0");
			stringBuilder.Append("STEAMVIDEO_INTERFACE_V007").Append("\0");
			using global::Steamworks.InteropHelp.UTF8StringHandle pszInternalCheckInterfaceVersions = new global::Steamworks.InteropHelp.UTF8StringHandle(stringBuilder.ToString());
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(1024);
			global::Steamworks.ESteamAPIInitResult eSteamAPIInitResult = global::Steamworks.NativeMethods.SteamInternal_SteamAPI_Init(pszInternalCheckInterfaceVersions, intPtr);
			OutSteamErrMsg = global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
			if (eSteamAPIInitResult == global::Steamworks.ESteamAPIInitResult.k_ESteamAPIInitResult_OK)
			{
				if (global::Steamworks.CSteamAPIContext.Init())
				{
					global::Steamworks.CallbackDispatcher.Initialize();
				}
				else
				{
					eSteamAPIInitResult = global::Steamworks.ESteamAPIInitResult.k_ESteamAPIInitResult_FailedGeneric;
					OutSteamErrMsg = "[Steamworks.NET] Failed to initialize CSteamAPIContext";
				}
			}
			return eSteamAPIInitResult;
		}

		public static bool Init()
		{
			global::Steamworks.InteropHelp.TestIfPlatformSupported();
			string OutSteamErrMsg;
			return InitEx(out OutSteamErrMsg) == global::Steamworks.ESteamAPIInitResult.k_ESteamAPIInitResult_OK;
		}

		public static void Shutdown()
		{
			global::Steamworks.InteropHelp.TestIfPlatformSupported();
			global::Steamworks.NativeMethods.SteamAPI_Shutdown();
			global::Steamworks.CSteamAPIContext.Clear();
			global::Steamworks.CallbackDispatcher.Shutdown();
		}

		public static bool RestartAppIfNecessary(global::Steamworks.AppId_t unOwnAppID)
		{
			global::Steamworks.InteropHelp.TestIfPlatformSupported();
			return global::Steamworks.NativeMethods.SteamAPI_RestartAppIfNecessary(unOwnAppID);
		}

		public static void ReleaseCurrentThreadMemory()
		{
			global::Steamworks.InteropHelp.TestIfPlatformSupported();
			global::Steamworks.NativeMethods.SteamAPI_ReleaseCurrentThreadMemory();
		}

		public static void RunCallbacks()
		{
			global::Steamworks.CallbackDispatcher.RunFrame(isGameServer: false);
		}

		public static bool IsSteamRunning()
		{
			global::Steamworks.InteropHelp.TestIfPlatformSupported();
			return global::Steamworks.NativeMethods.SteamAPI_IsSteamRunning();
		}

		public static global::Steamworks.HSteamPipe GetHSteamPipe()
		{
			global::Steamworks.InteropHelp.TestIfPlatformSupported();
			return (global::Steamworks.HSteamPipe)global::Steamworks.NativeMethods.SteamAPI_GetHSteamPipe();
		}

		public static global::Steamworks.HSteamUser GetHSteamUser()
		{
			global::Steamworks.InteropHelp.TestIfPlatformSupported();
			return (global::Steamworks.HSteamUser)global::Steamworks.NativeMethods.SteamAPI_GetHSteamUser();
		}
	}
}
