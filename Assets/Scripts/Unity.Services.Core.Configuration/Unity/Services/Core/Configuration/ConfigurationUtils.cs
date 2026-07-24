namespace Unity.Services.Core.Configuration
{
	internal static class ConfigurationUtils
	{
		public const string ConfigFileName = "UnityServicesProjectConfiguration.json";

		public static global::Unity.Services.Core.Configuration.IConfigurationLoader ConfigurationLoader { get; internal set; } = new global::Unity.Services.Core.Configuration.StreamingAssetsConfigurationLoader(new global::Unity.Services.Core.Internal.Serialization.NewtonsoftSerializer());
	}
}
