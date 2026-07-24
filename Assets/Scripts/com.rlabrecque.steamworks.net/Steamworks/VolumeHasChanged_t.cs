namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(4002)]
	public struct VolumeHasChanged_t
	{
		public const int k_iCallback = 4002;

		public float m_flNewVolume;
	}
}
