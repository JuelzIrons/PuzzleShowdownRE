namespace Unity.Services.Core.Internal
{
	internal class DependencyTree
	{
		public readonly global::System.Collections.Generic.Dictionary<int, global::Unity.Services.Core.Internal.IInitializablePackage> PackageTypeHashToInstance;

		public readonly global::System.Collections.Generic.Dictionary<int, int> ComponentTypeHashToPackageTypeHash;

		public readonly global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<int>> PackageTypeHashToComponentTypeHashDependencies;

		public readonly global::System.Collections.Generic.Dictionary<int, global::Unity.Services.Core.Internal.IServiceComponent> ComponentTypeHashToInstance;

		internal DependencyTree()
			: this(new global::System.Collections.Generic.Dictionary<int, global::Unity.Services.Core.Internal.IInitializablePackage>(), new global::System.Collections.Generic.Dictionary<int, int>(), new global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<int>>(), new global::System.Collections.Generic.Dictionary<int, global::Unity.Services.Core.Internal.IServiceComponent>())
		{
		}

		internal DependencyTree(global::System.Collections.Generic.Dictionary<int, global::Unity.Services.Core.Internal.IInitializablePackage> packageToInstance, global::System.Collections.Generic.Dictionary<int, int> componentToPackage, global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<int>> packageToComponentDependencies, global::System.Collections.Generic.Dictionary<int, global::Unity.Services.Core.Internal.IServiceComponent> componentToInstance)
		{
			PackageTypeHashToInstance = packageToInstance;
			ComponentTypeHashToPackageTypeHash = componentToPackage;
			PackageTypeHashToComponentTypeHashDependencies = packageToComponentDependencies;
			ComponentTypeHashToInstance = componentToInstance;
		}
	}
}
