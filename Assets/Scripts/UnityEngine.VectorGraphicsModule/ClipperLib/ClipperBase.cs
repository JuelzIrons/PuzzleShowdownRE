namespace ClipperLib
{
	internal class ClipperBase
	{
		internal const double horizontal = -3.4E+38;

		internal const int Skip = -2;

		internal const int Unassigned = -1;

		internal const double tolerance = 1E-20;

		public const long loRange = 1073741823L;

		public const long hiRange = 4611686018427387903L;

		internal global::ClipperLib.LocalMinima m_MinimaList;

		internal global::ClipperLib.LocalMinima m_CurrentLM;

		internal global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::ClipperLib.TEdge>> m_edges = new global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::ClipperLib.TEdge>>();

		internal global::ClipperLib.Scanbeam m_Scanbeam;

		internal global::System.Collections.Generic.List<global::ClipperLib.OutRec> m_PolyOuts;

		internal global::ClipperLib.TEdge m_ActiveEdges;

		internal bool m_UseFullRange;

		internal bool m_HasOpenPaths;

		public bool PreserveCollinear { get; set; }

		internal static bool near_zero(double val)
		{
			return val > -1E-20 && val < 1E-20;
		}

		public void Swap(ref long val1, ref long val2)
		{
			long num = val1;
			val1 = val2;
			val2 = num;
		}

		internal static bool IsHorizontal(global::ClipperLib.TEdge e)
		{
			return e.Delta.Y == 0;
		}

		internal bool PointIsVertex(global::ClipperLib.IntPoint pt, global::ClipperLib.OutPt pp)
		{
			global::ClipperLib.OutPt outPt = pp;
			do
			{
				if (outPt.Pt == pt)
				{
					return true;
				}
				outPt = outPt.Next;
			}
			while (outPt != pp);
			return false;
		}

		internal bool PointOnLineSegment(global::ClipperLib.IntPoint pt, global::ClipperLib.IntPoint linePt1, global::ClipperLib.IntPoint linePt2, bool UseFullRange)
		{
			if (UseFullRange)
			{
				return (pt.X == linePt1.X && pt.Y == linePt1.Y) || (pt.X == linePt2.X && pt.Y == linePt2.Y) || (pt.X > linePt1.X == pt.X < linePt2.X && pt.Y > linePt1.Y == pt.Y < linePt2.Y && global::ClipperLib.Int128.Int128Mul(pt.X - linePt1.X, linePt2.Y - linePt1.Y) == global::ClipperLib.Int128.Int128Mul(linePt2.X - linePt1.X, pt.Y - linePt1.Y));
			}
			return (pt.X == linePt1.X && pt.Y == linePt1.Y) || (pt.X == linePt2.X && pt.Y == linePt2.Y) || (pt.X > linePt1.X == pt.X < linePt2.X && pt.Y > linePt1.Y == pt.Y < linePt2.Y && (pt.X - linePt1.X) * (linePt2.Y - linePt1.Y) == (linePt2.X - linePt1.X) * (pt.Y - linePt1.Y));
		}

		internal bool PointOnPolygon(global::ClipperLib.IntPoint pt, global::ClipperLib.OutPt pp, bool UseFullRange)
		{
			global::ClipperLib.OutPt outPt = pp;
			do
			{
				if (PointOnLineSegment(pt, outPt.Pt, outPt.Next.Pt, UseFullRange))
				{
					return true;
				}
				outPt = outPt.Next;
			}
			while (outPt != pp);
			return false;
		}

		internal static bool SlopesEqual(global::ClipperLib.TEdge e1, global::ClipperLib.TEdge e2, bool UseFullRange)
		{
			if (UseFullRange)
			{
				return global::ClipperLib.Int128.Int128Mul(e1.Delta.Y, e2.Delta.X) == global::ClipperLib.Int128.Int128Mul(e1.Delta.X, e2.Delta.Y);
			}
			return e1.Delta.Y * e2.Delta.X == e1.Delta.X * e2.Delta.Y;
		}

		internal static bool SlopesEqual(global::ClipperLib.IntPoint pt1, global::ClipperLib.IntPoint pt2, global::ClipperLib.IntPoint pt3, bool UseFullRange)
		{
			if (UseFullRange)
			{
				return global::ClipperLib.Int128.Int128Mul(pt1.Y - pt2.Y, pt2.X - pt3.X) == global::ClipperLib.Int128.Int128Mul(pt1.X - pt2.X, pt2.Y - pt3.Y);
			}
			return (pt1.Y - pt2.Y) * (pt2.X - pt3.X) - (pt1.X - pt2.X) * (pt2.Y - pt3.Y) == 0;
		}

		internal static bool SlopesEqual(global::ClipperLib.IntPoint pt1, global::ClipperLib.IntPoint pt2, global::ClipperLib.IntPoint pt3, global::ClipperLib.IntPoint pt4, bool UseFullRange)
		{
			if (UseFullRange)
			{
				return global::ClipperLib.Int128.Int128Mul(pt1.Y - pt2.Y, pt3.X - pt4.X) == global::ClipperLib.Int128.Int128Mul(pt1.X - pt2.X, pt3.Y - pt4.Y);
			}
			return (pt1.Y - pt2.Y) * (pt3.X - pt4.X) - (pt1.X - pt2.X) * (pt3.Y - pt4.Y) == 0;
		}

		internal ClipperBase()
		{
			m_MinimaList = null;
			m_CurrentLM = null;
			m_UseFullRange = false;
			m_HasOpenPaths = false;
		}

		public virtual void Clear()
		{
			DisposeLocalMinimaList();
			for (int i = 0; i < m_edges.Count; i++)
			{
				for (int j = 0; j < m_edges[i].Count; j++)
				{
					m_edges[i][j] = null;
				}
				m_edges[i].Clear();
			}
			m_edges.Clear();
			m_UseFullRange = false;
			m_HasOpenPaths = false;
		}

		private void DisposeLocalMinimaList()
		{
			while (m_MinimaList != null)
			{
				global::ClipperLib.LocalMinima next = m_MinimaList.Next;
				m_MinimaList = null;
				m_MinimaList = next;
			}
			m_CurrentLM = null;
		}

		private void RangeTest(global::ClipperLib.IntPoint Pt, ref bool useFullRange)
		{
			if (useFullRange)
			{
				if (Pt.X > 4611686018427387903L || Pt.Y > 4611686018427387903L || -Pt.X > 4611686018427387903L || -Pt.Y > 4611686018427387903L)
				{
					throw new global::ClipperLib.ClipperException("Coordinate outside allowed range");
				}
			}
			else if (Pt.X > 1073741823 || Pt.Y > 1073741823 || -Pt.X > 1073741823 || -Pt.Y > 1073741823)
			{
				useFullRange = true;
				RangeTest(Pt, ref useFullRange);
			}
		}

		private void InitEdge(global::ClipperLib.TEdge e, global::ClipperLib.TEdge eNext, global::ClipperLib.TEdge ePrev, global::ClipperLib.IntPoint pt)
		{
			e.Next = eNext;
			e.Prev = ePrev;
			e.Curr = pt;
			e.OutIdx = -1;
		}

		private void InitEdge2(global::ClipperLib.TEdge e, global::ClipperLib.PolyType polyType)
		{
			if (e.Curr.Y >= e.Next.Curr.Y)
			{
				e.Bot = e.Curr;
				e.Top = e.Next.Curr;
			}
			else
			{
				e.Top = e.Curr;
				e.Bot = e.Next.Curr;
			}
			SetDx(e);
			e.PolyTyp = polyType;
		}

		private global::ClipperLib.TEdge FindNextLocMin(global::ClipperLib.TEdge E)
		{
			while (true)
			{
				if (!(E.Bot != E.Prev.Bot) && !(E.Curr == E.Top))
				{
					if (E.Dx != -3.4E+38 && E.Prev.Dx != -3.4E+38)
					{
						break;
					}
					while (E.Prev.Dx == -3.4E+38)
					{
						E = E.Prev;
					}
					global::ClipperLib.TEdge tEdge = E;
					while (E.Dx == -3.4E+38)
					{
						E = E.Next;
					}
					if (E.Top.Y != E.Prev.Bot.Y)
					{
						if (tEdge.Prev.Bot.X < E.Bot.X)
						{
							E = tEdge;
						}
						break;
					}
				}
				else
				{
					E = E.Next;
				}
			}
			return E;
		}

		private global::ClipperLib.TEdge ProcessBound(global::ClipperLib.TEdge E, bool LeftBoundIsForward)
		{
			global::ClipperLib.TEdge tEdge = E;
			if (tEdge.OutIdx == -2)
			{
				E = tEdge;
				if (LeftBoundIsForward)
				{
					while (E.Top.Y == E.Next.Bot.Y)
					{
						E = E.Next;
					}
					while (E != tEdge && E.Dx == -3.4E+38)
					{
						E = E.Prev;
					}
				}
				else
				{
					while (E.Top.Y == E.Prev.Bot.Y)
					{
						E = E.Prev;
					}
					while (E != tEdge && E.Dx == -3.4E+38)
					{
						E = E.Next;
					}
				}
				if (E == tEdge)
				{
					tEdge = ((!LeftBoundIsForward) ? E.Prev : E.Next);
				}
				else
				{
					E = ((!LeftBoundIsForward) ? tEdge.Prev : tEdge.Next);
					global::ClipperLib.LocalMinima localMinima = new global::ClipperLib.LocalMinima();
					localMinima.Next = null;
					localMinima.Y = E.Bot.Y;
					localMinima.LeftBound = null;
					localMinima.RightBound = E;
					E.WindDelta = 0;
					tEdge = ProcessBound(E, LeftBoundIsForward);
					InsertLocalMinima(localMinima);
				}
				return tEdge;
			}
			global::ClipperLib.TEdge tEdge2;
			if (E.Dx == -3.4E+38)
			{
				tEdge2 = ((!LeftBoundIsForward) ? E.Next : E.Prev);
				if (tEdge2.Dx == -3.4E+38)
				{
					if (tEdge2.Bot.X != E.Bot.X && tEdge2.Top.X != E.Bot.X)
					{
						ReverseHorizontal(E);
					}
				}
				else if (tEdge2.Bot.X != E.Bot.X)
				{
					ReverseHorizontal(E);
				}
			}
			tEdge2 = E;
			if (LeftBoundIsForward)
			{
				while (tEdge.Top.Y == tEdge.Next.Bot.Y && tEdge.Next.OutIdx != -2)
				{
					tEdge = tEdge.Next;
				}
				if (tEdge.Dx == -3.4E+38 && tEdge.Next.OutIdx != -2)
				{
					global::ClipperLib.TEdge tEdge3 = tEdge;
					while (tEdge3.Prev.Dx == -3.4E+38)
					{
						tEdge3 = tEdge3.Prev;
					}
					if (tEdge3.Prev.Top.X > tEdge.Next.Top.X)
					{
						tEdge = tEdge3.Prev;
					}
				}
				while (E != tEdge)
				{
					E.NextInLML = E.Next;
					if (E.Dx == -3.4E+38 && E != tEdge2 && E.Bot.X != E.Prev.Top.X)
					{
						ReverseHorizontal(E);
					}
					E = E.Next;
				}
				if (E.Dx == -3.4E+38 && E != tEdge2 && E.Bot.X != E.Prev.Top.X)
				{
					ReverseHorizontal(E);
				}
				tEdge = tEdge.Next;
			}
			else
			{
				while (tEdge.Top.Y == tEdge.Prev.Bot.Y && tEdge.Prev.OutIdx != -2)
				{
					tEdge = tEdge.Prev;
				}
				if (tEdge.Dx == -3.4E+38 && tEdge.Prev.OutIdx != -2)
				{
					global::ClipperLib.TEdge tEdge3 = tEdge;
					while (tEdge3.Next.Dx == -3.4E+38)
					{
						tEdge3 = tEdge3.Next;
					}
					if (tEdge3.Next.Top.X == tEdge.Prev.Top.X || tEdge3.Next.Top.X > tEdge.Prev.Top.X)
					{
						tEdge = tEdge3.Next;
					}
				}
				while (E != tEdge)
				{
					E.NextInLML = E.Prev;
					if (E.Dx == -3.4E+38 && E != tEdge2 && E.Bot.X != E.Next.Top.X)
					{
						ReverseHorizontal(E);
					}
					E = E.Prev;
				}
				if (E.Dx == -3.4E+38 && E != tEdge2 && E.Bot.X != E.Next.Top.X)
				{
					ReverseHorizontal(E);
				}
				tEdge = tEdge.Prev;
			}
			return tEdge;
		}

		public bool AddPath(global::System.Collections.Generic.List<global::ClipperLib.IntPoint> pg, global::ClipperLib.PolyType polyType, bool Closed)
		{
			if (!Closed && polyType == global::ClipperLib.PolyType.ptClip)
			{
				throw new global::ClipperLib.ClipperException("AddPath: Open paths must be subject.");
			}
			int num = pg.Count - 1;
			if (Closed)
			{
				while (num > 0 && pg[num] == pg[0])
				{
					num--;
				}
			}
			while (num > 0 && pg[num] == pg[num - 1])
			{
				num--;
			}
			if ((Closed && num < 2) || (!Closed && num < 1))
			{
				return false;
			}
			global::System.Collections.Generic.List<global::ClipperLib.TEdge> list = new global::System.Collections.Generic.List<global::ClipperLib.TEdge>(num + 1);
			for (int i = 0; i <= num; i++)
			{
				list.Add(new global::ClipperLib.TEdge());
			}
			bool flag = true;
			list[1].Curr = pg[1];
			RangeTest(pg[0], ref m_UseFullRange);
			RangeTest(pg[num], ref m_UseFullRange);
			InitEdge(list[0], list[1], list[num], pg[0]);
			InitEdge(list[num], list[0], list[num - 1], pg[num]);
			for (int num2 = num - 1; num2 >= 1; num2--)
			{
				RangeTest(pg[num2], ref m_UseFullRange);
				InitEdge(list[num2], list[num2 + 1], list[num2 - 1], pg[num2]);
			}
			global::ClipperLib.TEdge tEdge = list[0];
			global::ClipperLib.TEdge tEdge2 = tEdge;
			global::ClipperLib.TEdge tEdge3 = tEdge;
			while (true)
			{
				if (tEdge2.Curr == tEdge2.Next.Curr && (Closed || tEdge2.Next != tEdge))
				{
					if (tEdge2 == tEdge2.Next)
					{
						break;
					}
					if (tEdge2 == tEdge)
					{
						tEdge = tEdge2.Next;
					}
					tEdge2 = RemoveEdge(tEdge2);
					tEdge3 = tEdge2;
					continue;
				}
				if (tEdge2.Prev == tEdge2.Next)
				{
					break;
				}
				if (Closed && SlopesEqual(tEdge2.Prev.Curr, tEdge2.Curr, tEdge2.Next.Curr, m_UseFullRange) && (!PreserveCollinear || !Pt2IsBetweenPt1AndPt3(tEdge2.Prev.Curr, tEdge2.Curr, tEdge2.Next.Curr)))
				{
					if (tEdge2 == tEdge)
					{
						tEdge = tEdge2.Next;
					}
					tEdge2 = RemoveEdge(tEdge2);
					tEdge2 = tEdge2.Prev;
					tEdge3 = tEdge2;
				}
				else
				{
					tEdge2 = tEdge2.Next;
					if (tEdge2 == tEdge3 || (!Closed && tEdge2.Next == tEdge))
					{
						break;
					}
				}
			}
			if ((!Closed && tEdge2 == tEdge2.Next) || (Closed && tEdge2.Prev == tEdge2.Next))
			{
				return false;
			}
			if (!Closed)
			{
				m_HasOpenPaths = true;
				tEdge.Prev.OutIdx = -2;
			}
			tEdge2 = tEdge;
			do
			{
				InitEdge2(tEdge2, polyType);
				tEdge2 = tEdge2.Next;
				if (flag && tEdge2.Curr.Y != tEdge.Curr.Y)
				{
					flag = false;
				}
			}
			while (tEdge2 != tEdge);
			if (flag)
			{
				if (Closed)
				{
					return false;
				}
				tEdge2.Prev.OutIdx = -2;
				global::ClipperLib.LocalMinima localMinima = new global::ClipperLib.LocalMinima();
				localMinima.Next = null;
				localMinima.Y = tEdge2.Bot.Y;
				localMinima.LeftBound = null;
				localMinima.RightBound = tEdge2;
				localMinima.RightBound.Side = global::ClipperLib.EdgeSide.esRight;
				localMinima.RightBound.WindDelta = 0;
				while (true)
				{
					if (tEdge2.Bot.X != tEdge2.Prev.Top.X)
					{
						ReverseHorizontal(tEdge2);
					}
					if (tEdge2.Next.OutIdx == -2)
					{
						break;
					}
					tEdge2.NextInLML = tEdge2.Next;
					tEdge2 = tEdge2.Next;
				}
				InsertLocalMinima(localMinima);
				m_edges.Add(list);
				return true;
			}
			m_edges.Add(list);
			global::ClipperLib.TEdge tEdge4 = null;
			if (tEdge2.Prev.Bot == tEdge2.Prev.Top)
			{
				tEdge2 = tEdge2.Next;
			}
			while (true)
			{
				tEdge2 = FindNextLocMin(tEdge2);
				if (tEdge2 == tEdge4)
				{
					break;
				}
				if (tEdge4 == null)
				{
					tEdge4 = tEdge2;
				}
				global::ClipperLib.LocalMinima localMinima2 = new global::ClipperLib.LocalMinima();
				localMinima2.Next = null;
				localMinima2.Y = tEdge2.Bot.Y;
				bool flag2;
				if (tEdge2.Dx < tEdge2.Prev.Dx)
				{
					localMinima2.LeftBound = tEdge2.Prev;
					localMinima2.RightBound = tEdge2;
					flag2 = false;
				}
				else
				{
					localMinima2.LeftBound = tEdge2;
					localMinima2.RightBound = tEdge2.Prev;
					flag2 = true;
				}
				localMinima2.LeftBound.Side = global::ClipperLib.EdgeSide.esLeft;
				localMinima2.RightBound.Side = global::ClipperLib.EdgeSide.esRight;
				if (!Closed)
				{
					localMinima2.LeftBound.WindDelta = 0;
				}
				else if (localMinima2.LeftBound.Next == localMinima2.RightBound)
				{
					localMinima2.LeftBound.WindDelta = -1;
				}
				else
				{
					localMinima2.LeftBound.WindDelta = 1;
				}
				localMinima2.RightBound.WindDelta = -localMinima2.LeftBound.WindDelta;
				tEdge2 = ProcessBound(localMinima2.LeftBound, flag2);
				if (tEdge2.OutIdx == -2)
				{
					tEdge2 = ProcessBound(tEdge2, flag2);
				}
				global::ClipperLib.TEdge tEdge5 = ProcessBound(localMinima2.RightBound, !flag2);
				if (tEdge5.OutIdx == -2)
				{
					tEdge5 = ProcessBound(tEdge5, !flag2);
				}
				if (localMinima2.LeftBound.OutIdx == -2)
				{
					localMinima2.LeftBound = null;
				}
				else if (localMinima2.RightBound.OutIdx == -2)
				{
					localMinima2.RightBound = null;
				}
				InsertLocalMinima(localMinima2);
				if (!flag2)
				{
					tEdge2 = tEdge5;
				}
			}
			return true;
		}

		public bool AddPaths(global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::ClipperLib.IntPoint>> ppg, global::ClipperLib.PolyType polyType, bool closed)
		{
			bool result = false;
			for (int i = 0; i < ppg.Count; i++)
			{
				if (AddPath(ppg[i], polyType, closed))
				{
					result = true;
				}
			}
			return result;
		}

		internal bool Pt2IsBetweenPt1AndPt3(global::ClipperLib.IntPoint pt1, global::ClipperLib.IntPoint pt2, global::ClipperLib.IntPoint pt3)
		{
			if (pt1 == pt3 || pt1 == pt2 || pt3 == pt2)
			{
				return false;
			}
			if (pt1.X != pt3.X)
			{
				return pt2.X > pt1.X == pt2.X < pt3.X;
			}
			return pt2.Y > pt1.Y == pt2.Y < pt3.Y;
		}

		private global::ClipperLib.TEdge RemoveEdge(global::ClipperLib.TEdge e)
		{
			e.Prev.Next = e.Next;
			e.Next.Prev = e.Prev;
			global::ClipperLib.TEdge next = e.Next;
			e.Prev = null;
			return next;
		}

		private void SetDx(global::ClipperLib.TEdge e)
		{
			e.Delta.X = e.Top.X - e.Bot.X;
			e.Delta.Y = e.Top.Y - e.Bot.Y;
			if (e.Delta.Y == 0)
			{
				e.Dx = -3.4E+38;
			}
			else
			{
				e.Dx = (double)e.Delta.X / (double)e.Delta.Y;
			}
		}

		private void InsertLocalMinima(global::ClipperLib.LocalMinima newLm)
		{
			if (m_MinimaList == null)
			{
				m_MinimaList = newLm;
				return;
			}
			if (newLm.Y >= m_MinimaList.Y)
			{
				newLm.Next = m_MinimaList;
				m_MinimaList = newLm;
				return;
			}
			global::ClipperLib.LocalMinima localMinima = m_MinimaList;
			while (localMinima.Next != null && newLm.Y < localMinima.Next.Y)
			{
				localMinima = localMinima.Next;
			}
			newLm.Next = localMinima.Next;
			localMinima.Next = newLm;
		}

		internal bool PopLocalMinima(long Y, out global::ClipperLib.LocalMinima current)
		{
			current = m_CurrentLM;
			if (m_CurrentLM != null && m_CurrentLM.Y == Y)
			{
				m_CurrentLM = m_CurrentLM.Next;
				return true;
			}
			return false;
		}

		private void ReverseHorizontal(global::ClipperLib.TEdge e)
		{
			Swap(ref e.Top.X, ref e.Bot.X);
		}

		internal virtual void Reset()
		{
			m_CurrentLM = m_MinimaList;
			if (m_CurrentLM == null)
			{
				return;
			}
			m_Scanbeam = null;
			for (global::ClipperLib.LocalMinima localMinima = m_MinimaList; localMinima != null; localMinima = localMinima.Next)
			{
				InsertScanbeam(localMinima.Y);
				global::ClipperLib.TEdge leftBound = localMinima.LeftBound;
				if (leftBound != null)
				{
					leftBound.Curr = leftBound.Bot;
					leftBound.OutIdx = -1;
				}
				leftBound = localMinima.RightBound;
				if (leftBound != null)
				{
					leftBound.Curr = leftBound.Bot;
					leftBound.OutIdx = -1;
				}
			}
			m_ActiveEdges = null;
		}

		public static global::ClipperLib.IntRect GetBounds(global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::ClipperLib.IntPoint>> paths)
		{
			int i = 0;
			int count;
			for (count = paths.Count; i < count && paths[i].Count == 0; i++)
			{
			}
			if (i == count)
			{
				return new global::ClipperLib.IntRect(0L, 0L, 0L, 0L);
			}
			global::ClipperLib.IntRect result = default(global::ClipperLib.IntRect);
			result.left = paths[i][0].X;
			result.right = result.left;
			result.top = paths[i][0].Y;
			result.bottom = result.top;
			for (; i < count; i++)
			{
				for (int j = 0; j < paths[i].Count; j++)
				{
					if (paths[i][j].X < result.left)
					{
						result.left = paths[i][j].X;
					}
					else if (paths[i][j].X > result.right)
					{
						result.right = paths[i][j].X;
					}
					if (paths[i][j].Y < result.top)
					{
						result.top = paths[i][j].Y;
					}
					else if (paths[i][j].Y > result.bottom)
					{
						result.bottom = paths[i][j].Y;
					}
				}
			}
			return result;
		}

		internal void InsertScanbeam(long Y)
		{
			if (m_Scanbeam == null)
			{
				m_Scanbeam = new global::ClipperLib.Scanbeam();
				m_Scanbeam.Next = null;
				m_Scanbeam.Y = Y;
				return;
			}
			if (Y > m_Scanbeam.Y)
			{
				global::ClipperLib.Scanbeam scanbeam = new global::ClipperLib.Scanbeam();
				scanbeam.Y = Y;
				scanbeam.Next = m_Scanbeam;
				m_Scanbeam = scanbeam;
				return;
			}
			global::ClipperLib.Scanbeam scanbeam2 = m_Scanbeam;
			while (scanbeam2.Next != null && Y <= scanbeam2.Next.Y)
			{
				scanbeam2 = scanbeam2.Next;
			}
			if (Y != scanbeam2.Y)
			{
				global::ClipperLib.Scanbeam scanbeam3 = new global::ClipperLib.Scanbeam();
				scanbeam3.Y = Y;
				scanbeam3.Next = scanbeam2.Next;
				scanbeam2.Next = scanbeam3;
			}
		}

		internal bool PopScanbeam(out long Y)
		{
			if (m_Scanbeam == null)
			{
				Y = 0L;
				return false;
			}
			Y = m_Scanbeam.Y;
			m_Scanbeam = m_Scanbeam.Next;
			return true;
		}

		internal bool LocalMinimaPending()
		{
			return m_CurrentLM != null;
		}

		internal global::ClipperLib.OutRec CreateOutRec()
		{
			global::ClipperLib.OutRec outRec = new global::ClipperLib.OutRec();
			outRec.Idx = -1;
			outRec.IsHole = false;
			outRec.IsOpen = false;
			outRec.FirstLeft = null;
			outRec.Pts = null;
			outRec.BottomPt = null;
			outRec.PolyNode = null;
			m_PolyOuts.Add(outRec);
			outRec.Idx = m_PolyOuts.Count - 1;
			return outRec;
		}

		internal void DisposeOutRec(int index)
		{
			global::ClipperLib.OutRec outRec = m_PolyOuts[index];
			outRec.Pts = null;
			outRec = null;
			m_PolyOuts[index] = null;
		}

		internal void UpdateEdgeIntoAEL(ref global::ClipperLib.TEdge e)
		{
			if (e.NextInLML == null)
			{
				throw new global::ClipperLib.ClipperException("UpdateEdgeIntoAEL: invalid call");
			}
			global::ClipperLib.TEdge prevInAEL = e.PrevInAEL;
			global::ClipperLib.TEdge nextInAEL = e.NextInAEL;
			e.NextInLML.OutIdx = e.OutIdx;
			if (prevInAEL != null)
			{
				prevInAEL.NextInAEL = e.NextInLML;
			}
			else
			{
				m_ActiveEdges = e.NextInLML;
			}
			if (nextInAEL != null)
			{
				nextInAEL.PrevInAEL = e.NextInLML;
			}
			e.NextInLML.Side = e.Side;
			e.NextInLML.WindDelta = e.WindDelta;
			e.NextInLML.WindCnt = e.WindCnt;
			e.NextInLML.WindCnt2 = e.WindCnt2;
			e = e.NextInLML;
			e.Curr = e.Bot;
			e.PrevInAEL = prevInAEL;
			e.NextInAEL = nextInAEL;
			if (!IsHorizontal(e))
			{
				InsertScanbeam(e.Top.Y);
			}
		}

		internal void SwapPositionsInAEL(global::ClipperLib.TEdge edge1, global::ClipperLib.TEdge edge2)
		{
			if (edge1.NextInAEL == edge1.PrevInAEL || edge2.NextInAEL == edge2.PrevInAEL)
			{
				return;
			}
			if (edge1.NextInAEL == edge2)
			{
				global::ClipperLib.TEdge nextInAEL = edge2.NextInAEL;
				if (nextInAEL != null)
				{
					nextInAEL.PrevInAEL = edge1;
				}
				global::ClipperLib.TEdge prevInAEL = edge1.PrevInAEL;
				if (prevInAEL != null)
				{
					prevInAEL.NextInAEL = edge2;
				}
				edge2.PrevInAEL = prevInAEL;
				edge2.NextInAEL = edge1;
				edge1.PrevInAEL = edge2;
				edge1.NextInAEL = nextInAEL;
			}
			else if (edge2.NextInAEL == edge1)
			{
				global::ClipperLib.TEdge nextInAEL2 = edge1.NextInAEL;
				if (nextInAEL2 != null)
				{
					nextInAEL2.PrevInAEL = edge2;
				}
				global::ClipperLib.TEdge prevInAEL2 = edge2.PrevInAEL;
				if (prevInAEL2 != null)
				{
					prevInAEL2.NextInAEL = edge1;
				}
				edge1.PrevInAEL = prevInAEL2;
				edge1.NextInAEL = edge2;
				edge2.PrevInAEL = edge1;
				edge2.NextInAEL = nextInAEL2;
			}
			else
			{
				global::ClipperLib.TEdge nextInAEL3 = edge1.NextInAEL;
				global::ClipperLib.TEdge prevInAEL3 = edge1.PrevInAEL;
				edge1.NextInAEL = edge2.NextInAEL;
				if (edge1.NextInAEL != null)
				{
					edge1.NextInAEL.PrevInAEL = edge1;
				}
				edge1.PrevInAEL = edge2.PrevInAEL;
				if (edge1.PrevInAEL != null)
				{
					edge1.PrevInAEL.NextInAEL = edge1;
				}
				edge2.NextInAEL = nextInAEL3;
				if (edge2.NextInAEL != null)
				{
					edge2.NextInAEL.PrevInAEL = edge2;
				}
				edge2.PrevInAEL = prevInAEL3;
				if (edge2.PrevInAEL != null)
				{
					edge2.PrevInAEL.NextInAEL = edge2;
				}
			}
			if (edge1.PrevInAEL == null)
			{
				m_ActiveEdges = edge1;
			}
			else if (edge2.PrevInAEL == null)
			{
				m_ActiveEdges = edge2;
			}
		}

		internal void DeleteFromAEL(global::ClipperLib.TEdge e)
		{
			global::ClipperLib.TEdge prevInAEL = e.PrevInAEL;
			global::ClipperLib.TEdge nextInAEL = e.NextInAEL;
			if (prevInAEL != null || nextInAEL != null || e == m_ActiveEdges)
			{
				if (prevInAEL != null)
				{
					prevInAEL.NextInAEL = nextInAEL;
				}
				else
				{
					m_ActiveEdges = nextInAEL;
				}
				if (nextInAEL != null)
				{
					nextInAEL.PrevInAEL = prevInAEL;
				}
				e.NextInAEL = null;
				e.PrevInAEL = null;
			}
		}
	}
}
