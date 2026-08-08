namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(2804)]
	public struct SteamInputGamepadSlotChange_t
	{
		public const int k_iCallback = 2804;

		public global::Steamworks.AppId_t m_unAppID;

		public global::Steamworks.InputHandle_t m_ulDeviceHandle;

		public global::Steamworks.ESteamInputType m_eDeviceType;

		public int m_nOldGamepadSlot;

		public int m_nNewGamepadSlot;
	}
}
