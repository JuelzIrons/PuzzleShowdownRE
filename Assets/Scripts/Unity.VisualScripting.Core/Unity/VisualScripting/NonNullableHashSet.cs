namespace Unity.VisualScripting
{
	public class NonNullableHashSet<T> : global::Unity.VisualScripting.ISet<T>, global::System.Collections.Generic.ICollection<T>, global::System.Collections.Generic.IEnumerable<T>, global::System.Collections.IEnumerable
	{
		private readonly global::System.Collections.Generic.HashSet<T> set;

		public int Count => set.Count;

		public bool IsReadOnly => false;

		public NonNullableHashSet()
		{
			set = new global::System.Collections.Generic.HashSet<T>();
		}

		public NonNullableHashSet(global::System.Collections.Generic.IEqualityComparer<T> comparer)
		{
			set = new global::System.Collections.Generic.HashSet<T>(comparer);
		}

		public NonNullableHashSet(global::System.Collections.Generic.IEnumerable<T> collection)
		{
			set = new global::System.Collections.Generic.HashSet<T>(collection);
		}

		public NonNullableHashSet(global::System.Collections.Generic.IEnumerable<T> collection, global::System.Collections.Generic.IEqualityComparer<T> comparer)
		{
			set = new global::System.Collections.Generic.HashSet<T>(collection, comparer);
		}

		public bool Add(T item)
		{
			if (item == null)
			{
				throw new global::System.ArgumentNullException("item");
			}
			return set.Add(item);
		}

		public void Clear()
		{
			set.Clear();
		}

		public bool Contains(T item)
		{
			if (item == null)
			{
				throw new global::System.ArgumentNullException("item");
			}
			return set.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			set.CopyTo(array, arrayIndex);
		}

		public void ExceptWith(global::System.Collections.Generic.IEnumerable<T> other)
		{
			set.ExceptWith(other);
		}

		public global::System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			return set.GetEnumerator();
		}

		public void IntersectWith(global::System.Collections.Generic.IEnumerable<T> other)
		{
			set.IntersectWith(other);
		}

		public bool IsProperSubsetOf(global::System.Collections.Generic.IEnumerable<T> other)
		{
			return set.IsProperSubsetOf(other);
		}

		public bool IsProperSupersetOf(global::System.Collections.Generic.IEnumerable<T> other)
		{
			return set.IsProperSupersetOf(other);
		}

		public bool IsSubsetOf(global::System.Collections.Generic.IEnumerable<T> other)
		{
			return set.IsSubsetOf(other);
		}

		public bool IsSupersetOf(global::System.Collections.Generic.IEnumerable<T> other)
		{
			return set.IsSupersetOf(other);
		}

		public bool Overlaps(global::System.Collections.Generic.IEnumerable<T> other)
		{
			return set.Overlaps(other);
		}

		public bool Remove(T item)
		{
			if (item == null)
			{
				throw new global::System.ArgumentNullException("item");
			}
			return set.Remove(item);
		}

		public bool SetEquals(global::System.Collections.Generic.IEnumerable<T> other)
		{
			return set.SetEquals(other);
		}

		public void SymmetricExceptWith(global::System.Collections.Generic.IEnumerable<T> other)
		{
			set.SymmetricExceptWith(other);
		}

		public void UnionWith(global::System.Collections.Generic.IEnumerable<T> other)
		{
			set.UnionWith(other);
		}

		void global::System.Collections.Generic.ICollection<T>.Add(T item)
		{
			if (item == null)
			{
				throw new global::System.ArgumentNullException("item");
			}
			((global::System.Collections.Generic.ICollection<T>)set).Add(item);
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			return ((global::System.Collections.IEnumerable)set).GetEnumerator();
		}
	}
}
