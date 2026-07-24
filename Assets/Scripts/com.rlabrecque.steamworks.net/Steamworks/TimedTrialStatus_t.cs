namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(1030)]
	public struct TimedTrialStatus_t
	{
		public const int k_iCallback = 1030;

		public global::Steamworks.AppId_t m_unAppID;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public bool m_bIsOffline;

		public uint m_unSecondsAllowed;

		public uint m_unSecondsPlayed;
	}
}
