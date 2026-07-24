namespace Unity.Multiplayer.Tools.NetStatsMonitor.Implementation
{
	internal struct GraphBufferParameters
	{
		public const float k_MaxPointsPerPixel = 1f;

		public int StatCount { get; set; }

		public int GraphWidthPoints { get; set; }

		internal GraphBufferParameters(in global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphParameters graphParams, float graphContentWidth, float maxPointsPerPixel)
		{
			StatCount = graphParams.StatCount;
			GraphWidthPoints = global::System.Math.Min((int)(maxPointsPerPixel * graphContentWidth), graphParams.SamplesPerStat);
		}
	}
}
