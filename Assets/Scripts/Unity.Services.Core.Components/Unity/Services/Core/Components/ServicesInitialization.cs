namespace Unity.Services.Core.Components
{
	[global::UnityEngine.AddComponentMenu("Services/Services Initialization")]
	public class ServicesInitialization : global::Unity.Services.Core.Components.ServicesBehaviour
	{
		[global::UnityEngine.Header("Automation")]
		[global::UnityEngine.Tooltip("This will attempt to initialize the services in Start().")]
		[global::UnityEngine.SerializeField]
		public bool InitializeOnStart;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Use this to set a custom environment in the initialization options. Defaults to the environment defined in the project settings or production.")]
		[global::Unity.Services.Core.Internal.Visibility("InitializeOnStart", true)]
		public bool UseCustomEnvironment;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Choose the environment name to pass in the initialization options. You can configure environments in the unity dashboard.")]
		[global::Unity.Services.Core.Internal.Visibility("UseCustomEnvironment", true)]
		public string EnvironmentName = "production";

		[global::UnityEngine.Header("Events")]
		[global::UnityEngine.SerializeField]
		public global::Unity.Services.Core.Components.ServicesInitializationEvents Events = new global::Unity.Services.Core.Components.ServicesInitializationEvents();

		internal bool IsSetupDone { get; private set; }

		internal ServicesInitialization()
		{
		}

		protected override async void OnServicesReady()
		{
			await SetupAsync();
		}

		protected override void OnServicesInitialized()
		{
		}

		protected override void Cleanup()
		{
			if (base.Services != null)
			{
				base.Services.Initialized -= OnInitialized;
				base.Services.InitializeFailed -= OnInitializeFailed;
			}
		}

		internal async global::System.Threading.Tasks.Task SetupAsync()
		{
			if (base.Services.State != global::Unity.Services.Core.ServicesInitializationState.Initialized)
			{
				base.Services.Initialized -= OnInitialized;
				base.Services.Initialized += OnInitialized;
				base.Services.InitializeFailed -= OnInitializeFailed;
				base.Services.InitializeFailed += OnInitializeFailed;
			}
			if (base.Services.State == global::Unity.Services.Core.ServicesInitializationState.Uninitialized && InitializeOnStart)
			{
				await InitializeOnStartAsync();
			}
			IsSetupDone = true;
		}

		internal async global::System.Threading.Tasks.Task InitializeOnStartAsync()
		{
			if (base.Services == null)
			{
				Events?.InitializeFailed?.Invoke(new global::System.Exception("Trying to initiliaze services before the registry is set."));
				return;
			}
			try
			{
				await base.Services.InitializeAsync(BuildInitializationOptions());
			}
			catch (global::System.Exception)
			{
			}
		}

		internal global::Unity.Services.Core.InitializationOptions BuildInitializationOptions()
		{
			global::Unity.Services.Core.InitializationOptions initializationOptions = new global::Unity.Services.Core.InitializationOptions();
			if (UseCustomEnvironment)
			{
				global::Unity.Services.Core.Environments.EnvironmentsOptionsExtensions.SetEnvironmentName(initializationOptions, EnvironmentName);
			}
			return initializationOptions;
		}

		private void OnInitialized()
		{
			Events?.Initialized?.Invoke();
		}

		private void OnInitializeFailed(global::System.Exception e)
		{
			Events?.InitializeFailed?.Invoke(e);
		}
	}
}
