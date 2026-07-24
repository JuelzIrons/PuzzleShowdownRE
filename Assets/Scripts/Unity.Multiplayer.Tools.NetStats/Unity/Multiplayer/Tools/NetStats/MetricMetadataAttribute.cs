namespace Unity.Multiplayer.Tools.NetStats
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Field)]
	public class MetricMetadataAttribute : global::System.Attribute
	{
		public string DisplayName { get; set; }

		public global::Unity.Multiplayer.Tools.NetStats.MetricKind MetricKind { get; set; }

		public global::Unity.Multiplayer.Tools.NetStats.Units Units { get; set; }

		public bool DisplayAsPercentage { get; set; }
	}
}
