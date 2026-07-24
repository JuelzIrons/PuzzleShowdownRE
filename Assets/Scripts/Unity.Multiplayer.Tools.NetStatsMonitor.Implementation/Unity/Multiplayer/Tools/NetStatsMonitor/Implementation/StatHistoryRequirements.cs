namespace Unity.Multiplayer.Tools.NetStatsMonitor.Implementation
{
	internal class StatHistoryRequirements
	{
		public global::System.Collections.Generic.HashSet<double> DecayConstants { get; }

		public global::Unity.Multiplayer.Tools.Common.EnumMap<global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate, int> SampleCounts { get; set; }

		public StatHistoryRequirements()
		{
			DecayConstants = new global::System.Collections.Generic.HashSet<double>();
			SampleCounts = new global::Unity.Multiplayer.Tools.Common.EnumMap<global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate, int>();
		}

		public StatHistoryRequirements(global::System.Collections.Generic.HashSet<double> decayConstants, global::Unity.Multiplayer.Tools.Common.EnumMap<global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate, int> sampleCounts)
		{
			DecayConstants = decayConstants;
			SampleCounts = sampleCounts;
		}

		public StatHistoryRequirements(global::System.Collections.Generic.IEnumerable<double> decayConstants, global::Unity.Multiplayer.Tools.Common.EnumMap<global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate, int> sampleCounts)
		{
			DecayConstants = global::System.Linq.Enumerable.ToHashSet(decayConstants);
			SampleCounts = sampleCounts;
		}
	}
}
