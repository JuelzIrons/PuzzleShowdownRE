namespace Unity.VisualScripting
{
	public class ReferenceEqualityComparer : global::System.Collections.Generic.IEqualityComparer<object>
	{
		public static readonly global::Unity.VisualScripting.ReferenceEqualityComparer Instance = new global::Unity.VisualScripting.ReferenceEqualityComparer();

		private ReferenceEqualityComparer()
		{
		}

		bool global::System.Collections.Generic.IEqualityComparer<object>.Equals(object a, object b)
		{
			return a == b;
		}

		int global::System.Collections.Generic.IEqualityComparer<object>.GetHashCode(object a)
		{
			return GetHashCode(a);
		}

		public static int GetHashCode(object a)
		{
			return global::System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(a);
		}
	}
	public class ReferenceEqualityComparer<T> : global::System.Collections.Generic.IEqualityComparer<T>
	{
		public static readonly global::Unity.VisualScripting.ReferenceEqualityComparer<T> Instance = new global::Unity.VisualScripting.ReferenceEqualityComparer<T>();

		private ReferenceEqualityComparer()
		{
		}

		bool global::System.Collections.Generic.IEqualityComparer<T>.Equals(T a, T b)
		{
			return (object)a == (object)b;
		}

		int global::System.Collections.Generic.IEqualityComparer<T>.GetHashCode(T a)
		{
			return GetHashCode(a);
		}

		public static int GetHashCode(T a)
		{
			return global::System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(a);
		}
	}
}
