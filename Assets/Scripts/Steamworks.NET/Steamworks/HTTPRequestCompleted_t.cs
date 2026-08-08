namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(2101)]
	public struct HTTPRequestCompleted_t
	{
		public const int k_iCallback = 2101;

		public global::Steamworks.HTTPRequestHandle m_hRequest;

		public ulong m_ulContextValue;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public bool m_bRequestSuccessful;

		public global::Steamworks.EHTTPStatusCode m_eStatusCode;

		public uint m_unBodySize;
	}
}
