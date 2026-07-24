namespace Unity.Services.Core.Internal
{
	internal class CoreRegistryInitializer
	{
		[global::JetBrains.Annotations.NotNull]
		private readonly global::Unity.Services.Core.Internal.CoreRegistry m_Registry;

		[global::JetBrains.Annotations.NotNull]
		private readonly global::System.Collections.Generic.List<int> m_SortedPackageTypeHashes;

		public CoreRegistryInitializer([global::JetBrains.Annotations.NotNull] global::Unity.Services.Core.Internal.CoreRegistry registry, [global::JetBrains.Annotations.NotNull] global::System.Collections.Generic.List<int> sortedPackageTypeHashes)
		{
			m_Registry = registry;
			m_SortedPackageTypeHashes = sortedPackageTypeHashes;
		}

		public async global::System.Threading.Tasks.Task<global::System.Collections.Generic.List<global::Unity.Services.Core.Internal.PackageInitializationInfo>> InitializeRegistryAsync()
		{
			global::System.Collections.Generic.List<global::Unity.Services.Core.Internal.PackageInitializationInfo> packagesInitInfos = new global::System.Collections.Generic.List<global::Unity.Services.Core.Internal.PackageInitializationInfo>(m_SortedPackageTypeHashes.Count);
			if (m_SortedPackageTypeHashes.Count <= 0)
			{
				return packagesInitInfos;
			}
			global::Unity.Services.Core.Internal.DependencyTree dependencyTree = m_Registry.PackageRegistry.Tree;
			if (dependencyTree == null)
			{
				global::System.NullReferenceException innerException = new global::System.NullReferenceException("Registry requires a valid dependency tree to be initialized.");
				throw new global::Unity.Services.Core.ServicesInitializationException("Registry is in an invalid state (dependency tree is null) and can't be initialized.", innerException);
			}
			m_Registry.ComponentRegistry.ResetProvidedComponents(dependencyTree.ComponentTypeHashToInstance);
			global::System.Collections.Generic.List<global::System.Exception> failureReasons = new global::System.Collections.Generic.List<global::System.Exception>(m_SortedPackageTypeHashes.Count);
			global::System.Diagnostics.Stopwatch stopwatch = new global::System.Diagnostics.Stopwatch();
			for (int i = 0; i < m_SortedPackageTypeHashes.Count; i++)
			{
				global::Unity.Services.Core.Internal.IInitializablePackage package = GetPackageAt(i);
				await TryInitializePackageAsync(package);
			}
			if (failureReasons.Count > 0)
			{
				Fail();
			}
			return packagesInitInfos;
			void Fail()
			{
				global::System.AggregateException innerException2 = new global::System.AggregateException(failureReasons);
				throw new global::Unity.Services.Core.ServicesInitializationException("Some services couldn't be initialized. Look at inner exceptions to get more information.", innerException2);
			}
			global::Unity.Services.Core.Internal.IInitializablePackage GetPackageAt(int index)
			{
				int key = m_SortedPackageTypeHashes[index];
				return dependencyTree.PackageTypeHashToInstance[key];
			}
			async global::System.Threading.Tasks.Task InitializePackageAsync(global::Unity.Services.Core.Internal.IInitializablePackage initializablePackage)
			{
				switch (m_Registry.Type)
				{
				case global::Unity.Services.Core.Internal.ServicesType.Default:
					await initializablePackage.Initialize(m_Registry);
					break;
				case global::Unity.Services.Core.Internal.ServicesType.Instance:
					if (initializablePackage is global::Unity.Services.Core.Internal.IInitializablePackageV2)
					{
						await ((global::Unity.Services.Core.Internal.IInitializablePackageV2)initializablePackage).InitializeInstanceAsync(m_Registry);
					}
					break;
				}
			}
			async global::System.Threading.Tasks.Task TryInitializePackageAsync(global::Unity.Services.Core.Internal.IInitializablePackage initializablePackage)
			{
				try
				{
					stopwatch.Restart();
					await InitializePackageAsync(initializablePackage);
					stopwatch.Stop();
					global::Unity.Services.Core.Internal.PackageInitializationInfo item = new global::Unity.Services.Core.Internal.PackageInitializationInfo
					{
						PackageType = initializablePackage.GetType(),
						InitializationTimeInSeconds = stopwatch.Elapsed.TotalSeconds
					};
					packagesInitInfos.Add(item);
				}
				catch (global::System.Exception item2)
				{
					stopwatch.Stop();
					failureReasons.Add(item2);
				}
			}
		}
	}
}
