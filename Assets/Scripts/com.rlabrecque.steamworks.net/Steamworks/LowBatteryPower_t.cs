namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(702)]
	public struct LowBatteryPower_t
	{
		public const int k_iCallback = 702;

		public byte m_nMinutesBatteryLeft;
	}
}
