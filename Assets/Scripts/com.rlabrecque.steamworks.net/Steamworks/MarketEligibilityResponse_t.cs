namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(166)]
	public struct MarketEligibilityResponse_t
	{
		public const int k_iCallback = 166;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public bool m_bAllowed;

		public global::Steamworks.EMarketNotAllowedReasonFlags m_eNotAllowedReason;

		public global::Steamworks.RTime32 m_rtAllowedAtTime;

		public int m_cdaySteamGuardRequiredDays;

		public int m_cdayNewDeviceCooldown;
	}
}
