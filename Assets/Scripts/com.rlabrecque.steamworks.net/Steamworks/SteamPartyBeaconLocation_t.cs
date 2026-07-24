namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	public struct SteamPartyBeaconLocation_t
	{
		public global::Steamworks.ESteamPartyBeaconLocationType m_eType;

		public ulong m_ulLocationID;
	}
}
