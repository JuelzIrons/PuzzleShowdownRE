namespace UnityEngine.U2D.Common.UTess
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	internal struct DelaEdgeCompare : global::System.Collections.Generic.IComparer<global::Unity.Mathematics.int4>
	{
		public int Compare(global::Unity.Mathematics.int4 a, global::Unity.Mathematics.int4 b)
		{
			int num = a.x - b.x;
			if (num != 0)
			{
				return num;
			}
			num = a.y - b.y;
			if (num != 0)
			{
				return num;
			}
			num = a.z - b.z;
			if (num != 0)
			{
				return num;
			}
			return a.w - b.w;
		}
	}
}
