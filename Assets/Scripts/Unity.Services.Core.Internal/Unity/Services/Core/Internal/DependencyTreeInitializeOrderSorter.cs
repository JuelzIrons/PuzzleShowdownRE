namespace Unity.Services.Core.Internal
{
	internal struct DependencyTreeInitializeOrderSorter
	{
		private enum ExplorationMark
		{
			None = 0,
			Viewed = 1,
			Sorted = 2
		}

		public readonly global::Unity.Services.Core.Internal.DependencyTree Tree;

		public readonly global::System.Collections.Generic.ICollection<int> Target;

		private global::System.Collections.Generic.Dictionary<int, global::Unity.Services.Core.Internal.DependencyTreeInitializeOrderSorter.ExplorationMark> m_PackageTypeHashExplorationHistory;

		public DependencyTreeInitializeOrderSorter(global::Unity.Services.Core.Internal.DependencyTree tree, global::System.Collections.Generic.ICollection<int> target)
		{
			Tree = tree;
			Target = target;
			m_PackageTypeHashExplorationHistory = null;
		}

		public void SortRegisteredPackagesIntoTarget()
		{
			Target.Clear();
			RemoveUnprovidedOptionalDependenciesFromTree();
			global::System.Collections.Generic.IReadOnlyCollection<int> packageTypeHashes = GetPackageTypeHashes();
			m_PackageTypeHashExplorationHistory = new global::System.Collections.Generic.Dictionary<int, global::Unity.Services.Core.Internal.DependencyTreeInitializeOrderSorter.ExplorationMark>(packageTypeHashes.Count);
			try
			{
				foreach (int item in packageTypeHashes)
				{
					SortTreeThrough(item);
				}
			}
			catch (global::Unity.Services.Core.Internal.HashException inner)
			{
				throw new global::Unity.Services.Core.Internal.DependencyTreeSortFailedException(Tree, Target, inner);
			}
			m_PackageTypeHashExplorationHistory = null;
		}

		private void RemoveUnprovidedOptionalDependenciesFromTree()
		{
			foreach (global::System.Collections.Generic.List<int> value in Tree.PackageTypeHashToComponentTypeHashDependencies.Values)
			{
				RemoveUnprovidedOptionalDependencies(value);
			}
		}

		private void RemoveUnprovidedOptionalDependencies(global::System.Collections.Generic.IList<int> dependencyTypeHashes)
		{
			for (int num = dependencyTypeHashes.Count - 1; num >= 0; num--)
			{
				int componentTypeHash = dependencyTypeHashes[num];
				if (Tree.IsOptional(componentTypeHash) && !Tree.IsProvided(componentTypeHash))
				{
					dependencyTypeHashes.RemoveAt(num);
				}
			}
		}

		private void SortTreeThrough(int packageTypeHash)
		{
			m_PackageTypeHashExplorationHistory.TryGetValue(packageTypeHash, out var value);
			switch (value)
			{
			case global::Unity.Services.Core.Internal.DependencyTreeInitializeOrderSorter.ExplorationMark.Viewed:
				throw new global::Unity.Services.Core.Internal.CircularDependencyException();
			case global::Unity.Services.Core.Internal.DependencyTreeInitializeOrderSorter.ExplorationMark.Sorted:
				return;
			}
			MarkPackage(packageTypeHash, global::Unity.Services.Core.Internal.DependencyTreeInitializeOrderSorter.ExplorationMark.Viewed);
			global::System.Collections.Generic.IEnumerable<int> dependencyTypeHashesFor = GetDependencyTypeHashesFor(packageTypeHash);
			try
			{
				SortTreeThrough(dependencyTypeHashesFor);
			}
			catch (global::Unity.Services.Core.Internal.DependencyTreeComponentHashException ex)
			{
				throw new global::Unity.Services.Core.Internal.DependencyTreePackageHashException(packageTypeHash, $"Component with hash[{ex.Hash}] threw exception when sorting package[{packageTypeHash}][{Tree.PackageTypeHashToInstance[packageTypeHash].GetType().FullName}]", ex);
			}
			Target.Add(packageTypeHash);
			MarkPackage(packageTypeHash, global::Unity.Services.Core.Internal.DependencyTreeInitializeOrderSorter.ExplorationMark.Sorted);
		}

		private void SortTreeThrough(global::System.Collections.Generic.IEnumerable<int> dependencyTypeHashes)
		{
			foreach (int dependencyTypeHash in dependencyTypeHashes)
			{
				int packageTypeHashFor = GetPackageTypeHashFor(dependencyTypeHash);
				SortTreeThrough(packageTypeHashFor);
			}
		}

		private void MarkPackage(int packageTypeHash, global::Unity.Services.Core.Internal.DependencyTreeInitializeOrderSorter.ExplorationMark mark)
		{
			m_PackageTypeHashExplorationHistory[packageTypeHash] = mark;
		}

		private global::System.Collections.Generic.IReadOnlyCollection<int> GetPackageTypeHashes()
		{
			return Tree.PackageTypeHashToInstance.Keys;
		}

		private int GetPackageTypeHashFor(int componentTypeHash)
		{
			if (!Tree.ComponentTypeHashToPackageTypeHash.TryGetValue(componentTypeHash, out var value))
			{
				throw new global::Unity.Services.Core.Internal.DependencyTreeComponentHashException(componentTypeHash, $"Component with hash[{componentTypeHash}] does not exist!");
			}
			return value;
		}

		private global::System.Collections.Generic.IEnumerable<int> GetDependencyTypeHashesFor(int packageTypeHash)
		{
			if (!Tree.PackageTypeHashToComponentTypeHashDependencies.TryGetValue(packageTypeHash, out var value))
			{
				throw new global::Unity.Services.Core.Internal.DependencyTreePackageHashException(packageTypeHash, $"Package with hash[{packageTypeHash}] does not exist!");
			}
			return value;
		}
	}
}
