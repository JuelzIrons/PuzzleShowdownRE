namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(207)]
	public struct GSGameplayStats_t
	{
		public const int k_iCallback = 207;

		public global::Steamworks.EResult m_eResult;

		public int m_nRank;

		public uint m_unTotalConnects;

		public uint m_unTotalMinutesPlayed;
	}
}
