namespace Unity.Services.Core.Telemetry.Internal
{
	internal class DiagnosticsFactory : global::Unity.Services.Core.Telemetry.Internal.IDiagnosticsFactory, global::Unity.Services.Core.Internal.IServiceComponent
	{
		public global::System.Collections.Generic.IReadOnlyDictionary<string, string> CommonTags { get; } = new global::System.Collections.Generic.Dictionary<string, string>();

		public global::Unity.Services.Core.Telemetry.Internal.IDiagnostics Create(string packageName)
		{
			return new global::Unity.Services.Core.Telemetry.Internal.Diagnostics();
		}
	}
}
