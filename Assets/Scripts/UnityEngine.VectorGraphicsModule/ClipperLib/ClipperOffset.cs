namespace ClipperLib
{
	internal class ClipperOffset
	{
		private global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::ClipperLib.IntPoint>> m_destPolys;

		private global::System.Collections.Generic.List<global::ClipperLib.IntPoint> m_srcPoly;

		private global::System.Collections.Generic.List<global::ClipperLib.IntPoint> m_destPoly;

		private global::System.Collections.Generic.List<global::ClipperLib.DoublePoint> m_normals = new global::System.Collections.Generic.List<global::ClipperLib.DoublePoint>();

		private double m_delta;

		private double m_sinA;

		private double m_sin;

		private double m_cos;

		private double m_miterLim;

		private double m_StepsPerRad;

		private global::ClipperLib.IntPoint m_lowest;

		private global::ClipperLib.PolyNode m_polyNodes = new global::ClipperLib.PolyNode();

		private const double two_pi = global::System.Math.PI * 2.0;

		private const double def_arc_tolerance = 0.25;

		public double ArcTolerance { get; set; }

		public double MiterLimit { get; set; }

		public ClipperOffset(double miterLimit = 2.0, double arcTolerance = 0.25)
		{
			MiterLimit = miterLimit;
			ArcTolerance = arcTolerance;
			m_lowest.X = -1L;
		}

		public void Clear()
		{
			m_polyNodes.Childs.Clear();
			m_lowest.X = -1L;
		}

		internal static long Round(double value)
		{
			return (value < 0.0) ? ((long)(value - 0.5)) : ((long)(value + 0.5));
		}

		public void AddPath(global::System.Collections.Generic.List<global::ClipperLib.IntPoint> path, global::ClipperLib.JoinType joinType, global::ClipperLib.EndType endType)
		{
			int num = path.Count - 1;
			if (num < 0)
			{
				return;
			}
			global::ClipperLib.PolyNode polyNode = new global::ClipperLib.PolyNode();
			polyNode.m_jointype = joinType;
			polyNode.m_endtype = endType;
			if (endType == global::ClipperLib.EndType.etClosedLine || endType == global::ClipperLib.EndType.etClosedPolygon)
			{
				while (num > 0 && path[0] == path[num])
				{
					num--;
				}
			}
			polyNode.m_polygon.Capacity = num + 1;
			polyNode.m_polygon.Add(path[0]);
			int num2 = 0;
			int num3 = 0;
			for (int i = 1; i <= num; i++)
			{
				if (polyNode.m_polygon[num2] != path[i])
				{
					num2++;
					polyNode.m_polygon.Add(path[i]);
					if (path[i].Y > polyNode.m_polygon[num3].Y || (path[i].Y == polyNode.m_polygon[num3].Y && path[i].X < polyNode.m_polygon[num3].X))
					{
						num3 = num2;
					}
				}
			}
			if (endType == global::ClipperLib.EndType.etClosedPolygon && num2 < 2)
			{
				return;
			}
			m_polyNodes.AddChild(polyNode);
			if (endType != global::ClipperLib.EndType.etClosedPolygon)
			{
				return;
			}
			if (m_lowest.X < 0)
			{
				m_lowest = new global::ClipperLib.IntPoint(m_polyNodes.ChildCount - 1, num3);
				return;
			}
			global::ClipperLib.IntPoint intPoint = m_polyNodes.Childs[(int)m_lowest.X].m_polygon[(int)m_lowest.Y];
			if (polyNode.m_polygon[num3].Y > intPoint.Y || (polyNode.m_polygon[num3].Y == intPoint.Y && polyNode.m_polygon[num3].X < intPoint.X))
			{
				m_lowest = new global::ClipperLib.IntPoint(m_polyNodes.ChildCount - 1, num3);
			}
		}

		public void AddPaths(global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::ClipperLib.IntPoint>> paths, global::ClipperLib.JoinType joinType, global::ClipperLib.EndType endType)
		{
			foreach (global::System.Collections.Generic.List<global::ClipperLib.IntPoint> path in paths)
			{
				AddPath(path, joinType, endType);
			}
		}

		private void FixOrientations()
		{
			if (m_lowest.X >= 0 && !global::ClipperLib.Clipper.Orientation(m_polyNodes.Childs[(int)m_lowest.X].m_polygon))
			{
				for (int i = 0; i < m_polyNodes.ChildCount; i++)
				{
					global::ClipperLib.PolyNode polyNode = m_polyNodes.Childs[i];
					if (polyNode.m_endtype == global::ClipperLib.EndType.etClosedPolygon || (polyNode.m_endtype == global::ClipperLib.EndType.etClosedLine && global::ClipperLib.Clipper.Orientation(polyNode.m_polygon)))
					{
						polyNode.m_polygon.Reverse();
					}
				}
				return;
			}
			for (int j = 0; j < m_polyNodes.ChildCount; j++)
			{
				global::ClipperLib.PolyNode polyNode2 = m_polyNodes.Childs[j];
				if (polyNode2.m_endtype == global::ClipperLib.EndType.etClosedLine && !global::ClipperLib.Clipper.Orientation(polyNode2.m_polygon))
				{
					polyNode2.m_polygon.Reverse();
				}
			}
		}

		internal static global::ClipperLib.DoublePoint GetUnitNormal(global::ClipperLib.IntPoint pt1, global::ClipperLib.IntPoint pt2)
		{
			double num = pt2.X - pt1.X;
			double num2 = pt2.Y - pt1.Y;
			if (num == 0.0 && num2 == 0.0)
			{
				return default(global::ClipperLib.DoublePoint);
			}
			double num3 = 1.0 / global::System.Math.Sqrt(num * num + num2 * num2);
			num *= num3;
			num2 *= num3;
			return new global::ClipperLib.DoublePoint(num2, 0.0 - num);
		}

		private void DoOffset(double delta)
		{
			m_destPolys = new global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::ClipperLib.IntPoint>>();
			m_delta = delta;
			if (global::ClipperLib.ClipperBase.near_zero(delta))
			{
				m_destPolys.Capacity = m_polyNodes.ChildCount;
				for (int i = 0; i < m_polyNodes.ChildCount; i++)
				{
					global::ClipperLib.PolyNode polyNode = m_polyNodes.Childs[i];
					if (polyNode.m_endtype == global::ClipperLib.EndType.etClosedPolygon)
					{
						m_destPolys.Add(polyNode.m_polygon);
					}
				}
				return;
			}
			if (MiterLimit > 2.0)
			{
				m_miterLim = 2.0 / (MiterLimit * MiterLimit);
			}
			else
			{
				m_miterLim = 0.5;
			}
			double num = ((ArcTolerance <= 0.0) ? 0.25 : ((!(ArcTolerance > global::System.Math.Abs(delta) * 0.25)) ? ArcTolerance : (global::System.Math.Abs(delta) * 0.25)));
			double num2 = global::System.Math.PI / global::System.Math.Acos(1.0 - num / global::System.Math.Abs(delta));
			m_sin = global::System.Math.Sin(global::System.Math.PI * 2.0 / num2);
			m_cos = global::System.Math.Cos(global::System.Math.PI * 2.0 / num2);
			m_StepsPerRad = num2 / (global::System.Math.PI * 2.0);
			if (delta < 0.0)
			{
				m_sin = 0.0 - m_sin;
			}
			m_destPolys.Capacity = m_polyNodes.ChildCount * 2;
			for (int j = 0; j < m_polyNodes.ChildCount; j++)
			{
				global::ClipperLib.PolyNode polyNode2 = m_polyNodes.Childs[j];
				m_srcPoly = polyNode2.m_polygon;
				int count = m_srcPoly.Count;
				if (count == 0 || (delta <= 0.0 && (count < 3 || polyNode2.m_endtype != global::ClipperLib.EndType.etClosedPolygon)))
				{
					continue;
				}
				m_destPoly = new global::System.Collections.Generic.List<global::ClipperLib.IntPoint>();
				if (count == 1)
				{
					if (polyNode2.m_jointype == global::ClipperLib.JoinType.jtRound)
					{
						double num3 = 1.0;
						double num4 = 0.0;
						for (int k = 1; (double)k <= num2; k++)
						{
							m_destPoly.Add(new global::ClipperLib.IntPoint(Round((double)m_srcPoly[0].X + num3 * delta), Round((double)m_srcPoly[0].Y + num4 * delta)));
							double num5 = num3;
							num3 = num3 * m_cos - m_sin * num4;
							num4 = num5 * m_sin + num4 * m_cos;
						}
					}
					else
					{
						double num6 = -1.0;
						double num7 = -1.0;
						for (int l = 0; l < 4; l++)
						{
							m_destPoly.Add(new global::ClipperLib.IntPoint(Round((double)m_srcPoly[0].X + num6 * delta), Round((double)m_srcPoly[0].Y + num7 * delta)));
							if (num6 < 0.0)
							{
								num6 = 1.0;
							}
							else if (num7 < 0.0)
							{
								num7 = 1.0;
							}
							else
							{
								num6 = -1.0;
							}
						}
					}
					m_destPolys.Add(m_destPoly);
					continue;
				}
				m_normals.Clear();
				m_normals.Capacity = count;
				for (int m = 0; m < count - 1; m++)
				{
					m_normals.Add(GetUnitNormal(m_srcPoly[m], m_srcPoly[m + 1]));
				}
				if (polyNode2.m_endtype == global::ClipperLib.EndType.etClosedLine || polyNode2.m_endtype == global::ClipperLib.EndType.etClosedPolygon)
				{
					m_normals.Add(GetUnitNormal(m_srcPoly[count - 1], m_srcPoly[0]));
				}
				else
				{
					m_normals.Add(new global::ClipperLib.DoublePoint(m_normals[count - 2]));
				}
				if (polyNode2.m_endtype == global::ClipperLib.EndType.etClosedPolygon)
				{
					int k2 = count - 1;
					for (int n = 0; n < count; n++)
					{
						OffsetPoint(n, ref k2, polyNode2.m_jointype);
					}
					m_destPolys.Add(m_destPoly);
					continue;
				}
				if (polyNode2.m_endtype == global::ClipperLib.EndType.etClosedLine)
				{
					int k3 = count - 1;
					for (int num8 = 0; num8 < count; num8++)
					{
						OffsetPoint(num8, ref k3, polyNode2.m_jointype);
					}
					m_destPolys.Add(m_destPoly);
					m_destPoly = new global::System.Collections.Generic.List<global::ClipperLib.IntPoint>();
					global::ClipperLib.DoublePoint doublePoint = m_normals[count - 1];
					for (int num9 = count - 1; num9 > 0; num9--)
					{
						m_normals[num9] = new global::ClipperLib.DoublePoint(0.0 - m_normals[num9 - 1].X, 0.0 - m_normals[num9 - 1].Y);
					}
					m_normals[0] = new global::ClipperLib.DoublePoint(0.0 - doublePoint.X, 0.0 - doublePoint.Y);
					k3 = 0;
					for (int num10 = count - 1; num10 >= 0; num10--)
					{
						OffsetPoint(num10, ref k3, polyNode2.m_jointype);
					}
					m_destPolys.Add(m_destPoly);
					continue;
				}
				int k4 = 0;
				for (int num11 = 1; num11 < count - 1; num11++)
				{
					OffsetPoint(num11, ref k4, polyNode2.m_jointype);
				}
				if (polyNode2.m_endtype == global::ClipperLib.EndType.etOpenButt)
				{
					int index = count - 1;
					global::ClipperLib.IntPoint item = new global::ClipperLib.IntPoint(Round((double)m_srcPoly[index].X + m_normals[index].X * delta), Round((double)m_srcPoly[index].Y + m_normals[index].Y * delta));
					m_destPoly.Add(item);
					item = new global::ClipperLib.IntPoint(Round((double)m_srcPoly[index].X - m_normals[index].X * delta), Round((double)m_srcPoly[index].Y - m_normals[index].Y * delta));
					m_destPoly.Add(item);
				}
				else
				{
					int num12 = count - 1;
					k4 = count - 2;
					m_sinA = 0.0;
					m_normals[num12] = new global::ClipperLib.DoublePoint(0.0 - m_normals[num12].X, 0.0 - m_normals[num12].Y);
					if (polyNode2.m_endtype == global::ClipperLib.EndType.etOpenSquare)
					{
						DoSquare(num12, k4);
					}
					else
					{
						DoRound(num12, k4);
					}
				}
				for (int num13 = count - 1; num13 > 0; num13--)
				{
					m_normals[num13] = new global::ClipperLib.DoublePoint(0.0 - m_normals[num13 - 1].X, 0.0 - m_normals[num13 - 1].Y);
				}
				m_normals[0] = new global::ClipperLib.DoublePoint(0.0 - m_normals[1].X, 0.0 - m_normals[1].Y);
				k4 = count - 1;
				for (int num14 = k4 - 1; num14 > 0; num14--)
				{
					OffsetPoint(num14, ref k4, polyNode2.m_jointype);
				}
				if (polyNode2.m_endtype == global::ClipperLib.EndType.etOpenButt)
				{
					global::ClipperLib.IntPoint item = new global::ClipperLib.IntPoint(Round((double)m_srcPoly[0].X - m_normals[0].X * delta), Round((double)m_srcPoly[0].Y - m_normals[0].Y * delta));
					m_destPoly.Add(item);
					item = new global::ClipperLib.IntPoint(Round((double)m_srcPoly[0].X + m_normals[0].X * delta), Round((double)m_srcPoly[0].Y + m_normals[0].Y * delta));
					m_destPoly.Add(item);
				}
				else
				{
					k4 = 1;
					m_sinA = 0.0;
					if (polyNode2.m_endtype == global::ClipperLib.EndType.etOpenSquare)
					{
						DoSquare(0, 1);
					}
					else
					{
						DoRound(0, 1);
					}
				}
				m_destPolys.Add(m_destPoly);
			}
		}

		public void Execute(ref global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::ClipperLib.IntPoint>> solution, double delta)
		{
			solution.Clear();
			FixOrientations();
			DoOffset(delta);
			global::ClipperLib.Clipper clipper = new global::ClipperLib.Clipper();
			clipper.AddPaths(m_destPolys, global::ClipperLib.PolyType.ptSubject, closed: true);
			if (delta > 0.0)
			{
				clipper.Execute(global::ClipperLib.ClipType.ctUnion, solution, global::ClipperLib.PolyFillType.pftPositive, global::ClipperLib.PolyFillType.pftPositive);
				return;
			}
			global::ClipperLib.IntRect bounds = global::ClipperLib.ClipperBase.GetBounds(m_destPolys);
			global::System.Collections.Generic.List<global::ClipperLib.IntPoint> list = new global::System.Collections.Generic.List<global::ClipperLib.IntPoint>(4);
			list.Add(new global::ClipperLib.IntPoint(bounds.left - 10, bounds.bottom + 10));
			list.Add(new global::ClipperLib.IntPoint(bounds.right + 10, bounds.bottom + 10));
			list.Add(new global::ClipperLib.IntPoint(bounds.right + 10, bounds.top - 10));
			list.Add(new global::ClipperLib.IntPoint(bounds.left - 10, bounds.top - 10));
			clipper.AddPath(list, global::ClipperLib.PolyType.ptSubject, Closed: true);
			clipper.ReverseSolution = true;
			clipper.Execute(global::ClipperLib.ClipType.ctUnion, solution, global::ClipperLib.PolyFillType.pftNegative, global::ClipperLib.PolyFillType.pftNegative);
			if (solution.Count > 0)
			{
				solution.RemoveAt(0);
			}
		}

		public void Execute(ref global::ClipperLib.PolyTree solution, double delta)
		{
			solution.Clear();
			FixOrientations();
			DoOffset(delta);
			global::ClipperLib.Clipper clipper = new global::ClipperLib.Clipper();
			clipper.AddPaths(m_destPolys, global::ClipperLib.PolyType.ptSubject, closed: true);
			if (delta > 0.0)
			{
				clipper.Execute(global::ClipperLib.ClipType.ctUnion, solution, global::ClipperLib.PolyFillType.pftPositive, global::ClipperLib.PolyFillType.pftPositive);
				return;
			}
			global::ClipperLib.IntRect bounds = global::ClipperLib.ClipperBase.GetBounds(m_destPolys);
			global::System.Collections.Generic.List<global::ClipperLib.IntPoint> list = new global::System.Collections.Generic.List<global::ClipperLib.IntPoint>(4);
			list.Add(new global::ClipperLib.IntPoint(bounds.left - 10, bounds.bottom + 10));
			list.Add(new global::ClipperLib.IntPoint(bounds.right + 10, bounds.bottom + 10));
			list.Add(new global::ClipperLib.IntPoint(bounds.right + 10, bounds.top - 10));
			list.Add(new global::ClipperLib.IntPoint(bounds.left - 10, bounds.top - 10));
			clipper.AddPath(list, global::ClipperLib.PolyType.ptSubject, Closed: true);
			clipper.ReverseSolution = true;
			clipper.Execute(global::ClipperLib.ClipType.ctUnion, solution, global::ClipperLib.PolyFillType.pftNegative, global::ClipperLib.PolyFillType.pftNegative);
			if (solution.ChildCount == 1 && solution.Childs[0].ChildCount > 0)
			{
				global::ClipperLib.PolyNode polyNode = solution.Childs[0];
				solution.Childs.Capacity = polyNode.ChildCount;
				solution.Childs[0] = polyNode.Childs[0];
				solution.Childs[0].m_Parent = solution;
				for (int i = 1; i < polyNode.ChildCount; i++)
				{
					solution.AddChild(polyNode.Childs[i]);
				}
			}
			else
			{
				solution.Clear();
			}
		}

		private void OffsetPoint(int j, ref int k, global::ClipperLib.JoinType jointype)
		{
			m_sinA = m_normals[k].X * m_normals[j].Y - m_normals[j].X * m_normals[k].Y;
			if (global::System.Math.Abs(m_sinA * m_delta) < 1.0)
			{
				double num = m_normals[k].X * m_normals[j].X + m_normals[j].Y * m_normals[k].Y;
				if (num > 0.0)
				{
					m_destPoly.Add(new global::ClipperLib.IntPoint(Round((double)m_srcPoly[j].X + m_normals[k].X * m_delta), Round((double)m_srcPoly[j].Y + m_normals[k].Y * m_delta)));
					return;
				}
			}
			else if (m_sinA > 1.0)
			{
				m_sinA = 1.0;
			}
			else if (m_sinA < -1.0)
			{
				m_sinA = -1.0;
			}
			if (m_sinA * m_delta < 0.0)
			{
				m_destPoly.Add(new global::ClipperLib.IntPoint(Round((double)m_srcPoly[j].X + m_normals[k].X * m_delta), Round((double)m_srcPoly[j].Y + m_normals[k].Y * m_delta)));
				m_destPoly.Add(m_srcPoly[j]);
				m_destPoly.Add(new global::ClipperLib.IntPoint(Round((double)m_srcPoly[j].X + m_normals[j].X * m_delta), Round((double)m_srcPoly[j].Y + m_normals[j].Y * m_delta)));
			}
			else
			{
				switch (jointype)
				{
				case global::ClipperLib.JoinType.jtMiter:
				{
					double num2 = 1.0 + (m_normals[j].X * m_normals[k].X + m_normals[j].Y * m_normals[k].Y);
					if (num2 >= m_miterLim)
					{
						DoMiter(j, k, num2);
					}
					else
					{
						DoSquare(j, k);
					}
					break;
				}
				case global::ClipperLib.JoinType.jtSquare:
					DoSquare(j, k);
					break;
				case global::ClipperLib.JoinType.jtRound:
					DoRound(j, k);
					break;
				}
			}
			k = j;
		}

		internal void DoSquare(int j, int k)
		{
			double num = global::System.Math.Tan(global::System.Math.Atan2(m_sinA, m_normals[k].X * m_normals[j].X + m_normals[k].Y * m_normals[j].Y) / 4.0);
			m_destPoly.Add(new global::ClipperLib.IntPoint(Round((double)m_srcPoly[j].X + m_delta * (m_normals[k].X - m_normals[k].Y * num)), Round((double)m_srcPoly[j].Y + m_delta * (m_normals[k].Y + m_normals[k].X * num))));
			m_destPoly.Add(new global::ClipperLib.IntPoint(Round((double)m_srcPoly[j].X + m_delta * (m_normals[j].X + m_normals[j].Y * num)), Round((double)m_srcPoly[j].Y + m_delta * (m_normals[j].Y - m_normals[j].X * num))));
		}

		internal void DoMiter(int j, int k, double r)
		{
			double num = m_delta / r;
			m_destPoly.Add(new global::ClipperLib.IntPoint(Round((double)m_srcPoly[j].X + (m_normals[k].X + m_normals[j].X) * num), Round((double)m_srcPoly[j].Y + (m_normals[k].Y + m_normals[j].Y) * num)));
		}

		internal void DoRound(int j, int k)
		{
			double value = global::System.Math.Atan2(m_sinA, m_normals[k].X * m_normals[j].X + m_normals[k].Y * m_normals[j].Y);
			int num = global::System.Math.Max((int)Round(m_StepsPerRad * global::System.Math.Abs(value)), 1);
			double num2 = m_normals[k].X;
			double num3 = m_normals[k].Y;
			for (int i = 0; i < num; i++)
			{
				m_destPoly.Add(new global::ClipperLib.IntPoint(Round((double)m_srcPoly[j].X + num2 * m_delta), Round((double)m_srcPoly[j].Y + num3 * m_delta)));
				double num4 = num2;
				num2 = num2 * m_cos - m_sin * num3;
				num3 = num4 * m_sin + num3 * m_cos;
			}
			m_destPoly.Add(new global::ClipperLib.IntPoint(Round((double)m_srcPoly[j].X + m_normals[j].X * m_delta), Round((double)m_srcPoly[j].Y + m_normals[j].Y * m_delta)));
		}
	}
}
