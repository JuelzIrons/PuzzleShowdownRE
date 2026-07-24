namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(3402)]
	public struct SteamUGCRequestUGCDetailsResult_t
	{
		public const int k_iCallback = 3402;

		public global::Steamworks.SteamUGCDetails_t m_details;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public bool m_bCachedData;
	}
}
