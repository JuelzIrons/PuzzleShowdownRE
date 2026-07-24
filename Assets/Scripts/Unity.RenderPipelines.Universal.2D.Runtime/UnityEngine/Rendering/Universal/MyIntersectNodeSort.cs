namespace UnityEngine.Rendering.Universal
{
	internal class MyIntersectNodeSort : global::System.Collections.Generic.IComparer<global::UnityEngine.Rendering.Universal.IntersectNode>
	{
		public int Compare(global::UnityEngine.Rendering.Universal.IntersectNode node1, global::UnityEngine.Rendering.Universal.IntersectNode node2)
		{
			long num = node2.Pt.Y - node1.Pt.Y;
			if (num > 0)
			{
				return 1;
			}
			if (num < 0)
			{
				return -1;
			}
			return 0;
		}
	}
}
