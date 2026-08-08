namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	public struct RemotePlayInputMouseWheel_t
	{
		public global::Steamworks.ERemotePlayMouseWheelDirection m_eDirection;

		public float m_flAmount;
	}
}
