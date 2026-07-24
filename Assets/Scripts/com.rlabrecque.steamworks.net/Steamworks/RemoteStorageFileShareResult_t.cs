namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(1307)]
	public struct RemoteStorageFileShareResult_t
	{
		public const int k_iCallback = 1307;

		public global::Steamworks.EResult m_eResult;

		public global::Steamworks.UGCHandle_t m_hFile;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 260)]
		private byte[] m_rgchFilename_;

		public string m_rgchFilename
		{
			get
			{
				return global::Steamworks.InteropHelp.ByteArrayToStringUTF8(m_rgchFilename_);
			}
			set
			{
				global::Steamworks.InteropHelp.StringToByteArrayUTF8(value, m_rgchFilename_, 260);
			}
		}
	}
}
