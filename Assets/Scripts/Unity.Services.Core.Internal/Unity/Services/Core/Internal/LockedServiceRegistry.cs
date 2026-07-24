namespace Unity.Services.Core.Internal
{
	internal class LockedServiceRegistry : global::Unity.Services.Core.Internal.IServiceRegistry
	{
		private const string k_ErrorMessage = "Service registration has been locked. Make sure to register service services before all packages have finished initializing.";

		[global::JetBrains.Annotations.NotNull]
		internal global::Unity.Services.Core.Internal.IServiceRegistry Registry { get; }

		public LockedServiceRegistry([global::JetBrains.Annotations.NotNull] global::Unity.Services.Core.Internal.IServiceRegistry registryToLock)
		{
			Registry = registryToLock;
		}

		public void RegisterService<T>(T service)
		{
			throw new global::System.InvalidOperationException("Service registration has been locked. Make sure to register service services before all packages have finished initializing.");
		}

		public T GetService<T>()
		{
			return Registry.GetService<T>();
		}
	}
}
