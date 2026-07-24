namespace Unity.Services.Authentication
{
	internal class AuthenticationPackageInitializer : global::Unity.Services.Core.Internal.IInitializablePackageV2, global::Unity.Services.Core.Internal.IInitializablePackage
	{
		private const string k_CloudEnvironmentKey = "com.unity.services.core.cloud-environment";

		private const string k_StagingEnvironment = "staging";

		private const string k_DefaultProfile = "default";

		private const string k_EditorModeArg = "-editor-mode";

		private const string k_NameArg = "-name";

		[global::UnityEngine.RuntimeInitializeOnLoadMethod(global::UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void InitializeOnLoad()
		{
			new global::Unity.Services.Authentication.AuthenticationPackageInitializer().Register(global::Unity.Services.Core.Internal.CorePackageRegistry.Instance);
		}

		public void Register(global::Unity.Services.Core.Internal.CorePackageRegistry registry)
		{
			registry.Register(this).DependsOn<global::Unity.Services.Core.Environments.Internal.IEnvironments>().DependsOn<global::Unity.Services.Core.Scheduler.Internal.IActionScheduler>()
				.DependsOn<global::Unity.Services.Core.Configuration.Internal.ICloudProjectId>()
				.DependsOn<global::Unity.Services.Core.Configuration.Internal.IProjectConfiguration>()
				.DependsOn<global::Unity.Services.Core.Telemetry.Internal.IMetricsFactory>()
				.ProvidesComponent<global::Unity.Services.Authentication.Internal.IPlayerId>()
				.ProvidesComponent<global::Unity.Services.Authentication.Internal.IPlayerNameComponent>()
				.ProvidesComponent<global::Unity.Services.Authentication.Internal.IAccessToken>()
				.ProvidesComponent<global::Unity.Services.Authentication.Internal.IAccessTokenObserver>()
				.ProvidesComponent<global::Unity.Services.Authentication.Internal.IEnvironmentId>();
		}

		public global::System.Threading.Tasks.Task Initialize(global::Unity.Services.Core.Internal.CoreRegistry registry)
		{
			global::Unity.Services.Authentication.AuthenticationService.Instance = InitializeService(registry);
			return global::System.Threading.Tasks.Task.CompletedTask;
		}

		public global::System.Threading.Tasks.Task InitializeInstanceAsync(global::Unity.Services.Core.Internal.CoreRegistry registry)
		{
			InitializeService(registry);
			return global::System.Threading.Tasks.Task.CompletedTask;
		}

		private global::Unity.Services.Authentication.AuthenticationServiceInternal InitializeService(global::Unity.Services.Core.Internal.CoreRegistry registry)
		{
			global::Unity.Services.Authentication.AuthenticationSettings settings = new global::Unity.Services.Authentication.AuthenticationSettings();
			global::Unity.Services.Core.Scheduler.Internal.IActionScheduler serviceComponent = registry.GetServiceComponent<global::Unity.Services.Core.Scheduler.Internal.IActionScheduler>();
			global::Unity.Services.Core.Environments.Internal.IEnvironments serviceComponent2 = registry.GetServiceComponent<global::Unity.Services.Core.Environments.Internal.IEnvironments>();
			global::Unity.Services.Core.Configuration.Internal.ICloudProjectId serviceComponent3 = registry.GetServiceComponent<global::Unity.Services.Core.Configuration.Internal.ICloudProjectId>();
			global::Unity.Services.Core.Configuration.Internal.IProjectConfiguration serviceComponent4 = registry.GetServiceComponent<global::Unity.Services.Core.Configuration.Internal.IProjectConfiguration>();
			global::Unity.Services.Authentication.ProfileComponent profile = new global::Unity.Services.Authentication.ProfileComponent(GetProfile(serviceComponent4));
			global::Unity.Services.Authentication.AuthenticationMetrics metrics = new global::Unity.Services.Authentication.AuthenticationMetrics(registry.GetServiceComponent<global::Unity.Services.Core.Telemetry.Internal.IMetricsFactory>());
			global::Unity.Services.Authentication.JwtDecoder jwtDecoder = new global::Unity.Services.Authentication.JwtDecoder();
			global::Unity.Services.Authentication.AuthenticationCache cache = new global::Unity.Services.Authentication.AuthenticationCache(serviceComponent3, profile);
			global::Unity.Services.Authentication.AccessTokenComponent accessToken = new global::Unity.Services.Authentication.AccessTokenComponent();
			global::Unity.Services.Authentication.EnvironmentIdComponent environmentId = new global::Unity.Services.Authentication.EnvironmentIdComponent();
			global::Unity.Services.Authentication.PlayerIdComponent playerId = new global::Unity.Services.Authentication.PlayerIdComponent(cache);
			global::Unity.Services.Authentication.PlayerNameComponent playerName = new global::Unity.Services.Authentication.PlayerNameComponent(cache);
			global::Unity.Services.Authentication.SessionTokenComponent sessionToken = new global::Unity.Services.Authentication.SessionTokenComponent(cache);
			global::Unity.Services.Authentication.NetworkConfiguration configuration = new global::Unity.Services.Authentication.NetworkConfiguration();
			global::Unity.Services.Authentication.NetworkHandler networkHandler = new global::Unity.Services.Authentication.NetworkHandler(configuration);
			string playerAuthHost = GetPlayerAuthHost(serviceComponent4);
			global::Unity.Services.Authentication.Generated.PlayerNamesApi playerNamesApi = new global::Unity.Services.Authentication.Generated.PlayerNamesApi(new global::Unity.Services.Authentication.AuthenticationApiClient(configuration), new global::Unity.Services.Authentication.Shared.ApiConfiguration
			{
				BasePath = GetPlayerNamesHost(serviceComponent4)
			});
			global::Unity.Services.Authentication.AuthenticationNetworkClient networkClient = new global::Unity.Services.Authentication.AuthenticationNetworkClient(playerAuthHost, serviceComponent3, serviceComponent2, networkHandler, accessToken);
			global::Unity.Services.Authentication.AuthenticationServiceInternal authenticationServiceInternal = new global::Unity.Services.Authentication.AuthenticationServiceInternal(settings, networkClient, playerNamesApi, profile, jwtDecoder, cache, serviceComponent, metrics, accessToken, environmentId, playerId, playerName, sessionToken, serviceComponent2);
			registry.RegisterService((global::Unity.Services.Authentication.IAuthenticationService)authenticationServiceInternal);
			registry.RegisterServiceComponent((global::Unity.Services.Authentication.Internal.IAccessToken)authenticationServiceInternal.AccessTokenComponent);
			registry.RegisterServiceComponent((global::Unity.Services.Authentication.Internal.IAccessTokenObserver)authenticationServiceInternal.AccessTokenComponent);
			registry.RegisterServiceComponent((global::Unity.Services.Authentication.Internal.IEnvironmentId)authenticationServiceInternal.EnvironmentIdComponent);
			registry.RegisterServiceComponent((global::Unity.Services.Authentication.Internal.IPlayerId)authenticationServiceInternal.PlayerIdComponent);
			registry.RegisterServiceComponent((global::Unity.Services.Authentication.Internal.IPlayerNameComponent)authenticationServiceInternal.PlayerNameComponent);
			return authenticationServiceInternal;
		}

		private string GetProfile(global::Unity.Services.Core.Configuration.Internal.IProjectConfiguration projectConfiguration)
		{
			return projectConfiguration.GetString("com.unity.services.authentication.profile", "default");
		}

		private string GetPlayerAuthHost(global::Unity.Services.Core.Configuration.Internal.IProjectConfiguration projectConfiguration)
		{
			if (projectConfiguration?.GetString("com.unity.services.core.cloud-environment") == "staging")
			{
				return "https://player-auth-stg.services.api.unity.com";
			}
			return "https://player-auth.services.api.unity.com";
		}

		private string GetPlayerNamesHost(global::Unity.Services.Core.Configuration.Internal.IProjectConfiguration projectConfiguration)
		{
			if (projectConfiguration?.GetString("com.unity.services.core.cloud-environment") == "staging")
			{
				return "https://social-stg.services.api.unity.com/v1";
			}
			return "https://social.services.api.unity.com/v1";
		}
	}
}
