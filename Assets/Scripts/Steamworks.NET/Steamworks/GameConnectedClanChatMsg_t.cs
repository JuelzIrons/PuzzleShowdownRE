namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 4)]
	[global::Steamworks.CallbackIdentity(338)]
	public struct GameConnectedClanChatMsg_t
	{
		public const int k_iCallback = 338;

		public global::Steamworks.CSteamID m_steamIDClanChat;

		public global::Steamworks.CSteamID m_steamIDUser;

		public int m_iMessageID;
	}
}
