namespace Unity.Services.Authentication.Shared
{
	internal class ApiConfiguration : global::Unity.Services.Authentication.Shared.IApiConfiguration
	{
		public const string ISO8601_DATETIME_FORMAT = "o";

		private string _basePath;

		private global::System.Collections.Generic.IDictionary<string, string> _apiKey;

		private global::System.Collections.Generic.IDictionary<string, string> _apiKeyPrefix;

		private string _dateTimeFormat = "o";

		private string _tempFolderPath = global::System.IO.Path.GetTempPath();

		public virtual string BasePath
		{
			get
			{
				return _basePath;
			}
			set
			{
				_basePath = value;
			}
		}

		public virtual global::System.Collections.Generic.IDictionary<string, string> DefaultHeaders { get; set; }

		public virtual int Timeout { get; set; }

		public virtual string UserAgent { get; set; }

		public virtual string Username { get; set; }

		public virtual string Password { get; set; }

		public virtual string AccessToken { get; set; }

		public virtual string DateTimeFormat
		{
			get
			{
				return _dateTimeFormat;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					_dateTimeFormat = "o";
				}
				else
				{
					_dateTimeFormat = value;
				}
			}
		}

		public virtual global::System.Collections.Generic.IDictionary<string, string> ApiKeyPrefix
		{
			get
			{
				return _apiKeyPrefix;
			}
			set
			{
				if (value == null)
				{
					throw new global::System.InvalidOperationException("ApiKeyPrefix collection may not be null.");
				}
				_apiKeyPrefix = value;
			}
		}

		public virtual global::System.Collections.Generic.IDictionary<string, string> ApiKey
		{
			get
			{
				return _apiKey;
			}
			set
			{
				if (value == null)
				{
					throw new global::System.InvalidOperationException("ApiKey collection may not be null.");
				}
				_apiKey = value;
			}
		}

		public ApiConfiguration()
		{
			UserAgent = global::System.Net.WebUtility.UrlEncode("openapi-generator/csharp");
			DefaultHeaders = new global::System.Collections.Concurrent.ConcurrentDictionary<string, string>();
			ApiKey = new global::System.Collections.Concurrent.ConcurrentDictionary<string, string>();
			ApiKeyPrefix = new global::System.Collections.Concurrent.ConcurrentDictionary<string, string>();
			Timeout = 10;
		}

		public ApiConfiguration(global::System.Collections.Generic.IDictionary<string, string> defaultHeaders, global::System.Collections.Generic.IDictionary<string, string> apiKey, global::System.Collections.Generic.IDictionary<string, string> apiKeyPrefix, string basePath)
			: this()
		{
			if (string.IsNullOrWhiteSpace(basePath))
			{
				throw new global::System.ArgumentException("The provided basePath is invalid.", "basePath");
			}
			if (defaultHeaders == null)
			{
				throw new global::System.ArgumentNullException("defaultHeaders");
			}
			if (apiKey == null)
			{
				throw new global::System.ArgumentNullException("apiKey");
			}
			if (apiKeyPrefix == null)
			{
				throw new global::System.ArgumentNullException("apiKeyPrefix");
			}
			BasePath = basePath;
			foreach (global::System.Collections.Generic.KeyValuePair<string, string> defaultHeader in defaultHeaders)
			{
				DefaultHeaders.Add(defaultHeader);
			}
			foreach (global::System.Collections.Generic.KeyValuePair<string, string> item in apiKey)
			{
				ApiKey.Add(item);
			}
			foreach (global::System.Collections.Generic.KeyValuePair<string, string> item2 in apiKeyPrefix)
			{
				ApiKeyPrefix.Add(item2);
			}
		}

		public string GetApiKeyWithPrefix(string apiKeyIdentifier)
		{
			ApiKey.TryGetValue(apiKeyIdentifier, out var value);
			if (ApiKeyPrefix.TryGetValue(apiKeyIdentifier, out var value2))
			{
				return value2 + " " + value;
			}
			return value;
		}

		public void AddApiKey(string key, string value)
		{
			ApiKey[key] = value;
		}

		public void AddApiKeyPrefix(string key, string value)
		{
			ApiKeyPrefix[key] = value;
		}
	}
}
