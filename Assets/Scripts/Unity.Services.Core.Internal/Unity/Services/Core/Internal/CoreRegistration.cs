namespace Unity.Services.Core.Internal
{
	public readonly struct CoreRegistration
	{
		private readonly global::Unity.Services.Core.Internal.IPackageRegistry m_Registry;

		private readonly int m_PackageHash;

		internal CoreRegistration(global::Unity.Services.Core.Internal.IPackageRegistry registry, int packageHash)
		{
			m_Registry = registry;
			m_PackageHash = packageHash;
		}

		public global::Unity.Services.Core.Internal.CoreRegistration DependsOn<T>() where T : global::Unity.Services.Core.Internal.IServiceComponent
		{
			m_Registry.RegisterDependency<T>(m_PackageHash);
			return this;
		}

		public global::Unity.Services.Core.Internal.CoreRegistration OptionallyDependsOn<T>() where T : global::Unity.Services.Core.Internal.IServiceComponent
		{
			m_Registry.RegisterOptionalDependency<T>(m_PackageHash);
			return this;
		}

		public global::Unity.Services.Core.Internal.CoreRegistration ProvidesComponent<T>() where T : global::Unity.Services.Core.Internal.IServiceComponent
		{
			m_Registry.RegisterProvision<T>(m_PackageHash);
			return this;
		}
	}
}
