namespace Unity.Services.Core.Configuration.Internal
{
	public interface IProjectConfiguration : global::Unity.Services.Core.Internal.IServiceComponent
	{
		bool GetBool(string key, bool defaultValue = false);

		int GetInt(string key, int defaultValue = 0);

		float GetFloat(string key, float defaultValue = 0f);

		string GetString(string key, string defaultValue = null);
	}
}
