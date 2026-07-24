namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(4703)]
	public struct SteamInventoryEligiblePromoItemDefIDs_t
	{
		public const int k_iCallback = 4703;

		public global::Steamworks.EResult m_result;

		public global::Steamworks.CSteamID m_steamID;

		public int m_numEligiblePromoItemDefs;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public bool m_bCachedData;
	}
}
