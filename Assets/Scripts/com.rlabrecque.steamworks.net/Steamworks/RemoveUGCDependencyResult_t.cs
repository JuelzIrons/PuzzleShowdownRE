namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(3413)]
	public struct RemoveUGCDependencyResult_t
	{
		public const int k_iCallback = 3413;

		public global::Steamworks.EResult m_eResult;

		public global::Steamworks.PublishedFileId_t m_nPublishedFileId;

		public global::Steamworks.PublishedFileId_t m_nChildPublishedFileId;
	}
}
