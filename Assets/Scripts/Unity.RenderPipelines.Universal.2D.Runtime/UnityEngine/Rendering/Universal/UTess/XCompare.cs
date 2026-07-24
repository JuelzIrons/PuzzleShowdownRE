namespace UnityEngine.Rendering.Universal.UTess
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	internal struct XCompare : global::System.Collections.Generic.IComparer<double>
	{
		public int Compare(double a, double b)
		{
			if (!(a < b))
			{
				return 1;
			}
			return -1;
		}
	}
}
