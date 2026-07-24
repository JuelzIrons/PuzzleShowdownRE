namespace Unity.VisualScripting
{
	public static class XHashSetPool
	{
		public static global::System.Collections.Generic.HashSet<T> ToHashSetPooled<T>(this global::System.Collections.Generic.IEnumerable<T> source)
		{
			global::System.Collections.Generic.HashSet<T> hashSet = global::Unity.VisualScripting.HashSetPool<T>.New();
			foreach (T item in source)
			{
				hashSet.Add(item);
			}
			return hashSet;
		}

		public static void Free<T>(this global::System.Collections.Generic.HashSet<T> hashSet)
		{
			global::Unity.VisualScripting.HashSetPool<T>.Free(hashSet);
		}
	}
}
