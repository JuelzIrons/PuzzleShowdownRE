namespace Unity.Services.Matchmaker.Overrides
{
	internal class ABAnalytics : global::Unity.Services.Matchmaker.Overrides.IABAnalytics
	{
		private readonly string _projectId;

		private readonly string _environmentId;

		private readonly global::Unity.Services.Core.Analytics.Internal.IAnalyticsStandardEventComponent _analyticsService;

		public ABAnalytics(string projectId, string environmentId, global::Unity.Services.Core.Analytics.Internal.IAnalyticsStandardEventComponent analyticsService)
		{
			_projectId = projectId;
			_environmentId = environmentId;
			_analyticsService = analyticsService;
		}

		public void SubmitUserAssignmentConfirmedEvent(string rcVariantID, string rcAssignmentID)
		{
			global::System.Collections.Generic.Dictionary<string, object> eventParameters = new global::System.Collections.Generic.Dictionary<string, object>
			{
				["sdkMethod"] = "Matchmaker.ABTests.ABAnalytics.userAssignmentConfirmed",
				["projectID"] = _projectId,
				["rcVariantID"] = rcVariantID,
				["rcAssignmentID"] = rcAssignmentID,
				["rcEnvironmentID"] = _environmentId,
				["assignmentSource"] = "matchmaker"
			};
			_analyticsService?.Record("userAssignmentConfirmed", eventParameters, 1, "com.unity.services.multiplayer");
		}
	}
}
