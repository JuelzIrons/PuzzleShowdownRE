namespace Unity.Multiplayer.Tools.NetStatsMonitor.Implementation
{
	internal class MultiStatHistoryRequirements
	{
		[global::JetBrains.Annotations.NotNull]
		internal global::System.Collections.Generic.Dictionary<global::Unity.Multiplayer.Tools.NetStats.MetricId, global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.StatHistoryRequirements> Data { get; } = new global::System.Collections.Generic.Dictionary<global::Unity.Multiplayer.Tools.NetStats.MetricId, global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.StatHistoryRequirements>();

		internal static global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MultiStatHistoryRequirements FromConfiguration(global::Unity.Multiplayer.Tools.NetStatsMonitor.NetStatsMonitorConfiguration configuration)
		{
			global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MultiStatHistoryRequirements multiStatHistoryRequirements = new global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MultiStatHistoryRequirements();
			if (configuration == null)
			{
				return multiStatHistoryRequirements;
			}
			global::System.Collections.Generic.Dictionary<global::Unity.Multiplayer.Tools.NetStats.MetricId, global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.StatHistoryRequirements> data = multiStatHistoryRequirements.Data;
			foreach (global::Unity.Multiplayer.Tools.NetStatsMonitor.DisplayElementConfiguration displayElement in configuration.DisplayElements)
			{
				int sampleCount = displayElement.SampleCount;
				global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate sampleRate = displayElement.SampleRate;
				double? decayConstant = displayElement.DecayConstant;
				foreach (global::Unity.Multiplayer.Tools.NetStats.MetricId stat in displayElement.Stats)
				{
					if (!data.ContainsKey(stat))
					{
						data[stat] = new global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.StatHistoryRequirements(new global::System.Collections.Generic.HashSet<double>(), new global::Unity.Multiplayer.Tools.Common.EnumMap<global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate, int>());
					}
					global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.StatHistoryRequirements statHistoryRequirements = multiStatHistoryRequirements.Data[stat];
					statHistoryRequirements.SampleCounts[sampleRate] = global::System.Math.Max(statHistoryRequirements.SampleCounts[sampleRate], sampleCount);
					if (decayConstant.HasValue)
					{
						statHistoryRequirements.DecayConstants.Add(decayConstant.Value);
					}
				}
			}
			return multiStatHistoryRequirements;
		}
	}
}
