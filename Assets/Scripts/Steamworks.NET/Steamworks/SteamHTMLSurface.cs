namespace Steamworks
{
	public static class SteamHTMLSurface
	{
		public static bool Init()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamHTMLSurface_Init(global::Steamworks.CSteamAPIContext.GetSteamHTMLSurface());
		}

		public static bool Shutdown()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamHTMLSurface_Shutdown(global::Steamworks.CSteamAPIContext.GetSteamHTMLSurface());
		}

		public static global::Steamworks.SteamAPICall_t CreateBrowser(string pchUserAgent, string pchUserCSS)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchUserAgent2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchUserAgent);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchUserCSS2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchUserCSS);
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamHTMLSurface_CreateBrowser(global::Steamworks.CSteamAPIContext.GetSteamHTMLSurface(), pchUserAgent2, pchUserCSS2);
		}

		public static void RemoveBrowser(global::Steamworks.HHTMLBrowser unBrowserHandle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamHTMLSurface_RemoveBrowser(global::Steamworks.CSteamAPIContext.GetSteamHTMLSurface(), unBrowserHandle);
		}

		public static void LoadURL(global::Steamworks.HHTMLBrowser unBrowserHandle, string pchURL, string pchPostData)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchURL2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchURL);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchPostData2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchPostData);
			global::Steamworks.NativeMethods.ISteamHTMLSurface_LoadURL(global::Steamworks.CSteamAPIContext.GetSteamHTMLSurface(), unBrowserHandle, pchURL2, pchPostData2);
		}

		public static void SetSize(global::Steamworks.HHTMLBrowser unBrowserHandle, uint unWidth, uint unHeight)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamHTMLSurface_SetSize(global::Steamworks.CSteamAPIContext.GetSteamHTMLSurface(), unBrowserHandle, unWidth, unHeight);
		}

		public static void StopLoad(global::Steamworks.HHTMLBrowser unBrowserHandle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamHTMLSurface_StopLoad(global::Steamworks.CSteamAPIContext.GetSteamHTMLSurface(), unBrowserHandle);
		}

		public static void Reload(global::Steamworks.HHTMLBrowser unBrowserHandle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamHTMLSurface_Reload(global::Steamworks.CSteamAPIContext.GetSteamHTMLSurface(), unBrowserHandle);
		}

		public static void GoBack(global::Steamworks.HHTMLBrowser unBrowserHandle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamHTMLSurface_GoBack(global::Steamworks.CSteamAPIContext.GetSteamHTMLSurface(), unBrowserHandle);
		}

		public static void GoForward(global::Steamworks.HHTMLBrowser unBrowserHandle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamHTMLSurface_GoForward(global::Steamworks.CSteamAPIContext.GetSteamHTMLSurface(), unBrowserHandle);
		}

		public static void AddHeader(global::Steamworks.HHTMLBrowser unBrowserHandle, string pchKey, string pchValue)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchKey2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchKey);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchValue2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchValue);
			global::Steamworks.NativeMethods.ISteamHTMLSurface_AddHeader(global::Steamworks.CSteamAPIContext.GetSteamHTMLSurface(), unBrowserHandle, pchKey2, pchValue2);
		}

		public static void ExecuteJavascript(global::Steamworks.HHTMLBrowser unBrowserHandle, string pchScript)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchScript2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchScript);
			global::Steamworks.NativeMethods.ISteamHTMLSurface_ExecuteJavascript(global::Steamworks.CSteamAPIContext.GetSteamHTMLSurface(), unBrowserHandle, pchScript2);
		}

		public static void MouseUp(global::Steamworks.HHTMLBrowser unBrowserHandle, global::Steamworks.EHTMLMouseButton eMouseButton)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamHTMLSurface_MouseUp(global::Steamworks.CSteamAPIContext.GetSteamHTMLSurface(), unBrowserHandle, eMouseButton);
		}

		public static void MouseDown(global::Steamworks.HHTMLBrowser unBrowserHandle, global::Steamworks.EHTMLMouseButton eMouseButton)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamHTMLSurface_MouseDown(global::Steamworks.CSteamAPIContext.GetSteamHTMLSurface(), unBrowserHandle, eMouseButton);
		}

		public static void MouseDoubleClick(global::Steamworks.HHTMLBrowser unBrowserHandle, global::Steamworks.EHTMLMouseButton eMouseButton)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamHTMLSurface_MouseDoubleClick(global::Steamworks.CSteamAPIContext.GetSteamHTMLSurface(), unBrowserHandle, eMouseButton);
		}

		public static void MouseMove(global::Steamworks.HHTMLBrowser unBrowserHandle, int x, int y)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamHTMLSurface_MouseMove(global::Steamworks.CSteamAPIContext.GetSteamHTMLSurface(), unBrowserHandle, x, y);
		}

		public static void MouseWheel(global::Steamworks.HHTMLBrowser unBrowserHandle, int nDelta)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamHTMLSurface_MouseWheel(global::Steamworks.CSteamAPIContext.GetSteamHTMLSurface(), unBrowserHandle, nDelta);
		}

		public static void KeyDown(global::Steamworks.HHTMLBrowser unBrowserHandle, uint nNativeKeyCode, global::Steamworks.EHTMLKeyModifiers eHTMLKeyModifiers, bool bIsSystemKey = false)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamHTMLSurface_KeyDown(global::Steamworks.CSteamAPIContext.GetSteamHTMLSurface(), unBrowserHandle, nNativeKeyCode, eHTMLKeyModifiers, bIsSystemKey);
		}

		public static void KeyUp(global::Steamworks.HHTMLBrowser unBrowserHandle, uint nNativeKeyCode, global::Steamworks.EHTMLKeyModifiers eHTMLKeyModifiers)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamHTMLSurface_KeyUp(global::Steamworks.CSteamAPIContext.GetSteamHTMLSurface(), unBrowserHandle, nNativeKeyCode, eHTMLKeyModifiers);
		}

		public static void KeyChar(global::Steamworks.HHTMLBrowser unBrowserHandle, uint cUnicodeChar, global::Steamworks.EHTMLKeyModifiers eHTMLKeyModifiers)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamHTMLSurface_KeyChar(global::Steamworks.CSteamAPIContext.GetSteamHTMLSurface(), unBrowserHandle, cUnicodeChar, eHTMLKeyModifiers);
		}

		public static void SetHorizontalScroll(global::Steamworks.HHTMLBrowser unBrowserHandle, uint nAbsolutePixelScroll)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamHTMLSurface_SetHorizontalScroll(global::Steamworks.CSteamAPIContext.GetSteamHTMLSurface(), unBrowserHandle, nAbsolutePixelScroll);
		}

		public static void SetVerticalScroll(global::Steamworks.HHTMLBrowser unBrowserHandle, uint nAbsolutePixelScroll)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamHTMLSurface_SetVerticalScroll(global::Steamworks.CSteamAPIContext.GetSteamHTMLSurface(), unBrowserHandle, nAbsolutePixelScroll);
		}

		public static void SetKeyFocus(global::Steamworks.HHTMLBrowser unBrowserHandle, bool bHasKeyFocus)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamHTMLSurface_SetKeyFocus(global::Steamworks.CSteamAPIContext.GetSteamHTMLSurface(), unBrowserHandle, bHasKeyFocus);
		}

		public static void ViewSource(global::Steamworks.HHTMLBrowser unBrowserHandle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamHTMLSurface_ViewSource(global::Steamworks.CSteamAPIContext.GetSteamHTMLSurface(), unBrowserHandle);
		}

		public static void CopyToClipboard(global::Steamworks.HHTMLBrowser unBrowserHandle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamHTMLSurface_CopyToClipboard(global::Steamworks.CSteamAPIContext.GetSteamHTMLSurface(), unBrowserHandle);
		}

		public static void PasteFromClipboard(global::Steamworks.HHTMLBrowser unBrowserHandle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamHTMLSurface_PasteFromClipboard(global::Steamworks.CSteamAPIContext.GetSteamHTMLSurface(), unBrowserHandle);
		}

		public static void Find(global::Steamworks.HHTMLBrowser unBrowserHandle, string pchSearchStr, bool bCurrentlyInFind, bool bReverse)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchSearchStr2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchSearchStr);
			global::Steamworks.NativeMethods.ISteamHTMLSurface_Find(global::Steamworks.CSteamAPIContext.GetSteamHTMLSurface(), unBrowserHandle, pchSearchStr2, bCurrentlyInFind, bReverse);
		}

		public static void StopFind(global::Steamworks.HHTMLBrowser unBrowserHandle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamHTMLSurface_StopFind(global::Steamworks.CSteamAPIContext.GetSteamHTMLSurface(), unBrowserHandle);
		}

		public static void GetLinkAtPosition(global::Steamworks.HHTMLBrowser unBrowserHandle, int x, int y)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamHTMLSurface_GetLinkAtPosition(global::Steamworks.CSteamAPIContext.GetSteamHTMLSurface(), unBrowserHandle, x, y);
		}

		public static void SetCookie(string pchHostname, string pchKey, string pchValue, string pchPath = "/", uint nExpires = 0u, bool bSecure = false, bool bHTTPOnly = false)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchHostname2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchHostname);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchKey2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchKey);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchValue2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchValue);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchPath2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchPath);
			global::Steamworks.NativeMethods.ISteamHTMLSurface_SetCookie(global::Steamworks.CSteamAPIContext.GetSteamHTMLSurface(), pchHostname2, pchKey2, pchValue2, pchPath2, nExpires, bSecure, bHTTPOnly);
		}

		public static void SetPageScaleFactor(global::Steamworks.HHTMLBrowser unBrowserHandle, float flZoom, int nPointX, int nPointY)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamHTMLSurface_SetPageScaleFactor(global::Steamworks.CSteamAPIContext.GetSteamHTMLSurface(), unBrowserHandle, flZoom, nPointX, nPointY);
		}

		public static void SetBackgroundMode(global::Steamworks.HHTMLBrowser unBrowserHandle, bool bBackgroundMode)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamHTMLSurface_SetBackgroundMode(global::Steamworks.CSteamAPIContext.GetSteamHTMLSurface(), unBrowserHandle, bBackgroundMode);
		}

		public static void SetDPIScalingFactor(global::Steamworks.HHTMLBrowser unBrowserHandle, float flDPIScaling)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamHTMLSurface_SetDPIScalingFactor(global::Steamworks.CSteamAPIContext.GetSteamHTMLSurface(), unBrowserHandle, flDPIScaling);
		}

		public static void OpenDeveloperTools(global::Steamworks.HHTMLBrowser unBrowserHandle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamHTMLSurface_OpenDeveloperTools(global::Steamworks.CSteamAPIContext.GetSteamHTMLSurface(), unBrowserHandle);
		}

		public static void AllowStartRequest(global::Steamworks.HHTMLBrowser unBrowserHandle, bool bAllowed)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamHTMLSurface_AllowStartRequest(global::Steamworks.CSteamAPIContext.GetSteamHTMLSurface(), unBrowserHandle, bAllowed);
		}

		public static void JSDialogResponse(global::Steamworks.HHTMLBrowser unBrowserHandle, bool bResult)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamHTMLSurface_JSDialogResponse(global::Steamworks.CSteamAPIContext.GetSteamHTMLSurface(), unBrowserHandle, bResult);
		}

		public static void FileLoadDialogResponse(global::Steamworks.HHTMLBrowser unBrowserHandle, global::System.IntPtr pchSelectedFiles)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamHTMLSurface_FileLoadDialogResponse(global::Steamworks.CSteamAPIContext.GetSteamHTMLSurface(), unBrowserHandle, pchSelectedFiles);
		}
	}
}
