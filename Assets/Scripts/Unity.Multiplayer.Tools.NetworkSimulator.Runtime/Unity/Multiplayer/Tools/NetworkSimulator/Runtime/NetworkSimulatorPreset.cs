namespace Unity.Multiplayer.Tools.NetworkSimulator.Runtime
{
	[global::System.Serializable]
	public class NetworkSimulatorPreset : global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.INetworkSimulatorPreset, global::System.IEquatable<global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset>
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

		public static global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset Create(string name, string description = "", int packetDelayMs = 0, int packetJitterMs = 0, int packetLossInterval = 0, int packetLossPercent = 0)
		{
			return new global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset
			{
				Name = name,
				Description = description,
				PacketDelayMs = packetDelayMs,
				PacketJitterMs = packetJitterMs,
				PacketLossInterval = packetLossInterval,
				PacketLossPercent = packetLossPercent
			};
		}

		public bool Equals(global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			if (Name == other.Name && Description == other.Description && PacketDelayMs == other.PacketDelayMs && PacketJitterMs == other.PacketJitterMs && PacketLossInterval == other.PacketLossInterval)
			{
				return PacketLossPercent == other.PacketLossPercent;
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (this == obj)
			{
				return true;
			}
			if (obj.GetType() != GetType())
			{
				return false;
			}
			return Equals((global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset)obj);
		}

		public override int GetHashCode()
		{
			return global::System.HashCode.Combine(Name, Description, PacketDelayMs, PacketJitterMs, PacketLossInterval, PacketLossPercent);
		}

		public override string ToString()
		{
			return "NetworkSimulatorPreset " + Name;
		}
	}
}
