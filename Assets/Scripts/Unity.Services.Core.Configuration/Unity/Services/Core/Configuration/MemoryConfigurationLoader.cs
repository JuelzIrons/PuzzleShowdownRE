namespace Unity.Services.Core.Configuration
{
	internal class MemoryConfigurationLoader : global::Unity.Services.Core.Configuration.IConfigurationLoader
	{
		public global::Unity.Services.Core.Configuration.SerializableProjectConfiguration Config { get; set; }

		global::System.Threading.Tasks.Task<global::Unity.Services.Core.Configuration.SerializableProjectConfiguration> global::Unity.Services.Core.Configuration.IConfigurationLoader.GetConfigAsync()
		{
			global::System.Threading.Tasks.TaskCompletionSource<global::Unity.Services.Core.Configuration.SerializableProjectConfiguration> taskCompletionSource = new global::System.Threading.Tasks.TaskCompletionSource<global::Unity.Services.Core.Configuration.SerializableProjectConfiguration>();
			taskCompletionSource.SetResult(Config);
			return taskCompletionSource.Task;
		}
	}
}
