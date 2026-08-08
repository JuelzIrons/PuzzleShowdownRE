namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(1317)]
	public struct RemoteStorageDownloadUGCResult_t
	{
		public const int k_iCallback = 1317;

		public global::Steamworks.EResult m_eResult;

		public global::Steamworks.UGCHandle_t m_hFile;

		public global::Steamworks.AppId_t m_nAppID;

		public int m_nSizeInBytes;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 260)]
		private byte[] m_pchFileName_;

		public ulong m_ulSteamIDOwner;

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
	}
}
