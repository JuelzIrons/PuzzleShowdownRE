namespace UnityEngine.U2D.Common.UTess
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	internal struct TessEventCompare : global::System.Collections.Generic.IComparer<global::UnityEngine.U2D.Common.UTess.UEvent>
	{
		public int Compare(global::UnityEngine.U2D.Common.UTess.UEvent a, global::UnityEngine.U2D.Common.UTess.UEvent b)
		{
			float num = a.a.x - b.a.x;
			if (0f != num)
			{
				if (!(num > 0f))
				{
					return -1;
				}
				return 1;
			}
			num = a.a.y - b.a.y;
			if (0f != num)
			{
				if (!(num > 0f))
				{
					return -1;
				}
				return 1;
			}
			int num2 = a.type - b.type;
			if (num2 != 0)
			{
				return num2;
			}
			if (a.type != 0)
			{
				float num3 = global::UnityEngine.U2D.Common.UTess.ModuleHandle.OrientFast(a.a, a.b, b.b);
				if (0f != num3)
				{
					if (!(num3 > 0f))
					{
						return -1;
					}
					return 1;
				}
			}
			return a.idx - b.idx;
		}
	}
}
