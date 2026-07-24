namespace ClipperLib
{
	internal class TEdge
	{
		internal global::ClipperLib.IntPoint Bot;

		internal global::ClipperLib.IntPoint Curr;

		internal global::ClipperLib.IntPoint Top;

		internal global::ClipperLib.IntPoint Delta;

		internal double Dx;

		internal global::ClipperLib.PolyType PolyTyp;

		internal global::ClipperLib.EdgeSide Side;

		internal int WindDelta;

		internal int WindCnt;

		internal int WindCnt2;

		internal int OutIdx;

		internal global::ClipperLib.TEdge Next;

		internal global::ClipperLib.TEdge Prev;

		internal global::ClipperLib.TEdge NextInLML;

		internal global::ClipperLib.TEdge NextInAEL;

		internal global::ClipperLib.TEdge PrevInAEL;

		internal global::ClipperLib.TEdge NextInSEL;

		internal global::ClipperLib.TEdge PrevInSEL;
	}
}
