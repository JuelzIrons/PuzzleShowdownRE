namespace Steamworks
{
	internal static class CSteamGameServerAPIContext
	{
		private static global::System.IntPtr m_pSteamClient;

		private static global::System.IntPtr m_pSteamGameServer;

		private static global::System.IntPtr m_pSteamUtils;

		private static global::System.IntPtr m_pSteamNetworking;

		private static global::System.IntPtr m_pSteamGameServerStats;

		private static global::System.IntPtr m_pSteamHTTP;

		private static global::System.IntPtr m_pSteamInventory;

		private static global::System.IntPtr m_pSteamUGC;

		private static global::System.IntPtr m_pSteamNetworkingUtils;

		private static global::System.IntPtr m_pSteamNetworkingSockets;

		private static global::System.IntPtr m_pSteamNetworkingMessages;

		internal static void Clear()
		{
			m_pSteamClient = global::System.IntPtr.Zero;
			m_pSteamGameServer = global::System.IntPtr.Zero;
			m_pSteamUtils = global::System.IntPtr.Zero;
			m_pSteamNetworking = global::System.IntPtr.Zero;
			m_pSteamGameServerStats = global::System.IntPtr.Zero;
			m_pSteamHTTP = global::System.IntPtr.Zero;
			m_pSteamInventory = global::System.IntPtr.Zero;
			m_pSteamUGC = global::System.IntPtr.Zero;
			m_pSteamNetworkingUtils = global::System.IntPtr.Zero;
			m_pSteamNetworkingSockets = global::System.IntPtr.Zero;
			m_pSteamNetworkingMessages = global::System.IntPtr.Zero;
		}

		internal static bool Init()
		{
			global::Steamworks.HSteamUser hSteamUser = global::Steamworks.GameServer.GetHSteamUser();
			global::Steamworks.HSteamPipe hSteamPipe = global::Steamworks.GameServer.GetHSteamPipe();
			if (hSteamPipe == (global::Steamworks.HSteamPipe)0)
			{
				return false;
			}
			using (global::Steamworks.InteropHelp.UTF8StringHandle ver = new global::Steamworks.InteropHelp.UTF8StringHandle("SteamClient023"))
			{
				m_pSteamClient = global::Steamworks.NativeMethods.SteamInternal_CreateInterface(ver);
			}
			if (m_pSteamClient == global::System.IntPtr.Zero)
			{
				return false;
			}
			m_pSteamGameServer = global::Steamworks.SteamGameServerClient.GetISteamGameServer(hSteamUser, hSteamPipe, "SteamGameServer015");
			if (m_pSteamGameServer == global::System.IntPtr.Zero)
			{
				return false;
			}
			m_pSteamUtils = global::Steamworks.SteamGameServerClient.GetISteamUtils(hSteamPipe, "SteamUtils010");
			if (m_pSteamUtils == global::System.IntPtr.Zero)
			{
				return false;
			}
			m_pSteamNetworking = global::Steamworks.SteamGameServerClient.GetISteamNetworking(hSteamUser, hSteamPipe, "SteamNetworking006");
			if (m_pSteamNetworking == global::System.IntPtr.Zero)
			{
				return false;
			}
			m_pSteamGameServerStats = global::Steamworks.SteamGameServerClient.GetISteamGameServerStats(hSteamUser, hSteamPipe, "SteamGameServerStats001");
			if (m_pSteamGameServerStats == global::System.IntPtr.Zero)
			{
				return false;
			}
			m_pSteamHTTP = global::Steamworks.SteamGameServerClient.GetISteamHTTP(hSteamUser, hSteamPipe, "STEAMHTTP_INTERFACE_VERSION003");
			if (m_pSteamHTTP == global::System.IntPtr.Zero)
			{
				return false;
			}
			m_pSteamInventory = global::Steamworks.SteamGameServerClient.GetISteamInventory(hSteamUser, hSteamPipe, "STEAMINVENTORY_INTERFACE_V003");
			if (m_pSteamInventory == global::System.IntPtr.Zero)
			{
				return false;
			}
			m_pSteamUGC = global::Steamworks.SteamGameServerClient.GetISteamUGC(hSteamUser, hSteamPipe, "STEAMUGC_INTERFACE_VERSION021");
			if (m_pSteamUGC == global::System.IntPtr.Zero)
			{
				return false;
			}
			using (global::Steamworks.InteropHelp.UTF8StringHandle pszVersion = new global::Steamworks.InteropHelp.UTF8StringHandle("SteamNetworkingUtils004"))
			{
				m_pSteamNetworkingUtils = ((global::Steamworks.NativeMethods.SteamInternal_FindOrCreateUserInterface(hSteamUser, pszVersion) != global::System.IntPtr.Zero) ? global::Steamworks.NativeMethods.SteamInternal_FindOrCreateUserInterface(hSteamUser, pszVersion) : global::Steamworks.NativeMethods.SteamInternal_FindOrCreateGameServerInterface(hSteamUser, pszVersion));
			}
			if (m_pSteamNetworkingUtils == global::System.IntPtr.Zero)
			{
				return false;
			}
			using (global::Steamworks.InteropHelp.UTF8StringHandle pszVersion2 = new global::Steamworks.InteropHelp.UTF8StringHandle("SteamNetworkingSockets012"))
			{
				m_pSteamNetworkingSockets = global::Steamworks.NativeMethods.SteamInternal_FindOrCreateGameServerInterface(hSteamUser, pszVersion2);
			}
			if (m_pSteamNetworkingSockets == global::System.IntPtr.Zero)
			{
				return false;
			}
			using (global::Steamworks.InteropHelp.UTF8StringHandle pszVersion3 = new global::Steamworks.InteropHelp.UTF8StringHandle("SteamNetworkingMessages002"))
			{
				m_pSteamNetworkingMessages = global::Steamworks.NativeMethods.SteamInternal_FindOrCreateGameServerInterface(hSteamUser, pszVersion3);
			}
			if (m_pSteamNetworkingMessages == global::System.IntPtr.Zero)
			{
				return false;
			}
			return true;
		}

		internal static global::System.IntPtr GetSteamClient()
		{
			return m_pSteamClient;
		}

		internal static global::System.IntPtr GetSteamGameServer()
		{
			return m_pSteamGameServer;
		}

		internal static global::System.IntPtr GetSteamUtils()
		{
			return m_pSteamUtils;
		}

		internal static global::System.IntPtr GetSteamNetworking()
		{
			return m_pSteamNetworking;
		}

		internal static global::System.IntPtr GetSteamGameServerStats()
		{
			return m_pSteamGameServerStats;
		}

		internal static global::System.IntPtr GetSteamHTTP()
		{
			return m_pSteamHTTP;
		}

		internal static global::System.IntPtr GetSteamInventory()
		{
			return m_pSteamInventory;
		}

		internal static global::System.IntPtr GetSteamUGC()
		{
			return m_pSteamUGC;
		}

		internal static global::System.IntPtr GetSteamNetworkingUtils()
		{
			return m_pSteamNetworkingUtils;
		}

		internal static global::System.IntPtr GetSteamNetworkingSockets()
		{
			return m_pSteamNetworkingSockets;
		}

		internal static global::System.IntPtr GetSteamNetworkingMessages()
		{
			return m_pSteamNetworkingMessages;
		}
	}
}
