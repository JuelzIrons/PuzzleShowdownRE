namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(2802)]
	public struct SteamInputDeviceDisconnected_t
	{
		public const int k_iCallback = 2802;

		public global::Steamworks.InputHandle_t m_ulDisconnectedDeviceHandle;
	}
}
