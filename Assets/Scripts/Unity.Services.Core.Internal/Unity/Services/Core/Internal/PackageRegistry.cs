namespace Unity.Services.Core.Internal
{
	internal class PackageRegistry : global::Unity.Services.Core.Internal.IPackageRegistry
	{
		public global::Unity.Services.Core.Internal.DependencyTree Tree { get; set; }

		public PackageRegistry([global::JetBrains.Annotations.CanBeNull] global::Unity.Services.Core.Internal.DependencyTree tree)
		{
			Tree = tree;
		}

		public global::Unity.Services.Core.Internal.CoreRegistration RegisterPackage<TPackage>(TPackage package) where TPackage : global::Unity.Services.Core.Internal.IInitializablePackage
		{
			int hashCode = typeof(TPackage).GetHashCode();
			Tree.PackageTypeHashToInstance[hashCode] = package;
			Tree.PackageTypeHashToComponentTypeHashDependencies[hashCode] = new global::System.Collections.Generic.List<int>();
			return new global::Unity.Services.Core.Internal.CoreRegistration(this, hashCode);
		}

		public void RegisterDependency<TComponent>(int packageTypeHash) where TComponent : global::Unity.Services.Core.Internal.IServiceComponent
		{
			global::System.Type typeFromHandle = typeof(TComponent);
			int hashCode = typeFromHandle.GetHashCode();
			Tree.ComponentTypeHashToInstance[hashCode] = new global::Unity.Services.Core.Internal.MissingComponent(typeFromHandle);
			AddComponentDependencyToPackage(hashCode, packageTypeHash);
		}

		public void RegisterOptionalDependency<TComponent>(int packageTypeHash) where TComponent : global::Unity.Services.Core.Internal.IServiceComponent
		{
			int hashCode = typeof(TComponent).GetHashCode();
			if (!Tree.ComponentTypeHashToInstance.ContainsKey(hashCode))
			{
				Tree.ComponentTypeHashToInstance[hashCode] = null;
			}
			AddComponentDependencyToPackage(hashCode, packageTypeHash);
		}

		public void RegisterProvision<TComponent>(int packageTypeHash) where TComponent : global::Unity.Services.Core.Internal.IServiceComponent
		{
			int hashCode = typeof(TComponent).GetHashCode();
			Tree.ComponentTypeHashToPackageTypeHash[hashCode] = packageTypeHash;
		}

		private void AddComponentDependencyToPackage(int componentTypeHash, int packageTypeHash)
		{
			global::System.Collections.Generic.List<int> list = Tree.PackageTypeHashToComponentTypeHashDependencies[packageTypeHash];
			if (!list.Contains(componentTypeHash))
			{
				list.Add(componentTypeHash);
			}
		}
	}
}
