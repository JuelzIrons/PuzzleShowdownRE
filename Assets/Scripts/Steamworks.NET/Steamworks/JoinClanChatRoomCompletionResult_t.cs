namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 4)]
	[global::Steamworks.CallbackIdentity(342)]
	public struct JoinClanChatRoomCompletionResult_t
	{
		public const int k_iCallback = 342;

		public global::Steamworks.CSteamID m_steamIDClanChat;

		public global::Steamworks.EChatRoomEnterResponse m_eChatRoomEnterResponse;
	}
}
