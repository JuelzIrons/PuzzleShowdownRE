namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(6001)]
	public struct SteamTimelineGamePhaseRecordingExists_t
	{
		public const int k_iCallback = 6001;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 64)]
		private byte[] m_rgchPhaseID_;

		public ulong m_ulRecordingMS;

		public ulong m_ulLongestClipMS;

		public uint m_unClipCount;

		public uint m_unScreenshotCount;

		public string m_rgchPhaseID
		{
			get
			{
				return global::Steamworks.InteropHelp.ByteArrayToStringUTF8(m_rgchPhaseID_);
			}
			set
			{
				global::Steamworks.InteropHelp.StringToByteArrayUTF8(value, m_rgchPhaseID_, 64);
			}
		}
	}
}
