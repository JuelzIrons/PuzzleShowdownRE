namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(5302)]
	public struct CreateBeaconCallback_t
	{
		public const int k_iCallback = 5302;

		public global::Steamworks.EResult m_eResult;

		public global::Steamworks.PartyBeaconID_t m_ulBeaconID;
	}
}
