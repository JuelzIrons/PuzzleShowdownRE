namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(4624)]
	public struct GetOPFSettingsResult_t
	{
		public const int k_iCallback = 4624;

		public global::Steamworks.EResult m_eResult;

		public global::Steamworks.AppId_t m_unVideoAppID;
	}
}
