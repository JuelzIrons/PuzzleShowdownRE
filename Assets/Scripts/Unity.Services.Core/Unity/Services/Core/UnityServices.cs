namespace Unity.Services.Core
{
	public static class UnityServices
	{
		internal static global::Unity.Services.Core.ExternalUserIdProperty ExternalUserIdProperty = new global::Unity.Services.Core.ExternalUserIdProperty();

		public static global::Unity.Services.Core.IUnityServices Instance { get; set; }

		public static global::System.Collections.Generic.IReadOnlyDictionary<string, global::Unity.Services.Core.IUnityServices> Services => s_Services;

		internal static global::System.Threading.Tasks.TaskCompletionSource<object> InstantiationCompletion { get; set; }

		private static global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Core.IUnityServices> s_Services { get; } = new global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Core.IUnityServices>();

		public static global::Unity.Services.Core.ServicesInitializationState State
		{
			get
			{
				if (!global::Unity.Services.Core.UnityThreadUtils.IsRunningOnUnityThread)
				{
					throw new global::Unity.Services.Core.ServicesInitializationException("You are attempting to access UnityServices.State from a non-Unity Thread. UnityServices.State can only be accessed from Unity Thread");
				}
				if (Instance != null)
				{
					return Instance.State;
				}
				global::System.Threading.Tasks.TaskCompletionSource<object> instantiationCompletion = InstantiationCompletion;
				if (instantiationCompletion != null && instantiationCompletion.Task.Status == global::System.Threading.Tasks.TaskStatus.WaitingForActivation)
				{
					return global::Unity.Services.Core.ServicesInitializationState.Initializing;
				}
				return global::Unity.Services.Core.ServicesInitializationState.Uninitialized;
			}
		}

		public static string ExternalUserId
		{
			get
			{
				return ExternalUserIdProperty.UserId;
			}
			set
			{
				ExternalUserIdProperty.UserId = value;
			}
		}

		public static event global::System.Action Initialized
		{
			add
			{
				if (Instance != null)
				{
					Instance.Initialized += value;
				}
			}
			remove
			{
				if (Instance != null)
				{
					Instance.Initialized -= value;
				}
			}
		}

		public static event global::System.Action<global::System.Exception> InitializeFailed
		{
			add
			{
				if (Instance != null)
				{
					Instance.InitializeFailed += value;
				}
			}
			remove
			{
				if (Instance != null)
				{
					Instance.InitializeFailed -= value;
				}
			}
		}

		public static global::System.Threading.Tasks.Task InitializeAsync()
		{
			return InitializeAsync(new global::Unity.Services.Core.InitializationOptions());
		}

		[global::System.Runtime.CompilerServices.PreserveDependency("Register()", "Unity.Services.Core.Registration.CorePackageInitializer", "Unity.Services.Core.Registration")]
		[global::System.Runtime.CompilerServices.PreserveDependency("CreateStaticInstance()", "Unity.Services.Core.Internal.UnityServicesInitializer", "Unity.Services.Core.Internal")]
		[global::System.Runtime.CompilerServices.PreserveDependency("EnableServicesInitializationAsync()", "Unity.Services.Core.Internal.UnityServicesInitializer", "Unity.Services.Core.Internal")]
		[global::System.Runtime.CompilerServices.PreserveDependency("CaptureUnityThreadInfo()", "Unity.Services.Core.UnityThreadUtils", "Unity.Services.Core")]
		public static async global::System.Threading.Tasks.Task InitializeAsync(global::Unity.Services.Core.InitializationOptions options)
		{
			if (!global::Unity.Services.Core.UnityThreadUtils.IsRunningOnUnityThread)
			{
				throw new global::Unity.Services.Core.ServicesInitializationException("You are attempting to initialize Unity Services from a non-Unity Thread. Unity Services can only be initialized from Unity Thread");
			}
			if (!global::UnityEngine.Application.isPlaying)
			{
				throw new global::Unity.Services.Core.ServicesInitializationException("You are attempting to initialize Unity Services in Edit Mode. Unity Services can only be initialized in Play Mode");
			}
			if (Instance == null)
			{
				if (InstantiationCompletion == null)
				{
					InstantiationCompletion = new global::System.Threading.Tasks.TaskCompletionSource<object>();
				}
				await InstantiationCompletion.Task;
			}
			await Instance.InitializeAsync(options);
		}

		public static global::Unity.Services.Core.IUnityServices CreateServices()
		{
			return CreateServices(global::System.Guid.NewGuid().ToString());
		}

		public static global::Unity.Services.Core.IUnityServices CreateServices(string servicesId)
		{
			if (string.IsNullOrEmpty(servicesId))
			{
				throw new global::System.ArgumentException("The services identifier cannot be null or empty");
			}
			if (s_Services.ContainsKey(servicesId))
			{
				throw new global::Unity.Services.Core.ServicesCreationException("The services identifier '" + servicesId + "' is already registered.");
			}
			global::Unity.Services.Core.IUnityServices unityServices = global::Unity.Services.Core.UnityServicesBuilder.Create(servicesId);
			s_Services[servicesId] = unityServices;
			return unityServices;
		}

		internal static void ClearServices()
		{
			s_Services.Clear();
		}
	}
}
