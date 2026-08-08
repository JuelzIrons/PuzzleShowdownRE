namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(4522)]
	public struct HTML_SetCursor_t
	{
		public const int k_iCallback = 4522;

		public global::Steamworks.HHTMLBrowser unBrowserHandle;

		public uint eMouseCursor;
	}
}
