namespace Steamworks
{
	public static class SteamRemotePlay
	{
		public static uint GetSessionCount()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamRemotePlay_GetSessionCount(global::Steamworks.CSteamAPIContext.GetSteamRemotePlay());
		}

		public static global::Steamworks.RemotePlaySessionID_t GetSessionID(int iSessionIndex)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.RemotePlaySessionID_t)global::Steamworks.NativeMethods.ISteamRemotePlay_GetSessionID(global::Steamworks.CSteamAPIContext.GetSteamRemotePlay(), iSessionIndex);
		}

		public static global::Steamworks.CSteamID GetSessionSteamID(global::Steamworks.RemotePlaySessionID_t unSessionID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.CSteamID)global::Steamworks.NativeMethods.ISteamRemotePlay_GetSessionSteamID(global::Steamworks.CSteamAPIContext.GetSteamRemotePlay(), unSessionID);
		}

		public static string GetSessionClientName(global::Steamworks.RemotePlaySessionID_t unSessionID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.InteropHelp.PtrToStringUTF8(global::Steamworks.NativeMethods.ISteamRemotePlay_GetSessionClientName(global::Steamworks.CSteamAPIContext.GetSteamRemotePlay(), unSessionID));
		}

		public static global::Steamworks.ESteamDeviceFormFactor GetSessionClientFormFactor(global::Steamworks.RemotePlaySessionID_t unSessionID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamRemotePlay_GetSessionClientFormFactor(global::Steamworks.CSteamAPIContext.GetSteamRemotePlay(), unSessionID);
		}

		public static bool BGetSessionClientResolution(global::Steamworks.RemotePlaySessionID_t unSessionID, out int pnResolutionX, out int pnResolutionY)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamRemotePlay_BGetSessionClientResolution(global::Steamworks.CSteamAPIContext.GetSteamRemotePlay(), unSessionID, out pnResolutionX, out pnResolutionY);
		}

		public static bool ShowRemotePlayTogetherUI()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamRemotePlay_ShowRemotePlayTogetherUI(global::Steamworks.CSteamAPIContext.GetSteamRemotePlay());
		}

		public static bool BSendRemotePlayTogetherInvite(global::Steamworks.CSteamID steamIDFriend)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamRemotePlay_BSendRemotePlayTogetherInvite(global::Steamworks.CSteamAPIContext.GetSteamRemotePlay(), steamIDFriend);
		}

		public static bool BEnableRemotePlayTogetherDirectInput()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamRemotePlay_BEnableRemotePlayTogetherDirectInput(global::Steamworks.CSteamAPIContext.GetSteamRemotePlay());
		}

		public static void DisableRemotePlayTogetherDirectInput()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamRemotePlay_DisableRemotePlayTogetherDirectInput(global::Steamworks.CSteamAPIContext.GetSteamRemotePlay());
		}

		public static uint GetInput(global::Steamworks.RemotePlayInput_t[] pInput, uint unMaxEvents)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamRemotePlay_GetInput(global::Steamworks.CSteamAPIContext.GetSteamRemotePlay(), pInput, unMaxEvents);
		}

		public static void SetMouseVisibility(global::Steamworks.RemotePlaySessionID_t unSessionID, bool bVisible)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamRemotePlay_SetMouseVisibility(global::Steamworks.CSteamAPIContext.GetSteamRemotePlay(), unSessionID, bVisible);
		}

		public static void SetMousePosition(global::Steamworks.RemotePlaySessionID_t unSessionID, float flNormalizedX, float flNormalizedY)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamRemotePlay_SetMousePosition(global::Steamworks.CSteamAPIContext.GetSteamRemotePlay(), unSessionID, flNormalizedX, flNormalizedY);
		}

		public static global::Steamworks.RemotePlayCursorID_t CreateMouseCursor(int nWidth, int nHeight, int nHotX, int nHotY, global::System.IntPtr pBGRA, int nPitch = 0)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.RemotePlayCursorID_t)global::Steamworks.NativeMethods.ISteamRemotePlay_CreateMouseCursor(global::Steamworks.CSteamAPIContext.GetSteamRemotePlay(), nWidth, nHeight, nHotX, nHotY, pBGRA, nPitch);
		}

		public static void SetMouseCursor(global::Steamworks.RemotePlaySessionID_t unSessionID, global::Steamworks.RemotePlayCursorID_t unCursorID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamRemotePlay_SetMouseCursor(global::Steamworks.CSteamAPIContext.GetSteamRemotePlay(), unSessionID, unCursorID);
		}
	}
}
