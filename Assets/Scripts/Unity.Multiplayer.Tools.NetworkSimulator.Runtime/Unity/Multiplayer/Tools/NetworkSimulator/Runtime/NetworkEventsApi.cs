namespace Unity.Multiplayer.Tools.NetworkSimulator.Runtime
{
	internal class NetworkEventsApi : global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.INetworkEventsApi
	{
		private bool m_IsLageSpikeRunning;

		private readonly global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulator m_NetworkSimulator;

		private readonly global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.INetworkTransportApi m_NetworkTransportApi;

		public bool IsAvailable => m_NetworkTransportApi.IsAvailable;

		public bool IsConnected => m_NetworkTransportApi.IsConnected;

		public global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.INetworkSimulatorPreset CurrentPreset => m_NetworkSimulator.ConnectionPreset;

		internal NetworkEventsApi(global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulator networkSimulator, global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.INetworkTransportApi networkTransportApi)
		{
			m_NetworkSimulator = networkSimulator;
			m_NetworkTransportApi = networkTransportApi;
		}

		public void Disconnect()
		{
			if (!(m_NetworkSimulator == null) && m_NetworkSimulator.enabled)
			{
				m_NetworkTransportApi.SimulateDisconnect();
			}
		}

		public void Reconnect()
		{
			if (!(m_NetworkSimulator == null) && m_NetworkSimulator.enabled)
			{
				m_NetworkTransportApi.SimulateReconnect();
			}
		}

		public void TriggerLagSpike(global::System.TimeSpan duration)
		{
			if (!(m_NetworkSimulator == null) && m_NetworkSimulator.enabled)
			{
				global::Unity.Multiplayer.Tools.Common.TaskExtensions.Forget(RunLagSpikeAsync(duration));
			}
		}

		public global::System.Threading.Tasks.Task TriggerLagSpikeAsync(global::System.TimeSpan duration)
		{
			if (m_NetworkSimulator == null || !m_NetworkSimulator.enabled)
			{
				return global::System.Threading.Tasks.Task.CompletedTask;
			}
			return RunLagSpikeAsync(duration);
		}

		public void ChangeConnectionPreset(global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.INetworkSimulatorPreset newNetworkSimulatorPreset)
		{
			if (!(m_NetworkSimulator == null) && m_NetworkSimulator.enabled)
			{
				m_NetworkSimulator.ConnectionPreset = newNetworkSimulatorPreset;
			}
		}

		private async global::System.Threading.Tasks.Task RunLagSpikeAsync(global::System.TimeSpan duration)
		{
			m_IsLageSpikeRunning = true;
			Disconnect();
			await global::System.Threading.Tasks.Task.Delay(duration);
			Reconnect();
			m_IsLageSpikeRunning = false;
		}
	}
}
