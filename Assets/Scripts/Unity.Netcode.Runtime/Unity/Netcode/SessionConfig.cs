namespace Unity.Netcode
{
	internal class SessionConfig
	{
		public const uint NoFeatureCompatibility = 0u;

		public const uint BypassFeatureCompatible = 1u;

		public const uint ServerDistributionCompatible = 2u;

		public const uint SessionStateToken = 3u;

		public const uint NetworkBehaviourSerializationSafety = 4u;

		public const uint FixConnectionFlow = 5u;

		internal uint SessionVersion;

		public bool ServiceSideDistribution;

		public static uint PackageSessionVersion => 5u;

		public SessionConfig(global::Unity.Netcode.ServiceConfig serviceConfig)
		{
			SessionVersion = serviceConfig.SessionVersion;
			ServiceSideDistribution = serviceConfig.ServerRedistribution;
		}

		public SessionConfig(uint version)
		{
			SessionVersion = version;
			ServiceSideDistribution = false;
		}

		public SessionConfig()
		{
			SessionVersion = PackageSessionVersion;
			ServiceSideDistribution = false;
		}
	}
}
