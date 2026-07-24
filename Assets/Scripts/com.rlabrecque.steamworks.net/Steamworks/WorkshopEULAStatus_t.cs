namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(3420)]
	public struct WorkshopEULAStatus_t
	{
		public const int k_iCallback = 3420;

		public global::Steamworks.EResult m_eResult;

		public global::Steamworks.AppId_t m_nAppID;

		public uint m_unVersion;

		public global::Steamworks.RTime32 m_rtAction;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public bool m_bAccepted;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public bool m_bNeedsAction;
	}
}
