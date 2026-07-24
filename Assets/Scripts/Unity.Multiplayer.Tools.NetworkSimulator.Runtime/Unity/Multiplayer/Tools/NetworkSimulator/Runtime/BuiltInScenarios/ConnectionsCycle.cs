namespace Unity.Multiplayer.Tools.NetworkSimulator.Runtime.BuiltInScenarios
{
	[global::System.Serializable]
	[global::JetBrains.Annotations.UsedImplicitly]
	public sealed class ConnectionsCycle : global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkScenarioTask
	{
		[global::System.Serializable]
		public sealed class Configuration : global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.BuiltInScenarios.PresetConfiguration
		{
			[global::UnityEngine.SerializeField]
			public int ChangeIntervalMilliseconds = 5000;
		}

		private int m_Index;

		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.BuiltInScenarios.ConnectionsCycle.Configuration> m_Configurations = new global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.BuiltInScenarios.ConnectionsCycle.Configuration>(new global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.BuiltInScenarios.ConnectionsCycle.Configuration[1]
		{
			new global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.BuiltInScenarios.ConnectionsCycle.Configuration
			{
				ConnectionPreset = global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPresets.None
			}
		});

		public global::System.Collections.Generic.ICollection<global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.BuiltInScenarios.ConnectionsCycle.Configuration> Configurations => m_Configurations;

		protected override async global::System.Threading.Tasks.Task Run(global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.INetworkEventsApi networkEventsApi, global::System.Threading.CancellationToken cancellationToken)
		{
			while (!cancellationToken.IsCancellationRequested)
			{
				if (base.IsPaused)
				{
					await global::System.Threading.Tasks.Task.Yield();
					continue;
				}
				if (m_Index >= m_Configurations.Count)
				{
					global::UnityEngine.Debug.LogWarning(string.Format("Skipping scenario item #{0} as {1}.{2} doesn't have enough elements.", m_Index, "ConnectionsCycle", "Configurations"));
					await global::System.Threading.Tasks.Task.Yield();
					Iterate();
					continue;
				}
				global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.BuiltInScenarios.ConnectionsCycle.Configuration configuration = m_Configurations[m_Index];
				global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.INetworkSimulatorPreset connectionPreset = configuration.ConnectionPreset;
				networkEventsApi.ChangeConnectionPreset(connectionPreset);
				if (configuration.ChangeIntervalMilliseconds <= 0)
				{
					global::UnityEngine.Debug.LogWarning(string.Format("Skipping scenario item #{0}. {1}.{2}[{3}].{4} must be greater than 0.", m_Index, "ConnectionsCycle", "Configurations", m_Index, configuration.ChangeIntervalMilliseconds));
					await global::System.Threading.Tasks.Task.Yield();
					Iterate();
				}
				else
				{
					await global::System.Threading.Tasks.Task.Delay(configuration.ChangeIntervalMilliseconds, cancellationToken);
					Iterate();
				}
			}
		}

		private void Iterate()
		{
			m_Index = ((++m_Index < m_Configurations.Count) ? m_Index : 0);
		}
	}
}
