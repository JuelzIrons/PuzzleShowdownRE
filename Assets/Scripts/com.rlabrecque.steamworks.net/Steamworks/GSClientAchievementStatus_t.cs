namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(206)]
	public struct GSClientAchievementStatus_t
	{
		public const int k_iCallback = 206;

		public ulong m_SteamID;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 128)]
		private byte[] m_pchAchievement_;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public bool m_bUnlocked;

		public string m_pchAchievement
		{
			get
			{
				return global::Steamworks.InteropHelp.ByteArrayToStringUTF8(m_pchAchievement_);
			}
			set
			{
				global::Steamworks.InteropHelp.StringToByteArrayUTF8(value, m_pchAchievement_, 128);
			}
		}
	}
}
