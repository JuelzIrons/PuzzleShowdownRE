namespace UnityEngine.Rendering.Universal.UTess
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	internal struct ModuleHandle
	{
		internal static readonly int kMaxArea = 65536;

		internal static readonly int kMaxEdgeCount = 65536;

		internal static readonly int kMaxIndexCount = 65536;

		internal static readonly int kMaxVertexCount = 65536;

		internal static readonly int kMaxTriangleCount = kMaxIndexCount / 3;

		internal static readonly int kMaxRefineIterations = 48;

		internal static readonly int kMaxSmoothenIterations = 256;

		internal static readonly float kIncrementAreaFactor = 1.2f;

		internal static void Copy<T>(global::Unity.Collections.NativeArray<T> src, int srcIndex, global::Unity.Collections.NativeArray<T> dst, int dstIndex, int length) where T : struct
		{
			global::Unity.Collections.NativeArray<T>.Copy(src, srcIndex, dst, dstIndex, length);
		}

		internal static void Copy<T>(global::Unity.Collections.NativeArray<T> src, global::Unity.Collections.NativeArray<T> dst, int length) where T : struct
		{
			Copy(src, 0, dst, 0, length);
		}

		internal unsafe static void InsertionSort<T, U>(void* array, int lo, int hi, U comp) where T : struct where U : global::System.Collections.Generic.IComparer<T>
		{
			for (int i = lo; i < hi; i++)
			{
				int num = i;
				T val = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(array, i + 1);
				while (num >= lo && comp.Compare(val, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(array, num)) < 0)
				{
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(array, num + 1, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(array, num));
					num--;
				}
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(array, num + 1, val);
			}
		}

		internal static int GetLower<T, U, X>(global::Unity.Collections.NativeArray<T> values, int count, U check, X condition) where T : struct where U : struct where X : global::UnityEngine.Rendering.Universal.UTess.ICondition2<T, U>
		{
			int num = 0;
			int num2 = count - 1;
			int result = num - 1;
			while (num <= num2)
			{
				int num3 = num + num2 >> 1;
				float t = 0f;
				if (condition.Test(values[num3], check, ref t))
				{
					result = num3;
					num = num3 + 1;
				}
				else
				{
					num2 = num3 - 1;
				}
			}
			return result;
		}

		internal static int GetUpper<T, U, X>(global::Unity.Collections.NativeArray<T> values, int count, U check, X condition) where T : struct where U : struct where X : global::UnityEngine.Rendering.Universal.UTess.ICondition2<T, U>
		{
			int num = 0;
			int num2 = count - 1;
			int result = num2 + 1;
			while (num <= num2)
			{
				int num3 = num + num2 >> 1;
				float t = 0f;
				if (condition.Test(values[num3], check, ref t))
				{
					result = num3;
					num2 = num3 - 1;
				}
				else
				{
					num = num3 + 1;
				}
			}
			return result;
		}

		internal static int GetEqual<T, U, X>(global::Unity.Collections.NativeArray<T> values, int count, U check, X condition) where T : struct where U : struct where X : global::UnityEngine.Rendering.Universal.UTess.ICondition2<T, U>
		{
			int num = 0;
			int num2 = count - 1;
			while (num <= num2)
			{
				int num3 = num + num2 >> 1;
				float t = 0f;
				condition.Test(values[num3], check, ref t);
				if (t == 0f)
				{
					return num3;
				}
				if (t <= 0f)
				{
					num = num3 + 1;
				}
				else
				{
					num2 = num3 - 1;
				}
			}
			return -1;
		}

		internal static float OrientFast(global::Unity.Mathematics.float2 a, global::Unity.Mathematics.float2 b, global::Unity.Mathematics.float2 c)
		{
			float num = 1.110223E-16f;
			float num2 = (b.y - a.y) * (c.x - b.x) - (b.x - a.x) * (c.y - b.y);
			if (global::Unity.Mathematics.math.abs(num2) < num)
			{
				return 0f;
			}
			return num2;
		}

		internal static double OrientFastDouble(global::Unity.Mathematics.double2 a, global::Unity.Mathematics.double2 b, global::Unity.Mathematics.double2 c)
		{
			double num = 1.1102230246251565E-16;
			double num2 = (b.y - a.y) * (c.x - b.x) - (b.x - a.x) * (c.y - b.y);
			if (global::Unity.Mathematics.math.abs(num2) < num)
			{
				return 0.0;
			}
			return num2;
		}

		internal static global::UnityEngine.Rendering.Universal.UTess.UCircle CircumCircle(global::UnityEngine.Rendering.Universal.UTess.UTriangle tri)
		{
			float num = tri.va.x * tri.va.x;
			float num2 = tri.vb.x * tri.vb.x;
			float num3 = tri.vc.x * tri.vc.x;
			float num4 = tri.va.y * tri.va.y;
			float num5 = tri.vb.y * tri.vb.y;
			float num6 = tri.vc.y * tri.vc.y;
			float num7 = 2f * ((tri.vb.x - tri.va.x) * (tri.vc.y - tri.va.y) - (tri.vb.y - tri.va.y) * (tri.vc.x - tri.va.x));
			float num8 = ((tri.vc.y - tri.va.y) * (num2 - num + num5 - num4) + (tri.va.y - tri.vb.y) * (num3 - num + num6 - num4)) / num7;
			float num9 = ((tri.va.x - tri.vc.x) * (num2 - num + num5 - num4) + (tri.vb.x - tri.va.x) * (num3 - num + num6 - num4)) / num7;
			float num10 = tri.va.x - num8;
			float num11 = tri.va.y - num9;
			return new global::UnityEngine.Rendering.Universal.UTess.UCircle
			{
				center = new global::Unity.Mathematics.float2(num8, num9),
				radius = global::Unity.Mathematics.math.sqrt(num10 * num10 + num11 * num11)
			};
		}

		internal static bool IsInsideCircle(global::UnityEngine.Rendering.Universal.UTess.UCircle c, global::Unity.Mathematics.float2 v)
		{
			return global::Unity.Mathematics.math.distance(v, c.center) < c.radius;
		}

		internal static float TriangleArea(global::Unity.Mathematics.float2 va, global::Unity.Mathematics.float2 vb, global::Unity.Mathematics.float2 vc)
		{
			global::Unity.Mathematics.float3 float5 = new global::Unity.Mathematics.float3(va.x, va.y, 0f);
			global::Unity.Mathematics.float3 float6 = new global::Unity.Mathematics.float3(vb.x, vb.y, 0f);
			return global::Unity.Mathematics.math.abs(global::Unity.Mathematics.math.cross(y: float5 - new global::Unity.Mathematics.float3(vc.x, vc.y, 0f), x: float5 - float6).z) * 0.5f;
		}

		internal static float Sign(global::Unity.Mathematics.float2 p1, global::Unity.Mathematics.float2 p2, global::Unity.Mathematics.float2 p3)
		{
			return (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);
		}

		internal static bool IsInsideTriangle(global::Unity.Mathematics.float2 pt, global::Unity.Mathematics.float2 v1, global::Unity.Mathematics.float2 v2, global::Unity.Mathematics.float2 v3)
		{
			float num = Sign(pt, v1, v2);
			float num2 = Sign(pt, v2, v3);
			float num3 = Sign(pt, v3, v1);
			bool flag = num < 0f || num2 < 0f || num3 < 0f;
			bool flag2 = num > 0f || num2 > 0f || num3 > 0f;
			return !(flag && flag2);
		}

		internal static bool IsInsideTriangleApproximate(global::Unity.Mathematics.float2 pt, global::Unity.Mathematics.float2 v1, global::Unity.Mathematics.float2 v2, global::Unity.Mathematics.float2 v3)
		{
			float num = TriangleArea(v1, v2, v3);
			float num2 = TriangleArea(pt, v1, v2);
			float num3 = TriangleArea(pt, v2, v3);
			float num4 = TriangleArea(pt, v3, v1);
			float num5 = 1.110223E-16f;
			return global::UnityEngine.Mathf.Abs(num - (num2 + num3 + num4)) < num5;
		}

		internal static bool IsInsideCircle(global::Unity.Mathematics.float2 a, global::Unity.Mathematics.float2 b, global::Unity.Mathematics.float2 c, global::Unity.Mathematics.float2 p)
		{
			float num = global::Unity.Mathematics.math.dot(a, a);
			float num2 = global::Unity.Mathematics.math.dot(b, b);
			float num3 = global::Unity.Mathematics.math.dot(c, c);
			float x = a.x;
			float y = a.y;
			float x2 = b.x;
			float y2 = b.y;
			float x3 = c.x;
			float y3 = c.y;
			float num4 = (num * (y3 - y2) + num2 * (y - y3) + num3 * (y2 - y)) / (x * (y3 - y2) + x2 * (y - y3) + x3 * (y2 - y));
			float num5 = (num * (x3 - x2) + num2 * (x - x3) + num3 * (x2 - x)) / (y * (x3 - x2) + y2 * (x - x3) + y3 * (x2 - x));
			global::Unity.Mathematics.float2 y4 = new global::Unity.Mathematics.float2
			{
				x = num4 / 2f,
				y = num5 / 2f
			};
			float num6 = global::Unity.Mathematics.math.distance(a, y4);
			float num7 = global::Unity.Mathematics.math.distance(p, y4);
			return num6 - num7 > 1E-05f;
		}

		internal static void BuildTriangles(global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> vertices, int vertexCount, global::Unity.Collections.NativeArray<int> indices, int indexCount, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.UTess.UTriangle> triangles, ref int triangleCount, ref float maxArea, ref float avgArea, ref float minArea)
		{
			for (int i = 0; i < indexCount; i += 3)
			{
				global::UnityEngine.Rendering.Universal.UTess.UTriangle uTriangle = default(global::UnityEngine.Rendering.Universal.UTess.UTriangle);
				int index = indices[i];
				int index2 = indices[i + 1];
				int index3 = indices[i + 2];
				uTriangle.va = vertices[index];
				uTriangle.vb = vertices[index2];
				uTriangle.vc = vertices[index3];
				uTriangle.c = CircumCircle(uTriangle);
				uTriangle.area = TriangleArea(uTriangle.va, uTriangle.vb, uTriangle.vc);
				maxArea = global::Unity.Mathematics.math.max(uTriangle.area, maxArea);
				minArea = global::Unity.Mathematics.math.min(uTriangle.area, minArea);
				avgArea += uTriangle.area;
				triangles[triangleCount++] = uTriangle;
			}
			avgArea /= triangleCount;
		}

		internal static void BuildTriangles(global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> vertices, int vertexCount, global::Unity.Collections.NativeArray<int> indices, int indexCount, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.UTess.UTriangle> triangles, ref int triangleCount, ref float maxArea, ref float avgArea, ref float minArea, ref float maxEdge, ref float avgEdge, ref float minEdge)
		{
			for (int i = 0; i < indexCount; i += 3)
			{
				global::UnityEngine.Rendering.Universal.UTess.UTriangle uTriangle = default(global::UnityEngine.Rendering.Universal.UTess.UTriangle);
				int index = indices[i];
				int index2 = indices[i + 1];
				int index3 = indices[i + 2];
				uTriangle.va = vertices[index];
				uTriangle.vb = vertices[index2];
				uTriangle.vc = vertices[index3];
				uTriangle.c = CircumCircle(uTriangle);
				uTriangle.area = TriangleArea(uTriangle.va, uTriangle.vb, uTriangle.vc);
				maxArea = global::Unity.Mathematics.math.max(uTriangle.area, maxArea);
				minArea = global::Unity.Mathematics.math.min(uTriangle.area, minArea);
				avgArea += uTriangle.area;
				float num = global::Unity.Mathematics.math.distance(uTriangle.va, uTriangle.vb);
				float num2 = global::Unity.Mathematics.math.distance(uTriangle.vb, uTriangle.vc);
				float num3 = global::Unity.Mathematics.math.distance(uTriangle.vc, uTriangle.va);
				maxEdge = global::Unity.Mathematics.math.max(num, maxEdge);
				maxEdge = global::Unity.Mathematics.math.max(num2, maxEdge);
				maxEdge = global::Unity.Mathematics.math.max(num3, maxEdge);
				minEdge = global::Unity.Mathematics.math.min(num, minEdge);
				minEdge = global::Unity.Mathematics.math.min(num2, minEdge);
				minEdge = global::Unity.Mathematics.math.min(num3, minEdge);
				avgEdge += num;
				avgEdge += num2;
				avgEdge += num3;
				triangles[triangleCount++] = uTriangle;
			}
			avgArea /= triangleCount;
			avgEdge /= indexCount;
		}

		internal static void BuildTrianglesAndEdges(global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> vertices, int vertexCount, global::Unity.Collections.NativeArray<int> indices, int indexCount, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.UTess.UTriangle> triangles, ref int triangleCount, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.int4> delaEdges, ref int delaEdgeCount, ref float maxArea, ref float avgArea, ref float minArea)
		{
			for (int i = 0; i < indexCount; i += 3)
			{
				global::UnityEngine.Rendering.Universal.UTess.UTriangle uTriangle = default(global::UnityEngine.Rendering.Universal.UTess.UTriangle);
				int num = indices[i];
				int num2 = indices[i + 1];
				int num3 = indices[i + 2];
				uTriangle.va = vertices[num];
				uTriangle.vb = vertices[num2];
				uTriangle.vc = vertices[num3];
				uTriangle.c = CircumCircle(uTriangle);
				uTriangle.area = TriangleArea(uTriangle.va, uTriangle.vb, uTriangle.vc);
				maxArea = global::Unity.Mathematics.math.max(uTriangle.area, maxArea);
				minArea = global::Unity.Mathematics.math.min(uTriangle.area, minArea);
				avgArea += uTriangle.area;
				uTriangle.indices = new global::Unity.Mathematics.int3(num, num2, num3);
				delaEdges[delaEdgeCount++] = new global::Unity.Mathematics.int4(global::Unity.Mathematics.math.min(num, num2), global::Unity.Mathematics.math.max(num, num2), triangleCount, -1);
				delaEdges[delaEdgeCount++] = new global::Unity.Mathematics.int4(global::Unity.Mathematics.math.min(num2, num3), global::Unity.Mathematics.math.max(num2, num3), triangleCount, -1);
				delaEdges[delaEdgeCount++] = new global::Unity.Mathematics.int4(global::Unity.Mathematics.math.min(num3, num), global::Unity.Mathematics.math.max(num3, num), triangleCount, -1);
				triangles[triangleCount++] = uTriangle;
			}
			avgArea /= triangleCount;
		}

		private static void CopyGraph(global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> srcPoints, int srcPointCount, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> dstPoints, ref int dstPointCount, global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> srcEdges, int srcEdgeCount, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> dstEdges, ref int dstEdgeCount)
		{
			dstEdgeCount = srcEdgeCount;
			dstPointCount = srcPointCount;
			Copy(srcEdges, dstEdges, srcEdgeCount);
			Copy(srcPoints, dstPoints, srcPointCount);
		}

		private static void CopyGeometry(global::Unity.Collections.NativeArray<int> srcIndices, int srcIndexCount, ref global::Unity.Collections.NativeArray<int> dstIndices, ref int dstIndexCount, global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> srcVertices, int srcVertexCount, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> dstVertices, ref int dstVertexCount)
		{
			dstIndexCount = srcIndexCount;
			dstVertexCount = srcVertexCount;
			Copy(srcIndices, dstIndices, srcIndexCount);
			Copy(srcVertices, dstVertices, srcVertexCount);
		}

		private static void TransferOutput(global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> srcEdges, int srcEdgeCount, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> dstEdges, ref int dstEdgeCount, global::Unity.Collections.NativeArray<int> srcIndices, int srcIndexCount, ref global::Unity.Collections.NativeArray<int> dstIndices, ref int dstIndexCount, global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> srcVertices, int srcVertexCount, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> dstVertices, ref int dstVertexCount)
		{
			dstEdgeCount = srcEdgeCount;
			dstIndexCount = srcIndexCount;
			dstVertexCount = srcVertexCount;
			Copy(srcEdges, dstEdges, srcEdgeCount);
			Copy(srcIndices, dstIndices, srcIndexCount);
			Copy(srcVertices, dstVertices, srcVertexCount);
		}

		private static void GraphConditioner(global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> points, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> pgPoints, ref int pgPointCount, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> pgEdges, ref int pgEdgeCount, bool resetTopology)
		{
			global::Unity.Mathematics.float2 float5 = new global::Unity.Mathematics.float2(float.PositiveInfinity, float.PositiveInfinity);
			global::Unity.Mathematics.float2 float6 = global::Unity.Mathematics.float2.zero;
			for (int i = 0; i < points.Length; i++)
			{
				float5 = global::Unity.Mathematics.math.min(points[i], float5);
				float6 = global::Unity.Mathematics.math.max(points[i], float6);
			}
			global::Unity.Mathematics.float2 float7 = (float6 - float5) * 0.5f;
			float num = 0.0001f;
			pgPointCount = ((!resetTopology) ? pgPointCount : 0);
			int num2 = pgPointCount;
			pgPoints[pgPointCount++] = new global::Unity.Mathematics.float2(float5.x, float5.y);
			pgPoints[pgPointCount++] = new global::Unity.Mathematics.float2(float5.x - num, float5.y + float7.y);
			pgPoints[pgPointCount++] = new global::Unity.Mathematics.float2(float5.x, float6.y);
			pgPoints[pgPointCount++] = new global::Unity.Mathematics.float2(float5.x + float7.x, float6.y + num);
			pgPoints[pgPointCount++] = new global::Unity.Mathematics.float2(float6.x, float6.y);
			pgPoints[pgPointCount++] = new global::Unity.Mathematics.float2(float6.x + num, float5.y + float7.y);
			pgPoints[pgPointCount++] = new global::Unity.Mathematics.float2(float6.x, float5.y);
			pgPoints[pgPointCount++] = new global::Unity.Mathematics.float2(float5.x + float7.x, float5.y - num);
			pgEdgeCount = 8;
			pgEdges[0] = new global::Unity.Mathematics.int2(num2, num2 + 1);
			pgEdges[1] = new global::Unity.Mathematics.int2(num2 + 1, num2 + 2);
			pgEdges[2] = new global::Unity.Mathematics.int2(num2 + 2, num2 + 3);
			pgEdges[3] = new global::Unity.Mathematics.int2(num2 + 3, num2 + 4);
			pgEdges[4] = new global::Unity.Mathematics.int2(num2 + 4, num2 + 5);
			pgEdges[5] = new global::Unity.Mathematics.int2(num2 + 5, num2 + 6);
			pgEdges[6] = new global::Unity.Mathematics.int2(num2 + 6, num2 + 7);
			pgEdges[7] = new global::Unity.Mathematics.int2(num2 + 7, num2);
		}

		private static void Reorder(int startVertexCount, int index, ref global::Unity.Collections.NativeArray<int> indices, ref int indexCount, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> vertices, ref int vertexCount)
		{
			bool flag = false;
			for (int i = 0; i < indexCount; i++)
			{
				if (indices[i] == index)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				return;
			}
			vertexCount--;
			vertices[index] = vertices[vertexCount];
			for (int j = 0; j < indexCount; j++)
			{
				if (indices[j] == vertexCount)
				{
					indices[j] = index;
				}
			}
		}

		internal static void VertexCleanupConditioner(int startVertexCount, ref global::Unity.Collections.NativeArray<int> indices, ref int indexCount, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> vertices, ref int vertexCount)
		{
			for (int i = startVertexCount; i < vertexCount; i++)
			{
				Reorder(startVertexCount, i, ref indices, ref indexCount, ref vertices, ref vertexCount);
			}
		}

		public static global::Unity.Mathematics.float4 ConvexQuad(global::Unity.Collections.Allocator allocator, global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> points, global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> edges, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> outVertices, ref int outVertexCount, ref global::Unity.Collections.NativeArray<int> outIndices, ref int outIndexCount, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> outEdges, ref int outEdgeCount)
		{
			global::Unity.Mathematics.float4 zero = global::Unity.Mathematics.float4.zero;
			outEdgeCount = 0;
			outIndexCount = 0;
			outVertexCount = 0;
			if (points.Length < 3 || points.Length >= kMaxVertexCount)
			{
				return zero;
			}
			int pgEdgeCount = 0;
			int pgPointCount = 0;
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> pgEdges = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2>(kMaxEdgeCount, allocator);
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> pgPoints = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2>(kMaxVertexCount, allocator);
			GraphConditioner(points, ref pgPoints, ref pgPointCount, ref pgEdges, ref pgEdgeCount, resetTopology: true);
			global::UnityEngine.Rendering.Universal.UTess.Tessellator.Tessellate(allocator, pgPoints, pgPointCount, pgEdges, pgEdgeCount, ref outVertices, ref outVertexCount, ref outIndices, ref outIndexCount);
			pgPoints.Dispose();
			pgEdges.Dispose();
			return zero;
		}

		public static global::Unity.Mathematics.float4 Tessellate(global::Unity.Collections.Allocator allocator, global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> points, global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> edges, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> outVertices, ref int outVertexCount, ref global::Unity.Collections.NativeArray<int> outIndices, ref int outIndexCount, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> outEdges, ref int outEdgeCount)
		{
			global::Unity.Mathematics.float4 zero = global::Unity.Mathematics.float4.zero;
			outEdgeCount = 0;
			outIndexCount = 0;
			outVertexCount = 0;
			if (points.Length < 3 || points.Length >= kMaxVertexCount)
			{
				return zero;
			}
			global::Unity.Collections.NativeArray<int> outputIndices = new global::Unity.Collections.NativeArray<int>(points.Length * 8, allocator);
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> outputVertices = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2>(points.Length * 4, allocator);
			int indexCount = 0;
			int vertexCount = 0;
			if (global::UnityEngine.Rendering.Universal.UTess.Tessellator.Tessellate(allocator, points, points.Length, edges, edges.Length, ref outputVertices, ref vertexCount, ref outputIndices, ref indexCount))
			{
				TransferOutput(edges, edges.Length, ref outEdges, ref outEdgeCount, outputIndices, indexCount, ref outIndices, ref outIndexCount, outputVertices, vertexCount, ref outVertices, ref outVertexCount);
			}
			outputVertices.Dispose();
			outputIndices.Dispose();
			return zero;
		}
	}
}
