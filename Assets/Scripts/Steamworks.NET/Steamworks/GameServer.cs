namespace Steamworks
{
	public static class GameServer
	{
		public static global::Steamworks.ESteamAPIInitResult InitEx(uint unIP, ushort usGamePort, ushort usQueryPort, global::Steamworks.EServerMode eServerMode, string pchVersionString, out string OutSteamErrMsg)
		{
			global::Steamworks.InteropHelp.TestIfPlatformSupported();
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			stringBuilder.Append("SteamUtils010").Append('\0');
			stringBuilder.Append("SteamNetworkingUtils004").Append('\0');
			stringBuilder.Append("SteamGameServer015").Append('\0');
			stringBuilder.Append("SteamGameServerStats001").Append('\0');
			stringBuilder.Append("STEAMHTTP_INTERFACE_VERSION003").Append('\0');
			stringBuilder.Append("STEAMINVENTORY_INTERFACE_V003").Append('\0');
			stringBuilder.Append("SteamNetworking006").Append('\0');
			stringBuilder.Append("SteamNetworkingMessages002").Append('\0');
			stringBuilder.Append("SteamNetworkingSockets012").Append('\0');
			stringBuilder.Append("STEAMUGC_INTERFACE_VERSION021").Append('\0');
			using global::Steamworks.InteropHelp.UTF8StringHandle pchVersionString2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchVersionString);
			using global::Steamworks.InteropHelp.UTF8StringHandle pszInternalCheckInterfaceVersions = new global::Steamworks.InteropHelp.UTF8StringHandle(stringBuilder.ToString());
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(1024);
			global::Steamworks.ESteamAPIInitResult eSteamAPIInitResult = global::Steamworks.NativeMethods.SteamInternal_GameServer_Init_V2(unIP, usGamePort, usQueryPort, eServerMode, pchVersionString2, pszInternalCheckInterfaceVersions, intPtr);
			OutSteamErrMsg = global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
			if (eSteamAPIInitResult == global::Steamworks.ESteamAPIInitResult.k_ESteamAPIInitResult_OK)
			{
				if (global::Steamworks.CSteamGameServerAPIContext.Init())
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

		public static bool Init(uint unIP, ushort usGamePort, ushort usQueryPort, global::Steamworks.EServerMode eServerMode, string pchVersionString)
		{
			global::Steamworks.InteropHelp.TestIfPlatformSupported();
			string OutSteamErrMsg;
			return InitEx(unIP, usGamePort, usQueryPort, eServerMode, pchVersionString, out OutSteamErrMsg) == global::Steamworks.ESteamAPIInitResult.k_ESteamAPIInitResult_OK;
		}

		public static void Shutdown()
		{
			global::Steamworks.InteropHelp.TestIfPlatformSupported();
			global::Steamworks.NativeMethods.SteamGameServer_Shutdown();
			global::Steamworks.CSteamGameServerAPIContext.Clear();
			global::Steamworks.CallbackDispatcher.Shutdown();
		}

		public static void RunCallbacks()
		{
			global::Steamworks.CallbackDispatcher.RunFrame(isGameServer: true);
		}

		public static void ReleaseCurrentThreadMemory()
		{
			global::Steamworks.InteropHelp.TestIfPlatformSupported();
			global::Steamworks.NativeMethods.SteamGameServer_ReleaseCurrentThreadMemory();
		}

		public static bool BSecure()
		{
			global::Steamworks.InteropHelp.TestIfPlatformSupported();
			return global::Steamworks.NativeMethods.SteamGameServer_BSecure();
		}

		public static global::Steamworks.CSteamID GetSteamID()
		{
			global::Steamworks.InteropHelp.TestIfPlatformSupported();
			return (global::Steamworks.CSteamID)global::Steamworks.NativeMethods.SteamGameServer_GetSteamID();
		}

		public static global::Steamworks.HSteamPipe GetHSteamPipe()
		{
			global::Steamworks.InteropHelp.TestIfPlatformSupported();
			return (global::Steamworks.HSteamPipe)global::Steamworks.NativeMethods.SteamGameServer_GetHSteamPipe();
		}

		public static global::Steamworks.HSteamUser GetHSteamUser()
		{
			global::Steamworks.InteropHelp.TestIfPlatformSupported();
			return (global::Steamworks.HSteamUser)global::Steamworks.NativeMethods.SteamGameServer_GetHSteamUser();
		}
	}
}
