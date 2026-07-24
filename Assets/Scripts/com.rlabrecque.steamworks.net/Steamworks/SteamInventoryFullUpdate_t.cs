namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(4701)]
	public struct SteamInventoryFullUpdate_t
	{
		public const int k_iCallback = 4701;

		public global::Steamworks.SteamInventoryResult_t m_handle;
	}
}
