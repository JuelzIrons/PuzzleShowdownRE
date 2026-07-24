namespace Unity.Services.Authentication.Shared
{
	internal interface IApiConfiguration
	{
		string AccessToken { get; set; }

		global::System.Collections.Generic.IDictionary<string, string> ApiKey { get; set; }

		global::System.Collections.Generic.IDictionary<string, string> ApiKeyPrefix { get; set; }

		global::System.Collections.Generic.IDictionary<string, string> DefaultHeaders { get; set; }

		string BasePath { get; set; }

		string DateTimeFormat { get; set; }

		int Timeout { get; set; }

		string UserAgent { get; set; }

		string Username { get; set; }

		string Password { get; set; }

		string GetApiKeyWithPrefix(string apiKeyIdentifier);
	}
}
