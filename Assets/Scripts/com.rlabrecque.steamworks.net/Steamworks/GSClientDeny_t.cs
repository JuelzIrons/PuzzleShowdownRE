namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 4)]
	[global::Steamworks.CallbackIdentity(202)]
	public struct GSClientDeny_t
	{
		public const int k_iCallback = 202;

		public global::Steamworks.CSteamID m_SteamID;

		public global::Steamworks.EDenyReason m_eDenyReason;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 128)]
		private byte[] m_rgchOptionalText_;

		public string m_rgchOptionalText
		{
			get
			{
				return global::Steamworks.InteropHelp.ByteArrayToStringUTF8(m_rgchOptionalText_);
			}
			set
			{
				global::Steamworks.InteropHelp.StringToByteArrayUTF8(value, m_rgchOptionalText_, 128);
			}
		}
	}
}
