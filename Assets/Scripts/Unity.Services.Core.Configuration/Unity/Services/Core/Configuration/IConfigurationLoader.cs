namespace Unity.Services.Core.Configuration
{
	internal interface IConfigurationLoader
	{
		global::System.Threading.Tasks.Task<global::Unity.Services.Core.Configuration.SerializableProjectConfiguration> GetConfigAsync();
	}
}
