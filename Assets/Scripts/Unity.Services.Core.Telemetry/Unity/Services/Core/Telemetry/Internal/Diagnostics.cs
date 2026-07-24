namespace Unity.Services.Core.Telemetry.Internal
{
	internal class Diagnostics : global::Unity.Services.Core.Telemetry.Internal.IDiagnostics
	{
		internal global::System.Collections.Generic.IDictionary<string, string> PackageTags { get; } = new global::System.Collections.Generic.Dictionary<string, string>();

		public void SendDiagnostic(string name, string message, global::System.Collections.Generic.IDictionary<string, string> tags = null)
		{
		}
	}
}
