namespace Unity.VisualScripting
{
	public static class XArrayPool
	{
		public static T[] ToArrayPooled<T>(this global::System.Collections.Generic.IEnumerable<T> source)
		{
			T[] array = global::Unity.VisualScripting.ArrayPool<T>.New(global::System.Linq.Enumerable.Count(source));
			int num = 0;
			foreach (T item in source)
			{
				array[num++] = item;
			}
			return array;
		}

		public static void Free<T>(this T[] array)
		{
			global::Unity.VisualScripting.ArrayPool<T>.Free(array);
		}
	}
}
