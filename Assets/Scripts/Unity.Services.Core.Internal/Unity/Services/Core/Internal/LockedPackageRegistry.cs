namespace Unity.Services.Core.Internal
{
	internal class LockedPackageRegistry : global::Unity.Services.Core.Internal.IPackageRegistry
	{
		private const string k_ErrorMessage = "Package registration has been locked. Make sure to register service packages in[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)].";

		[global::JetBrains.Annotations.NotNull]
		internal global::Unity.Services.Core.Internal.IPackageRegistry Registry { get; }

		public global::Unity.Services.Core.Internal.DependencyTree Tree
		{
			get
			{
				return Registry.Tree;
			}
			set
			{
				Registry.Tree = value;
			}
		}

		public LockedPackageRegistry([global::JetBrains.Annotations.NotNull] global::Unity.Services.Core.Internal.IPackageRegistry registryToLock)
		{
			Registry = registryToLock;
		}

		public global::Unity.Services.Core.Internal.CoreRegistration RegisterPackage<TPackage>(TPackage package) where TPackage : global::Unity.Services.Core.Internal.IInitializablePackage
		{
			throw new global::System.InvalidOperationException("Package registration has been locked. Make sure to register service packages in[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)].");
		}

		public void RegisterDependency<TComponent>(int packageTypeHash) where TComponent : global::Unity.Services.Core.Internal.IServiceComponent
		{
			throw new global::System.InvalidOperationException("Package registration has been locked. Make sure to register service packages in[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)].");
		}

		public void RegisterOptionalDependency<TComponent>(int packageTypeHash) where TComponent : global::Unity.Services.Core.Internal.IServiceComponent
		{
			throw new global::System.InvalidOperationException("Package registration has been locked. Make sure to register service packages in[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)].");
		}

		public void RegisterProvision<TComponent>(int packageTypeHash) where TComponent : global::Unity.Services.Core.Internal.IServiceComponent
		{
			throw new global::System.InvalidOperationException("Package registration has been locked. Make sure to register service packages in[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)].");
		}
	}
}
