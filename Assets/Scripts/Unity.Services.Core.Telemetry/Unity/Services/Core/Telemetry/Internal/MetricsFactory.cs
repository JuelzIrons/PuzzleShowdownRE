namespace Unity.Services.Core.Telemetry.Internal
{
	internal class MetricsFactory : global::Unity.Services.Core.Telemetry.Internal.IMetricsFactory, global::Unity.Services.Core.Internal.IServiceComponent
	{
		public global::System.Collections.Generic.IReadOnlyDictionary<string, string> CommonTags { get; } = new global::System.Collections.Generic.Dictionary<string, string>();

		public global::Unity.Services.Core.Telemetry.Internal.IMetrics Create(string packageName)
		{
			return new global::Unity.Services.Core.Telemetry.Internal.Metrics();
		}
	}
}
