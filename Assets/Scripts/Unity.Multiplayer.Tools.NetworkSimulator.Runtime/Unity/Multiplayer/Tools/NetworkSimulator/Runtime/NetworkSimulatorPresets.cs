namespace Unity.Multiplayer.Tools.NetworkSimulator.Runtime
{
	public static class NetworkSimulatorPresets
	{
		private const string k_BroadbandDescription = "Typical of desktop and console platforms (and generally speaking most mobile players too).";

		private const string k_OpticalDescription = "Best case scenario for desktop and console platforms. Excellent ping, excellent jitter and no packet loss.";

		private const string k_CableDescription = "Optimal scenario for desktop and console platforms. Very good ping, very good jitter and no packet loss.";

		private const string k_DslDescription = "Average scenario for desktop and console platforms. Good ping, good jitter and no packet loss.";

		private const string k_SatelliteDescription = "Low Earth orbit satellite network scenario for desktop and console platforms. Bad ping, good jitter and no packet loss.";

		private const string k_CongestedNetworkDescription = "Desktop and console platforms trough congested networks (multiple downloads or streaming already using the network's maximum capacity). Medium ping, bad jitter and little packet loss.";

		private const string k_PoorMobileDescription = "Extremely poor connection, completely unsuitable for synchronous multiplayer gaming due to exceptionally high ping. Turn based games may work.";

		private const string k_MediumMobileDescription = "This is the minimum supported mobile connection for synchronous gameplay. Expect high pings, jitter, stuttering and packet loss.";

		private const string k_DecentMobileDescription = "Suitable for synchronous multiplayer, except that ping (and overall connection quality and stability) may be quite poor.\n\nExpect to handle players dropping all packets in bursts of 1-60s. I.e. Ensure you handle reconnections.";

		private const string k_GoodMobileDescription = "In many places, expect this to be 'as good as' or 'better than' home broadband.";

		public static readonly global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset None;

		public static readonly global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset HomeBroadband;

		public static readonly global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset HomeOptical;

		public static readonly global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset HomeCable;

		public static readonly global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset HomeDSL;

		public static readonly global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset HomeSatellite;

		public static readonly global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset HomeCongestedNetwork;

		public static readonly global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset Mobile2G;

		public static readonly global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset Mobile2_5G;

		public static readonly global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset Mobile2_75G;

		public static readonly global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset Mobile3G;

		public static readonly global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset Mobile3_5G;

		public static readonly global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset Mobile3_75G;

		public static readonly global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset Mobile4G;

		public static readonly global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset Mobile4_5G;

		public static readonly global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset Mobile5G;

		public static readonly global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset[] Values;

		internal static readonly string[] Names;

		static NetworkSimulatorPresets()
		{
			None = global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset.Create("None", string.Empty);
			HomeBroadband = global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset.Create("Home Broadband [WIFI, Cable, Console, PC]", "Typical of desktop and console platforms (and generally speaking most mobile players too).", 32, 12, 0, 2);
			HomeOptical = global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset.Create("Home Fiber [Best real-world scenario]", "Best case scenario for desktop and console platforms. Excellent ping, excellent jitter and no packet loss.", 10, 1);
			HomeCable = global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset.Create("Home Cable [Optimal real-world scenario]", "Optimal scenario for desktop and console platforms. Very good ping, very good jitter and no packet loss.", 25, 5);
			HomeDSL = global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset.Create("Home DSL [ADSL or VDSL]", "Average scenario for desktop and console platforms. Good ping, good jitter and no packet loss.", 30, 10);
			HomeSatellite = global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset.Create("Home Satellite [low Earth orbit]", "Low Earth orbit satellite network scenario for desktop and console platforms. Bad ping, good jitter and no packet loss.", 100, 10);
			HomeCongestedNetwork = global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset.Create("Home Broadband with Congested Network", "Desktop and console platforms trough congested networks (multiple downloads or streaming already using the network's maximum capacity). Medium ping, bad jitter and little packet loss.", 50, 50, 0, 1);
			Mobile2G = global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset.Create("Mobile 2G [CDMA & GSM, '00]", "Extremely poor connection, completely unsuitable for synchronous multiplayer gaming due to exceptionally high ping. Turn based games may work.", 520, 50, 0, 7);
			Mobile2_5G = global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset.Create("Mobile 2.5G [GPRS, G, '00]", "Extremely poor connection, completely unsuitable for synchronous multiplayer gaming due to exceptionally high ping. Turn based games may work.", 480, 40, 0, 7);
			Mobile2_75G = global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset.Create("Mobile 2.75G [Edge, E, '06]", "Extremely poor connection, completely unsuitable for synchronous multiplayer gaming due to exceptionally high ping. Turn based games may work.", 440, 40, 0, 7);
			Mobile3G = global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset.Create("Mobile 3G [WCDMA & UMTS, '03]", "Extremely poor connection, completely unsuitable for synchronous multiplayer gaming due to exceptionally high ping. Turn based games may work.", 360, 30, 0, 7);
			Mobile3_5G = global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset.Create("Mobile 3.5G [HSDPA, H, '06]", "This is the minimum supported mobile connection for synchronous gameplay. Expect high pings, jitter, stuttering and packet loss.", 160, 30, 0, 6);
			Mobile3_75G = global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset.Create("Mobile 3.75G [HDSDPA+, H+, '11]", "Suitable for synchronous multiplayer, except that ping (and overall connection quality and stability) may be quite poor.\n\nExpect to handle players dropping all packets in bursts of 1-60s. I.e. Ensure you handle reconnections.", 130, 30, 0, 6);
			Mobile4G = global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset.Create("Mobile 4G [4G, LTE, '13]", "Suitable for synchronous multiplayer, except that ping (and overall connection quality and stability) may be quite poor.\n\nExpect to handle players dropping all packets in bursts of 1-60s. I.e. Ensure you handle reconnections.", 100, 20, 0, 4);
			Mobile4_5G = global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset.Create("Mobile 4.5G [4G+, LTE-A, '16]", "Suitable for synchronous multiplayer, except that ping (and overall connection quality and stability) may be quite poor.\n\nExpect to handle players dropping all packets in bursts of 1-60s. I.e. Ensure you handle reconnections.", 80, 20, 0, 4);
			Mobile5G = global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset.Create("Mobile 5G ['20]", "In many places, expect this to be 'as good as' or 'better than' home broadband.", 30, 20, 0, 4);
			Values = new global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset[16]
			{
				None, HomeBroadband, HomeOptical, HomeCable, HomeDSL, HomeSatellite, HomeCongestedNetwork, Mobile2G, Mobile2_5G, Mobile2_75G,
				Mobile3G, Mobile3_5G, Mobile3_75G, Mobile4G, Mobile4_5G, Mobile5G
			};
			Names = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(Values, (global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset v) => v.Name));
		}
	}
}
