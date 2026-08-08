namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(3418)]
	public struct UserSubscribedItemsListChanged_t
	{
		public const int k_iCallback = 3418;

		public global::Steamworks.AppId_t m_nAppID;
	}
}
