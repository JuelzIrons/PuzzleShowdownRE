namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(1109)]
	public struct UserAchievementIconFetched_t
	{
		public const int k_iCallback = 1109;

		public global::Steamworks.CGameID m_nGameID;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 128)]
		private byte[] m_rgchAchievementName_;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public bool m_bAchieved;

		public int m_nIconHandle;

		public string m_rgchAchievementName
		{
			get
			{
				return global::Steamworks.InteropHelp.ByteArrayToStringUTF8(m_rgchAchievementName_);
			}
			set
			{
				global::Steamworks.InteropHelp.StringToByteArrayUTF8(value, m_rgchAchievementName_, 128);
			}
		}
	}
}
