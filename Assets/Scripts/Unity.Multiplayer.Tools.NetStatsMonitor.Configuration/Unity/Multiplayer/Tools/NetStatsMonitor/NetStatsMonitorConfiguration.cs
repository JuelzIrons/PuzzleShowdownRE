namespace Unity.Multiplayer.Tools.NetStatsMonitor
{
	[global::UnityEngine.CreateAssetMenu(fileName = "NetStatsMonitorConfiguration", menuName = "Multiplayer/NetStatsMonitorConfiguration", order = 900)]
	public class NetStatsMonitorConfiguration : global::UnityEngine.ScriptableObject
	{
		[field: global::UnityEngine.SerializeField]
		public global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStatsMonitor.DisplayElementConfiguration> DisplayElements { get; set; } = new global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStatsMonitor.DisplayElementConfiguration>();

		[field: global::UnityEngine.SerializeField]
		internal int? ConfigurationHash { get; private set; }

		public void OnConfigurationModified()
		{
			RecomputeConfigurationHash();
		}

		internal void OnValidate()
		{
			for (int i = 0; i < DisplayElements.Count; i++)
			{
				if (!DisplayElements[i].FieldsInitialized)
				{
					DisplayElements[i] = new global::Unity.Multiplayer.Tools.NetStatsMonitor.DisplayElementConfiguration();
				}
				else
				{
					DisplayElements[i].OnValidate();
				}
			}
			RecomputeConfigurationHash();
		}

		internal void RecomputeConfigurationHash()
		{
			int num = 0;
			foreach (global::Unity.Multiplayer.Tools.NetStatsMonitor.DisplayElementConfiguration displayElement in DisplayElements)
			{
				num = global::System.HashCode.Combine(num, displayElement.ComputeHashCode());
			}
			ConfigurationHash = num;
		}
	}
}
