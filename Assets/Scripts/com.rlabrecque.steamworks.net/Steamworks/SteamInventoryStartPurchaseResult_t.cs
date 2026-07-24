namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(4704)]
	public struct SteamInventoryStartPurchaseResult_t
	{
		public const int k_iCallback = 4704;

		public global::Steamworks.EResult m_result;

		public ulong m_ulOrderID;

		public ulong m_ulTransID;
	}
}
