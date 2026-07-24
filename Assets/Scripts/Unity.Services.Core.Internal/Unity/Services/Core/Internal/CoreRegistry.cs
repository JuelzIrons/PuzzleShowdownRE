namespace Unity.Services.Core.Internal
{
	public sealed class CoreRegistry
	{
		public static global::Unity.Services.Core.Internal.CoreRegistry Instance { get; internal set; }

		public string InstanceId { get; }

		internal global::Unity.Services.Core.Internal.ServicesType Type { get; private set; }

		internal global::Unity.Services.Core.InitializationOptions Options { get; set; }

		[global::JetBrains.Annotations.NotNull]
		internal global::Unity.Services.Core.Internal.IPackageRegistry PackageRegistry { get; private set; }

		[global::JetBrains.Annotations.NotNull]
		internal global::Unity.Services.Core.Internal.IComponentRegistry ComponentRegistry { get; private set; }

		[global::JetBrains.Annotations.NotNull]
		internal global::Unity.Services.Core.Internal.IServiceRegistry ServiceRegistry { get; private set; }

		internal CoreRegistry()
		{
			Type = global::Unity.Services.Core.Internal.ServicesType.Default;
			InstanceId = null;
			PackageRegistry = new global::Unity.Services.Core.Internal.PackageRegistry(new global::Unity.Services.Core.Internal.DependencyTree());
			ComponentRegistry = new global::Unity.Services.Core.Internal.ComponentRegistry();
			ServiceRegistry = new global::Unity.Services.Core.Internal.ServiceRegistry();
		}

		internal CoreRegistry(global::Unity.Services.Core.Internal.IPackageRegistry packageRegistry, global::Unity.Services.Core.Internal.ServicesType type = global::Unity.Services.Core.Internal.ServicesType.Default, string instanceId = null)
		{
			Type = type;
			InstanceId = instanceId;
			PackageRegistry = packageRegistry;
			ComponentRegistry = new global::Unity.Services.Core.Internal.ComponentRegistry();
			ServiceRegistry = new global::Unity.Services.Core.Internal.ServiceRegistry();
		}

		public global::Unity.Services.Core.Internal.CoreRegistration RegisterPackage<TPackage>([global::JetBrains.Annotations.NotNull] TPackage package) where TPackage : global::Unity.Services.Core.Internal.IInitializablePackage
		{
			return PackageRegistry.RegisterPackage(package);
		}

		public void RegisterServiceComponent<TComponent>([global::JetBrains.Annotations.NotNull] TComponent component) where TComponent : global::Unity.Services.Core.Internal.IServiceComponent
		{
			ComponentRegistry.RegisterServiceComponent(component);
		}

		public TComponent GetServiceComponent<TComponent>() where TComponent : global::Unity.Services.Core.Internal.IServiceComponent
		{
			return ComponentRegistry.GetServiceComponent<TComponent>();
		}

		public bool TryGetServiceComponent<TComponent>(out TComponent component) where TComponent : global::Unity.Services.Core.Internal.IServiceComponent
		{
			return ComponentRegistry.TryGetServiceComponent<TComponent>(out component);
		}

		public void RegisterService<T>([global::JetBrains.Annotations.NotNull] T service)
		{
			ServiceRegistry.RegisterService(service);
		}

		public T GetService<T>()
		{
			return ServiceRegistry.GetService<T>();
		}

		internal void LockComponentRegistration()
		{
			if (!(ComponentRegistry is global::Unity.Services.Core.Internal.LockedComponentRegistry))
			{
				ComponentRegistry = new global::Unity.Services.Core.Internal.LockedComponentRegistry(ComponentRegistry);
			}
		}

		internal void LockServiceRegistration()
		{
			if (!(ServiceRegistry is global::Unity.Services.Core.Internal.LockedServiceRegistry))
			{
				ServiceRegistry = new global::Unity.Services.Core.Internal.LockedServiceRegistry(ServiceRegistry);
			}
		}
	}
}
