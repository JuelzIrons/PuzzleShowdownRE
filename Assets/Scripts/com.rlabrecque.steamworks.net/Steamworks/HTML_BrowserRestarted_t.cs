namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(4527)]
	public struct HTML_BrowserRestarted_t
	{
		public const int k_iCallback = 4527;

		public global::Steamworks.HHTMLBrowser unBrowserHandle;

		public global::Steamworks.HHTMLBrowser unOldBrowserHandle;
	}
}
