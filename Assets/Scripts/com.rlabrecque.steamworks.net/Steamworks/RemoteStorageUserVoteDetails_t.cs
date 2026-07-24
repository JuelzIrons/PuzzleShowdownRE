namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(1325)]
	public struct RemoteStorageUserVoteDetails_t
	{
		public const int k_iCallback = 1325;

		public global::Steamworks.EResult m_eResult;

		public global::Steamworks.PublishedFileId_t m_nPublishedFileId;

		public global::Steamworks.EWorkshopVote m_eVote;
	}
}
