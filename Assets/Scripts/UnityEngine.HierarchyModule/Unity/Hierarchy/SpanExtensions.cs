namespace Unity.Hierarchy
{
	internal static class SpanExtensions
	{
		public static bool Contains<T>(this in global::System.ReadOnlySpan<T> span, T value) where T : global::System.IEquatable<T>
		{
			for (int i = 0; i < span.Length; i++)
			{
				if (span[i].Equals(value))
				{
					return true;
				}
			}
			return false;
		}
	}
}
