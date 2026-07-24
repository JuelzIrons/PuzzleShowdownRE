namespace UnityEngine.Rendering
{
	public static class HashSetPool<T>
	{
		private static readonly global::UnityEngine.Rendering.ObjectPool<global::System.Collections.Generic.HashSet<T>> s_Pool = new global::UnityEngine.Rendering.ObjectPool<global::System.Collections.Generic.HashSet<T>>(null, delegate(global::System.Collections.Generic.HashSet<T> l)
		{
			l.Clear();
		});

		public static global::System.Collections.Generic.HashSet<T> Get()
		{
			return s_Pool.Get();
		}

		public static global::UnityEngine.Rendering.ObjectPool<global::System.Collections.Generic.HashSet<T>>.PooledObject Get(out global::System.Collections.Generic.HashSet<T> value)
		{
			return s_Pool.Get(out value);
		}

		public static void Release(global::System.Collections.Generic.HashSet<T> toRelease)
		{
			s_Pool.Release(toRelease);
		}
	}
}
