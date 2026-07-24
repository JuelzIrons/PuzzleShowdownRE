namespace Unity.Services.Core.Internal
{
	internal class ServiceRegistry : global::Unity.Services.Core.Internal.IServiceRegistry
	{
		[global::JetBrains.Annotations.NotNull]
		internal global::System.Collections.Generic.Dictionary<int, object> ServiceTypeHashToInstance { get; }

		public ServiceRegistry()
		{
			ServiceTypeHashToInstance = new global::System.Collections.Generic.Dictionary<int, object>();
		}

		public ServiceRegistry([global::JetBrains.Annotations.NotNull] global::System.Collections.Generic.Dictionary<int, object> serviceTypeHashToInstance)
		{
			ServiceTypeHashToInstance = serviceTypeHashToInstance;
		}

		public void RegisterService<T>(T service)
		{
			global::System.Type typeFromHandle = typeof(T);
			if (service.GetType() == typeFromHandle)
			{
				throw new global::System.ArgumentException("Interface type of service not specified.");
			}
			int hashCode = typeFromHandle.GetHashCode();
			ServiceTypeHashToInstance[hashCode] = service;
		}

		public T GetService<T>()
		{
			global::System.Type typeFromHandle = typeof(T);
			if (!ServiceTypeHashToInstance.TryGetValue(typeFromHandle.GetHashCode(), out var value))
			{
				return default(T);
			}
			return (T)value;
		}
	}
}
