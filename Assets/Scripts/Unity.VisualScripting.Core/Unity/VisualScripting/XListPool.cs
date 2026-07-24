namespace Unity.VisualScripting
{
	public static class XListPool
	{
		public static global::System.Collections.Generic.List<T> ToListPooled<T>(this global::System.Collections.Generic.IEnumerable<T> source)
		{
			global::System.Collections.Generic.List<T> list = global::Unity.VisualScripting.ListPool<T>.New();
			foreach (T item in source)
			{
				list.Add(item);
			}
			return list;
		}

		public static void Free<T>(this global::System.Collections.Generic.List<T> list)
		{
			global::Unity.VisualScripting.ListPool<T>.Free(list);
		}
	}
}
