namespace Steamworks
{
	public static class SteamNetworkingMessages
	{
		public static global::Steamworks.EResult SendMessageToUser(ref global::Steamworks.SteamNetworkingIdentity identityRemote, global::System.IntPtr pubData, uint cubData, int nSendFlags, int nRemoteChannel)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamNetworkingMessages_SendMessageToUser(global::Steamworks.CSteamAPIContext.GetSteamNetworkingMessages(), ref identityRemote, pubData, cubData, nSendFlags, nRemoteChannel);
		}

		public static int ReceiveMessagesOnChannel(int nLocalChannel, global::System.IntPtr[] ppOutMessages, int nMaxMessages)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			if (ppOutMessages != null && ppOutMessages.Length != nMaxMessages)
			{
				throw new global::System.ArgumentException("ppOutMessages must be the same size as nMaxMessages!");
			}
			return global::Steamworks.NativeMethods.ISteamNetworkingMessages_ReceiveMessagesOnChannel(global::Steamworks.CSteamAPIContext.GetSteamNetworkingMessages(), nLocalChannel, ppOutMessages, nMaxMessages);
		}

		public static bool AcceptSessionWithUser(ref global::Steamworks.SteamNetworkingIdentity identityRemote)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamNetworkingMessages_AcceptSessionWithUser(global::Steamworks.CSteamAPIContext.GetSteamNetworkingMessages(), ref identityRemote);
		}

		public static bool CloseSessionWithUser(ref global::Steamworks.SteamNetworkingIdentity identityRemote)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamNetworkingMessages_CloseSessionWithUser(global::Steamworks.CSteamAPIContext.GetSteamNetworkingMessages(), ref identityRemote);
		}

		public static bool CloseChannelWithUser(ref global::Steamworks.SteamNetworkingIdentity identityRemote, int nLocalChannel)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamNetworkingMessages_CloseChannelWithUser(global::Steamworks.CSteamAPIContext.GetSteamNetworkingMessages(), ref identityRemote, nLocalChannel);
		}

		public static global::Steamworks.ESteamNetworkingConnectionState GetSessionConnectionInfo(ref global::Steamworks.SteamNetworkingIdentity identityRemote, out global::Steamworks.SteamNetConnectionInfo_t pConnectionInfo, out global::Steamworks.SteamNetConnectionRealTimeStatus_t pQuickStatus)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamNetworkingMessages_GetSessionConnectionInfo(global::Steamworks.CSteamAPIContext.GetSteamNetworkingMessages(), ref identityRemote, out pConnectionInfo, out pQuickStatus);
		}
	}
}
