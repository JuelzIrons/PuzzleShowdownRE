namespace Unity.Multiplayer.Tools.NetVis.Configuration
{
	[global::System.Serializable]
	internal class NetVisConfiguration
	{
		public global::Unity.Multiplayer.Tools.NetVis.Configuration.NetVisMetric Metric { get; set; }

		public bool MeshShadingEnabled => Metric switch
		{
			global::Unity.Multiplayer.Tools.NetVis.Configuration.NetVisMetric.Bandwidth => Settings.Bandwidth.MeshShadingEnabled, 
			global::Unity.Multiplayer.Tools.NetVis.Configuration.NetVisMetric.Ownership => Settings.Ownership.MeshShadingEnabled, 
			_ => false, 
		};

		public bool TextOverlayEnabled => Metric switch
		{
			global::Unity.Multiplayer.Tools.NetVis.Configuration.NetVisMetric.Bandwidth => Settings.Bandwidth.TextOverlayEnabled, 
			global::Unity.Multiplayer.Tools.NetVis.Configuration.NetVisMetric.Ownership => Settings.Ownership.TextOverlayEnabled, 
			_ => false, 
		};

		public global::Unity.Multiplayer.Tools.NetVis.Configuration.NetVisSettings Settings { get; } = new global::Unity.Multiplayer.Tools.NetVis.Configuration.NetVisSettings();
	}
}
