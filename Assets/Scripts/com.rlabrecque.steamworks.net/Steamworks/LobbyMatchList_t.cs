namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(510)]
	public struct LobbyMatchList_t
	{
		public const int k_iCallback = 510;

		public uint m_nLobbiesMatching;
	}
}
