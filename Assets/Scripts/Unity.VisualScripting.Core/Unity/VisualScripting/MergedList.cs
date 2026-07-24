namespace Unity.VisualScripting
{
	public class MergedList<T> : global::Unity.VisualScripting.IMergedCollection<T>, global::System.Collections.Generic.ICollection<T>, global::System.Collections.Generic.IEnumerable<T>, global::System.Collections.IEnumerable
	{
		public struct Enumerator : global::System.Collections.Generic.IEnumerator<T>, global::System.Collections.IEnumerator, global::System.IDisposable
		{
			private global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Collections.Generic.IList<T>>.Enumerator listsEnumerator;

			private T currentItem;

			private global::System.Collections.Generic.IList<T> currentList;

			private int indexInCurrentList;

			private bool exceeded;

			public T Current => currentItem;

			object global::System.Collections.IEnumerator.Current
			{
				get
				{
					if (exceeded)
					{
						throw new global::System.InvalidOperationException();
					}
					return Current;
				}
			}

			public Enumerator(global::Unity.VisualScripting.MergedList<T> merged)
			{
				this = default(global::Unity.VisualScripting.MergedList<T>.Enumerator);
				listsEnumerator = merged.lists.GetEnumerator();
			}

			public void Dispose()
			{
			}

			public bool MoveNext()
			{
				if (currentList == null)
				{
					if (!listsEnumerator.MoveNext())
					{
						currentItem = default(T);
						exceeded = true;
						return false;
					}
					currentList = listsEnumerator.Current.Value;
					if (currentList == null)
					{
						throw new global::System.InvalidOperationException("Merged sub list is null.");
					}
				}
				if (indexInCurrentList < currentList.Count)
				{
					currentItem = currentList[indexInCurrentList];
					indexInCurrentList++;
					return true;
				}
				while (listsEnumerator.MoveNext())
				{
					currentList = listsEnumerator.Current.Value;
					indexInCurrentList = 0;
					if (currentList == null)
					{
						throw new global::System.InvalidOperationException("Merged sub list is null.");
					}
					if (indexInCurrentList < currentList.Count)
					{
						currentItem = currentList[indexInCurrentList];
						indexInCurrentList++;
						return true;
					}
				}
				currentItem = default(T);
				exceeded = true;
				return false;
			}

			void global::System.Collections.IEnumerator.Reset()
			{
				throw new global::System.InvalidOperationException();
			}
		}

		protected readonly global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Collections.Generic.IList<T>> lists;

		public int Count
		{
			get
			{
				int num = 0;
				foreach (global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Collections.Generic.IList<T>> list in lists)
				{
					num += list.Value.Count;
				}
				return num;
			}
		}

		public bool IsReadOnly => false;

		public MergedList()
		{
			lists = new global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Collections.Generic.IList<T>>();
		}

		public virtual void Include<TI>(global::System.Collections.Generic.IList<TI> list) where TI : T
		{
			lists.Add(typeof(TI), new global::Unity.VisualScripting.VariantList<T, TI>(list));
		}

		public bool Includes<TI>() where TI : T
		{
			return Includes(typeof(TI));
		}

		public bool Includes(global::System.Type elementType)
		{
			return GetListForType(elementType, throwOnFail: false) != null;
		}

		public global::System.Collections.Generic.IList<TI> ForType<TI>() where TI : T
		{
			return ((global::Unity.VisualScripting.VariantList<T, TI>)GetListForType(typeof(TI))).implementation;
		}

		protected global::System.Collections.Generic.IList<T> GetListForItem(T item)
		{
			global::Unity.VisualScripting.Ensure.That("item").IsNotNull(item);
			return GetListForType(item.GetType());
		}

		protected global::System.Collections.Generic.IList<T> GetListForType(global::System.Type type, bool throwOnFail = true)
		{
			if (lists.ContainsKey(type))
			{
				return lists[type];
			}
			foreach (global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Collections.Generic.IList<T>> list in lists)
			{
				if (list.Key.IsAssignableFrom(type))
				{
					return list.Value;
				}
			}
			if (throwOnFail)
			{
				throw new global::System.InvalidOperationException($"No sub-collection available for type '{type}'.");
			}
			return null;
		}

		public bool Contains(T item)
		{
			return GetListForItem(item).Contains(item);
		}

		public virtual void Add(T item)
		{
			GetListForItem(item).Add(item);
		}

		public virtual void Clear()
		{
			foreach (global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Collections.Generic.IList<T>> list in lists)
			{
				list.Value.Clear();
			}
		}

		public virtual bool Remove(T item)
		{
			return GetListForItem(item).Remove(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
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
			int num = 0;
			foreach (global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Collections.Generic.IList<T>> list in lists)
			{
				global::System.Collections.Generic.IList<T> value = list.Value;
				value.CopyTo(array, arrayIndex + num);
				num += value.Count;
			}
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		global::System.Collections.Generic.IEnumerator<T> global::System.Collections.Generic.IEnumerable<T>.GetEnumerator()
		{
			return GetEnumerator();
		}

		public global::Unity.VisualScripting.MergedList<T>.Enumerator GetEnumerator()
		{
			return new global::Unity.VisualScripting.MergedList<T>.Enumerator(this);
		}
	}
}
