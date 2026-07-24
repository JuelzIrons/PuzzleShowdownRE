namespace Unity.Multiplayer.Tools.NetStatsMonitor.Implementation
{
	internal class GraphBoundsTransformer
	{
		private float m_BoundsXMin;

		private float m_BoundsXMax;

		private float m_BoundsYMin;

		private float m_BoundsYMax;

		private float m_YValueMin;

		private float m_YValueMax;

		public GraphBoundsTransformer(float boundsXMin, float boundsXMax, float boundsYMin, float boundsYMax, float yValueMin, float yValueMax)
		{
			m_BoundsXMin = boundsXMin;
			m_BoundsXMax = boundsXMax;
			m_BoundsYMin = boundsYMin;
			m_BoundsYMax = boundsYMax;
			m_YValueMin = yValueMin;
			m_YValueMax = yValueMax;
		}

		private global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.LinearTransform ComputeXAxisTransform(float newRenderBoundsXMin, float newRenderBoundsXMax, int pointsToAdvance)
		{
			return global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.LinearTransform.Identity;
		}

		private global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.LinearTransform ComputeYAxisTransform(float newBoundsYMin, float newBoundsYMax, float newYValueMin, float newYValueMax)
		{
			if (newBoundsYMin == m_BoundsYMin && newBoundsYMax == m_BoundsYMax && newYValueMin == m_YValueMin && newYValueMax == m_YValueMax)
			{
				return global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.LinearTransform.Identity;
			}
			float num = newBoundsYMax - newBoundsYMin;
			float num2 = m_BoundsYMax - m_BoundsYMin;
			float num3 = newYValueMax - newYValueMin;
			float num4 = (m_YValueMax - m_YValueMin) / num2;
			float num5 = num / num3;
			float num6 = num4 * num5;
			float b = (0f - m_BoundsYMin) * num6 + (m_YValueMin - newYValueMin) * num5 + newBoundsYMin;
			m_BoundsYMin = newBoundsYMin;
			m_BoundsYMax = newBoundsYMax;
			m_YValueMin = newYValueMin;
			m_YValueMax = newYValueMax;
			return new global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.LinearTransform
			{
				A = num6,
				B = b
			};
		}

		public (global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.LinearTransform transformX, global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.LinearTransform transformY) ComputeTransformsForNewBounds(float newBoundsXMin, float newBoundsXMax, float newBoundsYMin, float newBoundsYMax, float newYAxisMin, float newYAxisMax, int pointsToAdvance)
		{
			return (transformX: ComputeXAxisTransform(newBoundsXMin, newBoundsXMax, pointsToAdvance), transformY: ComputeYAxisTransform(newBoundsYMin, newBoundsYMax, newYAxisMin, newYAxisMax));
		}
	}
}
