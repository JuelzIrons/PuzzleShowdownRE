namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(1329)]
	public struct RemoteStoragePublishFileProgress_t
	{
		public const int k_iCallback = 1329;

		public double m_dPercentFile;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public bool m_bPreview;
	}
}
