namespace Unity.Services.Core.Telemetry.Internal
{
	public interface IDiagnosticsFactory : global::Unity.Services.Core.Internal.IServiceComponent
	{
		global::System.Collections.Generic.IReadOnlyDictionary<string, string> CommonTags { get; }

		global::Unity.Services.Core.Telemetry.Internal.IDiagnostics Create(string packageName);
	}
}
