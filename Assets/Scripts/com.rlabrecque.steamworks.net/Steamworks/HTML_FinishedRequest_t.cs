namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(4506)]
	public struct HTML_FinishedRequest_t
	{
		public const int k_iCallback = 4506;

		public global::Steamworks.HHTMLBrowser unBrowserHandle;

		public string pchURL;

		public string pchPageTitle;
	}
}
