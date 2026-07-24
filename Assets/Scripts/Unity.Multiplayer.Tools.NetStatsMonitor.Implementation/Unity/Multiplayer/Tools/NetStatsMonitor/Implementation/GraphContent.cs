namespace Unity.Multiplayer.Tools.NetStatsMonitor.Implementation
{
	internal class GraphContent : global::UnityEngine.UIElements.VisualElement
	{
		private global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphParameters m_GraphParams;

		private global::UnityEngine.Color[] m_VariableColors;

		private readonly global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphBuffers m_Buffers = new global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphBuffers();

		private readonly global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphInputSynchronizer m_InputSynchronizer = new global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphInputSynchronizer();

		private readonly global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphDataSampler m_DataSampler = new global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphDataSampler();

		private global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.IGraphRenderer m_Renderer;

		public GraphContent()
		{
			base.generateVisualContent = (global::System.Action<global::UnityEngine.UIElements.MeshGenerationContext>)global::System.Delegate.Combine(base.generateVisualContent, new global::System.Action<global::UnityEngine.UIElements.MeshGenerationContext>(OnGenerateVisualContent));
		}

		public void UpdateConfiguration(global::Unity.Multiplayer.Tools.NetStatsMonitor.DisplayElementConfiguration config)
		{
			m_GraphParams = new global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphParameters
			{
				StatCount = config.Stats.Count,
				SamplesPerStat = config.GraphConfiguration.SampleCount
			};
			m_VariableColors = config.GraphConfiguration.VariableColors.ToArray();
			switch (config.Type)
			{
			case global::Unity.Multiplayer.Tools.NetStatsMonitor.DisplayElementType.LineGraph:
				if (!(m_Renderer is global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.LineGraphRenderer))
				{
					m_Renderer = new global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.LineGraphRenderer();
				}
				break;
			case global::Unity.Multiplayer.Tools.NetStatsMonitor.DisplayElementType.StackedAreaGraph:
				if (!(m_Renderer is global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.StackedAreaGraphRenderer))
				{
					m_Renderer = new global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.StackedAreaGraphRenderer();
				}
				break;
			}
			m_DataSampler.UpdateConfiguration(config.Stats);
			m_Renderer.UpdateConfiguration(config);
		}

		public global::Unity.Multiplayer.Tools.Common.MinAndMax UpdateDisplayData(global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MultiStatHistory history, global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStats.MetricId> stats, global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate rate, float minPlotValue, float maxPlotValue)
		{
			if (m_Renderer == null)
			{
				return default(global::Unity.Multiplayer.Tools.Common.MinAndMax);
			}
			global::UnityEngine.Rect rect = base.contentRect;
			float width = rect.width;
			if (float.IsNaN(width))
			{
				return default(global::Unity.Multiplayer.Tools.Common.MinAndMax);
			}
			global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphBufferParameters bufferParams = new global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphBufferParameters(in m_GraphParams, width, m_Renderer.MaxPointsPerPixel);
			int samplesPerStat = m_GraphParams.SamplesPerStat;
			int graphWidthPoints = bufferParams.GraphWidthPoints;
			float num = (float)samplesPerStat / (float)graphWidthPoints;
			int pointsToAdvance = m_InputSynchronizer.ComputeNumberOfPointsToAdvance(history.TimeStamps[rate], num);
			m_DataSampler.ResizeBuffersIfNeeded(in bufferParams);
			m_DataSampler.SampleNewPoints(history, stats, rate, graphWidthPoints, samplesPerStat, num, pointsToAdvance);
			m_Buffers.UpdateIfNeeded(in bufferParams, in m_VariableColors);
			global::Unity.Multiplayer.Tools.Common.MinAndMax result = m_Renderer.UpdateVertices(stats, m_DataSampler, pointsToAdvance, minPlotValue, maxPlotValue, in m_GraphParams, m_Buffers.Parameters, rect.xMin, rect.xMax, rect.yMax, rect.yMin, m_Buffers.Vertices);
			MarkDirtyRepaint();
			return result;
		}

		private void OnGenerateVisualContent(global::UnityEngine.UIElements.MeshGenerationContext mgc)
		{
			if (mgc != null)
			{
				m_Buffers.WriteToMeshGenerationContext(mgc);
			}
		}
	}
}
