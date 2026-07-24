namespace Unity.Services.Wire.Internal
{
	internal class WireServiceProvider : global::Unity.Services.Core.Internal.IInitializablePackageV2, global::Unity.Services.Core.Internal.IInitializablePackage
	{
		private const string k_CloudEnvironmentKey = "com.unity.services.core.cloud-environment";

		private const string k_StagingEnvironment = "staging";

		[global::UnityEngine.RuntimeInitializeOnLoadMethod(global::UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void InitializeOnLoad()
		{
			new global::Unity.Services.Wire.Internal.WireServiceProvider().Register(global::Unity.Services.Core.Internal.CorePackageRegistry.Instance);
		}

		public void Register(global::Unity.Services.Core.Internal.CorePackageRegistry registry)
		{
			registry.Register(this).DependsOn<global::Unity.Services.Authentication.Internal.IAccessToken>().DependsOn<global::Unity.Services.Authentication.Internal.IPlayerId>()
				.DependsOn<global::Unity.Services.Core.Scheduler.Internal.IActionScheduler>()
				.DependsOn<global::Unity.Services.Core.Threading.Internal.IUnityThreadUtils>()
				.DependsOn<global::Unity.Services.Core.Telemetry.Internal.IMetricsFactory>()
				.DependsOn<global::Unity.Services.Core.Configuration.Internal.IProjectConfiguration>()
				.ProvidesComponent<global::Unity.Services.Wire.Internal.IWire>();
		}

		public global::System.Threading.Tasks.Task Initialize(global::Unity.Services.Core.Internal.CoreRegistry registry)
		{
			return InitializeComponent(registry);
		}

		public global::System.Threading.Tasks.Task InitializeInstanceAsync(global::Unity.Services.Core.Internal.CoreRegistry registry)
		{
			return InitializeComponent(registry);
		}

		private async global::System.Threading.Tasks.Task InitializeComponent(global::Unity.Services.Core.Internal.CoreRegistry registry)
		{
			global::Unity.Services.Core.Scheduler.Internal.IActionScheduler serviceComponent = registry.GetServiceComponent<global::Unity.Services.Core.Scheduler.Internal.IActionScheduler>();
			if (serviceComponent == null)
			{
				throw new global::UnityEngine.MissingComponentException("IActionScheduler component not initialized.");
			}
			global::Unity.Services.Core.Threading.Internal.IUnityThreadUtils serviceComponent2 = registry.GetServiceComponent<global::Unity.Services.Core.Threading.Internal.IUnityThreadUtils>();
			if (serviceComponent2 == null)
			{
				throw new global::UnityEngine.MissingComponentException("IUnityThreadUtils component not initialized.");
			}
			global::Unity.Services.Authentication.Internal.IAccessToken serviceComponent3 = registry.GetServiceComponent<global::Unity.Services.Authentication.Internal.IAccessToken>();
			if (serviceComponent3 == null)
			{
				throw new global::UnityEngine.MissingComponentException("IAccessToken component not initialized.");
			}
			global::Unity.Services.Authentication.Internal.IPlayerId obj = registry.GetServiceComponent<global::Unity.Services.Authentication.Internal.IPlayerId>() ?? throw new global::UnityEngine.MissingComponentException("IPlayerId component not initialized.");
			global::Unity.Services.Core.Telemetry.Internal.IMetrics metrics = registry.GetServiceComponent<global::Unity.Services.Core.Telemetry.Internal.IMetricsFactory>().Create("com.unity.services.wire");
			metrics.SendSumMetric("wire_init");
			global::Unity.Services.Core.Configuration.Internal.IProjectConfiguration serviceComponent4 = registry.GetServiceComponent<global::Unity.Services.Core.Configuration.Internal.IProjectConfiguration>();
			if (serviceComponent4 == null)
			{
				throw new global::UnityEngine.MissingComponentException("IProjectConfiguration component not initialized.");
			}
			global::Unity.Services.Wire.Internal.Client client = new global::Unity.Services.Wire.Internal.Client(GetConfiguration(serviceComponent3, serviceComponent4), serviceComponent, metrics, serviceComponent2, new global::Unity.Services.Wire.Internal.WebSocketFactory());
			obj.PlayerIdChanged += client.OnIdentityChanged;
			if (!string.IsNullOrEmpty(serviceComponent3.AccessToken))
			{
				await client.ResetAsync(reconnect: true);
			}
			registry.RegisterServiceComponent((global::Unity.Services.Wire.Internal.IWire)client);
		}

		internal global::Unity.Services.Wire.Internal.Configuration GetConfiguration(global::Unity.Services.Authentication.Internal.IAccessToken token, global::Unity.Services.Core.Configuration.Internal.IProjectConfiguration projectCfg)
		{
			string address = "wss://wire.unity3d.com/v2/ws";
			if (projectCfg?.GetString("com.unity.services.core.cloud-environment") == "staging")
			{
				address = "wss://wire-stg.unity3d.com/v2/ws";
			}
			return new global::Unity.Services.Wire.Internal.Configuration
			{
				token = token,
				address = address
			};
		}
	}
}
