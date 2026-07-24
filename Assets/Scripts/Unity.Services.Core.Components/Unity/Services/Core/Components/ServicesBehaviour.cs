namespace Unity.Services.Core.Components
{
	public abstract class ServicesBehaviour : global::UnityEngine.MonoBehaviour
	{
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Use this to setup a custom services registry. All services in a registry are unique.")]
		[global::Unity.Services.Core.Internal.Visibility("ShowAdvancedFeatures", true)]
		public bool UseCustomServices;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Unique local identifier for the custom set of services. Used as the key in the registries dictionary.")]
		[global::Unity.Services.Core.Internal.Visibility("ShowAdvancedFeatures", true)]
		public string ServicesIdentifier;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		internal bool ShowAdvancedFeatures;

		public global::Unity.Services.Core.IUnityServices Services { get; internal set; }

		internal virtual void Start()
		{
			SetRegistry();
			if (Services != null)
			{
				if (Services.State == global::Unity.Services.Core.ServicesInitializationState.Initialized)
				{
					OnServicesInitialized();
					return;
				}
				Services.Initialized -= OnServicesInitialized;
				Services.Initialized += OnServicesInitialized;
			}
		}

		internal virtual void OnDestroy()
		{
			if (Services != null)
			{
				Services.Initialized -= OnServicesInitialized;
			}
			Cleanup();
		}

		private void SetRegistry()
		{
			Services = ((!UseCustomServices) ? global::Unity.Services.Core.UnityServices.Instance : (global::Unity.Services.Core.UnityServices.Services.ContainsKey(ServicesIdentifier) ? global::Unity.Services.Core.UnityServices.Services[ServicesIdentifier] : global::Unity.Services.Core.UnityServices.CreateServices(ServicesIdentifier)));
			OnServicesReady();
		}

		protected abstract void OnServicesReady();

		protected abstract void OnServicesInitialized();

		protected abstract void Cleanup();
	}
}
