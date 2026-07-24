namespace Unity.VisualScripting
{
	public class NonNullableList<T> : global::System.Collections.Generic.IList<T>, global::System.Collections.Generic.ICollection<T>, global::System.Collections.Generic.IEnumerable<T>, global::System.Collections.IEnumerable, global::System.Collections.IList, global::System.Collections.ICollection
	{
		private readonly global::System.Collections.Generic.List<T> list;

		public T this[int index]
		{
			get
			{
				return list[index];
			}
			set
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				list[index] = value;
			}
		}

		object global::System.Collections.IList.this[int index]
		{
			get
			{
				return ((global::System.Collections.IList)list)[index];
			}
			set
			{
				((global::System.Collections.IList)list)[index] = value;
			}
		}

		public int Count => list.Count;

		public bool IsSynchronized => ((global::System.Collections.ICollection)list).IsSynchronized;

		public object SyncRoot => ((global::System.Collections.ICollection)list).SyncRoot;

		public bool IsReadOnly => false;

		public bool IsFixedSize => ((global::System.Collections.IList)list).IsFixedSize;

		public NonNullableList()
		{
			list = new global::System.Collections.Generic.List<T>();
		}

		public NonNullableList(int capacity)
		{
			list = new global::System.Collections.Generic.List<T>(capacity);
		}

		public NonNullableList(global::System.Collections.Generic.IEnumerable<T> collection)
		{
			list = new global::System.Collections.Generic.List<T>(collection);
		}

		public void CopyTo(global::System.Array array, int index)
		{
			((global::System.Collections.ICollection)list).CopyTo(array, index);
		}

		public void Add(T item)
		{
			if (item == null)
			{
				throw new global::System.ArgumentNullException("item");
			}
			list.Add(item);
		}

		public int Add(object value)
		{
			return ((global::System.Collections.IList)list).Add(value);
		}

		public void Clear()
		{
			list.Clear();
		}

		public bool Contains(object value)
		{
			return ((global::System.Collections.IList)list).Contains(value);
		}

		public int IndexOf(object value)
		{
			return ((global::System.Collections.IList)list).IndexOf(value);
		}

		public void Insert(int index, object value)
		{
			((global::System.Collections.IList)list).Insert(index, value);
		}

		public void Remove(object value)
		{
			((global::System.Collections.IList)list).Remove(value);
		}

		public bool Contains(T item)
		{
			if (item == null)
			{
				throw new global::System.ArgumentNullException("item");
			}
			return list.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			list.CopyTo(array, arrayIndex);
		}

		public global::System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			return list.GetEnumerator();
		}

		public int IndexOf(T item)
		{
			if (item == null)
			{
				throw new global::System.ArgumentNullException("item");
			}
			return list.IndexOf(item);
		}

		public void Insert(int index, T item)
		{
			if (item == null)
			{
				throw new global::System.ArgumentNullException("item");
			}
			list.Insert(index, item);
		}

		public bool Remove(T item)
		{
			if (item == null)
			{
				throw new global::System.ArgumentNullException("item");
			}
			return list.Remove(item);
		}

		public void RemoveAt(int index)
		{
			list.RemoveAt(index);
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			return list.GetEnumerator();
		}

		public void AddRange(global::System.Collections.Generic.IEnumerable<T> collection)
		{
			foreach (T item in collection)
			{
				Add(item);
			}
		}
	}
}
