namespace Unity.Services.Core.Configuration
{
	[global::System.Serializable]
	internal struct SerializableProjectConfiguration
	{
		[global::Newtonsoft.Json.JsonRequired]
		[global::UnityEngine.SerializeField]
		internal string[] Keys;

		[global::Newtonsoft.Json.JsonRequired]
		[global::UnityEngine.SerializeField]
		internal global::Unity.Services.Core.Configuration.ConfigurationEntry[] Values;

		public static global::Unity.Services.Core.Configuration.SerializableProjectConfiguration Empty => new global::Unity.Services.Core.Configuration.SerializableProjectConfiguration
		{
			Keys = global::System.Array.Empty<string>(),
			Values = global::System.Array.Empty<global::Unity.Services.Core.Configuration.ConfigurationEntry>()
		};

		public SerializableProjectConfiguration(global::System.Collections.Generic.IDictionary<string, global::Unity.Services.Core.Configuration.ConfigurationEntry> configValues)
		{
			Keys = new string[configValues.Count];
			Values = new global::Unity.Services.Core.Configuration.ConfigurationEntry[configValues.Count];
			int num = 0;
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::Unity.Services.Core.Configuration.ConfigurationEntry> configValue in configValues)
			{
				Keys[num] = configValue.Key;
				Values[num] = configValue.Value;
				num++;
			}
		}
	}
}
