namespace Unity.Services.Core.Internal
{
	internal class CoreDiagnostics
	{
		internal const string CorePackageName = "com.unity.services.core";

		internal const string CircularDependencyDiagnosticName = "circular_dependency";

		internal const string CorePackageInitDiagnosticName = "core_package_init";

		internal const string OperateServicesInitDiagnosticName = "operate_services_init";

		internal const string ProjectConfigTagName = "project_config";

		public static global::Unity.Services.Core.Internal.CoreDiagnostics Instance { get; internal set; }

		public global::System.Collections.Generic.IDictionary<string, string> CoreTags { get; } = new global::System.Collections.Generic.Dictionary<string, string>();

		internal global::Unity.Services.Core.Telemetry.Internal.IDiagnosticsComponentProvider DiagnosticsComponentProvider { get; set; }

		internal global::Unity.Services.Core.Telemetry.Internal.IDiagnostics Diagnostics { get; set; }

		public void SetProjectConfiguration(string serializedProjectConfig)
		{
		}

		public void SendCircularDependencyDiagnostics(global::System.Exception exception)
		{
		}

		public void SendCorePackageInitDiagnostics(global::System.Exception exception)
		{
		}

		public void SendOperateServicesInitDiagnostics(global::System.Exception exception)
		{
		}

		internal async global::System.Threading.Tasks.Task SendCoreDiagnosticsAsync(string diagnosticName, global::System.Exception exception)
		{
			await global::System.Threading.Tasks.Task.CompletedTask;
		}

		private static void OnSendFailed(global::System.Threading.Tasks.Task failedSendTask)
		{
		}

		internal async global::System.Threading.Tasks.Task<global::Unity.Services.Core.Telemetry.Internal.IDiagnostics> GetOrCreateDiagnosticsAsync()
		{
			if (Diagnostics == null)
			{
				Diagnostics = (await DiagnosticsComponentProvider.CreateDiagnosticsComponents()).Create("com.unity.services.core");
			}
			return Diagnostics;
		}
	}
}
