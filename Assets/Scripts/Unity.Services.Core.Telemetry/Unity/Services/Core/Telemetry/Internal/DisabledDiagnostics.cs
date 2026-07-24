namespace Unity.Services.Core.Telemetry.Internal
{
	internal class DisabledDiagnostics : global::Unity.Services.Core.Telemetry.Internal.IDiagnostics
	{
		void global::Unity.Services.Core.Telemetry.Internal.IDiagnostics.SendDiagnostic(string name, string message, global::System.Collections.Generic.IDictionary<string, string> tags)
		{
		}
	}
}
