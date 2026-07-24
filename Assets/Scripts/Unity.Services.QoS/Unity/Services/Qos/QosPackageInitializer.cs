namespace Unity.Services.Qos
{
	internal class QosPackageInitializer : global::Unity.Services.Core.Internal.IInitializablePackageV2, global::Unity.Services.Core.Internal.IInitializablePackage
	{
		private const string k_CloudEnvironmentKey = "com.unity.services.core.cloud-environment";

		private const string k_PackageName = "com.unity.services.qos";

		private const string k_StagingEnvironment = "staging";

		[global::UnityEngine.RuntimeInitializeOnLoadMethod(global::UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
		internal static void InitializeOnLoad()
		{
			new global::Unity.Services.Qos.QosPackageInitializer().Register(global::Unity.Services.Core.Internal.CorePackageRegistry.Instance);
		}

		public void Register(global::Unity.Services.Core.Internal.CorePackageRegistry registry)
		{
			registry.Register(this).DependsOn<global::Unity.Services.Authentication.Internal.IAccessToken>().DependsOn<global::Unity.Services.Core.Telemetry.Internal.IMetricsFactory>()
				.DependsOn<global::Unity.Services.Core.Configuration.Internal.IProjectConfiguration>()
				.ProvidesComponent<global::Unity.Services.Qos.Internal.IQosResults>()
				.ProvidesComponent<global::Unity.Services.Qos.IQosServiceComponent>();
		}

		internal void Register(global::Unity.Services.Core.Internal.CoreRegistry registry)
		{
			registry.RegisterPackage(this).DependsOn<global::Unity.Services.Authentication.Internal.IAccessToken>().DependsOn<global::Unity.Services.Core.Telemetry.Internal.IMetricsFactory>()
				.DependsOn<global::Unity.Services.Core.Configuration.Internal.IProjectConfiguration>()
				.ProvidesComponent<global::Unity.Services.Qos.Internal.IQosResults>()
				.ProvidesComponent<global::Unity.Services.Qos.IQosServiceComponent>();
		}

		public global::System.Threading.Tasks.Task Initialize(global::Unity.Services.Core.Internal.CoreRegistry registry)
		{
			global::Unity.Services.Qos.QosService.Instance = InitializeService(registry);
			return global::System.Threading.Tasks.Task.CompletedTask;
		}

		public global::System.Threading.Tasks.Task InitializeInstanceAsync(global::Unity.Services.Core.Internal.CoreRegistry registry)
		{
			InitializeService(registry);
			return global::System.Threading.Tasks.Task.CompletedTask;
		}

		private global::Unity.Services.Qos.IQosService InitializeService(global::Unity.Services.Core.Internal.CoreRegistry registry)
		{
			global::Unity.Services.Core.Configuration.Internal.IProjectConfiguration serviceComponent = registry.GetServiceComponent<global::Unity.Services.Core.Configuration.Internal.IProjectConfiguration>();
			global::Unity.Services.Authentication.Internal.IAccessToken serviceComponent2 = registry.GetServiceComponent<global::Unity.Services.Authentication.Internal.IAccessToken>();
			global::Unity.Services.Core.Telemetry.Internal.IMetrics metrics = registry.GetServiceComponent<global::Unity.Services.Core.Telemetry.Internal.IMetricsFactory>().Create("com.unity.services.qos");
			global::Unity.Services.Qos.Http.HttpClient httpClient = new global::Unity.Services.Qos.Http.HttpClient();
			global::Unity.Services.Qos.InternalQosDiscoveryService internalQosDiscoveryService = new global::Unity.Services.Qos.InternalQosDiscoveryService(GetHost(serviceComponent), httpClient, serviceComponent2);
			global::Unity.Services.Qos.V2.Http.HttpClient httpClient2 = new global::Unity.Services.Qos.V2.Http.HttpClient();
			global::Unity.Services.Qos.V2.Configuration configuration = new global::Unity.Services.Qos.V2.Configuration(GetHost(serviceComponent), 10, 4, null);
			global::Unity.Services.Qos.WrappedQosService wrappedQosService = new global::Unity.Services.Qos.WrappedQosService(qosDiscoveryApiClientV2: new global::Unity.Services.Qos.V2.Apis.QosDiscovery.QosDiscoveryApiClient(httpClient2, serviceComponent2, configuration), qosDiscoveryApiClient: internalQosDiscoveryService.QosDiscoveryApi, qosRunner: new global::Unity.Services.Qos.Runner.BaselibQosRunner(), accessToken: serviceComponent2, metrics: metrics);
			registry.RegisterService((global::Unity.Services.Qos.IQosService)wrappedQosService);
			registry.RegisterServiceComponent((global::Unity.Services.Qos.Internal.IQosResults)new global::Unity.Services.Qos.QosResults(wrappedQosService));
			registry.RegisterServiceComponent((global::Unity.Services.Qos.IQosServiceComponent)new global::Unity.Services.Qos.QosServiceComponent(wrappedQosService));
			return wrappedQosService;
		}

		private string GetHost(global::Unity.Services.Core.Configuration.Internal.IProjectConfiguration projectConfiguration)
		{
			if (projectConfiguration?.GetString("com.unity.services.core.cloud-environment") == "staging")
			{
				return "https://qos-discovery-stg.services.api.unity.com";
			}
			return "https://qos-discovery.services.api.unity.com";
		}
	}
}
