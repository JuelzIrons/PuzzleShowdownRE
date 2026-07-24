namespace UnityEngine.Rendering
{
	public static class ListPool<T>
	{
		private static readonly global::UnityEngine.Rendering.ObjectPool<global::System.Collections.Generic.List<T>> s_Pool = new global::UnityEngine.Rendering.ObjectPool<global::System.Collections.Generic.List<T>>(null, delegate(global::System.Collections.Generic.List<T> l)
		{
			l.Clear();
		});

		public static global::System.Collections.Generic.List<T> Get()
		{
			return s_Pool.Get();
		}

		public static global::UnityEngine.Rendering.ObjectPool<global::System.Collections.Generic.List<T>>.PooledObject Get(out global::System.Collections.Generic.List<T> value)
		{
			return s_Pool.Get(out value);
		}

		public static void Release(global::System.Collections.Generic.List<T> toRelease)
		{
			s_Pool.Release(toRelease);
		}
	}
}
