namespace Unity.Multiplayer.Tools.NetworkSimulator.Runtime
{
	public interface INetworkEventsApi
	{
		bool IsAvailable { get; }

		bool IsConnected { get; }

		global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.INetworkSimulatorPreset CurrentPreset { get; }

		void Disconnect();

		void Reconnect();

		void TriggerLagSpike(global::System.TimeSpan duration);

		global::System.Threading.Tasks.Task TriggerLagSpikeAsync(global::System.TimeSpan duration);

		void ChangeConnectionPreset(global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.INetworkSimulatorPreset preset);
	}
}
