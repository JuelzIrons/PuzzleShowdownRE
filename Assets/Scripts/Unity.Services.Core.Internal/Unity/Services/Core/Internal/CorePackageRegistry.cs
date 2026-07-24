namespace Unity.Services.Core.Internal
{
	public sealed class CorePackageRegistry
	{
		public static global::Unity.Services.Core.Internal.CorePackageRegistry Instance { get; internal set; }

		internal global::Unity.Services.Core.Internal.IPackageRegistry Registry { get; set; }

		internal CorePackageRegistry()
		{
			Registry = new global::Unity.Services.Core.Internal.PackageRegistry(new global::Unity.Services.Core.Internal.DependencyTree());
		}

		internal CorePackageRegistry(global::Unity.Services.Core.Internal.IPackageRegistry registry)
		{
			Registry = registry;
		}

		public global::Unity.Services.Core.Internal.CoreRegistration Register<TPackage>([global::JetBrains.Annotations.NotNull] TPackage package) where TPackage : global::Unity.Services.Core.Internal.IInitializablePackage
		{
			return Registry.RegisterPackage(package);
		}

		internal void Lock()
		{
			if (!(Registry is global::Unity.Services.Core.Internal.LockedPackageRegistry))
			{
				Registry = new global::Unity.Services.Core.Internal.LockedPackageRegistry(Registry);
			}
		}
	}
}
