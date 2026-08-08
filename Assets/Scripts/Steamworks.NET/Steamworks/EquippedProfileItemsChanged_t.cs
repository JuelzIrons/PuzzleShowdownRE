namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(350)]
	public struct EquippedProfileItemsChanged_t
	{
		public const int k_iCallback = 350;

		public global::Steamworks.CSteamID m_steamID;
	}
}
