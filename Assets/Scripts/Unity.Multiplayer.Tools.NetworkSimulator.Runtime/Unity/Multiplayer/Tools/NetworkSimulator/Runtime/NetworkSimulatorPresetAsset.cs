namespace Unity.Multiplayer.Tools.NetworkSimulator.Runtime
{
	[global::UnityEngine.CreateAssetMenu(fileName = "NetworkSimulatorPresetAsset", menuName = "Multiplayer/NetworkSimulatorPresetAsset")]
	public class NetworkSimulatorPresetAsset : global::UnityEngine.ScriptableObject, global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.INetworkSimulatorPreset
	{
		[field: global::UnityEngine.SerializeField]
		public string Name { get; set; }

		[field: global::UnityEngine.SerializeField]
		public string Description { get; set; }

		[field: global::UnityEngine.SerializeField]
		public int PacketDelayMs { get; set; }

		[field: global::UnityEngine.SerializeField]
		public int PacketJitterMs { get; set; }

		[field: global::UnityEngine.SerializeField]
		public int PacketLossInterval { get; set; }

		[field: global::UnityEngine.SerializeField]
		public int PacketLossPercent { get; set; }

		public static global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPresetAsset Create(string name, string description = "", int packetDelayMs = 0, int packetJitterMs = 0, int packetLossInterval = 0, int packetLossPercent = 0)
		{
			global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPresetAsset networkSimulatorPresetAsset = global::UnityEngine.ScriptableObject.CreateInstance<global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPresetAsset>();
			networkSimulatorPresetAsset.Name = name;
			networkSimulatorPresetAsset.Description = description;
			networkSimulatorPresetAsset.PacketDelayMs = packetDelayMs;
			networkSimulatorPresetAsset.PacketJitterMs = packetJitterMs;
			networkSimulatorPresetAsset.PacketLossInterval = packetLossInterval;
			networkSimulatorPresetAsset.PacketLossPercent = packetLossPercent;
			return networkSimulatorPresetAsset;
		}
	}
}
