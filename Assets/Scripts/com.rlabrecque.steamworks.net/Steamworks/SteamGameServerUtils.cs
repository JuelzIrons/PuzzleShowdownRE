namespace Steamworks
{
	public static class SteamGameServerUtils
	{
		public static uint GetSecondsSinceAppActive()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUtils_GetSecondsSinceAppActive(global::Steamworks.CSteamGameServerAPIContext.GetSteamUtils());
		}

		public static uint GetSecondsSinceComputerActive()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUtils_GetSecondsSinceComputerActive(global::Steamworks.CSteamGameServerAPIContext.GetSteamUtils());
		}

		public static global::Steamworks.EUniverse GetConnectedUniverse()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUtils_GetConnectedUniverse(global::Steamworks.CSteamGameServerAPIContext.GetSteamUtils());
		}

		public static uint GetServerRealTime()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUtils_GetServerRealTime(global::Steamworks.CSteamGameServerAPIContext.GetSteamUtils());
		}

		public static string GetIPCountry()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.InteropHelp.PtrToStringUTF8(global::Steamworks.NativeMethods.ISteamUtils_GetIPCountry(global::Steamworks.CSteamGameServerAPIContext.GetSteamUtils()));
		}

		public static bool GetImageSize(int iImage, out uint pnWidth, out uint pnHeight)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUtils_GetImageSize(global::Steamworks.CSteamGameServerAPIContext.GetSteamUtils(), iImage, out pnWidth, out pnHeight);
		}

		public static bool GetImageRGBA(int iImage, byte[] pubDest, int nDestBufferSize)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUtils_GetImageRGBA(global::Steamworks.CSteamGameServerAPIContext.GetSteamUtils(), iImage, pubDest, nDestBufferSize);
		}

		public static byte GetCurrentBatteryPower()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUtils_GetCurrentBatteryPower(global::Steamworks.CSteamGameServerAPIContext.GetSteamUtils());
		}

		public static global::Steamworks.AppId_t GetAppID()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.AppId_t)global::Steamworks.NativeMethods.ISteamUtils_GetAppID(global::Steamworks.CSteamGameServerAPIContext.GetSteamUtils());
		}

		public static void SetOverlayNotificationPosition(global::Steamworks.ENotificationPosition eNotificationPosition)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::Steamworks.NativeMethods.ISteamUtils_SetOverlayNotificationPosition(global::Steamworks.CSteamGameServerAPIContext.GetSteamUtils(), eNotificationPosition);
		}

		public static bool IsAPICallCompleted(global::Steamworks.SteamAPICall_t hSteamAPICall, out bool pbFailed)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUtils_IsAPICallCompleted(global::Steamworks.CSteamGameServerAPIContext.GetSteamUtils(), hSteamAPICall, out pbFailed);
		}

		public static global::Steamworks.ESteamAPICallFailure GetAPICallFailureReason(global::Steamworks.SteamAPICall_t hSteamAPICall)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUtils_GetAPICallFailureReason(global::Steamworks.CSteamGameServerAPIContext.GetSteamUtils(), hSteamAPICall);
		}

		public static bool GetAPICallResult(global::Steamworks.SteamAPICall_t hSteamAPICall, global::System.IntPtr pCallback, int cubCallback, int iCallbackExpected, out bool pbFailed)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUtils_GetAPICallResult(global::Steamworks.CSteamGameServerAPIContext.GetSteamUtils(), hSteamAPICall, pCallback, cubCallback, iCallbackExpected, out pbFailed);
		}

		public static uint GetIPCCallCount()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUtils_GetIPCCallCount(global::Steamworks.CSteamGameServerAPIContext.GetSteamUtils());
		}

		public static void SetWarningMessageHook(global::Steamworks.SteamAPIWarningMessageHook_t pFunction)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::Steamworks.NativeMethods.ISteamUtils_SetWarningMessageHook(global::Steamworks.CSteamGameServerAPIContext.GetSteamUtils(), pFunction);
		}

		public static bool IsOverlayEnabled()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUtils_IsOverlayEnabled(global::Steamworks.CSteamGameServerAPIContext.GetSteamUtils());
		}

		public static bool BOverlayNeedsPresent()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUtils_BOverlayNeedsPresent(global::Steamworks.CSteamGameServerAPIContext.GetSteamUtils());
		}

		public static global::Steamworks.SteamAPICall_t CheckFileSignature(string szFileName)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle szFileName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(szFileName);
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamUtils_CheckFileSignature(global::Steamworks.CSteamGameServerAPIContext.GetSteamUtils(), szFileName2);
		}

		public static bool ShowGamepadTextInput(global::Steamworks.EGamepadTextInputMode eInputMode, global::Steamworks.EGamepadTextInputLineMode eLineInputMode, string pchDescription, uint unCharMax, string pchExistingText)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchDescription2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchDescription);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchExistingText2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchExistingText);
			return global::Steamworks.NativeMethods.ISteamUtils_ShowGamepadTextInput(global::Steamworks.CSteamGameServerAPIContext.GetSteamUtils(), eInputMode, eLineInputMode, pchDescription2, unCharMax, pchExistingText2);
		}

		public static uint GetEnteredGamepadTextLength()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUtils_GetEnteredGamepadTextLength(global::Steamworks.CSteamGameServerAPIContext.GetSteamUtils());
		}

		public static bool GetEnteredGamepadTextInput(out string pchText, uint cchText)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal((int)cchText);
			bool flag = global::Steamworks.NativeMethods.ISteamUtils_GetEnteredGamepadTextInput(global::Steamworks.CSteamGameServerAPIContext.GetSteamUtils(), intPtr, cchText);
			pchText = (flag ? global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr) : null);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		public static string GetSteamUILanguage()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.InteropHelp.PtrToStringUTF8(global::Steamworks.NativeMethods.ISteamUtils_GetSteamUILanguage(global::Steamworks.CSteamGameServerAPIContext.GetSteamUtils()));
		}

		public static bool IsSteamRunningInVR()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUtils_IsSteamRunningInVR(global::Steamworks.CSteamGameServerAPIContext.GetSteamUtils());
		}

		public static void SetOverlayNotificationInset(int nHorizontalInset, int nVerticalInset)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::Steamworks.NativeMethods.ISteamUtils_SetOverlayNotificationInset(global::Steamworks.CSteamGameServerAPIContext.GetSteamUtils(), nHorizontalInset, nVerticalInset);
		}

		public static bool IsSteamInBigPictureMode()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUtils_IsSteamInBigPictureMode(global::Steamworks.CSteamGameServerAPIContext.GetSteamUtils());
		}

		public static void StartVRDashboard()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::Steamworks.NativeMethods.ISteamUtils_StartVRDashboard(global::Steamworks.CSteamGameServerAPIContext.GetSteamUtils());
		}

		public static bool IsVRHeadsetStreamingEnabled()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUtils_IsVRHeadsetStreamingEnabled(global::Steamworks.CSteamGameServerAPIContext.GetSteamUtils());
		}

		public static void SetVRHeadsetStreamingEnabled(bool bEnabled)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::Steamworks.NativeMethods.ISteamUtils_SetVRHeadsetStreamingEnabled(global::Steamworks.CSteamGameServerAPIContext.GetSteamUtils(), bEnabled);
		}

		public static bool IsSteamChinaLauncher()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUtils_IsSteamChinaLauncher(global::Steamworks.CSteamGameServerAPIContext.GetSteamUtils());
		}

		public static bool InitFilterText(uint unFilterOptions = 0u)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUtils_InitFilterText(global::Steamworks.CSteamGameServerAPIContext.GetSteamUtils(), unFilterOptions);
		}

		public static int FilterText(global::Steamworks.ETextFilteringContext eContext, global::Steamworks.CSteamID sourceSteamID, string pchInputMessage, out string pchOutFilteredText, uint nByteSizeOutFilteredText)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal((int)nByteSizeOutFilteredText);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchInputMessage2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchInputMessage);
			int num = global::Steamworks.NativeMethods.ISteamUtils_FilterText(global::Steamworks.CSteamGameServerAPIContext.GetSteamUtils(), eContext, sourceSteamID, pchInputMessage2, intPtr, nByteSizeOutFilteredText);
			pchOutFilteredText = ((num != -1) ? global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr) : null);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
			return num;
		}

		public static global::Steamworks.ESteamIPv6ConnectivityState GetIPv6ConnectivityState(global::Steamworks.ESteamIPv6ConnectivityProtocol eProtocol)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUtils_GetIPv6ConnectivityState(global::Steamworks.CSteamGameServerAPIContext.GetSteamUtils(), eProtocol);
		}

		public static bool IsSteamRunningOnSteamDeck()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUtils_IsSteamRunningOnSteamDeck(global::Steamworks.CSteamGameServerAPIContext.GetSteamUtils());
		}

		public static bool ShowFloatingGamepadTextInput(global::Steamworks.EFloatingGamepadTextInputMode eKeyboardMode, int nTextFieldXPosition, int nTextFieldYPosition, int nTextFieldWidth, int nTextFieldHeight)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUtils_ShowFloatingGamepadTextInput(global::Steamworks.CSteamGameServerAPIContext.GetSteamUtils(), eKeyboardMode, nTextFieldXPosition, nTextFieldYPosition, nTextFieldWidth, nTextFieldHeight);
		}

		public static void SetGameLauncherMode(bool bLauncherMode)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::Steamworks.NativeMethods.ISteamUtils_SetGameLauncherMode(global::Steamworks.CSteamGameServerAPIContext.GetSteamUtils(), bLauncherMode);
		}

		public static bool DismissFloatingGamepadTextInput()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUtils_DismissFloatingGamepadTextInput(global::Steamworks.CSteamGameServerAPIContext.GetSteamUtils());
		}

		public static bool DismissGamepadTextInput()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUtils_DismissGamepadTextInput(global::Steamworks.CSteamGameServerAPIContext.GetSteamUtils());
		}
	}
}
