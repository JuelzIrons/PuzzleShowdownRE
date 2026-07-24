namespace TMPro
{
	internal static class TMP_ListPool<T>
	{
		private static readonly global::TMPro.TMP_ObjectPool<global::System.Collections.Generic.List<T>> s_ListPool = new global::TMPro.TMP_ObjectPool<global::System.Collections.Generic.List<T>>(null, delegate(global::System.Collections.Generic.List<T> l)
		{
			l.Clear();
		});

		public static global::System.Collections.Generic.List<T> Get()
		{
			return s_ListPool.Get();
		}

		public static void Release(global::System.Collections.Generic.List<T> toRelease)
		{
			s_ListPool.Release(toRelease);
		}
	}
}
