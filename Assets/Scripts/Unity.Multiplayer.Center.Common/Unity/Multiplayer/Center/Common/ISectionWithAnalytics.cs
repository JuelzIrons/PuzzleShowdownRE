namespace Unity.Multiplayer.Center.Common
{
	public interface ISectionWithAnalytics
	{
		global::Unity.Multiplayer.Center.Common.Analytics.IOnboardingSectionAnalyticsProvider AnalyticsProvider { get; set; }
	}
}
