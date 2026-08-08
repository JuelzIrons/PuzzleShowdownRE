namespace Steamworks
{
	public static class SteamGameServerNetworkingMessages
	{
		public static global::Steamworks.EResult SendMessageToUser(ref global::Steamworks.SteamNetworkingIdentity identityRemote, global::System.IntPtr pubData, uint cubData, int nSendFlags, int nRemoteChannel)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworkingMessages_SendMessageToUser(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworkingMessages(), ref identityRemote, pubData, cubData, nSendFlags, nRemoteChannel);
		}

		public static int ReceiveMessagesOnChannel(int nLocalChannel, global::System.IntPtr[] ppOutMessages, int nMaxMessages)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			if (ppOutMessages != null && ppOutMessages.Length != nMaxMessages)
			{
				throw new global::System.ArgumentException("ppOutMessages must be the same size as nMaxMessages!");
			}
			return global::Steamworks.NativeMethods.ISteamNetworkingMessages_ReceiveMessagesOnChannel(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworkingMessages(), nLocalChannel, ppOutMessages, nMaxMessages);
		}

		public static bool AcceptSessionWithUser(ref global::Steamworks.SteamNetworkingIdentity identityRemote)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworkingMessages_AcceptSessionWithUser(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworkingMessages(), ref identityRemote);
		}

		public static bool CloseSessionWithUser(ref global::Steamworks.SteamNetworkingIdentity identityRemote)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworkingMessages_CloseSessionWithUser(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworkingMessages(), ref identityRemote);
		}

		public static bool CloseChannelWithUser(ref global::Steamworks.SteamNetworkingIdentity identityRemote, int nLocalChannel)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworkingMessages_CloseChannelWithUser(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworkingMessages(), ref identityRemote, nLocalChannel);
		}

		public static global::Steamworks.ESteamNetworkingConnectionState GetSessionConnectionInfo(ref global::Steamworks.SteamNetworkingIdentity identityRemote, out global::Steamworks.SteamNetConnectionInfo_t pConnectionInfo, out global::Steamworks.SteamNetConnectionRealTimeStatus_t pQuickStatus)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamNetworkingMessages_GetSessionConnectionInfo(global::Steamworks.CSteamGameServerAPIContext.GetSteamNetworkingMessages(), ref identityRemote, out pConnectionInfo, out pQuickStatus);
		}
	}
}
