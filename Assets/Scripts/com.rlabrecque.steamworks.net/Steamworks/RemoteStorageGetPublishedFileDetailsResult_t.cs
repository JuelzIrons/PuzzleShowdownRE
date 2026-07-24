namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(1318)]
	public struct RemoteStorageGetPublishedFileDetailsResult_t
	{
		public const int k_iCallback = 1318;

		public global::Steamworks.EResult m_eResult;

		public global::Steamworks.PublishedFileId_t m_nPublishedFileId;

		public global::Steamworks.AppId_t m_nCreatorAppID;

		public global::Steamworks.AppId_t m_nConsumerAppID;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 129)]
		private byte[] m_rgchTitle_;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 8000)]
		private byte[] m_rgchDescription_;

		public global::Steamworks.UGCHandle_t m_hFile;

		public global::Steamworks.UGCHandle_t m_hPreviewFile;

		public ulong m_ulSteamIDOwner;

		public uint m_rtimeCreated;

		public uint m_rtimeUpdated;

		public global::Steamworks.ERemoteStoragePublishedFileVisibility m_eVisibility;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public bool m_bBanned;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 1025)]
		private byte[] m_rgchTags_;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public bool m_bTagsTruncated;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 260)]
		private byte[] m_pchFileName_;

		public int m_nFileSize;

		public int m_nPreviewFileSize;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 256)]
		private byte[] m_rgchURL_;

		public global::Steamworks.EWorkshopFileType m_eFileType;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public bool m_bAcceptedForUse;

		public string m_rgchTitle
		{
			get
			{
				return global::Steamworks.InteropHelp.ByteArrayToStringUTF8(m_rgchTitle_);
			}
			set
			{
				global::Steamworks.InteropHelp.StringToByteArrayUTF8(value, m_rgchTitle_, 129);
			}
		}

		public string m_rgchDescription
		{
			get
			{
				return global::Steamworks.InteropHelp.ByteArrayToStringUTF8(m_rgchDescription_);
			}
			set
			{
				global::Steamworks.InteropHelp.StringToByteArrayUTF8(value, m_rgchDescription_, 8000);
			}
		}

		public string m_rgchTags
		{
			get
			{
				return global::Steamworks.InteropHelp.ByteArrayToStringUTF8(m_rgchTags_);
			}
			set
			{
				global::Steamworks.InteropHelp.StringToByteArrayUTF8(value, m_rgchTags_, 1025);
			}
		}

		public string m_pchFileName
		{
			get
			{
				return global::Steamworks.InteropHelp.ByteArrayToStringUTF8(m_pchFileName_);
			}
			set
			{
				global::Steamworks.InteropHelp.StringToByteArrayUTF8(value, m_pchFileName_, 260);
			}
		}

		public string m_rgchURL
		{
			get
			{
				return global::Steamworks.InteropHelp.ByteArrayToStringUTF8(m_rgchURL_);
			}
			set
			{
				global::Steamworks.InteropHelp.StringToByteArrayUTF8(value, m_rgchURL_, 256);
			}
		}
	}
}
