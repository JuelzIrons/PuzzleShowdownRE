namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(3406)]
	public struct DownloadItemResult_t
	{
		public const int k_iCallback = 3406;

		public global::Steamworks.AppId_t m_unAppID;

		public global::Steamworks.PublishedFileId_t m_nPublishedFileId;

		public global::Steamworks.EResult m_eResult;
	}
}
