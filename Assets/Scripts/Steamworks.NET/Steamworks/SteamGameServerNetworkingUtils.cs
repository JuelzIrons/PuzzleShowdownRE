namespace Steamworks
{
	public static class SteamGameServerNetworkingUtils
	{
		public static global::System.IntPtr AllocateMessage(int cbAllocateBuffer)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworkingUtils_AllocateMessage(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworkingUtils(), cbAllocateBuffer);
		}

		public static void InitRelayNetworkAccess()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::Steamworks.NativeMethods.ISteamNetworkingUtils_InitRelayNetworkAccess(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworkingUtils());
		}

		public static global::Steamworks.ESteamNetworkingAvailability GetRelayNetworkStatus(out global::Steamworks.SteamRelayNetworkStatus_t pDetails)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworkingUtils_GetRelayNetworkStatus(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworkingUtils(), out pDetails);
		}

		public static float GetLocalPingLocation(out global::Steamworks.SteamNetworkPingLocation_t result)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworkingUtils_GetLocalPingLocation(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworkingUtils(), out result);
		}

		public static int EstimatePingTimeBetweenTwoLocations(ref global::Steamworks.SteamNetworkPingLocation_t location1, ref global::Steamworks.SteamNetworkPingLocation_t location2)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworkingUtils_EstimatePingTimeBetweenTwoLocations(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworkingUtils(), ref location1, ref location2);
		}

		public static int EstimatePingTimeFromLocalHost(ref global::Steamworks.SteamNetworkPingLocation_t remoteLocation)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworkingUtils_EstimatePingTimeFromLocalHost(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworkingUtils(), ref remoteLocation);
		}

		public static void ConvertPingLocationToString(ref global::Steamworks.SteamNetworkPingLocation_t location, out string pszBuf, int cchBufSize)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(cchBufSize);
			global::Steamworks.NativeMethods.ISteamNetworkingUtils_ConvertPingLocationToString(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworkingUtils(), ref location, intPtr, cchBufSize);
			pszBuf = global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
		}

		public static bool ParsePingLocationString(string pszString, out global::Steamworks.SteamNetworkPingLocation_t result)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pszString2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pszString);
			return global::Steamworks.NativeMethods.ISteamNetworkingUtils_ParsePingLocationString(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworkingUtils(), pszString2, out result);
		}

		public static bool CheckPingDataUpToDate(float flMaxAgeSeconds)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworkingUtils_CheckPingDataUpToDate(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworkingUtils(), flMaxAgeSeconds);
		}

		public static int GetPingToDataCenter(global::Steamworks.SteamNetworkingPOPID popID, out global::Steamworks.SteamNetworkingPOPID pViaRelayPoP)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworkingUtils_GetPingToDataCenter(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworkingUtils(), popID, out pViaRelayPoP);
		}

		public static int GetDirectPingToPOP(global::Steamworks.SteamNetworkingPOPID popID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworkingUtils_GetDirectPingToPOP(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworkingUtils(), popID);
		}

		public static int GetPOPCount()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworkingUtils_GetPOPCount(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworkingUtils());
		}

		public static int GetPOPList(out global::Steamworks.SteamNetworkingPOPID list, int nListSz)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworkingUtils_GetPOPList(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworkingUtils(), out list, nListSz);
		}

		public static global::Steamworks.SteamNetworkingMicroseconds GetLocalTimestamp()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.SteamNetworkingMicroseconds)global::Steamworks.NativeMethods.ISteamNetworkingUtils_GetLocalTimestamp(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworkingUtils());
		}

		public static void SetDebugOutputFunction(global::Steamworks.ESteamNetworkingSocketsDebugOutputType eDetailLevel, global::Steamworks.FSteamNetworkingSocketsDebugOutput pfnFunc)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::Steamworks.NativeMethods.ISteamNetworkingUtils_SetDebugOutputFunction(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworkingUtils(), eDetailLevel, pfnFunc);
		}

		public static bool IsFakeIPv4(uint nIPv4)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworkingUtils_IsFakeIPv4(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworkingUtils(), nIPv4);
		}

		public static global::Steamworks.ESteamNetworkingFakeIPType GetIPv4FakeIPType(uint nIPv4)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworkingUtils_GetIPv4FakeIPType(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworkingUtils(), nIPv4);
		}

		public static global::Steamworks.EResult GetRealIdentityForFakeIP(ref global::Steamworks.SteamNetworkingIPAddr fakeIP, out global::Steamworks.SteamNetworkingIdentity pOutRealIdentity)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworkingUtils_GetRealIdentityForFakeIP(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworkingUtils(), ref fakeIP, out pOutRealIdentity);
		}

		public static bool SetConfigValue(global::Steamworks.ESteamNetworkingConfigValue eValue, global::Steamworks.ESteamNetworkingConfigScope eScopeType, global::System.IntPtr scopeObj, global::Steamworks.ESteamNetworkingConfigDataType eDataType, global::System.IntPtr pArg)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworkingUtils_SetConfigValue(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworkingUtils(), eValue, eScopeType, scopeObj, eDataType, pArg);
		}

		public static global::Steamworks.ESteamNetworkingGetConfigValueResult GetConfigValue(global::Steamworks.ESteamNetworkingConfigValue eValue, global::Steamworks.ESteamNetworkingConfigScope eScopeType, global::System.IntPtr scopeObj, out global::Steamworks.ESteamNetworkingConfigDataType pOutDataType, global::System.IntPtr pResult, ref ulong cbResult)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworkingUtils_GetConfigValue(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworkingUtils(), eValue, eScopeType, scopeObj, out pOutDataType, pResult, ref cbResult);
		}

		public static string GetConfigValueInfo(global::Steamworks.ESteamNetworkingConfigValue eValue, out global::Steamworks.ESteamNetworkingConfigDataType pOutDataType, out global::Steamworks.ESteamNetworkingConfigScope pOutScope)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.InteropHelp.PtrToStringUTF8(global::Steamworks.NativeMethods.ISteamNetworkingUtils_GetConfigValueInfo(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworkingUtils(), eValue, out pOutDataType, out pOutScope));
		}

		public static global::Steamworks.ESteamNetworkingConfigValue IterateGenericEditableConfigValues(global::Steamworks.ESteamNetworkingConfigValue eCurrent, bool bEnumerateDevVars)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworkingUtils_IterateGenericEditableConfigValues(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworkingUtils(), eCurrent, bEnumerateDevVars);
		}

		public static void SteamNetworkingIPAddr_ToString(ref global::Steamworks.SteamNetworkingIPAddr addr, out string buf, uint cbBuf, bool bWithPort)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal((int)cbBuf);
			global::Steamworks.NativeMethods.ISteamNetworkingUtils_SteamNetworkingIPAddr_ToString(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworkingUtils(), ref addr, intPtr, cbBuf, bWithPort);
			buf = global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
		}

		public static bool SteamNetworkingIPAddr_ParseString(out global::Steamworks.SteamNetworkingIPAddr pAddr, string pszStr)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pszStr2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pszStr);
			return global::Steamworks.NativeMethods.ISteamNetworkingUtils_SteamNetworkingIPAddr_ParseString(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworkingUtils(), out pAddr, pszStr2);
		}

		public static global::Steamworks.ESteamNetworkingFakeIPType SteamNetworkingIPAddr_GetFakeIPType(ref global::Steamworks.SteamNetworkingIPAddr addr)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworkingUtils_SteamNetworkingIPAddr_GetFakeIPType(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworkingUtils(), ref addr);
		}

		public static void SteamNetworkingIdentity_ToString(ref global::Steamworks.SteamNetworkingIdentity identity, out string buf, uint cbBuf)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal((int)cbBuf);
			global::Steamworks.NativeMethods.ISteamNetworkingUtils_SteamNetworkingIdentity_ToString(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworkingUtils(), ref identity, intPtr, cbBuf);
			buf = global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
		}

		public static bool SteamNetworkingIdentity_ParseString(out global::Steamworks.SteamNetworkingIdentity pIdentity, string pszStr)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pszStr2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pszStr);
			return global::Steamworks.NativeMethods.ISteamNetworkingUtils_SteamNetworkingIdentity_ParseString(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworkingUtils(), out pIdentity, pszStr2);
		}
	}
}
