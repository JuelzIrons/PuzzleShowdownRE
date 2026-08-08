namespace Steamworks
{
	internal static class CSteamAPIContext
	{
		private static global::System.IntPtr m_pSteamClient;

		private static global::System.IntPtr m_pSteamUser;

		private static global::System.IntPtr m_pSteamFriends;

		private static global::System.IntPtr m_pSteamUtils;

		private static global::System.IntPtr m_pSteamMatchmaking;

		private static global::System.IntPtr m_pSteamUserStats;

		private static global::System.IntPtr m_pSteamApps;

		private static global::System.IntPtr m_pSteamMatchmakingServers;

		private static global::System.IntPtr m_pSteamNetworking;

		private static global::System.IntPtr m_pSteamRemoteStorage;

		private static global::System.IntPtr m_pSteamScreenshots;

		private static global::System.IntPtr m_pSteamHTTP;

		private static global::System.IntPtr m_pController;

		private static global::System.IntPtr m_pSteamUGC;

		private static global::System.IntPtr m_pSteamMusic;

		private static global::System.IntPtr m_pSteamHTMLSurface;

		private static global::System.IntPtr m_pSteamInventory;

		private static global::System.IntPtr m_pSteamVideo;

		private static global::System.IntPtr m_pSteamParentalSettings;

		private static global::System.IntPtr m_pSteamInput;

		private static global::System.IntPtr m_pSteamParties;

		private static global::System.IntPtr m_pSteamRemotePlay;

		private static global::System.IntPtr m_pSteamNetworkingUtils;

		private static global::System.IntPtr m_pSteamNetworkingSockets;

		private static global::System.IntPtr m_pSteamNetworkingMessages;

		private static global::System.IntPtr m_pSteamTimeline;

		internal static void Clear()
		{
			m_pSteamClient = global::System.IntPtr.Zero;
			m_pSteamUser = global::System.IntPtr.Zero;
			m_pSteamFriends = global::System.IntPtr.Zero;
			m_pSteamUtils = global::System.IntPtr.Zero;
			m_pSteamMatchmaking = global::System.IntPtr.Zero;
			m_pSteamUserStats = global::System.IntPtr.Zero;
			m_pSteamApps = global::System.IntPtr.Zero;
			m_pSteamMatchmakingServers = global::System.IntPtr.Zero;
			m_pSteamNetworking = global::System.IntPtr.Zero;
			m_pSteamRemoteStorage = global::System.IntPtr.Zero;
			m_pSteamHTTP = global::System.IntPtr.Zero;
			m_pSteamScreenshots = global::System.IntPtr.Zero;
			m_pSteamMusic = global::System.IntPtr.Zero;
			m_pController = global::System.IntPtr.Zero;
			m_pSteamUGC = global::System.IntPtr.Zero;
			m_pSteamMusic = global::System.IntPtr.Zero;
			m_pSteamHTMLSurface = global::System.IntPtr.Zero;
			m_pSteamInventory = global::System.IntPtr.Zero;
			m_pSteamVideo = global::System.IntPtr.Zero;
			m_pSteamParentalSettings = global::System.IntPtr.Zero;
			m_pSteamInput = global::System.IntPtr.Zero;
			m_pSteamParties = global::System.IntPtr.Zero;
			m_pSteamRemotePlay = global::System.IntPtr.Zero;
			m_pSteamNetworkingUtils = global::System.IntPtr.Zero;
			m_pSteamNetworkingSockets = global::System.IntPtr.Zero;
			m_pSteamNetworkingMessages = global::System.IntPtr.Zero;
			m_pSteamTimeline = global::System.IntPtr.Zero;
		}

		internal static bool Init()
		{
			global::Steamworks.HSteamUser hSteamUser = global::Steamworks.SteamAPI.GetHSteamUser();
			global::Steamworks.HSteamPipe hSteamPipe = global::Steamworks.SteamAPI.GetHSteamPipe();
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
			m_pSteamUser = global::Steamworks.SteamClient.GetISteamUser(hSteamUser, hSteamPipe, "SteamUser023");
			if (m_pSteamUser == global::System.IntPtr.Zero)
			{
				return false;
			}
			m_pSteamFriends = global::Steamworks.SteamClient.GetISteamFriends(hSteamUser, hSteamPipe, "SteamFriends018");
			if (m_pSteamFriends == global::System.IntPtr.Zero)
			{
				return false;
			}
			m_pSteamUtils = global::Steamworks.SteamClient.GetISteamUtils(hSteamPipe, "SteamUtils010");
			if (m_pSteamUtils == global::System.IntPtr.Zero)
			{
				return false;
			}
			m_pSteamMatchmaking = global::Steamworks.SteamClient.GetISteamMatchmaking(hSteamUser, hSteamPipe, "SteamMatchMaking009");
			if (m_pSteamMatchmaking == global::System.IntPtr.Zero)
			{
				return false;
			}
			m_pSteamMatchmakingServers = global::Steamworks.SteamClient.GetISteamMatchmakingServers(hSteamUser, hSteamPipe, "SteamMatchMakingServers002");
			if (m_pSteamMatchmakingServers == global::System.IntPtr.Zero)
			{
				return false;
			}
			m_pSteamUserStats = global::Steamworks.SteamClient.GetISteamUserStats(hSteamUser, hSteamPipe, "STEAMUSERSTATS_INTERFACE_VERSION013");
			if (m_pSteamUserStats == global::System.IntPtr.Zero)
			{
				return false;
			}
			m_pSteamApps = global::Steamworks.SteamClient.GetISteamApps(hSteamUser, hSteamPipe, "STEAMAPPS_INTERFACE_VERSION008");
			if (m_pSteamApps == global::System.IntPtr.Zero)
			{
				return false;
			}
			m_pSteamNetworking = global::Steamworks.SteamClient.GetISteamNetworking(hSteamUser, hSteamPipe, "SteamNetworking006");
			if (m_pSteamNetworking == global::System.IntPtr.Zero)
			{
				return false;
			}
			m_pSteamRemoteStorage = global::Steamworks.SteamClient.GetISteamRemoteStorage(hSteamUser, hSteamPipe, "STEAMREMOTESTORAGE_INTERFACE_VERSION016");
			if (m_pSteamRemoteStorage == global::System.IntPtr.Zero)
			{
				return false;
			}
			m_pSteamScreenshots = global::Steamworks.SteamClient.GetISteamScreenshots(hSteamUser, hSteamPipe, "STEAMSCREENSHOTS_INTERFACE_VERSION003");
			if (m_pSteamScreenshots == global::System.IntPtr.Zero)
			{
				return false;
			}
			m_pSteamHTTP = global::Steamworks.SteamClient.GetISteamHTTP(hSteamUser, hSteamPipe, "STEAMHTTP_INTERFACE_VERSION003");
			if (m_pSteamHTTP == global::System.IntPtr.Zero)
			{
				return false;
			}
			m_pSteamUGC = global::Steamworks.SteamClient.GetISteamUGC(hSteamUser, hSteamPipe, "STEAMUGC_INTERFACE_VERSION021");
			if (m_pSteamUGC == global::System.IntPtr.Zero)
			{
				return false;
			}
			m_pSteamMusic = global::Steamworks.SteamClient.GetISteamMusic(hSteamUser, hSteamPipe, "STEAMMUSIC_INTERFACE_VERSION001");
			if (m_pSteamMusic == global::System.IntPtr.Zero)
			{
				return false;
			}
			m_pSteamHTMLSurface = global::Steamworks.SteamClient.GetISteamHTMLSurface(hSteamUser, hSteamPipe, "STEAMHTMLSURFACE_INTERFACE_VERSION_005");
			if (m_pSteamHTMLSurface == global::System.IntPtr.Zero)
			{
				return false;
			}
			m_pSteamInventory = global::Steamworks.SteamClient.GetISteamInventory(hSteamUser, hSteamPipe, "STEAMINVENTORY_INTERFACE_V003");
			if (m_pSteamInventory == global::System.IntPtr.Zero)
			{
				return false;
			}
			m_pSteamVideo = global::Steamworks.SteamClient.GetISteamVideo(hSteamUser, hSteamPipe, "STEAMVIDEO_INTERFACE_V007");
			if (m_pSteamVideo == global::System.IntPtr.Zero)
			{
				return false;
			}
			m_pSteamParentalSettings = global::Steamworks.SteamClient.GetISteamParentalSettings(hSteamUser, hSteamPipe, "STEAMPARENTALSETTINGS_INTERFACE_VERSION001");
			if (m_pSteamParentalSettings == global::System.IntPtr.Zero)
			{
				return false;
			}
			m_pSteamInput = global::Steamworks.SteamClient.GetISteamInput(hSteamUser, hSteamPipe, "SteamInput006");
			if (m_pSteamInput == global::System.IntPtr.Zero)
			{
				return false;
			}
			m_pSteamParties = global::Steamworks.SteamClient.GetISteamParties(hSteamUser, hSteamPipe, "SteamParties002");
			if (m_pSteamParties == global::System.IntPtr.Zero)
			{
				return false;
			}
			m_pSteamRemotePlay = global::Steamworks.SteamClient.GetISteamRemotePlay(hSteamUser, hSteamPipe, "STEAMREMOTEPLAY_INTERFACE_VERSION003");
			if (m_pSteamRemotePlay == global::System.IntPtr.Zero)
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
				m_pSteamNetworkingSockets = global::Steamworks.NativeMethods.SteamInternal_FindOrCreateUserInterface(hSteamUser, pszVersion2);
			}
			if (m_pSteamNetworkingSockets == global::System.IntPtr.Zero)
			{
				return false;
			}
			using (global::Steamworks.InteropHelp.UTF8StringHandle pszVersion3 = new global::Steamworks.InteropHelp.UTF8StringHandle("SteamNetworkingMessages002"))
			{
				m_pSteamNetworkingMessages = global::Steamworks.NativeMethods.SteamInternal_FindOrCreateUserInterface(hSteamUser, pszVersion3);
			}
			if (m_pSteamNetworkingMessages == global::System.IntPtr.Zero)
			{
				return false;
			}
			using (global::Steamworks.InteropHelp.UTF8StringHandle pszVersion4 = new global::Steamworks.InteropHelp.UTF8StringHandle("STEAMTIMELINE_INTERFACE_V004"))
			{
				m_pSteamTimeline = global::Steamworks.NativeMethods.SteamInternal_FindOrCreateUserInterface(hSteamUser, pszVersion4);
			}
			if (m_pSteamTimeline == global::System.IntPtr.Zero)
			{
				return false;
			}
			return true;
		}

		internal static global::System.IntPtr GetSteamClient()
		{
			return m_pSteamClient;
		}

		internal static global::System.IntPtr GetSteamUser()
		{
			return m_pSteamUser;
		}

		internal static global::System.IntPtr GetSteamFriends()
		{
			return m_pSteamFriends;
		}

		internal static global::System.IntPtr GetSteamUtils()
		{
			return m_pSteamUtils;
		}

		internal static global::System.IntPtr GetSteamMatchmaking()
		{
			return m_pSteamMatchmaking;
		}

		internal static global::System.IntPtr GetSteamUserStats()
		{
			return m_pSteamUserStats;
		}

		internal static global::System.IntPtr GetSteamApps()
		{
			return m_pSteamApps;
		}

		internal static global::System.IntPtr GetSteamMatchmakingServers()
		{
			return m_pSteamMatchmakingServers;
		}

		internal static global::System.IntPtr GetSteamNetworking()
		{
			return m_pSteamNetworking;
		}

		internal static global::System.IntPtr GetSteamRemoteStorage()
		{
			return m_pSteamRemoteStorage;
		}

		internal static global::System.IntPtr GetSteamScreenshots()
		{
			return m_pSteamScreenshots;
		}

		internal static global::System.IntPtr GetSteamHTTP()
		{
			return m_pSteamHTTP;
		}

		internal static global::System.IntPtr GetSteamController()
		{
			return m_pController;
		}

		internal static global::System.IntPtr GetSteamUGC()
		{
			return m_pSteamUGC;
		}

		internal static global::System.IntPtr GetSteamMusic()
		{
			return m_pSteamMusic;
		}

		internal static global::System.IntPtr GetSteamHTMLSurface()
		{
			return m_pSteamHTMLSurface;
		}

		internal static global::System.IntPtr GetSteamInventory()
		{
			return m_pSteamInventory;
		}

		internal static global::System.IntPtr GetSteamVideo()
		{
			return m_pSteamVideo;
		}

		internal static global::System.IntPtr GetSteamParentalSettings()
		{
			return m_pSteamParentalSettings;
		}

		internal static global::System.IntPtr GetSteamInput()
		{
			return m_pSteamInput;
		}

		internal static global::System.IntPtr GetSteamParties()
		{
			return m_pSteamParties;
		}

		internal static global::System.IntPtr GetSteamRemotePlay()
		{
			return m_pSteamRemotePlay;
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

		internal static global::System.IntPtr GetSteamTimeline()
		{
			return m_pSteamTimeline;
		}
	}
}
