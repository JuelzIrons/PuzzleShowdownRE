namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(6002)]
	public struct SteamTimelineEventRecordingExists_t
	{
		public const int k_iCallback = 6002;

		public ulong m_ulEventID;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public bool m_bRecordingExists;
	}
}
