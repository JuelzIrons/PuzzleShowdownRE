namespace Unity.Multiplayer.Tools.NetStatsMonitor.Implementation
{
	internal interface IGraphRenderer
	{
		float MaxPointsPerPixel { get; }

		global::Unity.Multiplayer.Tools.Common.MinAndMax UpdateVertices(global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStats.MetricId> stats, global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphDataSampler dataSampler, int pointsToAdvance, float yAxisMin, float yAxisMax, in global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphParameters graphParams, in global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphBufferParameters bufferParams, float renderBoundsXMin, float renderBoundsXMax, float renderBoundsYMin, float renderBoundsYMax, global::UnityEngine.UIElements.Vertex[] vertices);

		void UpdateConfiguration(global::Unity.Multiplayer.Tools.NetStatsMonitor.DisplayElementConfiguration config)
		{
		}
	}
}
