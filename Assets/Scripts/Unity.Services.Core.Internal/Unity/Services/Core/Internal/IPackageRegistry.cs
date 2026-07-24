namespace Unity.Services.Core.Internal
{
	internal interface IPackageRegistry
	{
		[global::JetBrains.Annotations.CanBeNull]
		global::Unity.Services.Core.Internal.DependencyTree Tree { get; set; }

		global::Unity.Services.Core.Internal.CoreRegistration RegisterPackage<TPackage>([global::JetBrains.Annotations.NotNull] TPackage package) where TPackage : global::Unity.Services.Core.Internal.IInitializablePackage;

		void RegisterDependency<TComponent>(int packageTypeHash) where TComponent : global::Unity.Services.Core.Internal.IServiceComponent;

		void RegisterOptionalDependency<TComponent>(int packageTypeHash) where TComponent : global::Unity.Services.Core.Internal.IServiceComponent;

		void RegisterProvision<TComponent>(int packageTypeHash) where TComponent : global::Unity.Services.Core.Internal.IServiceComponent;
	}
}
