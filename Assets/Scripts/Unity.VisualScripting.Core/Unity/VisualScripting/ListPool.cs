namespace Unity.VisualScripting
{
	public static class ListPool<T>
	{
		private static readonly object @lock = new object();

		private static readonly global::System.Collections.Generic.Stack<global::System.Collections.Generic.List<T>> free = new global::System.Collections.Generic.Stack<global::System.Collections.Generic.List<T>>();

		private static readonly global::System.Collections.Generic.HashSet<global::System.Collections.Generic.List<T>> busy = new global::System.Collections.Generic.HashSet<global::System.Collections.Generic.List<T>>();

		public static global::System.Collections.Generic.List<T> New()
		{
			lock (@lock)
			{
				if (free.Count == 0)
				{
					free.Push(new global::System.Collections.Generic.List<T>());
				}
				global::System.Collections.Generic.List<T> list = free.Pop();
				busy.Add(list);
				return list;
			}
		}

		public static void Free(global::System.Collections.Generic.List<T> list)
		{
			lock (@lock)
			{
				if (!busy.Contains(list))
				{
					throw new global::System.ArgumentException("The list to free is not in use by the pool.", "list");
				}
				list.Clear();
				busy.Remove(list);
				free.Push(list);
			}
		}
	}
}
