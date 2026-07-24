namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(4611)]
	public struct GetVideoURLResult_t
	{
		public const int k_iCallback = 4611;

		public global::Steamworks.EResult m_eResult;

		public global::Steamworks.AppId_t m_unVideoAppID;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 256)]
		private byte[] m_rgchURL_;

		public string m_rgchURL
		{
			get
			{
				return global::Steamworks.InteropHelp.ByteArrayToStringUTF8(m_rgchURL_);
			}
			set
			{
				global::Steamworks.InteropHelp.StringToByteArrayUTF8(value, m_rgchURL_, 256);
			}
		}
	}
}
