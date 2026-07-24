namespace UnityEngine.InputSystem.Utilities
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	public struct Vector3MagnitudeComparer : global::System.Collections.Generic.IComparer<global::UnityEngine.Vector3>
	{
		public int Compare(global::UnityEngine.Vector3 x, global::UnityEngine.Vector3 y)
		{
			float sqrMagnitude = x.sqrMagnitude;
			float sqrMagnitude2 = y.sqrMagnitude;
			if (sqrMagnitude < sqrMagnitude2)
			{
				return -1;
			}
			if (sqrMagnitude > sqrMagnitude2)
			{
				return 1;
			}
			return 0;
		}
	}
}
