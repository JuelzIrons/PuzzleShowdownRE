namespace Unity.VisualScripting
{
	public class VariantCollection<TBase, TImplementation> : global::System.Collections.Generic.ICollection<TBase>, global::System.Collections.Generic.IEnumerable<TBase>, global::System.Collections.IEnumerable where TImplementation : TBase
	{
		public global::System.Collections.Generic.ICollection<TImplementation> implementation { get; private set; }

		public int Count => implementation.Count;

		public bool IsReadOnly => implementation.IsReadOnly;

		public VariantCollection(global::System.Collections.Generic.ICollection<TImplementation> implementation)
		{
			if (implementation == null)
			{
				throw new global::System.ArgumentNullException("implementation");
			}
			this.implementation = implementation;
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public global::System.Collections.Generic.IEnumerator<TBase> GetEnumerator()
		{
			foreach (TImplementation item in implementation)
			{
				yield return (TBase)(object)item;
			}
		}

		public void Add(TBase item)
		{
			if (!(item is TImplementation))
			{
				throw new global::System.NotSupportedException();
			}
			implementation.Add((TImplementation)(object)item);
		}

		public void Clear()
		{
			implementation.Clear();
		}

		public bool Contains(TBase item)
		{
			if (!(item is TImplementation))
			{
				throw new global::System.NotSupportedException();
			}
			return implementation.Contains((TImplementation)(object)item);
		}

		public bool Remove(TBase item)
		{
			if (!(item is TImplementation))
			{
				throw new global::System.NotSupportedException();
			}
			return implementation.Remove((TImplementation)(object)item);
		}

		public void CopyTo(TBase[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new global::System.ArgumentNullException("array");
			}
			if (arrayIndex < 0)
			{
				throw new global::System.ArgumentOutOfRangeException("arrayIndex");
			}
			if (array.Length - arrayIndex < Count)
			{
				throw new global::System.ArgumentException();
			}
			TImplementation[] array2 = new TImplementation[Count];
			implementation.CopyTo(array2, 0);
			for (int i = 0; i < Count; i++)
			{
				array[i + arrayIndex] = (TBase)(object)array2[i];
			}
		}
	}
}
