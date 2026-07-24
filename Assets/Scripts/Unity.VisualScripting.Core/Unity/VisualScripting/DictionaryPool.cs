namespace Unity.VisualScripting
{
	public static class DictionaryPool<TKey, TValue>
	{
		private static readonly object @lock = new object();

		private static readonly global::System.Collections.Generic.Stack<global::System.Collections.Generic.Dictionary<TKey, TValue>> free = new global::System.Collections.Generic.Stack<global::System.Collections.Generic.Dictionary<TKey, TValue>>();

		private static readonly global::System.Collections.Generic.HashSet<global::System.Collections.Generic.Dictionary<TKey, TValue>> busy = new global::System.Collections.Generic.HashSet<global::System.Collections.Generic.Dictionary<TKey, TValue>>();

		public static global::System.Collections.Generic.Dictionary<TKey, TValue> New(global::System.Collections.Generic.Dictionary<TKey, TValue> source = null)
		{
			lock (@lock)
			{
				if (free.Count == 0)
				{
					free.Push(new global::System.Collections.Generic.Dictionary<TKey, TValue>());
				}
				global::System.Collections.Generic.Dictionary<TKey, TValue> dictionary = free.Pop();
				busy.Add(dictionary);
				if (source != null)
				{
					foreach (global::System.Collections.Generic.KeyValuePair<TKey, TValue> item in source)
					{
						dictionary.Add(item.Key, item.Value);
					}
				}
				return dictionary;
			}
		}

		public static void Free(global::System.Collections.Generic.Dictionary<TKey, TValue> dictionary)
		{
			lock (@lock)
			{
				if (!busy.Contains(dictionary))
				{
					throw new global::System.ArgumentException("The dictionary to free is not in use by the pool.", "dictionary");
				}
				dictionary.Clear();
				busy.Remove(dictionary);
				free.Push(dictionary);
			}
		}
	}
}
