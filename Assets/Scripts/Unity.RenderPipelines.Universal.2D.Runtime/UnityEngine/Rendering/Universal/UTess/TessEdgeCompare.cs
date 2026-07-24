namespace UnityEngine.Rendering.Universal.UTess
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	internal struct TessEdgeCompare : global::System.Collections.Generic.IComparer<global::Unity.Mathematics.int2>
	{
		public int Compare(global::Unity.Mathematics.int2 a, global::Unity.Mathematics.int2 b)
		{
			int num = a.x - b.x;
			if (num != 0)
			{
				return num;
			}
			return a.y - b.y;
		}
	}
}
