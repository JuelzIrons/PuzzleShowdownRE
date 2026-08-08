namespace Steamworks
{
	public static class SteamRemoteStorage
	{
		public static bool FileWrite(string pchFile, byte[] pvData, int cubData)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchFile2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchFile);
			return global::Steamworks.NativeMethods.ISteamRemoteStorage_FileWrite(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), pchFile2, pvData, cubData);
		}

		public static int FileRead(string pchFile, byte[] pvData, int cubDataToRead)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchFile2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchFile);
			return global::Steamworks.NativeMethods.ISteamRemoteStorage_FileRead(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), pchFile2, pvData, cubDataToRead);
		}

		public static global::Steamworks.SteamAPICall_t FileWriteAsync(string pchFile, byte[] pvData, uint cubData)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchFile2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchFile);
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamRemoteStorage_FileWriteAsync(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), pchFile2, pvData, cubData);
		}

		public static global::Steamworks.SteamAPICall_t FileReadAsync(string pchFile, uint nOffset, uint cubToRead)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchFile2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchFile);
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamRemoteStorage_FileReadAsync(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), pchFile2, nOffset, cubToRead);
		}

		public static bool FileReadAsyncComplete(global::Steamworks.SteamAPICall_t hReadCall, byte[] pvBuffer, uint cubToRead)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamRemoteStorage_FileReadAsyncComplete(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), hReadCall, pvBuffer, cubToRead);
		}

		public static bool FileForget(string pchFile)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchFile2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchFile);
			return global::Steamworks.NativeMethods.ISteamRemoteStorage_FileForget(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), pchFile2);
		}

		public static bool FileDelete(string pchFile)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchFile2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchFile);
			return global::Steamworks.NativeMethods.ISteamRemoteStorage_FileDelete(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), pchFile2);
		}

		public static global::Steamworks.SteamAPICall_t FileShare(string pchFile)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchFile2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchFile);
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamRemoteStorage_FileShare(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), pchFile2);
		}

		public static bool SetSyncPlatforms(string pchFile, global::Steamworks.ERemoteStoragePlatform eRemoteStoragePlatform)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchFile2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchFile);
			return global::Steamworks.NativeMethods.ISteamRemoteStorage_SetSyncPlatforms(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), pchFile2, eRemoteStoragePlatform);
		}

		public static global::Steamworks.UGCFileWriteStreamHandle_t FileWriteStreamOpen(string pchFile)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchFile2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchFile);
			return (global::Steamworks.UGCFileWriteStreamHandle_t)global::Steamworks.NativeMethods.ISteamRemoteStorage_FileWriteStreamOpen(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), pchFile2);
		}

		public static bool FileWriteStreamWriteChunk(global::Steamworks.UGCFileWriteStreamHandle_t writeHandle, byte[] pvData, int cubData)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamRemoteStorage_FileWriteStreamWriteChunk(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), writeHandle, pvData, cubData);
		}

		public static bool FileWriteStreamClose(global::Steamworks.UGCFileWriteStreamHandle_t writeHandle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamRemoteStorage_FileWriteStreamClose(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), writeHandle);
		}

		public static bool FileWriteStreamCancel(global::Steamworks.UGCFileWriteStreamHandle_t writeHandle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamRemoteStorage_FileWriteStreamCancel(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), writeHandle);
		}

		public static bool FileExists(string pchFile)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchFile2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchFile);
			return global::Steamworks.NativeMethods.ISteamRemoteStorage_FileExists(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), pchFile2);
		}

		public static bool FilePersisted(string pchFile)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchFile2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchFile);
			return global::Steamworks.NativeMethods.ISteamRemoteStorage_FilePersisted(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), pchFile2);
		}

		public static int GetFileSize(string pchFile)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchFile2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchFile);
			return global::Steamworks.NativeMethods.ISteamRemoteStorage_GetFileSize(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), pchFile2);
		}

		public static long GetFileTimestamp(string pchFile)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchFile2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchFile);
			return global::Steamworks.NativeMethods.ISteamRemoteStorage_GetFileTimestamp(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), pchFile2);
		}

		public static global::Steamworks.ERemoteStoragePlatform GetSyncPlatforms(string pchFile)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchFile2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchFile);
			return global::Steamworks.NativeMethods.ISteamRemoteStorage_GetSyncPlatforms(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), pchFile2);
		}

		public static int GetFileCount()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamRemoteStorage_GetFileCount(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage());
		}

		public static string GetFileNameAndSize(int iFile, out int pnFileSizeInBytes)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.InteropHelp.PtrToStringUTF8(global::Steamworks.NativeMethods.ISteamRemoteStorage_GetFileNameAndSize(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), iFile, out pnFileSizeInBytes));
		}

		public static bool GetQuota(out ulong pnTotalBytes, out ulong puAvailableBytes)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamRemoteStorage_GetQuota(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), out pnTotalBytes, out puAvailableBytes);
		}

		public static bool IsCloudEnabledForAccount()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamRemoteStorage_IsCloudEnabledForAccount(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage());
		}

		public static bool IsCloudEnabledForApp()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamRemoteStorage_IsCloudEnabledForApp(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage());
		}

		public static void SetCloudEnabledForApp(bool bEnabled)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamRemoteStorage_SetCloudEnabledForApp(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), bEnabled);
		}

		public static global::Steamworks.SteamAPICall_t UGCDownload(global::Steamworks.UGCHandle_t hContent, uint unPriority)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamRemoteStorage_UGCDownload(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), hContent, unPriority);
		}

		public static bool GetUGCDownloadProgress(global::Steamworks.UGCHandle_t hContent, out int pnBytesDownloaded, out int pnBytesExpected)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamRemoteStorage_GetUGCDownloadProgress(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), hContent, out pnBytesDownloaded, out pnBytesExpected);
		}

		public static bool GetUGCDetails(global::Steamworks.UGCHandle_t hContent, out global::Steamworks.AppId_t pnAppID, out string ppchName, out int pnFileSizeInBytes, out global::Steamworks.CSteamID pSteamIDOwner)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::System.IntPtr ppchName2;
			bool flag = global::Steamworks.NativeMethods.ISteamRemoteStorage_GetUGCDetails(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), hContent, out pnAppID, out ppchName2, out pnFileSizeInBytes, out pSteamIDOwner);
			ppchName = (flag ? global::Steamworks.InteropHelp.PtrToStringUTF8(ppchName2) : null);
			return flag;
		}

		public static int UGCRead(global::Steamworks.UGCHandle_t hContent, byte[] pvData, int cubDataToRead, uint cOffset, global::Steamworks.EUGCReadAction eAction)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamRemoteStorage_UGCRead(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), hContent, pvData, cubDataToRead, cOffset, eAction);
		}

		public static int GetCachedUGCCount()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamRemoteStorage_GetCachedUGCCount(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage());
		}

		public static global::Steamworks.UGCHandle_t GetCachedUGCHandle(int iCachedContent)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.UGCHandle_t)global::Steamworks.NativeMethods.ISteamRemoteStorage_GetCachedUGCHandle(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), iCachedContent);
		}

		public static global::Steamworks.SteamAPICall_t PublishWorkshopFile(string pchFile, string pchPreviewFile, global::Steamworks.AppId_t nConsumerAppId, string pchTitle, string pchDescription, global::Steamworks.ERemoteStoragePublishedFileVisibility eVisibility, global::System.Collections.Generic.IList<string> pTags, global::Steamworks.EWorkshopFileType eWorkshopFileType)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchFile2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchFile);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchPreviewFile2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchPreviewFile);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchTitle2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchTitle);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchDescription2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchDescription);
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamRemoteStorage_PublishWorkshopFile(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), pchFile2, pchPreviewFile2, nConsumerAppId, pchTitle2, pchDescription2, eVisibility, new global::Steamworks.InteropHelp.SteamParamStringArray(pTags), eWorkshopFileType);
		}

		public static global::Steamworks.PublishedFileUpdateHandle_t CreatePublishedFileUpdateRequest(global::Steamworks.PublishedFileId_t unPublishedFileId)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.PublishedFileUpdateHandle_t)global::Steamworks.NativeMethods.ISteamRemoteStorage_CreatePublishedFileUpdateRequest(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), unPublishedFileId);
		}

		public static bool UpdatePublishedFileFile(global::Steamworks.PublishedFileUpdateHandle_t updateHandle, string pchFile)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchFile2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchFile);
			return global::Steamworks.NativeMethods.ISteamRemoteStorage_UpdatePublishedFileFile(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), updateHandle, pchFile2);
		}

		public static bool UpdatePublishedFilePreviewFile(global::Steamworks.PublishedFileUpdateHandle_t updateHandle, string pchPreviewFile)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchPreviewFile2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchPreviewFile);
			return global::Steamworks.NativeMethods.ISteamRemoteStorage_UpdatePublishedFilePreviewFile(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), updateHandle, pchPreviewFile2);
		}

		public static bool UpdatePublishedFileTitle(global::Steamworks.PublishedFileUpdateHandle_t updateHandle, string pchTitle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchTitle2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchTitle);
			return global::Steamworks.NativeMethods.ISteamRemoteStorage_UpdatePublishedFileTitle(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), updateHandle, pchTitle2);
		}

		public static bool UpdatePublishedFileDescription(global::Steamworks.PublishedFileUpdateHandle_t updateHandle, string pchDescription)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchDescription2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchDescription);
			return global::Steamworks.NativeMethods.ISteamRemoteStorage_UpdatePublishedFileDescription(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), updateHandle, pchDescription2);
		}

		public static bool UpdatePublishedFileVisibility(global::Steamworks.PublishedFileUpdateHandle_t updateHandle, global::Steamworks.ERemoteStoragePublishedFileVisibility eVisibility)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamRemoteStorage_UpdatePublishedFileVisibility(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), updateHandle, eVisibility);
		}

		public static bool UpdatePublishedFileTags(global::Steamworks.PublishedFileUpdateHandle_t updateHandle, global::System.Collections.Generic.IList<string> pTags)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamRemoteStorage_UpdatePublishedFileTags(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), updateHandle, new global::Steamworks.InteropHelp.SteamParamStringArray(pTags));
		}

		public static global::Steamworks.SteamAPICall_t CommitPublishedFileUpdate(global::Steamworks.PublishedFileUpdateHandle_t updateHandle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamRemoteStorage_CommitPublishedFileUpdate(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), updateHandle);
		}

		public static global::Steamworks.SteamAPICall_t GetPublishedFileDetails(global::Steamworks.PublishedFileId_t unPublishedFileId, uint unMaxSecondsOld)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamRemoteStorage_GetPublishedFileDetails(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), unPublishedFileId, unMaxSecondsOld);
		}

		public static global::Steamworks.SteamAPICall_t DeletePublishedFile(global::Steamworks.PublishedFileId_t unPublishedFileId)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamRemoteStorage_DeletePublishedFile(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), unPublishedFileId);
		}

		public static global::Steamworks.SteamAPICall_t EnumerateUserPublishedFiles(uint unStartIndex)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamRemoteStorage_EnumerateUserPublishedFiles(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), unStartIndex);
		}

		public static global::Steamworks.SteamAPICall_t SubscribePublishedFile(global::Steamworks.PublishedFileId_t unPublishedFileId)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamRemoteStorage_SubscribePublishedFile(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), unPublishedFileId);
		}

		public static global::Steamworks.SteamAPICall_t EnumerateUserSubscribedFiles(uint unStartIndex)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamRemoteStorage_EnumerateUserSubscribedFiles(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), unStartIndex);
		}

		public static global::Steamworks.SteamAPICall_t UnsubscribePublishedFile(global::Steamworks.PublishedFileId_t unPublishedFileId)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamRemoteStorage_UnsubscribePublishedFile(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), unPublishedFileId);
		}

		public static bool UpdatePublishedFileSetChangeDescription(global::Steamworks.PublishedFileUpdateHandle_t updateHandle, string pchChangeDescription)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchChangeDescription2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchChangeDescription);
			return global::Steamworks.NativeMethods.ISteamRemoteStorage_UpdatePublishedFileSetChangeDescription(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), updateHandle, pchChangeDescription2);
		}

		public static global::Steamworks.SteamAPICall_t GetPublishedItemVoteDetails(global::Steamworks.PublishedFileId_t unPublishedFileId)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamRemoteStorage_GetPublishedItemVoteDetails(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), unPublishedFileId);
		}

		public static global::Steamworks.SteamAPICall_t UpdateUserPublishedItemVote(global::Steamworks.PublishedFileId_t unPublishedFileId, bool bVoteUp)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamRemoteStorage_UpdateUserPublishedItemVote(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), unPublishedFileId, bVoteUp);
		}

		public static global::Steamworks.SteamAPICall_t GetUserPublishedItemVoteDetails(global::Steamworks.PublishedFileId_t unPublishedFileId)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamRemoteStorage_GetUserPublishedItemVoteDetails(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), unPublishedFileId);
		}

		public static global::Steamworks.SteamAPICall_t EnumerateUserSharedWorkshopFiles(global::Steamworks.CSteamID steamId, uint unStartIndex, global::System.Collections.Generic.IList<string> pRequiredTags, global::System.Collections.Generic.IList<string> pExcludedTags)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamRemoteStorage_EnumerateUserSharedWorkshopFiles(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), steamId, unStartIndex, new global::Steamworks.InteropHelp.SteamParamStringArray(pRequiredTags), new global::Steamworks.InteropHelp.SteamParamStringArray(pExcludedTags));
		}

		public static global::Steamworks.SteamAPICall_t PublishVideo(global::Steamworks.EWorkshopVideoProvider eVideoProvider, string pchVideoAccount, string pchVideoIdentifier, string pchPreviewFile, global::Steamworks.AppId_t nConsumerAppId, string pchTitle, string pchDescription, global::Steamworks.ERemoteStoragePublishedFileVisibility eVisibility, global::System.Collections.Generic.IList<string> pTags)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchVideoAccount2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchVideoAccount);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchVideoIdentifier2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchVideoIdentifier);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchPreviewFile2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchPreviewFile);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchTitle2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchTitle);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchDescription2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchDescription);
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamRemoteStorage_PublishVideo(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), eVideoProvider, pchVideoAccount2, pchVideoIdentifier2, pchPreviewFile2, nConsumerAppId, pchTitle2, pchDescription2, eVisibility, new global::Steamworks.InteropHelp.SteamParamStringArray(pTags));
		}

		public static global::Steamworks.SteamAPICall_t SetUserPublishedFileAction(global::Steamworks.PublishedFileId_t unPublishedFileId, global::Steamworks.EWorkshopFileAction eAction)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamRemoteStorage_SetUserPublishedFileAction(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), unPublishedFileId, eAction);
		}

		public static global::Steamworks.SteamAPICall_t EnumeratePublishedFilesByUserAction(global::Steamworks.EWorkshopFileAction eAction, uint unStartIndex)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamRemoteStorage_EnumeratePublishedFilesByUserAction(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), eAction, unStartIndex);
		}

		public static global::Steamworks.SteamAPICall_t EnumeratePublishedWorkshopFiles(global::Steamworks.EWorkshopEnumerationType eEnumerationType, uint unStartIndex, uint unCount, uint unDays, global::System.Collections.Generic.IList<string> pTags, global::System.Collections.Generic.IList<string> pUserTags)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamRemoteStorage_EnumeratePublishedWorkshopFiles(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), eEnumerationType, unStartIndex, unCount, unDays, new global::Steamworks.InteropHelp.SteamParamStringArray(pTags), new global::Steamworks.InteropHelp.SteamParamStringArray(pUserTags));
		}

		public static global::Steamworks.SteamAPICall_t UGCDownloadToLocation(global::Steamworks.UGCHandle_t hContent, string pchLocation, uint unPriority)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchLocation2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchLocation);
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamRemoteStorage_UGCDownloadToLocation(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), hContent, pchLocation2, unPriority);
		}

		public static int GetLocalFileChangeCount()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamRemoteStorage_GetLocalFileChangeCount(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage());
		}

		public static string GetLocalFileChange(int iFile, out global::Steamworks.ERemoteStorageLocalFileChange pEChangeType, out global::Steamworks.ERemoteStorageFilePathType pEFilePathType)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.InteropHelp.PtrToStringUTF8(global::Steamworks.NativeMethods.ISteamRemoteStorage_GetLocalFileChange(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage(), iFile, out pEChangeType, out pEFilePathType));
		}

		public static bool BeginFileWriteBatch()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamRemoteStorage_BeginFileWriteBatch(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage());
		}

		public static bool EndFileWriteBatch()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamRemoteStorage_EndFileWriteBatch(global::Steamworks.CSteamAPIContext.GetSteamRemoteStorage());
		}
	}
}
