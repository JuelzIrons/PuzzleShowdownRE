namespace UnityEngine.Rendering.Universal
{
	internal class TEdge
	{
		internal global::UnityEngine.Rendering.Universal.IntPoint Bot;

		internal global::UnityEngine.Rendering.Universal.IntPoint Curr;

		internal global::UnityEngine.Rendering.Universal.IntPoint Top;

		internal global::UnityEngine.Rendering.Universal.IntPoint Delta;

		internal double Dx;

		internal global::UnityEngine.Rendering.Universal.PolyTypes PolyTyp;

		internal global::UnityEngine.Rendering.Universal.EdgeSides Side;

		internal int WindDelta;

		internal int WindCnt;

		internal int WindCnt2;

		internal int OutIdx;

		internal global::UnityEngine.Rendering.Universal.TEdge Next;

		internal global::UnityEngine.Rendering.Universal.TEdge Prev;

		internal global::UnityEngine.Rendering.Universal.TEdge NextInLML;

		internal global::UnityEngine.Rendering.Universal.TEdge NextInAEL;

		internal global::UnityEngine.Rendering.Universal.TEdge PrevInAEL;

		internal global::UnityEngine.Rendering.Universal.TEdge NextInSEL;

		internal global::UnityEngine.Rendering.Universal.TEdge PrevInSEL;
	}
}
