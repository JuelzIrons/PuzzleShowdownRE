namespace Unity.Services.Multiplayer
{
	internal class GameObjectsNetcodeNetworkHandler : global::Unity.Services.Multiplayer.INetworkHandler
	{
		private const string k_EnclosingType = "GameObjectsNetcodeNetworkHandler";

		private global::Unity.Services.Multiplayer.NetworkManagerSession m_CurrentSession;

		public async global::System.Threading.Tasks.Task StartAsync(global::Unity.Services.Multiplayer.NetworkConfiguration configuration)
		{
			global::Unity.Netcode.NetworkManager singleton = global::Unity.Netcode.NetworkManager.Singleton;
			if (singleton == null)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("Cannot start when Singleton is not set.", global::Unity.Services.Multiplayer.SessionError.NetworkManagerNotInitialized);
			}
			if (m_CurrentSession != null)
			{
				global::Unity.Services.Multiplayer.Logger.LogCallWarning("GameObjectsNetcodeNetworkHandler", "Session already started.", "StartAsync");
				return;
			}
			global::Unity.Services.Multiplayer.NetworkManagerSession newSession = new global::Unity.Services.Multiplayer.NetworkManagerSession(singleton, configuration.Role);
			switch (configuration.Type)
			{
			case global::Unity.Services.Multiplayer.NetworkType.Direct:
				SetupDirect(newSession, configuration);
				break;
			case global::Unity.Services.Multiplayer.NetworkType.Relay:
				SetupRelay(newSession, configuration);
				break;
			case global::Unity.Services.Multiplayer.NetworkType.DistributedAuthority:
				SetupDistributedAuthority(newSession, configuration);
				break;
			}
			await newSession.StartAsync();
			if (configuration.Type == global::Unity.Services.Multiplayer.NetworkType.Direct)
			{
				UpdateDirectConnectPortBinding(newSession, configuration);
			}
			m_CurrentSession = newSession;
		}

		public async global::System.Threading.Tasks.Task StopAsync()
		{
			if (m_CurrentSession == null)
			{
				global::Unity.Services.Multiplayer.Logger.LogCallWarning("GameObjectsNetcodeNetworkHandler", "Failed to stop session: session was never started.", "StopAsync");
				return;
			}
			using global::Unity.Services.Multiplayer.NetworkManagerSession stoppingSession = m_CurrentSession;
			m_CurrentSession = null;
			await stoppingSession.StopAsync();
		}

		private static void SetupDirect(global::Unity.Services.Multiplayer.NetworkManagerSession session, global::Unity.Services.Multiplayer.NetworkConfiguration configuration)
		{
			global::Unity.Netcode.Transports.UTP.UnityTransport unityTransport = session.GetUnityTransport();
			if (unityTransport == null)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("NetworkManager must have a UnityTransport component.", global::Unity.Services.Multiplayer.SessionError.TranportComponentMissing);
			}
			unityTransport.SetConnectionData(forceOverrideCommandLineArgs: true, configuration.DirectNetworkPublishAddress.ToFixedStringNoPort().ToString(), configuration.DirectNetworkPublishAddress.Port, configuration.DirectNetworkListenAddress.Address);
		}

		private static void UpdateDirectConnectPortBinding(global::Unity.Services.Multiplayer.NetworkManagerSession session, global::Unity.Services.Multiplayer.NetworkConfiguration configuration)
		{
			if (configuration.Role != global::Unity.Services.Multiplayer.NetworkRole.Client && configuration.DirectNetworkListenAddress.Port == 0)
			{
				configuration.UpdatePublishPort(session.GetUnityTransport().GetLocalEndpoint().Port);
			}
		}

		private static void SetupRelay(global::Unity.Services.Multiplayer.NetworkManagerSession session, global::Unity.Services.Multiplayer.NetworkConfiguration configuration)
		{
			if (configuration.Role == global::Unity.Services.Multiplayer.NetworkRole.Server)
			{
				session.SetNetworkRole(global::Unity.Services.Multiplayer.NetworkRole.Host);
			}
			global::Unity.Netcode.Transports.UTP.UnityTransport unityTransport = session.GetUnityTransport();
			if (unityTransport == null)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("NetworkManager must have a UnityTransport component.", global::Unity.Services.Multiplayer.SessionError.TranportComponentMissing);
			}
			unityTransport.SetRelayServerData(configuration.RelayServerData);
		}

		private static void SetupDistributedAuthority(global::Unity.Services.Multiplayer.NetworkManagerSession session, global::Unity.Services.Multiplayer.NetworkConfiguration configuration)
		{
			session.SetNetworkRole(global::Unity.Services.Multiplayer.NetworkRole.Client);
			global::Unity.Services.DistributedAuthority.DistributedAuthorityTransport distributedAuthorityTransport = session.GetDistributedAuthorityTransport();
			if (!distributedAuthorityTransport)
			{
				global::Unity.Netcode.NetworkManager networkManager = session.NetworkManager;
				distributedAuthorityTransport = networkManager.GetComponent<global::Unity.Services.DistributedAuthority.DistributedAuthorityTransport>();
				if (!distributedAuthorityTransport)
				{
					distributedAuthorityTransport = networkManager.gameObject.AddComponent<global::Unity.Services.DistributedAuthority.DistributedAuthorityTransport>();
				}
				session.SetTransport(distributedAuthorityTransport);
			}
			distributedAuthorityTransport.ConnectPayload = configuration.DistributedAuthorityConnectionPayload;
			distributedAuthorityTransport.SetRelayServerData(configuration.RelayServerData);
			session.ConfigureForDistributedAuthority();
		}
	}
}
