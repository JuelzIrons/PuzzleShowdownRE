namespace Unity.Multiplayer.Tools.NetVis.Configuration
{
	internal class NetVisSettings
	{
		public global::Unity.Multiplayer.Tools.NetVis.Configuration.NetVisCommonSettings Common { get; } = new global::Unity.Multiplayer.Tools.NetVis.Configuration.NetVisCommonSettings();

		public global::Unity.Multiplayer.Tools.NetVis.Configuration.BandwidthSettings Bandwidth { get; } = new global::Unity.Multiplayer.Tools.NetVis.Configuration.BandwidthSettings();

		public global::Unity.Multiplayer.Tools.NetVis.Configuration.OwnershipSettings Ownership { get; } = new global::Unity.Multiplayer.Tools.NetVis.Configuration.OwnershipSettings();
	}
}
