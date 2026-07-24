namespace Unity.Services.Lobbies.Http
{
	internal sealed class ApiTelemetryScope : global::System.IDisposable
	{
		private const string k_RequestLatencyMetric = "http_request_ms";

		private const string k_ApiTag = "api";

		private readonly global::Unity.Services.Core.Telemetry.Internal.IMetrics m_Metrics;

		private readonly global::System.Collections.Generic.Dictionary<string, string> m_Tags;

		private readonly global::System.Diagnostics.Stopwatch m_Stopwatch;

		private bool m_Disposed;

		public ApiTelemetryScope(global::Unity.Services.Core.Telemetry.Internal.IMetrics metrics, string api)
		{
			m_Metrics = metrics;
			m_Tags = new global::System.Collections.Generic.Dictionary<string, string> { { "api", api } };
			m_Stopwatch = new global::System.Diagnostics.Stopwatch();
			m_Stopwatch.Start();
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			global::System.GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (!m_Disposed)
			{
				m_Disposed = true;
				if (disposing)
				{
					m_Stopwatch.Stop();
					m_Metrics.SendHistogramMetric("http_request_ms", m_Stopwatch.ElapsedMilliseconds, m_Tags);
				}
			}
		}
	}
}
