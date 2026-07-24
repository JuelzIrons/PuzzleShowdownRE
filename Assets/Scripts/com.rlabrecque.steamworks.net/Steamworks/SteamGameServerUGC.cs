namespace Steamworks
{
	public static class SteamGameServerUGC
	{
		public static global::Steamworks.UGCQueryHandle_t CreateQueryUserUGCRequest(global::Steamworks.AccountID_t unAccountID, global::Steamworks.EUserUGCList eListType, global::Steamworks.EUGCMatchingUGCType eMatchingUGCType, global::Steamworks.EUserUGCListSortOrder eSortOrder, global::Steamworks.AppId_t nCreatorAppID, global::Steamworks.AppId_t nConsumerAppID, uint unPage)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.UGCQueryHandle_t)global::Steamworks.NativeMethods.ISteamUGC_CreateQueryUserUGCRequest(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), unAccountID, eListType, eMatchingUGCType, eSortOrder, nCreatorAppID, nConsumerAppID, unPage);
		}

		public static global::Steamworks.UGCQueryHandle_t CreateQueryAllUGCRequest(global::Steamworks.EUGCQuery eQueryType, global::Steamworks.EUGCMatchingUGCType eMatchingeMatchingUGCTypeFileType, global::Steamworks.AppId_t nCreatorAppID, global::Steamworks.AppId_t nConsumerAppID, uint unPage)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.UGCQueryHandle_t)global::Steamworks.NativeMethods.ISteamUGC_CreateQueryAllUGCRequestPage(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), eQueryType, eMatchingeMatchingUGCTypeFileType, nCreatorAppID, nConsumerAppID, unPage);
		}

		public static global::Steamworks.UGCQueryHandle_t CreateQueryAllUGCRequest(global::Steamworks.EUGCQuery eQueryType, global::Steamworks.EUGCMatchingUGCType eMatchingeMatchingUGCTypeFileType, global::Steamworks.AppId_t nCreatorAppID, global::Steamworks.AppId_t nConsumerAppID, string pchCursor = null)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchCursor2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchCursor);
			return (global::Steamworks.UGCQueryHandle_t)global::Steamworks.NativeMethods.ISteamUGC_CreateQueryAllUGCRequestCursor(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), eQueryType, eMatchingeMatchingUGCTypeFileType, nCreatorAppID, nConsumerAppID, pchCursor2);
		}

		public static global::Steamworks.UGCQueryHandle_t CreateQueryUGCDetailsRequest(global::Steamworks.PublishedFileId_t[] pvecPublishedFileID, uint unNumPublishedFileIDs)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.UGCQueryHandle_t)global::Steamworks.NativeMethods.ISteamUGC_CreateQueryUGCDetailsRequest(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), pvecPublishedFileID, unNumPublishedFileIDs);
		}

		public static global::Steamworks.SteamAPICall_t SendQueryUGCRequest(global::Steamworks.UGCQueryHandle_t handle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamUGC_SendQueryUGCRequest(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle);
		}

		public static bool GetQueryUGCResult(global::Steamworks.UGCQueryHandle_t handle, uint index, out global::Steamworks.SteamUGCDetails_t pDetails)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_GetQueryUGCResult(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, index, out pDetails);
		}

		public static uint GetQueryUGCNumTags(global::Steamworks.UGCQueryHandle_t handle, uint index)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_GetQueryUGCNumTags(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, index);
		}

		public static bool GetQueryUGCTag(global::Steamworks.UGCQueryHandle_t handle, uint index, uint indexTag, out string pchValue, uint cchValueSize)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal((int)cchValueSize);
			bool flag = global::Steamworks.NativeMethods.ISteamUGC_GetQueryUGCTag(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, index, indexTag, intPtr, cchValueSize);
			pchValue = (flag ? global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr) : null);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		public static bool GetQueryUGCTagDisplayName(global::Steamworks.UGCQueryHandle_t handle, uint index, uint indexTag, out string pchValue, uint cchValueSize)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal((int)cchValueSize);
			bool flag = global::Steamworks.NativeMethods.ISteamUGC_GetQueryUGCTagDisplayName(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, index, indexTag, intPtr, cchValueSize);
			pchValue = (flag ? global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr) : null);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		public static bool GetQueryUGCPreviewURL(global::Steamworks.UGCQueryHandle_t handle, uint index, out string pchURL, uint cchURLSize)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal((int)cchURLSize);
			bool flag = global::Steamworks.NativeMethods.ISteamUGC_GetQueryUGCPreviewURL(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, index, intPtr, cchURLSize);
			pchURL = (flag ? global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr) : null);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		public static bool GetQueryUGCMetadata(global::Steamworks.UGCQueryHandle_t handle, uint index, out string pchMetadata, uint cchMetadatasize)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal((int)cchMetadatasize);
			bool flag = global::Steamworks.NativeMethods.ISteamUGC_GetQueryUGCMetadata(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, index, intPtr, cchMetadatasize);
			pchMetadata = (flag ? global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr) : null);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		public static bool GetQueryUGCChildren(global::Steamworks.UGCQueryHandle_t handle, uint index, global::Steamworks.PublishedFileId_t[] pvecPublishedFileID, uint cMaxEntries)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_GetQueryUGCChildren(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, index, pvecPublishedFileID, cMaxEntries);
		}

		public static bool GetQueryUGCStatistic(global::Steamworks.UGCQueryHandle_t handle, uint index, global::Steamworks.EItemStatistic eStatType, out ulong pStatValue)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_GetQueryUGCStatistic(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, index, eStatType, out pStatValue);
		}

		public static uint GetQueryUGCNumAdditionalPreviews(global::Steamworks.UGCQueryHandle_t handle, uint index)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_GetQueryUGCNumAdditionalPreviews(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, index);
		}

		public static bool GetQueryUGCAdditionalPreview(global::Steamworks.UGCQueryHandle_t handle, uint index, uint previewIndex, out string pchURLOrVideoID, uint cchURLSize, out string pchOriginalFileName, uint cchOriginalFileNameSize, out global::Steamworks.EItemPreviewType pPreviewType)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal((int)cchURLSize);
			global::System.IntPtr intPtr2 = global::System.Runtime.InteropServices.Marshal.AllocHGlobal((int)cchOriginalFileNameSize);
			bool flag = global::Steamworks.NativeMethods.ISteamUGC_GetQueryUGCAdditionalPreview(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, index, previewIndex, intPtr, cchURLSize, intPtr2, cchOriginalFileNameSize, out pPreviewType);
			pchURLOrVideoID = (flag ? global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr) : null);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
			pchOriginalFileName = (flag ? global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr2) : null);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr2);
			return flag;
		}

		public static uint GetQueryUGCNumKeyValueTags(global::Steamworks.UGCQueryHandle_t handle, uint index)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_GetQueryUGCNumKeyValueTags(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, index);
		}

		public static bool GetQueryUGCKeyValueTag(global::Steamworks.UGCQueryHandle_t handle, uint index, uint keyValueTagIndex, out string pchKey, uint cchKeySize, out string pchValue, uint cchValueSize)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal((int)cchKeySize);
			global::System.IntPtr intPtr2 = global::System.Runtime.InteropServices.Marshal.AllocHGlobal((int)cchValueSize);
			bool flag = global::Steamworks.NativeMethods.ISteamUGC_GetQueryUGCKeyValueTag(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, index, keyValueTagIndex, intPtr, cchKeySize, intPtr2, cchValueSize);
			pchKey = (flag ? global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr) : null);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
			pchValue = (flag ? global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr2) : null);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr2);
			return flag;
		}

		public static bool GetQueryUGCKeyValueTag(global::Steamworks.UGCQueryHandle_t handle, uint index, string pchKey, out string pchValue, uint cchValueSize)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal((int)cchValueSize);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchKey2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchKey);
			bool flag = global::Steamworks.NativeMethods.ISteamUGC_GetQueryFirstUGCKeyValueTag(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, index, pchKey2, intPtr, cchValueSize);
			pchValue = (flag ? global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr) : null);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		public static uint GetNumSupportedGameVersions(global::Steamworks.UGCQueryHandle_t handle, uint index)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_GetNumSupportedGameVersions(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, index);
		}

		public static bool GetSupportedGameVersionData(global::Steamworks.UGCQueryHandle_t handle, uint index, uint versionIndex, out string pchGameBranchMin, out string pchGameBranchMax, uint cchGameBranchSize)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal((int)cchGameBranchSize);
			global::System.IntPtr intPtr2 = global::System.Runtime.InteropServices.Marshal.AllocHGlobal((int)cchGameBranchSize);
			bool flag = global::Steamworks.NativeMethods.ISteamUGC_GetSupportedGameVersionData(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, index, versionIndex, intPtr, intPtr2, cchGameBranchSize);
			pchGameBranchMin = (flag ? global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr) : null);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
			pchGameBranchMax = (flag ? global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr2) : null);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr2);
			return flag;
		}

		public static uint GetQueryUGCContentDescriptors(global::Steamworks.UGCQueryHandle_t handle, uint index, global::Steamworks.EUGCContentDescriptorID[] pvecDescriptors, uint cMaxEntries)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			if (pvecDescriptors != null && pvecDescriptors.Length != cMaxEntries)
			{
				throw new global::System.ArgumentException("pvecDescriptors must be the same size as cMaxEntries!");
			}
			return global::Steamworks.NativeMethods.ISteamUGC_GetQueryUGCContentDescriptors(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, index, pvecDescriptors, cMaxEntries);
		}

		public static bool ReleaseQueryUGCRequest(global::Steamworks.UGCQueryHandle_t handle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_ReleaseQueryUGCRequest(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle);
		}

		public static bool AddRequiredTag(global::Steamworks.UGCQueryHandle_t handle, string pTagName)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pTagName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pTagName);
			return global::Steamworks.NativeMethods.ISteamUGC_AddRequiredTag(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, pTagName2);
		}

		public static bool AddRequiredTagGroup(global::Steamworks.UGCQueryHandle_t handle, global::System.Collections.Generic.IList<string> pTagGroups)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_AddRequiredTagGroup(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, new global::Steamworks.InteropHelp.SteamParamStringArray(pTagGroups));
		}

		public static bool AddExcludedTag(global::Steamworks.UGCQueryHandle_t handle, string pTagName)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pTagName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pTagName);
			return global::Steamworks.NativeMethods.ISteamUGC_AddExcludedTag(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, pTagName2);
		}

		public static bool SetReturnOnlyIDs(global::Steamworks.UGCQueryHandle_t handle, bool bReturnOnlyIDs)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_SetReturnOnlyIDs(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, bReturnOnlyIDs);
		}

		public static bool SetReturnKeyValueTags(global::Steamworks.UGCQueryHandle_t handle, bool bReturnKeyValueTags)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_SetReturnKeyValueTags(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, bReturnKeyValueTags);
		}

		public static bool SetReturnLongDescription(global::Steamworks.UGCQueryHandle_t handle, bool bReturnLongDescription)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_SetReturnLongDescription(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, bReturnLongDescription);
		}

		public static bool SetReturnMetadata(global::Steamworks.UGCQueryHandle_t handle, bool bReturnMetadata)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_SetReturnMetadata(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, bReturnMetadata);
		}

		public static bool SetReturnChildren(global::Steamworks.UGCQueryHandle_t handle, bool bReturnChildren)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_SetReturnChildren(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, bReturnChildren);
		}

		public static bool SetReturnAdditionalPreviews(global::Steamworks.UGCQueryHandle_t handle, bool bReturnAdditionalPreviews)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_SetReturnAdditionalPreviews(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, bReturnAdditionalPreviews);
		}

		public static bool SetReturnTotalOnly(global::Steamworks.UGCQueryHandle_t handle, bool bReturnTotalOnly)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_SetReturnTotalOnly(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, bReturnTotalOnly);
		}

		public static bool SetReturnPlaytimeStats(global::Steamworks.UGCQueryHandle_t handle, uint unDays)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_SetReturnPlaytimeStats(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, unDays);
		}

		public static bool SetLanguage(global::Steamworks.UGCQueryHandle_t handle, string pchLanguage)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchLanguage2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchLanguage);
			return global::Steamworks.NativeMethods.ISteamUGC_SetLanguage(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, pchLanguage2);
		}

		public static bool SetAllowCachedResponse(global::Steamworks.UGCQueryHandle_t handle, uint unMaxAgeSeconds)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_SetAllowCachedResponse(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, unMaxAgeSeconds);
		}

		public static bool SetAdminQuery(global::Steamworks.UGCUpdateHandle_t handle, bool bAdminQuery)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_SetAdminQuery(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, bAdminQuery);
		}

		public static bool SetCloudFileNameFilter(global::Steamworks.UGCQueryHandle_t handle, string pMatchCloudFileName)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pMatchCloudFileName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pMatchCloudFileName);
			return global::Steamworks.NativeMethods.ISteamUGC_SetCloudFileNameFilter(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, pMatchCloudFileName2);
		}

		public static bool SetMatchAnyTag(global::Steamworks.UGCQueryHandle_t handle, bool bMatchAnyTag)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_SetMatchAnyTag(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, bMatchAnyTag);
		}

		public static bool SetSearchText(global::Steamworks.UGCQueryHandle_t handle, string pSearchText)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pSearchText2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pSearchText);
			return global::Steamworks.NativeMethods.ISteamUGC_SetSearchText(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, pSearchText2);
		}

		public static bool SetRankedByTrendDays(global::Steamworks.UGCQueryHandle_t handle, uint unDays)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_SetRankedByTrendDays(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, unDays);
		}

		public static bool SetTimeCreatedDateRange(global::Steamworks.UGCQueryHandle_t handle, uint rtStart, uint rtEnd)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_SetTimeCreatedDateRange(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, rtStart, rtEnd);
		}

		public static bool SetTimeUpdatedDateRange(global::Steamworks.UGCQueryHandle_t handle, uint rtStart, uint rtEnd)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_SetTimeUpdatedDateRange(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, rtStart, rtEnd);
		}

		public static bool AddRequiredKeyValueTag(global::Steamworks.UGCQueryHandle_t handle, string pKey, string pValue)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pKey2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pKey);
			using global::Steamworks.InteropHelp.UTF8StringHandle pValue2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pValue);
			return global::Steamworks.NativeMethods.ISteamUGC_AddRequiredKeyValueTag(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, pKey2, pValue2);
		}

		public static global::Steamworks.SteamAPICall_t RequestUGCDetails(global::Steamworks.PublishedFileId_t nPublishedFileID, uint unMaxAgeSeconds)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamUGC_RequestUGCDetails(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), nPublishedFileID, unMaxAgeSeconds);
		}

		public static global::Steamworks.SteamAPICall_t CreateItem(global::Steamworks.AppId_t nConsumerAppId, global::Steamworks.EWorkshopFileType eFileType)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamUGC_CreateItem(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), nConsumerAppId, eFileType);
		}

		public static global::Steamworks.UGCUpdateHandle_t StartItemUpdate(global::Steamworks.AppId_t nConsumerAppId, global::Steamworks.PublishedFileId_t nPublishedFileID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.UGCUpdateHandle_t)global::Steamworks.NativeMethods.ISteamUGC_StartItemUpdate(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), nConsumerAppId, nPublishedFileID);
		}

		public static bool SetItemTitle(global::Steamworks.UGCUpdateHandle_t handle, string pchTitle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchTitle2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchTitle);
			return global::Steamworks.NativeMethods.ISteamUGC_SetItemTitle(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, pchTitle2);
		}

		public static bool SetItemDescription(global::Steamworks.UGCUpdateHandle_t handle, string pchDescription)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchDescription2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchDescription);
			return global::Steamworks.NativeMethods.ISteamUGC_SetItemDescription(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, pchDescription2);
		}

		public static bool SetItemUpdateLanguage(global::Steamworks.UGCUpdateHandle_t handle, string pchLanguage)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchLanguage2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchLanguage);
			return global::Steamworks.NativeMethods.ISteamUGC_SetItemUpdateLanguage(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, pchLanguage2);
		}

		public static bool SetItemMetadata(global::Steamworks.UGCUpdateHandle_t handle, string pchMetaData)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchMetaData2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchMetaData);
			return global::Steamworks.NativeMethods.ISteamUGC_SetItemMetadata(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, pchMetaData2);
		}

		public static bool SetItemVisibility(global::Steamworks.UGCUpdateHandle_t handle, global::Steamworks.ERemoteStoragePublishedFileVisibility eVisibility)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_SetItemVisibility(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, eVisibility);
		}

		public static bool SetItemTags(global::Steamworks.UGCUpdateHandle_t updateHandle, global::System.Collections.Generic.IList<string> pTags, bool bAllowAdminTags = false)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_SetItemTags(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), updateHandle, new global::Steamworks.InteropHelp.SteamParamStringArray(pTags), bAllowAdminTags);
		}

		public static bool SetItemContent(global::Steamworks.UGCUpdateHandle_t handle, string pszContentFolder)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pszContentFolder2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pszContentFolder);
			return global::Steamworks.NativeMethods.ISteamUGC_SetItemContent(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, pszContentFolder2);
		}

		public static bool SetItemPreview(global::Steamworks.UGCUpdateHandle_t handle, string pszPreviewFile)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pszPreviewFile2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pszPreviewFile);
			return global::Steamworks.NativeMethods.ISteamUGC_SetItemPreview(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, pszPreviewFile2);
		}

		public static bool SetAllowLegacyUpload(global::Steamworks.UGCUpdateHandle_t handle, bool bAllowLegacyUpload)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_SetAllowLegacyUpload(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, bAllowLegacyUpload);
		}

		public static bool RemoveAllItemKeyValueTags(global::Steamworks.UGCUpdateHandle_t handle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_RemoveAllItemKeyValueTags(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle);
		}

		public static bool RemoveItemKeyValueTags(global::Steamworks.UGCUpdateHandle_t handle, string pchKey)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchKey2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchKey);
			return global::Steamworks.NativeMethods.ISteamUGC_RemoveItemKeyValueTags(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, pchKey2);
		}

		public static bool AddItemKeyValueTag(global::Steamworks.UGCUpdateHandle_t handle, string pchKey, string pchValue)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchKey2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchKey);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchValue2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchValue);
			return global::Steamworks.NativeMethods.ISteamUGC_AddItemKeyValueTag(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, pchKey2, pchValue2);
		}

		public static bool AddItemPreviewFile(global::Steamworks.UGCUpdateHandle_t handle, string pszPreviewFile, global::Steamworks.EItemPreviewType type)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pszPreviewFile2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pszPreviewFile);
			return global::Steamworks.NativeMethods.ISteamUGC_AddItemPreviewFile(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, pszPreviewFile2, type);
		}

		public static bool AddItemPreviewVideo(global::Steamworks.UGCUpdateHandle_t handle, string pszVideoID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pszVideoID2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pszVideoID);
			return global::Steamworks.NativeMethods.ISteamUGC_AddItemPreviewVideo(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, pszVideoID2);
		}

		public static bool UpdateItemPreviewFile(global::Steamworks.UGCUpdateHandle_t handle, uint index, string pszPreviewFile)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pszPreviewFile2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pszPreviewFile);
			return global::Steamworks.NativeMethods.ISteamUGC_UpdateItemPreviewFile(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, index, pszPreviewFile2);
		}

		public static bool UpdateItemPreviewVideo(global::Steamworks.UGCUpdateHandle_t handle, uint index, string pszVideoID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pszVideoID2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pszVideoID);
			return global::Steamworks.NativeMethods.ISteamUGC_UpdateItemPreviewVideo(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, index, pszVideoID2);
		}

		public static bool RemoveItemPreview(global::Steamworks.UGCUpdateHandle_t handle, uint index)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_RemoveItemPreview(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, index);
		}

		public static bool AddContentDescriptor(global::Steamworks.UGCUpdateHandle_t handle, global::Steamworks.EUGCContentDescriptorID descid)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_AddContentDescriptor(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, descid);
		}

		public static bool RemoveContentDescriptor(global::Steamworks.UGCUpdateHandle_t handle, global::Steamworks.EUGCContentDescriptorID descid)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_RemoveContentDescriptor(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, descid);
		}

		public static bool SetRequiredGameVersions(global::Steamworks.UGCUpdateHandle_t handle, string pszGameBranchMin, string pszGameBranchMax)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pszGameBranchMin2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pszGameBranchMin);
			using global::Steamworks.InteropHelp.UTF8StringHandle pszGameBranchMax2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pszGameBranchMax);
			return global::Steamworks.NativeMethods.ISteamUGC_SetRequiredGameVersions(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, pszGameBranchMin2, pszGameBranchMax2);
		}

		public static global::Steamworks.SteamAPICall_t SubmitItemUpdate(global::Steamworks.UGCUpdateHandle_t handle, string pchChangeNote)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchChangeNote2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchChangeNote);
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamUGC_SubmitItemUpdate(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, pchChangeNote2);
		}

		public static global::Steamworks.EItemUpdateStatus GetItemUpdateProgress(global::Steamworks.UGCUpdateHandle_t handle, out ulong punBytesProcessed, out ulong punBytesTotal)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_GetItemUpdateProgress(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), handle, out punBytesProcessed, out punBytesTotal);
		}

		public static global::Steamworks.SteamAPICall_t SetUserItemVote(global::Steamworks.PublishedFileId_t nPublishedFileID, bool bVoteUp)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamUGC_SetUserItemVote(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), nPublishedFileID, bVoteUp);
		}

		public static global::Steamworks.SteamAPICall_t GetUserItemVote(global::Steamworks.PublishedFileId_t nPublishedFileID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamUGC_GetUserItemVote(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), nPublishedFileID);
		}

		public static global::Steamworks.SteamAPICall_t AddItemToFavorites(global::Steamworks.AppId_t nAppId, global::Steamworks.PublishedFileId_t nPublishedFileID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamUGC_AddItemToFavorites(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), nAppId, nPublishedFileID);
		}

		public static global::Steamworks.SteamAPICall_t RemoveItemFromFavorites(global::Steamworks.AppId_t nAppId, global::Steamworks.PublishedFileId_t nPublishedFileID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamUGC_RemoveItemFromFavorites(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), nAppId, nPublishedFileID);
		}

		public static global::Steamworks.SteamAPICall_t SubscribeItem(global::Steamworks.PublishedFileId_t nPublishedFileID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamUGC_SubscribeItem(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), nPublishedFileID);
		}

		public static global::Steamworks.SteamAPICall_t UnsubscribeItem(global::Steamworks.PublishedFileId_t nPublishedFileID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamUGC_UnsubscribeItem(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), nPublishedFileID);
		}

		public static uint GetNumSubscribedItems(bool bIncludeLocallyDisabled = false)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_GetNumSubscribedItems(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), bIncludeLocallyDisabled);
		}

		public static uint GetSubscribedItems(global::Steamworks.PublishedFileId_t[] pvecPublishedFileID, uint cMaxEntries, bool bIncludeLocallyDisabled = false)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_GetSubscribedItems(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), pvecPublishedFileID, cMaxEntries, bIncludeLocallyDisabled);
		}

		public static uint GetItemState(global::Steamworks.PublishedFileId_t nPublishedFileID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_GetItemState(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), nPublishedFileID);
		}

		public static bool GetItemInstallInfo(global::Steamworks.PublishedFileId_t nPublishedFileID, out ulong punSizeOnDisk, out string pchFolder, uint cchFolderSize, out uint punTimeStamp)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal((int)cchFolderSize);
			bool flag = global::Steamworks.NativeMethods.ISteamUGC_GetItemInstallInfo(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), nPublishedFileID, out punSizeOnDisk, intPtr, cchFolderSize, out punTimeStamp);
			pchFolder = (flag ? global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr) : null);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		public static bool GetItemDownloadInfo(global::Steamworks.PublishedFileId_t nPublishedFileID, out ulong punBytesDownloaded, out ulong punBytesTotal)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_GetItemDownloadInfo(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), nPublishedFileID, out punBytesDownloaded, out punBytesTotal);
		}

		public static bool DownloadItem(global::Steamworks.PublishedFileId_t nPublishedFileID, bool bHighPriority)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_DownloadItem(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), nPublishedFileID, bHighPriority);
		}

		public static bool BInitWorkshopForGameServer(global::Steamworks.DepotId_t unWorkshopDepotID, string pszFolder)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pszFolder2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pszFolder);
			return global::Steamworks.NativeMethods.ISteamUGC_BInitWorkshopForGameServer(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), unWorkshopDepotID, pszFolder2);
		}

		public static void SuspendDownloads(bool bSuspend)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::Steamworks.NativeMethods.ISteamUGC_SuspendDownloads(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), bSuspend);
		}

		public static global::Steamworks.SteamAPICall_t StartPlaytimeTracking(global::Steamworks.PublishedFileId_t[] pvecPublishedFileID, uint unNumPublishedFileIDs)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamUGC_StartPlaytimeTracking(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), pvecPublishedFileID, unNumPublishedFileIDs);
		}

		public static global::Steamworks.SteamAPICall_t StopPlaytimeTracking(global::Steamworks.PublishedFileId_t[] pvecPublishedFileID, uint unNumPublishedFileIDs)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamUGC_StopPlaytimeTracking(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), pvecPublishedFileID, unNumPublishedFileIDs);
		}

		public static global::Steamworks.SteamAPICall_t StopPlaytimeTrackingForAllItems()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamUGC_StopPlaytimeTrackingForAllItems(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC());
		}

		public static global::Steamworks.SteamAPICall_t AddDependency(global::Steamworks.PublishedFileId_t nParentPublishedFileID, global::Steamworks.PublishedFileId_t nChildPublishedFileID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamUGC_AddDependency(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), nParentPublishedFileID, nChildPublishedFileID);
		}

		public static global::Steamworks.SteamAPICall_t RemoveDependency(global::Steamworks.PublishedFileId_t nParentPublishedFileID, global::Steamworks.PublishedFileId_t nChildPublishedFileID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamUGC_RemoveDependency(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), nParentPublishedFileID, nChildPublishedFileID);
		}

		public static global::Steamworks.SteamAPICall_t AddAppDependency(global::Steamworks.PublishedFileId_t nPublishedFileID, global::Steamworks.AppId_t nAppID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamUGC_AddAppDependency(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), nPublishedFileID, nAppID);
		}

		public static global::Steamworks.SteamAPICall_t RemoveAppDependency(global::Steamworks.PublishedFileId_t nPublishedFileID, global::Steamworks.AppId_t nAppID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamUGC_RemoveAppDependency(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), nPublishedFileID, nAppID);
		}

		public static global::Steamworks.SteamAPICall_t GetAppDependencies(global::Steamworks.PublishedFileId_t nPublishedFileID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamUGC_GetAppDependencies(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), nPublishedFileID);
		}

		public static global::Steamworks.SteamAPICall_t DeleteItem(global::Steamworks.PublishedFileId_t nPublishedFileID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamUGC_DeleteItem(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), nPublishedFileID);
		}

		public static bool ShowWorkshopEULA()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_ShowWorkshopEULA(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC());
		}

		public static global::Steamworks.SteamAPICall_t GetWorkshopEULAStatus()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamUGC_GetWorkshopEULAStatus(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC());
		}

		public static uint GetUserContentDescriptorPreferences(global::Steamworks.EUGCContentDescriptorID[] pvecDescriptors, uint cMaxEntries)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_GetUserContentDescriptorPreferences(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), pvecDescriptors, cMaxEntries);
		}

		public static bool SetItemsDisabledLocally(global::Steamworks.PublishedFileId_t[] pvecPublishedFileIDs, uint unNumPublishedFileIDs, bool bDisabledLocally)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_SetItemsDisabledLocally(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), pvecPublishedFileIDs, unNumPublishedFileIDs, bDisabledLocally);
		}

		public static bool SetSubscriptionsLoadOrder(global::Steamworks.PublishedFileId_t[] pvecPublishedFileIDs, uint unNumPublishedFileIDs)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamUGC_SetSubscriptionsLoadOrder(global::Steamworks.CSteamGameServerAPIContext.GetSteamUGC(), pvecPublishedFileIDs, unNumPublishedFileIDs);
		}
	}
}
