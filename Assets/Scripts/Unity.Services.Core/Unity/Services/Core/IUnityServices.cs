namespace Unity.Services.Core
{
	public interface IUnityServices
	{
		global::Unity.Services.Core.ServicesInitializationState State { get; }

		event global::System.Action Initialized;

		event global::System.Action<global::System.Exception> InitializeFailed;

		global::System.Threading.Tasks.Task InitializeAsync(global::Unity.Services.Core.InitializationOptions options = null);

		string GetIdentifier()
		{
			return null;
		}

		T GetService<T>();
	}
}
