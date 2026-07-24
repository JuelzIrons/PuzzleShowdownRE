namespace Unity.Services.Core.Telemetry.Internal
{
	public interface IMetricsFactory : global::Unity.Services.Core.Internal.IServiceComponent
	{
		global::System.Collections.Generic.IReadOnlyDictionary<string, string> CommonTags { get; }

		global::Unity.Services.Core.Telemetry.Internal.IMetrics Create(string packageName);
	}
}
