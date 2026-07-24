namespace UnityEngine.U2D.Common.UTess
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	internal struct Refinery
	{
		private static readonly float kMinAreaFactor = 0.0482f;

		private static readonly float kMaxAreaFactor = 0.482f;

		private static readonly int kMaxSteinerCount = 4084;

		private static bool RequiresRefining(global::UnityEngine.U2D.Common.UTess.UTriangle tri, float maxArea)
		{
			return tri.area > maxArea;
		}

		private static void FetchEncroachedSegments(global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> pgPoints, int pgPointCount, global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> pgEdges, int pgEdgeCount, ref global::UnityEngine.U2D.Common.UTess.Array<global::UnityEngine.U2D.Common.UTess.UEncroachingSegment> encroach, ref int encroachCount, global::UnityEngine.U2D.Common.UTess.UCircle c)
		{
			for (int i = 0; i < pgEdgeCount; i++)
			{
				global::Unity.Mathematics.int2 int5 = pgEdges[i];
				global::Unity.Mathematics.float2 float5 = pgPoints[int5.x];
				global::Unity.Mathematics.float2 float6 = pgPoints[int5.y];
				if (global::Unity.Mathematics.math.any(c.center - float5) && global::Unity.Mathematics.math.any(c.center - float6))
				{
					global::Unity.Mathematics.float2 x = float5 - float6;
					global::Unity.Mathematics.float2 obj = (float5 + float6) * 0.5f;
					float num = global::Unity.Mathematics.math.length(x) * 0.5f;
					if (!(global::Unity.Mathematics.math.length(obj - c.center) > num))
					{
						global::UnityEngine.U2D.Common.UTess.UEncroachingSegment value = new global::UnityEngine.U2D.Common.UTess.UEncroachingSegment
						{
							a = float5,
							b = float6,
							index = i
						};
						encroach[encroachCount++] = value;
					}
				}
			}
		}

		private static void InsertVertex(ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> pgPoints, ref int pgPointCount, global::Unity.Mathematics.float2 newVertex, ref int nid)
		{
			nid = pgPointCount;
			pgPoints[nid] = newVertex;
			pgPointCount++;
		}

		private static void SplitSegments(ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> pgPoints, ref int pgPointCount, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> pgEdges, ref int pgEdgeCount, global::UnityEngine.U2D.Common.UTess.UEncroachingSegment es)
		{
			int index = es.index;
			global::Unity.Mathematics.int2 int5 = pgEdges[index];
			global::Unity.Mathematics.float2 obj = pgPoints[int5.x];
			global::Unity.Mathematics.float2 float5 = pgPoints[int5.y];
			global::Unity.Mathematics.float2 float6 = (obj + float5) * 0.5f;
			int num = 0;
			if (global::Unity.Mathematics.math.abs(int5.x - int5.y) == 1)
			{
				num = ((int5.x > int5.y) ? int5.x : int5.y);
				InsertVertex(ref pgPoints, ref pgPointCount, float6, ref num);
				global::Unity.Mathematics.int2 int6 = pgEdges[index];
				pgEdges[index] = new global::Unity.Mathematics.int2(int6.x, num);
				for (int num2 = pgEdgeCount; num2 > index + 1; num2--)
				{
					pgEdges[num2] = pgEdges[num2 - 1];
				}
				pgEdges[index + 1] = new global::Unity.Mathematics.int2(num, int6.y);
				pgEdgeCount++;
			}
			else
			{
				num = pgPointCount;
				pgPoints[pgPointCount++] = float6;
				pgEdges[index] = new global::Unity.Mathematics.int2(global::Unity.Mathematics.math.max(int5.x, int5.y), num);
				pgEdges[pgEdgeCount++] = new global::Unity.Mathematics.int2(global::Unity.Mathematics.math.min(int5.x, int5.y), num);
			}
		}

		internal static bool Condition(global::Unity.Collections.Allocator allocator, float factorArea, float targetArea, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> pgPoints, ref int pgPointCount, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> pgEdges, ref int pgEdgeCount, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> vertices, ref int vertexCount, ref global::Unity.Collections.NativeArray<int> indices, ref int indexCount, ref float maxArea)
		{
			maxArea = 0f;
			float minArea = 0f;
			float avgArea = 0f;
			bool flag = false;
			bool flag2 = true;
			int triangleCount = 0;
			int num = -1;
			int num2 = pgPointCount;
			global::UnityEngine.U2D.Common.UTess.Array<global::UnityEngine.U2D.Common.UTess.UEncroachingSegment> encroach = new global::UnityEngine.U2D.Common.UTess.Array<global::UnityEngine.U2D.Common.UTess.UEncroachingSegment>(num2, global::UnityEngine.U2D.Common.UTess.ModuleHandle.kMaxEdgeCount, allocator, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::UnityEngine.U2D.Common.UTess.Array<global::UnityEngine.U2D.Common.UTess.UTriangle> triangles = new global::UnityEngine.U2D.Common.UTess.Array<global::UnityEngine.U2D.Common.UTess.UTriangle>(num2 * 4, global::UnityEngine.U2D.Common.UTess.ModuleHandle.kMaxTriangleCount, allocator, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::UnityEngine.U2D.Common.UTess.ModuleHandle.BuildTriangles(vertices, vertexCount, indices, indexCount, ref triangles, ref triangleCount, ref maxArea, ref avgArea, ref minArea);
			factorArea = ((factorArea != 0f) ? global::Unity.Mathematics.math.clamp(factorArea, kMinAreaFactor, kMaxAreaFactor) : factorArea);
			float x = maxArea * factorArea;
			x = global::Unity.Mathematics.math.max(x, targetArea);
			while (!flag && flag2)
			{
				for (int i = 0; i < triangleCount; i++)
				{
					if (RequiresRefining(triangles[i], x))
					{
						num = i;
						break;
					}
				}
				if (num != -1)
				{
					global::UnityEngine.U2D.Common.UTess.UTriangle uTriangle = triangles[num];
					int encroachCount = 0;
					FetchEncroachedSegments(pgPoints, pgPointCount, pgEdges, pgEdgeCount, ref encroach, ref encroachCount, uTriangle.c);
					if (encroachCount != 0)
					{
						for (int j = 0; j < encroachCount; j++)
						{
							SplitSegments(ref pgPoints, ref pgPointCount, ref pgEdges, ref pgEdgeCount, encroach[j]);
						}
					}
					else
					{
						global::Unity.Mathematics.float2 center = uTriangle.c.center;
						pgPoints[pgPointCount++] = center;
					}
					indexCount = 0;
					vertexCount = 0;
					flag2 = global::UnityEngine.U2D.Common.UTess.Tessellator.Tessellate(allocator, pgPoints, pgPointCount, pgEdges, pgEdgeCount, ref vertices, ref vertexCount, ref indices, ref indexCount);
					encroachCount = 0;
					triangleCount = 0;
					num = -1;
					if (flag2)
					{
						global::UnityEngine.U2D.Common.UTess.ModuleHandle.BuildTriangles(vertices, vertexCount, indices, indexCount, ref triangles, ref triangleCount, ref maxArea, ref avgArea, ref minArea);
					}
					if (pgPointCount - num2 > kMaxSteinerCount)
					{
						break;
					}
				}
				else
				{
					flag = true;
				}
			}
			triangles.Dispose();
			encroach.Dispose();
			return flag;
		}
	}
}
