namespace Unity.VisualScripting
{
	public interface ISet<T> : global::System.Collections.Generic.ICollection<T>, global::System.Collections.Generic.IEnumerable<T>, global::System.Collections.IEnumerable
	{
		new bool Add(T item);

		void UnionWith(global::System.Collections.Generic.IEnumerable<T> other);

		void IntersectWith(global::System.Collections.Generic.IEnumerable<T> other);

		void ExceptWith(global::System.Collections.Generic.IEnumerable<T> other);

		void SymmetricExceptWith(global::System.Collections.Generic.IEnumerable<T> other);

		bool IsSubsetOf(global::System.Collections.Generic.IEnumerable<T> other);

		bool IsSupersetOf(global::System.Collections.Generic.IEnumerable<T> other);

		bool IsProperSubsetOf(global::System.Collections.Generic.IEnumerable<T> other);

		bool IsProperSupersetOf(global::System.Collections.Generic.IEnumerable<T> other);

		bool Overlaps(global::System.Collections.Generic.IEnumerable<T> other);

		bool SetEquals(global::System.Collections.Generic.IEnumerable<T> other);
	}
}
