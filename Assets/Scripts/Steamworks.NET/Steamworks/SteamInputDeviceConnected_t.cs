namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(2801)]
	public struct SteamInputDeviceConnected_t
	{
		public const int k_iCallback = 2801;

		public global::Steamworks.InputHandle_t m_ulConnectedDeviceHandle;
	}
}
