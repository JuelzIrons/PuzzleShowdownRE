namespace UnityEngine.Rendering.Universal
{
	internal class Clipper : global::UnityEngine.Rendering.Universal.ClipperBase
	{
		internal enum NodeType
		{
			ntAny = 0,
			ntOpen = 1,
			ntClosed = 2
		}

		public const int ioReverseSolution = 1;

		public const int ioStrictlySimple = 2;

		public const int ioPreserveCollinear = 4;

		private global::UnityEngine.Rendering.Universal.ClipTypes m_ClipType;

		private global::UnityEngine.Rendering.Universal.Maxima m_Maxima;

		private global::UnityEngine.Rendering.Universal.TEdge m_SortedEdges;

		private global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntersectNode> m_IntersectList;

		private global::System.Collections.Generic.IComparer<global::UnityEngine.Rendering.Universal.IntersectNode> m_IntersectNodeComparer;

		private bool m_ExecuteLocked;

		private global::UnityEngine.Rendering.Universal.PolyFillTypes m_ClipFillType;

		private global::UnityEngine.Rendering.Universal.PolyFillTypes m_SubjFillType;

		private global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.Join> m_Joins;

		private global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.Join> m_GhostJoins;

		private bool m_UsingPolyTree;

		public int LastIndex { get; set; }

		public bool ReverseSolution { get; set; }

		public bool StrictlySimple { get; set; }

		public Clipper(int InitOptions = 0)
		{
			m_Scanbeam = null;
			m_Maxima = null;
			m_ActiveEdges = null;
			m_SortedEdges = null;
			m_IntersectList = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntersectNode>();
			m_IntersectNodeComparer = new global::UnityEngine.Rendering.Universal.MyIntersectNodeSort();
			m_ExecuteLocked = false;
			m_UsingPolyTree = false;
			m_PolyOuts = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.OutRec>();
			m_Joins = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.Join>();
			m_GhostJoins = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.Join>();
			ReverseSolution = (1 & InitOptions) != 0;
			StrictlySimple = (2 & InitOptions) != 0;
			base.PreserveCollinear = (4 & InitOptions) != 0;
		}

		private void InsertMaxima(long X)
		{
			global::UnityEngine.Rendering.Universal.Maxima maxima = new global::UnityEngine.Rendering.Universal.Maxima();
			maxima.X = X;
			if (m_Maxima == null)
			{
				m_Maxima = maxima;
				m_Maxima.Next = null;
				m_Maxima.Prev = null;
				return;
			}
			if (X < m_Maxima.X)
			{
				maxima.Next = m_Maxima;
				maxima.Prev = null;
				m_Maxima = maxima;
				return;
			}
			global::UnityEngine.Rendering.Universal.Maxima maxima2 = m_Maxima;
			while (maxima2.Next != null && X >= maxima2.Next.X)
			{
				maxima2 = maxima2.Next;
			}
			if (X != maxima2.X)
			{
				maxima.Next = maxima2.Next;
				maxima.Prev = maxima2;
				if (maxima2.Next != null)
				{
					maxima2.Next.Prev = maxima;
				}
				maxima2.Next = maxima;
			}
		}

		public bool Execute(global::UnityEngine.Rendering.Universal.ClipTypes clipType, global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>> solution, global::UnityEngine.Rendering.Universal.PolyFillTypes FillType = global::UnityEngine.Rendering.Universal.PolyFillTypes.pftEvenOdd)
		{
			return Execute(clipType, solution, FillType, FillType);
		}

		public bool Execute(global::UnityEngine.Rendering.Universal.ClipTypes clipType, global::UnityEngine.Rendering.Universal.PolyTree polytree, global::UnityEngine.Rendering.Universal.PolyFillTypes FillType = global::UnityEngine.Rendering.Universal.PolyFillTypes.pftEvenOdd)
		{
			return Execute(clipType, polytree, FillType, FillType);
		}

		public bool Execute(global::UnityEngine.Rendering.Universal.ClipTypes clipType, global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>> solution, global::UnityEngine.Rendering.Universal.PolyFillTypes subjFillType, global::UnityEngine.Rendering.Universal.PolyFillTypes clipFillType)
		{
			if (m_ExecuteLocked)
			{
				return false;
			}
			if (m_HasOpenPaths)
			{
				throw new global::UnityEngine.Rendering.Universal.ClipperException("Error: PolyTree struct is needed for open path clipping.");
			}
			m_ExecuteLocked = true;
			solution.Clear();
			m_SubjFillType = subjFillType;
			m_ClipFillType = clipFillType;
			m_ClipType = clipType;
			m_UsingPolyTree = false;
			bool flag;
			try
			{
				flag = ExecuteInternal();
				if (flag)
				{
					BuildResult(solution);
				}
			}
			finally
			{
				DisposeAllPolyPts();
				m_ExecuteLocked = false;
			}
			return flag;
		}

		public bool Execute(global::UnityEngine.Rendering.Universal.ClipTypes clipType, global::UnityEngine.Rendering.Universal.PolyTree polytree, global::UnityEngine.Rendering.Universal.PolyFillTypes subjFillType, global::UnityEngine.Rendering.Universal.PolyFillTypes clipFillType)
		{
			if (m_ExecuteLocked)
			{
				return false;
			}
			m_ExecuteLocked = true;
			m_SubjFillType = subjFillType;
			m_ClipFillType = clipFillType;
			m_ClipType = clipType;
			m_UsingPolyTree = true;
			bool flag;
			try
			{
				flag = ExecuteInternal();
				if (flag)
				{
					BuildResult2(polytree);
				}
			}
			finally
			{
				DisposeAllPolyPts();
				m_ExecuteLocked = false;
			}
			return flag;
		}

		internal void FixHoleLinkage(global::UnityEngine.Rendering.Universal.OutRec outRec)
		{
			if (outRec.FirstLeft != null && (outRec.IsHole == outRec.FirstLeft.IsHole || outRec.FirstLeft.Pts == null))
			{
				global::UnityEngine.Rendering.Universal.OutRec firstLeft = outRec.FirstLeft;
				while (firstLeft != null && (firstLeft.IsHole == outRec.IsHole || firstLeft.Pts == null))
				{
					firstLeft = firstLeft.FirstLeft;
				}
				outRec.FirstLeft = firstLeft;
			}
		}

		private bool ExecuteInternal()
		{
			try
			{
				Reset();
				m_SortedEdges = null;
				m_Maxima = null;
				if (!PopScanbeam(out var Y))
				{
					return false;
				}
				InsertLocalMinimaIntoAEL(Y);
				long Y2;
				while (PopScanbeam(out Y2) || LocalMinimaPending())
				{
					ProcessHorizontals();
					m_GhostJoins.Clear();
					if (!ProcessIntersections(Y2))
					{
						return false;
					}
					ProcessEdgesAtTopOfScanbeam(Y2);
					Y = Y2;
					InsertLocalMinimaIntoAEL(Y);
				}
				foreach (global::UnityEngine.Rendering.Universal.OutRec polyOut in m_PolyOuts)
				{
					if (polyOut.Pts != null && !polyOut.IsOpen && (polyOut.IsHole ^ ReverseSolution) == Area(polyOut) > 0.0)
					{
						ReversePolyPtLinks(polyOut.Pts);
					}
				}
				JoinCommonEdges();
				foreach (global::UnityEngine.Rendering.Universal.OutRec polyOut2 in m_PolyOuts)
				{
					if (polyOut2.Pts != null)
					{
						if (polyOut2.IsOpen)
						{
							FixupOutPolyline(polyOut2);
						}
						else
						{
							FixupOutPolygon(polyOut2);
						}
					}
				}
				if (StrictlySimple)
				{
					DoSimplePolygons();
				}
				return true;
			}
			finally
			{
				m_Joins.Clear();
				m_GhostJoins.Clear();
			}
		}

		private void DisposeAllPolyPts()
		{
			for (int i = 0; i < m_PolyOuts.Count; i++)
			{
				DisposeOutRec(i);
			}
			m_PolyOuts.Clear();
		}

		private void AddJoin(global::UnityEngine.Rendering.Universal.OutPt Op1, global::UnityEngine.Rendering.Universal.OutPt Op2, global::UnityEngine.Rendering.Universal.IntPoint OffPt)
		{
			global::UnityEngine.Rendering.Universal.Join obj = new global::UnityEngine.Rendering.Universal.Join();
			obj.OutPt1 = Op1;
			obj.OutPt2 = Op2;
			obj.OffPt = OffPt;
			m_Joins.Add(obj);
		}

		private void AddGhostJoin(global::UnityEngine.Rendering.Universal.OutPt Op, global::UnityEngine.Rendering.Universal.IntPoint OffPt)
		{
			global::UnityEngine.Rendering.Universal.Join obj = new global::UnityEngine.Rendering.Universal.Join();
			obj.OutPt1 = Op;
			obj.OffPt = OffPt;
			m_GhostJoins.Add(obj);
		}

		private void InsertLocalMinimaIntoAEL(long botY)
		{
			global::UnityEngine.Rendering.Universal.LocalMinima current;
			while (PopLocalMinima(botY, out current))
			{
				global::UnityEngine.Rendering.Universal.TEdge leftBound = current.LeftBound;
				global::UnityEngine.Rendering.Universal.TEdge rightBound = current.RightBound;
				global::UnityEngine.Rendering.Universal.OutPt outPt = null;
				if (leftBound == null)
				{
					InsertEdgeIntoAEL(rightBound, null);
					SetWindingCount(rightBound);
					if (IsContributing(rightBound))
					{
						outPt = AddOutPt(rightBound, rightBound.Bot);
					}
				}
				else if (rightBound == null)
				{
					InsertEdgeIntoAEL(leftBound, null);
					SetWindingCount(leftBound);
					if (IsContributing(leftBound))
					{
						outPt = AddOutPt(leftBound, leftBound.Bot);
					}
					InsertScanbeam(leftBound.Top.Y);
				}
				else
				{
					InsertEdgeIntoAEL(leftBound, null);
					InsertEdgeIntoAEL(rightBound, leftBound);
					SetWindingCount(leftBound);
					rightBound.WindCnt = leftBound.WindCnt;
					rightBound.WindCnt2 = leftBound.WindCnt2;
					if (IsContributing(leftBound))
					{
						outPt = AddLocalMinPoly(leftBound, rightBound, leftBound.Bot);
					}
					InsertScanbeam(leftBound.Top.Y);
				}
				if (rightBound != null)
				{
					if (global::UnityEngine.Rendering.Universal.ClipperBase.IsHorizontal(rightBound))
					{
						if (rightBound.NextInLML != null)
						{
							InsertScanbeam(rightBound.NextInLML.Top.Y);
						}
						AddEdgeToSEL(rightBound);
					}
					else
					{
						InsertScanbeam(rightBound.Top.Y);
					}
				}
				if (leftBound == null || rightBound == null)
				{
					continue;
				}
				if (outPt != null && global::UnityEngine.Rendering.Universal.ClipperBase.IsHorizontal(rightBound) && m_GhostJoins.Count > 0 && rightBound.WindDelta != 0)
				{
					for (int i = 0; i < m_GhostJoins.Count; i++)
					{
						global::UnityEngine.Rendering.Universal.Join obj = m_GhostJoins[i];
						if (HorzSegmentsOverlap(obj.OutPt1.Pt.X, obj.OffPt.X, rightBound.Bot.X, rightBound.Top.X))
						{
							AddJoin(obj.OutPt1, outPt, obj.OffPt);
						}
					}
				}
				if (leftBound.OutIdx >= 0 && leftBound.PrevInAEL != null && leftBound.PrevInAEL.Curr.X == leftBound.Bot.X && leftBound.PrevInAEL.OutIdx >= 0 && global::UnityEngine.Rendering.Universal.ClipperBase.SlopesEqual(leftBound.PrevInAEL.Curr, leftBound.PrevInAEL.Top, leftBound.Curr, leftBound.Top, m_UseFullRange) && leftBound.WindDelta != 0 && leftBound.PrevInAEL.WindDelta != 0)
				{
					global::UnityEngine.Rendering.Universal.OutPt op = AddOutPt(leftBound.PrevInAEL, leftBound.Bot);
					AddJoin(outPt, op, leftBound.Top);
				}
				if (leftBound.NextInAEL == rightBound)
				{
					continue;
				}
				if (rightBound.OutIdx >= 0 && rightBound.PrevInAEL.OutIdx >= 0 && global::UnityEngine.Rendering.Universal.ClipperBase.SlopesEqual(rightBound.PrevInAEL.Curr, rightBound.PrevInAEL.Top, rightBound.Curr, rightBound.Top, m_UseFullRange) && rightBound.WindDelta != 0 && rightBound.PrevInAEL.WindDelta != 0)
				{
					global::UnityEngine.Rendering.Universal.OutPt op2 = AddOutPt(rightBound.PrevInAEL, rightBound.Bot);
					AddJoin(outPt, op2, rightBound.Top);
				}
				global::UnityEngine.Rendering.Universal.TEdge nextInAEL = leftBound.NextInAEL;
				if (nextInAEL != null)
				{
					while (nextInAEL != rightBound)
					{
						IntersectEdges(rightBound, nextInAEL, leftBound.Curr);
						nextInAEL = nextInAEL.NextInAEL;
					}
				}
			}
		}

		private void InsertEdgeIntoAEL(global::UnityEngine.Rendering.Universal.TEdge edge, global::UnityEngine.Rendering.Universal.TEdge startEdge)
		{
			if (m_ActiveEdges == null)
			{
				edge.PrevInAEL = null;
				edge.NextInAEL = null;
				m_ActiveEdges = edge;
				return;
			}
			if (startEdge == null && E2InsertsBeforeE1(m_ActiveEdges, edge))
			{
				edge.PrevInAEL = null;
				edge.NextInAEL = m_ActiveEdges;
				m_ActiveEdges.PrevInAEL = edge;
				m_ActiveEdges = edge;
				return;
			}
			if (startEdge == null)
			{
				startEdge = m_ActiveEdges;
			}
			while (startEdge.NextInAEL != null && !E2InsertsBeforeE1(startEdge.NextInAEL, edge))
			{
				startEdge = startEdge.NextInAEL;
			}
			edge.NextInAEL = startEdge.NextInAEL;
			if (startEdge.NextInAEL != null)
			{
				startEdge.NextInAEL.PrevInAEL = edge;
			}
			edge.PrevInAEL = startEdge;
			startEdge.NextInAEL = edge;
		}

		private bool E2InsertsBeforeE1(global::UnityEngine.Rendering.Universal.TEdge e1, global::UnityEngine.Rendering.Universal.TEdge e2)
		{
			if (e2.Curr.X == e1.Curr.X)
			{
				if (e2.Top.Y > e1.Top.Y)
				{
					return e2.Top.X < TopX(e1, e2.Top.Y);
				}
				return e1.Top.X > TopX(e2, e1.Top.Y);
			}
			return e2.Curr.X < e1.Curr.X;
		}

		private bool IsEvenOddFillType(global::UnityEngine.Rendering.Universal.TEdge edge)
		{
			if (edge.PolyTyp == global::UnityEngine.Rendering.Universal.PolyTypes.ptSubject)
			{
				return m_SubjFillType == global::UnityEngine.Rendering.Universal.PolyFillTypes.pftEvenOdd;
			}
			return m_ClipFillType == global::UnityEngine.Rendering.Universal.PolyFillTypes.pftEvenOdd;
		}

		private bool IsEvenOddAltFillType(global::UnityEngine.Rendering.Universal.TEdge edge)
		{
			if (edge.PolyTyp == global::UnityEngine.Rendering.Universal.PolyTypes.ptSubject)
			{
				return m_ClipFillType == global::UnityEngine.Rendering.Universal.PolyFillTypes.pftEvenOdd;
			}
			return m_SubjFillType == global::UnityEngine.Rendering.Universal.PolyFillTypes.pftEvenOdd;
		}

		private bool IsContributing(global::UnityEngine.Rendering.Universal.TEdge edge)
		{
			global::UnityEngine.Rendering.Universal.PolyFillTypes polyFillTypes;
			global::UnityEngine.Rendering.Universal.PolyFillTypes polyFillTypes2;
			if (edge.PolyTyp == global::UnityEngine.Rendering.Universal.PolyTypes.ptSubject)
			{
				polyFillTypes = m_SubjFillType;
				polyFillTypes2 = m_ClipFillType;
			}
			else
			{
				polyFillTypes = m_ClipFillType;
				polyFillTypes2 = m_SubjFillType;
			}
			switch (polyFillTypes)
			{
			case global::UnityEngine.Rendering.Universal.PolyFillTypes.pftEvenOdd:
				if (edge.WindDelta == 0 && edge.WindCnt != 1)
				{
					return false;
				}
				break;
			case global::UnityEngine.Rendering.Universal.PolyFillTypes.pftNonZero:
				if (global::System.Math.Abs(edge.WindCnt) != 1)
				{
					return false;
				}
				break;
			case global::UnityEngine.Rendering.Universal.PolyFillTypes.pftPositive:
				if (edge.WindCnt != 1)
				{
					return false;
				}
				break;
			default:
				if (edge.WindCnt != -1)
				{
					return false;
				}
				break;
			}
			switch (m_ClipType)
			{
			case global::UnityEngine.Rendering.Universal.ClipTypes.ctIntersection:
				switch (polyFillTypes2)
				{
				case global::UnityEngine.Rendering.Universal.PolyFillTypes.pftEvenOdd:
				case global::UnityEngine.Rendering.Universal.PolyFillTypes.pftNonZero:
					return edge.WindCnt2 != 0;
				case global::UnityEngine.Rendering.Universal.PolyFillTypes.pftPositive:
					return edge.WindCnt2 > 0;
				default:
					return edge.WindCnt2 < 0;
				}
			case global::UnityEngine.Rendering.Universal.ClipTypes.ctUnion:
				switch (polyFillTypes2)
				{
				case global::UnityEngine.Rendering.Universal.PolyFillTypes.pftEvenOdd:
				case global::UnityEngine.Rendering.Universal.PolyFillTypes.pftNonZero:
					return edge.WindCnt2 == 0;
				case global::UnityEngine.Rendering.Universal.PolyFillTypes.pftPositive:
					return edge.WindCnt2 <= 0;
				default:
					return edge.WindCnt2 >= 0;
				}
			case global::UnityEngine.Rendering.Universal.ClipTypes.ctDifference:
				if (edge.PolyTyp == global::UnityEngine.Rendering.Universal.PolyTypes.ptSubject)
				{
					switch (polyFillTypes2)
					{
					case global::UnityEngine.Rendering.Universal.PolyFillTypes.pftEvenOdd:
					case global::UnityEngine.Rendering.Universal.PolyFillTypes.pftNonZero:
						return edge.WindCnt2 == 0;
					case global::UnityEngine.Rendering.Universal.PolyFillTypes.pftPositive:
						return edge.WindCnt2 <= 0;
					default:
						return edge.WindCnt2 >= 0;
					}
				}
				switch (polyFillTypes2)
				{
				case global::UnityEngine.Rendering.Universal.PolyFillTypes.pftEvenOdd:
				case global::UnityEngine.Rendering.Universal.PolyFillTypes.pftNonZero:
					return edge.WindCnt2 != 0;
				case global::UnityEngine.Rendering.Universal.PolyFillTypes.pftPositive:
					return edge.WindCnt2 > 0;
				default:
					return edge.WindCnt2 < 0;
				}
			case global::UnityEngine.Rendering.Universal.ClipTypes.ctXor:
				if (edge.WindDelta == 0)
				{
					switch (polyFillTypes2)
					{
					case global::UnityEngine.Rendering.Universal.PolyFillTypes.pftEvenOdd:
					case global::UnityEngine.Rendering.Universal.PolyFillTypes.pftNonZero:
						return edge.WindCnt2 == 0;
					case global::UnityEngine.Rendering.Universal.PolyFillTypes.pftPositive:
						return edge.WindCnt2 <= 0;
					default:
						return edge.WindCnt2 >= 0;
					}
				}
				return true;
			default:
				return true;
			}
		}

		private void SetWindingCount(global::UnityEngine.Rendering.Universal.TEdge edge)
		{
			global::UnityEngine.Rendering.Universal.TEdge prevInAEL = edge.PrevInAEL;
			while (prevInAEL != null && (prevInAEL.PolyTyp != edge.PolyTyp || prevInAEL.WindDelta == 0))
			{
				prevInAEL = prevInAEL.PrevInAEL;
			}
			if (prevInAEL == null)
			{
				global::UnityEngine.Rendering.Universal.PolyFillTypes polyFillTypes = ((edge.PolyTyp == global::UnityEngine.Rendering.Universal.PolyTypes.ptSubject) ? m_SubjFillType : m_ClipFillType);
				if (edge.WindDelta == 0)
				{
					edge.WindCnt = ((polyFillTypes != global::UnityEngine.Rendering.Universal.PolyFillTypes.pftNegative) ? 1 : (-1));
				}
				else
				{
					edge.WindCnt = edge.WindDelta;
				}
				edge.WindCnt2 = 0;
				prevInAEL = m_ActiveEdges;
			}
			else if (edge.WindDelta == 0 && m_ClipType != global::UnityEngine.Rendering.Universal.ClipTypes.ctUnion)
			{
				edge.WindCnt = 1;
				edge.WindCnt2 = prevInAEL.WindCnt2;
				prevInAEL = prevInAEL.NextInAEL;
			}
			else if (IsEvenOddFillType(edge))
			{
				if (edge.WindDelta == 0)
				{
					bool flag = true;
					for (global::UnityEngine.Rendering.Universal.TEdge prevInAEL2 = prevInAEL.PrevInAEL; prevInAEL2 != null; prevInAEL2 = prevInAEL2.PrevInAEL)
					{
						if (prevInAEL2.PolyTyp == prevInAEL.PolyTyp && prevInAEL2.WindDelta != 0)
						{
							flag = !flag;
						}
					}
					edge.WindCnt = ((!flag) ? 1 : 0);
				}
				else
				{
					edge.WindCnt = edge.WindDelta;
				}
				edge.WindCnt2 = prevInAEL.WindCnt2;
				prevInAEL = prevInAEL.NextInAEL;
			}
			else
			{
				if (prevInAEL.WindCnt * prevInAEL.WindDelta < 0)
				{
					if (global::System.Math.Abs(prevInAEL.WindCnt) > 1)
					{
						if (prevInAEL.WindDelta * edge.WindDelta < 0)
						{
							edge.WindCnt = prevInAEL.WindCnt;
						}
						else
						{
							edge.WindCnt = prevInAEL.WindCnt + edge.WindDelta;
						}
					}
					else
					{
						edge.WindCnt = ((edge.WindDelta == 0) ? 1 : edge.WindDelta);
					}
				}
				else if (edge.WindDelta == 0)
				{
					edge.WindCnt = ((prevInAEL.WindCnt < 0) ? (prevInAEL.WindCnt - 1) : (prevInAEL.WindCnt + 1));
				}
				else if (prevInAEL.WindDelta * edge.WindDelta < 0)
				{
					edge.WindCnt = prevInAEL.WindCnt;
				}
				else
				{
					edge.WindCnt = prevInAEL.WindCnt + edge.WindDelta;
				}
				edge.WindCnt2 = prevInAEL.WindCnt2;
				prevInAEL = prevInAEL.NextInAEL;
			}
			if (IsEvenOddAltFillType(edge))
			{
				while (prevInAEL != edge)
				{
					if (prevInAEL.WindDelta != 0)
					{
						edge.WindCnt2 = ((edge.WindCnt2 == 0) ? 1 : 0);
					}
					prevInAEL = prevInAEL.NextInAEL;
				}
			}
			else
			{
				while (prevInAEL != edge)
				{
					edge.WindCnt2 += prevInAEL.WindDelta;
					prevInAEL = prevInAEL.NextInAEL;
				}
			}
		}

		private void AddEdgeToSEL(global::UnityEngine.Rendering.Universal.TEdge edge)
		{
			if (m_SortedEdges == null)
			{
				m_SortedEdges = edge;
				edge.PrevInSEL = null;
				edge.NextInSEL = null;
			}
			else
			{
				edge.NextInSEL = m_SortedEdges;
				edge.PrevInSEL = null;
				m_SortedEdges.PrevInSEL = edge;
				m_SortedEdges = edge;
			}
		}

		internal bool PopEdgeFromSEL(out global::UnityEngine.Rendering.Universal.TEdge e)
		{
			e = m_SortedEdges;
			if (e == null)
			{
				return false;
			}
			global::UnityEngine.Rendering.Universal.TEdge obj = e;
			m_SortedEdges = e.NextInSEL;
			if (m_SortedEdges != null)
			{
				m_SortedEdges.PrevInSEL = null;
			}
			obj.NextInSEL = null;
			obj.PrevInSEL = null;
			return true;
		}

		private void CopyAELToSEL()
		{
			for (global::UnityEngine.Rendering.Universal.TEdge tEdge = (m_SortedEdges = m_ActiveEdges); tEdge != null; tEdge = tEdge.NextInAEL)
			{
				tEdge.PrevInSEL = tEdge.PrevInAEL;
				tEdge.NextInSEL = tEdge.NextInAEL;
			}
		}

		private void SwapPositionsInSEL(global::UnityEngine.Rendering.Universal.TEdge edge1, global::UnityEngine.Rendering.Universal.TEdge edge2)
		{
			if ((edge1.NextInSEL == null && edge1.PrevInSEL == null) || (edge2.NextInSEL == null && edge2.PrevInSEL == null))
			{
				return;
			}
			if (edge1.NextInSEL == edge2)
			{
				global::UnityEngine.Rendering.Universal.TEdge nextInSEL = edge2.NextInSEL;
				if (nextInSEL != null)
				{
					nextInSEL.PrevInSEL = edge1;
				}
				global::UnityEngine.Rendering.Universal.TEdge prevInSEL = edge1.PrevInSEL;
				if (prevInSEL != null)
				{
					prevInSEL.NextInSEL = edge2;
				}
				edge2.PrevInSEL = prevInSEL;
				edge2.NextInSEL = edge1;
				edge1.PrevInSEL = edge2;
				edge1.NextInSEL = nextInSEL;
			}
			else if (edge2.NextInSEL == edge1)
			{
				global::UnityEngine.Rendering.Universal.TEdge nextInSEL2 = edge1.NextInSEL;
				if (nextInSEL2 != null)
				{
					nextInSEL2.PrevInSEL = edge2;
				}
				global::UnityEngine.Rendering.Universal.TEdge prevInSEL2 = edge2.PrevInSEL;
				if (prevInSEL2 != null)
				{
					prevInSEL2.NextInSEL = edge1;
				}
				edge1.PrevInSEL = prevInSEL2;
				edge1.NextInSEL = edge2;
				edge2.PrevInSEL = edge1;
				edge2.NextInSEL = nextInSEL2;
			}
			else
			{
				global::UnityEngine.Rendering.Universal.TEdge nextInSEL3 = edge1.NextInSEL;
				global::UnityEngine.Rendering.Universal.TEdge prevInSEL3 = edge1.PrevInSEL;
				edge1.NextInSEL = edge2.NextInSEL;
				if (edge1.NextInSEL != null)
				{
					edge1.NextInSEL.PrevInSEL = edge1;
				}
				edge1.PrevInSEL = edge2.PrevInSEL;
				if (edge1.PrevInSEL != null)
				{
					edge1.PrevInSEL.NextInSEL = edge1;
				}
				edge2.NextInSEL = nextInSEL3;
				if (edge2.NextInSEL != null)
				{
					edge2.NextInSEL.PrevInSEL = edge2;
				}
				edge2.PrevInSEL = prevInSEL3;
				if (edge2.PrevInSEL != null)
				{
					edge2.PrevInSEL.NextInSEL = edge2;
				}
			}
			if (edge1.PrevInSEL == null)
			{
				m_SortedEdges = edge1;
			}
			else if (edge2.PrevInSEL == null)
			{
				m_SortedEdges = edge2;
			}
		}

		private void AddLocalMaxPoly(global::UnityEngine.Rendering.Universal.TEdge e1, global::UnityEngine.Rendering.Universal.TEdge e2, global::UnityEngine.Rendering.Universal.IntPoint pt)
		{
			AddOutPt(e1, pt);
			if (e2.WindDelta == 0)
			{
				AddOutPt(e2, pt);
			}
			if (e1.OutIdx == e2.OutIdx)
			{
				e1.OutIdx = -1;
				e2.OutIdx = -1;
			}
			else if (e1.OutIdx < e2.OutIdx)
			{
				AppendPolygon(e1, e2);
			}
			else
			{
				AppendPolygon(e2, e1);
			}
		}

		private global::UnityEngine.Rendering.Universal.OutPt AddLocalMinPoly(global::UnityEngine.Rendering.Universal.TEdge e1, global::UnityEngine.Rendering.Universal.TEdge e2, global::UnityEngine.Rendering.Universal.IntPoint pt)
		{
			global::UnityEngine.Rendering.Universal.OutPt outPt;
			global::UnityEngine.Rendering.Universal.TEdge tEdge;
			global::UnityEngine.Rendering.Universal.TEdge tEdge2;
			if (global::UnityEngine.Rendering.Universal.ClipperBase.IsHorizontal(e2) || e1.Dx > e2.Dx)
			{
				outPt = AddOutPt(e1, pt);
				e2.OutIdx = e1.OutIdx;
				e1.Side = global::UnityEngine.Rendering.Universal.EdgeSides.esLeft;
				e2.Side = global::UnityEngine.Rendering.Universal.EdgeSides.esRight;
				tEdge = e1;
				tEdge2 = ((tEdge.PrevInAEL != e2) ? tEdge.PrevInAEL : e2.PrevInAEL);
			}
			else
			{
				outPt = AddOutPt(e2, pt);
				e1.OutIdx = e2.OutIdx;
				e1.Side = global::UnityEngine.Rendering.Universal.EdgeSides.esRight;
				e2.Side = global::UnityEngine.Rendering.Universal.EdgeSides.esLeft;
				tEdge = e2;
				tEdge2 = ((tEdge.PrevInAEL != e1) ? tEdge.PrevInAEL : e1.PrevInAEL);
			}
			if (tEdge2 != null && tEdge2.OutIdx >= 0 && tEdge2.Top.Y < pt.Y && tEdge.Top.Y < pt.Y)
			{
				long num = TopX(tEdge2, pt.Y);
				long num2 = TopX(tEdge, pt.Y);
				if (num == num2 && tEdge.WindDelta != 0 && tEdge2.WindDelta != 0 && global::UnityEngine.Rendering.Universal.ClipperBase.SlopesEqual(new global::UnityEngine.Rendering.Universal.IntPoint(num, pt.Y), tEdge2.Top, new global::UnityEngine.Rendering.Universal.IntPoint(num2, pt.Y), tEdge.Top, m_UseFullRange))
				{
					global::UnityEngine.Rendering.Universal.OutPt op = AddOutPt(tEdge2, pt);
					AddJoin(outPt, op, tEdge.Top);
				}
			}
			return outPt;
		}

		private global::UnityEngine.Rendering.Universal.OutPt AddOutPt(global::UnityEngine.Rendering.Universal.TEdge e, global::UnityEngine.Rendering.Universal.IntPoint pt)
		{
			if (e.OutIdx < 0)
			{
				global::UnityEngine.Rendering.Universal.OutRec outRec = CreateOutRec();
				outRec.IsOpen = e.WindDelta == 0;
				global::UnityEngine.Rendering.Universal.OutPt outPt = (outRec.Pts = new global::UnityEngine.Rendering.Universal.OutPt());
				outPt.Idx = outRec.Idx;
				outPt.Pt = pt;
				outPt.Next = outPt;
				outPt.Prev = outPt;
				if (!outRec.IsOpen)
				{
					SetHoleState(e, outRec);
				}
				e.OutIdx = outRec.Idx;
				return outPt;
			}
			global::UnityEngine.Rendering.Universal.OutRec outRec2 = m_PolyOuts[e.OutIdx];
			global::UnityEngine.Rendering.Universal.OutPt pts = outRec2.Pts;
			bool flag = e.Side == global::UnityEngine.Rendering.Universal.EdgeSides.esLeft;
			if (flag && pt == pts.Pt)
			{
				return pts;
			}
			if (!flag && pt == pts.Prev.Pt)
			{
				return pts.Prev;
			}
			global::UnityEngine.Rendering.Universal.OutPt outPt2 = new global::UnityEngine.Rendering.Universal.OutPt();
			outPt2.Idx = outRec2.Idx;
			outPt2.Pt = pt;
			outPt2.Next = pts;
			outPt2.Prev = pts.Prev;
			outPt2.Prev.Next = outPt2;
			pts.Prev = outPt2;
			if (flag)
			{
				outRec2.Pts = outPt2;
			}
			return outPt2;
		}

		private global::UnityEngine.Rendering.Universal.OutPt GetLastOutPt(global::UnityEngine.Rendering.Universal.TEdge e)
		{
			global::UnityEngine.Rendering.Universal.OutRec outRec = m_PolyOuts[e.OutIdx];
			if (e.Side == global::UnityEngine.Rendering.Universal.EdgeSides.esLeft)
			{
				return outRec.Pts;
			}
			return outRec.Pts.Prev;
		}

		internal void SwapPoints(ref global::UnityEngine.Rendering.Universal.IntPoint pt1, ref global::UnityEngine.Rendering.Universal.IntPoint pt2)
		{
			global::UnityEngine.Rendering.Universal.IntPoint intPoint = new global::UnityEngine.Rendering.Universal.IntPoint(pt1);
			pt1 = pt2;
			pt2 = intPoint;
		}

		private bool HorzSegmentsOverlap(long seg1a, long seg1b, long seg2a, long seg2b)
		{
			if (seg1a > seg1b)
			{
				Swap(ref seg1a, ref seg1b);
			}
			if (seg2a > seg2b)
			{
				Swap(ref seg2a, ref seg2b);
			}
			if (seg1a < seg2b)
			{
				return seg2a < seg1b;
			}
			return false;
		}

		private void SetHoleState(global::UnityEngine.Rendering.Universal.TEdge e, global::UnityEngine.Rendering.Universal.OutRec outRec)
		{
			global::UnityEngine.Rendering.Universal.TEdge prevInAEL = e.PrevInAEL;
			global::UnityEngine.Rendering.Universal.TEdge tEdge = null;
			while (prevInAEL != null)
			{
				if (prevInAEL.OutIdx >= 0 && prevInAEL.WindDelta != 0)
				{
					if (tEdge == null)
					{
						tEdge = prevInAEL;
					}
					else if (tEdge.OutIdx == prevInAEL.OutIdx)
					{
						tEdge = null;
					}
				}
				prevInAEL = prevInAEL.PrevInAEL;
			}
			if (tEdge == null)
			{
				outRec.FirstLeft = null;
				outRec.IsHole = false;
			}
			else
			{
				outRec.FirstLeft = m_PolyOuts[tEdge.OutIdx];
				outRec.IsHole = !outRec.FirstLeft.IsHole;
			}
		}

		private double GetDx(global::UnityEngine.Rendering.Universal.IntPoint pt1, global::UnityEngine.Rendering.Universal.IntPoint pt2)
		{
			if (pt1.Y == pt2.Y)
			{
				return -3.4E+38;
			}
			return (double)(pt2.X - pt1.X) / (double)(pt2.Y - pt1.Y);
		}

		private bool FirstIsBottomPt(global::UnityEngine.Rendering.Universal.OutPt btmPt1, global::UnityEngine.Rendering.Universal.OutPt btmPt2)
		{
			global::UnityEngine.Rendering.Universal.OutPt prev = btmPt1.Prev;
			while (prev.Pt == btmPt1.Pt && prev != btmPt1)
			{
				prev = prev.Prev;
			}
			double num = global::System.Math.Abs(GetDx(btmPt1.Pt, prev.Pt));
			prev = btmPt1.Next;
			while (prev.Pt == btmPt1.Pt && prev != btmPt1)
			{
				prev = prev.Next;
			}
			double num2 = global::System.Math.Abs(GetDx(btmPt1.Pt, prev.Pt));
			prev = btmPt2.Prev;
			while (prev.Pt == btmPt2.Pt && prev != btmPt2)
			{
				prev = prev.Prev;
			}
			double num3 = global::System.Math.Abs(GetDx(btmPt2.Pt, prev.Pt));
			prev = btmPt2.Next;
			while (prev.Pt == btmPt2.Pt && prev != btmPt2)
			{
				prev = prev.Next;
			}
			double num4 = global::System.Math.Abs(GetDx(btmPt2.Pt, prev.Pt));
			if (global::System.Math.Max(num, num2) == global::System.Math.Max(num3, num4) && global::System.Math.Min(num, num2) == global::System.Math.Min(num3, num4))
			{
				return Area(btmPt1) > 0.0;
			}
			if (!(num >= num3) || !(num >= num4))
			{
				if (num2 >= num3)
				{
					return num2 >= num4;
				}
				return false;
			}
			return true;
		}

		private global::UnityEngine.Rendering.Universal.OutPt GetBottomPt(global::UnityEngine.Rendering.Universal.OutPt pp)
		{
			global::UnityEngine.Rendering.Universal.OutPt outPt = null;
			global::UnityEngine.Rendering.Universal.OutPt next;
			for (next = pp.Next; next != pp; next = next.Next)
			{
				if (next.Pt.Y > pp.Pt.Y)
				{
					pp = next;
					outPt = null;
				}
				else if (next.Pt.Y == pp.Pt.Y && next.Pt.X <= pp.Pt.X)
				{
					if (next.Pt.X < pp.Pt.X)
					{
						outPt = null;
						pp = next;
					}
					else if (next.Next != pp && next.Prev != pp)
					{
						outPt = next;
					}
				}
			}
			if (outPt != null)
			{
				while (outPt != next)
				{
					if (!FirstIsBottomPt(next, outPt))
					{
						pp = outPt;
					}
					outPt = outPt.Next;
					while (outPt.Pt != pp.Pt)
					{
						outPt = outPt.Next;
					}
				}
			}
			return pp;
		}

		private global::UnityEngine.Rendering.Universal.OutRec GetLowermostRec(global::UnityEngine.Rendering.Universal.OutRec outRec1, global::UnityEngine.Rendering.Universal.OutRec outRec2)
		{
			if (outRec1.BottomPt == null)
			{
				outRec1.BottomPt = GetBottomPt(outRec1.Pts);
			}
			if (outRec2.BottomPt == null)
			{
				outRec2.BottomPt = GetBottomPt(outRec2.Pts);
			}
			global::UnityEngine.Rendering.Universal.OutPt bottomPt = outRec1.BottomPt;
			global::UnityEngine.Rendering.Universal.OutPt bottomPt2 = outRec2.BottomPt;
			if (bottomPt.Pt.Y > bottomPt2.Pt.Y)
			{
				return outRec1;
			}
			if (bottomPt.Pt.Y < bottomPt2.Pt.Y)
			{
				return outRec2;
			}
			if (bottomPt.Pt.X < bottomPt2.Pt.X)
			{
				return outRec1;
			}
			if (bottomPt.Pt.X > bottomPt2.Pt.X)
			{
				return outRec2;
			}
			if (bottomPt.Next == bottomPt)
			{
				return outRec2;
			}
			if (bottomPt2.Next == bottomPt2)
			{
				return outRec1;
			}
			if (FirstIsBottomPt(bottomPt, bottomPt2))
			{
				return outRec1;
			}
			return outRec2;
		}

		private bool OutRec1RightOfOutRec2(global::UnityEngine.Rendering.Universal.OutRec outRec1, global::UnityEngine.Rendering.Universal.OutRec outRec2)
		{
			do
			{
				outRec1 = outRec1.FirstLeft;
				if (outRec1 == outRec2)
				{
					return true;
				}
			}
			while (outRec1 != null);
			return false;
		}

		private global::UnityEngine.Rendering.Universal.OutRec GetOutRec(int idx)
		{
			global::UnityEngine.Rendering.Universal.OutRec outRec;
			for (outRec = m_PolyOuts[idx]; outRec != m_PolyOuts[outRec.Idx]; outRec = m_PolyOuts[outRec.Idx])
			{
			}
			return outRec;
		}

		private void AppendPolygon(global::UnityEngine.Rendering.Universal.TEdge e1, global::UnityEngine.Rendering.Universal.TEdge e2)
		{
			global::UnityEngine.Rendering.Universal.OutRec outRec = m_PolyOuts[e1.OutIdx];
			global::UnityEngine.Rendering.Universal.OutRec outRec2 = m_PolyOuts[e2.OutIdx];
			global::UnityEngine.Rendering.Universal.OutRec outRec3 = (OutRec1RightOfOutRec2(outRec, outRec2) ? outRec2 : ((!OutRec1RightOfOutRec2(outRec2, outRec)) ? GetLowermostRec(outRec, outRec2) : outRec));
			global::UnityEngine.Rendering.Universal.OutPt pts = outRec.Pts;
			global::UnityEngine.Rendering.Universal.OutPt prev = pts.Prev;
			global::UnityEngine.Rendering.Universal.OutPt pts2 = outRec2.Pts;
			global::UnityEngine.Rendering.Universal.OutPt prev2 = pts2.Prev;
			if (e1.Side == global::UnityEngine.Rendering.Universal.EdgeSides.esLeft)
			{
				if (e2.Side == global::UnityEngine.Rendering.Universal.EdgeSides.esLeft)
				{
					ReversePolyPtLinks(pts2);
					pts2.Next = pts;
					pts.Prev = pts2;
					prev.Next = prev2;
					prev2.Prev = prev;
					outRec.Pts = prev2;
				}
				else
				{
					prev2.Next = pts;
					pts.Prev = prev2;
					pts2.Prev = prev;
					prev.Next = pts2;
					outRec.Pts = pts2;
				}
			}
			else if (e2.Side == global::UnityEngine.Rendering.Universal.EdgeSides.esRight)
			{
				ReversePolyPtLinks(pts2);
				prev.Next = prev2;
				prev2.Prev = prev;
				pts2.Next = pts;
				pts.Prev = pts2;
			}
			else
			{
				prev.Next = pts2;
				pts2.Prev = prev;
				pts.Prev = prev2;
				prev2.Next = pts;
			}
			outRec.BottomPt = null;
			if (outRec3 == outRec2)
			{
				if (outRec2.FirstLeft != outRec)
				{
					outRec.FirstLeft = outRec2.FirstLeft;
				}
				outRec.IsHole = outRec2.IsHole;
			}
			outRec2.Pts = null;
			outRec2.BottomPt = null;
			outRec2.FirstLeft = outRec;
			int outIdx = e1.OutIdx;
			int outIdx2 = e2.OutIdx;
			e1.OutIdx = -1;
			e2.OutIdx = -1;
			for (global::UnityEngine.Rendering.Universal.TEdge tEdge = m_ActiveEdges; tEdge != null; tEdge = tEdge.NextInAEL)
			{
				if (tEdge.OutIdx == outIdx2)
				{
					tEdge.OutIdx = outIdx;
					tEdge.Side = e1.Side;
					break;
				}
			}
			outRec2.Idx = outRec.Idx;
		}

		private void ReversePolyPtLinks(global::UnityEngine.Rendering.Universal.OutPt pp)
		{
			if (pp != null)
			{
				global::UnityEngine.Rendering.Universal.OutPt outPt = pp;
				do
				{
					global::UnityEngine.Rendering.Universal.OutPt next = outPt.Next;
					outPt.Next = outPt.Prev;
					outPt.Prev = next;
					outPt = next;
				}
				while (outPt != pp);
			}
		}

		private static void SwapSides(global::UnityEngine.Rendering.Universal.TEdge edge1, global::UnityEngine.Rendering.Universal.TEdge edge2)
		{
			global::UnityEngine.Rendering.Universal.EdgeSides side = edge1.Side;
			edge1.Side = edge2.Side;
			edge2.Side = side;
		}

		private static void SwapPolyIndexes(global::UnityEngine.Rendering.Universal.TEdge edge1, global::UnityEngine.Rendering.Universal.TEdge edge2)
		{
			int outIdx = edge1.OutIdx;
			edge1.OutIdx = edge2.OutIdx;
			edge2.OutIdx = outIdx;
		}

		private void IntersectEdges(global::UnityEngine.Rendering.Universal.TEdge e1, global::UnityEngine.Rendering.Universal.TEdge e2, global::UnityEngine.Rendering.Universal.IntPoint pt)
		{
			bool flag = e1.OutIdx >= 0;
			bool flag2 = e2.OutIdx >= 0;
			if (e1.WindDelta == 0 || e2.WindDelta == 0)
			{
				if (e1.WindDelta == 0 && e2.WindDelta == 0)
				{
					return;
				}
				if (e1.PolyTyp == e2.PolyTyp && e1.WindDelta != e2.WindDelta && m_ClipType == global::UnityEngine.Rendering.Universal.ClipTypes.ctUnion)
				{
					if (e1.WindDelta == 0)
					{
						if (flag2)
						{
							AddOutPt(e1, pt);
							if (flag)
							{
								e1.OutIdx = -1;
							}
						}
					}
					else if (flag)
					{
						AddOutPt(e2, pt);
						if (flag2)
						{
							e2.OutIdx = -1;
						}
					}
				}
				else
				{
					if (e1.PolyTyp == e2.PolyTyp)
					{
						return;
					}
					if (e1.WindDelta == 0 && global::System.Math.Abs(e2.WindCnt) == 1 && (m_ClipType != global::UnityEngine.Rendering.Universal.ClipTypes.ctUnion || e2.WindCnt2 == 0))
					{
						AddOutPt(e1, pt);
						if (flag)
						{
							e1.OutIdx = -1;
						}
					}
					else if (e2.WindDelta == 0 && global::System.Math.Abs(e1.WindCnt) == 1 && (m_ClipType != global::UnityEngine.Rendering.Universal.ClipTypes.ctUnion || e1.WindCnt2 == 0))
					{
						AddOutPt(e2, pt);
						if (flag2)
						{
							e2.OutIdx = -1;
						}
					}
				}
				return;
			}
			if (e1.PolyTyp == e2.PolyTyp)
			{
				if (IsEvenOddFillType(e1))
				{
					int windCnt = e1.WindCnt;
					e1.WindCnt = e2.WindCnt;
					e2.WindCnt = windCnt;
				}
				else
				{
					if (e1.WindCnt + e2.WindDelta == 0)
					{
						e1.WindCnt = -e1.WindCnt;
					}
					else
					{
						e1.WindCnt += e2.WindDelta;
					}
					if (e2.WindCnt - e1.WindDelta == 0)
					{
						e2.WindCnt = -e2.WindCnt;
					}
					else
					{
						e2.WindCnt -= e1.WindDelta;
					}
				}
			}
			else
			{
				if (!IsEvenOddFillType(e2))
				{
					e1.WindCnt2 += e2.WindDelta;
				}
				else
				{
					e1.WindCnt2 = ((e1.WindCnt2 == 0) ? 1 : 0);
				}
				if (!IsEvenOddFillType(e1))
				{
					e2.WindCnt2 -= e1.WindDelta;
				}
				else
				{
					e2.WindCnt2 = ((e2.WindCnt2 == 0) ? 1 : 0);
				}
			}
			global::UnityEngine.Rendering.Universal.PolyFillTypes polyFillTypes;
			global::UnityEngine.Rendering.Universal.PolyFillTypes polyFillTypes2;
			if (e1.PolyTyp == global::UnityEngine.Rendering.Universal.PolyTypes.ptSubject)
			{
				polyFillTypes = m_SubjFillType;
				polyFillTypes2 = m_ClipFillType;
			}
			else
			{
				polyFillTypes = m_ClipFillType;
				polyFillTypes2 = m_SubjFillType;
			}
			global::UnityEngine.Rendering.Universal.PolyFillTypes polyFillTypes3;
			global::UnityEngine.Rendering.Universal.PolyFillTypes polyFillTypes4;
			if (e2.PolyTyp == global::UnityEngine.Rendering.Universal.PolyTypes.ptSubject)
			{
				polyFillTypes3 = m_SubjFillType;
				polyFillTypes4 = m_ClipFillType;
			}
			else
			{
				polyFillTypes3 = m_ClipFillType;
				polyFillTypes4 = m_SubjFillType;
			}
			int num = polyFillTypes switch
			{
				global::UnityEngine.Rendering.Universal.PolyFillTypes.pftPositive => e1.WindCnt, 
				global::UnityEngine.Rendering.Universal.PolyFillTypes.pftNegative => -e1.WindCnt, 
				_ => global::System.Math.Abs(e1.WindCnt), 
			};
			int num2 = polyFillTypes3 switch
			{
				global::UnityEngine.Rendering.Universal.PolyFillTypes.pftPositive => e2.WindCnt, 
				global::UnityEngine.Rendering.Universal.PolyFillTypes.pftNegative => -e2.WindCnt, 
				_ => global::System.Math.Abs(e2.WindCnt), 
			};
			if (flag && flag2)
			{
				if ((num != 0 && num != 1) || (num2 != 0 && num2 != 1) || (e1.PolyTyp != e2.PolyTyp && m_ClipType != global::UnityEngine.Rendering.Universal.ClipTypes.ctXor))
				{
					AddLocalMaxPoly(e1, e2, pt);
					return;
				}
				AddOutPt(e1, pt);
				AddOutPt(e2, pt);
				SwapSides(e1, e2);
				SwapPolyIndexes(e1, e2);
			}
			else if (flag)
			{
				if (num2 == 0 || num2 == 1)
				{
					AddOutPt(e1, pt);
					SwapSides(e1, e2);
					SwapPolyIndexes(e1, e2);
				}
			}
			else if (flag2)
			{
				if (num == 0 || num == 1)
				{
					AddOutPt(e2, pt);
					SwapSides(e1, e2);
					SwapPolyIndexes(e1, e2);
				}
			}
			else
			{
				if ((num != 0 && num != 1) || (num2 != 0 && num2 != 1))
				{
					return;
				}
				long num3 = polyFillTypes2 switch
				{
					global::UnityEngine.Rendering.Universal.PolyFillTypes.pftPositive => e1.WindCnt2, 
					global::UnityEngine.Rendering.Universal.PolyFillTypes.pftNegative => -e1.WindCnt2, 
					_ => global::System.Math.Abs(e1.WindCnt2), 
				};
				long num4 = polyFillTypes4 switch
				{
					global::UnityEngine.Rendering.Universal.PolyFillTypes.pftPositive => e2.WindCnt2, 
					global::UnityEngine.Rendering.Universal.PolyFillTypes.pftNegative => -e2.WindCnt2, 
					_ => global::System.Math.Abs(e2.WindCnt2), 
				};
				if (e1.PolyTyp != e2.PolyTyp)
				{
					AddLocalMinPoly(e1, e2, pt);
				}
				else if (num == 1 && num2 == 1)
				{
					switch (m_ClipType)
					{
					case global::UnityEngine.Rendering.Universal.ClipTypes.ctIntersection:
						if (num3 > 0 && num4 > 0)
						{
							AddLocalMinPoly(e1, e2, pt);
						}
						break;
					case global::UnityEngine.Rendering.Universal.ClipTypes.ctUnion:
						if (num3 <= 0 && num4 <= 0)
						{
							AddLocalMinPoly(e1, e2, pt);
						}
						break;
					case global::UnityEngine.Rendering.Universal.ClipTypes.ctDifference:
						if ((e1.PolyTyp == global::UnityEngine.Rendering.Universal.PolyTypes.ptClip && num3 > 0 && num4 > 0) || (e1.PolyTyp == global::UnityEngine.Rendering.Universal.PolyTypes.ptSubject && num3 <= 0 && num4 <= 0))
						{
							AddLocalMinPoly(e1, e2, pt);
						}
						break;
					case global::UnityEngine.Rendering.Universal.ClipTypes.ctXor:
						AddLocalMinPoly(e1, e2, pt);
						break;
					}
				}
				else
				{
					SwapSides(e1, e2);
				}
			}
		}

		private void DeleteFromSEL(global::UnityEngine.Rendering.Universal.TEdge e)
		{
			global::UnityEngine.Rendering.Universal.TEdge prevInSEL = e.PrevInSEL;
			global::UnityEngine.Rendering.Universal.TEdge nextInSEL = e.NextInSEL;
			if (prevInSEL != null || nextInSEL != null || e == m_SortedEdges)
			{
				if (prevInSEL != null)
				{
					prevInSEL.NextInSEL = nextInSEL;
				}
				else
				{
					m_SortedEdges = nextInSEL;
				}
				if (nextInSEL != null)
				{
					nextInSEL.PrevInSEL = prevInSEL;
				}
				e.NextInSEL = null;
				e.PrevInSEL = null;
			}
		}

		private void ProcessHorizontals()
		{
			global::UnityEngine.Rendering.Universal.TEdge e;
			while (PopEdgeFromSEL(out e))
			{
				ProcessHorizontal(e);
			}
		}

		private void GetHorzDirection(global::UnityEngine.Rendering.Universal.TEdge HorzEdge, out global::UnityEngine.Rendering.Universal.Directions Dir, out long Left, out long Right)
		{
			if (HorzEdge.Bot.X < HorzEdge.Top.X)
			{
				Left = HorzEdge.Bot.X;
				Right = HorzEdge.Top.X;
				Dir = global::UnityEngine.Rendering.Universal.Directions.dLeftToRight;
			}
			else
			{
				Left = HorzEdge.Top.X;
				Right = HorzEdge.Bot.X;
				Dir = global::UnityEngine.Rendering.Universal.Directions.dRightToLeft;
			}
		}

		private void ProcessHorizontal(global::UnityEngine.Rendering.Universal.TEdge horzEdge)
		{
			bool flag = horzEdge.WindDelta == 0;
			GetHorzDirection(horzEdge, out var Dir, out var Left, out var Right);
			global::UnityEngine.Rendering.Universal.TEdge tEdge = horzEdge;
			global::UnityEngine.Rendering.Universal.TEdge tEdge2 = null;
			while (tEdge.NextInLML != null && global::UnityEngine.Rendering.Universal.ClipperBase.IsHorizontal(tEdge.NextInLML))
			{
				tEdge = tEdge.NextInLML;
			}
			if (tEdge.NextInLML == null)
			{
				tEdge2 = GetMaximaPair(tEdge);
			}
			global::UnityEngine.Rendering.Universal.Maxima maxima = m_Maxima;
			if (maxima != null)
			{
				if (Dir == global::UnityEngine.Rendering.Universal.Directions.dLeftToRight)
				{
					while (maxima != null && maxima.X <= horzEdge.Bot.X)
					{
						maxima = maxima.Next;
					}
					if (maxima != null && maxima.X >= tEdge.Top.X)
					{
						maxima = null;
					}
				}
				else
				{
					while (maxima.Next != null && maxima.Next.X < horzEdge.Bot.X)
					{
						maxima = maxima.Next;
					}
					if (maxima.X <= tEdge.Top.X)
					{
						maxima = null;
					}
				}
			}
			global::UnityEngine.Rendering.Universal.OutPt outPt = null;
			while (true)
			{
				bool flag2 = horzEdge == tEdge;
				global::UnityEngine.Rendering.Universal.TEdge tEdge3 = GetNextInAEL(horzEdge, Dir);
				while (tEdge3 != null)
				{
					if (maxima != null)
					{
						if (Dir == global::UnityEngine.Rendering.Universal.Directions.dLeftToRight)
						{
							while (maxima != null && maxima.X < tEdge3.Curr.X)
							{
								if (horzEdge.OutIdx >= 0 && !flag)
								{
									AddOutPt(horzEdge, new global::UnityEngine.Rendering.Universal.IntPoint(maxima.X, horzEdge.Bot.Y));
								}
								maxima = maxima.Next;
							}
						}
						else
						{
							while (maxima != null && maxima.X > tEdge3.Curr.X)
							{
								if (horzEdge.OutIdx >= 0 && !flag)
								{
									AddOutPt(horzEdge, new global::UnityEngine.Rendering.Universal.IntPoint(maxima.X, horzEdge.Bot.Y));
								}
								maxima = maxima.Prev;
							}
						}
					}
					if ((Dir == global::UnityEngine.Rendering.Universal.Directions.dLeftToRight && tEdge3.Curr.X > Right) || (Dir == global::UnityEngine.Rendering.Universal.Directions.dRightToLeft && tEdge3.Curr.X < Left) || (tEdge3.Curr.X == horzEdge.Top.X && horzEdge.NextInLML != null && tEdge3.Dx < horzEdge.NextInLML.Dx))
					{
						break;
					}
					if (horzEdge.OutIdx >= 0 && !flag)
					{
						outPt = AddOutPt(horzEdge, tEdge3.Curr);
						for (global::UnityEngine.Rendering.Universal.TEdge tEdge4 = m_SortedEdges; tEdge4 != null; tEdge4 = tEdge4.NextInSEL)
						{
							if (tEdge4.OutIdx >= 0 && HorzSegmentsOverlap(horzEdge.Bot.X, horzEdge.Top.X, tEdge4.Bot.X, tEdge4.Top.X))
							{
								global::UnityEngine.Rendering.Universal.OutPt lastOutPt = GetLastOutPt(tEdge4);
								AddJoin(lastOutPt, outPt, tEdge4.Top);
							}
						}
						AddGhostJoin(outPt, horzEdge.Bot);
					}
					if (tEdge3 == tEdge2 && flag2)
					{
						if (horzEdge.OutIdx >= 0)
						{
							AddLocalMaxPoly(horzEdge, tEdge2, horzEdge.Top);
						}
						DeleteFromAEL(horzEdge);
						DeleteFromAEL(tEdge2);
						return;
					}
					if (Dir == global::UnityEngine.Rendering.Universal.Directions.dLeftToRight)
					{
						IntersectEdges(pt: new global::UnityEngine.Rendering.Universal.IntPoint(tEdge3.Curr.X, horzEdge.Curr.Y), e1: horzEdge, e2: tEdge3);
					}
					else
					{
						IntersectEdges(pt: new global::UnityEngine.Rendering.Universal.IntPoint(tEdge3.Curr.X, horzEdge.Curr.Y), e1: tEdge3, e2: horzEdge);
					}
					global::UnityEngine.Rendering.Universal.TEdge nextInAEL = GetNextInAEL(tEdge3, Dir);
					SwapPositionsInAEL(horzEdge, tEdge3);
					tEdge3 = nextInAEL;
				}
				if (horzEdge.NextInLML == null || !global::UnityEngine.Rendering.Universal.ClipperBase.IsHorizontal(horzEdge.NextInLML))
				{
					break;
				}
				UpdateEdgeIntoAEL(ref horzEdge);
				if (horzEdge.OutIdx >= 0)
				{
					AddOutPt(horzEdge, horzEdge.Bot);
				}
				GetHorzDirection(horzEdge, out Dir, out Left, out Right);
			}
			if (horzEdge.OutIdx >= 0 && outPt == null)
			{
				outPt = GetLastOutPt(horzEdge);
				for (global::UnityEngine.Rendering.Universal.TEdge tEdge5 = m_SortedEdges; tEdge5 != null; tEdge5 = tEdge5.NextInSEL)
				{
					if (tEdge5.OutIdx >= 0 && HorzSegmentsOverlap(horzEdge.Bot.X, horzEdge.Top.X, tEdge5.Bot.X, tEdge5.Top.X))
					{
						global::UnityEngine.Rendering.Universal.OutPt lastOutPt2 = GetLastOutPt(tEdge5);
						AddJoin(lastOutPt2, outPt, tEdge5.Top);
					}
				}
				AddGhostJoin(outPt, horzEdge.Top);
			}
			if (horzEdge.NextInLML != null)
			{
				if (horzEdge.OutIdx >= 0)
				{
					outPt = AddOutPt(horzEdge, horzEdge.Top);
					UpdateEdgeIntoAEL(ref horzEdge);
					if (horzEdge.WindDelta != 0)
					{
						global::UnityEngine.Rendering.Universal.TEdge prevInAEL = horzEdge.PrevInAEL;
						global::UnityEngine.Rendering.Universal.TEdge nextInAEL2 = horzEdge.NextInAEL;
						if (prevInAEL != null && prevInAEL.Curr.X == horzEdge.Bot.X && prevInAEL.Curr.Y == horzEdge.Bot.Y && prevInAEL.WindDelta != 0 && prevInAEL.OutIdx >= 0 && prevInAEL.Curr.Y > prevInAEL.Top.Y && global::UnityEngine.Rendering.Universal.ClipperBase.SlopesEqual(horzEdge, prevInAEL, m_UseFullRange))
						{
							global::UnityEngine.Rendering.Universal.OutPt op = AddOutPt(prevInAEL, horzEdge.Bot);
							AddJoin(outPt, op, horzEdge.Top);
						}
						else if (nextInAEL2 != null && nextInAEL2.Curr.X == horzEdge.Bot.X && nextInAEL2.Curr.Y == horzEdge.Bot.Y && nextInAEL2.WindDelta != 0 && nextInAEL2.OutIdx >= 0 && nextInAEL2.Curr.Y > nextInAEL2.Top.Y && global::UnityEngine.Rendering.Universal.ClipperBase.SlopesEqual(horzEdge, nextInAEL2, m_UseFullRange))
						{
							global::UnityEngine.Rendering.Universal.OutPt op2 = AddOutPt(nextInAEL2, horzEdge.Bot);
							AddJoin(outPt, op2, horzEdge.Top);
						}
					}
				}
				else
				{
					UpdateEdgeIntoAEL(ref horzEdge);
				}
			}
			else
			{
				if (horzEdge.OutIdx >= 0)
				{
					AddOutPt(horzEdge, horzEdge.Top);
				}
				DeleteFromAEL(horzEdge);
			}
		}

		private global::UnityEngine.Rendering.Universal.TEdge GetNextInAEL(global::UnityEngine.Rendering.Universal.TEdge e, global::UnityEngine.Rendering.Universal.Directions Directions)
		{
			if (Directions != global::UnityEngine.Rendering.Universal.Directions.dLeftToRight)
			{
				return e.PrevInAEL;
			}
			return e.NextInAEL;
		}

		private bool IsMinima(global::UnityEngine.Rendering.Universal.TEdge e)
		{
			if (e != null && e.Prev.NextInLML != e)
			{
				return e.Next.NextInLML != e;
			}
			return false;
		}

		private bool IsMaxima(global::UnityEngine.Rendering.Universal.TEdge e, double Y)
		{
			if (e != null && (double)e.Top.Y == Y)
			{
				return e.NextInLML == null;
			}
			return false;
		}

		private bool IsIntermediate(global::UnityEngine.Rendering.Universal.TEdge e, double Y)
		{
			if ((double)e.Top.Y == Y)
			{
				return e.NextInLML != null;
			}
			return false;
		}

		internal global::UnityEngine.Rendering.Universal.TEdge GetMaximaPair(global::UnityEngine.Rendering.Universal.TEdge e)
		{
			if (e.Next.Top == e.Top && e.Next.NextInLML == null)
			{
				return e.Next;
			}
			if (e.Prev.Top == e.Top && e.Prev.NextInLML == null)
			{
				return e.Prev;
			}
			return null;
		}

		internal global::UnityEngine.Rendering.Universal.TEdge GetMaximaPairEx(global::UnityEngine.Rendering.Universal.TEdge e)
		{
			global::UnityEngine.Rendering.Universal.TEdge maximaPair = GetMaximaPair(e);
			if (maximaPair == null || maximaPair.OutIdx == -2 || (maximaPair.NextInAEL == maximaPair.PrevInAEL && !global::UnityEngine.Rendering.Universal.ClipperBase.IsHorizontal(maximaPair)))
			{
				return null;
			}
			return maximaPair;
		}

		private bool ProcessIntersections(long topY)
		{
			if (m_ActiveEdges == null)
			{
				return true;
			}
			try
			{
				BuildIntersectList(topY);
				if (m_IntersectList.Count == 0)
				{
					return true;
				}
				if (m_IntersectList.Count != 1 && !FixupIntersectionOrder())
				{
					return false;
				}
				ProcessIntersectList();
			}
			catch
			{
				m_SortedEdges = null;
				m_IntersectList.Clear();
				throw new global::UnityEngine.Rendering.Universal.ClipperException("ProcessIntersections error");
			}
			m_SortedEdges = null;
			return true;
		}

		private void BuildIntersectList(long topY)
		{
			if (m_ActiveEdges == null)
			{
				return;
			}
			for (global::UnityEngine.Rendering.Universal.TEdge tEdge = (m_SortedEdges = m_ActiveEdges); tEdge != null; tEdge = tEdge.NextInAEL)
			{
				tEdge.PrevInSEL = tEdge.PrevInAEL;
				tEdge.NextInSEL = tEdge.NextInAEL;
				tEdge.Curr.X = TopX(tEdge, topY);
			}
			bool flag = true;
			while (flag && m_SortedEdges != null)
			{
				flag = false;
				global::UnityEngine.Rendering.Universal.TEdge tEdge = m_SortedEdges;
				while (tEdge.NextInSEL != null)
				{
					global::UnityEngine.Rendering.Universal.TEdge nextInSEL = tEdge.NextInSEL;
					if (tEdge.Curr.X > nextInSEL.Curr.X)
					{
						IntersectPoint(tEdge, nextInSEL, out var ip);
						if (ip.Y < topY)
						{
							ip = new global::UnityEngine.Rendering.Universal.IntPoint(TopX(tEdge, topY), topY);
						}
						global::UnityEngine.Rendering.Universal.IntersectNode intersectNode = new global::UnityEngine.Rendering.Universal.IntersectNode();
						intersectNode.Edge1 = tEdge;
						intersectNode.Edge2 = nextInSEL;
						intersectNode.Pt = ip;
						m_IntersectList.Add(intersectNode);
						SwapPositionsInSEL(tEdge, nextInSEL);
						flag = true;
					}
					else
					{
						tEdge = nextInSEL;
					}
				}
				if (tEdge.PrevInSEL == null)
				{
					break;
				}
				tEdge.PrevInSEL.NextInSEL = null;
			}
			m_SortedEdges = null;
		}

		private bool EdgesAdjacent(global::UnityEngine.Rendering.Universal.IntersectNode inode)
		{
			if (inode.Edge1.NextInSEL != inode.Edge2)
			{
				return inode.Edge1.PrevInSEL == inode.Edge2;
			}
			return true;
		}

		private static int IntersectNodeSort(global::UnityEngine.Rendering.Universal.IntersectNode node1, global::UnityEngine.Rendering.Universal.IntersectNode node2)
		{
			return (int)(node2.Pt.Y - node1.Pt.Y);
		}

		private bool FixupIntersectionOrder()
		{
			m_IntersectList.Sort(m_IntersectNodeComparer);
			CopyAELToSEL();
			int count = m_IntersectList.Count;
			for (int i = 0; i < count; i++)
			{
				if (!EdgesAdjacent(m_IntersectList[i]))
				{
					int j;
					for (j = i + 1; j < count && !EdgesAdjacent(m_IntersectList[j]); j++)
					{
					}
					if (j == count)
					{
						return false;
					}
					global::UnityEngine.Rendering.Universal.IntersectNode value = m_IntersectList[i];
					m_IntersectList[i] = m_IntersectList[j];
					m_IntersectList[j] = value;
				}
				SwapPositionsInSEL(m_IntersectList[i].Edge1, m_IntersectList[i].Edge2);
			}
			return true;
		}

		private void ProcessIntersectList()
		{
			for (int i = 0; i < m_IntersectList.Count; i++)
			{
				global::UnityEngine.Rendering.Universal.IntersectNode intersectNode = m_IntersectList[i];
				IntersectEdges(intersectNode.Edge1, intersectNode.Edge2, intersectNode.Pt);
				SwapPositionsInAEL(intersectNode.Edge1, intersectNode.Edge2);
			}
			m_IntersectList.Clear();
		}

		internal static long Round(double value)
		{
			if (!(value < 0.0))
			{
				return (long)(value + 0.5);
			}
			return (long)(value - 0.5);
		}

		private static long TopX(global::UnityEngine.Rendering.Universal.TEdge edge, long currentY)
		{
			if (currentY == edge.Top.Y)
			{
				return edge.Top.X;
			}
			return edge.Bot.X + Round(edge.Dx * (double)(currentY - edge.Bot.Y));
		}

		private void IntersectPoint(global::UnityEngine.Rendering.Universal.TEdge edge1, global::UnityEngine.Rendering.Universal.TEdge edge2, out global::UnityEngine.Rendering.Universal.IntPoint ip)
		{
			ip = default(global::UnityEngine.Rendering.Universal.IntPoint);
			long num = -1L;
			bool flag = edge2.Curr.N > 0 && edge2.Curr.N < LastIndex && edge1.Curr.N > 0 && edge1.Curr.N < LastIndex;
			if (edge1.Curr.N > edge2.Curr.N)
			{
				if (edge2.Curr.N != -1)
				{
					if (flag)
					{
						num = ((edge1.Curr.N > 0) ? (edge1.Curr.N - 1) : 0);
					}
				}
				else
				{
					num = edge1.Curr.N;
				}
			}
			else if (edge1.Curr.N != -1)
			{
				if (flag)
				{
					num = edge2.Curr.N;
				}
			}
			else
			{
				num = ((edge2.Curr.N > 0) ? (edge2.Curr.N - 1) : 0);
			}
			ip.D = 2L;
			ip.N = (flag ? num : (-1));
			if (edge1.Dx == edge2.Dx)
			{
				ip.Y = edge1.Curr.Y;
				ip.X = TopX(edge1, ip.Y);
				return;
			}
			if (edge1.Delta.X == 0L)
			{
				ip.X = edge1.Bot.X;
				if (global::UnityEngine.Rendering.Universal.ClipperBase.IsHorizontal(edge2))
				{
					ip.Y = edge2.Bot.Y;
				}
				else
				{
					double num2 = (double)edge2.Bot.Y - (double)edge2.Bot.X / edge2.Dx;
					ip.Y = Round((double)ip.X / edge2.Dx + num2);
				}
			}
			else if (edge2.Delta.X == 0L)
			{
				ip.X = edge2.Bot.X;
				if (global::UnityEngine.Rendering.Universal.ClipperBase.IsHorizontal(edge1))
				{
					ip.Y = edge1.Bot.Y;
				}
				else
				{
					double num3 = (double)edge1.Bot.Y - (double)edge1.Bot.X / edge1.Dx;
					ip.Y = Round((double)ip.X / edge1.Dx + num3);
				}
			}
			else
			{
				double num3 = (double)edge1.Bot.X - (double)edge1.Bot.Y * edge1.Dx;
				double num2 = (double)edge2.Bot.X - (double)edge2.Bot.Y * edge2.Dx;
				double num4 = (num2 - num3) / (edge1.Dx - edge2.Dx);
				ip.Y = Round(num4);
				if (global::System.Math.Abs(edge1.Dx) < global::System.Math.Abs(edge2.Dx))
				{
					ip.X = Round(edge1.Dx * num4 + num3);
				}
				else
				{
					ip.X = Round(edge2.Dx * num4 + num2);
				}
			}
			if (ip.Y < edge1.Top.Y || ip.Y < edge2.Top.Y)
			{
				if (edge1.Top.Y > edge2.Top.Y)
				{
					ip.Y = edge1.Top.Y;
				}
				else
				{
					ip.Y = edge2.Top.Y;
				}
				if (global::System.Math.Abs(edge1.Dx) < global::System.Math.Abs(edge2.Dx))
				{
					ip.X = TopX(edge1, ip.Y);
				}
				else
				{
					ip.X = TopX(edge2, ip.Y);
				}
			}
			if (ip.Y > edge1.Curr.Y)
			{
				ip.Y = edge1.Curr.Y;
				if (global::System.Math.Abs(edge1.Dx) > global::System.Math.Abs(edge2.Dx))
				{
					ip.X = TopX(edge2, ip.Y);
				}
				else
				{
					ip.X = TopX(edge1, ip.Y);
				}
			}
		}

		private void ProcessEdgesAtTopOfScanbeam(long topY)
		{
			global::UnityEngine.Rendering.Universal.TEdge e = m_ActiveEdges;
			while (e != null)
			{
				bool flag = IsMaxima(e, topY);
				if (flag)
				{
					global::UnityEngine.Rendering.Universal.TEdge maximaPairEx = GetMaximaPairEx(e);
					flag = maximaPairEx == null || !global::UnityEngine.Rendering.Universal.ClipperBase.IsHorizontal(maximaPairEx);
				}
				if (flag)
				{
					if (StrictlySimple)
					{
						InsertMaxima(e.Top.X);
					}
					global::UnityEngine.Rendering.Universal.TEdge prevInAEL = e.PrevInAEL;
					DoMaxima(e);
					e = ((prevInAEL != null) ? prevInAEL.NextInAEL : m_ActiveEdges);
					continue;
				}
				if (IsIntermediate(e, topY) && global::UnityEngine.Rendering.Universal.ClipperBase.IsHorizontal(e.NextInLML))
				{
					UpdateEdgeIntoAEL(ref e);
					if (e.OutIdx >= 0)
					{
						AddOutPt(e, e.Bot);
					}
					AddEdgeToSEL(e);
				}
				else
				{
					e.Curr.X = TopX(e, topY);
					e.Curr.Y = topY;
				}
				if (StrictlySimple)
				{
					global::UnityEngine.Rendering.Universal.TEdge prevInAEL2 = e.PrevInAEL;
					if (e.OutIdx >= 0 && e.WindDelta != 0 && prevInAEL2 != null && prevInAEL2.OutIdx >= 0 && prevInAEL2.Curr.X == e.Curr.X && prevInAEL2.WindDelta != 0)
					{
						global::UnityEngine.Rendering.Universal.IntPoint intPoint = new global::UnityEngine.Rendering.Universal.IntPoint(e.Curr);
						global::UnityEngine.Rendering.Universal.OutPt op = AddOutPt(prevInAEL2, intPoint);
						global::UnityEngine.Rendering.Universal.OutPt op2 = AddOutPt(e, intPoint);
						AddJoin(op, op2, intPoint);
					}
				}
				e = e.NextInAEL;
			}
			ProcessHorizontals();
			m_Maxima = null;
			for (e = m_ActiveEdges; e != null; e = e.NextInAEL)
			{
				if (IsIntermediate(e, topY))
				{
					global::UnityEngine.Rendering.Universal.OutPt outPt = null;
					if (e.OutIdx >= 0)
					{
						outPt = AddOutPt(e, e.Top);
					}
					UpdateEdgeIntoAEL(ref e);
					global::UnityEngine.Rendering.Universal.TEdge prevInAEL3 = e.PrevInAEL;
					global::UnityEngine.Rendering.Universal.TEdge nextInAEL = e.NextInAEL;
					if (prevInAEL3 != null && prevInAEL3.Curr.X == e.Bot.X && prevInAEL3.Curr.Y == e.Bot.Y && outPt != null && prevInAEL3.OutIdx >= 0 && prevInAEL3.Curr.Y > prevInAEL3.Top.Y && global::UnityEngine.Rendering.Universal.ClipperBase.SlopesEqual(e.Curr, e.Top, prevInAEL3.Curr, prevInAEL3.Top, m_UseFullRange) && e.WindDelta != 0 && prevInAEL3.WindDelta != 0)
					{
						global::UnityEngine.Rendering.Universal.OutPt op3 = AddOutPt(prevInAEL3, e.Bot);
						AddJoin(outPt, op3, e.Top);
					}
					else if (nextInAEL != null && nextInAEL.Curr.X == e.Bot.X && nextInAEL.Curr.Y == e.Bot.Y && outPt != null && nextInAEL.OutIdx >= 0 && nextInAEL.Curr.Y > nextInAEL.Top.Y && global::UnityEngine.Rendering.Universal.ClipperBase.SlopesEqual(e.Curr, e.Top, nextInAEL.Curr, nextInAEL.Top, m_UseFullRange) && e.WindDelta != 0 && nextInAEL.WindDelta != 0)
					{
						global::UnityEngine.Rendering.Universal.OutPt op4 = AddOutPt(nextInAEL, e.Bot);
						AddJoin(outPt, op4, e.Top);
					}
				}
			}
		}

		private void DoMaxima(global::UnityEngine.Rendering.Universal.TEdge e)
		{
			global::UnityEngine.Rendering.Universal.TEdge maximaPairEx = GetMaximaPairEx(e);
			if (maximaPairEx == null)
			{
				if (e.OutIdx >= 0)
				{
					AddOutPt(e, e.Top);
				}
				DeleteFromAEL(e);
				return;
			}
			global::UnityEngine.Rendering.Universal.TEdge nextInAEL = e.NextInAEL;
			while (nextInAEL != null && nextInAEL != maximaPairEx)
			{
				IntersectEdges(e, nextInAEL, e.Top);
				SwapPositionsInAEL(e, nextInAEL);
				nextInAEL = e.NextInAEL;
			}
			if (e.OutIdx == -1 && maximaPairEx.OutIdx == -1)
			{
				DeleteFromAEL(e);
				DeleteFromAEL(maximaPairEx);
				return;
			}
			if (e.OutIdx >= 0 && maximaPairEx.OutIdx >= 0)
			{
				if (e.OutIdx >= 0)
				{
					AddLocalMaxPoly(e, maximaPairEx, e.Top);
				}
				DeleteFromAEL(e);
				DeleteFromAEL(maximaPairEx);
				return;
			}
			if (e.WindDelta == 0)
			{
				if (e.OutIdx >= 0)
				{
					AddOutPt(e, e.Top);
					e.OutIdx = -1;
				}
				DeleteFromAEL(e);
				if (maximaPairEx.OutIdx >= 0)
				{
					AddOutPt(maximaPairEx, e.Top);
					maximaPairEx.OutIdx = -1;
				}
				DeleteFromAEL(maximaPairEx);
				return;
			}
			throw new global::UnityEngine.Rendering.Universal.ClipperException("DoMaxima error");
		}

		public static void ReversePaths(global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>> polys)
		{
			for (int i = 0; i < polys.Count; i++)
			{
				polys[i].Reverse();
			}
		}

		public static bool Orientation(global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> poly)
		{
			return Area(poly) >= 0.0;
		}

		private int PointCount(global::UnityEngine.Rendering.Universal.OutPt pts)
		{
			if (pts == null)
			{
				return 0;
			}
			int num = 0;
			global::UnityEngine.Rendering.Universal.OutPt outPt = pts;
			do
			{
				num++;
				outPt = outPt.Next;
			}
			while (outPt != pts);
			return num;
		}

		private void BuildResult(global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>> polyg)
		{
			polyg.Clear();
			polyg.Capacity = m_PolyOuts.Count;
			for (int i = 0; i < m_PolyOuts.Count; i++)
			{
				global::UnityEngine.Rendering.Universal.OutRec outRec = m_PolyOuts[i];
				if (outRec.Pts == null)
				{
					continue;
				}
				global::UnityEngine.Rendering.Universal.OutPt prev = outRec.Pts.Prev;
				int num = PointCount(prev);
				if (num >= 2)
				{
					global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> list = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>(num);
					for (int j = 0; j < num; j++)
					{
						list.Add(prev.Pt);
						prev = prev.Prev;
					}
					polyg.Add(list);
				}
			}
		}

		private void BuildResult2(global::UnityEngine.Rendering.Universal.PolyTree polytree)
		{
			polytree.Clear();
			polytree.m_AllPolys.Capacity = m_PolyOuts.Count;
			for (int i = 0; i < m_PolyOuts.Count; i++)
			{
				global::UnityEngine.Rendering.Universal.OutRec outRec = m_PolyOuts[i];
				int num = PointCount(outRec.Pts);
				if ((!outRec.IsOpen || num >= 2) && (outRec.IsOpen || num >= 3))
				{
					FixHoleLinkage(outRec);
					global::UnityEngine.Rendering.Universal.PolyNode polyNode = new global::UnityEngine.Rendering.Universal.PolyNode();
					polytree.m_AllPolys.Add(polyNode);
					outRec.PolyNode = polyNode;
					polyNode.m_polygon.Capacity = num;
					global::UnityEngine.Rendering.Universal.OutPt prev = outRec.Pts.Prev;
					for (int j = 0; j < num; j++)
					{
						polyNode.m_polygon.Add(prev.Pt);
						prev = prev.Prev;
					}
				}
			}
			polytree.m_Childs.Capacity = m_PolyOuts.Count;
			for (int k = 0; k < m_PolyOuts.Count; k++)
			{
				global::UnityEngine.Rendering.Universal.OutRec outRec2 = m_PolyOuts[k];
				if (outRec2.PolyNode != null)
				{
					if (outRec2.IsOpen)
					{
						outRec2.PolyNode.IsOpen = true;
						polytree.AddChild(outRec2.PolyNode);
					}
					else if (outRec2.FirstLeft != null && outRec2.FirstLeft.PolyNode != null)
					{
						outRec2.FirstLeft.PolyNode.AddChild(outRec2.PolyNode);
					}
					else
					{
						polytree.AddChild(outRec2.PolyNode);
					}
				}
			}
		}

		private void FixupOutPolyline(global::UnityEngine.Rendering.Universal.OutRec outrec)
		{
			global::UnityEngine.Rendering.Universal.OutPt outPt = outrec.Pts;
			global::UnityEngine.Rendering.Universal.OutPt prev = outPt.Prev;
			while (outPt != prev)
			{
				outPt = outPt.Next;
				if (outPt.Pt == outPt.Prev.Pt)
				{
					if (outPt == prev)
					{
						prev = outPt.Prev;
					}
					global::UnityEngine.Rendering.Universal.OutPt prev2 = outPt.Prev;
					prev2.Next = outPt.Next;
					outPt.Next.Prev = prev2;
					outPt = prev2;
				}
			}
			if (outPt == outPt.Prev)
			{
				outrec.Pts = null;
			}
		}

		private void FixupOutPolygon(global::UnityEngine.Rendering.Universal.OutRec outRec)
		{
			global::UnityEngine.Rendering.Universal.OutPt outPt = null;
			outRec.BottomPt = null;
			global::UnityEngine.Rendering.Universal.OutPt outPt2 = outRec.Pts;
			bool flag = base.PreserveCollinear || StrictlySimple;
			while (true)
			{
				if (outPt2.Prev == outPt2 || outPt2.Prev == outPt2.Next)
				{
					outRec.Pts = null;
					return;
				}
				if (outPt2.Pt == outPt2.Next.Pt || outPt2.Pt == outPt2.Prev.Pt || (global::UnityEngine.Rendering.Universal.ClipperBase.SlopesEqual(outPt2.Prev.Pt, outPt2.Pt, outPt2.Next.Pt, m_UseFullRange) && (!flag || !Pt2IsBetweenPt1AndPt3(outPt2.Prev.Pt, outPt2.Pt, outPt2.Next.Pt))))
				{
					outPt = null;
					outPt2.Prev.Next = outPt2.Next;
					outPt2.Next.Prev = outPt2.Prev;
					outPt2 = outPt2.Prev;
					continue;
				}
				if (outPt2 == outPt)
				{
					break;
				}
				if (outPt == null)
				{
					outPt = outPt2;
				}
				outPt2 = outPt2.Next;
			}
			outRec.Pts = outPt2;
		}

		private global::UnityEngine.Rendering.Universal.OutPt DupOutPt(global::UnityEngine.Rendering.Universal.OutPt outPt, bool InsertAfter)
		{
			global::UnityEngine.Rendering.Universal.OutPt outPt2 = new global::UnityEngine.Rendering.Universal.OutPt();
			outPt2.Pt = outPt.Pt;
			outPt2.Idx = outPt.Idx;
			if (InsertAfter)
			{
				outPt2.Next = outPt.Next;
				outPt2.Prev = outPt;
				outPt.Next.Prev = outPt2;
				outPt.Next = outPt2;
			}
			else
			{
				outPt2.Prev = outPt.Prev;
				outPt2.Next = outPt;
				outPt.Prev.Next = outPt2;
				outPt.Prev = outPt2;
			}
			return outPt2;
		}

		private bool GetOverlap(long a1, long a2, long b1, long b2, out long Left, out long Right)
		{
			if (a1 < a2)
			{
				if (b1 < b2)
				{
					Left = global::System.Math.Max(a1, b1);
					Right = global::System.Math.Min(a2, b2);
				}
				else
				{
					Left = global::System.Math.Max(a1, b2);
					Right = global::System.Math.Min(a2, b1);
				}
			}
			else if (b1 < b2)
			{
				Left = global::System.Math.Max(a2, b1);
				Right = global::System.Math.Min(a1, b2);
			}
			else
			{
				Left = global::System.Math.Max(a2, b2);
				Right = global::System.Math.Min(a1, b1);
			}
			return Left < Right;
		}

		private bool JoinHorz(global::UnityEngine.Rendering.Universal.OutPt op1, global::UnityEngine.Rendering.Universal.OutPt op1b, global::UnityEngine.Rendering.Universal.OutPt op2, global::UnityEngine.Rendering.Universal.OutPt op2b, global::UnityEngine.Rendering.Universal.IntPoint Pt, bool DiscardLeft)
		{
			global::UnityEngine.Rendering.Universal.Directions directions = ((op1.Pt.X <= op1b.Pt.X) ? global::UnityEngine.Rendering.Universal.Directions.dLeftToRight : global::UnityEngine.Rendering.Universal.Directions.dRightToLeft);
			global::UnityEngine.Rendering.Universal.Directions directions2 = ((op2.Pt.X <= op2b.Pt.X) ? global::UnityEngine.Rendering.Universal.Directions.dLeftToRight : global::UnityEngine.Rendering.Universal.Directions.dRightToLeft);
			if (directions == directions2)
			{
				return false;
			}
			if (directions == global::UnityEngine.Rendering.Universal.Directions.dLeftToRight)
			{
				while (op1.Next.Pt.X <= Pt.X && op1.Next.Pt.X >= op1.Pt.X && op1.Next.Pt.Y == Pt.Y)
				{
					op1 = op1.Next;
				}
				if (DiscardLeft && op1.Pt.X != Pt.X)
				{
					op1 = op1.Next;
				}
				op1b = DupOutPt(op1, !DiscardLeft);
				if (op1b.Pt != Pt)
				{
					op1 = op1b;
					op1.Pt = Pt;
					op1b = DupOutPt(op1, !DiscardLeft);
				}
			}
			else
			{
				while (op1.Next.Pt.X >= Pt.X && op1.Next.Pt.X <= op1.Pt.X && op1.Next.Pt.Y == Pt.Y)
				{
					op1 = op1.Next;
				}
				if (!DiscardLeft && op1.Pt.X != Pt.X)
				{
					op1 = op1.Next;
				}
				op1b = DupOutPt(op1, DiscardLeft);
				if (op1b.Pt != Pt)
				{
					op1 = op1b;
					op1.Pt = Pt;
					op1b = DupOutPt(op1, DiscardLeft);
				}
			}
			if (directions2 == global::UnityEngine.Rendering.Universal.Directions.dLeftToRight)
			{
				while (op2.Next.Pt.X <= Pt.X && op2.Next.Pt.X >= op2.Pt.X && op2.Next.Pt.Y == Pt.Y)
				{
					op2 = op2.Next;
				}
				if (DiscardLeft && op2.Pt.X != Pt.X)
				{
					op2 = op2.Next;
				}
				op2b = DupOutPt(op2, !DiscardLeft);
				if (op2b.Pt != Pt)
				{
					op2 = op2b;
					op2.Pt = Pt;
					op2b = DupOutPt(op2, !DiscardLeft);
				}
			}
			else
			{
				while (op2.Next.Pt.X >= Pt.X && op2.Next.Pt.X <= op2.Pt.X && op2.Next.Pt.Y == Pt.Y)
				{
					op2 = op2.Next;
				}
				if (!DiscardLeft && op2.Pt.X != Pt.X)
				{
					op2 = op2.Next;
				}
				op2b = DupOutPt(op2, DiscardLeft);
				if (op2b.Pt != Pt)
				{
					op2 = op2b;
					op2.Pt = Pt;
					op2b = DupOutPt(op2, DiscardLeft);
				}
			}
			if (directions == global::UnityEngine.Rendering.Universal.Directions.dLeftToRight == DiscardLeft)
			{
				op1.Prev = op2;
				op2.Next = op1;
				op1b.Next = op2b;
				op2b.Prev = op1b;
			}
			else
			{
				op1.Next = op2;
				op2.Prev = op1;
				op1b.Prev = op2b;
				op2b.Next = op1b;
			}
			return true;
		}

		private bool JoinPoints(global::UnityEngine.Rendering.Universal.Join j, global::UnityEngine.Rendering.Universal.OutRec outRec1, global::UnityEngine.Rendering.Universal.OutRec outRec2)
		{
			global::UnityEngine.Rendering.Universal.OutPt outPt = j.OutPt1;
			global::UnityEngine.Rendering.Universal.OutPt outPt2 = j.OutPt2;
			bool flag = j.OutPt1.Pt.Y == j.OffPt.Y;
			global::UnityEngine.Rendering.Universal.OutPt next;
			global::UnityEngine.Rendering.Universal.OutPt next2;
			if (flag && j.OffPt == j.OutPt1.Pt && j.OffPt == j.OutPt2.Pt)
			{
				if (outRec1 != outRec2)
				{
					return false;
				}
				next = j.OutPt1.Next;
				while (next != outPt && next.Pt == j.OffPt)
				{
					next = next.Next;
				}
				bool flag2 = next.Pt.Y > j.OffPt.Y;
				next2 = j.OutPt2.Next;
				while (next2 != outPt2 && next2.Pt == j.OffPt)
				{
					next2 = next2.Next;
				}
				bool flag3 = next2.Pt.Y > j.OffPt.Y;
				if (flag2 == flag3)
				{
					return false;
				}
				if (flag2)
				{
					next = DupOutPt(outPt, InsertAfter: false);
					next2 = DupOutPt(outPt2, InsertAfter: true);
					outPt.Prev = outPt2;
					outPt2.Next = outPt;
					next.Next = next2;
					next2.Prev = next;
					j.OutPt1 = outPt;
					j.OutPt2 = next;
					return true;
				}
				next = DupOutPt(outPt, InsertAfter: true);
				next2 = DupOutPt(outPt2, InsertAfter: false);
				outPt.Next = outPt2;
				outPt2.Prev = outPt;
				next.Prev = next2;
				next2.Next = next;
				j.OutPt1 = outPt;
				j.OutPt2 = next;
				return true;
			}
			if (flag)
			{
				next = outPt;
				while (outPt.Prev.Pt.Y == outPt.Pt.Y && outPt.Prev != next && outPt.Prev != outPt2)
				{
					outPt = outPt.Prev;
				}
				while (next.Next.Pt.Y == next.Pt.Y && next.Next != outPt && next.Next != outPt2)
				{
					next = next.Next;
				}
				if (next.Next == outPt || next.Next == outPt2)
				{
					return false;
				}
				next2 = outPt2;
				while (outPt2.Prev.Pt.Y == outPt2.Pt.Y && outPt2.Prev != next2 && outPt2.Prev != next)
				{
					outPt2 = outPt2.Prev;
				}
				while (next2.Next.Pt.Y == next2.Pt.Y && next2.Next != outPt2 && next2.Next != outPt)
				{
					next2 = next2.Next;
				}
				if (next2.Next == outPt2 || next2.Next == outPt)
				{
					return false;
				}
				if (!GetOverlap(outPt.Pt.X, next.Pt.X, outPt2.Pt.X, next2.Pt.X, out var Left, out var Right))
				{
					return false;
				}
				global::UnityEngine.Rendering.Universal.IntPoint pt;
				bool discardLeft;
				if (outPt.Pt.X >= Left && outPt.Pt.X <= Right)
				{
					pt = outPt.Pt;
					discardLeft = outPt.Pt.X > next.Pt.X;
				}
				else if (outPt2.Pt.X >= Left && outPt2.Pt.X <= Right)
				{
					pt = outPt2.Pt;
					discardLeft = outPt2.Pt.X > next2.Pt.X;
				}
				else if (next.Pt.X >= Left && next.Pt.X <= Right)
				{
					pt = next.Pt;
					discardLeft = next.Pt.X > outPt.Pt.X;
				}
				else
				{
					pt = next2.Pt;
					discardLeft = next2.Pt.X > outPt2.Pt.X;
				}
				j.OutPt1 = outPt;
				j.OutPt2 = outPt2;
				return JoinHorz(outPt, next, outPt2, next2, pt, discardLeft);
			}
			next = outPt.Next;
			while (next.Pt == outPt.Pt && next != outPt)
			{
				next = next.Next;
			}
			bool flag4 = next.Pt.Y > outPt.Pt.Y || !global::UnityEngine.Rendering.Universal.ClipperBase.SlopesEqual(outPt.Pt, next.Pt, j.OffPt, m_UseFullRange);
			if (flag4)
			{
				next = outPt.Prev;
				while (next.Pt == outPt.Pt && next != outPt)
				{
					next = next.Prev;
				}
				if (next.Pt.Y > outPt.Pt.Y || !global::UnityEngine.Rendering.Universal.ClipperBase.SlopesEqual(outPt.Pt, next.Pt, j.OffPt, m_UseFullRange))
				{
					return false;
				}
			}
			next2 = outPt2.Next;
			while (next2.Pt == outPt2.Pt && next2 != outPt2)
			{
				next2 = next2.Next;
			}
			bool flag5 = next2.Pt.Y > outPt2.Pt.Y || !global::UnityEngine.Rendering.Universal.ClipperBase.SlopesEqual(outPt2.Pt, next2.Pt, j.OffPt, m_UseFullRange);
			if (flag5)
			{
				next2 = outPt2.Prev;
				while (next2.Pt == outPt2.Pt && next2 != outPt2)
				{
					next2 = next2.Prev;
				}
				if (next2.Pt.Y > outPt2.Pt.Y || !global::UnityEngine.Rendering.Universal.ClipperBase.SlopesEqual(outPt2.Pt, next2.Pt, j.OffPt, m_UseFullRange))
				{
					return false;
				}
			}
			if (next == outPt || next2 == outPt2 || next == next2 || (outRec1 == outRec2 && flag4 == flag5))
			{
				return false;
			}
			if (flag4)
			{
				next = DupOutPt(outPt, InsertAfter: false);
				next2 = DupOutPt(outPt2, InsertAfter: true);
				outPt.Prev = outPt2;
				outPt2.Next = outPt;
				next.Next = next2;
				next2.Prev = next;
				j.OutPt1 = outPt;
				j.OutPt2 = next;
				return true;
			}
			next = DupOutPt(outPt, InsertAfter: true);
			next2 = DupOutPt(outPt2, InsertAfter: false);
			outPt.Next = outPt2;
			outPt2.Prev = outPt;
			next.Prev = next2;
			next2.Next = next;
			j.OutPt1 = outPt;
			j.OutPt2 = next;
			return true;
		}

		public static int PointInPolygon(global::UnityEngine.Rendering.Universal.IntPoint pt, global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> path)
		{
			int num = 0;
			int count = path.Count;
			if (count < 3)
			{
				return 0;
			}
			global::UnityEngine.Rendering.Universal.IntPoint intPoint = path[0];
			for (int i = 1; i <= count; i++)
			{
				global::UnityEngine.Rendering.Universal.IntPoint intPoint2 = ((i == count) ? path[0] : path[i]);
				if (intPoint2.Y == pt.Y && (intPoint2.X == pt.X || (intPoint.Y == pt.Y && intPoint2.X > pt.X == intPoint.X < pt.X)))
				{
					return -1;
				}
				if (intPoint.Y < pt.Y != intPoint2.Y < pt.Y)
				{
					if (intPoint.X >= pt.X)
					{
						if (intPoint2.X > pt.X)
						{
							num = 1 - num;
						}
						else
						{
							double num2 = (double)(intPoint.X - pt.X) * (double)(intPoint2.Y - pt.Y) - (double)(intPoint2.X - pt.X) * (double)(intPoint.Y - pt.Y);
							if (num2 == 0.0)
							{
								return -1;
							}
							if (num2 > 0.0 == intPoint2.Y > intPoint.Y)
							{
								num = 1 - num;
							}
						}
					}
					else if (intPoint2.X > pt.X)
					{
						double num3 = (double)(intPoint.X - pt.X) * (double)(intPoint2.Y - pt.Y) - (double)(intPoint2.X - pt.X) * (double)(intPoint.Y - pt.Y);
						if (num3 == 0.0)
						{
							return -1;
						}
						if (num3 > 0.0 == intPoint2.Y > intPoint.Y)
						{
							num = 1 - num;
						}
					}
				}
				intPoint = intPoint2;
			}
			return num;
		}

		private static int PointInPolygon(global::UnityEngine.Rendering.Universal.IntPoint pt, global::UnityEngine.Rendering.Universal.OutPt op)
		{
			int num = 0;
			global::UnityEngine.Rendering.Universal.OutPt outPt = op;
			long x = pt.X;
			long y = pt.Y;
			long num2 = op.Pt.X;
			long num3 = op.Pt.Y;
			do
			{
				op = op.Next;
				long x2 = op.Pt.X;
				long y2 = op.Pt.Y;
				if (y2 == y && (x2 == x || (num3 == y && x2 > x == num2 < x)))
				{
					return -1;
				}
				if (num3 < y != y2 < y)
				{
					if (num2 >= x)
					{
						if (x2 > x)
						{
							num = 1 - num;
						}
						else
						{
							double num4 = (double)(num2 - x) * (double)(y2 - y) - (double)(x2 - x) * (double)(num3 - y);
							if (num4 == 0.0)
							{
								return -1;
							}
							if (num4 > 0.0 == y2 > num3)
							{
								num = 1 - num;
							}
						}
					}
					else if (x2 > x)
					{
						double num5 = (double)(num2 - x) * (double)(y2 - y) - (double)(x2 - x) * (double)(num3 - y);
						if (num5 == 0.0)
						{
							return -1;
						}
						if (num5 > 0.0 == y2 > num3)
						{
							num = 1 - num;
						}
					}
				}
				num2 = x2;
				num3 = y2;
			}
			while (outPt != op);
			return num;
		}

		private static bool Poly2ContainsPoly1(global::UnityEngine.Rendering.Universal.OutPt outPt1, global::UnityEngine.Rendering.Universal.OutPt outPt2)
		{
			global::UnityEngine.Rendering.Universal.OutPt outPt3 = outPt1;
			do
			{
				int num = PointInPolygon(outPt3.Pt, outPt2);
				if (num >= 0)
				{
					return num > 0;
				}
				outPt3 = outPt3.Next;
			}
			while (outPt3 != outPt1);
			return true;
		}

		private void FixupFirstLefts1(global::UnityEngine.Rendering.Universal.OutRec OldOutRec, global::UnityEngine.Rendering.Universal.OutRec NewOutRec)
		{
			foreach (global::UnityEngine.Rendering.Universal.OutRec polyOut in m_PolyOuts)
			{
				global::UnityEngine.Rendering.Universal.OutRec outRec = ParseFirstLeft(polyOut.FirstLeft);
				if (polyOut.Pts != null && outRec == OldOutRec && Poly2ContainsPoly1(polyOut.Pts, NewOutRec.Pts))
				{
					polyOut.FirstLeft = NewOutRec;
				}
			}
		}

		private void FixupFirstLefts2(global::UnityEngine.Rendering.Universal.OutRec innerOutRec, global::UnityEngine.Rendering.Universal.OutRec outerOutRec)
		{
			global::UnityEngine.Rendering.Universal.OutRec firstLeft = outerOutRec.FirstLeft;
			foreach (global::UnityEngine.Rendering.Universal.OutRec polyOut in m_PolyOuts)
			{
				if (polyOut.Pts == null || polyOut == outerOutRec || polyOut == innerOutRec)
				{
					continue;
				}
				global::UnityEngine.Rendering.Universal.OutRec outRec = ParseFirstLeft(polyOut.FirstLeft);
				if (outRec == firstLeft || outRec == innerOutRec || outRec == outerOutRec)
				{
					if (Poly2ContainsPoly1(polyOut.Pts, innerOutRec.Pts))
					{
						polyOut.FirstLeft = innerOutRec;
					}
					else if (Poly2ContainsPoly1(polyOut.Pts, outerOutRec.Pts))
					{
						polyOut.FirstLeft = outerOutRec;
					}
					else if (polyOut.FirstLeft == innerOutRec || polyOut.FirstLeft == outerOutRec)
					{
						polyOut.FirstLeft = firstLeft;
					}
				}
			}
		}

		private void FixupFirstLefts3(global::UnityEngine.Rendering.Universal.OutRec OldOutRec, global::UnityEngine.Rendering.Universal.OutRec NewOutRec)
		{
			foreach (global::UnityEngine.Rendering.Universal.OutRec polyOut in m_PolyOuts)
			{
				global::UnityEngine.Rendering.Universal.OutRec outRec = ParseFirstLeft(polyOut.FirstLeft);
				if (polyOut.Pts != null && outRec == OldOutRec)
				{
					polyOut.FirstLeft = NewOutRec;
				}
			}
		}

		private static global::UnityEngine.Rendering.Universal.OutRec ParseFirstLeft(global::UnityEngine.Rendering.Universal.OutRec FirstLeft)
		{
			while (FirstLeft != null && FirstLeft.Pts == null)
			{
				FirstLeft = FirstLeft.FirstLeft;
			}
			return FirstLeft;
		}

		private void JoinCommonEdges()
		{
			for (int i = 0; i < m_Joins.Count; i++)
			{
				global::UnityEngine.Rendering.Universal.Join obj = m_Joins[i];
				global::UnityEngine.Rendering.Universal.OutRec outRec = GetOutRec(obj.OutPt1.Idx);
				global::UnityEngine.Rendering.Universal.OutRec outRec2 = GetOutRec(obj.OutPt2.Idx);
				if (outRec.Pts == null || outRec2.Pts == null || outRec.IsOpen || outRec2.IsOpen)
				{
					continue;
				}
				global::UnityEngine.Rendering.Universal.OutRec outRec3 = ((outRec == outRec2) ? outRec : (OutRec1RightOfOutRec2(outRec, outRec2) ? outRec2 : ((!OutRec1RightOfOutRec2(outRec2, outRec)) ? GetLowermostRec(outRec, outRec2) : outRec)));
				if (!JoinPoints(obj, outRec, outRec2))
				{
					continue;
				}
				if (outRec == outRec2)
				{
					outRec.Pts = obj.OutPt1;
					outRec.BottomPt = null;
					outRec2 = CreateOutRec();
					outRec2.Pts = obj.OutPt2;
					UpdateOutPtIdxs(outRec2);
					if (Poly2ContainsPoly1(outRec2.Pts, outRec.Pts))
					{
						outRec2.IsHole = !outRec.IsHole;
						outRec2.FirstLeft = outRec;
						if (m_UsingPolyTree)
						{
							FixupFirstLefts2(outRec2, outRec);
						}
						if ((outRec2.IsHole ^ ReverseSolution) == Area(outRec2) > 0.0)
						{
							ReversePolyPtLinks(outRec2.Pts);
						}
					}
					else if (Poly2ContainsPoly1(outRec.Pts, outRec2.Pts))
					{
						outRec2.IsHole = outRec.IsHole;
						outRec.IsHole = !outRec2.IsHole;
						outRec2.FirstLeft = outRec.FirstLeft;
						outRec.FirstLeft = outRec2;
						if (m_UsingPolyTree)
						{
							FixupFirstLefts2(outRec, outRec2);
						}
						if ((outRec.IsHole ^ ReverseSolution) == Area(outRec) > 0.0)
						{
							ReversePolyPtLinks(outRec.Pts);
						}
					}
					else
					{
						outRec2.IsHole = outRec.IsHole;
						outRec2.FirstLeft = outRec.FirstLeft;
						if (m_UsingPolyTree)
						{
							FixupFirstLefts1(outRec, outRec2);
						}
					}
				}
				else
				{
					outRec2.Pts = null;
					outRec2.BottomPt = null;
					outRec2.Idx = outRec.Idx;
					outRec.IsHole = outRec3.IsHole;
					if (outRec3 == outRec2)
					{
						outRec.FirstLeft = outRec2.FirstLeft;
					}
					outRec2.FirstLeft = outRec;
					if (m_UsingPolyTree)
					{
						FixupFirstLefts3(outRec2, outRec);
					}
				}
			}
		}

		private void UpdateOutPtIdxs(global::UnityEngine.Rendering.Universal.OutRec outrec)
		{
			global::UnityEngine.Rendering.Universal.OutPt outPt = outrec.Pts;
			do
			{
				outPt.Idx = outrec.Idx;
				outPt = outPt.Prev;
			}
			while (outPt != outrec.Pts);
		}

		private void DoSimplePolygons()
		{
			int num = 0;
			while (num < m_PolyOuts.Count)
			{
				global::UnityEngine.Rendering.Universal.OutRec outRec = m_PolyOuts[num++];
				global::UnityEngine.Rendering.Universal.OutPt outPt = outRec.Pts;
				if (outPt == null || outRec.IsOpen)
				{
					continue;
				}
				do
				{
					for (global::UnityEngine.Rendering.Universal.OutPt outPt2 = outPt.Next; outPt2 != outRec.Pts; outPt2 = outPt2.Next)
					{
						if (outPt.Pt == outPt2.Pt && outPt2.Next != outPt && outPt2.Prev != outPt)
						{
							global::UnityEngine.Rendering.Universal.OutPt prev = outPt.Prev;
							(outPt.Prev = outPt2.Prev).Next = outPt;
							outPt2.Prev = prev;
							prev.Next = outPt2;
							outRec.Pts = outPt;
							global::UnityEngine.Rendering.Universal.OutRec outRec2 = CreateOutRec();
							outRec2.Pts = outPt2;
							UpdateOutPtIdxs(outRec2);
							if (Poly2ContainsPoly1(outRec2.Pts, outRec.Pts))
							{
								outRec2.IsHole = !outRec.IsHole;
								outRec2.FirstLeft = outRec;
								if (m_UsingPolyTree)
								{
									FixupFirstLefts2(outRec2, outRec);
								}
							}
							else if (Poly2ContainsPoly1(outRec.Pts, outRec2.Pts))
							{
								outRec2.IsHole = outRec.IsHole;
								outRec.IsHole = !outRec2.IsHole;
								outRec2.FirstLeft = outRec.FirstLeft;
								outRec.FirstLeft = outRec2;
								if (m_UsingPolyTree)
								{
									FixupFirstLefts2(outRec, outRec2);
								}
							}
							else
							{
								outRec2.IsHole = outRec.IsHole;
								outRec2.FirstLeft = outRec.FirstLeft;
								if (m_UsingPolyTree)
								{
									FixupFirstLefts1(outRec, outRec2);
								}
							}
							outPt2 = outPt;
						}
					}
					outPt = outPt.Next;
				}
				while (outPt != outRec.Pts);
			}
		}

		public static double Area(global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> poly)
		{
			int count = poly.Count;
			if (count < 3)
			{
				return 0.0;
			}
			double num = 0.0;
			int i = 0;
			int index = count - 1;
			for (; i < count; i++)
			{
				num += ((double)poly[index].X + (double)poly[i].X) * ((double)poly[index].Y - (double)poly[i].Y);
				index = i;
			}
			return (0.0 - num) * 0.5;
		}

		internal double Area(global::UnityEngine.Rendering.Universal.OutRec outRec)
		{
			return Area(outRec.Pts);
		}

		internal double Area(global::UnityEngine.Rendering.Universal.OutPt op)
		{
			global::UnityEngine.Rendering.Universal.OutPt outPt = op;
			if (op == null)
			{
				return 0.0;
			}
			double num = 0.0;
			do
			{
				num += (double)(op.Prev.Pt.X + op.Pt.X) * (double)(op.Prev.Pt.Y - op.Pt.Y);
				op = op.Next;
			}
			while (op != outPt);
			return num * 0.5;
		}

		public static global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>> SimplifyPolygon(global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> poly, global::UnityEngine.Rendering.Universal.PolyFillTypes fillType = global::UnityEngine.Rendering.Universal.PolyFillTypes.pftEvenOdd)
		{
			global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>> list = new global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>>();
			global::UnityEngine.Rendering.Universal.Clipper clipper = new global::UnityEngine.Rendering.Universal.Clipper();
			clipper.StrictlySimple = true;
			clipper.AddPath(poly, global::UnityEngine.Rendering.Universal.PolyTypes.ptSubject, Closed: true);
			clipper.Execute(global::UnityEngine.Rendering.Universal.ClipTypes.ctUnion, list, fillType, fillType);
			return list;
		}

		public static global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>> SimplifyPolygons(global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>> polys, global::UnityEngine.Rendering.Universal.PolyFillTypes fillType = global::UnityEngine.Rendering.Universal.PolyFillTypes.pftEvenOdd)
		{
			global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>> list = new global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>>();
			global::UnityEngine.Rendering.Universal.Clipper clipper = new global::UnityEngine.Rendering.Universal.Clipper();
			clipper.StrictlySimple = true;
			clipper.AddPaths(polys, global::UnityEngine.Rendering.Universal.PolyTypes.ptSubject, closed: true);
			clipper.Execute(global::UnityEngine.Rendering.Universal.ClipTypes.ctUnion, list, fillType, fillType);
			return list;
		}

		private static double DistanceSqrd(global::UnityEngine.Rendering.Universal.IntPoint pt1, global::UnityEngine.Rendering.Universal.IntPoint pt2)
		{
			double num = (double)pt1.X - (double)pt2.X;
			double num2 = (double)pt1.Y - (double)pt2.Y;
			return num * num + num2 * num2;
		}

		private static double DistanceFromLineSqrd(global::UnityEngine.Rendering.Universal.IntPoint pt, global::UnityEngine.Rendering.Universal.IntPoint ln1, global::UnityEngine.Rendering.Universal.IntPoint ln2)
		{
			double num = ln1.Y - ln2.Y;
			double num2 = ln2.X - ln1.X;
			double num3 = num * (double)ln1.X + num2 * (double)ln1.Y;
			num3 = num * (double)pt.X + num2 * (double)pt.Y - num3;
			return num3 * num3 / (num * num + num2 * num2);
		}

		private static bool SlopesNearCollinear(global::UnityEngine.Rendering.Universal.IntPoint pt1, global::UnityEngine.Rendering.Universal.IntPoint pt2, global::UnityEngine.Rendering.Universal.IntPoint pt3, double distSqrd)
		{
			if (global::System.Math.Abs(pt1.X - pt2.X) > global::System.Math.Abs(pt1.Y - pt2.Y))
			{
				if (pt1.X > pt2.X == pt1.X < pt3.X)
				{
					return DistanceFromLineSqrd(pt1, pt2, pt3) < distSqrd;
				}
				if (pt2.X > pt1.X == pt2.X < pt3.X)
				{
					return DistanceFromLineSqrd(pt2, pt1, pt3) < distSqrd;
				}
				return DistanceFromLineSqrd(pt3, pt1, pt2) < distSqrd;
			}
			if (pt1.Y > pt2.Y == pt1.Y < pt3.Y)
			{
				return DistanceFromLineSqrd(pt1, pt2, pt3) < distSqrd;
			}
			if (pt2.Y > pt1.Y == pt2.Y < pt3.Y)
			{
				return DistanceFromLineSqrd(pt2, pt1, pt3) < distSqrd;
			}
			return DistanceFromLineSqrd(pt3, pt1, pt2) < distSqrd;
		}

		private static bool PointsAreClose(global::UnityEngine.Rendering.Universal.IntPoint pt1, global::UnityEngine.Rendering.Universal.IntPoint pt2, double distSqrd)
		{
			double num = (double)pt1.X - (double)pt2.X;
			double num2 = (double)pt1.Y - (double)pt2.Y;
			return num * num + num2 * num2 <= distSqrd;
		}

		private static global::UnityEngine.Rendering.Universal.OutPt ExcludeOp(global::UnityEngine.Rendering.Universal.OutPt op)
		{
			global::UnityEngine.Rendering.Universal.OutPt prev = op.Prev;
			prev.Next = op.Next;
			op.Next.Prev = prev;
			prev.Idx = 0;
			return prev;
		}

		public static global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> CleanPolygon(global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> path, double distance = 1.415)
		{
			int num = path.Count;
			if (num == 0)
			{
				return new global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>();
			}
			global::UnityEngine.Rendering.Universal.OutPt[] array = new global::UnityEngine.Rendering.Universal.OutPt[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = new global::UnityEngine.Rendering.Universal.OutPt();
			}
			for (int j = 0; j < num; j++)
			{
				array[j].Pt = path[j];
				array[j].Next = array[(j + 1) % num];
				array[j].Next.Prev = array[j];
				array[j].Idx = 0;
			}
			double distSqrd = distance * distance;
			global::UnityEngine.Rendering.Universal.OutPt outPt = array[0];
			while (outPt.Idx == 0 && outPt.Next != outPt.Prev)
			{
				if (PointsAreClose(outPt.Pt, outPt.Prev.Pt, distSqrd))
				{
					outPt = ExcludeOp(outPt);
					num--;
				}
				else if (PointsAreClose(outPt.Prev.Pt, outPt.Next.Pt, distSqrd))
				{
					ExcludeOp(outPt.Next);
					outPt = ExcludeOp(outPt);
					num -= 2;
				}
				else if (SlopesNearCollinear(outPt.Prev.Pt, outPt.Pt, outPt.Next.Pt, distSqrd))
				{
					outPt = ExcludeOp(outPt);
					num--;
				}
				else
				{
					outPt.Idx = 1;
					outPt = outPt.Next;
				}
			}
			if (num < 3)
			{
				num = 0;
			}
			global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> list = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>(num);
			for (int k = 0; k < num; k++)
			{
				list.Add(outPt.Pt);
				outPt = outPt.Next;
			}
			array = null;
			return list;
		}

		public static global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>> CleanPolygons(global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>> polys, double distance = 1.415)
		{
			global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>> list = new global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>>(polys.Count);
			for (int i = 0; i < polys.Count; i++)
			{
				list.Add(CleanPolygon(polys[i], distance));
			}
			return list;
		}

		internal static global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>> Minkowski(global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> pattern, global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> path, bool IsSum, bool IsClosed)
		{
			int num = (IsClosed ? 1 : 0);
			int count = pattern.Count;
			int count2 = path.Count;
			global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>> list = new global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>>(count2);
			if (IsSum)
			{
				for (int i = 0; i < count2; i++)
				{
					global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> list2 = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>(count);
					for (int j = 0; j < pattern.Count; j++)
					{
						global::UnityEngine.Rendering.Universal.IntPoint intPoint = pattern[j];
						list2.Add(new global::UnityEngine.Rendering.Universal.IntPoint(path[i].X + intPoint.X, path[i].Y + intPoint.Y));
					}
					list.Add(list2);
				}
			}
			else
			{
				for (int k = 0; k < count2; k++)
				{
					global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> list3 = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>(count);
					for (int l = 0; l < pattern.Count; l++)
					{
						global::UnityEngine.Rendering.Universal.IntPoint intPoint2 = pattern[l];
						list3.Add(new global::UnityEngine.Rendering.Universal.IntPoint(path[k].X - intPoint2.X, path[k].Y - intPoint2.Y));
					}
					list.Add(list3);
				}
			}
			global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>> list4 = new global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>>((count2 + num) * (count + 1));
			for (int m = 0; m < count2 - 1 + num; m++)
			{
				for (int n = 0; n < count; n++)
				{
					global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> list5 = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>(4);
					list5.Add(list[m % count2][n % count]);
					list5.Add(list[(m + 1) % count2][n % count]);
					list5.Add(list[(m + 1) % count2][(n + 1) % count]);
					list5.Add(list[m % count2][(n + 1) % count]);
					if (!Orientation(list5))
					{
						list5.Reverse();
					}
					list4.Add(list5);
				}
			}
			return list4;
		}

		public static global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>> MinkowskiSum(global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> pattern, global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> path, bool pathIsClosed)
		{
			global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>> list = Minkowski(pattern, path, IsSum: true, pathIsClosed);
			global::UnityEngine.Rendering.Universal.Clipper clipper = new global::UnityEngine.Rendering.Universal.Clipper();
			clipper.AddPaths(list, global::UnityEngine.Rendering.Universal.PolyTypes.ptSubject, closed: true);
			clipper.Execute(global::UnityEngine.Rendering.Universal.ClipTypes.ctUnion, list, global::UnityEngine.Rendering.Universal.PolyFillTypes.pftNonZero, global::UnityEngine.Rendering.Universal.PolyFillTypes.pftNonZero);
			return list;
		}

		private static global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> TranslatePath(global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> path, global::UnityEngine.Rendering.Universal.IntPoint delta)
		{
			global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> list = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>(path.Count);
			for (int i = 0; i < path.Count; i++)
			{
				list.Add(new global::UnityEngine.Rendering.Universal.IntPoint(path[i].X + delta.X, path[i].Y + delta.Y));
			}
			return list;
		}

		public static global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>> MinkowskiSum(global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> pattern, global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>> paths, bool pathIsClosed)
		{
			global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>> list = new global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>>();
			global::UnityEngine.Rendering.Universal.Clipper clipper = new global::UnityEngine.Rendering.Universal.Clipper();
			for (int i = 0; i < paths.Count; i++)
			{
				global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>> ppg = Minkowski(pattern, paths[i], IsSum: true, pathIsClosed);
				clipper.AddPaths(ppg, global::UnityEngine.Rendering.Universal.PolyTypes.ptSubject, closed: true);
				if (pathIsClosed)
				{
					global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> pg = TranslatePath(paths[i], pattern[0]);
					clipper.AddPath(pg, global::UnityEngine.Rendering.Universal.PolyTypes.ptClip, Closed: true);
				}
			}
			clipper.Execute(global::UnityEngine.Rendering.Universal.ClipTypes.ctUnion, list, global::UnityEngine.Rendering.Universal.PolyFillTypes.pftNonZero, global::UnityEngine.Rendering.Universal.PolyFillTypes.pftNonZero);
			return list;
		}

		public static global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>> MinkowskiDiff(global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> poly1, global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> poly2)
		{
			global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>> list = Minkowski(poly1, poly2, IsSum: false, IsClosed: true);
			global::UnityEngine.Rendering.Universal.Clipper clipper = new global::UnityEngine.Rendering.Universal.Clipper();
			clipper.AddPaths(list, global::UnityEngine.Rendering.Universal.PolyTypes.ptSubject, closed: true);
			clipper.Execute(global::UnityEngine.Rendering.Universal.ClipTypes.ctUnion, list, global::UnityEngine.Rendering.Universal.PolyFillTypes.pftNonZero, global::UnityEngine.Rendering.Universal.PolyFillTypes.pftNonZero);
			return list;
		}

		public static global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>> PolyTreeToPaths(global::UnityEngine.Rendering.Universal.PolyTree polytree)
		{
			global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>> list = new global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>>();
			list.Capacity = polytree.Total;
			AddPolyNodeToPaths(polytree, global::UnityEngine.Rendering.Universal.Clipper.NodeType.ntAny, list);
			return list;
		}

		internal static void AddPolyNodeToPaths(global::UnityEngine.Rendering.Universal.PolyNode polynode, global::UnityEngine.Rendering.Universal.Clipper.NodeType nt, global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>> paths)
		{
			bool flag = true;
			switch (nt)
			{
			case global::UnityEngine.Rendering.Universal.Clipper.NodeType.ntOpen:
				return;
			case global::UnityEngine.Rendering.Universal.Clipper.NodeType.ntClosed:
				flag = !polynode.IsOpen;
				break;
			}
			if (polynode.m_polygon.Count > 0 && flag)
			{
				paths.Add(polynode.m_polygon);
			}
			foreach (global::UnityEngine.Rendering.Universal.PolyNode child in polynode.Childs)
			{
				AddPolyNodeToPaths(child, nt, paths);
			}
		}

		public static global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>> OpenPathsFromPolyTree(global::UnityEngine.Rendering.Universal.PolyTree polytree)
		{
			global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>> list = new global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>>();
			list.Capacity = polytree.ChildCount;
			for (int i = 0; i < polytree.ChildCount; i++)
			{
				if (polytree.Childs[i].IsOpen)
				{
					list.Add(polytree.Childs[i].m_polygon);
				}
			}
			return list;
		}

		public static global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>> ClosedPathsFromPolyTree(global::UnityEngine.Rendering.Universal.PolyTree polytree)
		{
			global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>> list = new global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>>();
			list.Capacity = polytree.Total;
			AddPolyNodeToPaths(polytree, global::UnityEngine.Rendering.Universal.Clipper.NodeType.ntClosed, list);
			return list;
		}
	}
}
