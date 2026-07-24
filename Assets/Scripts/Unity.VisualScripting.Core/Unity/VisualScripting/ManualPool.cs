namespace Unity.VisualScripting
{
	public static class ManualPool<T> where T : class
	{
		private static readonly object @lock = new object();

		private static readonly global::System.Collections.Generic.Stack<T> free = new global::System.Collections.Generic.Stack<T>();

		private static readonly global::System.Collections.Generic.HashSet<T> busy = new global::System.Collections.Generic.HashSet<T>();

		public static T New(global::System.Func<T> constructor)
		{
			lock (@lock)
			{
				if (free.Count == 0)
				{
					free.Push(constructor());
				}
				T val = free.Pop();
				busy.Add(val);
				return val;
			}
		}

		public static void Free(T item)
		{
			lock (@lock)
			{
				if (!busy.Contains(item))
				{
					throw new global::System.ArgumentException("The item to free is not in use by the pool.", "item");
				}
				busy.Remove(item);
				free.Push(item);
			}
		}
	}
}
