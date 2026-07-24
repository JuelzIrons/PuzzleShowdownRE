namespace Unity.Services.Core.Telemetry.Internal
{
	internal class DisabledDiagnosticsFactory : global::Unity.Services.Core.Telemetry.Internal.IDiagnosticsFactory, global::Unity.Services.Core.Internal.IServiceComponent
	{
		global::System.Collections.Generic.IReadOnlyDictionary<string, string> global::Unity.Services.Core.Telemetry.Internal.IDiagnosticsFactory.CommonTags { get; } = new global::System.Collections.Generic.Dictionary<string, string>();

		global::Unity.Services.Core.Telemetry.Internal.IDiagnostics global::Unity.Services.Core.Telemetry.Internal.IDiagnosticsFactory.Create(string packageName)
		{
			return new global::Unity.Services.Core.Telemetry.Internal.DisabledDiagnostics();
		}
	}
}
