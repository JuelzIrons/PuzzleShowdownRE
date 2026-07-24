namespace Unity.Services.Core.Telemetry.Internal
{
	internal class DisabledMetrics : global::Unity.Services.Core.Telemetry.Internal.IMetrics
	{
		void global::Unity.Services.Core.Telemetry.Internal.IMetrics.SendGaugeMetric(string name, double value, global::System.Collections.Generic.IDictionary<string, string> tags)
		{
		}

		void global::Unity.Services.Core.Telemetry.Internal.IMetrics.SendHistogramMetric(string name, double time, global::System.Collections.Generic.IDictionary<string, string> tags)
		{
		}

		void global::Unity.Services.Core.Telemetry.Internal.IMetrics.SendSumMetric(string name, double value, global::System.Collections.Generic.IDictionary<string, string> tags)
		{
		}
	}
}
