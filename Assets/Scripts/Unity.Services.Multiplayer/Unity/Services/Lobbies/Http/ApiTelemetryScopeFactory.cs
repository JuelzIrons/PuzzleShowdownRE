namespace Unity.Services.Lobbies.Http
{
	internal class ApiTelemetryScopeFactory
	{
		private readonly global::Unity.Services.Core.Telemetry.Internal.IMetrics m_Metrics;

		public ApiTelemetryScopeFactory(global::Unity.Services.Core.Telemetry.Internal.IMetrics metrics)
		{
			m_Metrics = metrics;
		}

		public global::Unity.Services.Lobbies.Http.ApiTelemetryScope Instrument(string api)
		{
			return new global::Unity.Services.Lobbies.Http.ApiTelemetryScope(m_Metrics, api);
		}
	}
}
