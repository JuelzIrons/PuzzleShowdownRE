namespace UnityEngine.U2D.Common.UTess
{
	internal struct Tessellator
	{
		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		private struct TestHullPointL : global::UnityEngine.U2D.Common.UTess.ICondition2<global::UnityEngine.U2D.Common.UTess.UHull, global::Unity.Mathematics.float2>
		{
			public bool Test(global::UnityEngine.U2D.Common.UTess.UHull h, global::Unity.Mathematics.float2 p, ref float t)
			{
				t = global::UnityEngine.U2D.Common.UTess.ModuleHandle.OrientFast(h.a, h.b, p);
				return t < 0f;
			}
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		private struct TestHullPointU : global::UnityEngine.U2D.Common.UTess.ICondition2<global::UnityEngine.U2D.Common.UTess.UHull, global::Unity.Mathematics.float2>
		{
			public bool Test(global::UnityEngine.U2D.Common.UTess.UHull h, global::Unity.Mathematics.float2 p, ref float t)
			{
				t = global::UnityEngine.U2D.Common.UTess.ModuleHandle.OrientFast(h.a, h.b, p);
				return t > 0f;
			}
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		private struct TestHullEventLe : global::UnityEngine.U2D.Common.UTess.ICondition2<global::UnityEngine.U2D.Common.UTess.UHull, global::UnityEngine.U2D.Common.UTess.UEvent>
		{
			public bool Test(global::UnityEngine.U2D.Common.UTess.UHull h, global::UnityEngine.U2D.Common.UTess.UEvent p, ref float t)
			{
				t = FindSplit(h, p);
				return t <= 0f;
			}
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		private struct TestHullEventE : global::UnityEngine.U2D.Common.UTess.ICondition2<global::UnityEngine.U2D.Common.UTess.UHull, global::UnityEngine.U2D.Common.UTess.UEvent>
		{
			public bool Test(global::UnityEngine.U2D.Common.UTess.UHull h, global::UnityEngine.U2D.Common.UTess.UEvent p, ref float t)
			{
				t = FindSplit(h, p);
				return t == 0f;
			}
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		private struct TestEdgePointE : global::UnityEngine.U2D.Common.UTess.ICondition2<global::Unity.Mathematics.int2, global::Unity.Mathematics.int2>
		{
			public bool Test(global::Unity.Mathematics.int2 h, global::Unity.Mathematics.int2 p, ref float t)
			{
				t = default(global::UnityEngine.U2D.Common.UTess.TessEdgeCompare).Compare(h, p);
				return t == 0f;
			}
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		private struct TestCellE : global::UnityEngine.U2D.Common.UTess.ICondition2<global::Unity.Mathematics.int3, global::Unity.Mathematics.int3>
		{
			public bool Test(global::Unity.Mathematics.int3 h, global::Unity.Mathematics.int3 p, ref float t)
			{
				t = default(global::UnityEngine.U2D.Common.UTess.TessCellCompare).Compare(h, p);
				return t == 0f;
			}
		}

		private global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> m_Edges;

		private global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Common.UTess.UStar> m_Stars;

		private global::UnityEngine.U2D.Common.UTess.Array<global::Unity.Mathematics.int3> m_Cells;

		private int m_CellCount;

		private global::Unity.Collections.NativeArray<int> m_ILArray;

		private global::Unity.Collections.NativeArray<int> m_IUArray;

		private global::Unity.Collections.NativeArray<int> m_SPArray;

		private int m_NumEdges;

		private int m_NumHulls;

		private int m_NumPoints;

		private int m_StarCount;

		private global::Unity.Collections.NativeArray<int> m_Flags;

		private global::Unity.Collections.NativeArray<int> m_Neighbors;

		private global::Unity.Collections.NativeArray<int> m_Constraints;

		private global::Unity.Collections.Allocator m_Allocator;

		private static float FindSplit(global::UnityEngine.U2D.Common.UTess.UHull hull, global::UnityEngine.U2D.Common.UTess.UEvent edge)
		{
			float num = 0f;
			num = ((!(hull.a.x < edge.a.x)) ? global::UnityEngine.U2D.Common.UTess.ModuleHandle.OrientFast(edge.b, edge.a, hull.a) : global::UnityEngine.U2D.Common.UTess.ModuleHandle.OrientFast(hull.a, hull.b, edge.a));
			if (0f != num)
			{
				return num;
			}
			num = ((!(edge.b.x < hull.b.x)) ? global::UnityEngine.U2D.Common.UTess.ModuleHandle.OrientFast(edge.b, edge.a, hull.b) : global::UnityEngine.U2D.Common.UTess.ModuleHandle.OrientFast(hull.a, hull.b, edge.b));
			if (0f != num)
			{
				return num;
			}
			return hull.idx - edge.idx;
		}

		private void SetAllocator(global::Unity.Collections.Allocator allocator)
		{
			m_Allocator = allocator;
		}

		private bool AddPoint(global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Common.UTess.UHull> hulls, int hullCount, global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> points, global::Unity.Mathematics.float2 p, int idx)
		{
			int lower = global::UnityEngine.U2D.Common.UTess.ModuleHandle.GetLower(hulls, hullCount, p, default(global::UnityEngine.U2D.Common.UTess.Tessellator.TestHullPointL));
			int upper = global::UnityEngine.U2D.Common.UTess.ModuleHandle.GetUpper(hulls, hullCount, p, default(global::UnityEngine.U2D.Common.UTess.Tessellator.TestHullPointU));
			if (lower < 0 || upper < 0)
			{
				return false;
			}
			for (int i = lower; i < upper; i++)
			{
				global::UnityEngine.U2D.Common.UTess.UHull value = hulls[i];
				int num = value.ilcount;
				while (num > 1 && global::UnityEngine.U2D.Common.UTess.ModuleHandle.OrientFast(points[value.ilarray[num - 2]], points[value.ilarray[num - 1]], p) > 0f)
				{
					global::Unity.Mathematics.int3 value2 = new global::Unity.Mathematics.int3
					{
						x = value.ilarray[num - 1],
						y = value.ilarray[num - 2],
						z = idx
					};
					m_Cells[m_CellCount++] = value2;
					num--;
				}
				value.ilcount = num + 1;
				if (value.ilcount > value.ilarray.Length)
				{
					return false;
				}
				value.ilarray[num] = idx;
				num = value.iucount;
				while (num > 1 && global::UnityEngine.U2D.Common.UTess.ModuleHandle.OrientFast(points[value.iuarray[num - 2]], points[value.iuarray[num - 1]], p) < 0f)
				{
					global::Unity.Mathematics.int3 value3 = new global::Unity.Mathematics.int3
					{
						x = value.iuarray[num - 2],
						y = value.iuarray[num - 1],
						z = idx
					};
					m_Cells[m_CellCount++] = value3;
					num--;
				}
				value.iucount = num + 1;
				if (value.iucount > value.iuarray.Length)
				{
					return false;
				}
				value.iuarray[num] = idx;
				hulls[i] = value;
			}
			return true;
		}

		private static void InsertHull(global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Common.UTess.UHull> Hulls, int Pos, ref int Count, global::UnityEngine.U2D.Common.UTess.UHull Value)
		{
			if (Count < Hulls.Length - 1)
			{
				for (int num = Count; num > Pos; num--)
				{
					Hulls[num] = Hulls[num - 1];
				}
				Hulls[Pos] = Value;
				Count++;
			}
		}

		private static void EraseHull(global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Common.UTess.UHull> Hulls, int Pos, ref int Count)
		{
			if (Count < Hulls.Length)
			{
				for (int i = Pos; i < Count - 1; i++)
				{
					Hulls[i] = Hulls[i + 1];
				}
				Count--;
			}
		}

		private bool SplitHulls(global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Common.UTess.UHull> hulls, ref int hullCount, global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> points, global::UnityEngine.U2D.Common.UTess.UEvent evt)
		{
			int lower = global::UnityEngine.U2D.Common.UTess.ModuleHandle.GetLower(hulls, hullCount, evt, default(global::UnityEngine.U2D.Common.UTess.Tessellator.TestHullEventLe));
			if (lower < 0)
			{
				return false;
			}
			global::UnityEngine.U2D.Common.UTess.UHull value = hulls[lower];
			global::UnityEngine.U2D.Common.UTess.UHull value2 = default(global::UnityEngine.U2D.Common.UTess.UHull);
			value2.a = evt.a;
			value2.b = evt.b;
			value2.idx = evt.idx;
			int value3 = value.iuarray[value.iucount - 1];
			value2.iuarray = new global::UnityEngine.U2D.Common.UTess.ArraySlice<int>(m_IUArray, value2.idx * m_NumHulls, m_NumHulls);
			value2.iucount = value.iucount;
			for (int i = 0; i < value2.iucount; i++)
			{
				value2.iuarray[i] = value.iuarray[i];
			}
			value.iuarray[0] = value3;
			value.iucount = 1;
			hulls[lower] = value;
			value2.ilarray = new global::UnityEngine.U2D.Common.UTess.ArraySlice<int>(m_ILArray, value2.idx * m_NumHulls, m_NumHulls);
			value2.ilarray[0] = value3;
			value2.ilcount = 1;
			InsertHull(hulls, lower + 1, ref hullCount, value2);
			return true;
		}

		private bool MergeHulls(global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Common.UTess.UHull> hulls, ref int hullCount, global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> points, global::UnityEngine.U2D.Common.UTess.UEvent evt)
		{
			global::Unity.Mathematics.float2 a = evt.a;
			evt.a = evt.b;
			evt.b = a;
			int equal = global::UnityEngine.U2D.Common.UTess.ModuleHandle.GetEqual(hulls, hullCount, evt, default(global::UnityEngine.U2D.Common.UTess.Tessellator.TestHullEventE));
			if (equal < 0)
			{
				return false;
			}
			global::UnityEngine.U2D.Common.UTess.UHull uHull = hulls[equal];
			global::UnityEngine.U2D.Common.UTess.UHull value = hulls[equal - 1];
			value.iucount = uHull.iucount;
			for (int i = 0; i < value.iucount; i++)
			{
				value.iuarray[i] = uHull.iuarray[i];
			}
			hulls[equal - 1] = value;
			EraseHull(hulls, equal, ref hullCount);
			return true;
		}

		private static void InsertUniqueEdge(global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> edges, global::Unity.Mathematics.int2 e, ref int edgeCount)
		{
			global::UnityEngine.U2D.Common.UTess.TessEdgeCompare tessEdgeCompare = default(global::UnityEngine.U2D.Common.UTess.TessEdgeCompare);
			bool flag = true;
			int num = 0;
			while (flag && num < edgeCount)
			{
				if (tessEdgeCompare.Compare(e, edges[num]) == 0)
				{
					flag = false;
				}
				num++;
			}
			if (flag)
			{
				edges[edgeCount++] = e;
			}
		}

		private unsafe void PrepareDelaunay(global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> edges, int edgeCount)
		{
			m_StarCount = m_CellCount * 3;
			m_Stars = new global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Common.UTess.UStar>(m_StarCount, m_Allocator);
			m_SPArray = new global::Unity.Collections.NativeArray<int>(m_StarCount * m_StarCount, m_Allocator, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			int edgeCount2 = 0;
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> edges2 = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2>(m_StarCount, m_Allocator);
			for (int i = 0; i < edgeCount; i++)
			{
				global::Unity.Mathematics.int2 int5 = edges[i];
				int5.x = ((edges[i].x < edges[i].y) ? edges[i].x : edges[i].y);
				int5.y = ((edges[i].x > edges[i].y) ? edges[i].x : edges[i].y);
				edges[i] = int5;
				InsertUniqueEdge(edges2, int5, ref edgeCount2);
			}
			m_Edges = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2>(edgeCount2, m_Allocator);
			for (int j = 0; j < edgeCount2; j++)
			{
				m_Edges[j] = edges2[j];
			}
			edges2.Dispose();
			global::UnityEngine.U2D.Common.UTess.ModuleHandle.InsertionSort<global::Unity.Mathematics.int2, global::UnityEngine.U2D.Common.UTess.TessEdgeCompare>(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(m_Edges), 0, m_Edges.Length - 1, default(global::UnityEngine.U2D.Common.UTess.TessEdgeCompare));
			for (int k = 0; k < m_StarCount; k++)
			{
				global::UnityEngine.U2D.Common.UTess.UStar value = m_Stars[k];
				value.points = new global::UnityEngine.U2D.Common.UTess.ArraySlice<int>(m_SPArray, k * m_StarCount, m_StarCount);
				value.pointCount = 0;
				m_Stars[k] = value;
			}
			for (int l = 0; l < m_CellCount; l++)
			{
				int x = m_Cells[l].x;
				int y = m_Cells[l].y;
				int z = m_Cells[l].z;
				global::UnityEngine.U2D.Common.UTess.UStar value2 = m_Stars[x];
				global::UnityEngine.U2D.Common.UTess.UStar value3 = m_Stars[y];
				global::UnityEngine.U2D.Common.UTess.UStar value4 = m_Stars[z];
				value2.points[value2.pointCount++] = y;
				value2.points[value2.pointCount++] = z;
				value3.points[value3.pointCount++] = z;
				value3.points[value3.pointCount++] = x;
				value4.points[value4.pointCount++] = x;
				value4.points[value4.pointCount++] = y;
				m_Stars[x] = value2;
				m_Stars[y] = value3;
				m_Stars[z] = value4;
			}
		}

		private int OppositeOf(int a, int b)
		{
			global::UnityEngine.U2D.Common.UTess.ArraySlice<int> points = m_Stars[b].points;
			int i = 1;
			for (int pointCount = m_Stars[b].pointCount; i < pointCount; i += 2)
			{
				if (points[i] == a)
				{
					return points[i - 1];
				}
			}
			return -1;
		}

		private int FindConstraint(int a, int b)
		{
			global::Unity.Mathematics.int2 check = default(global::Unity.Mathematics.int2);
			check.x = ((a < b) ? a : b);
			check.y = ((a > b) ? a : b);
			return global::UnityEngine.U2D.Common.UTess.ModuleHandle.GetEqual(m_Edges, m_Edges.Length, check, default(global::UnityEngine.U2D.Common.UTess.Tessellator.TestEdgePointE));
		}

		private void AddTriangle(int i, int j, int k)
		{
			global::UnityEngine.U2D.Common.UTess.UStar value = m_Stars[i];
			global::UnityEngine.U2D.Common.UTess.UStar value2 = m_Stars[j];
			global::UnityEngine.U2D.Common.UTess.UStar value3 = m_Stars[k];
			value.points[value.pointCount++] = j;
			value.points[value.pointCount++] = k;
			value2.points[value2.pointCount++] = k;
			value2.points[value2.pointCount++] = i;
			value3.points[value3.pointCount++] = i;
			value3.points[value3.pointCount++] = j;
			m_Stars[i] = value;
			m_Stars[j] = value2;
			m_Stars[k] = value3;
		}

		private void RemovePair(int r, int j, int k)
		{
			global::UnityEngine.U2D.Common.UTess.UStar value = m_Stars[r];
			global::UnityEngine.U2D.Common.UTess.ArraySlice<int> points = value.points;
			int i = 1;
			for (int pointCount = value.pointCount; i < pointCount; i += 2)
			{
				if (points[i - 1] == j && points[i] == k)
				{
					points[i - 1] = points[pointCount - 2];
					points[i] = points[pointCount - 1];
					value.points = points;
					value.pointCount -= 2;
					m_Stars[r] = value;
					break;
				}
			}
		}

		private void RemoveTriangle(int i, int j, int k)
		{
			RemovePair(i, j, k);
			RemovePair(j, k, i);
			RemovePair(k, i, j);
		}

		private void EdgeFlip(int i, int j)
		{
			int num = OppositeOf(i, j);
			int num2 = OppositeOf(j, i);
			RemoveTriangle(i, j, num);
			RemoveTriangle(j, i, num2);
			AddTriangle(i, num2, num);
			AddTriangle(j, num, num2);
		}

		private bool Flip(global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> points, ref global::UnityEngine.U2D.Common.UTess.Array<int> stack, ref int stackCount, int a, int b, int x)
		{
			int num = OppositeOf(a, b);
			if (num < 0)
			{
				return true;
			}
			if (b < a)
			{
				int num2 = a;
				a = b;
				b = num2;
				int num3 = x;
				x = num;
				num = num3;
			}
			if (FindConstraint(a, b) != -1)
			{
				return true;
			}
			if (global::UnityEngine.U2D.Common.UTess.ModuleHandle.IsInsideCircle(points[a], points[b], points[x], points[num]))
			{
				if (2 + stackCount >= stack.Length)
				{
					return false;
				}
				stack[stackCount++] = a;
				stack[stackCount++] = b;
			}
			return true;
		}

		private global::UnityEngine.U2D.Common.UTess.Array<global::Unity.Mathematics.int3> GetCells(ref int count)
		{
			global::UnityEngine.U2D.Common.UTess.Array<global::Unity.Mathematics.int3> result = new global::UnityEngine.U2D.Common.UTess.Array<global::Unity.Mathematics.int3>(m_NumPoints * 4, m_NumPoints * (m_NumPoints + 1), m_Allocator, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			count = 0;
			int i = 0;
			for (int length = m_Stars.Length; i < length; i++)
			{
				global::UnityEngine.U2D.Common.UTess.ArraySlice<int> points = m_Stars[i].points;
				int j = 0;
				for (int pointCount = m_Stars[i].pointCount; j < pointCount; j += 2)
				{
					int num = points[j];
					int num2 = points[j + 1];
					if (i < global::Unity.Mathematics.math.min(num, num2))
					{
						global::Unity.Mathematics.int3 value = new global::Unity.Mathematics.int3
						{
							x = i,
							y = num,
							z = num2
						};
						result[count++] = value;
					}
				}
			}
			return result;
		}

		internal bool ApplyDelaunay(global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> points, global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> edges)
		{
			if (m_CellCount == 0)
			{
				return false;
			}
			global::UnityEngine.U2D.Common.UTess.Array<int> stack = new global::UnityEngine.U2D.Common.UTess.Array<int>(m_NumPoints * 4, m_NumPoints * (m_NumPoints + 1), m_Allocator, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			int stackCount = 0;
			bool flag = true;
			PrepareDelaunay(edges, m_NumEdges);
			int num = 0;
			while (flag && num < m_NumPoints)
			{
				global::UnityEngine.U2D.Common.UTess.UStar uStar = m_Stars[num];
				for (int i = 1; i < uStar.pointCount; i += 2)
				{
					int num2 = uStar.points[i];
					if (num2 < num || FindConstraint(num, num2) >= 0)
					{
						continue;
					}
					int index = uStar.points[i - 1];
					int num3 = -1;
					for (int j = 1; j < uStar.pointCount; j += 2)
					{
						if (uStar.points[j - 1] == num2)
						{
							num3 = uStar.points[j];
							break;
						}
					}
					if (num3 >= 0 && global::UnityEngine.U2D.Common.UTess.ModuleHandle.IsInsideCircle(points[num], points[num2], points[index], points[num3]))
					{
						if (2 + stackCount >= stack.Length)
						{
							flag = false;
							break;
						}
						stack[stackCount++] = num;
						stack[stackCount++] = num2;
					}
				}
				num++;
			}
			int num4 = m_NumPoints * m_NumPoints;
			while (stackCount > 0 && flag)
			{
				int num5 = stack[stackCount - 1];
				stackCount--;
				int num6 = stack[stackCount - 1];
				stackCount--;
				int num7 = -1;
				int num8 = -1;
				global::UnityEngine.U2D.Common.UTess.UStar uStar2 = m_Stars[num6];
				for (int k = 1; k < uStar2.pointCount; k += 2)
				{
					int num9 = uStar2.points[k - 1];
					int num10 = uStar2.points[k];
					if (num9 == num5)
					{
						num8 = num10;
					}
					else if (num10 == num5)
					{
						num7 = num9;
					}
				}
				if (num7 >= 0 && num8 >= 0 && global::UnityEngine.U2D.Common.UTess.ModuleHandle.IsInsideCircle(points[num6], points[num5], points[num7], points[num8]))
				{
					EdgeFlip(num6, num5);
					flag = Flip(points, ref stack, ref stackCount, num7, num6, num8) && Flip(points, ref stack, ref stackCount, num6, num8, num7) && Flip(points, ref stack, ref stackCount, num8, num5, num7) && Flip(points, ref stack, ref stackCount, num5, num7, num8) && --num4 > 0;
				}
			}
			stack.Dispose();
			return flag;
		}

		private int FindNeighbor(global::UnityEngine.U2D.Common.UTess.Array<global::Unity.Mathematics.int3> cells, int count, int a, int b, int c)
		{
			int num = a;
			int y = b;
			int z = c;
			if (b < c)
			{
				if (b < a)
				{
					num = b;
					y = c;
					z = a;
				}
			}
			else if (c < a)
			{
				num = c;
				y = a;
				z = b;
			}
			if (num < 0)
			{
				return -1;
			}
			global::Unity.Mathematics.int3 check = default(global::Unity.Mathematics.int3);
			check.x = num;
			check.y = y;
			check.z = z;
			return global::UnityEngine.U2D.Common.UTess.ModuleHandle.GetEqual(cells, count, check, default(global::UnityEngine.U2D.Common.UTess.Tessellator.TestCellE));
		}

		private unsafe global::UnityEngine.U2D.Common.UTess.Array<global::Unity.Mathematics.int3> Constrain(ref int count)
		{
			global::UnityEngine.U2D.Common.UTess.Array<global::Unity.Mathematics.int3> cells = GetCells(ref count);
			int num = count;
			for (int i = 0; i < num; i++)
			{
				global::Unity.Mathematics.int3 value = cells[i];
				int x = value.x;
				int y = value.y;
				int z = value.z;
				if (y < z)
				{
					if (y < x)
					{
						value.x = y;
						value.y = z;
						value.z = x;
					}
				}
				else if (z < x)
				{
					value.x = z;
					value.y = x;
					value.z = y;
				}
				cells[i] = value;
			}
			global::UnityEngine.U2D.Common.UTess.ModuleHandle.InsertionSort<global::Unity.Mathematics.int3, global::UnityEngine.U2D.Common.UTess.TessCellCompare>(cells.UnsafePtr, 0, m_CellCount - 1, default(global::UnityEngine.U2D.Common.UTess.TessCellCompare));
			m_Flags = new global::Unity.Collections.NativeArray<int>(num, m_Allocator);
			m_Neighbors = new global::Unity.Collections.NativeArray<int>(num * 3, m_Allocator);
			m_Constraints = new global::Unity.Collections.NativeArray<int>(num * 3, m_Allocator);
			global::Unity.Collections.NativeArray<int> nativeArray = new global::Unity.Collections.NativeArray<int>(num * 3, m_Allocator);
			global::Unity.Collections.NativeArray<int> nativeArray2 = new global::Unity.Collections.NativeArray<int>(num * 3, m_Allocator);
			int num2 = 1;
			int num3 = 0;
			int num4 = 0;
			for (int j = 0; j < num; j++)
			{
				global::Unity.Mathematics.int3 int5 = cells[j];
				for (int k = 0; k < 3; k++)
				{
					int num5 = k;
					int num6 = (k + 1) % 3;
					num5 = ((num5 == 0) ? int5.x : ((k == 1) ? int5.y : int5.z));
					num6 = num6 switch
					{
						1 => int5.y, 
						0 => int5.x, 
						_ => int5.z, 
					};
					int c = OppositeOf(num6, num5);
					int num7 = (m_Neighbors[3 * j + k] = FindNeighbor(cells, count, num6, num5, c));
					int num9 = num7;
					num7 = (m_Constraints[3 * j + k] = ((-1 != FindConstraint(num5, num6)) ? 1 : 0));
					int num11 = num7;
					if (num9 < 0)
					{
						if (num11 != 0)
						{
							nativeArray[num3++] = j;
							continue;
						}
						nativeArray2[num4++] = j;
						m_Flags[j] = 1;
					}
				}
			}
			while (num4 > 0 || num3 > 0)
			{
				while (num4 > 0)
				{
					int num12 = nativeArray2[num4 - 1];
					num4--;
					if (m_Flags[num12] == -num2)
					{
						continue;
					}
					m_Flags[num12] = num2;
					_ = cells[num12];
					for (int l = 0; l < 3; l++)
					{
						int num13 = m_Neighbors[3 * num12 + l];
						if (num13 >= 0 && m_Flags[num13] == 0)
						{
							if (m_Constraints[3 * num12 + l] != 0)
							{
								nativeArray[num3++] = num13;
								continue;
							}
							nativeArray2[num4++] = num13;
							m_Flags[num13] = num2;
						}
					}
				}
				for (int m = 0; m < num3; m++)
				{
					nativeArray2[m] = nativeArray[m];
				}
				num4 = num3;
				num3 = 0;
				num2 = -num2;
			}
			nativeArray2.Dispose();
			nativeArray.Dispose();
			return cells;
		}

		internal global::Unity.Collections.NativeArray<global::Unity.Mathematics.int3> RemoveExterior(ref int cellCount)
		{
			int count = 0;
			global::UnityEngine.U2D.Common.UTess.Array<global::Unity.Mathematics.int3> array = Constrain(ref count);
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.int3> result = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.int3>(count, m_Allocator);
			cellCount = 0;
			for (int i = 0; i < count; i++)
			{
				if (m_Flags[i] == -1)
				{
					result[cellCount++] = array[i];
				}
			}
			array.Dispose();
			return result;
		}

		internal global::Unity.Collections.NativeArray<global::Unity.Mathematics.int3> RemoveInterior(int cellCount)
		{
			int count = 0;
			global::UnityEngine.U2D.Common.UTess.Array<global::Unity.Mathematics.int3> array = Constrain(ref count);
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.int3> result = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.int3>(count, m_Allocator);
			cellCount = 0;
			for (int i = 0; i < count; i++)
			{
				if (m_Flags[i] == 1)
				{
					result[cellCount++] = array[i];
				}
			}
			array.Dispose();
			return result;
		}

		internal unsafe bool Triangulate(global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> points, int pointCount, global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> edges, int edgeCount)
		{
			m_NumEdges = edgeCount;
			m_NumHulls = edgeCount * 2;
			m_NumPoints = pointCount;
			m_CellCount = 0;
			int length = m_NumHulls * (m_NumHulls + 1);
			m_Cells = new global::UnityEngine.U2D.Common.UTess.Array<global::Unity.Mathematics.int3>(length, global::UnityEngine.U2D.Common.UTess.ModuleHandle.kMaxTriangleCount, m_Allocator, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			m_ILArray = new global::Unity.Collections.NativeArray<int>(length, m_Allocator);
			m_IUArray = new global::Unity.Collections.NativeArray<int>(length, m_Allocator);
			global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Common.UTess.UHull> hulls = new global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Common.UTess.UHull>(m_NumPoints * 8, m_Allocator);
			int hullCount = 0;
			global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Common.UTess.UEvent> nativeArray = new global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Common.UTess.UEvent>(m_NumPoints + m_NumEdges * 2, m_Allocator);
			int num = 0;
			for (int i = 0; i < m_NumPoints; i++)
			{
				global::UnityEngine.U2D.Common.UTess.UEvent value = new global::UnityEngine.U2D.Common.UTess.UEvent
				{
					a = points[i],
					b = default(global::Unity.Mathematics.float2),
					idx = i,
					type = 0
				};
				nativeArray[num++] = value;
			}
			for (int j = 0; j < m_NumEdges; j++)
			{
				global::Unity.Mathematics.int2 int5 = edges[j];
				global::Unity.Mathematics.float2 float5 = points[int5.x];
				global::Unity.Mathematics.float2 float6 = points[int5.y];
				if (float5.x < float6.x)
				{
					global::UnityEngine.U2D.Common.UTess.UEvent value2 = new global::UnityEngine.U2D.Common.UTess.UEvent
					{
						a = float5,
						b = float6,
						idx = j,
						type = 2
					};
					global::UnityEngine.U2D.Common.UTess.UEvent value3 = new global::UnityEngine.U2D.Common.UTess.UEvent
					{
						a = float6,
						b = float5,
						idx = j,
						type = 1
					};
					nativeArray[num++] = value2;
					nativeArray[num++] = value3;
				}
				else if (float5.x > float6.x)
				{
					global::UnityEngine.U2D.Common.UTess.UEvent value4 = new global::UnityEngine.U2D.Common.UTess.UEvent
					{
						a = float6,
						b = float5,
						idx = j,
						type = 2
					};
					global::UnityEngine.U2D.Common.UTess.UEvent value5 = new global::UnityEngine.U2D.Common.UTess.UEvent
					{
						a = float5,
						b = float6,
						idx = j,
						type = 1
					};
					nativeArray[num++] = value4;
					nativeArray[num++] = value5;
				}
			}
			global::UnityEngine.U2D.Common.UTess.ModuleHandle.InsertionSort<global::UnityEngine.U2D.Common.UTess.UEvent, global::UnityEngine.U2D.Common.UTess.TessEventCompare>(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(nativeArray), 0, num - 1, default(global::UnityEngine.U2D.Common.UTess.TessEventCompare));
			bool flag = true;
			float x = nativeArray[0].a.x - (1f + global::Unity.Mathematics.math.abs(nativeArray[0].a.x)) * global::Unity.Mathematics.math.pow(2f, -16f);
			global::UnityEngine.U2D.Common.UTess.UHull value6 = default(global::UnityEngine.U2D.Common.UTess.UHull);
			value6.a.x = x;
			value6.a.y = 1f;
			value6.b.x = x;
			value6.b.y = 0f;
			value6.idx = -1;
			value6.ilarray = new global::UnityEngine.U2D.Common.UTess.ArraySlice<int>(m_ILArray, m_NumHulls * m_NumHulls, m_NumHulls);
			value6.iuarray = new global::UnityEngine.U2D.Common.UTess.ArraySlice<int>(m_IUArray, m_NumHulls * m_NumHulls, m_NumHulls);
			value6.ilcount = 0;
			value6.iucount = 0;
			hulls[hullCount++] = value6;
			int k = 0;
			for (int num2 = num; k < num2; k++)
			{
				flag = nativeArray[k].type switch
				{
					0 => AddPoint(hulls, hullCount, points, nativeArray[k].a, nativeArray[k].idx), 
					2 => SplitHulls(hulls, ref hullCount, points, nativeArray[k]), 
					_ => MergeHulls(hulls, ref hullCount, points, nativeArray[k]), 
				};
				if (!flag)
				{
					break;
				}
			}
			nativeArray.Dispose();
			hulls.Dispose();
			return flag;
		}

		internal static bool Tessellate(global::Unity.Collections.Allocator allocator, global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> pgPoints, int pgPointCount, global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> pgEdges, int pgEdgeCount, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> outputVertices, ref int vertexCount, ref global::Unity.Collections.NativeArray<int> outputIndices, ref int indexCount)
		{
			global::UnityEngine.U2D.Common.UTess.Tessellator tessellator = default(global::UnityEngine.U2D.Common.UTess.Tessellator);
			tessellator.SetAllocator(allocator);
			int num = 0;
			int cellCount = 0;
			bool flag = true;
			flag = tessellator.Triangulate(pgPoints, pgPointCount, pgEdges, pgEdgeCount) && tessellator.ApplyDelaunay(pgPoints, pgEdges);
			if (flag)
			{
				global::Unity.Collections.NativeArray<global::Unity.Mathematics.int3> nativeArray = tessellator.RemoveExterior(ref cellCount);
				for (int i = 0; i < cellCount; i++)
				{
					ushort num2 = (ushort)nativeArray[i].x;
					ushort num3 = (ushort)nativeArray[i].y;
					ushort num4 = (ushort)nativeArray[i].z;
					if (num2 != num3 && num3 != num4 && num2 != num4)
					{
						outputIndices[indexCount++] = num2;
						outputIndices[indexCount++] = num4;
						outputIndices[indexCount++] = num3;
					}
					num = global::Unity.Mathematics.math.max(global::Unity.Mathematics.math.max(global::Unity.Mathematics.math.max(nativeArray[i].x, nativeArray[i].y), nativeArray[i].z), num);
				}
				num = ((num != 0) ? (num + 1) : 0);
				for (int j = 0; j < num; j++)
				{
					outputVertices[vertexCount++] = pgPoints[j];
				}
				nativeArray.Dispose();
			}
			tessellator.Cleanup();
			return flag;
		}

		internal void Cleanup()
		{
			if (m_Edges.IsCreated)
			{
				m_Edges.Dispose();
			}
			if (m_Stars.IsCreated)
			{
				m_Stars.Dispose();
			}
			if (m_SPArray.IsCreated)
			{
				m_SPArray.Dispose();
			}
			if (m_Cells.IsCreated)
			{
				m_Cells.Dispose();
			}
			if (m_ILArray.IsCreated)
			{
				m_ILArray.Dispose();
			}
			if (m_IUArray.IsCreated)
			{
				m_IUArray.Dispose();
			}
			if (m_Flags.IsCreated)
			{
				m_Flags.Dispose();
			}
			if (m_Neighbors.IsCreated)
			{
				m_Neighbors.Dispose();
			}
			if (m_Constraints.IsCreated)
			{
				m_Constraints.Dispose();
			}
		}
	}
}
