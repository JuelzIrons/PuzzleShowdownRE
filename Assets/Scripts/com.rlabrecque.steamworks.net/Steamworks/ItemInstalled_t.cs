namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(3405)]
	public struct ItemInstalled_t
	{
		public const int k_iCallback = 3405;

		public global::Steamworks.AppId_t m_unAppID;

		public global::Steamworks.PublishedFileId_t m_nPublishedFileId;

		public global::Steamworks.UGCHandle_t m_hLegacyContent;

		public ulong m_unManifestID;
	}
}
