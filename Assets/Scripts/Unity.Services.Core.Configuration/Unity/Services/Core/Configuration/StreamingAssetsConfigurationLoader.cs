namespace Unity.Services.Core.Configuration
{
	internal class StreamingAssetsConfigurationLoader : global::Unity.Services.Core.Configuration.IConfigurationLoader
	{
		private readonly global::Unity.Services.Core.Internal.Serialization.IJsonSerializer m_Serializer;

		public StreamingAssetsConfigurationLoader(global::Unity.Services.Core.Internal.Serialization.IJsonSerializer serializer)
		{
			m_Serializer = serializer;
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Core.Configuration.SerializableProjectConfiguration> GetConfigAsync()
		{
			string value = await global::Unity.Services.Core.Configuration.StreamingAssetsUtils.GetFileTextFromStreamingAssetsAsync("UnityServicesProjectConfiguration.json");
			return m_Serializer.DeserializeObject<global::Unity.Services.Core.Configuration.SerializableProjectConfiguration>(value);
		}
	}
}
