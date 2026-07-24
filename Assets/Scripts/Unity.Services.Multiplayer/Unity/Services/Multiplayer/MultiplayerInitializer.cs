namespace Unity.Services.Multiplayer
{
	internal class MultiplayerInitializer : global::Unity.Services.Core.Internal.IInitializablePackageV2, global::Unity.Services.Core.Internal.IInitializablePackage
	{
		private const string k_CloudEnvironmentKey = "com.unity.services.core.cloud-environment";

		private const string k_PackageName = "com.unity.services.multiplayer";

		private const string k_StagingEnvironment = "staging";

		[global::UnityEngine.RuntimeInitializeOnLoadMethod(global::UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Register()
		{
			new global::Unity.Services.Multiplayer.MultiplayerInitializer().Register(global::Unity.Services.Core.Internal.CorePackageRegistry.Instance);
		}

		public void Register(global::Unity.Services.Core.Internal.CorePackageRegistry registry)
		{
			registry.Register(this).DependsOn<global::Unity.Services.Authentication.Internal.IAccessToken>().DependsOn<global::Unity.Services.Core.Scheduler.Internal.IActionScheduler>()
				.DependsOn<global::Unity.Services.Core.Configuration.Internal.ICloudProjectId>()
				.DependsOn<global::Unity.Services.Authentication.Internal.IEnvironmentId>()
				.DependsOn<global::Unity.Services.Core.Device.Internal.IInstallationId>()
				.DependsOn<global::Unity.Services.Core.Telemetry.Internal.IMetricsFactory>()
				.DependsOn<global::Unity.Services.Authentication.Internal.IPlayerId>()
				.DependsOn<global::Unity.Services.Authentication.Internal.IPlayerNameComponent>()
				.DependsOn<global::Unity.Services.Core.Configuration.Internal.IProjectConfiguration>()
				.DependsOn<global::Unity.Services.Qos.Internal.IQosResults>()
				.DependsOn<global::Unity.Services.Qos.IQosServiceComponent>()
				.OptionallyDependsOn<global::Unity.Services.Wire.Internal.IWire>()
				.OptionallyDependsOn<global::Unity.Services.Vivox.Internal.IVivox>();
		}

		public global::System.Threading.Tasks.Task Initialize(global::Unity.Services.Core.Internal.CoreRegistry registry)
		{
			InitializeServices(registry, globalRegistry: true);
			return global::System.Threading.Tasks.Task.CompletedTask;
		}

		public global::System.Threading.Tasks.Task InitializeInstanceAsync(global::Unity.Services.Core.Internal.CoreRegistry registry)
		{
			InitializeServices(registry, globalRegistry: false);
			return global::System.Threading.Tasks.Task.CompletedTask;
		}

		private void InitializeServices(global::Unity.Services.Core.Internal.CoreRegistry registry, bool globalRegistry)
		{
			global::Unity.Services.Core.Scheduler.Internal.IActionScheduler serviceComponent = registry.GetServiceComponent<global::Unity.Services.Core.Scheduler.Internal.IActionScheduler>();
			global::Unity.Services.Core.Configuration.Internal.ICloudProjectId serviceComponent2 = registry.GetServiceComponent<global::Unity.Services.Core.Configuration.Internal.ICloudProjectId>();
			global::Unity.Services.Authentication.Internal.IPlayerId serviceComponent3 = registry.GetServiceComponent<global::Unity.Services.Authentication.Internal.IPlayerId>();
			global::Unity.Services.Authentication.Internal.IPlayerNameComponent serviceComponent4 = registry.GetServiceComponent<global::Unity.Services.Authentication.Internal.IPlayerNameComponent>();
			global::Unity.Services.Authentication.Internal.IAccessToken serviceComponent5 = registry.GetServiceComponent<global::Unity.Services.Authentication.Internal.IAccessToken>();
			global::Unity.Services.Authentication.Internal.IAccessTokenObserver serviceComponent6 = registry.GetServiceComponent<global::Unity.Services.Authentication.Internal.IAccessTokenObserver>();
			global::Unity.Services.Authentication.Internal.IEnvironmentId serviceComponent7 = registry.GetServiceComponent<global::Unity.Services.Authentication.Internal.IEnvironmentId>();
			global::Unity.Services.Core.Device.Internal.IInstallationId serviceComponent8 = registry.GetServiceComponent<global::Unity.Services.Core.Device.Internal.IInstallationId>();
			global::Unity.Services.Core.Telemetry.Internal.IMetricsFactory serviceComponent9 = registry.GetServiceComponent<global::Unity.Services.Core.Telemetry.Internal.IMetricsFactory>();
			global::Unity.Services.Core.Configuration.Internal.IProjectConfiguration serviceComponent10 = registry.GetServiceComponent<global::Unity.Services.Core.Configuration.Internal.IProjectConfiguration>();
			global::Unity.Services.Qos.Internal.IQosResults serviceComponent11 = registry.GetServiceComponent<global::Unity.Services.Qos.Internal.IQosResults>();
			global::Unity.Services.Qos.IQosServiceComponent serviceComponent12 = registry.GetServiceComponent<global::Unity.Services.Qos.IQosServiceComponent>();
			registry.TryGetServiceComponent<global::Unity.Services.Authentication.Server.Internal.IServerAccessToken>(out var component);
			registry.TryGetServiceComponent<global::Unity.Services.Wire.Internal.IWire>(out var component2);
			registry.TryGetServiceComponent<global::Unity.Services.Vivox.Internal.IVivox>(out var component3);
			string cloudEnvironment = serviceComponent10.GetString("com.unity.services.core.cloud-environment");
			global::Unity.Services.Lobbies.Internal.WrappedLobbyService wrappedLobbyService = InitializeLobbyService(serviceComponent5, serviceComponent7, serviceComponent9, serviceComponent3, null, component3, component2, cloudEnvironment);
			global::Unity.Services.Multiplayer.LobbyBuilder lobbyBuilder = new global::Unity.Services.Multiplayer.LobbyBuilder(serviceComponent, wrappedLobbyService, serviceComponent3, null, serviceComponent5, serviceComponent6, usePolling: false);
			registry.RegisterService((global::Unity.Services.Lobbies.ILobbyService)wrappedLobbyService);
			global::Unity.Services.Matchmaker.WrappedMatchmakerService wrappedMatchmakerService = InitializeMatchmakerService(serviceComponent5, component, serviceComponent2, serviceComponent7, serviceComponent8, serviceComponent10, cloudEnvironment);
			registry.RegisterService((global::Unity.Services.Matchmaker.IMatchmakerService)wrappedMatchmakerService);
			global::Unity.Services.Relay.WrappedRelayService wrappedRelayService = InitializeRelayService(serviceComponent5, serviceComponent10, serviceComponent11);
			global::Unity.Services.Multiplayer.RelayBuilder relayBuilder = new global::Unity.Services.Multiplayer.RelayBuilder(wrappedRelayService);
			registry.RegisterService((global::Unity.Services.Relay.IRelayService)wrappedRelayService);
			global::Unity.Services.DistributedAuthority.WrappedDistributedAuthorityService wrappedDistributedAuthorityService = InitializeDaService(serviceComponent5, serviceComponent10, wrappedLobbyService, wrappedRelayService, serviceComponent11, serviceComponent);
			global::Unity.Services.Multiplayer.DaBuilder daBuilder = new global::Unity.Services.Multiplayer.DaBuilder(wrappedRelayService, wrappedDistributedAuthorityService, serviceComponent3);
			registry.RegisterService((global::Unity.Services.DistributedAuthority.IDistributedAuthorityService)wrappedDistributedAuthorityService);
			global::Unity.Services.Qos.IQosService service = serviceComponent12.Service;
			global::Unity.Services.Multiplayer.ModuleRegistry moduleRegistry = new global::Unity.Services.Multiplayer.ModuleRegistry();
			global::Unity.Services.Multiplayer.MatchmakerProvider moduleProvider = new global::Unity.Services.Multiplayer.MatchmakerProvider(serviceComponent, wrappedMatchmakerService);
			global::Unity.Services.Multiplayer.SessionQuerier sessionQuerier = new global::Unity.Services.Multiplayer.SessionQuerier(serviceComponent, wrappedLobbyService);
			global::Unity.Services.Multiplayer.SessionManager sessionManager = new global::Unity.Services.Multiplayer.SessionManager(serviceComponent, moduleRegistry, lobbyBuilder, serviceComponent3, serviceComponent4, serviceComponent6);
			global::Unity.Services.Multiplayer.MatchmakerManager matchmakerManager = new global::Unity.Services.Multiplayer.MatchmakerManager(sessionManager, serviceComponent, service, wrappedMatchmakerService, serviceComponent3, serviceComponent5, serviceComponent6);
			global::Unity.Services.Multiplayer.WrappedMultiplayerService wrappedMultiplayerService = new global::Unity.Services.Multiplayer.WrappedMultiplayerService(sessionQuerier, sessionManager, matchmakerManager, moduleRegistry);
			global::Unity.Services.Multiplayer.NetworkProvider moduleProvider2 = new global::Unity.Services.Multiplayer.NetworkProvider(new global::Unity.Services.Multiplayer.NetworkBuilder(serviceComponent), daBuilder, relayBuilder);
			global::Unity.Services.Multiplayer.PlayerNameModuleProvider moduleProvider3 = new global::Unity.Services.Multiplayer.PlayerNameModuleProvider(serviceComponent4);
			moduleRegistry.RegisterModuleProvider(moduleProvider2);
			moduleRegistry.RegisterModuleProvider(moduleProvider);
			moduleRegistry.RegisterModuleProvider(moduleProvider3);
			registry.RegisterService((global::Unity.Services.Multiplayer.IMultiplayerService)wrappedMultiplayerService);
			if (globalRegistry)
			{
				global::Unity.Services.Lobbies.LobbyService.Instance = wrappedLobbyService;
				global::Unity.Services.Matchmaker.MatchmakerService.Instance = wrappedMatchmakerService;
				global::Unity.Services.Relay.RelayService.Instance = wrappedRelayService;
				global::Unity.Services.Multiplayer.MultiplayerService.Instance = wrappedMultiplayerService;
				global::Unity.Services.DistributedAuthority.DistributedAuthorityService.Instance = wrappedDistributedAuthorityService;
			}
		}

		internal static global::Unity.Services.DistributedAuthority.WrappedDistributedAuthorityService InitializeDaService(global::Unity.Services.Authentication.Internal.IAccessToken accessToken, global::Unity.Services.Core.Configuration.Internal.IProjectConfiguration projectConfiguration, global::Unity.Services.Lobbies.ILobbyService lobbyService, global::Unity.Services.Relay.IRelayService relayService, global::Unity.Services.Qos.Internal.IQosResults qosResults, global::Unity.Services.Core.Scheduler.Internal.IActionScheduler actionScheduler)
		{
			global::Unity.Services.DistributedAuthority.Http.HttpClient httpClient = new global::Unity.Services.DistributedAuthority.Http.HttpClient();
			global::Unity.Services.DistributedAuthority.Configuration configuration = new global::Unity.Services.DistributedAuthority.Configuration(GetDaHost(projectConfiguration), 100, 4, null);
			return new global::Unity.Services.DistributedAuthority.WrappedDistributedAuthorityService(new global::Unity.Services.DistributedAuthority.Apis.DistributedAuthority.DistributedAuthorityApiClient(httpClient, accessToken, configuration), new global::Unity.Services.DistributedAuthority.ErrorMitigation.RetryPolicyProvider(actionScheduler), new global::Unity.Services.DistributedAuthority.Internal.DefaultClock(), configuration, new global::Unity.Services.DistributedAuthority.InternalDaLobbyService(new global::System.Lazy<global::Unity.Services.Lobbies.ILobbyService>(() => lobbyService, global::System.Threading.LazyThreadSafetyMode.PublicationOnly)), relayService, qosResults);
		}

		private static string GetDaHost(global::Unity.Services.Core.Configuration.Internal.IProjectConfiguration projectConfiguration)
		{
			if (projectConfiguration?.GetString("com.unity.services.core.cloud-environment") == "staging")
			{
				return "https://cmb-stg.services.api.unity.com";
			}
			return "https://cmb.services.api.unity.com";
		}

		internal static global::Unity.Services.Lobbies.Internal.WrappedLobbyService InitializeLobbyService(global::Unity.Services.Authentication.Internal.IAccessToken accessToken, global::Unity.Services.Authentication.Internal.IEnvironmentId environmentId, global::Unity.Services.Core.Telemetry.Internal.IMetricsFactory metricsFactory, global::Unity.Services.Authentication.Internal.IPlayerId playerId, global::Unity.Services.Multiplayer.IServiceID serviceID, global::Unity.Services.Vivox.Internal.IVivox vivox, global::Unity.Services.Wire.Internal.IWire wire, string cloudEnvironment)
		{
			global::Unity.Services.Lobbies.Http.HttpClient httpClient = new global::Unity.Services.Lobbies.Http.HttpClient();
			if (wire == null)
			{
				global::Unity.Services.Multiplayer.Logger.LogWarning("The IWire component is not available. LobbyEvents functionality unavailable.");
			}
			global::Unity.Services.Core.Telemetry.Internal.IMetrics metrics = metricsFactory.Create("com.unity.services.multiplayer");
			return new global::Unity.Services.Lobbies.Internal.WrappedLobbyService(new global::Unity.Services.Lobbies.InternalLobbyService(httpClient, accessToken, wire, metrics, cloudEnvironment), playerId, serviceID);
		}

		internal static global::Unity.Services.Matchmaker.WrappedMatchmakerService InitializeMatchmakerService(global::Unity.Services.Authentication.Internal.IAccessToken accessToken, global::Unity.Services.Authentication.Server.Internal.IServerAccessToken serverAccessToken, global::Unity.Services.Core.Configuration.Internal.ICloudProjectId cloudProjectId, global::Unity.Services.Authentication.Internal.IEnvironmentId environmentId, global::Unity.Services.Core.Device.Internal.IInstallationId installationId, global::Unity.Services.Core.Configuration.Internal.IProjectConfiguration projectConfiguration, string cloudEnvironment)
		{
			global::Unity.Services.Matchmaker.InternalMatchmakerServiceSdk matchmakerService = new global::Unity.Services.Matchmaker.InternalMatchmakerServiceSdk(new global::Unity.Services.Matchmaker.Http.HttpClient(), cloudEnvironment, accessToken, serverAccessToken);
			return new global::Unity.Services.Matchmaker.WrappedMatchmakerService(cloudProjectId, projectConfiguration, installationId, environmentId, matchmakerService);
		}

		internal static global::Unity.Services.Relay.WrappedRelayService InitializeRelayService(global::Unity.Services.Authentication.Internal.IAccessToken accessToken, global::Unity.Services.Core.Configuration.Internal.IProjectConfiguration projectConfiguration, global::Unity.Services.Qos.Internal.IQosResults qosResults)
		{
			return new global::Unity.Services.Relay.WrappedRelayService(new global::Unity.Services.Relay.InternalRelayService(new global::Unity.Services.Relay.Http.HttpClient(), projectConfiguration, accessToken, qosResults));
		}
	}
}
