namespace Steamworks
{
	public static class SteamVideo
	{
		public static void GetVideoURL(global::Steamworks.AppId_t unVideoAppID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamVideo_GetVideoURL(global::Steamworks.CSteamAPIContext.GetSteamVideo(), unVideoAppID);
		}

		public static bool IsBroadcasting(out int pnNumViewers)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamVideo_IsBroadcasting(global::Steamworks.CSteamAPIContext.GetSteamVideo(), out pnNumViewers);
		}

		public static void GetOPFSettings(global::Steamworks.AppId_t unVideoAppID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamVideo_GetOPFSettings(global::Steamworks.CSteamAPIContext.GetSteamVideo(), unVideoAppID);
		}

		public static bool GetOPFStringForApp(global::Steamworks.AppId_t unVideoAppID, out string pchBuffer, ref int pnBufferSize)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(pnBufferSize);
			bool flag = global::Steamworks.NativeMethods.ISteamVideo_GetOPFStringForApp(global::Steamworks.CSteamAPIContext.GetSteamVideo(), unVideoAppID, intPtr, ref pnBufferSize);
			pchBuffer = (flag ? global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr) : null);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
			return flag;
		}
	}
}
