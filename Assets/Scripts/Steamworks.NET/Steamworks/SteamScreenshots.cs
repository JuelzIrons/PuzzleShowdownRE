namespace Steamworks
{
	public static class SteamScreenshots
	{
		public static global::Steamworks.ScreenshotHandle WriteScreenshot(byte[] pubRGB, uint cubRGB, int nWidth, int nHeight)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.ScreenshotHandle)global::Steamworks.NativeMethods.ISteamScreenshots_WriteScreenshot(global::Steamworks.CSteamAPIContext.GetSteamScreenshots(), pubRGB, cubRGB, nWidth, nHeight);
		}

		public static global::Steamworks.ScreenshotHandle AddScreenshotToLibrary(string pchFilename, string pchThumbnailFilename, int nWidth, int nHeight)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchFilename2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchFilename);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchThumbnailFilename2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchThumbnailFilename);
			return (global::Steamworks.ScreenshotHandle)global::Steamworks.NativeMethods.ISteamScreenshots_AddScreenshotToLibrary(global::Steamworks.CSteamAPIContext.GetSteamScreenshots(), pchFilename2, pchThumbnailFilename2, nWidth, nHeight);
		}

		public static void TriggerScreenshot()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamScreenshots_TriggerScreenshot(global::Steamworks.CSteamAPIContext.GetSteamScreenshots());
		}

		public static void HookScreenshots(bool bHook)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamScreenshots_HookScreenshots(global::Steamworks.CSteamAPIContext.GetSteamScreenshots(), bHook);
		}

		public static bool SetLocation(global::Steamworks.ScreenshotHandle hScreenshot, string pchLocation)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchLocation2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchLocation);
			return global::Steamworks.NativeMethods.ISteamScreenshots_SetLocation(global::Steamworks.CSteamAPIContext.GetSteamScreenshots(), hScreenshot, pchLocation2);
		}

		public static bool TagUser(global::Steamworks.ScreenshotHandle hScreenshot, global::Steamworks.CSteamID steamID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamScreenshots_TagUser(global::Steamworks.CSteamAPIContext.GetSteamScreenshots(), hScreenshot, steamID);
		}

		public static bool TagPublishedFile(global::Steamworks.ScreenshotHandle hScreenshot, global::Steamworks.PublishedFileId_t unPublishedFileID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamScreenshots_TagPublishedFile(global::Steamworks.CSteamAPIContext.GetSteamScreenshots(), hScreenshot, unPublishedFileID);
		}

		public static bool IsScreenshotsHooked()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamScreenshots_IsScreenshotsHooked(global::Steamworks.CSteamAPIContext.GetSteamScreenshots());
		}

		public static global::Steamworks.ScreenshotHandle AddVRScreenshotToLibrary(global::Steamworks.EVRScreenshotType eType, string pchFilename, string pchVRFilename)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchFilename2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchFilename);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchVRFilename2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchVRFilename);
			return (global::Steamworks.ScreenshotHandle)global::Steamworks.NativeMethods.ISteamScreenshots_AddVRScreenshotToLibrary(global::Steamworks.CSteamAPIContext.GetSteamScreenshots(), eType, pchFilename2, pchVRFilename2);
		}
	}
}
