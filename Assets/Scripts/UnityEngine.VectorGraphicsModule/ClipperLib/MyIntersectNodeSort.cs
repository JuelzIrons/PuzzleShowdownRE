namespace ClipperLib
{
	internal class MyIntersectNodeSort : global::System.Collections.Generic.IComparer<global::ClipperLib.IntersectNode>
	{
		public int Compare(global::ClipperLib.IntersectNode node1, global::ClipperLib.IntersectNode node2)
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
