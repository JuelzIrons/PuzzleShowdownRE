namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(168)]
	public struct GetTicketForWebApiResponse_t
	{
		public const int k_iCallback = 168;

		public global::Steamworks.HAuthTicket m_hAuthTicket;

		public global::Steamworks.EResult m_eResult;

		public int m_cubTicket;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 2560)]
		public byte[] m_rgubTicket;
	}
}
