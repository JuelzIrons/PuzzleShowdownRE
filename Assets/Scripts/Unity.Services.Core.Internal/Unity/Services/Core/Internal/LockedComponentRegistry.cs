namespace Unity.Services.Core.Internal
{
	internal class LockedComponentRegistry : global::Unity.Services.Core.Internal.IComponentRegistry
	{
		private const string k_ErrorMessage = "Component registration has been locked. Make sure to register service components before all packages have finished initializing.";

		[global::JetBrains.Annotations.NotNull]
		internal global::Unity.Services.Core.Internal.IComponentRegistry Registry { get; }

		public LockedComponentRegistry([global::JetBrains.Annotations.NotNull] global::Unity.Services.Core.Internal.IComponentRegistry registryToLock)
		{
			Registry = registryToLock;
		}

		public void RegisterServiceComponent<TComponent>(TComponent component) where TComponent : global::Unity.Services.Core.Internal.IServiceComponent
		{
			throw new global::System.InvalidOperationException("Component registration has been locked. Make sure to register service components before all packages have finished initializing.");
		}

		public TComponent GetServiceComponent<TComponent>() where TComponent : global::Unity.Services.Core.Internal.IServiceComponent
		{
			return Registry.GetServiceComponent<TComponent>();
		}

		public bool TryGetServiceComponent<TComponent>(out TComponent component) where TComponent : global::Unity.Services.Core.Internal.IServiceComponent
		{
			return Registry.TryGetServiceComponent<TComponent>(out component);
		}

		public void ResetProvidedComponents(global::System.Collections.Generic.IDictionary<int, global::Unity.Services.Core.Internal.IServiceComponent> componentTypeHashToInstance)
		{
			throw new global::System.InvalidOperationException("Component registration has been locked. Make sure to register service components before all packages have finished initializing.");
		}
	}
}
