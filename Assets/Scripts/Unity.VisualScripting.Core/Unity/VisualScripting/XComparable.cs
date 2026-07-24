namespace Unity.VisualScripting
{
	internal static class XComparable
	{
		internal static bool IsLt<T>(this global::System.IComparable<T> x, T y)
		{
			return x.CompareTo(y) < 0;
		}

		internal static bool IsEq<T>(this global::System.IComparable<T> x, T y)
		{
			return x.CompareTo(y) == 0;
		}

		internal static bool IsGt<T>(this global::System.IComparable<T> x, T y)
		{
			return x.CompareTo(y) > 0;
		}
	}
}
