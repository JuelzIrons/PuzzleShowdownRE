namespace Unity.Multiplayer.Tools.NetStatsMonitor.Implementation
{
	internal class StackedAreaGraphRenderer : global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.IGraphRenderer
	{
		private const float k_MaxPointsPerPixel = 1f;

		private global::Unity.Multiplayer.Tools.Common.RingBuffer<float> m_PointSums;

		private global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphBoundsTransformer m_BoundsTransformer;

		private float m_PointValueMax;

		public float MaxPointsPerPixel => 1f;

		private void ResizePointSumsIfNeeded(in global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphBufferParameters bufferParams)
		{
			int graphWidthPoints = bufferParams.GraphWidthPoints;
			if (m_PointSums == null)
			{
				m_PointSums = new global::Unity.Multiplayer.Tools.Common.RingBuffer<float>(graphWidthPoints);
				m_PointSums.Length = graphWidthPoints;
			}
			else if (m_PointSums.Capacity != graphWidthPoints)
			{
				m_PointSums.Capacity = graphWidthPoints;
				m_PointSums.Length = graphWidthPoints;
			}
		}

		public global::Unity.Multiplayer.Tools.Common.MinAndMax UpdateVertices(global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStats.MetricId> stats, global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphDataSampler dataSampler, int pointsToAdvance, float yAxisMin, float yAxisMax, in global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphParameters graphParams, in global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphBufferParameters bufferParams, float renderBoundsXMin, float renderBoundsXMax, float renderBoundsYMin, float renderBoundsYMax, global::UnityEngine.UIElements.Vertex[] vertices)
		{
			ResizePointSumsIfNeeded(in bufferParams);
			int num = global::System.Math.Min(bufferParams.StatCount, stats.Count);
			int graphWidthPoints = bufferParams.GraphWidthPoints;
			float xScale = (renderBoundsXMax - renderBoundsXMin) / (float)(graphWidthPoints - 1);
			float yScale = (renderBoundsYMax - renderBoundsYMin) / yAxisMax;
			int num2 = 2 * graphWidthPoints;
			float num3 = (float)graphParams.SamplesPerStat / (float)graphWidthPoints;
			_ = 1f / num3;
			if (m_BoundsTransformer == null)
			{
				m_BoundsTransformer = new global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphBoundsTransformer(renderBoundsXMin, renderBoundsXMax, renderBoundsYMin, renderBoundsYMax, yAxisMin, yAxisMax);
			}
			var (xAxisTransform, yAxisTransform) = m_BoundsTransformer.ComputeTransformsForNewBounds(renderBoundsXMin, renderBoundsXMax, renderBoundsYMin, renderBoundsYMax, yAxisMin, yAxisMax, pointsToAdvance);
			if (pointsToAdvance <= 0)
			{
				RescaleExistingGeometryIfNeeded(xAxisTransform, yAxisTransform, vertices);
				return new global::Unity.Multiplayer.Tools.Common.MinAndMax
				{
					Min = 0f,
					Max = m_PointValueMax
				};
			}
			ShiftExistingPointSums(pointsToAdvance);
			int pointsToCopy = global::System.Math.Max(graphWidthPoints - pointsToAdvance, 0);
			for (int i = 0; i < num; i++)
			{
				global::Unity.Multiplayer.Tools.NetStats.MetricId key = stats[i];
				global::Unity.Multiplayer.Tools.Common.RingBuffer<float> pointValues = dataSampler.PointValues[key];
				int statVerticesBegin = i * num2;
				ShiftExistingGeometryAndRescaleIfNeeded(vertices, statVerticesBegin, yAxisTransform, xScale, renderBoundsXMin, pointsToCopy, pointsToAdvance);
				ComputeNewGeometry(vertices, statVerticesBegin, yAxisMax, renderBoundsXMin, renderBoundsYMin, graphWidthPoints, xScale, yScale, pointsToCopy, pointValues);
			}
			return new global::Unity.Multiplayer.Tools.Common.MinAndMax
			{
				Min = 0f,
				Max = m_PointValueMax
			};
		}

		private void RescaleExistingGeometryIfNeeded(global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.LinearTransform xAxisTransform, global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.LinearTransform yAxisTransform, global::UnityEngine.UIElements.Vertex[] vertices)
		{
			bool flag = !xAxisTransform.IsIdentity;
			bool flag2 = !yAxisTransform.IsIdentity;
			int num = vertices.Length;
			if (flag && flag2)
			{
				for (int i = 0; i < num; i++)
				{
					global::UnityEngine.Vector3 position = vertices[i].position;
					position.x = xAxisTransform.Apply(position.x);
					position.y = yAxisTransform.Apply(position.y);
					vertices[i].position = position;
				}
			}
			else if (flag)
			{
				for (int j = 0; j < num; j++)
				{
					global::UnityEngine.Vector3 position2 = vertices[j].position;
					position2.x = xAxisTransform.Apply(position2.x);
					vertices[j].position = position2;
				}
			}
			else if (flag2)
			{
				for (int k = 0; k < num; k++)
				{
					global::UnityEngine.Vector3 position3 = vertices[k].position;
					position3.y = yAxisTransform.Apply(position3.y);
					vertices[k].position = position3;
				}
			}
		}

		private void ShiftExistingPointSums(int pointsToAdvance)
		{
			bool flag = false;
			for (int i = 0; i < pointsToAdvance; i++)
			{
				if (m_PointSums[0] >= m_PointValueMax - float.Epsilon)
				{
					flag = true;
				}
				m_PointSums.PushBack(0f);
			}
			if (flag)
			{
				m_PointValueMax = global::Unity.Multiplayer.Tools.Common.RingBufferExtensions.Max(m_PointSums);
			}
		}

		private static void ShiftExistingGeometryAndRescaleIfNeeded(global::UnityEngine.UIElements.Vertex[] vertices, int statVerticesBegin, global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.LinearTransform yAxisTransform, float xScale, float renderBoundsXMin, int pointsToCopy, int pointsToAdvance)
		{
			if (!yAxisTransform.IsIdentity)
			{
				for (int i = 0; i < pointsToCopy; i++)
				{
					float x = (float)i * xScale + renderBoundsXMin;
					int num = i + pointsToAdvance;
					int num2 = statVerticesBegin + num * 2;
					float y = yAxisTransform.Apply(vertices[num2].position.y);
					float y2 = yAxisTransform.Apply(vertices[num2 + 1].position.y);
					int num3 = statVerticesBegin + i * 2;
					vertices[num3].position = new global::UnityEngine.Vector3(x, y);
					vertices[num3 + 1].position = new global::UnityEngine.Vector3(x, y2);
				}
			}
			else
			{
				for (int j = 0; j < pointsToCopy; j++)
				{
					float x2 = (float)j * xScale + renderBoundsXMin;
					int num4 = j + pointsToAdvance;
					int num5 = statVerticesBegin + num4 * 2;
					float y3 = vertices[num5].position.y;
					float y4 = vertices[num5 + 1].position.y;
					int num6 = statVerticesBegin + j * 2;
					vertices[num6].position = new global::UnityEngine.Vector3(x2, y3);
					vertices[num6 + 1].position = new global::UnityEngine.Vector3(x2, y4);
				}
			}
		}

		private void ComputeNewGeometry(global::UnityEngine.UIElements.Vertex[] vertices, int statVerticesBegin, float yAxisMax, float renderBoundsXMin, float renderBoundsYMin, int pointsPerStat, float xScale, float yScale, int pointsToCopy, global::Unity.Multiplayer.Tools.Common.RingBuffer<float> pointValues)
		{
			for (int i = pointsToCopy; i < pointsPerStat; i++)
			{
				float num = pointValues[i];
				float num2 = m_PointSums[i];
				float num3 = num2 + num;
				m_PointValueMax = global::System.MathF.Max(num3, m_PointValueMax);
				float num4 = global::System.MathF.Min(num3, yAxisMax);
				m_PointSums[i] = num3;
				float x = (float)i * xScale + renderBoundsXMin;
				float y = num2 * yScale + renderBoundsYMin;
				float y2 = num4 * yScale + renderBoundsYMin;
				int num5 = statVerticesBegin + 2 * i;
				vertices[num5 + 1].position = new global::UnityEngine.Vector3(x, y2);
				vertices[num5].position = new global::UnityEngine.Vector3(x, y);
			}
		}

		global::Unity.Multiplayer.Tools.Common.MinAndMax global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.IGraphRenderer.UpdateVertices(global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStats.MetricId> stats, global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphDataSampler dataSampler, int pointsToAdvance, float yAxisMin, float yAxisMax, in global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphParameters graphParams, in global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphBufferParameters bufferParams, float renderBoundsXMin, float renderBoundsXMax, float renderBoundsYMin, float renderBoundsYMax, global::UnityEngine.UIElements.Vertex[] vertices)
		{
			return UpdateVertices(stats, dataSampler, pointsToAdvance, yAxisMin, yAxisMax, in graphParams, in bufferParams, renderBoundsXMin, renderBoundsXMax, renderBoundsYMin, renderBoundsYMax, vertices);
		}
	}
}
