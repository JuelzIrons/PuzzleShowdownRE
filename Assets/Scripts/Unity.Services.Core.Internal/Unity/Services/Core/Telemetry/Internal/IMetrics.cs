namespace Unity.Services.Core.Telemetry.Internal
{
	public interface IMetrics
	{
		void SendGaugeMetric(string name, double value = 0.0, global::System.Collections.Generic.IDictionary<string, string> tags = null);

		void SendHistogramMetric(string name, double time, global::System.Collections.Generic.IDictionary<string, string> tags = null);

		void SendSumMetric(string name, double value = 1.0, global::System.Collections.Generic.IDictionary<string, string> tags = null);
	}
}
