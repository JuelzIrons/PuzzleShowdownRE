namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(4513)]
	public struct HTML_LinkAtPosition_t
	{
		public const int k_iCallback = 4513;

		public global::Steamworks.HHTMLBrowser unBrowserHandle;

		public uint x;

		public uint y;

		public string pchURL;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public bool bInput;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public bool bLiveLink;
	}
}
