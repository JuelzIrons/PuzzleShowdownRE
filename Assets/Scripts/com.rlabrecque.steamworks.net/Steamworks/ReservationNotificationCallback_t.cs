namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(5303)]
	public struct ReservationNotificationCallback_t
	{
		public const int k_iCallback = 5303;

		public global::Steamworks.PartyBeaconID_t m_ulBeaconID;

		public global::Steamworks.CSteamID m_steamIDJoiner;
	}
}
