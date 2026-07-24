namespace Unity.VisualScripting
{
	public static class GenericPool<T> where T : class, global::Unity.VisualScripting.IPoolable
	{
		private static readonly object @lock = new object();

		private static readonly global::System.Collections.Generic.Stack<T> free = new global::System.Collections.Generic.Stack<T>();

		private static readonly global::System.Collections.Generic.HashSet<T> busy = new global::System.Collections.Generic.HashSet<T>(global::Unity.VisualScripting.ReferenceEqualityComparer<T>.Instance);

		public static T New(global::System.Func<T> constructor)
		{
			lock (@lock)
			{
				if (free.Count == 0)
				{
					free.Push(constructor());
				}
				T val = free.Pop();
				val.New();
				busy.Add(val);
				return val;
			}
		}

		public static void Free(T item)
		{
			lock (@lock)
			{
				if (!busy.Remove(item))
				{
					throw new global::System.ArgumentException("The item to free is not in use by the pool.", "item");
				}
				item.Free();
				free.Push(item);
			}
		}
	}
}
