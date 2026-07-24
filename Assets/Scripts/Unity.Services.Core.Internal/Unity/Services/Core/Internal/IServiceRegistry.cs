namespace Unity.Services.Core.Internal
{
	internal interface IServiceRegistry
	{
		void RegisterService<T>([global::JetBrains.Annotations.NotNull] T service);

		T GetService<T>();
	}
}
