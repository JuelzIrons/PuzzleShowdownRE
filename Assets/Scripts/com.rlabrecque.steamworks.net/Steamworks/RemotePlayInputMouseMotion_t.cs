namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	public struct RemotePlayInputMouseMotion_t
	{
		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public bool m_bAbsolute;

		public float m_flNormalizedX;

		public float m_flNormalizedY;

		public int m_nDeltaX;

		public int m_nDeltaY;
	}
}
