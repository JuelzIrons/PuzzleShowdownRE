namespace UnityEngine.AdaptivePerformance
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class)]
	public sealed class AdaptivePerformanceConfigurationDataAttribute : global::System.Attribute
	{
		public string displayName { get; set; }

		public string buildSettingsKey { get; set; }

		private AdaptivePerformanceConfigurationDataAttribute()
		{
		}

		public AdaptivePerformanceConfigurationDataAttribute(string displayName, string buildSettingsKey)
		{
			this.displayName = displayName;
			this.buildSettingsKey = buildSettingsKey;
		}
	}
}
