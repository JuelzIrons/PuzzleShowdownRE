namespace Unity.Services.Core.Threading.Internal
{
	public interface IUnityThreadUtils : global::Unity.Services.Core.Internal.IServiceComponent
	{
		bool IsRunningOnUnityThread { get; }

		global::System.Threading.Tasks.Task PostAsync([global::JetBrains.Annotations.NotNull] global::System.Action action);

		global::System.Threading.Tasks.Task PostAsync([global::JetBrains.Annotations.NotNull] global::System.Action<object> action, object state);

		global::System.Threading.Tasks.Task<T> PostAsync<T>([global::JetBrains.Annotations.NotNull] global::System.Func<T> action);

		global::System.Threading.Tasks.Task<T> PostAsync<T>([global::JetBrains.Annotations.NotNull] global::System.Func<object, T> action, object state);

		void Send([global::JetBrains.Annotations.NotNull] global::System.Action action);

		void Send([global::JetBrains.Annotations.NotNull] global::System.Action<object> action, object state);

		T Send<T>([global::JetBrains.Annotations.NotNull] global::System.Func<T> action);

		T Send<T>([global::JetBrains.Annotations.NotNull] global::System.Func<object, T> action, object state);
	}
}
