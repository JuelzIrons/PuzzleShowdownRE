namespace Unity.Services.Core.Configuration
{
	internal class ProjectConfiguration : global::Unity.Services.Core.Configuration.Internal.IProjectConfiguration, global::Unity.Services.Core.Internal.IServiceComponent
	{
		private string m_JsonCache;

		private readonly global::System.Collections.Generic.IReadOnlyDictionary<string, global::Unity.Services.Core.Configuration.ConfigurationEntry> m_ConfigValues;

		internal global::Unity.Services.Core.Internal.Serialization.IJsonSerializer Serializer { get; }

		public ProjectConfiguration(global::System.Collections.Generic.IReadOnlyDictionary<string, global::Unity.Services.Core.Configuration.ConfigurationEntry> configValues, global::Unity.Services.Core.Internal.Serialization.IJsonSerializer serializer)
		{
			m_ConfigValues = configValues;
			Serializer = serializer;
		}

		public bool GetBool(string key, bool defaultValue = false)
		{
			if (!bool.TryParse(GetString(key), out var result))
			{
				return defaultValue;
			}
			return result;
		}

		public int GetInt(string key, int defaultValue = 0)
		{
			if (!int.TryParse(GetString(key), out var result))
			{
				return defaultValue;
			}
			return result;
		}

		public float GetFloat(string key, float defaultValue = 0f)
		{
			if (!float.TryParse(GetString(key), global::System.Globalization.NumberStyles.Float, global::System.Globalization.CultureInfo.InvariantCulture, out var result))
			{
				return defaultValue;
			}
			return result;
		}

		public string GetString(string key, string defaultValue = null)
		{
			if (!m_ConfigValues.TryGetValue(key, out var value))
			{
				return defaultValue;
			}
			return value.Value;
		}

		public string ToJson()
		{
			if (m_JsonCache == null)
			{
				global::System.Collections.Generic.Dictionary<string, string> value = global::System.Linq.Enumerable.ToDictionary(m_ConfigValues, (global::System.Collections.Generic.KeyValuePair<string, global::Unity.Services.Core.Configuration.ConfigurationEntry> pair) => pair.Key, (global::System.Collections.Generic.KeyValuePair<string, global::Unity.Services.Core.Configuration.ConfigurationEntry> pair) => pair.Value.Value);
				m_JsonCache = Serializer.SerializeObject(value);
			}
			return m_JsonCache;
		}
	}
}
