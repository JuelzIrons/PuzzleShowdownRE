namespace Steamworks
{
	[global::System.Security.SuppressUnmanagedCodeSecurity]
	internal static class NativeMethods
	{
		internal const string NativeLibraryName = "steam_api64";

		internal const string NativeLibrary_SDKEncryptedAppTicket = "sdkencryptedappticket64";

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern global::Steamworks.ESteamAPIInitResult SteamInternal_SteamAPI_Init(global::Steamworks.InteropHelp.UTF8StringHandle pszInternalCheckInterfaceVersions, global::System.IntPtr pOutErrMsg);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern void SteamAPI_Shutdown();

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool SteamAPI_RestartAppIfNecessary(global::Steamworks.AppId_t unOwnAppID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern void SteamAPI_ReleaseCurrentThreadMemory();

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern void SteamAPI_WriteMiniDump(uint uStructuredExceptionCode, global::System.IntPtr pvExceptionInfo, uint uBuildID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern void SteamAPI_SetMiniDumpComment(global::Steamworks.InteropHelp.UTF8StringHandle pchMsg);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern void SteamAPI_RunCallbacks();

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern void SteamAPI_RegisterCallback(global::System.IntPtr pCallback, int iCallback);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern void SteamAPI_UnregisterCallback(global::System.IntPtr pCallback);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern void SteamAPI_RegisterCallResult(global::System.IntPtr pCallback, ulong hAPICall);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern void SteamAPI_UnregisterCallResult(global::System.IntPtr pCallback, ulong hAPICall);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool SteamAPI_IsSteamRunning();

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern int SteamAPI_GetSteamInstallPath();

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern int SteamAPI_GetHSteamPipe();

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern void SteamAPI_SetTryCatchCallbacks([global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bTryCatchCallbacks);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern int SteamAPI_GetHSteamUser();

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern global::System.IntPtr SteamInternal_ContextInit(global::System.IntPtr pContextInitData);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern global::System.IntPtr SteamInternal_CreateInterface(global::Steamworks.InteropHelp.UTF8StringHandle ver);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern global::System.IntPtr SteamInternal_FindOrCreateUserInterface(global::Steamworks.HSteamUser hSteamUser, global::Steamworks.InteropHelp.UTF8StringHandle pszVersion);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern global::System.IntPtr SteamInternal_FindOrCreateGameServerInterface(global::Steamworks.HSteamUser hSteamUser, global::Steamworks.InteropHelp.UTF8StringHandle pszVersion);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern void SteamAPI_UseBreakpadCrashHandler(global::Steamworks.InteropHelp.UTF8StringHandle pchVersion, global::Steamworks.InteropHelp.UTF8StringHandle pchDate, global::Steamworks.InteropHelp.UTF8StringHandle pchTime, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bFullMemoryDumps, global::System.IntPtr pvContext, global::System.IntPtr m_pfnPreMinidumpCallback);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern void SteamAPI_SetBreakpadAppID(uint unAppID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern void SteamAPI_ManualDispatch_Init();

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern void SteamAPI_ManualDispatch_RunFrame(global::Steamworks.HSteamPipe hSteamPipe);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool SteamAPI_ManualDispatch_GetNextCallback(global::Steamworks.HSteamPipe hSteamPipe, global::System.IntPtr pCallbackMsg);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern void SteamAPI_ManualDispatch_FreeLastCallback(global::Steamworks.HSteamPipe hSteamPipe);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool SteamAPI_ManualDispatch_GetAPICallResult(global::Steamworks.HSteamPipe hSteamPipe, global::Steamworks.SteamAPICall_t hSteamAPICall, global::System.IntPtr pCallback, int cubCallback, int iCallbackExpected, out bool pbFailed);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern void SteamGameServer_Shutdown();

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern void SteamGameServer_RunCallbacks();

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern void SteamGameServer_ReleaseCurrentThreadMemory();

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool SteamGameServer_BSecure();

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern ulong SteamGameServer_GetSteamID();

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern int SteamGameServer_GetHSteamPipe();

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern int SteamGameServer_GetHSteamUser();

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern global::Steamworks.ESteamAPIInitResult SteamInternal_GameServer_Init_V2(uint unIP, ushort usGamePort, ushort usQueryPort, global::Steamworks.EServerMode eServerMode, global::Steamworks.InteropHelp.UTF8StringHandle pchVersionString, global::Steamworks.InteropHelp.UTF8StringHandle pszInternalCheckInterfaceVersions, global::System.IntPtr pOutErrMsg);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern global::System.IntPtr SteamClient();

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern global::System.IntPtr SteamGameServerClient();

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern void SteamAPI_SteamNetworkingIPAddr_Clear(ref global::Steamworks.SteamNetworkingIPAddr self);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool SteamAPI_SteamNetworkingIPAddr_IsIPv6AllZeros(ref global::Steamworks.SteamNetworkingIPAddr self);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern void SteamAPI_SteamNetworkingIPAddr_SetIPv6(ref global::Steamworks.SteamNetworkingIPAddr self, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] byte[] ipv6, ushort nPort);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern void SteamAPI_SteamNetworkingIPAddr_SetIPv4(ref global::Steamworks.SteamNetworkingIPAddr self, uint nIP, ushort nPort);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool SteamAPI_SteamNetworkingIPAddr_IsIPv4(ref global::Steamworks.SteamNetworkingIPAddr self);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern uint SteamAPI_SteamNetworkingIPAddr_GetIPv4(ref global::Steamworks.SteamNetworkingIPAddr self);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern void SteamAPI_SteamNetworkingIPAddr_SetIPv6LocalHost(ref global::Steamworks.SteamNetworkingIPAddr self, ushort nPort);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool SteamAPI_SteamNetworkingIPAddr_IsLocalHost(ref global::Steamworks.SteamNetworkingIPAddr self);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern void SteamAPI_SteamNetworkingIPAddr_ToString(ref global::Steamworks.SteamNetworkingIPAddr self, global::System.IntPtr buf, uint cbBuf, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bWithPort);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool SteamAPI_SteamNetworkingIPAddr_ParseString(ref global::Steamworks.SteamNetworkingIPAddr self, global::Steamworks.InteropHelp.UTF8StringHandle pszStr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool SteamAPI_SteamNetworkingIPAddr_IsEqualTo(ref global::Steamworks.SteamNetworkingIPAddr self, ref global::Steamworks.SteamNetworkingIPAddr x);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern global::Steamworks.ESteamNetworkingFakeIPType SteamAPI_SteamNetworkingIPAddr_GetFakeIPType(ref global::Steamworks.SteamNetworkingIPAddr self);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool SteamAPI_SteamNetworkingIPAddr_IsFakeIP(ref global::Steamworks.SteamNetworkingIPAddr self);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern void SteamAPI_SteamNetworkingIdentity_Clear(ref global::Steamworks.SteamNetworkingIdentity self);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool SteamAPI_SteamNetworkingIdentity_IsInvalid(ref global::Steamworks.SteamNetworkingIdentity self);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern void SteamAPI_SteamNetworkingIdentity_SetSteamID(ref global::Steamworks.SteamNetworkingIdentity self, ulong steamID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern ulong SteamAPI_SteamNetworkingIdentity_GetSteamID(ref global::Steamworks.SteamNetworkingIdentity self);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern void SteamAPI_SteamNetworkingIdentity_SetSteamID64(ref global::Steamworks.SteamNetworkingIdentity self, ulong steamID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern ulong SteamAPI_SteamNetworkingIdentity_GetSteamID64(ref global::Steamworks.SteamNetworkingIdentity self);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool SteamAPI_SteamNetworkingIdentity_SetXboxPairwiseID(ref global::Steamworks.SteamNetworkingIdentity self, global::Steamworks.InteropHelp.UTF8StringHandle pszString);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern global::System.IntPtr SteamAPI_SteamNetworkingIdentity_GetXboxPairwiseID(ref global::Steamworks.SteamNetworkingIdentity self);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern void SteamAPI_SteamNetworkingIdentity_SetPSNID(ref global::Steamworks.SteamNetworkingIdentity self, ulong id);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern ulong SteamAPI_SteamNetworkingIdentity_GetPSNID(ref global::Steamworks.SteamNetworkingIdentity self);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern global::System.IntPtr SteamAPI_SteamNetworkingIdentity_SetIPAddr(ref global::Steamworks.SteamNetworkingIdentity self, ref global::Steamworks.SteamNetworkingIPAddr addr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern global::System.IntPtr SteamAPI_SteamNetworkingIdentity_GetIPAddr(ref global::Steamworks.SteamNetworkingIdentity self);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern void SteamAPI_SteamNetworkingIdentity_SetIPv4Addr(ref global::Steamworks.SteamNetworkingIdentity self, uint nIPv4, ushort nPort);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern uint SteamAPI_SteamNetworkingIdentity_GetIPv4(ref global::Steamworks.SteamNetworkingIdentity self);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern global::Steamworks.ESteamNetworkingFakeIPType SteamAPI_SteamNetworkingIdentity_GetFakeIPType(ref global::Steamworks.SteamNetworkingIdentity self);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool SteamAPI_SteamNetworkingIdentity_IsFakeIP(ref global::Steamworks.SteamNetworkingIdentity self);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern void SteamAPI_SteamNetworkingIdentity_SetLocalHost(ref global::Steamworks.SteamNetworkingIdentity self);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool SteamAPI_SteamNetworkingIdentity_IsLocalHost(ref global::Steamworks.SteamNetworkingIdentity self);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool SteamAPI_SteamNetworkingIdentity_SetGenericString(ref global::Steamworks.SteamNetworkingIdentity self, global::Steamworks.InteropHelp.UTF8StringHandle pszString);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern global::System.IntPtr SteamAPI_SteamNetworkingIdentity_GetGenericString(ref global::Steamworks.SteamNetworkingIdentity self);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool SteamAPI_SteamNetworkingIdentity_SetGenericBytes(ref global::Steamworks.SteamNetworkingIdentity self, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] byte[] data, uint cbLen);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern global::System.IntPtr SteamAPI_SteamNetworkingIdentity_GetGenericBytes(ref global::Steamworks.SteamNetworkingIdentity self, out int cbLen);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool SteamAPI_SteamNetworkingIdentity_IsEqualTo(ref global::Steamworks.SteamNetworkingIdentity self, ref global::Steamworks.SteamNetworkingIdentity x);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern void SteamAPI_SteamNetworkingIdentity_ToString(ref global::Steamworks.SteamNetworkingIdentity self, global::System.IntPtr buf, uint cbBuf);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool SteamAPI_SteamNetworkingIdentity_ParseString(ref global::Steamworks.SteamNetworkingIdentity self, global::Steamworks.InteropHelp.UTF8StringHandle pszStr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern void SteamAPI_SteamNetworkingMessage_t_Release(global::System.IntPtr self);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool SteamAPI_ISteamNetworkingConnectionSignaling_SendSignal(ref global::Steamworks.ISteamNetworkingConnectionSignaling self, global::Steamworks.HSteamNetConnection hConn, ref global::Steamworks.SteamNetConnectionInfo_t info, global::System.IntPtr pMsg, int cbMsg);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern void SteamAPI_ISteamNetworkingConnectionSignaling_Release(ref global::Steamworks.ISteamNetworkingConnectionSignaling self);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern global::System.IntPtr SteamAPI_ISteamNetworkingSignalingRecvContext_OnConnectRequest(ref global::Steamworks.ISteamNetworkingSignalingRecvContext self, global::Steamworks.HSteamNetConnection hConn, ref global::Steamworks.SteamNetworkingIdentity identityPeer, int nLocalVirtualPort);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern void SteamAPI_ISteamNetworkingSignalingRecvContext_SendRejectionSignal(ref global::Steamworks.ISteamNetworkingSignalingRecvContext self, ref global::Steamworks.SteamNetworkingIdentity identityPeer, global::System.IntPtr pMsg, int cbMsg);

		[global::System.Runtime.InteropServices.DllImport("sdkencryptedappticket64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool SteamEncryptedAppTicket_BDecryptTicket([global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] byte[] rgubTicketEncrypted, uint cubTicketEncrypted, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] byte[] rgubTicketDecrypted, ref uint pcubTicketDecrypted, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.LPArray, SizeConst = 32)] byte[] rgubKey, int cubKey);

		[global::System.Runtime.InteropServices.DllImport("sdkencryptedappticket64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool SteamEncryptedAppTicket_BIsTicketForApp([global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] byte[] rgubTicketDecrypted, uint cubTicketDecrypted, global::Steamworks.AppId_t nAppID);

		[global::System.Runtime.InteropServices.DllImport("sdkencryptedappticket64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern uint SteamEncryptedAppTicket_GetTicketIssueTime([global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] byte[] rgubTicketDecrypted, uint cubTicketDecrypted);

		[global::System.Runtime.InteropServices.DllImport("sdkencryptedappticket64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern void SteamEncryptedAppTicket_GetTicketSteamID([global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] byte[] rgubTicketDecrypted, uint cubTicketDecrypted, out global::Steamworks.CSteamID psteamID);

		[global::System.Runtime.InteropServices.DllImport("sdkencryptedappticket64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern uint SteamEncryptedAppTicket_GetTicketAppID([global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] byte[] rgubTicketDecrypted, uint cubTicketDecrypted);

		[global::System.Runtime.InteropServices.DllImport("sdkencryptedappticket64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool SteamEncryptedAppTicket_BUserOwnsAppInTicket([global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] byte[] rgubTicketDecrypted, uint cubTicketDecrypted, global::Steamworks.AppId_t nAppID);

		[global::System.Runtime.InteropServices.DllImport("sdkencryptedappticket64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool SteamEncryptedAppTicket_BUserIsVacBanned([global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] byte[] rgubTicketDecrypted, uint cubTicketDecrypted);

		[global::System.Runtime.InteropServices.DllImport("sdkencryptedappticket64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public static extern global::System.IntPtr SteamEncryptedAppTicket_GetUserVariableData([global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] byte[] rgubTicketDecrypted, uint cubTicketDecrypted, out uint pcubUserData);

		[global::System.Runtime.InteropServices.DllImport("sdkencryptedappticket64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool SteamEncryptedAppTicket_BIsTicketSigned([global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] byte[] rgubTicketDecrypted, uint cubTicketDecrypted, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] byte[] pubRSAKey, uint cubRSAKey);

		[global::System.Runtime.InteropServices.DllImport("sdkencryptedappticket64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool SteamEncryptedAppTicket_BIsLicenseBorrowed([global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] byte[] rgubTicketDecrypted, uint cubTicketDecrypted);

		[global::System.Runtime.InteropServices.DllImport("sdkencryptedappticket64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool SteamEncryptedAppTicket_BIsLicenseTemporary([global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] byte[] rgubTicketDecrypted, uint cubTicketDecrypted);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_BIsSubscribed")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamApps_BIsSubscribed(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_BIsLowViolence")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamApps_BIsLowViolence(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_BIsCybercafe")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamApps_BIsCybercafe(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_BIsVACBanned")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamApps_BIsVACBanned(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetCurrentGameLanguage")]
		public static extern global::System.IntPtr ISteamApps_GetCurrentGameLanguage(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetAvailableGameLanguages")]
		public static extern global::System.IntPtr ISteamApps_GetAvailableGameLanguages(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_BIsSubscribedApp")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamApps_BIsSubscribedApp(global::System.IntPtr instancePtr, global::Steamworks.AppId_t appID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_BIsDlcInstalled")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamApps_BIsDlcInstalled(global::System.IntPtr instancePtr, global::Steamworks.AppId_t appID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetEarliestPurchaseUnixTime")]
		public static extern uint ISteamApps_GetEarliestPurchaseUnixTime(global::System.IntPtr instancePtr, global::Steamworks.AppId_t nAppID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_BIsSubscribedFromFreeWeekend")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamApps_BIsSubscribedFromFreeWeekend(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetDLCCount")]
		public static extern int ISteamApps_GetDLCCount(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_BGetDLCDataByIndex")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamApps_BGetDLCDataByIndex(global::System.IntPtr instancePtr, int iDLC, out global::Steamworks.AppId_t pAppID, out bool pbAvailable, global::System.IntPtr pchName, int cchNameBufferSize);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_InstallDLC")]
		public static extern void ISteamApps_InstallDLC(global::System.IntPtr instancePtr, global::Steamworks.AppId_t nAppID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_UninstallDLC")]
		public static extern void ISteamApps_UninstallDLC(global::System.IntPtr instancePtr, global::Steamworks.AppId_t nAppID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_RequestAppProofOfPurchaseKey")]
		public static extern void ISteamApps_RequestAppProofOfPurchaseKey(global::System.IntPtr instancePtr, global::Steamworks.AppId_t nAppID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetCurrentBetaName")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamApps_GetCurrentBetaName(global::System.IntPtr instancePtr, global::System.IntPtr pchName, int cchNameBufferSize);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_MarkContentCorrupt")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamApps_MarkContentCorrupt(global::System.IntPtr instancePtr, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bMissingFilesOnly);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetInstalledDepots")]
		public static extern uint ISteamApps_GetInstalledDepots(global::System.IntPtr instancePtr, global::Steamworks.AppId_t appID, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.DepotId_t[] pvecDepots, uint cMaxDepots);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetAppInstallDir")]
		public static extern uint ISteamApps_GetAppInstallDir(global::System.IntPtr instancePtr, global::Steamworks.AppId_t appID, global::System.IntPtr pchFolder, uint cchFolderBufferSize);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_BIsAppInstalled")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamApps_BIsAppInstalled(global::System.IntPtr instancePtr, global::Steamworks.AppId_t appID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetAppOwner")]
		public static extern ulong ISteamApps_GetAppOwner(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetLaunchQueryParam")]
		public static extern global::System.IntPtr ISteamApps_GetLaunchQueryParam(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchKey);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetDlcDownloadProgress")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamApps_GetDlcDownloadProgress(global::System.IntPtr instancePtr, global::Steamworks.AppId_t nAppID, out ulong punBytesDownloaded, out ulong punBytesTotal);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetAppBuildId")]
		public static extern int ISteamApps_GetAppBuildId(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_RequestAllProofOfPurchaseKeys")]
		public static extern void ISteamApps_RequestAllProofOfPurchaseKeys(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetFileDetails")]
		public static extern ulong ISteamApps_GetFileDetails(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pszFileName);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetLaunchCommandLine")]
		public static extern int ISteamApps_GetLaunchCommandLine(global::System.IntPtr instancePtr, global::System.IntPtr pszCommandLine, int cubCommandLine);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_BIsSubscribedFromFamilySharing")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamApps_BIsSubscribedFromFamilySharing(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_BIsTimedTrial")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamApps_BIsTimedTrial(global::System.IntPtr instancePtr, out uint punSecondsAllowed, out uint punSecondsPlayed);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_SetDlcContext")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamApps_SetDlcContext(global::System.IntPtr instancePtr, global::Steamworks.AppId_t nAppID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetNumBetas")]
		public static extern int ISteamApps_GetNumBetas(global::System.IntPtr instancePtr, out int pnAvailable, out int pnPrivate);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetBetaInfo")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamApps_GetBetaInfo(global::System.IntPtr instancePtr, int iBetaIndex, out uint punFlags, out uint punBuildID, global::System.IntPtr pchBetaName, int cchBetaName, global::System.IntPtr pchDescription, int cchDescription);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_SetActiveBeta")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamApps_SetActiveBeta(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchBetaName);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_CreateSteamPipe")]
		public static extern int ISteamClient_CreateSteamPipe(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_BReleaseSteamPipe")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamClient_BReleaseSteamPipe(global::System.IntPtr instancePtr, global::Steamworks.HSteamPipe hSteamPipe);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_ConnectToGlobalUser")]
		public static extern int ISteamClient_ConnectToGlobalUser(global::System.IntPtr instancePtr, global::Steamworks.HSteamPipe hSteamPipe);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_CreateLocalUser")]
		public static extern int ISteamClient_CreateLocalUser(global::System.IntPtr instancePtr, out global::Steamworks.HSteamPipe phSteamPipe, global::Steamworks.EAccountType eAccountType);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_ReleaseUser")]
		public static extern void ISteamClient_ReleaseUser(global::System.IntPtr instancePtr, global::Steamworks.HSteamPipe hSteamPipe, global::Steamworks.HSteamUser hUser);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamUser")]
		public static extern global::System.IntPtr ISteamClient_GetISteamUser(global::System.IntPtr instancePtr, global::Steamworks.HSteamUser hSteamUser, global::Steamworks.HSteamPipe hSteamPipe, global::Steamworks.InteropHelp.UTF8StringHandle pchVersion);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamGameServer")]
		public static extern global::System.IntPtr ISteamClient_GetISteamGameServer(global::System.IntPtr instancePtr, global::Steamworks.HSteamUser hSteamUser, global::Steamworks.HSteamPipe hSteamPipe, global::Steamworks.InteropHelp.UTF8StringHandle pchVersion);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_SetLocalIPBinding")]
		public static extern void ISteamClient_SetLocalIPBinding(global::System.IntPtr instancePtr, ref global::Steamworks.SteamIPAddress_t unIP, ushort usPort);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamFriends")]
		public static extern global::System.IntPtr ISteamClient_GetISteamFriends(global::System.IntPtr instancePtr, global::Steamworks.HSteamUser hSteamUser, global::Steamworks.HSteamPipe hSteamPipe, global::Steamworks.InteropHelp.UTF8StringHandle pchVersion);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamUtils")]
		public static extern global::System.IntPtr ISteamClient_GetISteamUtils(global::System.IntPtr instancePtr, global::Steamworks.HSteamPipe hSteamPipe, global::Steamworks.InteropHelp.UTF8StringHandle pchVersion);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamMatchmaking")]
		public static extern global::System.IntPtr ISteamClient_GetISteamMatchmaking(global::System.IntPtr instancePtr, global::Steamworks.HSteamUser hSteamUser, global::Steamworks.HSteamPipe hSteamPipe, global::Steamworks.InteropHelp.UTF8StringHandle pchVersion);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamMatchmakingServers")]
		public static extern global::System.IntPtr ISteamClient_GetISteamMatchmakingServers(global::System.IntPtr instancePtr, global::Steamworks.HSteamUser hSteamUser, global::Steamworks.HSteamPipe hSteamPipe, global::Steamworks.InteropHelp.UTF8StringHandle pchVersion);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamGenericInterface")]
		public static extern global::System.IntPtr ISteamClient_GetISteamGenericInterface(global::System.IntPtr instancePtr, global::Steamworks.HSteamUser hSteamUser, global::Steamworks.HSteamPipe hSteamPipe, global::Steamworks.InteropHelp.UTF8StringHandle pchVersion);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamUserStats")]
		public static extern global::System.IntPtr ISteamClient_GetISteamUserStats(global::System.IntPtr instancePtr, global::Steamworks.HSteamUser hSteamUser, global::Steamworks.HSteamPipe hSteamPipe, global::Steamworks.InteropHelp.UTF8StringHandle pchVersion);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamGameServerStats")]
		public static extern global::System.IntPtr ISteamClient_GetISteamGameServerStats(global::System.IntPtr instancePtr, global::Steamworks.HSteamUser hSteamuser, global::Steamworks.HSteamPipe hSteamPipe, global::Steamworks.InteropHelp.UTF8StringHandle pchVersion);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamApps")]
		public static extern global::System.IntPtr ISteamClient_GetISteamApps(global::System.IntPtr instancePtr, global::Steamworks.HSteamUser hSteamUser, global::Steamworks.HSteamPipe hSteamPipe, global::Steamworks.InteropHelp.UTF8StringHandle pchVersion);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamNetworking")]
		public static extern global::System.IntPtr ISteamClient_GetISteamNetworking(global::System.IntPtr instancePtr, global::Steamworks.HSteamUser hSteamUser, global::Steamworks.HSteamPipe hSteamPipe, global::Steamworks.InteropHelp.UTF8StringHandle pchVersion);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamRemoteStorage")]
		public static extern global::System.IntPtr ISteamClient_GetISteamRemoteStorage(global::System.IntPtr instancePtr, global::Steamworks.HSteamUser hSteamuser, global::Steamworks.HSteamPipe hSteamPipe, global::Steamworks.InteropHelp.UTF8StringHandle pchVersion);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamScreenshots")]
		public static extern global::System.IntPtr ISteamClient_GetISteamScreenshots(global::System.IntPtr instancePtr, global::Steamworks.HSteamUser hSteamuser, global::Steamworks.HSteamPipe hSteamPipe, global::Steamworks.InteropHelp.UTF8StringHandle pchVersion);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetIPCCallCount")]
		public static extern uint ISteamClient_GetIPCCallCount(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_SetWarningMessageHook")]
		public static extern void ISteamClient_SetWarningMessageHook(global::System.IntPtr instancePtr, global::Steamworks.SteamAPIWarningMessageHook_t pFunction);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_BShutdownIfAllPipesClosed")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamClient_BShutdownIfAllPipesClosed(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamHTTP")]
		public static extern global::System.IntPtr ISteamClient_GetISteamHTTP(global::System.IntPtr instancePtr, global::Steamworks.HSteamUser hSteamuser, global::Steamworks.HSteamPipe hSteamPipe, global::Steamworks.InteropHelp.UTF8StringHandle pchVersion);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamController")]
		public static extern global::System.IntPtr ISteamClient_GetISteamController(global::System.IntPtr instancePtr, global::Steamworks.HSteamUser hSteamUser, global::Steamworks.HSteamPipe hSteamPipe, global::Steamworks.InteropHelp.UTF8StringHandle pchVersion);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamUGC")]
		public static extern global::System.IntPtr ISteamClient_GetISteamUGC(global::System.IntPtr instancePtr, global::Steamworks.HSteamUser hSteamUser, global::Steamworks.HSteamPipe hSteamPipe, global::Steamworks.InteropHelp.UTF8StringHandle pchVersion);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamMusic")]
		public static extern global::System.IntPtr ISteamClient_GetISteamMusic(global::System.IntPtr instancePtr, global::Steamworks.HSteamUser hSteamuser, global::Steamworks.HSteamPipe hSteamPipe, global::Steamworks.InteropHelp.UTF8StringHandle pchVersion);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamHTMLSurface")]
		public static extern global::System.IntPtr ISteamClient_GetISteamHTMLSurface(global::System.IntPtr instancePtr, global::Steamworks.HSteamUser hSteamuser, global::Steamworks.HSteamPipe hSteamPipe, global::Steamworks.InteropHelp.UTF8StringHandle pchVersion);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamInventory")]
		public static extern global::System.IntPtr ISteamClient_GetISteamInventory(global::System.IntPtr instancePtr, global::Steamworks.HSteamUser hSteamuser, global::Steamworks.HSteamPipe hSteamPipe, global::Steamworks.InteropHelp.UTF8StringHandle pchVersion);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamVideo")]
		public static extern global::System.IntPtr ISteamClient_GetISteamVideo(global::System.IntPtr instancePtr, global::Steamworks.HSteamUser hSteamuser, global::Steamworks.HSteamPipe hSteamPipe, global::Steamworks.InteropHelp.UTF8StringHandle pchVersion);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamParentalSettings")]
		public static extern global::System.IntPtr ISteamClient_GetISteamParentalSettings(global::System.IntPtr instancePtr, global::Steamworks.HSteamUser hSteamuser, global::Steamworks.HSteamPipe hSteamPipe, global::Steamworks.InteropHelp.UTF8StringHandle pchVersion);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamInput")]
		public static extern global::System.IntPtr ISteamClient_GetISteamInput(global::System.IntPtr instancePtr, global::Steamworks.HSteamUser hSteamUser, global::Steamworks.HSteamPipe hSteamPipe, global::Steamworks.InteropHelp.UTF8StringHandle pchVersion);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamParties")]
		public static extern global::System.IntPtr ISteamClient_GetISteamParties(global::System.IntPtr instancePtr, global::Steamworks.HSteamUser hSteamUser, global::Steamworks.HSteamPipe hSteamPipe, global::Steamworks.InteropHelp.UTF8StringHandle pchVersion);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamRemotePlay")]
		public static extern global::System.IntPtr ISteamClient_GetISteamRemotePlay(global::System.IntPtr instancePtr, global::Steamworks.HSteamUser hSteamUser, global::Steamworks.HSteamPipe hSteamPipe, global::Steamworks.InteropHelp.UTF8StringHandle pchVersion);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetPersonaName")]
		public static extern global::System.IntPtr ISteamFriends_GetPersonaName(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetPersonaState")]
		public static extern global::Steamworks.EPersonaState ISteamFriends_GetPersonaState(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendCount")]
		public static extern int ISteamFriends_GetFriendCount(global::System.IntPtr instancePtr, global::Steamworks.EFriendFlags iFriendFlags);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendByIndex")]
		public static extern ulong ISteamFriends_GetFriendByIndex(global::System.IntPtr instancePtr, int iFriend, global::Steamworks.EFriendFlags iFriendFlags);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendRelationship")]
		public static extern global::Steamworks.EFriendRelationship ISteamFriends_GetFriendRelationship(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDFriend);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendPersonaState")]
		public static extern global::Steamworks.EPersonaState ISteamFriends_GetFriendPersonaState(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDFriend);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendPersonaName")]
		public static extern global::System.IntPtr ISteamFriends_GetFriendPersonaName(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDFriend);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendGamePlayed")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamFriends_GetFriendGamePlayed(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDFriend, out global::Steamworks.FriendGameInfo_t pFriendGameInfo);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendPersonaNameHistory")]
		public static extern global::System.IntPtr ISteamFriends_GetFriendPersonaNameHistory(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDFriend, int iPersonaName);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendSteamLevel")]
		public static extern int ISteamFriends_GetFriendSteamLevel(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDFriend);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetPlayerNickname")]
		public static extern global::System.IntPtr ISteamFriends_GetPlayerNickname(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDPlayer);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendsGroupCount")]
		public static extern int ISteamFriends_GetFriendsGroupCount(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendsGroupIDByIndex")]
		public static extern short ISteamFriends_GetFriendsGroupIDByIndex(global::System.IntPtr instancePtr, int iFG);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendsGroupName")]
		public static extern global::System.IntPtr ISteamFriends_GetFriendsGroupName(global::System.IntPtr instancePtr, global::Steamworks.FriendsGroupID_t friendsGroupID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendsGroupMembersCount")]
		public static extern int ISteamFriends_GetFriendsGroupMembersCount(global::System.IntPtr instancePtr, global::Steamworks.FriendsGroupID_t friendsGroupID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendsGroupMembersList")]
		public static extern void ISteamFriends_GetFriendsGroupMembersList(global::System.IntPtr instancePtr, global::Steamworks.FriendsGroupID_t friendsGroupID, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.CSteamID[] pOutSteamIDMembers, int nMembersCount);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_HasFriend")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamFriends_HasFriend(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDFriend, global::Steamworks.EFriendFlags iFriendFlags);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetClanCount")]
		public static extern int ISteamFriends_GetClanCount(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetClanByIndex")]
		public static extern ulong ISteamFriends_GetClanByIndex(global::System.IntPtr instancePtr, int iClan);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetClanName")]
		public static extern global::System.IntPtr ISteamFriends_GetClanName(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDClan);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetClanTag")]
		public static extern global::System.IntPtr ISteamFriends_GetClanTag(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDClan);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetClanActivityCounts")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamFriends_GetClanActivityCounts(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDClan, out int pnOnline, out int pnInGame, out int pnChatting);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_DownloadClanActivityCounts")]
		public static extern ulong ISteamFriends_DownloadClanActivityCounts(global::System.IntPtr instancePtr, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.CSteamID[] psteamIDClans, int cClansToRequest);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendCountFromSource")]
		public static extern int ISteamFriends_GetFriendCountFromSource(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDSource);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendFromSourceByIndex")]
		public static extern ulong ISteamFriends_GetFriendFromSourceByIndex(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDSource, int iFriend);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_IsUserInSource")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamFriends_IsUserInSource(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDUser, global::Steamworks.CSteamID steamIDSource);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_SetInGameVoiceSpeaking")]
		public static extern void ISteamFriends_SetInGameVoiceSpeaking(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDUser, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bSpeaking);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_ActivateGameOverlay")]
		public static extern void ISteamFriends_ActivateGameOverlay(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchDialog);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_ActivateGameOverlayToUser")]
		public static extern void ISteamFriends_ActivateGameOverlayToUser(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchDialog, global::Steamworks.CSteamID steamID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_ActivateGameOverlayToWebPage")]
		public static extern void ISteamFriends_ActivateGameOverlayToWebPage(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchURL, global::Steamworks.EActivateGameOverlayToWebPageMode eMode);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_ActivateGameOverlayToStore")]
		public static extern void ISteamFriends_ActivateGameOverlayToStore(global::System.IntPtr instancePtr, global::Steamworks.AppId_t nAppID, global::Steamworks.EOverlayToStoreFlag eFlag);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_SetPlayedWith")]
		public static extern void ISteamFriends_SetPlayedWith(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDUserPlayedWith);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_ActivateGameOverlayInviteDialog")]
		public static extern void ISteamFriends_ActivateGameOverlayInviteDialog(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDLobby);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetSmallFriendAvatar")]
		public static extern int ISteamFriends_GetSmallFriendAvatar(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDFriend);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetMediumFriendAvatar")]
		public static extern int ISteamFriends_GetMediumFriendAvatar(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDFriend);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetLargeFriendAvatar")]
		public static extern int ISteamFriends_GetLargeFriendAvatar(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDFriend);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_RequestUserInformation")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamFriends_RequestUserInformation(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDUser, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bRequireNameOnly);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_RequestClanOfficerList")]
		public static extern ulong ISteamFriends_RequestClanOfficerList(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDClan);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetClanOwner")]
		public static extern ulong ISteamFriends_GetClanOwner(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDClan);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetClanOfficerCount")]
		public static extern int ISteamFriends_GetClanOfficerCount(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDClan);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetClanOfficerByIndex")]
		public static extern ulong ISteamFriends_GetClanOfficerByIndex(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDClan, int iOfficer);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_SetRichPresence")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamFriends_SetRichPresence(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchKey, global::Steamworks.InteropHelp.UTF8StringHandle pchValue);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_ClearRichPresence")]
		public static extern void ISteamFriends_ClearRichPresence(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendRichPresence")]
		public static extern global::System.IntPtr ISteamFriends_GetFriendRichPresence(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDFriend, global::Steamworks.InteropHelp.UTF8StringHandle pchKey);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendRichPresenceKeyCount")]
		public static extern int ISteamFriends_GetFriendRichPresenceKeyCount(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDFriend);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendRichPresenceKeyByIndex")]
		public static extern global::System.IntPtr ISteamFriends_GetFriendRichPresenceKeyByIndex(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDFriend, int iKey);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_RequestFriendRichPresence")]
		public static extern void ISteamFriends_RequestFriendRichPresence(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDFriend);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_InviteUserToGame")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamFriends_InviteUserToGame(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDFriend, global::Steamworks.InteropHelp.UTF8StringHandle pchConnectString);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetCoplayFriendCount")]
		public static extern int ISteamFriends_GetCoplayFriendCount(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetCoplayFriend")]
		public static extern ulong ISteamFriends_GetCoplayFriend(global::System.IntPtr instancePtr, int iCoplayFriend);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendCoplayTime")]
		public static extern int ISteamFriends_GetFriendCoplayTime(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDFriend);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendCoplayGame")]
		public static extern uint ISteamFriends_GetFriendCoplayGame(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDFriend);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_JoinClanChatRoom")]
		public static extern ulong ISteamFriends_JoinClanChatRoom(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDClan);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_LeaveClanChatRoom")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamFriends_LeaveClanChatRoom(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDClan);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetClanChatMemberCount")]
		public static extern int ISteamFriends_GetClanChatMemberCount(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDClan);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetChatMemberByIndex")]
		public static extern ulong ISteamFriends_GetChatMemberByIndex(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDClan, int iUser);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_SendClanChatMessage")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamFriends_SendClanChatMessage(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDClanChat, global::Steamworks.InteropHelp.UTF8StringHandle pchText);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetClanChatMessage")]
		public static extern int ISteamFriends_GetClanChatMessage(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDClanChat, int iMessage, global::System.IntPtr prgchText, int cchTextMax, out global::Steamworks.EChatEntryType peChatEntryType, out global::Steamworks.CSteamID psteamidChatter);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_IsClanChatAdmin")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamFriends_IsClanChatAdmin(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDClanChat, global::Steamworks.CSteamID steamIDUser);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_IsClanChatWindowOpenInSteam")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamFriends_IsClanChatWindowOpenInSteam(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDClanChat);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_OpenClanChatWindowInSteam")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamFriends_OpenClanChatWindowInSteam(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDClanChat);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_CloseClanChatWindowInSteam")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamFriends_CloseClanChatWindowInSteam(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDClanChat);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_SetListenForFriendsMessages")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamFriends_SetListenForFriendsMessages(global::System.IntPtr instancePtr, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bInterceptEnabled);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_ReplyToFriendMessage")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamFriends_ReplyToFriendMessage(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDFriend, global::Steamworks.InteropHelp.UTF8StringHandle pchMsgToSend);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendMessage")]
		public static extern int ISteamFriends_GetFriendMessage(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDFriend, int iMessageID, global::System.IntPtr pvData, int cubData, out global::Steamworks.EChatEntryType peChatEntryType);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFollowerCount")]
		public static extern ulong ISteamFriends_GetFollowerCount(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_IsFollowing")]
		public static extern ulong ISteamFriends_IsFollowing(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_EnumerateFollowingList")]
		public static extern ulong ISteamFriends_EnumerateFollowingList(global::System.IntPtr instancePtr, uint unStartIndex);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_IsClanPublic")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamFriends_IsClanPublic(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDClan);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_IsClanOfficialGameGroup")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamFriends_IsClanOfficialGameGroup(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDClan);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetNumChatsWithUnreadPriorityMessages")]
		public static extern int ISteamFriends_GetNumChatsWithUnreadPriorityMessages(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_ActivateGameOverlayRemotePlayTogetherInviteDialog")]
		public static extern void ISteamFriends_ActivateGameOverlayRemotePlayTogetherInviteDialog(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDLobby);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_RegisterProtocolInOverlayBrowser")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamFriends_RegisterProtocolInOverlayBrowser(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchProtocol);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_ActivateGameOverlayInviteDialogConnectString")]
		public static extern void ISteamFriends_ActivateGameOverlayInviteDialogConnectString(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchConnectString);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_RequestEquippedProfileItems")]
		public static extern ulong ISteamFriends_RequestEquippedProfileItems(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_BHasEquippedProfileItem")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamFriends_BHasEquippedProfileItem(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamID, global::Steamworks.ECommunityProfileItemType itemType);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetProfileItemPropertyString")]
		public static extern global::System.IntPtr ISteamFriends_GetProfileItemPropertyString(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamID, global::Steamworks.ECommunityProfileItemType itemType, global::Steamworks.ECommunityProfileItemProperty prop);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetProfileItemPropertyUint")]
		public static extern uint ISteamFriends_GetProfileItemPropertyUint(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamID, global::Steamworks.ECommunityProfileItemType itemType, global::Steamworks.ECommunityProfileItemProperty prop);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetProduct")]
		public static extern void ISteamGameServer_SetProduct(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pszProduct);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetGameDescription")]
		public static extern void ISteamGameServer_SetGameDescription(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pszGameDescription);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetModDir")]
		public static extern void ISteamGameServer_SetModDir(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pszModDir);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetDedicatedServer")]
		public static extern void ISteamGameServer_SetDedicatedServer(global::System.IntPtr instancePtr, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bDedicated);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_LogOn")]
		public static extern void ISteamGameServer_LogOn(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pszToken);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_LogOnAnonymous")]
		public static extern void ISteamGameServer_LogOnAnonymous(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_LogOff")]
		public static extern void ISteamGameServer_LogOff(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_BLoggedOn")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamGameServer_BLoggedOn(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_BSecure")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamGameServer_BSecure(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_GetSteamID")]
		public static extern ulong ISteamGameServer_GetSteamID(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_WasRestartRequested")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamGameServer_WasRestartRequested(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetMaxPlayerCount")]
		public static extern void ISteamGameServer_SetMaxPlayerCount(global::System.IntPtr instancePtr, int cPlayersMax);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetBotPlayerCount")]
		public static extern void ISteamGameServer_SetBotPlayerCount(global::System.IntPtr instancePtr, int cBotplayers);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetServerName")]
		public static extern void ISteamGameServer_SetServerName(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pszServerName);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetMapName")]
		public static extern void ISteamGameServer_SetMapName(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pszMapName);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetPasswordProtected")]
		public static extern void ISteamGameServer_SetPasswordProtected(global::System.IntPtr instancePtr, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bPasswordProtected);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetSpectatorPort")]
		public static extern void ISteamGameServer_SetSpectatorPort(global::System.IntPtr instancePtr, ushort unSpectatorPort);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetSpectatorServerName")]
		public static extern void ISteamGameServer_SetSpectatorServerName(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pszSpectatorServerName);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_ClearAllKeyValues")]
		public static extern void ISteamGameServer_ClearAllKeyValues(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetKeyValue")]
		public static extern void ISteamGameServer_SetKeyValue(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pKey, global::Steamworks.InteropHelp.UTF8StringHandle pValue);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetGameTags")]
		public static extern void ISteamGameServer_SetGameTags(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchGameTags);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetGameData")]
		public static extern void ISteamGameServer_SetGameData(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchGameData);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetRegion")]
		public static extern void ISteamGameServer_SetRegion(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pszRegion);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetAdvertiseServerActive")]
		public static extern void ISteamGameServer_SetAdvertiseServerActive(global::System.IntPtr instancePtr, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bActive);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_GetAuthSessionTicket")]
		public static extern uint ISteamGameServer_GetAuthSessionTicket(global::System.IntPtr instancePtr, byte[] pTicket, int cbMaxTicket, out uint pcbTicket, ref global::Steamworks.SteamNetworkingIdentity pSnid);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_BeginAuthSession")]
		public static extern global::Steamworks.EBeginAuthSessionResult ISteamGameServer_BeginAuthSession(global::System.IntPtr instancePtr, byte[] pAuthTicket, int cbAuthTicket, global::Steamworks.CSteamID steamID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_EndAuthSession")]
		public static extern void ISteamGameServer_EndAuthSession(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_CancelAuthTicket")]
		public static extern void ISteamGameServer_CancelAuthTicket(global::System.IntPtr instancePtr, global::Steamworks.HAuthTicket hAuthTicket);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_UserHasLicenseForApp")]
		public static extern global::Steamworks.EUserHasLicenseForAppResult ISteamGameServer_UserHasLicenseForApp(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamID, global::Steamworks.AppId_t appID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_RequestUserGroupStatus")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamGameServer_RequestUserGroupStatus(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDUser, global::Steamworks.CSteamID steamIDGroup);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_GetGameplayStats")]
		public static extern void ISteamGameServer_GetGameplayStats(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_GetServerReputation")]
		public static extern ulong ISteamGameServer_GetServerReputation(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_GetPublicIP")]
		public static extern global::Steamworks.SteamIPAddress_t ISteamGameServer_GetPublicIP(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_HandleIncomingPacket")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamGameServer_HandleIncomingPacket(global::System.IntPtr instancePtr, byte[] pData, int cbData, uint srcIP, ushort srcPort);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_GetNextOutgoingPacket")]
		public static extern int ISteamGameServer_GetNextOutgoingPacket(global::System.IntPtr instancePtr, byte[] pOut, int cbMaxOut, out uint pNetAdr, out ushort pPort);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_AssociateWithClan")]
		public static extern ulong ISteamGameServer_AssociateWithClan(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDClan);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_ComputeNewPlayerCompatibility")]
		public static extern ulong ISteamGameServer_ComputeNewPlayerCompatibility(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDNewPlayer);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SendUserConnectAndAuthenticate_DEPRECATED")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamGameServer_SendUserConnectAndAuthenticate_DEPRECATED(global::System.IntPtr instancePtr, uint unIPClient, byte[] pvAuthBlob, uint cubAuthBlobSize, out global::Steamworks.CSteamID pSteamIDUser);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_CreateUnauthenticatedUserConnection")]
		public static extern ulong ISteamGameServer_CreateUnauthenticatedUserConnection(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SendUserDisconnect_DEPRECATED")]
		public static extern void ISteamGameServer_SendUserDisconnect_DEPRECATED(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDUser);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_BUpdateUserData")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamGameServer_BUpdateUserData(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDUser, global::Steamworks.InteropHelp.UTF8StringHandle pchPlayerName, uint uScore);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServerStats_RequestUserStats")]
		public static extern ulong ISteamGameServerStats_RequestUserStats(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDUser);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServerStats_GetUserStatInt32")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamGameServerStats_GetUserStatInt32(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDUser, global::Steamworks.InteropHelp.UTF8StringHandle pchName, out int pData);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServerStats_GetUserStatFloat")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamGameServerStats_GetUserStatFloat(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDUser, global::Steamworks.InteropHelp.UTF8StringHandle pchName, out float pData);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServerStats_GetUserAchievement")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamGameServerStats_GetUserAchievement(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDUser, global::Steamworks.InteropHelp.UTF8StringHandle pchName, out bool pbAchieved);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServerStats_SetUserStatInt32")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamGameServerStats_SetUserStatInt32(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDUser, global::Steamworks.InteropHelp.UTF8StringHandle pchName, int nData);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServerStats_SetUserStatFloat")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamGameServerStats_SetUserStatFloat(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDUser, global::Steamworks.InteropHelp.UTF8StringHandle pchName, float fData);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServerStats_UpdateUserAvgRateStat")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamGameServerStats_UpdateUserAvgRateStat(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDUser, global::Steamworks.InteropHelp.UTF8StringHandle pchName, float flCountThisSession, double dSessionLength);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServerStats_SetUserAchievement")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamGameServerStats_SetUserAchievement(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDUser, global::Steamworks.InteropHelp.UTF8StringHandle pchName);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServerStats_ClearUserAchievement")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamGameServerStats_ClearUserAchievement(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDUser, global::Steamworks.InteropHelp.UTF8StringHandle pchName);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServerStats_StoreUserStats")]
		public static extern ulong ISteamGameServerStats_StoreUserStats(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDUser);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_Init")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamHTMLSurface_Init(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_Shutdown")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamHTMLSurface_Shutdown(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_CreateBrowser")]
		public static extern ulong ISteamHTMLSurface_CreateBrowser(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchUserAgent, global::Steamworks.InteropHelp.UTF8StringHandle pchUserCSS);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_RemoveBrowser")]
		public static extern void ISteamHTMLSurface_RemoveBrowser(global::System.IntPtr instancePtr, global::Steamworks.HHTMLBrowser unBrowserHandle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_LoadURL")]
		public static extern void ISteamHTMLSurface_LoadURL(global::System.IntPtr instancePtr, global::Steamworks.HHTMLBrowser unBrowserHandle, global::Steamworks.InteropHelp.UTF8StringHandle pchURL, global::Steamworks.InteropHelp.UTF8StringHandle pchPostData);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_SetSize")]
		public static extern void ISteamHTMLSurface_SetSize(global::System.IntPtr instancePtr, global::Steamworks.HHTMLBrowser unBrowserHandle, uint unWidth, uint unHeight);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_StopLoad")]
		public static extern void ISteamHTMLSurface_StopLoad(global::System.IntPtr instancePtr, global::Steamworks.HHTMLBrowser unBrowserHandle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_Reload")]
		public static extern void ISteamHTMLSurface_Reload(global::System.IntPtr instancePtr, global::Steamworks.HHTMLBrowser unBrowserHandle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_GoBack")]
		public static extern void ISteamHTMLSurface_GoBack(global::System.IntPtr instancePtr, global::Steamworks.HHTMLBrowser unBrowserHandle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_GoForward")]
		public static extern void ISteamHTMLSurface_GoForward(global::System.IntPtr instancePtr, global::Steamworks.HHTMLBrowser unBrowserHandle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_AddHeader")]
		public static extern void ISteamHTMLSurface_AddHeader(global::System.IntPtr instancePtr, global::Steamworks.HHTMLBrowser unBrowserHandle, global::Steamworks.InteropHelp.UTF8StringHandle pchKey, global::Steamworks.InteropHelp.UTF8StringHandle pchValue);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_ExecuteJavascript")]
		public static extern void ISteamHTMLSurface_ExecuteJavascript(global::System.IntPtr instancePtr, global::Steamworks.HHTMLBrowser unBrowserHandle, global::Steamworks.InteropHelp.UTF8StringHandle pchScript);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_MouseUp")]
		public static extern void ISteamHTMLSurface_MouseUp(global::System.IntPtr instancePtr, global::Steamworks.HHTMLBrowser unBrowserHandle, global::Steamworks.EHTMLMouseButton eMouseButton);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_MouseDown")]
		public static extern void ISteamHTMLSurface_MouseDown(global::System.IntPtr instancePtr, global::Steamworks.HHTMLBrowser unBrowserHandle, global::Steamworks.EHTMLMouseButton eMouseButton);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_MouseDoubleClick")]
		public static extern void ISteamHTMLSurface_MouseDoubleClick(global::System.IntPtr instancePtr, global::Steamworks.HHTMLBrowser unBrowserHandle, global::Steamworks.EHTMLMouseButton eMouseButton);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_MouseMove")]
		public static extern void ISteamHTMLSurface_MouseMove(global::System.IntPtr instancePtr, global::Steamworks.HHTMLBrowser unBrowserHandle, int x, int y);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_MouseWheel")]
		public static extern void ISteamHTMLSurface_MouseWheel(global::System.IntPtr instancePtr, global::Steamworks.HHTMLBrowser unBrowserHandle, int nDelta);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_KeyDown")]
		public static extern void ISteamHTMLSurface_KeyDown(global::System.IntPtr instancePtr, global::Steamworks.HHTMLBrowser unBrowserHandle, uint nNativeKeyCode, global::Steamworks.EHTMLKeyModifiers eHTMLKeyModifiers, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bIsSystemKey);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_KeyUp")]
		public static extern void ISteamHTMLSurface_KeyUp(global::System.IntPtr instancePtr, global::Steamworks.HHTMLBrowser unBrowserHandle, uint nNativeKeyCode, global::Steamworks.EHTMLKeyModifiers eHTMLKeyModifiers);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_KeyChar")]
		public static extern void ISteamHTMLSurface_KeyChar(global::System.IntPtr instancePtr, global::Steamworks.HHTMLBrowser unBrowserHandle, uint cUnicodeChar, global::Steamworks.EHTMLKeyModifiers eHTMLKeyModifiers);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_SetHorizontalScroll")]
		public static extern void ISteamHTMLSurface_SetHorizontalScroll(global::System.IntPtr instancePtr, global::Steamworks.HHTMLBrowser unBrowserHandle, uint nAbsolutePixelScroll);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_SetVerticalScroll")]
		public static extern void ISteamHTMLSurface_SetVerticalScroll(global::System.IntPtr instancePtr, global::Steamworks.HHTMLBrowser unBrowserHandle, uint nAbsolutePixelScroll);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_SetKeyFocus")]
		public static extern void ISteamHTMLSurface_SetKeyFocus(global::System.IntPtr instancePtr, global::Steamworks.HHTMLBrowser unBrowserHandle, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bHasKeyFocus);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_ViewSource")]
		public static extern void ISteamHTMLSurface_ViewSource(global::System.IntPtr instancePtr, global::Steamworks.HHTMLBrowser unBrowserHandle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_CopyToClipboard")]
		public static extern void ISteamHTMLSurface_CopyToClipboard(global::System.IntPtr instancePtr, global::Steamworks.HHTMLBrowser unBrowserHandle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_PasteFromClipboard")]
		public static extern void ISteamHTMLSurface_PasteFromClipboard(global::System.IntPtr instancePtr, global::Steamworks.HHTMLBrowser unBrowserHandle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_Find")]
		public static extern void ISteamHTMLSurface_Find(global::System.IntPtr instancePtr, global::Steamworks.HHTMLBrowser unBrowserHandle, global::Steamworks.InteropHelp.UTF8StringHandle pchSearchStr, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bCurrentlyInFind, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bReverse);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_StopFind")]
		public static extern void ISteamHTMLSurface_StopFind(global::System.IntPtr instancePtr, global::Steamworks.HHTMLBrowser unBrowserHandle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_GetLinkAtPosition")]
		public static extern void ISteamHTMLSurface_GetLinkAtPosition(global::System.IntPtr instancePtr, global::Steamworks.HHTMLBrowser unBrowserHandle, int x, int y);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_SetCookie")]
		public static extern void ISteamHTMLSurface_SetCookie(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchHostname, global::Steamworks.InteropHelp.UTF8StringHandle pchKey, global::Steamworks.InteropHelp.UTF8StringHandle pchValue, global::Steamworks.InteropHelp.UTF8StringHandle pchPath, uint nExpires, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bSecure, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bHTTPOnly);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_SetPageScaleFactor")]
		public static extern void ISteamHTMLSurface_SetPageScaleFactor(global::System.IntPtr instancePtr, global::Steamworks.HHTMLBrowser unBrowserHandle, float flZoom, int nPointX, int nPointY);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_SetBackgroundMode")]
		public static extern void ISteamHTMLSurface_SetBackgroundMode(global::System.IntPtr instancePtr, global::Steamworks.HHTMLBrowser unBrowserHandle, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bBackgroundMode);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_SetDPIScalingFactor")]
		public static extern void ISteamHTMLSurface_SetDPIScalingFactor(global::System.IntPtr instancePtr, global::Steamworks.HHTMLBrowser unBrowserHandle, float flDPIScaling);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_OpenDeveloperTools")]
		public static extern void ISteamHTMLSurface_OpenDeveloperTools(global::System.IntPtr instancePtr, global::Steamworks.HHTMLBrowser unBrowserHandle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_AllowStartRequest")]
		public static extern void ISteamHTMLSurface_AllowStartRequest(global::System.IntPtr instancePtr, global::Steamworks.HHTMLBrowser unBrowserHandle, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bAllowed);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_JSDialogResponse")]
		public static extern void ISteamHTMLSurface_JSDialogResponse(global::System.IntPtr instancePtr, global::Steamworks.HHTMLBrowser unBrowserHandle, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bResult);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_FileLoadDialogResponse")]
		public static extern void ISteamHTMLSurface_FileLoadDialogResponse(global::System.IntPtr instancePtr, global::Steamworks.HHTMLBrowser unBrowserHandle, global::System.IntPtr pchSelectedFiles);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_CreateHTTPRequest")]
		public static extern uint ISteamHTTP_CreateHTTPRequest(global::System.IntPtr instancePtr, global::Steamworks.EHTTPMethod eHTTPRequestMethod, global::Steamworks.InteropHelp.UTF8StringHandle pchAbsoluteURL);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestContextValue")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamHTTP_SetHTTPRequestContextValue(global::System.IntPtr instancePtr, global::Steamworks.HTTPRequestHandle hRequest, ulong ulContextValue);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestNetworkActivityTimeout")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamHTTP_SetHTTPRequestNetworkActivityTimeout(global::System.IntPtr instancePtr, global::Steamworks.HTTPRequestHandle hRequest, uint unTimeoutSeconds);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestHeaderValue")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamHTTP_SetHTTPRequestHeaderValue(global::System.IntPtr instancePtr, global::Steamworks.HTTPRequestHandle hRequest, global::Steamworks.InteropHelp.UTF8StringHandle pchHeaderName, global::Steamworks.InteropHelp.UTF8StringHandle pchHeaderValue);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestGetOrPostParameter")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamHTTP_SetHTTPRequestGetOrPostParameter(global::System.IntPtr instancePtr, global::Steamworks.HTTPRequestHandle hRequest, global::Steamworks.InteropHelp.UTF8StringHandle pchParamName, global::Steamworks.InteropHelp.UTF8StringHandle pchParamValue);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_SendHTTPRequest")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamHTTP_SendHTTPRequest(global::System.IntPtr instancePtr, global::Steamworks.HTTPRequestHandle hRequest, out global::Steamworks.SteamAPICall_t pCallHandle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_SendHTTPRequestAndStreamResponse")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamHTTP_SendHTTPRequestAndStreamResponse(global::System.IntPtr instancePtr, global::Steamworks.HTTPRequestHandle hRequest, out global::Steamworks.SteamAPICall_t pCallHandle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_DeferHTTPRequest")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamHTTP_DeferHTTPRequest(global::System.IntPtr instancePtr, global::Steamworks.HTTPRequestHandle hRequest);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_PrioritizeHTTPRequest")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamHTTP_PrioritizeHTTPRequest(global::System.IntPtr instancePtr, global::Steamworks.HTTPRequestHandle hRequest);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPResponseHeaderSize")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamHTTP_GetHTTPResponseHeaderSize(global::System.IntPtr instancePtr, global::Steamworks.HTTPRequestHandle hRequest, global::Steamworks.InteropHelp.UTF8StringHandle pchHeaderName, out uint unResponseHeaderSize);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPResponseHeaderValue")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamHTTP_GetHTTPResponseHeaderValue(global::System.IntPtr instancePtr, global::Steamworks.HTTPRequestHandle hRequest, global::Steamworks.InteropHelp.UTF8StringHandle pchHeaderName, byte[] pHeaderValueBuffer, uint unBufferSize);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPResponseBodySize")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamHTTP_GetHTTPResponseBodySize(global::System.IntPtr instancePtr, global::Steamworks.HTTPRequestHandle hRequest, out uint unBodySize);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPResponseBodyData")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamHTTP_GetHTTPResponseBodyData(global::System.IntPtr instancePtr, global::Steamworks.HTTPRequestHandle hRequest, byte[] pBodyDataBuffer, uint unBufferSize);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPStreamingResponseBodyData")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamHTTP_GetHTTPStreamingResponseBodyData(global::System.IntPtr instancePtr, global::Steamworks.HTTPRequestHandle hRequest, uint cOffset, byte[] pBodyDataBuffer, uint unBufferSize);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_ReleaseHTTPRequest")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamHTTP_ReleaseHTTPRequest(global::System.IntPtr instancePtr, global::Steamworks.HTTPRequestHandle hRequest);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPDownloadProgressPct")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamHTTP_GetHTTPDownloadProgressPct(global::System.IntPtr instancePtr, global::Steamworks.HTTPRequestHandle hRequest, out float pflPercentOut);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestRawPostBody")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamHTTP_SetHTTPRequestRawPostBody(global::System.IntPtr instancePtr, global::Steamworks.HTTPRequestHandle hRequest, global::Steamworks.InteropHelp.UTF8StringHandle pchContentType, byte[] pubBody, uint unBodyLen);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_CreateCookieContainer")]
		public static extern uint ISteamHTTP_CreateCookieContainer(global::System.IntPtr instancePtr, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bAllowResponsesToModify);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_ReleaseCookieContainer")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamHTTP_ReleaseCookieContainer(global::System.IntPtr instancePtr, global::Steamworks.HTTPCookieContainerHandle hCookieContainer);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_SetCookie")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamHTTP_SetCookie(global::System.IntPtr instancePtr, global::Steamworks.HTTPCookieContainerHandle hCookieContainer, global::Steamworks.InteropHelp.UTF8StringHandle pchHost, global::Steamworks.InteropHelp.UTF8StringHandle pchUrl, global::Steamworks.InteropHelp.UTF8StringHandle pchCookie);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestCookieContainer")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamHTTP_SetHTTPRequestCookieContainer(global::System.IntPtr instancePtr, global::Steamworks.HTTPRequestHandle hRequest, global::Steamworks.HTTPCookieContainerHandle hCookieContainer);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestUserAgentInfo")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamHTTP_SetHTTPRequestUserAgentInfo(global::System.IntPtr instancePtr, global::Steamworks.HTTPRequestHandle hRequest, global::Steamworks.InteropHelp.UTF8StringHandle pchUserAgentInfo);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestRequiresVerifiedCertificate")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamHTTP_SetHTTPRequestRequiresVerifiedCertificate(global::System.IntPtr instancePtr, global::Steamworks.HTTPRequestHandle hRequest, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bRequireVerifiedCertificate);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestAbsoluteTimeoutMS")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamHTTP_SetHTTPRequestAbsoluteTimeoutMS(global::System.IntPtr instancePtr, global::Steamworks.HTTPRequestHandle hRequest, uint unMilliseconds);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPRequestWasTimedOut")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamHTTP_GetHTTPRequestWasTimedOut(global::System.IntPtr instancePtr, global::Steamworks.HTTPRequestHandle hRequest, out bool pbWasTimedOut);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_Init")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamInput_Init(global::System.IntPtr instancePtr, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bExplicitlyCallRunFrame);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_Shutdown")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamInput_Shutdown(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_SetInputActionManifestFilePath")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamInput_SetInputActionManifestFilePath(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchInputActionManifestAbsolutePath);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_RunFrame")]
		public static extern void ISteamInput_RunFrame(global::System.IntPtr instancePtr, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bReservedValue);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_BWaitForData")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamInput_BWaitForData(global::System.IntPtr instancePtr, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bWaitForever, uint unTimeout);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_BNewDataAvailable")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamInput_BNewDataAvailable(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetConnectedControllers")]
		public static extern int ISteamInput_GetConnectedControllers(global::System.IntPtr instancePtr, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.InputHandle_t[] handlesOut);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_EnableDeviceCallbacks")]
		public static extern void ISteamInput_EnableDeviceCallbacks(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_EnableActionEventCallbacks")]
		public static extern void ISteamInput_EnableActionEventCallbacks(global::System.IntPtr instancePtr, global::Steamworks.SteamInputActionEventCallbackPointer pCallback);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetActionSetHandle")]
		public static extern ulong ISteamInput_GetActionSetHandle(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pszActionSetName);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_ActivateActionSet")]
		public static extern void ISteamInput_ActivateActionSet(global::System.IntPtr instancePtr, global::Steamworks.InputHandle_t inputHandle, global::Steamworks.InputActionSetHandle_t actionSetHandle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetCurrentActionSet")]
		public static extern ulong ISteamInput_GetCurrentActionSet(global::System.IntPtr instancePtr, global::Steamworks.InputHandle_t inputHandle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_ActivateActionSetLayer")]
		public static extern void ISteamInput_ActivateActionSetLayer(global::System.IntPtr instancePtr, global::Steamworks.InputHandle_t inputHandle, global::Steamworks.InputActionSetHandle_t actionSetLayerHandle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_DeactivateActionSetLayer")]
		public static extern void ISteamInput_DeactivateActionSetLayer(global::System.IntPtr instancePtr, global::Steamworks.InputHandle_t inputHandle, global::Steamworks.InputActionSetHandle_t actionSetLayerHandle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_DeactivateAllActionSetLayers")]
		public static extern void ISteamInput_DeactivateAllActionSetLayers(global::System.IntPtr instancePtr, global::Steamworks.InputHandle_t inputHandle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetActiveActionSetLayers")]
		public static extern int ISteamInput_GetActiveActionSetLayers(global::System.IntPtr instancePtr, global::Steamworks.InputHandle_t inputHandle, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.InputActionSetHandle_t[] handlesOut);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetDigitalActionHandle")]
		public static extern ulong ISteamInput_GetDigitalActionHandle(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pszActionName);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetDigitalActionData")]
		public static extern global::Steamworks.InputDigitalActionData_t ISteamInput_GetDigitalActionData(global::System.IntPtr instancePtr, global::Steamworks.InputHandle_t inputHandle, global::Steamworks.InputDigitalActionHandle_t digitalActionHandle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetDigitalActionOrigins")]
		public static extern int ISteamInput_GetDigitalActionOrigins(global::System.IntPtr instancePtr, global::Steamworks.InputHandle_t inputHandle, global::Steamworks.InputActionSetHandle_t actionSetHandle, global::Steamworks.InputDigitalActionHandle_t digitalActionHandle, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.EInputActionOrigin[] originsOut);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetStringForDigitalActionName")]
		public static extern global::System.IntPtr ISteamInput_GetStringForDigitalActionName(global::System.IntPtr instancePtr, global::Steamworks.InputDigitalActionHandle_t eActionHandle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetAnalogActionHandle")]
		public static extern ulong ISteamInput_GetAnalogActionHandle(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pszActionName);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetAnalogActionData")]
		public static extern global::Steamworks.InputAnalogActionData_t ISteamInput_GetAnalogActionData(global::System.IntPtr instancePtr, global::Steamworks.InputHandle_t inputHandle, global::Steamworks.InputAnalogActionHandle_t analogActionHandle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetAnalogActionOrigins")]
		public static extern int ISteamInput_GetAnalogActionOrigins(global::System.IntPtr instancePtr, global::Steamworks.InputHandle_t inputHandle, global::Steamworks.InputActionSetHandle_t actionSetHandle, global::Steamworks.InputAnalogActionHandle_t analogActionHandle, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.EInputActionOrigin[] originsOut);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetGlyphPNGForActionOrigin")]
		public static extern global::System.IntPtr ISteamInput_GetGlyphPNGForActionOrigin(global::System.IntPtr instancePtr, global::Steamworks.EInputActionOrigin eOrigin, global::Steamworks.ESteamInputGlyphSize eSize, uint unFlags);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetGlyphSVGForActionOrigin")]
		public static extern global::System.IntPtr ISteamInput_GetGlyphSVGForActionOrigin(global::System.IntPtr instancePtr, global::Steamworks.EInputActionOrigin eOrigin, uint unFlags);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetGlyphForActionOrigin_Legacy")]
		public static extern global::System.IntPtr ISteamInput_GetGlyphForActionOrigin_Legacy(global::System.IntPtr instancePtr, global::Steamworks.EInputActionOrigin eOrigin);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetStringForActionOrigin")]
		public static extern global::System.IntPtr ISteamInput_GetStringForActionOrigin(global::System.IntPtr instancePtr, global::Steamworks.EInputActionOrigin eOrigin);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetStringForAnalogActionName")]
		public static extern global::System.IntPtr ISteamInput_GetStringForAnalogActionName(global::System.IntPtr instancePtr, global::Steamworks.InputAnalogActionHandle_t eActionHandle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_StopAnalogActionMomentum")]
		public static extern void ISteamInput_StopAnalogActionMomentum(global::System.IntPtr instancePtr, global::Steamworks.InputHandle_t inputHandle, global::Steamworks.InputAnalogActionHandle_t eAction);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetMotionData")]
		public static extern global::Steamworks.InputMotionData_t ISteamInput_GetMotionData(global::System.IntPtr instancePtr, global::Steamworks.InputHandle_t inputHandle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_TriggerVibration")]
		public static extern void ISteamInput_TriggerVibration(global::System.IntPtr instancePtr, global::Steamworks.InputHandle_t inputHandle, ushort usLeftSpeed, ushort usRightSpeed);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_TriggerVibrationExtended")]
		public static extern void ISteamInput_TriggerVibrationExtended(global::System.IntPtr instancePtr, global::Steamworks.InputHandle_t inputHandle, ushort usLeftSpeed, ushort usRightSpeed, ushort usLeftTriggerSpeed, ushort usRightTriggerSpeed);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_TriggerSimpleHapticEvent")]
		public static extern void ISteamInput_TriggerSimpleHapticEvent(global::System.IntPtr instancePtr, global::Steamworks.InputHandle_t inputHandle, global::Steamworks.EControllerHapticLocation eHapticLocation, byte nIntensity, char nGainDB, byte nOtherIntensity, char nOtherGainDB);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_SetLEDColor")]
		public static extern void ISteamInput_SetLEDColor(global::System.IntPtr instancePtr, global::Steamworks.InputHandle_t inputHandle, byte nColorR, byte nColorG, byte nColorB, uint nFlags);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_Legacy_TriggerHapticPulse")]
		public static extern void ISteamInput_Legacy_TriggerHapticPulse(global::System.IntPtr instancePtr, global::Steamworks.InputHandle_t inputHandle, global::Steamworks.ESteamControllerPad eTargetPad, ushort usDurationMicroSec);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_Legacy_TriggerRepeatedHapticPulse")]
		public static extern void ISteamInput_Legacy_TriggerRepeatedHapticPulse(global::System.IntPtr instancePtr, global::Steamworks.InputHandle_t inputHandle, global::Steamworks.ESteamControllerPad eTargetPad, ushort usDurationMicroSec, ushort usOffMicroSec, ushort unRepeat, uint nFlags);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_ShowBindingPanel")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamInput_ShowBindingPanel(global::System.IntPtr instancePtr, global::Steamworks.InputHandle_t inputHandle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetInputTypeForHandle")]
		public static extern global::Steamworks.ESteamInputType ISteamInput_GetInputTypeForHandle(global::System.IntPtr instancePtr, global::Steamworks.InputHandle_t inputHandle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetControllerForGamepadIndex")]
		public static extern ulong ISteamInput_GetControllerForGamepadIndex(global::System.IntPtr instancePtr, int nIndex);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetGamepadIndexForController")]
		public static extern int ISteamInput_GetGamepadIndexForController(global::System.IntPtr instancePtr, global::Steamworks.InputHandle_t ulinputHandle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetStringForXboxOrigin")]
		public static extern global::System.IntPtr ISteamInput_GetStringForXboxOrigin(global::System.IntPtr instancePtr, global::Steamworks.EXboxOrigin eOrigin);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetGlyphForXboxOrigin")]
		public static extern global::System.IntPtr ISteamInput_GetGlyphForXboxOrigin(global::System.IntPtr instancePtr, global::Steamworks.EXboxOrigin eOrigin);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetActionOriginFromXboxOrigin")]
		public static extern global::Steamworks.EInputActionOrigin ISteamInput_GetActionOriginFromXboxOrigin(global::System.IntPtr instancePtr, global::Steamworks.InputHandle_t inputHandle, global::Steamworks.EXboxOrigin eOrigin);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_TranslateActionOrigin")]
		public static extern global::Steamworks.EInputActionOrigin ISteamInput_TranslateActionOrigin(global::System.IntPtr instancePtr, global::Steamworks.ESteamInputType eDestinationInputType, global::Steamworks.EInputActionOrigin eSourceOrigin);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetDeviceBindingRevision")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamInput_GetDeviceBindingRevision(global::System.IntPtr instancePtr, global::Steamworks.InputHandle_t inputHandle, out int pMajor, out int pMinor);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetRemotePlaySessionID")]
		public static extern uint ISteamInput_GetRemotePlaySessionID(global::System.IntPtr instancePtr, global::Steamworks.InputHandle_t inputHandle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetSessionInputConfigurationSettings")]
		public static extern ushort ISteamInput_GetSessionInputConfigurationSettings(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_SetDualSenseTriggerEffect")]
		public static extern void ISteamInput_SetDualSenseTriggerEffect(global::System.IntPtr instancePtr, global::Steamworks.InputHandle_t inputHandle, global::System.IntPtr pParam);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_GetResultStatus")]
		public static extern global::Steamworks.EResult ISteamInventory_GetResultStatus(global::System.IntPtr instancePtr, global::Steamworks.SteamInventoryResult_t resultHandle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_GetResultItems")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamInventory_GetResultItems(global::System.IntPtr instancePtr, global::Steamworks.SteamInventoryResult_t resultHandle, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.SteamItemDetails_t[] pOutItemsArray, ref uint punOutItemsArraySize);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_GetResultItemProperty")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamInventory_GetResultItemProperty(global::System.IntPtr instancePtr, global::Steamworks.SteamInventoryResult_t resultHandle, uint unItemIndex, global::Steamworks.InteropHelp.UTF8StringHandle pchPropertyName, global::System.IntPtr pchValueBuffer, ref uint punValueBufferSizeOut);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_GetResultTimestamp")]
		public static extern uint ISteamInventory_GetResultTimestamp(global::System.IntPtr instancePtr, global::Steamworks.SteamInventoryResult_t resultHandle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_CheckResultSteamID")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamInventory_CheckResultSteamID(global::System.IntPtr instancePtr, global::Steamworks.SteamInventoryResult_t resultHandle, global::Steamworks.CSteamID steamIDExpected);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_DestroyResult")]
		public static extern void ISteamInventory_DestroyResult(global::System.IntPtr instancePtr, global::Steamworks.SteamInventoryResult_t resultHandle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_GetAllItems")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamInventory_GetAllItems(global::System.IntPtr instancePtr, out global::Steamworks.SteamInventoryResult_t pResultHandle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_GetItemsByID")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamInventory_GetItemsByID(global::System.IntPtr instancePtr, out global::Steamworks.SteamInventoryResult_t pResultHandle, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.SteamItemInstanceID_t[] pInstanceIDs, uint unCountInstanceIDs);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_SerializeResult")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamInventory_SerializeResult(global::System.IntPtr instancePtr, global::Steamworks.SteamInventoryResult_t resultHandle, byte[] pOutBuffer, out uint punOutBufferSize);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_DeserializeResult")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamInventory_DeserializeResult(global::System.IntPtr instancePtr, out global::Steamworks.SteamInventoryResult_t pOutResultHandle, byte[] pBuffer, uint unBufferSize, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bRESERVED_MUST_BE_FALSE);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_GenerateItems")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamInventory_GenerateItems(global::System.IntPtr instancePtr, out global::Steamworks.SteamInventoryResult_t pResultHandle, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.SteamItemDef_t[] pArrayItemDefs, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] uint[] punArrayQuantity, uint unArrayLength);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_GrantPromoItems")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamInventory_GrantPromoItems(global::System.IntPtr instancePtr, out global::Steamworks.SteamInventoryResult_t pResultHandle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_AddPromoItem")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamInventory_AddPromoItem(global::System.IntPtr instancePtr, out global::Steamworks.SteamInventoryResult_t pResultHandle, global::Steamworks.SteamItemDef_t itemDef);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_AddPromoItems")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamInventory_AddPromoItems(global::System.IntPtr instancePtr, out global::Steamworks.SteamInventoryResult_t pResultHandle, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.SteamItemDef_t[] pArrayItemDefs, uint unArrayLength);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_ConsumeItem")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamInventory_ConsumeItem(global::System.IntPtr instancePtr, out global::Steamworks.SteamInventoryResult_t pResultHandle, global::Steamworks.SteamItemInstanceID_t itemConsume, uint unQuantity);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_ExchangeItems")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamInventory_ExchangeItems(global::System.IntPtr instancePtr, out global::Steamworks.SteamInventoryResult_t pResultHandle, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.SteamItemDef_t[] pArrayGenerate, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] uint[] punArrayGenerateQuantity, uint unArrayGenerateLength, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.SteamItemInstanceID_t[] pArrayDestroy, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] uint[] punArrayDestroyQuantity, uint unArrayDestroyLength);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_TransferItemQuantity")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamInventory_TransferItemQuantity(global::System.IntPtr instancePtr, out global::Steamworks.SteamInventoryResult_t pResultHandle, global::Steamworks.SteamItemInstanceID_t itemIdSource, uint unQuantity, global::Steamworks.SteamItemInstanceID_t itemIdDest);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_SendItemDropHeartbeat")]
		public static extern void ISteamInventory_SendItemDropHeartbeat(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_TriggerItemDrop")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamInventory_TriggerItemDrop(global::System.IntPtr instancePtr, out global::Steamworks.SteamInventoryResult_t pResultHandle, global::Steamworks.SteamItemDef_t dropListDefinition);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_TradeItems")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamInventory_TradeItems(global::System.IntPtr instancePtr, out global::Steamworks.SteamInventoryResult_t pResultHandle, global::Steamworks.CSteamID steamIDTradePartner, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.SteamItemInstanceID_t[] pArrayGive, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] uint[] pArrayGiveQuantity, uint nArrayGiveLength, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.SteamItemInstanceID_t[] pArrayGet, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] uint[] pArrayGetQuantity, uint nArrayGetLength);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_LoadItemDefinitions")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamInventory_LoadItemDefinitions(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_GetItemDefinitionIDs")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamInventory_GetItemDefinitionIDs(global::System.IntPtr instancePtr, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.SteamItemDef_t[] pItemDefIDs, ref uint punItemDefIDsArraySize);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_GetItemDefinitionProperty")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamInventory_GetItemDefinitionProperty(global::System.IntPtr instancePtr, global::Steamworks.SteamItemDef_t iDefinition, global::Steamworks.InteropHelp.UTF8StringHandle pchPropertyName, global::System.IntPtr pchValueBuffer, ref uint punValueBufferSizeOut);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_RequestEligiblePromoItemDefinitionsIDs")]
		public static extern ulong ISteamInventory_RequestEligiblePromoItemDefinitionsIDs(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_GetEligiblePromoItemDefinitionIDs")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamInventory_GetEligiblePromoItemDefinitionIDs(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamID, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.SteamItemDef_t[] pItemDefIDs, ref uint punItemDefIDsArraySize);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_StartPurchase")]
		public static extern ulong ISteamInventory_StartPurchase(global::System.IntPtr instancePtr, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.SteamItemDef_t[] pArrayItemDefs, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] uint[] punArrayQuantity, uint unArrayLength);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_RequestPrices")]
		public static extern ulong ISteamInventory_RequestPrices(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_GetNumItemsWithPrices")]
		public static extern uint ISteamInventory_GetNumItemsWithPrices(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_GetItemsWithPrices")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamInventory_GetItemsWithPrices(global::System.IntPtr instancePtr, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.SteamItemDef_t[] pArrayItemDefs, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] ulong[] pCurrentPrices, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] ulong[] pBasePrices, uint unArrayLength);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_GetItemPrice")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamInventory_GetItemPrice(global::System.IntPtr instancePtr, global::Steamworks.SteamItemDef_t iDefinition, out ulong pCurrentPrice, out ulong pBasePrice);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_StartUpdateProperties")]
		public static extern ulong ISteamInventory_StartUpdateProperties(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_RemoveProperty")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamInventory_RemoveProperty(global::System.IntPtr instancePtr, global::Steamworks.SteamInventoryUpdateHandle_t handle, global::Steamworks.SteamItemInstanceID_t nItemID, global::Steamworks.InteropHelp.UTF8StringHandle pchPropertyName);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_SetPropertyString")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamInventory_SetPropertyString(global::System.IntPtr instancePtr, global::Steamworks.SteamInventoryUpdateHandle_t handle, global::Steamworks.SteamItemInstanceID_t nItemID, global::Steamworks.InteropHelp.UTF8StringHandle pchPropertyName, global::Steamworks.InteropHelp.UTF8StringHandle pchPropertyValue);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_SetPropertyBool")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamInventory_SetPropertyBool(global::System.IntPtr instancePtr, global::Steamworks.SteamInventoryUpdateHandle_t handle, global::Steamworks.SteamItemInstanceID_t nItemID, global::Steamworks.InteropHelp.UTF8StringHandle pchPropertyName, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bValue);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_SetPropertyInt64")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamInventory_SetPropertyInt64(global::System.IntPtr instancePtr, global::Steamworks.SteamInventoryUpdateHandle_t handle, global::Steamworks.SteamItemInstanceID_t nItemID, global::Steamworks.InteropHelp.UTF8StringHandle pchPropertyName, long nValue);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_SetPropertyFloat")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamInventory_SetPropertyFloat(global::System.IntPtr instancePtr, global::Steamworks.SteamInventoryUpdateHandle_t handle, global::Steamworks.SteamItemInstanceID_t nItemID, global::Steamworks.InteropHelp.UTF8StringHandle pchPropertyName, float flValue);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_SubmitUpdateProperties")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamInventory_SubmitUpdateProperties(global::System.IntPtr instancePtr, global::Steamworks.SteamInventoryUpdateHandle_t handle, out global::Steamworks.SteamInventoryResult_t pResultHandle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_InspectItem")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamInventory_InspectItem(global::System.IntPtr instancePtr, out global::Steamworks.SteamInventoryResult_t pResultHandle, global::Steamworks.InteropHelp.UTF8StringHandle pchItemToken);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetFavoriteGameCount")]
		public static extern int ISteamMatchmaking_GetFavoriteGameCount(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetFavoriteGame")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamMatchmaking_GetFavoriteGame(global::System.IntPtr instancePtr, int iGame, out global::Steamworks.AppId_t pnAppID, out uint pnIP, out ushort pnConnPort, out ushort pnQueryPort, out uint punFlags, out uint pRTime32LastPlayedOnServer);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_AddFavoriteGame")]
		public static extern int ISteamMatchmaking_AddFavoriteGame(global::System.IntPtr instancePtr, global::Steamworks.AppId_t nAppID, uint nIP, ushort nConnPort, ushort nQueryPort, uint unFlags, uint rTime32LastPlayedOnServer);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_RemoveFavoriteGame")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamMatchmaking_RemoveFavoriteGame(global::System.IntPtr instancePtr, global::Steamworks.AppId_t nAppID, uint nIP, ushort nConnPort, ushort nQueryPort, uint unFlags);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_RequestLobbyList")]
		public static extern ulong ISteamMatchmaking_RequestLobbyList(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_AddRequestLobbyListStringFilter")]
		public static extern void ISteamMatchmaking_AddRequestLobbyListStringFilter(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchKeyToMatch, global::Steamworks.InteropHelp.UTF8StringHandle pchValueToMatch, global::Steamworks.ELobbyComparison eComparisonType);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_AddRequestLobbyListNumericalFilter")]
		public static extern void ISteamMatchmaking_AddRequestLobbyListNumericalFilter(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchKeyToMatch, int nValueToMatch, global::Steamworks.ELobbyComparison eComparisonType);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_AddRequestLobbyListNearValueFilter")]
		public static extern void ISteamMatchmaking_AddRequestLobbyListNearValueFilter(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchKeyToMatch, int nValueToBeCloseTo);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_AddRequestLobbyListFilterSlotsAvailable")]
		public static extern void ISteamMatchmaking_AddRequestLobbyListFilterSlotsAvailable(global::System.IntPtr instancePtr, int nSlotsAvailable);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_AddRequestLobbyListDistanceFilter")]
		public static extern void ISteamMatchmaking_AddRequestLobbyListDistanceFilter(global::System.IntPtr instancePtr, global::Steamworks.ELobbyDistanceFilter eLobbyDistanceFilter);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_AddRequestLobbyListResultCountFilter")]
		public static extern void ISteamMatchmaking_AddRequestLobbyListResultCountFilter(global::System.IntPtr instancePtr, int cMaxResults);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_AddRequestLobbyListCompatibleMembersFilter")]
		public static extern void ISteamMatchmaking_AddRequestLobbyListCompatibleMembersFilter(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDLobby);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyByIndex")]
		public static extern ulong ISteamMatchmaking_GetLobbyByIndex(global::System.IntPtr instancePtr, int iLobby);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_CreateLobby")]
		public static extern ulong ISteamMatchmaking_CreateLobby(global::System.IntPtr instancePtr, global::Steamworks.ELobbyType eLobbyType, int cMaxMembers);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_JoinLobby")]
		public static extern ulong ISteamMatchmaking_JoinLobby(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDLobby);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_LeaveLobby")]
		public static extern void ISteamMatchmaking_LeaveLobby(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDLobby);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_InviteUserToLobby")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamMatchmaking_InviteUserToLobby(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDLobby, global::Steamworks.CSteamID steamIDInvitee);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetNumLobbyMembers")]
		public static extern int ISteamMatchmaking_GetNumLobbyMembers(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDLobby);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyMemberByIndex")]
		public static extern ulong ISteamMatchmaking_GetLobbyMemberByIndex(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDLobby, int iMember);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyData")]
		public static extern global::System.IntPtr ISteamMatchmaking_GetLobbyData(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDLobby, global::Steamworks.InteropHelp.UTF8StringHandle pchKey);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_SetLobbyData")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamMatchmaking_SetLobbyData(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDLobby, global::Steamworks.InteropHelp.UTF8StringHandle pchKey, global::Steamworks.InteropHelp.UTF8StringHandle pchValue);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyDataCount")]
		public static extern int ISteamMatchmaking_GetLobbyDataCount(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDLobby);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyDataByIndex")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamMatchmaking_GetLobbyDataByIndex(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDLobby, int iLobbyData, global::System.IntPtr pchKey, int cchKeyBufferSize, global::System.IntPtr pchValue, int cchValueBufferSize);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_DeleteLobbyData")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamMatchmaking_DeleteLobbyData(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDLobby, global::Steamworks.InteropHelp.UTF8StringHandle pchKey);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyMemberData")]
		public static extern global::System.IntPtr ISteamMatchmaking_GetLobbyMemberData(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDLobby, global::Steamworks.CSteamID steamIDUser, global::Steamworks.InteropHelp.UTF8StringHandle pchKey);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_SetLobbyMemberData")]
		public static extern void ISteamMatchmaking_SetLobbyMemberData(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDLobby, global::Steamworks.InteropHelp.UTF8StringHandle pchKey, global::Steamworks.InteropHelp.UTF8StringHandle pchValue);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_SendLobbyChatMsg")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamMatchmaking_SendLobbyChatMsg(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDLobby, byte[] pvMsgBody, int cubMsgBody);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyChatEntry")]
		public static extern int ISteamMatchmaking_GetLobbyChatEntry(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDLobby, int iChatID, out global::Steamworks.CSteamID pSteamIDUser, byte[] pvData, int cubData, out global::Steamworks.EChatEntryType peChatEntryType);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_RequestLobbyData")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamMatchmaking_RequestLobbyData(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDLobby);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_SetLobbyGameServer")]
		public static extern void ISteamMatchmaking_SetLobbyGameServer(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDLobby, uint unGameServerIP, ushort unGameServerPort, global::Steamworks.CSteamID steamIDGameServer);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyGameServer")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamMatchmaking_GetLobbyGameServer(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDLobby, out uint punGameServerIP, out ushort punGameServerPort, out global::Steamworks.CSteamID psteamIDGameServer);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_SetLobbyMemberLimit")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamMatchmaking_SetLobbyMemberLimit(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDLobby, int cMaxMembers);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyMemberLimit")]
		public static extern int ISteamMatchmaking_GetLobbyMemberLimit(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDLobby);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_SetLobbyType")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamMatchmaking_SetLobbyType(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDLobby, global::Steamworks.ELobbyType eLobbyType);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_SetLobbyJoinable")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamMatchmaking_SetLobbyJoinable(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDLobby, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bLobbyJoinable);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyOwner")]
		public static extern ulong ISteamMatchmaking_GetLobbyOwner(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDLobby);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_SetLobbyOwner")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamMatchmaking_SetLobbyOwner(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDLobby, global::Steamworks.CSteamID steamIDNewOwner);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_SetLinkedLobby")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamMatchmaking_SetLinkedLobby(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDLobby, global::Steamworks.CSteamID steamIDLobbyDependent);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_RequestInternetServerList")]
		public static extern global::System.IntPtr ISteamMatchmakingServers_RequestInternetServerList(global::System.IntPtr instancePtr, global::Steamworks.AppId_t iApp, global::System.IntPtr ppchFilters, uint nFilters, global::System.IntPtr pRequestServersResponse);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_RequestLANServerList")]
		public static extern global::System.IntPtr ISteamMatchmakingServers_RequestLANServerList(global::System.IntPtr instancePtr, global::Steamworks.AppId_t iApp, global::System.IntPtr pRequestServersResponse);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_RequestFriendsServerList")]
		public static extern global::System.IntPtr ISteamMatchmakingServers_RequestFriendsServerList(global::System.IntPtr instancePtr, global::Steamworks.AppId_t iApp, global::System.IntPtr ppchFilters, uint nFilters, global::System.IntPtr pRequestServersResponse);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_RequestFavoritesServerList")]
		public static extern global::System.IntPtr ISteamMatchmakingServers_RequestFavoritesServerList(global::System.IntPtr instancePtr, global::Steamworks.AppId_t iApp, global::System.IntPtr ppchFilters, uint nFilters, global::System.IntPtr pRequestServersResponse);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_RequestHistoryServerList")]
		public static extern global::System.IntPtr ISteamMatchmakingServers_RequestHistoryServerList(global::System.IntPtr instancePtr, global::Steamworks.AppId_t iApp, global::System.IntPtr ppchFilters, uint nFilters, global::System.IntPtr pRequestServersResponse);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_RequestSpectatorServerList")]
		public static extern global::System.IntPtr ISteamMatchmakingServers_RequestSpectatorServerList(global::System.IntPtr instancePtr, global::Steamworks.AppId_t iApp, global::System.IntPtr ppchFilters, uint nFilters, global::System.IntPtr pRequestServersResponse);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_ReleaseRequest")]
		public static extern void ISteamMatchmakingServers_ReleaseRequest(global::System.IntPtr instancePtr, global::Steamworks.HServerListRequest hServerListRequest);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_GetServerDetails")]
		public static extern global::System.IntPtr ISteamMatchmakingServers_GetServerDetails(global::System.IntPtr instancePtr, global::Steamworks.HServerListRequest hRequest, int iServer);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_CancelQuery")]
		public static extern void ISteamMatchmakingServers_CancelQuery(global::System.IntPtr instancePtr, global::Steamworks.HServerListRequest hRequest);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_RefreshQuery")]
		public static extern void ISteamMatchmakingServers_RefreshQuery(global::System.IntPtr instancePtr, global::Steamworks.HServerListRequest hRequest);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_IsRefreshing")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamMatchmakingServers_IsRefreshing(global::System.IntPtr instancePtr, global::Steamworks.HServerListRequest hRequest);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_GetServerCount")]
		public static extern int ISteamMatchmakingServers_GetServerCount(global::System.IntPtr instancePtr, global::Steamworks.HServerListRequest hRequest);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_RefreshServer")]
		public static extern void ISteamMatchmakingServers_RefreshServer(global::System.IntPtr instancePtr, global::Steamworks.HServerListRequest hRequest, int iServer);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_PingServer")]
		public static extern int ISteamMatchmakingServers_PingServer(global::System.IntPtr instancePtr, uint unIP, ushort usPort, global::System.IntPtr pRequestServersResponse);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_PlayerDetails")]
		public static extern int ISteamMatchmakingServers_PlayerDetails(global::System.IntPtr instancePtr, uint unIP, ushort usPort, global::System.IntPtr pRequestServersResponse);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_ServerRules")]
		public static extern int ISteamMatchmakingServers_ServerRules(global::System.IntPtr instancePtr, uint unIP, ushort usPort, global::System.IntPtr pRequestServersResponse);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_CancelServerQuery")]
		public static extern void ISteamMatchmakingServers_CancelServerQuery(global::System.IntPtr instancePtr, global::Steamworks.HServerQuery hServerQuery);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamParties_GetNumActiveBeacons")]
		public static extern uint ISteamParties_GetNumActiveBeacons(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamParties_GetBeaconByIndex")]
		public static extern ulong ISteamParties_GetBeaconByIndex(global::System.IntPtr instancePtr, uint unIndex);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamParties_GetBeaconDetails")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamParties_GetBeaconDetails(global::System.IntPtr instancePtr, global::Steamworks.PartyBeaconID_t ulBeaconID, out global::Steamworks.CSteamID pSteamIDBeaconOwner, out global::Steamworks.SteamPartyBeaconLocation_t pLocation, global::System.IntPtr pchMetadata, int cchMetadata);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamParties_JoinParty")]
		public static extern ulong ISteamParties_JoinParty(global::System.IntPtr instancePtr, global::Steamworks.PartyBeaconID_t ulBeaconID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamParties_GetNumAvailableBeaconLocations")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamParties_GetNumAvailableBeaconLocations(global::System.IntPtr instancePtr, out uint puNumLocations);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamParties_GetAvailableBeaconLocations")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamParties_GetAvailableBeaconLocations(global::System.IntPtr instancePtr, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.SteamPartyBeaconLocation_t[] pLocationList, uint uMaxNumLocations);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamParties_CreateBeacon")]
		public static extern ulong ISteamParties_CreateBeacon(global::System.IntPtr instancePtr, uint unOpenSlots, ref global::Steamworks.SteamPartyBeaconLocation_t pBeaconLocation, global::Steamworks.InteropHelp.UTF8StringHandle pchConnectString, global::Steamworks.InteropHelp.UTF8StringHandle pchMetadata);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamParties_OnReservationCompleted")]
		public static extern void ISteamParties_OnReservationCompleted(global::System.IntPtr instancePtr, global::Steamworks.PartyBeaconID_t ulBeacon, global::Steamworks.CSteamID steamIDUser);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamParties_CancelReservation")]
		public static extern void ISteamParties_CancelReservation(global::System.IntPtr instancePtr, global::Steamworks.PartyBeaconID_t ulBeacon, global::Steamworks.CSteamID steamIDUser);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamParties_ChangeNumOpenSlots")]
		public static extern ulong ISteamParties_ChangeNumOpenSlots(global::System.IntPtr instancePtr, global::Steamworks.PartyBeaconID_t ulBeacon, uint unOpenSlots);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamParties_DestroyBeacon")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamParties_DestroyBeacon(global::System.IntPtr instancePtr, global::Steamworks.PartyBeaconID_t ulBeacon);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamParties_GetBeaconLocationData")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamParties_GetBeaconLocationData(global::System.IntPtr instancePtr, global::Steamworks.SteamPartyBeaconLocation_t BeaconLocation, global::Steamworks.ESteamPartyBeaconLocationData eData, global::System.IntPtr pchDataStringOut, int cchDataStringOut);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusic_BIsEnabled")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamMusic_BIsEnabled(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusic_BIsPlaying")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamMusic_BIsPlaying(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusic_GetPlaybackStatus")]
		public static extern global::Steamworks.AudioPlayback_Status ISteamMusic_GetPlaybackStatus(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusic_Play")]
		public static extern void ISteamMusic_Play(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusic_Pause")]
		public static extern void ISteamMusic_Pause(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusic_PlayPrevious")]
		public static extern void ISteamMusic_PlayPrevious(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusic_PlayNext")]
		public static extern void ISteamMusic_PlayNext(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusic_SetVolume")]
		public static extern void ISteamMusic_SetVolume(global::System.IntPtr instancePtr, float flVolume);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusic_GetVolume")]
		public static extern float ISteamMusic_GetVolume(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_SendP2PPacket")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworking_SendP2PPacket(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDRemote, byte[] pubData, uint cubData, global::Steamworks.EP2PSend eP2PSendType, int nChannel);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_IsP2PPacketAvailable")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworking_IsP2PPacketAvailable(global::System.IntPtr instancePtr, out uint pcubMsgSize, int nChannel);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_ReadP2PPacket")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworking_ReadP2PPacket(global::System.IntPtr instancePtr, byte[] pubDest, uint cubDest, out uint pcubMsgSize, out global::Steamworks.CSteamID psteamIDRemote, int nChannel);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_AcceptP2PSessionWithUser")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworking_AcceptP2PSessionWithUser(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDRemote);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_CloseP2PSessionWithUser")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworking_CloseP2PSessionWithUser(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDRemote);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_CloseP2PChannelWithUser")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworking_CloseP2PChannelWithUser(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDRemote, int nChannel);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_GetP2PSessionState")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworking_GetP2PSessionState(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDRemote, out global::Steamworks.P2PSessionState_t pConnectionState);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_AllowP2PPacketRelay")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworking_AllowP2PPacketRelay(global::System.IntPtr instancePtr, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bAllow);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_CreateListenSocket")]
		public static extern uint ISteamNetworking_CreateListenSocket(global::System.IntPtr instancePtr, int nVirtualP2PPort, global::Steamworks.SteamIPAddress_t nIP, ushort nPort, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bAllowUseOfPacketRelay);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_CreateP2PConnectionSocket")]
		public static extern uint ISteamNetworking_CreateP2PConnectionSocket(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDTarget, int nVirtualPort, int nTimeoutSec, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bAllowUseOfPacketRelay);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_CreateConnectionSocket")]
		public static extern uint ISteamNetworking_CreateConnectionSocket(global::System.IntPtr instancePtr, global::Steamworks.SteamIPAddress_t nIP, ushort nPort, int nTimeoutSec);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_DestroySocket")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworking_DestroySocket(global::System.IntPtr instancePtr, global::Steamworks.SNetSocket_t hSocket, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bNotifyRemoteEnd);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_DestroyListenSocket")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworking_DestroyListenSocket(global::System.IntPtr instancePtr, global::Steamworks.SNetListenSocket_t hSocket, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bNotifyRemoteEnd);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_SendDataOnSocket")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworking_SendDataOnSocket(global::System.IntPtr instancePtr, global::Steamworks.SNetSocket_t hSocket, byte[] pubData, uint cubData, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bReliable);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_IsDataAvailableOnSocket")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworking_IsDataAvailableOnSocket(global::System.IntPtr instancePtr, global::Steamworks.SNetSocket_t hSocket, out uint pcubMsgSize);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_RetrieveDataFromSocket")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworking_RetrieveDataFromSocket(global::System.IntPtr instancePtr, global::Steamworks.SNetSocket_t hSocket, byte[] pubDest, uint cubDest, out uint pcubMsgSize);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_IsDataAvailable")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworking_IsDataAvailable(global::System.IntPtr instancePtr, global::Steamworks.SNetListenSocket_t hListenSocket, out uint pcubMsgSize, out global::Steamworks.SNetSocket_t phSocket);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_RetrieveData")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworking_RetrieveData(global::System.IntPtr instancePtr, global::Steamworks.SNetListenSocket_t hListenSocket, byte[] pubDest, uint cubDest, out uint pcubMsgSize, out global::Steamworks.SNetSocket_t phSocket);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_GetSocketInfo")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworking_GetSocketInfo(global::System.IntPtr instancePtr, global::Steamworks.SNetSocket_t hSocket, out global::Steamworks.CSteamID pSteamIDRemote, out int peSocketStatus, out global::Steamworks.SteamIPAddress_t punIPRemote, out ushort punPortRemote);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_GetListenSocketInfo")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworking_GetListenSocketInfo(global::System.IntPtr instancePtr, global::Steamworks.SNetListenSocket_t hListenSocket, out global::Steamworks.SteamIPAddress_t pnIP, out ushort pnPort);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_GetSocketConnectionType")]
		public static extern global::Steamworks.ESNetSocketConnectionType ISteamNetworking_GetSocketConnectionType(global::System.IntPtr instancePtr, global::Steamworks.SNetSocket_t hSocket);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_GetMaxPacketSize")]
		public static extern int ISteamNetworking_GetMaxPacketSize(global::System.IntPtr instancePtr, global::Steamworks.SNetSocket_t hSocket);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingMessages_SendMessageToUser")]
		public static extern global::Steamworks.EResult ISteamNetworkingMessages_SendMessageToUser(global::System.IntPtr instancePtr, ref global::Steamworks.SteamNetworkingIdentity identityRemote, global::System.IntPtr pubData, uint cubData, int nSendFlags, int nRemoteChannel);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingMessages_ReceiveMessagesOnChannel")]
		public static extern int ISteamNetworkingMessages_ReceiveMessagesOnChannel(global::System.IntPtr instancePtr, int nLocalChannel, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::System.IntPtr[] ppOutMessages, int nMaxMessages);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingMessages_AcceptSessionWithUser")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworkingMessages_AcceptSessionWithUser(global::System.IntPtr instancePtr, ref global::Steamworks.SteamNetworkingIdentity identityRemote);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingMessages_CloseSessionWithUser")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworkingMessages_CloseSessionWithUser(global::System.IntPtr instancePtr, ref global::Steamworks.SteamNetworkingIdentity identityRemote);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingMessages_CloseChannelWithUser")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworkingMessages_CloseChannelWithUser(global::System.IntPtr instancePtr, ref global::Steamworks.SteamNetworkingIdentity identityRemote, int nLocalChannel);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingMessages_GetSessionConnectionInfo")]
		public static extern global::Steamworks.ESteamNetworkingConnectionState ISteamNetworkingMessages_GetSessionConnectionInfo(global::System.IntPtr instancePtr, ref global::Steamworks.SteamNetworkingIdentity identityRemote, out global::Steamworks.SteamNetConnectionInfo_t pConnectionInfo, out global::Steamworks.SteamNetConnectionRealTimeStatus_t pQuickStatus);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_CreateListenSocketIP")]
		public static extern uint ISteamNetworkingSockets_CreateListenSocketIP(global::System.IntPtr instancePtr, ref global::Steamworks.SteamNetworkingIPAddr localAddress, int nOptions, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.SteamNetworkingConfigValue_t[] pOptions);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_ConnectByIPAddress")]
		public static extern uint ISteamNetworkingSockets_ConnectByIPAddress(global::System.IntPtr instancePtr, ref global::Steamworks.SteamNetworkingIPAddr address, int nOptions, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.SteamNetworkingConfigValue_t[] pOptions);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_CreateListenSocketP2P")]
		public static extern uint ISteamNetworkingSockets_CreateListenSocketP2P(global::System.IntPtr instancePtr, int nLocalVirtualPort, int nOptions, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.SteamNetworkingConfigValue_t[] pOptions);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_ConnectP2P")]
		public static extern uint ISteamNetworkingSockets_ConnectP2P(global::System.IntPtr instancePtr, ref global::Steamworks.SteamNetworkingIdentity identityRemote, int nRemoteVirtualPort, int nOptions, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.SteamNetworkingConfigValue_t[] pOptions);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_AcceptConnection")]
		public static extern global::Steamworks.EResult ISteamNetworkingSockets_AcceptConnection(global::System.IntPtr instancePtr, global::Steamworks.HSteamNetConnection hConn);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_CloseConnection")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworkingSockets_CloseConnection(global::System.IntPtr instancePtr, global::Steamworks.HSteamNetConnection hPeer, int nReason, global::Steamworks.InteropHelp.UTF8StringHandle pszDebug, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bEnableLinger);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_CloseListenSocket")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworkingSockets_CloseListenSocket(global::System.IntPtr instancePtr, global::Steamworks.HSteamListenSocket hSocket);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_SetConnectionUserData")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworkingSockets_SetConnectionUserData(global::System.IntPtr instancePtr, global::Steamworks.HSteamNetConnection hPeer, long nUserData);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_GetConnectionUserData")]
		public static extern long ISteamNetworkingSockets_GetConnectionUserData(global::System.IntPtr instancePtr, global::Steamworks.HSteamNetConnection hPeer);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_SetConnectionName")]
		public static extern void ISteamNetworkingSockets_SetConnectionName(global::System.IntPtr instancePtr, global::Steamworks.HSteamNetConnection hPeer, global::Steamworks.InteropHelp.UTF8StringHandle pszName);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_GetConnectionName")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworkingSockets_GetConnectionName(global::System.IntPtr instancePtr, global::Steamworks.HSteamNetConnection hPeer, global::System.IntPtr pszName, int nMaxLen);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_SendMessageToConnection")]
		public static extern global::Steamworks.EResult ISteamNetworkingSockets_SendMessageToConnection(global::System.IntPtr instancePtr, global::Steamworks.HSteamNetConnection hConn, global::System.IntPtr pData, uint cbData, int nSendFlags, out long pOutMessageNumber);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_SendMessages")]
		public static extern void ISteamNetworkingSockets_SendMessages(global::System.IntPtr instancePtr, int nMessages, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::System.IntPtr[] pMessages, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] long[] pOutMessageNumberOrResult);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_FlushMessagesOnConnection")]
		public static extern global::Steamworks.EResult ISteamNetworkingSockets_FlushMessagesOnConnection(global::System.IntPtr instancePtr, global::Steamworks.HSteamNetConnection hConn);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_ReceiveMessagesOnConnection")]
		public static extern int ISteamNetworkingSockets_ReceiveMessagesOnConnection(global::System.IntPtr instancePtr, global::Steamworks.HSteamNetConnection hConn, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::System.IntPtr[] ppOutMessages, int nMaxMessages);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_GetConnectionInfo")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworkingSockets_GetConnectionInfo(global::System.IntPtr instancePtr, global::Steamworks.HSteamNetConnection hConn, out global::Steamworks.SteamNetConnectionInfo_t pInfo);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_GetConnectionRealTimeStatus")]
		public static extern global::Steamworks.EResult ISteamNetworkingSockets_GetConnectionRealTimeStatus(global::System.IntPtr instancePtr, global::Steamworks.HSteamNetConnection hConn, ref global::Steamworks.SteamNetConnectionRealTimeStatus_t pStatus, int nLanes, ref global::Steamworks.SteamNetConnectionRealTimeLaneStatus_t pLanes);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_GetDetailedConnectionStatus")]
		public static extern int ISteamNetworkingSockets_GetDetailedConnectionStatus(global::System.IntPtr instancePtr, global::Steamworks.HSteamNetConnection hConn, global::System.IntPtr pszBuf, int cbBuf);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_GetListenSocketAddress")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworkingSockets_GetListenSocketAddress(global::System.IntPtr instancePtr, global::Steamworks.HSteamListenSocket hSocket, out global::Steamworks.SteamNetworkingIPAddr address);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_CreateSocketPair")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworkingSockets_CreateSocketPair(global::System.IntPtr instancePtr, out global::Steamworks.HSteamNetConnection pOutConnection1, out global::Steamworks.HSteamNetConnection pOutConnection2, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bUseNetworkLoopback, ref global::Steamworks.SteamNetworkingIdentity pIdentity1, ref global::Steamworks.SteamNetworkingIdentity pIdentity2);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_ConfigureConnectionLanes")]
		public static extern global::Steamworks.EResult ISteamNetworkingSockets_ConfigureConnectionLanes(global::System.IntPtr instancePtr, global::Steamworks.HSteamNetConnection hConn, int nNumLanes, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] int[] pLanePriorities, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] ushort[] pLaneWeights);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_GetIdentity")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworkingSockets_GetIdentity(global::System.IntPtr instancePtr, out global::Steamworks.SteamNetworkingIdentity pIdentity);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_InitAuthentication")]
		public static extern global::Steamworks.ESteamNetworkingAvailability ISteamNetworkingSockets_InitAuthentication(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_GetAuthenticationStatus")]
		public static extern global::Steamworks.ESteamNetworkingAvailability ISteamNetworkingSockets_GetAuthenticationStatus(global::System.IntPtr instancePtr, out global::Steamworks.SteamNetAuthenticationStatus_t pDetails);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_CreatePollGroup")]
		public static extern uint ISteamNetworkingSockets_CreatePollGroup(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_DestroyPollGroup")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworkingSockets_DestroyPollGroup(global::System.IntPtr instancePtr, global::Steamworks.HSteamNetPollGroup hPollGroup);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_SetConnectionPollGroup")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworkingSockets_SetConnectionPollGroup(global::System.IntPtr instancePtr, global::Steamworks.HSteamNetConnection hConn, global::Steamworks.HSteamNetPollGroup hPollGroup);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_ReceiveMessagesOnPollGroup")]
		public static extern int ISteamNetworkingSockets_ReceiveMessagesOnPollGroup(global::System.IntPtr instancePtr, global::Steamworks.HSteamNetPollGroup hPollGroup, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::System.IntPtr[] ppOutMessages, int nMaxMessages);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_ReceivedRelayAuthTicket")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworkingSockets_ReceivedRelayAuthTicket(global::System.IntPtr instancePtr, global::System.IntPtr pvTicket, int cbTicket, out global::Steamworks.SteamDatagramRelayAuthTicket pOutParsedTicket);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_FindRelayAuthTicketForServer")]
		public static extern int ISteamNetworkingSockets_FindRelayAuthTicketForServer(global::System.IntPtr instancePtr, ref global::Steamworks.SteamNetworkingIdentity identityGameServer, int nRemoteVirtualPort, out global::Steamworks.SteamDatagramRelayAuthTicket pOutParsedTicket);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_ConnectToHostedDedicatedServer")]
		public static extern uint ISteamNetworkingSockets_ConnectToHostedDedicatedServer(global::System.IntPtr instancePtr, ref global::Steamworks.SteamNetworkingIdentity identityTarget, int nRemoteVirtualPort, int nOptions, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.SteamNetworkingConfigValue_t[] pOptions);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_GetHostedDedicatedServerPort")]
		public static extern ushort ISteamNetworkingSockets_GetHostedDedicatedServerPort(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_GetHostedDedicatedServerPOPID")]
		public static extern uint ISteamNetworkingSockets_GetHostedDedicatedServerPOPID(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_GetHostedDedicatedServerAddress")]
		public static extern global::Steamworks.EResult ISteamNetworkingSockets_GetHostedDedicatedServerAddress(global::System.IntPtr instancePtr, out global::Steamworks.SteamDatagramHostedAddress pRouting);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_CreateHostedDedicatedServerListenSocket")]
		public static extern uint ISteamNetworkingSockets_CreateHostedDedicatedServerListenSocket(global::System.IntPtr instancePtr, int nLocalVirtualPort, int nOptions, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.SteamNetworkingConfigValue_t[] pOptions);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_GetGameCoordinatorServerLogin")]
		public static extern global::Steamworks.EResult ISteamNetworkingSockets_GetGameCoordinatorServerLogin(global::System.IntPtr instancePtr, global::System.IntPtr pLoginInfo, out int pcbSignedBlob, global::System.IntPtr pBlob);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_ConnectP2PCustomSignaling")]
		public static extern uint ISteamNetworkingSockets_ConnectP2PCustomSignaling(global::System.IntPtr instancePtr, out global::Steamworks.ISteamNetworkingConnectionSignaling pSignaling, ref global::Steamworks.SteamNetworkingIdentity pPeerIdentity, int nRemoteVirtualPort, int nOptions, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.SteamNetworkingConfigValue_t[] pOptions);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_ReceivedP2PCustomSignal")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworkingSockets_ReceivedP2PCustomSignal(global::System.IntPtr instancePtr, global::System.IntPtr pMsg, int cbMsg, out global::Steamworks.ISteamNetworkingSignalingRecvContext pContext);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_GetCertificateRequest")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworkingSockets_GetCertificateRequest(global::System.IntPtr instancePtr, out int pcbBlob, global::System.IntPtr pBlob, out global::Steamworks.SteamNetworkingErrMsg errMsg);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_SetCertificate")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworkingSockets_SetCertificate(global::System.IntPtr instancePtr, global::System.IntPtr pCertificate, int cbCertificate, out global::Steamworks.SteamNetworkingErrMsg errMsg);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_ResetIdentity")]
		public static extern void ISteamNetworkingSockets_ResetIdentity(global::System.IntPtr instancePtr, ref global::Steamworks.SteamNetworkingIdentity pIdentity);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_RunCallbacks")]
		public static extern void ISteamNetworkingSockets_RunCallbacks(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_BeginAsyncRequestFakeIP")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworkingSockets_BeginAsyncRequestFakeIP(global::System.IntPtr instancePtr, int nNumPorts);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_GetFakeIP")]
		public static extern void ISteamNetworkingSockets_GetFakeIP(global::System.IntPtr instancePtr, int idxFirstPort, out global::Steamworks.SteamNetworkingFakeIPResult_t pInfo);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_CreateListenSocketP2PFakeIP")]
		public static extern uint ISteamNetworkingSockets_CreateListenSocketP2PFakeIP(global::System.IntPtr instancePtr, int idxFakePort, int nOptions, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.SteamNetworkingConfigValue_t[] pOptions);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_GetRemoteFakeIPForConnection")]
		public static extern global::Steamworks.EResult ISteamNetworkingSockets_GetRemoteFakeIPForConnection(global::System.IntPtr instancePtr, global::Steamworks.HSteamNetConnection hConn, out global::Steamworks.SteamNetworkingIPAddr pOutAddr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_CreateFakeUDPPort")]
		public static extern global::System.IntPtr ISteamNetworkingSockets_CreateFakeUDPPort(global::System.IntPtr instancePtr, int idxFakeServerPort);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_AllocateMessage")]
		public static extern global::System.IntPtr ISteamNetworkingUtils_AllocateMessage(global::System.IntPtr instancePtr, int cbAllocateBuffer);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_InitRelayNetworkAccess")]
		public static extern void ISteamNetworkingUtils_InitRelayNetworkAccess(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetRelayNetworkStatus")]
		public static extern global::Steamworks.ESteamNetworkingAvailability ISteamNetworkingUtils_GetRelayNetworkStatus(global::System.IntPtr instancePtr, out global::Steamworks.SteamRelayNetworkStatus_t pDetails);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetLocalPingLocation")]
		public static extern float ISteamNetworkingUtils_GetLocalPingLocation(global::System.IntPtr instancePtr, out global::Steamworks.SteamNetworkPingLocation_t result);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_EstimatePingTimeBetweenTwoLocations")]
		public static extern int ISteamNetworkingUtils_EstimatePingTimeBetweenTwoLocations(global::System.IntPtr instancePtr, ref global::Steamworks.SteamNetworkPingLocation_t location1, ref global::Steamworks.SteamNetworkPingLocation_t location2);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_EstimatePingTimeFromLocalHost")]
		public static extern int ISteamNetworkingUtils_EstimatePingTimeFromLocalHost(global::System.IntPtr instancePtr, ref global::Steamworks.SteamNetworkPingLocation_t remoteLocation);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_ConvertPingLocationToString")]
		public static extern void ISteamNetworkingUtils_ConvertPingLocationToString(global::System.IntPtr instancePtr, ref global::Steamworks.SteamNetworkPingLocation_t location, global::System.IntPtr pszBuf, int cchBufSize);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_ParsePingLocationString")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworkingUtils_ParsePingLocationString(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pszString, out global::Steamworks.SteamNetworkPingLocation_t result);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_CheckPingDataUpToDate")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworkingUtils_CheckPingDataUpToDate(global::System.IntPtr instancePtr, float flMaxAgeSeconds);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetPingToDataCenter")]
		public static extern int ISteamNetworkingUtils_GetPingToDataCenter(global::System.IntPtr instancePtr, global::Steamworks.SteamNetworkingPOPID popID, out global::Steamworks.SteamNetworkingPOPID pViaRelayPoP);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetDirectPingToPOP")]
		public static extern int ISteamNetworkingUtils_GetDirectPingToPOP(global::System.IntPtr instancePtr, global::Steamworks.SteamNetworkingPOPID popID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetPOPCount")]
		public static extern int ISteamNetworkingUtils_GetPOPCount(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetPOPList")]
		public static extern int ISteamNetworkingUtils_GetPOPList(global::System.IntPtr instancePtr, out global::Steamworks.SteamNetworkingPOPID list, int nListSz);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetLocalTimestamp")]
		public static extern long ISteamNetworkingUtils_GetLocalTimestamp(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SetDebugOutputFunction")]
		public static extern void ISteamNetworkingUtils_SetDebugOutputFunction(global::System.IntPtr instancePtr, global::Steamworks.ESteamNetworkingSocketsDebugOutputType eDetailLevel, global::Steamworks.FSteamNetworkingSocketsDebugOutput pfnFunc);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_IsFakeIPv4")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworkingUtils_IsFakeIPv4(global::System.IntPtr instancePtr, uint nIPv4);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetIPv4FakeIPType")]
		public static extern global::Steamworks.ESteamNetworkingFakeIPType ISteamNetworkingUtils_GetIPv4FakeIPType(global::System.IntPtr instancePtr, uint nIPv4);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetRealIdentityForFakeIP")]
		public static extern global::Steamworks.EResult ISteamNetworkingUtils_GetRealIdentityForFakeIP(global::System.IntPtr instancePtr, ref global::Steamworks.SteamNetworkingIPAddr fakeIP, out global::Steamworks.SteamNetworkingIdentity pOutRealIdentity);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SetConfigValue")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworkingUtils_SetConfigValue(global::System.IntPtr instancePtr, global::Steamworks.ESteamNetworkingConfigValue eValue, global::Steamworks.ESteamNetworkingConfigScope eScopeType, global::System.IntPtr scopeObj, global::Steamworks.ESteamNetworkingConfigDataType eDataType, global::System.IntPtr pArg);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetConfigValue")]
		public static extern global::Steamworks.ESteamNetworkingGetConfigValueResult ISteamNetworkingUtils_GetConfigValue(global::System.IntPtr instancePtr, global::Steamworks.ESteamNetworkingConfigValue eValue, global::Steamworks.ESteamNetworkingConfigScope eScopeType, global::System.IntPtr scopeObj, out global::Steamworks.ESteamNetworkingConfigDataType pOutDataType, global::System.IntPtr pResult, ref ulong cbResult);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetConfigValueInfo")]
		public static extern global::System.IntPtr ISteamNetworkingUtils_GetConfigValueInfo(global::System.IntPtr instancePtr, global::Steamworks.ESteamNetworkingConfigValue eValue, out global::Steamworks.ESteamNetworkingConfigDataType pOutDataType, out global::Steamworks.ESteamNetworkingConfigScope pOutScope);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_IterateGenericEditableConfigValues")]
		public static extern global::Steamworks.ESteamNetworkingConfigValue ISteamNetworkingUtils_IterateGenericEditableConfigValues(global::System.IntPtr instancePtr, global::Steamworks.ESteamNetworkingConfigValue eCurrent, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bEnumerateDevVars);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SteamNetworkingIPAddr_ToString")]
		public static extern void ISteamNetworkingUtils_SteamNetworkingIPAddr_ToString(global::System.IntPtr instancePtr, ref global::Steamworks.SteamNetworkingIPAddr addr, global::System.IntPtr buf, uint cbBuf, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bWithPort);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SteamNetworkingIPAddr_ParseString")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworkingUtils_SteamNetworkingIPAddr_ParseString(global::System.IntPtr instancePtr, out global::Steamworks.SteamNetworkingIPAddr pAddr, global::Steamworks.InteropHelp.UTF8StringHandle pszStr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SteamNetworkingIPAddr_GetFakeIPType")]
		public static extern global::Steamworks.ESteamNetworkingFakeIPType ISteamNetworkingUtils_SteamNetworkingIPAddr_GetFakeIPType(global::System.IntPtr instancePtr, ref global::Steamworks.SteamNetworkingIPAddr addr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SteamNetworkingIdentity_ToString")]
		public static extern void ISteamNetworkingUtils_SteamNetworkingIdentity_ToString(global::System.IntPtr instancePtr, ref global::Steamworks.SteamNetworkingIdentity identity, global::System.IntPtr buf, uint cbBuf);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SteamNetworkingIdentity_ParseString")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamNetworkingUtils_SteamNetworkingIdentity_ParseString(global::System.IntPtr instancePtr, out global::Steamworks.SteamNetworkingIdentity pIdentity, global::Steamworks.InteropHelp.UTF8StringHandle pszStr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamParentalSettings_BIsParentalLockEnabled")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamParentalSettings_BIsParentalLockEnabled(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamParentalSettings_BIsParentalLockLocked")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamParentalSettings_BIsParentalLockLocked(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamParentalSettings_BIsAppBlocked")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamParentalSettings_BIsAppBlocked(global::System.IntPtr instancePtr, global::Steamworks.AppId_t nAppID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamParentalSettings_BIsAppInBlockList")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamParentalSettings_BIsAppInBlockList(global::System.IntPtr instancePtr, global::Steamworks.AppId_t nAppID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamParentalSettings_BIsFeatureBlocked")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamParentalSettings_BIsFeatureBlocked(global::System.IntPtr instancePtr, global::Steamworks.EParentalFeature eFeature);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamParentalSettings_BIsFeatureInBlockList")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamParentalSettings_BIsFeatureInBlockList(global::System.IntPtr instancePtr, global::Steamworks.EParentalFeature eFeature);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemotePlay_GetSessionCount")]
		public static extern uint ISteamRemotePlay_GetSessionCount(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemotePlay_GetSessionID")]
		public static extern uint ISteamRemotePlay_GetSessionID(global::System.IntPtr instancePtr, int iSessionIndex);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemotePlay_GetSessionSteamID")]
		public static extern ulong ISteamRemotePlay_GetSessionSteamID(global::System.IntPtr instancePtr, global::Steamworks.RemotePlaySessionID_t unSessionID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemotePlay_GetSessionClientName")]
		public static extern global::System.IntPtr ISteamRemotePlay_GetSessionClientName(global::System.IntPtr instancePtr, global::Steamworks.RemotePlaySessionID_t unSessionID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemotePlay_GetSessionClientFormFactor")]
		public static extern global::Steamworks.ESteamDeviceFormFactor ISteamRemotePlay_GetSessionClientFormFactor(global::System.IntPtr instancePtr, global::Steamworks.RemotePlaySessionID_t unSessionID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemotePlay_BGetSessionClientResolution")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamRemotePlay_BGetSessionClientResolution(global::System.IntPtr instancePtr, global::Steamworks.RemotePlaySessionID_t unSessionID, out int pnResolutionX, out int pnResolutionY);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemotePlay_ShowRemotePlayTogetherUI")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamRemotePlay_ShowRemotePlayTogetherUI(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemotePlay_BSendRemotePlayTogetherInvite")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamRemotePlay_BSendRemotePlayTogetherInvite(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDFriend);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemotePlay_BEnableRemotePlayTogetherDirectInput")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamRemotePlay_BEnableRemotePlayTogetherDirectInput(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemotePlay_DisableRemotePlayTogetherDirectInput")]
		public static extern void ISteamRemotePlay_DisableRemotePlayTogetherDirectInput(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemotePlay_GetInput")]
		public static extern uint ISteamRemotePlay_GetInput(global::System.IntPtr instancePtr, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.RemotePlayInput_t[] pInput, uint unMaxEvents);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemotePlay_SetMouseVisibility")]
		public static extern void ISteamRemotePlay_SetMouseVisibility(global::System.IntPtr instancePtr, global::Steamworks.RemotePlaySessionID_t unSessionID, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bVisible);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemotePlay_SetMousePosition")]
		public static extern void ISteamRemotePlay_SetMousePosition(global::System.IntPtr instancePtr, global::Steamworks.RemotePlaySessionID_t unSessionID, float flNormalizedX, float flNormalizedY);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemotePlay_CreateMouseCursor")]
		public static extern uint ISteamRemotePlay_CreateMouseCursor(global::System.IntPtr instancePtr, int nWidth, int nHeight, int nHotX, int nHotY, global::System.IntPtr pBGRA, int nPitch);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemotePlay_SetMouseCursor")]
		public static extern void ISteamRemotePlay_SetMouseCursor(global::System.IntPtr instancePtr, global::Steamworks.RemotePlaySessionID_t unSessionID, global::Steamworks.RemotePlayCursorID_t unCursorID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileWrite")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_FileWrite(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchFile, byte[] pvData, int cubData);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileRead")]
		public static extern int ISteamRemoteStorage_FileRead(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchFile, byte[] pvData, int cubDataToRead);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileWriteAsync")]
		public static extern ulong ISteamRemoteStorage_FileWriteAsync(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchFile, byte[] pvData, uint cubData);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileReadAsync")]
		public static extern ulong ISteamRemoteStorage_FileReadAsync(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchFile, uint nOffset, uint cubToRead);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileReadAsyncComplete")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_FileReadAsyncComplete(global::System.IntPtr instancePtr, global::Steamworks.SteamAPICall_t hReadCall, byte[] pvBuffer, uint cubToRead);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileForget")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_FileForget(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchFile);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileDelete")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_FileDelete(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchFile);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileShare")]
		public static extern ulong ISteamRemoteStorage_FileShare(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchFile);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_SetSyncPlatforms")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_SetSyncPlatforms(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchFile, global::Steamworks.ERemoteStoragePlatform eRemoteStoragePlatform);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileWriteStreamOpen")]
		public static extern ulong ISteamRemoteStorage_FileWriteStreamOpen(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchFile);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileWriteStreamWriteChunk")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_FileWriteStreamWriteChunk(global::System.IntPtr instancePtr, global::Steamworks.UGCFileWriteStreamHandle_t writeHandle, byte[] pvData, int cubData);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileWriteStreamClose")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_FileWriteStreamClose(global::System.IntPtr instancePtr, global::Steamworks.UGCFileWriteStreamHandle_t writeHandle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileWriteStreamCancel")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_FileWriteStreamCancel(global::System.IntPtr instancePtr, global::Steamworks.UGCFileWriteStreamHandle_t writeHandle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileExists")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_FileExists(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchFile);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FilePersisted")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_FilePersisted(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchFile);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetFileSize")]
		public static extern int ISteamRemoteStorage_GetFileSize(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchFile);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetFileTimestamp")]
		public static extern long ISteamRemoteStorage_GetFileTimestamp(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchFile);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetSyncPlatforms")]
		public static extern global::Steamworks.ERemoteStoragePlatform ISteamRemoteStorage_GetSyncPlatforms(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchFile);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetFileCount")]
		public static extern int ISteamRemoteStorage_GetFileCount(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetFileNameAndSize")]
		public static extern global::System.IntPtr ISteamRemoteStorage_GetFileNameAndSize(global::System.IntPtr instancePtr, int iFile, out int pnFileSizeInBytes);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetQuota")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_GetQuota(global::System.IntPtr instancePtr, out ulong pnTotalBytes, out ulong puAvailableBytes);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_IsCloudEnabledForAccount")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_IsCloudEnabledForAccount(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_IsCloudEnabledForApp")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_IsCloudEnabledForApp(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_SetCloudEnabledForApp")]
		public static extern void ISteamRemoteStorage_SetCloudEnabledForApp(global::System.IntPtr instancePtr, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bEnabled);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_UGCDownload")]
		public static extern ulong ISteamRemoteStorage_UGCDownload(global::System.IntPtr instancePtr, global::Steamworks.UGCHandle_t hContent, uint unPriority);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetUGCDownloadProgress")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_GetUGCDownloadProgress(global::System.IntPtr instancePtr, global::Steamworks.UGCHandle_t hContent, out int pnBytesDownloaded, out int pnBytesExpected);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetUGCDetails")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_GetUGCDetails(global::System.IntPtr instancePtr, global::Steamworks.UGCHandle_t hContent, out global::Steamworks.AppId_t pnAppID, out global::System.IntPtr ppchName, out int pnFileSizeInBytes, out global::Steamworks.CSteamID pSteamIDOwner);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_UGCRead")]
		public static extern int ISteamRemoteStorage_UGCRead(global::System.IntPtr instancePtr, global::Steamworks.UGCHandle_t hContent, byte[] pvData, int cubDataToRead, uint cOffset, global::Steamworks.EUGCReadAction eAction);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetCachedUGCCount")]
		public static extern int ISteamRemoteStorage_GetCachedUGCCount(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetCachedUGCHandle")]
		public static extern ulong ISteamRemoteStorage_GetCachedUGCHandle(global::System.IntPtr instancePtr, int iCachedContent);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_PublishWorkshopFile")]
		public static extern ulong ISteamRemoteStorage_PublishWorkshopFile(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchFile, global::Steamworks.InteropHelp.UTF8StringHandle pchPreviewFile, global::Steamworks.AppId_t nConsumerAppId, global::Steamworks.InteropHelp.UTF8StringHandle pchTitle, global::Steamworks.InteropHelp.UTF8StringHandle pchDescription, global::Steamworks.ERemoteStoragePublishedFileVisibility eVisibility, global::System.IntPtr pTags, global::Steamworks.EWorkshopFileType eWorkshopFileType);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_CreatePublishedFileUpdateRequest")]
		public static extern ulong ISteamRemoteStorage_CreatePublishedFileUpdateRequest(global::System.IntPtr instancePtr, global::Steamworks.PublishedFileId_t unPublishedFileId);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_UpdatePublishedFileFile")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_UpdatePublishedFileFile(global::System.IntPtr instancePtr, global::Steamworks.PublishedFileUpdateHandle_t updateHandle, global::Steamworks.InteropHelp.UTF8StringHandle pchFile);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_UpdatePublishedFilePreviewFile")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_UpdatePublishedFilePreviewFile(global::System.IntPtr instancePtr, global::Steamworks.PublishedFileUpdateHandle_t updateHandle, global::Steamworks.InteropHelp.UTF8StringHandle pchPreviewFile);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_UpdatePublishedFileTitle")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_UpdatePublishedFileTitle(global::System.IntPtr instancePtr, global::Steamworks.PublishedFileUpdateHandle_t updateHandle, global::Steamworks.InteropHelp.UTF8StringHandle pchTitle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_UpdatePublishedFileDescription")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_UpdatePublishedFileDescription(global::System.IntPtr instancePtr, global::Steamworks.PublishedFileUpdateHandle_t updateHandle, global::Steamworks.InteropHelp.UTF8StringHandle pchDescription);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_UpdatePublishedFileVisibility")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_UpdatePublishedFileVisibility(global::System.IntPtr instancePtr, global::Steamworks.PublishedFileUpdateHandle_t updateHandle, global::Steamworks.ERemoteStoragePublishedFileVisibility eVisibility);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_UpdatePublishedFileTags")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_UpdatePublishedFileTags(global::System.IntPtr instancePtr, global::Steamworks.PublishedFileUpdateHandle_t updateHandle, global::System.IntPtr pTags);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_CommitPublishedFileUpdate")]
		public static extern ulong ISteamRemoteStorage_CommitPublishedFileUpdate(global::System.IntPtr instancePtr, global::Steamworks.PublishedFileUpdateHandle_t updateHandle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetPublishedFileDetails")]
		public static extern ulong ISteamRemoteStorage_GetPublishedFileDetails(global::System.IntPtr instancePtr, global::Steamworks.PublishedFileId_t unPublishedFileId, uint unMaxSecondsOld);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_DeletePublishedFile")]
		public static extern ulong ISteamRemoteStorage_DeletePublishedFile(global::System.IntPtr instancePtr, global::Steamworks.PublishedFileId_t unPublishedFileId);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_EnumerateUserPublishedFiles")]
		public static extern ulong ISteamRemoteStorage_EnumerateUserPublishedFiles(global::System.IntPtr instancePtr, uint unStartIndex);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_SubscribePublishedFile")]
		public static extern ulong ISteamRemoteStorage_SubscribePublishedFile(global::System.IntPtr instancePtr, global::Steamworks.PublishedFileId_t unPublishedFileId);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_EnumerateUserSubscribedFiles")]
		public static extern ulong ISteamRemoteStorage_EnumerateUserSubscribedFiles(global::System.IntPtr instancePtr, uint unStartIndex);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_UnsubscribePublishedFile")]
		public static extern ulong ISteamRemoteStorage_UnsubscribePublishedFile(global::System.IntPtr instancePtr, global::Steamworks.PublishedFileId_t unPublishedFileId);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_UpdatePublishedFileSetChangeDescription")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_UpdatePublishedFileSetChangeDescription(global::System.IntPtr instancePtr, global::Steamworks.PublishedFileUpdateHandle_t updateHandle, global::Steamworks.InteropHelp.UTF8StringHandle pchChangeDescription);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetPublishedItemVoteDetails")]
		public static extern ulong ISteamRemoteStorage_GetPublishedItemVoteDetails(global::System.IntPtr instancePtr, global::Steamworks.PublishedFileId_t unPublishedFileId);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_UpdateUserPublishedItemVote")]
		public static extern ulong ISteamRemoteStorage_UpdateUserPublishedItemVote(global::System.IntPtr instancePtr, global::Steamworks.PublishedFileId_t unPublishedFileId, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bVoteUp);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetUserPublishedItemVoteDetails")]
		public static extern ulong ISteamRemoteStorage_GetUserPublishedItemVoteDetails(global::System.IntPtr instancePtr, global::Steamworks.PublishedFileId_t unPublishedFileId);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_EnumerateUserSharedWorkshopFiles")]
		public static extern ulong ISteamRemoteStorage_EnumerateUserSharedWorkshopFiles(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamId, uint unStartIndex, global::System.IntPtr pRequiredTags, global::System.IntPtr pExcludedTags);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_PublishVideo")]
		public static extern ulong ISteamRemoteStorage_PublishVideo(global::System.IntPtr instancePtr, global::Steamworks.EWorkshopVideoProvider eVideoProvider, global::Steamworks.InteropHelp.UTF8StringHandle pchVideoAccount, global::Steamworks.InteropHelp.UTF8StringHandle pchVideoIdentifier, global::Steamworks.InteropHelp.UTF8StringHandle pchPreviewFile, global::Steamworks.AppId_t nConsumerAppId, global::Steamworks.InteropHelp.UTF8StringHandle pchTitle, global::Steamworks.InteropHelp.UTF8StringHandle pchDescription, global::Steamworks.ERemoteStoragePublishedFileVisibility eVisibility, global::System.IntPtr pTags);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_SetUserPublishedFileAction")]
		public static extern ulong ISteamRemoteStorage_SetUserPublishedFileAction(global::System.IntPtr instancePtr, global::Steamworks.PublishedFileId_t unPublishedFileId, global::Steamworks.EWorkshopFileAction eAction);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_EnumeratePublishedFilesByUserAction")]
		public static extern ulong ISteamRemoteStorage_EnumeratePublishedFilesByUserAction(global::System.IntPtr instancePtr, global::Steamworks.EWorkshopFileAction eAction, uint unStartIndex);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_EnumeratePublishedWorkshopFiles")]
		public static extern ulong ISteamRemoteStorage_EnumeratePublishedWorkshopFiles(global::System.IntPtr instancePtr, global::Steamworks.EWorkshopEnumerationType eEnumerationType, uint unStartIndex, uint unCount, uint unDays, global::System.IntPtr pTags, global::System.IntPtr pUserTags);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_UGCDownloadToLocation")]
		public static extern ulong ISteamRemoteStorage_UGCDownloadToLocation(global::System.IntPtr instancePtr, global::Steamworks.UGCHandle_t hContent, global::Steamworks.InteropHelp.UTF8StringHandle pchLocation, uint unPriority);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetLocalFileChangeCount")]
		public static extern int ISteamRemoteStorage_GetLocalFileChangeCount(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetLocalFileChange")]
		public static extern global::System.IntPtr ISteamRemoteStorage_GetLocalFileChange(global::System.IntPtr instancePtr, int iFile, out global::Steamworks.ERemoteStorageLocalFileChange pEChangeType, out global::Steamworks.ERemoteStorageFilePathType pEFilePathType);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_BeginFileWriteBatch")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_BeginFileWriteBatch(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_EndFileWriteBatch")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamRemoteStorage_EndFileWriteBatch(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamScreenshots_WriteScreenshot")]
		public static extern uint ISteamScreenshots_WriteScreenshot(global::System.IntPtr instancePtr, byte[] pubRGB, uint cubRGB, int nWidth, int nHeight);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamScreenshots_AddScreenshotToLibrary")]
		public static extern uint ISteamScreenshots_AddScreenshotToLibrary(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchFilename, global::Steamworks.InteropHelp.UTF8StringHandle pchThumbnailFilename, int nWidth, int nHeight);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamScreenshots_TriggerScreenshot")]
		public static extern void ISteamScreenshots_TriggerScreenshot(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamScreenshots_HookScreenshots")]
		public static extern void ISteamScreenshots_HookScreenshots(global::System.IntPtr instancePtr, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bHook);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamScreenshots_SetLocation")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamScreenshots_SetLocation(global::System.IntPtr instancePtr, global::Steamworks.ScreenshotHandle hScreenshot, global::Steamworks.InteropHelp.UTF8StringHandle pchLocation);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamScreenshots_TagUser")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamScreenshots_TagUser(global::System.IntPtr instancePtr, global::Steamworks.ScreenshotHandle hScreenshot, global::Steamworks.CSteamID steamID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamScreenshots_TagPublishedFile")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamScreenshots_TagPublishedFile(global::System.IntPtr instancePtr, global::Steamworks.ScreenshotHandle hScreenshot, global::Steamworks.PublishedFileId_t unPublishedFileID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamScreenshots_IsScreenshotsHooked")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamScreenshots_IsScreenshotsHooked(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamScreenshots_AddVRScreenshotToLibrary")]
		public static extern uint ISteamScreenshots_AddVRScreenshotToLibrary(global::System.IntPtr instancePtr, global::Steamworks.EVRScreenshotType eType, global::Steamworks.InteropHelp.UTF8StringHandle pchFilename, global::Steamworks.InteropHelp.UTF8StringHandle pchVRFilename);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamTimeline_SetTimelineTooltip")]
		public static extern void ISteamTimeline_SetTimelineTooltip(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchDescription, float flTimeDelta);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamTimeline_ClearTimelineTooltip")]
		public static extern void ISteamTimeline_ClearTimelineTooltip(global::System.IntPtr instancePtr, float flTimeDelta);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamTimeline_SetTimelineGameMode")]
		public static extern void ISteamTimeline_SetTimelineGameMode(global::System.IntPtr instancePtr, global::Steamworks.ETimelineGameMode eMode);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamTimeline_AddInstantaneousTimelineEvent")]
		public static extern ulong ISteamTimeline_AddInstantaneousTimelineEvent(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchTitle, global::Steamworks.InteropHelp.UTF8StringHandle pchDescription, global::Steamworks.InteropHelp.UTF8StringHandle pchIcon, uint unIconPriority, float flStartOffsetSeconds, global::Steamworks.ETimelineEventClipPriority ePossibleClip);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamTimeline_AddRangeTimelineEvent")]
		public static extern ulong ISteamTimeline_AddRangeTimelineEvent(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchTitle, global::Steamworks.InteropHelp.UTF8StringHandle pchDescription, global::Steamworks.InteropHelp.UTF8StringHandle pchIcon, uint unIconPriority, float flStartOffsetSeconds, float flDuration, global::Steamworks.ETimelineEventClipPriority ePossibleClip);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamTimeline_StartRangeTimelineEvent")]
		public static extern ulong ISteamTimeline_StartRangeTimelineEvent(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchTitle, global::Steamworks.InteropHelp.UTF8StringHandle pchDescription, global::Steamworks.InteropHelp.UTF8StringHandle pchIcon, uint unPriority, float flStartOffsetSeconds, global::Steamworks.ETimelineEventClipPriority ePossibleClip);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamTimeline_UpdateRangeTimelineEvent")]
		public static extern void ISteamTimeline_UpdateRangeTimelineEvent(global::System.IntPtr instancePtr, global::Steamworks.TimelineEventHandle_t ulEvent, global::Steamworks.InteropHelp.UTF8StringHandle pchTitle, global::Steamworks.InteropHelp.UTF8StringHandle pchDescription, global::Steamworks.InteropHelp.UTF8StringHandle pchIcon, uint unPriority, global::Steamworks.ETimelineEventClipPriority ePossibleClip);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamTimeline_EndRangeTimelineEvent")]
		public static extern void ISteamTimeline_EndRangeTimelineEvent(global::System.IntPtr instancePtr, global::Steamworks.TimelineEventHandle_t ulEvent, float flEndOffsetSeconds);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamTimeline_RemoveTimelineEvent")]
		public static extern void ISteamTimeline_RemoveTimelineEvent(global::System.IntPtr instancePtr, global::Steamworks.TimelineEventHandle_t ulEvent);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamTimeline_DoesEventRecordingExist")]
		public static extern ulong ISteamTimeline_DoesEventRecordingExist(global::System.IntPtr instancePtr, global::Steamworks.TimelineEventHandle_t ulEvent);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamTimeline_StartGamePhase")]
		public static extern void ISteamTimeline_StartGamePhase(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamTimeline_EndGamePhase")]
		public static extern void ISteamTimeline_EndGamePhase(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamTimeline_SetGamePhaseID")]
		public static extern void ISteamTimeline_SetGamePhaseID(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchPhaseID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamTimeline_DoesGamePhaseRecordingExist")]
		public static extern ulong ISteamTimeline_DoesGamePhaseRecordingExist(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchPhaseID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamTimeline_AddGamePhaseTag")]
		public static extern void ISteamTimeline_AddGamePhaseTag(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchTagName, global::Steamworks.InteropHelp.UTF8StringHandle pchTagIcon, global::Steamworks.InteropHelp.UTF8StringHandle pchTagGroup, uint unPriority);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamTimeline_SetGamePhaseAttribute")]
		public static extern void ISteamTimeline_SetGamePhaseAttribute(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchAttributeGroup, global::Steamworks.InteropHelp.UTF8StringHandle pchAttributeValue, uint unPriority);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamTimeline_OpenOverlayToGamePhase")]
		public static extern void ISteamTimeline_OpenOverlayToGamePhase(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchPhaseID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamTimeline_OpenOverlayToTimelineEvent")]
		public static extern void ISteamTimeline_OpenOverlayToTimelineEvent(global::System.IntPtr instancePtr, global::Steamworks.TimelineEventHandle_t ulEvent);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_CreateQueryUserUGCRequest")]
		public static extern ulong ISteamUGC_CreateQueryUserUGCRequest(global::System.IntPtr instancePtr, global::Steamworks.AccountID_t unAccountID, global::Steamworks.EUserUGCList eListType, global::Steamworks.EUGCMatchingUGCType eMatchingUGCType, global::Steamworks.EUserUGCListSortOrder eSortOrder, global::Steamworks.AppId_t nCreatorAppID, global::Steamworks.AppId_t nConsumerAppID, uint unPage);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_CreateQueryAllUGCRequestPage")]
		public static extern ulong ISteamUGC_CreateQueryAllUGCRequestPage(global::System.IntPtr instancePtr, global::Steamworks.EUGCQuery eQueryType, global::Steamworks.EUGCMatchingUGCType eMatchingeMatchingUGCTypeFileType, global::Steamworks.AppId_t nCreatorAppID, global::Steamworks.AppId_t nConsumerAppID, uint unPage);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_CreateQueryAllUGCRequestCursor")]
		public static extern ulong ISteamUGC_CreateQueryAllUGCRequestCursor(global::System.IntPtr instancePtr, global::Steamworks.EUGCQuery eQueryType, global::Steamworks.EUGCMatchingUGCType eMatchingeMatchingUGCTypeFileType, global::Steamworks.AppId_t nCreatorAppID, global::Steamworks.AppId_t nConsumerAppID, global::Steamworks.InteropHelp.UTF8StringHandle pchCursor);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_CreateQueryUGCDetailsRequest")]
		public static extern ulong ISteamUGC_CreateQueryUGCDetailsRequest(global::System.IntPtr instancePtr, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.PublishedFileId_t[] pvecPublishedFileID, uint unNumPublishedFileIDs);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SendQueryUGCRequest")]
		public static extern ulong ISteamUGC_SendQueryUGCRequest(global::System.IntPtr instancePtr, global::Steamworks.UGCQueryHandle_t handle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCResult")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_GetQueryUGCResult(global::System.IntPtr instancePtr, global::Steamworks.UGCQueryHandle_t handle, uint index, out global::Steamworks.SteamUGCDetails_t pDetails);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCNumTags")]
		public static extern uint ISteamUGC_GetQueryUGCNumTags(global::System.IntPtr instancePtr, global::Steamworks.UGCQueryHandle_t handle, uint index);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCTag")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_GetQueryUGCTag(global::System.IntPtr instancePtr, global::Steamworks.UGCQueryHandle_t handle, uint index, uint indexTag, global::System.IntPtr pchValue, uint cchValueSize);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCTagDisplayName")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_GetQueryUGCTagDisplayName(global::System.IntPtr instancePtr, global::Steamworks.UGCQueryHandle_t handle, uint index, uint indexTag, global::System.IntPtr pchValue, uint cchValueSize);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCPreviewURL")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_GetQueryUGCPreviewURL(global::System.IntPtr instancePtr, global::Steamworks.UGCQueryHandle_t handle, uint index, global::System.IntPtr pchURL, uint cchURLSize);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCMetadata")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_GetQueryUGCMetadata(global::System.IntPtr instancePtr, global::Steamworks.UGCQueryHandle_t handle, uint index, global::System.IntPtr pchMetadata, uint cchMetadatasize);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCChildren")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_GetQueryUGCChildren(global::System.IntPtr instancePtr, global::Steamworks.UGCQueryHandle_t handle, uint index, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.PublishedFileId_t[] pvecPublishedFileID, uint cMaxEntries);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCStatistic")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_GetQueryUGCStatistic(global::System.IntPtr instancePtr, global::Steamworks.UGCQueryHandle_t handle, uint index, global::Steamworks.EItemStatistic eStatType, out ulong pStatValue);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCNumAdditionalPreviews")]
		public static extern uint ISteamUGC_GetQueryUGCNumAdditionalPreviews(global::System.IntPtr instancePtr, global::Steamworks.UGCQueryHandle_t handle, uint index);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCAdditionalPreview")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_GetQueryUGCAdditionalPreview(global::System.IntPtr instancePtr, global::Steamworks.UGCQueryHandle_t handle, uint index, uint previewIndex, global::System.IntPtr pchURLOrVideoID, uint cchURLSize, global::System.IntPtr pchOriginalFileName, uint cchOriginalFileNameSize, out global::Steamworks.EItemPreviewType pPreviewType);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCNumKeyValueTags")]
		public static extern uint ISteamUGC_GetQueryUGCNumKeyValueTags(global::System.IntPtr instancePtr, global::Steamworks.UGCQueryHandle_t handle, uint index);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCKeyValueTag")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_GetQueryUGCKeyValueTag(global::System.IntPtr instancePtr, global::Steamworks.UGCQueryHandle_t handle, uint index, uint keyValueTagIndex, global::System.IntPtr pchKey, uint cchKeySize, global::System.IntPtr pchValue, uint cchValueSize);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetQueryFirstUGCKeyValueTag")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_GetQueryFirstUGCKeyValueTag(global::System.IntPtr instancePtr, global::Steamworks.UGCQueryHandle_t handle, uint index, global::Steamworks.InteropHelp.UTF8StringHandle pchKey, global::System.IntPtr pchValue, uint cchValueSize);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetNumSupportedGameVersions")]
		public static extern uint ISteamUGC_GetNumSupportedGameVersions(global::System.IntPtr instancePtr, global::Steamworks.UGCQueryHandle_t handle, uint index);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetSupportedGameVersionData")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_GetSupportedGameVersionData(global::System.IntPtr instancePtr, global::Steamworks.UGCQueryHandle_t handle, uint index, uint versionIndex, global::System.IntPtr pchGameBranchMin, global::System.IntPtr pchGameBranchMax, uint cchGameBranchSize);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCContentDescriptors")]
		public static extern uint ISteamUGC_GetQueryUGCContentDescriptors(global::System.IntPtr instancePtr, global::Steamworks.UGCQueryHandle_t handle, uint index, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.EUGCContentDescriptorID[] pvecDescriptors, uint cMaxEntries);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_ReleaseQueryUGCRequest")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_ReleaseQueryUGCRequest(global::System.IntPtr instancePtr, global::Steamworks.UGCQueryHandle_t handle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_AddRequiredTag")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_AddRequiredTag(global::System.IntPtr instancePtr, global::Steamworks.UGCQueryHandle_t handle, global::Steamworks.InteropHelp.UTF8StringHandle pTagName);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_AddRequiredTagGroup")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_AddRequiredTagGroup(global::System.IntPtr instancePtr, global::Steamworks.UGCQueryHandle_t handle, global::System.IntPtr pTagGroups);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_AddExcludedTag")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_AddExcludedTag(global::System.IntPtr instancePtr, global::Steamworks.UGCQueryHandle_t handle, global::Steamworks.InteropHelp.UTF8StringHandle pTagName);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetReturnOnlyIDs")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetReturnOnlyIDs(global::System.IntPtr instancePtr, global::Steamworks.UGCQueryHandle_t handle, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bReturnOnlyIDs);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetReturnKeyValueTags")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetReturnKeyValueTags(global::System.IntPtr instancePtr, global::Steamworks.UGCQueryHandle_t handle, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bReturnKeyValueTags);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetReturnLongDescription")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetReturnLongDescription(global::System.IntPtr instancePtr, global::Steamworks.UGCQueryHandle_t handle, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bReturnLongDescription);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetReturnMetadata")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetReturnMetadata(global::System.IntPtr instancePtr, global::Steamworks.UGCQueryHandle_t handle, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bReturnMetadata);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetReturnChildren")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetReturnChildren(global::System.IntPtr instancePtr, global::Steamworks.UGCQueryHandle_t handle, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bReturnChildren);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetReturnAdditionalPreviews")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetReturnAdditionalPreviews(global::System.IntPtr instancePtr, global::Steamworks.UGCQueryHandle_t handle, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bReturnAdditionalPreviews);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetReturnTotalOnly")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetReturnTotalOnly(global::System.IntPtr instancePtr, global::Steamworks.UGCQueryHandle_t handle, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bReturnTotalOnly);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetReturnPlaytimeStats")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetReturnPlaytimeStats(global::System.IntPtr instancePtr, global::Steamworks.UGCQueryHandle_t handle, uint unDays);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetLanguage")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetLanguage(global::System.IntPtr instancePtr, global::Steamworks.UGCQueryHandle_t handle, global::Steamworks.InteropHelp.UTF8StringHandle pchLanguage);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetAllowCachedResponse")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetAllowCachedResponse(global::System.IntPtr instancePtr, global::Steamworks.UGCQueryHandle_t handle, uint unMaxAgeSeconds);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetAdminQuery")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetAdminQuery(global::System.IntPtr instancePtr, global::Steamworks.UGCUpdateHandle_t handle, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bAdminQuery);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetCloudFileNameFilter")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetCloudFileNameFilter(global::System.IntPtr instancePtr, global::Steamworks.UGCQueryHandle_t handle, global::Steamworks.InteropHelp.UTF8StringHandle pMatchCloudFileName);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetMatchAnyTag")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetMatchAnyTag(global::System.IntPtr instancePtr, global::Steamworks.UGCQueryHandle_t handle, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bMatchAnyTag);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetSearchText")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetSearchText(global::System.IntPtr instancePtr, global::Steamworks.UGCQueryHandle_t handle, global::Steamworks.InteropHelp.UTF8StringHandle pSearchText);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetRankedByTrendDays")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetRankedByTrendDays(global::System.IntPtr instancePtr, global::Steamworks.UGCQueryHandle_t handle, uint unDays);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetTimeCreatedDateRange")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetTimeCreatedDateRange(global::System.IntPtr instancePtr, global::Steamworks.UGCQueryHandle_t handle, uint rtStart, uint rtEnd);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetTimeUpdatedDateRange")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetTimeUpdatedDateRange(global::System.IntPtr instancePtr, global::Steamworks.UGCQueryHandle_t handle, uint rtStart, uint rtEnd);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_AddRequiredKeyValueTag")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_AddRequiredKeyValueTag(global::System.IntPtr instancePtr, global::Steamworks.UGCQueryHandle_t handle, global::Steamworks.InteropHelp.UTF8StringHandle pKey, global::Steamworks.InteropHelp.UTF8StringHandle pValue);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_RequestUGCDetails")]
		public static extern ulong ISteamUGC_RequestUGCDetails(global::System.IntPtr instancePtr, global::Steamworks.PublishedFileId_t nPublishedFileID, uint unMaxAgeSeconds);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_CreateItem")]
		public static extern ulong ISteamUGC_CreateItem(global::System.IntPtr instancePtr, global::Steamworks.AppId_t nConsumerAppId, global::Steamworks.EWorkshopFileType eFileType);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_StartItemUpdate")]
		public static extern ulong ISteamUGC_StartItemUpdate(global::System.IntPtr instancePtr, global::Steamworks.AppId_t nConsumerAppId, global::Steamworks.PublishedFileId_t nPublishedFileID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetItemTitle")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetItemTitle(global::System.IntPtr instancePtr, global::Steamworks.UGCUpdateHandle_t handle, global::Steamworks.InteropHelp.UTF8StringHandle pchTitle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetItemDescription")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetItemDescription(global::System.IntPtr instancePtr, global::Steamworks.UGCUpdateHandle_t handle, global::Steamworks.InteropHelp.UTF8StringHandle pchDescription);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetItemUpdateLanguage")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetItemUpdateLanguage(global::System.IntPtr instancePtr, global::Steamworks.UGCUpdateHandle_t handle, global::Steamworks.InteropHelp.UTF8StringHandle pchLanguage);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetItemMetadata")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetItemMetadata(global::System.IntPtr instancePtr, global::Steamworks.UGCUpdateHandle_t handle, global::Steamworks.InteropHelp.UTF8StringHandle pchMetaData);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetItemVisibility")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetItemVisibility(global::System.IntPtr instancePtr, global::Steamworks.UGCUpdateHandle_t handle, global::Steamworks.ERemoteStoragePublishedFileVisibility eVisibility);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetItemTags")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetItemTags(global::System.IntPtr instancePtr, global::Steamworks.UGCUpdateHandle_t updateHandle, global::System.IntPtr pTags, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bAllowAdminTags);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetItemContent")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetItemContent(global::System.IntPtr instancePtr, global::Steamworks.UGCUpdateHandle_t handle, global::Steamworks.InteropHelp.UTF8StringHandle pszContentFolder);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetItemPreview")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetItemPreview(global::System.IntPtr instancePtr, global::Steamworks.UGCUpdateHandle_t handle, global::Steamworks.InteropHelp.UTF8StringHandle pszPreviewFile);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetAllowLegacyUpload")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetAllowLegacyUpload(global::System.IntPtr instancePtr, global::Steamworks.UGCUpdateHandle_t handle, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bAllowLegacyUpload);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_RemoveAllItemKeyValueTags")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_RemoveAllItemKeyValueTags(global::System.IntPtr instancePtr, global::Steamworks.UGCUpdateHandle_t handle);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_RemoveItemKeyValueTags")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_RemoveItemKeyValueTags(global::System.IntPtr instancePtr, global::Steamworks.UGCUpdateHandle_t handle, global::Steamworks.InteropHelp.UTF8StringHandle pchKey);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_AddItemKeyValueTag")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_AddItemKeyValueTag(global::System.IntPtr instancePtr, global::Steamworks.UGCUpdateHandle_t handle, global::Steamworks.InteropHelp.UTF8StringHandle pchKey, global::Steamworks.InteropHelp.UTF8StringHandle pchValue);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_AddItemPreviewFile")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_AddItemPreviewFile(global::System.IntPtr instancePtr, global::Steamworks.UGCUpdateHandle_t handle, global::Steamworks.InteropHelp.UTF8StringHandle pszPreviewFile, global::Steamworks.EItemPreviewType type);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_AddItemPreviewVideo")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_AddItemPreviewVideo(global::System.IntPtr instancePtr, global::Steamworks.UGCUpdateHandle_t handle, global::Steamworks.InteropHelp.UTF8StringHandle pszVideoID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_UpdateItemPreviewFile")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_UpdateItemPreviewFile(global::System.IntPtr instancePtr, global::Steamworks.UGCUpdateHandle_t handle, uint index, global::Steamworks.InteropHelp.UTF8StringHandle pszPreviewFile);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_UpdateItemPreviewVideo")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_UpdateItemPreviewVideo(global::System.IntPtr instancePtr, global::Steamworks.UGCUpdateHandle_t handle, uint index, global::Steamworks.InteropHelp.UTF8StringHandle pszVideoID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_RemoveItemPreview")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_RemoveItemPreview(global::System.IntPtr instancePtr, global::Steamworks.UGCUpdateHandle_t handle, uint index);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_AddContentDescriptor")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_AddContentDescriptor(global::System.IntPtr instancePtr, global::Steamworks.UGCUpdateHandle_t handle, global::Steamworks.EUGCContentDescriptorID descid);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_RemoveContentDescriptor")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_RemoveContentDescriptor(global::System.IntPtr instancePtr, global::Steamworks.UGCUpdateHandle_t handle, global::Steamworks.EUGCContentDescriptorID descid);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetRequiredGameVersions")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetRequiredGameVersions(global::System.IntPtr instancePtr, global::Steamworks.UGCUpdateHandle_t handle, global::Steamworks.InteropHelp.UTF8StringHandle pszGameBranchMin, global::Steamworks.InteropHelp.UTF8StringHandle pszGameBranchMax);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SubmitItemUpdate")]
		public static extern ulong ISteamUGC_SubmitItemUpdate(global::System.IntPtr instancePtr, global::Steamworks.UGCUpdateHandle_t handle, global::Steamworks.InteropHelp.UTF8StringHandle pchChangeNote);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetItemUpdateProgress")]
		public static extern global::Steamworks.EItemUpdateStatus ISteamUGC_GetItemUpdateProgress(global::System.IntPtr instancePtr, global::Steamworks.UGCUpdateHandle_t handle, out ulong punBytesProcessed, out ulong punBytesTotal);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetUserItemVote")]
		public static extern ulong ISteamUGC_SetUserItemVote(global::System.IntPtr instancePtr, global::Steamworks.PublishedFileId_t nPublishedFileID, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bVoteUp);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetUserItemVote")]
		public static extern ulong ISteamUGC_GetUserItemVote(global::System.IntPtr instancePtr, global::Steamworks.PublishedFileId_t nPublishedFileID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_AddItemToFavorites")]
		public static extern ulong ISteamUGC_AddItemToFavorites(global::System.IntPtr instancePtr, global::Steamworks.AppId_t nAppId, global::Steamworks.PublishedFileId_t nPublishedFileID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_RemoveItemFromFavorites")]
		public static extern ulong ISteamUGC_RemoveItemFromFavorites(global::System.IntPtr instancePtr, global::Steamworks.AppId_t nAppId, global::Steamworks.PublishedFileId_t nPublishedFileID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SubscribeItem")]
		public static extern ulong ISteamUGC_SubscribeItem(global::System.IntPtr instancePtr, global::Steamworks.PublishedFileId_t nPublishedFileID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_UnsubscribeItem")]
		public static extern ulong ISteamUGC_UnsubscribeItem(global::System.IntPtr instancePtr, global::Steamworks.PublishedFileId_t nPublishedFileID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetNumSubscribedItems")]
		public static extern uint ISteamUGC_GetNumSubscribedItems(global::System.IntPtr instancePtr, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bIncludeLocallyDisabled);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetSubscribedItems")]
		public static extern uint ISteamUGC_GetSubscribedItems(global::System.IntPtr instancePtr, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.PublishedFileId_t[] pvecPublishedFileID, uint cMaxEntries, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bIncludeLocallyDisabled);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetItemState")]
		public static extern uint ISteamUGC_GetItemState(global::System.IntPtr instancePtr, global::Steamworks.PublishedFileId_t nPublishedFileID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetItemInstallInfo")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_GetItemInstallInfo(global::System.IntPtr instancePtr, global::Steamworks.PublishedFileId_t nPublishedFileID, out ulong punSizeOnDisk, global::System.IntPtr pchFolder, uint cchFolderSize, out uint punTimeStamp);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetItemDownloadInfo")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_GetItemDownloadInfo(global::System.IntPtr instancePtr, global::Steamworks.PublishedFileId_t nPublishedFileID, out ulong punBytesDownloaded, out ulong punBytesTotal);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_DownloadItem")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_DownloadItem(global::System.IntPtr instancePtr, global::Steamworks.PublishedFileId_t nPublishedFileID, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bHighPriority);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_BInitWorkshopForGameServer")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_BInitWorkshopForGameServer(global::System.IntPtr instancePtr, global::Steamworks.DepotId_t unWorkshopDepotID, global::Steamworks.InteropHelp.UTF8StringHandle pszFolder);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SuspendDownloads")]
		public static extern void ISteamUGC_SuspendDownloads(global::System.IntPtr instancePtr, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bSuspend);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_StartPlaytimeTracking")]
		public static extern ulong ISteamUGC_StartPlaytimeTracking(global::System.IntPtr instancePtr, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.PublishedFileId_t[] pvecPublishedFileID, uint unNumPublishedFileIDs);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_StopPlaytimeTracking")]
		public static extern ulong ISteamUGC_StopPlaytimeTracking(global::System.IntPtr instancePtr, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.PublishedFileId_t[] pvecPublishedFileID, uint unNumPublishedFileIDs);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_StopPlaytimeTrackingForAllItems")]
		public static extern ulong ISteamUGC_StopPlaytimeTrackingForAllItems(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_AddDependency")]
		public static extern ulong ISteamUGC_AddDependency(global::System.IntPtr instancePtr, global::Steamworks.PublishedFileId_t nParentPublishedFileID, global::Steamworks.PublishedFileId_t nChildPublishedFileID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_RemoveDependency")]
		public static extern ulong ISteamUGC_RemoveDependency(global::System.IntPtr instancePtr, global::Steamworks.PublishedFileId_t nParentPublishedFileID, global::Steamworks.PublishedFileId_t nChildPublishedFileID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_AddAppDependency")]
		public static extern ulong ISteamUGC_AddAppDependency(global::System.IntPtr instancePtr, global::Steamworks.PublishedFileId_t nPublishedFileID, global::Steamworks.AppId_t nAppID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_RemoveAppDependency")]
		public static extern ulong ISteamUGC_RemoveAppDependency(global::System.IntPtr instancePtr, global::Steamworks.PublishedFileId_t nPublishedFileID, global::Steamworks.AppId_t nAppID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetAppDependencies")]
		public static extern ulong ISteamUGC_GetAppDependencies(global::System.IntPtr instancePtr, global::Steamworks.PublishedFileId_t nPublishedFileID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_DeleteItem")]
		public static extern ulong ISteamUGC_DeleteItem(global::System.IntPtr instancePtr, global::Steamworks.PublishedFileId_t nPublishedFileID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_ShowWorkshopEULA")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_ShowWorkshopEULA(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetWorkshopEULAStatus")]
		public static extern ulong ISteamUGC_GetWorkshopEULAStatus(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetUserContentDescriptorPreferences")]
		public static extern uint ISteamUGC_GetUserContentDescriptorPreferences(global::System.IntPtr instancePtr, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.EUGCContentDescriptorID[] pvecDescriptors, uint cMaxEntries);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetItemsDisabledLocally")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetItemsDisabledLocally(global::System.IntPtr instancePtr, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.PublishedFileId_t[] pvecPublishedFileIDs, uint unNumPublishedFileIDs, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bDisabledLocally);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetSubscriptionsLoadOrder")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUGC_SetSubscriptionsLoadOrder(global::System.IntPtr instancePtr, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.PublishedFileId_t[] pvecPublishedFileIDs, uint unNumPublishedFileIDs);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_GetHSteamUser")]
		public static extern int ISteamUser_GetHSteamUser(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_BLoggedOn")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUser_BLoggedOn(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_GetSteamID")]
		public static extern ulong ISteamUser_GetSteamID(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_InitiateGameConnection_DEPRECATED")]
		public static extern int ISteamUser_InitiateGameConnection_DEPRECATED(global::System.IntPtr instancePtr, byte[] pAuthBlob, int cbMaxAuthBlob, global::Steamworks.CSteamID steamIDGameServer, uint unIPServer, ushort usPortServer, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bSecure);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_TerminateGameConnection_DEPRECATED")]
		public static extern void ISteamUser_TerminateGameConnection_DEPRECATED(global::System.IntPtr instancePtr, uint unIPServer, ushort usPortServer);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_TrackAppUsageEvent")]
		public static extern void ISteamUser_TrackAppUsageEvent(global::System.IntPtr instancePtr, global::Steamworks.CGameID gameID, int eAppUsageEvent, global::Steamworks.InteropHelp.UTF8StringHandle pchExtraInfo);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_GetUserDataFolder")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUser_GetUserDataFolder(global::System.IntPtr instancePtr, global::System.IntPtr pchBuffer, int cubBuffer);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_StartVoiceRecording")]
		public static extern void ISteamUser_StartVoiceRecording(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_StopVoiceRecording")]
		public static extern void ISteamUser_StopVoiceRecording(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_GetAvailableVoice")]
		public static extern global::Steamworks.EVoiceResult ISteamUser_GetAvailableVoice(global::System.IntPtr instancePtr, out uint pcbCompressed, global::System.IntPtr pcbUncompressed_Deprecated, uint nUncompressedVoiceDesiredSampleRate_Deprecated);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_GetVoice")]
		public static extern global::Steamworks.EVoiceResult ISteamUser_GetVoice(global::System.IntPtr instancePtr, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bWantCompressed, byte[] pDestBuffer, uint cbDestBufferSize, out uint nBytesWritten, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bWantUncompressed_Deprecated, global::System.IntPtr pUncompressedDestBuffer_Deprecated, uint cbUncompressedDestBufferSize_Deprecated, global::System.IntPtr nUncompressBytesWritten_Deprecated, uint nUncompressedVoiceDesiredSampleRate_Deprecated);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_DecompressVoice")]
		public static extern global::Steamworks.EVoiceResult ISteamUser_DecompressVoice(global::System.IntPtr instancePtr, byte[] pCompressed, uint cbCompressed, byte[] pDestBuffer, uint cbDestBufferSize, out uint nBytesWritten, uint nDesiredSampleRate);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_GetVoiceOptimalSampleRate")]
		public static extern uint ISteamUser_GetVoiceOptimalSampleRate(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_GetAuthSessionTicket")]
		public static extern uint ISteamUser_GetAuthSessionTicket(global::System.IntPtr instancePtr, byte[] pTicket, int cbMaxTicket, out uint pcbTicket, ref global::Steamworks.SteamNetworkingIdentity pSteamNetworkingIdentity);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_GetAuthTicketForWebApi")]
		public static extern uint ISteamUser_GetAuthTicketForWebApi(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchIdentity);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_BeginAuthSession")]
		public static extern global::Steamworks.EBeginAuthSessionResult ISteamUser_BeginAuthSession(global::System.IntPtr instancePtr, byte[] pAuthTicket, int cbAuthTicket, global::Steamworks.CSteamID steamID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_EndAuthSession")]
		public static extern void ISteamUser_EndAuthSession(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_CancelAuthTicket")]
		public static extern void ISteamUser_CancelAuthTicket(global::System.IntPtr instancePtr, global::Steamworks.HAuthTicket hAuthTicket);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_UserHasLicenseForApp")]
		public static extern global::Steamworks.EUserHasLicenseForAppResult ISteamUser_UserHasLicenseForApp(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamID, global::Steamworks.AppId_t appID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_BIsBehindNAT")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUser_BIsBehindNAT(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_AdvertiseGame")]
		public static extern void ISteamUser_AdvertiseGame(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDGameServer, uint unIPServer, ushort usPortServer);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_RequestEncryptedAppTicket")]
		public static extern ulong ISteamUser_RequestEncryptedAppTicket(global::System.IntPtr instancePtr, byte[] pDataToInclude, int cbDataToInclude);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_GetEncryptedAppTicket")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUser_GetEncryptedAppTicket(global::System.IntPtr instancePtr, byte[] pTicket, int cbMaxTicket, out uint pcbTicket);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_GetGameBadgeLevel")]
		public static extern int ISteamUser_GetGameBadgeLevel(global::System.IntPtr instancePtr, int nSeries, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bFoil);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_GetPlayerSteamLevel")]
		public static extern int ISteamUser_GetPlayerSteamLevel(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_RequestStoreAuthURL")]
		public static extern ulong ISteamUser_RequestStoreAuthURL(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchRedirectURL);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_BIsPhoneVerified")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUser_BIsPhoneVerified(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_BIsTwoFactorEnabled")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUser_BIsTwoFactorEnabled(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_BIsPhoneIdentifying")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUser_BIsPhoneIdentifying(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_BIsPhoneRequiringVerification")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUser_BIsPhoneRequiringVerification(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_GetMarketEligibility")]
		public static extern ulong ISteamUser_GetMarketEligibility(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_GetDurationControl")]
		public static extern ulong ISteamUser_GetDurationControl(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_BSetDurationControlOnlineState")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUser_BSetDurationControlOnlineState(global::System.IntPtr instancePtr, global::Steamworks.EDurationControlOnlineState eNewState);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetStatInt32")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUserStats_GetStatInt32(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchName, out int pData);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetStatFloat")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUserStats_GetStatFloat(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchName, out float pData);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_SetStatInt32")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUserStats_SetStatInt32(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchName, int nData);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_SetStatFloat")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUserStats_SetStatFloat(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchName, float fData);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_UpdateAvgRateStat")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUserStats_UpdateAvgRateStat(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchName, float flCountThisSession, double dSessionLength);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetAchievement")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUserStats_GetAchievement(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchName, out bool pbAchieved);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_SetAchievement")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUserStats_SetAchievement(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchName);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_ClearAchievement")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUserStats_ClearAchievement(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchName);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetAchievementAndUnlockTime")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUserStats_GetAchievementAndUnlockTime(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchName, out bool pbAchieved, out uint punUnlockTime);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_StoreStats")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUserStats_StoreStats(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetAchievementIcon")]
		public static extern int ISteamUserStats_GetAchievementIcon(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchName);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetAchievementDisplayAttribute")]
		public static extern global::System.IntPtr ISteamUserStats_GetAchievementDisplayAttribute(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchName, global::Steamworks.InteropHelp.UTF8StringHandle pchKey);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_IndicateAchievementProgress")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUserStats_IndicateAchievementProgress(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchName, uint nCurProgress, uint nMaxProgress);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetNumAchievements")]
		public static extern uint ISteamUserStats_GetNumAchievements(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetAchievementName")]
		public static extern global::System.IntPtr ISteamUserStats_GetAchievementName(global::System.IntPtr instancePtr, uint iAchievement);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_RequestUserStats")]
		public static extern ulong ISteamUserStats_RequestUserStats(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDUser);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetUserStatInt32")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUserStats_GetUserStatInt32(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDUser, global::Steamworks.InteropHelp.UTF8StringHandle pchName, out int pData);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetUserStatFloat")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUserStats_GetUserStatFloat(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDUser, global::Steamworks.InteropHelp.UTF8StringHandle pchName, out float pData);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetUserAchievement")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUserStats_GetUserAchievement(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDUser, global::Steamworks.InteropHelp.UTF8StringHandle pchName, out bool pbAchieved);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetUserAchievementAndUnlockTime")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUserStats_GetUserAchievementAndUnlockTime(global::System.IntPtr instancePtr, global::Steamworks.CSteamID steamIDUser, global::Steamworks.InteropHelp.UTF8StringHandle pchName, out bool pbAchieved, out uint punUnlockTime);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_ResetAllStats")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUserStats_ResetAllStats(global::System.IntPtr instancePtr, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bAchievementsToo);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_FindOrCreateLeaderboard")]
		public static extern ulong ISteamUserStats_FindOrCreateLeaderboard(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchLeaderboardName, global::Steamworks.ELeaderboardSortMethod eLeaderboardSortMethod, global::Steamworks.ELeaderboardDisplayType eLeaderboardDisplayType);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_FindLeaderboard")]
		public static extern ulong ISteamUserStats_FindLeaderboard(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchLeaderboardName);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetLeaderboardName")]
		public static extern global::System.IntPtr ISteamUserStats_GetLeaderboardName(global::System.IntPtr instancePtr, global::Steamworks.SteamLeaderboard_t hSteamLeaderboard);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetLeaderboardEntryCount")]
		public static extern int ISteamUserStats_GetLeaderboardEntryCount(global::System.IntPtr instancePtr, global::Steamworks.SteamLeaderboard_t hSteamLeaderboard);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetLeaderboardSortMethod")]
		public static extern global::Steamworks.ELeaderboardSortMethod ISteamUserStats_GetLeaderboardSortMethod(global::System.IntPtr instancePtr, global::Steamworks.SteamLeaderboard_t hSteamLeaderboard);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetLeaderboardDisplayType")]
		public static extern global::Steamworks.ELeaderboardDisplayType ISteamUserStats_GetLeaderboardDisplayType(global::System.IntPtr instancePtr, global::Steamworks.SteamLeaderboard_t hSteamLeaderboard);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_DownloadLeaderboardEntries")]
		public static extern ulong ISteamUserStats_DownloadLeaderboardEntries(global::System.IntPtr instancePtr, global::Steamworks.SteamLeaderboard_t hSteamLeaderboard, global::Steamworks.ELeaderboardDataRequest eLeaderboardDataRequest, int nRangeStart, int nRangeEnd);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_DownloadLeaderboardEntriesForUsers")]
		public static extern ulong ISteamUserStats_DownloadLeaderboardEntriesForUsers(global::System.IntPtr instancePtr, global::Steamworks.SteamLeaderboard_t hSteamLeaderboard, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] global::Steamworks.CSteamID[] prgUsers, int cUsers);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetDownloadedLeaderboardEntry")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUserStats_GetDownloadedLeaderboardEntry(global::System.IntPtr instancePtr, global::Steamworks.SteamLeaderboardEntries_t hSteamLeaderboardEntries, int index, out global::Steamworks.LeaderboardEntry_t pLeaderboardEntry, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] int[] pDetails, int cDetailsMax);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_UploadLeaderboardScore")]
		public static extern ulong ISteamUserStats_UploadLeaderboardScore(global::System.IntPtr instancePtr, global::Steamworks.SteamLeaderboard_t hSteamLeaderboard, global::Steamworks.ELeaderboardUploadScoreMethod eLeaderboardUploadScoreMethod, int nScore, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] int[] pScoreDetails, int cScoreDetailsCount);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_AttachLeaderboardUGC")]
		public static extern ulong ISteamUserStats_AttachLeaderboardUGC(global::System.IntPtr instancePtr, global::Steamworks.SteamLeaderboard_t hSteamLeaderboard, global::Steamworks.UGCHandle_t hUGC);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetNumberOfCurrentPlayers")]
		public static extern ulong ISteamUserStats_GetNumberOfCurrentPlayers(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_RequestGlobalAchievementPercentages")]
		public static extern ulong ISteamUserStats_RequestGlobalAchievementPercentages(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetMostAchievedAchievementInfo")]
		public static extern int ISteamUserStats_GetMostAchievedAchievementInfo(global::System.IntPtr instancePtr, global::System.IntPtr pchName, uint unNameBufLen, out float pflPercent, out bool pbAchieved);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetNextMostAchievedAchievementInfo")]
		public static extern int ISteamUserStats_GetNextMostAchievedAchievementInfo(global::System.IntPtr instancePtr, int iIteratorPrevious, global::System.IntPtr pchName, uint unNameBufLen, out float pflPercent, out bool pbAchieved);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetAchievementAchievedPercent")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUserStats_GetAchievementAchievedPercent(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchName, out float pflPercent);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_RequestGlobalStats")]
		public static extern ulong ISteamUserStats_RequestGlobalStats(global::System.IntPtr instancePtr, int nHistoryDays);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetGlobalStatInt64")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUserStats_GetGlobalStatInt64(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchStatName, out long pData);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetGlobalStatDouble")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUserStats_GetGlobalStatDouble(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchStatName, out double pData);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetGlobalStatHistoryInt64")]
		public static extern int ISteamUserStats_GetGlobalStatHistoryInt64(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchStatName, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] long[] pData, uint cubData);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetGlobalStatHistoryDouble")]
		public static extern int ISteamUserStats_GetGlobalStatHistoryDouble(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchStatName, [global::System.Runtime.InteropServices.In][global::System.Runtime.InteropServices.Out] double[] pData, uint cubData);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetAchievementProgressLimitsInt32")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUserStats_GetAchievementProgressLimitsInt32(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchName, out int pnMinProgress, out int pnMaxProgress);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetAchievementProgressLimitsFloat")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUserStats_GetAchievementProgressLimitsFloat(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle pchName, out float pfMinProgress, out float pfMaxProgress);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetSecondsSinceAppActive")]
		public static extern uint ISteamUtils_GetSecondsSinceAppActive(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetSecondsSinceComputerActive")]
		public static extern uint ISteamUtils_GetSecondsSinceComputerActive(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetConnectedUniverse")]
		public static extern global::Steamworks.EUniverse ISteamUtils_GetConnectedUniverse(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetServerRealTime")]
		public static extern uint ISteamUtils_GetServerRealTime(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetIPCountry")]
		public static extern global::System.IntPtr ISteamUtils_GetIPCountry(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetImageSize")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUtils_GetImageSize(global::System.IntPtr instancePtr, int iImage, out uint pnWidth, out uint pnHeight);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetImageRGBA")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUtils_GetImageRGBA(global::System.IntPtr instancePtr, int iImage, byte[] pubDest, int nDestBufferSize);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetCurrentBatteryPower")]
		public static extern byte ISteamUtils_GetCurrentBatteryPower(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetAppID")]
		public static extern uint ISteamUtils_GetAppID(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_SetOverlayNotificationPosition")]
		public static extern void ISteamUtils_SetOverlayNotificationPosition(global::System.IntPtr instancePtr, global::Steamworks.ENotificationPosition eNotificationPosition);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_IsAPICallCompleted")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUtils_IsAPICallCompleted(global::System.IntPtr instancePtr, global::Steamworks.SteamAPICall_t hSteamAPICall, out bool pbFailed);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetAPICallFailureReason")]
		public static extern global::Steamworks.ESteamAPICallFailure ISteamUtils_GetAPICallFailureReason(global::System.IntPtr instancePtr, global::Steamworks.SteamAPICall_t hSteamAPICall);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetAPICallResult")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUtils_GetAPICallResult(global::System.IntPtr instancePtr, global::Steamworks.SteamAPICall_t hSteamAPICall, global::System.IntPtr pCallback, int cubCallback, int iCallbackExpected, out bool pbFailed);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetIPCCallCount")]
		public static extern uint ISteamUtils_GetIPCCallCount(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_SetWarningMessageHook")]
		public static extern void ISteamUtils_SetWarningMessageHook(global::System.IntPtr instancePtr, global::Steamworks.SteamAPIWarningMessageHook_t pFunction);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_IsOverlayEnabled")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUtils_IsOverlayEnabled(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_BOverlayNeedsPresent")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUtils_BOverlayNeedsPresent(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_CheckFileSignature")]
		public static extern ulong ISteamUtils_CheckFileSignature(global::System.IntPtr instancePtr, global::Steamworks.InteropHelp.UTF8StringHandle szFileName);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_ShowGamepadTextInput")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUtils_ShowGamepadTextInput(global::System.IntPtr instancePtr, global::Steamworks.EGamepadTextInputMode eInputMode, global::Steamworks.EGamepadTextInputLineMode eLineInputMode, global::Steamworks.InteropHelp.UTF8StringHandle pchDescription, uint unCharMax, global::Steamworks.InteropHelp.UTF8StringHandle pchExistingText);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetEnteredGamepadTextLength")]
		public static extern uint ISteamUtils_GetEnteredGamepadTextLength(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetEnteredGamepadTextInput")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUtils_GetEnteredGamepadTextInput(global::System.IntPtr instancePtr, global::System.IntPtr pchText, uint cchText);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetSteamUILanguage")]
		public static extern global::System.IntPtr ISteamUtils_GetSteamUILanguage(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_IsSteamRunningInVR")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUtils_IsSteamRunningInVR(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_SetOverlayNotificationInset")]
		public static extern void ISteamUtils_SetOverlayNotificationInset(global::System.IntPtr instancePtr, int nHorizontalInset, int nVerticalInset);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_IsSteamInBigPictureMode")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUtils_IsSteamInBigPictureMode(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_StartVRDashboard")]
		public static extern void ISteamUtils_StartVRDashboard(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_IsVRHeadsetStreamingEnabled")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUtils_IsVRHeadsetStreamingEnabled(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_SetVRHeadsetStreamingEnabled")]
		public static extern void ISteamUtils_SetVRHeadsetStreamingEnabled(global::System.IntPtr instancePtr, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bEnabled);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_IsSteamChinaLauncher")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUtils_IsSteamChinaLauncher(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_InitFilterText")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUtils_InitFilterText(global::System.IntPtr instancePtr, uint unFilterOptions);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_FilterText")]
		public static extern int ISteamUtils_FilterText(global::System.IntPtr instancePtr, global::Steamworks.ETextFilteringContext eContext, global::Steamworks.CSteamID sourceSteamID, global::Steamworks.InteropHelp.UTF8StringHandle pchInputMessage, global::System.IntPtr pchOutFilteredText, uint nByteSizeOutFilteredText);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetIPv6ConnectivityState")]
		public static extern global::Steamworks.ESteamIPv6ConnectivityState ISteamUtils_GetIPv6ConnectivityState(global::System.IntPtr instancePtr, global::Steamworks.ESteamIPv6ConnectivityProtocol eProtocol);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_IsSteamRunningOnSteamDeck")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUtils_IsSteamRunningOnSteamDeck(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_ShowFloatingGamepadTextInput")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUtils_ShowFloatingGamepadTextInput(global::System.IntPtr instancePtr, global::Steamworks.EFloatingGamepadTextInputMode eKeyboardMode, int nTextFieldXPosition, int nTextFieldYPosition, int nTextFieldWidth, int nTextFieldHeight);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_SetGameLauncherMode")]
		public static extern void ISteamUtils_SetGameLauncherMode(global::System.IntPtr instancePtr, [global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)] bool bLauncherMode);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_DismissFloatingGamepadTextInput")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUtils_DismissFloatingGamepadTextInput(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_DismissGamepadTextInput")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamUtils_DismissGamepadTextInput(global::System.IntPtr instancePtr);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamVideo_GetVideoURL")]
		public static extern void ISteamVideo_GetVideoURL(global::System.IntPtr instancePtr, global::Steamworks.AppId_t unVideoAppID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamVideo_IsBroadcasting")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamVideo_IsBroadcasting(global::System.IntPtr instancePtr, out int pnNumViewers);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamVideo_GetOPFSettings")]
		public static extern void ISteamVideo_GetOPFSettings(global::System.IntPtr instancePtr, global::Steamworks.AppId_t unVideoAppID);

		[global::System.Runtime.InteropServices.DllImport("steam_api64", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamVideo_GetOPFStringForApp")]
		[return: global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public static extern bool ISteamVideo_GetOPFStringForApp(global::System.IntPtr instancePtr, global::Steamworks.AppId_t unVideoAppID, global::System.IntPtr pchBuffer, ref int pnBufferSize);
	}
}
