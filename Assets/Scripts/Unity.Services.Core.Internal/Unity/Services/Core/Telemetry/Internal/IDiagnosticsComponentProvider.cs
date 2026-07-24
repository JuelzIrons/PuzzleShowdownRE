namespace Unity.Services.Core.Telemetry.Internal
{
	internal interface IDiagnosticsComponentProvider
	{
		global::System.Threading.Tasks.Task<global::Unity.Services.Core.Telemetry.Internal.IDiagnosticsFactory> CreateDiagnosticsComponents();

		global::System.Threading.Tasks.Task<string> GetSerializedProjectConfigurationAsync();
	}
}
