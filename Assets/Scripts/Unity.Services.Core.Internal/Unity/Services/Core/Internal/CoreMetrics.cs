namespace Unity.Services.Core.Internal
{
	internal class CoreMetrics
	{
		internal const string PackageInitTimeMetricName = "package_init_time";

		internal const string AllPackagesInitSuccessMetricName = "all_packages_init_success";

		internal const string AllPackagesInitTimeMetricName = "all_packages_init_time";

		internal const string PackageInitializerNamesKeyFormat = "{0}.initializer-assembly-qualified-names";

		internal const char PackageInitializerNamesSeparator = ';';

		internal const string AllPackageNamesKey = "com.unity.services.core.all-package-names";

		internal const char AllPackageNamesSeparator = ';';

		public static global::Unity.Services.Core.Internal.CoreMetrics Instance { get; internal set; }

		internal global::Unity.Services.Core.Telemetry.Internal.IMetrics Metrics { get; set; }

		internal global::System.Collections.Generic.IDictionary<global::System.Type, global::Unity.Services.Core.Telemetry.Internal.IMetrics> AllPackageMetrics { get; } = new global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.Services.Core.Telemetry.Internal.IMetrics>();

		public void SendAllPackagesInitSuccessMetric()
		{
		}

		public void SendAllPackagesInitTimeMetric(double initTimeSeconds)
		{
		}

		public void SendInitTimeMetricForPackage(global::System.Type packageType, double initTimeSeconds)
		{
		}

		public void Initialize(global::Unity.Services.Core.Configuration.Internal.IProjectConfiguration configuration, global::Unity.Services.Core.Telemetry.Internal.IMetricsFactory factory, global::System.Type corePackageType)
		{
		}

		internal void FindAndCacheAllPackageMetrics(global::Unity.Services.Core.Configuration.Internal.IProjectConfiguration configuration, global::Unity.Services.Core.Telemetry.Internal.IMetricsFactory factory)
		{
		}
	}
}
