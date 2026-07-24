namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(3401)]
	public struct SteamUGCQueryCompleted_t
	{
		public const int k_iCallback = 3401;

		public global::Steamworks.UGCQueryHandle_t m_handle;

		public global::Steamworks.EResult m_eResult;

		public uint m_unNumResultsReturned;

		public uint m_unTotalMatchingResults;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public bool m_bCachedData;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 256)]
		private byte[] m_rgchNextCursor_;

		public string m_rgchNextCursor
		{
			get
			{
				return global::Steamworks.InteropHelp.ByteArrayToStringUTF8(m_rgchNextCursor_);
			}
			set
			{
				global::Steamworks.InteropHelp.StringToByteArrayUTF8(value, m_rgchNextCursor_, 256);
			}
		}
	}
}
