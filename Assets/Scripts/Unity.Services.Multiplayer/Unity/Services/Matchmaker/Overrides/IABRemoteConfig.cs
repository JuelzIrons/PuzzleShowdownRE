namespace Unity.Services.Matchmaker.Overrides
{
	internal interface IABRemoteConfig
	{
		global::System.Collections.Generic.List<global::Unity.Services.Matchmaker.Models.Override> Overrides { get; }

		string AssignmentId { get; }

		global::System.Threading.Tasks.Task RefreshGameOverridesAsync();
	}
}
