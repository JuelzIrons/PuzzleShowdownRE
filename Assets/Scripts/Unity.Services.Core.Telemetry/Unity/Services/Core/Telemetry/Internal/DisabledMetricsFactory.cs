namespace Unity.Services.Core.Telemetry.Internal
{
	internal class DisabledMetricsFactory : global::Unity.Services.Core.Telemetry.Internal.IMetricsFactory, global::Unity.Services.Core.Internal.IServiceComponent
	{
		global::System.Collections.Generic.IReadOnlyDictionary<string, string> global::Unity.Services.Core.Telemetry.Internal.IMetricsFactory.CommonTags { get; } = new global::System.Collections.Generic.Dictionary<string, string>();

		global::Unity.Services.Core.Telemetry.Internal.IMetrics global::Unity.Services.Core.Telemetry.Internal.IMetricsFactory.Create(string packageName)
		{
			return new global::Unity.Services.Core.Telemetry.Internal.DisabledMetrics();
		}
	}
}
