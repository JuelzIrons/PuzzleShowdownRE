namespace Unity.Multiplayer.Tools.NetworkSimulator.Runtime
{
	internal class NoOpNetworkEventsApi : global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.INetworkEventsApi
	{
		public bool IsAvailable => global::Unity.Multiplayer.Tools.Common.RuntimeUtils.NoEffectWarning<bool>(this, "IsAvailable");

		public bool IsConnected => global::Unity.Multiplayer.Tools.Common.RuntimeUtils.NoEffectWarning<bool>(this, "IsConnected");

		public global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.INetworkSimulatorPreset CurrentPreset => global::Unity.Multiplayer.Tools.Common.RuntimeUtils.NoEffectWarning<global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.INetworkSimulatorPreset>(this, "CurrentPreset");

		public void Disconnect()
		{
			global::Unity.Multiplayer.Tools.Common.RuntimeUtils.NoEffectWarning(this, "Disconnect");
		}

		public void Reconnect()
		{
			global::Unity.Multiplayer.Tools.Common.RuntimeUtils.NoEffectWarning(this, "Reconnect");
		}

		public void TriggerLagSpike(global::System.TimeSpan duration)
		{
			global::Unity.Multiplayer.Tools.Common.RuntimeUtils.NoEffectWarning(this, "TriggerLagSpike");
		}

		public global::System.Threading.Tasks.Task TriggerLagSpikeAsync(global::System.TimeSpan duration)
		{
			return global::Unity.Multiplayer.Tools.Common.RuntimeUtils.NoEffectWarning<global::System.Threading.Tasks.Task>(this, "TriggerLagSpikeAsync");
		}

		public void ChangeConnectionPreset(global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.INetworkSimulatorPreset preset)
		{
			global::Unity.Multiplayer.Tools.Common.RuntimeUtils.NoEffectWarning(this, "ChangeConnectionPreset");
		}
	}
}
