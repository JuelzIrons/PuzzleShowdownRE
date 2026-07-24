namespace Unity.Services.Core.Telemetry.Internal
{
	internal class Metrics : global::Unity.Services.Core.Telemetry.Internal.IMetrics
	{
		internal global::System.Collections.Generic.IDictionary<string, string> PackageTags { get; } = new global::System.Collections.Generic.Dictionary<string, string>();

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
