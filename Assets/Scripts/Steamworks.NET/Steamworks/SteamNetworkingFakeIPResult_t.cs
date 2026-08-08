namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(1223)]
	public struct SteamNetworkingFakeIPResult_t
	{
		public const int k_iCallback = 1223;

		public global::Steamworks.EResult m_eResult;

		public global::Steamworks.SteamNetworkingIdentity m_identity;

		public uint m_unIP;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 8)]
		public ushort[] m_unPorts;
	}
}
