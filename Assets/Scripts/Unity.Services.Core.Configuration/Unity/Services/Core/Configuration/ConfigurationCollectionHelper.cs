namespace Unity.Services.Core.Configuration
{
	internal static class ConfigurationCollectionHelper
	{
		public static void FillWith(this global::System.Collections.Generic.IDictionary<string, global::Unity.Services.Core.Configuration.ConfigurationEntry> self, global::Unity.Services.Core.Configuration.SerializableProjectConfiguration config)
		{
			for (int i = 0; i < config.Keys.Length; i++)
			{
				string key = config.Keys[i];
				global::Unity.Services.Core.Configuration.ConfigurationEntry entry = config.Values[i];
				self.SetOrCreateEntry(key, entry);
			}
		}

		public static void FillWith(this global::System.Collections.Generic.IDictionary<string, global::Unity.Services.Core.Configuration.ConfigurationEntry> self, global::Unity.Services.Core.InitializationOptions options)
		{
			foreach (global::System.Collections.Generic.KeyValuePair<string, object> value in options.Values)
			{
				string text = global::System.Convert.ToString(value.Value, global::System.Globalization.CultureInfo.InvariantCulture);
				self.SetOrCreateEntry(value.Key, text);
			}
		}

		private static void SetOrCreateEntry(this global::System.Collections.Generic.IDictionary<string, global::Unity.Services.Core.Configuration.ConfigurationEntry> self, string key, global::Unity.Services.Core.Configuration.ConfigurationEntry entry)
		{
			if (self.TryGetValue(key, out var value))
			{
				if (!value.TrySetValue(entry))
				{
					global::Unity.Services.Core.Internal.CoreLogger.LogWarning("You are attempting to initialize Operate Solution SDK with an option \"" + key + "\" which is readonly at runtime and can be modified only through Project Settings. The value provided as initialization option will be ignored. Please update InitializationOptions in order to remove this warning.");
				}
			}
			else
			{
				self[key] = entry;
			}
		}
	}
}
