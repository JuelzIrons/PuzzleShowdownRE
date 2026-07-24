namespace Unity.Services.Core.Telemetry.Internal
{
	public interface IDiagnostics
	{
		void SendDiagnostic(string name, string message, global::System.Collections.Generic.IDictionary<string, string> tags = null);
	}
}
