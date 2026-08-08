namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(1021)]
	public struct AppProofOfPurchaseKeyResponse_t
	{
		public const int k_iCallback = 1021;

		public global::Steamworks.EResult m_eResult;

		public uint m_nAppID;

		public uint m_cchKeyLength;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 240)]
		private byte[] m_rgchKey_;

		public string m_rgchKey
		{
			get
			{
				return global::Steamworks.InteropHelp.ByteArrayToStringUTF8(m_rgchKey_);
			}
			set
			{
				global::Steamworks.InteropHelp.StringToByteArrayUTF8(value, m_rgchKey_, 240);
			}
		}
	}
}
