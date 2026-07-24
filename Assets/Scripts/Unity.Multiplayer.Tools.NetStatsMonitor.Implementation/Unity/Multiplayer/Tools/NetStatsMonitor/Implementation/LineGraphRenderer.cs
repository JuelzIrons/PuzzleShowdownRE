namespace Unity.Multiplayer.Tools.NetStatsMonitor.Implementation
{
	internal class LineGraphRenderer : global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.IGraphRenderer
	{
		private const float k_MaxPointsPerPixel = 0.5f;

		private global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphBoundsTransformer m_BoundsTransformer;

		private global::Unity.Multiplayer.Tools.Common.RingBuffer<float> m_MaxPointValues;

		private float m_MaxPointValue;

		private bool m_MustRecomputeMaxPointValue;

		public float MaxPointsPerPixel => 0.5f;

		internal float LineThickness { get; set; }

		public void UpdateConfiguration(global::Unity.Multiplayer.Tools.NetStatsMonitor.DisplayElementConfiguration config)
		{
			LineThickness = config?.GraphConfiguration?.LineGraphConfiguration?.LineThickness ?? 1f;
		}

		private void ResizeInternalBuffersIfNeeded(in global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphBufferParameters bufferParams)
		{
			int graphWidthPoints = bufferParams.GraphWidthPoints;
			if (m_MaxPointValues == null)
			{
				m_MaxPointValues = new global::Unity.Multiplayer.Tools.Common.RingBuffer<float>(graphWidthPoints);
				m_MaxPointValues.Length = graphWidthPoints;
			}
			else if (m_MaxPointValues.Capacity != graphWidthPoints)
			{
				m_MaxPointValues.Capacity = graphWidthPoints;
				m_MaxPointValues.Length = graphWidthPoints;
				m_MustRecomputeMaxPointValue = true;
			}
		}

		public global::Unity.Multiplayer.Tools.Common.MinAndMax UpdateVertices(global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStats.MetricId> stats, global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphDataSampler dataSampler, int pointsToAdvance, float yAxisMin, float yAxisMax, in global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphParameters graphParams, in global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphBufferParameters bufferParams, float renderBoundsXMin, float renderBoundsXMax, float renderBoundsYMin, float renderBoundsYMax, global::UnityEngine.UIElements.Vertex[] vertices)
		{
			if (bufferParams.GraphWidthPoints < 2)
			{
				return default(global::Unity.Multiplayer.Tools.Common.MinAndMax);
			}
			if (LineThickness <= 0f)
			{
				return default(global::Unity.Multiplayer.Tools.Common.MinAndMax);
			}
			ResizeInternalBuffersIfNeeded(in bufferParams);
			int num = global::System.Math.Min(bufferParams.StatCount, stats.Count);
			int graphWidthPoints = bufferParams.GraphWidthPoints;
			float num2 = (float)graphParams.SamplesPerStat / (float)graphWidthPoints;
			_ = 1f / num2;
			if (m_BoundsTransformer == null)
			{
				m_BoundsTransformer = new global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphBoundsTransformer(renderBoundsXMin, renderBoundsXMax, renderBoundsYMin, renderBoundsYMax, yAxisMin, yAxisMax);
			}
			(global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.LinearTransform transformX, global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.LinearTransform transformY) tuple = m_BoundsTransformer.ComputeTransformsForNewBounds(renderBoundsXMin, renderBoundsXMax, renderBoundsYMin, renderBoundsYMax, yAxisMin, yAxisMax, pointsToAdvance);
			global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.LinearTransform item = tuple.transformX;
			global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.LinearTransform item2 = tuple.transformY;
			float min = 0f;
			bool flag = !item.IsIdentity;
			bool flag2 = !item2.IsIdentity;
			if (!flag && !flag2 && pointsToAdvance <= 0)
			{
				return new global::Unity.Multiplayer.Tools.Common.MinAndMax
				{
					Min = min,
					Max = m_MaxPointValue
				};
			}
			UpdateMaxPointValues(stats, dataSampler, graphWidthPoints, pointsToAdvance);
			float num3 = (renderBoundsXMax - renderBoundsXMin) / (float)graphWidthPoints;
			float yScale = (renderBoundsYMax - renderBoundsYMin) / yAxisMax;
			float xScaleInverse = 1f / num3;
			int num4 = 2 * graphWidthPoints;
			int pointsToCopy = ComputeNumberOfPointsToCopy(flag, flag2, graphWidthPoints, pointsToAdvance);
			float renderBoundsYMinActual = global::System.Math.Min(renderBoundsYMin, renderBoundsYMax);
			float renderBoundsYMaxActual = global::System.Math.Max(renderBoundsYMin, renderBoundsYMax);
			float halfLineThickness = 0.5f * LineThickness;
			for (int i = 0; i < num; i++)
			{
				global::Unity.Multiplayer.Tools.NetStats.MetricId key = stats[i];
				int statVerticesBegin = i * num4;
				global::Unity.Multiplayer.Tools.Common.RingBuffer<float> pointValues = dataSampler.PointValues[key];
				ShiftExistingGeometry(vertices, statVerticesBegin, num3, pointsToAdvance, pointsToCopy);
				ComputeNewGeometry(vertices, statVerticesBegin, yAxisMin, yAxisMax, renderBoundsYMin, graphWidthPoints, num3, yScale, xScaleInverse, pointsToCopy, renderBoundsYMinActual, renderBoundsYMaxActual, halfLineThickness, pointValues);
			}
			return new global::Unity.Multiplayer.Tools.Common.MinAndMax(min, m_MaxPointValue);
		}

		private static int ComputeNumberOfPointsToCopy(bool xAxisChanged, bool yAxisChanged, int graphWidthPoints, int pointsToAdvance)
		{
			if (!(xAxisChanged || yAxisChanged))
			{
				return global::System.Math.Max(graphWidthPoints - pointsToAdvance - 1, 0);
			}
			return 0;
		}

		private void UpdateMaxPointValues(global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStats.MetricId> stats, global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphDataSampler sampler, int graphWidthPoints, int pointsToAdvance)
		{
			float num = 0f;
			for (int i = 0; i < pointsToAdvance; i++)
			{
				num = global::System.Math.Max(num, m_MaxPointValues.LeastRecent);
				m_MaxPointValues.PushBack(0f);
			}
			int num2 = global::System.Math.Max(graphWidthPoints - pointsToAdvance, 0);
			foreach (global::Unity.Multiplayer.Tools.NetStats.MetricId stat in stats)
			{
				global::Unity.Multiplayer.Tools.Common.RingBuffer<float> ringBuffer = sampler.PointValues[stat];
				for (int j = num2; j < graphWidthPoints; j++)
				{
					float val = ringBuffer[j];
					m_MaxPointValues[j] = global::System.Math.Max(m_MaxPointValues[j], val);
					m_MaxPointValue = global::System.Math.Max(m_MaxPointValue, val);
				}
			}
			if (num >= m_MaxPointValue)
			{
				m_MustRecomputeMaxPointValue = true;
			}
			if (m_MustRecomputeMaxPointValue)
			{
				m_MaxPointValue = global::Unity.Multiplayer.Tools.Common.RingBufferExtensions.Max(m_MaxPointValues);
				m_MustRecomputeMaxPointValue = false;
			}
		}

		private void ShiftExistingGeometry(global::UnityEngine.UIElements.Vertex[] vertices, int statVerticesBegin, float xScale, int pointsToAdvance, int pointsToCopy)
		{
			float num = (float)pointsToAdvance * xScale;
			int num2 = 0;
			int num3 = pointsToAdvance;
			while (num2 < pointsToCopy)
			{
				int num4 = statVerticesBegin + num3 * 2;
				int num5 = statVerticesBegin + num2 * 2;
				ref global::UnityEngine.Vector3 position = ref vertices[num4].position;
				ref global::UnityEngine.Vector3 position2 = ref vertices[num4 + 1].position;
				float x = position.x - num;
				float y = position.y;
				float x2 = position2.x - num;
				float y2 = position2.y;
				vertices[num5].position = new global::UnityEngine.Vector3(x, y);
				vertices[num5 + 1].position = new global::UnityEngine.Vector3(x2, y2);
				num2++;
				num3++;
			}
		}

		private void ComputeNewGeometry(global::UnityEngine.UIElements.Vertex[] vertices, int statVerticesBegin, float yAxisMin, float yAxisMax, float renderBoundsYMin, int graphWidthPoints, float xScale, float yScale, float xScaleInverse, int pointsToCopy, float renderBoundsYMinActual, float renderBoundsYMaxActual, float halfLineThickness, global::Unity.Multiplayer.Tools.Common.RingBuffer<float> pointValues)
		{
			int i = global::System.Math.Max(pointsToCopy, 0);
			float num = global::System.Math.Clamp(pointValues[i], yAxisMin, yAxisMax);
			float num2 = (float)i * xScale;
			float num3 = num * yScale + renderBoundsYMin;
			int index = global::System.Math.Max(i - 1, 0);
			float num4 = global::System.Math.Clamp(pointValues[index], yAxisMin, yAxisMax) * yScale + renderBoundsYMin;
			float num5 = (num3 - num4) * xScaleInverse;
			float num6 = num3 - num5 * num2;
			float num7 = halfLineThickness * global::System.MathF.Sqrt(1f + num5 * num5);
			float num8 = num6 + num7;
			for (; i < graphWidthPoints - 1; i++)
			{
				int num9 = i + 1;
				float num10 = global::System.Math.Clamp(pointValues[num9], yAxisMin, yAxisMax);
				float num11 = (float)num9 * xScale;
				float num12 = num10 * yScale + renderBoundsYMin;
				float num13 = (num12 - num3) * xScaleInverse;
				float num14 = num12 - num13 * num11;
				float num15 = halfLineThickness * global::System.MathF.Sqrt(1f + num13 * num13);
				float num16 = num14 + num15;
				float num17 = ((global::System.MathF.Abs(num5 - num13) < 0.0001f) ? (num2 - num13 / num15) : ((num16 - num8) / (num5 - num13)));
				float num18 = num17 * num13 + num16;
				float xb = 2f * num2 - num17;
				float yb = 2f * num3 - num18;
				WriteVertices(vertices, statVerticesBegin, i, renderBoundsYMinActual, renderBoundsYMaxActual, num17, num18, xb, yb);
				num2 = num11;
				num3 = num12;
				num5 = num13;
				num8 = num16;
				num7 = num15;
			}
			float ya = num3 + num7;
			float yb2 = num3 - num7;
			WriteVertices(vertices, statVerticesBegin, i, renderBoundsYMinActual, renderBoundsYMaxActual, num2, ya, num2, yb2);
		}

		private void WriteVertices(global::UnityEngine.UIElements.Vertex[] vertices, int statVerticesBegin, int index, float renderBoundsYMinActual, float renderBoundsYMaxActual, float xa, float ya, float xb, float yb)
		{
			ya = global::System.Math.Clamp(ya, renderBoundsYMinActual, renderBoundsYMaxActual);
			yb = global::System.Math.Clamp(yb, renderBoundsYMinActual, renderBoundsYMaxActual);
			int num = statVerticesBegin + index * 2;
			vertices[num].position = new global::UnityEngine.Vector3(xa, ya);
			vertices[num + 1].position = new global::UnityEngine.Vector3(xb, yb);
		}

		global::Unity.Multiplayer.Tools.Common.MinAndMax global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.IGraphRenderer.UpdateVertices(global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStats.MetricId> stats, global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphDataSampler dataSampler, int pointsToAdvance, float yAxisMin, float yAxisMax, in global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphParameters graphParams, in global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphBufferParameters bufferParams, float renderBoundsXMin, float renderBoundsXMax, float renderBoundsYMin, float renderBoundsYMax, global::UnityEngine.UIElements.Vertex[] vertices)
		{
			return UpdateVertices(stats, dataSampler, pointsToAdvance, yAxisMin, yAxisMax, in graphParams, in bufferParams, renderBoundsXMin, renderBoundsXMax, renderBoundsYMin, renderBoundsYMax, vertices);
		}
	}
}
