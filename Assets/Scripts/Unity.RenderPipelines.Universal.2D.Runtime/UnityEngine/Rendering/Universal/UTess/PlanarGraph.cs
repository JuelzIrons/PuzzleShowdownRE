namespace UnityEngine.Rendering.Universal.UTess
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	internal struct PlanarGraph
	{
		private static readonly double kEpsilon = 1E-05;

		private static readonly int kMaxIntersectionTolerance = 4;

		internal unsafe static void RemoveDuplicateEdges(ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> edges, ref int edgeCount, global::Unity.Collections.NativeArray<int> duplicates, int duplicateCount)
		{
			if (duplicateCount == 0)
			{
				for (int i = 0; i < edgeCount; i++)
				{
					global::Unity.Mathematics.int2 value = edges[i];
					value.x = global::Unity.Mathematics.math.min(edges[i].x, edges[i].y);
					value.y = global::Unity.Mathematics.math.max(edges[i].x, edges[i].y);
					edges[i] = value;
				}
			}
			else
			{
				for (int j = 0; j < edgeCount; j++)
				{
					global::Unity.Mathematics.int2 value2 = edges[j];
					int x = duplicates[value2.x];
					int y = duplicates[value2.y];
					value2.x = global::Unity.Mathematics.math.min(x, y);
					value2.y = global::Unity.Mathematics.math.max(x, y);
					edges[j] = value2;
				}
			}
			global::UnityEngine.Rendering.Universal.UTess.ModuleHandle.InsertionSort<global::Unity.Mathematics.int2, global::UnityEngine.Rendering.Universal.UTess.TessEdgeCompare>(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(edges), 0, edgeCount - 1, default(global::UnityEngine.Rendering.Universal.UTess.TessEdgeCompare));
			int num = 1;
			for (int k = 1; k < edgeCount; k++)
			{
				global::Unity.Mathematics.int2 int5 = edges[k - 1];
				global::Unity.Mathematics.int2 value3 = edges[k];
				if ((value3.x != int5.x || value3.y != int5.y) && value3.x != value3.y)
				{
					edges[num++] = value3;
				}
			}
			edgeCount = num;
		}

		internal static bool CheckCollinear(global::Unity.Mathematics.double2 a0, global::Unity.Mathematics.double2 a1, global::Unity.Mathematics.double2 b0, global::Unity.Mathematics.double2 b1)
		{
			global::Unity.Mathematics.double2 double5 = a0;
			global::Unity.Mathematics.double2 double6 = a1;
			global::Unity.Mathematics.double2 double7 = b0;
			global::Unity.Mathematics.double2 double8 = b1;
			double num = (double6.y - double5.y) / (double6.x - double5.x);
			double num2 = (double7.y - double5.y) / (double7.x - double5.x);
			double num3 = (double8.y - double5.y) / (double8.x - double5.x);
			if ((!global::Unity.Mathematics.math.isinf(num) || !global::Unity.Mathematics.math.isinf(num2) || !global::Unity.Mathematics.math.isinf(num3)) && global::Unity.Mathematics.math.abs(num - num2) > kEpsilon)
			{
				return global::Unity.Mathematics.math.abs(num - num3) > kEpsilon;
			}
			return false;
		}

		internal static bool LineLineIntersection(global::Unity.Mathematics.double2 a0, global::Unity.Mathematics.double2 a1, global::Unity.Mathematics.double2 b0, global::Unity.Mathematics.double2 b1)
		{
			double num = global::UnityEngine.Rendering.Universal.UTess.ModuleHandle.OrientFastDouble(a0, b0, b1);
			double num2 = global::UnityEngine.Rendering.Universal.UTess.ModuleHandle.OrientFastDouble(a1, b0, b1);
			if ((num > kEpsilon && num2 > kEpsilon) || (num < 0.0 - kEpsilon && num2 < 0.0 - kEpsilon))
			{
				return false;
			}
			double num3 = global::UnityEngine.Rendering.Universal.UTess.ModuleHandle.OrientFastDouble(b0, a0, a1);
			double num4 = global::UnityEngine.Rendering.Universal.UTess.ModuleHandle.OrientFastDouble(b1, a0, a1);
			if ((num3 > kEpsilon && num4 > kEpsilon) || (num3 < 0.0 - kEpsilon && num4 < 0.0 - kEpsilon))
			{
				return false;
			}
			if (global::Unity.Mathematics.math.abs(num) < kEpsilon && global::Unity.Mathematics.math.abs(num2) < kEpsilon && global::Unity.Mathematics.math.abs(num3) < kEpsilon && global::Unity.Mathematics.math.abs(num4) < kEpsilon)
			{
				return CheckCollinear(a0, a1, b0, b1);
			}
			return true;
		}

		internal static bool LineLineIntersection(global::Unity.Mathematics.double2 p1, global::Unity.Mathematics.double2 p2, global::Unity.Mathematics.double2 p3, global::Unity.Mathematics.double2 p4, ref global::Unity.Mathematics.double2 result)
		{
			double num = p2.x - p1.x;
			double num2 = p2.y - p1.y;
			double num3 = p4.x - p3.x;
			double num4 = p4.y - p3.y;
			double num5 = num * num4 - num2 * num3;
			if (global::Unity.Mathematics.math.abs(num5) < kEpsilon)
			{
				return false;
			}
			double num6 = p3.x - p1.x;
			double num7 = p3.y - p1.y;
			double num8 = (num6 * num4 - num7 * num3) / num5;
			if (num8 >= 0.0 - kEpsilon && num8 <= 1.0 + kEpsilon)
			{
				result.x = p1.x + num8 * num;
				result.y = p1.y + num8 * num2;
				return true;
			}
			return false;
		}

		internal unsafe static bool CalculateEdgeIntersections(global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> edges, int edgeCount, global::Unity.Collections.NativeArray<global::Unity.Mathematics.double2> points, int pointCount, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> results, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.double2> intersects, ref int resultCount)
		{
			resultCount = 0;
			for (int i = 0; i < edgeCount; i++)
			{
				for (int j = i + 1; j < edgeCount; j++)
				{
					global::Unity.Mathematics.int2 int5 = edges[i];
					global::Unity.Mathematics.int2 int6 = edges[j];
					if (int5.x == int6.x || int5.x == int6.y || int5.y == int6.x || int5.y == int6.y)
					{
						continue;
					}
					global::Unity.Mathematics.double2 double5 = points[int5.x];
					global::Unity.Mathematics.double2 double6 = points[int5.y];
					global::Unity.Mathematics.double2 double7 = points[int6.x];
					global::Unity.Mathematics.double2 double8 = points[int6.y];
					global::Unity.Mathematics.double2 result = global::Unity.Mathematics.double2.zero;
					if (LineLineIntersection(double5, double6, double7, double8) && LineLineIntersection(double5, double6, double7, double8, ref result))
					{
						if (resultCount >= intersects.Length)
						{
							return false;
						}
						intersects[resultCount] = result;
						results[resultCount++] = new global::Unity.Mathematics.int2(i, j);
					}
				}
			}
			if (resultCount > edgeCount * kMaxIntersectionTolerance)
			{
				return false;
			}
			global::UnityEngine.Rendering.Universal.UTess.ModuleHandle.InsertionSort<global::Unity.Mathematics.int2, global::UnityEngine.Rendering.Universal.UTess.IntersectionCompare>(comp: new global::UnityEngine.Rendering.Universal.UTess.IntersectionCompare
			{
				edges = edges,
				points = points
			}, array: global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(results), lo: 0, hi: resultCount - 1);
			return true;
		}

		internal static bool CalculateTJunctions(global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> edges, int edgeCount, global::Unity.Collections.NativeArray<global::Unity.Mathematics.double2> points, int pointCount, global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> results, ref int resultCount)
		{
			resultCount = 0;
			for (int i = 0; i < edgeCount; i++)
			{
				for (int j = 0; j < pointCount; j++)
				{
					global::Unity.Mathematics.int2 int5 = edges[i];
					if (int5.x == j || int5.y == j)
					{
						continue;
					}
					global::Unity.Mathematics.double2 a = points[int5.x];
					global::Unity.Mathematics.double2 a2 = points[int5.y];
					global::Unity.Mathematics.double2 b = points[j];
					global::Unity.Mathematics.double2 b2 = points[j];
					if (LineLineIntersection(a, a2, b, b2))
					{
						if (resultCount >= results.Length)
						{
							return false;
						}
						results[resultCount++] = new global::Unity.Mathematics.int2(i, j);
					}
				}
			}
			return true;
		}

		internal unsafe static bool CutEdges(ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.double2> points, ref int pointCount, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> edges, ref int edgeCount, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> tJunctions, ref int tJunctionCount, global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> intersections, global::Unity.Collections.NativeArray<global::Unity.Mathematics.double2> intersects, int intersectionCount)
		{
			for (int i = 0; i < intersectionCount; i++)
			{
				global::Unity.Mathematics.int2 obj = intersections[i];
				int x = obj.x;
				int y = obj.y;
				global::Unity.Mathematics.int2 zero = global::Unity.Mathematics.int2.zero;
				zero.x = x;
				zero.y = pointCount;
				tJunctions[tJunctionCount++] = zero;
				global::Unity.Mathematics.int2 zero2 = global::Unity.Mathematics.int2.zero;
				zero2.x = y;
				zero2.y = pointCount;
				tJunctions[tJunctionCount++] = zero2;
				if (pointCount >= points.Length)
				{
					return false;
				}
				points[pointCount++] = intersects[i];
			}
			global::UnityEngine.Rendering.Universal.UTess.ModuleHandle.InsertionSort<global::Unity.Mathematics.int2, global::UnityEngine.Rendering.Universal.UTess.TessJunctionCompare>(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(tJunctions), 0, tJunctionCount - 1, default(global::UnityEngine.Rendering.Universal.UTess.TessJunctionCompare));
			for (int num = tJunctionCount - 1; num >= 0; num--)
			{
				global::Unity.Mathematics.int2 int5 = tJunctions[num];
				int x2 = int5.x;
				global::Unity.Mathematics.int2 value = edges[x2];
				int num2 = value.x;
				int num3 = value.y;
				global::Unity.Mathematics.double2 double5 = points[num2];
				global::Unity.Mathematics.double2 double6 = points[num3];
				if (double5.x - double6.x < 0.0 || (double5.x == double6.x && double5.y - double6.y < 0.0))
				{
					int num4 = num2;
					num2 = num3;
					num3 = num4;
				}
				value.x = num2;
				int x3 = (value.y = int5.y);
				edges[x2] = value;
				while (num > 0 && tJunctions[num - 1].x == x2)
				{
					int y2 = tJunctions[--num].y;
					global::Unity.Mathematics.int2 value2 = new global::Unity.Mathematics.int2
					{
						x = x3,
						y = y2
					};
					edges[edgeCount++] = value2;
					x3 = y2;
				}
				global::Unity.Mathematics.int2 value3 = new global::Unity.Mathematics.int2
				{
					x = x3,
					y = num3
				};
				edges[edgeCount++] = value3;
			}
			return true;
		}

		internal static void RemoveDuplicatePoints(ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.double2> points, ref int pointCount, ref global::Unity.Collections.NativeArray<int> duplicates, ref int duplicateCount, global::Unity.Collections.Allocator allocator)
		{
			global::UnityEngine.Rendering.Universal.UTess.TessLink link = global::UnityEngine.Rendering.Universal.UTess.TessLink.CreateLink(pointCount, allocator);
			for (int i = 0; i < pointCount; i++)
			{
				for (int j = i + 1; j < pointCount; j++)
				{
					if (global::Unity.Mathematics.math.distance(points[i], points[j]) < kEpsilon)
					{
						link.Link(i, j);
					}
				}
			}
			duplicateCount = 0;
			for (int k = 0; k < pointCount; k++)
			{
				int num = link.Find(k);
				if (num != k)
				{
					duplicateCount++;
					points[num] = global::Unity.Mathematics.math.min(points[k], points[num]);
				}
			}
			if (duplicateCount != 0)
			{
				int num2 = pointCount;
				pointCount = 0;
				for (int l = 0; l < num2; l++)
				{
					if (link.Find(l) == l)
					{
						duplicates[l] = pointCount;
						points[pointCount++] = points[l];
					}
					else
					{
						duplicates[l] = -1;
					}
				}
				for (int m = 0; m < num2; m++)
				{
					if (duplicates[m] < 0)
					{
						duplicates[m] = duplicates[link.Find(m)];
					}
				}
			}
			global::UnityEngine.Rendering.Universal.UTess.TessLink.DestroyLink(link);
		}

		internal static bool Validate(global::Unity.Collections.Allocator allocator, global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> inputPoints, int pointCount, global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> inputEdges, int edgeCount, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> outputPoints, ref int outputPointCount, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> outputEdges, ref int outputEdgeCount)
		{
			float num = 10000f;
			int num2 = edgeCount;
			bool flag = true;
			bool flag2 = false;
			global::Unity.Collections.NativeArray<int> duplicates = new global::Unity.Collections.NativeArray<int>(global::UnityEngine.Rendering.Universal.UTess.ModuleHandle.kMaxEdgeCount, allocator);
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> edges = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2>(global::UnityEngine.Rendering.Universal.UTess.ModuleHandle.kMaxEdgeCount, allocator);
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> tJunctions = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2>(global::UnityEngine.Rendering.Universal.UTess.ModuleHandle.kMaxEdgeCount, allocator);
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> results = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2>(global::UnityEngine.Rendering.Universal.UTess.ModuleHandle.kMaxEdgeCount, allocator);
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.double2> points = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.double2>(pointCount * 8, allocator);
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.double2> intersects = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.double2>(pointCount * 8, allocator);
			for (int i = 0; i < pointCount; i++)
			{
				points[i] = inputPoints[i] * num;
			}
			global::UnityEngine.Rendering.Universal.UTess.ModuleHandle.Copy(inputEdges, edges, edgeCount);
			RemoveDuplicateEdges(ref edges, ref edgeCount, duplicates, 0);
			while (flag && --num2 > 0)
			{
				int resultCount = 0;
				flag2 = CalculateEdgeIntersections(edges, edgeCount, points, pointCount, ref results, ref intersects, ref resultCount);
				if (!flag2)
				{
					break;
				}
				int resultCount2 = 0;
				flag2 = CalculateTJunctions(edges, edgeCount, points, pointCount, tJunctions, ref resultCount2);
				if (!flag2)
				{
					break;
				}
				flag2 = CutEdges(ref points, ref pointCount, ref edges, ref edgeCount, ref tJunctions, ref resultCount2, results, intersects, resultCount);
				if (!flag2)
				{
					break;
				}
				int duplicateCount = 0;
				RemoveDuplicatePoints(ref points, ref pointCount, ref duplicates, ref duplicateCount, allocator);
				RemoveDuplicateEdges(ref edges, ref edgeCount, duplicates, duplicateCount);
				flag = resultCount != 0 || resultCount2 != 0;
			}
			if (flag2)
			{
				outputEdgeCount = edgeCount;
				outputPointCount = pointCount;
				global::UnityEngine.Rendering.Universal.UTess.ModuleHandle.Copy(edges, outputEdges, edgeCount);
				for (int j = 0; j < pointCount; j++)
				{
					outputPoints[j] = new global::Unity.Mathematics.float2((float)(points[j].x / (double)num), (float)(points[j].y / (double)num));
				}
			}
			edges.Dispose();
			points.Dispose();
			intersects.Dispose();
			duplicates.Dispose();
			tJunctions.Dispose();
			results.Dispose();
			if (flag2)
			{
				return num2 > 0;
			}
			return false;
		}
	}
}
