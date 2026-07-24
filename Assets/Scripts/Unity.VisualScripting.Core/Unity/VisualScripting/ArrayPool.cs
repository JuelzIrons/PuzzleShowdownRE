namespace Unity.VisualScripting
{
	public static class ArrayPool<T>
	{
		private static readonly object @lock = new object();

		private static readonly global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.Stack<T[]>> free = new global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.Stack<T[]>>();

		private static readonly global::System.Collections.Generic.HashSet<T[]> busy = new global::System.Collections.Generic.HashSet<T[]>();

		public static T[] New(int length)
		{
			lock (@lock)
			{
				if (!free.ContainsKey(length))
				{
					free.Add(length, new global::System.Collections.Generic.Stack<T[]>());
				}
				if (free[length].Count == 0)
				{
					free[length].Push(new T[length]);
				}
				T[] array = free[length].Pop();
				busy.Add(array);
				return array;
			}
		}

		public static void Free(T[] array)
		{
			lock (@lock)
			{
				if (!busy.Contains(array))
				{
					throw new global::System.ArgumentException("The array to free is not in use by the pool.", "array");
				}
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = default(T);
				}
				busy.Remove(array);
				free[array.Length].Push(array);
			}
		}
	}
}
