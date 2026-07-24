namespace Unity.Multiplayer.Tools.NetworkSimulator.Runtime.BuiltInScenarios
{
	[global::System.Serializable]
	[global::JetBrains.Annotations.UsedImplicitly]
	public sealed class RandomConnectionsSwap : global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkScenarioTask
	{
		[global::System.Serializable]
		public sealed class Configuration : global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.BuiltInScenarios.PresetConfiguration
		{
		}

		[global::UnityEngine.SerializeField]
		public int ChangeIntervalMilliseconds = 5000;

		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.BuiltInScenarios.RandomConnectionsSwap.Configuration> m_Configurations = new global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.BuiltInScenarios.RandomConnectionsSwap.Configuration>(new global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.BuiltInScenarios.RandomConnectionsSwap.Configuration[1]
		{
			new global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.BuiltInScenarios.RandomConnectionsSwap.Configuration
			{
				ConnectionPreset = global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPresets.None
			}
		});

		public global::System.Collections.Generic.ICollection<global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.BuiltInScenarios.RandomConnectionsSwap.Configuration> Configurations => m_Configurations;

		protected override async global::System.Threading.Tasks.Task Run(global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.INetworkEventsApi networkEventsApi, global::System.Threading.CancellationToken cancellationToken)
		{
			while (!cancellationToken.IsCancellationRequested)
			{
				if (base.IsPaused)
				{
					await global::System.Threading.Tasks.Task.Yield();
					continue;
				}
				int num = global::UnityEngine.Random.Range(0, Configurations.Count);
				if (num >= m_Configurations.Count)
				{
					global::UnityEngine.Debug.LogWarning(string.Format("Skipping scenario item #{0} as {1}.{2} doesn't have enough elements.", num, "RandomConnectionsSwap", "Configurations"));
					await global::System.Threading.Tasks.Task.Yield();
					continue;
				}
				global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.INetworkSimulatorPreset connectionPreset = m_Configurations[num].ConnectionPreset;
				networkEventsApi.ChangeConnectionPreset(connectionPreset);
				if (ChangeIntervalMilliseconds <= 0)
				{
					global::UnityEngine.Debug.LogWarning(string.Format("Skipping scenario item #{0}. {1}.{2} parameter must be greater than 0.", num, "RandomConnectionsSwap", ChangeIntervalMilliseconds));
					await global::System.Threading.Tasks.Task.Yield();
				}
				else
				{
					await global::System.Threading.Tasks.Task.Delay(ChangeIntervalMilliseconds, cancellationToken);
				}
			}
		}
	}
}
