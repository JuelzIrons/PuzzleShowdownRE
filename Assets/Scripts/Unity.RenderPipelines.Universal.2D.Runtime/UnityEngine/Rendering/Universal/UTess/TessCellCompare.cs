namespace UnityEngine.Rendering.Universal.UTess
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	internal struct TessCellCompare : global::System.Collections.Generic.IComparer<global::Unity.Mathematics.int3>
	{
		public int Compare(global::Unity.Mathematics.int3 a, global::Unity.Mathematics.int3 b)
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
			return a.z - b.z;
		}
	}
}
