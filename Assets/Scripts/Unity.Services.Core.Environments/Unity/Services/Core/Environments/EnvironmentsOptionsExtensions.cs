namespace Unity.Services.Core.Environments
{
	public static class EnvironmentsOptionsExtensions
	{
		internal const string EnvironmentNameKey = "com.unity.services.core.environment-name";

		internal const string EnvironmentDefaultNameValue = "production";

		public static global::Unity.Services.Core.InitializationOptions SetEnvironmentName(this global::Unity.Services.Core.InitializationOptions self, string environmentName)
		{
			if (string.IsNullOrEmpty(environmentName))
			{
				throw new global::System.ArgumentException("Environment name cannot be null or empty.", "environmentName");
			}
			self.SetOption("com.unity.services.core.environment-name", environmentName);
			return self;
		}
	}
}
