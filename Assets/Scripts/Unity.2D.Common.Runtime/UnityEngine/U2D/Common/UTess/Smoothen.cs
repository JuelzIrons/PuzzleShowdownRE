namespace UnityEngine.U2D.Common.UTess
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	internal struct Smoothen
	{
		private static readonly float kMaxAreaTolerance = 1.842f;

		private static readonly float kMaxEdgeTolerance = 2.482f;

		private static void RefineEdges(ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.int4> refinedEdges, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.int4> delaEdges, ref int delaEdgeCount, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.int4> voronoiEdges)
		{
			int num = delaEdgeCount;
			delaEdgeCount = 0;
			for (int i = 0; i < num - 1; i++)
			{
				global::Unity.Mathematics.int4 value = delaEdges[i];
				global::Unity.Mathematics.int4 int5 = delaEdges[i + 1];
				if (value.x == int5.x && value.y == int5.y)
				{
					value.w = int5.z;
					i++;
				}
				refinedEdges[delaEdgeCount++] = value;
			}
			for (int j = 0; j < delaEdgeCount; j++)
			{
				int z = refinedEdges[j].z;
				int w = refinedEdges[j].w;
				if (z != -1 && w != -1)
				{
					global::Unity.Mathematics.int4 value2 = new global::Unity.Mathematics.int4(w, z, j, 0);
					voronoiEdges[j] = value2;
				}
			}
			global::UnityEngine.U2D.Common.UTess.ModuleHandle.Copy(refinedEdges, delaEdges, delaEdgeCount);
		}

		private static void GetAffectingEdges(int pointIndex, global::Unity.Collections.NativeArray<global::Unity.Mathematics.int4> edges, int edgeCount, ref global::Unity.Collections.NativeArray<int> resultSet, ref global::Unity.Collections.NativeArray<int> checkSet, ref int resultCount)
		{
			resultCount = 0;
			for (int i = 0; i < edgeCount; i++)
			{
				if (pointIndex == edges[i].x || pointIndex == edges[i].y)
				{
					resultSet[resultCount++] = i;
				}
				checkSet[i] = 0;
			}
		}

		private static void CentroidByPoints(int triIndex, global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Common.UTess.UTriangle> triangles, ref global::Unity.Collections.NativeArray<int> centroidTris, ref int centroidCount, ref global::Unity.Mathematics.float2 aggregate, ref global::Unity.Mathematics.float2 point)
		{
			for (int i = 0; i < centroidCount; i++)
			{
				if (triIndex == centroidTris[i])
				{
					return;
				}
			}
			centroidTris[centroidCount++] = triIndex;
			aggregate += triangles[triIndex].c.center;
			point = aggregate / centroidCount;
		}

		private static void CentroidByPolygon(global::Unity.Mathematics.int4 e, global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Common.UTess.UTriangle> triangles, ref global::Unity.Mathematics.float2 centroid, ref float area, ref float distance)
		{
			global::Unity.Mathematics.float2 center = triangles[e.x].c.center;
			global::Unity.Mathematics.float2 center2 = triangles[e.y].c.center;
			float num = center.x * center2.y - center2.x * center.y;
			distance += global::Unity.Mathematics.math.distance(center, center2);
			area += num;
			centroid.x += (center2.x + center.x) * num;
			centroid.y += (center2.y + center.y) * num;
		}

		private static bool ConnectTriangles(ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.int4> connectedTri, ref global::Unity.Collections.NativeArray<int> affectEdges, ref global::Unity.Collections.NativeArray<int> checkSet, global::Unity.Collections.NativeArray<global::Unity.Mathematics.int4> voronoiEdges, int triangleCount)
		{
			int index = affectEdges[0];
			int index2 = affectEdges[0];
			connectedTri[0] = new global::Unity.Mathematics.int4(voronoiEdges[index].x, voronoiEdges[index].y, 0, 0);
			checkSet[index2] = 1;
			for (int i = 1; i < triangleCount; i++)
			{
				index2 = affectEdges[i];
				if (checkSet[index2] == 0)
				{
					if (voronoiEdges[index2].x == connectedTri[i - 1].y)
					{
						connectedTri[i] = new global::Unity.Mathematics.int4(voronoiEdges[index2].x, voronoiEdges[index2].y, 0, 0);
						checkSet[index2] = 1;
						continue;
					}
					if (voronoiEdges[index2].y == connectedTri[i - 1].y)
					{
						connectedTri[i] = new global::Unity.Mathematics.int4(voronoiEdges[index2].y, voronoiEdges[index2].x, 0, 0);
						checkSet[index2] = 1;
						continue;
					}
				}
				bool flag = false;
				for (int j = 0; j < triangleCount; j++)
				{
					index2 = affectEdges[j];
					if (checkSet[index2] != 1)
					{
						if (voronoiEdges[index2].x == connectedTri[i - 1].y)
						{
							connectedTri[i] = new global::Unity.Mathematics.int4(voronoiEdges[index2].x, voronoiEdges[index2].y, 0, 0);
							checkSet[index2] = 1;
							flag = true;
							break;
						}
						if (voronoiEdges[index2].y == connectedTri[i - 1].y)
						{
							connectedTri[i] = new global::Unity.Mathematics.int4(voronoiEdges[index2].y, voronoiEdges[index2].x, 0, 0);
							checkSet[index2] = 1;
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		internal unsafe static bool Condition(global::Unity.Collections.Allocator allocator, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> pgPoints, int pgPointCount, global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> pgEdges, int pgEdgeCount, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> vertices, ref int vertexCount, ref global::Unity.Collections.NativeArray<int> indices, ref int indexCount)
		{
			float maxArea = 0f;
			float maxArea2 = 0f;
			float minArea = 0f;
			float minArea2 = 0f;
			float avgArea = 0f;
			float minEdge = 0f;
			float maxEdge = 0f;
			float avgEdge = 0f;
			bool flag = true;
			bool flag2 = true;
			int triangleCount = 0;
			int delaEdgeCount = 0;
			int resultCount = 0;
			global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Common.UTess.UTriangle> triangles = new global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Common.UTess.UTriangle>(indexCount, allocator);
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.int4> delaEdges = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.int4>(indexCount, allocator);
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.int4> voronoiEdges = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.int4>(indexCount, allocator);
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.int4> connectedTri = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.int4>(vertexCount, allocator);
			global::Unity.Collections.NativeArray<int> checkSet = new global::Unity.Collections.NativeArray<int>(indexCount, allocator);
			global::Unity.Collections.NativeArray<int> resultSet = new global::Unity.Collections.NativeArray<int>(indexCount, allocator);
			global::Unity.Collections.NativeArray<int> nativeArray = new global::Unity.Collections.NativeArray<int>(vertexCount, allocator);
			global::UnityEngine.U2D.Common.UTess.ModuleHandle.BuildTrianglesAndEdges(vertices, vertexCount, indices, indexCount, ref triangles, ref triangleCount, ref delaEdges, ref delaEdgeCount, ref maxArea, ref avgArea, ref minArea);
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.int4> refinedEdges = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.int4>(delaEdgeCount, allocator);
			global::UnityEngine.U2D.Common.UTess.ModuleHandle.InsertionSort<global::Unity.Mathematics.int4, global::UnityEngine.U2D.Common.UTess.DelaEdgeCompare>(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(delaEdges), 0, delaEdgeCount - 1, default(global::UnityEngine.U2D.Common.UTess.DelaEdgeCompare));
			RefineEdges(ref refinedEdges, ref delaEdges, ref delaEdgeCount, ref voronoiEdges);
			for (int i = 0; i < vertexCount; i++)
			{
				GetAffectingEdges(i, delaEdges, delaEdgeCount, ref resultSet, ref checkSet, ref resultCount);
				bool flag3 = resultCount != 0;
				for (int j = 0; j < resultCount; j++)
				{
					int index = resultSet[j];
					if (delaEdges[index].z == -1 || delaEdges[index].w == -1)
					{
						flag3 = false;
						break;
					}
				}
				if (flag3)
				{
					flag = ConnectTriangles(ref connectedTri, ref resultSet, ref checkSet, voronoiEdges, resultCount);
					if (!flag)
					{
						break;
					}
					global::Unity.Mathematics.float2 centroid = global::Unity.Mathematics.float2.zero;
					float area = 0f;
					float distance = 0f;
					for (int k = 0; k < resultCount; k++)
					{
						CentroidByPolygon(connectedTri[k], triangles, ref centroid, ref area, ref distance);
					}
					centroid /= 3f * area;
					pgPoints[i] = centroid;
				}
			}
			int num = indexCount;
			int num2 = vertexCount;
			indexCount = 0;
			vertexCount = 0;
			triangleCount = 0;
			if (flag)
			{
				flag2 = global::UnityEngine.U2D.Common.UTess.Tessellator.Tessellate(allocator, pgPoints, pgPointCount, pgEdges, pgEdgeCount, ref vertices, ref vertexCount, ref indices, ref indexCount);
				if (flag2)
				{
					global::UnityEngine.U2D.Common.UTess.ModuleHandle.BuildTriangles(vertices, vertexCount, indices, indexCount, ref triangles, ref triangleCount, ref maxArea2, ref avgArea, ref minArea2, ref maxEdge, ref avgEdge, ref minEdge);
				}
				flag2 = flag2 && maxArea2 < maxArea * kMaxAreaTolerance && maxEdge < avgEdge * kMaxEdgeTolerance;
			}
			triangles.Dispose();
			delaEdges.Dispose();
			refinedEdges.Dispose();
			checkSet.Dispose();
			voronoiEdges.Dispose();
			resultSet.Dispose();
			nativeArray.Dispose();
			connectedTri.Dispose();
			if (flag2 && num == indexCount)
			{
				return num2 == vertexCount;
			}
			return false;
		}
	}
}
