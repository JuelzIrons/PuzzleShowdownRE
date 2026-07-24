namespace Unity.Services.Core.Internal
{
	internal static class UnityServicesInitializer
	{
		private static bool s_CreatedServices;

		[global::UnityEngine.RuntimeInitializeOnLoadMethod(global::UnityEngine.RuntimeInitializeLoadType.AfterAssembliesLoaded)]
		private static void CreateStaticInstance()
		{
			if (s_CreatedServices)
			{
				s_CreatedServices = false;
				return;
			}
			global::Unity.Services.Core.UnityServices.ClearServices();
			global::Unity.Services.Core.UnityServicesBuilder.InstanceCreationDelegate = CreateInstance;
			global::Unity.Services.Core.Internal.CorePackageRegistry corePackageRegistry = new global::Unity.Services.Core.Internal.CorePackageRegistry();
			global::Unity.Services.Core.Internal.CoreRegistry coreRegistry = new global::Unity.Services.Core.Internal.CoreRegistry(corePackageRegistry.Registry);
			global::Unity.Services.Core.Internal.CorePackageRegistry.Instance = corePackageRegistry;
			global::Unity.Services.Core.Internal.CoreRegistry.Instance = coreRegistry;
			global::Unity.Services.Core.Internal.CoreMetrics coreMetrics = new global::Unity.Services.Core.Internal.CoreMetrics();
			global::Unity.Services.Core.Internal.CoreDiagnostics coreDiagnostics = new global::Unity.Services.Core.Internal.CoreDiagnostics();
			global::Unity.Services.Core.UnityServices.Instance = new global::Unity.Services.Core.Internal.UnityServicesInternal(coreRegistry, coreMetrics, coreDiagnostics);
			global::Unity.Services.Core.UnityServices.InstantiationCompletion?.TrySetResult(null);
			global::Unity.Services.Core.Internal.CoreMetrics.Instance = coreMetrics;
			global::Unity.Services.Core.Internal.CoreDiagnostics.Instance = coreDiagnostics;
		}

		[global::UnityEngine.RuntimeInitializeOnLoadMethod(global::UnityEngine.RuntimeInitializeLoadType.AfterSceneLoad)]
		private static async void EnableServicesInitializationAsync()
		{
			await ((global::Unity.Services.Core.Internal.UnityServicesInternal)global::Unity.Services.Core.UnityServices.Instance).EnableInitializationAsync();
		}

		internal static global::Unity.Services.Core.IUnityServices CreateInstance(string servicesId)
		{
			global::Unity.Services.Core.Internal.UnityServicesInternal unityServicesInternal = new global::Unity.Services.Core.Internal.UnityServicesInternal(new global::Unity.Services.Core.Internal.CoreRegistry(global::Unity.Services.Core.Internal.CorePackageRegistry.Instance.Registry, global::Unity.Services.Core.Internal.ServicesType.Instance, servicesId), global::Unity.Services.Core.Internal.CoreMetrics.Instance, global::Unity.Services.Core.Internal.CoreDiagnostics.Instance);
			unityServicesInternal.EnableInitialization();
			return unityServicesInternal;
		}
	}
}
