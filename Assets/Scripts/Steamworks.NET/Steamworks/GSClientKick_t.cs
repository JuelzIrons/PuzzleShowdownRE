namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 4)]
	[global::Steamworks.CallbackIdentity(203)]
	public struct GSClientKick_t
	{
		public const int k_iCallback = 203;

		public global::Steamworks.CSteamID m_SteamID;

		public global::Steamworks.EDenyReason m_eDenyReason;
	}
}
