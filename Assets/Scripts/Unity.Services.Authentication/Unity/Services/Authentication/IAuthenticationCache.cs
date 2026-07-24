namespace Unity.Services.Authentication
{
	internal interface IAuthenticationCache : global::Unity.Services.Authentication.ICache
	{
		string Profile { get; }

		string CloudProjectId { get; }

		void Migrate(string key);
	}
}
