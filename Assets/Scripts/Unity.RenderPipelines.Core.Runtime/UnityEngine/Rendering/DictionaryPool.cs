namespace UnityEngine.Rendering
{
	public static class DictionaryPool<TKey, TValue>
	{
		private static readonly global::UnityEngine.Rendering.ObjectPool<global::System.Collections.Generic.Dictionary<TKey, TValue>> s_Pool = new global::UnityEngine.Rendering.ObjectPool<global::System.Collections.Generic.Dictionary<TKey, TValue>>(null, delegate(global::System.Collections.Generic.Dictionary<TKey, TValue> l)
		{
			l.Clear();
		});

		public static global::System.Collections.Generic.Dictionary<TKey, TValue> Get()
		{
			return s_Pool.Get();
		}

		public static global::UnityEngine.Rendering.ObjectPool<global::System.Collections.Generic.Dictionary<TKey, TValue>>.PooledObject Get(out global::System.Collections.Generic.Dictionary<TKey, TValue> value)
		{
			return s_Pool.Get(out value);
		}

		public static void Release(global::System.Collections.Generic.Dictionary<TKey, TValue> toRelease)
		{
			s_Pool.Release(toRelease);
		}
	}
}
