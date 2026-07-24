namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(4705)]
	public struct SteamInventoryRequestPricesResult_t
	{
		public const int k_iCallback = 4705;

		public global::Steamworks.EResult m_result;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 4)]
		private byte[] m_rgchCurrency_;

		public string m_rgchCurrency
		{
			get
			{
				return global::Steamworks.InteropHelp.ByteArrayToStringUTF8(m_rgchCurrency_);
			}
			set
			{
				global::Steamworks.InteropHelp.StringToByteArrayUTF8(value, m_rgchCurrency_, 4);
			}
		}
	}
}
