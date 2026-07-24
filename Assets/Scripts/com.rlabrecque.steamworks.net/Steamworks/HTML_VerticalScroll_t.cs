namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(4512)]
	public struct HTML_VerticalScroll_t
	{
		public const int k_iCallback = 4512;

		public global::Steamworks.HHTMLBrowser unBrowserHandle;

		public uint unScrollMax;

		public uint unScrollCurrent;

		public float flPageScale;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public bool bVisible;

		public uint unPageSize;
	}
}
