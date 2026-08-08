namespace Steamworks
{
	public static class SteamHTTP
	{
		public static global::Steamworks.HTTPRequestHandle CreateHTTPRequest(global::Steamworks.EHTTPMethod eHTTPRequestMethod, string pchAbsoluteURL)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchAbsoluteURL2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchAbsoluteURL);
			return (global::Steamworks.HTTPRequestHandle)global::Steamworks.NativeMethods.ISteamHTTP_CreateHTTPRequest(global::Steamworks.CSteamAPIContext.GetSteamHTTP(), eHTTPRequestMethod, pchAbsoluteURL2);
		}

		public static bool SetHTTPRequestContextValue(global::Steamworks.HTTPRequestHandle hRequest, ulong ulContextValue)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamHTTP_SetHTTPRequestContextValue(global::Steamworks.CSteamAPIContext.GetSteamHTTP(), hRequest, ulContextValue);
		}

		public static bool SetHTTPRequestNetworkActivityTimeout(global::Steamworks.HTTPRequestHandle hRequest, uint unTimeoutSeconds)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamHTTP_SetHTTPRequestNetworkActivityTimeout(global::Steamworks.CSteamAPIContext.GetSteamHTTP(), hRequest, unTimeoutSeconds);
		}

		public static bool SetHTTPRequestHeaderValue(global::Steamworks.HTTPRequestHandle hRequest, string pchHeaderName, string pchHeaderValue)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchHeaderName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchHeaderName);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchHeaderValue2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchHeaderValue);
			return global::Steamworks.NativeMethods.ISteamHTTP_SetHTTPRequestHeaderValue(global::Steamworks.CSteamAPIContext.GetSteamHTTP(), hRequest, pchHeaderName2, pchHeaderValue2);
		}

		public static bool SetHTTPRequestGetOrPostParameter(global::Steamworks.HTTPRequestHandle hRequest, string pchParamName, string pchParamValue)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchParamName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchParamName);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchParamValue2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchParamValue);
			return global::Steamworks.NativeMethods.ISteamHTTP_SetHTTPRequestGetOrPostParameter(global::Steamworks.CSteamAPIContext.GetSteamHTTP(), hRequest, pchParamName2, pchParamValue2);
		}

		public static bool SendHTTPRequest(global::Steamworks.HTTPRequestHandle hRequest, out global::Steamworks.SteamAPICall_t pCallHandle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamHTTP_SendHTTPRequest(global::Steamworks.CSteamAPIContext.GetSteamHTTP(), hRequest, out pCallHandle);
		}

		public static bool SendHTTPRequestAndStreamResponse(global::Steamworks.HTTPRequestHandle hRequest, out global::Steamworks.SteamAPICall_t pCallHandle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamHTTP_SendHTTPRequestAndStreamResponse(global::Steamworks.CSteamAPIContext.GetSteamHTTP(), hRequest, out pCallHandle);
		}

		public static bool DeferHTTPRequest(global::Steamworks.HTTPRequestHandle hRequest)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamHTTP_DeferHTTPRequest(global::Steamworks.CSteamAPIContext.GetSteamHTTP(), hRequest);
		}

		public static bool PrioritizeHTTPRequest(global::Steamworks.HTTPRequestHandle hRequest)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamHTTP_PrioritizeHTTPRequest(global::Steamworks.CSteamAPIContext.GetSteamHTTP(), hRequest);
		}

		public static bool GetHTTPResponseHeaderSize(global::Steamworks.HTTPRequestHandle hRequest, string pchHeaderName, out uint unResponseHeaderSize)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchHeaderName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchHeaderName);
			return global::Steamworks.NativeMethods.ISteamHTTP_GetHTTPResponseHeaderSize(global::Steamworks.CSteamAPIContext.GetSteamHTTP(), hRequest, pchHeaderName2, out unResponseHeaderSize);
		}

		public static bool GetHTTPResponseHeaderValue(global::Steamworks.HTTPRequestHandle hRequest, string pchHeaderName, byte[] pHeaderValueBuffer, uint unBufferSize)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchHeaderName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchHeaderName);
			return global::Steamworks.NativeMethods.ISteamHTTP_GetHTTPResponseHeaderValue(global::Steamworks.CSteamAPIContext.GetSteamHTTP(), hRequest, pchHeaderName2, pHeaderValueBuffer, unBufferSize);
		}

		public static bool GetHTTPResponseBodySize(global::Steamworks.HTTPRequestHandle hRequest, out uint unBodySize)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamHTTP_GetHTTPResponseBodySize(global::Steamworks.CSteamAPIContext.GetSteamHTTP(), hRequest, out unBodySize);
		}

		public static bool GetHTTPResponseBodyData(global::Steamworks.HTTPRequestHandle hRequest, byte[] pBodyDataBuffer, uint unBufferSize)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamHTTP_GetHTTPResponseBodyData(global::Steamworks.CSteamAPIContext.GetSteamHTTP(), hRequest, pBodyDataBuffer, unBufferSize);
		}

		public static bool GetHTTPStreamingResponseBodyData(global::Steamworks.HTTPRequestHandle hRequest, uint cOffset, byte[] pBodyDataBuffer, uint unBufferSize)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamHTTP_GetHTTPStreamingResponseBodyData(global::Steamworks.CSteamAPIContext.GetSteamHTTP(), hRequest, cOffset, pBodyDataBuffer, unBufferSize);
		}

		public static bool ReleaseHTTPRequest(global::Steamworks.HTTPRequestHandle hRequest)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamHTTP_ReleaseHTTPRequest(global::Steamworks.CSteamAPIContext.GetSteamHTTP(), hRequest);
		}

		public static bool GetHTTPDownloadProgressPct(global::Steamworks.HTTPRequestHandle hRequest, out float pflPercentOut)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamHTTP_GetHTTPDownloadProgressPct(global::Steamworks.CSteamAPIContext.GetSteamHTTP(), hRequest, out pflPercentOut);
		}

		public static bool SetHTTPRequestRawPostBody(global::Steamworks.HTTPRequestHandle hRequest, string pchContentType, byte[] pubBody, uint unBodyLen)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchContentType2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchContentType);
			return global::Steamworks.NativeMethods.ISteamHTTP_SetHTTPRequestRawPostBody(global::Steamworks.CSteamAPIContext.GetSteamHTTP(), hRequest, pchContentType2, pubBody, unBodyLen);
		}

		public static global::Steamworks.HTTPCookieContainerHandle CreateCookieContainer(bool bAllowResponsesToModify)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.HTTPCookieContainerHandle)global::Steamworks.NativeMethods.ISteamHTTP_CreateCookieContainer(global::Steamworks.CSteamAPIContext.GetSteamHTTP(), bAllowResponsesToModify);
		}

		public static bool ReleaseCookieContainer(global::Steamworks.HTTPCookieContainerHandle hCookieContainer)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamHTTP_ReleaseCookieContainer(global::Steamworks.CSteamAPIContext.GetSteamHTTP(), hCookieContainer);
		}

		public static bool SetCookie(global::Steamworks.HTTPCookieContainerHandle hCookieContainer, string pchHost, string pchUrl, string pchCookie)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchHost2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchHost);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchUrl2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchUrl);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchCookie2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchCookie);
			return global::Steamworks.NativeMethods.ISteamHTTP_SetCookie(global::Steamworks.CSteamAPIContext.GetSteamHTTP(), hCookieContainer, pchHost2, pchUrl2, pchCookie2);
		}

		public static bool SetHTTPRequestCookieContainer(global::Steamworks.HTTPRequestHandle hRequest, global::Steamworks.HTTPCookieContainerHandle hCookieContainer)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamHTTP_SetHTTPRequestCookieContainer(global::Steamworks.CSteamAPIContext.GetSteamHTTP(), hRequest, hCookieContainer);
		}

		public static bool SetHTTPRequestUserAgentInfo(global::Steamworks.HTTPRequestHandle hRequest, string pchUserAgentInfo)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchUserAgentInfo2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchUserAgentInfo);
			return global::Steamworks.NativeMethods.ISteamHTTP_SetHTTPRequestUserAgentInfo(global::Steamworks.CSteamAPIContext.GetSteamHTTP(), hRequest, pchUserAgentInfo2);
		}

		public static bool SetHTTPRequestRequiresVerifiedCertificate(global::Steamworks.HTTPRequestHandle hRequest, bool bRequireVerifiedCertificate)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamHTTP_SetHTTPRequestRequiresVerifiedCertificate(global::Steamworks.CSteamAPIContext.GetSteamHTTP(), hRequest, bRequireVerifiedCertificate);
		}

		public static bool SetHTTPRequestAbsoluteTimeoutMS(global::Steamworks.HTTPRequestHandle hRequest, uint unMilliseconds)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamHTTP_SetHTTPRequestAbsoluteTimeoutMS(global::Steamworks.CSteamAPIContext.GetSteamHTTP(), hRequest, unMilliseconds);
		}

		public static bool GetHTTPRequestWasTimedOut(global::Steamworks.HTTPRequestHandle hRequest, out bool pbWasTimedOut)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamHTTP_GetHTTPRequestWasTimedOut(global::Steamworks.CSteamAPIContext.GetSteamHTTP(), hRequest, out pbWasTimedOut);
		}
	}
}
