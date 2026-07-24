namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(349)]
	public struct OverlayBrowserProtocolNavigation_t
	{
		public const int k_iCallback = 349;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 1024)]
		private byte[] rgchURI_;

		public string rgchURI
		{
			get
			{
				return global::Steamworks.InteropHelp.ByteArrayToStringUTF8(rgchURI_);
			}
			set
			{
				global::Steamworks.InteropHelp.StringToByteArrayUTF8(value, rgchURI_, 1024);
			}
		}
	}
}
