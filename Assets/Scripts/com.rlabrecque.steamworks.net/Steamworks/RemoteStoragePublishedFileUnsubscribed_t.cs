namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(1322)]
	public struct RemoteStoragePublishedFileUnsubscribed_t
	{
		public const int k_iCallback = 1322;

		public global::Steamworks.PublishedFileId_t m_nPublishedFileId;

		public global::Steamworks.AppId_t m_nAppID;
	}
}
