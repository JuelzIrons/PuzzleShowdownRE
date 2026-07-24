namespace UnityEngine.U2D.Common.UTess
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	internal struct ModuleHandle
	{
		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		private struct Int3Compare : global::System.Collections.Generic.IComparer<global::Unity.Mathematics.int3>
		{
			public int Compare(global::Unity.Mathematics.int3 a, global::Unity.Mathematics.int3 b)
			{
				if (a.x >= b.x)
				{
					if (a.x <= b.x)
					{
						return 0;
					}
					return 1;
				}
				return -1;
			}
		}

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

		internal static int GetLower<T, U, X>(global::Unity.Collections.NativeArray<T> values, int count, U check, X condition) where T : struct where U : struct where X : global::UnityEngine.U2D.Common.UTess.ICondition2<T, U>
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

		internal static int GetUpper<T, U, X>(global::Unity.Collections.NativeArray<T> values, int count, U check, X condition) where T : struct where U : struct where X : global::UnityEngine.U2D.Common.UTess.ICondition2<T, U>
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

		internal static int GetEqual<T, U, X>(global::UnityEngine.U2D.Common.UTess.Array<T> values, int count, U check, X condition) where T : struct where U : struct where X : global::UnityEngine.U2D.Common.UTess.ICondition2<T, U>
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

		internal static int GetEqual<T, U, X>(global::Unity.Collections.NativeArray<T> values, int count, U check, X condition) where T : struct where U : struct where X : global::UnityEngine.U2D.Common.UTess.ICondition2<T, U>
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

		internal static global::UnityEngine.U2D.Common.UTess.UCircle CircumCircle(global::UnityEngine.U2D.Common.UTess.UTriangle tri)
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
			return new global::UnityEngine.U2D.Common.UTess.UCircle
			{
				center = new global::Unity.Mathematics.float2(num8, num9),
				radius = global::Unity.Mathematics.math.sqrt(num10 * num10 + num11 * num11)
			};
		}

		internal static bool IsInsideCircle(global::UnityEngine.U2D.Common.UTess.UCircle c, global::Unity.Mathematics.float2 v)
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

		internal static void GetIntermediate(ushort a, ushort b, ref global::Unity.Mathematics.int3 res)
		{
			res.x = (global::Unity.Mathematics.math.min((int)a, (int)b) << 16) | global::Unity.Mathematics.math.max((int)a, (int)b);
			res.y = a;
			res.z = b;
		}

		internal unsafe static void RawSort(global::Unity.Mathematics.int3* data, int length)
		{
			for (int i = 0; i < length; i++)
			{
				int num = i;
				global::Unity.Mathematics.int3 int5 = data[i + 1];
				while (num >= 0 && int5.x < data[num].x)
				{
					data[num + 1] = data[num];
					num--;
				}
				data[num + 1] = int5;
			}
		}

		internal unsafe static int GenerateOutlineFromTriangleIndices(in global::Unity.Collections.NativeArray<ushort> indices, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> outline)
		{
			int length = indices.Length;
			int num = 0;
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.int3> nativeArray = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.int3>(length * 4, global::Unity.Collections.Allocator.Temp, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			ushort* unsafeReadOnlyPtr = (ushort*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(indices);
			global::Unity.Mathematics.int3* unsafeReadOnlyPtr2 = (global::Unity.Mathematics.int3*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(nativeArray);
			global::Unity.Mathematics.int3* ptr = unsafeReadOnlyPtr2 + length * 2;
			for (int i = 0; i < length; i += 3)
			{
				GetIntermediate(unsafeReadOnlyPtr[i], unsafeReadOnlyPtr[i + 1], ref unsafeReadOnlyPtr2[i]);
				GetIntermediate(unsafeReadOnlyPtr[i + 1], unsafeReadOnlyPtr[i + 2], ref unsafeReadOnlyPtr2[i + 1]);
				GetIntermediate(unsafeReadOnlyPtr[i + 2], unsafeReadOnlyPtr[i], ref unsafeReadOnlyPtr2[i + 2]);
			}
			global::Unity.Collections.NativeSortExtension.Sort(unsafeReadOnlyPtr2, length, default(global::UnityEngine.U2D.Common.UTess.ModuleHandle.Int3Compare));
			for (int j = 0; j < length; j++)
			{
				int num2 = (j + 1) % length;
				if (unsafeReadOnlyPtr2[j].x != unsafeReadOnlyPtr2[num2].x)
				{
					ptr[num++] = unsafeReadOnlyPtr2[j];
					continue;
				}
				for (int k = j + 1; k < length && unsafeReadOnlyPtr2[j].x == unsafeReadOnlyPtr2[k].x; k++)
				{
					j++;
				}
			}
			int l = 0;
			int num3 = 0;
			for (; l < num; l++)
			{
				if (ptr[l].x >= int.MaxValue)
				{
					continue;
				}
				bool flag = true;
				unsafeReadOnlyPtr2[num3] = ptr[l];
				ptr[l].x = int.MaxValue;
				for (int m = l + 1; m < num; m++)
				{
					if (ptr[m].x < int.MaxValue)
					{
						if (unsafeReadOnlyPtr2[num3].y == ptr[m].z || unsafeReadOnlyPtr2[num3].z == ptr[m].y)
						{
							unsafeReadOnlyPtr2[++num3] = ptr[m];
							ptr[m].x = int.MaxValue;
							m = l + 1;
						}
						flag = false;
					}
					l = (flag ? (l + 1) : l);
				}
				unsafeReadOnlyPtr2[++num3] = ptr[l];
			}
			if (num != 0)
			{
				global::Unity.Mathematics.int3* unsafeReadOnlyPtr3 = (global::Unity.Mathematics.int3*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(nativeArray);
				global::Unity.Mathematics.int2* unsafeReadOnlyPtr4 = (global::Unity.Mathematics.int2*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(outline);
				for (int n = 0; n < num; n++)
				{
					unsafeReadOnlyPtr4[n] = unsafeReadOnlyPtr3[n].yz;
				}
				return num;
			}
			return 0;
		}

		internal static void BuildTriangles(global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> vertices, int vertexCount, global::Unity.Collections.NativeArray<int> indices, int indexCount, ref global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Common.UTess.UTriangle> triangles, ref int triangleCount, ref float maxArea, ref float avgArea, ref float minArea)
		{
			for (int i = 0; i < indexCount; i += 3)
			{
				global::UnityEngine.U2D.Common.UTess.UTriangle uTriangle = default(global::UnityEngine.U2D.Common.UTess.UTriangle);
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

		internal static void BuildTriangles(global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> vertices, int vertexCount, global::Unity.Collections.NativeArray<int> indices, int indexCount, ref global::UnityEngine.U2D.Common.UTess.Array<global::UnityEngine.U2D.Common.UTess.UTriangle> triangles, ref int triangleCount, ref float maxArea, ref float avgArea, ref float minArea)
		{
			for (int i = 0; i < indexCount; i += 3)
			{
				global::UnityEngine.U2D.Common.UTess.UTriangle uTriangle = default(global::UnityEngine.U2D.Common.UTess.UTriangle);
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

		internal static void BuildTriangles(global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> vertices, int vertexCount, global::Unity.Collections.NativeArray<int> indices, int indexCount, ref global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Common.UTess.UTriangle> triangles, ref int triangleCount, ref float maxArea, ref float avgArea, ref float minArea, ref float maxEdge, ref float avgEdge, ref float minEdge)
		{
			for (int i = 0; i < indexCount; i += 3)
			{
				global::UnityEngine.U2D.Common.UTess.UTriangle uTriangle = default(global::UnityEngine.U2D.Common.UTess.UTriangle);
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

		internal static void BuildTrianglesAndEdges(global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> vertices, int vertexCount, global::Unity.Collections.NativeArray<int> indices, int indexCount, ref global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Common.UTess.UTriangle> triangles, ref int triangleCount, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.int4> delaEdges, ref int delaEdgeCount, ref float maxArea, ref float avgArea, ref float minArea)
		{
			for (int i = 0; i < indexCount; i += 3)
			{
				global::UnityEngine.U2D.Common.UTess.UTriangle uTriangle = default(global::UnityEngine.U2D.Common.UTess.UTriangle);
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
			global::UnityEngine.U2D.Common.UTess.Tessellator.Tessellate(allocator, pgPoints, pgPointCount, pgEdges, pgEdgeCount, ref outVertices, ref outVertexCount, ref outIndices, ref outIndexCount);
			pgPoints.Dispose();
			pgEdges.Dispose();
			return zero;
		}

		public static global::Unity.Mathematics.float4 Tessellate(global::Unity.Collections.Allocator allocator, in global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> points, in global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> edges, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> outVertices, out int outVertexCount, ref global::Unity.Collections.NativeArray<int> outIndices, out int outIndexCount, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> outEdges, out int outEdgeCount, bool runPlanarGraph)
		{
			global::Unity.Mathematics.float4 zero = global::Unity.Mathematics.float4.zero;
			outEdgeCount = 0;
			outIndexCount = 0;
			outVertexCount = 0;
			if (points.Length < 3 || points.Length >= kMaxVertexCount)
			{
				return zero;
			}
			bool flag = false;
			bool flag2 = false;
			int outputEdgeCount = 0;
			int outputPointCount = 0;
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> outputEdges = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2>(edges.Length * 8, allocator);
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> outputPoints = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2>(points.Length * 4, allocator);
			if (runPlanarGraph)
			{
				if (edges.Length != 0)
				{
					flag = global::UnityEngine.U2D.Common.UTess.PlanarGraph.Validate(allocator, in points, points.Length, in edges, edges.Length, ref outputPoints, out outputPointCount, ref outputEdges, out outputEdgeCount);
				}
			}
			else
			{
				outputEdgeCount = edges.Length;
				outputPointCount = points.Length;
				Copy(edges, outputEdges, outputEdgeCount);
				Copy(points, outputPoints, outputPointCount);
			}
			if (!flag)
			{
				outEdgeCount = edges.Length;
				outVertexCount = points.Length;
				Copy(edges, outEdges, edges.Length);
				Copy(points, outVertices, points.Length);
			}
			if (outputPointCount > 2 && outputEdgeCount > 2)
			{
				global::Unity.Collections.NativeArray<int> outputIndices = new global::Unity.Collections.NativeArray<int>(outputPointCount * 8, allocator);
				global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> outputVertices = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2>(outputPointCount * 4, allocator);
				int indexCount = 0;
				int vertexCount = 0;
				if (global::UnityEngine.U2D.Common.UTess.Tessellator.Tessellate(allocator, outputPoints, outputPointCount, outputEdges, outputEdgeCount, ref outputVertices, ref vertexCount, ref outputIndices, ref indexCount))
				{
					TransferOutput(outputEdges, outputEdgeCount, ref outEdges, ref outEdgeCount, outputIndices, indexCount, ref outIndices, ref outIndexCount, outputVertices, vertexCount, ref outVertices, ref outVertexCount);
					if (flag2)
					{
						outEdgeCount = 0;
					}
				}
				outputVertices.Dispose();
				outputIndices.Dispose();
			}
			outputPoints.Dispose();
			outputEdges.Dispose();
			return zero;
		}

		public static global::Unity.Mathematics.float4 Subdivide(global::Unity.Collections.Allocator allocator, global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> points, global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> edges, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> outVertices, ref int outVertexCount, ref global::Unity.Collections.NativeArray<int> outIndices, ref int outIndexCount, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> outEdges, ref int outEdgeCount, float areaFactor, float targetArea, int refineIterations, int smoothenIterations)
		{
			global::Unity.Mathematics.float4 zero = global::Unity.Mathematics.float4.zero;
			outEdgeCount = 0;
			outIndexCount = 0;
			outVertexCount = 0;
			if (points.Length < 3 || points.Length >= kMaxVertexCount || edges.Length == 0)
			{
				return zero;
			}
			int indexCount = 0;
			int vertexCount = 0;
			global::Unity.Collections.NativeArray<int> outputIndices = new global::Unity.Collections.NativeArray<int>(kMaxIndexCount, allocator);
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> outputVertices = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2>(kMaxVertexCount, allocator);
			bool flag = global::UnityEngine.U2D.Common.UTess.Tessellator.Tessellate(allocator, points, points.Length, edges, edges.Length, ref outputVertices, ref vertexCount, ref outputIndices, ref indexCount);
			bool flag2 = false;
			bool flag3 = targetArea != 0f || areaFactor != 0f;
			if (flag && flag3)
			{
				float maxArea = 0f;
				float num = 0f;
				int dstEdgeCount = 0;
				int dstPointCount = 0;
				int dstIndexCount = 0;
				int dstVertexCount = 0;
				global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> dstEdges = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2>(kMaxEdgeCount, allocator);
				global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> dstPoints = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2>(kMaxVertexCount, allocator);
				global::Unity.Collections.NativeArray<int> dstIndices = new global::Unity.Collections.NativeArray<int>(kMaxIndexCount, allocator);
				global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> dstVertices = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2>(kMaxVertexCount, allocator);
				zero.x = 0f;
				refineIterations = global::System.Math.Min(refineIterations, kMaxRefineIterations);
				if (targetArea != 0f)
				{
					num = targetArea / 10f;
					while (targetArea < (float)kMaxArea && refineIterations > 0)
					{
						CopyGraph(points, points.Length, ref dstPoints, ref dstPointCount, edges, edges.Length, ref dstEdges, ref dstEdgeCount);
						CopyGeometry(outputIndices, indexCount, ref dstIndices, ref dstIndexCount, outputVertices, vertexCount, ref dstVertices, ref dstVertexCount);
						flag2 = global::UnityEngine.U2D.Common.UTess.Refinery.Condition(allocator, areaFactor, targetArea, ref dstPoints, ref dstPointCount, ref dstEdges, ref dstEdgeCount, ref dstVertices, ref dstVertexCount, ref dstIndices, ref dstIndexCount, ref maxArea);
						if (flag2 && dstIndexCount > dstPointCount)
						{
							zero.x = areaFactor;
							TransferOutput(dstEdges, dstEdgeCount, ref outEdges, ref outEdgeCount, dstIndices, dstIndexCount, ref outIndices, ref outIndexCount, dstVertices, dstVertexCount, ref outVertices, ref outVertexCount);
							break;
						}
						flag2 = false;
						targetArea += num;
						refineIterations--;
					}
				}
				else if (areaFactor != 0f)
				{
					areaFactor = global::Unity.Mathematics.math.lerp(0.1f, 0.54f, (areaFactor - 0.05f) / 0.45f);
					num = areaFactor / 10f;
					while (areaFactor < 0.8f && refineIterations > 0)
					{
						CopyGraph(points, points.Length, ref dstPoints, ref dstPointCount, edges, edges.Length, ref dstEdges, ref dstEdgeCount);
						CopyGeometry(outputIndices, indexCount, ref dstIndices, ref dstIndexCount, outputVertices, vertexCount, ref dstVertices, ref dstVertexCount);
						flag2 = global::UnityEngine.U2D.Common.UTess.Refinery.Condition(allocator, areaFactor, targetArea, ref dstPoints, ref dstPointCount, ref dstEdges, ref dstEdgeCount, ref dstVertices, ref dstVertexCount, ref dstIndices, ref dstIndexCount, ref maxArea);
						if (flag2 && dstIndexCount > dstPointCount)
						{
							zero.x = areaFactor;
							TransferOutput(dstEdges, dstEdgeCount, ref outEdges, ref outEdgeCount, dstIndices, dstIndexCount, ref outIndices, ref outIndexCount, dstVertices, dstVertexCount, ref outVertices, ref outVertexCount);
							break;
						}
						flag2 = false;
						areaFactor += num;
						refineIterations--;
					}
				}
				if (flag2)
				{
					if (zero.x != 0f)
					{
						VertexCleanupConditioner(vertexCount, ref dstIndices, ref dstIndexCount, ref dstVertices, ref dstVertexCount);
					}
					zero.y = 0f;
					smoothenIterations = global::Unity.Mathematics.math.clamp(smoothenIterations, 0, kMaxSmoothenIterations);
					while (smoothenIterations > 0 && global::UnityEngine.U2D.Common.UTess.Smoothen.Condition(allocator, ref dstPoints, dstPointCount, dstEdges, dstEdgeCount, ref dstVertices, ref dstVertexCount, ref dstIndices, ref dstIndexCount))
					{
						zero.y = smoothenIterations;
						TransferOutput(dstEdges, dstEdgeCount, ref outEdges, ref outEdgeCount, dstIndices, dstIndexCount, ref outIndices, ref outIndexCount, dstVertices, dstVertexCount, ref outVertices, ref outVertexCount);
						smoothenIterations--;
					}
					if (zero.y != 0f)
					{
						VertexCleanupConditioner(vertexCount, ref outIndices, ref outIndexCount, ref outVertices, ref outVertexCount);
					}
				}
				dstVertices.Dispose();
				dstIndices.Dispose();
				dstPoints.Dispose();
				dstEdges.Dispose();
			}
			if (flag && !flag2)
			{
				TransferOutput(edges, edges.Length, ref outEdges, ref outEdgeCount, outputIndices, indexCount, ref outIndices, ref outIndexCount, outputVertices, vertexCount, ref outVertices, ref outVertexCount);
			}
			outputVertices.Dispose();
			outputIndices.Dispose();
			return zero;
		}
	}
}
