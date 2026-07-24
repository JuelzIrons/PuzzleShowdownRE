namespace Unity.Services.Relay
{
	internal class Configuration
	{
		public string BasePath;

		public int? RequestTimeout;

		public int? NumberOfRetries;

		public global::System.Collections.Generic.IDictionary<string, string> Headers;

		public Configuration(string basePath, int? requestTimeout, int? numRetries, global::System.Collections.Generic.IDictionary<string, string> headers)
		{
			BasePath = basePath;
			RequestTimeout = requestTimeout;
			NumberOfRetries = numRetries;
			if (headers == null)
			{
				Headers = headers;
			}
			else
			{
				Headers = new global::System.Collections.Generic.Dictionary<string, string>(headers);
			}
		}

		public static global::Unity.Services.Relay.Configuration MergeConfigurations(global::Unity.Services.Relay.Configuration a, global::Unity.Services.Relay.Configuration b)
		{
			if (a == null || b == null)
			{
				return a ?? b;
			}
			global::Unity.Services.Relay.Configuration configuration = new global::Unity.Services.Relay.Configuration(a.BasePath, a.RequestTimeout, a.NumberOfRetries, a.Headers);
			if (configuration.BasePath == null)
			{
				configuration.BasePath = b.BasePath;
			}
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (b.Headers != null)
			{
				foreach (global::System.Collections.Generic.KeyValuePair<string, string> header in b.Headers)
				{
					dictionary[header.Key] = header.Value;
				}
			}
			if (configuration.Headers != null)
			{
				foreach (global::System.Collections.Generic.KeyValuePair<string, string> header2 in configuration.Headers)
				{
					dictionary[header2.Key] = header2.Value;
				}
			}
			configuration.Headers = dictionary;
			configuration.RequestTimeout = configuration.RequestTimeout ?? b.RequestTimeout;
			configuration.NumberOfRetries = configuration.NumberOfRetries ?? b.NumberOfRetries;
			return configuration;
		}
	}
}
