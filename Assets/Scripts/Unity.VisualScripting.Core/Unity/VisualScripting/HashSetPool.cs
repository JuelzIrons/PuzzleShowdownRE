namespace Unity.VisualScripting
{
	public static class HashSetPool<T>
	{
		private static readonly object @lock = new object();

		private static readonly global::System.Collections.Generic.Stack<global::System.Collections.Generic.HashSet<T>> free = new global::System.Collections.Generic.Stack<global::System.Collections.Generic.HashSet<T>>();

		private static readonly global::System.Collections.Generic.HashSet<global::System.Collections.Generic.HashSet<T>> busy = new global::System.Collections.Generic.HashSet<global::System.Collections.Generic.HashSet<T>>();

		public static global::System.Collections.Generic.HashSet<T> New()
		{
			lock (@lock)
			{
				if (free.Count == 0)
				{
					free.Push(new global::System.Collections.Generic.HashSet<T>());
				}
				global::System.Collections.Generic.HashSet<T> hashSet = free.Pop();
				busy.Add(hashSet);
				return hashSet;
			}
		}

		public static void Free(global::System.Collections.Generic.HashSet<T> hashSet)
		{
			lock (@lock)
			{
				if (!busy.Remove(hashSet))
				{
					throw new global::System.ArgumentException("The hash set to free is not in use by the pool.", "hashSet");
				}
				hashSet.Clear();
				free.Push(hashSet);
			}
		}
	}
}
