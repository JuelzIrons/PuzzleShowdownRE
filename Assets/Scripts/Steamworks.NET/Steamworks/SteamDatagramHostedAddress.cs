namespace Steamworks
{
	[global::System.Serializable]
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	public struct SteamDatagramHostedAddress
	{
		public int m_cbSize;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 128)]
		public byte[] m_data;

		public void Clear()
		{
			m_cbSize = 0;
			m_data = new byte[128];
		}
	}
}
