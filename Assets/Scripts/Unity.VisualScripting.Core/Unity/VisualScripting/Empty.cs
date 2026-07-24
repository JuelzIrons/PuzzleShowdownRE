namespace Unity.VisualScripting
{
	public static class Empty<T>
	{
		public static readonly T[] array = new T[0];

		public static readonly global::System.Collections.Generic.List<T> list = new global::System.Collections.Generic.List<T>(0);

		public static readonly global::System.Collections.Generic.HashSet<T> hashSet = new global::System.Collections.Generic.HashSet<T>();
	}
}
