namespace Unity.Services.Core.Registration
{
	internal class CorePackageInitializer : global::Unity.Services.Core.Internal.IInitializablePackageV2, global::Unity.Services.Core.Internal.IInitializablePackage, global::Unity.Services.Core.Telemetry.Internal.IDiagnosticsComponentProvider
	{
		internal const string CorePackageName = "com.unity.services.core";

		internal const string ProjectUnlinkMessage = "To use Unity's dashboard services, you need to link your Unity project to a project ID. To do this, go to Project Settings to select your organization, select your project and then link a project ID. You also need to make sure your organization has access to the required products. Visit https://dashboard.unity3d.com to sign up.";

		private global::Unity.Services.Core.Internal.CoreRegistry m_Registry;

		private readonly global::Unity.Services.Core.Internal.Serialization.IJsonSerializer m_Serializer;

		private global::Unity.Services.Core.InitializationOptions m_CurrentInitializationOptions;

		internal global::Unity.Services.Core.Scheduler.Internal.ActionScheduler ActionScheduler { get; private set; }

		internal global::Unity.Services.Core.Device.InstallationId InstallationId { get; private set; }

		internal global::Unity.Services.Core.Configuration.ProjectConfiguration ProjectConfig { get; private set; }

		internal global::Unity.Services.Core.Environments.Internal.Environments Environments { get; private set; }

		internal global::Unity.Services.Core.Configuration.ExternalUserId ExternalUserId { get; private set; }

		internal global::Unity.Services.Core.Configuration.Internal.ICloudProjectId CloudProjectId { get; private set; }

		internal global::Unity.Services.Core.Telemetry.Internal.IDiagnosticsFactory DiagnosticsFactory { get; private set; }

		internal global::Unity.Services.Core.Telemetry.Internal.IMetricsFactory MetricsFactory { get; private set; }

		internal global::Unity.Services.Core.Threading.Internal.UnityThreadUtilsInternal UnityThreadUtils { get; private set; }

		[global::UnityEngine.RuntimeInitializeOnLoadMethod(global::UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void InitializeOnLoad()
		{
			new global::Unity.Services.Core.Registration.CorePackageInitializer(new global::Unity.Services.Core.Internal.Serialization.NewtonsoftSerializer()).Register(global::Unity.Services.Core.Internal.CorePackageRegistry.Instance);
		}

		public void Register(global::Unity.Services.Core.Internal.CorePackageRegistry registry)
		{
			global::Unity.Services.Core.Internal.CoreDiagnostics.Instance.DiagnosticsComponentProvider = this;
			registry.Register(this).ProvidesComponent<global::Unity.Services.Core.Device.Internal.IInstallationId>().ProvidesComponent<global::Unity.Services.Core.Configuration.Internal.ICloudProjectId>()
				.ProvidesComponent<global::Unity.Services.Core.Scheduler.Internal.IActionScheduler>()
				.ProvidesComponent<global::Unity.Services.Core.Environments.Internal.IEnvironments>()
				.ProvidesComponent<global::Unity.Services.Core.Configuration.Internal.IProjectConfiguration>()
				.ProvidesComponent<global::Unity.Services.Core.Telemetry.Internal.IMetricsFactory>()
				.ProvidesComponent<global::Unity.Services.Core.Telemetry.Internal.IDiagnosticsFactory>()
				.ProvidesComponent<global::Unity.Services.Core.Threading.Internal.IUnityThreadUtils>()
				.ProvidesComponent<global::Unity.Services.Core.Configuration.Internal.IExternalUserId>();
		}

		public CorePackageInitializer()
		{
			m_Serializer = new global::Unity.Services.Core.Internal.Serialization.NewtonsoftSerializer();
		}

		public CorePackageInitializer(global::Unity.Services.Core.Internal.Serialization.IJsonSerializer serializer)
		{
			m_Serializer = serializer;
		}

		public global::System.Threading.Tasks.Task Initialize(global::Unity.Services.Core.Internal.CoreRegistry registry)
		{
			m_Registry = registry;
			return InitializeComponents();
		}

		public global::System.Threading.Tasks.Task InitializeInstanceAsync(global::Unity.Services.Core.Internal.CoreRegistry registry)
		{
			m_Registry = registry;
			return InitializeComponents();
		}

		private async global::System.Threading.Tasks.Task InitializeComponents()
		{
			try
			{
				if (HaveInitOptionsChanged())
				{
					FreeOptionsDependantComponents();
				}
				InitializeInstallationId();
				InitializeActionScheduler();
				await InitializeProjectConfigAsync(m_Registry.Options);
				InitializeExternalUserId(ProjectConfig);
				InitializeEnvironments(ProjectConfig);
				InitializeCloudProjectId();
				if (string.IsNullOrEmpty(CloudProjectId.GetCloudProjectId()))
				{
					throw new global::Unity.Services.Core.UnityProjectNotLinkedException("To use Unity's dashboard services, you need to link your Unity project to a project ID. To do this, go to Project Settings to select your organization, select your project and then link a project ID. You also need to make sure your organization has access to the required products. Visit https://dashboard.unity3d.com to sign up.");
				}
				InitializeMetrics();
				InitializeDiagnostics();
				InitializeUnityThreadUtils();
				RegisterProvidedComponents();
			}
			catch (global::System.Exception reason) when (SendFailedInitDiagnostic(reason))
			{
			}
			void RegisterProvidedComponents()
			{
				m_Registry.RegisterServiceComponent((global::Unity.Services.Core.Device.Internal.IInstallationId)InstallationId);
				m_Registry.RegisterServiceComponent((global::Unity.Services.Core.Scheduler.Internal.IActionScheduler)ActionScheduler);
				m_Registry.RegisterServiceComponent((global::Unity.Services.Core.Configuration.Internal.IProjectConfiguration)ProjectConfig);
				m_Registry.RegisterServiceComponent((global::Unity.Services.Core.Environments.Internal.IEnvironments)Environments);
				m_Registry.RegisterServiceComponent(MetricsFactory);
				m_Registry.RegisterServiceComponent(DiagnosticsFactory);
				m_Registry.RegisterServiceComponent(CloudProjectId);
				m_Registry.RegisterServiceComponent((global::Unity.Services.Core.Threading.Internal.IUnityThreadUtils)UnityThreadUtils);
				m_Registry.RegisterServiceComponent((global::Unity.Services.Core.Configuration.Internal.IExternalUserId)ExternalUserId);
			}
			static bool SendFailedInitDiagnostic(global::System.Exception ex)
			{
				return false;
			}
		}

		private bool HaveInitOptionsChanged()
		{
			if (m_CurrentInitializationOptions != null)
			{
				return !global::Unity.Services.Core.Internal.DictionaryExtensions.ValueEquals(m_CurrentInitializationOptions.Values, m_Registry.Options.Values);
			}
			return false;
		}

		private void FreeOptionsDependantComponents()
		{
			ProjectConfig = null;
			Environments = null;
			DiagnosticsFactory = null;
			MetricsFactory = null;
		}

		internal void InitializeInstallationId()
		{
			if (InstallationId == null)
			{
				global::Unity.Services.Core.Device.InstallationId installationId = new global::Unity.Services.Core.Device.InstallationId();
				installationId.CreateIdentifier();
				InstallationId = installationId;
			}
		}

		internal void InitializeActionScheduler()
		{
			if (ActionScheduler == null)
			{
				global::Unity.Services.Core.Scheduler.Internal.ActionScheduler actionScheduler = new global::Unity.Services.Core.Scheduler.Internal.ActionScheduler();
				actionScheduler.JoinPlayerLoopSystem();
				ActionScheduler = actionScheduler;
			}
		}

		internal async global::System.Threading.Tasks.Task InitializeProjectConfigAsync([global::JetBrains.Annotations.NotNull] global::Unity.Services.Core.InitializationOptions options)
		{
			if (ProjectConfig == null)
			{
				ProjectConfig = await GenerateProjectConfigurationAsync(options);
				m_CurrentInitializationOptions = new global::Unity.Services.Core.InitializationOptions(options);
			}
		}

		internal async global::System.Threading.Tasks.Task<global::Unity.Services.Core.Configuration.ProjectConfiguration> GenerateProjectConfigurationAsync([global::JetBrains.Annotations.NotNull] global::Unity.Services.Core.InitializationOptions options)
		{
			global::Unity.Services.Core.Configuration.SerializableProjectConfiguration config = await GetSerializedConfigOrEmptyAsync();
			if (config.Keys == null || config.Values == null)
			{
				config = global::Unity.Services.Core.Configuration.SerializableProjectConfiguration.Empty;
			}
			global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Core.Configuration.ConfigurationEntry> dictionary = new global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Core.Configuration.ConfigurationEntry>(config.Keys.Length);
			global::Unity.Services.Core.Configuration.ConfigurationCollectionHelper.FillWith(dictionary, config);
			global::Unity.Services.Core.Configuration.ConfigurationCollectionHelper.FillWith(dictionary, options);
			return new global::Unity.Services.Core.Configuration.ProjectConfiguration(dictionary, m_Serializer);
		}

		internal static async global::System.Threading.Tasks.Task<global::Unity.Services.Core.Configuration.SerializableProjectConfiguration> GetSerializedConfigOrEmptyAsync()
		{
			try
			{
				return await global::Unity.Services.Core.Configuration.ConfigurationUtils.ConfigurationLoader.GetConfigAsync();
			}
			catch (global::System.Exception ex)
			{
				global::Unity.Services.Core.Internal.CoreLogger.LogError("An error occured while trying to get the project configuration for services.\n" + ex.Message + "\n" + ex.StackTrace);
				return global::Unity.Services.Core.Configuration.SerializableProjectConfiguration.Empty;
			}
		}

		internal void InitializeExternalUserId(global::Unity.Services.Core.Configuration.Internal.IProjectConfiguration projectConfiguration)
		{
			if (global::Unity.Services.Core.UnityServices.ExternalUserId == null)
			{
				string text = projectConfiguration.GetString("com.unity.services.core.analytics-user-id");
				if (!string.IsNullOrEmpty(text))
				{
					global::Unity.Services.Core.UnityServices.ExternalUserId = text;
				}
			}
			if (ExternalUserId == null)
			{
				ExternalUserId = new global::Unity.Services.Core.Configuration.ExternalUserId();
			}
		}

		internal void InitializeEnvironments(global::Unity.Services.Core.Configuration.Internal.IProjectConfiguration projectConfiguration)
		{
			if (Environments == null)
			{
				string current = projectConfiguration.GetString("com.unity.services.core.environment-name", "production");
				Environments = new global::Unity.Services.Core.Environments.Internal.Environments
				{
					Current = current
				};
			}
		}

		internal void InitializeMetrics()
		{
			if (MetricsFactory == null)
			{
				MetricsFactory = new global::Unity.Services.Core.Telemetry.Internal.MetricsFactory();
			}
		}

		internal void InitializeDiagnostics()
		{
			if (DiagnosticsFactory == null)
			{
				DiagnosticsFactory = new global::Unity.Services.Core.Telemetry.Internal.DiagnosticsFactory();
			}
		}

		internal void InitializeCloudProjectId(global::Unity.Services.Core.Configuration.Internal.ICloudProjectId cloudProjectId = null)
		{
			if (CloudProjectId == null)
			{
				CloudProjectId = cloudProjectId ?? new global::Unity.Services.Core.Configuration.CloudProjectId();
			}
		}

		internal void InitializeUnityThreadUtils()
		{
			if (UnityThreadUtils == null)
			{
				UnityThreadUtils = new global::Unity.Services.Core.Threading.Internal.UnityThreadUtilsInternal();
			}
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Core.Telemetry.Internal.IDiagnosticsFactory> CreateDiagnosticsComponents()
		{
			if (HaveInitOptionsChanged())
			{
				FreeOptionsDependantComponents();
			}
			InitializeActionScheduler();
			await InitializeProjectConfigAsync(m_Registry.Options);
			InitializeEnvironments(ProjectConfig);
			InitializeCloudProjectId();
			return DiagnosticsFactory;
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_SERVICES_CORE_VERBOSE_LOGGING")]
		private void LogInitializationInfoJson()
		{
			global::Newtonsoft.Json.Linq.JObject jObject = new global::Newtonsoft.Json.Linq.JObject();
			global::Newtonsoft.Json.Linq.JObject jObject2 = global::Newtonsoft.Json.Linq.JObject.Parse(m_Serializer.SerializeObject(DiagnosticsFactory.CommonTags));
			global::Newtonsoft.Json.Linq.JObject value = global::Newtonsoft.Json.Linq.JObject.Parse(ProjectConfig.ToJson());
			global::Newtonsoft.Json.Linq.JObject content = global::Newtonsoft.Json.Linq.JObject.Parse("{\"installation_id\": \"" + InstallationId.Identifier + "\"}");
			jObject2.Merge(content);
			jObject.Add("CommonSettings", jObject2);
			jObject.Add("ServicesRuntimeSettings", value);
		}

		public async global::System.Threading.Tasks.Task<string> GetSerializedProjectConfigurationAsync()
		{
			await InitializeProjectConfigAsync(m_Registry.Options);
			return ProjectConfig.ToJson();
		}
	}
}
