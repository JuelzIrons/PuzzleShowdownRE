namespace Unity.Multiplayer.Tools.NetworkSimulator.Runtime
{
	public interface INetworkSimulatorPreset
	{
		string Name { get; set; }

		string Description { get; set; }

		int PacketDelayMs { get; set; }

		int PacketJitterMs { get; set; }

		int PacketLossInterval { get; set; }

		int PacketLossPercent { get; set; }
	}
}
